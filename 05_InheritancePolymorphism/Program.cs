using System; // Console を使うため

Console.WriteLine("=== 継承・ポリモーフィズムのデモ ==="); // 見出しの表示

// 基底クラスと派生クラスの多態性
Animal a1 = new Dog(); // Animal として参照しつつ、中身は Dog
Animal a2 = new Cat(); // Animal として参照しつつ、中身は Cat
a1.Speak(); // 実体に応じたオーバーライドが呼ばれる（Dog の処理）
a2.Speak(); // 実体に応じたオーバーライドが呼ばれる（Cat の処理）

// 抽象クラスの例
Shape s = new Circle(2.0); // 半径 2 の円を生成
Console.WriteLine($"Circle 面積: {s.Area()}"); // 抽象メソッドを実装した結果を呼ぶ

// インタフェースの例
IMovable m = new Car(); // IMovable として車を扱う
m.Move(10); // 10 メートル動かす（実装クラスに委譲）

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// ここから型定義
abstract class Animal // 抽象基底クラス（直接はインスタンス化しない）
{
    public abstract void Speak(); // 派生で実装必須の抽象メソッド
}

class Dog : Animal // Animal を継承した Dog
{
    public override void Speak() // 抽象メソッドを実装（オーバーライド）
    {
        Console.WriteLine("ワン！"); // 犬の鳴き声を表示
    }
}

class Cat : Animal // Animal を継承した Cat
{
    public override void Speak() // 抽象メソッドを実装（オーバーライド）
    {
        Console.WriteLine("ニャー！"); // 猫の鳴き声を表示
    }
}

abstract class Shape // 図形を表す抽象クラス
{
    public abstract double Area(); // 面積を返す抽象メソッド
}

class Circle : Shape // 円を表すクラス
{
    private readonly double _radius; // 半径（外部からは変更不可）
    public Circle(double radius) // 半径を受け取るコンストラクタ
    {
        _radius = radius; // フィールドに代入
    }
    public override double Area() // 面積を返す実装
    {
        return Math.PI * _radius * _radius; // 円の面積 πr^2
    }
}

interface IMovable // 移動可能なものを表すインタフェース
{
    void Move(int meters); // メートル単位で動く動作
}

class Car : IMovable // 車は動かせる
{
    public void Move(int meters) // インタフェースを実装
    {
        Console.WriteLine($"車が {meters}m 移動しました。"); // 動作を表示
    }
}


