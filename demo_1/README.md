## demo_1: 画像のOCRによる自動マスキング（最小実装）

このデモは、単一画像を入力として Tesseract OCR の TSV 出力を利用し、検出された単語領域を黒塗りでマスクした画像を出力します。将来的に動画フレーム処理やUI連携（Form.cs など）に拡張可能な最小構成です。

### 依存関係
- .NET 8 SDK
- Tesseract OCR（CLI）
  - macOS: `brew install tesseract`
- 画像処理ライブラリ: ImageSharp（csproj に同梱）

### 使い方
```bash
cd /Users/kouta_i5/Github/Personal/C#-study/demo_1
dotnet restore
dotnet run -- ../demo/data/sample_clip_out_of_body_detection.mp4 # ←動画ではなく画像パスを指定してください
```

オプション:
```bash
dotnet run -- <inputImagePath> [--lang jpn] [--tesseract /usr/local/bin/tesseract] [--out masked.png] [--print-only]
```

例（日本語OCR、出力ファイル名指定）
```bash
dotnet run -- ./sample.png --lang jpn --out ./sample.masked.png
```

最小動作（OCR文字列と座標の確認だけ）
```bash
dotnet run -- ./sample.png --lang jpn --print-only
```

### 出力先
- 既定では `demo_1/outputs/` に保存されます（フォルダが無ければ自動作成）。
- 任意の出力先にしたい場合は `--out` で明示してください。

### 仕組み
1. `tesseract <input> stdout --oem 1 --psm 6 -l <lang> tsv` を実行し、TSVを取得
2. TSVを解析し、単語レベル(level==5)の `left, top, width, height` を抽出
3. ImageSharpで該当領域を黒塗りして保存

### メモ
- まずは静止画でスモールスタートしています。フレーム抽出やUI反映は次段で拡張予定です。
- 画像内の患者ID・日時などのテキスト位置が一定であれば、その部分だけに限定したマスクや閾値フィルタも容易です。


