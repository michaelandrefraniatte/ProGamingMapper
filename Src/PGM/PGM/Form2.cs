using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using AlphaMode = SharpDX.Direct2D1.AlphaMode;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using Rectangle = System.Drawing.Rectangle;
using Bitmap = System.Drawing.Bitmap;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace PGM
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        [DllImport("winmm.dll", EntryPoint = "timeBeginPeriod")]
        public static extern uint TimeBeginPeriod(uint ms);
        [DllImport("winmm.dll", EntryPoint = "timeEndPeriod")]
        public static extern uint TimeEndPeriod(uint ms);
        [DllImport("ntdll.dll", EntryPoint = "NtSetTimerResolution")]
        public static extern void NtSetTimerResolution(uint DesiredResolution, bool SetResolution, ref uint CurrentResolution);
        public static uint CurrentResolution = 0;
        private static WindowRenderTarget target;
        private static SharpDX.Direct2D1.Factory1 fact = new SharpDX.Direct2D1.Factory1();
        private static RenderTargetProperties renderProp;
        private static HwndRenderTargetProperties winProp;
        private static int width, height;
        private void Form2_Load(object sender, EventArgs e)
        {
            TimeBeginPeriod(1);
            NtSetTimerResolution(1, true, ref CurrentResolution);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            width = this.pictureBox1.Width;
            height = this.pictureBox1.Height;
            InitDisplayCapture(this.pictureBox1.Handle);
        }
        public void SetWebcamInputs(Bitmap EditableImg)
        {
            try 
            {
                DisplayCapture(EditableImg);
            }
            catch { }
        }
        private static void InitDisplayCapture(IntPtr handle)
        {
            renderProp = new RenderTargetProperties()
            {
                DpiX = 0,
                DpiY = 0,
                MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                PixelFormat = new SharpDX.Direct2D1.PixelFormat(Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied),
                Type = RenderTargetType.Hardware,
                Usage = RenderTargetUsage.None
            };
            winProp = new HwndRenderTargetProperties()
            {
                Hwnd = handle,
                PixelSize = new Size2(width, height),
                PresentOptions = PresentOptions.Immediately
            };
            target = new WindowRenderTarget(fact, renderProp, winProp);
        }
        private static void DisplayCapture(Bitmap image1)
        {
            using (var bmp = image1)
            {
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                SharpDX.DataStream stream = new SharpDX.DataStream(bmpData.Scan0, bmpData.Stride * bmpData.Height, true, false);
                SharpDX.Direct2D1.PixelFormat pFormat = new SharpDX.Direct2D1.PixelFormat(SharpDX.DXGI.Format.B8G8R8A8_UNorm, AlphaMode.Premultiplied);
                SharpDX.Direct2D1.BitmapProperties bmpProps = new SharpDX.Direct2D1.BitmapProperties(pFormat);
                SharpDX.Direct2D1.Bitmap result = new SharpDX.Direct2D1.Bitmap(target, new SharpDX.Size2(width, height), stream, bmpData.Stride, bmpProps);
                bmp.UnlockBits(bmpData);
                stream.Dispose();
                bmp.Dispose();
                target.BeginDraw();
                target.Clear(new SharpDX.Mathematics.Interop.RawColor4(0, 0, 0, 1f));
                target.DrawBitmap(result, 1.0f, BitmapInterpolationMode.NearestNeighbor);
                target.EndDraw();
            }
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            Form1.red = trackBar1.Value;
        }
        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            Form1.green = trackBar2.Value;
        }
        private void trackBar3_ValueChanged(object sender, EventArgs e)
        {
            Form1.blue = trackBar3.Value;
        }
        private void trackBar4_ValueChanged(object sender, EventArgs e)
        {
            Form1.brightness = trackBar4.Value;
        }
        private void trackBar5_ValueChanged(object sender, EventArgs e)
        {
            Form1.radius = trackBar5.Value;
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.form2visible = false;
            this.Hide();
            e.Cancel = true;
        }
    }
}
