using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources.CoordinatesHandler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp.Sources.ImageHandle
{
    public enum ImageSensibilityLevel
    {
        SENSIBILTY_HIGH = 1,
        SENSIBILTY_MED = 2,
        SENSIBILTY_LOW = 3,

    }
    internal class ImageProcess
    {
        protected ScreenShotWinAPI screenshot;
        public ImageProcess()
        {
            screenshot = new ScreenShotWinAPI();
        }

        public bool CompareBitmaps(Bitmap bmp1, Bitmap bmp2, ImageSensibilityLevel level)
        {
            if (bmp1.Size != bmp2.Size)
            {
                throw new ArgumentException("Images must have the same dimensions");
            }

            int width = bmp1.Width;
            int height = bmp1.Height;
            int stride = width * sizeof(int); // Her satırın bellekte kapladığı byte sayısı

            // Görüntülerin belleğe kilitlenmesi (Lock)
            BitmapData bmpData1 = bmp1.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            BitmapData bmpData2 = bmp2.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            int similarity = 0;

            unsafe
            {
                byte* ptr1 = (byte*)bmpData1.Scan0;
                byte* ptr2 = (byte*)bmpData2.Scan0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < stride; x += sizeof(int))
                    {
                        int val1 = *((int*)(ptr1 + (y * bmpData1.Stride) + x));
                        int val2 = *((int*)(ptr2 + (y * bmpData2.Stride) + x));

                        if (CompareTwoRgbIntAdvanced(val1, val2))
                        {
                            similarity++;
                        }
                    }
                }
            }

            // Görüntülerin bellek kilidini açma (Unlock)
            bmp1.UnlockBits(bmpData1);
            bmp2.UnlockBits(bmpData2);

            //similarity /= (width * height); // Benzerlik oranını hesapla


            return similarity >= ((bmp1.Width * bmp2.Height) / ((int)level));
        }



        public bool CompareTwoRgbIntAdvanced(int source, int comparable)
        {
            int[] arrayFieldColorRed = new int[2];
            int[] arrayFieldColorGreen = new int[2];
            int[] arrayFieldColorBlue = new int[2];

            for (int index = 0; index < 2; index++)
            {
                if (index == 0)
                {
                    arrayFieldColorRed[index] = (source & 0x00ff0000) >> 16;
                    arrayFieldColorGreen[index] = (source & 0x0000ff00) >> 8;
                    arrayFieldColorBlue[index] = source & 0x000000ff;
                }
                else
                {
                    arrayFieldColorRed[index] = (comparable & 0x00ff0000) >> 16;
                    arrayFieldColorGreen[index] = (comparable & 0x0000ff00) >> 8;
                    arrayFieldColorBlue[index] = comparable & 0x000000ff;
                }

            }

            if (Math.Abs(arrayFieldColorRed[0] - arrayFieldColorRed[1]) < 5
                    && Math.Abs(arrayFieldColorGreen[0] - arrayFieldColorGreen[1]) < 5
                    && Math.Abs(arrayFieldColorBlue[0] - arrayFieldColorBlue[1]) < 5)
            {
                return true;
            }
            return false;

        }

        public bool CompareIntBetweenColors(int argbArray, params int[] wantedColors)
        {

            int[] redWantedColor = FillArrayRGB(wantedColors, ColorValue.R);
            int[] greenWantedColor = FillArrayRGB(wantedColors, ColorValue.G);
            int[] blueWantedColor = FillArrayRGB(wantedColors, ColorValue.B);

            int redMinValue = FindMinimum(redWantedColor);
            int redMaxValue = FindMaximum(redWantedColor);

            int greenMinValue = FindMinimum(greenWantedColor);
            int greenMaxValue = FindMaximum(greenWantedColor);

            int blueMinValue = FindMinimum(blueWantedColor);
            int blueMaxValue = FindMaximum(blueWantedColor);



            int redTarget = (argbArray >> 16) & 0xff;
            int greenTarget = (argbArray >> 8) & 0xff;
            int blueTarget = argbArray & 0xff;



            if (redTarget > redMinValue && greenTarget > greenMinValue && blueTarget > blueMinValue)
            {
                if (redTarget < redMaxValue && greenTarget < greenMaxValue && blueTarget < blueMaxValue)
                { return true; }
            }


            return false;
        }

        private int[] FillArrayRGB(int[] argbArray, ColorValue rgb)
        {
            int[] resultArray = new int[argbArray.Length];
            for (int i = 0; i < argbArray.Length; i++)
            {
                if (rgb == ColorValue.R)
                {
                    resultArray[i] = (argbArray[i] >> 16) & 0xFF;
                }
                else if (rgb == ColorValue.G)
                {
                    resultArray[i] = (argbArray[i] >> 8) & 0xFF;
                }
                else if (rgb == ColorValue.B)
                {
                    resultArray[i] = argbArray[i] & 0xFF;
                }
            }
            return resultArray;
        }

        public static void FillArrays<T>(T[] arrayStored, T[] arrayTarget)
        {
            if (arrayStored.Length != arrayTarget.Length) throw new ArgumentException("Both of them must be equal to each other");

            for (int i = 0; i < arrayStored.Length; i++)
            {
                arrayStored[i] = arrayTarget[i];
            }
        }
        private int FindMinimum(int[] array)
        {
            if (array == null || array.Length <= 0) return 0; 
            int min = array[0];
            foreach (int number in array)
            {
                if (number < min)
                {
                    min = number;
                }
            }
            return min;
        }
        private int FindMaximum(int[] array)
        {
            if(array == null || array.Length <= 0)return 0;
            int max = array[0];
            foreach (int number in array)
            {
                if (number > max)
                {
                    max = number;
                }
            }
            return max;
        }

        public bool CompareTwoArrayAdvanced(int[] sourceArray, int[] comparableArray, ImageSensibilityLevel sensiblityLevel)
        {
            int equalityNum = 0;

            if (comparableArray != null && comparableArray.Length > 0 && sourceArray != null && sourceArray.Length > 0)
            {
                int[] arrayColorRed = new int[2];
                int[] arrayColorGreen = new int[2];
                int[] arrayColorBlue = new int[2];


                if (sourceArray.Length > comparableArray.Length)
                {


                    return false;
                }
                else
                {
                    for (int i = 0; i < sourceArray.Length; i++)
                    {
                        for (int index = 0; index < 2; index++)
                        {
                            if (index == 0)
                            {
                                arrayColorRed[index] = (sourceArray[i] & 0x00ff0000) >> 16;
                                arrayColorGreen[index] = (sourceArray[i] & 0x0000ff00) >> 8;
                                arrayColorBlue[index] = sourceArray[i] & 0x000000ff;
                            }
                            else
                            {
                                arrayColorRed[index] = (comparableArray[i] & 0x00ff0000) >> 16;
                                arrayColorGreen[index] = (comparableArray[i] & 0x0000ff00) >> 8;
                                arrayColorBlue[index] = comparableArray[i] & 0x000000ff;
                            }

                        }

                        if (Math.Abs(arrayColorRed[0] - arrayColorRed[1]) < 4 && Math.Abs(arrayColorGreen[0] - arrayColorGreen[1]) < 4
                                && Math.Abs(arrayColorBlue[0] - arrayColorBlue[1]) < 4)
                        {
                            // System.out.println("22array color 0 = " + arrayColorRed[0] + " 22 array color 1 = " + arrayColorRed[1]);
                            // System.out.println("index num =  " + i + " return false ");
                            equalityNum++;
                        }
                    }

                }
            }
            //System.out.println("comparable array length = " + comparableArray.length + "equality var = " + equalityNum);
            return equalityNum >= ((sourceArray.Length / (int)sensiblityLevel) - ((5 * sourceArray.Length) / 100));
        }

        public bool compareTwoArrayQuickly(int[] source, int[] comparable)
        {

            if (comparable == null || source == null || source.Length <= 0 || comparable.Length <= 0)
            {
                //DebugPfCnsl.println("compareTwoArrayQuickly.. method arrays or array is null");
                return false;
            }
            if (source.Length > comparable.Length)
            {
                for (int i = 0; i < comparable.Length; i++)
                {
                    if (source[i] != comparable[i])
                    //if(!CompareTwoRgbIntAdvanced(source[i], comparable[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] != comparable[i])
                   // if (!CompareTwoRgbIntAdvanced(source[i], comparable[i]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool compareTwoArrayQuicklyBool(bool[] source, bool[] comparable)
        {

            if (comparable == null || source == null || source.Length <= 0 || comparable.Length <= 0)
            {
                DebugPfCnsl.println("compareTwoArrayQuickly.. method arrays or array is null");
                return false;
            }
            if (source.Length > comparable.Length)
            {
                for (int i = 0; i < comparable.Length; i++)
                {
                    if (source[i] != comparable[i])
                    {
                        return false;
                    }
                }
            }
            else
            {
                for (int i = 0; i < source.Length; i++)
                {
                    if (source[i] != comparable[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        /// <summary>
        /// Compares an array according to
        /// <paramref name="wantedColor"/> color value.
        /// </summary>
        /// <param name="wantedColor"></param>
        /// <param name="comparableArray"></param>
        /// <returns> new int array which its size is CHANGED !!</returns>
        public int[] RecordWantedColorIntArray(int wantedColor, int[] comparableArray)
        {
            if (comparableArray == null || comparableArray.Length <= 0)
            {
                DebugPfCnsl.println("RecordWantedColor.. method comparable array is NULL");
                return null;
            }

            int equalityIndex = 0;
            int[] equalityArray = null;

            int colorRed = (wantedColor & 0x00ff0000) >> 16;
            int colorGreen = (wantedColor & 0x0000ff00) >> 8;
            int colorBlue = wantedColor & 0x000000ff;

            for (int i = 0; i < comparableArray.Length; i++)
            {
                if (Math.Abs(colorRed - ((comparableArray[i] & 0x00ff0000) >> 16)) < 3 &&
                      Math.Abs(colorGreen - ((comparableArray[i] & 0x0000ff00) >> 8)) < 3 &&
                        Math.Abs(colorBlue - ((comparableArray[i] & 0x000000ff))) < 3)
                {
                    equalityIndex++;
                }
            }

            equalityArray = new int[equalityIndex];
            equalityIndex = 0;
            for (int i = 0; i < comparableArray.Length; i++)
            {
                if (Math.Abs(colorRed - ((comparableArray[i] & 0x00ff0000) >> 16)) < 3 &&
                      Math.Abs(colorGreen - ((comparableArray[i] & 0x0000ff00) >> 8)) < 3 &&
                        Math.Abs(colorBlue - ((comparableArray[i] & 0x000000ff))) < 3)
                {
                    equalityArray[equalityIndex++] = i;
                }

            }

            return equalityArray;
        }
        /// <summary>
        /// Record an bool arraye according to <paramref name="wantedColor"/>
        /// color variable
        /// </summary>
        /// <param name="wantedColor"></param>
        /// <param name="comparableArray"></param>
        /// <returns> new bool array which it size is the same with 
        /// <paramref name="comparableArray"/> array </returns>
        public bool[] RecordWantedColorAsBool(int wantedColor, int[] comparableArray)
        {
            if (comparableArray == null)
            {
                DebugPfCnsl.println("RecordWantedColor.. method comparable array is NULL");
                return null;
            }
            int colorRed = 0;
            int colorGreen = 0;
            int colorBlue = 0;

            bool[] equalityArray = new bool[comparableArray.Length];

            colorRed = (wantedColor & 0x00ff0000) >> 16;
            colorGreen = (wantedColor & 0x0000ff00) >> 8;
            colorBlue = wantedColor & 0x000000ff;

            for (int i = 0; i < comparableArray.Length; i++)
            {
                if (Math.Abs(colorRed - ((comparableArray[i] & 0x00ff0000) >> 16)) < 4 &&
                        Math.Abs(colorGreen - ((comparableArray[i] & 0x0000ff00) >> 8)) < 4 &&
                        Math.Abs(colorBlue - ((comparableArray[i] & 0x000000ff))) < 4)
                {
                    equalityArray[i] = true;
                }
                else
                {
                    equalityArray[i] = false;
                }
            }
            return equalityArray;
        }
        public Rectangle[] FindAllImagesBoolArrays(bool[] targetImage, Rectangle targetRect, Rectangle scannedArea,int colorValue)
        {
            if (targetImage.Length != targetRect.Width * targetRect.Height)
            {
                throw new ArgumentException("targetImage array size must be equal to targetRect width * height");
            }
            Rectangle ssBound;

            if (scannedArea == Rectangle.Empty)
            {
                ssBound = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                ssBound = scannedArea;
            }


            var rectangles = new List<Rectangle>();
            int[] fullScreenImage = screenshot.ImageArraySpecifiedArea(ssBound);
            bool[] sourceImage = RecordWantedColorAsBool(colorValue, fullScreenImage);

            for (int y = 0; y < ssBound.Height; y++)
            {
                for (int x = 0; x < ssBound.Width; x++)
                {

                    if (IsMatchBoolArrays(targetImage, targetRect, sourceImage, ssBound, x, y))
                    {
                        if (scannedArea != Rectangle.Empty)
                        {
                            rectangles.Add(new Rectangle(x + ssBound.X + CheckGameCoordinate.currentScreenGamePoint.X,
                                y + ssBound.Y + CheckGameCoordinate.currentScreenGamePoint.Y,
                                targetRect.Width, targetRect.Height));
                        }
                        else
                        {
                            rectangles.Add(new Rectangle(x, y, targetRect.Width, targetRect.Height));
                        }

                    }
                }
            }
            return rectangles.ToArray();
        }
        public Rectangle[] FindAllImagesBoolArrays(bool[] targetImage, Rectangle targetRect, bool[] scannedImage,Rectangle scannedArea)
        {
              //DebugPfCnsl.println("targetRecct  = " + targetRect.ToString() +
              //   "  scanRect  = " + scannedArea.ToString());
            if (targetImage.Length != targetRect.Width * targetRect.Height)
            {
                throw new ArgumentException("targetImage array size must be equal to targetRect width * height");
            }
            if(scannedImage.Length != scannedArea.Width * scannedArea.Height)
            {
                throw new ArgumentException("scannedImage array size must be equal to scanedArea rect width * height");
            }
            Rectangle ssBound;

            if (scannedArea == Rectangle.Empty)
            {
                throw new ArgumentException("scannedArea param cannot be empty!!");
            }
            else
            {
                ssBound = scannedArea;
            }

           // DebugPfCnsl.println("targetImage length = " + targetImage.Length +
           //    "  scanImage Lenth = " + scannedImage.Length);

          //  DebugPfCnsl.println("targetRecct  = " + targetRect.ToString() +
           //     "  scanRect  = " + scannedArea.ToString());

            var rectangles = new List<Rectangle>();
           
            bool[] sourceImage = scannedImage;

            for (int y = 0; y < ssBound.Height; y++)
            {
                for (int x = 0; x < ssBound.Width; x++)
                {

                    if (IsMatchBoolArrays(targetImage, targetRect, sourceImage, ssBound, x, y))
                    {
                        if (scannedArea != Rectangle.Empty)
                        {
                            rectangles.Add(new Rectangle(x + ssBound.X + CheckGameCoordinate.currentScreenGamePoint.X,
                                y + ssBound.Y + CheckGameCoordinate.currentScreenGamePoint.Y,
                                targetRect.Width, targetRect.Height));
                        }
                        else
                        {
                            rectangles.Add(new Rectangle(x, y, targetRect.Width, targetRect.Height));
                        }

                    }
                }
            }
            return rectangles.ToArray();
        }
        public Rectangle[] FindAllImagesOnScreen(int[] targetImage, Rectangle targetRect, Rectangle scannedArea)
        {
            if (targetImage.Length != targetRect.Width * targetRect.Height)
            {
                // throw new ArgumentException("targetImage array size must be equal to targetRect width * height");
                DebugPfCnsl.println("FindAllImagesOnScreen is returned null");
                return null;
            }
            Rectangle ssBound;

            if (scannedArea == Rectangle.Empty)
            {
                ssBound = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                ssBound = scannedArea;
            }


            var rectangles = new List<Rectangle>();
            Bitmap fullScreenImage = screenshot.CaptureSpecifiedScreen(ssBound);

            for (int y = 0; y < fullScreenImage.Height; y++)
            {
                for (int x = 0; x < fullScreenImage.Width; x++)
                {

                    if (IsMatch(targetImage, targetRect, fullScreenImage, x, y))
                    {
                        if (scannedArea != Rectangle.Empty)
                        {
                            rectangles.Add(new Rectangle(x + ssBound.X + CheckGameCoordinate.currentScreenGamePoint.X,
                                y + ssBound.Y + CheckGameCoordinate.currentScreenGamePoint.Y,
                                targetRect.Width, targetRect.Height));
                        }
                        else
                        {
                            rectangles.Add(new Rectangle(x, y, targetRect.Width, targetRect.Height));
                        }

                    }
                }
            }
            return rectangles.ToArray();
        }
        public Rectangle FindImageOnScreen(int[] targetImage, Rectangle targetRect)
        {
            if (targetImage.Length != targetRect.Width * targetRect.Height)
            {
                //throw new ArgumentException("targetImage array size must be equal to targetRect width * height");

                DebugPfCnsl.println("FindImageOnScreen returned empty");
                return Rectangle.Empty;
            }

            Bitmap fullScreenImage = screenshot.CaptureScreen();

            for (int y = 0; y < fullScreenImage.Height; y++)
            {
                for (int x = 0; x < fullScreenImage.Width; x++)
                {

                    if (IsMatch(targetImage, targetRect, fullScreenImage, x, y))
                    {
                        return new Rectangle(x, y, targetRect.Width, targetRect.Height);
                    }
                }
            }
            return Rectangle.Empty;
        }

        private bool IsMatch(int[] targetImage, Rectangle targetRect, Bitmap comparableImage, int x, int y)
        {
            if (x + targetRect.Width > comparableImage.Width || y + targetRect.Height > comparableImage.Height)
            {
              //  DebugPfCnsl.println("IsMatch burada hata var");
                return false;
            }
                

            Color color = Color.White;
            for (int height = 0; height < targetRect.Height; height++)
            {
                for (int x2 = 0; x2 < targetRect.Width; x2++)
                {
                    color = comparableImage.GetPixel(x + x2, y + height);
                    int argb = color.ToArgb();
                    // if (argb != targetImage[height * targetRect.Width + x2])
                    if (!CompareTwoRgbIntAdvanced(argb, targetImage[height * targetRect.Width + x2]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool IsMatchIntArrays(int[] targetImage, Rectangle targetRect, int[] scanImage, Rectangle scanRect, int x, int y)
        {
            if (x + targetRect.Width > scanRect.Width || y + targetRect.Height > scanRect.Height)
                return false;

            int scanImageBaseValue = (y * scanRect.Width) + x;

            for (int height = 0; height < targetRect.Height; height++)
            {
                for (int x2 = 0; x2 < targetRect.Width; x2++)
                {

                    // if (argb != targetImage[height * targetRect.Width + x2])
                    if (!CompareTwoRgbIntAdvanced(scanImage[scanImageBaseValue + (height * scanRect.Width) + x2],
                        targetImage[height * targetRect.Width + x2]))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public bool IsMatchBoolArrays(bool[] targetImage, Rectangle targetRect, bool[] scanImage, Rectangle scanRect, int x, int y)
        {

            if (x + targetRect.Width > scanRect.Width || y + targetRect.Height > scanRect.Height)
                return false;
 
            int scanImageBaseValue = (y * scanRect.Width) + x;
            //int scanWidthCounter = 0;
            // int targetWidthCounter = 0;

            for (int height = 0; height < targetRect.Height; height++)
            {
                for (int x2 = 0; x2 < targetRect.Width; x2++)
                {
                    // try
                    // {

                    if (scanImage[scanImageBaseValue + (height * scanRect.Width) + x2] !=
                                           targetImage[(height * targetRect.Width) + x2])
                    {
                        return false;
                    }
                    /* if ((scanImage[scanImageBaseValue + scanWidthCounter + x2] !=
                                           targetImage[targetWidthCounter + x2]))
                     {
                         return false;
                     }*/
                    // }
                    /*  catch(IndexOutOfRangeException)
                      {
                          DebugPfCnsl.println("scanImageBaseValue = " + scanImageBaseValue
                              + " scanImage Length = " + scanImage.Length + "\n" +
                              " (height * scanRect.Height) = " + (height * scanRect.Height) + " x2 = "
                               + x2);
                          DebugPfCnsl.println("targetImage Lenght = " + targetImage.Length +
                              " height * targetRect.Width = " + (height * targetRect.Width) + "\n" +
                              " x2 Value = " + x2 );
                          throw new IndexOutOfRangeException();
                      }*/


                }

            }

            return true;
        }


        public Rectangle FindBorderAreaForWantedColor(int wantedColor, Rectangle scanWantedArea)
        {
            if (scanWantedArea.Width <= 0)
            {

                // throw new Exception("FindBorderAreaForWantedColor func scanWantedArea param " +
                //     "value width is less equal to zero");

                DebugPfCnsl.println("FindBorderAreaForWantedColor returned empty");
                return Rectangle.Empty;
            }

            int arrayIncrementCounter = 0;
            int maxYValue = int.MinValue;
            int maxXValue = int.MinValue;
            int minYValue = int.MaxValue;
            int minXValue = int.MaxValue;




            // int [] comparableArray = takeScreenShotReturnRGBarray(scanWantedArea);
            int[] comparableArray = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(scanWantedArea));


            for (int yPos = 0; yPos < scanWantedArea.Height; yPos++)
            {
                for (int xPos = 0; xPos < scanWantedArea.Width; xPos++)
                {
                    if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[xPos + arrayIncrementCounter]))
                    {
                        if (maxYValue < yPos)
                        {
                            maxYValue = yPos;
                        }
                        if (minYValue > yPos)
                        {
                            minYValue = yPos;
                        }
                        if (maxXValue < xPos)
                        {
                            maxXValue = xPos;
                        }
                        if (minXValue > xPos)
                        {
                            minXValue = xPos;
                        }

                    }
                }

                arrayIncrementCounter += scanWantedArea.Width;
            }
            if (maxXValue <= 0 && maxYValue <= 0)
            {
                return Rectangle.Empty;
            }
            return new Rectangle(scanWantedArea.X + minXValue,
                    scanWantedArea.Y + minYValue,
                    ((scanWantedArea.X + maxXValue) - (scanWantedArea.X + minXValue) + 1),
                     (scanWantedArea.Y + maxYValue) - (scanWantedArea.Y + minYValue) + 1);
        }

        public Rectangle FindBorderAreaForWantedColorV2(int wantedColor, int[] comparableArray, Rectangle scanWantedArea)
        {

            if (scanWantedArea.Width <= 0 || comparableArray == null || 
                comparableArray.Length != scanWantedArea.Width * scanWantedArea.Height)
            {

                // throw new Exception("FindBorderAreaForWantedColor func scanWantedArea param " +
                //     "value width is less equal to zero");

                DebugPfCnsl.println("FindBorderAreaForWantedColor returned empty");
                return Rectangle.Empty;
            }

            int arrayIncrementCounter = 0;
            int maxYValue = int.MinValue;
            int maxXValue = int.MinValue;
            int minYValue = int.MaxValue;
            int minXValue = int.MaxValue;




            // int [] comparableArray = takeScreenShotReturnRGBarray(scanWantedArea);
            //int[] comparableArray = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(scanWantedArea));


            for (int yPos = 0; yPos < scanWantedArea.Height; yPos++)
            {
                for (int xPos = 0; xPos < scanWantedArea.Width; xPos++)
                {
                    if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[xPos + arrayIncrementCounter]))
                    {
                        if (maxYValue < yPos)
                        {
                            maxYValue = yPos;
                        }
                        if (minYValue > yPos)
                        {
                            minYValue = yPos;
                        }
                        if (maxXValue < xPos)
                        {
                            maxXValue = xPos;
                        }
                        if (minXValue > xPos)
                        {
                            minXValue = xPos;
                        }

                    }
                }

                arrayIncrementCounter += scanWantedArea.Width;
            }
            if (maxXValue <= 0 && maxYValue <= 0)
            {
                return Rectangle.Empty;
            }
            return new Rectangle(scanWantedArea.X + minXValue,
                    scanWantedArea.Y + minYValue,
                    ((scanWantedArea.X + maxXValue) - (scanWantedArea.X + minXValue) + 1),
                     (scanWantedArea.Y + maxYValue) - (scanWantedArea.Y + minYValue) + 1);
        }
        public Rectangle FindBorderAreaBetweenColors(Rectangle scanWantedArea, params int[] wantedColors)
        {
            if (scanWantedArea.Width <= 0)
            {

                //throw new Exception("FindBorderAreaForWantedColor func scanWantedArea param " +
                //  "value width is less equal to zero");
                DebugPfCnsl.println("FindBorderAreaBetweenColors returned empty");
                return Rectangle.Empty;
            }

            int arrayIncrementCounter = 0;
            int maxYValue = int.MinValue;
            int maxXValue = int.MinValue;
            int minYValue = int.MaxValue;
            int minXValue = int.MaxValue;




            // int [] comparableArray = takeScreenShotReturnRGBarray(scanWantedArea);
            int[] comparableArray = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(scanWantedArea));


            for (int yPos = 0; yPos < scanWantedArea.Height; yPos++)
            {
                for (int xPos = 0; xPos < scanWantedArea.Width; xPos++)
                {
                    if (CompareIntBetweenColors(comparableArray[xPos + arrayIncrementCounter], wantedColors))
                    {
                        if (maxYValue < yPos)
                        {
                            maxYValue = yPos;
                        }
                        if (minYValue > yPos)
                        {
                            minYValue = yPos;
                        }
                        if (maxXValue < xPos)
                        {
                            maxXValue = xPos;
                        }
                        if (minXValue > xPos)
                        {
                            minXValue = xPos;
                        }

                    }
                }

                arrayIncrementCounter += scanWantedArea.Width;
            }
            if (maxXValue <= 0 || maxYValue <= 0)
            {
                return Rectangle.Empty;
            }
            return new Rectangle(scanWantedArea.X + minXValue,
                    scanWantedArea.Y + minYValue,
                    ((scanWantedArea.X + maxXValue) - (scanWantedArea.X + minXValue) + 1),
                     (scanWantedArea.Y + maxYValue) - (scanWantedArea.Y + minYValue) + 1);
        }

        public Rectangle[] ParseWordsFromOneLineArea(int wantedColor,int verticalPixelSeperator, Rectangle rectBorderedBefore)
        {

            // DebugPfCnsl.println("findBorderAreasForWantedColor method is entered ");
            if ((rectBorderedBefore.Width <= 0 || rectBorderedBefore.Height <= 0) || wantedColor == 0)
            {
                //System.out.println("Param rectBorderedBefore is null or wantedColor value = 0 ");
                //throw new ArgumentException("rectBorderedBefore.Width  <= 0 || wantedColor == 0 conditions is" +
                //    "occured in the ParseWordsFromOneLineArea func");
                DebugPfCnsl.println("ParseWordsFromOneLineArea function returned null");
                return null;
            }
           
            Rectangle[] rectArrays = null;

            int arrayIncrementCounter = 0;

            bool firstWhitePixelDetected = false;
            int countColumnSpace = 0;

            int rectArraySize = 0;
            // int [] comparableArray = takeScreenShotReturnRGBarray(rectBorderedBefore);
            int[] comparableArray = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(rectBorderedBefore));

            for (int firstXPos = 0; firstXPos < rectBorderedBefore.Width; firstXPos++)
            {
                arrayIncrementCounter = 0;
                for (int firstYpos = 0; firstYpos < rectBorderedBefore.Height; firstYpos++)
                {
                    if (!CompareTwoRgbIntAdvanced(wantedColor, comparableArray[firstXPos + arrayIncrementCounter]))
                    {
                       
                            countColumnSpace++;
                            arrayIncrementCounter += rectBorderedBefore.Width;                       
                    }
                    else
                    {
                        countColumnSpace = 0;                     
                        break;
                    }
                }
                //For seperating game chat word verticalPixelSeperator value must be = 2
                if (countColumnSpace >= verticalPixelSeperator * rectBorderedBefore.Height)
                {
                    rectArraySize++;
                    countColumnSpace = 0;

                    int secondXCounter = 0;

                    for (int secondXPos = firstXPos; secondXPos < rectBorderedBefore.Width; secondXPos++)

                    {

                        for (int secondYPos = 0; secondYPos < rectBorderedBefore.Height; secondYPos++)
                        {
                            if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[secondXPos + secondXCounter]))
                            {
                                //System.out.println("detected wanted color first loops = " + secondXPos + " " + secondYPos);
                                firstXPos = secondXPos;
                                secondXPos = rectBorderedBefore.Width;
                                break;
                            }
                            secondXCounter += rectBorderedBefore.Width;
                        }
                        secondXCounter = 0;
                    }
                }

            }
            //Increase for last one we cannot detect because last height index ends with wanted color so
            //we can't detect space
            rectArraySize++;

            //System.out.println(" rectArraySize = " + rectArraySize);
            countColumnSpace = 0;
            arrayIncrementCounter = 0;

            rectArrays = new Rectangle[rectArraySize];
            rectArraySize = 0;
            int anotherRectXCoor = 0;

            for (int firstXPos = 0; firstXPos < rectBorderedBefore.Width; firstXPos++)
            {
                for (int firstYpos = 0; firstYpos < rectBorderedBefore.Height; firstYpos++)
                {
                    if (!CompareTwoRgbIntAdvanced(wantedColor, comparableArray[firstXPos + arrayIncrementCounter]))
                    {
                        countColumnSpace++;
                        arrayIncrementCounter += rectBorderedBefore.Width;
                    }
                    else
                    {
                        countColumnSpace = 0;
                        break;
                    }
                }
                if (countColumnSpace >= verticalPixelSeperator * rectBorderedBefore.Height)
                {
                    rectArrays[rectArraySize++] = new Rectangle(rectBorderedBefore.X + anotherRectXCoor, rectBorderedBefore.Y,
                           firstXPos - anotherRectXCoor - 1, rectBorderedBefore.Height);
                    anotherRectXCoor = firstXPos;

                    

                    countColumnSpace = 0;

                    int secondXCounter = 0;
                    for (int secondXPos = firstXPos; secondXPos < rectBorderedBefore.Width; secondXPos++)
                    // for(int secondYPos = 0; secondYPos < rectBorderedBefore.height; secondYPos++)
                    {
                        // for(int secondXPos = firstXPos; secondXPos < rectBorderedBefore.width; secondXPos++)
                        for (int secondYPos = 0; secondYPos < rectBorderedBefore.Height; secondYPos++)
                        {
                            if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[secondXPos + secondXCounter]))
                            {
                                //   System.out.println("detected wanted color second loops = " + secondXPos + " " + secondYPos);
                                firstXPos = secondXPos;
                                anotherRectXCoor = firstXPos;
                                secondXPos = rectBorderedBefore.Width;
                                break;
                            }
                            secondXCounter += rectBorderedBefore.Width;
                        }
                        secondXCounter = 0;
                    }
                }
                arrayIncrementCounter = 0;
            }
            rectArrays[rectArrays.Length - 1] = new Rectangle(rectBorderedBefore.X + anotherRectXCoor, rectBorderedBefore.Y,
                    (rectBorderedBefore.X + rectBorderedBefore.Width) - (rectBorderedBefore.X + anotherRectXCoor), rectBorderedBefore.Height);
           
            
            // System.out.println(" Not Specified Rectangle info = "+Arrays.deepToString(rectArrays));

            List<Rectangle> listRectangles = new List<Rectangle>();
            //Edit rectangle areas according to wanted Color again.
            for (int i = 0; i < rectArrays.Length; i++)
            {
                if (rectArrays[i] != null && rectArrays[i].Width > 0 && rectArrays[i].Height > 0)
                {
                    // rectArrays[i] = FindBorderAreaForWantedColor(wantedColor, rectArrays[i]);
                    listRectangles.Add(FindBorderAreaForWantedColor(wantedColor, rectArrays[i]));
                }

            }
          
            //System.out.println(" Specified Rectangle info = "+ Arrays.deepToString(rectArrays));


            return listRectangles.ToArray();
        }

        public Rectangle[] ParseWordsFromOneLineAreaV2(int wantedColor, int verticalPixelSeperator,int[] comparableArray, Rectangle rectBorderedBefore)
        {

            // DebugPfCnsl.println("findBorderAreasForWantedColor method is entered ");
            if ((rectBorderedBefore.Width <= 0 || rectBorderedBefore.Height <= 0) || wantedColor == 0)
            {
                //System.out.println("Param rectBorderedBefore is null or wantedColor value = 0 ");
                //throw new ArgumentException("rectBorderedBefore.Width  <= 0 || wantedColor == 0 conditions is" +
                //    "occured in the ParseWordsFromOneLineArea func");
                DebugPfCnsl.println("ParseWordsFromOneLineArea function returned null");
                return null;
            }

            Rectangle[] rectArrays = null;

            int arrayIncrementCounter = 0;

            bool firstWhitePixelDetected = false;
            int countColumnSpace = 0;

            int rectArraySize = 0;
            // int [] comparableArray = takeScreenShotReturnRGBarray(rectBorderedBefore);
            //int[] comparableArray = screenshot.ConvertBitmapToArray(screenshot.CaptureSpecifiedScreen(rectBorderedBefore));

            for (int firstXPos = 0; firstXPos < rectBorderedBefore.Width; firstXPos++)
            {
                arrayIncrementCounter = 0;
                for (int firstYpos = 0; firstYpos < rectBorderedBefore.Height; firstYpos++)
                {
                    if (!CompareTwoRgbIntAdvanced(wantedColor, comparableArray[firstXPos + arrayIncrementCounter]))
                    {

                        countColumnSpace++;
                        arrayIncrementCounter += rectBorderedBefore.Width;
                    }
                    else
                    {
                        countColumnSpace = 0;
                        break;
                    }
                }
                //For seperating game chat word verticalPixelSeperator value must be = 2
                if (countColumnSpace >= verticalPixelSeperator * rectBorderedBefore.Height)
                {
                    rectArraySize++;
                    countColumnSpace = 0;

                    int secondXCounter = 0;

                    for (int secondXPos = firstXPos; secondXPos < rectBorderedBefore.Width; secondXPos++)

                    {

                        for (int secondYPos = 0; secondYPos < rectBorderedBefore.Height; secondYPos++)
                        {
                            if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[secondXPos + secondXCounter]))
                            {
                                //System.out.println("detected wanted color first loops = " + secondXPos + " " + secondYPos);
                                firstXPos = secondXPos;
                                secondXPos = rectBorderedBefore.Width;
                                break;
                            }
                            secondXCounter += rectBorderedBefore.Width;
                        }
                        secondXCounter = 0;
                    }
                }

            }
            //Increase for last one we cannot detect because last height index ends with wanted color so
            //we can't detect space
            rectArraySize++;

            //System.out.println(" rectArraySize = " + rectArraySize);
            countColumnSpace = 0;
            arrayIncrementCounter = 0;

            rectArrays = new Rectangle[rectArraySize];
            rectArraySize = 0;
            int anotherRectXCoor = 0;

            for (int firstXPos = 0; firstXPos < rectBorderedBefore.Width; firstXPos++)
            {
                for (int firstYpos = 0; firstYpos < rectBorderedBefore.Height; firstYpos++)
                {
                    if (!CompareTwoRgbIntAdvanced(wantedColor, comparableArray[firstXPos + arrayIncrementCounter]))
                    {
                        countColumnSpace++;
                        arrayIncrementCounter += rectBorderedBefore.Width;
                    }
                    else
                    {
                        countColumnSpace = 0;
                        break;
                    }
                }
                if (countColumnSpace >= verticalPixelSeperator * rectBorderedBefore.Height)
                {
                    rectArrays[rectArraySize++] = new Rectangle(rectBorderedBefore.X + anotherRectXCoor, rectBorderedBefore.Y,
                           firstXPos - anotherRectXCoor - 1, rectBorderedBefore.Height);
                    anotherRectXCoor = firstXPos;



                    countColumnSpace = 0;

                    int secondXCounter = 0;
                    for (int secondXPos = firstXPos; secondXPos < rectBorderedBefore.Width; secondXPos++)
                    // for(int secondYPos = 0; secondYPos < rectBorderedBefore.height; secondYPos++)
                    {
                        // for(int secondXPos = firstXPos; secondXPos < rectBorderedBefore.width; secondXPos++)
                        for (int secondYPos = 0; secondYPos < rectBorderedBefore.Height; secondYPos++)
                        {
                            if (CompareTwoRgbIntAdvanced(wantedColor, comparableArray[secondXPos + secondXCounter]))
                            {
                                //   System.out.println("detected wanted color second loops = " + secondXPos + " " + secondYPos);
                                firstXPos = secondXPos;
                                anotherRectXCoor = firstXPos;
                                secondXPos = rectBorderedBefore.Width;
                                break;
                            }
                            secondXCounter += rectBorderedBefore.Width;
                        }
                        secondXCounter = 0;
                    }
                }
                arrayIncrementCounter = 0;
            }
            rectArrays[rectArrays.Length - 1] = new Rectangle(rectBorderedBefore.X + anotherRectXCoor, rectBorderedBefore.Y,
                    (rectBorderedBefore.X + rectBorderedBefore.Width) - (rectBorderedBefore.X + anotherRectXCoor), rectBorderedBefore.Height);


            // System.out.println(" Not Specified Rectangle info = "+Arrays.deepToString(rectArrays));

            List<Rectangle> listRectangles = new List<Rectangle>();
            List<int[]> listClippingArray = new List<int[]>();
            int indexCounter = 0;
            //Edit rectangle areas according to wanted Color again.
            for (int i = 0; i < rectArrays.Length; i++)
            {
                if (rectArrays[i] != null && rectArrays[i].Width > 0 && rectArrays[i].Height > 0)
                {
                    // rectArrays[i] = FindBorderAreaForWantedColor(wantedColor, rectArrays[i]);
                    //listRectangles.Add(FindBorderAreaForWantedColor(wantedColor, rectArrays[i]));
                    listClippingArray.Add(ClipIntArray(comparableArray, rectBorderedBefore, rectArrays[i]));
                    listRectangles.Add(FindBorderAreaForWantedColorV2(wantedColor, listClippingArray[indexCounter++], rectArrays[i]));
                }

            }

            //System.out.println(" Specified Rectangle info = "+ Arrays.deepToString(rectArrays));


            return listRectangles.ToArray();
        }

        //<summary>
        //It detects just worm, not white numbers
        //</summary>
        public int[] DetectWorms(int[] rgbArray)
        {

            int colorRed = 0;
            int colorGreen = 0;
            int colorBlue = 0;
            int equalityVar = 0;
            int[] arrayequalityVars;
            int counter = 0;

            for (int i = 0; i < rgbArray.Length; i++)
            {

                colorRed = (rgbArray[i] & 0x00ff0000) >> 16;
                colorGreen = (rgbArray[i] & 0x0000ff00) >> 8;
                colorBlue = rgbArray[i] & 0x000000ff;

                if (colorRed > 48 && colorGreen > 48 && colorBlue > 39)
                {
                    if (colorRed < 246 && colorGreen < 193 && colorBlue < 174)
                    {
                        equalityVar++;
                    }
                }
            }

            arrayequalityVars = new int[equalityVar];
            for (int k = 0; k < rgbArray.Length; k++)
            {
                colorRed = (rgbArray[k] & 0x00ff0000) >> 16;
                colorGreen = (rgbArray[k] & 0x0000ff00) >> 8;
                colorBlue = rgbArray[k] & 0x000000ff;

                if (colorRed > 50 && colorGreen > 50 && colorBlue > 40)
                {
                    if (colorRed < 246 && colorGreen < 193 && colorBlue < 174)
                    {
                        arrayequalityVars[counter++] = rgbArray[k];
                    }
                }
            }

            return arrayequalityVars;

        }
        ///<summary>
        ///This algorithm clips an integer array 
        ///</summary>
        ///<param name="sourceArgbArray"> 
        ///An array that will be clipped </param>
        ///<param name="rectSourceArray"> rectangle of the <paramref name="sourceArgbArray"/>
        /// array </param>
        /// <param name="rectClipValue"> A rectangle that will be maken clipping 
        /// process according to this Rectangle value </param>
        /// <returns> new int array which its size is equals <paramref name="rectClipValue"/> (height * width) </returns>
        public int[] ClipIntArray(int[] sourceArgbArray, Rectangle rectSourceArray, Rectangle rectClipValue)
        {


            if ((rectClipValue.Width * rectClipValue.Height) > sourceArgbArray.Length ||
                sourceArgbArray.Length != (rectSourceArray.Height * rectSourceArray.Width))
            {
                throw new ArgumentException("ClipIntArray func 'clipRectValue' param Length must be " +
                    "less than sourceArgbArray length or rectSourceArray length must be the same with sourceArgbArray !! ");
            }
            else if (rectClipValue.Width > rectSourceArray.Width ||
               rectClipValue.Height > rectSourceArray.Height )      
            {
                throw new ArgumentException("check this values !!!");
            }

            int[] croppedArray = new int[rectClipValue.Width * rectClipValue.Height];

            if ((rectClipValue.Width * rectClipValue.Height) == sourceArgbArray.Length)
            {
                croppedArray = sourceArgbArray;
                return croppedArray;
            }
            //rect value {X=186,Y=582,Width=56,Height=8}
            //clipped rect value {X=188,Y=585,Width=51,Height=5}
            int index = 0;

            int sourceBaseXValue = 0;
            int sourceBaseYValue = 0;

            if(rectClipValue.X != 0)
            {
                if(rectClipValue.X - rectSourceArray.X <0)
                {
                    //throw new ArgumentException("Please insert real screen coordinates for X");
                    DebugPfCnsl.println("X value is unvalidate returned null");
                    return null;
                }
                else if((rectClipValue.X - rectSourceArray.X) + rectClipValue.Width  > rectSourceArray.Width)
                {
                    // throw new ArgumentException("this implementation is not correct for x and width");
                    DebugPfCnsl.println("this implementation is not correct for x and width returned null");
                    return null;
                }
                sourceBaseXValue = (rectClipValue.X - rectSourceArray.X);
            }
            if(rectClipValue.Y != 0)
            {
                if (rectClipValue.Y -rectSourceArray.Y <0 )
                {
                    // throw new ArgumentException("Please insert real screen coordinates for Y");
                    DebugPfCnsl.println("Y value is unvalidate returned null");
                    return null;
                }
                else if((rectClipValue.Y - rectSourceArray.Y) + rectClipValue.Height > rectSourceArray.Height)
                {
                    //throw new ArgumentException("this implementation is not correct for y and height");
                    DebugPfCnsl.println("this implementation is not correct for y and width returned null");
                    return null;
                }
                sourceBaseYValue = (rectSourceArray.Width * (rectClipValue.Y - rectSourceArray.Y));
            }

            int sourceBaseValue = sourceBaseXValue + sourceBaseYValue;

            for(int y= 0; y < rectClipValue.Height; y++)
            {
                for(int x= 0; x < rectClipValue.Width; x++)
                {
                    croppedArray[index++] = sourceArgbArray[(y*rectSourceArray.Width) + x + sourceBaseValue];
                }
            }    

           

            return croppedArray;
        }

        ///<summary>
        ///This algorithm clips an bool array 
        ///</summary>
        ///<param name="sourceArgbArray"> 
        ///An array that will be clipped </param>
        ///<param name="rectSourceArray"> rectangle of the <paramref name="sourceArgbArray"/>
        /// array </param>
        /// <param name="rectClipValue"> A rectangle that will be maken clipping 
        /// process according to this Rectangle value </param>
        /// <returns> new bool array which its size is equals <paramref name="rectClipValue"/> (height * width) </returns>
        public bool[] ClipBoolArray(bool[] sourceArgbArray, Rectangle rectSourceArray, Rectangle rectClipValue)
        {
            if ((rectClipValue.Width * rectClipValue.Height) > sourceArgbArray.Length ||
                           sourceArgbArray.Length != (rectSourceArray.Height * rectSourceArray.Width))
            {
                throw new ArgumentException("ClipIntArray func 'clipRectValue' param Length must be " +
                    "less than sourceArgbArray length or rectSourceArray length must be the same with sourceArgbArray !! ");
            }
            else if (rectClipValue.Width > rectSourceArray.Width ||
                rectClipValue.Height > rectSourceArray.Height ||
                (rectClipValue.X - rectSourceArray.X) < 0 ||
                    (rectClipValue.Y - rectSourceArray.Y) <0 )
            {
                throw new ArgumentException("check this values !!!");
            }

            bool[] croppedArray = new bool[rectClipValue.Width * rectClipValue.Height];

            if ((rectClipValue.Width * rectClipValue.Height) == sourceArgbArray.Length)
            {
                croppedArray = sourceArgbArray;
                return croppedArray;
            }
           
            int index = 0;

            int sourceBaseValue = (rectClipValue.X - rectSourceArray.X) +
                (rectSourceArray.Width * (rectClipValue.Y - rectSourceArray.Y));

           // DebugPfCnsl.println("rectSourceArray rect = " + rectSourceArray.ToString());
           // DebugPfCnsl.println("rectClipValue rect = " + rectClipValue.ToString());
           // DebugPfCnsl.println("sourceBaseValue values = " + sourceBaseValue);

            for (int y = 0; y < rectClipValue.Height; y++)
            {
                for (int x = 0; x < rectClipValue.Width; x++)
                {
                    croppedArray[index++] = sourceArgbArray[(y * rectSourceArray.Width) + x + sourceBaseValue];
                }
            }



            return croppedArray;
        }

        
    }

    

}
