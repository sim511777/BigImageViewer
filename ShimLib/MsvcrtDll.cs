using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShimLib {
    public class MsvcrtDll {
        const string dll = "msvcrt.dll";
        [DllImport(dll)] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, long _Size);
        [DllImport(dll)] public static extern IntPtr memset(IntPtr _Dst, int _Val, long _Size);
    }
}