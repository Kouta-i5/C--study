using System; // Console

Console.WriteLine("=== Null 安全のデモ ==="); // 見出しの表示

// null 許容参照型（.csproj で <Nullable>enable</Nullable> を有効にしていると仮定）
string? maybeText = null; // null を入れられる string
Console.WriteLine(maybeText == null ? "null です" : maybeText); // null かどうかの確認

// null 条件演算子（?.）と null 合体演算子（??）
int? length = maybeText?.Length; // maybeText が null なら結果は null
Console.WriteLine($"長さ（null 可）: {length}"); // null かもしれない長さ

string fallback = maybeText ?? "デフォルト文字列"; // null なら右側を使う
Console.WriteLine($"fallback: {fallback}"); // デフォルトが使われる

// null 合体代入演算子（??=）
maybeText ??= "初期値"; // null なら代入、非 null なら何もしない
Console.WriteLine($"maybeText: {maybeText}"); // 値を確認

// 安全なナビゲーションの例（?. と ?? を組み合わせ）
Person? person = null; // Person 参照は null の可能性
int safeAge = person?.Age ?? -1; // null なら -1 を使う
Console.WriteLine($"safeAge: {safeAge}"); // -1 が出力される

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

class Person // null 安全サンプル用のクラス
{
    public int Age { get; set; } // 年齢プロパティ
}


