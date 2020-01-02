using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ShimLib {
    public class Msvcrt {
        const string dll = "msvcrt.dll";
        [DllImport("msvcrt.dll")] public static extern IntPtr memcpy(IntPtr _Dst, IntPtr _Src, ulong _Size);
        [DllImport("msvcrt.dll")] public static extern IntPtr memset(IntPtr _Dst, int _Val, ulong _Size);
    }
}
