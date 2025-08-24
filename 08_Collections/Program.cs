using System; // Console を使う
using System.Collections.Generic; // List, Dictionary, Queue, Stack, HashSet など

Console.WriteLine("=== コレクションのデモ ==="); // 見出しの表示

// 配列の例
int[] numbers = { 1, 2, 3 }; // 要素数 3 の int 配列
Console.WriteLine($"配列の長さ: {numbers.Length}"); // 長さを表示
Console.WriteLine($"numbers[1] = {numbers[1]}"); // インデックスアクセス

// List<T> の例（可変長配列）(可変長とは、要素数を動的に変えることができるということ)
//List<データ型> 変数名　= new List<データ型>();
//左のList<データ型>はListクラスであることの宣言、右のList<データ型>はListクラスのインスタンスを作成
List<string> list = new List<string>(); // 空のリストを作成
//.Addは要素を追加するメソッド、その他にも.Removeは要素を削除するメソッド、.Countは要素数を取得するメソッド、.Clearは要素を全て削除するメソッドなどがある
list.Add("apple"); // 要素を追加
list.Add("banana"); // 追加
list.Add("cherry"); // 追加
Console.WriteLine($"List 要素数: {list.Count}"); // 要素数
Console.WriteLine(string.Join(", ", list)); // 連結表示

// Dictionary<TKey,TValue> の例（キーと値のマップ）
Dictionary<string, int> ages = new Dictionary<string, int>(); // 空の辞書
ages["Taro"] = 30; // キーに対して値を設定
ages["Hanako"] = 28; // 設定
Console.WriteLine($"Taro の年齢: {ages["Taro"]}"); // キーで参照
if (ages.TryGetValue("Jiro", out int jiroAge)) // キーが存在するか安全に取得
{
    Console.WriteLine($"Jiro の年齢: {jiroAge}"); // 見つかった場合
}
else
{
    Console.WriteLine("Jiro は未登録"); // 見つからなかった場合
}

// HashSet<T> の例（集合：重複を許さない）
HashSet<int> set = new HashSet<int>(); // 空の集合
set.Add(1); // 1 を追加
set.Add(2); // 2 を追加
set.Add(2); // 2 は重複なので無視される
Console.WriteLine($"HashSet 要素数: {set.Count}"); // 2 になる

// Queue<T> の例（先入れ先出し）
Queue<string> queue = new Queue<string>(); // 空のキュー
queue.Enqueue("A"); // A を入れる
queue.Enqueue("B"); // B を入れる
Console.WriteLine(queue.Dequeue()); // 先頭（A）を取り出す
Console.WriteLine(queue.Peek()); // 次に出る要素（B）を参照

// Stack<T> の例（後入れ先出し）
Stack<string> stack = new Stack<string>(); // 空のスタック
stack.Push("X"); // X を積む
stack.Push("Y"); // Y を積む
Console.WriteLine(stack.Pop()); // 一番上（Y）を取り出す
Console.WriteLine(stack.Peek()); // 次の要素（X）を参照

Console.WriteLine("=== 終了 ==="); // 終了メッセージ


