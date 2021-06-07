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
using ShimLib;
using PointF = System.Drawing.PointF;

namespace BigImageViewer {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
        }

        // 로그 출력
        public void Log(string msg) {
            DateTime now = DateTime.Now;
            string timeMsg = now.ToString("[yy/MM/dd HH:mm:ss.fff] ");
            tbxLog.AppendText(timeMsg + msg + Environment.NewLine);
        }

        // 이미지 버퍼
        int imgBW;
        int imgBH;
        IntPtr imgBuf;

        // 포워드 정보
        int frmW;
        int frmH;
        int frmNum;
        int fwdNum;
        int fwdOvlp;

        // QuadTree
        QuadTree tree;

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

            imgBW = (frmW - fwdOvlp) * fwdNum + fwdOvlp;
            imgBH = frmH * frmNum;
            imgBuf = Marshal.AllocHGlobal((IntPtr)((long)imgBW * imgBH));
            Util.Memset(imgBuf, 0, (long)imgBW * imgBH);
        }

        // 포워드 이미지 로드
        private void LoadFwdImg(string dir, int ifwd) {
            long frmSize = imgBW * frmH;
            int succNum = 0;

            Log($"Load Fwd Image");

            for (int i = 0; i < frmNum; i++) {
                string filePath = $"{dir}\\Frame_{i:000}.BMP";
                IntPtr buf = (IntPtr)(imgBuf.ToInt64() + frmSize * i + ifwd * (frmW - fwdOvlp));

                var r = Util.Load8BitBmp(buf, imgBW, frmH, filePath);
                if (r) {
                    succNum++;
                } else {
                    Util.Memset(buf, 0, frmSize);
                }
            }
            Log($"Fwd_{ifwd} 이미지 {frmNum}개 중 {succNum}개 이미지 로드 성공");
            Log($"End Load Fwd Image");
        }

        // 현재 홀 정보 표시
        private void DrawCursorHoleInfo(ImageDrawing id) {
            float y = 23;
            if (cursorHole != null) {
                string info = cursorHole.ToString();
                //var rect = id.MeasureString(info, defFont);
                //id.FillRectangle(Color.White, 0, y, rect.Width, rect.Height);
                id.DrawString(info, defFont, Color.Black, 0, (int)y, Color.White);
            }
        }

        // 프레임 표시
        private void DrawFrame(ImageDrawing id) {
            var clientSize = pbxDraw.ClientSize;
            for (int ifwd = 0; ifwd < fwdNum; ifwd++) {
                int xfwd = (frmW - fwdOvlp) * ifwd;
                int yfwd = 0;
                id.DrawRectangle(Color.PowderBlue, xfwd - 0.5f, yfwd - 0.5f, frmW, imgBH);

                for (int ifrm = 1; ifrm < frmNum; ifrm++) {
                    int xfrm1 = xfwd;
                    int xfrm2 = xfwd + frmW;
                    int yfrm = yfwd + ifrm * frmH;
                    id.DrawLine(Color.PowderBlue, xfrm1 - 0.5f, yfrm - 0.5f, xfrm2 - 0.5f, yfrm - 0.5f);
                    if (frmH * pbxDraw.ZoomFactor < 20)
                        continue;
                    id.DrawString($"fwd={ifwd}/frm={ifrm}", Fonts.Unicode_16x16_hex, Color.LightBlue, xfrm1 - 0.5f, yfrm - 0.5f);
                }
            }
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
                    holes[holeW * iy + ix] = new Hole(x, y, w, h, ix, iy, (int)x / (frmW - fwdOvlp));
                }
            }
            return holes;
        }

        enum HoleInfoItemType { None, IndexX, IndexY, Fwd, }

        // 홀 드로우
        bool skipDrawHoleInfo;
        private void DrawHoles(ImageDrawing id) {
            if (holes == null)
                return;

            var ptDisp1 = new Point(0, 0);
            var ptDisp2 = (Point)pbxDraw.ClientSize;
            var ptImg1 = pbxDraw.DispToImg(ptDisp1);
            var ptImg2 = pbxDraw.DispToImg(ptDisp2);
            float imgX1 = (float)Math.Floor(ptImg1.X);
            float imgY1 = (float)Math.Floor(ptImg1.Y);
            float imgX2 = (float)Math.Floor(ptImg2.X);
            float imgY2 = (float)Math.Floor(ptImg2.Y);

            double zoomFactor = pbxDraw.ZoomFactor;
            skipDrawHoleInfo = zoomFactor < 0.5;

            Color lineColor = Color.Red;
            Color infoColor = Color.Yellow;
            IFont infoFont = Fonts.Unicode_16x16_hex;

            float holePitch = 32.0f;
            bool holeDrawCircle = zoomFactor > (4.0 / holePitch);

            HoleInfoItemType infoItemType = HoleInfoItemType.None;
            if (rdoHoleInfoIndexX.Checked)
                infoItemType = HoleInfoItemType.IndexX;
            else if (rdoHoleInfoIndexY.Checked)
                infoItemType = HoleInfoItemType.IndexY;
            else if (rdoHoleInfoFWD.Checked)
                infoItemType = HoleInfoItemType.Fwd;

            // 쿼드트리 사용하여 비저블 역역에 포함되는 노드만 그림
            DrawNodeHole(tree.root, imgX1, imgY1, imgX2, imgY2, id, zoomFactor, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);

            if (chkDrawCursorHole.Checked && cursorHole != null) {
                DrawHole(id, cursorHole, holeDrawCircle, Color.Lime, infoItemType, infoFont, infoColor);
            }
        }

        // 마우스 커서 홀 가져오기
        private void GetCursorHole() {
            if (tree == null) {
                cursorHole = null;
                return;
            }

            Point ptCur = pbxDraw.PointToClient(Cursor.Position);
            PointF ptImg = pbxDraw.DispToImg(ptCur);
            double holePitch = 32.0;
            double pickHoleMargin = holePitch * 0.5;
            cursorHole = GetCursorNodeHole(tree.root, ptImg, pickHoleMargin);
        }

        private Hole GetCursorNodeHole(QuadTreeNode node, PointF ptImg, double pickHoleMargin) {
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
                    double cx = ptImg.X - hole.x;
                    double cy = ptImg.Y - hole.y;
                    double rx = hole.w * 0.5;
                    double ry = hole.h * 0.5;
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
        private void DrawNodeHole(QuadTreeNode node, float imgX1, float imgY1, float imgX2, float imgY2, ImageDrawing id, double zoomFactor, bool holeDrawCircle, Color lineColor, HoleInfoItemType infoItemType, IFont infoFont, Color infoColor) {
            // 뷰 영역에 벗어난 노드는 리턴
            if (node.x1 > imgX2 || node.y1 > imgY2 || node.x2 < imgX1 || node.y2 < imgY1)
                return;

            // 현재 레벨에서 드로우
            if ((node.x2 - node.x1) * zoomFactor <= 8.0) {
                DrawHole(id, node.holeFront, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
                return;
            }

            // 노드가 리프 노드 이면 노드에 포함된 홀 그리고 리턴
            if (node.holes != null) {
                foreach (Hole hole in node.holes)
                    DrawHole(id, hole, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
                return;
            }

            // 하위 노드로 내려감
            if (node.childLT != null)
                DrawNodeHole(node.childLT, imgX1, imgY1, imgX2, imgY2, id, zoomFactor, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
            if (node.childRT != null)
                DrawNodeHole(node.childRT, imgX1, imgY1, imgX2, imgY2, id, zoomFactor, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
            if (node.childLB != null)
                DrawNodeHole(node.childLB, imgX1, imgY1, imgX2, imgY2, id, zoomFactor, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
            if (node.childRB != null)
                DrawNodeHole(node.childRB, imgX1, imgY1, imgX2, imgY2, id, zoomFactor, holeDrawCircle, lineColor, infoItemType, infoFont, infoColor);
        }

        // 홀그리기
        private void DrawHole(ImageDrawing id, Hole hole, bool holeDrawCircle, Color lineColor, HoleInfoItemType infoItemType, IFont infoFont, Color infoColor) {
            if (holeDrawCircle)
                DrawHoleCircle(id, hole, lineColor);
            else
                DrawHolePoint(id, hole, lineColor);

            if (infoItemType == HoleInfoItemType.None || skipDrawHoleInfo)
                return;
            
            string infoText;
            if (infoItemType == HoleInfoItemType.IndexX)
                infoText = hole.idxX.ToString();
            else if (infoItemType == HoleInfoItemType.IndexY)
                infoText = hole.idxY.ToString();
            else
                infoText = hole.fwd.ToString();
            DrawHoleInfo(id, hole, infoText, infoFont, infoColor);
        }

        // 개별 홀 써클 드로우
        private void DrawHoleCircle(ImageDrawing id, Hole hole, Color color) {
            float x1 = hole.x - hole.w * 0.5f;
            float y1 = hole.y - hole.h * 0.5f;
            float x2 = hole.x + hole.w * 0.5f;
            float y2 = hole.y + hole.h * 0.5f;
            id.DrawEllipse(color, x1, y1, x2 - x1, y2 - y1);
        }

        // 개별 홀 포인트 드로우
        private void DrawHolePoint(ImageDrawing id, Hole hole, Color lineColor) {
            id.DrawSquare(lineColor, hole.x, hole.y, 1, false);
        }

        // 홀 정보 표시
        private void DrawHoleInfo(ImageDrawing id, Hole hole, string infoText, IFont font, Color color) {
            id.DrawString(infoText, font, color, new PointF(hole.x, hole.y));
        }

        //=============================================================
        private void btnAlloc_Click(object sender, EventArgs e) {
            Log("Alloc Buffer");
            FreeImgBuf();
            AllocImgBuf();
            Log("End Alloc Buffer");
            pbxDraw.SetImageBuffer(imgBuf, imgBW, imgBH, 1, false);
            pbxDraw.Invalidate();
        }

        private void btnLoadFwd_Click(object sender, EventArgs e) {
            LoadFwdImg(tbxFwdDir.Text, 0);
            pbxDraw.Invalidate();
        }

        private void btnClearLog_Click(object sender, EventArgs e) {
            tbxLog.Clear();
        }

        IFont defFont = Fonts.Unicode_16x16_hex;
        private void pbxDraw_Paint(object sender, PaintEventArgs e) {
        }

        private void chkDrawFrame_CheckedChanged(object sender, EventArgs e) {
            pbxDraw.Invalidate();
        }

        private void btnResetZoom_Click(object sender, EventArgs e) {
            pbxDraw.ResetZoom();
            pbxDraw.Invalidate();
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

        private void btnLoadSurf_Click(object sender, EventArgs e) {
            bool fromBack = chkFromBack.Checked;
            for (int ifwd = 0; ifwd < fwdNum; ifwd++) {
                int fwd = ifwd;
                if (fromBack)
                    fwd = fwdNum - ifwd - 1;

                string dir = tbxSurfDir.Text + "\\Fwd" + fwd.ToString();
                LoadFwdImg(dir, fwd);
            }
            pbxDraw.Invalidate();
        }

        private void pbxDraw_MouseMove(object sender, MouseEventArgs e) {
            GetCursorHole();
            pbxDraw.Invalidate();
        }

        private void pbxDraw_PaintBackBuffer(object sender, IntPtr buf, int bw, int bh) {
            var id = pbxDraw.GetImageDrawing(buf, bw, bh);
            if (chkDrawHoles.Checked)
                DrawHoles(id);
            if (chkDrawFrame.Checked)
                DrawFrame(id);
            var idWnd = new ImageDrawing(buf, bw, bh);
            if (chkDrawCursorHole.Checked)
                DrawCursorHoleInfo(idWnd);
        }
    }
}
