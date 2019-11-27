using NativeImport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShimLib {
    public class ZoomPictureBox : PictureBox {
        private int dispBW;
        private int dispBH;
        private IntPtr dispBuf;
        private Bitmap dispBmp;
        private int imgBW;
        private int imgBH;
        private IntPtr imgBuf;

        private static float[] zoomFactors = { 1f / 512, 3f / 1024, 1f / 256, 3f / 512, 1f / 128, 3f / 256, 1f / 64, 3f / 128, 1f / 32, 3f / 64, 1f / 16, 3f / 32, 1f / 8, 3f / 16, 1f / 4, 3f / 8, 1f / 2, 3f / 4, 1, 3f / 2, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96 };
        private static string[] zoomTexts = { "1/512", "3/1024", "1/256", "3/512", "1/128", "3/256", "1/64", "3/128", "1/32", "3/64", "1/16", "3/32", "1/8", "3/16", "1/4", "3/8", "1/2", "3/4", "1", "3/2", "2", "3", "4", "6", "8", "12", "16", "24", "32", "48", "64", "96" };
        private const int zoomLevelReset = 8;

        private int zoomLevel = zoomLevelReset;
        [Browsable(false)]
        public float ZoomFactor { get { return zoomFactors[zoomLevel]; } }
        private string ZoomText { get { return zoomTexts[zoomLevel]; } }

        private Point ptPanning;
        [Browsable(false)]
        public Point PtPanning { get { return ptPanning; } }

        public bool UseDrawPixelValue { get; set; } = true;
        public bool UseDrawInfo { get; set; } = true;
        public bool UseDrawCenterLine { get; set; } = true;

        public ZoomPictureBox() {
            AllocDispBuf();
            RedrawImage();
        }

        ~ZoomPictureBox() {
            FreeDispBuf();
        }

        // 이미지 버퍼 세팅
        public void SetImgBuf(int bw, int bh, IntPtr buf) {
            imgBW = bw;
            imgBH = bh;
            imgBuf = buf;
            RedrawImage();
        }

        // zoom 리셋
        public void ResetZoom() {
            ptPanning = Point.Empty;
            zoomLevel = zoomLevelReset;
            RedrawImage();
        }

        protected override void OnLayout(LayoutEventArgs e) {
            base.OnLayout(e);

            AllocDispBuf();
            RedrawImage();
        }

        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics;

            g.DrawImage(dispBmp, 0, 0);
            if (UseDrawPixelValue)
                DrawPixelValue(g);
            if (UseDrawCenterLine)
                DrawCenterLine(g);
            
            base.OnPaint(e);

            if (UseDrawInfo)
                DrawInfo(g);
        }

        bool mouseDown = false;
        Point ptOld;
        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            if (Control.ModifierKeys == Keys.Control) {
                WheelScrollV(e);
            } else if (Control.ModifierKeys == Keys.Shift) {
                WheelScrollH(e);
            } else {
                WheelZoom(e);
            }
            RedrawImage();
        }

        // 휠 스크롤
        private void WheelScrollV(MouseEventArgs e) {
            int scroll = 128;
            ptPanning.Y += (e.Delta > 0) ? scroll : -scroll;
        }
        private void WheelScrollH(MouseEventArgs e) {
            int scroll = 128;
            ptPanning.X += (e.Delta > 0) ? scroll : -scroll;
        }

        // 휠 줌
        private void WheelZoom(MouseEventArgs e) {
            var ptImg = DispToImg(e.Location);

            var zoomFacotrOld = ZoomFactor;
            zoomLevel = (e.Delta > 0) ? zoomLevel + 1 : zoomLevel - 1;
            if (zoomLevel < 0)
                zoomLevel = 0;
            if (zoomLevel >= zoomFactors.Length)
                zoomLevel = zoomFactors.Length - 1;

            ptPanning.X += (int)Math.Floor(ptImg.X * (zoomFacotrOld - ZoomFactor));
            ptPanning.Y += (int)Math.Floor(ptImg.Y * (zoomFacotrOld - ZoomFactor));
        }

        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left) {
                mouseDown = true;
                ptOld = e.Location;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (mouseDown) {
                ptPanning += ((Size)e.Location - (Size)ptOld);
                ptOld = e.Location;
                RedrawImage();
            } else {
                this.Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        // 표시 버퍼 할당
        private void AllocDispBuf() {
            FreeDispBuf();

            dispBW = Math.Max((this.ClientSize.Width + 3) / 4 * 4, 0);
            dispBH = Math.Max(this.ClientSize.Height, 0);

            dispBuf = Marshal.AllocHGlobal((IntPtr)(dispBW * dispBH));
            MsvcrtDll.memset(dispBuf, 0, (ulong)(dispBW * dispBH));
            dispBmp = new Bitmap(dispBW, dispBH, dispBW, PixelFormat.Format8bppIndexed, dispBuf);
            var pal = dispBmp.Palette;
            for (int i = 0; i < 256; i++) {
                pal.Entries[i] = Color.FromArgb(i, i, i);
            }
            dispBmp.Palette = pal;
        }

        // 표시 버퍼 해제
        private void FreeDispBuf() {
            if (dispBmp != null)
                dispBmp.Dispose();
            if (dispBuf != IntPtr.Zero)
                Marshal.FreeHGlobal(dispBuf);
        }

        public void RedrawImage() {
            if (imgBuf == IntPtr.Zero) {
                MsvcrtDll.memset(dispBuf, 128, (ulong)dispBW * (ulong)dispBH);
            } else {
                var rect = this.ClientRectangle;
                int dw = Math.Min(rect.Width, dispBW);
                int dh = Math.Min(rect.Height, dispBH);
                NativeDll.CopyImageBufferZoom(imgBuf, imgBW, imgBH, dispBuf, dispBW, dispBH, dw, dh, ptPanning.X, ptPanning.Y, ZoomFactor, true);
            }
            this.Invalidate();
        }

        // 중심선 표시
        private void DrawCenterLine(Graphics g) {
            if (imgBuf == null)
                return;

            Pen pen = new Pen(Color.Yellow);
            pen.DashStyle = DashStyle.Dot;

            var rect = this.ClientRectangle;
            var ptImgL = new PointF(0, imgBH / 2);
            var ptImgR = new PointF(imgBW, imgBH / 2);
            var ptDispL = ImgToDisp(ptImgL);
            var ptDispR = ImgToDisp(ptImgR);
            ptDispL.X = Math.Max(ptDispL.X, 0);
            ptDispR.X = Math.Min(ptDispR.X, rect.Width);
            g.DrawLine(pen, ptDispL, ptDispR);
            var ptImgT = new PointF(imgBW / 2, 0);
            var ptImgB = new PointF(imgBW / 2, imgBH);
            var ptDispT = ImgToDisp(ptImgT);
            var ptDispB = ImgToDisp(ptImgB);
            ptDispT.Y = Math.Max(ptDispT.Y, 0);
            ptDispB.Y = Math.Min(ptDispB.Y, rect.Height);
            g.DrawLine(pen, ptDispT, ptDispB);
            pen.Dispose();
        }

        // 이미지 픽셀값 표시
        Brush[] pseudo = {
            Brushes.White,      // 0~31
            Brushes.LightCyan,  // 32~63
            Brushes.DodgerBlue, // 63~95
            Brushes.Yellow,     // 96~127
            Brushes.Brown,      // 128~159
            Brushes.Magenta,    // 160~191
            Brushes.Red    ,    // 192~223
            Brushes.Black,      // 224~255
        };
        private void DrawPixelValue(Graphics g) {
            if (ZoomFactor < 16)
                return;

            var ptDisp1 = Point.Empty;
            var ptDisp2 = (Point)this.ClientSize;
            var ptImg1 = DispToImg(ptDisp1);
            var ptImg2 = DispToImg(ptDisp2);
            int imgX1 = (int)Math.Floor(ptImg1.X);
            int imgY1 = (int)Math.Floor(ptImg1.Y);
            int imgX2 = (int)Math.Floor(ptImg2.X);
            int imgY2 = (int)Math.Floor(ptImg2.Y);
            if (imgX1 < 0)
                imgX1 = 0;
            if (imgY1 < 0)
                imgY1 = 0;
            if (imgX2 >= imgBW)
                imgX2 = imgBW - 1;
            if (imgY2 >= imgBH)
                imgY2 = imgBH - 1;

            float fontSize = ZoomFactor / 16 * 6;
            if (fontSize > 12)
                fontSize = 12;
            Font font = new Font("돋움체", fontSize);
            for (int imgY = imgY1; imgY <= imgY2; imgY++) {
                for (int imgX = imgX1; imgX <= imgX2; imgX++) {
                    var ptImg = new PointF(imgX, imgY);
                    var ptDisp = ImgToDisp(ptImg);
                    int pixelVal = GetImagePixelValue(imgX, imgY);
                    var brush = pseudo[pixelVal / 32];
                    g.DrawString(pixelVal.ToString(), font, brush, ptDisp.X, ptDisp.Y);
                }
            }
            font.Dispose();
        }

        Font defFont = SystemFonts.DefaultFont;
        private void DrawInfo(Graphics g) {
            Point ptCur = this.PointToClient(Cursor.Position);
            PointF ptImg = DispToImg(ptCur);
            int imgX = (int)Math.Floor(ptImg.X);
            int imgY = (int)Math.Floor(ptImg.Y);
            int pixelVal = GetImagePixelValue(imgX, imgY);
            string info = $"zoom={ZoomText} ({imgX},{imgY})={pixelVal}";
            var rect = g.MeasureString(info, defFont);
            g.FillRectangle(Brushes.White, 0, 0, rect.Width, rect.Height);
            g.DrawString(info, defFont, Brushes.Black, 0, 0);
        }

        // 표시 픽셀 좌표를 이미지 좌표로 변환
        public PointF DispToImg(Point pt) {
            var pt2 = pt - (Size)ptPanning;
            return new PointF(pt2.X / ZoomFactor, pt2.Y / ZoomFactor);
        }

        // 이미지 좌표를 표시 픽셀 좌표로 변환
        public Point ImgToDisp(PointF pt) {
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            return new Point((int)Math.Floor(pt2.X + ptPanning.X), (int)Math.Floor(pt2.Y + ptPanning.Y));
        }
        public Rectangle ImgToDisp(RectangleF rect) {
            var pt = rect.Location;
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            var size = rect.Size;
            var size2 = new SizeF(size.Width * ZoomFactor, size.Height * ZoomFactor);
            return new Rectangle((int)Math.Floor(pt2.X + ptPanning.X), (int)Math.Floor(pt2.Y + ptPanning.Y), (int)Math.Floor(size2.Width), (int)Math.Floor(size2.Height));
        }


        // 이미지 픽셀값 리턴
        private int GetImagePixelValue(int x, int y) {
            if (imgBuf == IntPtr.Zero || x < 0 || x >= imgBW || y < 0 || y >= imgBH)
                return -1;

            IntPtr ptr = (IntPtr)(imgBuf.ToInt64() + (long)imgBW * y + x);
            return Marshal.ReadByte(ptr);
        }
    }
}
