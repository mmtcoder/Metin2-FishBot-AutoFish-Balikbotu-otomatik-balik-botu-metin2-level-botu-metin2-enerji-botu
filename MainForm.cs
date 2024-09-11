using Metin2AutoFishCSharp;
using Metin2AutoFishCSharp.Sources;
using Metin2AutoFishCSharp.Sources.ChatHandler;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.GameHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace MusicPlayerApp
{
    public partial class MainForm : Form
    {
        FullScreen fullScreen;
        public static PictureBox pictureBox;
        public static Label labelCopyStartStatus;
        public static Label labelCopyLevelFarmStatus;
        public static Label labelCopyEnergyCristalStatus;
        public static Button buttonFishingStartCopy;
        public static Button buttonLevelFarmStartCopy;
        
        private ImageObjects imageObjects;
        private ThreadsHandler threadsHandler;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;

        private TelegramBot telegramBot;
        //ChatHandlerForm chatHandlerForm;

        private static FullScreen fullScreenStatic;
        private volatile int [] arrayTextBoxResult = new int[5];
        private volatile string fileName = null;
        private volatile PathWayStruct pathWay;

        readonly int rectX = 0;
        readonly int rectY = 1;
        readonly int rectWidth = 2;
        readonly int rectHeight = 3;
        readonly int timeSecondIndex = 4;

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
       

        private const int MY_HOTKEY_ID = 1;
        private const uint MOD_CONTROL = 0x0002; // Ctrl tuşu için modifiyer
        private const uint VK_O = 0x4F; // O harfi için sanal tuş kodu

        public MainForm()
        {
            InitializeComponent();
            Thread.CurrentThread.Name = "Main Thread";
            DebugPfCnsl.println( "MainForm constructor is called");
            DebugPfCnsl.println(Thread.CurrentThread.Name);
            InitializeToolTip();
            pictureBox = pictureBoxMainForm;
            labelCopyStartStatus = labelStartStatus;
            labelCopyLevelFarmStatus = labelLevelFarmStatus;
            labelCopyEnergyCristalStatus = labelEnergyCristal;
            buttonFishingStartCopy = buttonFishingStart;
            buttonLevelFarmStartCopy = buttonLevelStart;
            imageObjects = new ImageObjects();
            screenShot = new ScreenShotWinAPI();
            threadsHandler = new ThreadsHandler(imageObjects);
            fullScreenStatic = new FullScreen();
            coor = new GameObjectCoordinates(imageObjects);
            // Hotkey'i kaydet
            RegisterHotKey(this.Handle, MY_HOTKEY_ID, MOD_CONTROL, VK_O);
            // chatHandlerForm = new ChatHandlerForm();



            LoadComboBox();
            LoadCheckBoxes();
            EnableOrDisableTimerCheckBox(false);

        }

        protected override void WndProc(ref Message m)
        {
            const int WM_HOTKEY = 0x0312;
            if (m.Msg == WM_HOTKEY && m.WParam.ToInt32() == MY_HOTKEY_ID)
            {
                // Ctrl + O tuş kombinasyonuna basıldığında yapılacak işlem
                //MessageBox.Show("Ctrl + O tuşuna basıldı!");

                if(!ThreadGlobals.isFishingStopped)
                {                   
                    buttonFishingStart.PerformClick();
                    labelStartStatus.Text = "Game is stopped via hot keys";
                }
                if (!ThreadGlobals.isLevelFarmStopped)
                {
                    buttonLevelStart.PerformClick();
                    labelLevelFarmStatus.Text = "Game is stopped via hot keys";

                }
                if(!ThreadGlobals.isEnergyCristalStopped)
                {
                    buttonEnergyStart.PerformClick();
                    labelEnergyCristal.Text = "Game is stopped via hot keys";
                }
              /*  if (TelegramBot.TELEGRAM_BOT_IS_READY)
                {
                    TelegramBot.TELEGRAM_BOT_IS_READY = false;
                    TelegramBot.TELEGRAM_SEND_MESSAGE = null;

                    buttonTelegramTest.Text = "Test";
                   
                }*/
            }
            base.WndProc(ref m);
            
        }


        private void buttonFishingStartClick(object sender, EventArgs e)
        {
            // DebugPfCnsl.println("value " + tabPageFishing.CanFocus);
            if (ThreadGlobals.isLevelFarmStopped)
            {
                if (ThreadGlobals.isEnergyCristalStopped)
                {
                    if (ThreadGlobals.isFishingStopped)
                    {
                        ThreadGlobals.isFishingStopped = false;
                        threadsHandler.Start();
                        buttonFishingStartCopy.Text = "Fishing Stop";

                    }
                    else
                    {
                        ThreadGlobals.isFishingStopped = true;
                        threadsHandler.Stop();
                        threadsHandler.HandleFormElement(labelCopyStartStatus, "Fishing Bot is stopped");
                        buttonFishingStartCopy.Text = "Fishing Start";
                    }
                }
                else
                {
                    MessageBox.Show("Level Kasma Aktifken Balıkçılığı Başlatamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Enerji Kristali Aktifken Balıkçılığı Başlatamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonLevelStart_Click(object sender, EventArgs e)
        {
            if (ThreadGlobals.isFishingStopped)
            {
                if (ThreadGlobals.isEnergyCristalStopped)
                {
                    if (ThreadGlobals.isLevelFarmStopped)
                    {
                        if (ThreadGlobals.HP_SP_RATE[0] == 0 || ThreadGlobals.HP_SP_RATE[1] == 0)
                        {
                            if (int.TryParse(textBoxHpRate.Text, out int value) &&
                                int.TryParse(textBoxSpRate.Text, out int spValue))
                            {
                                ThreadGlobals.HP_SP_RATE[0] = value;
                                ThreadGlobals.HP_SP_RATE[1] = spValue;
                            }

                        }
                        if (ThreadGlobals.STATUS_PRIORITY[0] == 0 || ThreadGlobals.STATUS_PRIORITY[1] == 0
                            || ThreadGlobals.STATUS_PRIORITY[2] == 0 || ThreadGlobals.STATUS_PRIORITY[3] == 0)
                        {
                            if (int.TryParse(textBoxHp.Text, out int hpStatusPrio) && int.TryParse(textBoxSp.Text, out int spStatusPrio)
                                && int.TryParse(textBoxStr.Text, out int strStatusPrio) && int.TryParse(textBoxDex.Text, out int dexStatusPrio))
                            {
                                ThreadGlobals.STATUS_PRIORITY[0] = hpStatusPrio;
                                ThreadGlobals.STATUS_PRIORITY[1] = spStatusPrio;
                                ThreadGlobals.STATUS_PRIORITY[2] = strStatusPrio;
                                ThreadGlobals.STATUS_PRIORITY[3] = dexStatusPrio;
                            }
                        }
                        ThreadGlobals.isLevelFarmStopped = false;
                        threadsHandler.Start();
                        buttonLevelStart.Text = "STOP";
                    }
                    else
                    {
                        ThreadGlobals.isLevelFarmStopped = true;
                        threadsHandler.Stop();
                        threadsHandler.HandleFormElement(labelLevelFarmStatus, "Level Farm Bot is stopped");
                        buttonLevelStart.Text = "START";
                    }
                }
                else
                {
                    MessageBox.Show("Balıkçılık aktifken Level Farm butonuna basamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }else
            {
                MessageBox.Show("Enerji kristali aktifken Level Farm butonuna basamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void buttonEnergyCristalStart_Click(object sender, EventArgs e)
        {
            if (ThreadGlobals.isFishingStopped)
            {
                if(ThreadGlobals.isLevelFarmStopped)
                {
                    if(ThreadGlobals.isEnergyCristalStopped)
                    {
                        ThreadGlobals.isEnergyCristalStopped = false;
                        threadsHandler.Start();
                        buttonEnergyStart.Text = "STOP";
                        threadsHandler.HandleFormElement(labelEnergyCristal, "Energy Bot is started");
                    }
                    else
                    {
                        ThreadGlobals.isEnergyCristalStopped = true;
                        threadsHandler.Stop();
                        buttonEnergyStart.Text = "START";
                        threadsHandler.HandleFormElement(labelEnergyCristal, "Energy Bot is stopped");
                    }
                }
                else
                {
                    MessageBox.Show("Level ve Farm aktifken Enerji kristal Start butonuna basamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show("Balıkçılık aktifken Enerji kristal Start butonuna basamazsın", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonScreenShot_MouseClick(object sender, MouseEventArgs e)
        {
            fullScreen = new FullScreen();
            DebugPfCnsl.println("buttonScreenShot_MouseClick is called");
            fullScreen.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            telegramBot = new TelegramBot(imageObjects);
            DebugPfCnsl.println("Telegram Bot is Starting");
        }
     
   

        private void buttonQuickSS_Click(object sender, EventArgs e)
        {
            //new Thread( () =>
            // {

            if (fileName != null)
            {
                if (arrayTextBoxResult[rectWidth] <= 0)
                {

                    MessageBox.Show("Please enter specified number (Exmp: '10,20,100,50').", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    threadsHandler.HandleFormElement(textBoxRect, "1,1,1,1");
                    return;
                }

                for (int decrease = arrayTextBoxResult[timeSecondIndex]; decrease > 0; decrease--)
                {
                    threadsHandler.HandleFormElement(labelStartStatus,
                        decrease + " second later is taken ScreenShot");
                    Thread.Sleep(1000);
                }
                //Don't forget the uncomment this and up codes
                Rectangle rect = new Rectangle(arrayTextBoxResult[rectX], arrayTextBoxResult[rectY],
                     arrayTextBoxResult[rectWidth], arrayTextBoxResult[rectHeight]);

                if (rect != null && rect != Rectangle.Empty)
                {
                    Bitmap resultBitMap = screenShot.CaptureSpecifiedScreen(rect);
                    pictureBoxMainForm.Image = resultBitMap;

                    FileHandler.SaveImageAsPng(resultBitMap, fileName, pathWay);
                }

            }
            else
            {
                MessageBox.Show("Please enter valid name (Exmp = test)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                threadsHandler.HandleFormElement(textBoxFileName, "test");
            }
               

                
        //    }).Start();
        }


        private void textBoxRect_Leave(object sender, EventArgs e)
        {
          
            // Girilen metni boşluklara göre ayırarak parçalara böl
            string[] values = textBoxRect.Text.Split(new char[] { ' ',',','.' }, StringSplitOptions.RemoveEmptyEntries);

            
            // Eğer 4 adet sayı girişi varsa, dikdörtgeni oluştur ve kaydet
            if (values.Length >= 4 && int.TryParse(values[rectX], out int x) && int.TryParse(values[rectY], out int y) &&
                int.TryParse(values[rectWidth], out int width) && int.TryParse(values[rectHeight], out int height))
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;
                if( x > bounds.Width ||width >  bounds.Width || y > bounds.Height || height > bounds.Height )
                {
                    MessageBox.Show(" Width or Height that your determined is greater than your computer " +
                        "screen resolution. Your resolution values = " + bounds.Width + " " + bounds.Height, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                arrayTextBoxResult[rectX] = x; arrayTextBoxResult[rectY] = y;
                arrayTextBoxResult[rectWidth] = width; arrayTextBoxResult[rectHeight] = height;


                if (values.Length == 5 && int.TryParse(values[timeSecondIndex], out int timeSecond))
                {
                    arrayTextBoxResult[timeSecondIndex] = timeSecond;
                   
                }
                else if(values.Length == 5 && values[timeSecondIndex].Equals("-"))
                {
                   
                    DebugDrawingHandle.SetStaticRectangle(new Rectangle(x, y, width, height));

                }
                else
                {
                    arrayTextBoxResult[timeSecondIndex] = 0;
                }
        
            }
            else
            {
                MessageBox.Show("Please enter specified number (Exmp: '10 20 100 50').", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBoxFileName_Leave(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(textBoxFileName.Text))
            {
                MessageBox.Show("Please enter valid name (Exmp = test)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fileName = null;
                return;
            }
            fileName = textBoxFileName.Text + ".png";
            
        }

        private void LoadComboBox()
        {
            comboBoxPathWays.Items.Add("Desktop");
            comboBoxPathWays.Items.Add("Fishes");
            comboBoxPathWays.Items.Add("Images");
            comboBoxPathWays.Items.Add("Metin2 Alphabets");
            comboBoxPathWays.Items.Add("TestImages");
            comboBoxPathWays.SelectedIndex = 0;
            comboBoxPathWays.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadCheckBoxes()
        {
            checkBoxYabbie.Checked = true;
            checkBoxAltinSudak.Checked = true;
            checkBoxPalamut.Checked = true;
                  
        }

        private void EnableOrDisableTimerCheckBox(bool state)
        {
            if(state)
            {
                textBoxMinWorkTime.Enabled = true;
                textBoxMaxWorkTime.Enabled = true;
                textBoxStopGameTime.Enabled = true;
                textBoxMinMaxBreak.Enabled = true;
            }
            else
            {
                textBoxMinWorkTime.Enabled = false;
                textBoxMaxWorkTime.Enabled = false;
                textBoxStopGameTime.Enabled = false;
                textBoxMinMaxBreak.Enabled = false;
            }
           


        }
        private void comboBoxPathWays_SelectedIndexChanged(object sender, EventArgs e)
        {
            DebugPfCnsl.println("comboBoxPathWays_SelectedIndexChanged çalişti");
            if (comboBoxPathWays.SelectedIndex != -1)
            {
                if(comboBoxPathWays.SelectedItem.ToString().Equals("Desktop"))
                {
                    DebugPfCnsl.println("Desktop");
                    pathWay = PathWayStruct.PATH_DESTKOP;
                }
                else if (comboBoxPathWays.SelectedItem.ToString().Equals("Fishes"))
                {
                    DebugPfCnsl.println("Fishes");
                    pathWay = PathWayStruct.PATH_FISHES;
                }
                else if (comboBoxPathWays.SelectedItem.ToString().Equals("Images"))
                {
                    DebugPfCnsl.println("Images");
                    pathWay = PathWayStruct.PATH_IMAGE;
                }
                else if(comboBoxPathWays.SelectedItem.ToString().Equals("Metin2 Alphabets"))
                {
                    DebugPfCnsl.println("Metin2 Alphabets");
                    pathWay= PathWayStruct.PATH_CHAT_ALPHABETS;
                }
                else if(comboBoxPathWays.SelectedItem.ToString().Equals("TestImages"))
                {
                    DebugPfCnsl.println("TestImages");
                    pathWay= PathWayStruct.PATH_TESTIMAGES;
                }
            }
        }

        private void checkBoxsFishes_Click(object sender, EventArgs e)
        {
            DebugPfCnsl.println("checkBoxsFishes_Click çalişti");
            if (checkBoxYabbie.Checked)
            {
                ThreadGlobals.isYabbieSelected = true;
            }
            else
            {
                ThreadGlobals.isYabbieSelected = false;
            }
            if (checkBoxAltinSudak.Checked)
            {
                ThreadGlobals.isAltinSudakSelected = true;
            }
            else
            {
                ThreadGlobals.isAltinSudakSelected = false;
            }
            if (checkBoxPalamut.Checked)
            {
                ThreadGlobals.isPalamutSelected = true;
            }
            else
            {
                ThreadGlobals.isPalamutSelected = false;
            }
            if (checkBoxKurbaga.Checked)
            {
                ThreadGlobals.isKurbagaSelected = true;
            }
            else
            {
                ThreadGlobals.isKurbagaSelected = false;
            }
            if (checkBoxKadife.Checked)
            {
                ThreadGlobals.isKadifeSelected = true;
            }
            else
            {
                ThreadGlobals.isKadifeSelected = false;
            }
            if (checkBoxDeniz.Checked)
            {
                ThreadGlobals.isDenizkizSelected = true;
            }
            else
            {
                ThreadGlobals.isDenizkizSelected = false;
            }
        }

        private void checkBoxsHepsi_Click(object sender, EventArgs e)
        {
            DebugPfCnsl.println("checkBoxsHepsi_Click çalişti");
            if (checkBoxHepsi.Checked) 
            {
                ThreadGlobals.isHepsiSelected = true;

                checkBoxYabbie.Checked = false;
                checkBoxAltinSudak.Checked = false;
                checkBoxPalamut.Checked = false;
                checkBoxKurbaga.Checked = false;
                checkBoxKadife.Checked = false;
                checkBoxDeniz.Checked = false;

                checkBoxYabbie.Enabled = false;
                checkBoxAltinSudak.Enabled = false;
                checkBoxKurbaga.Enabled = false;
                checkBoxKadife.Enabled = false;
                checkBoxPalamut.Enabled = false;
                checkBoxDeniz.Enabled = false;
            }
            else
            {
                ThreadGlobals.isHepsiSelected= false;

                checkBoxYabbie.Enabled = true;
                checkBoxPalamut.Enabled = true;
                checkBoxKurbaga.Enabled = true;
                checkBoxDeniz.Enabled = true;
                checkBoxKadife.Enabled = true;
                checkBoxAltinSudak.Enabled = true;
                checkBoxDeniz.Enabled = true;

                checkBoxYabbie.Checked = true;
                checkBoxAltinSudak.Checked = true;
                checkBoxPalamut.Checked = true;
                checkBoxKurbaga.Checked = false;
                checkBoxDeniz.Checked = false;
                checkBoxKadife.Checked = false;
                checkBoxDeniz.Checked = false;

                ThreadGlobals.isYabbieSelected = true;
                ThreadGlobals.isAltinSudakSelected = true;
                ThreadGlobals.isPalamutSelected = true;
                ThreadGlobals.isKurbagaSelected = false;
                ThreadGlobals.isDenizkizSelected = false;
                ThreadGlobals.isKadifeSelected = false;

            }
        }

        private void checkBoxEnableTime_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxEnableTime.Checked)
            {
                ThreadGlobals.isTimerBreakEnabled = true;
                EnableOrDisableTimerCheckBox(true);
                if(ThreadGlobals.isFishingStopped)
                {
                    labelStartStatus.Text = "Enter times as minute unit";
                }
                
            }
            else
            {
                ThreadGlobals.isTimerBreakEnabled = false;
                EnableOrDisableTimerCheckBox(false);
                if(ThreadGlobals.isFishingStopped)
                {
                    labelStartStatus.Text = "";
                }
                
            }
            
        }

        private void textBoxMinWorkLeave(object sender, EventArgs e)
        {
            string value = textBoxMinWorkTime.Text;

            if (int.TryParse(value, out int minWork))
            {
                TimerGame.MIN_WORK_TIME = minWork;
            }
            else
            {
                MessageBox.Show("Please enter just a number as 'minute' unit (Exmp 3 or 35).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBoxMaxWorkLeave(object sender, EventArgs e)
        {
            string value = textBoxMaxWorkTime.Text;

            if (int.TryParse(value, out int maxWork))
            {
                TimerGame.MAX_WORK_TIME = maxWork;
            }
            else
            {
                MessageBox.Show("Please enter just a number as 'minute' unit (Exmp 3 or 35).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBoxMinMaxBreakLeave(object sender, EventArgs e)
        {

            string[] values = textBoxMinMaxBreak.Text.Split(new char[] { ' ', ',', '.' }, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length == 2 && int.TryParse(values[0], out int minBreakTime) &&
                int.TryParse(values[1], out int maxBreakTime))
            {
                TimerGame.MAX_BREAK_TIME = maxBreakTime;
                TimerGame.MIN_BREAK_TIME = minBreakTime;
            }
            else
            {
                MessageBox.Show("Please enter two numbers as 'minute' unit (Exmp 2 5 or 5 9).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void textBoxStopTime_Leave(object sender, EventArgs e)
        {
            //DebugPfCnsl.println("textBoxStopTime_Leave çalişti");
            string value = textBoxStopGameTime.Text;

            if (int.TryParse(value, out int stopGameTime))
            {
                TimerGame.GAME_STOP_TIME = stopGameTime;
            }
            else
            {
                MessageBox.Show("Please enter just a number as 'minute' unit (Exmp 3 or 35).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

    

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }


        private void buttonCheckChat_Click(object sender, EventArgs e)
        {
            DebugPfCnsl.println("buttonCheckChat_Click çalişti");
            ChatHandlerForm chatHandlerForm = new ChatHandlerForm();
            chatHandlerForm.Show();
        }

       

        private void textBoxStatus_Leave(object sender, EventArgs e)
        {
            string valueHp = textBoxHp.Text;
            string valueSp = textBoxSp.Text;
            string valueStr = textBoxStr.Text;
            string valueDex = textBoxDex.Text;

            if (int.TryParse(valueHp, out int hpPriority) && int.TryParse(valueSp, out int spPriority)
                && int.TryParse(valueStr, out int strPriority) && int.TryParse(valueDex, out int dexPriority)) 
            {

                ThreadGlobals.STATUS_PRIORITY[0] = hpPriority;
                ThreadGlobals.STATUS_PRIORITY[1] = spPriority;
                ThreadGlobals.STATUS_PRIORITY[2] = strPriority;
                ThreadGlobals.STATUS_PRIORITY[3] = dexPriority;
                DebugPfCnsl.PrintArray(ThreadGlobals.STATUS_PRIORITY);
            }
            else
            {
                MessageBox.Show("Lütfen öncelik sırasını sayılar ile belirleyiniz (örnek hp = 4 sp = 3 dex = 2 str = 1).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void trackBarHp_ValueChanged(object sender, EventArgs e)
        {
            textBoxHpRate.Text = trackBarHp.Value.ToString();
            ThreadGlobals.HP_SP_RATE[0] = trackBarHp.Value;
           // DebugPfCnsl.println("hp value for trackHpVar = " + trackBarHp.Value);
        }

        private void trackBarSp_ValueChanged(object sender, EventArgs e)
        {
            textBoxSpRate.Text = trackBarSp.Value.ToString();
            ThreadGlobals.HP_SP_RATE[1] = trackBarSp.Value;
           // DebugPfCnsl.println("sp value for trackspVar = " + trackBarSp.Value);
        }

        private void textBoxHpRate_Leave(object sender, EventArgs e)
        {
            string valueHpRate = textBoxHpRate.Text;
            string valueSpRate = textBoxSpRate.Text;
            

            if (int.TryParse(valueHpRate, out int hpRate) && int.TryParse(valueSpRate, out int spRate))
            {
                if((hpRate > 0 && hpRate < 101) && (spRate > 0 && spRate < 101))
                {
                    ThreadGlobals.HP_SP_RATE[0] = hpRate;
                    ThreadGlobals.HP_SP_RATE[1] = spRate;

                    trackBarHp.Value = hpRate;
                    trackBarSp.Value = spRate;

                    DebugPfCnsl.PrintArray(ThreadGlobals.HP_SP_RATE);
                }
              
             
                
            }
            else
            {
                MessageBox.Show("Lütfen yüzdelik belirlemesini rakam ile giriniz 1 ile 100 arası.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBoxHpAndSPRate_Leave(object sender, EventArgs e)
        {
            string valueHpRate = textBoxHpRate.Text;
            string valueSpRate = textBoxSpRate.Text;


            if (int.TryParse(valueHpRate, out int hpRate) && int.TryParse(valueSpRate, out int spRate))
            {
                if ((hpRate > 0 && hpRate < 101) && (spRate > 0 && spRate < 101))
                {
                    ThreadGlobals.HP_SP_RATE[0] = hpRate;
                    ThreadGlobals.HP_SP_RATE[1] = spRate;

                    trackBarHp.Value = hpRate;
                    trackBarSp.Value = spRate;

                    DebugPfCnsl.PrintArray(ThreadGlobals.HP_SP_RATE);
                }



            }
            else
            {
                MessageBox.Show("Lütfen yüzdelik belirlemesini rakam ile giriniz 1 ile 100 arası.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void checkBoxTelegram_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxTelegram.Checked)
            {
                ThreadGlobals.isTelegramBotActive = true;
            }
            else
            {
                ThreadGlobals.isTelegramBotActive=false;
            }
        }

        private void buttonTelegramTest_Click(object sender, EventArgs e)
        {
            if (ThreadGlobals.isTelegramBotActive)
            {
                if(!TelegramBot.TELEGRAM_BOT_IS_READY)
                {
                    if (TelegramBot.TELEGRAM_SEND_MESSAGE != null)
                    {
                        TelegramBot.TELEGRAM_DIALOG_PANEL_ACTIVE = true;

                        DialogResult result = MessageBox.Show(
                            TelegramBot.TELEGRAM_SEND_MESSAGE.Chat.FirstName + " adlı kullanıcı size mi ait?",
                            "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            TelegramBot.TELEGRAM_DIALOG_PANEL_ACTIVE = false;
                            labelTelegramStatus.ForeColor = Color.Green;
                            labelTelegramStatus.Text = "Bağlantı kuruldu. Kullanıma hazır";
                            TelegramBot.SendMessageTelegram("Bağlantı hazır");
                            TelegramBot.TELEGRAM_BOT_IS_READY = true;
                            buttonTelegramTest.Text = "Sıfırla";

                        }
                        else
                        {
                            TelegramBot.TELEGRAM_DIALOG_PANEL_ACTIVE = false;
                            labelTelegramStatus.ForeColor = Color.Red;
                            labelTelegramStatus.Text = "Bağlantı kurmak için tekrar mesaj gönderin";                          
                        }
                    }
                    else
                    {
                        MessageBox.Show("Telegram kanalına mesaj gönderip veya 'Start'" +
                            "butonuna bastıktan sonra tekrar 'Test' butonuna basınız.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show(
                            "Zaten " + TelegramBot.TELEGRAM_SEND_MESSAGE.Chat.FirstName + " ile bağlantı kuruldu" +
                            " Yeni bir kişi ile mi bağlantı kurmak istiyorsun?",
                            "Onay", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if(result == DialogResult.Yes)
                    {
                        TelegramBot.SendMessageTelegram("Bağlantınız sıfırlandı.Aktif etmek için yeniden " +
                            "mesaj gönderip 'Test' butonuna basınız ");

                        TelegramBot.TELEGRAM_BOT_IS_READY = false;
                        TelegramBot.TELEGRAM_SEND_MESSAGE = null;

                        labelTelegramStatus.ForeColor = Color.Magenta;
                        labelTelegramStatus.Text = "Bağlantınız sıfırlandı";
                        buttonTelegramTest.Text = "Test";

                    }
                   
                }
               
            }
            else
            {
                MessageBox.Show("Telegram Aktif Et Seçeneğini İşaretlemelisin.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void TextBoxes_Skills_Leave(object sender, EventArgs e)
        {
            HandleSkillTimeForTextBox(textBoxSkillOne, 0);
            HandleSkillTimeForTextBox(textBoxSkillTwo, 1);
            HandleSkillTimeForTextBox(textBoxSkillThree, 2);
            HandleSkillTimeForTextBox(textBoxSkillFour, 3);
            HandleSkillTimeForTextBox(textBoxSkillF1, 4);
            HandleSkillTimeForTextBox(textBoxSkillF2, 5);
            HandleSkillTimeForTextBox(textBoxSkillF3, 6);
            HandleSkillTimeForTextBox(textBoxSkillF4, 7);
        }

        private void HandleSkillTimeForTextBox(TextBox textBox,int indexForSkillTime)
        {
            string valueSkillTime = textBox.Text;



            if (int.TryParse(valueSkillTime, out int skillTime))
            {
                if (skillTime >= 0)
                {
                    ThreadGlobals.SKILL_TIME_FOR_KEYS[indexForSkillTime] = skillTime;
                }
                else
                {
                    MessageBox.Show("Lütfen " + textBox.Name + " zamanı 0 dan büyük bir değer giriniz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                

            }
            else
            {
                MessageBox.Show("Lütfen " + textBox.Name + " kısmındaki kutucuğa süre olarak sadece sayı giriniz.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxFishingMiniBreak_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxFishingMiniBreak.Checked)
            {
                ThreadGlobals.isFishingMiniBreakActive = true;
            }
            else
            {
                ThreadGlobals.isFishingMiniBreakActive = false;
            }
        }

        private void checkBoxChatActive_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxChatActive.Checked)
            {
                ThreadGlobals.isChattingAnswerActive = true;
            }
            else
            {
                ThreadGlobals.isChattingAnswerActive = false;
            }
        }

        private void checkBoxWhisperActive_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxWhisperActive.Checked)
            {
                ThreadGlobals.isWhisperAnswerActive = true;
            }
            else
            {
                ThreadGlobals.isWhisperAnswerActive = false;
            }
        }

        private void checkBoxETPPickUp_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxETPPickUp.Checked)
            {
                
                ThreadGlobals.isETPPickUpActive = true;
            }
            else
            {
                ThreadGlobals.isETPPickUpActive = false;
            }
        }

        private void InitializeToolTip()
        {
            toolTip.SetToolTip(checkBoxAdaptableFish, "Eğer haritada veya yakınınızda oyuncu var ise yavaş balık tutar");
            toolTip.SetToolTip(checkBoxPCSlow, "Eğer Bilgisayarın çok yavaş ise balık tutmak yada enerji parçası için bu seçeneği tıkla");
        }

        private void checkBoxEnableTime_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBoxAdaptable_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxAdaptableFish.Checked)
            {
                ThreadGlobals.isAdaptableFishing = true;
            }
            else
            {
                ThreadGlobals.isAdaptableFishing = false;
            }
        }

        private void checkBoxPCSlow_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxPCSlow.Checked)
            {
                TimerGame.IS_PC_SLOW = true;
            }
            else
            {
                TimerGame.IS_PC_SLOW = false;
            }
        }
    }
}
