using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPlayerApp.Debugs;
using System.Windows.Forms;
using System.Threading;
using MusicPlayerApp.Sources.ImageHandle;
using MusicPlayerApp.Sources.CharacterHandle;
using static MusicPlayerApp.Sources.KeyboardInput;

namespace MusicPlayerApp.Sources.GameHandler
{
    internal class FishingHandle
    {
        /* private int fishPixelOneValue = -12886405;
         private int fishPixelTwoValue = -12952454;
         private int fishPixelThreeValue = -13084037; 
         private int fishPixelFourValue = -13149830;*/

        private int fishPixelOneValue = -13412486;
        private int fishPixelTwoValue = -12885891;
        private int fishPixelThreeValue = -13149573;
        private int fishPixelFourValue = -12886405;
        private int fishPixelFifthValue = -13017734;
        private int fishPixelSixthValue = -13017989;
        private int fishPixelSeventhValue = -12886919;
        private int fishPixelEightValue = -13018247;
        private int fishPixelNinethValue = -13017991;
        private int fishPixelTenthValue = -13016706;
        private int fishPixelElevenValue = -13478021;
        private int fishPixelTwelveValue = -13018248;
        private int fishPixelThirteenthValue = -10329738;
        private int fishPixelFourteenthValue = -13084038;
        private int fishPixelFiveteenthValue = -13084039;
        private int fishPixelSixteenthValue = -12952455;
        private int fishPixelSeventeenthValue = -12952712;

        static int storeAttempt = 0;
        private int emptyCounter = 0;
        private int oldEmptyCounter = 0;
        private int slotCounter = 0;




        private bool isAltinTonDetected = false;
        private bool isHandlePinkFuncWorked = false;

        static int[] arrayStoredFishValue = new int[6];
        int[] arrayReferenceWormVal;

        private GameObjectCoordinates coordinates;
        private GameInputHandler inputs;
        private ScreenShotWinAPI screenshot;
        private CharSpecialThings charThings;
        private PrepareFishing prepareFish;
        private CharInfo charInfo;
        //private DebugPfCnsl debugConsole;

        private ImageObjects gameImages;
        private TimerGame timeGame ;
        private TimerGame timeGameRest;
        private TimerGame timerlittleBreak;
        private TimerGame timerWormEmptyCounter;
        private TimerGame timeWormController;

        private Rectangle ScreenBounds = Screen.PrimaryScreen.Bounds;
        Rectangle rectFish;
        public FishingHandle(ImageObjects imageObjects)
        { 
            gameImages = imageObjects;
            coordinates = new GameObjectCoordinates(imageObjects);
            
            inputs = new GameInputHandler();
            screenshot = new ScreenShotWinAPI();
            arrayReferenceWormVal = gameImages.DetectWorms(gameImages.arrayWorm200);
            charThings = new CharSpecialThings(imageObjects);
            prepareFish = new PrepareFishing(imageObjects);
            timeGame = new TimerGame();
            timeGameRest = new TimerGame();
            timerlittleBreak = new TimerGame();
            timerWormEmptyCounter = new TimerGame();
            timeWormController = new TimerGame();
            charInfo = new CharInfo(imageObjects);
           // debugConsole = new DebugPfCnsl();
            
        }

       public void StartFishing()
        {

           
            while (ThreadGlobals.CanFishingRightNow())
            {
               
                if (ThreadGlobals.isTimerBreakEnabled)
                {
                   // DebugPfCnsl.println("min Work time = " + TimerGame.MIN_WORK_TIME
                  //     + " max work time = " + TimerGame.MAX_WORK_TIME);
                    
                    //Break the Game
                    //uncomment
                    if(timeGameRest.CheckCountDownMinute(TimerGame.MIN_WORK_TIME,
                        TimerGame.MAX_WORK_TIME))
                    //comment
                    //if(!timeGameRest.CheckDelayTimeInSecond(15))
                    {
                        ThreadGlobals.isPausedTheGame = true;
                        int characterOrEntry = TimerGame.MakeRandomValue(1, 3);
                        if(characterOrEntry == 1)
                        {
                            charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                        }
                        else
                        {
                            
                            charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);
                        }

                        //  DebugPfCnsl.println("min break time = " + TimerGame.MIN_BREAK_TIME
                        // + " max break time = " + TimerGame.MAX_BREAK_TIME);

                        //uncomment later
                        TimerGame.SleepRandomMinute(TimerGame.MIN_BREAK_TIME,
                            TimerGame.MAX_BREAK_TIME);

                        //comment later
                       // TimerGame.SleepRandom(4000, 5999);

                        ThreadGlobals.isPausedTheGame = false;
                        timeWormController.SetStartedSecondTime();
                        while(!ThreadGlobals.isSettingButtonSeemed)
                        {
                            if (ThreadGlobals.isFishingStopped) return;
                        }
                        //comment
                        //timeGameRest.SetStartedSecondTime();
                        timeGame.SetStartedMinuteTime();
                       // timeGameRest.SetStartedSecondTime();
                    }
                }
                if (ThreadGlobals.isFishingMiniBreakActive)
                {
                    //Give little break 
                    if (timeGame.CheckCountDownMinute(4, 9))
                    // if (timeGameRest.CheckCountDownSecond(8,20))
                    {
                        timeGame.StartTimeBreakSecond(10, 25);
                        // timeGameRest.StartTimeBreakSecond(0, 1);
                        timeWormController.SetStartedSecondTime();
                    }

                }

                InsertWormForFishing();
                timeGame.SetStartedSecondTime();

                while(!ThreadGlobals.isActiveFishBoard && timeGame.CheckDelayTimeInSecond(5))
                {
                    if (ThreadGlobals.isFishingStopped) return;
                }
                TimerGame.SleepRandom(40, 60);

                if(timeGame.CheckDelayTimeInSecond(5))
                {
                    if (HandleFishPreferings())
                    {
                        while (!ThreadGlobals.isFishingStopped && ThreadGlobals.isActiveFishBoard)
                        {

                            ClickFish(screenshot.ConvertBitmapToArray(
                                screenshot.CaptureSpecifiedScreen(coordinates.RectFishClickArea())));
                        }
                        if(isAltinTonDetected)
                        {
                            TimerGame.SleepRandom(900, 1200);
                            for (int i = 0; i < 3; i++)
                            {
                                inputs.KeyPress(ScanCodeShort.ESCAPE);
                                TimerGame.SleepRandom(400, 600);
                            }
                            isAltinTonDetected = false;
                        }
                        
                        charThings.WearOnOffArmor();
                    }
                    else
                    {
                        while (ThreadGlobals.isActiveFishBoard)
                        {
                            if (ThreadGlobals.isFishingStopped) return;
                            inputs.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                            TimerGame.SleepRandom(15,30);
                        }
                        HandlePinkChat();
                        if(!isHandlePinkFuncWorked) 
                        {
                            charThings.WearOnOffArmor();
                        }
                        else
                        {
                            isHandlePinkFuncWorked = false;
                           
                        }
                        
                    }
                }
                else
                {
                    HandlePinkChat();
                    if (!isHandlePinkFuncWorked)
                    {
                        if(timerWormEmptyCounter.CheckDelayTimeInSecond(60))
                        {
                            if(oldEmptyCounter != 8)
                            {
                                charThings.WearOnOffArmor();
                                oldEmptyCounter = 0;
                            }
                        }
                        
                    }
                    else
                    {
                        isHandlePinkFuncWorked = false;

                    }
                }
                charThings.OpenCloseInventory(false);
              
            }
           // ThreadGlobals.DebugThreadGloablValues();


        }
        private void InsertWormForFishing()
        {
            if (!ThreadGlobals.CanFishingRightNow()) {
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }

            while(timeWormController.CheckDelayTimeInSecond(5))
            {
                slotCounter = 0;
                if (!ThreadGlobals.CanFishingRightNow())
                {
                    ThreadGlobals.DebugThreadGloablValues();
                    return;
                }
            }
            
            int[] arrayComparable;
            int[] arrayScannedWorms;
            emptyCounter = 0;

         //   DebugPfCnsl.println("InsertWorm DebugThreadGlobalPref = " +
            //        ThreadGlobals.GetGameStatusOrUserPrefer());
            //TimerGame.SleepRandom(100, 600);
            TimerGame.SleepRandomForPlayers(0, 0, 300, 600);

            if (slotCounter < 0 || slotCounter > 7) slotCounter = 0;

            for (; slotCounter < 8; slotCounter++)
            {
             
                //debugConsole.printlnTime("InsertWormForFishing is working",2);
                if (slotCounter < 4)
                {
                    arrayComparable = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(
                        new Rectangle(
                        coordinates.RectSkillSlotFirstPlace().X + (32 * slotCounter), coordinates.RectSkillSlotFirstPlace().Y
                        , coordinates.RectSkillSlotFirstPlace().Width, coordinates.RectSkillSlotFirstPlace().Height)));

                    arrayScannedWorms = gameImages.DetectWorms(arrayComparable);

                    if (gameImages.CompareTwoArrayAdvanced(arrayReferenceWormVal, arrayScannedWorms, ImageSensibilityLevel.SENSIBILTY_HIGH))
                    {
                        //DebugPfCnsl.println("first slot must press keys");
                        inputs.KeyPress(KeyboardInput.ScanCodeShort.KEY_1 + (short)slotCounter);//inputs.KeyPress(Keys.D1 + slotCounter);
                        inputs.KeyPress(KeyboardInput.ScanCodeShort.SPACE);
                        break;
                    }
                    if(timerWormEmptyCounter.CheckDelayTimeInSecond(60))
                    {
                        emptyCounter++;
                    }
                    
                    
                }
                else
                {
                   // if ( emptyCounter < 4 )
                    //{
                      
                       // DebugPfCnsl.println("Emptycounter <4 and enter char screen");
                        /*charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                        TimerGame.MakeRandomValue(20, 30);
                      
                         while (ThreadGlobals.isCharScreenActive)
                         {
                             if(!ThreadGlobals.GetGameStatusOrUserPrefer())return;
                         }*/
                       
                        
                 //   }
                 

                    arrayComparable = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(
                        new Rectangle(
                        coordinates.RectSkillSlotSecondPlace().X + (32 * (slotCounter - 4)), coordinates.RectSkillSlotSecondPlace().Y
                        , coordinates.RectSkillSlotSecondPlace().Width, coordinates.RectSkillSlotSecondPlace().Height)));

                    arrayScannedWorms = gameImages.DetectWorms(arrayComparable);

                    if (gameImages.CompareTwoArrayAdvanced(arrayReferenceWormVal, arrayScannedWorms, ImageSensibilityLevel.SENSIBILTY_HIGH))
                    {
                       
                        inputs.KeyPress(KeyboardInput.ScanCodeShort.F1 + (short)+slotCounter - 4);//inputs.KeyPress(Keys.F1 + slotCounter -4 );
                        inputs.KeyPress(KeyboardInput.ScanCodeShort.SPACE);
                        break;
                    }
                    if (timerWormEmptyCounter.CheckDelayTimeInSecond(60))
                    {
                        emptyCounter++;
                    }
                    //If 8 skill slot is fully empty, don't need to refresh game
                    //shift up the number quickly
                    if (emptyCounter >= 8)
                    {
                        CharSpecialThings.shiftNumber++;
                        DecideShiftPage(emptyCounter);
                        oldEmptyCounter = emptyCounter;
                        emptyCounter = 0;
                       
                        
                    }
                    //Refresh the game after char finished worms then shift the number up
                    else if (gameImages.CompareTwoArrayAdvanced(gameImages.arrayEmptySlotPlace,
                        arrayComparable, ImageSensibilityLevel.SENSIBILTY_HIGH) && slotCounter >= 7)
                    {
                        CharSpecialThings.shiftNumber++;
                        DecideShiftPage(emptyCounter);
                        oldEmptyCounter = emptyCounter;
                        emptyCounter = 0;
                    }


                    
                }
            }
        }
        
        private void DecideShiftPage(int emptyCounter)
        {
            if (!ThreadGlobals.CanFishingRightNow()) {
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }
            if (CharSpecialThings.shiftNumber > 4)
            {
                DebugPfCnsl.println("Second Worm Slot Refresh is Running");
                //TODO:enable refreshgame after test

                charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                //Wait Until Program is detected to exit Char Screen
                TimerGame.SleepRandom(3000, 4000);
                DebugPfCnsl.println(" Shift > 4 Gamestatus isCharScreenActive = " + ThreadGlobals.isCharScreenActive);
                while (!ThreadGlobals.CanFishingRightNow())
                {
                    //DebugPfCnsl.println("balık tutmak için hazir değil");
                    if (ThreadGlobals.isFishingStopped)
                    {
                        DebugPfCnsl.println("Game is Stopped");
                        return;
                    }
                }
                TimerGame.SleepRandom(3000, 4000);
                DebugPfCnsl.println(" @@@@  Shift > 4 Gamestatus isCharScreenActive = " + ThreadGlobals.isCharScreenActive);
                //Check Character Button is active and close it
                charThings.OpenCloseSettingButton(false);


                //Check Inventory Page is Closed
                charThings.OpenCloseInventory(false);



                prepareFish.StartPrepareFishing();
                TimerGame.SleepRandom(1000, 2000);
               // slotCounter = -1;

                CharSpecialThings.shiftNumber = 1;
                charThings.CheckShiftPageNumber();
              
            }
            else
            {
                if(emptyCounter <8)
                {
                    charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                    TimerGame.SleepRandom(20, 40);
                    //Wait Until Program is detected to exit Char Screen
                    
                    DebugPfCnsl.println(" emptyCounter < 8 Gamestatus isCharScreenActive = " + ThreadGlobals.isCharScreenActive);
                    while (!ThreadGlobals.isSettingButtonSeemed)
                    {

                        if (ThreadGlobals.isFishingStopped)
                        {
                            DebugPfCnsl.println("Game is Stopped");
                            return;
                        }
                    }
                    TimerGame.SleepRandom(2000, 5000);
                }
                DebugPfCnsl.println("shift degisecek)");
               
                charThings.CheckShiftPageNumber();
              //  slotCounter = -1;
                timerWormEmptyCounter.SetStartedSecondTime();
            }
            timeWormController.SetStartedSecondTime();
        }
        private void ClickFish(int[] arrayScreenShot)
        {
            rectFish = coordinates.RectFishClickArea();
            int rgbFishPixel = 0;
            for (int y = 0; y < rectFish.Height; y++)
            {
                for (int x = 0; x < rectFish.Width; x++)
                {
                    rgbFishPixel = arrayScreenShot[(y * rectFish.Width) + x];

                    if (rgbFishPixel == fishPixelOneValue || rgbFishPixel == fishPixelTwoValue
                     || rgbFishPixel == fishPixelThreeValue || rgbFishPixel == fishPixelFourValue
                     || rgbFishPixel == fishPixelFifthValue || rgbFishPixel == fishPixelSixthValue
                     || rgbFishPixel == fishPixelSeventhValue || rgbFishPixel == fishPixelEightValue
                     || rgbFishPixel == fishPixelNinethValue || rgbFishPixel == fishPixelTenthValue
                     || rgbFishPixel == fishPixelElevenValue || rgbFishPixel == fishPixelTwelveValue
                     || rgbFishPixel == fishPixelThirteenthValue || rgbFishPixel == fishPixelFourteenthValue
                     || rgbFishPixel == fishPixelFiveteenthValue || rgbFishPixel == fishPixelSixteenthValue
                     ||rgbFishPixel== fishPixelSeventeenthValue)


                        {
                        if (TimerGame.IS_PC_SLOW)
                        {
                            ClickForSlowPc(inputs, x, y);
                        }
                        else
                        {
                           
                            inputs.MouseMoveQuickly(rectFish.X + x, rectFish.Y + y);
                            if (storeAttempt >= 2)
                            {
                                inputs.MouseClickQuickly(rectFish.X + x, rectFish.X + x);
                                Thread.Sleep(TimerGame.MakeRandomValue(200, 400));
                                storeAttempt = 0;
                            }

                            storeAttempt++;
                        }
                       
                        return;
                    }
                }
            }
        }
        private void ClickForSlowPc(GameInputHandler robotFishing, int xPos, int yPos)
        {

            if (storeAttempt == 0)
            {
                //n = x index
                //n+1 = y index
                arrayStoredFishValue[0] = xPos;
                arrayStoredFishValue[1] = yPos;
                robotFishing.MouseMoveQuickly((coordinates.RectFishClickArea().X + xPos),
                       (coordinates.RectFishClickArea().Y + yPos));
                storeAttempt++;
                //  DebugDrawingHandle.DrawWantedObjectToScreen(new Rectangle((coordinates.RectFishClickArea().X + xPos),
                //      (coordinates.RectFishClickArea().Y + yPos), 15, 15));
            }
            else if (storeAttempt == 1)
            {
                //n = x index
                //n+1 = y index
                arrayStoredFishValue[2] = xPos;
                arrayStoredFishValue[3] = yPos;
                robotFishing.MouseMoveQuickly((coordinates.RectFishClickArea().X + xPos),
                        (coordinates.RectFishClickArea().Y + yPos));
                storeAttempt++;
                // DebugDrawingHandle.DrawWantedObjectToScreen(new Rectangle((coordinates.RectFishClickArea().X + xPos),
                //    (coordinates.RectFishClickArea().Y + yPos), 15, 15));
            }

            else
            {
                //n = x index
                //n+1 = y index
                arrayStoredFishValue[4] = xPos;
                arrayStoredFishValue[5] = yPos;

                int yResult1 = arrayStoredFishValue[1] - arrayStoredFishValue[3];
                int xResult1 = arrayStoredFishValue[0] - arrayStoredFishValue[2];

                int yResult2 = arrayStoredFishValue[3] - arrayStoredFishValue[5];
                int xResult2 = arrayStoredFishValue[2] - arrayStoredFishValue[4];

                int yResult = (yResult1 + yResult2) / 4;
                int xResult = (xResult1 + xResult2) / 4;

                //  DebugPfCnsl.println("xResult = " + xResult + "  yResult = " + yResult);

                if ((yResult > -10 && yResult < 10) && (xResult > -10 && xResult < 10))
                {

                    robotFishing.MouseMoveQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                        coordinates.RectFishClickArea().Y + yPos + yResult);
                    robotFishing.MouseClickQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                        coordinates.RectFishClickArea().Y + yPos + yResult);

                }
                else
                {

                    //x küçükten büyüğe, y küçükten büyüğe ele al
                    if ((arrayStoredFishValue[0] < arrayStoredFishValue[2] && arrayStoredFishValue[2] < arrayStoredFishValue[4]) &&
                        (arrayStoredFishValue[1] < arrayStoredFishValue[3] && arrayStoredFishValue[3] < arrayStoredFishValue[5]))
                    {
                        yResult = Math.Abs(yResult1 + yResult2) / 3;
                        xResult = Math.Abs(xResult1 + xResult2) / 3;

                        //     DebugPfCnsl.println("x küçükten büyüğe, y küçükten büyüğe çalıştı ");

                        robotFishing.MouseMoveQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                        robotFishing.MouseClickQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                    }
                    //x büyükten küçüğe, y küçükten büyüğe ele al
                    else if ((arrayStoredFishValue[0] > arrayStoredFishValue[2] && arrayStoredFishValue[2] > arrayStoredFishValue[4]) &&
                        (arrayStoredFishValue[1] < arrayStoredFishValue[3] && arrayStoredFishValue[3] < arrayStoredFishValue[5]))
                    {
                        yResult = Math.Abs(yResult1 + yResult2) / 3;
                        xResult = -1 * (xResult1 + xResult2) / 3;

                        //    DebugPfCnsl.println("x büyükten küçüğe, y küçükten büyüğe çalıştı ");

                        robotFishing.MouseMoveQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                        robotFishing.MouseClickQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                    }
                    //x küçükten büyüğe, y büyükten küçüğe ele al
                    else if ((arrayStoredFishValue[0] < arrayStoredFishValue[2] && arrayStoredFishValue[2] < arrayStoredFishValue[4]) &&
                        (arrayStoredFishValue[1] > arrayStoredFishValue[3] && arrayStoredFishValue[3] > arrayStoredFishValue[5]))
                    {
                        yResult = -1 * (yResult1 + yResult2) / 3;
                        xResult = Math.Abs(xResult1 + xResult2) / 3;

                        //     DebugPfCnsl.println("x küçükten büyüğe, y büyükten küçüğe çalıştı ");

                        robotFishing.MouseMoveQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                        robotFishing.MouseClickQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                    }
                    //x büyükten küçüğe, y büyükten küçüğe ele al
                    else if ((arrayStoredFishValue[0] > arrayStoredFishValue[2] && arrayStoredFishValue[2] > arrayStoredFishValue[4]) &&
                        (arrayStoredFishValue[1] > arrayStoredFishValue[3] && arrayStoredFishValue[3] > arrayStoredFishValue[5]))
                    {
                        yResult = -1 * (yResult1 + yResult2) / 3;
                        xResult = -1 * (xResult1 + xResult2) / 3;

                        //   DebugPfCnsl.println("x büyükten küçüğe, y büyükten küçüğe çalıştı ");

                        robotFishing.MouseMoveQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                        robotFishing.MouseClickQuickly(coordinates.RectFishClickArea().X + xPos + xResult,
                            coordinates.RectFishClickArea().Y + yPos + yResult);
                    }

                }

                storeAttempt = 0;
                //TimerGame.SleepRandom(400, 600);

            }
        }

        private void HandlePinkChat()
        {
            if (!ThreadGlobals.CanFishingRightNow())
            {
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }
            

            int[] targetPinkChat = gameImages.RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                screenshot.ImageArraySpecifiedArea(coordinates.RectChatArea()));

            if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkYerYok,
                targetPinkChat))
                {
                isHandlePinkFuncWorked = true;
                ThrowWorm();
                prepareFish.StartPrepareFishing();
                }

            if(gameImages.compareTwoArrayQuickly(gameImages.arrayPinkCanNotFish
                , targetPinkChat))
            {
                isHandlePinkFuncWorked = true;
                prepareFish.GoToFishPlace();
            }
            int[] targetPinkUpChat = gameImages.RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                screenshot.ImageArraySpecifiedArea(coordinates.RectUpChatArea()));

            if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkCanNotFish
                , targetPinkUpChat))
            {
                isHandlePinkFuncWorked = true;
                prepareFish.GoToFishPlace();
            }

        }

        public void ThrowWorm()
        {
            if (!ThreadGlobals.CanFishingRightNow())
            {
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }
            Rectangle[] resultWorm = charThings.CheckObjectInventory(gameImages.arrayWorm, coordinates.RectWormSample(),
                InventoryPage.Page_1);

            if(resultWorm.Length > 0)
            {
                while(!gameImages.CompareTwoArrayAdvanced(gameImages.arrayYereAtmaDialog,
                    screenshot.ImageArraySpecifiedArea(coordinates.RectYereAtmaAlgilama()),
                    ImageSensibilityLevel.SENSIBILTY_HIGH))
                {
                    if(!ThreadGlobals.CanFishingRightNow())return;
                    charThings.OpenCloseInventory(true);

                    inputs.MouseMoveAndPressLeft(resultWorm[0].X + resultWorm[0].Width / 2, resultWorm[0].Y);

                    inputs.MouseMoveAndPressLeft(resultWorm[0].X + resultWorm[0].Width - 200, resultWorm[0].Y);
                    TimerGame.SleepRandom(240, 340);

                }
                TimerGame.SleepRandom(300, 450);

                inputs.MouseMoveAndPressLeft(coordinates.PointYesButton().X,
                    coordinates.PointYesButton().Y);

                charThings.OpenCloseInventory(false);
            }

        }

        private bool HandleFishPreferings()
        {
            if (!ThreadGlobals.CanFishingRightNow()) {
                ThreadGlobals.DebugThreadGloablValues();
                return false;
            }
            TimerGame timePinkChat  = new TimerGame();

            int[] targetPinkChat = gameImages.RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
              screenshot.ImageArraySpecifiedArea(coordinates.RectChatArea()));
                while(targetPinkChat == null)
            {
                if(timePinkChat.CheckDelayTimeInSecond(2))
                {
                    if (!ThreadGlobals.CanFishingRightNow()) return false;

                    targetPinkChat = gameImages.RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                  screenshot.ImageArraySpecifiedArea(coordinates.RectChatArea()));
                }
                else
                {
                    DebugPfCnsl.println("Pink Chat value couldn't detected");
                    return false;
                }
                
            }

            if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkTon,
               targetPinkChat))
            {
                isAltinTonDetected = true;
                return true;
            }
            if (ThreadGlobals.isHepsiSelected)
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkOlta, targetPinkChat))
                {
                    return false;
                }
                return true;
            }
            if(ThreadGlobals.isYabbieSelected)
            {
                if(gameImages.compareTwoArrayQuickly(gameImages.arrayPinkYabbie, targetPinkChat))return true;
            }
            if(ThreadGlobals.isAltinSudakSelected) 
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkAltinSudak, targetPinkChat)) return true;
            }
            if(ThreadGlobals.isPalamutSelected)
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkPalamut, targetPinkChat)) return true;
            }
            if(ThreadGlobals.isKurbagaSelected)
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkKurbaga, targetPinkChat)) return true;
            }
            if (ThreadGlobals.isKadifeSelected)
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkKadife, targetPinkChat)) return true;
            }
            if (ThreadGlobals.isDenizkizSelected)
            {
                if (gameImages.compareTwoArrayQuickly(gameImages.arrayPinkDenizkiz, targetPinkChat)) return true;
            }
            return false;
        }
    }
}
