using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace BigImageViewer {
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct BITMAPFILEHEADER {
        public ushort bfType;
        public uint bfSize;
        public ushort bfReserved1;
        public ushort bfReserved2;
        public uint bfOffBits;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct BITMAPINFOHEADER {
        public uint biSize;
        public int biWidth;
        public int biHeight;
        public ushort biPlanes;
        public ushort biBitCount;
        public uint biCompression;
        public uint biSizeImage;
        public int biXPelsPerMeter;
        public int biYPelsPerMeter;
        public uint biClrUsed;
        public uint biClrImportant;
    }

    public class Util {
        // 8bit bmp 파일 버퍼에 로드
        public unsafe static T StreamReadStructure<T>(Stream sr) {
            int size = Marshal.SizeOf(typeof(T));
            byte[] buf = new byte[size];
            sr.Read(buf, 0, size);
            fixed (byte* ptr = buf) {
                T obj = (T)Marshal.PtrToStructure((IntPtr)ptr, typeof(T));
                return obj;
            }
        }
        public static bool Load8BitBmp(IntPtr buf, int bw, int bh, string filePath) {
            // 파일 오픈
            FileStream hFile;
            try {
                hFile = File.OpenRead(filePath);
            } catch {
                return false;
            }

            // 파일 헤더
            BITMAPFILEHEADER fh = StreamReadStructure<BITMAPFILEHEADER>(hFile);

            // 정보 헤더
            BITMAPINFOHEADER ih = StreamReadStructure<BITMAPINFOHEADER>(hFile);
            if (ih.biBitCount != 8) {   // 컬러비트 체크
                hFile.Dispose();
                return false;
            }

            hFile.Seek(fh.bfOffBits, SeekOrigin.Begin);

            int fbw = ih.biWidth;
            int fbh = ih.biHeight;

            // bmp파일은 파일 저장시 라인당 4byte padding을 한다.
            // bw가 4로 나눠 떨어지지 않을경우 padding처리 해야 함
            // int stride = (bw+3)/4*4;buf + y * bw
            int fstep = (fbw + 3) / 4 * 4;

            byte[] fbuf = new byte[fbh * fstep];
            hFile.Read(fbuf, 0, fbh * fstep);

            // 대상버퍼 width/height 소스버퍼 width/height 중 작은거 만큼 카피
            int minh = Math.Min(bh, fbh);
            int minw = Math.Min(bw, fbw);

            // bmp파일은 위아래가 뒤집혀 있으므로 파일에서 아래 라인부터 읽어서 버퍼에 쓴다
            for (int y = 0; y < minh; y++) {
                Marshal.Copy(fbuf, (fbh - y - 1) * fstep, buf + y * bw, minw);
            }

            hFile.Dispose();
            return true;
        }

        // Load Bitmap to buffer
        public unsafe static void BitmapToImageBuffer(Bitmap bmp, ref IntPtr imgBuf, ref int bw, ref int bh, ref int bytepp) {
            bw = bmp.Width;
            bh = bmp.Height;
            if (bmp.PixelFormat == PixelFormat.Format8bppIndexed)
                bytepp = 1;
            else if (bmp.PixelFormat == PixelFormat.Format16bppGrayScale)
                bytepp = 2;
            else if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                bytepp = 3;
            else if (bmp.PixelFormat == PixelFormat.Format32bppRgb || bmp.PixelFormat == PixelFormat.Format32bppArgb || bmp.PixelFormat == PixelFormat.Format32bppPArgb)
                bytepp = 4;
            long bufSize = (long)bw * bh * bytepp;
            imgBuf = AllocBuffer(bufSize);

            BitmapData bmpData = bmp.LockBits(new Rectangle(0, 0, bw, bh), ImageLockMode.ReadOnly, bmp.PixelFormat);
            int copySize = bw * bytepp;
            for (int y = 0; y < bh; y++) {
                IntPtr dstPtr = (IntPtr)((long)imgBuf + bw * y * bytepp);
                IntPtr srcPtr = (IntPtr)((long)bmpData.Scan0 + bmpData.Stride * y);
                Memcpy(dstPtr, srcPtr, copySize);
            }

            bmp.UnlockBits(bmpData);
        }

        public static unsafe IntPtr Memcpy(IntPtr _Dst, IntPtr _Src, Int64 _Size) {
            Int64 size4 = _Size / 4;
            Int64 size1 = _Size % 4;

            int* pdst4 = (int*)_Dst;
            int* psrc4 = (int*)_Src;
            while (size4-- > 0)
                *pdst4++ = *psrc4++;

            byte* pdst1 = (byte*)pdst4;
            byte* psrc1 = (byte*)psrc4;
            while (size1-- > 0)
                *pdst1++ = *psrc1++;

            return _Dst;
        }

        public static unsafe IntPtr Memset(IntPtr _Dst, int _Val, Int64 _Size) {
            Int64 size4 = _Size / 4;
            Int64 size1 = _Size % 4;

            int val4 = _Val | _Val << 8 | _Val << 16 | _Val << 24;
            byte val1 = (byte)_Val;

            int* pdst4 = (int*)_Dst;
            while (size4-- > 0)
                *pdst4++ = val4;

            byte* pdst1 = (byte*)pdst4;
            while (size1-- > 0)
                *pdst1++ = val1;

            return _Dst;
        }

        public static unsafe IntPtr Memset4(IntPtr _Dst, int _Val, Int64 _Size) {
            int* pdst = (int*)_Dst;
            while (_Size-- > 0)
                *pdst++ = _Val;
            return _Dst;
        }

        // free and set null
        public static void FreeBuffer(ref IntPtr buf) {
            if (buf != IntPtr.Zero) {
                Marshal.FreeHGlobal(buf);
                buf = IntPtr.Zero;
            }
        }

        public static IntPtr AllocBuffer(Int64 size) {
            IntPtr buf = Marshal.AllocHGlobal((IntPtr)size);
            Memset(buf, 0, size);
            return buf;
        }
    }
}
