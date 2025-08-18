using System; // Console, Exception など

Console.WriteLine("=== 例外処理のデモ ==="); // 見出しの表示

// try-catch-finally の例
try // 例外が起きうる処理を囲む
{
    int x = 10; // 10 を設定
    int y = 0; // 0 を設定（危険な除算）
    int z = x / y; // 0 で割ると例外（DivideByZeroException）が発生
    Console.WriteLine(z); // ここには到達しない
}
catch (DivideByZeroException ex) // 特定の例外を捕捉
{
    Console.WriteLine($"ゼロ除算: {ex.Message}"); // エラーメッセージを表示
}
catch (Exception ex) // 一般的な例外の捕捉（最後に広いもの）
{
    Console.WriteLine($"その他の例外: {ex.Message}"); // メッセージ表示
}
finally // 成否に関わらず実行
{
    Console.WriteLine("後処理を実行しました"); // リソース解放などを想定
}

// 明示的に例外を投げる例
try // 例外発生を試す
{
    ValidateAge(-1); // 不正値で呼ぶと例外が発生
}
catch (ArgumentOutOfRangeException ex) // 期待する例外を捕捉
{
    Console.WriteLine($"検証失敗: {ex.ParamName} -> {ex.Message}"); // 詳細表示
}

// 独自例外の例
try // カスタム例外を発生させる処理
{
    throw new BusinessException("業務上の条件を満たしていません"); // 自作例外を投げる
}
catch (BusinessException ex) // 自作例外を捕捉
{
    Console.WriteLine($"BusinessException: {ex.Message}"); // メッセージ表示
}

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// 入力値検証のサンプル関数
static void ValidateAge(int age) // 年齢が 0 以上であることを確認
{
    if (age < 0) // 不正な場合
    {
        throw new ArgumentOutOfRangeException(nameof(age), "年齢は 0 以上である必要があります"); // 例外を投げる
    }
}

// 自作例外（業務例外などを区別したいときに役立つ）
class BusinessException : Exception // Exception を継承
{
    public BusinessException(string message) : base(message) // 基底にメッセージを渡す
    {
    }
}




