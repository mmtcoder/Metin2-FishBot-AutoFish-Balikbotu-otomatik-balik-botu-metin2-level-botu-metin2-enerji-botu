using Metin2AutoFishCSharp.Sources.ChatHandler;
using Metin2AutoFishCSharp.Sources.GameHandler;
using Microsoft.SqlServer.Server;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.LevelAndFarms
{
    internal class StatusHandler
    {
        private ImageObjects imagesObject;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;
        private TimerGame timerStatusDetection;
        private GameInputHandler input;
        private CharSpecialThings charSpecials;
        private GameAlphabetDetecter alphabetDetecter;
        private LevelHandle levelGet;

        private bool isStatusFirstDetected = false;

        //Rectangle improve status plus icon detection area = {X=121,Y=282,Width=21,Height=112}
        private Rectangle rectPlusIconDetectArea = new Rectangle(121, 282,21,112);
       
        public StatusHandler(ImageObjects imagesObject,GameAlphabetDetecter alphabetDetecter,LevelHandle levelGet)
        {
            this.imagesObject = imagesObject;
            this.alphabetDetecter = alphabetDetecter;
            timerStatusDetection = new TimerGame();
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);
            input = new GameInputHandler();
            charSpecials = new CharSpecialThings(imagesObject);
            this.levelGet = levelGet;
        }

        public void StartStatusHandle()
        {
            levelGet.CheckCharacterLevel();
            OpenStatusPage();
            IncreaseStatus();
            CloseStatusPage();
        }
        public bool CheckStatusImproveTitle()
        {
            if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("CheckStatusImproveTitle function is returned false");               
                return false;
            }
            int[] targetWhiteStatus = imagesObject.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                screenShot.ImageArraySpecifiedArea(coor.RectStatusImproveTitle()));

            if (!isStatusFirstDetected)
            {
                if (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayStatusImproveWhiteTitle,
               targetWhiteStatus,ImageSensibilityLevel.SENSIBILTY_HIGH))
                {
                 
                    isStatusFirstDetected = true;
                    timerStatusDetection.SetStartedMilliSecTime();
                    return true;
                }
            }
            else
            {
                if (timerStatusDetection.CheckDelayTimeInMilliSec(2000))
                {
                    //DebugPfCnsl.println("2 saniyenin içinde ");
                    return true;
                }
                else
                {
                    // DebugPfCnsl.println("2 saniye geçti ");
                    isStatusFirstDetected = false;
                    timerStatusDetection.SetStartedMilliSecTime();
                   
                }
            }
          
           

            return false;

     
        }

        public bool CheckStatusPage()
        {
            int[] targetIcon = screenShot.ImageArraySpecifiedArea(coor.RectStatusTitle());

            if (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayStatusTitleIcon,
                targetIcon,ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                return true;
            }

            return false;
        }

        public bool OpenStatusPage()
        {
            TimerGame timerOpenStatus = new TimerGame();

            while (!CheckStatusPage())
            {
                if (timerOpenStatus.CheckDelayTimeInSecond(10))
                {
                    input.MouseMoveAndPressLeft(coor.PointStatusImproveClickImage().X,
                   coor.PointStatusImproveClickImage().Y);
                    TimerGame.SleepRandom(500, 650);
                }else
                {
                    DebugPfCnsl.println("OpenStatusPage time elapsed returned");
                    charSpecials.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                    TimerGame.SleepRandom(1500, 2500);
                    while(!ThreadGlobals.CanLevelAndFarmRightNow())
                    {
                        if(ThreadGlobals.isLevelFarmStopped)return false;

                    }
                    TimerGame.SleepRandom(3500, 4500);

                    return OpenStatusPage();
                }
               
            }
            return true;
        }

        public int CheckIncreasingStatuValue()
        {
            string result = alphabetDetecter.DetectGameText(ColorGame.CHAT_WHITE_COLOR,
                coor.RectStatusImproveCountArea());

            if (result != null && result != string.Empty)
            {
                if(int.TryParse(result, out int value))
                {
                   // DebugPfCnsl.println("tespit edilen statü arttırma değeri = " + result);
                    return value;
                }
                else
                {
                    DebugPfCnsl.println("değer  tespit etmede hata var");
                    return 1;
                }
            }

            return 1;
        }
        public void MakeStatusIncreasing(Rectangle rectIncreasePlusArea,int pressTime)
        {
            for (int i = 0; i < pressTime; i++)
            {
                input.MouseMoveAndPressLeft(rectIncreasePlusArea.X, rectIncreasePlusArea.Y);
                TimerGame.SleepRandom(700, 900);
            }
            
        }

        public void IncreaseStatus()
        {
            int[] statusPriorities = ThreadGlobals.STATUS_PRIORITY;
            int increaseCounting = CheckIncreasingStatuValue();

            int hp = statusPriorities[0];
            int sp = statusPriorities[1];
            int str = statusPriorities[2];
            int dex = statusPriorities[3];

            Rectangle[] rectStatusPlus = imagesObject.FindAllImagesOnScreen(imagesObject.arrayStatusPlusIcon,
               coor.RectStatusPlusImageSample(), rectPlusIconDetectArea);

            if(rectStatusPlus != null && rectStatusPlus.Length > 0)
            {
               // if(rectStatusPlus.Length == 4)
               if(CharInfo.CharLevel <= 29)
                {
                   // DebugPfCnsl.println("status plus improve rect values = ");
                   // DebugPfCnsl.PrintArray(rectStatusPlus);
                   //hp plus coor { X = 128,Y = 288,Width = 9,Height = 9}
                   //sp plus coor { X = 128,Y = 319,Width = 9,Height = 9}
                   // str plus coor { X = 128,Y = 350,Width = 9,Height = 9}
                   //dex plus coor { X = 128,Y = 381,Width = 9,Height = 9}

                    if (hp > sp && hp > str && hp > dex)
                    {
                        //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                        MakeStatusIncreasing(rectStatusPlus[0], increaseCounting);
                        TimerGame.SleepRandom(1000, 1500);
                    }
                    else if(sp > hp && sp > str && sp > dex)
                    {
                        //input.MouseMoveAndPressLeft(rectStatusPlus[1].X, rectStatusPlus[1].Y);
                        MakeStatusIncreasing(rectStatusPlus[1], increaseCounting);
                        TimerGame.SleepRandom(1000, 1500);
                    }
                    else if(str > hp && str > sp && str > dex)
                    {
                        // input.MouseMoveAndPressLeft(rectStatusPlus[2].X, rectStatusPlus[2].Y);
                        MakeStatusIncreasing(rectStatusPlus[2], increaseCounting);
                        TimerGame.SleepRandom(1000, 1500);
                    }
                    else if(dex > hp && dex > sp  && dex > str)
                    {
                        //input.MouseMoveAndPressLeft(rectStatusPlus[3].X, rectStatusPlus[3].Y);
                        MakeStatusIncreasing(rectStatusPlus[3], increaseCounting);
                        TimerGame.SleepRandom(1000, 1500);
                    }

                }
                //else if(rectStatusPlus.Length == 3)
                else if (CharInfo.CharLevel >= 30 && CharInfo.CharLevel <= 59)
                {
                     if (hp > sp && hp > str && hp > dex)
                    {
                        if(sp > str && sp > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[1], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if(str > sp && str > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[2], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if(dex > sp && dex > str)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[3], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                       
                    }
                    else if (sp > hp && sp > str && sp > dex)
                    {
                        if (hp > str && hp > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[0], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (str > hp && str > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[2], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (dex > hp && dex > str)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[3], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        
                    }
                    else if (str > hp && str > sp && str > dex)
                    {
                        if (sp > hp && sp > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[1], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (hp > sp && hp > dex)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[0], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (dex > hp && dex > sp)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[3], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                       
                    }
                    else if (dex > hp && dex > sp && dex > str)
                    {
                        if (sp > str && sp > hp)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[1], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (str > sp && str > hp)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[2], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                        else if (hp > sp && hp > str)
                        {
                            //input.MouseMoveAndPressLeft(rectStatusPlus[0].X, rectStatusPlus[0].Y);
                            MakeStatusIncreasing(rectStatusPlus[0], increaseCounting);
                            TimerGame.SleepRandom(1000, 1500);
                        }
                     
                    }

                }
            }

           
        }

        public bool CloseStatusPage()
        {
            TimerGame timerCloseStatus = new TimerGame();

            while (CheckStatusPage())
            {
                if (timerCloseStatus.CheckDelayTimeInSecond(10))
                {
                    input.KeyPress(KeyboardInput.ScanCodeShort.KEY_C);
                    TimerGame.SleepRandom(300, 450);
                }
                else
                {
                    DebugPfCnsl.println("OpenStatusPage time elapsed returned");
                    return false;
                }

            }
            return true;
        }
    }

}
