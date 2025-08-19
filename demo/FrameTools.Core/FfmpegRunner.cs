using System.Diagnostics;

namespace FrameTools.Core;

public sealed class FfmpegRunner
{
    private Process? _proc;
    public event Action<string>? OnLog;          // stderr 行
    public event Action<double>? OnProgress;     // 0..1（概算）
    public bool IsRunning => _proc != null && !_proc.HasExited;

    public async Task<int> RunAsync(string file, string args, CancellationToken ct)
    {
        var psi = new ProcessStartInfo
        {
            FileName = file,
            Arguments = args,
            UseShellExecute = false,
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };
        _proc = new Process { StartInfo = psi, EnableRaisingEvents = true };

        _proc.ErrorDataReceived += (_, e) =>
        {
            if (e.Data is null) return;
            OnLog?.Invoke(e.Data);

            // "time=00:00:12.34" のような進捗を拾う（総時間は別途セット）
            var idx = e.Data.IndexOf("time=");
            if (idx >= 0)
            {
                var part = e.Data[(idx + 5)..].Trim();
                var end = part.IndexOf(' ');
                var ts = (end > 0 ? part[..end] : part).Trim();
                if (TimeSpan.TryParse(ts, out var t) && _totalDurationSec > 0)
                    OnProgress?.Invoke(Math.Clamp(t.TotalSeconds / _totalDurationSec, 0, 1));
            }
        };

        _proc.Start();
        _proc.BeginErrorReadLine();

        await Task.Run(() =>
        {
            while (!_proc.HasExited)
            {
                if (ct.IsCancellationRequested)
                {
                    try { _proc.Kill(true); } catch { /* ignore */ }
                    break;
                }
                Thread.Sleep(50);
            }
        }, ct);

        var code = _proc.HasExited ? _proc.ExitCode : -1;
        _proc = null;
        return code;
    }

    private double _totalDurationSec;
    public void SetTotalDuration(double seconds) => _totalDurationSec = seconds;
}
