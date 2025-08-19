using System; // Console
using System.IO; // File, StreamWriter, StreamReader

Console.WriteLine("=== ファイル入出力のデモ ==="); // 見出しの表示

string path = Path.Combine(Path.GetTempPath(), "csharp_study_sample.txt"); // 一時ディレクトリ上のパスを作成
Console.WriteLine($"書き込み先: {path}"); // パス表示

// using 宣言で自動的にリソースを解放する
using (var writer = new StreamWriter(path, append: false)) // 新規に作成して書き込み
{
    writer.WriteLine("1行目: こんにちは"); // 1行目を書き込む
    writer.WriteLine("2行目: C# の I/O を学習中"); // 2行目を書き込む
}

// 読み込み（全行をまとめて）
string text = File.ReadAllText(path); // ファイル全体を文字列として読む
Console.WriteLine("=== 読み込み結果 ==="); // 区切り
Console.WriteLine(text); // 読んだ内容を表示

// 追記（append=true）
using (var writer = new StreamWriter(path, append: true)) // 追記モードで開く
{
    writer.WriteLine($"3行目: 時刻 {DateTime.Now:HH:mm:ss}"); // 現在時刻を追記
}

// 行単位で読み込み
string[] lines = File.ReadAllLines(path); // 各行を配列で取得
Console.WriteLine("=== 行単位 ==="); // 見出し
for (int i = 0; i < lines.Length; i++) // インデックスで回す
{
    Console.WriteLine($"[{i}] {lines[i]}"); // 行番号と内容を表示
}

Console.WriteLine("=== 終了 ==="); // 終了メッセージ


