using System; // 基本的なクラスを使うための宣言

Console.WriteLine("=== 制御構文のデモ ==="); // 見出しの表示

// if / else if / else の例
int score = 78; // テストの点数を例として設定
if (score >= 90) // 90点以上なら
{
    Console.WriteLine("評価: S"); // 最高評価
}
else if (score >= 70) // 70点以上90点未満なら
{
    Console.WriteLine("評価: A"); // 良い評価
}
else // それ以外
{
    Console.WriteLine("評価: B"); // 標準的な評価
}

// switch 文の例
string day = "Wed"; // 曜日を表す短縮文字列
switch (day) // 値に応じて分岐
{
    case "Mon": // 月曜なら
        Console.WriteLine("月曜です"); // メッセージ表示
        break; // switch を抜ける
    case "Tue": // 火曜なら
    case "Wed": // または水曜なら
        Console.WriteLine("火曜または水曜です"); // 複数のケースをまとめる
        break; // 抜ける
    default: // どれにも当てはまらない場合
        Console.WriteLine("その他の曜日です"); // 既定の動作
        break; // 抜ける
}

// for ループの例
for (int i = 0; i < 3; i++) // i を 0 から 2 まで増やしながら繰り返す
{
    Console.WriteLine($"for: i = {i}"); // 現在の i を表示
}

// while ループの例
int count = 3; // 繰り返し回数の初期値
while (count > 0) // 条件が true の間繰り返す
{
    Console.WriteLine($"while: count = {count}"); // 現在の count を表示
    count--; // 1 減らす（無限ループ防止）
}

// foreach の例（配列を列挙）
string[] fruits = { "apple", "banana", "cherry" }; // 3 要素の文字列配列
foreach (var fruit in fruits) // 配列の各要素を順に取り出す
{
    Console.WriteLine($"fruit: {fruit}"); // 要素を表示
}

Console.WriteLine("=== 終了 ==="); // デモの終了メッセージ


