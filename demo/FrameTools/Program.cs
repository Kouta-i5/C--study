using System.Diagnostics;
using FrameTools.Core;

static int Run(string file, string args)
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
    using var p = Process.Start(psi)!;
    p.WaitForExit();
    return p.ExitCode;
}

static IEnumerable<double> ProbePtsSeconds(string inputMp4)
{
    var psi = new ProcessStartInfo
    {
        FileName = "ffprobe",
        Arguments = $"-select_streams v:0 -show_frames -show_entries frame=pkt_pts_time -of csv \"{inputMp4}\"",
        UseShellExecute = false,
        RedirectStandardOutput = true,
        CreateNoWindow = true
    };
    using var p = Process.Start(psi)!;
    while (!p.StandardOutput.EndOfStream)
    {
        var line = p.StandardOutput.ReadLine();
        if (line is null) continue;
        var parts = line.Split(',');
        if (parts.Length >= 2 && double.TryParse(parts[1], out var sec))
            yield return sec;
    }
    p.WaitForExit();
}

static string TsName(double sec)
{
    var ts = TimeSpan.FromSeconds(sec);
    return $"{ts:mm\\_ss\\_fff}";
}

static Dictionary<string, string> ParseArgs(string[] args)
{
    var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    for (int i = 0; i < args.Length; i++)
    {
        if (args[i].StartsWith("--"))
        {
            var key = args[i].TrimStart('-');
            string val = "true";
            if (i + 1 < args.Length && !args[i + 1].StartsWith("--"))
            {
                val = args[i + 1]; i++;
            }
            dict[key] = val;
        }
    }
    return dict;
}

static void MainWork(string[] args)
{
    // ツール存在チェック
    try { Run("ffmpeg", "-version"); Run("ffprobe", "-version"); }
    catch { Console.Error.WriteLine("ffmpeg/ffprobe が見つかりません。PATHを確認してください。"); return; }

    var opt = ParseArgs(args);
    if (!opt.TryGetValue("input", out var input) || !File.Exists(input))
    {
        Console.WriteLine("使用例:");
        Console.WriteLine("  dotnet run -- --input /path/video.mp4 [--out out_dir] [--fps 2] [--start 00:01:00] [--end 00:02:00] [--sharp-th 180] [--sim-th 0.95]");
        return;
    }

    var outDir = opt.TryGetValue("out", out var o) ? o : Path.Combine(Directory.GetCurrentDirectory(), "out");
    Directory.CreateDirectory(outDir);

    var fps = opt.TryGetValue("fps", out var fpsStr) && double.TryParse(fpsStr, out var fpsVal) ? fpsVal : 2.0;
    var sharpTh = opt.TryGetValue("sharp-th", out var thStr) && double.TryParse(thStr, out var thVal) ? thVal : 180.0;
    var simTh = opt.TryGetValue("sim-th", out var simStr) && double.TryParse(simStr, out var simVal) ? simVal : 0.95;

    var ss = opt.TryGetValue("start", out var start) ? $"-ss {start} " : "";
    var to = opt.TryGetValue("end", out var end) ? $"-to {end} " : "";

    Console.WriteLine($"[設定] input={input}");
    Console.WriteLine($"       out={outDir}, fps={fps}, start={start}, end={end}, sharp-th={sharpTh}, sim-th={simTh}");

    // 1) フレーム抽出
    var pattern = Path.Combine(outDir, "frame_%06d.jpg");
    Console.WriteLine("1) 抽出...");
    var exit = Run("ffmpeg", $"-y {ss}{to}-i \"{input}\" -vf fps={fps} -an \"{pattern}\"");
    if (exit != 0) { Console.Error.WriteLine("ffmpeg 抽出エラー"); return; }

    // 2) 命名（理想時刻割当）
    Console.WriteLine("2) 命名...");
    var files = Directory.GetFiles(outDir, "frame_*.jpg").OrderBy(f => f).ToList();
    if (files.Count == 0) { Console.WriteLine("抽出0枚"); return; }

    // start/end 秒を計算
    double startSec = 0, endSec;
    if (opt.TryGetValue("start", out var startStr) && TimeSpan.TryParse(startStr, out var tsStart)) startSec = tsStart.TotalSeconds;
    if (opt.TryGetValue("end", out var endStr) && TimeSpan.TryParse(endStr, out var tsEnd)) endSec = tsEnd.TotalSeconds;
    else
    {
        // ffprobe で総時間取得
        endSec = GetDurationSeconds(input);
    }
    if (endSec <= startSec) endSec = GetDurationSeconds(input);

    var step = 1.0 / fps;
    var assignedTimes = new List<double>();
    double cur = startSec;
    for (int i = 0; i < files.Count; i++) { assignedTimes.Add(cur); cur += step; if (cur > endSec) break; }
    var n = Math.Min(files.Count, assignedTimes.Count);
    for (int i = 0; i < n; i++)
    {
        var newName = $"frame_{TsName(assignedTimes[i])}.jpg";
        var dst = Path.Combine(outDir, newName);
        File.Move(files[i], dst, overwrite: true);
        files[i] = dst;
    }
    for (int i = n; i < files.Count; i++) File.Delete(files[i]); // 余り削除

    // 3) スクリーニング＋manifest追記
    Console.WriteLine("3) スクリーニング＋manifest...");
    var manifest = Path.Combine(outDir, "manifest.csv");
    var kept = new List<string>();

    // 画質フィルタ
    foreach (var f in Directory.GetFiles(outDir, "frame_*.jpg").OrderBy(f => f))
    {
        var score = FrameScreening.SharpScore(f);
        if (score < sharpTh) { File.Delete(f); continue; }

        kept.Add(f);
        var sec = ParseSecondsFromName(Path.GetFileNameWithoutExtension(f));
        ManifestWriter.Append(manifest, new ManifestRow
        {
            VideoPath = input,
            FramePath = f,
            Seconds = sec,
            NameFragment = Path.GetFileNameWithoutExtension(f),
            SharpScore = score,
            ParamsJson = $"{{\"fps\":{fps},\"sharpTh\":{sharpTh}}}"
        });
    }

    // 類似間引き
    for (int i = 1; i < kept.Count; i++)
    {
        if (FrameScreening.IsSimilar(kept[i - 1], kept[i], simTh))
        {
            File.Delete(kept[i]);
            kept.RemoveAt(i);
            i--;
        }
    }

    Console.WriteLine($"done. kept={kept.Count}");
}

static double GetDurationSeconds(string input)
{
    var psi = new ProcessStartInfo
    {
        FileName = "ffprobe",
        Arguments = $"-v error -show_entries format=duration -of default=noprint_wrappers=1:nokey=1 \"{input}\"",
        UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true
    };
    using var p = Process.Start(psi)!;
    var s = p.StandardOutput.ReadToEnd().Trim();
    p.WaitForExit();
    return double.TryParse(s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var sec) ? sec : 0d;
}

static double ParseSecondsFromName(string name)
{
    // frame_01_23_120 → mm_ss_mmm
    var parts = name.Split('_').TakeLast(3).ToArray();
    if (parts.Length < 3) return 0;
    int mm = int.Parse(parts[0]); int ss = int.Parse(parts[1]); int ms = int.Parse(parts[2]);
    return mm * 60 + ss + ms / 1000.0;
}

MainWork(args);
