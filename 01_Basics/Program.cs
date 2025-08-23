using System; // System 名前空間（基本的なクラスや Console など）を使う宣言

// ここからトップレベル文（C# 9 以降）で簡潔に記述します
Console.WriteLine("=== C# 基本文法のデモ ==="); // 文字列を標準出力に表示

// 変数宣言（明示的な型）
int age = 30; // 整数型（32bit）の変数に 30 を代入
double height = 1.75; // 倍精度浮動小数点を代入
decimal salary = 5000000m; // 高精度の 10 進数（m サフィックス）を代入
bool isActive = true; // 真偽値を代入
char initial = 'A'; // 1 文字（シングルクォート）を代入
string name = "Taro"; // 文字列を代入

// 暗黙の型推論（var）
var message = "Hello"; // 右辺から型が推論され、これは string 型になる

// 定数（再代入不可）
const double Pi = 3.1415926535; // 円周率を定数として定義

// 算術演算
int a = 10; // 変数 a に 10
int b = 3; // 変数 b に 3
int sum = a + b; // 加算の結果を sum に代入
int diff = a - b; // 減算の結果を diff に代入
int prod = a * b; // 乗算の結果を prod に代入
int quot = a / b; // 整数同士の除算（小数点以下切り捨て）
int mod = a % b; // 余り（剰余）を計算

// 出力（文字列補間）
Console.WriteLine($"a + b = {sum}"); // 結果を埋め込みで表示
Console.WriteLine($"a - b = {diff}"); // 減算結果を表示
Console.WriteLine($"a * b = {prod}"); // 乗算結果を表示
Console.WriteLine($"a / b = {quot}"); // 整数除算の結果を表示
Console.WriteLine($"a % b = {mod}"); // 余りの結果を表示

// 浮動小数点の除算（正確な小数を得たい場合は double/decimal を使う）
double quotDouble = (double)a / b; // どちらかを double にすると小数点を保持
Console.WriteLine($"(double)a / b = {quotDouble}"); // 小数点を含む結果を表示

// 文字列の結合と書式化
string fullName = name + " " + initial + "."; // + で結合（空白とドットを含める）
Console.WriteLine($"Full Name: {fullName}"); // 補間で整形して表示
Console.WriteLine(string.Format("{0} は {1} 歳です。", name, age)); // Format メソッドでも可

// 型変換（キャストと変換メソッド）
int fromDouble = (int)height; // (int)とは整数値に変換する明示的キャスト：小数点以下は切り捨て
Console.WriteLine($"(int)height = {fromDouble}"); // キャスト結果を表示
string ageText = age.ToString(); // 数値 → 文字列へ変換,ToString()は数値を文字列に変換するメソッド
Console.WriteLine($"age.ToString() = {ageText}"); // 変換結果の表示
if (int.TryParse("42", out int parsed)) // 文字列 → 数値（安全な変換、失敗しないか判定）,TryParseは文字列を数値に変換するメソッド,out int parsedはoutは出力パラメーターとして、parsedは変換した数値を格納する変数
{
    Console.WriteLine($"TryParse 成功: {parsed}"); // 成功した場合の値を表示
}
else
{
    Console.WriteLine("TryParse 失敗"); // 失敗した場合のメッセージ
}

Console.WriteLine("=== 終了 ==="); // デモの終了を告げるメッセージ


