using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PaddingTool
{
    public partial class Form1 : Form
    {
        private string? inputFilePath;
        private Mat? processedImage;

        public Form1()
        {
            InitializeComponent();
            processButton.Enabled = false;
            saveButton.Enabled = false;
        }

        private void selectFileButton_Click(object sender, EventArgs e)
        {
            openFileDialog.Filter = "PNG 画像 (*.png)|*.png|すべてのファイル (*.*)|*.*";
            openFileDialog.Title = "入力画像を選択してください";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string filePath = openFileDialog.FileName;

                    inputPictureBox.Image?.Dispose();

                    // ファイルをロックしたままにしないよう、バイト列経由で読み込む
                    byte[] fileBytes;
                    using (new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        fileBytes = File.ReadAllBytes(filePath);
                    }
                    using (var ms = new MemoryStream(fileBytes))
                    {
                        inputPictureBox.Image = new Bitmap(ms);
                    }

                    inputFilePath = filePath;
                    processButton.Enabled = true;

                    outputPictureBox.Image?.Dispose();
                    outputPictureBox.Image = null;
                    processedImage?.Dispose();
                    processedImage = null;
                    saveButton.Enabled = false;

                    statusLabel.Text = "画像を処理できます.[処理実行]を押してください.";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("画像の読み込みに失敗しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    inputFilePath = null;
                    processButton.Enabled = false;
                    saveButton.Enabled = false;
                }
            }
        }

        private void processButton_Click(object sender, EventArgs e)
        {
            if (inputFilePath == null)
            {
                MessageBox.Show("先に入力画像を選択してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                statusLabel.Text = "処理中...";
                Application.DoEvents();

                using (Mat src = Cv2.ImRead(inputFilePath, ImreadModes.Unchanged))
                {
                    if (src.Empty())
                    {
                        throw new Exception("OpenCVSharp での画像読み込みに失敗しました.ファイルが破損しているか、サポートされていない形式の可能性があります.");
                    }
                    if (src.Channels() != 4)
                    {
                        throw new Exception("画像はRGBA形式（アルファチャンネル付き PNG など）である必要があります.");
                    }

                    processedImage?.Dispose();
                    processedImage = PaddingImage(src);
                }

                if (processedImage != null && !processedImage.Empty())
                {
                    outputPictureBox.Image?.Dispose();
                    outputPictureBox.Image = BitmapConverter.ToBitmap(processedImage);
                    saveButton.Enabled = true;
                    statusLabel.Text = "処理が完了しました.[保存]から画像を保存できます.";
                }
                else
                {
                    outputPictureBox.Image = null;
                    saveButton.Enabled = false;
                    if (processedImage == null)
                    {
                        MessageBox.Show("画像処理中に内部エラーが発生しました.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        statusLabel.Text = "処理中にエラーが発生しました";
                    }
                    else
                    {
                        MessageBox.Show("画像処理の結果、空の画像が生成されました.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        statusLabel.Text = "処理結果が空です";
                    }
                }
            }
            catch (Exception ex)
            {
                processedImage?.Dispose();
                processedImage = null;
                outputPictureBox.Image = null;
                saveButton.Enabled = false;
                statusLabel.Text = "処理中にエラーが発生しました";
                MessageBox.Show("処理中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 透明部分(アルファ=0)の色を、周囲の不透明ピクセルの色で埋める(パディング)。
        /// OpenCV の Inpaint (Alexandru Telea 法, 半径 3) を使用し、結果のアルファは全面 255 になる。
        /// </summary>
        private Mat? PaddingImage(Mat srcRgba)
        {
            Mat[]? channels = null;
            Mat? bgr = null;
            Mat? alpha = null;
            Mat? transparentMask = null;
            Mat? paddedBgr = null;
            Mat? resultRgba = null;
            Mat? finalAlpha = null;

            try
            {
                // チャンネル分離 (B, G, R, A)
                channels = Cv2.Split(srcRgba);
                bgr = new Mat();
                alpha = channels[3];
                Cv2.Merge(new[] { channels[0], channels[1], channels[2] }, bgr);

                // アルファが 0 の画素をマスクにする
                transparentMask = new Mat();
                Cv2.Compare(alpha, 0, transparentMask, CmpType.EQ);

                // マスク部分を Telea 法で修復
                paddedBgr = new Mat();
                Cv2.Inpaint(bgr, transparentMask, paddedBgr, 3, InpaintMethod.Telea);

                // BGR -> BGRA に戻し、アルファを不透明で埋める
                resultRgba = new Mat();
                Cv2.CvtColor(paddedBgr, resultRgba, ColorConversionCodes.BGR2BGRA);
                finalAlpha = new Mat(resultRgba.Size(), MatType.CV_8UC1, Scalar.All(255));
                Cv2.InsertChannel(finalAlpha, resultRgba, 3);

                return resultRgba;
            }
            catch (Exception)
            {
                resultRgba?.Dispose();
                return null;
            }
            finally
            {
                // alpha は channels[3] なのでここで一緒に解放される
                if (channels != null)
                {
                    foreach (Mat channel in channels)
                    {
                        channel?.Dispose();
                    }
                }
                bgr?.Dispose();
                transparentMask?.Dispose();
                paddedBgr?.Dispose();
                finalAlpha?.Dispose();
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (processedImage == null || processedImage.Empty())
            {
                MessageBox.Show("保存する処理済み画像がありません.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            saveFileDialog.Filter = "PNG 画像 (*.png)|*.png";
            saveFileDialog.Title = "処理済み画像を保存";

            if (inputFilePath != null)
            {
                try
                {
                    string originalFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                    string? directoryPath = Path.GetDirectoryName(inputFilePath);
                    if (!string.IsNullOrEmpty(directoryPath))
                    {
                        saveFileDialog.InitialDirectory = directoryPath;
                    }
                    else
                    {
                        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    }
                    saveFileDialog.FileName = originalFileName + "_padding.png";
                }
                catch
                {
                    saveFileDialog.FileName = "processed_padding.png";
                    saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
            }
            else
            {
                saveFileDialog.FileName = "processed_padding.png";
                saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string savePath = saveFileDialog.FileName;
                    if (Cv2.ImWrite(savePath, processedImage))
                    {
                        statusLabel.Text = "画像を保存しました: " + Path.GetFileName(savePath);
                    }
                    else
                    {
                        throw new Exception("Cv2.ImWrite が false を返しました.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("画像の保存に失敗しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
