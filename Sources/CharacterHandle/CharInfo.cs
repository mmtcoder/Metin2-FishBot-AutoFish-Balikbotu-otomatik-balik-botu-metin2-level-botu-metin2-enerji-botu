using Metin2AutoFishCSharp.Sources.ChatHandler;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.GameHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources.CharacterHandle
{
    internal class CharInfo
    {
       
        
        public static string CharNameString = string.Empty;
        public static Rectangle CharRectBounds = Rectangle.Empty;
        public static int CharLevel = 0;

        private ScreenShotWinAPI screenshot;
        private ImageObjects imagesObject;
        private GameObjectCoordinates coor;
        private GameInputHandler inputGame;
        private GameAlphabetDetecter chatDetecter;
        

        public CharInfo(ImageObjects imagesObject)
        {
            this.imagesObject = imagesObject;
            screenshot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);
            inputGame = new GameInputHandler();
            chatDetecter = new GameAlphabetDetecter(imagesObject);
           
        }

        public bool DetectCharName()
        {
           
             BirdViewPerpective();

             Rectangle playerYellowName = imagesObject.FindBorderAreaForWantedColor(ColorGame.PLAYER_YELLOW_COLOR,
             coor.RectCharNameArea());

                if (playerYellowName != Rectangle.Empty)
                {
                if (CharNameString == string.Empty)
                {
                    CharNameString = chatDetecter.DetectGameText(ColorGame.PLAYER_YELLOW_COLOR, playerYellowName);
                    CharNameString = CharNameString.Trim();
                    
                    CharRectBounds = playerYellowName;
                    DebugPfCnsl.println("Detected char name = " + CharNameString);
                }
                ThreadGlobals.isCharNameCanDetectable = true;

                return true;
                }
                else
                {
                ThreadGlobals.isCharNameCanDetectable = false;
                return false;
                }

            
            


                
          
        }

        public void ProvideCharNameCanSee()
        {
            
            TimerGame timerProvideCharName = new TimerGame();
            while (!DetectCharName())
            {
                if (timerProvideCharName.CheckDelayTimeInSecond(15))
                {
                    if (ThreadGlobals.CheckGameIsStopped() && !ThreadGlobals.isSettingButtonSeemed)
                    {
                        DebugPfCnsl.println("ProvideCharNameCanSee returned ");
                        return;
                    }
                    

                    inputGame.KeyPress(KeyboardInput.ScanCodeShort.ESCAPE);
                    TimerGame.SleepRandom(500, 800);
                }
                else
                {
                    DebugPfCnsl.println("uzun süredir isim tespit edilemedi sorun var");
                    return;
                }
               
            }
            
        }

        public void BirdViewPerpective()
        {
            inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_F);
            TimerGame.SleepRandom(25, 35);
            inputGame.KeyDown(KeyboardInput.ScanCodeShort.KEY_G);
            TimerGame.SleepRandom(1400, 1600);
            inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_F);
            TimerGame.SleepRandom(25, 35);
            inputGame.KeyRelease(KeyboardInput.ScanCodeShort.KEY_G);
            TimerGame.SleepRandom(10, 20);

        }
    }
}
