using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    class Alg {
        public static bool Load8bitBmp(IntPtr buf, int bw, int bh, string filePath) {
            Bitmap bmp = new Bitmap(filePath);
            BitmapData bd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
            MsvcrtDll.memcpy(buf, bd.Scan0, (ulong)(bw * bh));
            bmp.UnlockBits(bd);
            bmp.Dispose();
            return true;
        }
    }
}
