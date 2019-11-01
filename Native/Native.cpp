// Native.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
//

#include "stdafx.h"
#include "Native.h"
NATIVE_API BOOL Load8bitBmp(BYTE* buf, int bw, int bh, char* filePath) {
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

NATIVE_API BOOL Save8bitBmp(BYTE *buf, int bw, int bh, char *filePath) {
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

NATIVE_API void CopyImageBuf(BYTE* srcBuf, int srcBW, int srcBH, BYTE* dstBuf, int dstBW, int dstBH, int offsetX, int offsetY, float zoomLevel) {
    int *srcYs = new int[dstBH];
    int *srcXs = new int[dstBW];

    for (int y = 0; y < dstBH;  y++) {
        int srcY = (int)floor((y - offsetY) / zoomLevel);
        srcYs[y] = (srcY < 0 || srcY >= srcBH) ? -1 : srcY;
    }

    for (int x = 0; x < dstBW; x++) {
        int srcX = (int)floor((x - offsetX) / zoomLevel);
        srcXs[x] = (srcX < 0 || srcX >= srcBW) ? -1 : srcX;
    }

    for (int y = 0; y < dstBH; y++) {
        BYTE *dstPtr = dstBuf + (size_t)dstBW * y;
        
        int srcY = srcYs[y];
        if (srcY == -1) {
            memset(dstPtr, 128, dstBW);
            continue;
        }
        
        BYTE *srcPtr = srcBuf + (size_t)srcBW * srcY;

        for (int x = 0; x < dstBW; x++, dstPtr++) {
            int srcX = srcXs[x];
            *dstPtr = (srcX == -1) ? 128 : *(srcPtr + srcX);
        }
    }

    delete[] srcXs;
    delete[] srcYs;
}

NATIVE_API void DrawDC(HDC hdc) {
    Ellipse(hdc, 0, 0, 100, 100);
}

// 표시 픽셀 좌표를 이미지 좌표로 변환
float DispToImg(int pt, float zoomLevel, int pan) {
    return (pt - pan) / zoomLevel;
}

// 이미지 좌표를 표시 픽셀 좌표로 변환
int ImgToDisp(float pt, float zoomLevel, int pan) {
    return (int)floor(pt * zoomLevel + pan);
}

#define VC_BLACK       RGB(0,0,0)
#define VC_BLUE        RGB(0,0,255)
#define VC_GREEN       RGB(0,255,0)
#define VC_CYAN        RGB(0,255,255)
#define VC_RED         RGB(255,0,0)
#define VC_MAGENTA     RGB(255,0,255)
#define VC_YELLOW      RGB(255,255,0)
#define VC_WHITE       RGB(255,255,255)
#define VC_DARKBLUE    RGB(0,0,128)
#define VC_DARKGREEN   RGB(0,128,0)
#define VC_DARKCYAN    RGB(0,128,128)
#define VC_DARKRED     RGB(128,0,0)
#define VC_DARKMAGENTA RGB(128,0,128)
#define VC_DARKYELLOW  RGB(128,128,0)
#define VC_DARKGRAY    RGB(128,128,128)
#define VC_LIGHTGRAY   RGB(192,192,192)
#define VC_LIGHTGREEN  RGB(192,220,192)
#define VC_LIGHTCYAN   RGB(166,202,240)
#define VC_CREAM       RGB(255,251,240)
#define VC_MIDGRAY     RGB(160,160,164)
#define VC_DODGERBLUE  RGB(30,144,255)
#define VC_BROWN       RGB(165,42,42)

COLORREF pseudo[] = {
    
    VC_WHITE,      // 0~31
    VC_LIGHTCYAN,  // 32~63
    VC_DODGERBLUE, // 63~95
    VC_YELLOW,     // 96~127
    VC_BROWN,      // 128~159
    VC_MAGENTA,    // 160~191
    VC_RED    ,    // 192~223
    VC_BLACK,      // 224~255
};


NATIVE_API void DrawPixelValue(HDC hdc, float ZoomLevel, int panX, int panY, int clientW, int clientH, BYTE *imgBuf, int ImgBW, int ImgBH) {
    if (ZoomLevel < 15)
        return;

    int oldBkMode = SetBkMode(hdc, TRANSPARENT);
    
    int imgX1 = (int)floor(DispToImg(0, ZoomLevel, panX));
    int imgY1 = (int)floor(DispToImg(0, ZoomLevel, panY));
    int imgX2 = (int)floor(DispToImg(clientW, ZoomLevel, panX));
    int imgY2 = (int)floor(DispToImg(clientH, ZoomLevel, panY));
    if (imgX1 < 0)
        imgX1 = 0;
    if (imgY1 < 0)
        imgY1 = 0;
    if (imgX2 >= ImgBW)
        imgX2 = ImgBW - 1;
    if (imgY2 >= ImgBH)
        imgY2 = ImgBH - 1;

    char str[256];

    for (int imgY = imgY1; imgY <= imgY2; imgY++) {
        for (int imgX = imgX1; imgX <= imgX2; imgX++) {
            int dispX = ImgToDisp(imgX, ZoomLevel, panX);
            int dispY = ImgToDisp(imgY, ZoomLevel, panY);
            int pixelVal = imgBuf[(long)ImgBW * imgY + imgX];
            _itoa_s(pixelVal, str, 10);
            COLORREF oldColor = SetTextColor(hdc, pseudo[pixelVal/32]);
            TextOut(hdc, dispX, dispY, str, strlen(str));
            SetTextColor(hdc, oldColor);
        }
    }

    SetBkMode(hdc, oldBkMode);
}