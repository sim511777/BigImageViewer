using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace BigImageViewer {
    public class Msvcrt {
        const string dll = "msvcrt.dll";
        [DllImport(dll)] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, ulong _Size);
        [DllImport(dll)] public static extern IntPtr memset(IntPtr _Dst, int _Val, ulong _Size);
    }
}
