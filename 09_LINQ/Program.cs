using System; // Console
using System.Linq; // LINQ 拡張メソッド（Where/Select/OrderBy など）
using System.Collections.Generic; // List

Console.WriteLine("=== LINQ のデモ ==="); // 見出しの表示

// サンプルデータ（数値のリスト）
List<int> data = new List<int> { 5, 2, 9, 1, 5, 6 }; // 6 要素のリスト

// Where（条件で絞り込み）
var evens = data.Where(n => n % 2 == 0); // 偶数のみ残す
Console.WriteLine("偶数: " + string.Join(", ", evens)); // 結果を表示

// Select（変換）
var squares = data.Select(n => n * n); // 各要素を二乗に変換
Console.WriteLine("二乗: " + string.Join(", ", squares)); // 表示

// OrderBy（昇順ソート）
var ordered = data.OrderBy(n => n); // 小さい順に並べる
Console.WriteLine("昇順: " + string.Join(", ", ordered)); // 表示

// GroupBy（値ごとのグルーピング）
var grouped = data.GroupBy(n => n); // 同じ値でグループを作る
foreach (var g in grouped) // 各グループについて
{
    Console.WriteLine($"値 {g.Key}: 件数 {g.Count()}"); // キーと件数を表示
}

// 複合クエリ例（偶数→二乗→降順）
var query = data
    .Where(n => n % 2 == 0) // 偶数を選ぶ
    .Select(n => n * n) // 二乗にする
    .OrderByDescending(n => n); // 降順に並べる
Console.WriteLine("偶数の二乗（降順）: " + string.Join(", ", query)); // 表示

Console.WriteLine("=== 終了 ==="); // 終了メッセージ


