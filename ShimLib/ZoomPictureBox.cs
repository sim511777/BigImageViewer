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
    public class ZoomPictureBox : Control {
        // 디스플레이용 버퍼
        private int dispBW;
        private int dispBH;
        private IntPtr dispBuf;
        private Bitmap dispBmp;

        // 이미지용 버퍼
        private int imgBW;
        private int imgBH;
        private IntPtr imgBuf;
        private int bytepp = 1;

        // 생성자
        public ZoomPictureBox() {
            DoubleBuffered = true;
        }

        // 소멸자
        ~ZoomPictureBox() {
            FreeDispBuf();
        }

        // 화면 표시 옵션
        public bool UseDrawPixelValue { get; set; } = true;
        public bool UseDrawInfo { get; set; } = true;
        public bool UseDrawCenterLine { get; set; } = true;

        // 줌 파라미터
        private int zoomLevel;
        public int ZoomLevel {
            get { return zoomLevel; }
            set { zoomLevel = Util.IntClamp(value, 0, zoomTexts.Length-1);}
        }
        static readonly string[] zoomTexts   = { "1/1024", "3/2048", "1/512", "3/1024", "1/256", "3/512", "1/128", "3/256", "1/64", "3/128", "1/32", "3/64", "1/16", "3/32", "1/8", "3/16", "1/4", "3/8", "1/2", "3/4", "1", "3/2", "2", "3", "4", "6", "8", "12", "16", "24", "32", "48", "64", "96", "128", "192", "256", "384", "512", "768", "1024", };
        static readonly double[] zoomFactors = {  1/1024d,  3/2048d,  1/512d,  3/1024d,  1/256d,  3/512d,  1/128d,  3/256d,  1/64d,  3/128d,  1/32d,  3/64d,  1/16d,  3/32d,  1/8d,  3/16d,  1/4d,  3/8d,  1/2d,  3/4d,  1d,  3/2d,  2d,  3d,  4d,  6d,  8d,  12d,  16d,  24d,  32d,  48d,  64d,  96d,  128d,  192d,  256d,  384d,  512d,  768d,  1024d, };
        public double GetZoomFactor() => zoomFactors[ZoomLevel];
        private string GetZoomText() => zoomTexts[ZoomLevel];

        // 패닝 파라미터
        public Point PtPanning { get; set; }

        // 이미지 버퍼 세팅
        public void SetImgBuf(IntPtr buf, int bw, int bh, int bytepp) {
            imgBuf = buf;
            imgBW = bw;
            imgBH = bh;
            this.bytepp = bytepp;

            AllocDispBuf();
            Invalidate();
        }

        // 리사이즈 할때
        protected override void OnLayout(LayoutEventArgs e) {
            base.OnLayout(e);

            AllocDispBuf();
            Invalidate();
        }

        // 페인트 할때
        protected override void OnPaint(PaintEventArgs e) {
            var g = e.Graphics;

            DrawImage(g);
            if (UseDrawPixelValue)
                DrawPixelValue(g);
            if (UseDrawCenterLine)
                DrawCenterLine(g);

            base.OnPaint(e);

            if (UseDrawInfo)
                DrawInfo(g);
        }

        // 마우스 휠
        protected override void OnMouseWheel(MouseEventArgs e) {
            base.OnMouseWheel(e);

            if (Control.ModifierKeys == Keys.Control) {
                WheelScrollV(e);
            } else if (Control.ModifierKeys == Keys.Shift) {
                WheelScrollH(e);
            } else {
                WheelZoom(e);
            }
            Invalidate();
        }

        // 휠 스크롤
        private void WheelScrollV(MouseEventArgs e) {
            int scroll = (e.Delta > 0) ? 128 : -128;
            PtPanning += new Size(0, scroll);
        }
        private void WheelScrollH(MouseEventArgs e) {
            int scroll = (e.Delta > 0) ? 128 : -128;
            PtPanning += new Size(scroll, 0);
        }

        // 휠 줌
        private void WheelZoom(MouseEventArgs e) {
            var ptImg = DispToImg(e.Location);

            var zoomFacotrOld = GetZoomFactor();
            ZoomLevel = (e.Delta > 0) ? ZoomLevel + 1 : ZoomLevel - 1;

            var zoomFactorNew = GetZoomFactor();
            int sizeX = (int)Math.Floor(ptImg.X * (zoomFacotrOld - zoomFactorNew));
            int sizeY = (int)Math.Floor(ptImg.Y * (zoomFacotrOld - zoomFactorNew));
            PtPanning += new Size(sizeX, sizeY);
        }

        // 마우스 다운
        bool mouseDown = false;
        Point ptPanOld;
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left) {
                mouseDown = true;
                ptPanOld = e.Location;
            }
        }

        // 마우스 무브
        Point ptMove;
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            ptMove = e.Location;
            if (mouseDown) {
                PtPanning += ((Size)ptMove - (Size)ptPanOld);
                ptPanOld = ptMove;
            }
            Invalidate();
        }

        // 마우스 업
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        // 표시 버퍼 생성
        private void AllocDispBuf() {
            FreeDispBuf();

            if (bytepp == 1) {
                dispBW = Math.Max((this.ClientSize.Width + 3) / 4 * 4, 64);
                dispBH = Math.Max(this.ClientSize.Height, 64);

                dispBuf = Marshal.AllocHGlobal((IntPtr)(dispBW * dispBH));
                MsvcrtDll.memset(dispBuf, 0, dispBW * dispBH);
                dispBmp = new Bitmap(dispBW, dispBH, dispBW, PixelFormat.Format8bppIndexed, dispBuf);
                var pal = dispBmp.Palette;
                for (int i = 0; i < 256; i++) {
                    pal.Entries[i] = Color.FromArgb(i, i, i);
                }
                dispBmp.Palette = pal;
            } else { // bytepp = 4;
                dispBW = Math.Max(this.ClientSize.Width, 64);
                dispBH = Math.Max(this.ClientSize.Height, 64);

                dispBuf = Marshal.AllocHGlobal((IntPtr)(dispBW * dispBH * bytepp));
                MsvcrtDll.memset(dispBuf, 0, dispBW * dispBH * bytepp);
                dispBmp = new Bitmap(dispBW, dispBH, dispBW * bytepp, PixelFormat.Format32bppRgb, dispBuf);
            }
        }

        // 표시 버퍼 해제
        private void FreeDispBuf() {
            if (dispBmp != null)
                dispBmp.Dispose();
            if (dispBuf != IntPtr.Zero)
                Marshal.FreeHGlobal(dispBuf);
        }

        // 이미지 버퍼를 디스플레이 버퍼에 복사
        unsafe private static void CopyImageBufferZoom(IntPtr sbuf, int sbw, int sbh, IntPtr dbuf, int dbw, int dbh, int panx, int pany, double zoom, int bytepp) {
            // 인덱스 버퍼 생성
            int[] siys = new int[dbh];
            int[] sixs = new int[dbw];
            for (int y = 0; y < dbh; y++) {
                int siy = (int)Math.Floor((y - pany) / zoom);
                siys[y] = (sbuf == IntPtr.Zero || siy < 0 || siy >= sbh) ? -1 : siy;
            }
            for (int x = 0; x < dbw; x++) {
                int six = (int)Math.Floor((x - panx) / zoom);
                sixs[x] = (sbuf == IntPtr.Zero || six < 0 || six >= sbw) ? -1 : six;
            }

            // dst 범위만큼 루프를 돌면서 해당 픽셀값 쓰기
            bool oneChannel = (bytepp == 1);
            for (int y = 0; y < dbh; y++) {
                int siy = siys[y];
                byte* sp = (byte*)sbuf.ToPointer() + (Int64)sbw * siy * bytepp;
                byte* dp = (byte*)dbuf.ToPointer() + (Int64)dbw * y * bytepp;
                bool yClear = (siy == -1);
                for (int x = 0; x < dbw; x++, dp += bytepp) {
                    int six = sixs[x];
                    if (oneChannel)
                        *dp = (yClear || six == -1) ? (byte)0x80 : sp[six];
                    else
                        *(uint*)dp = (yClear || six == -1) ? 0xff808080 : ((uint*)sp)[six];
                }
            }
        }

        // 이미지 버퍼 그림
        private void DrawImage(Graphics g) {
            CopyImageBufferZoom(imgBuf, imgBW, imgBH, dispBuf, dispBW, dispBH, PtPanning.X, PtPanning.Y, GetZoomFactor(), bytepp);
            g.DrawImage(dispBmp, 0, 0);
        }

        // 중심선 표시
        private void DrawCenterLine(Graphics g) {
            if (imgBuf == IntPtr.Zero)
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
            int colorNum = bytepp == 1 ? 1 : 3;
            double ZoomFactor = GetZoomFactor();
            if (ZoomFactor < 16 * colorNum)
                return;

            var ptDisp1 = Point.Empty;
            var ptDisp2 = (Point)this.ClientSize;
            var ptImg1 = DispToImg(ptDisp1);
            var ptImg2 = DispToImg(ptDisp2);
            int imgX1 = Util.IntClamp((int)Math.Floor(ptImg1.X), 0, imgBW-1);
            int imgY1 = Util.IntClamp((int)Math.Floor(ptImg1.Y), 0, imgBH-1);
            int imgX2 = Util.IntClamp((int)Math.Floor(ptImg2.X), 0, imgBW-1);
            int imgY2 = Util.IntClamp((int)Math.Floor(ptImg2.Y), 0, imgBH-1);

            float fontSize = (float)(ZoomFactor / 16 * 6 / colorNum);
            if (fontSize > 12)
                fontSize = 12;
            Font font = new Font("돋움체", fontSize);
            for (int imgY = imgY1; imgY <= imgY2; imgY++) {
                for (int imgX = imgX1; imgX <= imgX2; imgX++) {
                    var ptImg = new PointF(imgX, imgY);
                    var ptDisp = ImgToDisp(ptImg);
                    string pixelValText = GetImagePixelValueText(imgX, imgY);
                    int pixelVal = GetImagePixelValueAverage(imgX, imgY);
                    var brush = pseudo[pixelVal / 32];
                    g.DrawString(pixelValText.ToString(), font, brush, ptDisp.X, ptDisp.Y);
                }
            }
            font.Dispose();
        }

        // 좌상단 정보 표시
        private void DrawInfo(Graphics g) {
            Point ptCur = ptMove;//this.PointToClient(Cursor.Position);
            PointF ptImg = DispToImg(ptCur);
            int imgX = (int)Math.Floor(ptImg.X);
            int imgY = (int)Math.Floor(ptImg.Y);
            string pixelVal = GetImagePixelValueText(imgX, imgY);
            string info = $"zoom={GetZoomText()} ({imgX},{imgY})={pixelVal}";
            var rect = g.MeasureString(info, SystemFonts.DefaultFont);
            g.FillRectangle(Brushes.White, 0, 0, rect.Width, rect.Height);
            g.DrawString(info, SystemFonts.DefaultFont, Brushes.Black, 0, 0);
        }

        // 표시 픽셀 좌표를 이미지 좌표로 변환
        public PointF DispToImg(Point pt) {
            double ZoomFactor = GetZoomFactor();
            float x = (float)((pt.X - PtPanning.X) / ZoomFactor);
            float y = (float)((pt.Y - PtPanning.Y) / ZoomFactor);
            return new PointF(x, y);
        }

        // 이미지 좌표를 표시 픽셀 좌표로 변환
        public Point ImgToDisp(PointF pt) {
            double ZoomFactor = GetZoomFactor();
            int x = (int)Math.Floor(pt.X * ZoomFactor + PtPanning.X);
            int y = (int)Math.Floor(pt.Y * ZoomFactor + PtPanning.Y);
            return new Point(x, y);
        }

        // 이미지 사각형을 픽셀 사각형으로 변환
        public Rectangle ImgToDisp(RectangleF rect) {
            double ZoomFactor = GetZoomFactor();
            int x = (int)Math.Floor(rect.X * ZoomFactor + PtPanning.X);
            int y = (int)Math.Floor(rect.Y * ZoomFactor + PtPanning.Y);
            int width = (int)Math.Floor(rect.Width * ZoomFactor);
            int height = (int)Math.Floor(rect.Height * ZoomFactor);
            return new Rectangle(x, y, width, height);
        }

        // 이미지 픽셀값 리턴
        private int[] GetImagePixelValues(int x, int y) {
            if (imgBuf == IntPtr.Zero || x < 0 || x >= imgBW || y < 0 || y >= imgBH) {
                if (bytepp == 1)
                    return new int[] { -1 };
                else
                    return new int[] { -1, -1, -1 };
            } else {
                IntPtr ptr = (IntPtr)(imgBuf.ToInt64() + ((long)imgBW * y + x) * bytepp);
                if (bytepp == 1)
                    return new int[] { Marshal.ReadByte(ptr) };
                else
                    return new int[] { Marshal.ReadByte(ptr, 2), Marshal.ReadByte(ptr, 1), Marshal.ReadByte(ptr, 0) };
            }
        }

        // 이미지 픽셀값 문자열 리턴 byte
        private string GetImagePixelValueText(int x, int y) {
            int[] values = GetImagePixelValues(x, y);
            return string.Join(",", values);
        }

        // 이미지 픽셀값 평균 리턴 (0~255)
        private int GetImagePixelValueAverage(int x, int y) {
            int[] values = GetImagePixelValues(x, y);
            return Util.IntClamp((int)values.Average(), 0, 255);
        }
    }
}