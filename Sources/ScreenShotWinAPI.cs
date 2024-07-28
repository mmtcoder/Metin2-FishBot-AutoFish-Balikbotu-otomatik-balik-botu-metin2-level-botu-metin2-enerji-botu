using MusicPlayerApp.Debugs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp.Sources
{
    internal class ScreenShotWinAPI
    {
        // Windows API'den gerekli fonksiyonları kullanmak için dış kod bildirimi
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hwnd, ref Rectangle rect);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        const int SRCCOPY = 0x00CC0020;

        const int CAPTUREBLT = 0x40000000;

        static object lockObject = new object();
        static object lockObject2 = new object();

        // Ekran görüntüsü alma işlemi
        public Bitmap CaptureScreen()
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            Bitmap fullScreen = new Bitmap(bounds.Width,bounds.Height);
            lock (lockObject)
            {
              

                
                IntPtr desktophWnd = IntPtr.Zero;
                IntPtr desktopDc = IntPtr.Zero;
                IntPtr memoryDc = IntPtr.Zero;
                IntPtr bitmap = IntPtr.Zero;
                IntPtr oldBitmap = IntPtr.Zero;
                bool success;
                Graphics g = null;

                try
                {
                    desktophWnd = GetDesktopWindow();

                    // Uyumluluk sağlayan bir DC oluşturun
                    desktopDc = GetWindowDC(desktophWnd);
                    memoryDc = CreateCompatibleDC(desktopDc);

                    // Yeni bir bitmap oluşturun ve uyumlu bir DC ile ilişkilendirin
                    bitmap = CreateCompatibleBitmap(desktopDc, bounds.Width, bounds.Height);
                    oldBitmap = SelectObject(memoryDc, bitmap);

                    success = BitBlt(memoryDc, 0, 0, bounds.Width, bounds.Height, desktopDc, bounds.Left, bounds.Top, SRCCOPY | CAPTUREBLT);

                    if (!success)
                    {
                        throw new Win32Exception();
                    }

                    // Bitmap'i doldurun
                    using (Bitmap result = Image.FromHbitmap(bitmap))
                    {
                        using (Graphics graphics = Graphics.FromImage(fullScreen))
                        {
                            graphics.DrawImage(result, Point.Empty);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("An argument exception occurred: " + ex.Message);

                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while capturing the screen: " + ex.Message);
                }
                finally
                {
                    SelectObject(memoryDc, oldBitmap);
                    DeleteObject(bitmap);
                    DeleteDC(memoryDc);
                    ReleaseDC(desktophWnd, desktopDc); ;
                }
            }
            return fullScreen;
        }

        public Bitmap CaptureSpecifiedScreen(Rectangle rectDefined)
        {
            if (rectDefined == null || rectDefined == Rectangle.Empty || rectDefined.Width <= 0
                || rectDefined.Height <= 0)
            {
                // throw new Exception("recDfined cannot be null or empty");
                DebugPfCnsl.println("CaptureSpecifiedScreen is returned null");
                return null;
            }
                
            Bitmap fullScreen = new Bitmap(rectDefined.Width, rectDefined.Height);
            lock (lockObject)
            {
                
                Rectangle bounds = rectDefined;
                IntPtr desktophWnd = IntPtr.Zero;
                IntPtr desktopDc = IntPtr.Zero;
                IntPtr memoryDc = IntPtr.Zero;
                IntPtr bitmap = IntPtr.Zero;
                IntPtr oldBitmap = IntPtr.Zero;
                bool success;
                Graphics g = null;

                try
                {
                    desktophWnd = GetDesktopWindow();

                    // Uyumluluk sağlayan bir DC oluşturun
                    desktopDc = GetWindowDC(desktophWnd);
                    memoryDc = CreateCompatibleDC(desktopDc);

                    // Yeni bir bitmap oluşturun ve uyumlu bir DC ile ilişkilendirin
                    bitmap = CreateCompatibleBitmap(desktopDc, bounds.Width, bounds.Height);
                    oldBitmap = SelectObject(memoryDc, bitmap);

                    success = BitBlt(memoryDc, 0, 0, bounds.Width, bounds.Height, desktopDc, bounds.Left, bounds.Top, SRCCOPY | CAPTUREBLT);

                    if (!success)
                    {
                        throw new Win32Exception();
                    }

                    // Bitmap'i doldurun
                    using (Bitmap result = Image.FromHbitmap(bitmap))
                    {
                        using (Graphics graphics = Graphics.FromImage(fullScreen))
                        {
                            graphics.DrawImage(result, Point.Empty);
                        }
                    }
                }
                catch (ArgumentException ex)
                {
                    throw new ArgumentException("An argument exception occurred: " + ex.Message);
                    
                }
                catch (Exception ex)
                {
                    //throw new Exception("An error occurred while capturing the screen: " + ex.Message);
                    if(fullScreen != null)fullScreen.Dispose();
                }
                finally
                {
                    SelectObject(memoryDc, oldBitmap);
                    DeleteObject(bitmap);
                    DeleteDC(memoryDc);
                    ReleaseDC(desktophWnd, desktopDc); ;
                }
            }
           
            return fullScreen;
        }

        public Bitmap CaptureScreenBasic(Rectangle rect)
        {
            if(rect == null || rect.Width <= 0 ||rect.Height <= 0)
            {
                DebugPfCnsl.println("CaptureScreenBasic returned null");
            return null; 
            }
            lock (lockObject) 
           {
                Bitmap captureBitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);

                /* Rectangle bounds = Screen.PrimaryScreen.Bounds;
                 //screenshot = new Bitmap(bounds.Width, bounds.Height);
                 using (Graphics g = Graphics.FromImage(screenshot))
                 {
                     g.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size);
                 }*/

                try
                {
                    
                    //Creating a New Graphics Object
                    Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                    //Copying Image from The Screen
                    captureGraphics.CopyFromScreen(rect.Left, rect.Top, 0, 0, rect.Size);
                    
                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return captureBitmap;
            }
            
        }

        public static void EditBipMapEndSave(Bitmap fullScreenBitMap, Rectangle rect, string fileName, PathWayStruct pathWay)
        {
            try
            {
                if (fullScreenBitMap != null && fullScreenBitMap.Width > 0)
                {
                    if (rect != null && rect.Width > 0)
                    {
                        Bitmap bitmapResult = fullScreenBitMap.Clone(rect, fullScreenBitMap.PixelFormat);

                        FileHandler.SaveImageAsPng(bitmapResult, fileName, pathWay);
                    }
                    else
                    {
                        DebugPfCnsl.println("rect is null or widht is less than zero editBipMapEndSave func");
                    }

                }
                else
                {
                    DebugPfCnsl.println(" bitmap null or widht is less than zero in editBipMapEndSave FUNC!!!");
                }
            }
            catch (OutOfMemoryException outEx)
            {
                DebugPfCnsl.println(" ScreenShotWinAPI EditBipMapEndSave error = " + outEx.Message);
            }

            catch (ArgumentException argEx)
            {
                DebugPfCnsl.println(argEx.Message);
            }

        }

        public int[] ConvertBitmapToArray(Bitmap screenshot)
        {
          
            if (screenshot == null || screenshot.Width < 0) 
            {// new Exception("bitmap value can't be null or less than zero");,
                DebugPfCnsl.println("ConvertBitmapToArray is returned null");
                return null;
            }
            int[] pixelData = new int[screenshot.Width * screenshot.Height];
            Color pixelColor = Color.White;
            try
            {
              
               
                for (int y = 0; y < screenshot.Height; y++)
                {
                    for (int x = 0; x < screenshot.Width; x++)
                    {
                        pixelColor = screenshot.GetPixel(x, y);
                        pixelData[(y * screenshot.Width) + x] = pixelColor.ToArgb();
                    }
                }

             
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return pixelData;
        }

        public int[] ImageArraySpecifiedArea(Rectangle rectangle)
        {
            return ConvertBitmapToArray(CaptureSpecifiedScreen(rectangle));
        }
    

        public Bitmap ClipBitmap(Bitmap fullScreenBitmap, Rectangle rect)
        {
            lock (lockObject)
            {
                if (fullScreenBitmap != null && fullScreenBitmap.Width > 0)
                {
                    if (rect != null && rect.Width > 0)
                    {
                        try
                        {
                                Bitmap bitmapResult = fullScreenBitmap.Clone(rect, fullScreenBitmap.PixelFormat);
                                return bitmapResult;
                            
                        }
                        catch (Exception ex)
                        {
                            // Hata durumunda, hata mesajını loglayın ve null döndürün
                            DebugPfCnsl.println("An error occurred while clipping bitmap: " + ex.Message);
                            return null;
                        }
                    }
                    else
                    {
                        DebugPfCnsl.println("The rectangle is null or the width is less than zero.");
                    }
                }
                else
                {
                    //DebugPfCnsl.println("The bitmap is null or the width is less than zero.");
                }
            }
            return null;
        }
    }

  
}