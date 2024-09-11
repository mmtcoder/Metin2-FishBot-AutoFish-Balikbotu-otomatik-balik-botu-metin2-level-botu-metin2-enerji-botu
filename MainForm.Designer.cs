using System.Runtime.InteropServices;
using System;

namespace MusicPlayerApp
{
    partial class MainForm
    {
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

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
            // Hotkey'i unregister yap
            UnregisterHotKey(this.Handle, MY_HOTKEY_ID);
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tabControlTelegram = new System.Windows.Forms.TabControl();
            this.tabPageFishing = new System.Windows.Forms.TabPage();
            this.checkBoxAdaptableFish = new System.Windows.Forms.CheckBox();
            this.checkBoxDeniz = new System.Windows.Forms.CheckBox();
            this.checkBoxWhisperActive = new System.Windows.Forms.CheckBox();
            this.checkBoxChatActive = new System.Windows.Forms.CheckBox();
            this.checkBoxFishingMiniBreak = new System.Windows.Forms.CheckBox();
            this.buttonCheckChat = new System.Windows.Forms.Button();
            this.textBoxMinMaxBreak = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxStopGameTime = new System.Windows.Forms.TextBox();
            this.textBoxMaxWorkTime = new System.Windows.Forms.TextBox();
            this.textBoxMinWorkTime = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxEnableTime = new System.Windows.Forms.CheckBox();
            this.checkBoxHepsi = new System.Windows.Forms.CheckBox();
            this.checkBoxKurbaga = new System.Windows.Forms.CheckBox();
            this.checkBoxKadife = new System.Windows.Forms.CheckBox();
            this.checkBoxPalamut = new System.Windows.Forms.CheckBox();
            this.checkBoxAltinSudak = new System.Windows.Forms.CheckBox();
            this.checkBoxYabbie = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxPathWays = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.textBoxRect = new System.Windows.Forms.TextBox();
            this.buttonQuickSS = new System.Windows.Forms.Button();
            this.labelStartStatus = new System.Windows.Forms.Label();
            this.buttonFishingStart = new System.Windows.Forms.Button();
            this.pictureBoxMainForm = new System.Windows.Forms.PictureBox();
            this.buttonScreenShot = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBoxETPPickUp = new System.Windows.Forms.CheckBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.textBoxSkillF4 = new System.Windows.Forms.TextBox();
            this.textBoxSkillF3 = new System.Windows.Forms.TextBox();
            this.textBoxSkillF2 = new System.Windows.Forms.TextBox();
            this.textBoxSkillF1 = new System.Windows.Forms.TextBox();
            this.textBoxSkillFour = new System.Windows.Forms.TextBox();
            this.textBoxSkillThree = new System.Windows.Forms.TextBox();
            this.textBoxSkillTwo = new System.Windows.Forms.TextBox();
            this.textBoxSkillOne = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.labelEnergyCristal = new System.Windows.Forms.Label();
            this.buttonEnergyStart = new System.Windows.Forms.Button();
            this.labelLevelFarmStatus = new System.Windows.Forms.Label();
            this.textBoxSp = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.trackBarSp = new System.Windows.Forms.TrackBar();
            this.textBoxHpRate = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxDex = new System.Windows.Forms.TextBox();
            this.textBoxStr = new System.Windows.Forms.TextBox();
            this.textBoxSpRate = new System.Windows.Forms.TextBox();
            this.textBoxHp = new System.Windows.Forms.TextBox();
            this.labelDex = new System.Windows.Forms.Label();
            this.labelStr = new System.Windows.Forms.Label();
            this.labelSP = new System.Windows.Forms.Label();
            this.labelHp = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.trackBarHp = new System.Windows.Forms.TrackBar();
            this.buttonLevelStart = new System.Windows.Forms.Button();
            this.tabPageTelegram = new System.Windows.Forms.TabPage();
            this.label33 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.labelTelegramStatus = new System.Windows.Forms.Label();
            this.buttonTelegramTest = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.checkBoxTelegram = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.checkBoxPCSlow = new System.Windows.Forms.CheckBox();
            this.tabControlTelegram.SuspendLayout();
            this.tabPageFishing.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainForm)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHp)).BeginInit();
            this.tabPageTelegram.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlTelegram
            // 
            this.tabControlTelegram.Controls.Add(this.tabPageFishing);
            this.tabControlTelegram.Controls.Add(this.tabPage2);
            this.tabControlTelegram.Controls.Add(this.tabPageTelegram);
            this.tabControlTelegram.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlTelegram.Location = new System.Drawing.Point(0, 0);
            this.tabControlTelegram.Name = "tabControlTelegram";
            this.tabControlTelegram.SelectedIndex = 0;
            this.tabControlTelegram.Size = new System.Drawing.Size(503, 472);
            this.tabControlTelegram.TabIndex = 0;
            // 
            // tabPageFishing
            // 
            this.tabPageFishing.Controls.Add(this.checkBoxPCSlow);
            this.tabPageFishing.Controls.Add(this.checkBoxAdaptableFish);
            this.tabPageFishing.Controls.Add(this.checkBoxDeniz);
            this.tabPageFishing.Controls.Add(this.checkBoxWhisperActive);
            this.tabPageFishing.Controls.Add(this.checkBoxChatActive);
            this.tabPageFishing.Controls.Add(this.checkBoxFishingMiniBreak);
            this.tabPageFishing.Controls.Add(this.buttonCheckChat);
            this.tabPageFishing.Controls.Add(this.textBoxMinMaxBreak);
            this.tabPageFishing.Controls.Add(this.label7);
            this.tabPageFishing.Controls.Add(this.textBoxStopGameTime);
            this.tabPageFishing.Controls.Add(this.textBoxMaxWorkTime);
            this.tabPageFishing.Controls.Add(this.textBoxMinWorkTime);
            this.tabPageFishing.Controls.Add(this.label6);
            this.tabPageFishing.Controls.Add(this.label5);
            this.tabPageFishing.Controls.Add(this.label4);
            this.tabPageFishing.Controls.Add(this.checkBoxEnableTime);
            this.tabPageFishing.Controls.Add(this.checkBoxHepsi);
            this.tabPageFishing.Controls.Add(this.checkBoxKurbaga);
            this.tabPageFishing.Controls.Add(this.checkBoxKadife);
            this.tabPageFishing.Controls.Add(this.checkBoxPalamut);
            this.tabPageFishing.Controls.Add(this.checkBoxAltinSudak);
            this.tabPageFishing.Controls.Add(this.checkBoxYabbie);
            this.tabPageFishing.Controls.Add(this.label3);
            this.tabPageFishing.Controls.Add(this.comboBoxPathWays);
            this.tabPageFishing.Controls.Add(this.label2);
            this.tabPageFishing.Controls.Add(this.label1);
            this.tabPageFishing.Controls.Add(this.textBoxFileName);
            this.tabPageFishing.Controls.Add(this.textBoxRect);
            this.tabPageFishing.Controls.Add(this.buttonQuickSS);
            this.tabPageFishing.Controls.Add(this.labelStartStatus);
            this.tabPageFishing.Controls.Add(this.buttonFishingStart);
            this.tabPageFishing.Controls.Add(this.pictureBoxMainForm);
            this.tabPageFishing.Controls.Add(this.buttonScreenShot);
            this.tabPageFishing.Location = new System.Drawing.Point(4, 22);
            this.tabPageFishing.Name = "tabPageFishing";
            this.tabPageFishing.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageFishing.Size = new System.Drawing.Size(495, 446);
            this.tabPageFishing.TabIndex = 0;
            this.tabPageFishing.Text = "Fishing";
            this.tabPageFishing.UseVisualStyleBackColor = true;
            this.tabPageFishing.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // checkBoxAdaptableFish
            // 
            this.checkBoxAdaptableFish.AutoSize = true;
            this.checkBoxAdaptableFish.Location = new System.Drawing.Point(374, 107);
            this.checkBoxAdaptableFish.Name = "checkBoxAdaptableFish";
            this.checkBoxAdaptableFish.Size = new System.Drawing.Size(93, 17);
            this.checkBoxAdaptableFish.TabIndex = 56;
            this.checkBoxAdaptableFish.Text = "Adapte Tutma";
            this.checkBoxAdaptableFish.UseVisualStyleBackColor = true;
            this.checkBoxAdaptableFish.CheckedChanged += new System.EventHandler(this.checkBoxAdaptable_CheckedChanged);
            // 
            // checkBoxDeniz
            // 
            this.checkBoxDeniz.AutoSize = true;
            this.checkBoxDeniz.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxDeniz.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxDeniz.Location = new System.Drawing.Point(405, 15);
            this.checkBoxDeniz.Name = "checkBoxDeniz";
            this.checkBoxDeniz.Size = new System.Drawing.Size(66, 17);
            this.checkBoxDeniz.TabIndex = 1;
            this.checkBoxDeniz.Text = "Denizkız";
            this.checkBoxDeniz.UseVisualStyleBackColor = true;
            this.checkBoxDeniz.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // checkBoxWhisperActive
            // 
            this.checkBoxWhisperActive.AutoSize = true;
            this.checkBoxWhisperActive.Location = new System.Drawing.Point(273, 107);
            this.checkBoxWhisperActive.Name = "checkBoxWhisperActive";
            this.checkBoxWhisperActive.Size = new System.Drawing.Size(104, 17);
            this.checkBoxWhisperActive.TabIndex = 54;
            this.checkBoxWhisperActive.Text = "Fısıltı Cevaplama";
            this.checkBoxWhisperActive.UseVisualStyleBackColor = true;
            this.checkBoxWhisperActive.CheckedChanged += new System.EventHandler(this.checkBoxWhisperActive_CheckedChanged);
            // 
            // checkBoxChatActive
            // 
            this.checkBoxChatActive.AutoSize = true;
            this.checkBoxChatActive.Location = new System.Drawing.Point(175, 108);
            this.checkBoxChatActive.Name = "checkBoxChatActive";
            this.checkBoxChatActive.Size = new System.Drawing.Size(104, 17);
            this.checkBoxChatActive.TabIndex = 53;
            this.checkBoxChatActive.Text = "Chat Cevaplama";
            this.checkBoxChatActive.UseVisualStyleBackColor = true;
            this.checkBoxChatActive.CheckedChanged += new System.EventHandler(this.checkBoxChatActive_CheckedChanged);
            // 
            // checkBoxFishingMiniBreak
            // 
            this.checkBoxFishingMiniBreak.AutoSize = true;
            this.checkBoxFishingMiniBreak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxFishingMiniBreak.Location = new System.Drawing.Point(175, 84);
            this.checkBoxFishingMiniBreak.Name = "checkBoxFishingMiniBreak";
            this.checkBoxFishingMiniBreak.Size = new System.Drawing.Size(275, 17);
            this.checkBoxFishingMiniBreak.TabIndex = 52;
            this.checkBoxFishingMiniBreak.Text = "Arada gerçekçilik için saniye cinsinden ufak mola ver";
            this.checkBoxFishingMiniBreak.UseVisualStyleBackColor = true;
            this.checkBoxFishingMiniBreak.CheckedChanged += new System.EventHandler(this.checkBoxFishingMiniBreak_CheckedChanged);
            // 
            // buttonCheckChat
            // 
            this.buttonCheckChat.Location = new System.Drawing.Point(327, 193);
            this.buttonCheckChat.Margin = new System.Windows.Forms.Padding(2);
            this.buttonCheckChat.Name = "buttonCheckChat";
            this.buttonCheckChat.Size = new System.Drawing.Size(107, 20);
            this.buttonCheckChat.TabIndex = 51;
            this.buttonCheckChat.Text = "Check Chat";
            this.buttonCheckChat.UseVisualStyleBackColor = true;
            this.buttonCheckChat.Click += new System.EventHandler(this.buttonCheckChat_Click);
            // 
            // textBoxMinMaxBreak
            // 
            this.textBoxMinMaxBreak.Location = new System.Drawing.Point(411, 132);
            this.textBoxMinMaxBreak.Name = "textBoxMinMaxBreak";
            this.textBoxMinMaxBreak.Size = new System.Drawing.Size(39, 20);
            this.textBoxMinMaxBreak.TabIndex = 50;
            this.textBoxMinMaxBreak.Leave += new System.EventHandler(this.textBoxMinMaxBreakLeave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(314, 133);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 16);
            this.label7.TabIndex = 49;
            this.label7.Text = "Min-Max Mola";
            // 
            // textBoxStopGameTime
            // 
            this.textBoxStopGameTime.Location = new System.Drawing.Point(411, 160);
            this.textBoxStopGameTime.Name = "textBoxStopGameTime";
            this.textBoxStopGameTime.Size = new System.Drawing.Size(39, 20);
            this.textBoxStopGameTime.TabIndex = 48;
            this.textBoxStopGameTime.Leave += new System.EventHandler(this.textBoxStopTime_Leave);
            // 
            // textBoxMaxWorkTime
            // 
            this.textBoxMaxWorkTime.Location = new System.Drawing.Point(263, 194);
            this.textBoxMaxWorkTime.Name = "textBoxMaxWorkTime";
            this.textBoxMaxWorkTime.Size = new System.Drawing.Size(32, 20);
            this.textBoxMaxWorkTime.TabIndex = 47;
            this.textBoxMaxWorkTime.Leave += new System.EventHandler(this.textBoxMaxWorkLeave);
            // 
            // textBoxMinWorkTime
            // 
            this.textBoxMinWorkTime.Location = new System.Drawing.Point(263, 162);
            this.textBoxMinWorkTime.Name = "textBoxMinWorkTime";
            this.textBoxMinWorkTime.Size = new System.Drawing.Size(32, 20);
            this.textBoxMinWorkTime.TabIndex = 46;
            this.textBoxMinWorkTime.Leave += new System.EventHandler(this.textBoxMinWorkLeave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.Red;
            this.label6.Location = new System.Drawing.Point(309, 162);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 16);
            this.label6.TabIndex = 45;
            this.label6.Text = "Oyun Durdurma";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(183, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 16);
            this.label5.TabIndex = 44;
            this.label5.Text = "Max Aktiflik";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(183, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 43;
            this.label4.Text = "Min Aktiflik";
            // 
            // checkBoxEnableTime
            // 
            this.checkBoxEnableTime.AutoSize = true;
            this.checkBoxEnableTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxEnableTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.checkBoxEnableTime.Location = new System.Drawing.Point(173, 133);
            this.checkBoxEnableTime.Name = "checkBoxEnableTime";
            this.checkBoxEnableTime.Size = new System.Drawing.Size(142, 20);
            this.checkBoxEnableTime.TabIndex = 42;
            this.checkBoxEnableTime.Text = "Zamanlayıcı Aktif Et";
            this.checkBoxEnableTime.UseVisualStyleBackColor = true;
            this.checkBoxEnableTime.CheckedChanged += new System.EventHandler(this.checkBoxEnableTime_CheckedChanged_1);
            this.checkBoxEnableTime.Click += new System.EventHandler(this.checkBoxEnableTime_CheckedChanged);
            // 
            // checkBoxHepsi
            // 
            this.checkBoxHepsi.AutoSize = true;
            this.checkBoxHepsi.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxHepsi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxHepsi.Location = new System.Drawing.Point(260, 60);
            this.checkBoxHepsi.Name = "checkBoxHepsi";
            this.checkBoxHepsi.Size = new System.Drawing.Size(53, 17);
            this.checkBoxHepsi.TabIndex = 41;
            this.checkBoxHepsi.Text = "Hepsi";
            this.checkBoxHepsi.UseVisualStyleBackColor = true;
            this.checkBoxHepsi.Click += new System.EventHandler(this.checkBoxsHepsi_Click);
            // 
            // checkBoxKurbaga
            // 
            this.checkBoxKurbaga.AutoSize = true;
            this.checkBoxKurbaga.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxKurbaga.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxKurbaga.Location = new System.Drawing.Point(330, 37);
            this.checkBoxKurbaga.Name = "checkBoxKurbaga";
            this.checkBoxKurbaga.Size = new System.Drawing.Size(66, 17);
            this.checkBoxKurbaga.TabIndex = 40;
            this.checkBoxKurbaga.Text = "Kurbaga";
            this.checkBoxKurbaga.UseVisualStyleBackColor = true;
            this.checkBoxKurbaga.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // checkBoxKadife
            // 
            this.checkBoxKadife.AutoSize = true;
            this.checkBoxKadife.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxKadife.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxKadife.Location = new System.Drawing.Point(330, 61);
            this.checkBoxKadife.Name = "checkBoxKadife";
            this.checkBoxKadife.Size = new System.Drawing.Size(56, 17);
            this.checkBoxKadife.TabIndex = 55;
            this.checkBoxKadife.Text = "Kadife";
            this.checkBoxKadife.UseVisualStyleBackColor = true;
            this.checkBoxKadife.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // checkBoxPalamut
            // 
            this.checkBoxPalamut.AutoSize = true;
            this.checkBoxPalamut.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxPalamut.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxPalamut.Location = new System.Drawing.Point(260, 37);
            this.checkBoxPalamut.Name = "checkBoxPalamut";
            this.checkBoxPalamut.Size = new System.Drawing.Size(64, 17);
            this.checkBoxPalamut.TabIndex = 39;
            this.checkBoxPalamut.Text = "Palamut";
            this.checkBoxPalamut.UseVisualStyleBackColor = true;
            this.checkBoxPalamut.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // checkBoxAltinSudak
            // 
            this.checkBoxAltinSudak.AutoSize = true;
            this.checkBoxAltinSudak.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxAltinSudak.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxAltinSudak.Location = new System.Drawing.Point(329, 15);
            this.checkBoxAltinSudak.Name = "checkBoxAltinSudak";
            this.checkBoxAltinSudak.Size = new System.Drawing.Size(79, 17);
            this.checkBoxAltinSudak.TabIndex = 38;
            this.checkBoxAltinSudak.Text = "GoldSudak";
            this.checkBoxAltinSudak.UseVisualStyleBackColor = true;
            this.checkBoxAltinSudak.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // checkBoxYabbie
            // 
            this.checkBoxYabbie.AutoSize = true;
            this.checkBoxYabbie.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.checkBoxYabbie.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.checkBoxYabbie.Location = new System.Drawing.Point(260, 15);
            this.checkBoxYabbie.Name = "checkBoxYabbie";
            this.checkBoxYabbie.Size = new System.Drawing.Size(59, 17);
            this.checkBoxYabbie.TabIndex = 37;
            this.checkBoxYabbie.Text = "Yabbie";
            this.checkBoxYabbie.UseVisualStyleBackColor = true;
            this.checkBoxYabbie.Click += new System.EventHandler(this.checkBoxsFishes_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(309, 390);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 18);
            this.label3.TabIndex = 36;
            this.label3.Text = "Path Ways";
            // 
            // comboBoxPathWays
            // 
            this.comboBoxPathWays.FormattingEnabled = true;
            this.comboBoxPathWays.Location = new System.Drawing.Point(312, 412);
            this.comboBoxPathWays.Margin = new System.Windows.Forms.Padding(2);
            this.comboBoxPathWays.Name = "comboBoxPathWays";
            this.comboBoxPathWays.Size = new System.Drawing.Size(92, 21);
            this.comboBoxPathWays.TabIndex = 35;
            this.comboBoxPathWays.SelectedIndexChanged += new System.EventHandler(this.comboBoxPathWays_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(308, 329);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 34;
            this.label2.Text = "File Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label1.Location = new System.Drawing.Point(165, 329);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 20);
            this.label1.TabIndex = 33;
            this.label1.Text = "Rectangle Info";
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(312, 355);
            this.textBoxFileName.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(93, 20);
            this.textBoxFileName.TabIndex = 32;
            this.textBoxFileName.Leave += new System.EventHandler(this.textBoxFileName_Leave);
            // 
            // textBoxRect
            // 
            this.textBoxRect.Location = new System.Drawing.Point(169, 355);
            this.textBoxRect.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxRect.Name = "textBoxRect";
            this.textBoxRect.Size = new System.Drawing.Size(139, 20);
            this.textBoxRect.TabIndex = 31;
            this.textBoxRect.Leave += new System.EventHandler(this.textBoxRect_Leave);
            // 
            // buttonQuickSS
            // 
            this.buttonQuickSS.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonQuickSS.ForeColor = System.Drawing.Color.Teal;
            this.buttonQuickSS.Location = new System.Drawing.Point(169, 396);
            this.buttonQuickSS.Margin = new System.Windows.Forms.Padding(2);
            this.buttonQuickSS.Name = "buttonQuickSS";
            this.buttonQuickSS.Size = new System.Drawing.Size(130, 43);
            this.buttonQuickSS.TabIndex = 30;
            this.buttonQuickSS.Text = "Take SShot";
            this.buttonQuickSS.UseVisualStyleBackColor = true;
            this.buttonQuickSS.Click += new System.EventHandler(this.buttonQuickSS_Click);
            // 
            // labelStartStatus
            // 
            this.labelStartStatus.AutoSize = true;
            this.labelStartStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelStartStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStartStatus.Location = new System.Drawing.Point(171, 291);
            this.labelStartStatus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelStartStatus.Name = "labelStartStatus";
            this.labelStartStatus.Size = new System.Drawing.Size(57, 24);
            this.labelStartStatus.TabIndex = 29;
            this.labelStartStatus.Text = "Good";
            this.labelStartStatus.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buttonFishingStart
            // 
            this.buttonFishingStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonFishingStart.Location = new System.Drawing.Point(175, 231);
            this.buttonFishingStart.Margin = new System.Windows.Forms.Padding(2);
            this.buttonFishingStart.Name = "buttonFishingStart";
            this.buttonFishingStart.Size = new System.Drawing.Size(184, 46);
            this.buttonFishingStart.TabIndex = 28;
            this.buttonFishingStart.Text = "Start";
            this.buttonFishingStart.UseVisualStyleBackColor = true;
            this.buttonFishingStart.Click += new System.EventHandler(this.buttonFishingStartClick);
            // 
            // pictureBoxMainForm
            // 
            this.pictureBoxMainForm.Location = new System.Drawing.Point(7, 15);
            this.pictureBoxMainForm.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBoxMainForm.Name = "pictureBoxMainForm";
            this.pictureBoxMainForm.Size = new System.Drawing.Size(153, 424);
            this.pictureBoxMainForm.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBoxMainForm.TabIndex = 27;
            this.pictureBoxMainForm.TabStop = false;
            // 
            // buttonScreenShot
            // 
            this.buttonScreenShot.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonScreenShot.Location = new System.Drawing.Point(175, 15);
            this.buttonScreenShot.Margin = new System.Windows.Forms.Padding(2);
            this.buttonScreenShot.Name = "buttonScreenShot";
            this.buttonScreenShot.Size = new System.Drawing.Size(79, 63);
            this.buttonScreenShot.TabIndex = 26;
            this.buttonScreenShot.Text = "Screen Shot";
            this.buttonScreenShot.UseVisualStyleBackColor = true;
            this.buttonScreenShot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.buttonScreenShot_MouseClick);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBoxETPPickUp);
            this.tabPage2.Controls.Add(this.label29);
            this.tabPage2.Controls.Add(this.label28);
            this.tabPage2.Controls.Add(this.textBoxSkillF4);
            this.tabPage2.Controls.Add(this.textBoxSkillF3);
            this.tabPage2.Controls.Add(this.textBoxSkillF2);
            this.tabPage2.Controls.Add(this.textBoxSkillF1);
            this.tabPage2.Controls.Add(this.textBoxSkillFour);
            this.tabPage2.Controls.Add(this.textBoxSkillThree);
            this.tabPage2.Controls.Add(this.textBoxSkillTwo);
            this.tabPage2.Controls.Add(this.textBoxSkillOne);
            this.tabPage2.Controls.Add(this.label23);
            this.tabPage2.Controls.Add(this.label22);
            this.tabPage2.Controls.Add(this.label21);
            this.tabPage2.Controls.Add(this.label20);
            this.tabPage2.Controls.Add(this.label19);
            this.tabPage2.Controls.Add(this.label18);
            this.tabPage2.Controls.Add(this.label17);
            this.tabPage2.Controls.Add(this.label16);
            this.tabPage2.Controls.Add(this.label15);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.labelLevelFarmStatus);
            this.tabPage2.Controls.Add(this.textBoxSp);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.trackBarSp);
            this.tabPage2.Controls.Add(this.textBoxHpRate);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.textBoxDex);
            this.tabPage2.Controls.Add(this.textBoxStr);
            this.tabPage2.Controls.Add(this.textBoxSpRate);
            this.tabPage2.Controls.Add(this.textBoxHp);
            this.tabPage2.Controls.Add(this.labelDex);
            this.tabPage2.Controls.Add(this.labelStr);
            this.tabPage2.Controls.Add(this.labelSP);
            this.tabPage2.Controls.Add(this.labelHp);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.trackBarHp);
            this.tabPage2.Controls.Add(this.buttonLevelStart);
            this.tabPage2.ForeColor = System.Drawing.Color.Turquoise;
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(495, 446);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Level and Farm";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // checkBoxETPPickUp
            // 
            this.checkBoxETPPickUp.AutoSize = true;
            this.checkBoxETPPickUp.ForeColor = System.Drawing.Color.DarkGoldenrod;
            this.checkBoxETPPickUp.Location = new System.Drawing.Point(23, 258);
            this.checkBoxETPPickUp.Name = "checkBoxETPPickUp";
            this.checkBoxETPPickUp.Size = new System.Drawing.Size(324, 17);
            this.checkBoxETPPickUp.TabIndex = 39;
            this.checkBoxETPPickUp.Text = "Yerden ETP görünce topla(Sadece yakınındaki düşeni görebilir)";
            this.checkBoxETPPickUp.UseVisualStyleBackColor = true;
            this.checkBoxETPPickUp.CheckedChanged += new System.EventHandler(this.checkBoxETPPickUp_CheckedChanged);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.label29.Location = new System.Drawing.Point(15, 187);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(361, 13);
            this.label29.TabIndex = 38;
            this.label29.Text = "npc uzakta olduğu zamanda vurmuş olarak sayıp vurmama ihtimalide vardır.";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label28.Location = new System.Drawing.Point(16, 174);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(207, 13);
            this.label28.TabIndex = 37;
            this.label28.Text = "Diğer beceri türleride süre geldiğince çalışır";
            // 
            // textBoxSkillF4
            // 
            this.textBoxSkillF4.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillF4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillF4.Location = new System.Drawing.Point(388, 222);
            this.textBoxSkillF4.Name = "textBoxSkillF4";
            this.textBoxSkillF4.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillF4.TabIndex = 36;
            this.textBoxSkillF4.Text = "0";
            this.textBoxSkillF4.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillF3
            // 
            this.textBoxSkillF3.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillF3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillF3.Location = new System.Drawing.Point(338, 222);
            this.textBoxSkillF3.Name = "textBoxSkillF3";
            this.textBoxSkillF3.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillF3.TabIndex = 35;
            this.textBoxSkillF3.Text = "0";
            this.textBoxSkillF3.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillF2
            // 
            this.textBoxSkillF2.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillF2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillF2.Location = new System.Drawing.Point(280, 222);
            this.textBoxSkillF2.Name = "textBoxSkillF2";
            this.textBoxSkillF2.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillF2.TabIndex = 34;
            this.textBoxSkillF2.Text = "0";
            this.textBoxSkillF2.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillF1
            // 
            this.textBoxSkillF1.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillF1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillF1.Location = new System.Drawing.Point(233, 222);
            this.textBoxSkillF1.Name = "textBoxSkillF1";
            this.textBoxSkillF1.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillF1.TabIndex = 33;
            this.textBoxSkillF1.Text = "0";
            this.textBoxSkillF1.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillFour
            // 
            this.textBoxSkillFour.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillFour.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillFour.Location = new System.Drawing.Point(161, 222);
            this.textBoxSkillFour.Name = "textBoxSkillFour";
            this.textBoxSkillFour.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillFour.TabIndex = 32;
            this.textBoxSkillFour.Text = "0";
            this.textBoxSkillFour.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillThree
            // 
            this.textBoxSkillThree.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillThree.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillThree.Location = new System.Drawing.Point(119, 222);
            this.textBoxSkillThree.Name = "textBoxSkillThree";
            this.textBoxSkillThree.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillThree.TabIndex = 31;
            this.textBoxSkillThree.Text = "0";
            this.textBoxSkillThree.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillTwo
            // 
            this.textBoxSkillTwo.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillTwo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillTwo.Location = new System.Drawing.Point(72, 222);
            this.textBoxSkillTwo.Name = "textBoxSkillTwo";
            this.textBoxSkillTwo.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillTwo.TabIndex = 30;
            this.textBoxSkillTwo.Text = "0";
            this.textBoxSkillTwo.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // textBoxSkillOne
            // 
            this.textBoxSkillOne.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSkillOne.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSkillOne.Location = new System.Drawing.Point(29, 222);
            this.textBoxSkillOne.Name = "textBoxSkillOne";
            this.textBoxSkillOne.Size = new System.Drawing.Size(31, 22);
            this.textBoxSkillOne.TabIndex = 29;
            this.textBoxSkillOne.Text = "0";
            this.textBoxSkillOne.Leave += new System.EventHandler(this.TextBoxes_Skills_Leave);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.ForeColor = System.Drawing.Color.Fuchsia;
            this.label23.Location = new System.Drawing.Point(386, 206);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(19, 13);
            this.label23.TabIndex = 28;
            this.label23.Text = "F4";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.ForeColor = System.Drawing.Color.Fuchsia;
            this.label22.Location = new System.Drawing.Point(339, 206);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(19, 13);
            this.label22.TabIndex = 27;
            this.label22.Text = "F3";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.ForeColor = System.Drawing.Color.Fuchsia;
            this.label21.Location = new System.Drawing.Point(292, 206);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 13);
            this.label21.TabIndex = 26;
            this.label21.Text = "F2";
            this.label21.Click += new System.EventHandler(this.label21_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.Color.Fuchsia;
            this.label20.Location = new System.Drawing.Point(245, 206);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(19, 13);
            this.label20.TabIndex = 25;
            this.label20.Text = "F1";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.ForeColor = System.Drawing.Color.Fuchsia;
            this.label19.Location = new System.Drawing.Point(173, 206);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(13, 13);
            this.label19.TabIndex = 24;
            this.label19.Text = "4";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.Color.Fuchsia;
            this.label18.Location = new System.Drawing.Point(126, 206);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(13, 13);
            this.label18.TabIndex = 23;
            this.label18.Text = "3";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.ForeColor = System.Drawing.Color.Fuchsia;
            this.label17.Location = new System.Drawing.Point(79, 206);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 22;
            this.label17.Text = "2";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.ForeColor = System.Drawing.Color.Fuchsia;
            this.label16.Location = new System.Drawing.Point(32, 206);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(13, 13);
            this.label16.TabIndex = 21;
            this.label16.Text = "1";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(13, 161);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(461, 13);
            this.label15.TabIndex = 20;
            this.label15.Text = "Sadece Pasif becerileri veya az hatalı olarak Uzaktan vuran beceriler için(Süre m" +
    "antığınca çalışır)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label27);
            this.groupBox1.Controls.Add(this.label26);
            this.groupBox1.Controls.Add(this.label25);
            this.groupBox1.Controls.Add(this.label24);
            this.groupBox1.Controls.Add(this.labelEnergyCristal);
            this.groupBox1.Controls.Add(this.buttonEnergyStart);
            this.groupBox1.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.groupBox1.Location = new System.Drawing.Point(6, 314);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 94);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Enerji kristali";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.ForeColor = System.Drawing.Color.Goldenrod;
            this.label27.Location = new System.Drawing.Point(7, 58);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(332, 13);
            this.label27.TabIndex = 24;
            this.label27.Text = "Market aç yazısı ikinci sırada olsun. Ardından silahcının orada başlatın";
            this.label27.Click += new System.EventHandler(this.label27_Click);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(7, 45);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(315, 13);
            this.label26.TabIndex = 23;
            this.label26.Text = "Silahcıdan yeni bir koku görevi alınmış olsun ama görevi yapmayın";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.ForeColor = System.Drawing.Color.Red;
            this.label25.Location = new System.Drawing.Point(7, 32);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(276, 13);
            this.label25.TabIndex = 22;
            this.label25.Text = "Simyacıdan enerji kristali görevini alın(35 lvl olması gerek )";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.label24.Location = new System.Drawing.Point(6, 16);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(276, 13);
            this.label24.TabIndex = 21;
            this.label24.Text = "Sadece kırmızı bayrakta çalıştır.(Atta takılma ihtimali artar))";
            // 
            // labelEnergyCristal
            // 
            this.labelEnergyCristal.AutoSize = true;
            this.labelEnergyCristal.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelEnergyCristal.ForeColor = System.Drawing.Color.Red;
            this.labelEnergyCristal.Location = new System.Drawing.Point(163, 69);
            this.labelEnergyCristal.Name = "labelEnergyCristal";
            this.labelEnergyCristal.Size = new System.Drawing.Size(72, 22);
            this.labelEnergyCristal.TabIndex = 20;
            this.labelEnergyCristal.Text = "Waiting";
            // 
            // buttonEnergyStart
            // 
            this.buttonEnergyStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonEnergyStart.Location = new System.Drawing.Point(337, 19);
            this.buttonEnergyStart.Name = "buttonEnergyStart";
            this.buttonEnergyStart.Size = new System.Drawing.Size(97, 23);
            this.buttonEnergyStart.TabIndex = 0;
            this.buttonEnergyStart.Text = "START";
            this.buttonEnergyStart.UseVisualStyleBackColor = true;
            this.buttonEnergyStart.Click += new System.EventHandler(this.buttonEnergyCristalStart_Click);
            // 
            // labelLevelFarmStatus
            // 
            this.labelLevelFarmStatus.AutoSize = true;
            this.labelLevelFarmStatus.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelLevelFarmStatus.ForeColor = System.Drawing.Color.Red;
            this.labelLevelFarmStatus.Location = new System.Drawing.Point(6, 278);
            this.labelLevelFarmStatus.Name = "labelLevelFarmStatus";
            this.labelLevelFarmStatus.Size = new System.Drawing.Size(72, 22);
            this.labelLevelFarmStatus.TabIndex = 18;
            this.labelLevelFarmStatus.Text = "Waiting";
            // 
            // textBoxSp
            // 
            this.textBoxSp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSp.Location = new System.Drawing.Point(56, 69);
            this.textBoxSp.Name = "textBoxSp";
            this.textBoxSp.Size = new System.Drawing.Size(31, 22);
            this.textBoxSp.TabIndex = 17;
            this.textBoxSp.Text = "2";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label11.ForeColor = System.Drawing.Color.Lime;
            this.label11.Location = new System.Drawing.Point(170, 127);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 16);
            this.label11.TabIndex = 15;
            this.label11.Text = "SP  YÜZDESİ =";
            // 
            // trackBarSp
            // 
            this.trackBarSp.Location = new System.Drawing.Point(322, 112);
            this.trackBarSp.Maximum = 100;
            this.trackBarSp.Name = "trackBarSp";
            this.trackBarSp.Size = new System.Drawing.Size(131, 45);
            this.trackBarSp.SmallChange = 5;
            this.trackBarSp.TabIndex = 14;
            this.trackBarSp.Value = 50;
            this.trackBarSp.ValueChanged += new System.EventHandler(this.trackBarSp_ValueChanged);
            // 
            // textBoxHpRate
            // 
            this.textBoxHpRate.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxHpRate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxHpRate.Location = new System.Drawing.Point(276, 55);
            this.textBoxHpRate.Name = "textBoxHpRate";
            this.textBoxHpRate.Size = new System.Drawing.Size(31, 22);
            this.textBoxHpRate.TabIndex = 13;
            this.textBoxHpRate.Text = "50";
            this.textBoxHpRate.Leave += new System.EventHandler(this.textBoxHpAndSPRate_Leave);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.ForeColor = System.Drawing.Color.Lime;
            this.label10.Location = new System.Drawing.Point(170, 61);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 16);
            this.label10.TabIndex = 12;
            this.label10.Text = "HP  YÜZDESİ =";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(208, 17);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(195, 19);
            this.label9.TabIndex = 11;
            this.label9.Text = "Hp ve Sp Yüzde Aralıklar";
            // 
            // textBoxDex
            // 
            this.textBoxDex.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxDex.Location = new System.Drawing.Point(56, 125);
            this.textBoxDex.Name = "textBoxDex";
            this.textBoxDex.Size = new System.Drawing.Size(31, 22);
            this.textBoxDex.TabIndex = 10;
            this.textBoxDex.Text = "1";
            this.textBoxDex.Leave += new System.EventHandler(this.textBoxStatus_Leave);
            // 
            // textBoxStr
            // 
            this.textBoxStr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxStr.Location = new System.Drawing.Point(56, 97);
            this.textBoxStr.Name = "textBoxStr";
            this.textBoxStr.Size = new System.Drawing.Size(31, 22);
            this.textBoxStr.TabIndex = 9;
            this.textBoxStr.Text = "3";
            this.textBoxStr.Leave += new System.EventHandler(this.textBoxStatus_Leave);
            // 
            // textBoxSpRate
            // 
            this.textBoxSpRate.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxSpRate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxSpRate.Location = new System.Drawing.Point(276, 121);
            this.textBoxSpRate.Name = "textBoxSpRate";
            this.textBoxSpRate.Size = new System.Drawing.Size(31, 22);
            this.textBoxSpRate.TabIndex = 16;
            this.textBoxSpRate.Text = "50";
            this.textBoxSpRate.Leave += new System.EventHandler(this.textBoxHpAndSPRate_Leave);
            // 
            // textBoxHp
            // 
            this.textBoxHp.BackColor = System.Drawing.SystemColors.Window;
            this.textBoxHp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxHp.Location = new System.Drawing.Point(56, 45);
            this.textBoxHp.Name = "textBoxHp";
            this.textBoxHp.Size = new System.Drawing.Size(31, 22);
            this.textBoxHp.TabIndex = 7;
            this.textBoxHp.Text = "4";
            this.textBoxHp.Leave += new System.EventHandler(this.textBoxStatus_Leave);
            // 
            // labelDex
            // 
            this.labelDex.AutoSize = true;
            this.labelDex.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelDex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelDex.Location = new System.Drawing.Point(13, 128);
            this.labelDex.Name = "labelDex";
            this.labelDex.Size = new System.Drawing.Size(44, 16);
            this.labelDex.TabIndex = 6;
            this.labelDex.Text = "DEX =";
            // 
            // labelStr
            // 
            this.labelStr.AutoSize = true;
            this.labelStr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelStr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelStr.Location = new System.Drawing.Point(13, 103);
            this.labelStr.Name = "labelStr";
            this.labelStr.Size = new System.Drawing.Size(44, 16);
            this.labelStr.TabIndex = 5;
            this.labelStr.Text = "STR =";
            // 
            // labelSP
            // 
            this.labelSP.AutoSize = true;
            this.labelSP.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelSP.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelSP.Location = new System.Drawing.Point(13, 75);
            this.labelSP.Name = "labelSP";
            this.labelSP.Size = new System.Drawing.Size(37, 16);
            this.labelSP.TabIndex = 4;
            this.labelSP.Text = "SP =";
            // 
            // labelHp
            // 
            this.labelHp.AutoSize = true;
            this.labelHp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelHp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.labelHp.Location = new System.Drawing.Point(13, 48);
            this.labelHp.Name = "labelHp";
            this.labelHp.Size = new System.Drawing.Size(37, 16);
            this.labelHp.TabIndex = 3;
            this.labelHp.Text = "HP =";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.ForeColor = System.Drawing.Color.Blue;
            this.label8.Location = new System.Drawing.Point(9, 17);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(167, 19);
            this.label8.TabIndex = 2;
            this.label8.Text = "Statü Önceliği Belirle";
            // 
            // trackBarHp
            // 
            this.trackBarHp.Location = new System.Drawing.Point(322, 46);
            this.trackBarHp.Maximum = 100;
            this.trackBarHp.Name = "trackBarHp";
            this.trackBarHp.Size = new System.Drawing.Size(131, 45);
            this.trackBarHp.SmallChange = 5;
            this.trackBarHp.TabIndex = 1;
            this.trackBarHp.Value = 50;
            this.trackBarHp.ValueChanged += new System.EventHandler(this.trackBarHp_ValueChanged);
            // 
            // buttonLevelStart
            // 
            this.buttonLevelStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonLevelStart.Location = new System.Drawing.Point(375, 271);
            this.buttonLevelStart.Name = "buttonLevelStart";
            this.buttonLevelStart.Size = new System.Drawing.Size(95, 29);
            this.buttonLevelStart.TabIndex = 0;
            this.buttonLevelStart.Text = "START";
            this.buttonLevelStart.UseVisualStyleBackColor = true;
            this.buttonLevelStart.Click += new System.EventHandler(this.buttonLevelStart_Click);
            // 
            // tabPageTelegram
            // 
            this.tabPageTelegram.Controls.Add(this.label33);
            this.tabPageTelegram.Controls.Add(this.label32);
            this.tabPageTelegram.Controls.Add(this.textBox1);
            this.tabPageTelegram.Controls.Add(this.label31);
            this.tabPageTelegram.Controls.Add(this.label30);
            this.tabPageTelegram.Controls.Add(this.labelTelegramStatus);
            this.tabPageTelegram.Controls.Add(this.buttonTelegramTest);
            this.tabPageTelegram.Controls.Add(this.label14);
            this.tabPageTelegram.Controls.Add(this.label13);
            this.tabPageTelegram.Controls.Add(this.checkBoxTelegram);
            this.tabPageTelegram.Controls.Add(this.label12);
            this.tabPageTelegram.Location = new System.Drawing.Point(4, 22);
            this.tabPageTelegram.Name = "tabPageTelegram";
            this.tabPageTelegram.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTelegram.Size = new System.Drawing.Size(495, 446);
            this.tabPageTelegram.TabIndex = 2;
            this.tabPageTelegram.Text = "Haberleşme ve Diğer";
            this.tabPageTelegram.UseVisualStyleBackColor = true;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label33.ForeColor = System.Drawing.Color.Red;
            this.label33.Location = new System.Drawing.Point(172, 331);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(91, 20);
            this.label33.TabIndex = 10;
            this.label33.Text = "Kuveyt Türk";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label32.ForeColor = System.Drawing.Color.Red;
            this.label32.Location = new System.Drawing.Point(22, 331);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(135, 20);
            this.label32.TabIndex = 9;
            this.label32.Text = "Mümtaz Taşdelen";
            // 
            // textBox1
            // 
            this.textBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.textBox1.Location = new System.Drawing.Point(26, 308);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(174, 20);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "TR160020500009467150900001 ";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label31.Location = new System.Drawing.Point(23, 280);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(300, 13);
            this.label31.TabIndex = 7;
            this.label31.Text = "Eğer program işine yarıyor ve emeğimi ödüllendirmek isterseniz.";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label30.Location = new System.Drawing.Point(19, 247);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(118, 20);
            this.label30.TabIndex = 6;
            this.label30.Text = "BAĞIŞ YAP :=)";
            // 
            // labelTelegramStatus
            // 
            this.labelTelegramStatus.AutoSize = true;
            this.labelTelegramStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelTelegramStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelTelegramStatus.Location = new System.Drawing.Point(181, 144);
            this.labelTelegramStatus.Name = "labelTelegramStatus";
            this.labelTelegramStatus.Size = new System.Drawing.Size(82, 20);
            this.labelTelegramStatus.TabIndex = 5;
            this.labelTelegramStatus.Text = "Bekleniyor";
            this.labelTelegramStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonTelegramTest
            // 
            this.buttonTelegramTest.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonTelegramTest.ForeColor = System.Drawing.Color.Blue;
            this.buttonTelegramTest.Location = new System.Drawing.Point(23, 131);
            this.buttonTelegramTest.Name = "buttonTelegramTest";
            this.buttonTelegramTest.Size = new System.Drawing.Size(120, 42);
            this.buttonTelegramTest.TabIndex = 4;
            this.buttonTelegramTest.Text = "Test Et";
            this.buttonTelegramTest.UseVisualStyleBackColor = true;
            this.buttonTelegramTest.Click += new System.EventHandler(this.buttonTelegramTest_Click);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label14.Location = new System.Drawing.Point(164, 56);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(288, 45);
            this.label14.TabIndex = 3;
            this.label14.Text = "kanalına start tuşuna basın veya mesaj gönderin";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label13.Location = new System.Drawing.Point(22, 56);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(136, 20);
            this.label13.TabIndex = 2;
            this.label13.Text = "@metin2gamebot";
            // 
            // checkBoxTelegram
            // 
            this.checkBoxTelegram.AutoSize = true;
            this.checkBoxTelegram.Location = new System.Drawing.Point(185, 23);
            this.checkBoxTelegram.Name = "checkBoxTelegram";
            this.checkBoxTelegram.Size = new System.Drawing.Size(15, 14);
            this.checkBoxTelegram.TabIndex = 1;
            this.checkBoxTelegram.UseVisualStyleBackColor = true;
            this.checkBoxTelegram.CheckedChanged += new System.EventHandler(this.checkBoxTelegram_CheckedChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label12.ForeColor = System.Drawing.Color.Red;
            this.label12.Location = new System.Drawing.Point(19, 18);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(160, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "Telegram Bot Aktif Et";
            // 
            // checkBoxPCSlow
            // 
            this.checkBoxPCSlow.AutoSize = true;
            this.checkBoxPCSlow.Location = new System.Drawing.Point(374, 231);
            this.checkBoxPCSlow.Name = "checkBoxPCSlow";
            this.checkBoxPCSlow.Size = new System.Drawing.Size(87, 17);
            this.checkBoxPCSlow.TabIndex = 57;
            this.checkBoxPCSlow.Text = "PC Yavaşsa ";
            this.checkBoxPCSlow.UseVisualStyleBackColor = true;
            this.checkBoxPCSlow.CheckedChanged += new System.EventHandler(this.checkBoxPCSlow_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 472);
            this.Controls.Add(this.tabControlTelegram);
            this.ForeColor = System.Drawing.SystemColors.Highlight;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainForm";
            this.Text = "MusicPlayer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControlTelegram.ResumeLayout(false);
            this.tabPageFishing.ResumeLayout(false);
            this.tabPageFishing.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMainForm)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarHp)).EndInit();
            this.tabPageTelegram.ResumeLayout(false);
            this.tabPageTelegram.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlTelegram;
        private System.Windows.Forms.TabPage tabPageFishing;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button buttonCheckChat;
        private System.Windows.Forms.TextBox textBoxMinMaxBreak;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxStopGameTime;
        private System.Windows.Forms.TextBox textBoxMaxWorkTime;
        private System.Windows.Forms.TextBox textBoxMinWorkTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxEnableTime;
        private System.Windows.Forms.CheckBox checkBoxHepsi;
        private System.Windows.Forms.CheckBox checkBoxKurbaga;
        private System.Windows.Forms.CheckBox checkBoxKadife;
        private System.Windows.Forms.CheckBox checkBoxPalamut;
        private System.Windows.Forms.CheckBox checkBoxAltinSudak;
        private System.Windows.Forms.CheckBox checkBoxYabbie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxPathWays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.TextBox textBoxRect;
        private System.Windows.Forms.Button buttonQuickSS;
        private System.Windows.Forms.Label labelStartStatus;
        private System.Windows.Forms.Button buttonFishingStart;
        public System.Windows.Forms.PictureBox pictureBoxMainForm;
        private System.Windows.Forms.Button buttonScreenShot;
        private System.Windows.Forms.Button buttonLevelStart;
        private System.Windows.Forms.TrackBar trackBarHp;
        private System.Windows.Forms.Label labelDex;
        private System.Windows.Forms.Label labelStr;
        private System.Windows.Forms.Label labelSP;
        private System.Windows.Forms.Label labelHp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxDex;
        private System.Windows.Forms.TextBox textBoxStr;
        private System.Windows.Forms.TextBox textBoxHp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxHpRate;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxSpRate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TrackBar trackBarSp;
        private System.Windows.Forms.TextBox textBoxSp;
        private System.Windows.Forms.Label labelLevelFarmStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonEnergyStart;
        private System.Windows.Forms.Label labelEnergyCristal;
        private System.Windows.Forms.TabPage tabPageTelegram;
        private System.Windows.Forms.CheckBox checkBoxTelegram;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label labelTelegramStatus;
        private System.Windows.Forms.Button buttonTelegramTest;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxSkillF4;
        private System.Windows.Forms.TextBox textBoxSkillF3;
        private System.Windows.Forms.TextBox textBoxSkillF2;
        private System.Windows.Forms.TextBox textBoxSkillF1;
        private System.Windows.Forms.TextBox textBoxSkillFour;
        private System.Windows.Forms.TextBox textBoxSkillThree;
        private System.Windows.Forms.TextBox textBoxSkillTwo;
        private System.Windows.Forms.TextBox textBoxSkillOne;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.CheckBox checkBoxFishingMiniBreak;
        private System.Windows.Forms.CheckBox checkBoxWhisperActive;
        private System.Windows.Forms.CheckBox checkBoxChatActive;
        private System.Windows.Forms.CheckBox checkBoxETPPickUp;
        private System.Windows.Forms.CheckBox checkBoxDeniz;
        private System.Windows.Forms.CheckBox checkBoxAdaptableFish;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.CheckBox checkBoxPCSlow;
    }
}

