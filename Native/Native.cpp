// Native.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include "Native.h"
NATIVE_API BOOL Load8BitBmp(BYTE *buf, int bw, int bh, char *filePath) {
    DWORD bytesRead;

    // 파일 오픈
    HANDLE hFile = CreateFile(filePath, GENERIC_READ, FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
        NULL, OPEN_EXISTING, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hFile == INVALID_HANDLE_VALUE)   // 파일오픈 체크
        return FALSE;

    // 파일 헤더
    BITMAPFILEHEADER fh;
    ReadFile(hFile, &fh, sizeof(BITMAPFILEHEADER), &bytesRead, NULL);
    DWORD bufSize = fh.bfSize - fh.bfOffBits;
    if (bufSize != bw * bh) {    // 버퍼사이즈 체크
        CloseHandle(hFile);
        return FALSE;
    }

    // 정보 헤더
    BITMAPINFOHEADER ih;
    ReadFile(hFile, &ih, sizeof(BITMAPINFOHEADER), &bytesRead, NULL);
    if (ih.biBitCount != 8) {   // 컬러비트 체크
        CloseHandle(hFile);
        return  FALSE;
    }

    if (ih.biWidth != bw || ih.biHeight != bh) {  // 이미지사이즈 체크
        CloseHandle(hFile);
        return FALSE;
    }

    LARGE_INTEGER largeSize;
    largeSize.QuadPart = fh.bfOffBits;
    SetFilePointerEx(hFile, largeSize, NULL, FILE_BEGIN);

    // bmp파일은 파일 저장시 라인당 4byte padding을 한다.
    // bw가 4로 나눠 떨어지지 않을경우 padding처리 해야 함
    // int stride = (bw+3)/4*4;

    // bmp파일은 위아래가 뒤집혀 있으므로 파일에서 읽어서 버퍼 아래라인 부터 쓴다
    for (int y = bh - 1; y >= 0; y--) {
        ReadFile(hFile, buf + y * bw, bw, &bytesRead, NULL);
    }

    CloseHandle(hFile);
    return TRUE;
}

DWORD grayPalette[256];
BOOL GenerateGrayPalette() {
    for (int i = 0; i < 256; i++) {
        BYTE *bgra = (BYTE *)&grayPalette[i];
        bgra[0] = bgra[1] = bgra[2] = i;
        bgra[2] = 0xff;
    }
    return TRUE;
}

NATIVE_API BOOL Save8BitBmp(BYTE *buf, int bw, int bh, char *filePath) {
    static BOOL doOnce = GenerateGrayPalette();

    DWORD bytesWritten;
    int bufSize = bw * bh;

    // 파일 오픈
    HANDLE hFile = CreateFile(filePath, GENERIC_WRITE, FILE_SHARE_READ | FILE_SHARE_WRITE | FILE_SHARE_DELETE,
        NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
    if (hFile == INVALID_HANDLE_VALUE)   // 파일오픈 체크
        return FALSE;

    // 파일 헤더
    BITMAPFILEHEADER fh;
    fh.bfType = 0x4d42;  // Magic NUMBER "BM"
    fh.bfOffBits = sizeof(BITMAPFILEHEADER) + sizeof(BITMAPINFOHEADER) + sizeof(grayPalette);   // offset to bitmap buffer from start
    fh.bfSize = fh.bfOffBits + bufSize;  // file size
    fh.bfReserved1 = 0;
    fh.bfReserved2 = 0;
    WriteFile(hFile, &fh, sizeof(BITMAPFILEHEADER), &bytesWritten, NULL);

    // 정보 헤더
    BITMAPINFOHEADER ih;
    ih.biSize = sizeof(BITMAPINFOHEADER);   // struct of BITMAPINFOHEADER
    ih.biWidth = bw; // widht
    ih.biHeight = bh; // height
    ih.biPlanes = 1;
    ih.biBitCount = 8;  // 8bit
    ih.biCompression = BI_RGB;
    ih.biSizeImage = 0;
    ih.biXPelsPerMeter = 3780;  // pixels-per-meter
    ih.biYPelsPerMeter = 3780;  // pixels-per-meter
    ih.biClrUsed = 256;   // grayPalette count
    ih.biClrImportant = 256;   // grayPalette count
    WriteFile(hFile, &ih, sizeof(BITMAPINFOHEADER), &bytesWritten, NULL);

    // RGB Palette
    WriteFile(hFile, &grayPalette, sizeof(grayPalette), &bytesWritten, NULL);

    // bmp파일은 파일 저장시 라인당 4byte padding을 한다.
    // bw가 4로 나눠 떨어지지 않을경우 padding처리 해야 함
    // int stride = (bw+3)/4*4;

    // bmp파일은 위아래가 뒤집혀 있으므로 버퍼 아래라인 부터 읽어서 파일에 쓴다
    for (int y = bh - 1; y >= 0; y--) {
        WriteFile(hFile, buf + y * bw, bw, &bytesWritten, NULL);
    }

    CloseHandle(hFile);
    return TRUE;
}

NATIVE_API void CopyImageBufferZoom(BYTE *sbuf, int sbw, int sbh, BYTE *dbuf, int dbw, int dbh, int dx, int dy, float zoom) {
    int *sys = new int[dbh];
    int *sxs = new int[dbw];

    for (int y = 0; y < dbh;  y++) {
        int sy = (int)floor((y - dy) / zoom);
        sys[y] = (sy < 0 || sy >= sbh) ? -1 : sy;
    }

    for (int x = 0; x < dbw; x++) {
        int sx = (int)floor((x - dx) / zoom);
        sxs[x] = (sx < 0 || sx >= sbw) ? -1 : sx;
    }

    for (int y = 0; y < dbh; y++) {
        BYTE *dp = dbuf + (size_t)dbw * y;
        
        int sy = sys[y];
        if (sy == -1) {
            memset(dp, 128, dbw);
            continue;
        }
        
        BYTE *sp = sbuf + (size_t)sbw * sy;

        for (int x = 0; x < dbw; x++, dp++) {
            int sx = sxs[x];
            *dp = (sx == -1) ? 128 : *(sp + sx);
        }
    }

    delete[] sxs;
    delete[] sys;
}
