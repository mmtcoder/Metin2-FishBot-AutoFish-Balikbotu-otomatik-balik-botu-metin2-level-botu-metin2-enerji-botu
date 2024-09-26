using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Metin2AutoFishCSharp.Sources.ChatHandler
{
    internal class GameAlphabetDetecter 
    {
        private ScreenShotWinAPI screenshot;
        private ImageProcess imageproc;
        private GameAlphabetRectangle alp;
        private string detectedAlphabet;
        private readonly string alphabets = "0123456789abcçdefgğhiıjklmnoöpqrsştuüvwxyz";
        private GameObjectCoordinates coor;

        private object lockObject = new object();
        private object lockObject1 = new object();

        bool[][] tempArrays;

        public GameAlphabetDetecter(ImageObjects imageObjects) 
        {
            screenshot = new ScreenShotWinAPI();
            coor = new GameObjectCoordinates(imageObjects);
            imageproc = imageObjects;
            alp = new GameAlphabetRectangle(imageObjects);

            tempArrays = convertAlphabetsBool();
         

        }

        public bool[][] GetAlphabetBoolArray(char c, Rectangle[] charRectangle )
        {
            bool[][] resultBool = new bool[2][];
            //Rectangle[] resultRectangle = new Rectangle[2]; 

           int index = alphabets.IndexOf(c);
            DebugPfCnsl.println(c + " harfinin kayıtlı indeks değeri = " +  index);

            if (index >= 0)
            {
                //lower letter
                resultBool[0] = tempArrays[index * 2];
                charRectangle[0] = alp.tempArrays[index * 2];
                //Capital letter
                resultBool[1] = tempArrays[(index * 2) + 1];
                charRectangle[1] = alp.tempArrays[(index * 2) + 1];

                
                return resultBool;
            }
            else
            {
                DebugPfCnsl.println("Parametredeki değer istenilen dışı !!");

                return null;
            }
            
        }

        public string DetectGameText(int colorGame,Rectangle rectDetectWords)
        {
            lock (lockObject)
            {
                Rectangle rectBordered = imageproc.FindBorderAreaForWantedColor(colorGame,
                    rectDetectWords);
                if (rectBordered == null || rectBordered.Width <= 0 || rectBordered.Height <= 0) return string.Empty;
                Rectangle[] rectWords = imageproc.ParseWordsFromOneLineArea(colorGame, 2,
                    imageproc.FindBorderAreaForWantedColor(colorGame, rectBordered));

                if (rectWords == null || rectWords.Length < 1)
                {
                    DebugPfCnsl.println("Herhangi cümle tespit edilemedi");
                    return string.Empty;
                }
                int[][] wordsImage = new int[rectWords.Length][];
                //List<int[][]> listWordImages = new List<int[][]>();

                for (int word = 0; word < rectWords.Length; word++)
                {
                    wordsImage[word] = screenshot.ImageArraySpecifiedArea(rectWords[word]);
                }

                bool[][] chatWordsBool = new bool[wordsImage.Length][];

                for (int word = 0; word < chatWordsBool.Length; word++)
                {
                    chatWordsBool[word] = imageproc.RecordWantedColorAsBool(colorGame,
                        wordsImage[word]);
                }

                string stringDetected = "";

                for (int stringWord = 0; stringWord < chatWordsBool.Length; stringWord++)
                {
                    stringDetected += ParseAlphabetFromWord(chatWordsBool[stringWord], rectWords[stringWord]);
                    stringDetected += " ";
                }



                DebugPfCnsl.println(" detected string result = " + stringDetected);



                return stringDetected;
            }
        }
        public string DetectGameTextSetableVerticalGap(int colorGame, Rectangle rectDetectWords,int verticalGap)
        {
            lock (lockObject)
            {
                Rectangle rectBordered = imageproc.FindBorderAreaForWantedColor(colorGame,
                    rectDetectWords);
                if (rectBordered == null || rectBordered.Width <= 0 || rectBordered.Height <= 0) return string.Empty;
                Rectangle[] rectWords = imageproc.ParseWordsFromOneLineArea(colorGame, verticalGap,
                    imageproc.FindBorderAreaForWantedColor(colorGame, rectBordered));

                if (rectWords == null || rectWords.Length < 1)
                {
                    DebugPfCnsl.println("Herhangi cümle tespit edilemedi");
                    return string.Empty;
                }
                int[][] wordsImage = new int[rectWords.Length][];
                //List<int[][]> listWordImages = new List<int[][]>();

                for (int word = 0; word < rectWords.Length; word++)
                {
                    wordsImage[word] = screenshot.ImageArraySpecifiedArea(rectWords[word]);
                }

                bool[][] chatWordsBool = new bool[wordsImage.Length][];

                for (int word = 0; word < chatWordsBool.Length; word++)
                {
                    chatWordsBool[word] = imageproc.RecordWantedColorAsBool(colorGame,
                        wordsImage[word]);
                }

                string stringDetected = "";

                for (int stringWord = 0; stringWord < chatWordsBool.Length; stringWord++)
                {
                    stringDetected += ParseAlphabetFromWord(chatWordsBool[stringWord], rectWords[stringWord]);
                    stringDetected += " ";
                }



               // DebugPfCnsl.println(" detected string result = " + stringDetected);



                return stringDetected;
            }
        }
        public string DetectGameTextWithProvidedImage(int[]targetImage,Rectangle targetRect,int colorGame)
        {
            if (targetRect == null || targetRect.Width == 0 || targetRect.Height == 0)
            {
                DebugPfCnsl.println("DetectGameTextWithProvidedImage is returned string empty");
                return string.Empty;
            }
            lock (lockObject1)
            {
                if(targetImage.Length != targetRect.Width * targetRect.Height)
                {
                    DebugPfCnsl.println("DetectGameTextWithProvidedImage fucntion param targetImage and" +
                        "param targetRect size must be the same ");
                    return string.Empty;
                }
                Rectangle rectBordered = imageproc.FindBorderAreaForWantedColorV2(colorGame,targetImage,
                    targetRect);
                if (rectBordered == null || rectBordered.Width <= 0 || rectBordered.Height <= 0) return string.Empty;
                Rectangle[] rectWords = imageproc.ParseWordsFromOneLineAreaV2(colorGame, 2,targetImage,
                    imageproc.FindBorderAreaForWantedColorV2(colorGame, targetImage, rectBordered));

                if (rectWords == null || rectWords.Length < 1)
                {
                    DebugPfCnsl.println("Herhangi cümle tespit edilemedi");
                    return string.Empty;
                }
                int[][] wordsImage = new int[rectWords.Length][];
                //List<int[][]> listWordImages = new List<int[][]>();

                for (int word = 0; word < rectWords.Length; word++)
                {
                    //  wordsImage[word] = screenshot.ImageArraySpecifiedArea(rectWords[word]);
                    wordsImage[word] = imageproc.ClipIntArray(targetImage,targetRect,rectWords[word]);
                }

                bool[][] chatWordsBool = new bool[wordsImage.Length][];

                for (int word = 0; word < chatWordsBool.Length; word++)
                {
                    chatWordsBool[word] = imageproc.RecordWantedColorAsBool(colorGame,
                        wordsImage[word]);
                }

                string stringDetected = "";

                for (int stringWord = 0; stringWord < chatWordsBool.Length; stringWord++)
                {
                    stringDetected += ParseAlphabetFromWord(chatWordsBool[stringWord], rectWords[stringWord]);
                    stringDetected += " ";
                }



                DebugPfCnsl.println(" detected string result V2 = " + stringDetected);



                return stringDetected;
            }
        }

        /// <summary>
        /// This algorithm detects metin2 Alphabets from
        /// <paramref name="rectScannedWord"/> area and 
        /// converts them into string.
        /// <paramref name="scannedWord"/> must be a seperated color array.
        /// You can call ImageProcess.RecordWantedColorAsBool method.
        /// <paramref name="rectScannedWord"/> rectangle must be bordered by ImageProcess.
        /// FindBorderAreaForWantedColor method .This rectangle height * width 
        /// must be equalt to <paramref name="scannedWord"/> array length !!
        /// </summary> 
        /// <param name="scannedWord">.This must be a seperated color array </param>
        /// <param name="rectScannedWord">This rectangle must be bordered by ImageProcess.
        /// FindBorderAreaForWantedColor method .This rectangle height * width 
        /// must be equalt to <paramref name="scannedWord"/> array length !!</param>
        /// <returns>returns string that detected word from specified <paramref name="rectScannedWord"/> 
        /// area </returns>
        public string ParseAlphabetFromWord(bool[] scannedWord, Rectangle rectScannedWord)
        {
            // int index = 0;
             //int index2 = 0;
           /* if(.Length != rectScannedWord.Width * rectScannedWord.Height)
            {
                throw new ArgumentException("scannedWord length must be equalt to rectScannedWord widht * height");
            }*/
           if(scannedWord == null || rectScannedWord == Rectangle.Empty)
            { return string.Empty; }

            Rectangle rectDetectAlpArea;
            if (rectScannedWord.Width < 12)
            {
                rectDetectAlpArea = new Rectangle(rectScannedWord.X, rectScannedWord.Y,rectScannedWord.Width, rectScannedWord.Height);
            }
            else
            {
                rectDetectAlpArea = new Rectangle(rectScannedWord.X, rectScannedWord.Y, 12, rectScannedWord.Height);
            }
           
            bool[] cropBool = imageproc.ClipBoolArray(scannedWord, rectScannedWord, rectDetectAlpArea);
          
            //FileHandler.SaveImageAsPng(screenshot.CaptureSpecifiedScreen(rectDetectAlpArea), "makarati" +
                // index2++ + ".png", PathWayStruct.PATH_TESTIMAGES);
          //  Console.WriteLine("rectDetectAlpArea result = " + rectDetectAlpArea.ToString());
                   
           
            // Console.WriteLine("rectScannedWord result = " + rectScannedWord.ToString());
             for(int x =0; x < rectScannedWord.Width;)
             {
                //if (ThreadGlobals.isStopped) return "";
                //Rectangle resultRectAlp = DetectAlphabet(cropBool, rectDetectAlpArea);
                Rectangle resultRectAlp = DetectAlphaberV2(cropBool, rectDetectAlpArea);
             
                //FileHandler.SaveImageAsPng(screenshot.CaptureSpecifiedScreen(resultRectAlp), "test" +
                 //   index++ + ".png", PathWayStruct.PATH_TESTIMAGES);

               //  Console.WriteLine("resultRectAlp result = " + resultRectAlp.ToString());

              
                 if ((resultRectAlp.X - rectScannedWord.X) + resultRectAlp.Width + 12 < rectScannedWord.Width)
                 {
                     rectDetectAlpArea = new Rectangle(resultRectAlp.X + resultRectAlp.Width, rectScannedWord.Y
                                         , 12, rectScannedWord.Height);

                   // FileHandler.SaveImageAsPng(screenshot.CaptureSpecifiedScreen(rectDetectAlpArea), "makarati" +
                  //index2++ + ".png", PathWayStruct.PATH_TESTIMAGES);
                  // Console.WriteLine("rectDetectAlpArea result = " + rectDetectAlpArea.ToString());
                     
                 }
                 else
                 {
                    if(rectScannedWord.Width - (resultRectAlp.X - rectScannedWord.X + resultRectAlp.Width) > 0)
                    {
                        rectDetectAlpArea = new Rectangle(resultRectAlp.X + resultRectAlp.Width, rectScannedWord.Y
                                      , rectScannedWord.Width - (resultRectAlp.X - rectScannedWord.X + resultRectAlp.Width), rectScannedWord.Height);
                       
                        //Console.WriteLine("rectDetectAlpArea else result = " + rectDetectAlpArea.ToString());
                       // FileHandler.SaveImageAsPng(screenshot.CaptureSpecifiedScreen(rectDetectAlpArea), "makarati" +
                     //index2++ + ".png", PathWayStruct.PATH_TESTIMAGES);
                        // DebugDrawingHandle.DrawWantedObjectToScreen(rectAlphabet);
                        // Thread.Sleep(2000);
                    }
                    else
                    {
                        //DebugPfCnsl.println("ParseAlphabetFromWord process is finished ");
                        string resultString = detectedAlphabet;
                        detectedAlphabet = "";
                       // DebugPfCnsl.println("detected alphabets = " + resultString);
                        return resultString;
                    }

                }

                 cropBool = imageproc.ClipBoolArray(scannedWord, rectScannedWord, rectDetectAlpArea);
                // Console.WriteLine("loop cropBool Array result = ");
               //  DebugPfCnsl.PrintArray(cropBool);
                

             }

            // DebugDrawingHandle.DrawWantedObjectToScreen(rectAlphabet);



            return detectedAlphabet;
        }

        public Rectangle DetectAlphaberV2(bool[] scannedWord, Rectangle rectScannedWord)
        {
          //  DebugPfCnsl.println("temp Array length = " + tempArrays.Length);

            List<Rectangle> listRect = new List<Rectangle>();
            List<string> listAlphabet = new List<string>(); 
            
            for (int alphabet=0; alphabet<tempArrays.Length; alphabet++)
            {
                Rectangle[] letterRect = imageproc.FindAllImagesBoolArrays(tempArrays[alphabet], alp.tempArrays[alphabet], scannedWord, rectScannedWord);
               // Rectangle[] capitalRect = imageproc.FindAllImagesBoolArrays(tempArrays[alphabet], alp.tempArrays[alphabet], scannedWord, rectScannedWord);

                if (letterRect.Length > 0)
                {
                    
                    //detectedAlphabet += alphabets.ElementAt(alphabet/2);
                   // DebugPfCnsl.println("Detected alphabet = " + alphabets.ElementAt(alphabet / 2));
                    // return aRect[0];
                    for(int i = 0; i < letterRect.Length; i++)
                    {
                        listAlphabet.Add(alphabets.ElementAt(alphabet / 2).ToString());
                        listRect.Add(letterRect[i]);
                    }
                   
                }
                /*else if (capitalRect.Length > 0)
                {
                   // detectedAlphabet +=  alphabets.ElementAt(alphabet / 2);
                   // DebugPfCnsl.println("Detected alphabet = " + alphabets.ElementAt(alphabet / 2));
                    //return ARect[0];
                    
                    for (int i = 0; i < letterRect.Length; i++)
                    {
                        listAlphabet.Add(alphabets.ElementAt(alphabet / 2).ToString());
                        listRect.Add(capitalRect[i]);
                    }
                }*/
            }

            if (listRect.Count > 0)
            {
              //  DebugPfCnsl.println("listRect length = " + listRect.Count);
              //  DebugPfCnsl.println("listRect Rect Result = ");
              //  DebugPfCnsl.PrintArray(listRect.ToArray());

                if (listRect.Count > 1)
                {
                    int minXValue = int.MaxValue;

                    int maxHeight = int.MinValue;
                    int maxWidth = int.MinValue;

                    List<int> listIndexes = new List<int> { 0 };
                    
                    for (int i = 0; i < listRect.Count; i++)
                    {
                        if (minXValue > listRect.ElementAt(i).X)
                        {
                            minXValue = listRect.ElementAt(i).X;
                            maxHeight = listRect.ElementAt(i).Height;
                            maxWidth = listRect.ElementAt(i).Width;

                            //If last detected x coor is less than 
                            //previous detects for minxValue reset list and fill 
                            //0 index
                            if(listIndexes.Count > 1)
                            {
                                listIndexes.Clear();
                                listIndexes.Add(i);
                            }
                            listIndexes[0] = i;
                        }
                        else if (minXValue == listRect.ElementAt(i).X &&
                            maxHeight < listRect.ElementAt(i).Height)
                        {
                            maxHeight = listRect.ElementAt(i).Height;
                            listIndexes.Add(i);
                        }
                        else if (minXValue == listRect.ElementAt(i).X &&
                            maxHeight == listRect.ElementAt(i).Height &&
                            maxWidth < listRect.ElementAt(i).Width)
                        {
                            maxWidth = listRect.ElementAt(i).Width;
                            listIndexes.Add(i);
                        }
                    }

                    detectedAlphabet += listAlphabet.ElementAt(listIndexes.ElementAt(listIndexes.Count - 1));
                   // DebugPfCnsl.println("detected alphabet = " + listAlphabet.ElementAt(listIndexes.ElementAt(listIndexes.Count - 1)));
                   // DebugPfCnsl.println("returned rect  = " + listRect.ElementAt(listIndexes.ElementAt(listIndexes.Count - 1)));
                    return listRect.ElementAt(listIndexes.ElementAt(listIndexes.Count - 1));
                }

              //  DebugPfCnsl.println("detected alphabet = " + listAlphabet.ElementAt(0));
                detectedAlphabet += listAlphabet.ElementAt(0);
                return listRect.ElementAt(listRect.Count - 1);
            }
            else
            {
                detectedAlphabet += "";
                return new Rectangle(rectScannedWord.X, rectScannedWord.Y, 1, rectScannedWord.Height);
            }

        }
       
        private bool[][] convertAlphabetsBool()
        {
            string[] fileNames = Directory.GetFiles(FileHandler.FindFolderNameFromBase("GameAlphabets"));

           // DebugPfCnsl.PrintArray(fileNames);

            bool[][] alphabetArrays = new bool[fileNames.Length][];
            
            for(int i = 0; i < fileNames.Length; i++)
            {
                alphabetArrays[i] = imageproc.RecordWantedColorAsBool(ColorGame.CHAT_WHITE_COLOR,
                    screenshot.ConvertBitmapToArray(FileHandler.ReadPngFileGetBitmap(fileNames[i],
                    PathWayStruct.PATH_CHAT_ALPHABETS)));
            }

            return alphabetArrays;
        }

        public GameAlphabetRectangle GetGameAlphabetRectangle()
        {
            return alp;
        }

        /*public Rectangle DetectAlphabet(bool[] scannedWord, Rectangle rectScannedWord)
        {
            List<Rectangle> listRect = new List<Rectangle>();

            Rectangle[] aRect = imageproc.FindAllImagesBoolArrays(a, alp.a, scannedWord, rectScannedWord);
            Rectangle[] ARect = imageproc.FindAllImagesBoolArrays(A, alp.A, scannedWord, rectScannedWord);
            if (aRect.Length > 0)
            {
                detectedAlphabet += "a";
                // return aRect[0];
                listRect.Add(aRect[0]);
            }
            else if (ARect.Length > 0)
            {
                detectedAlphabet += "a";
                //return ARect[0];
                listRect.Add(ARect[0]);
            }

            Rectangle[] bRect = imageproc.FindAllImagesBoolArrays(b, alp.b, scannedWord, rectScannedWord);
            Rectangle[] BRect = imageproc.FindAllImagesBoolArrays(B, alp.B, scannedWord, rectScannedWord);
            if (bRect.Length > 0)
            {
                detectedAlphabet += "b";
                //return bRect[0];
                listRect.Add(bRect[0]);
            }
            else if (BRect.Length > 0)
            {
                detectedAlphabet += "b";
                //return BRect[0];
                listRect.Add(BRect[0]);
            }

            Rectangle[] cRect = imageproc.FindAllImagesBoolArrays(c, alp.c, scannedWord, rectScannedWord);
            Rectangle[] CRect = imageproc.FindAllImagesBoolArrays(C, alp.C, scannedWord, rectScannedWord);
            if (cRect.Length > 0)
            {
                detectedAlphabet += "c";
                //return cRect[0];
                listRect.Add(cRect[0]);
            }
            else if (CRect.Length > 0)
            {
                detectedAlphabet += "c";
                //return CRect[0];
                listRect.Add(CRect[0]);
            }

            Rectangle[] cTRect = imageproc.FindAllImagesBoolArrays(cT, alp.cT, scannedWord, rectScannedWord);
            Rectangle[] CTRect = imageproc.FindAllImagesBoolArrays(CT, alp.CT, scannedWord, rectScannedWord);
            if (cTRect.Length > 0)
            {
                detectedAlphabet += "ç";
                //return cTRect[0];
                listRect.Add(cTRect[0]);
            }
            else if (CTRect.Length > 0)
            {
                detectedAlphabet += "ç";
                //return CTRect[0];
                listRect.Add(CTRect[0]);
            }

            Rectangle[] dRect = imageproc.FindAllImagesBoolArrays(d, alp.d, scannedWord, rectScannedWord);
            Rectangle[] DRect = imageproc.FindAllImagesBoolArrays(D, alp.D, scannedWord, rectScannedWord);
            if (dRect.Length > 0)
            {
                detectedAlphabet += "d";
                //return dRect[0];
                listRect.Add(dRect[0]);
            }
            else if (DRect.Length > 0)
            {
                detectedAlphabet += "d";
                //  return DRect[0];
                listRect.Add(DRect[0]);
            }

            Rectangle[] eRect = imageproc.FindAllImagesBoolArrays(e, alp.e, scannedWord, rectScannedWord);
            Rectangle[] ERect = imageproc.FindAllImagesBoolArrays(E, alp.E, scannedWord, rectScannedWord);
            if (eRect.Length > 0)
            {
                detectedAlphabet += "e";
                //return eRect[0];
                listRect.Add(eRect[0]);
            }
            else if (ERect.Length > 0)
            {
                detectedAlphabet += "e";
                // return ERect[0];
                listRect.Add(ERect[0]);

            }

            Rectangle[] fRect = imageproc.FindAllImagesBoolArrays(f, alp.f, scannedWord, rectScannedWord);
            Rectangle[] FRect = imageproc.FindAllImagesBoolArrays(F, alp.F, scannedWord, rectScannedWord);
            if (fRect.Length > 0)
            {
                detectedAlphabet += "f";
                // return fRect[0];
                listRect.Add(fRect[0]);
            }
            else if (FRect.Length > 0)
            {
                detectedAlphabet += "f";
                // return FRect[0];
                listRect.Add(FRect[0]);
            }

            Rectangle[] gRect = imageproc.FindAllImagesBoolArrays(g, alp.g, scannedWord, rectScannedWord);
            Rectangle[] GRect = imageproc.FindAllImagesBoolArrays(G, alp.G, scannedWord, rectScannedWord);
            if (gRect.Length > 0)
            {
                detectedAlphabet += "g";
                //return gRect[0];
                listRect.Add(gRect[0]);
            }
            else if (GRect.Length > 0)
            {
                detectedAlphabet += "g";
                //return GRect[0];
                listRect.Add(GRect[0]);
            }

            Rectangle[] gTRect = imageproc.FindAllImagesBoolArrays(gT, alp.gT, scannedWord, rectScannedWord);
            Rectangle[] GTRect = imageproc.FindAllImagesBoolArrays(GT, alp.GT, scannedWord, rectScannedWord);
            if (gTRect.Length > 0)
            {
                detectedAlphabet += "ğ";
                //return gTRect[0];
                listRect.Add(gTRect[0]);
            }
            else if (GTRect.Length > 0)
            {
                detectedAlphabet += "ğ";
                // return GTRect[0];
                listRect.Add(GTRect[0]);
            }

            Rectangle[] hRect = imageproc.FindAllImagesBoolArrays(h, alp.h, scannedWord, rectScannedWord);
            Rectangle[] HRect = imageproc.FindAllImagesBoolArrays(H, alp.H, scannedWord, rectScannedWord);
            if (hRect.Length > 0)
            {
                detectedAlphabet += "h";
                // return hRect[0];
                listRect.Add(hRect[0]);
            }
            else if (HRect.Length > 0)
            {
                detectedAlphabet += "h";
                // return HRect[0];
                listRect.Add(HRect[0]);
            }

            Rectangle[] iTRect = imageproc.FindAllImagesBoolArrays(iT, alp.iT, scannedWord, rectScannedWord);
            Rectangle[] ITRect = imageproc.FindAllImagesBoolArrays(IT, alp.IT, scannedWord, rectScannedWord);
            if (iTRect.Length > 0)
            {
                detectedAlphabet += "ı";
                // return iTRect[0];
                listRect.Add(iTRect[0]);
            }
            else if (ITRect.Length > 0)
            {
                detectedAlphabet += "ı";
                // return ITRect[0];
                listRect.Add(ITRect[0]);
            }

            Rectangle[] iRect = imageproc.FindAllImagesBoolArrays(i, alp.i, scannedWord, rectScannedWord);
            Rectangle[] IRect = imageproc.FindAllImagesBoolArrays(I, alp.I, scannedWord, rectScannedWord);
            if (iRect.Length > 0)
            {
                detectedAlphabet += "i";
                //return iRect[0];
                listRect.Add(iRect[0]);
            }
            else if (IRect.Length > 0)
            {
                detectedAlphabet += "i";
                // return IRect[0];
                listRect.Add(IRect[0]);
            }

            Rectangle[] jRect = imageproc.FindAllImagesBoolArrays(j, alp.j, scannedWord, rectScannedWord);
            Rectangle[] JRect = imageproc.FindAllImagesBoolArrays(J, alp.J, scannedWord, rectScannedWord);
            if (jRect.Length > 0)
            {
                detectedAlphabet += "j";
                //return jRect[0];
                listRect.Add(jRect[0]);
            }
            else if (JRect.Length > 0)
            {
                detectedAlphabet += "j";
                //return JRect[0];
                listRect.Add(JRect[0]);
            }

            Rectangle[] kRect = imageproc.FindAllImagesBoolArrays(k, alp.k, scannedWord, rectScannedWord);
            Rectangle[] KRect = imageproc.FindAllImagesBoolArrays(K, alp.K, scannedWord, rectScannedWord);
            if (kRect.Length > 0)
            {
                detectedAlphabet += "k";
                // return kRect[0];
                listRect.Add(kRect[0]);
            }
            else if (KRect.Length > 0)
            {
                detectedAlphabet += "k";
                //return KRect[0];
                listRect.Add(KRect[0]);
            }

            Rectangle[] lRect = imageproc.FindAllImagesBoolArrays(l, alp.l, scannedWord, rectScannedWord);
            Rectangle[] LRect = imageproc.FindAllImagesBoolArrays(L, alp.L, scannedWord, rectScannedWord);
            if (lRect.Length > 0)
            {
                detectedAlphabet += "l";
                // return lRect[0];
                listRect.Add(lRect[0]);
            }
            else if (LRect.Length > 0)
            {
                detectedAlphabet += "l";
                // return LRect[0];
                listRect.Add(LRect[0]);

            }

            Rectangle[] mRect = imageproc.FindAllImagesBoolArrays(m, alp.m, scannedWord, rectScannedWord);
            Rectangle[] MRect = imageproc.FindAllImagesBoolArrays(M, alp.M, scannedWord, rectScannedWord);
            if (mRect.Length > 0)
            {
                detectedAlphabet += "m";
                // return mRect[0];
                listRect.Add(mRect[0]);
            }
            else if (MRect.Length > 0)
            {
                detectedAlphabet += "m";
                // return MRect[0];
                listRect.Add(MRect[0]);
            }

            Rectangle[] nRect = imageproc.FindAllImagesBoolArrays(n, alp.n, scannedWord, rectScannedWord);
            Rectangle[] NRect = imageproc.FindAllImagesBoolArrays(N, alp.N, scannedWord, rectScannedWord);
            if (nRect.Length > 0)
            {
                detectedAlphabet += "n";
                //return nRect[0];
                listRect.Add(nRect[0]);
            }
            else if (NRect.Length > 0)
            {
                detectedAlphabet += "n";
                // return NRect[0];
                listRect.Add(NRect[0]);
            }

            Rectangle[] oRect = imageproc.FindAllImagesBoolArrays(o, alp.o, scannedWord, rectScannedWord);
            Rectangle[] ORect = imageproc.FindAllImagesBoolArrays(O, alp.O, scannedWord, rectScannedWord);
            if (oRect.Length > 0)
            {
                detectedAlphabet += "o";
                //return oRect[0];
                listRect.Add(oRect[0]);
            }
            else if (ORect.Length > 0)
            {
                detectedAlphabet += "o";
                //return ORect[0];
                listRect.Add(ORect[0]);
            }

            Rectangle[] oTRect = imageproc.FindAllImagesBoolArrays(oT, alp.oT, scannedWord, rectScannedWord);
            Rectangle[] OTRect = imageproc.FindAllImagesBoolArrays(OT, alp.OT, scannedWord, rectScannedWord);
            if (oTRect.Length > 0)
            {
                detectedAlphabet += "ö";
                //return oTRect[0];
                listRect.Add(oTRect[0]);
            }
            else if (OTRect.Length > 0)
            {
                detectedAlphabet += "ö";
                // return OTRect[0];
                listRect.Add(OTRect[0]);
            }

            Rectangle[] pRect = imageproc.FindAllImagesBoolArrays(p, alp.p, scannedWord, rectScannedWord);
            Rectangle[] PRect = imageproc.FindAllImagesBoolArrays(P, alp.P, scannedWord, rectScannedWord);
            if (pRect.Length > 0)
            {
                detectedAlphabet += "p";
                //return pRect[0];
                listRect.Add(pRect[0]);
            }
            else if (PRect.Length > 0)
            {
                detectedAlphabet += "p";
                //return PRect[0];
                listRect.Add(PRect[0]);
            }

            Rectangle[] qRect = imageproc.FindAllImagesBoolArrays(q, alp.q, scannedWord, rectScannedWord);
            Rectangle[] QRect = imageproc.FindAllImagesBoolArrays(Q, alp.Q, scannedWord, rectScannedWord);
            if (qRect.Length > 0)
            {
                detectedAlphabet += "q";
                //return qRect[0];
                listRect.Add(qRect[0]);
            }
            else if (QRect.Length > 0)
            {
                detectedAlphabet += "q";
                // return QRect[0];
                listRect.Add(QRect[0]);
            }

            Rectangle[] rRect = imageproc.FindAllImagesBoolArrays(r, alp.r, scannedWord, rectScannedWord);
            Rectangle[] RRect = imageproc.FindAllImagesBoolArrays(R, alp.R, scannedWord, rectScannedWord);
            if (rRect.Length > 0)
            {
                detectedAlphabet += "r";
                //return rRect[0];
                listRect.Add(rRect[0]);
            }
            else if (RRect.Length > 0)
            {
                detectedAlphabet += "r";
                // return RRect[0];
                listRect.Add(RRect[0]);
            }

            Rectangle[] sRect = imageproc.FindAllImagesBoolArrays(s, alp.s, scannedWord, rectScannedWord);
            Rectangle[] SRect = imageproc.FindAllImagesBoolArrays(S, alp.S, scannedWord, rectScannedWord);
            if (sRect.Length > 0)
            {
                detectedAlphabet += "s";
                // return sRect[0];
                listRect.Add(sRect[0]);
            }
            else if (SRect.Length > 0)
            {
                detectedAlphabet += "s";
                // return SRect[0];
                listRect.Add(SRect[0]);
            }

            Rectangle[] sTRect = imageproc.FindAllImagesBoolArrays(sT, alp.sT, scannedWord, rectScannedWord);
            Rectangle[] STRect = imageproc.FindAllImagesBoolArrays(ST, alp.ST, scannedWord, rectScannedWord);
            if (sTRect.Length > 0)
            {
                detectedAlphabet += "ş";
                //return sTRect[0];
                listRect.Add(sTRect[0]);
            }
            else if (STRect.Length > 0)
            {
                detectedAlphabet += "ş";
                //return STRect[0];
                listRect.Add(STRect[0]);
            }


            Rectangle[] tRect = imageproc.FindAllImagesBoolArrays(t, alp.t, scannedWord, rectScannedWord);
            Rectangle[] TRect = imageproc.FindAllImagesBoolArrays(T, alp.T, scannedWord, rectScannedWord);
            if (tRect.Length > 0)
            {
                detectedAlphabet += "t";
                //return tRect[0];
                listRect.Add(tRect[0]);
            }
            else if (TRect.Length > 0)
            {
                detectedAlphabet += "t";
                //return TRect[0];
                listRect.Add(TRect[0]);


            }

            Rectangle[] uRect = imageproc.FindAllImagesBoolArrays(u, alp.u, scannedWord, rectScannedWord);
            Rectangle[] URect = imageproc.FindAllImagesBoolArrays(U, alp.U, scannedWord, rectScannedWord);
            if (uRect.Length > 0)
            {
                detectedAlphabet += "u";
                //return uRect[0];
                listRect.Add(uRect[0]);
            }
            else if (URect.Length > 0)
            {
                detectedAlphabet += "u";
                //return URect[0];
                listRect.Add(URect[0]);


            }

            Rectangle[] uTRect = imageproc.FindAllImagesBoolArrays(uT, alp.uT, scannedWord, rectScannedWord);
            Rectangle[] UTRect = imageproc.FindAllImagesBoolArrays(UT, alp.UT, scannedWord, rectScannedWord);
            if (uTRect.Length > 0)
            {
                detectedAlphabet += "ü";
                // return uTRect[0];
                listRect.Add(uTRect[0]);
            }
            else if (UTRect.Length > 0)
            {
                detectedAlphabet += "ü";
                //return UTRect[0];
                listRect.Add(UTRect[0]);
            }

            Rectangle[] vRect = imageproc.FindAllImagesBoolArrays(v, alp.v, scannedWord, rectScannedWord);
            Rectangle[] VRect = imageproc.FindAllImagesBoolArrays(V, alp.V, scannedWord, rectScannedWord);
            if (vRect.Length > 0)
            {
                detectedAlphabet += "v";
                //return vRect[0];
                listRect.Add(vRect[0]);
            }
            else if (VRect.Length > 0)
            {
                detectedAlphabet += "v";
                // return VRect[0];
                listRect.Add(VRect[0]);
            }


            Rectangle[] wRect = imageproc.FindAllImagesBoolArrays(w, alp.w, scannedWord, rectScannedWord);
            Rectangle[] WRect = imageproc.FindAllImagesBoolArrays(W, alp.W, scannedWord, rectScannedWord);
            if (wRect.Length > 0)
            {
                detectedAlphabet += "w";
                // return wRect[0];
                listRect.Add(wRect[0]);
            }
            else if (WRect.Length > 0)
            {
                detectedAlphabet += "w";
                //return WRect[0];
                listRect.Add(WRect[0]);

            }


            Rectangle[] xRect = imageproc.FindAllImagesBoolArrays(x, alp.x, scannedWord, rectScannedWord);
            Rectangle[] XRect = imageproc.FindAllImagesBoolArrays(X, alp.X, scannedWord, rectScannedWord);
            if (xRect.Length > 0)
            {
                detectedAlphabet += "x";
                //return xRect[0];
                listRect.Add(xRect[0]);
            }
            else if (XRect.Length > 0)
            {
                detectedAlphabet += "x";
                //return XRect[0];
                listRect.Add(XRect[0]);

            }

            Rectangle[] yRect = imageproc.FindAllImagesBoolArrays(y, alp.y, scannedWord, rectScannedWord);
            Rectangle[] YRect = imageproc.FindAllImagesBoolArrays(Y, alp.Y, scannedWord, rectScannedWord);
            if (yRect.Length > 0)
            {
                detectedAlphabet += "y";
                // return yRect[0];
                listRect.Add(yRect[0]);
            }
            else if (YRect.Length > 0)
            {
                detectedAlphabet += "y";
                // return YRect[0];
                listRect.Add(YRect[0]);
            }

            Rectangle[] zRect = imageproc.FindAllImagesBoolArrays(z, alp.z, scannedWord, rectScannedWord);
            Rectangle[] ZRect = imageproc.FindAllImagesBoolArrays(Z, alp.Z, scannedWord, rectScannedWord);
            if (zRect.Length > 0)
            {
                detectedAlphabet += "z";
                //return zRect[0];
                listRect.Add(zRect[0]);
            }
            else if (ZRect.Length > 0)
            {
                detectedAlphabet += "z";
                // return ZRect[0];
                listRect.Add(ZRect[0]);
            }
            if (listRect.Count > 0)
            {
                DebugPfCnsl.println("listRect length = " + listRect.Count);
                DebugPfCnsl.println("listRect Rect Result = ");
                DebugPfCnsl.PrintArray(listRect.ToArray());

                if (listRect.Count > 1)
                {
                    int minXValue = int.MaxValue;

                    int maxHeight = int.MinValue;
                    int maxWidth = int.MinValue;

                    List<int> indexes = new List<int> { 0 };
                    //Get minXValue end
                    for (int i = 0; i < listRect.Count; i++)
                    {
                        if (minXValue > listRect.ElementAt(i).X)
                        {
                            minXValue = listRect.ElementAt(i).X;
                            maxHeight = listRect.ElementAt(i).Height;
                            maxWidth = listRect.ElementAt(i).Width;

                            indexes[0] = i;
                        }
                        else if (minXValue == listRect.ElementAt(i).X &&
                            maxHeight < listRect.ElementAt(i).Height)
                        {
                            maxHeight = listRect.ElementAt(i).Height;
                            indexes.Add(i);
                        }
                        else if (minXValue == listRect.ElementAt(i).X &&
                            maxHeight == listRect.ElementAt(i).Height &&
                            maxWidth < listRect.ElementAt(i).Width)
                        {
                            maxWidth = listRect.ElementAt(i).Width;
                            indexes.Add(i);
                        }
                    }

                    DebugPfCnsl.println("returned rect  = " + listRect.ElementAt(indexes.ElementAt(indexes.Count - 1)));
                    return listRect.ElementAt(indexes.ElementAt(indexes.Count - 1));
                }



                return listRect.ElementAt(listRect.Count - 1);
            }
            else
            {
                return new Rectangle(rectScannedWord.X, rectScannedWord.Y, 1, rectScannedWord.Height);
            }


        }*/

    }
}
