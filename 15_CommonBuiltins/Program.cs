using System; // Console, Math, Environment など
using System.IO; // Path, File
using System.Linq; // LINQ 基本

Console.WriteLine("=== よく使う組み込みAPIのデモ ==="); // 見出し

// string：基本操作
string s = " Hello, C# "; // 前後に空白のある文字列
Console.WriteLine(s.Trim()); // 前後の空白を除去
Console.WriteLine(s.ToUpper()); // 大文字化
Console.WriteLine(s.Replace("C#", ".NET")); // 置換
Console.WriteLine(string.Join(",", new[] { "a", "b", "c" })); // 連結

// Math：数値ユーティリティ
double v = -3.14; // 負の小数
Console.WriteLine(Math.Abs(v)); // 絶対値
Console.WriteLine(Math.Round(3.14159, 2)); // 小数第2位で丸め
Console.WriteLine(Math.Max(10, 20)); // 大きい方

// DateTime / TimeSpan：日時と差分
DateTime now = DateTime.Now; // 現在日時
DateTime tomorrow = now.AddDays(1); // 1日後
TimeSpan diff = tomorrow - now; // 差分
Console.WriteLine(now.ToString("yyyy-MM-dd HH:mm:ss")); // 書式化
Console.WriteLine($"差分(時間): {diff.TotalHours}"); // 総時間

// Random / Guid：乱数と一意ID
var rnd = new Random(); // 乱数生成器
Console.WriteLine(rnd.Next(1, 7)); // 1〜6 の乱数
Console.WriteLine(Guid.NewGuid()); // グローバル一意ID

// Convert / int.Parse / TryParse：変換
Console.WriteLine(Convert.ToInt32("123")); // 変換（例外あり）
if (int.TryParse("456", out int n)) // 失敗しないパターン
{
    Console.WriteLine(n); // 456
}

// Path / Environment：パス結合や環境情報
string p = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "sample.txt"); // デスクトップ配下
Console.WriteLine(p); // 結果パス
Console.WriteLine(Environment.OSVersion); // OS 情報

// LINQ：簡単なクエリ
int[] nums = { 5, 1, 4, 2, 3 }; // 配列
var sortedEvens = nums.Where(x => x % 2 == 0).OrderBy(x => x); // 偶数を昇順
Console.WriteLine(string.Join(", ", sortedEvens)); // 2, 4

Console.WriteLine("=== 終了 ==="); // 終了


