using MusicPlayerApp.Sources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp.Debugs
{
    internal class DebugDrawingHandle
    {

        private Point startPoint;
        private static Rectangle resutlRectangle = new Rectangle(1, 1, 1, 1);
        private bool drawing;
        public static bool staticBoolRect { private get; set; } = false;

        public static Rectangle staticRectangle {  get; set; } = Rectangle.Empty;
        public static Rectangle[] staticRectangles { get; set; }

        Pen pen;


        public DebugDrawingHandle()
        {
            InitializeDrawing();
        }


        private void InitializeDrawing()
        {
            drawing = false;
            resutlRectangle = new Rectangle();
            startPoint = new Point();
            pen = new Pen(Color.Red, 2);


        }

        public void OnPaint(PaintEventArgs e)
        {
            if (staticBoolRect)
            {
                if(staticRectangle != Rectangle.Empty)
                {
                    e.Graphics.DrawRectangle(pen, staticRectangle);
                    resutlRectangle = staticRectangle;
                }
                if(staticRectangles != null)
                {
                    foreach(var rect in staticRectangles)
                    {
                        if(rect != Rectangle.Empty)
                        {
                            e.Graphics.DrawRectangle(pen, rect);
                        }
                        else
                        {
                            DebugPfCnsl.println("Onpaint func " +  rect.ToString() + "is empty rect !!");
                        }
                        
                    }
                }
               
            }
            if (drawing)
            {
                e.Graphics.DrawRectangle(pen, resutlRectangle);
                staticBoolRect = false;
            }// gForge yukari ok poz = {X=543,Y=273,Width=13,Height=11} oyna button y pozisyonu =  {X=471,Y=369,Width=5,Height=5}
        }

        //@@@@@@@@@ HANDLE MOUSE EVENTS   @@@@@@@@@@@@@@@@@
        public void OnMouseDown(MouseEventArgs e)
        {
            //  Console.WriteLine("OnMouseDown is called");
            if (e.Button == MouseButtons.Left)
            {
                drawing = true;
                startPoint = e.Location;
            }
        }

        public void OnMouseUp(MouseEventArgs e)
        {
            if (drawing && e.Button == MouseButtons.Left)
            {
                drawing = false;
                staticRectangle = resutlRectangle;
                //staticRectangle = Rectangle.Empty;
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            // Console.WriteLine("OnMouseMove is called");
            if (drawing)
            {
                resutlRectangle.Location = new Point(
                    Math.Min(startPoint.X, e.X),
                    Math.Min(startPoint.Y, e.Y)
                );
                resutlRectangle.Size = new Size(
                    Math.Abs(startPoint.X - e.X),
                    Math.Abs(startPoint.Y - e.Y)
                );

            }
        }

        public Rectangle getResultRectangle()
        {
            if (resutlRectangle.Width <= 0 || resutlRectangle.Height <= 0)
            {
                DebugPfCnsl.println("DebugDrawingHandle getResultRectangle func width height zero");
                resutlRectangle = new Rectangle(1, 1, 2, 2);
            }

            return resutlRectangle;
            // return new Rectangle(5,5,49,49);
        }

        public static void SetStaticRectangle(Rectangle rect)
        {
            staticBoolRect = true;
            staticRectangle = rect;
        }
        public static void SetStaticRectangles(Rectangle[] rects)
        {
            staticBoolRect = true;
            staticRectangles = rects;
        }

        public static void DrawWantedObjectToScreen(params Rectangle[] rects)
        {
            if(rects == null ||rects.Length <= 0)
            {
                throw new Exception("rect length cannot be 0 or null");

            }

            SetStaticRectangles(rects);
            Application.Run(new FullScreen());
            TimerGame.SleepRandom(3000, 3200);
        }
    }

}
