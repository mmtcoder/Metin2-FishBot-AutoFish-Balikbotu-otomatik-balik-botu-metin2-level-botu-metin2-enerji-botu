using Metin2AutoFishCSharp.Sources;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.GameHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MusicPlayerApp.Sources.KeyboardInput;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MusicPlayerApp.Sources.CharacterHandle
{
    public enum SettingButtonPrefers
    {
        CHAR_BUTTON,
        EXIT_BUTTON
    }
    public enum InventoryPage
    {
        Page_1,
        Page_2
    }
    public enum InsertCountSetting
    {
        INSERT_COUNT_32,
        INSERT_COUNT_1
    }
    internal class CharSpecialThings : CharInfo
    {
        private ImageObjects imageObject;
        private GameInputHandler inputGame;
        private GameObjectCoordinates coor;
        private ScreenShotWinAPI screenshot;
        private PrepareFishing prepareFishing;

        private readonly int WHITE200_ITEM_DIF_X = 14;
        private readonly int WHITE200_ITEM_DIF_Y = 11;

        public static List<Rectangle> rectangleList;
        public static volatile int shiftNumber = 1;

        private object lockSettingButton = new object();

        private int pageCombineItems200 = 1;
        private int yCombineItems;
        private int xCombineItems;

        private int tradeImageCounter;

        private bool isCombineItem200failed = false;

        private int tradePathWayCounter = 1;
        public CharSpecialThings(ImageObjects imageObject) : base(imageObject)
        {
            this.imageObject = imageObject;
            inputGame = new GameInputHandler();
            coor = new GameObjectCoordinates(imageObject);
            screenshot = new ScreenShotWinAPI();
            rectangleList = new List<Rectangle>();
            prepareFishing = new PrepareFishing(imageObject,this);
        }

        public void WearOnOffArmor()
        {
            if (!ThreadGlobals.CanFishingRightNow())
            {
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }
            
            //inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
            TimerGame.SleepRandomForPlayers(20,40,400, 1300);
            inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_TURKISH_I);
            inputGame.MouseMoveAndPressRight(coor.PointArmorPlace().X, coor.PointArmorPlace().Y);
            inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_TURKISH_I);
        }

        public void CloseSaleTitle()
        {
            int[] targetArray = screenshot.ImageArraySpecifiedArea(coor.RectSaleCross());
            while(imageObject.CompareTwoArrayAdvanced(imageObject.arraySaleTitle,
                targetArray,ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                if(ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return;
                inputGame.MouseMove(coor.RectSaleCross().X, coor.RectSaleCross().Y);
                inputGame.MouseClick(0, 0);
                targetArray = screenshot.ImageArraySpecifiedArea(coor.RectSaleCross());
            }

            ThreadGlobals.isSaleTitleActive = false;
        }

        public void OpenCloseSettingButton(bool state)
        {
            int[] targetCharButton = screenshot.ImageArraySpecifiedArea(coor.RectCharButton());
            while (imageObject.CompareTwoArrayAdvanced(imageObject.arrayCharButton,
                targetCharButton,ImageSensibilityLevel.SENSIBILTY_HIGH) != state)
            {
                //buraya setting buton görülmedi thread gloabal değeri koyma!!
                if ((ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed))
                {
                    DebugPfCnsl.println("OpenCloseSettingButton is returned");
                    return; 
                } 
                inputGame.MouseMoveAndPressLeft(coor.RectSettingButton().X, coor.RectSettingButton().Y);
                targetCharButton = screenshot.ImageArraySpecifiedArea(coor.RectCharButton());
            }
        }
        
        public void CloseTradePanel()
        {
            TimerGame timerTrade = new TimerGame();

            FileHandler.SaveImageAsPng(screenshot.CaptureSpecifiedScreen(coor.RectMetin2GameScreen()),
                "trade" + tradeImageCounter++ + ".png", PathWayStruct.PATH_SCREENSHOTS);

            while (CheckTradePanelActive())
            {
                if (timerTrade.CheckDelayTimeInSecond(8))
                {
                    if (ThreadGlobals.CheckGameIsStopped())
                    {
                        DebugPfCnsl.println("ticaret cami kapatilamadı");
                        return;
                    }
                    

                    inputGame.MouseMoveAndPressLeft(coor.PointTradeCloseCross().X,
                        coor.PointTradeCloseCross().Y);

                    TimerGame.SleepRandom(300, 500);
                }
            }
            

            ThreadGlobals.isTradePanelActive = false;
            TimerGame.SleepRandom(2000, 3000);
        }
        public void OpenCloseInventory(bool state)
        {
            //DebugPfCnsl.println("OpenCloseInventory is running");
            TimerGame timerGame = new TimerGame();
            timerGame.SetStartedSecondTime();

            int[] targetArray = screenshot.ImageArraySpecifiedArea(coor.RectInventory());
            while(imageObject.CompareTwoArrayAdvanced(imageObject.arrayInventoryTitle,
                targetArray,ImageSensibilityLevel.SENSIBILTY_HIGH) != state)
            {
                if ((ThreadGlobals.CheckGameIsStopped()) || ThreadGlobals.isCharKilled ||
                    !ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("OpenCloseInventory fonksiyonu return edildi");
                    return;
                }
                
                if(timerGame.CheckDelayTimeInSecond(10))
                {
                    inputGame.MouseMove(coor.RectInventory().X - 100,
                    coor.RectInventory().Y);
                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_TURKISH_I);
                    TimerGame.SleepRandom(100, 150);
                    targetArray = screenshot.ImageArraySpecifiedArea(coor.RectInventory());
                }
                else
                {
                    if(!Thread.CurrentThread.Name.Equals(ThreadsHandler.T2Name)) 
                    {
                        DebugPfCnsl.println("envanter yazısı tespit edilemedi bu yüzden karakter" +
                            "atılıyor...");

                        inputGame.KeyPress(ScanCodeShort.ESCAPE);

                        SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);

                        timerGame.SetStartedSecondTime();
                        while (!ThreadGlobals.isSettingButtonSeemed)
                        {
                            if (ThreadGlobals.CheckGameIsStopped() ) return;
                            if(!timerGame.CheckDelayTimeInSecond(15))return;
                        }
                        TimerGame.SleepRandom(3000, 4000);
                        timerGame.SetStartedSecondTime();
                    }
                    else
                    {
                        throw new Exception("Calling this function is in the WRONG THREAD" +
                            "This thread already maintance char screen");
                    }
                }
               
            }
        }

        public void SettingButtonClick(SettingButtonPrefers buttonPrefers)
        {
            lock(lockSettingButton)
            {
                //Check Setting Option is active and get it
                OpenCloseSettingButton(true);
                while(prepareFishing.CheckFisherShopPage())
                {
                    if (ThreadGlobals.CheckGameIsStopped()) return;
                    prepareFishing.CloseFisherShopPage();
                }
                TimerGame timerGame = new TimerGame();
                timerGame.SetStartedSecondTime();

                if (buttonPrefers == SettingButtonPrefers.CHAR_BUTTON)
                {
                    ThreadGlobals.isCharScreenActive = true;
                    inputGame.MouseMove(coor.RectCharButton().X, coor.RectCharButton().Y);
                    inputGame.MouseClick(0, 0);
                    int[] targetCharScreen = screenshot.ImageArraySpecifiedArea(coor.RectCharScreen());

                    while (!imageObject.CompareTwoArrayAdvanced(imageObject.arrayCharScreen, targetCharScreen,
                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                    {
                        if (ThreadGlobals.CheckGameIsStopped()) return;
                        if (timerGame.CheckDelayTimeInSecond(11))
                        {
                            targetCharScreen = screenshot.ImageArraySpecifiedArea(coor.RectCharScreen());
                        }
                        else
                        {
                            OpenCloseSettingButton(true);
                            inputGame.MouseMove(coor.RectCharButton().X, coor.RectCharButton().Y);
                            inputGame.MouseClick(0, 0);
                            timerGame.SetStartedSecondTime();
                        }
                    }
                    ThreadGlobals.isCharScreenActive = true;
                }
                else
                {
                    ThreadGlobals.isEntryScreenActive = true;
                    inputGame.MouseMove(coor.PointExitButton().X, coor.PointExitButton().Y);
                    inputGame.MouseClick(0, 0);

                    int[] targetExitScreen = screenshot.ImageArraySpecifiedArea(coor.RectEntryScreen());
                    while (!imageObject.CompareTwoArrayAdvanced(imageObject.arrayEntryScreen, targetExitScreen,
                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                    {
                        if (ThreadGlobals.CheckGameIsStopped()) return;
                        if (timerGame.CheckDelayTimeInSecond(11))
                        {
                            targetExitScreen = screenshot.ImageArraySpecifiedArea(coor.RectEntryScreen());
                        }
                        else
                        {
                            OpenCloseSettingButton(true);
                            inputGame.MouseMove(coor.PointExitButton().X, coor.PointExitButton().Y);
                            inputGame.MouseClick(0, 0);
                            timerGame.SetStartedSecondTime();
                        }
                    }
                    ThreadGlobals.isEntryScreenActive = true;
                }
            }
                           
        }
        
        public void ClickWantedInventoryPage(InventoryPage page)
        {
            OpenCloseInventory(true);
            OpenCloseSettingButton(false);
            if(page == InventoryPage.Page_1)
            {
                if ((ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed))
                {
                    DebugPfCnsl.println("ClickWantedInventoryPage is returned");
                    return;
                }

                inputGame.MouseMoveAndPressLeft(coor.PointInventPageOne().X,
                    coor.PointInventPageOne().Y);
            }
            else if(page == InventoryPage.Page_2) 
            {
                if ((ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed))
                {
                    DebugPfCnsl.println("ClickWantedInventoryPage is returned");
                    return;
                }

                inputGame.MouseMoveAndPressLeft(coor.PointInventPageTwo().X,
                    coor.PointInventPageTwo().Y);
            }
        }

        public Rectangle[] CheckObjectInventory(int[]targetImage,Rectangle targetRect,InventoryPage page)
        {
         
            ClickWantedInventoryPage(page);

            Rectangle[] rects =  imageObject.FindAllImagesOnScreen(targetImage, targetRect, coor.RectInventoryPageArea());

            return rects;
        }

        public Rectangle[] CheckObjectTwoPageInventory(int[] targetImage, Rectangle targetRect)
            {
            ClickWantedInventoryPage(InventoryPage.Page_1);

            Rectangle[] rectsPage1 = imageObject.FindAllImagesOnScreen(targetImage, targetRect, coor.RectInventoryPageArea());

            ClickWantedInventoryPage(InventoryPage.Page_2);

            Rectangle[] rectsPage2 = imageObject.FindAllImagesOnScreen(targetImage, targetRect, coor.RectInventoryPageArea());

            List<Rectangle> totalListRects = new List<Rectangle>();

            if(rectsPage1 != null && rectsPage1.Length > 0)
            {
                foreach (var rect1 in rectsPage1)
                {
                    totalListRects.Add(rect1);
                }
            }
           

            totalListRects.Add(PageOneRectangle());

            if(rectsPage2 != null && rectsPage2.Length > 0)
            {
                foreach (var rect2 in rectsPage2)
                {
                    totalListRects.Add(rect2);
                }
            }
            
          
            return totalListRects.ToArray();
             }

        public Rectangle[] CheckObjectFromSkillSlots(int[] sourceItemIcon,Rectangle sourceItemSize)
        {
            DebugPfCnsl.println("CheckObjectFromSkillSlots is started");

            List<Rectangle> listRects = new List<Rectangle>();

            //first check first skill slot places

            for(int i = 0; i < 8; i++)
            {
                if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("CheckObjectFromSkillSlots return edildi");
                }
                if (i <= 3)
                {
                    Rectangle rectScanArea = new Rectangle(
                       coor.RectSkillSlotFirstPlace().X + (32 * i), coor.RectSkillSlotFirstPlace().Y - 16
                       , coor.RectSkillSlotFirstPlace().Width, coor.RectSkillSlotFirstPlace().Height +16);

                    Rectangle[] rectResult = imageObject.FindAllImagesOnScreen(sourceItemIcon, sourceItemSize
                        , rectScanArea);
                    //We can just detect one item so 0 index is enought for us
                    if(rectResult != null && rectResult.Length > 0)
                    {                     
                            listRects.Add(rectResult[0]);
                                               
                    }
                }
                else
                {
                    Rectangle rectScanArea = new Rectangle(
                      coor.RectSkillSlotSecondPlace().X + (32 * (i - 4)), coor.RectSkillSlotSecondPlace().Y -16
                      , coor.RectSkillSlotSecondPlace().Width, coor.RectSkillSlotSecondPlace().Height +16);

                    Rectangle[] rectResult = imageObject.FindAllImagesOnScreen(sourceItemIcon, sourceItemSize
                       , rectScanArea);
                    //We can just detect one item so 0 index is enought for us
                    if (rectResult != null && rectResult.Length > 0)
                    {
                        listRects.Add(rectResult[0]);

                    }
                }
               
        }
            DebugPfCnsl.PrintArray(listRects.ToArray());
            return listRects.ToArray();
        }

        public bool CheckTradePanelActive()
        {
            int[] currentTrade = screenshot.ImageArraySpecifiedArea(
                coor.RectTradeDetectionArea());

           
            
            if (imageObject.CompareTwoArrayAdvanced(imageObject.arrayTradeIcon,currentTrade,
                ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                ThreadGlobals.isTradePanelActive = true;
                TelegramBot.SendMessageTelegram("Birisi sana ticaret attı ve ScreenShot dosyasına görüntü kaydedildi");
                return true;
            }
            ThreadGlobals.isTradePanelActive = false;
            return false;
        }
        public void CheckShiftPageNumber()
        {

            TimerGame timerGame = new TimerGame();
            timerGame.SetStartedSecondTime();

            if (!ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("CheckShiftPageNumber is returned");
                ThreadGlobals.DebugThreadGloablValues();
                return;
            }
            
            try
            {


                if (shiftNumber == 1)
                {
                    MakeShiftChanging(timerGame, imageObject.arrayPageOne);
                }
                else if (shiftNumber == 2)
                {
                    MakeShiftChanging(timerGame, imageObject.arrayPageTwo);
                }
                else if (shiftNumber == 3)
                {
                    MakeShiftChanging(timerGame, imageObject.arrayPageThree);
                }
                else if (shiftNumber == 4)
                {
                    MakeShiftChanging(timerGame, imageObject.arrayPageFour);

                }
                ThreadGlobals.isCheckedShiftPage = true;

            }
            catch (Exception ex)
            {
                ThreadGlobals.isCheckedShiftPage = true;
                DebugPfCnsl.println(ex.Message);
            }
        }
        private void MakeShiftChanging(TimerGame timerGame, int[] sourceArray)
        {
            int[] targetPageArray = screenshot.ImageArraySpecifiedArea(coor.RectPageNumCoordinates());
            int[] sourceWhite = imageObject.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR, sourceArray);
            int[] targetWhite = imageObject.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR, targetPageArray);
            timerGame.SetStartedSecondTime();
            while ((timerGame.CheckDelayTimeInSecond(15)) &&
                !imageObject.compareTwoArrayQuickly(sourceWhite, targetWhite))
            {

                if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return;
                // TimerGame.MakeRandomValue(25, 35);//robot.delay(30);
                inputGame.KeyDown(KeyboardInput.ScanCodeShort.SHIFT);// inputs.KeyDown(Keys.ShiftKey);
                TimerGame.SleepRandom(25, 35);//robot.delay(20);
                inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_1 + (short)shiftNumber - 1);// inputs.KeyDown(Keys.D1 + shiftNumber - 1);
                TimerGame.SleepRandom(90, 110);//robot.delay(100);
                inputGame.KeyRelease(KeyboardInput.ScanCodeShort.SHIFT);//inputs.KeyUp(Keys.D1 + shiftNumber - 1);
                TimerGame.SleepRandom(25, 35);//robot.delay(20);
                inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_1 + (short)shiftNumber - 1);//inputs.KeyUp(Keys.ShiftKey);
                TimerGame.SleepRandom(600, 800);//robot.delay(700);

                targetPageArray = screenshot.ImageArraySpecifiedArea(coor.RectPageNumCoordinates());
                targetWhite = imageObject.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR, targetPageArray);

            }

        }

        public bool InsertObjectToSkillSlots(int[] itemImage200,Rectangle rectItemImageSize,InsertCountSetting insertObjectCount)
        {
            DebugPfCnsl.println("InsertSkillSlotsToObject function is called");
            
            Rectangle [] rectsCoor = CheckObjectTwoPageInventory(itemImage200, rectItemImageSize);

          
        //    DebugPfCnsl.PrintArray(rectsCoor);

            TimerGame timerInsert = new TimerGame();
            short item200Counter = 0;
            int[] arrayClippedImage = new int[1];

            if (insertObjectCount == InsertCountSetting.INSERT_COUNT_32)
            {
                arrayClippedImage = imageObject.ClipIntArray(itemImage200, coor.RectFirstSlotPlace(),
                coor.RectItemWithoutNumbersSample());
            }


            ClickWantedInventoryPage(InventoryPage.Page_1);

            DebugPfCnsl.println("rectsCoor lenght = " + rectsCoor.Length);
            if (rectsCoor.Length <= 0)
            {
                DebugPfCnsl.println("InsertSkillSlotsToObject rectsCoor lenght 0 returned false");
                return false;
            }
          
            OpenCloseInventory(true);
            timerInsert.SetStartedSecondTime();

            if(timerInsert.CheckDelayTimeInSecond(90))
            {
                for (int shiftChanger = 1; shiftChanger <= 4; shiftChanger++)
                {
                    shiftNumber = shiftChanger;
                    CheckShiftPageNumber();
                    for (int i = 0; i < 8; i++)
                    {
                        if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                        {
                            DebugPfCnsl.println("ya oyun durduruldu yada setting butonu görülmüyor");
                            return false;
                        }
                        if(insertObjectCount == InsertCountSetting.INSERT_COUNT_32)
                        {
                            if (rectsCoor[item200Counter].X == PageOneRectangle().X)
                            {
                                ClickWantedInventoryPage(InventoryPage.Page_2);
                                item200Counter++;
                            }
                        }
                        else
                        {
                            if(rectsCoor.Length > 1)
                            {
                                if (rectsCoor[item200Counter].X == PageOneRectangle().X)
                                {
                                    ClickWantedInventoryPage(InventoryPage.Page_2);
                                    item200Counter++;
                                }
                            }
                           else if(rectsCoor.Length == 1)
                            {
                                if (rectsCoor[item200Counter].X == PageOneRectangle().X)
                                {
                                    DebugPfCnsl.println("istenilen nesne tespit edilemedi");
                                    return false;
                                }
                            }
                        }
                       
                        if (i < 4)
                        {

                            Rectangle rectControlSlot = new Rectangle(coor.RectSkillSlotFirstPlace().X
                                + (i * coor.RectSkillSlotFirstPlace().Width), coor.RectSkillSlotFirstPlace().Y,
                                coor.RectSkillSlotFirstPlace().Width, coor.RectSkillSlotFirstPlace().Height);

                            //If skill slot place empty put wanted object
                            if (imageObject.CompareTwoArrayAdvanced(imageObject.arrayEmptySlotPlace,
                                screenshot.ImageArraySpecifiedArea(rectControlSlot), ImageSensibilityLevel.SENSIBILTY_HIGH))
                            {
                                DebugPfCnsl.println(i + " indek boş buraya yerleştiriliyor");
                                DebugPfCnsl.println("tespit edilen nesnenin rectangle değeri = " +
                                   rectsCoor[item200Counter]);

                                inputGame.KeyDown(ScanCodeShort.LCONTROL);
                                TimerGame.SleepRandom(25, 45);
                                inputGame.MouseMoveAndPressLeft(rectsCoor[item200Counter].X +
                                    rectsCoor[item200Counter].Width / 2, (rectsCoor[item200Counter].Y +
                                    rectsCoor[item200Counter].Height / 2));

                                inputGame.KeyRelease(ScanCodeShort.LCONTROL);

                                TimerGame.SleepRandom(300, 400);
                                if(insertObjectCount == InsertCountSetting.INSERT_COUNT_1)return true;
                                //TimerGame.SleepRandom(1000, 1400);
                            }
                            if(insertObjectCount == InsertCountSetting.INSERT_COUNT_32)
                            {
                                //If detected object is other than we wanted throw it from skill slot
                                //and put we wanted object
                                while (imageObject.FindAllImagesOnScreen(arrayClippedImage, coor.RectItemWithoutNumbersSample(),
                                    rectControlSlot).Length < 1)
                                {
                                    //Throw unwanted object
                                    if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed) return false;

                                    // inputGame.MouseMove(rectControlSlot.X + rectControlSlot.Width / 2,
                                    //    rectControlSlot.Y + rectControlSlot.Height);
                                    // inputGame.MouseDown();

                                    inputGame.MouseMoveAndPressLeft(rectControlSlot.X + rectControlSlot.Width / 2,
                                        rectControlSlot.Y + rectControlSlot.Height);

                                    TimerGame.SleepRandom(100, 200);
                                    inputGame.MouseMoveAndPressLeft(rectControlSlot.X + rectControlSlot.Width / 2,
                                       rectControlSlot.Y + rectControlSlot.Height - 100);


                                    TimerGame.SleepRandom(300, 400);

                                    //Insert wanted object
                                    inputGame.KeyDown(ScanCodeShort.LCONTROL);
                                    TimerGame.SleepRandom(25, 45);
                                    inputGame.MouseMoveAndPressLeft(rectsCoor[item200Counter].X +
                                        rectsCoor[item200Counter].Width / 2, (rectsCoor[item200Counter].Y +
                                        rectsCoor[item200Counter].Height / 2));

                                    inputGame.KeyRelease(ScanCodeShort.LCONTROL);

                                    TimerGame.SleepRandom(300, 400);

                                    rectControlSlot = new Rectangle(coor.RectSkillSlotFirstPlace().X
                                    + (i * coor.RectSkillSlotFirstPlace().Width), coor.RectSkillSlotFirstPlace().Y,
                                    coor.RectSkillSlotFirstPlace().Width, coor.RectSkillSlotFirstPlace().Height);
                                }

                                item200Counter++;
                            }
                            else
                            {
                                DebugPfCnsl.println(i + " numaralı indeksdeki yer dolu ");
                            }
                          
                        }
                        else
                        {


                            Rectangle rectControlSlot = new Rectangle(coor.RectSkillSlotSecondPlace().X
                                + ((i - 4) * coor.RectSkillSlotSecondPlace().Width), coor.RectSkillSlotSecondPlace().Y,
                                coor.RectSkillSlotSecondPlace().Width, coor.RectSkillSlotSecondPlace().Height);

                            //If skill slot place empty put wanted object
                            if (imageObject.CompareTwoArrayAdvanced(imageObject.arrayEmptySlotPlace,
                                screenshot.ImageArraySpecifiedArea(rectControlSlot), ImageSensibilityLevel.SENSIBILTY_HIGH))
                            {
                                inputGame.KeyDown(ScanCodeShort.LCONTROL);
                                TimerGame.SleepRandom(25, 45);
                                inputGame.MouseMoveAndPressLeft(rectsCoor[item200Counter].X +
                                    rectsCoor[item200Counter].Width / 2, (rectsCoor[item200Counter].Y +
                                    rectsCoor[item200Counter].Height / 2));

                                inputGame.KeyRelease(ScanCodeShort.LCONTROL);

                                TimerGame.SleepRandom(300, 400);
                                if (insertObjectCount == InsertCountSetting.INSERT_COUNT_1) return true;
                            }

                            if(insertObjectCount == InsertCountSetting.INSERT_COUNT_32)
                            {
                                //If detected object is other than we wanted throw it from skill slot
                                //and put we wanted object
                                while (imageObject.FindAllImagesOnScreen(arrayClippedImage, coor.RectItemWithoutNumbersSample(),
                                    rectControlSlot).Length < 1)
                                {
                                    //Throw unwanted object
                                    if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled) return false;
                                    inputGame.MouseMoveAndPressLeft(rectControlSlot.X + rectControlSlot.Width / 2,
                                        rectControlSlot.Y + rectControlSlot.Height);

                                    TimerGame.SleepRandom(100, 200);
                                    inputGame.MouseMoveAndPressLeft(rectControlSlot.X + rectControlSlot.Width / 2,
                                       rectControlSlot.Y + rectControlSlot.Height - 100);


                                    TimerGame.SleepRandom(300, 400);

                                    //Insert wanted object
                                    inputGame.KeyDown(ScanCodeShort.LCONTROL);
                                    TimerGame.SleepRandom(25, 45);
                                    inputGame.MouseMoveAndPressLeft(rectsCoor[item200Counter].X +
                                        rectsCoor[item200Counter].Width / 2, (rectsCoor[item200Counter].Y +
                                        rectsCoor[item200Counter].Height / 2));

                                    inputGame.KeyRelease(ScanCodeShort.LCONTROL);

                                    TimerGame.SleepRandom(300, 400);

                                    rectControlSlot = new Rectangle(coor.RectSkillSlotSecondPlace().X
                                     + ((i - 4) * coor.RectSkillSlotSecondPlace().Width), coor.RectSkillSlotSecondPlace().Y,
                                     coor.RectSkillSlotSecondPlace().Width, coor.RectSkillSlotSecondPlace().Height);

                                }
                                item200Counter++;
                            }
                            else
                            {
                                DebugPfCnsl.println(i + " numaralı indeksdeki yer dolu ");
                            }
                            
                        }
                    }
                    if(insertObjectCount == InsertCountSetting.INSERT_COUNT_1)
                    {
                        DebugPfCnsl.println("skill slot place inserting is failed maybe all of the place is full");
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }

            
            
            return true;
        }

       
        public int CombineItemsTo200(int[]targetImage)
        {
            OpenCloseInventory(true);
            OpenCloseSettingButton(false);

           
            int[] arrayBeforeCombine = null;
            int[] arrayClippedImage = imageObject.ClipIntArray(targetImage, coor.RectFirstSlotPlace(),
                 coor.RectItemWithoutNumbersSample());
            bool[] arrayWhite200Num = imageObject.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                imageObject.array200WhiteNumber);

            Rectangle rectCombineOne = Rectangle.Empty;
            Rectangle rectCombineTwo = Rectangle.Empty;

            int storeValue = 0;

           // int store200WhiteNumber = 0;
           // int indexStartPage2 = 0;

           // bool pageOneFinished = false;

            TimerGame timerGrillFishes = new TimerGame();


            for (; pageCombineItems200 <= 2; pageCombineItems200++)
            {
                if(isCombineItem200failed)
                {
                    xCombineItems = 0;
                    yCombineItems = 0;
                    pageCombineItems200 = 1;
                }
                if (pageCombineItems200 == 1)
                { 
                    ClickWantedInventoryPage(InventoryPage.Page_1);
                }
                else
                {
                    storeValue = 0;
                    ClickWantedInventoryPage(InventoryPage.Page_2);
                }

                for (; yCombineItems < 9; yCombineItems++)
                {
                    for (; xCombineItems < 5; xCombineItems++)
                    {
                        
                        if (timerGrillFishes.CheckDelayTimeInSecond(250))
                        {
                            Rectangle rectScanSlot = new Rectangle(coor.RectFirstSlotPlace().X + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * xCombineItems),
                           coor.RectFirstSlotPlace().Y + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * yCombineItems),
                           coor.RectFirstSlotPlace().Width, coor.RectFirstSlotPlace().Height);

                            //Detect wanted item image
                           if(imageObject.FindAllImagesOnScreen(arrayClippedImage,
                               coor.RectItemWithoutNumbersSample(),rectScanSlot).Length > 0)
                            {
                                bool[] scannedWhiteImage = imageObject.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                                    screenshot.ImageArraySpecifiedArea(rectScanSlot));
                                //Detect it doesn't has 200 white Number
                                if(imageObject.FindAllImagesBoolArrays(arrayWhite200Num,coor.Rect200WhiteSample(),
                                    scannedWhiteImage,rectScanSlot).Length <= 0)
                                {
                                    if (storeValue == 0)
                                    {
                                        arrayBeforeCombine = screenshot.ImageArraySpecifiedArea(rectScanSlot);
                                        rectCombineOne = rectScanSlot;
                                        storeValue++;
                                    }
                                    else
                                    {
                                        rectCombineTwo = rectScanSlot;
                                        storeValue = 0;
                                        
                                        
                                       // if(xCombineItems != 4)
                                       // {
                                            xCombineItems--;
                                            
                                       // }
                                        if (yCombineItems != 9)
                                        {
                                            yCombineItems--;
                                        }





                                        TimerGame timerCombine = new TimerGame();
                                        while (imageObject.CompareTwoArrayAdvanced(arrayBeforeCombine,
                                            screenshot.ImageArraySpecifiedArea(rectCombineOne),ImageSensibilityLevel.SENSIBILTY_HIGH))
                                        {
                                            if(timerCombine.CheckDelayTimeInSecond(5))
                                            {
                                                if(rectCombineOne.X == rectCombineTwo.X && 
                                                    rectCombineOne.Y == rectCombineTwo.Y)
                                                {
                                                    DebugPfCnsl.println("tespit edilen dikdörtgenlerin ikiside aynı");
                                                    yCombineItems++;
                                                   // storeValue = 1;
                                                    break;
                                                }
                                                inputGame.MouseMoveAndPressLeft((rectCombineOne.X + rectCombineOne.Width / 2),
                                           (rectCombineOne.Y + rectCombineOne.Height / 2));

                                                inputGame.MouseMoveAndPressLeft((rectCombineTwo.X + rectCombineTwo.Width / 2),
                                                 (rectCombineTwo.Y + rectCombineTwo.Height / 2));
                                                TimerGame.SleepRandom(300, 400);
                                            }
                                            else
                                            {
                                                DebugPfCnsl.println("CombineTime is elapsed so returned -1");
                                                isCombineItem200failed = true;
                                                return -1;
                                            }
                                           
                                        }
                                        
                                    }
                                }
                               
                                
                            }
                        
                        }
                        else
                        {
                            DebugPfCnsl.println("CombineItemsTo200 combining time is elapsed");
                            isCombineItem200failed = true;
                            //-1 means page seperator value 
                            return -1;
                        }


                    }
                    xCombineItems = 0;
                }
                yCombineItems = 0;
                
            }

            Rectangle[] rect200ItemCount = CheckObjectTwoPageInventory(targetImage, coor.RectItemSlotSizeSample()); 
            

            xCombineItems = 0;
            yCombineItems = 0;
            pageCombineItems200 = 1;
            isCombineItem200failed = false;
            
            if(rect200ItemCount != null)
            {
                DebugPfCnsl.println("CombineItemsTo200 counted item =  " + (rect200ItemCount.Length));

                return rect200ItemCount.Length -1;
            }
            else
            {
                return -1;
            }
            


        }

        public Rectangle PageOneRectangle()
        {
            return new Rectangle(9999, 9999, 1, 1);
        }

        public Rectangle PageTwoRectangle()
        {
            return new Rectangle(5555,5555,1,1);
        }
        private void CombineTwoItem(Rectangle[] rectItem, Rectangle[] recstWhite200)
        {
            Rectangle[] rect2Worm = new Rectangle[2];
            //Console.WriteLine("CombineTwoItem is called");
            //Console.WriteLine("rectItem lenght = " + rectItem.Length);
            //Console.WriteLine("recstWhite200 lenght = " + recstWhite200.Length);
           

            for(int itemIndex=0;  itemIndex< rectItem.Length; itemIndex++) 
            {
                for(int whiteNumber =0; whiteNumber < recstWhite200.Length; whiteNumber++)
              {
                
                    if (recstWhite200[whiteNumber].X - rectItem[itemIndex].X == WHITE200_ITEM_DIF_X &&
                        recstWhite200[whiteNumber].Y - rectItem[itemIndex].Y == WHITE200_ITEM_DIF_Y)
                    {
                      /*  Console.WriteLine("rectWhite200 else = " + recstWhite200[whiteNumber]
                         + "rectWhite200 index else = " + whiteNumber);
                        Console.WriteLine("rectPageOne  else = " + rectItem[itemIndex]
                            + "rectItem index else = " + itemIndex);
                        Console.WriteLine("rectItem Index is increased +1 ");*/
                        if(whiteNumber == recstWhite200.Length-1)
                        {
                            
                            whiteNumber--;
                        }
                        
                        itemIndex++;

                    }
                    else
                    {                     
                       /* Console.WriteLine("rectWhite200  = " + recstWhite200[whiteNumber]
                            + "rectWhite200 index = " + whiteNumber);
                        Console.WriteLine("rectPageOne  = " + rectItem[itemIndex]
                            + "rectItem index = " + itemIndex);*/

                        if (rect2Worm[0] == Rectangle.Empty)
                        {
                            rect2Worm[0] = rectItem[itemIndex];
                           // DebugPfCnsl.println("calisti 1 rect = " + rect2Worm[0]);
                            itemIndex++;
                            whiteNumber--;
                        }
                        else if (rect2Worm[1] == Rectangle.Empty)
                        {

                            rect2Worm[1] = rectItem[itemIndex];
                           // DebugPfCnsl.println("calisti 2 rect = " + rect2Worm[1]);

                            inputGame.MouseMoveAndPressLeft((rect2Worm[0].X + rect2Worm[0].Width / 2),
                              (rect2Worm[0].Y + rect2Worm[0].Height / 2));

                            inputGame.MouseMoveAndPressLeft((rect2Worm[1].X + rect2Worm[1].Width / 2),
                             (rect2Worm[1].Y + rect2Worm[1].Height / 2));
                            TimerGame.SleepRandom(250, 350);
                            return;
                        }
                    }
                }
                if(recstWhite200.Length <= 0)
                {
                    if (rect2Worm[0] == Rectangle.Empty)
                    {
                        rect2Worm[0] = rectItem[itemIndex];
                       // DebugPfCnsl.println("calisti 3 rect = " + rect2Worm[0]);
                       
                        
                    }
                    else if (rect2Worm[1] == Rectangle.Empty)
                    {

                        rect2Worm[1] = rectItem[itemIndex];
                       // DebugPfCnsl.println("calisti 3 rect = " + rect2Worm[1]);

                        inputGame.MouseMoveAndPressLeft((rect2Worm[0].X + rect2Worm[0].Width / 2),
                          (rect2Worm[0].Y + rect2Worm[0].Height / 2));

                        inputGame.MouseMoveAndPressLeft((rect2Worm[1].X + rect2Worm[1].Width / 2),
                         (rect2Worm[1].Y + rect2Worm[1].Height / 2));
                        TimerGame.SleepRandom(250, 350);
                        return;
                    }
                }
            }
        }


    }
}

/*  public int CombineItemsTo200(int[]targetImage,List<Rectangle> list200White)
        {
            OpenCloseInventory(true);
            OpenCloseSettingButton(false);

            if (list200White.Count > 0)
            {
                list200White.Clear();
            }
            int[] arrayBeforeCombine = null;
            int[] arrayClippedImage = imageObject.ClipIntArray(targetImage, coor.RectFirstSlotPlace(),
                 coor.RectItemWithoutNumbersSample());
            bool[] arrayWhite200Num = imageObject.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                imageObject.array200WhiteNumber);

            Rectangle rectCombineOne = Rectangle.Empty;
            Rectangle rectCombineTwo = Rectangle.Empty;

            int storeValue = 0;
           // int store200WhiteNumber = 0;
            int indexStartPage2 = 0;

            bool pageOneFinished = false;

            TimerGame timerGrillFishes = new TimerGame();


            for (; pageCombineItems200 <= 2; pageCombineItems200++)
            {
                if(isCombineItem200failed)
                {
                    xCombineItems = 0;
                    yCombineItems = 0;
                    pageCombineItems200 = 1;
                }
                if (pageCombineItems200 == 1)
                {
                    ClickWantedInventoryPage(InventoryPage.Page_1);
                }
                else
                {
                    ClickWantedInventoryPage(InventoryPage.Page_2);
                }

                for (; yCombineItems < 9; yCombineItems++)
                {
                    for (; xCombineItems < 5; xCombineItems++)
                    {
                        if (ThreadGlobals.isStopped || ThreadGlobals.isCharKilled)
                        {
                            
                            return list200White.Count -1;
                        }
                      

                        if (timerGrillFishes.CheckDelayTimeInSecond(250))
                        {
                            Rectangle rectScanSlot = new Rectangle(coor.RectFirstSlotPlace().X + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * xCombineItems),
                           coor.RectFirstSlotPlace().Y + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * yCombineItems),
                           coor.RectFirstSlotPlace().Width, coor.RectFirstSlotPlace().Height);

                            //Detect wanted item image
                           if(imageObject.FindAllImagesOnScreen(arrayClippedImage,
                               coor.RectItemWithoutNumbersSample(),rectScanSlot).Length > 0)
                            {
                                bool[] scannedWhiteImage = imageObject.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                                    screenshot.ImageArraySpecifiedArea(rectScanSlot));
                                //Detect it doesn't has 200 white Number
                                if(imageObject.FindAllImagesBoolArrays(arrayWhite200Num,coor.Rect200WhiteSample(),
                                    scannedWhiteImage,rectScanSlot).Length == 0)
                                {
                                    if (storeValue == 0)
                                    {
                                        arrayBeforeCombine = screenshot.ImageArraySpecifiedArea(rectScanSlot);
                                        rectCombineOne = rectScanSlot;
                                        storeValue++;
                                    }
                                    else
                                    {
                                        rectCombineTwo = rectScanSlot;
                                        storeValue = 0;
                                        
                                        
                                        if(xCombineItems != 4)
                                        {
                                            xCombineItems--;
                                            
                                        }
                                        else if (yCombineItems != 9)
                                        {
                                            yCombineItems--;
                                        }





                                        TimerGame timerCombine = new TimerGame();
                                        while (imageObject.CompareTwoArrayAdvanced(arrayBeforeCombine,
                                            screenshot.ImageArraySpecifiedArea(rectCombineOne),ImageSensibilityLevel.SENSIBILTY_HIGH))
                                        {
                                            if(timerCombine.CheckDelayTimeInSecond(5))
                                            {
                                                if(rectCombineOne.X == rectCombineTwo.X && 
                                                    rectCombineOne.Y == rectCombineTwo.Y)
                                                {
                                                    DebugPfCnsl.println("tespit edilen dikdörtgenlerin ikiside aynı");
                                                    yCombineItems++;
                                                    storeValue = 1;
                                                    break;
                                                }
                                                inputGame.MouseMoveAndPressLeft((rectCombineOne.X + rectCombineOne.Width / 2),
                                           (rectCombineOne.Y + rectCombineOne.Height / 2));

                                                inputGame.MouseMoveAndPressLeft((rectCombineTwo.X + rectCombineTwo.Width / 2),
                                                 (rectCombineTwo.Y + rectCombineTwo.Height / 2));
                                                TimerGame.SleepRandom(300, 400);
                                            }
                                            else
                                            {
                                                DebugPfCnsl.println("CombineTime is elapsed so returned 0");
                                                isCombineItem200failed = true;
                                                return 0;
                                            }
                                           
                                        }
                                        
                                    }
                                }
                                //Detected 200 White Number
                                else
                                {
                                    int removedIndex = 0;
                                    if (list200White.Count > 0)
                                    {
                                        if (!pageOneFinished)
                                        {
                                            for (int pageOneIndex = 0; pageOneIndex < list200White.Count; pageOneIndex++)
                                            {

                                                if (list200White[pageOneIndex].X == rectScanSlot.X && list200White[pageOneIndex].Y == rectScanSlot.Y)
                                                {
                                                    list200White.Remove(list200White[pageOneIndex]);
                                                    removedIndex = pageOneIndex;
                                                    break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(list200White.Count > indexStartPage2 +1)
                                            {
                                                for (int pageTwoIndex = indexStartPage2; pageTwoIndex < list200White.Count; pageTwoIndex++)
                                                {

                                                    if (list200White[pageTwoIndex].X == rectScanSlot.X && list200White[pageTwoIndex].Y == rectScanSlot.Y)
                                                    {
                                                        list200White.Remove(list200White[pageTwoIndex]);
                                                        removedIndex = pageTwoIndex;
                                                        break;
                                                    }
                                                }
                                            }
                                        
                                            
                                        }
               
                                    }
                                  
                                    DebugPfCnsl.println("x value = " + xCombineItems + "  y value = " + yCombineItems);
                                    if(removedIndex != list200White.Count)
                                    {
                                        list200White.Add(rectScanSlot);
                                    }
                                    else
                                    {
                                        list200White.Insert(removedIndex, rectScanSlot);
                                    }
                                   
                                }
                            }


                           
                        }
                        else
                        {
                            DebugPfCnsl.println("CombineItemsTo200 combining time is elapsed");
                            isCombineItem200failed = true;
                            //-1 means page seperator value 
                            return list200White.Count -1;
                        }


                    }
                    xCombineItems = 0;
                }
                yCombineItems = 0;
                if(pageCombineItems200 == 1)
                {
                    list200White.Add(PageOneRectangle());
                    pageOneFinished = true;
                    indexStartPage2 = list200White.Count -1;
                }
            }
            

            xCombineItems = 0;
            yCombineItems = 0;
            pageCombineItems200 = 1;
            isCombineItem200failed = false;
            DebugPfCnsl.println("CombineItemsTo200 counted item =  " + ( list200White.Count - 1));

            return list200White.Count - 1;


        }*/