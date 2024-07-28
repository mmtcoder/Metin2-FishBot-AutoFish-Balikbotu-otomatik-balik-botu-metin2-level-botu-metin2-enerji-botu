using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.LevelAndFarms
{
    internal class AutoHunter
    {
        private ImageObjects imagesObject;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;
        private GameInputHandler inputs;
        private CharSpecialThings charThings;

        private TimerGame timerAutoHunter;

        public static volatile bool IS_AUTO_HUNTER_STARTED = false;
        public AutoHunter(ImageObjects imagesObject)
        {
            this.imagesObject = imagesObject;
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);
            timerAutoHunter = new TimerGame();
            inputs = new GameInputHandler();
            charThings = new CharSpecialThings(imagesObject);
        }
        public void StartStopOtomatikAv()
        {
           
            if (!IS_AUTO_HUNTER_STARTED)
            {
               
                if (OpenOrCloseAutoHunterTitleActive(true))
                {
                    if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                    {
                        DebugPfCnsl.println("StartOtomatikAv is stopped the game");
                        IS_AUTO_HUNTER_STARTED = false;
                    }
                    
                        PressAutoHunterSaldirButton();
                        PressAutoHunterOdakButton();
                        PressAutoHunterBaslatButton();
                        OpenOrCloseAutoHunterTitleActive(false);
                        IS_AUTO_HUNTER_STARTED = true;
                }
                
                
            }
                    
        }

        public void StopOtomatikAv()
        {
            if(IS_AUTO_HUNTER_STARTED)
            {
                if (OpenOrCloseAutoHunterTitleActive(true))
                {
                    PressAutoHunterStopButton();
                    OpenOrCloseAutoHunterTitleActive(false);
                    IS_AUTO_HUNTER_STARTED = false;
                }
                
            }
            
        }

        public bool OpenOrCloseAutoHunterTitleActive(bool state)
        {
            TimerGame timerAutoHunterTitle = new TimerGame();
            int[] targetOtoAvTitleBar = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterTitle());

            while(imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayOtoAvTitleIcon,
                targetOtoAvTitleBar,ImageSensibilityLevel.SENSIBILTY_HIGH) != state)
            {
                if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("CheckAutoHunterTitleActive title is returned false");
                    return false;
                }
                if(timerAutoHunterTitle.CheckDelayTimeInSecond(20))
                {
                    inputs.KeyPress(KeyboardInput.ScanCodeShort.KEY_K);
                    TimerGame.SleepRandom(599, 799);
                    targetOtoAvTitleBar = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterTitle());
                }
                else
                {
                    charThings.SettingButtonClick(SettingButtonPrefers.CHAR_BUTTON);
                    while (!ThreadGlobals.CanLevelAndFarmRightNow()) {
                        if (!ThreadGlobals.isLevelFarmStopped)
                        {
                            DebugPfCnsl.println("CheckAutoHunterTitleActive title is returned false");
                            return false;
                        }
                        DebugPfCnsl.println("OpenOrCloseAutoHunterTitleActive zaman aşımına uğradı ve " +
                            "ve oynanabilir hale gelinmesi bekleniyor");
                    }
                    TimerGame.SleepRandom(5999, 8999);
                    OpenOrCloseAutoHunterTitleActive(true);
                }
            }
            return true;
        }
        
        public bool PressAutoHunterSaldirButton()
        {
            TimerGame timerSaldirButton = new TimerGame();

            int[] targetSaldirButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterSaldirButton());

            while(imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayOtoAvSaldirButtonIcon,
                targetSaldirButtonNotPressed,ImageSensibilityLevel.SENSIBILTY_HIGH))
                {
                if (ThreadGlobals.isLevelFarmStopped ||!ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("PressAutoHunterSaldirButton returned false.Game is stopped");
                    return false;
                }
                if (timerSaldirButton.CheckDelayTimeInSecond(5))
                {
                    inputs.MouseMoveAndPressLeft(coor.RectAutoHunterSaldirButton().X, coor.RectAutoHunterSaldirButton().Y);
                    TimerGame.SleepRandom(500, 700);
                    targetSaldirButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterSaldirButton());
                }
                else
                {
                    DebugPfCnsl.println("PressAutoHunterSaldirButton basım süresini aştı false döndürüldü");
                    return false;
                }
               
                }
            return true;
        }

        public bool PressAutoHunterOdakButton()
        {
            TimerGame timerOdakButton = new TimerGame();

            int[] targetOdakButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterOdakButton());

            while (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayOtoAvOdakButtonIcon,
                targetOdakButtonNotPressed, ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                if (ThreadGlobals.isLevelFarmStopped ||!ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("PressAutoHunterOdakButton returned false.Game is stopped");
                    return false;
                }
                if (timerOdakButton.CheckDelayTimeInSecond(5))
                {
                    inputs.MouseMoveAndPressLeft(coor.RectAutoHunterOdakButton().X, coor.RectAutoHunterOdakButton().Y);
                    TimerGame.SleepRandom(500, 700);
                    targetOdakButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterOdakButton());
                }
                else
                {
                    DebugPfCnsl.println("PressAutoHunterOdakButton basım süresini aştı false döndürüldü");
                    return false;
                }

            }
            return true;
        }

        public bool PressAutoHunterBaslatButton()
        {
            TimerGame timerStartButton = new TimerGame();

            int[] targetStartButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterStartButton());

            while (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayOtoAvBaslatButtonIcon,
                targetStartButtonNotPressed, ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                {
                    DebugPfCnsl.println("PressAutoHunterBaslatButton returned false.Game is stopped");
                    return false;
                }
                if (timerStartButton.CheckDelayTimeInSecond(5))
                {
                    inputs.MouseMoveAndPressLeft(coor.RectAutoHunterStartButton().X, coor.RectAutoHunterStartButton().Y);
                    TimerGame.SleepRandom(500, 700);
                    targetStartButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterStartButton());
                }
                else
                {
                    DebugPfCnsl.println("PressAutoHunterBaslatButton basım süresini aştı false döndürüldü");
                    return false;
                }

            }
            return true;
        }

        public bool PressAutoHunterStopButton()
        {
            TimerGame timerStopButton = new TimerGame();

            int[] targetStopButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterStopButton());

            while (imagesObject.CompareTwoArrayAdvanced(imagesObject.arrayOtoAvDurdurButtonIcon,
                targetStopButtonNotPressed, ImageSensibilityLevel.SENSIBILTY_HIGH))
            {
                if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed )
                {
                    DebugPfCnsl.println("PressAutoHunterStopButton returned false.Game is stopped");
                    return false;
                }
                if (timerStopButton.CheckDelayTimeInSecond(5))
                {
                    inputs.MouseMoveAndPressLeft(coor.RectAutoHunterStopButton().X, coor.RectAutoHunterStopButton().Y);
                    TimerGame.SleepRandom(500, 700);
                    targetStopButtonNotPressed = screenShot.ImageArraySpecifiedArea(coor.RectAutoHunterStopButton());
                }
                else
                {
                    DebugPfCnsl.println("PressAutoHunterStopButton basım süresini aştı false döndürüldü");
                    return false;
                }

            }
            return true;
        }
    }


}
