namespace Metin2AutoFishCSharp
{
    partial class ChatHandlerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDetecting = new System.Windows.Forms.TextBox();
            this.textBoxAnswers = new System.Windows.Forms.TextBox();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.richTextBoxReadFile = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(23, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "Tespit Edilecek Kelimler";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(23, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(341, 24);
            this.label2.TabIndex = 1;
            this.label2.Text = "Edilen Tespite Göre Verilecek Cevaplar";
            // 
            // textBoxDetecting
            // 
            this.textBoxDetecting.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxDetecting.Location = new System.Drawing.Point(28, 46);
            this.textBoxDetecting.Multiline = true;
            this.textBoxDetecting.Name = "textBoxDetecting";
            this.textBoxDetecting.Size = new System.Drawing.Size(434, 32);
            this.textBoxDetecting.TabIndex = 2;
            this.textBoxDetecting.Leave += new System.EventHandler(this.textBoxDetecting_Leave);
            // 
            // textBoxAnswers
            // 
            this.textBoxAnswers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxAnswers.Location = new System.Drawing.Point(28, 114);
            this.textBoxAnswers.Multiline = true;
            this.textBoxAnswers.Name = "textBoxAnswers";
            this.textBoxAnswers.Size = new System.Drawing.Size(434, 33);
            this.textBoxAnswers.TabIndex = 3;
            this.textBoxAnswers.Leave += new System.EventHandler(this.textBoxAnswers_Leave);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonLoad.Location = new System.Drawing.Point(167, 153);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(146, 34);
            this.buttonLoad.TabIndex = 4;
            this.buttonLoad.Text = "Verileri Yükle";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonLoad_MouseClick);
            // 
            // richTextBoxReadFile
            // 
            this.richTextBoxReadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.richTextBoxReadFile.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.richTextBoxReadFile.Location = new System.Drawing.Point(6, 209);
            this.richTextBoxReadFile.Name = "richTextBoxReadFile";
            this.richTextBoxReadFile.ReadOnly = true;
            this.richTextBoxReadFile.Size = new System.Drawing.Size(471, 301);
            this.richTextBoxReadFile.TabIndex = 6;
            this.richTextBoxReadFile.Text = "";
            // 
            // ChatHandlerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 522);
            this.Controls.Add(this.richTextBoxReadFile);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.textBoxAnswers);
            this.Controls.Add(this.textBoxDetecting);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ChatHandlerForm";
            this.Text = "ChatHandlerForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDetecting;
        private System.Windows.Forms.TextBox textBoxAnswers;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.RichTextBox richTextBoxReadFile;
    }
}