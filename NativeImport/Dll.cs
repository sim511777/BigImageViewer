using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NativeImport {
    public class MsvcrtDll {
        const string dll = "msvcrt.dll";
        [DllImport(dll)] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, ulong _Size);
        [DllImport(dll)] public static extern IntPtr memset(IntPtr _Dst, int _Val, ulong _Size);
    }

    public class NativeDll {
        const string dll = "native.dll";
        [DllImport(dll)] public static extern bool Load8BitBmp(IntPtr buf, int bw, int bh, string filePath);
        [DllImport(dll)] public static extern bool Save8BitBmp(IntPtr buf, int bw, int bh, string filePath);
        [DllImport(dll)] public static extern void CopyImageBufferZoom(IntPtr sbuf, int sbw, int sbh, IntPtr dbuf, int dbw, int dbh, int panx, int pany, int dw, int dh, float zoom, bool clear);
    }
}
