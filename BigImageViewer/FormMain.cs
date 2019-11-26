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
        int frmW;
        int frmH;
        int frmNum;
        int fwdNum;
        int fwdOvlp;
        int ImgBW { get { return (frmW - fwdOvlp) * fwdNum + fwdOvlp; } }
        int ImgBH { get { return frmH * frmNum; } }
        IntPtr imgBuf;

        // QuadTree
        QuadTree tree;

        // zoom, panning
        float[] zoomFactors = { 1f / 512, 3f / 1024, 1f / 256, 3f / 512, 1f / 128, 3f / 256, 1f / 64, 3f / 128, 1f / 32, 3f / 64, 1f / 16, 3f / 32, 1f / 8, 3f / 16, 1f / 4, 3f / 8, 1f / 2, 3f / 4, 1, 3f / 2, 2, 3, 4, 6, 8, 12, 16, 24, 32, 48, 64, 96 };
        string[] zoomTexts = { "1/512", "3/1024", "1/256", "3/512", "1/128", "3/256", "1/64", "3/128", "1/32", "3/64", "1/16", "3/32", "1/8", "3/16", "1/4", "3/8", "1/2", "3/4", "1", "3/2", "2", "3", "4", "6", "8", "12", "16", "24", "32", "48", "64", "96" };
        const int zoomLevelReset = 8;
        int zoomLevel = zoomLevelReset;
        float ZoomFactor { get { return zoomFactors[zoomLevel]; } }
        string ZoomText { get { return zoomTexts[zoomLevel]; } }
        Point ptPanning = Point.Empty;


        // 표시 버퍼 할당
        private void AllocDispBuf() {
            FreeDispBuf();
            
            dispBW = Math.Max((pbxDraw.ClientSize.Width + 3) / 4 * 4, 0);
            dispBH = Math.Max(pbxDraw.ClientSize.Height, 0);

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

        // 이미지 버퍼 해제
        private void FreeImgBuf() {
            if (imgBuf != IntPtr.Zero) {
                Marshal.FreeHGlobal(imgBuf);
            }
        }

        // 이미지 버퍼 할당
        private void AllocImgBuf() {
            frmW = (int)numFW.Value;
            frmH = (int)numFH.Value;
            frmNum = (int)numFNum.Value;
            fwdNum = (int)numFwd.Value;
            fwdOvlp = (int)numFwdOverlap.Value;
            imgBuf = Marshal.AllocHGlobal((IntPtr)((long)ImgBW * ImgBH));
            MsvcrtDll.memset(imgBuf, 0, (ulong)((long)ImgBW * ImgBH));
        }

        // 포워드 이미지 로드
        private void LoadFwdImg(string dir, int ifwd) {
            long frmSize = ImgBW * frmH;
            int succNum = 0;

            Log($"Load Fwd Image");

            for (int i = 0; i < frmNum; i++) {
                string filePath = $"{dir}\\Frame_{i:000}.BMP";
                IntPtr buf = (IntPtr)(imgBuf.ToInt64() + frmSize * i + ifwd * (frmW - fwdOvlp));

                var r = NativeDll.Load8BitBmp(buf, ImgBW, frmH, filePath);
                if (r) {
                    succNum++;
                } else {
                    MsvcrtDll.memset(buf, 0, (ulong)frmSize);
                }
            }
            Log($"Fwd_{ifwd} 이미지 {frmNum}개 중 {succNum}개 이미지 로드 성공");
            Log($"End Load Fwd Image");
        }

        // 이미지 버퍼를 표시 버퍼로 복사
        enum ImageBufferDrawer { C, Dotnet_Unsafe }
        private void RedrawImage() {
            var rect = pbxDraw.ClientRectangle;
            int dw = Math.Min(rect.Width, dispBW);
            int dh = Math.Min(rect.Height, dispBH);
            NativeDll.CopyImageBufferZoom(imgBuf, ImgBW, ImgBH, dispBuf, dispBW, dispBH, dw, dh, ptPanning.X, ptPanning.Y, ZoomFactor, true);
            pbxDraw.Invalidate();
        }

        // 표시 픽셀 좌표를 이미지 좌표로 변환
        private PointF DispToImg(Point pt) {
            var pt2 = pt - (Size)ptPanning;
            return new PointF(pt2.X / ZoomFactor, pt2.Y / ZoomFactor);
        }

        // 이미지 좌표를 표시 픽셀 좌표로 변환
        private Point ImgToDisp(PointF pt) {
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            return new Point((int)Math.Floor(pt2.X + ptPanning.X), (int)Math.Floor(pt2.Y + ptPanning.Y));
        }
        private Rectangle ImgToDisp(RectangleF rect) {
            var pt = rect.Location;
            var pt2 = new PointF(pt.X * ZoomFactor, pt.Y * ZoomFactor);
            var size = rect.Size;
            var size2 = new SizeF(size.Width * ZoomFactor, size.Height * ZoomFactor);
            return new Rectangle((int)Math.Floor(pt2.X + ptPanning.X), (int)Math.Floor(pt2.Y + ptPanning.Y), (int)Math.Floor(size2.Width), (int)Math.Floor(size2.Height));
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
            
            float y = rect.Height;
            if (chkDrawCursorHole.Checked && cursorHole != null) {
                info = cursorHole.ToString();
                rect = g.MeasureString(info, infoFont);
                g.FillRectangle(Brushes.White, 0, y, rect.Width, rect.Height);
                g.DrawString(info, infoFont, Brushes.Black, 0, y);
            }
        }

        // 프레임 표시
        private void DrawFrame(Graphics g) {
            var clientSize = pbxDraw.ClientSize;
            for (int ifwd = 0; ifwd < fwdNum; ifwd++) {
                int x1 = (frmW - fwdOvlp) * ifwd;
                var rectImg = new RectangleF(x1, 0, frmW, ImgBH);
                var rectDisp = ImgToDisp(rectImg);
                if (rectDisp.Bottom < 0 || rectDisp.Top >= clientSize.Height || rectDisp.Right < 0 || rectDisp.Left >= clientSize.Width)
                    continue;

                g.DrawRectangle(Pens.PowderBlue, rectDisp);

                for (int ifrm = 0; ifrm < frmNum; ifrm++) {
                    var ptImg1 = new PointF(x1, frmH * ifrm);
                    var ptImg2 = new PointF(x1 + frmW, frmH * ifrm);
                    var ptDisp1 = ImgToDisp(ptImg1);
                    var ptDisp2 = ImgToDisp(ptImg2);

                    if (ptDisp1.Y < 0 || ptDisp1.Y >= clientSize.Height || ptDisp1.X >= clientSize.Width || ptDisp2.X < 0)
                        continue;

                    g.DrawLine(Pens.PowderBlue, ptDisp1, ptDisp2);
                    if (frmH * ZoomFactor < 20)
                        continue;
                    g.DrawString($"fwd={ifwd}/frm={ifrm}", infoFont, Brushes.LightBlue, ptDisp1.X + 3, ptDisp1.Y + 5);
                }
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
            if (ZoomFactor < 16)
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

        // 휠 스크롤
        private void WheelScrollV(MouseEventArgs e)
        {
            int scroll = 128;
            ptPanning.Y += (e.Delta > 0) ? scroll : -scroll;
        }
        private void WheelScrollH(MouseEventArgs e)
        {
            int scroll = 128;
            ptPanning.X += (e.Delta > 0) ? scroll : -scroll;
        }

        int holeW;
        int holeH;
        Hole[] holes;
        Hole cursorHole;
        // 홀 버퍼 초기화
        private Hole[] InitHoles(int holeW, int holeH, float left, float top, float pitchX, float pitchY, float w, float h) {
            Hole[] holes = new Hole[holeW * holeH];
            int fwdStep = holeW / 4;
            for (int iy = 0; iy < holeH; iy++) {
                for (int ix = 0; ix < holeW; ix++) {
                    float x = left + ix * pitchX;
                    float y = top + iy * pitchY;
                    holes[holeW * iy + ix] = new Hole(x, y, w, h, ix, iy, ix / fwdStep);
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

            float zoomFactor = ZoomFactor;
            float panX = ptPanning.X;
            float panY = ptPanning.Y;

            Pen linePen = Pens.Red;
            Brush infoBrush = Brushes.Yellow;
            Font infoFont = SystemFonts.DefaultFont;

            float holePitch = 32.0f;
            bool holeDrawCircle = zoomFactor > (4.0f / holePitch);

            HoleInfoItemType infoItemType = HoleInfoItemType.None;
            if (rdoHoleInfoIndexX.Checked)
                infoItemType = HoleInfoItemType.IndexX;
            else if (rdoHoleInfoIndexY.Checked)
                infoItemType = HoleInfoItemType.IndexY;
            else if (rdoHoleInfoFWD.Checked)
                infoItemType = HoleInfoItemType.Fwd;

            // 모든 홀을 조회하면 비저블 영역에 포함되는 홀만 그림
            // zoom이 낮아지면 띄엄띄엄 그림
            //int step = (int)(2.0f / (zoomLevel * holePitch));
            //if (step < 1)
            //    step = 1;
            //for (int iy = 0; iy < holeH; iy += step) {
            //    for (int ix = 0; ix < holeW; ix += step) {
            //        var hole = holes[holeW * iy + ix];
            //        if (hole.x < imgX1 || hole.x > imgX2 || hole.y < imgY1 || hole.y > imgY2)
            //            continue;

            //        DrawHole(g, zoomLevel, panX, panY, hole, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
            //    }
            //}

            // 쿼드트리 사용하여 비저블 역역에 포함되는 노드만 그림
            DrawNodeHole(tree.root, imgX1, imgY1, imgX2, imgY2, g, zoomFactor, panX, panY, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);

            if (chkDrawCursorHole.Checked && cursorHole != null) {
                DrawHole(g, zoomFactor, panX, panY, cursorHole, holeDrawCircle, Pens.Lime, infoItemType, infoFont, infoBrush);
            }
        }

        // 마우스 커서 홀 가져오기
        private void GetCursorHole() {
            if (tree == null) {
                cursorHole = null;
                return;
            }

            Point ptCur = pbxDraw.PointToClient(Cursor.Position);
            PointF ptImg = DispToImg(ptCur);
            float holePitch = 32.0f;
            float pickHoleMargin = holePitch * 0.5f;
            cursorHole = GetCursorNodeHole(tree.root, ptImg, pickHoleMargin);
        }

        private Hole GetCursorNodeHole(QuadTreeNode node, PointF ptImg, float pickHoleMargin) {
            if (node == null)
                return null;
            
            if (node.holes != null) {
                foreach (var hole in node.holes) {
                    // 홀 boundary box 로 체크
                    //float x1 = hole.x - hole.w * 0.5f;
                    //float y1 = hole.y - hole.h * 0.5f;
                    //float x2 = x1 + hole.w;
                    //float y2 = y1 + hole.h;
                    //if (ptImg.X >= x1 && ptImg.Y >= y1 && ptImg.X <= x2 && ptImg.Y <= y2)
                    //    return hole;

                    // 홀 타원으로 체크
                    float cx = ptImg.X - hole.x;
                    float cy = ptImg.Y - hole.y;
                    float rx = hole.w * 0.5f;
                    float ry = hole.h * 0.5f;
                    if ((cx*cx)/(rx*rx) + (cy*cy)/(ry*ry) <= 1.0f)
                        return hole;
                }
                return null;
            }
            
            float midX = (node.x1 + node.x2) * 0.5f;
            float midY = (node.y1 + node.y2) * 0.5f;
            bool checkTop = (ptImg.Y <= midY + pickHoleMargin);
            bool checkBottom = (ptImg.Y >= midY - pickHoleMargin);
            bool checkLeft = (ptImg.X <= midX + pickHoleMargin);
            bool checkRight = (ptImg.X >= midX - pickHoleMargin);
            if (checkLeft && checkTop) {
                var pickHole = GetCursorNodeHole(node.childLT, ptImg, pickHoleMargin);
                if (pickHole != null)
                    return pickHole;
            }
            if (checkRight && checkTop) {
                var pickHole = GetCursorNodeHole(node.childRT, ptImg, pickHoleMargin);
                if (pickHole != null)
                    return pickHole;
            }
            if (checkLeft && checkBottom) {
                var pickHole = GetCursorNodeHole(node.childLB, ptImg, pickHoleMargin);
                if (pickHole != null)
                    return pickHole;
            }
            if (checkRight && checkBottom) {
                var pickHole = GetCursorNodeHole(node.childRB, ptImg, pickHoleMargin);
                if (pickHole != null)
                    return pickHole;
            }
            return null;
        }

        // 노드 홀 그리기
        private void DrawNodeHole(QuadTreeNode node, float imgX1, float imgY1, float imgX2, float imgY2, Graphics g, float zoomFactor, float panX, float panY, bool holeDrawCircle, Pen linePen, HoleInfoItemType infoItemType, Font infoFont, Brush infoBrush) {
            // 뷰 영역에 벗어난 노드는 리턴
            if (node.x1 > imgX2 || node.y1 > imgY2 || node.x2 < imgX1 || node.y2 < imgY1)
                return;

            // 현재 레벨에서 드로우
            if ((node.x2 - node.x1) * zoomFactor <= 4.0f) {
                DrawHole(g, zoomFactor, panX, panY, node.holeFront, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
                return;
            }

            // 노드가 리프 노드 이면 노드에 포함된 홀 그리고 리턴
            if (node.holes != null) {
                foreach (Hole hole in node.holes)
                    DrawHole(g, zoomFactor, panX, panY, hole, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
                return;
            }

            // 하위 노드로 내려감
            if (node.childLT != null)
                DrawNodeHole(node.childLT, imgX1, imgY1, imgX2, imgY2, g, zoomFactor, panX, panY, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
            if (node.childRT != null)
                DrawNodeHole(node.childRT, imgX1, imgY1, imgX2, imgY2, g, zoomFactor, panX, panY, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
            if (node.childLB != null)
                DrawNodeHole(node.childLB, imgX1, imgY1, imgX2, imgY2, g, zoomFactor, panX, panY, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
            if (node.childRB != null)
                DrawNodeHole(node.childRB, imgX1, imgY1, imgX2, imgY2, g, zoomFactor, panX, panY, holeDrawCircle, linePen, infoItemType, infoFont, infoBrush);
        }

        // 홀그리기
        private void DrawHole(Graphics g, float zoomFactor, float panX, float panY, Hole hole, bool holeDrawCircle, Pen linePen, HoleInfoItemType infoItemType, Font infoFont, Brush infoBrush) {
            if (holeDrawCircle)
                DrawHoleCircle(g, zoomFactor, panX, panY, hole, linePen);
            else
                DrawHolePoint(g, zoomFactor, panX, panY, hole, linePen);

            if (infoItemType == HoleInfoItemType.None || zoomFactor < 0.5f)
                return;
            
            string infoText;
            if (infoItemType == HoleInfoItemType.IndexX)
                infoText = hole.idxX.ToString();
            else if (infoItemType == HoleInfoItemType.IndexY)
                infoText = hole.idxY.ToString();
            else
                infoText = hole.fwd.ToString();
            DrawHoleInfo(g, zoomFactor, panX, panY, hole, infoText, infoFont, infoBrush);
        }

        // 개별 홀 써클 드로우
        private void DrawHoleCircle(Graphics g, float zoomFactor, float panX, float panY, Hole hole, Pen pen) {
            float x = (hole.x - hole.w / 2f) * zoomFactor + panX;
            float y = (hole.y - hole.h / 2f) * zoomFactor + panY;
            float width = hole.w * zoomFactor;
            float height = hole.h * zoomFactor;
            g.DrawEllipse(pen, x, y, width, height);
        }

        // 개별 홀 포인트 드로우
        private void DrawHolePoint(Graphics g, float zoomFactor, float panX, float panY, Hole hole, Pen linePen) {
            float x = (int)(hole.x * zoomFactor + panX) - 0.5f;
            float y = (int)(hole.y * zoomFactor + panY) - 0.5f;
            g.DrawLine(linePen, x, y, x + 1, y + 1);
        }

        // 홀 정보 표시
        private void DrawHoleInfo(Graphics g, float zoomFactor, float panX, float panY, Hole hole, string infoText, Font font, Brush brush) {
            float x = hole.x * zoomFactor + panX;
            float y = hole.y * zoomFactor + panY;
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

        private void btnLoadFwd_Click(object sender, EventArgs e) {
            LoadFwdImg(tbxFwdDir.Text, 0);
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
            zoomLevel = zoomLevelReset;
            RedrawImage();
        }

        private void PbxDraw_MouseWheel(object sender, MouseEventArgs e) {
            if (Control.ModifierKeys == Keys.Control) {
                WheelScrollV(e);
            } else if (Control.ModifierKeys == Keys.Shift) {
                WheelScrollH(e);
            } else {
                WheelZoom(e);
            }
            GetCursorHole();
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
                var rect = pbxDraw.ClientRectangle;
                int dw = Math.Min(rect.Width, dispBW);
                int dh = Math.Min(rect.Height, dispBH);
                NativeDll.CopyImageBufferZoom(imgBuf, ImgBW, ImgBH, dispBuf, dispBW, dispBH, dw, dh, ptPanning.X, ptPanning.Y, ZoomFactor, true);
            }
            
            GetCursorHole();
            
            pbxDraw.Invalidate();
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

            float minX = left;
            float minY = top;
            float maxX = left + (holeW - 1) * pitchX;
            float maxY = top + (holeH - 1) * pitchY;
            float minPitch = Math.Min(pitchX, pitchY) * 4.0f;
            Log("Init QuadTree");
            tree = QuadTree.Generate(minX, minY, maxX, maxY, minPitch);
            Log("End Init QuadTree");

            Log("Add Holes");
            tree.AddHoles(holes);
            Log("End Add Holes");

            pbxDraw.Invalidate();
        }

        private void pbxDraw_Layout(object sender, LayoutEventArgs e) {
            AllocDispBuf();
            RedrawImage();
        }

        private void btnLoadSurf_Click(object sender, EventArgs e) {
            bool fromBack = chkFromBack.Checked;
            for (int ifwd = 0; ifwd < fwdNum; ifwd++) {
                int fwd = ifwd;
                if (fromBack)
                    fwd = fwdNum - ifwd - 1;

                string dir = tbxSurfDir.Text + "\\Fwd" + fwd.ToString();
                LoadFwdImg(dir, fwd);
            }
            RedrawImage();
        }
    }
}
