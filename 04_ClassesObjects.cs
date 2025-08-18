using System; // Console などの基本 API を使う

Console.WriteLine("=== クラスとオブジェクトのデモ ==="); // 見出しの表示

// クラスからインスタンスを生成して使う例
Person p = new Person("Taro", 30); // コンストラクタで名前と年齢を設定
Console.WriteLine($"名前: {p.Name}, 年齢: {p.Age}"); // プロパティの参照
p.HaveBirthday(); // 誕生日メソッドを呼び年齢を 1 増やす
Console.WriteLine($"誕生日後の年齢: {p.Age}"); // 変更された年齢を表示

// static メンバーの利用（作成済みインスタンス数）
Console.WriteLine($"作成済み Person 数: {Person.InstanceCount}"); // クラス名から直接参照

Console.WriteLine("=== 終了 ==="); // 終了メッセージ

// クラス定義（トップレベル文の下に置ける）
class Person // 人を表すクラス
{
    public static int InstanceCount { get; private set; } // 生成数を表す static プロパティ（外部は読み取りのみ）

    public string Name { get; private set; } // 名前（外部は読み取り、内部のみ設定）
    public int Age { get; private set; } // 年齢（外部は読み取り、内部のみ設定）

    public Person(string name, int age) // コンストラクタ（生成時に必須の値を受け取る）
    {
        Name = name; // フィールド/プロパティに代入
        Age = age; // 同上
        InstanceCount++; // インスタンスが作られるたびにカウントを増やす
    }

    public void HaveBirthday() // 誕生日で年齢を 1 増やすメソッド
    {
        Age++; // 年齢をインクリメント
    }
}




