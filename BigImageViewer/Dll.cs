using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BigImageViewer {
    public class MsvcrtDll {
        const string dll = "msvcrt.dll";
        [DllImport(dll)] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, ulong _Size);
        [DllImport(dll)] public static extern IntPtr memset(IntPtr _Dst, int _Val, ulong _Size);
    }

    public class NativeDll {
        const string dll = "native.dll";
        [DllImport(dll)] public static extern bool Load8bitBmp(IntPtr buf, int bw, int bh, string filePath);
        [DllImport(dll)] public static extern void CopyImageBuf(IntPtr srcBuf, int srcBW, int srcBH, IntPtr dstBuf, int dstBW, int dstBH, int offsetX, int offsetY, float zoomLevel);
    }
}
