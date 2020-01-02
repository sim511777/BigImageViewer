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
using NativeImport;

namespace ShimLib {
    public class ZoomPictureBox : PictureBox {
        // 디스플레이용 버퍼
        private int dispBW;
        private int dispBH;
        private IntPtr dispBuf;
        private Bitmap dispBmp;

        // 이미지용 버퍼
        private int imgBW;
        private int imgBH;
        private IntPtr imgBuf;

        // 화면 표시 옵션
        public bool UseDrawPixelValue { get; set; } = true;
        public bool UseDrawInfo { get; set; } = true;
        public bool UseDrawCenterLine { get; set; } = true;

        // 줌 파라미터
        public int ZoomLevel { get; set; }

        // 패닝 파라미터
        public Point PtPanning { get; set; }

        // CopyImageBufferZoom() 함수 Native 코드 사용
        public bool UseNative { get; set; }

        // ZoomLevel = 0 => ZoomFactor = 1;
        // ..., 1/512, 3/1024, 1/256, 3/512, 1/128, 3/256, 1/64, 3/128, 1/32, 3/64, 1/16, 3/32, 1/8, 3/16, 1/4, 3/8, 1/2, 3/4, 1, 3/2, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96, ...
        public float GetZoomFactor () {
            int base_num = 2;
            int exp_num = (ZoomLevel >= 0) ? ZoomLevel / 2 : (ZoomLevel - 1) / 2;
            if (ZoomLevel % 2 != 0)
                exp_num--;
            int c = (ZoomLevel % 2 != 0) ? 3 : 1;
            float zoomFactor = c * (float)Math.Pow(base_num, exp_num);
            return zoomFactor;
        }
        private string GetZoomText() {
            int base_num = 2;
            int exp_num = (ZoomLevel >= 0) ? ZoomLevel / 2 : (ZoomLevel - 1) / 2;
            if (ZoomLevel % 2 != 0)
                exp_num--;
            int c = (ZoomLevel % 2 != 0) ? 3 : 1;
            string zoomText = (exp_num >= 0) ? (c * (int)Math.Pow(base_num, exp_num)).ToString() : c.ToString() + "/" + ((int)Math.Pow(base_num, -exp_num)).ToString();
            return zoomText;
        }

        // 생성자
        public ZoomPictureBox() {
            AllocDispBuf();
            RedrawImage();
        }

        // 소멸자
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

        // 리사이즈 할때
        protected override void OnLayout(LayoutEventArgs e) {
            base.OnLayout(e);

            AllocDispBuf();
            RedrawImage();
        }

        // 페인트 할때
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

        // 마우스 휠
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
            if (ZoomLevel > 20)
                ZoomLevel = 20;
            if (ZoomLevel < -20)
                ZoomLevel = -20;

            var zoomFactorNew = GetZoomFactor();
            int sizeX = (int)Math.Floor(ptImg.X * (zoomFacotrOld - zoomFactorNew));
            int sizeY = (int)Math.Floor(ptImg.Y * (zoomFacotrOld - zoomFactorNew));
            PtPanning += new Size(sizeX, sizeY);
        }

        // 마우스 다운
        protected override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left) {
                mouseDown = true;
                ptOld = e.Location;
            }
        }

        // 마우스 무브
        protected override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);

            if (mouseDown) {
                PtPanning += ((Size)e.Location - (Size)ptOld);
                ptOld = e.Location;
                RedrawImage();
            } else {
                this.Invalidate();
            }
        }

        // 마우스 업
        protected override void OnMouseUp(MouseEventArgs e) {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
                mouseDown = false;
        }

        // 표시 버퍼 할당
        const string dll = "msvcrt.dll";
        [DllImport("msvcrt.dll")] private static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, ulong _Size);
        [DllImport("msvcrt.dll")] private static extern IntPtr memset(IntPtr _Dst, int _Val, ulong _Size);

        private void AllocDispBuf() {
            FreeDispBuf();

            dispBW = Math.Max((this.ClientSize.Width + 3) / 4 * 4, 64);
            dispBH = Math.Max(this.ClientSize.Height, 64);

            dispBuf = Marshal.AllocHGlobal((IntPtr)(dispBW * dispBH));
            memset(dispBuf, 0, (ulong)(dispBW * dispBH));
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

        unsafe private static void CopyImageBufferZoom(IntPtr sbuf, int sbw, int sbh, IntPtr dbuf, int dbw, int dbh, int panx, int pany, float zoom) {
            // dst 인덱스의 범위를 구함
            int y1 = Math.Min(Math.Max(pany, 0), dbh);
            int y2 = Math.Max(Math.Min((int)Math.Floor(sbh * zoom + pany), dbh), 0);
            int x1 = Math.Min(Math.Max(panx, 0), dbw);
            int x2 = Math.Max(Math.Min((int)Math.Floor(sbw * zoom + panx), dbw), 0);

            // dst 인덱스에 해당하는 src 인덱스를 담을 버퍼 생성
            int[] siys = new int[dbh];
            int[] sixs = new int[dbw];

            // 버퍼에 값 구해서 넣음
            for (int y = y1; y < y2; y++) {
                siys[y] = (int)Math.Floor((y - pany) / zoom);
            }
            for (int x = x1; x < x2; x++) {
                sixs[x] = (int)Math.Floor((x - panx) / zoom);
            }

            // dst 인덱스의 범위만큼 루프를 돌면서 해당 픽셀값 쓰기
            for (int y = y1; y < y2; y++) {
                byte* dp = (byte*)dbuf.ToPointer() + (long)dbw * y + x1;

                int siy = siys[y];
                byte* sp = (byte*)sbuf.ToPointer() + (long)sbw * siy;
                for (int x = x1; x < x2; x++, dp++) {
                    int six = sixs[x];
                    *dp = *(sp + six);
                }
            }

            // 버퍼 삭제
        }

        // 이미지 다시 그림
        public void RedrawImage() {
            memset(dispBuf, 128, (ulong)dispBW * (ulong)dispBH);
            float ZoomFactor = GetZoomFactor();
            if (UseNative)
                NativeDll.CopyImageBufferZoom(imgBuf, imgBW, imgBH, dispBuf, dispBW, dispBH, PtPanning.X, PtPanning.Y, ZoomFactor);
            else
                CopyImageBufferZoom(imgBuf, imgBW, imgBH, dispBuf, dispBW, dispBH, PtPanning.X, PtPanning.Y, ZoomFactor);
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
            float ZoomFactor = GetZoomFactor();
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

        // 좌상단 정보 표시
        private void DrawInfo(Graphics g) {
            Point ptCur = this.PointToClient(Cursor.Position);
            PointF ptImg = DispToImg(ptCur);
            int imgX = (int)Math.Floor(ptImg.X);
            int imgY = (int)Math.Floor(ptImg.Y);
            int pixelVal = GetImagePixelValue(imgX, imgY);
            string info = $"zoom={GetZoomText()} ({imgX},{imgY})={pixelVal}";
            var rect = g.MeasureString(info, SystemFonts.DefaultFont);
            g.FillRectangle(Brushes.White, 0, 0, rect.Width, rect.Height);
            g.DrawString(info, SystemFonts.DefaultFont, Brushes.Black, 0, 0);
        }

        // 표시 픽셀 좌표를 이미지 좌표로 변환
        public PointF DispToImg(Point pt) {
            float ZoomFactor = GetZoomFactor();
            var pt2 = pt - (Size)PtPanning;
            return new PointF(pt2.X / ZoomFactor, pt2.Y / ZoomFactor);
        }

        // 이미지 좌표를 표시 픽셀 좌표로 변환
        public Point ImgToDisp(PointF pt) {
            float ZoomFactor = GetZoomFactor();
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            return new Point((int)Math.Floor(pt2.X + PtPanning.X), (int)Math.Floor(pt2.Y + PtPanning.Y));
        }

        // 이미지 사각형을 픽셀 사각형으로 변환
        public Rectangle ImgToDisp(RectangleF rect) {
            float ZoomFactor = GetZoomFactor();
            var pt = rect.Location;
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            var size = rect.Size;
            var size2 = new SizeF(size.Width * ZoomFactor, size.Height * ZoomFactor);
            return new Rectangle((int)Math.Floor(pt2.X + PtPanning.X), (int)Math.Floor(pt2.Y + PtPanning.Y), (int)Math.Floor(size2.Width), (int)Math.Floor(size2.Height));
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
