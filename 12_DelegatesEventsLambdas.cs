using System; // Console, EventHandler など

Console.WriteLine("=== デリゲート / イベント / ラムダ式のデモ ==="); // 見出しの表示

// デリゲート型の宣言（引数 int、戻り値 int の関数を指す）
delegate int Transformer(int value); // 関数そのものを変数として扱うための型

// ラムダ式を使ってデリゲートを作る
Transformer doubleFn = x => x * 2; // 入力を 2 倍する関数
Transformer tripleFn = x => x * 3; // 入力を 3 倍する関数

Console.WriteLine($"doubleFn(10) = {doubleFn(10)}"); // 10 を 2 倍
Console.WriteLine($"tripleFn(10) = {tripleFn(10)}"); // 10 を 3 倍

// 組込みデリゲート（Action/Func）
Action<string> printer = s => Console.WriteLine($"Hello, {s}"); // 引数だけで戻り値なし
printer("World"); // 実行

Func<int, int, int> add = (a, b) => a + b; // 2 引数 int を受けて int を返す
Console.WriteLine($"add(2,3) = {add(2,3)}"); // 2 + 3 を表示

// イベントの例
var btn = new Button(); // ボタン風のクラス
btn.Click += (sender, args) => Console.WriteLine("ボタンがクリックされました"); // クリック時の処理を購読
btn.OnClick(); // クリックを発生させる（イベントが呼ばれる）

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// 簡単なイベント発行側クラス
class Button // ボタンを模したクラス
{
    public event EventHandler? Click; // クリックイベント。購読者がいない可能性もあるため null 許容

    public void OnClick() // クリックを発生させるメソッド
    {
        Click?.Invoke(this, EventArgs.Empty); // 購読者がいるときだけイベントを発火
    }
}




