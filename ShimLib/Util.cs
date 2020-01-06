using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShimLib {
    public class Util {
        // 범위 제한 함수
        public static int IntClamp(int value, int min, int max) {
            if (value < min) return min;
            if (value > max) return max;
            return value;
        }

        public unsafe static IntPtr memset(IntPtr _Dst, int _Val, long _Size) {
            byte valByte = (byte)_Val;
            byte* pdst = (byte*)_Dst.ToPointer();
            for (long i = 0; i < _Size; i++, pdst++) {
                *pdst = valByte;
            }
            return _Dst;
        }

        public unsafe static IntPtr memcpy(IntPtr _Dst, IntPtr _Src, long _Size) {
            byte* psrc = (byte*)_Src.ToPointer();
            byte* pdst = (byte*)_Dst.ToPointer();
            for (long i = 0; i < _Size; i++, psrc++, pdst++) {
                *pdst = *psrc;
            }
            return _Dst;
        }
    }
}
