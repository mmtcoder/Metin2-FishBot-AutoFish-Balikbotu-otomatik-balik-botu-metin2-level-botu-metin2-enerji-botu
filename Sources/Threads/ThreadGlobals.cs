using Metin2AutoFishCSharp.Sources.LevelAndFarms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources
{
    public class ThreadGlobals
    {
        
        public static volatile bool isSettingButtonSeemed = false;
        public static volatile bool isCharKilled = false;
        public static volatile bool isCharScreenActive = false;
        public static volatile bool isEntryScreenActive = false;
        public static volatile bool isSaleTitleActive = false;     
        public static volatile bool isAnotherPlayerDetected = false;
        public static volatile bool isMetin2IconSeemed = true;
        public static volatile bool isChatting = false;
        public static volatile bool isPausedTheGame = false;
        public static volatile bool isCharStopped = false;
        public static volatile bool isEnemyDetected = false;
        public static volatile bool isTradePanelActive = false;
        public static volatile bool isWhisperDetected = false;
        public static volatile bool isCheckedShiftPage = true;
        public static volatile bool isCharNameCanDetectable = false;

       

       

        public static volatile bool IsThreadOneActive = false;
        public static volatile bool IsThreadTwoActive = false;
        public static volatile bool IsThreadThreeActive = false;


        public static volatile bool isTimerBreakEnabled = false;

        /*************   ABOUT FISHED ***************/
        public static volatile bool isFishingStopped = true;

        public static volatile bool isYabbieSelected = true;
        public static volatile bool isAltinSudakSelected = true;
        public static volatile bool isPalamutSelected = true;
        public static volatile bool isKurbagaSelected = false;
        public static volatile bool isKadifeSelected = false;
        public static volatile bool isHepsiSelected = false;
        public static volatile bool isDenizkizSelected = false;

        public static volatile bool isPrepareFishingStarted = false;

        public static volatile bool isActiveFishBoard = false;

        public static volatile bool isFishingMiniBreakActive = false;

        public static volatile bool isChattingAnswerActive = false;
        public static volatile bool isWhisperAnswerActive = false;
        public static volatile bool isAdaptableFishing = false;

        /*************    ABOUT LEVEL AND FARM    ***********/
        public static volatile bool isLevelFarmStopped = true;

        public static volatile int[] STATUS_PRIORITY = new int[4];
        public static volatile int[] HP_SP_RATE = new int[2];
        public static volatile int[] SKILL_TIME_FOR_KEYS = new int[8];

        public static volatile bool isETPPickUpActive = false;


        /************  ABOUT ENERGY CRİSTAL     ************/

        public static volatile bool  isEnergyCristalStopped =true;

        /***********    TELEGRAM    **********/

        public static bool isTelegramBotActive = false;

        

        public static void DebugThreadGloablValues()
        {
            Console.WriteLine("DebugThreadGloablValues is started  ");
            Console.WriteLine("isFishingStopped = " + isFishingStopped + " isSettingButtonSeemed = " + "\n"+
                isSettingButtonSeemed + " isCharKilled = " + isCharKilled + " isSaleTitleActive = " + "\n"
                + isSaleTitleActive + " isActiveFishBoard = " + isActiveFishBoard + "\n" + "isEntryScreenActive = " +
                isEntryScreenActive + " isPrepareFishingStarted = " + isPrepareFishingStarted + "\n"
                + " isCharStopped = " + isCharStopped + " isPausedTheGame = " + isPausedTheGame + "\n"
                + " isChatting = " + isChatting + " isCharScreenActive = " + isCharScreenActive  + "\n"
                + " isMetin2IconSeemed = " + isMetin2IconSeemed + " isTradePanelActive = " + isTradePanelActive + "\n"
                + "  isWhisperDetected = " + isWhisperDetected + " isCheckedShiftPage = " + isCheckedShiftPage + "\n"
                + " isLevelFarmStopped = " + isLevelFarmStopped + "STATUS_PRIORITY index 0 = " + STATUS_PRIORITY[0] + "\n"
                + "STATUS_PRIORITY index 1 = " + STATUS_PRIORITY[1] + "STATUS_PRIORITY index 2 = " + STATUS_PRIORITY[2] +"\n"
                + "STATUS_PRIORITY index 3 = " + STATUS_PRIORITY[3] + " HP RATE = " + HP_SP_RATE[0] + "\n"
                 + " SP RATE = " + HP_SP_RATE[1] + " isCharNameDetected = " + isCharNameCanDetectable + "\n"
                 + " isEnergyCristalStopped = " + isEnergyCristalStopped);
        }

        public static bool CanFishingRightNow()
        {

            return isFishingStopped == false && isChatting == false && isSettingButtonSeemed == true &&
                    isCharKilled == false && isSaleTitleActive == false && isCharScreenActive == false
            && isMetin2IconSeemed == true && isPausedTheGame == false && isEntryScreenActive ==false
            && isCharStopped == false == isTradePanelActive == false && isWhisperDetected == false &&
            isCheckedShiftPage == true && isCharNameCanDetectable == true;

        }

        public static bool CanLevelAndFarmRightNow()
        {
            return isLevelFarmStopped == false && isChatting == false && isSettingButtonSeemed == true &&
                    isCharKilled == false && isCharScreenActive == false
            && isMetin2IconSeemed == true && isPausedTheGame == false && isEntryScreenActive == false
            && isCharStopped == false == isTradePanelActive == false && isWhisperDetected == false
            && isCheckedShiftPage == true && isCharNameCanDetectable == true;
        }

        public static bool CanEnergyCristalRightNow()
        {
            return isEnergyCristalStopped == false && isChatting == false && isSettingButtonSeemed == true &&
                    isCharKilled == false && isCharScreenActive == false
            && isMetin2IconSeemed == true && isPausedTheGame == false && isEntryScreenActive == false
            && isCharStopped == false == isTradePanelActive == false && isWhisperDetected == false
            && isCheckedShiftPage == true && isCharNameCanDetectable == true;
        }

        public static bool CheckGameIsStopped()
        {
            if(isFishingStopped && isLevelFarmStopped && isEnergyCristalStopped)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public static void SetDefaultGloabalValues()
        {
            
            isSettingButtonSeemed = false;
            isCharKilled = false;
            isSaleTitleActive = false;
            isCharScreenActive = false;
            isMetin2IconSeemed = true;
            isPausedTheGame = false;
            isEntryScreenActive = false;
            isChatting = false;
            isCharStopped = false;
            isActiveFishBoard = false;
            isEnemyDetected = false;
            isAnotherPlayerDetected = false;
            isTradePanelActive = false;
            isWhisperDetected = false;
            isCharNameCanDetectable = false;
            


            isYabbieSelected = true;
            isAltinSudakSelected = true;
            isPalamutSelected = true;
            isKurbagaSelected = false;
            isKadifeSelected = false;
            isDenizkizSelected = false;
            isHepsiSelected = false;

            IsThreadOneActive = false;
            IsThreadTwoActive = false;
            IsThreadThreeActive = false;

            AutoHunter.IS_AUTO_HUNTER_STARTED = false;

            //isLevelFarmStopped = false;




        }
    }
}
