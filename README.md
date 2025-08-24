## C# 学習フォルダー（体系的・網羅的ロードマップ）

このリポジトリは、C# を初学者でも段階的に学べるように、トピックごとにフォルダとサンプルコード（1行ずつ日本語コメント付き）を整理しています。基本から応用まで、実務で頻出のテーマを体系的にカバーします。

### 使い方（推奨）
- **読むだけ派**: ルート直下の各 `.cs` を開いて、コメントを追って読み進めてください。
- **動かして理解派**:
  1. .NET SDK をインストールします（`dotnet --version` で確認）。
  2. コンソールアプリを作成して、学びたい `.cs` の内容で `Program.cs` を置き換えて実行します。
     - 例: `dotnet new console -n Playground && cd Playground` → `Program.cs` を対象 `.cs` の内容に置換 → `dotnet run`

### 収録トピック一覧
1. [01_Basics.cs](01_Basics.cs): C#の基本（Hello World、変数・型、演算、書式化）
2. [02_ControlFlow.cs](02_ControlFlow.cs): 制御構文（if/switch/for/while/foreach）
3. [03_Methods.cs](03_Methods.cs): メソッド（引数・戻り値、ref/out、オーバーロード、params）
4. [04_ClassesObjects.cs](04_ClassesObjects.cs): クラスとオブジェクト（プロパティ、コンストラクタ、static）
5. [05_InheritancePolymorphism.cs](05_InheritancePolymorphism.cs): 継承・ポリモーフィズム（virtual/override、abstract、interface）
6. [06_StructEnum.cs](06_StructEnum.cs): 構造体と列挙型（struct、enum）
7. [07_ExceptionHandling.cs](07_ExceptionHandling.cs): 例外処理（try/catch/finally、throw、自作例外）
8. [08_Collections.cs](08_Collections.cs): コレクション（配列、List、Dictionary、HashSet、Queue、Stack）
9. [09_LINQ.cs](09_LINQ.cs): LINQ 基本（Where/Select/OrderBy/GroupBy）
10. [10_AsyncAwait.cs](10_AsyncAwait.cs): 非同期処理（Task、async/await の基本）
11. [11_FileIO.cs](11_FileIO.cs): ファイル入出力（読み書き、using）
12. [12_DelegatesEventsLambdas.cs](12_DelegatesEventsLambdas.cs): デリゲート/イベント/ラムダ式（Action/Func含む）
13. [13_Generics.cs](13_Generics.cs): ジェネリクス（型パラメータ、型制約）
14. [14_NullSafety.cs](14_NullSafety.cs): Null安全（nullable参照型、null 条件演算子・合体演算子）
15. [15_CommonBuiltins/Program.cs](15_CommonBuiltins/Program.cs): よく使う組み込み（string/Math/DateTime/TimeSpan/Random/Guid/Convert/Path/Environment/LINQ）
16. [16_Modifiers/Program.cs](16_Modifiers/Program.cs): 修飾子（アクセス修飾子、static/const/readonly、abstract/virtual/override/sealed、ref/out/in など）

今後、record/tuple/パターンマッチング、DI、テスト、設計原則（SOLID）、拡張メソッド、演算子オーバーロード、Span/Memory、非同期ストリーム、Source Generator なども追加拡張できます。

### ライセンス
学習用途で自由にご利用ください。


# C--study
