using Metin2AutoFishCSharp.Sources.ChatHandler;
using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.CharacterHandle;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.CharacterHandle
{
    internal class CharPickUpItems : CharInfo
    {

        private ScreenShotWinAPI screenShot;
        private ImageObjects imagesObject;
        private GameAlphabetDetecter alphabetDetect;
        private GameInputHandler inputs;
        private GameObjectCoordinates coor;

        private int countFile = 0;

        private readonly string detectEjderhaTasi = "ejderha taşı";
        private readonly Rectangle rectEjderhaTasiSample = new Rectangle(0, 0, 93, 16);
        //düşen itemdeki yazan sarı yazı ile beyaz yazı arasındaki dikey ve sarı yazı başlangıç kısmı ile
        //Beyaz item yazı başlangıç arasındaki mesafe dikey ve yatay olarak dikkat et =  {X=451,Y=320,Width=23,Height=7}
        //sarı yazıyı tespit ettikten sonra düşen itemin yazısını kontrol etmek için dikey olarak en az fazla 7 boşluk ver
        public CharPickUpItems(ImageObjects imagesObject, GameAlphabetDetecter alphabetDetect) : base(imagesObject)
        {
            this.imagesObject = imagesObject;
            this.alphabetDetect = alphabetDetect;
            screenShot = new ScreenShotWinAPI();
            inputs = new GameInputHandler();
            coor = new GameObjectCoordinates(imagesObject);
        }

        /// <summary>
        /// This function just developed for pick up "Dragon Stone Piece"
        /// This may be not work well for another items
        /// you need to modify detected string rectangle widht if
        /// you want to add another items
        /// </summary>
        /// <param name="itemName"></param>
        public void PickUpWantedItem(string itemName)
        {
           
            List<Rectangle> listRectScannedItems = new List<Rectangle>();
            List<Rectangle> listRectUnScannableName = new List<Rectangle>();

            Rectangle rectScanningArea = coor.RectItemPickUpDetectArea();
           // int[] targetImageArray = screenShot.ImageArraySpecifiedArea(rectScanningArea);
            Bitmap bitmapTargetIcon = screenShot.CaptureSpecifiedScreen(rectScanningArea);
            int[] targetImageArray = screenShot.ConvertBitmapToArray(bitmapTargetIcon);

            bool mouseIsPressed = false;
           // DebugPfCnsl.println("ss çekildi filename = test"+ countFile++ +".png         ");
            //FileHandler.SaveImageAsPng(bitmapTargetIcon, "test" + countFile++ + ".png", PathWayStruct.PATH_SCREENSHOTS);
            

            for (int x = 0; x < rectScanningArea.Width; x++)
            {
                for (int y = 0; y < rectScanningArea.Height; y++)
                {
                    if (ThreadGlobals.isLevelFarmStopped || !ThreadGlobals.isSettingButtonSeemed)
                    {
                        DebugPfCnsl.println("DetectWantedObjectForPickUp returned");
                        if(mouseIsPressed)
                        {
                            inputs.MouseRelease();
                        }
                        return;
                    }
                    if (listRectScannedItems.Count > 0)
                    {
                        for (int i = 0; i < listRectScannedItems.Count; i++)
                        {
                            if (listRectScannedItems[i].Contains(x + rectScanningArea.X,y + rectScanningArea.Y))
                            {
                                if(x + listRectScannedItems[i].Width < rectScanningArea.Width)
                                {
                                    x += listRectScannedItems[i].Width;
                                }
                                else
                                {
                                    x = rectScanningArea.Width - 1;
                                }
                               
                            }
                        }
                    }

                    if (listRectUnScannableName.Count > 0)
                    {
                        for (int i = 0; i < listRectUnScannableName.Count; i++)
                        {
                            if (listRectUnScannableName[i].Contains(x + rectScanningArea.X, y + rectScanningArea.Y))
                            {
                                if (x + listRectUnScannableName[i].Width < rectScanningArea.Width)
                                {
                                    x += listRectUnScannableName[i].Width;
                                }
                                else
                                {
                                    x = rectScanningArea.Width - 1;
                                }

                            }
                        }
                    }

                    //First We scane player item name color
                    if (imagesObject.CompareTwoRgbIntAdvanced(ColorGame.YELLOW_PICKUP_PLAYER_NAME,
                        targetImageArray[(y * rectScanningArea.Width) + x]))
                    {
                       
                        //Secand check item name belongs to your char name
                        if(x + CharRectBounds.Width +2 <= rectScanningArea.Width && 
                            y + CharRectBounds.Height +1 <= rectScanningArea.Height)
                        {
                            Rectangle rectCharNameBound = new Rectangle(x + rectScanningArea.X -2, rectScanningArea.Y + y - 4,
                                CharRectBounds.Width +4, CharRectBounds.Height +5);

                            int[] clippedYellowName = imagesObject.ClipIntArray(targetImageArray, rectScanningArea, rectCharNameBound);

                            Rectangle resulYellowRect = imagesObject.FindBorderAreaForWantedColorV2(ColorGame.YELLOW_PICKUP_PLAYER_NAME, clippedYellowName
                                ,rectCharNameBound);

                            
                            if(resulYellowRect != Rectangle.Empty)
                            {
                              // DebugPfCnsl.println("resulYellowRect = " + resulYellowRect.ToString() );
                                // DebugDrawingHandle.DrawWantedObjectToScreen(resultRect);
                                int[] clippedYellowCharName = imagesObject.ClipIntArray(clippedYellowName, rectCharNameBound, resulYellowRect);

                                if (alphabetDetect.DetectGameTextWithProvidedImage(clippedYellowCharName, resulYellowRect,ColorGame.YELLOW_PICKUP_PLAYER_NAME).
                                    Contains(CharNameString))
                                {
                                    //DebugPfCnsl.println("düşen item tespit edildi");
                                   // DebugPfCnsl.println("resulYellowRect.X = " + resulYellowRect.X + " rectEjderhaTasiSample.Width/2 " + rectEjderhaTasiSample.Width / 2
                                   //     + " resulYellowRect.Y " + resulYellowRect.Y + "rectScanningArea.Height + " + rectScanningArea.Height);
                                    if(resulYellowRect.X - rectEjderhaTasiSample.Width/2 >= 0 && resulYellowRect.Y + 6 <= rectScanningArea.Y + rectScanningArea.Height)
                                    {
                                        Rectangle rectItemWhite = new Rectangle(resulYellowRect.X - rectEjderhaTasiSample.Width/2,
                                            resulYellowRect.Y + 5,CharRectBounds.Width + rectEjderhaTasiSample.Width/2,rectEjderhaTasiSample.Height + 5);
                                       // DebugDrawingHandle.DrawWantedObjectToScreen(rectItemWhite);
                                        int[] clippedWhiteItemName = imagesObject.ClipIntArray(targetImageArray, rectScanningArea, rectItemWhite);

                                        Rectangle resultItemWhite = imagesObject.FindBorderAreaForWantedColorV2(ColorGame.CHAT_WHITE_COLOR,
                                            clippedWhiteItemName, rectItemWhite);

                                        if (resultItemWhite != Rectangle.Empty)
                                        {
                                          //  DebugDrawingHandle.DrawWantedObjectToScreen(resultItemWhite);

                                            int[] detectWhiteWords = imagesObject.ClipIntArray(clippedWhiteItemName, rectItemWhite,resultItemWhite);
                                            if (alphabetDetect.DetectGameTextWithProvidedImage(detectWhiteWords, resultItemWhite,
                                                ColorGame.CHAT_WHITE_COLOR).Contains(itemName)) 
                                            {
                                                inputs.MouseMove(resultItemWhite.X + resultItemWhite.Width / 2,
                                                 resultItemWhite.Y + resultItemWhite.Height / 2);
                                                inputs.MouseDown();
                                               // TimerGame.SleepRandom(1000, 1500);
                                                inputs.MouseMove(resultItemWhite.X, resultItemWhite.Y + 20);  
                                                mouseIsPressed = true;
                                               // inputs.MouseRelease();
                                                //inputs.MouseMoveAndPressLeft(resultItemWhite.X + resultItemWhite.Width / 2,
                                                //  resultItemWhite.Y + resultItemWhite.Height / 2);
                                                //  inputs.MouseMove(resultItemWhite.X, resultItemWhite.Y + 50);
                                                //rectScanningArea = new Rectangle(resultItemWhite.X - 100, resultItemWhite.Y - 75,
                                               //     2 * rectEjderhaTasiSample.Width, 150);

                                                // bitmapTargetIcon = screenShot.CaptureSpecifiedScreen(rectScanningArea);
                                                // FileHandler.SaveImageAsPng(bitmapTargetIcon, "test" + countFile++ + ".png", PathWayStruct.PATH_SCREENSHOTS);
                                               // targetImageArray = screenShot.ConvertBitmapToArray(bitmapTargetIcon);
                                                listRectScannedItems.Clear();
                                                listRectUnScannableName.Clear();
                                                targetImageArray = screenShot.ImageArraySpecifiedArea(rectScanningArea);

                                                x = 0; y = 0; 
                                            }
                                            else
                                            {
                                                //Add scanned area to list and don't rescanne again
                                               //6 means padding vertical amount between white and yellow words
                                                if(rectItemWhite.Width - resulYellowRect.Width > 0)
                                                {
                                                    listRectScannedItems.Add(new Rectangle(rectItemWhite.X, resulYellowRect.Y,
                                                    rectItemWhite.Width, resulYellowRect.Height + rectItemWhite.Height + 6));
                                                }
                                                else
                                                {
                                                    listRectScannedItems.Add(new Rectangle(resulYellowRect.X, resulYellowRect.Y,
                                                      resulYellowRect.Width, resulYellowRect.Height + rectItemWhite.Height +6));

                                                }
                                                DebugPfCnsl.println("Added one unwanted rectangle and count = " +listRectScannedItems.Count);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        DebugPfCnsl.println("hatali");
                                    }

                                   /* inputs.MouseMoveAndPressLeft(resulYellowRect.X + resulYellowRect.Width/2,
                                        resulYellowRect.Y + resulYellowRect.Height/2);
                                    inputs.MouseMove(resulYellowRect.X, resulYellowRect.Y + 50);
                                    targetImageArray = screenShot.ImageArraySpecifiedArea(rectScanningArea);
                                    
                                    x = 0; y = 0;*/

                                }
                                else
                                {
                                   // DebugPfCnsl.println("rect value = " + resulYellowRect.ToString());
                                    if (x + resulYellowRect.Width < rectScanningArea.Width)
                                    {
                                        //DebugPfCnsl.println("çaliti");
                                        listRectUnScannableName.Add(resulYellowRect);
                                        x += resulYellowRect.Width;
                                        DebugPfCnsl.println("Added one Unscannable name rectangle and count = " + listRectUnScannableName.Count);
                                    }

                                }
                            }
                           
                           
                        }
                    }

                }
            }
            if (mouseIsPressed)
            {
                inputs.MouseRelease();
            }
           
        }

    }
}
