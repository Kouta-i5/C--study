using System; // Console
using System.Threading.Tasks; // Task, async/await

Console.WriteLine("=== 非同期処理 (async/await) のデモ ==="); // 見出しの表示

// 非同期メソッドを呼ぶ
await DemoAsync(); // トップレベル文では await が使える

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// 非同期メソッド定義
static async Task DemoAsync() // 非同期の戻り値 Task
{
    Console.WriteLine("処理開始"); // 開始メッセージ

    // 疑似的な非同期処理（I/O 待ちの代わりに Delay を使用）
    await Task.Delay(500); // 0.5 秒待つ（スレッドはブロックしない）
    Console.WriteLine("0.5 秒経過"); // 途中経過

    int value = await ComputeAsync(21); // 別の非同期処理の結果を待つ
    Console.WriteLine($"結果: {value}"); // 受け取った結果を表示
}

static async Task<int> ComputeAsync(int x) // int を返す非同期メソッド
{
    await Task.Delay(300); // 擬似的に待機
    return x * 2; // 値を計算して返す
}


