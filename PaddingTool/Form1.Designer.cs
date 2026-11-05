namespace PaddingTool
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            selectFileButton = new Button();
            processButton = new Button();
            inputPictureBox = new PictureBox();
            outputPictureBox = new PictureBox();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            saveButton = new Button();
            label1 = new Label();
            label2 = new Label();
            statusLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)inputPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)outputPictureBox).BeginInit();
            SuspendLayout();
            //
            // selectFileButton
            //
            selectFileButton.Location = new Point(23, 34);
            selectFileButton.Margin = new Padding(2);
            selectFileButton.Name = "selectFileButton";
            selectFileButton.Size = new Size(78, 30);
            selectFileButton.TabIndex = 0;
            selectFileButton.Text = "ファイル選択";
            selectFileButton.UseVisualStyleBackColor = true;
            selectFileButton.Click += selectFileButton_Click;
            //
            // processButton
            //
            processButton.Location = new Point(23, 103);
            processButton.Margin = new Padding(2);
            processButton.Name = "processButton";
            processButton.Size = new Size(78, 30);
            processButton.TabIndex = 1;
            processButton.Text = "処理実行";
            processButton.UseVisualStyleBackColor = true;
            processButton.Click += processButton_Click;
            //
            // inputPictureBox
            //
            inputPictureBox.BorderStyle = BorderStyle.FixedSingle;
            inputPictureBox.Location = new Point(140, 34);
            inputPictureBox.Margin = new Padding(2);
            inputPictureBox.Name = "inputPictureBox";
            inputPictureBox.Size = new Size(300, 300);
            inputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            inputPictureBox.TabIndex = 2;
            inputPictureBox.TabStop = false;
            //
            // outputPictureBox
            //
            outputPictureBox.BorderStyle = BorderStyle.FixedSingle;
            outputPictureBox.Location = new Point(485, 34);
            outputPictureBox.Margin = new Padding(2);
            outputPictureBox.Name = "outputPictureBox";
            outputPictureBox.Size = new Size(300, 300);
            outputPictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            outputPictureBox.TabIndex = 3;
            outputPictureBox.TabStop = false;
            //
            // openFileDialog
            //
            openFileDialog.FileName = "openFileDialog1";
            //
            // saveButton
            //
            saveButton.Location = new Point(23, 172);
            saveButton.Margin = new Padding(2);
            saveButton.Name = "saveButton";
            saveButton.Size = new Size(78, 30);
            saveButton.TabIndex = 4;
            saveButton.Text = "保存";
            saveButton.UseVisualStyleBackColor = true;
            saveButton.Click += saveButton_Click;
            //
            // label1
            //
            label1.AutoSize = true;
            label1.Font = new Font("Yu Gothic UI", 15F);
            label1.Location = new Point(242, 345);
            label1.Name = "label1";
            label1.Size = new Size(92, 28);
            label1.TabIndex = 5;
            label1.Text = "入力画像";
            //
            // label2
            //
            label2.AutoSize = true;
            label2.Font = new Font("Yu Gothic UI", 15F);
            label2.Location = new Point(587, 345);
            label2.Name = "label2";
            label2.Size = new Size(92, 28);
            label2.TabIndex = 6;
            label2.Text = "出力画像";
            //
            // statusLabel
            //
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Yu Gothic UI", 12F);
            statusLabel.Location = new Point(49, 391);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(270, 21);
            statusLabel.TabIndex = 7;
            statusLabel.Text = "[ファイル選択]から画像を選択してください.";
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(865, 436);
            Controls.Add(statusLabel);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(saveButton);
            Controls.Add(outputPictureBox);
            Controls.Add(inputPictureBox);
            Controls.Add(processButton);
            Controls.Add(selectFileButton);
            Margin = new Padding(2);
            Name = "Form1";
            Text = "PaddingTool";
            ((System.ComponentModel.ISupportInitialize)inputPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)outputPictureBox).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button selectFileButton;
        private Button processButton;
        private PictureBox inputPictureBox;
        private PictureBox outputPictureBox;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private Button saveButton;
        private Label label1;
        private Label label2;
        private Label statusLabel;
    }
}
