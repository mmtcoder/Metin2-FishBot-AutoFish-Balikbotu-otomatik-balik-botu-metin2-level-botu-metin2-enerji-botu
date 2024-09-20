using Metin2AutoFishCSharp.Sources.CharacterHandle;
using Metin2AutoFishCSharp.Sources.ChatHandler;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.CoordinatesHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.GameHandler
{
    internal class EnerjyCristalHandle
    {
        private ImageObjects imageObjects;
        private GameAlphabetDetecter gameAlphabet;
        private CharMovement charMove;
        private GameObjectCoordinates coordinate;
        private ScreenShotWinAPI screenShot;
        private GameInputHandler inputGame;
        private CharSpecialThings charThings;

        TimerGame timerCheckSilahci;

        private readonly int MINIMUM_EMPTY_PLACE = 15;

        public Point[] silahciToSimyaci;
        //597,559 - 612,556 - 610,530 - 623,522 - 623,514
        public Point[] simyaciToSilahci;
        public EnerjyCristalHandle(ImageObjects imageObjects, GameAlphabetDetecter gameAlphabet)
        {
            this.imageObjects = imageObjects;
            this.gameAlphabet = gameAlphabet;
            charMove = new CharMovement(imageObjects, gameAlphabet);

            silahciToSimyaci = new Point[] {new Point(600,561),new Point(612,556),
            new Point(610,530),new Point(623,522), new Point(623,514)};

            simyaciToSilahci = new Point[] { new Point(623, 514) , new Point(623, 522)
            , new Point(610,530),new Point(612,556),new Point(600,561)};

            coordinate = new GameObjectCoordinates(imageObjects);
            screenShot = new ScreenShotWinAPI();
            inputGame = new GameInputHandler();
            charThings = new CharSpecialThings(imageObjects);

            timerCheckSilahci = new TimerGame();
        }

        public void StartEnergyCristal()
        {

            while (ThreadGlobals.CanEnergyCristalRightNow())
            {

                // charMove.StartTravelling(silahciToSimyaci);
                charThings.ProvideCharNameCanSee();
                charMove.StartTravelling(simyaciToSilahci);
                inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_W);
                if (CheckSilahciIsThere())
                {
                    BuyKediIsirigi();
                    charMove.StartTravelling(silahciToSimyaci);

                    Rectangle findedSimyaci = FindSimyaciCoordinates();
                    TimerGame timerFindSimyaci = new TimerGame();

                    while (findedSimyaci == Rectangle.Empty)
                    {
                        if(timerFindSimyaci.CheckDelayTimeInSecond(15))
                        {
                            if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                            {
                                return;
                            }
                            inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                            findedSimyaci = FindSimyaciCoordinates();
                        }
                        else
                        {
                            charThings.OpenCloseInventory(true);
                            charThings.OpenCloseInventory(false);
                            charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);

                            while (!ThreadGlobals.CanEnergyCristalRightNow())
                            {
                                if (ThreadGlobals.CheckGameIsStopped())
                                {
                                    DebugPfCnsl.println("Find simyaci is returned");
                                    return;
                                }
                            }
                            TimerGame.SleepRandom(3000, 4000);
                            break;
                        }
                       
                    }

                    DragKediIsirigi(findedSimyaci);
                }
                else
                {
                    if (!timerCheckSilahci.CheckDelayTimeInSecond(15))
                    {

                        charThings.OpenCloseInventory(true);
                        charThings.OpenCloseInventory(false);
                        charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);

                        while (!ThreadGlobals.CanEnergyCristalRightNow())
                        {
                            if (ThreadGlobals.CheckGameIsStopped())
                            {
                                DebugPfCnsl.println("StartTravelling is returned");
                                return;
                            }
                        }
                        TimerGame.SleepRandom(3000, 4000);

                        timerCheckSilahci.SetStartedSecondTime();
                    }
                }

            }
        }

        private Rectangle FindSimyaciCoordinates()
        {
            charThings.OpenCloseInventory(true);
            charThings.OpenCloseInventory(false);
            charThings.OpenCloseSettingButton(false);

            bool[] targetGreenSimyaci = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN,
               imageObjects.arraySimyaciGreenTitle);
            //  Console.WriteLine("targetGreenFisher length = " + targetGreenFisher.Length);
            bool[] scannableGreenSimyaci = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN,
                screenShot.ImageArraySpecifiedArea(coordinate.RectMetin2GameScreen()));

            for (int y = 0; y < coordinate.RectMetin2GameScreen().Height; y++)
            {
                for (int x = 0; x < coordinate.RectMetin2GameScreen().Width; x++)
                {
                    if (imageObjects.IsMatchBoolArrays(targetGreenSimyaci, coordinate.RectSimyaciTitleSample(),
                        scannableGreenSimyaci, coordinate.RectMetin2GameScreen(), x, y))
                    {
                        return new Rectangle(x + coordinate.RectMetin2GameScreen().X,
                            y + coordinate.RectMetin2GameScreen().Y, coordinate.RectSimyaciTitleSample().Width,
                            coordinate.RectSimyaciTitleSample().Height);
                    }
                }
            }

            return Rectangle.Empty;
        }

        private void DragKediIsirigi(Rectangle simyaciPlace)
        {
            if (simyaciPlace != Rectangle.Empty)
            {
                Rectangle[] firstPageKnives = charThings.CheckObjectInventory(imageObjects.arrayKediIsirigi,
                    coordinate.RectKediIsirigiSample(), InventoryPage.Page_1);
                Rectangle[] secondPageKnives = charThings.CheckObjectInventory(imageObjects.arrayKediIsirigi,
                    coordinate.RectKediIsirigiSample(), InventoryPage.Page_2);

                TimerGame timerSimyaciProcess = new TimerGame();

                if (firstPageKnives.Length > 0)
                {
                    for (int pageOne = 0; pageOne < firstPageKnives.Length; pageOne++)
                    {
                        if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                        {
                            DebugPfCnsl.println("DragKediIsirigi is returned");
                            return;

                        }
                        charThings.ClickWantedInventoryPage(InventoryPage.Page_1);
                        TimerGame.SleepRandom(30, 40);

                        inputGame.MouseMoveAndPressLeft(firstPageKnives[pageOne].X + firstPageKnives[pageOne].Width / 2,
                            firstPageKnives[pageOne].Y + firstPageKnives[pageOne].Height / 2);

                        if(TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(50, 70);
                        }
                        else
                        {
                            TimerGame.SleepRandom(30, 45);
                        }
                        
                        inputGame.MouseMoveAndPressLeft(simyaciPlace.X + simyaciPlace.Width / 2,
                            simyaciPlace.Y + simyaciPlace.Height + 5);

                        if(TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(400, 450);
                        }
                        else
                        {
                            TimerGame.SleepRandom(200, 300);
                        }
                        

                        bool[] sourceSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            imageObjects.arraySimyaciEvetOptionPage);

                        bool[] targetSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSimyaciEvetButton()));

                        TimerGame timerSettingButtonDetect = new TimerGame();
                        //Çift kontrol yapıyoruz. Burada setting buton tespit edilemez ise simyacı ekranı
                        //çıkmış demektir
                        while(imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySettingButton,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSettingButton()),ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            if(timerSettingButtonDetect.CheckDelayTimeInSecond(3))
                            {
                                if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                                {
                                    DebugPfCnsl.println("DragKediIsirigi is returned");
                                    return;
                                }
                            }
                            else
                            {
                                break;
                            }
                            
                        }
                        //Burada ise evet hayır simyacı ekranı gelip gelmediğini kontrol ediyoruz
                        while (imageObjects.compareTwoArrayQuicklyBool(sourceSimyaciEvetWhite, targetSimyaciEvetWhite))
                        {
                            if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                            {
                                DebugPfCnsl.println("DragKediIsirigi is returned");
                                return;
                            }
                            inputGame.KeyPress(KeyboardInput.ScanCodeShort.ENTER);

                            if(TimerGame.IS_PC_SLOW)
                            {
                                TimerGame.SleepRandom(400, 500);
                            }
                            else
                            {
                                TimerGame.SleepRandom(200, 300);
                            }
                            
                            targetSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSimyaciEvetButton()));
                        }
                        //Sürükleme yaparken karakterin yapamamış ve yönü kaymış olmalı
                        //bu yüzden tekrar simyacıya gidip onu bulması lazım
                        //Burada eğer kedi ısırığı bıçağı envanterden eksilmemiş ise sürükleme başarısız
                        //anlamına gelir ve diğer işlemler yapılır
                        if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayKediIsirigi,
                            screenShot.ImageArraySpecifiedArea(firstPageKnives[pageOne]),
                            ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                            charMove.StartTravelling(silahciToSimyaci);

                            TimerGame timeCheckSimyaci = new TimerGame();
                            Rectangle findSimyaci = FindSimyaciCoordinates();
                            while (findSimyaci == Rectangle.Empty)
                            {
                                if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed || 
                                    ThreadGlobals.isCharKilled)
                                {
                                    DebugPfCnsl.println("DragKediIsirigi is returned");
                                    return;
                                }
                                if (timeCheckSimyaci.CheckDelayTimeInSecond(10))
                                {
                                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                                    findSimyaci = FindSimyaciCoordinates();
                                }
                                else
                                {
                                    break;
                                }
                               
                            }

                            DragKediIsirigi(findSimyaci);
                            return;
                        }
                        if (TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(400, 500);
                        }
                        else
                        {
                            TimerGame.SleepRandom(200, 300);
                        }
                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);

                    }


                }

                if (secondPageKnives.Length > 0)
                {
                    for (int pageTwo = 0; pageTwo < secondPageKnives.Length; pageTwo++)
                    {
                        if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                        {
                            DebugPfCnsl.println("DragKediIsirigi is returned");
                            return;

                        }
                        charThings.ClickWantedInventoryPage(InventoryPage.Page_2);
                        TimerGame.SleepRandom(30, 40);

                        inputGame.MouseMoveAndPressLeft(secondPageKnives[pageTwo].X + secondPageKnives[pageTwo].Width / 2,
                            secondPageKnives[pageTwo].Y + secondPageKnives[pageTwo].Height / 2);

                        if (TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(50, 70);
                        }
                        else
                        {
                            TimerGame.SleepRandom(30, 45);
                        }

                        inputGame.MouseMoveAndPressLeft(simyaciPlace.X + simyaciPlace.Width / 2,
                            simyaciPlace.Y + simyaciPlace.Height + 5);

                        if (TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(400, 450);
                        }
                        else
                        {
                            TimerGame.SleepRandom(200, 300);
                        }

                        bool[] sourceSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            imageObjects.arraySimyaciEvetOptionPage);

                        bool[] targetSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSimyaciEvetButton()));

                        TimerGame timerSettingButtonDetect = new TimerGame();
                        //Çift kontrol yapıyoruz. Burada setting buton tespit edilemez ise simyacı ekranı
                        //çıkmış demektir
                        while (imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySettingButton,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSettingButton()), ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            if (timerSettingButtonDetect.CheckDelayTimeInSecond(3))
                            {
                                if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                                {
                                    DebugPfCnsl.println("DragKediIsirigi is returned");
                                    return;
                                }
                            }
                            else
                            {
                                break;
                            }

                        }
                        //Burada ise evet hayır simyacı ekranı gelip gelmediğini kontrol ediyoruz
                        while (imageObjects.compareTwoArrayQuicklyBool(sourceSimyaciEvetWhite, targetSimyaciEvetWhite))
                        {
                            if (ThreadGlobals.CheckGameIsStopped() || ThreadGlobals.isCharKilled)
                            {
                                DebugPfCnsl.println("DragKediIsirigi is returned");
                                return;
                            }
                            inputGame.KeyPress(KeyboardInput.ScanCodeShort.ENTER);

                            if (TimerGame.IS_PC_SLOW)
                            {
                                TimerGame.SleepRandom(400, 500);
                            }
                            else
                            {
                                TimerGame.SleepRandom(200, 300);
                            }

                            targetSimyaciEvetWhite = imageObjects.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSimyaciEvetButton()));
                        }
                        //Sürükleme yaparken karakterin yapamamış ve yönü kaymış olmalı
                        //bu yüzden tekrar simyacıya gidip onu bulması lazım
                        if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayKediIsirigi,
                            screenShot.ImageArraySpecifiedArea(secondPageKnives[pageTwo]),
                            ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);

                            charMove.StartTravelling(silahciToSimyaci);

                            TimerGame timeCheckSimyaci = new TimerGame();
                            Rectangle findSimyaci = FindSimyaciCoordinates();
                            while (findSimyaci == Rectangle.Empty)
                            {
                                if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed
                                    || ThreadGlobals.isCharKilled)
                                {
                                    DebugPfCnsl.println("DragKediIsirigi is returned");
                                    return;
                                }
                                if (timeCheckSimyaci.CheckDelayTimeInSecond(10))
                                {
                                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                                    findSimyaci = FindSimyaciCoordinates();
                                }
                                else
                                {
                                    break;
                                }

                            }

                            DragKediIsirigi(findSimyaci);
                            return;
                        }
                        
                        if(TimerGame.IS_PC_SLOW)
                        {
                            TimerGame.SleepRandom(400, 500);
                        }
                        else
                        {
                            TimerGame.SleepRandom(200, 300);
                        }
                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);

                    }


                }
                inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_TURKISH_I);
                charThings.OpenCloseInventory(false);
                charThings.OpenCloseSettingButton(false);
            }
            
        }
        private bool CheckSilahciIsThere()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            bool[] targetGreenSilahci = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN,
                imageObjects.arraySilahciGreenTitle);
            //  Console.WriteLine("targetGreenFisher length = " + targetGreenFisher.Length);
            bool[] scannableGreenSilahci = imageObjects.RecordWantedColorAsBool(ColorGame.MAP_BALIKCI_GREEN,
                screenShot.ImageArraySpecifiedArea(coordinate.RectMetin2GameScreen()));

            // Console.WriteLine("RectMetin2GameScreen length = " +
            //    (coordinate.RectMetin2GameScreen().Width * coordinate.RectMetin2GameScreen().Height));

            for (int y = 0; y < coordinate.RectMetin2GameScreen().Height; y++)
            {
                for (int x = 0; x < coordinate.RectMetin2GameScreen().Width; x++)
                {
                    if (imageObjects.IsMatchBoolArrays(targetGreenSilahci, coordinate.RectSilahciSample(),
                        scannableGreenSilahci, coordinate.RectMetin2GameScreen(), x, y))
                    {
                        DebugPfCnsl.println("Elapsed Time = " + watch.ElapsedMilliseconds);


                        /*  DebugDrawingHandle.SetStaticRectangle(new Rectangle (x + coordinate.RectMetin2GameScreen().X + CheckGameCoordinate.currentScreenGamePoint.X,
                              y + coordinate.RectMetin2GameScreen().Y + CheckGameCoordinate.currentScreenGamePoint.Y,
                             coordinate.RectFisherSample().Width, coordinate.RectFisherSample().Height));

                          Application.Run(new FullScreen());*/


                        int xClickPos = x + coordinate.RectMetin2GameScreen().X + (coordinate.RectSilahciSample().Width / 2) + CheckGameCoordinate.currentScreenGamePoint.X;
                        int yClickPos = y + coordinate.RectMetin2GameScreen().Y + (coordinate.RectSilahciSample().Height / 2) + CheckGameCoordinate.currentScreenGamePoint.Y;
                        inputGame.MouseMoveAndPressLeft(xClickPos, yClickPos);

                        //Wait for activating  fisher options page
                        TimerGame.SleepRandom(700, 1000);

                        int[] targetWhiteSilahciAraEkran = imageObjects.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                            screenShot.ImageArraySpecifiedArea(coordinate.RectSilahciOptionMarketiAc()));

                        int[] sourceWhiteSilahciAraEkran = imageObjects.RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                            imageObjects.arraySilahciOptionPage);

                        if (imageObjects.CompareTwoArrayAdvanced(sourceWhiteSilahciAraEkran, targetWhiteSilahciAraEkran,
                            ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            inputGame.MouseMoveAndPressLeft(coordinate.RectSilahciOptionMarketiAc().X,
                                coordinate.RectSilahciOptionMarketiAc().Y);
                            //Wait for opening fisher shop page
                            TimerGame.SleepRandom(1400, 1660);
                            //  DebugPfCnsl.println("Elapsed 222  Time = " + watch.ElapsedMilliseconds);
                            while (!CheckSilahciShopPage())
                            {
                                if (ThreadGlobals.isEnergyCristalStopped || !ThreadGlobals.isSettingButtonSeemed
                                    || ThreadGlobals.isCharKilled)
                                {
                                    ThreadGlobals.DebugThreadGloablValues();
                                    return false;
                                }

                                inputGame.MouseMoveAndPressLeft(coordinate.RectSilahciOptionMarketiAc().X,
                             coordinate.RectSilahciOptionMarketiAc().Y);
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

                            return false;
                        }

                    }
                }


            }
            inputGame.KeyPress(KeyboardInput.ScanCodeShort.KEY_S);
            DebugPfCnsl.println(" Couldn't find Elapsed Time = " + watch.ElapsedMilliseconds);



            return false;
        }

        public void BuyKediIsirigi()
        {
            if (CheckSilahciShopPage())
            {
                Rectangle[] emptySpace = charThings.CheckObjectTwoPageInventory(imageObjects.arrayEmptySlotPlace,
                    coordinate.RectItemSlotSizeSample());

                Rectangle[] rectKediIsirigi = charThings.CheckObjectTwoPageInventory(imageObjects.arrayKediIsirigi,
                    coordinate.RectKediIsirigiSample());

                TimerGame timerSilahBuyingProc = new TimerGame();

                if (emptySpace != null)
                {

                    int countResult = 0;

                    if (rectKediIsirigi != null)
                    {
                        countResult = rectKediIsirigi.Length - 1;
                    }

                    countResult = emptySpace.Length - 1 + countResult;

                    //-1 rectangle 2 sayfanin değeri ayırmak için konmuştu
                    if (countResult > MINIMUM_EMPTY_PLACE)
                    {
                        while (!imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayFullDialogInFisherShop,
                        screenShot.ImageArraySpecifiedArea(coordinate.RectYereAtmaAlgilama()),
                        ImageSensibilityLevel.SENSIBILTY_HIGH))
                        {
                            if(timerSilahBuyingProc.CheckDelayTimeInSecond(250))
                            {
                                if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed
                                    || ThreadGlobals.isCharKilled)
                                {
                                    DebugPfCnsl.println("BuyKediIsirigi function is returned");
                                    return;
                                }
                                inputGame.MouseMoveAndPressRight(coordinate.PointKediIsirigi().X,
                                    coordinate.PointKediIsirigi().Y);

                                TimerGame.SleepRandom(500, 600);

                                if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arrayYangYokDilogIcon,
                                    screenShot.ImageArraySpecifiedArea(coordinate.RectSilahciYangYokDialog()),
                                    ImageSensibilityLevel.SENSIBILTY_HIGH))
                                {
                                    //Hesapta para yok normalde bana bildirilmesi gerek
                                    DebugPfCnsl.println("Char doesn't have yang to buy it!!");
                                    TelegramBot.SendMessageTelegram("Karakterde para yok paraaaa");

                                    CloseSilahciShopPage();
                                    charThings.OpenCloseInventory(true);
                                    charThings.OpenCloseInventory(false);
                                                                    
                                    charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);
                                    ThreadGlobals.isPausedTheGame = true;
                                    return;
                                }
                            }
                            else
                            {
                                CloseSilahciShopPage();
                                charThings.OpenCloseInventory(true);
                                charThings.OpenCloseInventory(false);                              
                                charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);

                                while(!ThreadGlobals.CanEnergyCristalRightNow())
                                {
                                    if (ThreadGlobals.CheckGameIsStopped())
                                    {
                                        DebugPfCnsl.println("BuyKediIsirigi function is returned");
                                        return;
                                    }
                                }
                                TimerGame.SleepRandom(2500, 3000);
                                BuyKediIsirigi();
                                return;
                            }
                           
                        }

                        inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                        TimerGame.SleepRandom(500, 700);

                        charThings.OpenCloseInventory(false);
                        CloseSilahciShopPage();
                    }
                    else
                    {
                        Rectangle[] rectCheckKnivesPageOne = charThings.CheckObjectInventory(imageObjects.arrayKediIsirigi,
                            coordinate.RectKediIsirigiSample(), InventoryPage.Page_1);

                        Rectangle[] rectCheckKnivesPageTwo = charThings.CheckObjectInventory(imageObjects.arrayKediIsirigi,
                        coordinate.RectKediIsirigiSample(), InventoryPage.Page_2);

                        int result = 0;
                        if (rectCheckKnivesPageOne != null)
                        {
                            DebugPfCnsl.println("rectCheckKnivesPageOne length = " + rectCheckKnivesPageOne.Length);
                            result = rectCheckKnivesPageOne.Length;
                        }
                        if (rectCheckKnivesPageTwo != null)
                        {
                            result += rectCheckKnivesPageTwo.Length;
                        }

                        if (result > 15)
                        {
                            DebugPfCnsl.println("envanterdeki olanlar götürülüyor");
                            CloseSilahciShopPage();
                            charThings.OpenCloseInventory(false);
                            return;
                        }

                        DebugPfCnsl.println("You need to sell them !!");
                        TelegramBot.SendMessageTelegram("Karakterde envanter full dolu boşalt köle");

                        charThings.OpenCloseInventory(true);
                        charThings.OpenCloseInventory(false);
                        CloseSilahciShopPage();
                        ThreadGlobals.isPausedTheGame = true;
                        charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);
                        return;
                    }
                }
            }
            else
            {
                DebugPfCnsl.println("silahcı sayfası açık değil !! ");
            }
        }
        public bool CheckSilahciShopPage()
        {
            if (imageObjects.CompareTwoArrayAdvanced(imageObjects.arraySilahciShopTitle,
               screenShot.ImageArraySpecifiedArea(coordinate.RectSilahciShopPage()),
               ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CloseSilahciShopPage()
        {
            TimerGame timerCloseFisher = new TimerGame();


            while (CheckSilahciShopPage())
            {
                if (timerCloseFisher.CheckDelayTimeInSecond(20))
                {
                    if (ThreadGlobals.isEnergyCristalStopped || ThreadGlobals.isCharKilled || !ThreadGlobals.isSettingButtonSeemed)
                    {
                        return;
                    }
                    DebugPfCnsl.println("closing shope page");
                    inputGame.MouseMoveAndPressLeft(coordinate.PointCloseSilahciShopPage().X,
                        coordinate.PointCloseSilahciShopPage().Y);
                    TimerGame.SleepRandom(400, 600);
                }
                else
                {
                    DebugPfCnsl.println("CloseSilahciShopPage CheckDelaytimeSecond else statemet started");
                    return;
                }
            }


        }
    }
}
