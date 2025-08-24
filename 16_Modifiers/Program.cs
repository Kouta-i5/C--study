using System; // Console

Console.WriteLine("=== 修飾子（C#）のデモ ==="); // 見出し

// アクセス修飾子: public / internal / protected / private
var pub = new PublicSample(); // public 型はどこからでも参照可能
Console.WriteLine(pub.PublicProp); // public メンバーは外部から参照可能

var internalObj = new InternalSample(); // internal 型は同一アセンブリ内から参照可能
Console.WriteLine(internalObj.Name); // internal メンバー（ここでは public プロパティ）

// static: 型に属するメンバー
Console.WriteLine(StaticSample.Count); // インスタンス不要で参照
var s1 = new StaticSample(); // コンストラクタでカウント増
var s2 = new StaticSample(); // 同上
Console.WriteLine(StaticSample.Count); // 共有カウントが 2 になっている

// readonly / const の違い
Console.WriteLine(ConstReadonlySample.Pi); // const はコンパイル時定数（暗黙に static）
var cr = new ConstReadonlySample(42); // readonly フィールドはコンストラクタでのみ代入可
Console.WriteLine(cr.CreatedAtTicks); // 以後は変更不可

// abstract / virtual / override / sealed
BaseAnimal dog = new Dog(); // 抽象クラスはインスタンス不可、派生インスタンスを使う
Console.WriteLine(dog.Speak()); // override 実装が呼ばれる
BaseAnimal cat = new Cat(); // 別の派生
Console.WriteLine(cat.Speak()); // こちらも override

// ref/out/in（パラメータ修飾子）
int a = 1; // 1
int b = 2; // 2
Swap(ref a, ref b); // 参照渡しで入れ替え
Console.WriteLine($"Swap: a={a}, b={b}"); // a=2, b=1
if (int.TryParse("123", out int parsed)) // out は出力専用
{
    Console.WriteLine($"TryParse: {parsed}"); // 123
}
ReadOnlyAdd(in a, in b, out int sum); // in は読み取り専用参照
Console.WriteLine($"in/out: sum={sum}"); // 3

Console.WriteLine("=== 終了 ==="); // 終了

// ===== 型/メンバー定義 =====
public class PublicSample // public 型
{
    public string PublicProp { get; } = "public ok"; // public プロパティ
    private int _secret = 10; // private は同一型内のみ
}

internal class InternalSample // internal 型（同一アセンブリのみ）
{
    public string Name { get; } = "internal ok"; // public な公開メンバー
}

public class StaticSample // static メンバーの例
{
    public static int Count { get; private set; } // 型に属する共有カウンタ
    public StaticSample() { Count++; } // 生成時にカウント増
}

public class ConstReadonlySample // const / readonly の違い
{
    public const double Pi = 3.1415926535; // コンパイル時定数（再代入不可）
    public readonly long CreatedAtTicks; // 生成時にのみ代入可
    public ConstReadonlySample(long ticks) // コンストラクタ
    {
        CreatedAtTicks = ticks; // ここでは代入できる
    }
}

public abstract class BaseAnimal // 抽象クラス
{
    public abstract string Speak(); // 抽象メソッド（実装なし）
    public virtual string Info() => "animal"; // 派生で override 可能
}

public class Dog : BaseAnimal // 具象クラス
{
    public override string Speak() => "ワン"; // 抽象の実装
    public sealed override string Info() => "dog"; // これ以上の override 禁止
}

public class Cat : BaseAnimal // 具象クラス
{
    public override string Speak() => "ニャー"; // 抽象の実装
}

static void Swap(ref int x, ref int y) // ref: 参照渡し（入出力）
{
    int t = x; // 退避
    x = y; // 代入
    y = t; // 代入
}

static void ReadOnlyAdd(in int x, in int y, out int sum) // in/out の組み合わせ
{
    sum = x + y; // out は必ず代入が必要
}


