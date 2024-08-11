using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.CoordinatesHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MusicPlayerApp.Sources.GameHandler
{
    internal class PrepareFishing
    {
        private int xGrillFishes = 0;
        private int yGrillFishes = 0;
        private int pageGrillFisher = 1;
        ImageObjects imageObjects;
        GameInputHandler inputGame;
        CharSpecialThings charThings;
        GameObjectCoordinates coordinate;
        ScreenShotWinAPI screenShot;

        private readonly int NEEDED_WORM200_COUNT = 32;

        private bool isGrillFishesFailed = false;

        List<Rectangle> listWorm200;

        public PrepareFishing(ImageObjects imageObjects)
        {
            this.imageObjects = imageObjects;
            inputGame = new GameInputHandler();
            charThings = new CharSpecialThings(imageObjects);
            coordinate = new GameObjectCoordinates(imageObjects);
            screenShot = new ScreenShotWinAPI();
            listWorm200 = new List<Rectangle>();
        }

        public PrepareFishing(ImageObjects imageObjects,CharSpecialThings charthings)
        {
            this.imageObjects = imageObjects;
            inputGame = new GameInputHandler();
            charThings = charthings;
            coordinate = new GameObjectCoordinates(imageObjects);
            screenShot = new ScreenShotWinAPI();
            listWorm200 = new List<Rectangle>();
        }

        public void StartPrepareFishing()
        {
            DebugPfCnsl.println("StartPrepareFishing is running");
            ThreadGlobals.isPrepareFishingStarted = true;
            FindFisher();
            GrillFishingHandle();
            WormsHandle();
            ThreadGlobals.isPrepareFishingStarted = false;
        }

        private void FindFisher()
        {
            TimerGame timeGame = new TimerGame();
            TimerGame.SleepRandom(1500, 2000);

            int zigZagWalking = 0;
            int walkSide = 1;

            DebugPfCnsl.println("FindFisher is Started");

            charThings.OpenCloseSettingButton(false);
            charThings.OpenCloseInventory(false);

            charThings.BirdViewPerpective();
            timeGame.SetStartedSecondTime();

            while (!CheckFisherIsThere())
            {
                if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled)
                {
                    ThreadGlobals.DebugThreadGloablValues();
                    return;
                }
                    
                if (timeGame.CheckDelayTimeInSecond(40))
                {
                    Rectangle walkingRect = imageObjects.FindBorderAreaBetweenColors(
                        coordinate.RectWoodDetectinonArea(),
                        ColorGame.MIN_WOOD_VALUE, ColorGame.MAX_WOOD_VALUE);

                    int xPos, yPos;

                    if (walkingRect != Rectangle.Empty)
                    {
                        zigZagWalking = TimerGame.MakeRandomValue(0,
                            120);



                        if (walkSide == 1)
                        {
                            xPos = walkingRect.X + (walkingRect.Width / 2) + zigZagWalking;
                            yPos = walkingRect.Y;
                            walkSide = 2;
                        } else
                        {
                            xPos = walkingRect.X + (walkingRect.Width / 2) - zigZagWalking;
                            yPos = walkingRect.Y;
                            walkSide = 1;
                        }

                        inputGame.MouseMoveAndPressLeft(xPos, yPos);
                       // TimerGame.SleepRandom(100, 200);
                       // inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_S);
                    }
                    else
                    {
                        xPos = coordinate.RectWoodDetectinonArea().X + (coordinate.RectWoodDetectinonArea().Width / 2);
                        yPos = coordinate.RectWoodDetectinonArea().Y;

                        inputGame.MouseMoveAndPressLeft(xPos, yPos);
                        //TimerGame.SleepRandom(100, 200);
                        //inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_S);
                    }
                }
                else
                {
                    DebugPfCnsl.println("FindFisher function couldn't find Balikci");
                    charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                    while(!ThreadGlobals.isSettingButtonSeemed)
                    {
                        if (ThreadGlobals.isFishingStopped) return;
                    }
                    charThings.OpenCloseSettingButton(false);
                    charThings.OpenCloseInventory(false);

                    TimerGame.SleepRandom(4000, 5500);

                    inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_A);
                    inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_S);
                    TimerGame.SleepRandom(1000, 1500);
                    inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_A);
                    inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_D);
                    TimerGame.SleepRandom(1000, 1500);
                    inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_D);
                    inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_S);
                    timeGame.SetStartedSecondTime();

                    charThings.OpenCloseSettingButton(false);
                    charThings.OpenCloseInventory(false);

                }
            }
        }

        private bool CheckFisherIsThere()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool[] targetGreenFisher = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN, imageObjects.arrayFisherWords);
          //  Console.WriteLine("targetGreenFisher length = " + targetGreenFisher.Length);
            bool[] scannableGreenFisher = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN,
                screenShot.ImageArraySpecifiedArea(coordinate.RectMetin2GameScreen()));

           // Console.WriteLine("RectMetin2GameScreen length = " +
           //    (coordinate.RectMetin2GameScreen().Width * coordinate.RectMetin2GameScreen().Height));

            for (int y = 0; y < coordinate.RectMetin2GameScreen().Height; y++)
            {
                for (int x = 0; x < coordinate.RectMetin2GameScreen().Width; x++)
                {
                    if (imageObjects.IsMatchBoolArrays(targetGreenFisher, coordinate.RectFisherSample(),
                        scannableGreenFisher, coordinate.RectMetin2GameScreen(), x, y))
                    {
                        DebugPfCnsl.println("Elapsed Time = " + watch.ElapsedMilliseconds);


                      /*  DebugDrawingHandle.SetStaticRectangle(new Rectangle (x + coordinate.RectMetin2GameScreen().X + CheckGameCoordinate.currentScreenGamePoint.X,
                            y + coordinate.RectMetin2GameScreen().Y + CheckGameCoordinate.currentScreenGamePoint.Y,
                           coordinate.RectFisherSample().Width, coordinate.RectFisherSample().Height));

                        Application.Run(new FullScreen());*/


                        int xClickPos = x + coordinate.RectMetin2GameScreen().X + (coordinate.RectFisherSample().Width / 2) + CheckGameCoordinate.currentScreenGamePoint.X;
                        int yClickPos = y +coordinate.RectMetin2GameScreen().Y + (coordinate.RectFisherSample().Height/2) + CheckGameCoordinate.currentScreenGamePoint.Y;
                        inputGame.MouseMoveAndPressLeft(xClickPos, yClickPos);

                        //Wait for activating  fisher options page
                        TimerGame.SleepRandom(700,1000);

                        int[] targetWhiteBalikciAraEkran = imageObjects.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectFisherOptionsPage()));

                        int[] sourceWhiteBalikciAraEkran = imageObjects.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                            imageObjects.arrayBalikciAraEkran);

                        if (imageObjects.CompareTwoArrayAdvanced(sourceWhiteBalikciAraEkran, targetWhiteBalikciAraEkran,
                            ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            inputGame.MouseMoveAndPressLeft(coordinate.RectFisherOptionsPage().X,
                                coordinate.RectFisherOptionsPage().Y);
                            //Wait for opening fisher shop page
                            TimerGame.SleepRandom(1400, 1660);
                          //  DebugPfCnsl.println("Elapsed 222  Time = " + watch.ElapsedMilliseconds);
                            while (!CheckFisherShopPage())
                            {
                                if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled)
                                {
                                    ThreadGlobals.DebugThreadGloablValues();
                                    return false;
                                }
                  
                                inputGame.MouseMoveAndPressLeft(coordinate.RectFisherOptionsPage().X,
                             coordinate.RectFisherOptionsPage().Y);
                                //Wait for opening fisher shop page
                                TimerGame.SleepRandom(1400, 1660);
                            }
                            return true;
                        }
                        else
                        {
                            inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_W);
                            TimerGame.SleepRandom(300, 500);
                            inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_W);

                           return CheckFisherIsThere();
                        }
                            
                    }
                }

                
            }
            DebugPfCnsl.println(" Couldn't find Elapsed Time = " + watch.ElapsedMilliseconds);



            return false;
        }

        private void WormsHandle()
        {
            TimerGame timeGame = new TimerGame();
           // Console.WriteLine("calisti wwww");
            if (CheckFisherShopPage())
            {
              //  Console.WriteLine("calisti 111");
                if (CombineAndCountWorms())
                {
                   // Console.WriteLine("calisti4232");
                    CloseFisherShopPage();
                   // Console.WriteLine("calisti");
                    if (charThings.InsertObjectToSkillSlots(imageObjects.arrayWorm200, coordinate.RectItemSlotSizeSample(),
                        InsertCountSetting.INSERT_COUNT_32
                        ))
                    {
                        GoToFishPlace();
                        listWorm200.Clear();
                        charThings.OpenCloseInventory(false);
                        
                    }
                    else
                    {
                        listWorm200.Clear();
                        charThings.OpenCloseInventory(false);
                    }
                   
                }
                else
                {
                    DebugPfCnsl.println("CombineAndCountWorms returned false ...");

                }
            }
            else
            {
                
                if(CheckFisherIsThere())
                {
                    WormsHandle();
                }
            }
        }

        private bool CombineAndCountWorms()
        {
            TimerGame timerCombine = new TimerGame();

            DebugPfCnsl.println("CombineAndCountWorms function is running");

            listWorm200.Clear();
                int worms200CharHave = charThings.CombineItemsTo200(imageObjects.arrayWorm200);
                DebugPfCnsl.println("worms200CharHave result = " + worms200CharHave);
                while (worms200CharHave < NEEDED_WORM200_COUNT)
                {
                if (timerCombine.CheckDelayTimeInSecond(500))
                {
                    if(worms200CharHave != -1)
                    {
                        if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled)
                        {
                            return false;
                        }
                        BuyFiftyWormAsNeeded(worms200CharHave);
                        listWorm200.Clear();
                        worms200CharHave = charThings.CombineItemsTo200(imageObjects.arrayWorm200);
                    }
                    else
                    {
                        worms200CharHave = charThings.CombineItemsTo200(imageObjects.arrayWorm200);
                    }
                    
                }
                else
                {
                    DebugPfCnsl.println("CombineAndCountWorms function time is elapsed");
                    return false;
                }
                }
            
            return true;
           
           
        }
        private void BuyFiftyWormAsNeeded(int wormsCharHave)
        {
            if(CheckFisherShopPage())
            {
                int neededWorms = NEEDED_WORM200_COUNT - wormsCharHave;

                for (int i = 0; i < neededWorms * 4; i++)
                {
                    inputGame.MouseMoveAndPressRight(coordinate.PointFisherShopFiftyWorm().X,
                       coordinate.PointFisherShopFiftyWorm().Y);

                    TimerGame.SleepRandom(500, 600);

                    if(imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayFullDialogInFisherShop,
                        screenShot.ImageArraySpecifiedArea(coordinate.RectYereAtmaAlgilama()),
                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                    {
                        DebugPfCnsl.println("There aren't any place to buy worm");
                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                        return;
                    }
                }
            }
            else
            {
                DebugPfCnsl.println("Fisher shop page is not open");
                return;
            }
           
        }

        private void BuyKampAtasiFromFisher()
        {
            DebugPfCnsl.println("BuyKampAtasiFromFisher is called");
            TimerGame timerBuyKamp = new TimerGame();
            if (CheckFisherShopPage())
            {
                
                //charThings.ClickWantedInventoryPage(InventoryPage.Page_1);
                
                while (charThings.CheckObjectInventory(imageObjects.arrayKampIcon, coordinate.RectItemSlotSizeSample(), InventoryPage.Page_1).Length <= 0 &&
                    charThings.CheckObjectInventory(imageObjects.arrayKampIcon, coordinate.RectItemSlotSizeSample(), InventoryPage.Page_2).Length <= 0)
                {
                    if (timerBuyKamp.CheckDelayTimeInSecond(6))
                    {
                        if (ThreadGlobals.isFishingStopped && ThreadGlobals.isCharKilled) return;

                        inputGame.MouseMoveAndPressRight(coordinate.PointFisherShopKampAtesi().X,
                            coordinate.PointFisherShopKampAtesi().Y);
                        TimerGame.SleepRandom(500, 800);
                    }
                    else
                    {
                        DebugPfCnsl.println("kamp atesi alinamadi");
                        return;
                    }
                }
               

               /* while (imageObjects.FindAllImagesOnScreen(imageObjects.arrayKampIcon,
                    coordinate.RectItemSlotSizeSample(), coordinate.RectInventoryPageArea()).Length < 1)
                {
                    
                    else
                    {
                        charThings.ClickWantedInventoryPage(InventoryPage.Page_2);
                        timerBuyKamp.SetStartedSecondTime();
                        if (timerBuyKamp.CheckDelayTimeInSecond(15))
                        {
                            if (ThreadGlobals.isStopped && ThreadGlobals.isCharKilled) return;

                            inputGame.MouseMoveAndPressRight(coordinate.PointFisherShopKampAtesi().X,
                                coordinate.PointFisherShopKampAtesi().Y);
                            TimerGame.SleepRandom(500, 800);
                        }
                    }
                    
                }*/
            }
            else
            {
                CheckFisherIsThere();
                BuyKampAtasiFromFisher();
            }
           
        }
        private void GrillFishingHandle()
        {
            TimerGame timeGame = new TimerGame();
            DebugPfCnsl.println("GrillFishingHandle func is called");

            if(CheckFisherShopPage() )
            {
                
                BuyKampAtasiFromFisher();
                if(!ThreadGlobals.isHepsiSelected)
                {
                    int fishTypes = 0;
                    int[][] fishResult = GetFishTypesForGrilling();

                    //DebugPfCnsl.println("fishResut length = " + fishResult.Length );
                    for (int i = 0; i < fishResult.Length; i++)
                    {
                        if (fishResult[i] != null)
                        {
                            fishTypes++;
                        }
                    }

                    if (fishTypes > 0)
                    {
                        Rectangle[][] fishCoordinatesPageOne = new Rectangle[fishTypes][];
                        Rectangle[][] fishCoordinatesPageTwo = new Rectangle[fishTypes][];

                        // DebugPfCnsl.println("fishResut length = " + fishCoordinatesPageOne.Length);

                        int rectCounter = 0;
                        for (int k = 0; k < fishResult.Length; k++)
                        {
                            if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled) return;
                            if (fishResult[k] != null)
                            {
                                fishCoordinatesPageOne[rectCounter] = charThings.CheckObjectInventory(fishResult[k],
                                    coordinate.RectItemSlotSizeSample(), InventoryPage.Page_1);
                                // DebugDrawingHandle.DrawWantedObjectToScreen(fishCoordinatesPageOne[rectCounter]);
                                fishCoordinatesPageTwo[rectCounter++] = charThings.CheckObjectInventory(fishResult[k],
                                   coordinate.RectItemSlotSizeSample(), InventoryPage.Page_2);
                                //DebugDrawingHandle.DrawWantedObjectToScreen(fishCoordinatesPageTwo[rectCounter]);
                            }
                        }

                        if (fishCoordinatesPageOne[0].Length == 0 &&
                            fishCoordinatesPageOne[1].Length == 0 &&
                            fishCoordinatesPageOne[2].Length == 0)
                        {
                            return;
                        }

                        Rectangle rectKampGreenResult = FireKampAtesi();
                        if (rectKampGreenResult != Rectangle.Empty)
                        {
                            while (!GrillFishes(fishCoordinatesPageOne, fishCoordinatesPageTwo, rectKampGreenResult))
                            {
                                if (CheckFisherIsThere())
                                {
                                    if (CheckFisherShopPage())
                                    {
                                        BuyKampAtasiFromFisher();
                                        rectKampGreenResult = FireKampAtesi();
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    Rectangle rectKampGreenResult = FireKampAtesi();
                    if (rectKampGreenResult != Rectangle.Empty)
                    {
                        while (!GrillFishes(null, null, rectKampGreenResult))
                        {
                            if (CheckFisherIsThere())
                            {
                                if (CheckFisherShopPage())
                                {
                                    BuyKampAtasiFromFisher();
                                    rectKampGreenResult = FireKampAtesi();
                                }
                            }
                        }
                    }
                }
               
                
            }
            else
            {
                DebugPfCnsl.println("GrillFishingHandle func CheckFisherShopPage else statement is running");
                if(CheckFisherIsThere())
                {
                    GrillFishingHandle();
                }
            }
        }
        private bool GrillFishes(Rectangle[][] rectPageOne , Rectangle[][] rectPageTwo,Rectangle kampAtesiGreen)
        {
            DebugPfCnsl.println("GrillFishes func is called");
            TimerGame timerGrillFishes = new TimerGame();

            if (!ThreadGlobals.isHepsiSelected)
            {
                if (rectPageOne.Length > 0)
                {
                    charThings.ClickWantedInventoryPage(InventoryPage.Page_1);
                    for (int pageOneLength = 0; pageOneLength < rectPageOne.Length; pageOneLength++)
                    {
                        for (int pageOneValue = 0; pageOneValue < rectPageOne[pageOneLength].Length; pageOneValue++)
                        {

                            Rectangle rectFish = rectPageOne[pageOneLength][pageOneValue];

                            int[] fishImageBeforeGrill = screenShot.ImageArraySpecifiedArea(rectFish);
                            int[] slotImageAfterGrill = screenShot.ImageArraySpecifiedArea(rectFish);

                            while (imageObjects.CompareTwoArrayAdvanced(fishImageBeforeGrill, slotImageAfterGrill
                                , ImageSensibilityLevel.SENSIBILTY_HIGH))
                            {
                                if (timerGrillFishes.CheckDelayTimeInSecond(60))
                                {
                                    if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled) return false;



                                    inputGame.MouseMoveAndPressLeft(rectFish.X + rectFish.Width / 2, rectFish.Y);
                                    inputGame.MouseMoveAndPressLeft(kampAtesiGreen.X + kampAtesiGreen.Width / 2,
                                        kampAtesiGreen.Y + kampAtesiGreen.Height / 2);

                                    TimerGame.SleepRandom(300, 400);

                                    if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayYereAtmaDialog,
                                        screenShot.ImageArraySpecifiedArea(coordinate.RectYereAtmaAlgilama()),
                                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                                    {
                                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                                        return false;
                                    }

                                    slotImageAfterGrill = screenShot.ImageArraySpecifiedArea(rectFish);
                                }
                                else
                                {
                                    DebugPfCnsl.println("GrillFishes func CheckDelayTimeSecond else statement started");
                                    return false;
                                }

                            }

                        }
                    }
                }
                if (rectPageTwo.Length > 0)
                {
                    charThings.ClickWantedInventoryPage(InventoryPage.Page_2);

                    for (int pageTwoLength = 0; pageTwoLength < rectPageTwo.Length; pageTwoLength++)
                    {
                        for (int pageTwoValue = 0; pageTwoValue < rectPageTwo[pageTwoLength].Length; pageTwoValue++)
                        {
                            if (timerGrillFishes.CheckDelayTimeInSecond(60))
                            {
                                Rectangle rectFish = rectPageTwo[pageTwoLength][pageTwoValue];

                                int[] fishImageBeforeGrill = screenShot.ImageArraySpecifiedArea(rectFish);
                                int[] slotImageAfterGrill = screenShot.ImageArraySpecifiedArea(rectFish);

                                while (imageObjects.CompareTwoArrayAdvanced(fishImageBeforeGrill, slotImageAfterGrill
                                    , ImageSensibilityLevel.SENSIBILTY_HIGH))
                                {

                                    if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled) return false;



                                    inputGame.MouseMoveAndPressLeft(rectFish.X + rectFish.Width / 2, rectFish.Y);
                                    inputGame.MouseMoveAndPressLeft(kampAtesiGreen.X + kampAtesiGreen.Width / 2,
                                        kampAtesiGreen.Y + kampAtesiGreen.Height / 2);

                                    TimerGame.SleepRandom(200, 400);

                                    if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayYereAtmaDialog,
                                        screenShot.ImageArraySpecifiedArea(coordinate.RectYereAtmaAlgilama()),
                                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                                    {
                                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                                        return false;
                                    }

                                    slotImageAfterGrill = screenShot.ImageArraySpecifiedArea(rectFish);
                                }
                            }
                            else
                            {
                                DebugPfCnsl.println("GrillFishes func CheckDelayTimeSecond else statement started");
                                return false;
                            }
                        }

                    }
                }
            }
            else
            {

                for (; pageGrillFisher <= 2; pageGrillFisher++)
                {
                    if(isGrillFishesFailed)
                    {
                        pageGrillFisher = 1;
                        xGrillFishes = 0;
                        yGrillFishes = 0;
                    }
                    if (pageGrillFisher == 1)
                    {
                        charThings.ClickWantedInventoryPage(InventoryPage.Page_1);
                    }
                    else
                    {
                        charThings.ClickWantedInventoryPage(InventoryPage.Page_2);
                    }

                    for (; yGrillFishes < 9; yGrillFishes++)
                    {
                        for (; xGrillFishes < 5; xGrillFishes++)
                        {
                            if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled) return false;

                            if (timerGrillFishes.CheckDelayTimeInSecond(60))
                            {
                                Rectangle rectScanSlot = new Rectangle(coordinate.RectFirstSlotPlace().X + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * xGrillFishes),
                               coordinate.RectFirstSlotPlace().Y + (GameObjectCoordinates.DISTANCE_BTWN_INV_SLOTS * yGrillFishes),
                               coordinate.RectFirstSlotPlace().Width, coordinate.RectFirstSlotPlace().Height);

                                int[] targetSlotImage = screenShot.ImageArraySpecifiedArea(rectScanSlot);

                                if (!imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayEmptySlotPlace, targetSlotImage,
                                    ImageSensibilityLevel.SENSIBILTY_HIGH))
                                {
                                    inputGame.MouseMoveAndPressLeft(rectScanSlot.X + rectScanSlot.Width / 2,
                                            rectScanSlot.Y);
                                    inputGame.MouseMoveAndPressLeft(kampAtesiGreen.X + kampAtesiGreen.Width / 2,
                                            kampAtesiGreen.Y + kampAtesiGreen.Height / 2);

                                    TimerGame.SleepRandom(200, 400);

                                    if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayYereAtmaDialog,
                                        screenShot.ImageArraySpecifiedArea(coordinate.RectYereAtmaAlgilama()),
                                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                                    {
                                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                isGrillFishesFailed = true;
                                return false;
                            }

                           
                        }
                        xGrillFishes = 0;
                    }
                    yGrillFishes = 0;
                    
                }
            }

            isGrillFishesFailed = false;
            pageGrillFisher = 1;
            xGrillFishes = 0;
            yGrillFishes =0;    

                return true;
                        
        }
        
        private Rectangle FireKampAtesi()
        {
            CloseFisherShopPage();
            TimerGame timerFireKamp = new TimerGame();

            Rectangle[] kampAtesiIconInvent = charThings.CheckObjectInventory(imageObjects.arrayKampIcon,
                coordinate.RectItemSlotSizeSample(),InventoryPage.Page_1);
            if(kampAtesiIconInvent.Length <= 0)
            {
                kampAtesiIconInvent = charThings.CheckObjectInventory(imageObjects.arrayKampIcon,
                coordinate.RectItemSlotSizeSample(), InventoryPage.Page_2);
            }

            if(kampAtesiIconInvent != null && kampAtesiIconInvent.Length > 0)
            {
                //DebugDrawingHandle.DrawWantedObjectToScreen(kampAtesiIconInvent);

                bool[] sourceKampAtesiGreen = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_CAMP_FIRE_GREEN,
                    imageObjects.arrayKampAtesiWords);

                while (imageObjects.FindAllImagesBoolArrays(sourceKampAtesiGreen, coordinate.RectKampGreenSample(),
                               coordinate.RectKampAtesiAsagiTarafKoordinat(), ColorGame.MAP_CAMP_FIRE_GREEN).Length <= 0)
                {
                    if (timerFireKamp.CheckDelayTimeInSecond(20))
                    {
                        if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled) { return Rectangle.Empty; }

                        inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_S);
                        TimerGame.SleepRandom(400, 500);
                        inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_S);

                        inputGame.MouseMoveAndPressRight(kampAtesiIconInvent[0].X + kampAtesiIconInvent[0].Width / 2,
                            kampAtesiIconInvent[0].Y);

                        TimerGame.SleepRandom(300, 400);
                    }
                    else
                    {
                        DebugPfCnsl.println("FireKampAtesi checkDelayTimeSecond else statement started");
                        return Rectangle.Empty;
                    }
                }

                return imageObjects.FindAllImagesBoolArrays(sourceKampAtesiGreen, coordinate.RectKampGreenSample(),
                    coordinate.RectKampAtesiAsagiTarafKoordinat(), ColorGame.MAP_CAMP_FIRE_GREEN)[0];
            }
            else
            {
                DebugPfCnsl.println("kamp ateşi bulunamadi");
            return Rectangle.Empty; 
            }

          
        }
        private int[][] GetFishTypesForGrilling()
        {
            int[][] fishTypes = new int[5][];

            if(ThreadGlobals.isYabbieSelected) 
            {
                fishTypes[0] = imageObjects.arrayYabbieIcon;
            }
            if(ThreadGlobals.isAltinSudakSelected)
            {
                fishTypes[1] = imageObjects.arrayAltinSudakIcon;
            }
            if(ThreadGlobals.isPalamutSelected)
            {
                fishTypes[2] = imageObjects.arrayPalamutIcon;
            }
            if(ThreadGlobals.isKurbagaSelected)
            {
                fishTypes[3] = imageObjects.arrayKurbagaIcon;
            }
            if (ThreadGlobals.isKadifeSelected)
            {
                fishTypes[4] = imageObjects.arrayKadifeIcon;
            }
            return fishTypes;
        }
        public void GoToFishPlace()
        {
            int zigZagWalking = 0;
            int walkSide = 1;

            for (int k=0; k < 10 ; k++) 
            {
                if (ThreadGlobals.isFishingStopped) return;

                Rectangle walkingRect = imageObjects.FindBorderAreaBetweenColors(
                       coordinate.RectReverseWoodDetectionArea(),
                       ColorGame.MIN_WOOD_VALUE, ColorGame.MAX_WOOD_VALUE);

                int xPos, yPos;

                if (walkingRect != Rectangle.Empty)
                {
                    zigZagWalking = TimerGame.MakeRandomValue(0,
                        120);



                    if (walkSide == 1)
                    {
                        xPos = walkingRect.X + (walkingRect.Width / 2) + zigZagWalking;
                        yPos = walkingRect.Y;
                        walkSide = 2;
                    }
                    else
                    {
                        xPos = walkingRect.X + (walkingRect.Width / 2) - zigZagWalking;
                        yPos = walkingRect.Y;
                        walkSide = 1;
                    }

                    inputGame.MouseMoveAndPressLeft(xPos, yPos);
                   
                }
                else
                {
                    xPos = coordinate.RectReverseWoodDetectionArea().X + (coordinate.RectReverseWoodDetectionArea().Width / 2);
                    yPos = coordinate.RectReverseWoodDetectionArea().Y;

                    inputGame.MouseMoveAndPressLeft(xPos, yPos);
                    
                   
                }
                TimerGame.SleepRandom(50, 100);
            }
        }
       

        public  bool CheckFisherShopPage()
        {
            if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayFisherShopPage,
               screenShot.ImageArraySpecifiedArea(coordinate.RectFisherShopPage()),
               ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseFisherShopPage()
        {
            TimerGame timerCloseFisher = new TimerGame();

            
                while (CheckFisherShopPage())
                {
                if (timerCloseFisher.CheckDelayTimeInSecond(20))
                {
                    if (ThreadGlobals.isFishingStopped || ThreadGlobals.isCharKilled || !ThreadGlobals.isSettingButtonSeemed)
                    {
                        return;
                    }
                    DebugPfCnsl.println("closing shope page");
                    inputGame.MouseMoveAndPressLeft(coordinate.PointFisherShopCloseButton().X,
                        coordinate.PointFisherShopCloseButton().Y);
                    TimerGame.SleepRandom(400, 600);
                }
                else
                {
                    DebugPfCnsl.println("CloseFisherShopPage CheckDelaytimeSecond else statemet started");
                    return;
                }
            }
            
            
        }
    }
}
