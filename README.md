# PaddingTool

3Dモデルのテクスチャに含まれる完全な透明領域を、周囲の色でパディング（塗り広げ）するための Windows アプリケーションです。

UV シーム付近で発生しやすい色の滲みやアーティファクトの軽減に役立ちます。

## 使い方

1. `PaddingTool.exe` を起動します。
2. 「ファイル選択」ボタンをクリックし、処理したい PNG 画像を選択します。
3. 左側のボックスに選択した元画像が表示されます。
4. 「処理実行」ボタンをクリックします。
5. 右側のボックスに、透明部分がパディングされた処理後の画像が表示されます。
6. 「保存」ボタンをクリックし、保存場所とファイル名を指定して保存します（既定のファイル名は `元の名前_padding.png`）。

### 入力画像について

- パディングを行いたい領域は完全な透明（アルファ = 0）にしてください。
- 入力はアルファチャンネル付きの画像（RGBA の PNG など）である必要があります。
- 元画像の半透明部分は色の参照元として扱われ、出力画像はすべて不透明になります。アンチエイリアスがかかったテクスチャを入力した際にアーティファクトが残らないようにするための措置です。

## 仕組み

処理の中身は [`Form1.PaddingImage`](PaddingTool/Form1.cs) にあります。

1. 画像を B / G / R / A の各チャンネルに分離
2. アルファが 0 のピクセルをマスクとして抽出
3. マスク領域を OpenCV の `cv::inpaint`（Alexandru Telea 法, 修復半径 3）で周囲の色から補完
4. アルファチャンネルを 255（不透明）で埋めて BGRA 画像として出力

## 動作環境

- Windows 10 / Windows 11（64 ビット版推奨）
- [.NET 8.0 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/8.0)

## ビルド

[.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) が必要です。Visual Studio 2022 で `PaddingTool/PaddingTool.csproj` を開くか、コマンドラインで以下を実行します。

```sh
dotnet build PaddingTool/PaddingTool.csproj -c Release
```

依存パッケージ（OpenCvSharp4 など）は NuGet から自動で取得されます。

## ライセンス

本アプリ独自の実装部分は [MIT License](LICENSE) で公開しています。個人利用・商用利用・改変・再配布いずれも自由です。

本ソフトウェアは以下のライブラリを利用しています。各ライセンス全文は [`Licenses`](Licenses) フォルダを参照してください。

| ライブラリ | ライセンス |
| --- | --- |
| [OpenCvSharp](https://github.com/shimat/opencvsharp) | Apache License 2.0 |
| [OpenCV](https://opencv.org/) | Apache License 2.0 |
| [.NET](https://github.com/dotnet/runtime) | MIT License |

## 免責事項

本ソフトウェアの使用によって生じた、いかなるトラブルや損害についても、作者は一切の責任を負いません。自己責任でご利用ください。

## 更新履歴

- v1.0.2 (2025-11-05) MIT ライセンスでオープンソース化
- v1.0.1 (2025-04-22) ライセンスファイルの誤りを訂正
- v1.0.0 (2025-04-22) 公開
