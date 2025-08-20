using System; // Console, Environment など
using System.Diagnostics; // Process 等（CLIは不使用だが計測などに）
using System.Globalization; // 数値パースで使用
using System.IO; // ファイル入出力
using System.Linq; // LINQ
using System.Collections.Generic; // List
using System.Runtime.InteropServices; // ネイティブライブラリ事前ロード
using SixLabors.ImageSharp; // 画像読み書き
using SixLabors.ImageSharp.PixelFormats; // Rgba32 など
using SixLabors.ImageSharp.Processing; // 画像処理（塗りつぶし）
using SixLabors.ImageSharp.Drawing.Processing; // 図形描画拡張
using Tesseract; // OCR ライブラリ

// 使い方: dotnet run -- <inputImagePath> [--lang jpn] [--tessdata ./tessdata] [--out masked.png] [--print-only]
Console.WriteLine("=== demo_1: 画像のOCRと自動マスク ==="); // 見出しの表示

// 引数の読み取り
var argsList = args.ToList(); // 可変処理しやすくする

string inputPath = argsList.FirstOrDefault(a => !a.StartsWith("--")) ?? string.Empty; // 最初の位置引数を画像パスとする
if (string.IsNullOrWhiteSpace(inputPath) || !File.Exists(inputPath)) // 入力チェック
{
    Console.WriteLine("使い方: dotnet run -- <inputImagePath> [--lang jpn] [--tesseract /usr/local/bin/tesseract] [--out masked.png]"); // ガイド
    Environment.Exit(1); // 終了
}

string lang = GetOption(argsList, "--lang", defaultValue: "eng"); // 言語（デフォルト英語）
string tessdataDirOption = GetOption(argsList, "--tessdata", defaultValue: string.Empty); // ユーザー指定 tessdata パス
// 出力先ディレクトリ（カレント直下の outputs）を用意
string outputsDir = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "outputs"); // outputs ディレクトリのパス
Directory.CreateDirectory(outputsDir); // なければ作成
string outputPath = GetOption(argsList, "--out", defaultValue: System.IO.Path.Combine(outputsDir, System.IO.Path.GetFileNameWithoutExtension(inputPath) + ".masked.png")); // 出力
bool printOnly = argsList.Contains("--print-only"); // 解析結果だけ表示して終了するか

Console.WriteLine($"入力: {inputPath}"); // 入力パス
Console.WriteLine($"言語: {lang}"); // OCR 言語
string resolvedTessdata = ResolveTessdataDir(tessdataDirOption); // 実際に使用する tessdata パス
Console.WriteLine($"tessdata: {resolvedTessdata}"); // 言語データパス
// 複数言語 (jpn+eng) にも対応したチェック
foreach (var l in lang.Split('+'))
{
    var td = System.IO.Path.Combine(resolvedTessdata, $"{l}.traineddata");
    if (!File.Exists(td))
    {
        Console.WriteLine($"注意: {l}.traineddata が {resolvedTessdata} に見つかりません。");
    }
}
Environment.SetEnvironmentVariable("TESSDATA_PREFIX", resolvedTessdata, EnvironmentVariableTarget.Process);
Console.WriteLine($"出力: {outputPath}"); // 出力パス

try // メイン処理
{
    // macOS(Homebrew) のネイティブdylibを事前ロード（arm64想定）
    PreloadNativeLibraries();
    // Tesseract.NET のネイティブ検索パスを追加（区切りはコロン）
    TesseractEnviornment.CustomSearchPath = "/opt/homebrew/lib:/usr/local/lib";
    // ライブラリで OCR 実施し単語レベルの矩形とテキストを取得（ライブラリ専用）
    var boxes = OcrViaLibrary(inputPath, lang, resolvedTessdata); // 位置とテキスト
    Console.WriteLine($"検出テキストボックス数: {boxes.Count}"); // 数を表示

    if (printOnly) // 表示のみで終了
    {
        Console.WriteLine("left\ttop\twidth\theight\ttext"); // ヘッダ
        foreach (var b in boxes)
        {
            var text = b.Text.Replace("\t", " ").Replace("\r", " ").Replace("\n", " "); // タブ/改行を空白に
            Console.WriteLine($"{b.X}\t{b.Y}\t{b.Width}\t{b.Height}\t{text}"); // 1 行出力
        }
        return; // マスクせずに終了
    }
    //TSVとは、タブ区切りのテキストファイルです。

    // 画像を読み込み、検出領域を黒塗り
    using Image<Rgba32> image = Image.Load<Rgba32>(inputPath); // 画像を読み込む
    foreach (var b in boxes) // すべてのボックスに対して
    {
        var rect = new SixLabors.ImageSharp.Rectangle(b.X, b.Y, b.Width, b.Height); // 矩形を作成
        image.Mutate(ctx => ctx.Fill(Color.Black, rect)); // 黒で塗りつぶし
    }

    // 書き出し
    image.Save(outputPath); // ファイルに保存
    Console.WriteLine("マスク済み画像を書き出しました。"); // 完了メッセージ
}
catch (Exception ex) // エラー時
{
    Console.WriteLine($"エラー: {ex.Message}"); // 概要
    if (ex.InnerException != null) Console.WriteLine($"Inner: {ex.InnerException.Message}"); // 内部例外
    Console.WriteLine(ex.ToString()); // 詳細スタック
    Environment.Exit(2); // 異常終了
}
// 一時ファイルの後始末は不要（ライブラリ利用時）

Console.WriteLine("=== 完了 ==="); // 終了メッセージ

// ここから下はヘルパ
static string GetOption(List<string> args, string key, string defaultValue) // オプション値の取得
{
    int idx = args.IndexOf(key); // キーの位置
    if (idx >= 0 && idx + 1 < args.Count) // 値が存在するか
    {
        return args[idx + 1]; // 取得
    }
    return defaultValue; // 既定値
}

static string ResolveTessdataDir(string userSpecified)
{
    // 優先順: ユーザー指定 → 環境変数 TESSDATA_PREFIX → ローカル ./tessdata → よくあるパス候補
    if (!string.IsNullOrWhiteSpace(userSpecified) && Directory.Exists(userSpecified)) return userSpecified;
    var env = Environment.GetEnvironmentVariable("TESSDATA_PREFIX");
    if (!string.IsNullOrWhiteSpace(env) && Directory.Exists(env)) return env;
    var local = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "tessdata");
    if (Directory.Exists(local)) return local;
    // macOS/Homebrew や Linux の代表的な配置先
    var candidates = new[]
    {
        "/opt/homebrew/share/tessdata",
        "/usr/local/share/tessdata",
        "/usr/share/tesseract-ocr/5/tessdata",
        "/usr/share/tesseract-ocr/4.00/tessdata",
        "/usr/share/tessdata"
    };
    foreach (var c in candidates) if (Directory.Exists(c)) return c;
    return local; // 最後はローカルを返す（存在しない場合は上で警告が出る）
}

static List<OcrBox> OcrViaLibrary(string inputPath, string lang, string tessdataDir)
{
    var boxes = new List<OcrBox>(); // 結果リスト
    using var img = Pix.LoadFromFile(inputPath); // 画像を Pix として読み込み
    using var engine = string.IsNullOrWhiteSpace(tessdataDir)
        ? new TesseractEngine("./tessdata", lang, EngineMode.Default)
        : new TesseractEngine(tessdataDir, lang, EngineMode.Default);

    using var page = engine.Process(img); // OCR 実行
    using var iter = page.GetIterator(); // 結果イテレータ
    iter.Begin(); // 先頭に移動
    do
    {
        if (iter.TryGetBoundingBox(PageIteratorLevel.Word, out var rect)) // 単語レベルの矩形
        {
            string text = iter.GetText(PageIteratorLevel.Word) ?? string.Empty; // 単語テキスト
            text = text.Trim(); // 前後空白を除去
            if (text.Length == 0) continue; // 空はスキップ
            boxes.Add(new OcrBox(rect.X1, rect.Y1, rect.Width, rect.Height, text)); // 追加
        }
    } while (iter.Next(PageIteratorLevel.Word)); // 次へ

    return boxes; // 返す
}

static void RunTesseractTsv(string tesseractPath, string inputPath, string outTsv, string lang) // tesseract 実行（CLI）
{
    // tesseract <in> stdout --oem 1 --psm 6 -l <lang> tsv > out.tsv
    var psi = new ProcessStartInfo
    {
        FileName = tesseractPath,
        ArgumentList = { inputPath, "stdout", "--oem", "1", "--psm", "6", "-l", lang, "tsv" },
        RedirectStandardOutput = true,
        RedirectStandardError = true,
        UseShellExecute = false,
        CreateNoWindow = true
    };
    using var proc = Process.Start(psi)!;
    using var sw = new StreamWriter(outTsv, false);
    proc.OutputDataReceived += (_, e) => { if (e.Data != null) sw.WriteLine(e.Data); };
    proc.BeginOutputReadLine();
    string stderr = proc.StandardError.ReadToEnd();
    proc.WaitForExit();
    sw.Flush();
    if (proc.ExitCode != 0)
    {
        throw new InvalidOperationException($"tesseract CLI 実行に失敗しました: {stderr}");
    }
}

static List<OcrBox> ParseTsv(string tsvPath) // TSV 解析
{
    var boxes = new List<OcrBox>();
    var lines = File.ReadAllLines(tsvPath);
    if (lines.Length <= 1) return boxes;
    // ヘッダ: level	page_num	block_num	par_num	line_num	word_num	left	top	width	height	conf	text
    foreach (var line in lines.Skip(1))
    {
        if (string.IsNullOrWhiteSpace(line)) continue;
        var cols = line.Split('\t');
        if (cols.Length < 12) continue;
        if (!int.TryParse(cols[0], out int level) || level != 5) continue; // 単語レベル
        if (!int.TryParse(cols[6], out int left)) continue;
        if (!int.TryParse(cols[7], out int top)) continue;
        if (!int.TryParse(cols[8], out int width)) continue;
        if (!int.TryParse(cols[9], out int height)) continue;
        string text = cols[11];
        if (string.IsNullOrWhiteSpace(text)) continue;
        boxes.Add(new OcrBox(left, top, width, height, text));
    }
    return boxes;
}

static void TryDelete(string path) // 安全な削除
{
    try { if (File.Exists(path)) File.Delete(path); } catch { /* 無視 */ }
}

static void PreloadNativeLibraries()
{
    if (!OperatingSystem.IsMacOS()) return; // 今回は macOS を想定
    var searchDirs = new[] { "/opt/homebrew/lib", "/usr/local/lib" };
    var leptonicaNames = new[] { "libleptonica-1.84.0.dylib", "libleptonica-1.83.1.dylib", "libleptonica-1.82.0.dylib", "libleptonica.dylib" };
    var tesseractNames = new[] { "libtesseract.5.dylib", "libtesseract.dylib" };

    var lep = FindFirstExisting(searchDirs, leptonicaNames);
    var tes = FindFirstExisting(searchDirs, tesseractNames);
    if (lep != null)
    {
        NativeLibrary.Load(lep);
        Console.WriteLine($"preload: {lep}");
    }
    if (tes != null)
    {
        NativeLibrary.Load(tes);
        Console.WriteLine($"preload: {tes}");
    }
}

static string? FindFirstExisting(IEnumerable<string> dirs, IEnumerable<string> fileNames)
{
    foreach (var d in dirs)
    {
        foreach (var f in fileNames)
        {
            var p = System.IO.Path.Combine(d, f);
            if (File.Exists(p)) return p;
        }
        try
        {
            var candidates = Directory.GetFiles(d, "lib*leptonica*.dylib");
            if (candidates.Length > 0) return candidates[0];
        }
        catch { }
    }
    return null;
}

record OcrBox(int X, int Y, int Width, int Height, string Text); // 位置とテキストを保持
