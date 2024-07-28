using Metin2AutoFishCSharp.Sources.ChatHandler;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.CharacterHandle
{
    internal class CharMovement : CharInfo
    {
        //Mini harita karakter bulunduğu nokta Point = 739,98
        //Mini harita koordinat değer rectangle = 685 97 45 16
        private ImageObjects imagesObject;
        private GameAlphabetDetecter gameAlphabet;
        private ScreenShotWinAPI screenShot;
        private GameObjectCoordinates coor;
        private GameInputHandler inputs;
        private CharSpecialThings charThings;

        //Bu oyun koordinat birimine göre hesaplanmıştır.
        private static int MAXIMUM_MINIMAP_DISTANCE = 50;

        private static int MIDDLE_BOTTOM_MAX_DISTANCE = 264;
        private static int MIDDLE_TOP_MAX_DISTANCE = 296;
        private static int MIDDLE_LEFT_MAX_DISTANCE = 400;
        private static int MIDDLE_RIGHT_MAX_DISTANCE = 400;

        //330 -167 yukarı ve aşağı için 
        private static int MAX_UP_CLICK_PIXEL = 157;//4 oyun koordinat uzaklığına denk geliyor
        private static int MAX_DOWN_CLICK_PIXEL = 157;
        //408 - 67 aşağı ve yukarı için
        private static int MAX_LEFT_CLICK_PIXEL = 331;//9 oyun koordinat uzaklığına denk geliyor 
        private static int MAX_RIGHT_CLICK_PIXEL = 331;
      //  private Point middlePoint = new Point(408, 330);
        public CharMovement(ImageObjects imagesObject, GameAlphabetDetecter gameAlphabet) : base(imagesObject)
        {
            this.imagesObject = imagesObject;
            this.gameAlphabet = gameAlphabet;
            screenShot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imagesObject);
            inputs = new GameInputHandler();
            charThings = new CharSpecialThings(imagesObject);
        }

       
        /// <summary>
        /// Burada karakterin mini haritaya göre yukarı baktığı varsayılarak
        /// yazılmıştır.Karakteri mini harita bakış açısı yukarı doğru olmalıdır.
        /// </summary>
        /// <param name="pointPath">Destination point/param>
        /// <returns></returns>
        public bool MoveCharWithMouseClick(Point pointPath)
        {
            if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("MoveCharWithMouseClick function is returned");
                return false; 
            }
            Point pointCurrentPos = ReadMiniMapCoordinates();

            Point pointMouseClick = new Point(coor.PointGameMiddlePoint().X, coor.PointGameMiddlePoint().Y);

            int distanceX = pointCurrentPos.X - pointPath.X;
            int distanceY = pointCurrentPos.Y - pointPath.Y;
            //eğer varılacak yer sol tarafta kalıyorsa
            if (distanceX >= 2 )
            {
                //eğer tıklanacak yer uzaklığı 9 oyun koordinatından büyük ise
                if(Math.Abs(distanceX) >= 9)
                {
                    pointMouseClick.X = coor.PointGameMiddlePoint().X - MAX_LEFT_CLICK_PIXEL;
                }
                else
                {
                    //ters orantı ile ne kadar uzaklık farkı varsa bul
                    int result = (MAX_LEFT_CLICK_PIXEL * Math.Abs(distanceX))/9;
                    pointMouseClick.X = coor.PointGameMiddlePoint().X - result;
                }
            }
            //eğer varılacak yer sağ tarafta kalıyorsa
            else if (distanceX <= -2)
            {
                //eğer tıklanacak yer uzaklığı 9 oyun koordinatından büyük ise
                if (Math.Abs(distanceX) >= 9)
                {
                    pointMouseClick.X = coor.PointGameMiddlePoint().X + MAX_RIGHT_CLICK_PIXEL;
                }
                else
                {
                    //ters orantı ile ne kadar uzaklık farkı varsa bul
                    int result = (MAX_RIGHT_CLICK_PIXEL * Math.Abs(distanceX)) / 9;
                    pointMouseClick.X = coor.PointGameMiddlePoint().X + result;
                }
            }//eğer x koordinatı varmış ise orta x değere eşitle
            else
            {
                pointMouseClick.X = coor.PointGameMiddlePoint().X;
            }

            //eğer varılacak yer yukarıda kalıyorsa
            if (distanceY >= 2)
            {
              //  DebugPfCnsl.println("distanceY >= 3 y çalişti");
                //eğer tıklanacak yer uzaklığı 4 oyun koordinatından büyük ise
                if (Math.Abs(distanceY) >= 4)
                {
                    pointMouseClick.Y = coor.PointGameMiddlePoint().Y - MAX_UP_CLICK_PIXEL;
                }
                else
                {
                    //ters orantı ile ne kadar uzaklık farkı varsa bul
                    int result = (MAX_UP_CLICK_PIXEL * Math.Abs(distanceY)) / 4;
                    pointMouseClick.Y = coor.PointGameMiddlePoint().Y - result;
                }
            }
            //eğer varılacak yer aşağıda kalıyorsa
            else if(distanceY <= -2)
            {
              //  DebugPfCnsl.println("distanceY <= 3 y çalişti");
                //eğer tıklanacak yer uzaklığı 4 oyun koordinatından büyük ise
                if (Math.Abs(distanceY) >= 4)
                {
                    pointMouseClick.Y = coor.PointGameMiddlePoint().Y + MAX_UP_CLICK_PIXEL;
                }
                else
                {
                    //ters orantı ile ne kadar uzaklık farkı varsa bul
                    int result = (MAX_UP_CLICK_PIXEL * Math.Abs(distanceY)) / 4;
                    pointMouseClick.Y = coor.PointGameMiddlePoint().Y + result;
                }
            }
            else
            {
              //  DebugPfCnsl.println("else y çalişti");
                pointMouseClick.Y = coor.PointGameMiddlePoint().Y;
            }

          
            if(pointMouseClick.X != coor.PointGameMiddlePoint().X ||
                pointMouseClick.Y != coor.PointGameMiddlePoint().Y)
            {
                if(pointMouseClick.X >= coor.RectMetin2GameScreen().X + coor.RectMetin2GameScreen().Width ||
                    pointMouseClick.Y >= coor.RectMetin2GameScreen().Y + coor.RectMetin2GameScreen().Height)
                {
                    DebugPfCnsl.println("sınır dışı tıklama");
                    return false;
                }
               // DebugPfCnsl.println("tıklanan nokta = " + pointMouseClick.ToString());
                inputs.MouseMoveAndPressLeft(pointMouseClick.X, pointMouseClick.Y);

                if(!TimerGame.IS_PC_SLOW)
                {
                    TimerGame.SleepRandom(100, 150);
                }
                
                return false;
            }

            return true;
        }

        public bool StartTravelling(Point[] pointsDestination )
        {
            if(ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("StartTravelling is returned");
                return false;
            }
            charThings.OpenCloseInventory(true);
            charThings.OpenCloseInventory(false);
            charThings.OpenCloseSettingButton(false);


            Point pointCurrentPos = ReadMiniMapCoordinates();

            if (pointCurrentPos != Point.Empty)
            {
                for (int comparision = 0; comparision < pointsDestination.Length; comparision++)
                {
                    TimerGame timerRotationControl = new TimerGame();

                    //Hangi noktaya daha yakınsak oradan başlıyalım
                    if (comparision + 1 < pointsDestination.Length)
                    {
                        int distanceXfirstIndex = Math.Abs(pointsDestination[comparision].X - pointCurrentPos.X);
                        int distanceYfirstIndex = Math.Abs(pointsDestination[comparision].Y - pointCurrentPos.Y);

                        int distanceXsecondIndex = Math.Abs(pointsDestination[comparision + 1].X - pointCurrentPos.X);
                        int distanceYsecondIndex = Math.Abs(pointsDestination[comparision + 1].Y - pointCurrentPos.Y);

                        int totalFirstIndex = distanceXfirstIndex + distanceYfirstIndex;
                        int totalSecondIndex = distanceXsecondIndex + distanceYsecondIndex;

                       
                        //Eğer ilk indeks diğerinden küçük ise bu indeksten başla
                        if (totalFirstIndex < totalSecondIndex)
                        {
                           
                            // DebugPfCnsl.println("başlanılan indeks = " + comparision);
                            for (int rotation = comparision; rotation < pointsDestination.Length; rotation++)
                            {
                                while (!MoveCharWithMouseClick(pointsDestination[rotation]))
                                {
                                    if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                                    {
                                        DebugPfCnsl.println("StartTravelling is returned");
                                        return false;
                                    }
                                    if(!timerRotationControl.CheckDelayTimeInSecond(20))
                                    {
                                        charThings.OpenCloseInventory(true);
                                        charThings.OpenCloseInventory(false);
                                        charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);

                                        while(!ThreadGlobals.CanEnergyCristalRightNow())
                                        {
                                            if (ThreadGlobals.CheckGameIsStopped())
                                            {
                                                DebugPfCnsl.println("StartTravelling is returned");
                                                return false;
                                            }
                                        }
                                        TimerGame.SleepRandom(3000, 4000);

                                        
                                    }
                                }
                                timerRotationControl.SetStartedSecondTime();
                            }
                            DebugPfCnsl.println("bütün hedefler başarı ile tamamlanmıştır.");
                            //bütün hedefler başarı ile tamamlanmıştır.
                            return true;
                        }
                    }
                    //burada son nokta anlamına gelir.
                    else
                    {
                        while (!MoveCharWithMouseClick(pointsDestination[comparision]))
                        {
                            if (ThreadGlobals.CheckGameIsStopped() || !ThreadGlobals.isSettingButtonSeemed)
                            {
                                DebugPfCnsl.println("StartTravelling is returned");
                                return false;
                            }
                            if (!timerRotationControl.CheckDelayTimeInSecond(20))
                            {
                                charThings.OpenCloseInventory(true);
                                charThings.OpenCloseInventory(false);
                                charThings.SettingButtonClick(SettingButtonPrefers.EXIT_BUTTON);

                                while (!ThreadGlobals.CanEnergyCristalRightNow())
                                {
                                    if (ThreadGlobals.CheckGameIsStopped())
                                    {
                                        DebugPfCnsl.println("StartTravelling is returned");
                                        return false;
                                    }
                                }
                                TimerGame.SleepRandom(3000, 4000);

                                timerRotationControl.SetStartedSecondTime();
                            }
                        }
                    }
                }
          
            }
            else
            {
                charThings.OpenCloseInventory(true);
                charThings.OpenCloseInventory(false);
                charThings.OpenCloseSettingButton(false);
                return StartTravelling(pointsDestination);
            }
            return true;
        }

        public Point ReadMiniMapCoordinates()
        {
            //Move mouse cursur to minimap coordinate place 
            inputs.MouseMove(coor.PointMiniMapCharSymbol().X, coor.PointMiniMapCharSymbol().Y);

            Point pointResult = Point.Empty;
            if (ThreadGlobals.isEnergyCristalStopped || !ThreadGlobals.isSettingButtonSeemed)
            {
                DebugPfCnsl.println("ReadMiniMapCoordinates is returned");
                return Point.Empty;
            }

            string result = gameAlphabet.DetectGameTextSetableVerticalGap(ColorGame.PLAYER_YELLOW_COLOR,
               coor.RectMiniMapCoordianetesArea(),3);
            if (result != string.Empty)
            {
               // DebugPfCnsl.println("okunan değerler = " + result);

                string[] parseString = result.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parseString.Length == 2)
                {
                   // DebugPfCnsl.PrintArray(parseString);

                   int x = ConvertMiniMapCoordinate(parseString[0]);
                   int y = ConvertMiniMapCoordinate(parseString[1]);

                    if (x > 0 && y > 0)
                    {
                        pointResult = new Point(x, y);
                      //  DebugPfCnsl.println("ayıklanma sonucu = " + pointResult.ToString());
                        return pointResult;
                    }
                }
            }
            else
            {
                return ReadMiniMapCoordinates();
            }


            return pointResult;
        }

        public int ConvertMiniMapCoordinate(string value)
        {
            if(value != string.Empty)
            {
                char[] chars = value.ToCharArray();

                int digitCounter = 3;
                int resultInt = 0;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] >= '0' && chars[i] <= '9')
                    {
                        if (digitCounter == 3)
                        {
                          
                            resultInt = int.Parse(chars[i].ToString()) * 100;
                           
                            digitCounter--;
                        }
                        else if (digitCounter == 2)
                        {
                          
                            resultInt += (int.Parse(chars[i].ToString()) * 10);
                         
                            digitCounter--;
                        }
                        else if (digitCounter == 1)
                        {
                           
                            resultInt += int.Parse(chars[i].ToString());
                            
                            return resultInt;
                        }
                    }
                }
                return resultInt;
            }
            else
            {
                DebugPfCnsl.println("ConvertMiniMapCoordinate fonksiyondaki parametre boş string");
                return 0;   
            }
            
        }
    }
}
