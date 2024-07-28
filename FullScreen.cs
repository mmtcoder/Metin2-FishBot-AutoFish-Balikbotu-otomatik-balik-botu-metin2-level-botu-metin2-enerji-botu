using MusicPlayerApp.Debugs;
using MusicPlayerApp.Sources;
using MusicPlayerApp.Sources.ImageHandle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp
{
    public partial class FullScreen : Form
    {
        private ScreenShotWinAPI screenShot;
        private DebugDrawingHandle drawingDebug;
        public  bool isFormLoaded = false;

        static Rectangle ScreenBounds = Screen.PrimaryScreen.Bounds;
        private Bitmap fullScreenImageBtmap = new Bitmap(ScreenBounds.Width,ScreenBounds.Height);
        public FullScreen()
        {
            InitializeComponent();
            DebugPfCnsl.println("FullScreen constructor is called");
            this.FormBorderStyle = FormBorderStyle.None; // Kenarlık olmadan
            this.WindowState = FormWindowState.Maximized; // Tam ekran
            this.TopMost = true; // Diğer pencerelerin üstünde

            screenShot = new ScreenShotWinAPI();
            fullScreenImageBtmap = screenShot.CaptureScreen();
            this.BackgroundImage = fullScreenImageBtmap;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            drawingDebug = new DebugDrawingHandle();
            this.DoubleBuffered = true; // Çift tamponlamayı etkinleştirme, ekran titremesini önler
        }

        private void FullScreen_Paint(object sender, PaintEventArgs e)
        {

            // base.OnPaint(e);
         //   Console.WriteLine("Fullscreen paint is called");
                drawingDebug.OnPaint(e);
            
         
        }

        private void FullScreen_MouseMove(object sender, MouseEventArgs e)
        {
            drawingDebug.OnMouseMove(e);
            this.Invalidate(); // Yeniden çizim talep et
        }

        private void FullScreen_MouseDown(object sender, MouseEventArgs e)
        {
            drawingDebug.OnMouseDown(e);
            
        }

        private void FullScreen_MouseUp(object sender, MouseEventArgs e)
        {
            drawingDebug.OnMouseUp(e);
            // Dikdörtgen çizimi bittiğinde ekranı yeniden çiz
            this.Invalidate();
            
            MainForm.pictureBox.Image = fullScreenImageBtmap.Clone(drawingDebug.getResultRectangle()
                ,fullScreenImageBtmap.PixelFormat);
            ScreenShotWinAPI.EditBipMapEndSave(fullScreenImageBtmap, drawingDebug.getResultRectangle()
                , "test.png", PathWayStruct.PATH_DESTKOP);

            Bitmap bitmap = null;
            bitmap = screenShot.ClipBitmap(fullScreenImageBtmap, drawingDebug.getResultRectangle());
            Console.WriteLine("determined rectangle result " + drawingDebug.getResultRectangle());
            DebugPfCnsl.printIntArrayAsDescended(screenShot.ConvertBitmapToArray(bitmap));
            //DebugPfCnsl.PrintArray(screenShot.ConvertBitmapToArray(bitmap));

            Close();

        }

        private void FullScreen_Load(object sender, EventArgs e)
        {
            isFormLoaded = true;
           
        }
    }
}
