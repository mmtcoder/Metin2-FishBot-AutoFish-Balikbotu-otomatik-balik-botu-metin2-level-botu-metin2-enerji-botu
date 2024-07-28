using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources.ImageHandle
{
    internal class ImageObjects : ImageProcess
    {
       
        public int[] arrayMetin2Icon { get; private set; }
        public int[] arrayInventoryTitle { get; private set; }
        public int[] arrayEntryScreen { get; private set; }
        public int[] arrayCharScreen { get; private set; }
        public int[] arrayCharButton { get; private set; }
        public int[] arrayKilledScreen { get; private set; }
        public int[] arraySaleTitle { get; private set; }
        public int[] arraySettingButton { get; private set; }
        public int[] arrayPageOne { get; private set; }
        public int[] arrayPageTwo { get; private set; }
        public int[] arrayPageThree { get; private set; }
        public int[] arrayPageFour { get; private set; }
        public int[] arrayBalikciAraEkran { get; private set; }
        public int[] arrayFullDialogInFisherShop { get; private set; }
        public int[] arrayKampIcon { get; private set; }
        public int[] arrayYereAtmaDialog { get; private set; }
        public int[] arrayEmptySlotPlace { get; private set; }
        public int[] arrayWorm200 { get; private set; }
        public int[] arrayFishTitle { get; private set; }
        public int[] arrayFisherShopPage { get; private set; }
        public int[] arrayWorm {get; private set; }
        public int[] array200WhiteNumber { get; private set; }
        public int[] arrayYabbieIcon { get; private set; }
        public int[] arrayAltinSudakIcon { get; private set; }
        public int[] arrayPalamutIcon { get; private set; }
        public int[] arrayFisherWords { get; private set; }
        public int[] arrayKampAtesiWords { get; private set; }
        public int[] arrayGameForgeOyna {  get; private set; }
        public int[] arrayGameForgeAppIcon { get; private set; }
        public int[] arrayGameForgeOynaDownBut { get; private set; }
        public int[] arrayGameForgeOynaUpBut { get; private set; }   
        public int[] arrayGameForgeMaximizeBut { get; private set; }
        public int[] arrayTradeIcon { get; private set; }
        public int[] arrayWhisperIcon { get; private set; }
        public int[] arrayWhisperPanelDetect { get; private set; }
        public int[] arrayBluePotKIcon { get; private set; }
        public int[] arrayBluePotOIcon { get; private set; } 
        public int[] arrayBluePotBIcon { get; private set; }
        public int[] arrayBluePotXxlIcon { get; private set; }
        public int[] arrayRedPotKIcon { get; private set; }
        public int[] arrayRedPotOIcon { get; private set; }
        public int[] arrayRedPotBIcon { get; private set; }
        public int[] arrayRedPotXxlIcon { get; private set; }
        public int[] arrayStatusTitleIcon { get; private set; }
        public int[] arrayStatusPlusIcon { get; private set; }
        public int[] arrayOtoAvTitleIcon { get; private set; }
        public int[] arrayOtoAvSaldirButtonIcon { get; private set; }
        public int[] arrayOtoAvOdakButtonIcon { get; private set; }
        public int[] arrayOtoAvBaslatButtonIcon { get; private set; }
        public int[] arrayOtoAvDurdurButtonIcon { get; private set; }
        public int[] arraySilahciGreenTitle {  get; private set; }
        public int[] arraySilahciOptionPage {  get; private set; }
        public int[] arraySilahciShopTitle { get; private set; }
        public int[] arrayYangYokDilogIcon { get; private set; }
        public int[] arraySimyaciGreenTitle { get; private set; }
        public int[] arraySimyaciEvetOptionPage { get; private set; }
        public int[] arrayKediIsirigi {  get; private set; }


        //@@@@@@@@@@  ONE COLOR ARRAYS @@@@@@@@@@@@@@@

        public int[] arrayPinkYabbie { get; private set; }
        public int[] arrayPinkAltinSudak { get; private set; }
        public int[] arrayPinkPalamut { get; private set; }
        public int[] arrayPinkKurbaga { get; private set; }
        public int[] arrayPinkTon { get; private set; }
        public int[] arrayPinkYerYok { get; private set; }
        public int[] arrayPinkExitChat { get; private set; }
        public int[] arrayPinkCanNotFish { get; private set; }
        public int[] arrayWhiteChatIsActive { get; private set; }
        public int[] arrayEmptyBar { get; private set; }
        public int[] arrayStatusImproveWhiteTitle { get; private set; }
        //public int[] arrayEmptySlotIcon { get; private set; }


        public ImageObjects() : base()
        {
            arrayMetin2Icon = convertBitMapToIntArray(ImagePathNames.metin2IconFileName, PathWayStruct.PATH_IMAGE);
            arrayInventoryTitle = convertBitMapToIntArray(ImagePathNames.inventoryFileName, PathWayStruct.PATH_IMAGE);
            arrayEntryScreen = convertBitMapToIntArray(ImagePathNames.entryScreenFileName, PathWayStruct.PATH_IMAGE);
            arrayCharScreen = convertBitMapToIntArray(ImagePathNames.charScreenFileName, PathWayStruct.PATH_IMAGE);
            arrayCharButton = convertBitMapToIntArray(ImagePathNames.charButtonFileName, PathWayStruct.PATH_IMAGE);
            arrayKilledScreen = convertBitMapToIntArray(ImagePathNames.dieScreenFileName, PathWayStruct.PATH_IMAGE);
            arraySaleTitle = convertBitMapToIntArray(ImagePathNames.saleTitleFileName, PathWayStruct.PATH_IMAGE);
            arraySettingButton = convertBitMapToIntArray(ImagePathNames.settingButtonFileName, PathWayStruct.PATH_IMAGE);
            arrayPageOne = convertBitMapToIntArray(ImagePathNames.pageOneFileName, PathWayStruct.PATH_IMAGE);
            arrayPageTwo = convertBitMapToIntArray(ImagePathNames.pageTwoFileName, PathWayStruct.PATH_IMAGE);
            arrayPageThree = convertBitMapToIntArray(ImagePathNames.pageThreeFileName, PathWayStruct.PATH_IMAGE);
            arrayPageFour = convertBitMapToIntArray(ImagePathNames.pageFourFileName, PathWayStruct.PATH_IMAGE);
            arrayBalikciAraEkran = convertBitMapToIntArray(ImagePathNames.balikciAraEkranFileName, PathWayStruct.PATH_IMAGE);
            arrayFullDialogInFisherShop = convertBitMapToIntArray(ImagePathNames.fullEnvanterFileName, PathWayStruct.PATH_IMAGE);
            arrayKampIcon = convertBitMapToIntArray(ImagePathNames.kampIconFileName, PathWayStruct.PATH_IMAGE);
            arrayYereAtmaDialog = convertBitMapToIntArray(ImagePathNames.yereAtmaFileName, PathWayStruct.PATH_IMAGE);
            arrayEmptySlotPlace = convertBitMapToIntArray(ImagePathNames.firstSkillEmptyFileName, PathWayStruct.PATH_IMAGE);
            arrayWorm200 = convertBitMapToIntArray(ImagePathNames.worm200FileName, PathWayStruct.PATH_IMAGE);
            arrayFishTitle = convertBitMapToIntArray(ImagePathNames.fishTitleFileName, PathWayStruct.PATH_IMAGE);
            arrayFisherShopPage = convertBitMapToIntArray(ImagePathNames.fisherShopPageFileName, PathWayStruct.PATH_IMAGE);
            arrayFisherWords = convertBitMapToIntArray(ImagePathNames.fisherGreenFileName, PathWayStruct.PATH_IMAGE);
            arrayKampAtesiWords = convertBitMapToIntArray(ImagePathNames.kampAtesiGreenFileName, PathWayStruct.PATH_IMAGE);
            arrayWorm = convertBitMapToIntArray(ImagePathNames.wormFileName, PathWayStruct.PATH_IMAGE);
            array200WhiteNumber = convertBitMapToIntArray(ImagePathNames.white200FileName, PathWayStruct.PATH_IMAGE);
            arrayGameForgeOyna = convertBitMapToIntArray(ImagePathNames.gameForgeOynaFileName, PathWayStruct.PATH_IMAGE);
            arrayGameForgeAppIcon = convertBitMapToIntArray(ImagePathNames.gForgeAppIconFileName, PathWayStruct.PATH_IMAGE);    
            arrayGameForgeOynaDownBut = convertBitMapToIntArray(ImagePathNames.gForgeOynaDownFileName, PathWayStruct.PATH_IMAGE);   
            arrayGameForgeOynaUpBut = convertBitMapToIntArray(ImagePathNames.gForgeOynaUpFileName, PathWayStruct.PATH_IMAGE);   
            arrayGameForgeMaximizeBut = convertBitMapToIntArray(ImagePathNames.gForgeMaximizeFileName, PathWayStruct.PATH_IMAGE);
            arrayTradeIcon = convertBitMapToIntArray(ImagePathNames.tradeIconFileName, PathWayStruct.PATH_IMAGE);
            arrayWhisperIcon = convertBitMapToIntArray(ImagePathNames.whispersIconFileName, PathWayStruct.PATH_IMAGE);
            arrayWhisperPanelDetect = convertBitMapToIntArray(ImagePathNames.whisperPanelDetectFileName, PathWayStruct.PATH_IMAGE);
            arrayEmptyBar = convertBitMapToIntArray(ImagePathNames.emptyBarFileName, PathWayStruct.PATH_IMAGE);
            arrayBluePotKIcon = convertBitMapToIntArray(ImagePathNames.bluePotkFileName, PathWayStruct.PATH_IMAGE);
            arrayBluePotOIcon = convertBitMapToIntArray(ImagePathNames.bluePotOFileName, PathWayStruct.PATH_IMAGE);
            arrayBluePotBIcon = convertBitMapToIntArray(ImagePathNames.bluePotBFileName, PathWayStruct.PATH_IMAGE);
            arrayBluePotXxlIcon = convertBitMapToIntArray(ImagePathNames.bluePotXxlFileName, PathWayStruct.PATH_IMAGE);
            arrayRedPotKIcon = convertBitMapToIntArray(ImagePathNames.redPotKFileName, PathWayStruct.PATH_IMAGE);
            arrayRedPotOIcon = convertBitMapToIntArray(ImagePathNames.redPotOFileName , PathWayStruct.PATH_IMAGE);
            arrayRedPotBIcon = convertBitMapToIntArray(ImagePathNames.redPotBFileName , PathWayStruct.PATH_IMAGE);
            arrayRedPotXxlIcon = convertBitMapToIntArray(ImagePathNames.redPotXxlFileName , PathWayStruct.PATH_IMAGE);
            arrayStatusTitleIcon = convertBitMapToIntArray(ImagePathNames.statusTitleFileName , PathWayStruct.PATH_IMAGE);
            arrayStatusPlusIcon = convertBitMapToIntArray(ImagePathNames.statusPlusIconFileName , PathWayStruct.PATH_IMAGE);
            arrayOtoAvTitleIcon = convertBitMapToIntArray(ImagePathNames.otoAvTitleIconFileName , PathWayStruct.PATH_IMAGE);
            arrayOtoAvSaldirButtonIcon = convertBitMapToIntArray(ImagePathNames.otoAvSaldirIconFileName , PathWayStruct.PATH_IMAGE);
            arrayOtoAvOdakButtonIcon = convertBitMapToIntArray(ImagePathNames.otoAvOdakIconFileName , PathWayStruct.PATH_IMAGE);
            arrayOtoAvBaslatButtonIcon = convertBitMapToIntArray(ImagePathNames.otoAvBaslatIconFileName, PathWayStruct.PATH_IMAGE); 
            arrayOtoAvDurdurButtonIcon = convertBitMapToIntArray(ImagePathNames.otoAvDurdurIconFileName , PathWayStruct.PATH_IMAGE);
            arraySilahciGreenTitle = convertBitMapToIntArray(ImagePathNames.silahciGreenFileName , PathWayStruct.PATH_IMAGE);
            arraySilahciOptionPage = convertBitMapToIntArray(ImagePathNames.silahciOptionPageFileName , PathWayStruct.PATH_IMAGE);
            arraySilahciShopTitle = convertBitMapToIntArray(ImagePathNames.silahciShopFileName , PathWayStruct.PATH_IMAGE);
            arrayYangYokDilogIcon = convertBitMapToIntArray(ImagePathNames.yangYokDialogFileName , PathWayStruct.PATH_IMAGE);
            arraySimyaciGreenTitle = convertBitMapToIntArray(ImagePathNames.simyaciGreenFileName , PathWayStruct.PATH_IMAGE);
            arraySimyaciEvetOptionPage = convertBitMapToIntArray(ImagePathNames.simyaciEvetFileName , PathWayStruct.PATH_IMAGE);
            arrayKediIsirigi = convertBitMapToIntArray(ImagePathNames.kediIsirigiFileName , PathWayStruct.PATH_IMAGE);


            arrayYabbieIcon = convertBitMapToIntArray(ImagePathNames.yabbieIconFileName, PathWayStruct.PATH_FISHES);
            arrayAltinSudakIcon = convertBitMapToIntArray(ImagePathNames.altinSudakIconFileName, PathWayStruct.PATH_FISHES);
            arrayPalamutIcon = convertBitMapToIntArray(ImagePathNames.palamutIconFileName, PathWayStruct.PATH_FISHES);

            //@@@@@@@@@@@@@@@@@@@@  ONE COLOR ARRAYS  @@@@@@@@@@@@@@@@@@@@@@@

            arrayPinkYabbie = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.yabbieChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkAltinSudak = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.altinSudakChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkPalamut = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.palamutChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkKurbaga = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.kurbagaChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkTon = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.altinTonChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkYerYok = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.yeryokChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkExitChat = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.exitChatFileName, PathWayStruct.PATH_FISHES));
            arrayPinkCanNotFish = RecordWantedColorIntArray(ColorGame.CHAT_PINK_COLOR,
                convertBitMapToIntArray(ImagePathNames.cannotFishChatFileName, PathWayStruct.PATH_FISHES));
            arrayWhiteChatIsActive = RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                convertBitMapToIntArray(ImagePathNames.chatActiveFileName, PathWayStruct.PATH_IMAGE));
            arrayStatusImproveWhiteTitle = RecordWantedColorIntArray(ColorGame.CHAT_WHITE_COLOR,
                convertBitMapToIntArray(ImagePathNames.statusImproveWhiteFileName, PathWayStruct.PATH_IMAGE));
             }

        private int[] convertBitMapToIntArray(string fileName,PathWayStruct pathWay)
        {
            return screenshot.ConvertBitmapToArray(FileHandler.ReadPngFileGetBitmap(fileName,pathWay));
        }
    }

    
}
