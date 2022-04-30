

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AviCap32.dll.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AviCap32.dll.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices ;


namespace IGK.AVICaptureApi.Native
{
    /// <summary>
    /// represent a avic cap 32 native implementation
    /// </summary>
    internal static class AviCap32
    {
#if DLL_32
        const string DLLName = "VFW32.dll"
#else
        const string DLLName = "MSVFW32.dll";
#endif
        // ------------------------------------------------------------------
        //  Window Messages  WM_CAP... which can be sent to an AVICAP window
        // ------------------------------------------------------------------
// UNICODE
//
// The Win32 version of AVICAP on NT supports UNICODE applications:
// for each API or message that takes a char or string parameter, there are
// two versions, ApiNameA and ApiNameW. The default name ApiName is internal const int d
// to one or other depending on whether UNICODE is defined. Apps can call
// the A and W apis directly, and mix them.
//
// The 32-bit AVICAP on NT uses unicode exclusively internally.
// ApiNameA() will be implemented as a call to ApiNameW() together with
// translation of strings.
        internal const int WM_USER = 0x400;
// Defines start of the message range
internal const int  WM_CAP_START                    = 0x400;//WM_USER
// start of unicode messages
internal const int  WM_CAP_UNICODE_START =           WM_USER+100;
internal const int  WM_CAP_GET_CAPSTREAMPTR=         (WM_CAP_START+  1);
internal const int  WM_CAP_SET_CALLBACK_ERRORW    = (WM_CAP_UNICODE_START+  2);
internal const int  WM_CAP_SET_CALLBACK_STATUSW    =(WM_CAP_UNICODE_START+  3);
internal const int  WM_CAP_SET_CALLBACK_ERRORA     =(WM_CAP_START+  2);
internal const int  WM_CAP_SET_CALLBACK_STATUSA    =(WM_CAP_START+  3);
//#ifdef UNICODE;
internal const int  WM_CAP_SET_CALLBACK_ERROR       =WM_CAP_SET_CALLBACK_ERRORW;
internal const int  WM_CAP_SET_CALLBACK_STATUS      =WM_CAP_SET_CALLBACK_STATUSW;
//#else
//internal const int  WM_CAP_SET_CALLBACK_ERROR       =WM_CAP_SET_CALLBACK_ERRORA;
//internal const int  WM_CAP_SET_CALLBACK_STATUS      =WM_CAP_SET_CALLBACK_STATUSA;
//#endif
internal const int  WM_CAP_SET_CALLBACK_YIELD  =     (WM_CAP_START+  4);
internal const int  WM_CAP_SET_CALLBACK_FRAME   =    (WM_CAP_START+  5);
internal const int  WM_CAP_SET_CALLBACK_VIDEOSTREAM =(WM_CAP_START+  6);
internal const int  WM_CAP_SET_CALLBACK_WAVESTREAM = (WM_CAP_START+  7);
internal const int  WM_CAP_GET_USER_DATA		=(WM_CAP_START+  8);
internal const int  WM_CAP_SET_USER_DATA		=(WM_CAP_START+  9);
internal const int  WM_CAP_DRIVER_CONNECT    =       (WM_CAP_START+  10);
internal const int  WM_CAP_DRIVER_DISCONNECT  =      (WM_CAP_START+  11);
internal const int  WM_CAP_DRIVER_GET_NAME    =    (WM_CAP_START+  12);
internal const int  WM_CAP_DRIVER_GET_VERSION  =   (WM_CAP_START+  13);
//internal const int  WM_CAP_DRIVER_GET_NAMEW      =  (WM_CAP_UNICODE_START+  12);
//internal const int  WM_CAP_DRIVER_GET_VERSIONW    = (WM_CAP_UNICODE_START+  13);
////#ifdef UNICODE
//internal const int  WM_CAP_DRIVER_GET_NAME         = WM_CAP_DRIVER_GET_NAMEW;
//internal const int  WM_CAP_DRIVER_GET_VERSION      = WM_CAP_DRIVER_GET_VERSIONW;
////#else
//internal const int  WM_CAP_DRIVER_GET_NAME        =  WM_CAP_DRIVER_GET_NAMEA;
//internal const int  WM_CAP_DRIVER_GET_VERSION     =  WM_CAP_DRIVER_GET_VERSIONA;
////#endif
internal const int  WM_CAP_DRIVER_GET_CAPS         = (WM_CAP_START+  14);
internal const int  WM_CAP_FILE_SET_CAPTURE_FILEA  =(WM_CAP_START+  20);
internal const int  WM_CAP_FILE_GET_CAPTURE_FILEA  =(WM_CAP_START+  21);
internal const int  WM_CAP_FILE_SAVEASA            =(WM_CAP_START+  23);
internal const int  WM_CAP_FILE_SAVEDIBA           =(WM_CAP_START+  25);
internal const int  WM_CAP_FILE_SET_CAPTURE_FILEW  =(WM_CAP_UNICODE_START+  20);
internal const int  WM_CAP_FILE_GET_CAPTURE_FILEW  =(WM_CAP_UNICODE_START+  21);
internal const int  WM_CAP_FILE_SAVEASW            =(WM_CAP_UNICODE_START+  23);
internal const int  WM_CAP_FILE_SAVEDIBW           =(WM_CAP_UNICODE_START+  25);
//#ifdef UNICODE
internal const int  WM_CAP_FILE_SET_CAPTURE_FILE    =WM_CAP_FILE_SET_CAPTURE_FILEW;
internal const int  WM_CAP_FILE_GET_CAPTURE_FILE    =WM_CAP_FILE_GET_CAPTURE_FILEW;
internal const int  WM_CAP_FILE_SAVEAS              =WM_CAP_FILE_SAVEASW;
internal const int  WM_CAP_FILE_SAVEDIB             =WM_CAP_FILE_SAVEDIBW;
//#else
//internal const int  WM_CAP_FILE_SET_CAPTURE_FILE    =WM_CAP_FILE_SET_CAPTURE_FILEA;
//internal const int  WM_CAP_FILE_GET_CAPTURE_FILE    =WM_CAP_FILE_GET_CAPTURE_FILEA;
//internal const int  WM_CAP_FILE_SAVEAS              =WM_CAP_FILE_SAVEASA;
//internal const int  WM_CAP_FILE_SAVEDIB             =WM_CAP_FILE_SAVEDIBA;
//#endif
// out of order to save on ifdefs
internal const int  WM_CAP_FILE_ALLOCATE            =(WM_CAP_START+  22);
internal const int  WM_CAP_FILE_SET_INFOCHUNK       =(WM_CAP_START+  24);
internal const int  WM_CAP_EDIT_COPY               = (WM_CAP_START+  30);
internal const int  WM_CAP_SET_AUDIOFORMAT         = (WM_CAP_START+  35);
internal const int  WM_CAP_GET_AUDIOFORMAT         = (WM_CAP_START+  36);
internal const int  WM_CAP_DLG_VIDEOFORMAT         = (WM_CAP_START+  41);
internal const int  WM_CAP_DLG_VIDEOSOURCE         = (WM_CAP_START+  42);
internal const int  WM_CAP_DLG_VIDEODISPLAY        = (WM_CAP_START+  43);
internal const int  WM_CAP_GET_VIDEOFORMAT         = (WM_CAP_START+  44);
internal const int  WM_CAP_SET_VIDEOFORMAT         = (WM_CAP_START+  45);
internal const int  WM_CAP_DLG_VIDEOCOMPRESSION    = (WM_CAP_START+  46);
internal const int  WM_CAP_SET_PREVIEW              =(WM_CAP_START+  50);
internal const int  WM_CAP_SET_OVERLAY              =(WM_CAP_START+  51);
internal const int  WM_CAP_SET_PREVIEWRATE          =(WM_CAP_START+  52);
internal const int  WM_CAP_SET_SCALE                =(WM_CAP_START+  53);
internal const int  WM_CAP_GET_STATUS               =(WM_CAP_START+  54);
internal const int  WM_CAP_SET_SCROLL               =(WM_CAP_START+  55);
internal const int  WM_CAP_GRAB_FRAME               =(WM_CAP_START+  60);
internal const int  WM_CAP_GRAB_FRAME_NOSTOP        =(WM_CAP_START+  61);
internal const int  WM_CAP_SEQUENCE               =  (WM_CAP_START+  62);
internal const int  WM_CAP_SEQUENCE_NOFILE        =  (WM_CAP_START+  63);
internal const int  WM_CAP_SET_SEQUENCE_SETUP     =  (WM_CAP_START+  64);
internal const int  WM_CAP_GET_SEQUENCE_SETUP     =  (WM_CAP_START+  65);
internal const int  WM_CAP_SET_MCI_DEVICEA         =(WM_CAP_START+  66);
internal const int  WM_CAP_GET_MCI_DEVICEA         =(WM_CAP_START+  67);
internal const int  WM_CAP_SET_MCI_DEVICEW         =(WM_CAP_UNICODE_START+  66);
internal const int  WM_CAP_GET_MCI_DEVICEW         =(WM_CAP_UNICODE_START+  67);
//#ifdef UNICODE
internal const int  WM_CAP_SET_MCI_DEVICE          = WM_CAP_SET_MCI_DEVICEW;
internal const int  WM_CAP_GET_MCI_DEVICE          = WM_CAP_GET_MCI_DEVICEW;
//#else
//internal const int  WM_CAP_SET_MCI_DEVICE          = WM_CAP_SET_MCI_DEVICEA;
//internal const int  WM_CAP_GET_MCI_DEVICE          = WM_CAP_GET_MCI_DEVICEA;
//#endif
internal const int  WM_CAP_STOP                =     (WM_CAP_START+  68);
internal const int  WM_CAP_ABORT               =     (WM_CAP_START+  69);
internal const int  WM_CAP_SINGLE_FRAME_OPEN   =     (WM_CAP_START+  70);
internal const int  WM_CAP_SINGLE_FRAME_CLOSE  =     (WM_CAP_START+  71);
internal const int  WM_CAP_SINGLE_FRAME        =     (WM_CAP_START+  72);
internal const int  WM_CAP_PAL_OPEN           =    (WM_CAP_START+  80);
internal const int  WM_CAP_PAL_SAVE           =    (WM_CAP_START+  81);
//internal const int  WM_CAP_PAL_OPENW           =    (WM_CAP_UNICODE_START+  80);
//internal const int  WM_CAP_PAL_SAVEW           =    (WM_CAP_UNICODE_START+  81);
////#ifdef UNICODE
//internal const int  WM_CAP_PAL_OPEN                 =WM_CAP_PAL_OPENW;
//internal const int  WM_CAP_PAL_SAVE                 =WM_CAP_PAL_SAVEW;
////#else
//internal const int  WM_CAP_PAL_OPEN                 =WM_CAP_PAL_OPENA;
//internal const int  WM_CAP_PAL_SAVE                 =WM_CAP_PAL_SAVEA;
////#endif
internal const int  WM_CAP_PAL_PASTE                =(WM_CAP_START+  82);
internal const int  WM_CAP_PAL_AUTOCREATE           =(WM_CAP_START+  83);
internal const int  WM_CAP_PAL_MANUALCREATE         =(WM_CAP_START+  84);
// Following added post VFW 1.1
internal const int  WM_CAP_SET_CALLBACK_CAPCONTROL  =(WM_CAP_START+  85);
// Defines end of the message range
internal const int  WM_CAP_UNICODE_END              =WM_CAP_PAL_SAVE;
internal const int  WM_CAP_END                      =WM_CAP_UNICODE_END;
// ------------------------------------------------------------------
//  Message crackers for above
// ------------------------------------------------------------------
// message wrapper macros are defined for the default messages only. Apps
// that wish to mix Ansi and UNICODE message sending will have to
// reference the _A and _W messages directly
//delegate   capSetCallbackOnError(hwnd, fpProc)        ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_ERROR, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnStatus(hwnd, fpProc)       ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_STATUS, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnYield(hwnd, fpProc)        ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_YIELD, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnFrame(hwnd, fpProc)        ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_FRAME, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnVideoStream(hwnd, fpProc)  ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_VIDEOSTREAM, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnWaveStream(hwnd, fpProc)   ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_WAVESTREAM, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetCallbackOnCapControl(hwnd, fpProc)   ((bool)AVICapSM(hwnd, WM_CAP_SET_CALLBACK_CAPCONTROL, 0, (LPARAM)(LPVOID)(fpProc)))
//internal const int  capSetUserData(IntPtr hwnd,IntPr  lUser)        ((bool)AVICapSM(hwnd, WM_CAP_SET_USER_DATA, 0, (LPARAM)lUser))
//internal const int  capGetUserData(hwnd)               (AVICapSM(hwnd, WM_CAP_GET_USER_DATA, 0, 0))
//internal const int  capDriverConnect(hwnd, i)                  ((bool)AVICapSM(hwnd, WM_CAP_DRIVER_CONNECT, (WPARAM)(i), 0L))
//internal const int  capDriverDisconnect(hwnd)                  ((bool)AVICapSM(hwnd, WM_CAP_DRIVER_DISCONNECT, (WPARAM)0, 0L))
//internal const int  capDriverGetName(hwnd, szName, wSize)      ((bool)AVICapSM(hwnd, WM_CAP_DRIVER_GET_NAME, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capDriverGetVersion(hwnd, szVer, wSize)    ((bool)AVICapSM(hwnd, WM_CAP_DRIVER_GET_VERSION, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPTSTR)(szVer)))
//internal const int  capDriverGetCaps(hwnd, s, wSize)           ((bool)AVICapSM(hwnd, WM_CAP_DRIVER_GET_CAPS, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPCAPDRIVERCAPS)(s)))
//internal const int  capFileSetCaptureFile(hwnd, szName)        ((bool)AVICapSM(hwnd, WM_CAP_FILE_SET_CAPTURE_FILE, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capFileGetCaptureFile(hwnd, szName, wSize) ((bool)AVICapSM(hwnd, WM_CAP_FILE_GET_CAPTURE_FILE, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capFileAlloc(hwnd, dwSize)                 ((bool)AVICapSM(hwnd, WM_CAP_FILE_ALLOCATE, 0, (LPARAM)(Int32)(dwSize)))
//internal const int  capFileSaveAs(hwnd, szName)                ((bool)AVICapSM(hwnd, WM_CAP_FILE_SAVEAS, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capFileSetInfoChunk(hwnd, lpInfoChunk)     ((bool)AVICapSM(hwnd, WM_CAP_FILE_SET_INFOCHUNK, (WPARAM)0, (LPARAM)(LPCAPINFOCHUNK)(lpInfoChunk)))
//internal const int  capFileSaveDIB(hwnd, szName)               ((bool)AVICapSM(hwnd, WM_CAP_FILE_SAVEDIB, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capEditCopy(hwnd)                          ((bool)AVICapSM(hwnd, WM_CAP_EDIT_COPY, 0, 0L))
//internal const int  capSetAudioFormat(hwnd, s, wSize)          ((bool)AVICapSM(hwnd, WM_CAP_SET_AUDIOFORMAT, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPWAVEFORMATEX)(s)))
//internal const int  capGetAudioFormat(hwnd, s, wSize)          ((Int32)AVICapSM(hwnd, WM_CAP_GET_AUDIOFORMAT, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPWAVEFORMATEX)(s)))
//internal const int  capGetAudioFormatSize(hwnd)                ((Int32)AVICapSM(hwnd, WM_CAP_GET_AUDIOFORMAT, (WPARAM)0, (LPARAM)0L))
//internal const int  capDlgVideoFormat(hwnd)                    ((bool)AVICapSM(hwnd, WM_CAP_DLG_VIDEOFORMAT, 0, 0L))
//internal const int  capDlgVideoSource(hwnd)                    ((bool)AVICapSM(hwnd, WM_CAP_DLG_VIDEOSOURCE, 0, 0L))
//internal const int  capDlgVideoDisplay(hwnd)                   ((bool)AVICapSM(hwnd, WM_CAP_DLG_VIDEODISPLAY, 0, 0L))
//internal const int  capDlgVideoCompression(hwnd)               ((bool)AVICapSM(hwnd, WM_CAP_DLG_VIDEOCOMPRESSION, 0, 0L))
//internal const int  capGetVideoFormat(hwnd, s, wSize)          ((Int32)AVICapSM(hwnd, WM_CAP_GET_VIDEOFORMAT, (WPARAM)(wSize), (LPARAM)(LPVOID)(s)))
//internal const int  capGetVideoFormatSize(hwnd)            ((Int32)AVICapSM(hwnd, WM_CAP_GET_VIDEOFORMAT, 0, 0L))
//internal const int  capSetVideoFormat(hwnd, s, wSize)          ((bool)AVICapSM(hwnd, WM_CAP_SET_VIDEOFORMAT, (WPARAM)(wSize), (LPARAM)(LPVOID)(s)))
//internal const int  capPreview(hwnd, f)                        ((bool)AVICapSM(hwnd, WM_CAP_SET_PREVIEW, (WPARAM)(bool)(f), 0L))
//internal const int  capPreviewRate(hwnd, wMS)                  ((bool)AVICapSM(hwnd, WM_CAP_SET_PREVIEWRATE, (WPARAM)(wMS), 0))
//internal const int  capOverlay(hwnd, f)                        ((bool)AVICapSM(hwnd, WM_CAP_SET_OVERLAY, (WPARAM)(bool)(f), 0L))
//internal const int  capPreviewScale(hwnd, f)                   ((bool)AVICapSM(hwnd, WM_CAP_SET_SCALE, (WPARAM)(bool)f, 0L))
//internal const int  capGetStatus(hwnd, s, wSize)               ((bool)AVICapSM(hwnd, WM_CAP_GET_STATUS, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPCAPSTATUS)(s)))
//internal const int  capSetScrollPos(hwnd, lpP)                 ((bool)AVICapSM(hwnd, WM_CAP_SET_SCROLL, (WPARAM)0, (LPARAM)(LPPOINT)(lpP)))
//internal const int  capGrabFrame(hwnd)                         ((bool)AVICapSM(hwnd, WM_CAP_GRAB_FRAME, (WPARAM)0, (LPARAM)0L))
//internal const int  capGrabFrameNoStop(hwnd)                   ((bool)AVICapSM(hwnd, WM_CAP_GRAB_FRAME_NOSTOP, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureSequence(hwnd)                   ((bool)AVICapSM(hwnd, WM_CAP_SEQUENCE, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureSequenceNoFile(hwnd)             ((bool)AVICapSM(hwnd, WM_CAP_SEQUENCE_NOFILE, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureStop(hwnd)                       ((bool)AVICapSM(hwnd, WM_CAP_STOP, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureAbort(hwnd)                      ((bool)AVICapSM(hwnd, WM_CAP_ABORT, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureSingleFrameOpen(hwnd)            ((bool)AVICapSM(hwnd, WM_CAP_SINGLE_FRAME_OPEN, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureSingleFrameClose(hwnd)           ((bool)AVICapSM(hwnd, WM_CAP_SINGLE_FRAME_CLOSE, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureSingleFrame(hwnd)                ((bool)AVICapSM(hwnd, WM_CAP_SINGLE_FRAME, (WPARAM)0, (LPARAM)0L))
//internal const int  capCaptureGetSetup(hwnd, s, wSize)         ((bool)AVICapSM(hwnd, WM_CAP_GET_SEQUENCE_SETUP, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPCAPTUREPARMS)(s)))
//internal const int  capCaptureSetSetup(hwnd, s, wSize)         ((bool)AVICapSM(hwnd, WM_CAP_SET_SEQUENCE_SETUP, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPCAPTUREPARMS)(s)))
//internal const int  capSetMCIDeviceName(hwnd, szName)          ((bool)AVICapSM(hwnd, WM_CAP_SET_MCI_DEVICE, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capGetMCIDeviceName(hwnd, szName, wSize)   ((bool)AVICapSM(hwnd, WM_CAP_GET_MCI_DEVICE, (WPARAM)(wSize), (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capPaletteOpen(hwnd, szName)               ((bool)AVICapSM(hwnd, WM_CAP_PAL_OPEN, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capPaletteSave(hwnd, szName)               ((bool)AVICapSM(hwnd, WM_CAP_PAL_SAVE, 0, (LPARAM)(LPVOID)(LPTSTR)(szName)))
//internal const int  capPalettePaste(hwnd)                      ((bool)AVICapSM(hwnd, WM_CAP_PAL_PASTE, (WPARAM) 0, (LPARAM)0L))
//internal const int  capPaletteAuto(hwnd, iFrames, iColors)     ((bool)AVICapSM(hwnd, WM_CAP_PAL_AUTOCREATE, (WPARAM)(iFrames), (LPARAM)(Int32)(iColors)))
//internal const int  capPaletteManual(hwnd, fGrab, iColors)     ((bool)AVICapSM(hwnd, WM_CAP_PAL_MANUALCREATE, (WPARAM)(fGrab), (LPARAM)(Int32)(iColors)))
// ------------------------------------------------------------------
//  Structures
// ------------------------------------------------------------------
        [StructLayout (LayoutKind.Sequential,  Pack=1)]
 internal struct CAPDRIVERCAPS {
    uint        wDeviceIndex;               // Driver index in system.ini
    bool        fHasOverlay;                // Can device overlay?
    bool        fHasDlgVideoSource;         // Has Video source dlg?
    bool        fHasDlgVideoFormat;         // Has Format dlg?
    bool        fHasDlgVideoDisplay;        // Has External out dlg?
    bool        fCaptureInitialized;        // Driver ready to capture?
    bool        fDriverSuppliesPalettes;    // Can driver make palettes?
// following always NULL on Win32.
    IntPtr      hVideoIn;                   // Driver In channel
    IntPtr      hVideoOut;                  // Driver Out channel
    IntPtr      hVideoExtIn;                // Driver Ext In channel
    IntPtr      hVideoExtOut;               // Driver Ext Out channel
} //CAPDRIVERCAPS, *PCAPDRIVERCAPS, FAR *LPCAPDRIVERCAPS;
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
 internal struct tagCapStatus {
    uint        uiImageWidth;               // Width of the image
    uint        uiImageHeight;              // Height of the image
    bool        fLiveWindow;                // Now Previewing video?
    bool        fOverlayWindow;             // Now Overlaying video?
    bool        fScale;                     // Scale image to client?
    POINT       ptScroll;                   // Scroll position
    bool        fUsingDefaultPalette;       // Using default driver palette?
    bool        fAudioHardware;             // Audio hardware present?
    bool        fCapFileExists;             // Does capture file exist?
    Int32       dwCurrentVideoFrame;        // # of video frames cap'td
    Int32       dwCurrentVideoFramesDropped;// # of video frames dropped
    Int32       dwCurrentWaveSamples;       // # of wave samples cap'td
    Int32       dwCurrentTimeElapsedMS;     // Elapsed capture duration
    IntPtr /*HPALETTE*/    hPalCurrent;                // Current palette in use
    bool        fCapturingNow;              // Capture in progress?
    Int32       dwReturn;                   // Error value after any operation
    uint        wNumVideoAllocated;         // Actual number of video buffers
    uint        wNumAudioAllocated;         // Actual number of audio buffers
}// CAPSTATUS, *PCAPSTATUS, FAR *LPCAPSTATUS;
                                            // Default values in parenthesis
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
 internal struct tagCaptureParms {
   internal Int32       dwRequestMicroSecPerFrame;  // Requested capture rate
   internal bool        fMakeUserHitOKToCapture;    // Show "Hit OK to cap" dlg?
   internal uint        wPercentDropForError;       // Give error msg if > (10%)
   internal bool        fYield;                     // Capture via background task?
   internal Int32       dwIndexSize;                // Max index size in frames (32K)
   internal uint        wChunkGranularity;          // Junk chunk granularity (2K)
   internal bool        fUsingDOSMemory;            // Use DOS buffers?
   internal uint        wNumVideoRequested;         // # video buffers, If 0, autocalc
   internal bool        fCaptureAudio;              // Capture audio?
   internal uint        wNumAudioRequested;         // # audio buffers, If 0, autocalc
   internal uint        vKeyAbort;                  // Virtual key causing abort
   internal bool        fAbortLeftMouse;            // Abort on left mouse?
   internal bool        fAbortRightMouse;           // Abort on right mouse?
   internal bool        fLimitEnabled;              // Use wTimeLimit?
   internal uint        wTimeLimit;                 // Seconds to capture
   internal bool        fMCIControl;                // Use MCI video source?
   internal bool        fStepMCIDevice;             // Step MCI device?
   internal Int32       dwMCIStartTime;             // Time to start in MS
   internal Int32       dwMCIStopTime;              // Time to stop in MS
   internal bool        fStepCaptureAt2x;           // Perform spatial averaging 2x
   internal uint        wStepCaptureAverageFrames;  // Temporal average n Frames
   internal Int32       dwAudioBufferSize;          // Size of audio bufs (0 = default)
   internal bool        fDisableWriteCache;         // Attempt to disable write cache
   internal uint        AVStreamMaster;             // Which stream controls length?
} //CAPTUREPARMS, *PCAPTUREPARMS, FAR *LPCAPTUREPARMS;
// ------------------------------------------------------------------
//  AVStreamMaster
//  Since Audio and Video streams generally use non-synchronized capture
//  clocks, this flag determines whether the audio stream is to be considered
//  the master or controlling clock when writing the AVI file:
//
//  AVSTREAMMASTER_AUDIO  - Audio is master, video frame duration is forced
//                          to match audio duration (VFW 1.0, 1.1 default)
//  AVSTREAMMASTER_NONE   - No master, audio and video streams may be of
//                          different lengths
// ------------------------------------------------------------------
internal const int  AVSTREAMMASTER_AUDIO            =0; /* Audio master (VFW 1.0, 1.1) */
internal const int  AVSTREAMMASTER_NONE             =1; /* No master */
    [StructLayout (LayoutKind.Sequential , Pack = 1)]
 internal struct tagCapInfoChunk {
     internal Int32 fccInfoID;                  // Chunk ID, "ICOP" for copyright
    internal IntPtr lpData;                     // pointer to data
    internal int        cbData;                     // size of lpData
} //CAPINFOCHUNK, *PCAPINFOCHUNK, FAR *LPCAPINFOCHUNK;
// ------------------------------------------------------------------
//  Callback Definitions
// ------------------------------------------------------------------
 //IntPtr /*LRESULT*/ (CALLBACK* CAPYIELDCALLBACK)  ( IntPtr hWnd);
 //IntPtr /*LRESULT*/ (CALLBACK* CAPSTATUSCALLBACKW) ( IntPtr hWnd,  int nID, LPCWSTR lpsz);
 //IntPtr /*LRESULT*/ (CALLBACK* CAPERRORCALLBACKW)  ( IntPtr hWnd,  int nID, LPCWSTR lpsz);
 //IntPtr /*LRESULT*/ (CALLBACK* CAPSTATUSCALLBACKA) ( IntPtr hWnd,  int nID, LPCSTR lpsz);
 //IntPtr /*LRESULT*/ (CALLBACK* CAPERRORCALLBACKA)  ( IntPtr hWnd,  int nID, LPCSTR lpsz);
////#ifdef UNICODE
//internal const int  CAPSTATUSCALLBACK  =CAPSTATUSCALLBACKW;
//internal const int  CAPERRORCALLBACK   =CAPERRORCALLBACKW;
////#else
//internal const int  CAPSTATUSCALLBACK  =CAPSTATUSCALLBACKA;
//internal const int  CAPERRORCALLBACK   =CAPERRORCALLBACKA;
////#endif
// IntPtr /*LRESULT*/ (CALLBACK* CAPVIDEOCALLBACK)  ( IntPtr hWnd,  LPVIDEOHDR lpVHdr);
// IntPtr /*LRESULT*/ (CALLBACK* CAPWAVECALLBACK)   ( IntPtr hWnd,  LPWAVEHDR lpWHdr);
// IntPtr /*LRESULT*/ (CALLBACK* CAPCONTROLCALLBACK)( IntPtr hWnd,  int nState);
// ------------------------------------------------------------------
//  CapControlCallback states
// ------------------------------------------------------------------
internal const int  CONTROLCALLBACK_PREROLL         =1; /* Waiting to start capture */
internal const int  CONTROLCALLBACK_CAPTURING       =2; /* Now capturing */
// ------------------------------------------------------------------
//  The only exported functions from AVICAP.DLL
// ------------------------------------------------------------------
[DllImport ("avicap32.dll")]
public static extern IntPtr capCreateCaptureWindow (
        string lpszWindowName,
        int dwStyle,
        int x,
        int y,
        int nWidth, 
         int nHeight,
         IntPtr hwndParent,  int nID);
[DllImport ("avicap32.dll")]
public static extern bool capGetDriverDescription (uint wDriverIndex,
        IntPtr  lpszName,  int cbName,
        IntPtr lpszVer,  
        int cbVer);
public static bool capDriverDisconnect(IntPtr hwnd)
{
    if (User32.SendMessage(hwnd, WM_CAP_DRIVER_DISCONNECT, IntPtr.Zero, IntPtr.Zero) == 0)
    {
        return true;
    }
    return false;
}
        [DllImport ("avicap32.dll", EntryPoint ="capDriverConnect")]
public extern static bool capDriverConnectT(IntPtr hwnd, int id);
        /// <summary>
        /// connect to driver
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="id"></param>
        /// <returns></returns>
public static bool capDriverConnect(IntPtr hwnd, uint id)
{
    return  User32.BSendMessage(hwnd,WM_CAP_DRIVER_CONNECT, new IntPtr ((int)id), IntPtr.Zero);    
}
//#endif  /* RC_INVOKED */
public static int mmioFOURCC(char ch0, char ch1, char ch2, char ch3)
{
    return ((int)ch0 | ((int)ch1 << 8) | ((int)ch2 << 16) | ((int)ch3 << 24));
}
// ------------------------------------------------------------------
// New Information chunk IDs
// ------------------------------------------------------------------
internal static readonly int  infotypeDIGITIZATION_TIME  =mmioFOURCC ('I','D','I','T');
internal static readonly int  infotypeSMPTE_TIME         =mmioFOURCC ('I','S','M','P');
// ------------------------------------------------------------------
// String IDs from status and error callbacks
// ------------------------------------------------------------------
internal const int  IDS_CAP_BEGIN           =   300;  /* "Capture Start" */
internal const int  IDS_CAP_END                =301 ; /* "Capture End" */
internal const int  IDS_CAP_INFO           =  401 ;/* "%s" */
internal const int  IDS_CAP_OUTOFMEM       =  402 ;/* "Out of memory" */
internal const int  IDS_CAP_FILEEXISTS     =  403 ;/* "File '%s' exists -- overwrite it?" */
internal const int  IDS_CAP_ERRORPALOPEN   =  404 ;/* "Error opening palette '%s'" */
internal const int  IDS_CAP_ERRORPALSAVE   =  405 ;/* "Error saving palette '%s'" */
internal const int  IDS_CAP_ERRORDIBSAVE   =  406 ;/* "Error saving frame '%s'" */
internal const int  IDS_CAP_DEFAVIEXT      =  407 ;/* "avi" */
internal const int  IDS_CAP_DEFPALEXT      =  408 ;/* "pal" */
internal const int  IDS_CAP_CANTOPEN       =  409 ;/* "Cannot open '%s'" */
internal const int  IDS_CAP_SEQ_MSGSTART   =  410 ;/* "Select OK to start capture\nof video sequence\nto %s." */
internal const int  IDS_CAP_SEQ_MSGSTOP    =  411 ;/* "Hit ESCAPE or click to end capture" */
internal const int  IDS_CAP_VIDEDITERR         =412; /* "An error occurred while trying to run VidEdit." */
internal const int  IDS_CAP_READONLYFILE       =413; /* "The file '%s' is a read-only file." */
internal const int  IDS_CAP_WRITEERROR         =414; /* "Unable to write to file '%s'.\nDisk may be full." */
internal const int  IDS_CAP_NODISKSPACE        =415; /* "There is no space to create a capture file on the specified device." */
internal const int  IDS_CAP_SETFILESIZE        =416; /* "Set File Size" */
internal const int  IDS_CAP_SAVEASPERCENT      =417; /* "SaveAs: %2ld%%  Hit Escape to abort." */
internal const int  IDS_CAP_DRIVER_ERROR       =418; /* Driver specific error message */
internal const int  IDS_CAP_WAVE_OPEN_ERROR    =419; /* "Error: Cannot open the wave input device.\nCheck sample size, frequency, and channels." */
internal const int  IDS_CAP_WAVE_ALLOC_ERROR   =420; /* "Error: Out of memory for wave buffers." */
internal const int  IDS_CAP_WAVE_PREPARE_ERROR =421; /* "Error: Cannot prepare wave buffers." */
internal const int  IDS_CAP_WAVE_ADD_ERROR     =422; /* "Error: Cannot add wave buffers." */
internal const int  IDS_CAP_WAVE_SIZE_ERROR    =423; /* "Error: Bad wave size." */
internal const int  IDS_CAP_VIDEO_OPEN_ERROR   =424; /* "Error: Cannot open the video input device." */
internal const int  IDS_CAP_VIDEO_ALLOC_ERROR  =425; /* "Error: Out of memory for video buffers." */
internal const int  IDS_CAP_VIDEO_PREPARE_ERROR= 426 ; /* "Error: Cannot prepare video buffers." */
internal const int  IDS_CAP_VIDEO_ADD_ERROR     =427 ; /* "Error: Cannot add video buffers." */
internal const int  IDS_CAP_VIDEO_SIZE_ERROR    =428 ; /* "Error: Bad video size." */
internal const int  IDS_CAP_FILE_OPEN_ERROR     =429 ; /* "Error: Cannot open capture file." */
internal const int  IDS_CAP_FILE_WRITE_ERROR    =430 ; /* "Error: Cannot write to capture file.  Disk may be full." */
internal const int  IDS_CAP_RECORDING_ERROR     =431 ; /* "Error: Cannot write to capture file.  Data rate too high or disk full." */
internal const int  IDS_CAP_RECORDING_ERROR2    =432 ; /* "Error while recording" */
internal const int  IDS_CAP_AVI_INIT_ERROR      =433 ; /* "Error: Unable to initialize for capture." */
internal const int  IDS_CAP_NO_FRAME_CAP_ERROR  =434 ; /* "Warning: No frames captured.\nConfirm that vertical sync interrupts\nare configured and enabled." */
internal const int  IDS_CAP_NO_PALETTE_WARN     =435 ; /* "Warning: Using default palette." */
internal const int  IDS_CAP_MCI_CONTROL_ERROR   =436 ; /* "Error: Unable to access MCI device." */
internal const int  IDS_CAP_MCI_CANT_STEP_ERROR =437 ; /* "Error: Unable to step MCI device." */
internal const int  IDS_CAP_NO_AUDIO_CAP_ERROR  =438 ; /* "Error: No audio data captured.\nCheck audio card settings." */
internal const int  IDS_CAP_AVI_DRAWDIB_ERROR   =439 ; /* "Error: Unable to draw this data format." */
internal const int  IDS_CAP_COMPRESSOR_ERROR    =440 ; /* "Error: Unable to initialize compressor." */
internal const int  IDS_CAP_AUDIO_DROP_ERROR    =441 ; /* "Error: Audio data was lost during capture, reduce capture rate." */
internal const int  IDS_CAP_AUDIO_DROP_COMPERROR =442;  /* "Error: Audio data was lost during capture.  Try capturing without compressing." */
/* status string IDs */
internal const int  IDS_CAP_STAT_LIVE_MODE      =500;  /* "Live window" */
internal const int  IDS_CAP_STAT_OVERLAY_MODE   =501;  /* "Overlay window" */
internal const int  IDS_CAP_STAT_CAP_INIT       =502;  /* "Setting up for capture - Please wait" */
internal const int  IDS_CAP_STAT_CAP_FINI       =503;  /* "Finished capture, now writing frame %ld" */
internal const int  IDS_CAP_STAT_PALETTE_BUILD  =504;  /* "Building palette map" */
internal const int  IDS_CAP_STAT_OPTPAL_BUILD   =505;  /* "Computing optimal palette" */
internal const int  IDS_CAP_STAT_I_FRAMES       =506;  /* "%d frames" */
internal const int  IDS_CAP_STAT_L_FRAMES       =507;  /* "%ld frames" */
internal const int  IDS_CAP_STAT_CAP_L_FRAMES   =508;  /* "Captured %ld frames" */
internal const int  IDS_CAP_STAT_CAP_AUDIO      =509;  /* "Capturing audio" */
internal const int  IDS_CAP_STAT_VIDEOCURRENT   =510;  /* "Captured %ld frames (%ld dropped) %d.%03d sec." */
internal const int  IDS_CAP_STAT_VIDEOAUDIO     =511;  /* "Captured %d.%03d sec.  %ld frames (%ld dropped) (%d.%03d fps).  %ld audio bytes (%d,%03d sps)" */
internal const int  IDS_CAP_STAT_VIDEOONLY      =512;  /* "Captured %d.%03d sec.  %ld frames (%ld dropped) (%d.%03d fps)" */
internal const int  IDS_CAP_STAT_FRAMESDROPPED  =513;  /* "Dropped %ld of %ld frames (%d.%02d%%) during capture." */
//#endif  /* NOAVIFILE */
//#ifdef __cplusplus
} // extern "C"
//#endif  /* __cplusplus */
    }

