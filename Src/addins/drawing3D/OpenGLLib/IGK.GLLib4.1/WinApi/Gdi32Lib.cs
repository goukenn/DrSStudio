

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Gdi32Lib.cs
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
file:Gdi32Lib.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.GLLib
{
        [StructLayout(LayoutKind.Sequential)]
        public struct LOGFONT
        {
            public long lfHeight;
            public long lfWidth;
            public long lfEscapement;
            public long lfOrientation;
            public long lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.LPStr, SizeConst = Gdi32Lib.LF_FACESIZE)]
            public string lfFaceName;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct POINTFLOAT
        {
            internal float x;
            internal float y;
        } ;
        [StructLayout(LayoutKind.Sequential)]
        public struct GLYPHMETRICSFLOAT
        {
            internal float gmfBlackBoxX;
            internal float gmfBlackBoxY;
            internal POINTFLOAT gmfptGlyphOrigin;
            internal float gmfCellIncX;
            internal float gmfCellIncY;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct LAYERPLANEDESCRIPTOR
        { // lpd
            short nSize;
            byte nVersion;
            int dwFlags;
            byte iPixelType;
            byte cColorBits;
            byte cRedBits;
            byte cRedShift;
            byte cGreenBits;
            byte cGreenShift;
            byte cBlueBits;
            byte cBlueShift;
            byte cAlphaBits;
            byte cAlphaShift;
            byte cAccumBits;
            byte cAccumRedBits;
            byte cAccumGreenBits;
            byte cAccumBlueBits;
            byte cAccumAlphaBits;
            byte cDepthBits;
            byte cStencilBits;
            byte cAuxBuffers;
            byte iLayerPlane;
            byte bReserved;
            System.Drawing.Color crTransparent;
        }
        //#region /*----------- Elements Gdi 32 nécessaire----*/
        [StructLayout(LayoutKind.Sequential)]
        public struct STpixelFormatDescriptor
        {
            public ushort nSize;
            public ushort nVersion;
            public uint dwFlags;
            public byte iPixelType;
            public byte cColorBits;
            public byte cRedBits;
            public byte cRedShift;
            public byte cGreenBits;
            public byte cGreenShift;
            public byte cBlueBits;
            public byte cBlueShift;
            public byte cAlphaBits;
            public byte cAlphaShift;
            public byte cAccumBits;
            public byte cAccumRedBits;
            public byte cAccumGreenBits;
            public byte cAccumBlueBits;
            public byte cAccumAlphaBits;
            public byte cDepthBits;
            public byte cStencilBits;
            public byte cAuxBuffers;
            public byte iLayerType;
            public byte bReserved;
            public int dwLayerMask;
            public int dwVisibleMask;
            public int dwDamageMask;
        }
        //#endregion
    public static class Gdi32Lib
    {
        internal const int LF_FACESIZE = 32;
        /*initialisation du context*/
        public const uint WGL_FONT_LINES = 0;
        public const uint WGL_FONT_POLYGONS = 1;
        /* LAYERPLANEDESCRIPTOR flags */
        internal const int LPD_DOUBLEBUFFER       = 0x00000001;
        internal const int LPD_STEREO             = 0x00000002;
        internal const int LPD_SUPPORT_GDI        = 0x00000010;
        internal const int LPD_SUPPORT_OPENGL     = 0x00000020;
        internal const int LPD_SHARE_DEPTH        = 0x00000040;
        internal const int LPD_SHARE_STENCIL      = 0x00000080;
        internal const int LPD_SHARE_ACCUM        =0x00000100;
        internal const int LPD_SWAP_EXCHANGE      = 0x00000200;
        internal const int LPD_SWAP_COPY          = 0x00000400;
        internal const int LPD_TRANSPARENT = 0x00001000;
        internal const int LPD_TYPE_RGBA        =0;
        internal const int LPD_TYPE_COLORINDEX  =1;
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFont(
        int nHeight,	// logical height of font 
        int nWidth,	// logical average character width 
        int nEscapement,	// angle of escapement 
        int nOrientation,	// base-line orientation angle 
        int fnWeight,	// font weight 
        short fdwItalic,	// italic attribute flag 
        short fdwUnderline,	// underline attribute flag 
        short fdwStrikeOut,	// strikeout attribute flag 
        short fdwCharSet,	// character set identifier 
        short fdwOutputPrecision,	// output precision 
        short fdwClipPrecision,	// clipping precision 
        short fdwQuality,	// output quality 
        short fdwPitchAndFamily,	// pitch and family 
        string lpszFace 	// address of typeface name string 
       );
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateFontIndirect(ref LOGFONT ft);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(
               IntPtr hdc,	// handle of device context 
               IntPtr hgdiobj 	// handle of object  
       );
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(
               IntPtr hgdiobj 	// handle of object  
       );
        /* Stock Logical Objects */
        public const int WHITE_BRUSH = 0;
public const int  LTGRAY_BRUSH        =1;
public const int  GRAY_BRUSH          =2;
public const int  DKGRAY_BRUSH        =3;
public const int  BLACK_BRUSH         =4;
public const int  NULL_BRUSH          =5;
public const int  HOLLOW_BRUSH        =NULL_BRUSH;
public const int  WHITE_PEN           =6;
public const int  BLACK_PEN           =7;
public const int  NULL_PEN            =8;
public const int  OEM_FIXED_FONT      =10;
public const int  ANSI_FIXED_FONT     =11;
public const int  ANSI_VAR_FONT       =12;
public const int  SYSTEM_FONT         =13;
public const int  DEVICE_DEFAULT_FONT =14;
public const int  DEFAULT_PALETTE     =15;
public const int  SYSTEM_FIXED_FONT   =16;
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern IntPtr GetStockObject(int nIndex);
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SwapBuffers(IntPtr hdc);
        /*Attribute recupère les pixel */
        [DllImport("gdi32.Dll", SetLastError = true)]
        public static extern int ChoosePixelFormat(IntPtr hdc, IntPtr pxDescriptor);
        [DllImport("gdi32.Dll", SetLastError = true)]
        public static extern bool SetPixelFormat(IntPtr hdc, int pixelFlag, IntPtr pixelFlags);
        [DllImport("gdi32.Dll", SetLastError = true)]
        internal static extern IntPtr CreateFontIndirect(IntPtr lfont);
		[DllImport("gdi32.Dll", SetLastError = true)]
        public static extern int GetPixelFormat(IntPtr hdc );
		[DllImport("gdi32.Dll", SetLastError = true)]
		public static extern int DescribePixelFormat(IntPtr hdc, int index,
            uint size,
            ref STpixelFormatDescriptor format);
        [DllImport("gdi32.Dll", SetLastError = true)]
        public static extern int DescribePixelFormat(IntPtr hdc, int index,
            uint size,
            IntPtr format);
    }
}

