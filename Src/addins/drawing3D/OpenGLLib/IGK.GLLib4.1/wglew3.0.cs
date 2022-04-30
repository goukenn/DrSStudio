

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: wglew3.0.cs
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
file:wglew3.0.cs
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
/*
** The OpenGL Extension Wrangler Library
** Copyright (C) 2002-2008, Milan Ikits <milan ikits[]ieee org>
** Copyright (C) 2002-2008, Marcelo E. Magallon <mmagallo[]debian org>
** Copyright (C) 2002, Lev Povalahev
** All rights reserved.
** 
** Redistribution and use in source and binary forms, with or without 
** modification, are permitted provided that the following conditions are met:
** 
** * Redistributions of source code must retain the above copyright notice, 
**   this list of conditions and the following disclaimer.
** * Redistributions in binary form must reproduce the above copyright notice, 
**   this list of conditions and the following disclaimer in the documentation 
**   and/or other materials provided with the distribution.
** * The name of the author may be used to endorse or promote products 
**   derived from this software without specific prior written permission.
**
** THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" 
** AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE 
** IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
** ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE 
** LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
** CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF 
** SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
** INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
** CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
** ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF
** THE POSSIBILITY OF SUCH DAMAGE.
*/
/*
 * Mesa 3-D graphics library
 * Version:  7.0
 *
 * Copyright (C) 1999-2007  Brian Paul   All Rights Reserved.
 *
 * Permission is hereby granted, free of charge, to any person obtaining a
 * copy of this software and associated documentation files (the "Software"),
 * to deal in the Software without restriction, including without limitation
 * the rights to use, copy, modify, merge, publish, distribute, sublicense,
 * and/or sell copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included
 * in all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS
 * OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.  IN NO EVENT SHALL
 * BRIAN PAUL BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN
 * AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
 * CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */
/*
** Copyright (c) 2007 The Khronos Group Inc.
** 
** Permission is hereby granted, free of charge, to any person obtaining a
** copy of this software and/or associated documentation files (the
** "Materials"), to deal in the Materials without restriction, including
** without limitation the rights to use, copy, modify, merge, publish,
** distribute, sublicense, and/or sell copies of the Materials, and to
** permit persons to whom the Materials are furnished to do so, subject to
** the following conditions:
** 
** The above copyright notice and this permission notice shall be included
** in all copies or substantial portions of the Materials.
** 
** THE MATERIALS ARE PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
** EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
** MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
** IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY
** CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
** TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE
** MATERIALS OR THE USE OR OTHER DEALINGS IN THE MATERIALS.
*/
/*
 * Copyright (C) 2008 - 2009 - Bondje doue charles wglew3.0.cs automatique g�n�ration file
 * All Rigths reserved
 * */
#pragma warning disable
using System;
using System.Runtime.InteropServices;
namespace IGK.GLLib
{public partial class WGL {
/*
/*
/*
/* -------------------------- WGL_3DFX_multisample ------------------------- */
public const uint WGL_3DFX_multisample = 0x1;
public const uint WGL_SAMPLE_BUFFERS_3DFX = 0x2060;
public const uint WGL_SAMPLES_3DFX = 0x2061;
public static readonly bool isWGL_3DFX_multisample;
/* ------------------------- WGL_3DL_stereo_control ------------------------ */
public const uint WGL_3DL_stereo_control = 0x1;
public const uint WGL_STEREO_EMITTER_ENABLE_3DL = 0x2055;
public const uint WGL_STEREO_EMITTER_DISABLE_3DL = 0x2056;
public const uint WGL_STEREO_POLARITY_NORMAL_3DL = 0x2057;
public const uint WGL_STEREO_POLARITY_INVERT_3DL = 0x2058;
internal delegate bool PFNWGLSETSTEREOEMITTERSTATE3DLPROC  (IntPtr hDC, uint uState);
internal static PFNWGLSETSTEREOEMITTERSTATE3DLPROC _wglSetStereoEmitterState3DL;// =(PFNWGLSETSTEREOEMITTERSTATE3DLPROC) GetProcedure("wglSetStereoEmitterState3DL", typeof(PFNWGLSETSTEREOEMITTERSTATE3DLPROC));
public static readonly bool isWGL_3DL_stereo_control;
/* ------------------------- WGL_ARB_buffer_region ------------------------- */
public const uint WGL_ARB_buffer_region = 0x1;
public const uint WGL_FRONT_COLOR_BUFFER_BIT_ARB = 0x1;
public const uint WGL_BACK_COLOR_BUFFER_BIT_ARB = 0x2;
public const uint WGL_DEPTH_BUFFER_BIT_ARB = 0x4;
public const uint WGL_STENCIL_BUFFER_BIT_ARB = 0x8;
internal delegate IntPtr PFNWGLCREATEBUFFERREGIONARBPROC  (IntPtr hDC, int iLayerPlane, uint uType);
internal delegate IntPtr PFNWGLDELETEBUFFERREGIONARBPROC  (IntPtr hRegion);
internal delegate bool PFNWGLRESTOREBUFFERREGIONARBPROC  (IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc);
internal delegate bool PFNWGLSAVEBUFFERREGIONARBPROC  (IntPtr hRegion, int x, int y, int width, int height);
internal static PFNWGLCREATEBUFFERREGIONARBPROC _wglCreateBufferRegionARB;// =(PFNWGLCREATEBUFFERREGIONARBPROC) GetProcedure("wglCreateBufferRegionARB", typeof(PFNWGLCREATEBUFFERREGIONARBPROC));
internal static PFNWGLDELETEBUFFERREGIONARBPROC _wglDeleteBufferRegionARB;// =(PFNWGLDELETEBUFFERREGIONARBPROC) GetProcedure("wglDeleteBufferRegionARB", typeof(PFNWGLDELETEBUFFERREGIONARBPROC));
internal static PFNWGLRESTOREBUFFERREGIONARBPROC _wglRestoreBufferRegionARB;// =(PFNWGLRESTOREBUFFERREGIONARBPROC) GetProcedure("wglRestoreBufferRegionARB", typeof(PFNWGLRESTOREBUFFERREGIONARBPROC));
internal static PFNWGLSAVEBUFFERREGIONARBPROC _wglSaveBufferRegionARB;// =(PFNWGLSAVEBUFFERREGIONARBPROC) GetProcedure("wglSaveBufferRegionARB", typeof(PFNWGLSAVEBUFFERREGIONARBPROC));
public static readonly bool isWGL_ARB_buffer_region;
/* ------------------------- WGL_ARB_create_context ------------------------ */
public const uint WGL_ARB_create_context = 0x1;
public const uint WGL_CONTEXT_DEBUG_BIT_ARB = 0x1;
public const uint WGL_CONTEXT_FORWARD_COMPATIBLE_BIT_ARB = 0x2;
public const uint WGL_CONTEXT_MAJOR_VERSION_ARB = 0x2091;
public const uint WGL_CONTEXT_MINOR_VERSION_ARB = 0x2092;
public const uint WGL_CONTEXT_LAYER_PLANE_ARB = 0x2093;
public const uint WGL_CONTEXT_FLAGS_ARB = 0x2094;
internal delegate IntPtr PFNWGLCREATECONTEXTATTRIBSARBPROC  (IntPtr hDC, IntPtr hShareContext, int[] attribList);
internal static PFNWGLCREATECONTEXTATTRIBSARBPROC _wglCreateContextAttribsARB;// =(PFNWGLCREATECONTEXTATTRIBSARBPROC) GetProcedure("wglCreateContextAttribsARB", typeof(PFNWGLCREATECONTEXTATTRIBSARBPROC));
public static readonly bool isWGL_ARB_create_context;
/* ----------------------- WGL_ARB_extensions_string ----------------------- */
public const uint WGL_ARB_extensions_string = 0x1;
internal delegate string PFNWGLGETEXTENSIONSSTRINGARBPROC  (IntPtr hdc);
internal static PFNWGLGETEXTENSIONSSTRINGARBPROC _wglGetExtensionsStringARB;// =(PFNWGLGETEXTENSIONSSTRINGARBPROC) GetProcedure("wglGetExtensionsStringARB", typeof(PFNWGLGETEXTENSIONSSTRINGARBPROC));
public static readonly bool isWGL_ARB_extensions_string;
/* ------------------------ WGL_ARB_framebuffer_sRGB ----------------------- */
public const uint WGL_ARB_framebuffer_sRGB = 0x1;
public static readonly bool isWGL_ARB_framebuffer_sRGB;
/* ----------------------- WGL_ARB_make_current_read ----------------------- */
public const uint WGL_ARB_make_current_read = 0x1;
public const uint ERROR_INVALID_PIXEL_TYPE_ARB = 0x2043;
public const uint ERROR_INCOMPATIBLE_DEVICE_CONTEXTS_ARB = 0x2054;
internal delegate IntPtr PFNWGLGETCURRENTREADDCARBPROC  (IntPtr v0);
internal delegate bool PFNWGLMAKECONTEXTCURRENTARBPROC  (IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);
internal static PFNWGLGETCURRENTREADDCARBPROC _wglGetCurrentReadDCARB;// =(PFNWGLGETCURRENTREADDCARBPROC) GetProcedure("wglGetCurrentReadDCARB", typeof(PFNWGLGETCURRENTREADDCARBPROC));
internal static PFNWGLMAKECONTEXTCURRENTARBPROC _wglMakeContextCurrentARB;// =(PFNWGLMAKECONTEXTCURRENTARBPROC) GetProcedure("wglMakeContextCurrentARB", typeof(PFNWGLMAKECONTEXTCURRENTARBPROC));
public static readonly bool isWGL_ARB_make_current_read;
/* -------------------------- WGL_ARB_multisample -------------------------- */
public const uint WGL_ARB_multisample = 0x1;
public const uint WGL_SAMPLE_BUFFERS_ARB = 0x2041;
public const uint WGL_SAMPLES_ARB = 0x2042;
public static readonly bool isWGL_ARB_multisample;
/* ---------------------------- WGL_ARB_pbuffer ---------------------------- */
public const uint WGL_ARB_pbuffer = 0x1;
public const uint WGL_MAX_PBUFFER_HEIGHT_ARB = 0x2030;
public const uint WGL_PBUFFER_LARGEST_ARB = 0x2033;
public const uint WGL_PBUFFER_WIDTH_ARB = 0x2034;
public const uint WGL_PBUFFER_HEIGHT_ARB = 0x2035;
public const uint WGL_PBUFFER_LOST_ARB = 0x2036;
internal delegate IntPtr PFNWGLCREATEPBUFFERARBPROC  (IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList);
internal delegate bool PFNWGLDESTROYPBUFFERARBPROC  (IntPtr hPbuffer);
internal delegate IntPtr PFNWGLGETPBUFFERDCARBPROC  (IntPtr hPbuffer);
internal delegate bool PFNWGLQUERYPBUFFERARBPROC  (IntPtr hPbuffer, int iAttribute, IntPtr piValue);
internal delegate int PFNWGLRELEASEPBUFFERDCARBPROC  (IntPtr hPbuffer, IntPtr hDC);
internal static PFNWGLCREATEPBUFFERARBPROC _wglCreatePbufferARB;// =(PFNWGLCREATEPBUFFERARBPROC) GetProcedure("wglCreatePbufferARB", typeof(PFNWGLCREATEPBUFFERARBPROC));
internal static PFNWGLDESTROYPBUFFERARBPROC _wglDestroyPbufferARB;// =(PFNWGLDESTROYPBUFFERARBPROC) GetProcedure("wglDestroyPbufferARB", typeof(PFNWGLDESTROYPBUFFERARBPROC));
internal static PFNWGLGETPBUFFERDCARBPROC _wglGetPbufferDCARB;// =(PFNWGLGETPBUFFERDCARBPROC) GetProcedure("wglGetPbufferDCARB", typeof(PFNWGLGETPBUFFERDCARBPROC));
internal static PFNWGLQUERYPBUFFERARBPROC _wglQueryPbufferARB;// =(PFNWGLQUERYPBUFFERARBPROC) GetProcedure("wglQueryPbufferARB", typeof(PFNWGLQUERYPBUFFERARBPROC));
internal static PFNWGLRELEASEPBUFFERDCARBPROC _wglReleasePbufferDCARB;// =(PFNWGLRELEASEPBUFFERDCARBPROC) GetProcedure("wglReleasePbufferDCARB", typeof(PFNWGLRELEASEPBUFFERDCARBPROC));
public static readonly bool isWGL_ARB_pbuffer;
/* -------------------------- WGL_ARB_pixel_format ------------------------- */
public const uint WGL_ARB_pixel_format = 0x1;
public const uint WGL_NUMBER_PIXEL_FORMATS_ARB = 0x2000;
public const uint WGL_DRAW_TO_WINDOW_ARB = 0x2001;
public const uint WGL_DRAW_TO_BITMAP_ARB = 0x2002;
public const uint WGL_ACCELERATION_ARB = 0x2003;
public const uint WGL_NEED_PALETTE_ARB = 0x2004;
public const uint WGL_NEED_SYSTEM_PALETTE_ARB = 0x2005;
public const uint WGL_SWAP_LAYER_BUFFERS_ARB = 0x2006;
public const uint WGL_SWAP_METHOD_ARB = 0x2007;
public const uint WGL_NUMBER_OVERLAYS_ARB = 0x2008;
public const uint WGL_NUMBER_UNDERLAYS_ARB = 0x2009;
public const uint WGL_SUPPORT_OPENGL_ARB = 0x2010;
public const uint WGL_DOUBLE_BUFFER_ARB = 0x2011;
public const uint WGL_STEREO_ARB = 0x2012;
public const uint WGL_PIXEL_TYPE_ARB = 0x2013;
public const uint WGL_COLOR_BITS_ARB = 0x2014;
public const uint WGL_RED_BITS_ARB = 0x2015;
public const uint WGL_RED_SHIFT_ARB = 0x2016;
public const uint WGL_GREEN_BITS_ARB = 0x2017;
public const uint WGL_GREEN_SHIFT_ARB = 0x2018;
public const uint WGL_BLUE_BITS_ARB = 0x2019;
public const uint WGL_ACCUM_BLUE_BITS_ARB = 0x2020;
public const uint WGL_ACCUM_ALPHA_BITS_ARB = 0x2021;
public const uint WGL_DEPTH_BITS_ARB = 0x2022;
public const uint WGL_STENCIL_BITS_ARB = 0x2023;
public const uint WGL_AUX_BUFFERS_ARB = 0x2024;
public const uint WGL_NO_ACCELERATION_ARB = 0x2025;
public const uint WGL_GENERIC_ACCELERATION_ARB = 0x2026;
public const uint WGL_FULL_ACCELERATION_ARB = 0x2027;
public const uint WGL_SWAP_EXCHANGE_ARB = 0x2028;
public const uint WGL_SWAP_COPY_ARB = 0x2029;
public const uint WGL_TRANSPARENT_RED_VALUE_ARB = 0x2037;
public const uint WGL_TRANSPARENT_GREEN_VALUE_ARB = 0x2038;
public const uint WGL_TRANSPARENT_BLUE_VALUE_ARB = 0x2039;
internal delegate bool PFNWGLCHOOSEPIXELFORMATARBPROC  (IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, IntPtr piFormats, IntPtr nNumFormats);
internal delegate bool PFNWGLGETPIXELFORMATATTRIBFVARBPROC  (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr pfValues);
internal delegate bool PFNWGLGETPIXELFORMATATTRIBIVARBPROC  (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr piValues);
internal static PFNWGLCHOOSEPIXELFORMATARBPROC _wglChoosePixelFormatARB;// =(PFNWGLCHOOSEPIXELFORMATARBPROC) GetProcedure("wglChoosePixelFormatARB", typeof(PFNWGLCHOOSEPIXELFORMATARBPROC));
internal static PFNWGLGETPIXELFORMATATTRIBFVARBPROC _wglGetPixelFormatAttribfvARB;// =(PFNWGLGETPIXELFORMATATTRIBFVARBPROC) GetProcedure("wglGetPixelFormatAttribfvARB", typeof(PFNWGLGETPIXELFORMATATTRIBFVARBPROC));
internal static PFNWGLGETPIXELFORMATATTRIBIVARBPROC _wglGetPixelFormatAttribivARB;// =(PFNWGLGETPIXELFORMATATTRIBIVARBPROC) GetProcedure("wglGetPixelFormatAttribivARB", typeof(PFNWGLGETPIXELFORMATATTRIBIVARBPROC));
public static readonly bool isWGL_ARB_pixel_format;
/* ----------------------- WGL_ARB_pixel_format_float ---------------------- */
public const uint WGL_ARB_pixel_format_float = 0x1;
public static readonly bool isWGL_ARB_pixel_format_float;
/* ------------------------- WGL_ARB_render_texture ------------------------ */
public const uint WGL_ARB_render_texture = 0x1;
public const uint WGL_BIND_TO_TEXTURE_RGB_ARB = 0x2070;
public const uint WGL_BIND_TO_TEXTURE_RGBA_ARB = 0x2071;
public const uint WGL_TEXTURE_FORMAT_ARB = 0x2072;
public const uint WGL_TEXTURE_TARGET_ARB = 0x2073;
public const uint WGL_MIPMAP_TEXTURE_ARB = 0x2074;
public const uint WGL_TEXTURE_RGB_ARB = 0x2075;
public const uint WGL_TEXTURE_RGBA_ARB = 0x2076;
public const uint WGL_NO_TEXTURE_ARB = 0x2077;
public const uint WGL_TEXTURE_CUBE_MAP_ARB = 0x2078;
public const uint WGL_TEXTURE_1D_ARB = 0x2079;
public const uint WGL_TEXTURE_CUBE_MAP_NEGATIVE_Y_ARB = 0x2080;
public const uint WGL_TEXTURE_CUBE_MAP_POSITIVE_Z_ARB = 0x2081;
public const uint WGL_TEXTURE_CUBE_MAP_NEGATIVE_Z_ARB = 0x2082;
public const uint WGL_FRONT_LEFT_ARB = 0x2083;
public const uint WGL_FRONT_RIGHT_ARB = 0x2084;
public const uint WGL_BACK_LEFT_ARB = 0x2085;
public const uint WGL_BACK_RIGHT_ARB = 0x2086;
public const uint WGL_AUX0_ARB = 0x2087;
public const uint WGL_AUX1_ARB = 0x2088;
public const uint WGL_AUX2_ARB = 0x2089;
public const uint WGL_AUX9_ARB = 0x2090;
internal delegate bool PFNWGLBINDTEXIMAGEARBPROC  (IntPtr hPbuffer, int iBuffer);
internal delegate bool PFNWGLRELEASETEXIMAGEARBPROC  (IntPtr hPbuffer, int iBuffer);
internal delegate bool PFNWGLSETPBUFFERATTRIBARBPROC  (IntPtr hPbuffer, IntPtr piAttribList);
internal static PFNWGLBINDTEXIMAGEARBPROC _wglBindTexImageARB;// =(PFNWGLBINDTEXIMAGEARBPROC) GetProcedure("wglBindTexImageARB", typeof(PFNWGLBINDTEXIMAGEARBPROC));
internal static PFNWGLRELEASETEXIMAGEARBPROC _wglReleaseTexImageARB;// =(PFNWGLRELEASETEXIMAGEARBPROC) GetProcedure("wglReleaseTexImageARB", typeof(PFNWGLRELEASETEXIMAGEARBPROC));
internal static PFNWGLSETPBUFFERATTRIBARBPROC _wglSetPbufferAttribARB;// =(PFNWGLSETPBUFFERATTRIBARBPROC) GetProcedure("wglSetPbufferAttribARB", typeof(PFNWGLSETPBUFFERATTRIBARBPROC));
public static readonly bool isWGL_ARB_render_texture;
/* ----------------------- WGL_ATI_pixel_format_float ---------------------- */
public const uint WGL_ATI_pixel_format_float = 0x1;
public const uint GL_RGBA_FLOAT_MODE_ATI = 0x8820;
public const uint GL_COLOR_CLEAR_UNCLAMPED_VALUE_ATI = 0x8835;
public static readonly bool isWGL_ATI_pixel_format_float;
/* -------------------- WGL_ATI_render_texture_rectangle ------------------- */
public const uint WGL_ATI_render_texture_rectangle = 0x1;
public static readonly bool isWGL_ATI_render_texture_rectangle;
/* -------------------------- WGL_EXT_depth_float -------------------------- */
public const uint WGL_EXT_depth_float = 0x1;
public const uint WGL_DEPTH_FLOAT_EXT = 0x2040;
public static readonly bool isWGL_EXT_depth_float;
/* ---------------------- WGL_EXT_display_color_table ---------------------- */
public const uint WGL_EXT_display_color_table = 0x1;
internal delegate bool PFNWGLBINDDISPLAYCOLORTABLEEXTPROC  (ushort id);
internal delegate bool PFNWGLCREATEDISPLAYCOLORTABLEEXTPROC  (ushort id);
internal delegate IntPtr PFNWGLDESTROYDISPLAYCOLORTABLEEXTPROC  (ushort id);
internal delegate bool PFNWGLLOADDISPLAYCOLORTABLEEXTPROC  (IntPtr table, uint length);
internal static PFNWGLBINDDISPLAYCOLORTABLEEXTPROC _wglBindDisplayColorTableEXT;// =(PFNWGLBINDDISPLAYCOLORTABLEEXTPROC) GetProcedure("wglBindDisplayColorTableEXT", typeof(PFNWGLBINDDISPLAYCOLORTABLEEXTPROC));
internal static PFNWGLCREATEDISPLAYCOLORTABLEEXTPROC _wglCreateDisplayColorTableEXT;// =(PFNWGLCREATEDISPLAYCOLORTABLEEXTPROC) GetProcedure("wglCreateDisplayColorTableEXT", typeof(PFNWGLCREATEDISPLAYCOLORTABLEEXTPROC));
internal static PFNWGLDESTROYDISPLAYCOLORTABLEEXTPROC _wglDestroyDisplayColorTableEXT;// =(PFNWGLDESTROYDISPLAYCOLORTABLEEXTPROC) GetProcedure("wglDestroyDisplayColorTableEXT", typeof(PFNWGLDESTROYDISPLAYCOLORTABLEEXTPROC));
internal static PFNWGLLOADDISPLAYCOLORTABLEEXTPROC _wglLoadDisplayColorTableEXT;// =(PFNWGLLOADDISPLAYCOLORTABLEEXTPROC) GetProcedure("wglLoadDisplayColorTableEXT", typeof(PFNWGLLOADDISPLAYCOLORTABLEEXTPROC));
public static readonly bool isWGL_EXT_display_color_table;
/* ----------------------- WGL_EXT_extensions_string ----------------------- */
public const uint WGL_EXT_extensions_string = 0x1;
internal delegate string PFNWGLGETEXTENSIONSSTRINGEXTPROC  ();
internal static PFNWGLGETEXTENSIONSSTRINGEXTPROC _wglGetExtensionsStringEXT;// =(PFNWGLGETEXTENSIONSSTRINGEXTPROC) GetProcedure("wglGetExtensionsStringEXT", typeof(PFNWGLGETEXTENSIONSSTRINGEXTPROC));
public static readonly bool isWGL_EXT_extensions_string;
/* ------------------------ WGL_EXT_framebuffer_sRGB ----------------------- */
public const uint WGL_EXT_framebuffer_sRGB = 0x1;
public static readonly bool isWGL_EXT_framebuffer_sRGB;
/* ----------------------- WGL_EXT_make_current_read ----------------------- */
public const uint WGL_EXT_make_current_read = 0x1;
public const uint ERROR_INVALID_PIXEL_TYPE_EXT = 0x2043;
internal delegate IntPtr PFNWGLGETCURRENTREADDCEXTPROC  (IntPtr v0);
internal delegate bool PFNWGLMAKECONTEXTCURRENTEXTPROC  (IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc);
internal static PFNWGLGETCURRENTREADDCEXTPROC _wglGetCurrentReadDCEXT;// =(PFNWGLGETCURRENTREADDCEXTPROC) GetProcedure("wglGetCurrentReadDCEXT", typeof(PFNWGLGETCURRENTREADDCEXTPROC));
internal static PFNWGLMAKECONTEXTCURRENTEXTPROC _wglMakeContextCurrentEXT;// =(PFNWGLMAKECONTEXTCURRENTEXTPROC) GetProcedure("wglMakeContextCurrentEXT", typeof(PFNWGLMAKECONTEXTCURRENTEXTPROC));
public static readonly bool isWGL_EXT_make_current_read;
/* -------------------------- WGL_EXT_multisample -------------------------- */
public const uint WGL_EXT_multisample = 0x1;
public const uint WGL_SAMPLE_BUFFERS_EXT = 0x2041;
public const uint WGL_SAMPLES_EXT = 0x2042;
public static readonly bool isWGL_EXT_multisample;
/* ---------------------------- WGL_EXT_pbuffer ---------------------------- */
public const uint WGL_EXT_pbuffer = 0x1;
public const uint WGL_MAX_PBUFFER_HEIGHT_EXT = 0x2030;
public const uint WGL_OPTIMAL_PBUFFER_WIDTH_EXT = 0x2031;
public const uint WGL_OPTIMAL_PBUFFER_HEIGHT_EXT = 0x2032;
public const uint WGL_PBUFFER_LARGEST_EXT = 0x2033;
public const uint WGL_PBUFFER_WIDTH_EXT = 0x2034;
public const uint WGL_PBUFFER_HEIGHT_EXT = 0x2035;
internal delegate IntPtr PFNWGLCREATEPBUFFEREXTPROC  (IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList);
internal delegate bool PFNWGLDESTROYPBUFFEREXTPROC  (IntPtr hPbuffer);
internal delegate IntPtr PFNWGLGETPBUFFERDCEXTPROC  (IntPtr hPbuffer);
internal delegate bool PFNWGLQUERYPBUFFEREXTPROC  (IntPtr hPbuffer, int iAttribute, IntPtr piValue);
internal delegate int PFNWGLRELEASEPBUFFERDCEXTPROC  (IntPtr hPbuffer, IntPtr hDC);
internal static PFNWGLCREATEPBUFFEREXTPROC _wglCreatePbufferEXT;// =(PFNWGLCREATEPBUFFEREXTPROC) GetProcedure("wglCreatePbufferEXT", typeof(PFNWGLCREATEPBUFFEREXTPROC));
internal static PFNWGLDESTROYPBUFFEREXTPROC _wglDestroyPbufferEXT;// =(PFNWGLDESTROYPBUFFEREXTPROC) GetProcedure("wglDestroyPbufferEXT", typeof(PFNWGLDESTROYPBUFFEREXTPROC));
internal static PFNWGLGETPBUFFERDCEXTPROC _wglGetPbufferDCEXT;// =(PFNWGLGETPBUFFERDCEXTPROC) GetProcedure("wglGetPbufferDCEXT", typeof(PFNWGLGETPBUFFERDCEXTPROC));
internal static PFNWGLQUERYPBUFFEREXTPROC _wglQueryPbufferEXT;// =(PFNWGLQUERYPBUFFEREXTPROC) GetProcedure("wglQueryPbufferEXT", typeof(PFNWGLQUERYPBUFFEREXTPROC));
internal static PFNWGLRELEASEPBUFFERDCEXTPROC _wglReleasePbufferDCEXT;// =(PFNWGLRELEASEPBUFFERDCEXTPROC) GetProcedure("wglReleasePbufferDCEXT", typeof(PFNWGLRELEASEPBUFFERDCEXTPROC));
public static readonly bool isWGL_EXT_pbuffer;
/* -------------------------- WGL_EXT_pixel_format ------------------------- */
public const uint WGL_EXT_pixel_format = 0x1;
public const uint WGL_NUMBER_PIXEL_FORMATS_EXT = 0x2000;
public const uint WGL_DRAW_TO_WINDOW_EXT = 0x2001;
public const uint WGL_DRAW_TO_BITMAP_EXT = 0x2002;
public const uint WGL_ACCELERATION_EXT = 0x2003;
public const uint WGL_NEED_PALETTE_EXT = 0x2004;
public const uint WGL_NEED_SYSTEM_PALETTE_EXT = 0x2005;
public const uint WGL_SWAP_LAYER_BUFFERS_EXT = 0x2006;
public const uint WGL_SWAP_METHOD_EXT = 0x2007;
public const uint WGL_NUMBER_OVERLAYS_EXT = 0x2008;
public const uint WGL_NUMBER_UNDERLAYS_EXT = 0x2009;
public const uint WGL_SUPPORT_OPENGL_EXT = 0x2010;
public const uint WGL_DOUBLE_BUFFER_EXT = 0x2011;
public const uint WGL_STEREO_EXT = 0x2012;
public const uint WGL_PIXEL_TYPE_EXT = 0x2013;
public const uint WGL_COLOR_BITS_EXT = 0x2014;
public const uint WGL_RED_BITS_EXT = 0x2015;
public const uint WGL_RED_SHIFT_EXT = 0x2016;
public const uint WGL_GREEN_BITS_EXT = 0x2017;
public const uint WGL_GREEN_SHIFT_EXT = 0x2018;
public const uint WGL_BLUE_BITS_EXT = 0x2019;
public const uint WGL_ACCUM_BLUE_BITS_EXT = 0x2020;
public const uint WGL_ACCUM_ALPHA_BITS_EXT = 0x2021;
public const uint WGL_DEPTH_BITS_EXT = 0x2022;
public const uint WGL_STENCIL_BITS_EXT = 0x2023;
public const uint WGL_AUX_BUFFERS_EXT = 0x2024;
public const uint WGL_NO_ACCELERATION_EXT = 0x2025;
public const uint WGL_GENERIC_ACCELERATION_EXT = 0x2026;
public const uint WGL_FULL_ACCELERATION_EXT = 0x2027;
public const uint WGL_SWAP_EXCHANGE_EXT = 0x2028;
public const uint WGL_SWAP_COPY_EXT = 0x2029;
internal delegate bool PFNWGLCHOOSEPIXELFORMATEXTPROC  (IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, IntPtr piFormats, IntPtr nNumFormats);
internal delegate bool PFNWGLGETPIXELFORMATATTRIBFVEXTPROC  (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr pfValues);
internal delegate bool PFNWGLGETPIXELFORMATATTRIBIVEXTPROC  (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr piValues);
internal static PFNWGLCHOOSEPIXELFORMATEXTPROC _wglChoosePixelFormatEXT;// =(PFNWGLCHOOSEPIXELFORMATEXTPROC) GetProcedure("wglChoosePixelFormatEXT", typeof(PFNWGLCHOOSEPIXELFORMATEXTPROC));
internal static PFNWGLGETPIXELFORMATATTRIBFVEXTPROC _wglGetPixelFormatAttribfvEXT;// =(PFNWGLGETPIXELFORMATATTRIBFVEXTPROC) GetProcedure("wglGetPixelFormatAttribfvEXT", typeof(PFNWGLGETPIXELFORMATATTRIBFVEXTPROC));
internal static PFNWGLGETPIXELFORMATATTRIBIVEXTPROC _wglGetPixelFormatAttribivEXT;// =(PFNWGLGETPIXELFORMATATTRIBIVEXTPROC) GetProcedure("wglGetPixelFormatAttribivEXT", typeof(PFNWGLGETPIXELFORMATATTRIBIVEXTPROC));
public static readonly bool isWGL_EXT_pixel_format;
/* ------------------- WGL_EXT_pixel_format_packed_float ------------------- */
public const uint WGL_EXT_pixel_format_packed_float = 0x1;
public static readonly bool isWGL_EXT_pixel_format_packed_float;
/* -------------------------- WGL_EXT_swap_control ------------------------- */
public const uint WGL_EXT_swap_control = 0x1;
internal delegate int PFNWGLGETSWAPINTERVALEXTPROC  ();
internal delegate bool PFNWGLSWAPINTERVALEXTPROC  (int interval);
internal static PFNWGLGETSWAPINTERVALEXTPROC _wglGetSwapIntervalEXT;// =(PFNWGLGETSWAPINTERVALEXTPROC) GetProcedure("wglGetSwapIntervalEXT", typeof(PFNWGLGETSWAPINTERVALEXTPROC));
internal static PFNWGLSWAPINTERVALEXTPROC _wglSwapIntervalEXT;// =(PFNWGLSWAPINTERVALEXTPROC) GetProcedure("wglSwapIntervalEXT", typeof(PFNWGLSWAPINTERVALEXTPROC));
public static readonly bool isWGL_EXT_swap_control;
/* --------------------- WGL_I3D_digital_video_control --------------------- */
public const uint WGL_I3D_digital_video_control = 0x1;
public const uint WGL_DIGITAL_VIDEO_CURSOR_ALPHA_FRAMEBUFFER_I3D = 0x2050;
public const uint WGL_DIGITAL_VIDEO_CURSOR_ALPHA_VALUE_I3D = 0x2051;
public const uint WGL_DIGITAL_VIDEO_CURSOR_INCLUDED_I3D = 0x2052;
public const uint WGL_DIGITAL_VIDEO_GAMMA_CORRECTED_I3D = 0x2053;
internal delegate bool PFNWGLGETDIGITALVIDEOPARAMETERSI3DPROC  (IntPtr hDC, int iAttribute, IntPtr piValue);
internal delegate bool PFNWGLSETDIGITALVIDEOPARAMETERSI3DPROC  (IntPtr hDC, int iAttribute, IntPtr piValue);
internal static PFNWGLGETDIGITALVIDEOPARAMETERSI3DPROC _wglGetDigitalVideoParametersI3D;// =(PFNWGLGETDIGITALVIDEOPARAMETERSI3DPROC) GetProcedure("wglGetDigitalVideoParametersI3D", typeof(PFNWGLGETDIGITALVIDEOPARAMETERSI3DPROC));
internal static PFNWGLSETDIGITALVIDEOPARAMETERSI3DPROC _wglSetDigitalVideoParametersI3D;// =(PFNWGLSETDIGITALVIDEOPARAMETERSI3DPROC) GetProcedure("wglSetDigitalVideoParametersI3D", typeof(PFNWGLSETDIGITALVIDEOPARAMETERSI3DPROC));
public static readonly bool isWGL_I3D_digital_video_control;
/* ----------------------------- WGL_I3D_gamma ----------------------------- */
public const uint WGL_I3D_gamma = 0x1;
internal delegate bool PFNWGLGETGAMMATABLEI3DPROC  (IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue);
internal delegate bool PFNWGLGETGAMMATABLEPARAMETERSI3DPROC  (IntPtr hDC, int iAttribute, IntPtr piValue);
internal delegate bool PFNWGLSETGAMMATABLEI3DPROC  (IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue);
internal delegate bool PFNWGLSETGAMMATABLEPARAMETERSI3DPROC  (IntPtr hDC, int iAttribute, IntPtr piValue);
internal static PFNWGLGETGAMMATABLEI3DPROC _wglGetGammaTableI3D;// =(PFNWGLGETGAMMATABLEI3DPROC) GetProcedure("wglGetGammaTableI3D", typeof(PFNWGLGETGAMMATABLEI3DPROC));
internal static PFNWGLGETGAMMATABLEPARAMETERSI3DPROC _wglGetGammaTableParametersI3D;// =(PFNWGLGETGAMMATABLEPARAMETERSI3DPROC) GetProcedure("wglGetGammaTableParametersI3D", typeof(PFNWGLGETGAMMATABLEPARAMETERSI3DPROC));
internal static PFNWGLSETGAMMATABLEI3DPROC _wglSetGammaTableI3D;// =(PFNWGLSETGAMMATABLEI3DPROC) GetProcedure("wglSetGammaTableI3D", typeof(PFNWGLSETGAMMATABLEI3DPROC));
internal static PFNWGLSETGAMMATABLEPARAMETERSI3DPROC _wglSetGammaTableParametersI3D;// =(PFNWGLSETGAMMATABLEPARAMETERSI3DPROC) GetProcedure("wglSetGammaTableParametersI3D", typeof(PFNWGLSETGAMMATABLEPARAMETERSI3DPROC));
public static readonly bool isWGL_I3D_gamma;
/* ---------------------------- WGL_I3D_genlock ---------------------------- */
public const uint WGL_I3D_genlock = 0x1;
public const uint WGL_GENLOCK_SOURCE_MULTIVIEW_I3D = 0x2044;
public const uint WGL_GENLOCK_SOURCE_EXTERNAL_SYNC_I3D = 0x2045;
public const uint WGL_GENLOCK_SOURCE_EXTERNAL_FIELD_I3D = 0x2046;
public const uint WGL_GENLOCK_SOURCE_EXTERNAL_TTL_I3D = 0x2047;
public const uint WGL_GENLOCK_SOURCE_DIGITAL_SYNC_I3D = 0x2048;
public const uint WGL_GENLOCK_SOURCE_DIGITAL_FIELD_I3D = 0x2049;
internal delegate bool PFNWGLDISABLEGENLOCKI3DPROC  (IntPtr hDC);
internal delegate bool PFNWGLENABLEGENLOCKI3DPROC  (IntPtr hDC);
internal delegate bool PFNWGLGENLOCKSAMPLERATEI3DPROC  (IntPtr hDC, uint uRate);
internal delegate bool PFNWGLGENLOCKSOURCEDELAYI3DPROC  (IntPtr hDC, uint uDelay);
internal delegate bool PFNWGLGENLOCKSOURCEEDGEI3DPROC  (IntPtr hDC, uint uEdge);
internal delegate bool PFNWGLGENLOCKSOURCEI3DPROC  (IntPtr hDC, uint uSource);
internal delegate bool PFNWGLGETGENLOCKSAMPLERATEI3DPROC  (IntPtr hDC, IntPtr uRate);
internal delegate bool PFNWGLGETGENLOCKSOURCEDELAYI3DPROC  (IntPtr hDC, IntPtr uDelay);
internal delegate bool PFNWGLGETGENLOCKSOURCEEDGEI3DPROC  (IntPtr hDC, IntPtr uEdge);
internal delegate bool PFNWGLGETGENLOCKSOURCEI3DPROC  (IntPtr hDC, IntPtr uSource);
internal delegate bool PFNWGLISENABLEDGENLOCKI3DPROC  (IntPtr hDC, IntPtr pFlag);
internal delegate bool PFNWGLQUERYGENLOCKMAXSOURCEDELAYI3DPROC  (IntPtr hDC, IntPtr uMaxLineDelay, IntPtr uMaxPixelDelay);
internal static PFNWGLDISABLEGENLOCKI3DPROC _wglDisableGenlockI3D;// =(PFNWGLDISABLEGENLOCKI3DPROC) GetProcedure("wglDisableGenlockI3D", typeof(PFNWGLDISABLEGENLOCKI3DPROC));
internal static PFNWGLENABLEGENLOCKI3DPROC _wglEnableGenlockI3D;// =(PFNWGLENABLEGENLOCKI3DPROC) GetProcedure("wglEnableGenlockI3D", typeof(PFNWGLENABLEGENLOCKI3DPROC));
internal static PFNWGLGENLOCKSAMPLERATEI3DPROC _wglGenlockSampleRateI3D;// =(PFNWGLGENLOCKSAMPLERATEI3DPROC) GetProcedure("wglGenlockSampleRateI3D", typeof(PFNWGLGENLOCKSAMPLERATEI3DPROC));
internal static PFNWGLGENLOCKSOURCEDELAYI3DPROC _wglGenlockSourceDelayI3D;// =(PFNWGLGENLOCKSOURCEDELAYI3DPROC) GetProcedure("wglGenlockSourceDelayI3D", typeof(PFNWGLGENLOCKSOURCEDELAYI3DPROC));
internal static PFNWGLGENLOCKSOURCEEDGEI3DPROC _wglGenlockSourceEdgeI3D;// =(PFNWGLGENLOCKSOURCEEDGEI3DPROC) GetProcedure("wglGenlockSourceEdgeI3D", typeof(PFNWGLGENLOCKSOURCEEDGEI3DPROC));
internal static PFNWGLGENLOCKSOURCEI3DPROC _wglGenlockSourceI3D;// =(PFNWGLGENLOCKSOURCEI3DPROC) GetProcedure("wglGenlockSourceI3D", typeof(PFNWGLGENLOCKSOURCEI3DPROC));
internal static PFNWGLGETGENLOCKSAMPLERATEI3DPROC _wglGetGenlockSampleRateI3D;// =(PFNWGLGETGENLOCKSAMPLERATEI3DPROC) GetProcedure("wglGetGenlockSampleRateI3D", typeof(PFNWGLGETGENLOCKSAMPLERATEI3DPROC));
internal static PFNWGLGETGENLOCKSOURCEDELAYI3DPROC _wglGetGenlockSourceDelayI3D;// =(PFNWGLGETGENLOCKSOURCEDELAYI3DPROC) GetProcedure("wglGetGenlockSourceDelayI3D", typeof(PFNWGLGETGENLOCKSOURCEDELAYI3DPROC));
internal static PFNWGLGETGENLOCKSOURCEEDGEI3DPROC _wglGetGenlockSourceEdgeI3D;// =(PFNWGLGETGENLOCKSOURCEEDGEI3DPROC) GetProcedure("wglGetGenlockSourceEdgeI3D", typeof(PFNWGLGETGENLOCKSOURCEEDGEI3DPROC));
internal static PFNWGLGETGENLOCKSOURCEI3DPROC _wglGetGenlockSourceI3D;// =(PFNWGLGETGENLOCKSOURCEI3DPROC) GetProcedure("wglGetGenlockSourceI3D", typeof(PFNWGLGETGENLOCKSOURCEI3DPROC));
internal static PFNWGLISENABLEDGENLOCKI3DPROC _wglIsEnabledGenlockI3D;// =(PFNWGLISENABLEDGENLOCKI3DPROC) GetProcedure("wglIsEnabledGenlockI3D", typeof(PFNWGLISENABLEDGENLOCKI3DPROC));
internal static PFNWGLQUERYGENLOCKMAXSOURCEDELAYI3DPROC _wglQueryGenlockMaxSourceDelayI3D;// =(PFNWGLQUERYGENLOCKMAXSOURCEDELAYI3DPROC) GetProcedure("wglQueryGenlockMaxSourceDelayI3D", typeof(PFNWGLQUERYGENLOCKMAXSOURCEDELAYI3DPROC));
public static readonly bool isWGL_I3D_genlock;
/* -------------------------- WGL_I3D_image_buffer ------------------------- */
public const uint WGL_I3D_image_buffer = 0x1;
public const uint WGL_IMAGE_BUFFER_MIN_ACCESS_I3D = 0x1;
public const uint WGL_IMAGE_BUFFER_LOCK_I3D = 0x2;
internal delegate bool PFNWGLASSOCIATEIMAGEBUFFEREVENTSI3DPROC  (IntPtr hdc, IntPtr pEvent, IntPtr pAddress, IntPtr pSize, uint count);
internal delegate IntPtr PFNWGLCREATEIMAGEBUFFERI3DPROC  (IntPtr hDC, int dwSize, uint uFlags);
internal delegate bool PFNWGLDESTROYIMAGEBUFFERI3DPROC  (IntPtr hDC, IntPtr pAddress);
internal delegate bool PFNWGLRELEASEIMAGEBUFFEREVENTSI3DPROC  (IntPtr hdc, IntPtr pAddress, uint count);
internal static PFNWGLASSOCIATEIMAGEBUFFEREVENTSI3DPROC _wglAssociateImageBufferEventsI3D;// =(PFNWGLASSOCIATEIMAGEBUFFEREVENTSI3DPROC) GetProcedure("wglAssociateImageBufferEventsI3D", typeof(PFNWGLASSOCIATEIMAGEBUFFEREVENTSI3DPROC));
internal static PFNWGLCREATEIMAGEBUFFERI3DPROC _wglCreateImageBufferI3D;// =(PFNWGLCREATEIMAGEBUFFERI3DPROC) GetProcedure("wglCreateImageBufferI3D", typeof(PFNWGLCREATEIMAGEBUFFERI3DPROC));
internal static PFNWGLDESTROYIMAGEBUFFERI3DPROC _wglDestroyImageBufferI3D;// =(PFNWGLDESTROYIMAGEBUFFERI3DPROC) GetProcedure("wglDestroyImageBufferI3D", typeof(PFNWGLDESTROYIMAGEBUFFERI3DPROC));
internal static PFNWGLRELEASEIMAGEBUFFEREVENTSI3DPROC _wglReleaseImageBufferEventsI3D;// =(PFNWGLRELEASEIMAGEBUFFEREVENTSI3DPROC) GetProcedure("wglReleaseImageBufferEventsI3D", typeof(PFNWGLRELEASEIMAGEBUFFEREVENTSI3DPROC));
public static readonly bool isWGL_I3D_image_buffer;
/* ------------------------ WGL_I3D_swap_frame_lock ------------------------ */
public const uint WGL_I3D_swap_frame_lock = 0x1;
internal delegate bool PFNWGLDISABLEFRAMELOCKI3DPROC  (IntPtr v0);
internal delegate bool PFNWGLENABLEFRAMELOCKI3DPROC  (IntPtr v0);
internal delegate bool PFNWGLISENABLEDFRAMELOCKI3DPROC  (IntPtr pFlag);
internal delegate bool PFNWGLQUERYFRAMELOCKMASTERI3DPROC  (IntPtr pFlag);
internal static PFNWGLDISABLEFRAMELOCKI3DPROC _wglDisableFrameLockI3D;// =(PFNWGLDISABLEFRAMELOCKI3DPROC) GetProcedure("wglDisableFrameLockI3D", typeof(PFNWGLDISABLEFRAMELOCKI3DPROC));
internal static PFNWGLENABLEFRAMELOCKI3DPROC _wglEnableFrameLockI3D;// =(PFNWGLENABLEFRAMELOCKI3DPROC) GetProcedure("wglEnableFrameLockI3D", typeof(PFNWGLENABLEFRAMELOCKI3DPROC));
internal static PFNWGLISENABLEDFRAMELOCKI3DPROC _wglIsEnabledFrameLockI3D;// =(PFNWGLISENABLEDFRAMELOCKI3DPROC) GetProcedure("wglIsEnabledFrameLockI3D", typeof(PFNWGLISENABLEDFRAMELOCKI3DPROC));
internal static PFNWGLQUERYFRAMELOCKMASTERI3DPROC _wglQueryFrameLockMasterI3D;// =(PFNWGLQUERYFRAMELOCKMASTERI3DPROC) GetProcedure("wglQueryFrameLockMasterI3D", typeof(PFNWGLQUERYFRAMELOCKMASTERI3DPROC));
public static readonly bool isWGL_I3D_swap_frame_lock;
/* ------------------------ WGL_I3D_swap_frame_usage ----------------------- */
public const uint WGL_I3D_swap_frame_usage = 0x1;
internal delegate bool PFNWGLBEGINFRAMETRACKINGI3DPROC  ();
internal delegate bool PFNWGLENDFRAMETRACKINGI3DPROC  ();
internal delegate bool PFNWGLGETFRAMEUSAGEI3DPROC  (IntPtr pUsage);
internal delegate bool PFNWGLQUERYFRAMETRACKINGI3DPROC  (IntPtr pFrameCount, IntPtr pMissedFrames, IntPtr pLastMissedUsage);
internal static PFNWGLBEGINFRAMETRACKINGI3DPROC _wglBeginFrameTrackingI3D;// =(PFNWGLBEGINFRAMETRACKINGI3DPROC) GetProcedure("wglBeginFrameTrackingI3D", typeof(PFNWGLBEGINFRAMETRACKINGI3DPROC));
internal static PFNWGLENDFRAMETRACKINGI3DPROC _wglEndFrameTrackingI3D;// =(PFNWGLENDFRAMETRACKINGI3DPROC) GetProcedure("wglEndFrameTrackingI3D", typeof(PFNWGLENDFRAMETRACKINGI3DPROC));
internal static PFNWGLGETFRAMEUSAGEI3DPROC _wglGetFrameUsageI3D;// =(PFNWGLGETFRAMEUSAGEI3DPROC) GetProcedure("wglGetFrameUsageI3D", typeof(PFNWGLGETFRAMEUSAGEI3DPROC));
internal static PFNWGLQUERYFRAMETRACKINGI3DPROC _wglQueryFrameTrackingI3D;// =(PFNWGLQUERYFRAMETRACKINGI3DPROC) GetProcedure("wglQueryFrameTrackingI3D", typeof(PFNWGLQUERYFRAMETRACKINGI3DPROC));
public static readonly bool isWGL_I3D_swap_frame_usage;
/* -------------------------- WGL_NV_float_buffer -------------------------- */
public const uint WGL_NV_float_buffer = 0x1;
public static readonly bool isWGL_NV_float_buffer;
/* -------------------------- WGL_NV_gpu_affinity -------------------------- */
public const uint WGL_NV_gpu_affinity = 0x1;
internal delegate IntPtr PFNWGLCREATEAFFINITYDCNVPROC  (IntPtr phGpuList);
internal delegate bool PFNWGLDELETEDCNVPROC  (IntPtr hdc);
internal delegate bool PFNWGLENUMGPUDEVICESNVPROC  (IntPtr hGpu, uint iDeviceIndex, IntPtr lpGpuDevice);
internal delegate bool PFNWGLENUMGPUSFROMAFFINITYDCNVPROC  (IntPtr hAffinityDC, uint iGpuIndex, IntPtr hGpu);
internal delegate bool PFNWGLENUMGPUSNVPROC  (uint iGpuIndex, IntPtr phGpu);
internal static PFNWGLCREATEAFFINITYDCNVPROC _wglCreateAffinityDCNV;// =(PFNWGLCREATEAFFINITYDCNVPROC) GetProcedure("wglCreateAffinityDCNV", typeof(PFNWGLCREATEAFFINITYDCNVPROC));
internal static PFNWGLDELETEDCNVPROC _wglDeleteDCNV;// =(PFNWGLDELETEDCNVPROC) GetProcedure("wglDeleteDCNV", typeof(PFNWGLDELETEDCNVPROC));
internal static PFNWGLENUMGPUDEVICESNVPROC _wglEnumGpuDevicesNV;// =(PFNWGLENUMGPUDEVICESNVPROC) GetProcedure("wglEnumGpuDevicesNV", typeof(PFNWGLENUMGPUDEVICESNVPROC));
internal static PFNWGLENUMGPUSFROMAFFINITYDCNVPROC _wglEnumGpusFromAffinityDCNV;// =(PFNWGLENUMGPUSFROMAFFINITYDCNVPROC) GetProcedure("wglEnumGpusFromAffinityDCNV", typeof(PFNWGLENUMGPUSFROMAFFINITYDCNVPROC));
internal static PFNWGLENUMGPUSNVPROC _wglEnumGpusNV;// =(PFNWGLENUMGPUSNVPROC) GetProcedure("wglEnumGpusNV", typeof(PFNWGLENUMGPUSNVPROC));
public static readonly bool isWGL_NV_gpu_affinity;
/* -------------------------- WGL_NV_present_video ------------------------- */
public const uint WGL_NV_present_video = 0x1;
internal delegate bool PFNWGLBINDVIDEODEVICENVPROC  (IntPtr hDc, uint  uVideoSlot, IntPtr hVideoDevice, IntPtr piAttribList);
internal delegate int PFNWGLENUMERATEVIDEODEVICESNVPROC  (IntPtr hDc, IntPtr phDeviceList);
internal delegate bool PFNWGLQUERYCURRENTCONTEXTNVPROC  (int iAttribute, IntPtr piValue);
internal static PFNWGLBINDVIDEODEVICENVPROC _wglBindVideoDeviceNV;// =(PFNWGLBINDVIDEODEVICENVPROC) GetProcedure("wglBindVideoDeviceNV", typeof(PFNWGLBINDVIDEODEVICENVPROC));
internal static PFNWGLENUMERATEVIDEODEVICESNVPROC _wglEnumerateVideoDevicesNV;// =(PFNWGLENUMERATEVIDEODEVICESNVPROC) GetProcedure("wglEnumerateVideoDevicesNV", typeof(PFNWGLENUMERATEVIDEODEVICESNVPROC));
internal static PFNWGLQUERYCURRENTCONTEXTNVPROC _wglQueryCurrentContextNV;// =(PFNWGLQUERYCURRENTCONTEXTNVPROC) GetProcedure("wglQueryCurrentContextNV", typeof(PFNWGLQUERYCURRENTCONTEXTNVPROC));
public static readonly bool isWGL_NV_present_video;
/* ---------------------- WGL_NV_render_depth_texture ---------------------- */
public const uint WGL_NV_render_depth_texture = 0x1;
//public const uint WGL_NO_TEXTURE_ARB = 0x2077;
public static readonly bool isWGL_NV_render_depth_texture;
/* -------------------- WGL_NV_render_texture_rectangle -------------------- */
public const uint WGL_NV_render_texture_rectangle = 0x1;
public static readonly bool isWGL_NV_render_texture_rectangle;
/* --------------------------- WGL_NV_swap_group --------------------------- */
public const uint WGL_NV_swap_group = 0x1;
internal delegate bool PFNWGLBINDSWAPBARRIERNVPROC  (uint group, uint barrier);
internal delegate bool PFNWGLJOINSWAPGROUPNVPROC  (IntPtr hDC, uint group);
internal delegate bool PFNWGLQUERYFRAMECOUNTNVPROC  (IntPtr hDC, IntPtr count);
internal delegate bool PFNWGLQUERYMAXSWAPGROUPSNVPROC  (IntPtr hDC, IntPtr maxGroups, IntPtr maxBarriers);
internal delegate bool PFNWGLQUERYSWAPGROUPNVPROC  (IntPtr hDC, IntPtr group);
internal delegate bool PFNWGLRESETFRAMECOUNTNVPROC  (IntPtr hDC);
internal static PFNWGLBINDSWAPBARRIERNVPROC _wglBindSwapBarrierNV;// =(PFNWGLBINDSWAPBARRIERNVPROC) GetProcedure("wglBindSwapBarrierNV", typeof(PFNWGLBINDSWAPBARRIERNVPROC));
internal static PFNWGLJOINSWAPGROUPNVPROC _wglJoinSwapGroupNV;// =(PFNWGLJOINSWAPGROUPNVPROC) GetProcedure("wglJoinSwapGroupNV", typeof(PFNWGLJOINSWAPGROUPNVPROC));
internal static PFNWGLQUERYFRAMECOUNTNVPROC _wglQueryFrameCountNV;// =(PFNWGLQUERYFRAMECOUNTNVPROC) GetProcedure("wglQueryFrameCountNV", typeof(PFNWGLQUERYFRAMECOUNTNVPROC));
internal static PFNWGLQUERYMAXSWAPGROUPSNVPROC _wglQueryMaxSwapGroupsNV;// =(PFNWGLQUERYMAXSWAPGROUPSNVPROC) GetProcedure("wglQueryMaxSwapGroupsNV", typeof(PFNWGLQUERYMAXSWAPGROUPSNVPROC));
internal static PFNWGLQUERYSWAPGROUPNVPROC _wglQuerySwapGroupNV;// =(PFNWGLQUERYSWAPGROUPNVPROC) GetProcedure("wglQuerySwapGroupNV", typeof(PFNWGLQUERYSWAPGROUPNVPROC));
internal static PFNWGLRESETFRAMECOUNTNVPROC _wglResetFrameCountNV;// =(PFNWGLRESETFRAMECOUNTNVPROC) GetProcedure("wglResetFrameCountNV", typeof(PFNWGLRESETFRAMECOUNTNVPROC));
public static readonly bool isWGL_NV_swap_group;
/* ----------------------- WGL_NV_vertex_array_range ----------------------- */
public const uint WGL_NV_vertex_array_range = 0x1;
internal delegate IntPtr PFNWGLALLOCATEMEMORYNVPROC  (int size, float readFrequency, float writeFrequency, float priority);
internal delegate IntPtr PFNWGLFREEMEMORYNVPROC  (IntPtr pointer);
internal static PFNWGLALLOCATEMEMORYNVPROC _wglAllocateMemoryNV;// =(PFNWGLALLOCATEMEMORYNVPROC) GetProcedure("wglAllocateMemoryNV", typeof(PFNWGLALLOCATEMEMORYNVPROC));
internal static PFNWGLFREEMEMORYNVPROC _wglFreeMemoryNV;// =(PFNWGLFREEMEMORYNVPROC) GetProcedure("wglFreeMemoryNV", typeof(PFNWGLFREEMEMORYNVPROC));
public static readonly bool isWGL_NV_vertex_array_range;
/* -------------------------- WGL_NV_video_output -------------------------- */
public const uint WGL_NV_video_output = 0x1;
internal delegate bool PFNWGLBINDVIDEOIMAGENVPROC  (IntPtr hVideoDevice, IntPtr hPbuffer, int iVideoBuffer);
internal delegate bool PFNWGLGETVIDEODEVICENVPROC  (IntPtr hDC, int numDevices, IntPtr hVideoDevice);
internal delegate bool PFNWGLGETVIDEOINFONVPROC  (IntPtr IntPtrice, ulong pulCounterOutputPbuffer, ulong pulCounterOutputVideo);
internal delegate bool PFNWGLRELEASEVIDEODEVICENVPROC  (IntPtr hVideoDevice);
internal delegate bool PFNWGLRELEASEVIDEOIMAGENVPROC  (IntPtr hPbuffer, int iVideoBuffer);
internal delegate bool PFNWGLSENDPBUFFERTOVIDEONVPROC  (IntPtr hPbuffer, int iBufferType, ulong  pulCounterPbuffer, bool bBlock);
internal static PFNWGLBINDVIDEOIMAGENVPROC _wglBindVideoImageNV;// =(PFNWGLBINDVIDEOIMAGENVPROC) GetProcedure("wglBindVideoImageNV", typeof(PFNWGLBINDVIDEOIMAGENVPROC));
internal static PFNWGLGETVIDEODEVICENVPROC _wglGetVideoDeviceNV;// =(PFNWGLGETVIDEODEVICENVPROC) GetProcedure("wglGetVideoDeviceNV", typeof(PFNWGLGETVIDEODEVICENVPROC));
internal static PFNWGLGETVIDEOINFONVPROC _wglGetVideoInfoNV;// =(PFNWGLGETVIDEOINFONVPROC) GetProcedure("wglGetVideoInfoNV", typeof(PFNWGLGETVIDEOINFONVPROC));
internal static PFNWGLRELEASEVIDEODEVICENVPROC _wglReleaseVideoDeviceNV;// =(PFNWGLRELEASEVIDEODEVICENVPROC) GetProcedure("wglReleaseVideoDeviceNV", typeof(PFNWGLRELEASEVIDEODEVICENVPROC));
internal static PFNWGLRELEASEVIDEOIMAGENVPROC _wglReleaseVideoImageNV;// =(PFNWGLRELEASEVIDEOIMAGENVPROC) GetProcedure("wglReleaseVideoImageNV", typeof(PFNWGLRELEASEVIDEOIMAGENVPROC));
internal static PFNWGLSENDPBUFFERTOVIDEONVPROC _wglSendPbufferToVideoNV;// =(PFNWGLSENDPBUFFERTOVIDEONVPROC) GetProcedure("wglSendPbufferToVideoNV", typeof(PFNWGLSENDPBUFFERTOVIDEONVPROC));
public static readonly bool isWGL_NV_video_output;
/* -------------------------- WGL_OML_sync_control ------------------------- */
public const uint WGL_OML_sync_control = 0x1;
internal delegate bool PFNWGLGETMSCRATEOMLPROC  (IntPtr hdc, IntPtr numerator, IntPtr denominator);
internal delegate bool PFNWGLGETSYNCVALUESOMLPROC  (IntPtr hdc, IntPtr ust, IntPtr msc, IntPtr sbc);
internal delegate Int64 PFNWGLSWAPBUFFERSMSCOMLPROC  (IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder);
internal delegate Int64 PFNWGLSWAPLAYERBUFFERSMSCOMLPROC  (IntPtr hdc, int fuPlanes, Int64 target_msc, Int64 divisor, Int64 remainder);
internal delegate bool PFNWGLWAITFORMSCOMLPROC  (IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder, IntPtr ust, IntPtr msc, IntPtr sbc);
internal delegate bool PFNWGLWAITFORSBCOMLPROC  (IntPtr hdc, Int64 target_sbc, IntPtr ust, IntPtr msc, IntPtr sbc);
internal static PFNWGLGETMSCRATEOMLPROC _wglGetMscRateOML;// =(PFNWGLGETMSCRATEOMLPROC) GetProcedure("wglGetMscRateOML", typeof(PFNWGLGETMSCRATEOMLPROC));
internal static PFNWGLGETSYNCVALUESOMLPROC _wglGetSyncValuesOML;// =(PFNWGLGETSYNCVALUESOMLPROC) GetProcedure("wglGetSyncValuesOML", typeof(PFNWGLGETSYNCVALUESOMLPROC));
internal static PFNWGLSWAPBUFFERSMSCOMLPROC _wglSwapBuffersMscOML;// =(PFNWGLSWAPBUFFERSMSCOMLPROC) GetProcedure("wglSwapBuffersMscOML", typeof(PFNWGLSWAPBUFFERSMSCOMLPROC));
internal static PFNWGLSWAPLAYERBUFFERSMSCOMLPROC _wglSwapLayerBuffersMscOML;// =(PFNWGLSWAPLAYERBUFFERSMSCOMLPROC) GetProcedure("wglSwapLayerBuffersMscOML", typeof(PFNWGLSWAPLAYERBUFFERSMSCOMLPROC));
internal static PFNWGLWAITFORMSCOMLPROC _wglWaitForMscOML;// =(PFNWGLWAITFORMSCOMLPROC) GetProcedure("wglWaitForMscOML", typeof(PFNWGLWAITFORMSCOMLPROC));
internal static PFNWGLWAITFORSBCOMLPROC _wglWaitForSbcOML;// =(PFNWGLWAITFORSBCOMLPROC) GetProcedure("wglWaitForSbcOML", typeof(PFNWGLWAITFORSBCOMLPROC));
public static readonly bool isWGL_OML_sync_control;
/* ------------------------------------------------------------------------- */
/* ------------------------------------------------------------------------- */
/************************************Exported Method********/
public static bool wglSetStereoEmitterState3DL (IntPtr hDC, uint uState){ if (_wglSetStereoEmitterState3DL!=null) return _wglSetStereoEmitterState3DL (hDC, uState); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSetStereoEmitterState3DL"));}
public static IntPtr wglCreateBufferRegionARB (IntPtr hDC, int iLayerPlane, uint uType){ if (_wglCreateBufferRegionARB!=null) return _wglCreateBufferRegionARB (hDC, iLayerPlane, uType); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreateBufferRegionARB"));}
public static IntPtr wglDeleteBufferRegionARB (IntPtr hRegion){ if (_wglDeleteBufferRegionARB!=null) return _wglDeleteBufferRegionARB (hRegion); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDeleteBufferRegionARB"));}
public static bool wglRestoreBufferRegionARB (IntPtr hRegion, int x, int y, int width, int height, int xSrc, int ySrc){ if (_wglRestoreBufferRegionARB!=null) return _wglRestoreBufferRegionARB (hRegion, x, y, width, height, xSrc, ySrc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglRestoreBufferRegionARB"));}
public static bool wglSaveBufferRegionARB (IntPtr hRegion, int x, int y, int width, int height){ if (_wglSaveBufferRegionARB!=null) return _wglSaveBufferRegionARB (hRegion, x, y, width, height); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSaveBufferRegionARB"));}
public static IntPtr wglCreateContextAttribsARB (IntPtr hDC, IntPtr hShareContext, int[] attribList){ if (_wglCreateContextAttribsARB!=null) return _wglCreateContextAttribsARB (hDC, hShareContext, attribList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreateContextAttribsARB"));}
public static string wglGetExtensionsStringARB (IntPtr hdc){ if (_wglGetExtensionsStringARB!=null) return _wglGetExtensionsStringARB (hdc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetExtensionsStringARB"));}
public static IntPtr wglGetCurrentReadDCARB (IntPtr v0){ if (_wglGetCurrentReadDCARB!=null) return _wglGetCurrentReadDCARB (v0); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetCurrentReadDCARB"));}
public static bool wglMakeContextCurrentARB (IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc){ if (_wglMakeContextCurrentARB!=null) return _wglMakeContextCurrentARB (hDrawDC, hReadDC, hglrc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglMakeContextCurrentARB"));}
public static IntPtr wglCreatePbufferARB (IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList){ if (_wglCreatePbufferARB!=null) return _wglCreatePbufferARB (hDC, iPixelFormat, iWidth, iHeight, piAttribList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreatePbufferARB"));}
public static bool wglDestroyPbufferARB (IntPtr hPbuffer){ if (_wglDestroyPbufferARB!=null) return _wglDestroyPbufferARB (hPbuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDestroyPbufferARB"));}
public static IntPtr wglGetPbufferDCARB (IntPtr hPbuffer){ if (_wglGetPbufferDCARB!=null) return _wglGetPbufferDCARB (hPbuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPbufferDCARB"));}
public static bool wglQueryPbufferARB (IntPtr hPbuffer, int iAttribute, IntPtr piValue){ if (_wglQueryPbufferARB!=null) return _wglQueryPbufferARB (hPbuffer, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryPbufferARB"));}
public static int wglReleasePbufferDCARB (IntPtr hPbuffer, IntPtr hDC){ if (_wglReleasePbufferDCARB!=null) return _wglReleasePbufferDCARB (hPbuffer, hDC); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleasePbufferDCARB"));}
public static bool wglChoosePixelFormatARB (IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, IntPtr piFormats, IntPtr nNumFormats){ if (_wglChoosePixelFormatARB!=null) return _wglChoosePixelFormatARB (hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, nNumFormats); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglChoosePixelFormatARB"));}
public static bool wglGetPixelFormatAttribfvARB (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr pfValues){ if (_wglGetPixelFormatAttribfvARB!=null) return _wglGetPixelFormatAttribfvARB (hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, pfValues); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPixelFormatAttribfvARB"));}
public static bool wglGetPixelFormatAttribivARB (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr piValues){ if (_wglGetPixelFormatAttribivARB!=null) return _wglGetPixelFormatAttribivARB (hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, piValues); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPixelFormatAttribivARB"));}
public static bool wglBindTexImageARB (IntPtr hPbuffer, int iBuffer){ if (_wglBindTexImageARB!=null) return _wglBindTexImageARB (hPbuffer, iBuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBindTexImageARB"));}
public static bool wglReleaseTexImageARB (IntPtr hPbuffer, int iBuffer){ if (_wglReleaseTexImageARB!=null) return _wglReleaseTexImageARB (hPbuffer, iBuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleaseTexImageARB"));}
public static bool wglSetPbufferAttribARB (IntPtr hPbuffer, IntPtr piAttribList){ if (_wglSetPbufferAttribARB!=null) return _wglSetPbufferAttribARB (hPbuffer, piAttribList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSetPbufferAttribARB"));}
public static bool wglBindDisplayColorTableEXT (ushort id){ if (_wglBindDisplayColorTableEXT!=null) return _wglBindDisplayColorTableEXT (id); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBindDisplayColorTableEXT"));}
public static bool wglCreateDisplayColorTableEXT (ushort id){ if (_wglCreateDisplayColorTableEXT!=null) return _wglCreateDisplayColorTableEXT (id); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreateDisplayColorTableEXT"));}
public static IntPtr wglDestroyDisplayColorTableEXT (ushort id){ if (_wglDestroyDisplayColorTableEXT!=null) return _wglDestroyDisplayColorTableEXT (id); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDestroyDisplayColorTableEXT"));}
public static bool wglLoadDisplayColorTableEXT (IntPtr table, uint length){ if (_wglLoadDisplayColorTableEXT!=null) return _wglLoadDisplayColorTableEXT (table, length); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglLoadDisplayColorTableEXT"));}
public static string wglGetExtensionsStringEXT (){ if (_wglGetExtensionsStringEXT!=null) return _wglGetExtensionsStringEXT (); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetExtensionsStringEXT"));}
public static IntPtr wglGetCurrentReadDCEXT (IntPtr v0){ if (_wglGetCurrentReadDCEXT!=null) return _wglGetCurrentReadDCEXT (v0); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetCurrentReadDCEXT"));}
public static bool wglMakeContextCurrentEXT (IntPtr hDrawDC, IntPtr hReadDC, IntPtr hglrc){ if (_wglMakeContextCurrentEXT!=null) return _wglMakeContextCurrentEXT (hDrawDC, hReadDC, hglrc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglMakeContextCurrentEXT"));}
public static IntPtr wglCreatePbufferEXT (IntPtr hDC, int iPixelFormat, int iWidth, int iHeight, IntPtr piAttribList){ if (_wglCreatePbufferEXT!=null) return _wglCreatePbufferEXT (hDC, iPixelFormat, iWidth, iHeight, piAttribList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreatePbufferEXT"));}
public static bool wglDestroyPbufferEXT (IntPtr hPbuffer){ if (_wglDestroyPbufferEXT!=null) return _wglDestroyPbufferEXT (hPbuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDestroyPbufferEXT"));}
public static IntPtr wglGetPbufferDCEXT (IntPtr hPbuffer){ if (_wglGetPbufferDCEXT!=null) return _wglGetPbufferDCEXT (hPbuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPbufferDCEXT"));}
public static bool wglQueryPbufferEXT (IntPtr hPbuffer, int iAttribute, IntPtr piValue){ if (_wglQueryPbufferEXT!=null) return _wglQueryPbufferEXT (hPbuffer, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryPbufferEXT"));}
public static int wglReleasePbufferDCEXT (IntPtr hPbuffer, IntPtr hDC){ if (_wglReleasePbufferDCEXT!=null) return _wglReleasePbufferDCEXT (hPbuffer, hDC); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleasePbufferDCEXT"));}
public static bool wglChoosePixelFormatEXT (IntPtr hdc, IntPtr piAttribIList, IntPtr pfAttribFList, uint nMaxFormats, IntPtr piFormats, IntPtr nNumFormats){ if (_wglChoosePixelFormatEXT!=null) return _wglChoosePixelFormatEXT (hdc, piAttribIList, pfAttribFList, nMaxFormats, piFormats, nNumFormats); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglChoosePixelFormatEXT"));}
public static bool wglGetPixelFormatAttribfvEXT (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr pfValues){ if (_wglGetPixelFormatAttribfvEXT!=null) return _wglGetPixelFormatAttribfvEXT (hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, pfValues); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPixelFormatAttribfvEXT"));}
public static bool wglGetPixelFormatAttribivEXT (IntPtr hdc, int iPixelFormat, int iLayerPlane, uint nAttributes, IntPtr piAttributes, IntPtr piValues){ if (_wglGetPixelFormatAttribivEXT!=null) return _wglGetPixelFormatAttribivEXT (hdc, iPixelFormat, iLayerPlane, nAttributes, piAttributes, piValues); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetPixelFormatAttribivEXT"));}
public static int wglGetSwapIntervalEXT (){ if (_wglGetSwapIntervalEXT!=null) return _wglGetSwapIntervalEXT (); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetSwapIntervalEXT"));}
public static bool wglSwapIntervalEXT (int interval){ if (_wglSwapIntervalEXT!=null) return _wglSwapIntervalEXT (interval); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSwapIntervalEXT"));}
public static bool wglGetDigitalVideoParametersI3D (IntPtr hDC, int iAttribute, IntPtr piValue){ if (_wglGetDigitalVideoParametersI3D!=null) return _wglGetDigitalVideoParametersI3D (hDC, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetDigitalVideoParametersI3D"));}
public static bool wglSetDigitalVideoParametersI3D (IntPtr hDC, int iAttribute, IntPtr piValue){ if (_wglSetDigitalVideoParametersI3D!=null) return _wglSetDigitalVideoParametersI3D (hDC, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSetDigitalVideoParametersI3D"));}
public static bool wglGetGammaTableI3D (IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue){ if (_wglGetGammaTableI3D!=null) return _wglGetGammaTableI3D (hDC, iEntries, puRed, puGreen, puBlue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGammaTableI3D"));}
public static bool wglGetGammaTableParametersI3D (IntPtr hDC, int iAttribute, IntPtr piValue){ if (_wglGetGammaTableParametersI3D!=null) return _wglGetGammaTableParametersI3D (hDC, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGammaTableParametersI3D"));}
public static bool wglSetGammaTableI3D (IntPtr hDC, int iEntries, IntPtr puRed, IntPtr puGreen, IntPtr puBlue){ if (_wglSetGammaTableI3D!=null) return _wglSetGammaTableI3D (hDC, iEntries, puRed, puGreen, puBlue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSetGammaTableI3D"));}
public static bool wglSetGammaTableParametersI3D (IntPtr hDC, int iAttribute, IntPtr piValue){ if (_wglSetGammaTableParametersI3D!=null) return _wglSetGammaTableParametersI3D (hDC, iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSetGammaTableParametersI3D"));}
public static bool wglDisableGenlockI3D (IntPtr hDC){ if (_wglDisableGenlockI3D!=null) return _wglDisableGenlockI3D (hDC); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDisableGenlockI3D"));}
public static bool wglEnableGenlockI3D (IntPtr hDC){ if (_wglEnableGenlockI3D!=null) return _wglEnableGenlockI3D (hDC); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnableGenlockI3D"));}
public static bool wglGenlockSampleRateI3D (IntPtr hDC, uint uRate){ if (_wglGenlockSampleRateI3D!=null) return _wglGenlockSampleRateI3D (hDC, uRate); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGenlockSampleRateI3D"));}
public static bool wglGenlockSourceDelayI3D (IntPtr hDC, uint uDelay){ if (_wglGenlockSourceDelayI3D!=null) return _wglGenlockSourceDelayI3D (hDC, uDelay); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGenlockSourceDelayI3D"));}
public static bool wglGenlockSourceEdgeI3D (IntPtr hDC, uint uEdge){ if (_wglGenlockSourceEdgeI3D!=null) return _wglGenlockSourceEdgeI3D (hDC, uEdge); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGenlockSourceEdgeI3D"));}
public static bool wglGenlockSourceI3D (IntPtr hDC, uint uSource){ if (_wglGenlockSourceI3D!=null) return _wglGenlockSourceI3D (hDC, uSource); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGenlockSourceI3D"));}
public static bool wglGetGenlockSampleRateI3D (IntPtr hDC, IntPtr uRate){ if (_wglGetGenlockSampleRateI3D!=null) return _wglGetGenlockSampleRateI3D (hDC, uRate); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGenlockSampleRateI3D"));}
public static bool wglGetGenlockSourceDelayI3D (IntPtr hDC, IntPtr uDelay){ if (_wglGetGenlockSourceDelayI3D!=null) return _wglGetGenlockSourceDelayI3D (hDC, uDelay); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGenlockSourceDelayI3D"));}
public static bool wglGetGenlockSourceEdgeI3D (IntPtr hDC, IntPtr uEdge){ if (_wglGetGenlockSourceEdgeI3D!=null) return _wglGetGenlockSourceEdgeI3D (hDC, uEdge); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGenlockSourceEdgeI3D"));}
public static bool wglGetGenlockSourceI3D (IntPtr hDC, IntPtr uSource){ if (_wglGetGenlockSourceI3D!=null) return _wglGetGenlockSourceI3D (hDC, uSource); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetGenlockSourceI3D"));}
public static bool wglIsEnabledGenlockI3D (IntPtr hDC, IntPtr pFlag){ if (_wglIsEnabledGenlockI3D!=null) return _wglIsEnabledGenlockI3D (hDC, pFlag); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglIsEnabledGenlockI3D"));}
public static bool wglQueryGenlockMaxSourceDelayI3D (IntPtr hDC, IntPtr uMaxLineDelay, IntPtr uMaxPixelDelay){ if (_wglQueryGenlockMaxSourceDelayI3D!=null) return _wglQueryGenlockMaxSourceDelayI3D (hDC, uMaxLineDelay, uMaxPixelDelay); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryGenlockMaxSourceDelayI3D"));}
public static bool wglAssociateImageBufferEventsI3D (IntPtr hdc, IntPtr pEvent, IntPtr pAddress, IntPtr pSize, uint count){ if (_wglAssociateImageBufferEventsI3D!=null) return _wglAssociateImageBufferEventsI3D (hdc, pEvent, pAddress, pSize, count); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglAssociateImageBufferEventsI3D"));}
public static IntPtr wglCreateImageBufferI3D (IntPtr hDC, int dwSize, uint uFlags){ if (_wglCreateImageBufferI3D!=null) return _wglCreateImageBufferI3D (hDC, dwSize, uFlags); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreateImageBufferI3D"));}
public static bool wglDestroyImageBufferI3D (IntPtr hDC, IntPtr pAddress){ if (_wglDestroyImageBufferI3D!=null) return _wglDestroyImageBufferI3D (hDC, pAddress); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDestroyImageBufferI3D"));}
public static bool wglReleaseImageBufferEventsI3D (IntPtr hdc, IntPtr pAddress, uint count){ if (_wglReleaseImageBufferEventsI3D!=null) return _wglReleaseImageBufferEventsI3D (hdc, pAddress, count); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleaseImageBufferEventsI3D"));}
public static bool wglDisableFrameLockI3D (IntPtr v0){ if (_wglDisableFrameLockI3D!=null) return _wglDisableFrameLockI3D (v0); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDisableFrameLockI3D"));}
public static bool wglEnableFrameLockI3D (IntPtr v0){ if (_wglEnableFrameLockI3D!=null) return _wglEnableFrameLockI3D (v0); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnableFrameLockI3D"));}
public static bool wglIsEnabledFrameLockI3D (IntPtr pFlag){ if (_wglIsEnabledFrameLockI3D!=null) return _wglIsEnabledFrameLockI3D (pFlag); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglIsEnabledFrameLockI3D"));}
public static bool wglQueryFrameLockMasterI3D (IntPtr pFlag){ if (_wglQueryFrameLockMasterI3D!=null) return _wglQueryFrameLockMasterI3D (pFlag); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryFrameLockMasterI3D"));}
public static bool wglBeginFrameTrackingI3D (){ if (_wglBeginFrameTrackingI3D!=null) return _wglBeginFrameTrackingI3D (); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBeginFrameTrackingI3D"));}
public static bool wglEndFrameTrackingI3D (){ if (_wglEndFrameTrackingI3D!=null) return _wglEndFrameTrackingI3D (); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEndFrameTrackingI3D"));}
public static bool wglGetFrameUsageI3D (IntPtr pUsage){ if (_wglGetFrameUsageI3D!=null) return _wglGetFrameUsageI3D (pUsage); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetFrameUsageI3D"));}
public static bool wglQueryFrameTrackingI3D (IntPtr pFrameCount, IntPtr pMissedFrames, IntPtr pLastMissedUsage){ if (_wglQueryFrameTrackingI3D!=null) return _wglQueryFrameTrackingI3D (pFrameCount, pMissedFrames, pLastMissedUsage); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryFrameTrackingI3D"));}
public static IntPtr wglCreateAffinityDCNV (IntPtr phGpuList){ if (_wglCreateAffinityDCNV!=null) return _wglCreateAffinityDCNV (phGpuList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglCreateAffinityDCNV"));}
public static bool wglDeleteDCNV (IntPtr hdc){ if (_wglDeleteDCNV!=null) return _wglDeleteDCNV (hdc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglDeleteDCNV"));}
public static bool wglEnumGpuDevicesNV (IntPtr hGpu, uint iDeviceIndex, IntPtr lpGpuDevice){ if (_wglEnumGpuDevicesNV!=null) return _wglEnumGpuDevicesNV (hGpu, iDeviceIndex, lpGpuDevice); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnumGpuDevicesNV"));}
public static bool wglEnumGpusFromAffinityDCNV (IntPtr hAffinityDC, uint iGpuIndex, IntPtr hGpu){ if (_wglEnumGpusFromAffinityDCNV!=null) return _wglEnumGpusFromAffinityDCNV (hAffinityDC, iGpuIndex, hGpu); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnumGpusFromAffinityDCNV"));}
public static bool wglEnumGpusNV (uint iGpuIndex, IntPtr phGpu){ if (_wglEnumGpusNV!=null) return _wglEnumGpusNV (iGpuIndex, phGpu); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnumGpusNV"));}
public static bool wglBindVideoDeviceNV (IntPtr hDc, uint  uVideoSlot, IntPtr hVideoDevice, IntPtr piAttribList){ if (_wglBindVideoDeviceNV!=null) return _wglBindVideoDeviceNV (hDc,  uVideoSlot,  hVideoDevice, piAttribList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBindVideoDeviceNV"));}
public static int wglEnumerateVideoDevicesNV (IntPtr hDc, IntPtr phDeviceList){ if (_wglEnumerateVideoDevicesNV!=null) return _wglEnumerateVideoDevicesNV (hDc, phDeviceList); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglEnumerateVideoDevicesNV"));}
public static bool wglQueryCurrentContextNV (int iAttribute, IntPtr piValue){ if (_wglQueryCurrentContextNV!=null) return _wglQueryCurrentContextNV (iAttribute, piValue); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryCurrentContextNV"));}
public static bool wglBindSwapBarrierNV (uint group, uint barrier){ if (_wglBindSwapBarrierNV!=null) return _wglBindSwapBarrierNV (group, barrier); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBindSwapBarrierNV"));}
public static bool wglJoinSwapGroupNV (IntPtr hDC, uint group){ if (_wglJoinSwapGroupNV!=null) return _wglJoinSwapGroupNV (hDC, group); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglJoinSwapGroupNV"));}
public static bool wglQueryFrameCountNV (IntPtr hDC, IntPtr count){ if (_wglQueryFrameCountNV!=null) return _wglQueryFrameCountNV (hDC, count); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryFrameCountNV"));}
public static bool wglQueryMaxSwapGroupsNV (IntPtr hDC, IntPtr maxGroups, IntPtr maxBarriers){ if (_wglQueryMaxSwapGroupsNV!=null) return _wglQueryMaxSwapGroupsNV (hDC, maxGroups, maxBarriers); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQueryMaxSwapGroupsNV"));}
public static bool wglQuerySwapGroupNV (IntPtr hDC, IntPtr group){ if (_wglQuerySwapGroupNV!=null) return _wglQuerySwapGroupNV (hDC, group); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglQuerySwapGroupNV"));}
public static bool wglResetFrameCountNV (IntPtr hDC){ if (_wglResetFrameCountNV!=null) return _wglResetFrameCountNV (hDC); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglResetFrameCountNV"));}
public static IntPtr wglAllocateMemoryNV (int size, float readFrequency, float writeFrequency, float priority){ if (_wglAllocateMemoryNV!=null) return _wglAllocateMemoryNV (size, readFrequency, writeFrequency, priority); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglAllocateMemoryNV"));}
public static IntPtr wglFreeMemoryNV (IntPtr pointer){ if (_wglFreeMemoryNV!=null) return _wglFreeMemoryNV (pointer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglFreeMemoryNV"));}
public static bool wglBindVideoImageNV (IntPtr hVideoDevice, IntPtr hPbuffer, int iVideoBuffer){ if (_wglBindVideoImageNV!=null) return _wglBindVideoImageNV ( hVideoDevice, hPbuffer, iVideoBuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglBindVideoImageNV"));}
public static bool wglGetVideoDeviceNV (IntPtr hDC, int numDevices, IntPtr hVideoDevice){ if (_wglGetVideoDeviceNV!=null) return _wglGetVideoDeviceNV (hDC, numDevices, hVideoDevice); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetVideoDeviceNV"));}
public static bool wglGetVideoInfoNV (IntPtr IntPtrice, ulong  pulCounterOutputPbuffer, ulong pulCounterOutputVideo){ if (_wglGetVideoInfoNV!=null) return _wglGetVideoInfoNV ( IntPtrice,   pulCounterOutputPbuffer,  pulCounterOutputVideo); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetVideoInfoNV"));}
public static bool wglReleaseVideoDeviceNV (IntPtr hVideoDevice){ if (_wglReleaseVideoDeviceNV!=null) return _wglReleaseVideoDeviceNV (hVideoDevice); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleaseVideoDeviceNV"));}
public static bool wglReleaseVideoImageNV (IntPtr hPbuffer, int iVideoBuffer){ if (_wglReleaseVideoImageNV!=null) return _wglReleaseVideoImageNV (hPbuffer, iVideoBuffer); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglReleaseVideoImageNV"));}
public static bool wglSendPbufferToVideoNV (IntPtr hPbuffer, int iBufferType, ulong  pulCounterPbuffer, bool bBlock){ if (_wglSendPbufferToVideoNV!=null) return _wglSendPbufferToVideoNV (hPbuffer, iBufferType,   pulCounterPbuffer, bBlock); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSendPbufferToVideoNV"));}
public static bool wglGetMscRateOML (IntPtr hdc, IntPtr numerator, IntPtr denominator){ if (_wglGetMscRateOML!=null) return _wglGetMscRateOML (hdc, numerator, denominator); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetMscRateOML"));}
public static bool wglGetSyncValuesOML (IntPtr hdc, IntPtr ust, IntPtr msc, IntPtr sbc){ if (_wglGetSyncValuesOML!=null) return _wglGetSyncValuesOML (hdc, ust, msc, sbc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglGetSyncValuesOML"));}
public static Int64 wglSwapBuffersMscOML (IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder){ if (_wglSwapBuffersMscOML!=null) return _wglSwapBuffersMscOML (hdc, target_msc, divisor, remainder); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSwapBuffersMscOML"));}
public static Int64 wglSwapLayerBuffersMscOML (IntPtr hdc, int fuPlanes, Int64 target_msc, Int64 divisor, Int64 remainder){ if (_wglSwapLayerBuffersMscOML!=null) return _wglSwapLayerBuffersMscOML (hdc, fuPlanes, target_msc, divisor, remainder); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglSwapLayerBuffersMscOML"));}
public static bool wglWaitForMscOML (IntPtr hdc, Int64 target_msc, Int64 divisor, Int64 remainder, IntPtr ust, IntPtr msc, IntPtr sbc){ if (_wglWaitForMscOML!=null) return _wglWaitForMscOML (hdc, target_msc, divisor, remainder, ust, msc, sbc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglWaitForMscOML"));}
public static bool wglWaitForSbcOML (IntPtr hdc, Int64 target_sbc, IntPtr ust, IntPtr msc, IntPtr sbc){ if (_wglWaitForSbcOML!=null) return _wglWaitForSbcOML (hdc, target_sbc, ust, msc, sbc); else throw new GLException (enuGLError.NotImplement, string.Format (Properties.Resources.GLError_NotImplement ,"wglWaitForSbcOML"));}
}
}
#pragma warning enable

