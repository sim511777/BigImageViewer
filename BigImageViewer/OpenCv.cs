using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    class OpenCv {
        public static bool Load8bitBmp(IntPtr buf, int bw, int bh, string filePath) {
            Mat mat = new Mat(filePath, ImreadModes.Grayscale);
            MsvcrtDll.memcpy(buf, mat.Data, (ulong)(bw * bh));
            mat.Dispose();
            return true;
        }
    }
}
