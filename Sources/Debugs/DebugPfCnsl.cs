using MusicPlayerApp.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace MusicPlayerApp.Debugs
{
    internal class DebugPfCnsl
    {
        public static readonly ConsoleColor COLOR_DARK_RED = ConsoleColor.DarkRed;
        public static readonly ConsoleColor COLOR_DARK_GREEN = ConsoleColor.DarkGreen;
        public static readonly ConsoleColor COLOR_DARK_YELLOW = ConsoleColor.DarkYellow;
        public static readonly ConsoleColor COLOR_DARK_BLUE = ConsoleColor.DarkBlue;
        public static readonly ConsoleColor COLOR_DARK_MAGENTA = ConsoleColor.DarkMagenta;
        public static readonly ConsoleColor COLOR_BRIGHT_RED = ConsoleColor.Red;
        public static readonly ConsoleColor COLOR_BRIGHT_GREEN = ConsoleColor.Green;
        public static readonly ConsoleColor COLOR_BRIGHT_BLUE = ConsoleColor.Blue;
        public static readonly ConsoleColor COLOR_BRIGHT_MAGENTA = ConsoleColor.Magenta;

        private static int colorChangerCounter;

        private static ConsoleColor[] arrayColors = new ConsoleColor[]
        {
        COLOR_DARK_RED,COLOR_DARK_GREEN,COLOR_DARK_YELLOW,
        COLOR_DARK_BLUE,COLOR_DARK_MAGENTA,COLOR_BRIGHT_RED,
        COLOR_BRIGHT_GREEN,COLOR_BRIGHT_BLUE,COLOR_BRIGHT_MAGENTA
        };

        private TimerGame timerDebug = new TimerGame();

        public static void println(string message)
        {

            // Console.BackgroundColor = colorPref;
            Console.WriteLine(message + "     " + DateTime.Now);
        }

        public static void printBitMapArray(Bitmap bitmap)
        {
            int[] array = new int[bitmap.Width * bitmap.Height];
            if (bitmap == null) { println("bitmap value is NULL"); return; }
            println("bitmap total size pixel = " + bitmap.Size);
            println("bitmap total width = " + bitmap.Width);

            int width = bitmap.Width;
            int height = bitmap.Height;
           

            Color pixelColor = Color.White;
          //  Console.Write("Array result = [ ");
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    pixelColor = bitmap.GetPixel(x, y);
                    int val1 = pixelColor.ToArgb();
                    //int valNotAlpha = (0x00FFFFFF) & (val1);
                    /*Console.WriteLine("index value = {0}, dec value ={1}, hex value = {2},",
                        x,val1,val1.ToString("X"));*/
                   // Console.WriteLine("index value = " + x + " dec value = " + val1
                    //    + "  hex value = " + val1.ToString("X") + "  hex value RGB =" + valNotAlpha.ToString("X"));
                    array[(y * width) + x] = val1;
                }

            }
          //  Console.Write(" ]");

        
            

            printIntArrayAsDescended(array);
        }

        public static void printIntArrayAsDescended(int[] array)
        {
            if(array == null || array.Length <= 0)
            { println("printInt fonction array variable = null"); return; }

            println("array lenght of the printIntAsDescended fun = " + array.Length);
            // Dizideki öğeleri gruplara ayırma ve her bir öğenin sayısını hesaplama
            var grouped = array.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() });

            // Öğeleri sayılarına göre büyükten küçüğe sıralama
            var sorted = grouped.OrderByDescending(g => g.Count);

            // Sıralı öğeleri yeni bir diziye ekleyerek döndürme
            int[] result = sorted.SelectMany(g => Enumerable.Repeat(g.Value, g.Count)).ToArray();

            Console.Write("Descended common values result = ");
            // Sonucu yazdırma
            foreach (int num in result)
            {
                Console.WriteLine($"Decimal: {num}, Hexadecimal: 0x{num:X}");
            }
        }

        public static void printIntArray(int[] argbArray)
        {
            Console.WriteLine("PrintIntArray function result = ");
            foreach (int num in argbArray)
            {
                Console.WriteLine($"Decimal: {num}, Hexadecimal: 0x{num:X}");
                Console.WriteLine("Red value = " + ((num >> 16)& 0xFF) + " Blue value = " +
                    ((num >> 8)& 0xFF) + " Green value = " + (num & 0xFF));
            }
        }

        public void printlnTime(string message,int delayTimeSecond)
        {
           if(timerDebug.CheckDelayTimeInSecond(delayTimeSecond))
            {
             
            }
            else
            {
                Console.WriteLine(message + "     " + DateTime.Now);
                timerDebug.SetStartedSecondTime();
            }

           
        }

        public static void PrintArray<T>(T[] array)
        {
            if (array != null)
            {
                println("PrintArray func array length = " + array.Length.ToString());

                foreach (T item in array)
                {
                    Console.WriteLine(item);
                }
            }
            else
            {
                println("PrintArray function param is null");
            }

            
        }

        /* public static void printlnRndColor(string message)
         {
             if (colorChangerCounter >= arrayColors.Length) colorChangerCounter = 0;
             Console.BackgroundColor = arrayColors[colorChangerCounter++];
             Console.WriteLine(message + " " + DateTime.Now);

         }*/


    }
}
