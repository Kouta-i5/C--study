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
//foreachはfor文とは少し違う。for文はインデックスを使って配列の要素を取り出すが、foreachは配列の要素を順に取り出すことができる
//foreach (var 変数名(イテレータ) in 配列名)
//var は型推論を行うためのキーワード,var は変数の型を自動的に推論するためのキーワード
//foreachは配列の各要素を順に取り出すためのループ文
// 初めに配列を宣言してから要素を代入する方法

string [] subjects; // 配列を宣言,null参照,nullは何も参照していないことを表す、出力すると0が出る。
subjects = new string[3]; // 配列を初期化(newはインスタンスを作成してメモリを確保するためのキーワード)→ヒープメモリ
subjects[0] = "国語"; // 0番目の要素に"apple"を代入
subjects[1] = "数学"; // 1番目の要素に"banana"を代入
subjects[2] = "英語"; // 2番目の要素に"cherry"を代入


string [] items = new string[3]; // 3 要素の文字列配列を宣言
items[0] = "apple"; // 0番目の要素に"apple"を代入
items[1] = "banana"; // 1番目の要素に"banana"を代入
items[2] = "cherry"; // 2番目の要素に"cherry"を代入

// 配列を宣言とともに要素を入れて初期化する。
string[] fruits = { "apple", "banana", "cherry" }; // 3 要素の文字列配列で初期化して要素を代入
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
