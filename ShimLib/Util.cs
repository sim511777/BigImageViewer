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

        public unsafe static IntPtr memset4(IntPtr _Dst, uint _Val, long _Size) {
            uint* pdst = (uint*)_Dst.ToPointer();
            for (long i = 0; i < _Size; i++, pdst++) {
                *pdst = _Val;
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

        public unsafe static IntPtr memcpy4(IntPtr _Dst, IntPtr _Src, long _Size) {
            uint* psrc = (uint*)_Src.ToPointer();
            uint* pdst = (uint*)_Dst.ToPointer();
            for (long i = 0; i < _Size; i++, psrc++, pdst++) {
                *pdst = *psrc;
            }
            return _Dst;
        }

        // 이미지 버퍼를 디스플레이 버퍼에 복사
        public unsafe static void CopyImageBufferZoom(IntPtr sbuf, int sbw, int sbh, IntPtr dbuf, int dbw, int dbh, int panx, int pany, double zoom, int bytepp) {
            // 인덱스 버퍼 생성
            int[] siys = new int[dbh];
            int[] sixs = new int[dbw];
            for (int y = 0; y < dbh; y++) {
                int siy = (int)Math.Floor((y - pany) / zoom);
                siys[y] = (sbuf == IntPtr.Zero || siy < 0 || siy >= sbh) ? -1 : siy;
            }
            for (int x = 0; x < dbw; x++) {
                int six = (int)Math.Floor((x - panx) / zoom);
                sixs[x] = (sbuf == IntPtr.Zero || six < 0 || six >= sbw) ? -1 : six;
            }

            // dst 범위만큼 루프를 돌면서 해당 픽셀값 쓰기
            for (int y = 0; y < dbh; y++) {
                int siy = siys[y];
                byte* sp = (byte*)sbuf.ToPointer() + (Int64)sbw * siy * bytepp;
                byte* dp = (byte*)dbuf.ToPointer() + (Int64)dbw * y * 4;
                for (int x = 0; x < dbw; x++, dp += 4) {
                    int six = sixs[x];
                    if (siy == -1 || six == -1) {
                        *(uint*)dp = 0xff808080;
                    } else {
                        if (bytepp == 1) {
                            dp[0] = dp[1] = dp[2] = sp[six];
                        } else {
                            *(uint*)dp = ((uint*)sp)[six];
                        }
                    }
                }
            }
        }
    }
}
