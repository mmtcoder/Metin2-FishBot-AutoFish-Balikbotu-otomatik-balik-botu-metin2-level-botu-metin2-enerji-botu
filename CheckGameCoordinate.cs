using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPlayerApp.Sources.CoordinatesHandler
{
    internal class CheckGameCoordinate 
    {

        private static TimerGame timerGame;
        private ScreenShotWinAPI screen;
        ImageObjects process;

        private static bool isScannedMt2Icon = false;

        private readonly Rectangle referenceCoordinates = new Rectangle(18, 11, 6, 6);

        public static Point currentScreenGamePoint = new Point();
        public static volatile bool IS_METIN2_ICON_DETECTED = true;

        public CheckGameCoordinate(ImageObjects imageObjects)
        {
           timerGame = new TimerGame();
           screen = new ScreenShotWinAPI();
           process = imageObjects;
        }
      
        protected Point CheckGameScreenPlace()
        {
              if (!isScannedMt2Icon)
              {

                  Rectangle rectNewMetin2Icon = process.FindImageOnScreen(process.arrayMetin2Icon,referenceCoordinates);

                  if (rectNewMetin2Icon != Rectangle.Empty)
                  {
                   
                    timerGame.SetStartedSecondTime();
                    isScannedMt2Icon = true;

                    int metin2IconX = rectNewMetin2Icon.X - referenceCoordinates.X;
                    int metin2IconY = rectNewMetin2Icon.Y - referenceCoordinates.Y;

                    currentScreenGamePoint = new Point(metin2IconX, metin2IconY);

                    IS_METIN2_ICON_DETECTED = true;
                    return new Point(metin2IconX, metin2IconY);
                   
                  }
                  else
                  {
                      
                    DebugPfCnsl.println("Program couldn't get the metin2 icon");
                    IS_METIN2_ICON_DETECTED = false;
                    return new Point(0,0);
                }
                  
              }
              else
              {

                  if (timerGame.CheckDelayTimeInSecond(60))
                  {
                    IS_METIN2_ICON_DETECTED = true;
                    return currentScreenGamePoint;
                  }
                  
                  else
                  {

                      isScannedMt2Icon = false;
                      timerGame.SetStartedSecondTime();
                     return currentScreenGamePoint;
                  }
              }

              

        }
    }


    
    
}
