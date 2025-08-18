using System; // Console

Console.WriteLine("=== ジェネリクスのデモ ==="); // 見出しの表示

// ジェネリッククラスの利用
Box<int> intBox = new Box<int>(); // int を入れる箱
intBox.Value = 123; // 値を設定
Console.WriteLine($"intBox.Value = {intBox.Value}"); // 表示

Box<string> stringBox = new Box<string>(); // string を入れる箱
stringBox.Value = "Hello"; // 値を設定
Console.WriteLine($"stringBox.Value = {stringBox.Value}"); // 表示

// ジェネリックメソッドの利用
// Swap(ref intBox.Value, ref stringBox.Value); // 型が異なるのでコンパイルできない（エラー例）
// 上行はあえてのエラー解説です（型が一致していないため）。実際の利用例は下記。

int a = 1; // 1 を設定
int b = 2; // 2 を設定
Swap(ref a, ref b); // 同じ型なら入れ替えられる
Console.WriteLine($"Swap 後: a={a}, b={b}"); // 結果

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// ジェネリッククラス定義
class Box<T> // 型 T を入れられる箱
{
    public T Value { get; set; } // T 型の値
    public Box() { Value = default!; } // 既定値で初期化（null 許容でない警告を抑制）
}

// ジェネリックメソッド定義
static void Swap<T>(ref T left, ref T right) // 任意の型 T の 2 値を入れ替える
{
    T temp = left; // 一時退避
    left = right; // 代入
    right = temp; // 代入
}




