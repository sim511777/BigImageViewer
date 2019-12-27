﻿// Native.cpp : DLL 응용 프로그램을 위해 내보낸 함수를 정의합니다.
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

    // 정보 헤더
    BITMAPINFOHEADER ih;
    ReadFile(hFile, &ih, sizeof(BITMAPINFOHEADER), &bytesRead, NULL);
    if (ih.biBitCount != 8) {   // 컬러비트 체크
        CloseHandle(hFile);
        return  FALSE;
    }

    LARGE_INTEGER largeSize;
    largeSize.QuadPart = fh.bfOffBits;
    SetFilePointerEx(hFile, largeSize, NULL, FILE_BEGIN);

    int fbw = ih.biWidth;
    int fbh = ih.biHeight;

    // bmp파일은 파일 저장시 라인당 4byte padding을 한다.
    // bw가 4로 나눠 떨어지지 않을경우 padding처리 해야 함
    // int stride = (bw+3)/4*4;
    int fstep = (fbw + 3) / 4 * 4;
    
    BYTE *fbuf = new BYTE[fbh * fstep];
    ReadFile(hFile, fbuf, fbh * fstep, &bytesRead, NULL);

    // 대상버퍼 width/height 소스버퍼 width/height 중 작은거 만큼 카피
    int minh = min(bh, fbh);
    int minw = min(bw, fbw);
    
    // bmp파일은 위아래가 뒤집혀 있으므로 파일에서 아래 라인부터 읽어서 버퍼에 쓴다
    for (int y = 0; y < minh; y++) {
        memcpy(buf + y * bw, fbuf + (fbh-y-1) * fstep, minw);
    }

    delete[] fbuf;
    
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
    int fstep = (bw + 3) / 4 * 4;
    int paddingSize = fstep - bw;
    BYTE paddingBuf[] = {0,0,0,0};

    // bmp파일은 위아래가 뒤집혀 있으므로 버퍼 아래라인 부터 읽어서 파일에 쓴다
    for (int y = bh - 1; y >= 0; y--) {
        WriteFile(hFile, buf + y * bw, bw, &bytesWritten, NULL);
        if (paddingSize > 0)
            WriteFile(hFile, paddingBuf, paddingSize, &bytesWritten, NULL);
    }

    CloseHandle(hFile);
    return TRUE;
}

NATIVE_API void CopyImageBufferZoom(BYTE *sbuf, int sbw, int sbh, BYTE *dbuf, int dbw, int dbh, int panx, int pany, float zoom, BOOL clear, int clearColor, BOOL clearFull) {
    // dst 인덱스의 범위를 구함
    int y1 = min(max(pany, 0), dbh);
    int y2 = max(min((int)floor(sbh * zoom + pany), dbh), 0);
    int x1 = min(max(panx, 0), dbw);
    int x2 = max(min((int)floor(sbw * zoom + panx), dbw), 0);

    // 디스플레이 영역 클리어
    if (clear) {
        if (clearFull) {
            memset(dbuf, clearColor, (size_t)dbw * dbh);
        } else {
            BYTE clearColorByte = (BYTE)clearColor;
            for (int y = 0; y < y1; y++) {
                BYTE* dp = (BYTE*)dbuf + (long)dbw * y;
                for (int x = 0; x < dbw; x++, dp++) {
                    *dp = clearColorByte;
                }
            }
            for (int y = y1; y < y2; y++) {
                BYTE* dp = (BYTE*)dbuf + (long)dbw * y;
                for (int x = 0; x < x1; x++, dp++) {
                    *dp = clearColorByte;
                }
                dp = (BYTE*)dbuf + (long)dbw * y + x2;
                for (int x = x2; x < dbw; x++, dp++) {
                    *dp = clearColorByte;
                }
            }
            for (int y = y2; y < dbh; y++) {
                BYTE* dp = (BYTE*)dbuf + (long)dbw * y;
                for (int x = 0; x < dbw; x++, dp++) {
                    *dp = clearColorByte;
                }
            }
        }
    }

    // dst 인덱스에 해당하는 src 인덱스를 담을 버퍼 생성
    int *siys = new int[dbh];
    int *sixs = new int[dbw];

    // 버퍼에 값 구해서 넣음
    for (int y = y1; y < y2;  y++) {
        siys[y] = (int)floor((y - pany) / zoom);
    }
    for (int x = x1; x < x2; x++) {
        sixs[x] = (int)floor((x - panx) / zoom);
    }

    // dst 인덱스의 범위만큼 루프를 돌면서 해당 픽셀값 쓰기
    for (int y = y1; y < y2; y++) {
        BYTE *dp = dbuf + (size_t)dbw * y + x1;

        int siy = siys[y];
        BYTE *sp = sbuf + (size_t)sbw * siy;
        for (int x = x1; x < x2; x++, dp++) {
            int six = sixs[x];
            *dp = *(sp + six);
        }
    }

    // 버퍼 삭제
    delete[] sixs;
    delete[] siys;
}
