using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BigImageViewer {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            this.pbxDraw.MouseWheel += this.PbxDraw_MouseWheel;

            AllocDispBuf();
            RedrawImage();
        }

        ~FormMain() {
            FreeImgBuf();
            FreeDispBuf();
        }

        // 로그 출력
        public void Log(string msg) {
            DateTime now = DateTime.Now;
            string timeMsg = now.ToString("[yy/MM/dd HH:mm:ss.fff] ");
            tbxLog.AppendText(timeMsg + msg + Environment.NewLine);
        }

        // 버퍼
        int dispBW;
        int dispBH;
        IntPtr dispBuf;
        Bitmap dispBmp;
        int imgFW;
        int imgFH;
        int numFrm;
        int ImgBW { get { return imgFW; } }
        int ImgBH { get { return imgFH * numFrm; } }
        IntPtr imgBuf;

        // zoom, panning
        float[] zoomLevels = { 1f / 512, 3f / 1024, 1f / 256, 3f / 512, 1f / 128, 3f / 256, 1f / 64, 3f / 128, 1f / 32, 3f / 64, 1f / 16, 3f / 32, 1f / 8, 3f / 16, 1f / 4, 3f / 8, 1f / 2, 3f / 4, 1, 3f / 2, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96 };
        string[] zoomTexts = { "1/512", "3/1024", "1/256", "3/512", "1/128", "3/256", "1/64", "3/128", "1/32", "3/64", "1/16", "3/32", "1/8", "3/16", "1/4", "3/8", "1/2", "3/4", "1", "3/2", "2", "3", "4", "6", "8", "12", "16", "24", "32", "48", "64", "96" };
        const int zoomIndexReset = 8;
        int zoomIndex = zoomIndexReset;
        float ZoomLevel { get { return zoomLevels[zoomIndex]; } }
        string ZoomText { get { return zoomTexts[zoomIndex]; } }
        Point ptPanning = Point.Empty;


        // 표시 버퍼 할당
        private void AllocDispBuf() {
            dispBW = 1920;
            dispBH = 1080;
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
            Marshal.FreeHGlobal(dispBuf);
        }

        // 이미지 버퍼 해제
        private void FreeImgBuf() {
            if (imgBuf != IntPtr.Zero) {
                Marshal.FreeHGlobal(imgBuf);
            }
        }

        // 이미지 버퍼 할당
        private void AllocImgBuf() {
            imgFW = (int)numFW.Value;
            imgFH = (int)numFH.Value;
            numFrm = (int)numFNum.Value;
            imgBuf = Marshal.AllocHGlobal((IntPtr)((long)ImgBW * ImgBH));
            MsvcrtDll.memset(imgBuf, 0, (ulong)((long)ImgBW * ImgBH));
        }

        // 포워드 이미지 로드
        enum BitmapLoader { C, OpenCV, Dotnet_Bitmap }
        private void LoadFwdImg() {
            long frmSize = imgFW * imgFH;
            string dir = tbxFwdDir.Text;
            int succNum = 0;

            Log($"Load Fwd Image");

            for (int i = 0; i < numFrm; i++) {
                string filePath = $"{dir}\\Frame_{i:000}.BMP";
                IntPtr buf = (IntPtr)(imgBuf.ToInt64() + frmSize * i);

                var r = OpenCv.Load8bitBmp(buf, imgFW, imgFH, filePath);
                if (r) {
                    succNum++;
                } else {
                    MsvcrtDll.memset(buf, 0, (ulong)frmSize);
                }
            }
            Log($"이미지 {numFrm}개 중 {succNum}개 이미지 로드 성공");
            Log($"End Load Fwd Image");
        }

        // 이미지 버퍼를 표시 버퍼로 복사
        enum ImageBufferDrawer { C, Dotnet_Unsafe }
        private void RedrawImage() {
            NativeDll.CopyImageBuf(imgBuf, ImgBW, ImgBH, dispBuf, dispBW, dispBH, ptPanning.X, ptPanning.Y, ZoomLevel);
            pbxDraw.Invalidate();
        }

        // 표시 픽셀 좌표를 이미지 좌표로 변환
        private PointF DispToImg(Point pt) {
            var pt2 = pt - (Size)ptPanning;
            return new PointF(pt2.X / ZoomLevel, pt2.Y / ZoomLevel);
        }

        // 이미지 좌표를 표시 픽셀 좌표로 변환
        private Point ImgToDisp(PointF pt) {
            var pt2 = new PointF(pt.X * ZoomLevel, pt.Y * ZoomLevel);
            return new Point((int)Math.Floor(pt2.X + ptPanning.X), (int)Math.Floor(pt2.Y + ptPanning.Y));
        }

        // 이미지 픽셀값 리턴
        private int GetImagePixelValue(int x, int y) {
            if (imgBuf == IntPtr.Zero || x < 0 || x >= ImgBW || y < 0 || y >= ImgBH)
                return -1;

            IntPtr ptr = (IntPtr)(imgBuf.ToInt64() + (long)ImgBW * y + x);
            return Marshal.ReadByte(ptr);
        }

        // 정보 표시
        private void DrawInfo(Graphics g) {
            Point ptCur = pbxDraw.PointToClient(Cursor.Position);
            PointF ptImg = DispToImg(ptCur);
            int imgX = (int)Math.Floor(ptImg.X);
            int imgY = (int)Math.Floor(ptImg.Y);
            int pixelVal = GetImagePixelValue(imgX, imgY);
            string info = $"{ZoomText}x ({imgX},{imgY}) [{pixelVal}]";
            var rect = g.MeasureString(info, infoFont);
            g.FillRectangle(Brushes.White, 0, 0, rect.Width, rect.Height);
            g.DrawString(info, infoFont, Brushes.Black, 0, 0);
        }

        // 프레임 표시
        private void DrawFrame(Graphics g) {
            if (imgFH * ZoomLevel < 15)
                return;
            var clientSize = pbxDraw.ClientSize;
            for (int i = 0; i < numFrm; i++) {
                var ptImg1 = new PointF(0, imgFH * i);
                var ptImg2 = new PointF(imgFW, imgFH * i);
                var ptDisp1 = ImgToDisp(ptImg1);
                var ptDisp2 = ImgToDisp(ptImg2);

                if (ptDisp1.Y < 0 || ptDisp1.Y >= clientSize.Height || ptDisp1.X >= clientSize.Width || ptDisp2.X < 0)
                    continue;

                g.DrawLine(Pens.PowderBlue, ptDisp1, ptDisp2);
                g.DrawString($"Frame={i}", infoFont, Brushes.LightBlue, ptDisp1.X, ptDisp1.Y + 2);
            }
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
            if (ZoomLevel < 15)
                return;

            var ptDisp1 = Point.Empty;
            var ptDisp2 = (Point)pbxDraw.ClientSize;
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
            if (imgX2 >= ImgBW)
                imgX2 = ImgBW - 1;
            if (imgY2 >= ImgBH)
                imgY2 = ImgBH - 1;

            for (int imgY = imgY1; imgY <= imgY2; imgY++) {
                for (int imgX = imgX1; imgX <= imgX2; imgX++) {
                    var ptImg = new PointF(imgX, imgY);
                    var ptDisp = ImgToDisp(ptImg);
                    int pixelVal = GetImagePixelValue(imgX, imgY);
                    var brush = pseudo[pixelVal / 32];
                    g.DrawString(pixelVal.ToString(), infoFont, brush, ptDisp.X, ptDisp.Y);
                }
            }
        }

        // 휠 줌
        private void WheelZoom(MouseEventArgs e) {
            var ptImg = DispToImg(e.Location);

            var zoomLevelOld = ZoomLevel;
            zoomIndex = (e.Delta > 0) ? zoomIndex + 1 : zoomIndex - 1;
            if (zoomIndex < 0)
                zoomIndex = 0;
            if (zoomIndex >= zoomLevels.Length)
                zoomIndex = zoomLevels.Length - 1;

            ptPanning.X += (int)Math.Floor(ptImg.X * (zoomLevelOld - ZoomLevel));
            ptPanning.Y += (int)Math.Floor(ptImg.Y * (zoomLevelOld - ZoomLevel));
        }

        // 휠 스크롤
        private void WheelScroll(MouseEventArgs e) {
            int scroll = 128;
            ptPanning.Y += (e.Delta > 0) ? scroll : -scroll;
        }

        int holeW;
        int holeH;
        Hole[] holes;
        // 홀 버퍼 초기화
        private Hole[] InitHoles(int holeW, int holeH, float left, float top, float pitchX, float pitchY, float dx, float dy) {
            Hole[] holes = new Hole[holeW * holeH];
            int fwdStep = holeW / 4;
            for (int iy = 0; iy < holeH; iy++) {
                for (int ix = 0; ix < holeW; ix++) {
                    float x = left + ix * pitchX;
                    float y = top + iy * pitchY;
                    holes[holeW * iy + ix] = new Hole(x, y, dx, dy, ix, iy, ix / fwdStep);
                }
            }
            return holes;
        }

        enum HoleInfoItemType { None, IndexX, IndexY, Fwd, }

        // 홀버퍼 드로우
        private void DrawHoles(Graphics g) {
            if (holes == null)
                return;

            var ptDisp1 = Point.Empty;
            var ptDisp2 = (Point)pbxDraw.ClientSize;
            var ptImg1 = DispToImg(ptDisp1);
            var ptImg2 = DispToImg(ptDisp2);
            float imgX1 = (float)Math.Floor(ptImg1.X);
            float imgY1 = (float)Math.Floor(ptImg1.Y);
            float imgX2 = (float)Math.Floor(ptImg2.X);
            float imgY2 = (float)Math.Floor(ptImg2.Y);

            float zoomLevel = ZoomLevel;
            float panX = ptPanning.X;
            float panY = ptPanning.Y;

            Pen linePen = Pens.Red;
            Brush infoBrush = Brushes.Yellow;
            Font infoFont = SystemFonts.DefaultFont;

            float holePitch = 32.0f;
            int step = (int)(2.0f / (zoomLevel * holePitch));
            if (step < 1)
                step = 1;
            bool holeDrawCircle = zoomLevel > (4.0f / holePitch);

            HoleInfoItemType infoItemType = HoleInfoItemType.None;
            if (rdoHoleInfoIndexX.Checked)
                infoItemType = HoleInfoItemType.IndexX;
            else if (rdoHoleInfoIndexY.Checked)
                infoItemType = HoleInfoItemType.IndexY;
            else if (rdoHoleInfoFWD.Checked)
                infoItemType = HoleInfoItemType.Fwd;

            for (int iy = 0; iy < holeH; iy += step) {
                for (int ix = 0; ix < holeW; ix += step) {
                    var hole = holes[holeW * iy + ix];
                    if (hole.x < imgX1 || hole.x > imgX2 || hole.y < imgY1 || hole.y > imgY2)
                        continue;
                    if (holeDrawCircle)
                        DrawHoleCircle(g, zoomLevel, panX, panY, hole, linePen);
                    else
                        DrawHolePoint(g, zoomLevel, panX, panY, hole, linePen);
                    
                    if (zoomLevel < 0.5f)
                        continue;
                    DrawHoleInfo(g, zoomLevel, panX, panY, hole, infoItemType, infoFont, infoBrush);
                }
            }
        }

        // 개별 홀 써클 드로우
        private void DrawHoleCircle(Graphics g, float zoomLevel, float panX, float panY, Hole hole, Pen pen) {
            float x = (hole.x - hole.w / 2f) * zoomLevel + panX;
            float y = (hole.y - hole.h / 2f) * zoomLevel + panY;
            float width = hole.w * zoomLevel;
            float height = hole.h * zoomLevel;
            g.DrawEllipse(pen, x, y, width, height);
        }

        // 개별 홀 포인트 드로우
        private void DrawHolePoint(Graphics g, float zoomLevel, float panX, float panY, Hole hole, Pen linePen) {
            float x = (int)(hole.x * zoomLevel + panX) - 0.5f;
            float y = (int)(hole.y * zoomLevel + panY) - 0.5f;
            g.DrawLine(linePen, x, y, x + 1, y + 1);
        }

        // 홀 정보 표시
        private void DrawHoleInfo(Graphics g, float zoomLevel, float panX, float panY, Hole hole, HoleInfoItemType infoItemType, Font font, Brush brush) {
            if (infoItemType == HoleInfoItemType.None)
                return;

            string infoText;
            if (infoItemType == HoleInfoItemType.IndexX)
                infoText = hole.idxX.ToString();
            else if (infoItemType == HoleInfoItemType.IndexY)
                infoText = hole.idxY.ToString();
            else
                infoText = hole.fwd.ToString();

            float x = hole.x * zoomLevel + panX;
            float y = hole.y * zoomLevel + panY;
            g.DrawString(infoText, font, brush, x, y);
        }

        //=============================================================
        private void btnAlloc_Click(object sender, EventArgs e) {
            Log("Alloc Buffer");
            FreeImgBuf();
            AllocImgBuf();
            Log("End Alloc Buffer");
            RedrawImage();
        }

        private void btnFwdDir_Click(object sender, EventArgs e) {
            var dr = dlgFolder.ShowDialog(this);
            if (dr != DialogResult.OK)
                return;
            tbxFwdDir.Text = dlgFolder.SelectedPath;
        }

        private void btnLoadFwd_Click(object sender, EventArgs e) {
            LoadFwdImg();
            RedrawImage();
        }

        private void btnClearLog_Click(object sender, EventArgs e) {
            tbxLog.Clear();
        }

        Font infoFont = SystemFonts.DefaultFont;
        private void pbxDraw_Paint(object sender, PaintEventArgs e) {
            var g = e.Graphics;

            g.DrawImage(dispBmp, 0, 0);
            if (chkDrawPixelValue.Checked)
                DrawPixelValue(g);
            if (chkDrawHoles.Checked)
                DrawHoles(g);
            if (chkDrawFrame.Checked)
                DrawFrame(g);
            if (chkDrawInfo.Checked)
                DrawInfo(g);
        }

        private void chkDrawFrame_CheckedChanged(object sender, EventArgs e) {
            pbxDraw.Invalidate();
        }

        private void btnResetZoom_Click(object sender, EventArgs e) {
            ptPanning = Point.Empty;
            zoomIndex = zoomIndexReset;
            RedrawImage();
        }

        private void PbxDraw_MouseWheel(object sender, MouseEventArgs e) {
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control) {
                WheelScroll(e);
            } else {
                WheelZoom(e);
            }
            RedrawImage();
        }

        bool mouseDown = false;
        Point ptOld;
        private void pbxDraw_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;

            mouseDown = true;
            ptOld = e.Location;
        }

        private void pbxDraw_MouseMove(object sender, MouseEventArgs e) {
            if (mouseDown) {
                ptPanning += ((Size)e.Location - (Size)ptOld);
                ptOld = e.Location;
                RedrawImage();
            } else {
                pbxDraw.Invalidate();
            }
        }

        private void pbxDraw_MouseUp(object sender, MouseEventArgs e) {
            if (e.Button != MouseButtons.Left)
                return;

            mouseDown = false;
        }

        private void btnInitHoles_Click(object sender, EventArgs e) {
            holeW = (int)numHoleW.Value;
            holeH = (int)numHoleH.Value;
            float left = (float)numHoleLeft.Value;
            float top = (float)numHoleTop.Value;
            float pitchX = (float)numHolePitchX.Value;
            float pitchY = (float)numHolePitchY.Value;
            float dx = (float)numHoleDx.Value;
            float dy = (float)numHoleDy.Value;
            Log("Init Holes");
            holes = InitHoles(holeW, holeH, left, top, pitchX, pitchY, dx, dy);
            Log("End Init Holes");
            pbxDraw.Invalidate();
        }
    }
}
