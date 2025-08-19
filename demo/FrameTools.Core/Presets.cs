using System.Text.Json;

namespace FrameTools.Core;

public sealed class AppPresets
{
    public List<CropPreset> ExtractPresets { get; set; } = new();
    public List<MaskPreset>  MaskPresets   { get; set; } = new();

    public static AppPresets Load(string path)
        => File.Exists(path)
           ? JsonSerializer.Deserialize<AppPresets>(File.ReadAllText(path)) ?? new()
           : new();

    public void Save(string path)
        => File.WriteAllText(path, JsonSerializer.Serialize(this, new JsonSerializerOptions{ WriteIndented = true }));
}

public sealed class CropPreset { public string Name { get; set; } = "256"; public int Width { get; set; } = 256; public int Height { get; set; } = 256; }
public sealed class MaskPreset { public string Name { get; set; } = "PatientID"; public List<RectSpec> Rects { get; set; } = new(); }
public sealed class RectSpec { public int X { get; set; } public int Y { get; set; } public int W { get; set; } public int H { get; set; } }
