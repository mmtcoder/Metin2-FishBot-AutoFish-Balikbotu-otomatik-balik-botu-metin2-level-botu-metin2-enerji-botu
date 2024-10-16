using MusicPlayerApp.Sources.CoordinatesHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources
{
    internal class GameObjectCoordinates : CheckGameCoordinate
    {
        public static readonly int DISTANCE_BTWN_INV_SLOTS = 32;

        public static readonly int DISTANCE_BETW_CHAT_HEIGHT = 15;
        public GameObjectCoordinates(ImageObjects imageObjects) : base(imageObjects)
        {
            
        }

        public Rectangle RectFishClickArea()
        {
            //return new Rectangle(181, 154, 124, 124);
            //return new Rectangle(186, 160, 113, 113);
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(187 + currentMetin2Icon.X, 159 + currentMetin2Icon.Y, 123, 123);
        }

        public Rectangle RectChatArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(117 + currentMetin2Icon.X, 581 + currentMetin2Icon.Y, 40, 11);
        }

        public Rectangle RectUpChatArea()
        {
            return new Rectangle(RectChatArea().X, RectChatArea().Y - DISTANCE_BETW_CHAT_HEIGHT,
                RectChatArea().Width, RectChatArea().Height);
        }

        public Rectangle RectOneLineFullChatArea()
        {
            return new Rectangle(RectChatArea().X, RectChatArea().Y, 500, RectChatArea().Height);
        }
        public Rectangle RectExitChatArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(130 + currentMetin2Icon.X, 578 + currentMetin2Icon.Y, 30, 11);
        }

        public Rectangle RectFishTitle()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(222 + currentMetin2Icon.X, 89 + currentMetin2Icon.Y, 10, 2);
        }

        public Rectangle RectEntryScreen()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(246 + currentMetin2Icon.X, 122 + currentMetin2Icon.Y, 10, 2);
        }//246 122 10 2

        public Rectangle RectCharScreen()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(368 + currentMetin2Icon.X, 576 + currentMetin2Icon.Y, 17, 7);
        }

        public Rectangle RectCharButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            //return new Rectangle(362 +currentMetin2Icon.X, 365 + currentMetin2Icon.Y, 20, 11);
            return new Rectangle(364 + currentMetin2Icon.X, 350 + currentMetin2Icon.Y, 11, 8);
        }

        public Rectangle RectDieScreen()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(105 + currentMetin2Icon.X, 104 + currentMetin2Icon.Y, 25, 11);
        }

        public Rectangle RectSaleCross()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(206 + currentMetin2Icon.X, 444 + currentMetin2Icon.Y, 15, 11);
        }

        public Rectangle RectSettingButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(793 + currentMetin2Icon.X, 605 + currentMetin2Icon.Y, 10, 10);
        }

        public Rectangle RectSkillSlotFirstPlace()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(322 + currentMetin2Icon.X, 609 + currentMetin2Icon.Y, 32, 16);
        }

        public Rectangle RectSkillSlotSecondPlace()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(464 + currentMetin2Icon.X, 609 + currentMetin2Icon.Y, 32, 16);
        }

        public Rectangle RectPageNumCoordinates()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(595 + currentMetin2Icon.X, 608 + currentMetin2Icon.Y, 9, 7);
        }

        public Rectangle RectMetin2GameScreen()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(12 + currentMetin2Icon.X, 28 + currentMetin2Icon.Y, 800, 598);
        }

        public Rectangle RectMiniMapSymbolCoor()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(667 + currentMetin2Icon.X, 92 + currentMetin2Icon.Y, 14, 10);
        }

        public Rectangle RectMiniMapArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(688 + currentMetin2Icon.X, 38 + currentMetin2Icon.Y, 117, 117);
            //return new Rectangle(710 + currentMetin2Icon.X, 65 + currentMetin2Icon.Y, 60, 60);
            
        }

        public Rectangle RectWoodDetectinonArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(22 + currentMetin2Icon.X, 434 + currentMetin2Icon.Y, 800, 2);
        }

        public Rectangle RectReverseWoodDetectionArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(70 + currentMetin2Icon.X, 187 + currentMetin2Icon.Y, 800, 2);
        }

        public Rectangle RectFisherSample()
        {
            //We need just widht and height in the saved image
            return new Rectangle(0, 0, 27, 7);
        }

        public Rectangle RectSilahciSample()
        {
            return new Rectangle(0, 0, 52, 7);
        }

        public Rectangle RectSimyaciTitleSample()
        {
            return new Rectangle(0, 0, 32, 8);
        }
        public Rectangle RectFisherOptionsPage()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(388+ currentMetin2Icon.X, 244 + currentMetin2Icon.Y, 20, 11);
        }

        public Rectangle RectKampAtesiAsagiTarafKoordinat()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(380 + currentMetin2Icon.X, 330 + currentMetin2Icon.Y, 64, 60);
        }

        public Rectangle RectYereAtmaAlgilama()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(448 + currentMetin2Icon.X, 289 + currentMetin2Icon.Y, 48, 16);
        }

        public Rectangle RectChatIsActive()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(123 + currentMetin2Icon.X, 573 + currentMetin2Icon.Y, 29, 10);
        }

        public Rectangle RectCharNameArea() 
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(350 + currentMetin2Icon.X, 312 + currentMetin2Icon.Y, 125, 15); 
        }

        public Rectangle RectCharLevelDetectionArea()
        {

            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(325 + currentMetin2Icon.X, 313 + currentMetin2Icon.Y, 88, 14);
        }
        public Rectangle RectInventIsFullPanel()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(356 + currentMetin2Icon.X, 313+ currentMetin2Icon.Y, 9, 3);
        }
        public Rectangle RectInventory()
        { 
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(697 + currentMetin2Icon.X, 229 + currentMetin2Icon.Y, 25, 11);
        }
        public Rectangle RectFirstSlotPlace()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(640 + currentMetin2Icon.X, 286 + currentMetin2Icon.Y, 32, 16);
        }
        public Rectangle RectFirstSlotUpperPlace()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(650 + currentMetin2Icon.X, 284 + currentMetin2Icon.Y, 16, 9);
        }
        public Rectangle RectStatusTitle() 
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(161 + currentMetin2Icon.X, 144 + currentMetin2Icon.Y, 18, 12);

        }
        public Rectangle RectPlusIconsArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(RectStatusTitle().X - 41, RectStatusTitle().Y + 140, 23, 111);
        }

        public Rectangle RectStatusImproveTitle()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(63 + currentMetin2Icon.X, 574 + currentMetin2Icon.Y, 11, 9);
        }
        public Rectangle RectStatusImproveCountArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(247 + currentMetin2Icon.X, 265 + currentMetin2Icon.Y, 18, 14);
        }

        public Rectangle RectFisherShopPage()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(482 + currentMetin2Icon.X, 58+ currentMetin2Icon.Y, 22, 6);
        }
        
        public Rectangle RectKampGreenSample()
        {
            return new Rectangle(0,0,48,7);
        }
        public Rectangle RectEquipmentArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(700 + currentMetin2Icon.X, 286+ currentMetin2Icon.Y, 16, 6);
        }

        public Rectangle RectHpTitleBar()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            // return new Rectangle(67 + currentMetin2Icon.X, 598 + currentMetin2Icon.Y, 95 ,10);4
            return new Rectangle(67 + currentMetin2Icon.X, 602 + currentMetin2Icon.Y, 95, 1);
        }
        public Rectangle RectManaTitleBar()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            //return new Rectangle(67 + currentMetin2Icon.X, 609 + currentMetin2Icon.Y, 95, 11);
            return new Rectangle(67 + currentMetin2Icon.X, 614 + currentMetin2Icon.Y, 95, 1);
        }
        public Rectangle RectAutoHunterTitle()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(379 + currentMetin2Icon.X, 99 + currentMetin2Icon.Y, 16, 8);
        }
        public Rectangle RectAutoHunterSaldirButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(486 + currentMetin2Icon.X, 419 + currentMetin2Icon.Y, 13, 10);
        }
        public Rectangle RectAutoHunterOdakButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(484 + currentMetin2Icon.X, 447 + currentMetin2Icon.Y, 15, 9);
        }
        public Rectangle RectAutoHunterStartButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(396 + currentMetin2Icon.X, 552 + currentMetin2Icon.Y, 11, 8);
        }
        public Rectangle RectAutoHunterStopButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(478 + currentMetin2Icon.X, 552 + currentMetin2Icon.Y, 10, 8);
        }
        public Rectangle RectItemPickUpDetectArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(205 + currentMetin2Icon.X, 208 + currentMetin2Icon.Y, 382, 246);
            // return new Rectangle(106+ + currentMetin2Icon.X, 119 + currentMetin2Icon.Y, 568, 458);
        }
        public Rectangle RectSilahciOptionMarketiAc()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(387 + currentMetin2Icon.X, 249 + currentMetin2Icon.Y, 19, 8);          
        }
        public Rectangle RectSilahciShopPage()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(480 + currentMetin2Icon.X, 56 + currentMetin2Icon.Y, 16, 9);
        }

        public Rectangle RectSilahciYangYokDialog()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(346 + currentMetin2Icon.X, 313 + currentMetin2Icon.Y, 19, 9);
        }
        public Rectangle RectSimyaciEvetButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(397 + currentMetin2Icon.X, 266 + currentMetin2Icon.Y, 14, 7);
        }
        public Rectangle RectItemWithoutNumbersSample() 
        {
            return new Rectangle(0, 0, 32, 10);
        }
        public Rectangle Rect200WhiteSample()
        {
            return new Rectangle(0, 0, 13, 5);
        }
        public Rectangle RectWormSample() 
        {
            return new Rectangle(0, 0, 32, 10);
        }
       
        public Rectangle RectItemSlotSizeSample()
        {
            return new Rectangle(0, 0, 32, 16);
        }
        public Rectangle RectItemSlotSmallSizeSample()
        {
            return new Rectangle(0, 0, 16, 9);
        }
        public Rectangle RectWhisperPanelDetecSample()
        {
            return new Rectangle(0,0,15,13);
        }
        public Rectangle RectGForgeAppIconSample()
        {
            return new Rectangle(0, 0, 7, 6);
        }
        public Rectangle RectGForgeMaximizeButtonSample()
        {
            return new Rectangle(0, 0, 12, 7);
        }
        public Rectangle RectStatusPlusImageSample()
        {
            return new Rectangle(0, 0, 9, 9);
        }
        public Rectangle RectEmptySlotIconSample()
        {
            return new Rectangle(0, 0, 17, 17);
        }
        public Rectangle RectGForgeOynaButton()
        {
            return new Rectangle(0, 0, 37, 8);
        }
        public Rectangle RectGForgeOynaDownButtton()
        {
            return new Rectangle(0, 0, 8, 6);
        }
        public Rectangle RectGForgeOynaUpButton()
        {
            return new Rectangle(0, 0, 7, 7);
        }
        public Rectangle RectKediIsirigiSample()
        {
            return new Rectangle(0, 0, 5, 5);
        }
        public Rectangle RectInventoryPageArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(630 + currentMetin2Icon.X, 270 + currentMetin2Icon.Y,
                180, 300);
        }
        public Rectangle RectTradeDetectionArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(272 + currentMetin2Icon.X, 247 + currentMetin2Icon.Y,
                14, 10);
        }

        public Rectangle RectWhisperFullArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(720 + currentMetin2Icon.X, 200 + currentMetin2Icon.Y,
                70, 242);
        }

        public Rectangle RectWhisperOneArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(746 + currentMetin2Icon.X, 211 + currentMetin2Icon.Y,
                15, 13);
        }

        public Rectangle RectMiniMapCoordianetesArea()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Rectangle(684 + currentMetin2Icon.X, 97 + currentMetin2Icon.Y,
                46, 16);
        }

        

        //>>>>>>>>>>>  POINT COORDINATES  <<<<<<<<<<<<<<<<
        public Point PointInventPageOne()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(657 + currentMetin2Icon.X, 255+ currentMetin2Icon.Y);
        }
        public Point PointInventPageTwo()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(698 + currentMetin2Icon.X, 255 + currentMetin2Icon.Y);
        }
        public Point PointFisherShopKampAtesi()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(469 + currentMetin2Icon.X, 85 + currentMetin2Icon.Y);
        }
        public Point PointChSix()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(496+ currentMetin2Icon.X, 291 + currentMetin2Icon.Y);
        }
        public Point PointFisherShopCloseButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(578 + currentMetin2Icon.X, 53 + currentMetin2Icon.Y);
        }
        public Point PointFisherShopFiftyWorm()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(501 + currentMetin2Icon.X, 116 + currentMetin2Icon.Y);
        }
        public Point PointArmorPlace()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(657 + currentMetin2Icon.X, 293 + currentMetin2Icon.Y);
        }
        public Point PointOkButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(535+ currentMetin2Icon.X, 516 + currentMetin2Icon.Y);
        }

        public Point PointExitButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            //return new Point(405 + currentMetin2Icon.X, 393 + currentMetin2Icon.Y);
            return new Point(403 + currentMetin2Icon.X, 379 + currentMetin2Icon.Y);
        }

        public Point PointCharEnterButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(363 + currentMetin2Icon.X, 572 + currentMetin2Icon.Y);
        }

        public Point PointYesButton()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(368 + currentMetin2Icon.X, 348 + currentMetin2Icon.Y);
        }

        public Point PointTradeCloseCross()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(533 + currentMetin2Icon.X, 252 + currentMetin2Icon.Y);
        }
        public Point PointHpPlus()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(RectStatusTitle().X - 30 + currentMetin2Icon.X,
                RectStatusTitle().Y + 144 + currentMetin2Icon.Y);
        }

        public Point PointSpPlus()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(RectStatusTitle().X - 30 + currentMetin2Icon.X,
                RectStatusTitle().Y + 175 + currentMetin2Icon.Y);
        }

        public Point PointStrPlus()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(RectStatusTitle().X - 30 + currentMetin2Icon.X,
                RectStatusTitle().Y + 209 + currentMetin2Icon.Y);
        }

        public Point PointDexPlus()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(RectStatusTitle().X - 30 + currentMetin2Icon.X,
                RectStatusTitle().Y + 237 + currentMetin2Icon.Y);
        }

        public Point PointStatusImproveClickImage()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(89 + currentMetin2Icon.X,
                542 + currentMetin2Icon.Y);
        }

        public Point PointMiniMapCharSymbol()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(739 + currentMetin2Icon.X,
                98 + currentMetin2Icon.Y);
        }

        public Point PointGameMiddlePoint()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(408 + currentMetin2Icon.X,
                330 + currentMetin2Icon.Y);
        }

        public Point PointKediIsirigi()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(562 + currentMetin2Icon.X,
                89 + currentMetin2Icon.Y);
        }

        public Point PointCloseSilahciShopPage()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(573 + currentMetin2Icon.X,
                57 + currentMetin2Icon.Y);
        }
        public Point PointInventThirdYCoorForKediIsirigi()
        {
            Point currentMetin2Icon = CheckGameScreenPlace();
            return new Point(650 + currentMetin2Icon.X,
                344 + currentMetin2Icon.Y);
        }
        
    }
}
