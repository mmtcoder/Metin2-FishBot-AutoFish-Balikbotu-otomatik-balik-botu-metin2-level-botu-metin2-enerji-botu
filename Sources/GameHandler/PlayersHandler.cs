using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Drawing;
using System.Threading;


namespace Metin2AutoFishCSharp.Sources.GameHandler
{
    internal class PlayersHandler
    {
        private ImageObjects imageobject;
        private GameObjectCoordinates coor;
        private ScreenShotWinAPI screenShot;
        private CharInfo charInfo;
        private TimerGame timerDetectPlayersName;
        private CharSpecialThings charThings;
        private bool storeOldDetectValue = false;

        private Rectangle rectBackSideNameDetection = Rectangle.Empty;
        //Back side detectNameRect heigth should be 105 pixel
        public PlayersHandler(ImageObjects imageobject)
        {
            this.imageobject = imageobject;
            coor = new GameObjectCoordinates(imageobject);
            screenShot = new ScreenShotWinAPI();
            charInfo = new CharInfo(imageobject);
            timerDetectPlayersName = new TimerGame();
            timerDetectPlayersName.SetStartedSecondTime();
            charThings = new CharSpecialThings(imageobject);
        }
        public bool DetectAnotherPlayersMiniMap()
        {
            Rectangle rectMiniMap = coor.RectMiniMapArea();
            int[] miniMapImage = screenShot.ImageArraySpecifiedArea(rectMiniMap);
            for (int y = 0; y < rectMiniMap.Height; y++)
            {
                for (int x = 0; x < rectMiniMap.Width; x++)
                {
                    if (imageobject.CompareTwoRgbIntAdvanced(ColorGame.MINIMAP_PLAYER_DARKYELLOW,
                        miniMapImage[(y * rectMiniMap.Height) + x]) ||
                        imageobject.CompareTwoRgbIntAdvanced(ColorGame.MINIMAP_PLAYER_OPENYELLOW,
                         miniMapImage[(y * rectMiniMap.Height) + x]))
                    {
                        ThreadGlobals.isAnotherPlayerDetected = true;
                        return true;
                    }
                    if (imageobject.CompareTwoRgbIntAdvanced(ColorGame.MINIMAP_PLAYER_DARKPINK,
                        miniMapImage[(y * rectMiniMap.Height) + x]) ||
                        imageobject.CompareTwoRgbIntAdvanced(ColorGame.MINIMAP_PLAYER_OPENPINK,
                         miniMapImage[(y * rectMiniMap.Height) + x]))
                    {
                        ThreadGlobals.isAnotherPlayerDetected = true;
                        ThreadGlobals.isEnemyDetected = true;
                        return true;
                    }
                }
        
            }
            ThreadGlobals.isAnotherPlayerDetected = false;
            ThreadGlobals.isEnemyDetected = false;
            return false;
        }

        public bool DetectAnotherPlayer()
        {
            
            if (CharInfo.CharRectBounds != Rectangle.Empty)
            {
                if (!timerDetectPlayersName.CheckDelayTimeInSecond(5))
                {
                    timerDetectPlayersName.SetStartedSecondTime();

                    while(ThreadGlobals.isActiveFishBoard)
                    {
                        if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                        { return false; }
                       
                    }
                    charThings.OpenCloseSettingButton(false);

                    Rectangle rectDetectedMyName = imageobject.FindBorderAreaForWantedColor(ColorGame.PLAYER_YELLOW_COLOR,
                                         coor.RectCharNameArea());

                    if (Math.Abs(rectDetectedMyName.Width - CharInfo.CharRectBounds.Width) > 3)
                    {
                        //DebugPfCnsl.println("çalişti 11");
                        ThreadGlobals.isAnotherPlayerDetected = true;
                        storeOldDetectValue = true;
                        return true;
                    }

                    Rectangle rectDetectSameFlagName = new Rectangle(coor.RectCharNameArea().X, coor.RectCharNameArea().Y +
                        coor.RectCharNameArea().Height, coor.RectCharNameArea().Width, 105);

                    // DebugDrawingHandle.DrawWantedObjectToScreen(rectDetectSameFlagName);
                    //Thread.Sleep(2000);

                    if (imageobject.FindBorderAreaForWantedColor(ColorGame.PLAYER_YELLOW_COLOR,
                        rectDetectSameFlagName) != Rectangle.Empty)
                    {
                        // DebugPfCnsl.println("çalişti 222");
                        ThreadGlobals.isAnotherPlayerDetected = true;
                        storeOldDetectValue = true;
                        return true;
                    }

                    Rectangle rectEnemyName = new Rectangle(coor.RectCharNameArea().X, coor.RectCharNameArea().Y,
                        coor.RectCharNameArea().Width, 105 + coor.RectCharNameArea().Height);

                    if (imageobject.FindBorderAreaForWantedColor(ColorGame.PLAYER_PINK_COLOR,
                        rectEnemyName) != Rectangle.Empty)
                    {
                        //  DebugPfCnsl.println("çalişti 333");
                        ThreadGlobals.isEnemyDetected = true;
                        ThreadGlobals.isAnotherPlayerDetected = true;
                        storeOldDetectValue = true;
                        return true;
                    }
                   
                }
                else
                {
                    return storeOldDetectValue;
            
                }
                
            }
            else
            {
                charInfo.DetectCharName();
            }
            ThreadGlobals.isEnemyDetected = false;
            ThreadGlobals.isAnotherPlayerDetected = false;
            storeOldDetectValue = false;
            return false;
        }
    }
}
