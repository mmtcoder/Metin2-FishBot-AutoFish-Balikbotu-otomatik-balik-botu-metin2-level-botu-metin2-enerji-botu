using Metin2AutoFishCSharp.Sources.Inputs;
using MusicPlayerApp.Sources.CoordinatesHandler;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace MusicPlayerApp.Sources
{
    internal class GameInputHandler : KeyboardTextInput
    {
        // user32.dll'deki mouse eventlerini tetikleyen fonksiyonları tanımlamak için kullanılan metotlar
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        // user32.dll'deki SetCursorPos fonksiyonunu tanımlamak için kullanılan metot
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008; // Sağ tık bas
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;   // Sağ tık bırak

        private static readonly object lockMouseClick = new object();
        private static readonly object lockMouseRightClick = new object();
        private static readonly object lockMouseMove = new object();

       
        public GameInputHandler() 
        {
            
        }

        /// <summary>
        /// Click quikly to specified point
        /// @@ Warning : This method can't handle multiple Thread
        /// so you should use this method in the one thread.
        /// You can use this just for critic performans things
        /// like clicking a fish in the Metin2...
        /// </summary>
        /// <param name="x"> x coordinate of the screen </param>
        /// <param name="y"> y coordinate of the screen </param>
       
        public void MouseClickQuickly (int x,int y) 
        {
            lock (lockMouseClick)
            {
                // Mouse'un sol düğmesini basılı tut
                mouse_event(0x0002, (uint)x, (uint)y, 0, 0);
                // Kısa bir süre bekle
                Thread.Sleep(5);
                // Mouse'un sol düğmesini serbest bırak
                mouse_event(0x0004, (uint)x, (uint)y, 0, 0);

                Thread.Sleep(5);

                
              }

        }

        public void MouseClick(int x, int y)
        {
            lock (lockMouseClick)
            {
                // Mouse'un sol düğmesini basılı tut
                mouse_event(0x0002, (uint)x, (uint)y, 0, 0);
                // Kısa bir süre bekle
                Thread.Sleep(5);
                // Mouse'un sol düğmesini serbest bırak
                mouse_event(0x0004, (uint)x, (uint)y, 0, 0);

                TimerGame.SleepRandom(40, 65);


            }

        }

        public void MouseDown()
        {
            lock(lockMouseClick)
            {
                // Mouse'un sol düğmesini basılı tut
                mouse_event(0x0002, (uint)0, (uint)0, 0, 0);
                // Kısa bir süre bekle
                Thread.Sleep(5);

            }
        }

        public void MouseRelease()
        {
            lock (lockMouseClick)
            {
                // Mouse'un sol düğmesini serbest bırak
                mouse_event(0x0004, 0, 0, 0, 0);

                TimerGame.SleepRandom(3,6);
            }
            
        }
        /// <summary>
        /// Move quikly to specified point
        /// @@ Warning : This method can't handle multiple Thread
        /// so you should use this method in the one thread.
        /// You can use this just for critic performans things
        /// like moving cursor in order to click a fish in the Metin2...
        /// </summary>
        /// <param name="x"> x coordinate of the screen </param>
        /// <param name="y"> y coordinate of the screen </param>
        public void MouseMoveQuickly(int x, int y)
        {
            
            lock (lockMouseMove)
            {
                // Fare imlecini belirtilen konuma taşı
                SetCursorPos(x , y);
                //sim.Mouse.MoveMouseTo(x, y);
                Thread.Sleep(5);
            }
        }

        
        public void MouseMove(int x,int y)
        {
            lock (lockMouseMove)
            {
                // Fare imlecini belirtilen konuma taşı
                SetCursorPos(x , y);
                TimerGame.SleepRandom(40, 60);
            }
            
        }

        public void MouseMoveAndPressLeft(int x, int y)
        {
            lock (lockMouseClick)
            {
                MouseMove(x, y);
                MouseClick(x, y);
            }
        }

        public void MouseMoveAndPressRight(int x,int y)
        {
            lock(lockMouseMove)
            {
                MouseMove(x, y);
                // Mouse'un sağ düğmesini basılı tut
                mouse_event(MOUSEEVENTF_RIGHTDOWN, (uint)0, (uint)0, 0, 0);
                // Kısa bir süre bekle
                TimerGame.SleepRandom(10, 20);
                // Mouse'un sağ düğmesini serbest bırak
                mouse_event(MOUSEEVENTF_RIGHTUP, (uint)0, (uint)0, 0, 0);
                TimerGame.SleepRandom(40, 60);
            }
        }

      
    }
}
