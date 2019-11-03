using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BigImageViewer {
    class Alg {
        unsafe public static void CopyImageBuf(IntPtr srcBuf, int srcBW, int srcBH, IntPtr dstBuf, int dstBW, int dstBH, int offsetX, int offsetY, float zoomLevel) {
            int[] srcYs = new int[dstBH];
            int[] srcXs = new int[dstBW];

            for (int y = 0; y < dstBH; y++) {
                int srcY = (int)Math.Floor((y - offsetY) / zoomLevel);
                srcYs[y] = (srcY < 0 || srcY >= srcBH) ? -1 : srcY;
            }

            for (int x = 0; x < dstBW; x++) {
                int srcX = (int)Math.Floor((x - offsetX) / zoomLevel);
                srcXs[x] = (srcX < 0 || srcX >= srcBW) ? -1 : srcX;
            }

            for (int y = 0; y < dstBH; y++) {
                byte* dstPtr = (byte*)dstBuf.ToPointer() + (long)dstBW * y;

                int srcY = srcYs[y];
                if (srcY == -1) {
                    MsvcrtDll.memset((IntPtr)dstPtr, 128, (ulong)dstBW);
                    continue;
                }

                byte* srcPtr = (byte*)srcBuf.ToPointer() + (long)srcBW * srcY;

                for (int x = 0; x < dstBW; x++, dstPtr += 1) {
                    int srcX = srcXs[x];
                    *dstPtr = (srcX == -1) ? (byte)128 : *(srcPtr + srcX);
                }
            }
        }
    }
}
