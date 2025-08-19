using System; // Console を使うため

Console.WriteLine("=== 構造体と列挙型のデモ ==="); // 見出しの表示

// enum の利用例
Day today = Day.Wednesday; // 列挙型のメンバーを代入
Console.WriteLine($"今日は {today} です"); // 列挙値の表示（ToString 相当）

// struct の利用例（値型のカスタム型）
Point p1 = new Point(3, 5); // 構造体のコンストラクタで初期化
Point p2 = p1; // 値型なのでコピーが作られる
p2.X = 10; // p2 のみが変わる（p1 は影響を受けない）
Console.WriteLine($"p1=({p1.X},{p1.Y}), p2=({p2.X},{p2.Y})"); // それぞれの座標を表示

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// 列挙型（整数に名前を付けて可読性を上げる）
enum Day // 曜日を表す列挙
{
    Sunday, // 0
    Monday, // 1
    Tuesday, // 2
    Wednesday, // 3
    Thursday, // 4
    Friday, // 5
    Saturday // 6
}

// 構造体（軽量な値型。小さなデータを表すのに有効）
struct Point // 2D 座標を表す
{
    public int X; // X 座標（公開フィールド）
    public int Y; // Y 座標（公開フィールド）

    public Point(int x, int y) // コンストラクタ
    {
        X = x; // 引数をフィールドに代入
        Y = y; // 同上
    }
}


