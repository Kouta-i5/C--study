using System.Globalization;
using System.Text;

namespace FrameTools.Core;

public static class ManifestWriter
{
    public static void Append(string manifestCsvPath, ManifestRow row)
    {
        var exists = File.Exists(manifestCsvPath);
        using var sw = new StreamWriter(manifestCsvPath, append: true, Encoding.UTF8);
        if (!exists)
        {
            sw.WriteLine("video_path,frame_path,sec,mm_ss_mmm,sharp_score,mask_json,filters_json,params_json");
        }
        string line = string.Join(",",
            Escape(row.VideoPath),
            Escape(row.FramePath),
            row.Seconds.ToString("0.###", CultureInfo.InvariantCulture),
            Escape(row.NameFragment),
            row.SharpScore.ToString("0.###", CultureInfo.InvariantCulture),
            Escape(row.MaskJson ?? "{}"),
            Escape(row.FiltersJson ?? "[]"),
            Escape(row.ParamsJson ?? "{}")
        );
        sw.WriteLine(line);
    }

    private static string Escape(string s)
        => $"\"{s.Replace("\"", "\"\"")}\"";
}

public sealed class ManifestRow
{
    public string VideoPath { get; init; } = "";
    public string FramePath { get; init; } = "";
    public double Seconds { get; init; }
    public string NameFragment { get; init; } = "";
    public double SharpScore { get; init; }
    public string? MaskJson { get; init; }
    public string? FiltersJson { get; init; }
    public string? ParamsJson { get; init; }
}
