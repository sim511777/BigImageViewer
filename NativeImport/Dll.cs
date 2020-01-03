using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NativeImport {
    public class NativeDll {
        const string dll = "native.dll";
        [DllImport(dll)] public static extern bool Load8BitBmp(IntPtr buf, int bw, int bh, string filePath);
        [DllImport(dll)] public static extern bool Save8BitBmp(IntPtr buf, int bw, int bh, string filePath);
        [DllImport(dll)] public static extern void CopyImageBufferZoom(IntPtr sbuf, int sbw, int sbh, IntPtr dbuf, int dbw, int dbh, int panx, int pany, double zoom, int bytepp);
    }
}
