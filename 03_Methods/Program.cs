using System; // Console などを使うための宣言

Console.WriteLine("=== メソッドのデモ ==="); // 見出しの表示

// メソッド呼び出し（後方に定義した static メソッドをここで呼べる）
int added = Add(2, 3); // 2 と 3 を足し算し、結果を受け取る
Console.WriteLine($"Add(2, 3) = {added}"); // 結果を表示

// out パラメータの例（TryParse に似た作り）
bool ok = TryDivide(10, 3, out double result); // 10 / 3 を試し、結果を out で受ける
Console.WriteLine($"TryDivide 成功: {ok}, 結果: {result}"); // 成功フラグと結果の表示

// ref パラメータの例（値を入れ替える）
int x = 1; // x を 1 に設定
int y = 2; // y を 2 に設定
Swap(ref x, ref y); // x と y の値を入れ替える
Console.WriteLine($"Swap 後: x={x}, y={y}"); // 入れ替え結果を表示

// オーバーロードの例（同名だが引数が異なるメソッド）
Console.WriteLine(Describe(123)); // int 引数の Describe を呼ぶ
Console.WriteLine(Describe("hello")); // string 引数の Describe を呼ぶ

// 可変長引数（params）の例
int total = SumAll(1, 2, 3, 4); // 複数の引数を渡して合計
Console.WriteLine($"SumAll = {total}"); // 合計を表示

Console.WriteLine("=== 終了 ==="); // デモの終了

// ここから下はメソッド定義（トップレベル文の後ろに static メソッドを置ける）
static int Add(int left, int right) // 2 つの int を受け取り、和を返す
{
    return left + right; // 左右を足して返す
}

static bool TryDivide(double numerator, double denominator, out double quotient) // 割り算を安全に試す
{
    if (denominator == 0) // 0 で割るのは不可
    {
        quotient = double.NaN; // 結果は非数としておく
        return false; // 失敗を示す
    }
    quotient = numerator / denominator; // 割り算を実行
    return true; // 成功を示す
}

static void Swap(ref int left, ref int right) // 2 つの値を入れ替える
{
    int temp = left; // 一時退避
    left = right; // 右を左へ
    right = temp; // 退避しておいた値を右へ
}

static string Describe(int value) // int を説明するオーバーロード
{
    return $"int 値: {value}"; // 文字列で返す
}

static string Describe(string text) // string を説明するオーバーロード
{
    return $"string 値: {text}"; // 文字列で返す
}

static int SumAll(params int[] numbers) // 可変長引数を配列として受け取る
{
    int sum = 0; // 合計用の変数
    foreach (var n in numbers) // すべての要素を合計
    {
        sum += n; // 足し込む
    }
    return sum; // 合計を返す
}


