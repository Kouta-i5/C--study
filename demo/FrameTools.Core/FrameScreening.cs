using OpenCvSharp;

namespace FrameTools.Core;

public static class FrameScreening
{
    // ブレ/ボケの指標（Laplacian分散）
    public static double SharpScore(string jpgPath)
    {
        using var src = Cv2.ImRead(jpgPath, ImreadModes.Grayscale);
        using var lap = new Mat();
        Cv2.Laplacian(src, lap, MatType.CV_64F);
        Cv2.MeanStdDev(lap, out _, out var std);
        return std.Val0 * std.Val0;
    }

    // 類似フレーム判定（ヒストグラム相関）
    public static bool IsSimilar(string a, string b, double threshold = 0.95)
    {
        using var imgA = Cv2.ImRead(a, ImreadModes.Grayscale);
        using var imgB = Cv2.ImRead(b, ImreadModes.Grayscale);
        using var histA = new Mat(); using var histB = new Mat();

        int[] channels = { 0 };
        int[] histSize = { 64 };
        Rangef[] ranges = { new Rangef(0, 256) };
        Cv2.CalcHist(new[] { imgA }, channels, null, histA, 1, histSize, ranges);
        Cv2.CalcHist(new[] { imgB }, channels, null, histB, 1, histSize, ranges);
        Cv2.Normalize(histA, histA); Cv2.Normalize(histB, histB);

        var corr = Cv2.CompareHist(histA, histB, HistCompMethods.Correl);
        return corr >= threshold;
    }
}
