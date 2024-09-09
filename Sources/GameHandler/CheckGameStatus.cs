using Metin2AutoFishCSharp.Sources;
using Metin2AutoFishCSharp.Sources.GameHandler;
using Metin2AutoFishCSharp.Sources.LevelAndFarms;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.CoordinatesHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources.GameHandler
{
    internal class CheckGameStatus
    {
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        public static readonly string gameForgeProcessName = "gfservice";
        public static readonly string metin2ProcessNAme = "metin2client";

        private int[] imageEntryScrn;
        private int[] imageKillScrn;
        private int[] imageCharScreen;
        private int[] imageSaleTitle;
        private int[] imageIsCharOnline;
        private int[] imageIsAnotPlayer;
        private int[] imageIsMetin2IconSeemed;

        GameObjectCoordinates coordinates;
        ScreenShotWinAPI screenShot;
        ImageObjects imageObjects;
        GameInputHandler inputGame;
        CharSpecialThings charThigs;
        TimerGame timerSettingButton;
        TimerGame timerCheckOnline;
        LevelHandle levelHandle;
       
        private DebugPfCnsl debugConsole;

        private static int ChannelValue = 0;

        public CheckGameStatus(ImageObjects imageObjects)
        {
            this.imageObjects = imageObjects;
            coordinates = new GameObjectCoordinates(imageObjects);
            screenShot = new ScreenShotWinAPI();       
            inputGame = new GameInputHandler();
            charThigs = new CharSpecialThings(imageObjects);
            timerSettingButton = new TimerGame();
            debugConsole = new DebugPfCnsl();
            timerCheckOnline = new TimerGame();
            levelHandle = new LevelHandle(imageObjects);
        }
        public void StartCheckingGame()
        {
            if (ThreadGlobals.isPausedTheGame)
            {
                
                timerSettingButton.SetStartedSecondTime();
                timerCheckOnline.SetStartedSecondTime();
                return;
            }

            if(ThreadGlobals.CheckGameIsStopped())
            {
                DebugPfCnsl.println("StartCheckingGame is returned");
                return;
            }

            imageEntryScrn = screenShot.ConvertBitmapToArray(screenShot.CaptureSpecifiedScreen
                (coordinates.RectEntryScreen()));
            imageKillScrn = screenShot.ConvertBitmapToArray(screenShot.CaptureSpecifiedScreen
                (coordinates.RectDieScreen()));
            imageCharScreen = screenShot.ConvertBitmapToArray(screenShot.CaptureSpecifiedScreen
                (coordinates.RectCharScreen()));
            imageSaleTitle = screenShot.ConvertBitmapToArray(screenShot.CaptureSpecifiedScreen
                (coordinates.RectSaleCross()));
            imageIsCharOnline = screenShot.ConvertBitmapToArray(screenShot.CaptureSpecifiedScreen
                (coordinates.RectSettingButton()));

           // DebugPfCnsl.printlnTime("StartChecking taken images", 2);

            if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayEntryScreen,
                imageEntryScrn, ImageSensibilityLevel.SENSIBILTY_MED))
            {
                ThreadGlobals.isSettingButtonSeemed = false;
                ThreadGlobals.isEntryScreenActive = true;
                EntryScreenHandle();
            }
            else if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayCharScreen,
                imageCharScreen, ImageSensibilityLevel.SENSIBILTY_MED))
            {
                ThreadGlobals.isSettingButtonSeemed = false;
                ThreadGlobals.isCharScreenActive = true;
                CharacterScreenHandle();
            }
            else if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayKilledScreen,
                imageKillScrn, ImageSensibilityLevel.SENSIBILTY_MED))
            {
               
                ThreadGlobals.isCharKilled = true;
                TimerGame.SleepRandom(3000, 4599);
                charThigs.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);
                ThreadGlobals.isCharKilled = false;
            }
            else if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySaleTitle,
                imageSaleTitle, ImageSensibilityLevel.SENSIBILTY_MED))
            {
                ThreadGlobals.isSaleTitleActive = true;
                CloseSaleTitle();
            }
            else if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySettingButton,
                imageIsCharOnline, ImageSensibilityLevel.SENSIBILTY_MED))
            {
                ThreadGlobals.isSettingButtonSeemed = true;
                ThreadGlobals.isEntryScreenActive = false;
                ThreadGlobals.isCharScreenActive = false;

                timerSettingButton.SetStartedSecondTime();
                timerCheckOnline.SetStartedSecondTime();

                if (!ThreadGlobals.isFishingStopped)
                {
                    //debugConsole.printlnTime("detected settingButton ", 2);

                    int[] imageIsActiveFishBoard = screenShot.ConvertBitmapToArray(screenShot
                        .CaptureSpecifiedScreen(coordinates.RectFishTitle()));

                    ThreadGlobals.isActiveFishBoard = imageObjects.CompareTwoArrayAdvanced(
                        imageObjects.arrayFishTitle, imageIsActiveFishBoard, ImageSensibilityLevel.SENSIBILTY_MED);
                    //DebugPfCnsl.println("isActiveFishboard "+ ThreadGlobals.isActiveFishBoard);
                   
                }
                else if (!ThreadGlobals.isLevelFarmStopped)
                {

                }
                
            }
            else
            {
                ThreadGlobals.isSettingButtonSeemed = false;
               if(!timerCheckOnline.CheckDelayTimeInSecond(30))
                {
                    DebugPfCnsl.println("Setting button tespit edilemediği için esc" +
                        "tuşuna basılıyor");
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    TimerGame.SleepRandom(400, 600);
                   // timerCheckOnline.SetStartedSecondTime();
                }
                  if(!timerSettingButton.CheckDelayTimeInSecond((TimerGame.MAX_BREAK_TIME +2) * 60) || 
                    !CheckGameCoordinate.IS_METIN2_ICON_DETECTED)                  
                    {
                    TelegramBot.SendMessageTelegram("Metin2 sembolü tespit edilemiyor.Tekrar oyuna girilmesi sağlanıyor. " +
                        "Bilgisayarı kontrol etmekte fayda var");

                    DebugPfCnsl.println("Program couldn't detect setting button  " +
                         "\n so ProcessCreateNewMetin2 function called");
                    if(ProcessCreateNewMetin2())
                    {
                        timerSettingButton.SetStartedSecondTime();
                        timerCheckOnline.SetStartedSecondTime();
                        DebugPfCnsl.println("başarı ile metin2 oluşturuldu");
                    }
                }
                
            }
        }

        private void CharacterScreenHandle()
        {
            TimerGame timerGame = new TimerGame();
            timerGame.SetStartedSecondTime();
            int[] targetCharScreen = screenShot.ImageArraySpecifiedArea(coordinates.RectCharScreen());

            while (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayCharScreen, targetCharScreen,
                ImageSensibilityLevel.SENSIBILTY_HIGH) && timerGame.CheckDelayTimeInSecond(15))
            {
                if(ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isPausedTheGame) return;
                DebugPfCnsl.println("In the Char Screen");
                inputGame.MouseMoveAndPressLeft(coordinates.PointCharEnterButton().X,
                    coordinates.PointCharEnterButton().Y);
                TimerGame.SleepRandom(1500, 2599);
                targetCharScreen = screenShot.ImageArraySpecifiedArea(coordinates.RectCharScreen());

            }

            if(!timerGame.CheckDelayTimeInSecond(15))
            {
                DebugPfCnsl.println("Game is bug at the Char Screen more than 15 second...");
                //TODO: Burada mutlaka GameForge dan yeni metin2 yi baslat.
                ProcessCreateNewMetin2();
                
            }
            else
            {
                TimerGame timerSettingButtonCanSee = new TimerGame();
                int[] targetSettingButton = screenShot.ImageArraySpecifiedArea(coordinates.RectSettingButton());
                while(!imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySettingButton, targetSettingButton,
                    ImageSensibilityLevel.SENSIBILTY_HIGH))
                {
                    // DebugPfCnsl.println("setting button görülmüyor");
                    if (timerSettingButtonCanSee.CheckDelayTimeInSecond(15))
                    {
                        if (ThreadGlobals.CheckGameIsStopped()) return;
                        targetSettingButton = screenShot.ImageArraySpecifiedArea(coordinates.RectSettingButton());
                    }
                    else
                    {
                        DebugPfCnsl.println("uzun süredir CharacterScreenHandle setting button tespit edilemedi return edildi");
                        return;
                    }
                    
                   
                }
                TimerGame.SleepRandom(2000, 4000);

               
                if (!ThreadGlobals.isFishingStopped)
                {
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    TimerGame.SleepRandom(90, 120);
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    TimerGame.SleepRandom(90, 120);
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    TimerGame.SleepRandom(90, 120);

                    inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_W);
                    TimerGame.SleepRandom(90, 120);
                    inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_W);
                }
                else if (!ThreadGlobals.isLevelFarmStopped)
                {
                    AutoHunter.IS_AUTO_HUNTER_STARTED = false;
                }

                ThreadGlobals.isCheckedShiftPage = false;
                ThreadGlobals.isSettingButtonSeemed = true;
                ThreadGlobals.isCharScreenActive = false;

                charThigs.ProvideCharNameCanSee();
                charThigs.CheckShiftPageNumber();

                

                timerSettingButtonCanSee.SetStartedSecondTime();
                timerCheckOnline.SetStartedSecondTime();
                //  DebugPfCnsl.println("CheckGameStatus DebugThreadGlobalPref = " +
                //    ThreadGlobals.GetGameStatusOrUserPrefer());
            }

            
        }

        private void EntryScreenHandle()
        {
            if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isPausedTheGame) return;

            TimerGame timeGame = new TimerGame();

            int randomChannelValue = TimerGame.MakeRandomValue(0, 4);

            inputGame.MouseMoveAndPressLeft(coordinates.PointChSix().X, 
                coordinates.PointChSix().Y - ( randomChannelValue* 17));
            inputGame.MouseMoveAndPressLeft(coordinates.PointOkButton().X,
                coordinates.PointOkButton().Y );

            timeGame.SetStartedSecondTime();

            while (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayEntryScreen,
                screenShot.ImageArraySpecifiedArea(coordinates.RectEntryScreen()), 
                ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                if(ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isPausedTheGame) return;

                if(!timeGame.CheckDelayTimeInSecond(9))
                {
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    randomChannelValue = TimerGame.MakeRandomValue(0, 4);
                    inputGame.MouseMoveAndPressLeft(coordinates.PointChSix().X,
                coordinates.PointChSix().Y - (randomChannelValue * 17));
                    inputGame.MouseMoveAndPressLeft(coordinates.PointOkButton().X,
                        coordinates.PointOkButton().Y);
                    timeGame.SetStartedSecondTime();

                }
               
            }

            ThreadGlobals.isEntryScreenActive = false;
           

        }
       private void CloseSaleTitle()
       {
            int[] targetSaleTitle = screenShot.ImageArraySpecifiedArea(coordinates.RectSaleCross());

            while(imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySaleTitle,
                targetSaleTitle,ImageSensibilityLevel.SENSIBILTY_MED)) 
            {
                if(ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isPausedTheGame) return;
                inputGame.MouseMoveAndPressLeft(coordinates.RectSaleCross().X,
                    coordinates.RectSaleCross().Y);
                targetSaleTitle = screenShot.ImageArraySpecifiedArea(coordinates.RectSaleCross());
            }
            ThreadGlobals.isSaleTitleActive = false;
       }

        private bool CheckProgramIsActive(string programName)
        {
            Process[] processes = Process.GetProcessesByName(programName);
           
            if (processes.Length > 0)
            {
                // Program çalışıyorsa odaklan
                DebugPfCnsl.println($"'{programName}' programı zaten çalışıyor. Odaklanılıyor...");
               
                return true;
            }
            else
            {
                // Program çalışmıyorsa mesaj göster
                DebugPfCnsl.println($"'{programName}' programı çalışmıyor.");
                return false;
            }
            
        }
        
        public bool ProcessCreateNewMetin2()
        {
            if (ThreadGlobals.CheckGameIsStopped())
            {
                DebugPfCnsl.println("ProcessCreateNewMetin2 is returned false");
                return false; 
            } 

          

           /* if (hwdnMetin2Exe != IntPtr.Zero)
            {
                DebugPfCnsl.println("zaten metin2 açık odaklanılıyor");
                SetForegroundWindow(hwdnMetin2Exe);
                TimerGame.SleepRandom(1500, 2500);
                return true;
            }*/
             if (CheckProgramIsActive(gameForgeProcessName))
            {
                DebugPfCnsl.println("Gforge arka planda çalışıyor");
                Rectangle[] rectTargetsGForgeApp = imageObjects.FindAllImagesOnScreen(
                    imageObjects.arrayGameForgeAppIcon, coordinates.RectGForgeAppIconSample(),
                    Rectangle.Empty);

                if(rectTargetsGForgeApp.Length > 0)
                {
                    DebugPfCnsl.println("Gforge sembolü bulundu");
                    for (int program =0;  program<rectTargetsGForgeApp.Length; program++) 
                    {
                        inputGame.MouseMoveAndPressLeft(rectTargetsGForgeApp[program].X,
                            rectTargetsGForgeApp[program].Y);
                        TimerGame.SleepRandom(2000, 3000);

                        //Press game Forge Maximize Icon 

                        Rectangle rectGFMaximize = imageObjects.FindImageOnScreen(imageObjects.arrayGameForgeMaximizeBut,
                            coordinates.RectGForgeMaximizeButtonSample());

                        if (rectGFMaximize != Rectangle.Empty)
                        {
                            DebugPfCnsl.println("Gforge ekranı büyütülüyor");

                            inputGame.MouseMoveAndPressLeft(rectGFMaximize.X + rectGFMaximize.Width / 2,
                            rectGFMaximize.Y + rectGFMaximize.Height / 2);
                            TimerGame.SleepRandom(2000, 3000);

                        }
                            //Check GameForge Page is open and button text is "Oyna "
                            Rectangle rectGForgeOynaButton = imageObjects.FindImageOnScreen(
                                imageObjects.arrayGameForgeOyna, coordinates.RectGForgeOynaButton());

                            if (rectGForgeOynaButton != Rectangle.Empty)
                            {
                                inputGame.MouseMoveAndPressLeft(rectGForgeOynaButton.X,
                                    rectGForgeOynaButton.Y);

                                TimerGame timerMetin2Icon = new TimerGame();


                                while (!imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayEntryScreen,
                                    screenShot.ImageArraySpecifiedArea(coordinates.RectEntryScreen()),
                                    ImageSensibilityLevel.SENSIBILTY_HIGH))
                                {
                                    if (timerMetin2Icon.CheckDelayTimeInSecond(60))
                                    {
                                        if (ThreadGlobals.CheckGameIsStopped()) return false;
                                        inputGame.MouseMoveAndPressLeft(rectTargetsGForgeApp[program].X,
                                        rectTargetsGForgeApp[program].Y);
                                        TimerGame.SleepRandom(1000, 2000);
                                    }

                                    else
                                    {
                                        DebugPfCnsl.println("We couldn't get Metin2 Application");
                                        return false;
                                    }
                                }

                                //Focus metin2 application
                                inputGame.MouseMoveAndPressLeft(coordinates.RectEntryScreen().X,
                                    coordinates.RectEntryScreen().Y);

                                DebugPfCnsl.println("Metin2 Application is opened and entry screen detected");
                                return true;

                            }
                          
                            //If button text is not equal to "Oyna"
                            else
                            {
                                Rectangle rectGForgeOynaDownBut = imageObjects.FindImageOnScreen(
                                    imageObjects.arrayGameForgeOynaDownBut, coordinates.RectGForgeOynaDownButtton());
                                if (rectGForgeOynaDownBut != Rectangle.Empty)
                                {
                                    inputGame.MouseMoveAndPressLeft(rectGForgeOynaDownBut.X,
                                        rectGForgeOynaDownBut.Y);

                                    //For Blocking Button Highlight Effect
                                    inputGame.MouseMove(rectGForgeOynaDownBut.X + 100,
                                        rectGForgeOynaDownBut.Y);
                                    TimerGame.SleepRandom(2000, 4000);

                                    Rectangle rectGForgeOynaUpDown = imageObjects.FindImageOnScreen(
                                        imageObjects.arrayGameForgeOynaUpBut, coordinates.RectGForgeOynaUpButton());

                                    if (rectGForgeOynaUpDown != Rectangle.Empty)
                                    {
                                        inputGame.MouseMoveAndPressLeft(rectGForgeOynaUpDown.X,
                                            rectGForgeOynaUpDown.Y + 100);

                                        TimerGame timerMetin2Icon = new TimerGame();

                                        while (!imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayEntryScreen,
                                     screenShot.ImageArraySpecifiedArea(coordinates.RectEntryScreen()),
                                     ImageSensibilityLevel.SENSIBILTY_HIGH))
                                        {
                                            if (timerMetin2Icon.CheckDelayTimeInSecond(60))
                                            {
                                                if (ThreadGlobals.CheckGameIsStopped()) return false;
                                                inputGame.MouseMoveAndPressLeft(rectTargetsGForgeApp[program].X,
                                              rectTargetsGForgeApp[program].Y);
                                                TimerGame.SleepRandom(1000, 2000);
                                            }

                                            else
                                            {
                                                DebugPfCnsl.println("We couldn't get Metin2 Application");
                                                return false;
                                            }
                                        }


                                        //Focus metin2 application
                                        inputGame.MouseMoveAndPressLeft(coordinates.RectEntryScreen().X,
                                            coordinates.RectEntryScreen().Y);

                                        DebugPfCnsl.println("Metin2 Application is opened and entry screen detected");
                                        return true;



                                    }
                                    else
                                    {
                                        DebugPfCnsl.println("Game Forge Down Button not Detected");
                                    }
                                }
                                else
                                {
                                    DebugPfCnsl.println("rectGForgeOynaDownBut butonu tespit edilemedi");
                                }
                            }
                        
                       
                    }
                }
                else
                {
                    DebugPfCnsl.println("Game Forge Icon is Not Detected");
                }
            }
            return false;
        }
    }
}
