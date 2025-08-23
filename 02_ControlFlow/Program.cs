using System; // 基本的なクラスを使うための宣言

Console.WriteLine("=== 制御構文のデモ ==="); // 見出しの表示

// if / else if / else の例
// if (条件式) { 条件式がtrueの場合に実行する処理 } 
// else if (条件式) { 条件式がtrueの場合に実行する処理 } 
// else { 条件式がfalseの場合に実行する処理 }

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
string day = "Fri"; // 曜日を表す短縮文字列
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
// for (初期化; 条件式; 更新) { 条件式がtrueの場合に実行する処理 }
for (int i = 0; i < 10; i+=3) // i を 0 から 9 まで 3 ずつ増やしながら繰り返す
{
    Console.WriteLine($"for: i = {i}"); // 現在の i を表示
}

// while ループの例
int count = 3; // 繰り返し回数の初期値
while (count > 0) // 条件が true の間繰り返す
{
    Console.WriteLine($"while: count = {count}"); // 現在の count を表示
    count -= 1; // 1 減らす（無限ループ防止）,count--と同じ
}

// foreach の例（配列を列挙）
//string [] 変数名　= { 要素1, 要素2, 要素3 }
//for (var 変数名 in 配列名) { 配列の各要素を順に取り出す }
//var は型推論を行うためのキーワード,var は変数の型を自動的に推論するためのキーワード
string[] fruits = { "apple", "banana", "cherry" }; // 3 要素の文字列配列
foreach (var fruit in fruits) // 配列の各要素を順に取り出す
{
    Console.WriteLine($"fruit: {fruit}"); // 要素を表示
}

Console.WriteLine("=== 終了 ==="); // デモの終了メッセージ

//foreach の例(辞書を列挙)
//foreach (var 変数名 in 辞書名) { 辞書の各要素を順に取り出す }
//Dictionary<string, int> は辞書型の宣言,new Dictionary<string, int> は辞書型のインスタンスを作成
//Dictionary<string, int> 辞書名　= new Dictionary<string, int> { キー, 値 }
Dictionary<string, int> scores = new Dictionary<string, int> {
    { "Alice", 85 },
    { "Bob", 90 },
    { "Charlie", 78 }
};

foreach (var score in scores) {
    Console.WriteLine($"{score.Key}: {score.Value}");
}
