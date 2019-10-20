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
            InitBuf();
        }

        ~FormMain() {
            FreeBuf();
        }

        public void Log(string msg) {
            DateTime now = DateTime.Now;
            string timeMsg = now.ToString("[yyyy/MM/dd HH:mm:ss.fff] ");
            tbxLog.AppendText(timeMsg + msg + Environment.NewLine);
        }

        int dispBW;
        int dispBH;
        IntPtr dispBuf;
        Bitmap dispBmp;
        
        int imgFW;
        int imgFH;
        int iNum;
        int ImgBW { get { return imgFW; } }
        int ImgBH { get { return imgFH * iNum; } }
        IntPtr imgBuf;

        private void InitBuf() {
            dispBW = 1920;
            dispBH = 1080;
            dispBuf = Marshal.AllocHGlobal((IntPtr)(dispBW * dispBH));
            Msvcrt.memset(dispBuf, 0, (ulong)(dispBW * dispBH));
            dispBmp = new Bitmap(dispBW, dispBH, dispBW, PixelFormat.Format8bppIndexed, dispBuf);
        }

        private void FreeBuf() {
            throw new NotImplementedException();
        }

        private void btnAlloc_Click(object sender, EventArgs e) {
            Log("Alloc Buffer");
            imgFW = (int)numFW.Value;
            imgFH = (int)numFH.Value;
            iNum = (int)numFNum.Value;
            if (imgBuf != IntPtr.Zero)
                Marshal.FreeHGlobal(imgBuf);
            imgBuf = Marshal.AllocHGlobal((IntPtr)((long)ImgBW * ImgBH));
            Msvcrt.memset(imgBuf, 0, (ulong)((long)ImgBW * ImgBH));
            Log("End Alloc Buffer");
        }
    }
}
