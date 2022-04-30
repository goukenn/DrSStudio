

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XGL.cs
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
file:XGL.cs
*/
using System;
using System.Runtime.InteropServices ;
#pragma warning disable
namespace IGK.GLLib
{
	public static class XGL
	{
		const string LIBNAME = "libGL.so.1";
		[DllImport(LIBNAME)]
		public static extern IntPtr glXGetProcAddress(string name);
		[DllImport(LIBNAME)]public static extern bool glXMakeCurrent(IntPtr display,IntPtr drawable,IntPtr context);
		[DllImport(LIBNAME)]public static extern bool glXQueryExtension(IntPtr display, ref int errorBase, ref int eventBase);
		[DllImport(LIBNAME)]public static extern bool glXQueryVersion(IntPtr display, out int major, out int minor);
		[DllImport(LIBNAME)]public static extern string glXGetClientString(IntPtr display, int name);
		[DllImport(LIBNAME)]public static extern string glXQueryServerString(IntPtr display, int screen, int name);
		[DllImport(LIBNAME)]public static extern string glXQueryExtensionsString(IntPtr display, int scree);
		[DllImport(LIBNAME)] public static extern IntPtr glXGetFBConfigs(IntPtr display, int screen, ref int nelements);
		/// <summary>
		/// Gls the X choose FB config.
		/// </summary>
		/// <returns>
		/// The X choose FB config.
		/// </returns>
		/// <param name='display'>
		/// Display.
		/// </param>
		/// <param name='screen'>
		/// Screen.
		/// </param>
		/// <param name='nelements'>
		/// Nelements.
		/// </param>
		/// <param name='retNums'>
		/// Ret nums.
		/// </param>
		[DllImport(LIBNAME)] public static extern IntPtr glXChooseFBConfig(IntPtr display, int screen, int[] nelements, ref int retNums);
		[DllImport(LIBNAME)] public static extern IntPtr glXCreateWindow(IntPtr display, IntPtr glxFBConfig, IntPtr hwnd, IntPtr  attribList);
		[DllImport(LIBNAME)] public static extern void glXSwapBuffers(IntPtr display, IntPtr drawable);
		[DllImport(LIBNAME)] public static extern void glXUseXFont(IntPtr font,int first, int count, int listbase);
		//destruction
		[DllImport(LIBNAME)] public static extern void glXDestroyWindow(IntPtr display, IntPtr xwindow);
		[DllImport(LIBNAME)] public static extern void glXDestroyPixmap(IntPtr display, IntPtr pixmap);
		[DllImport(LIBNAME)] public static extern void glXDestroyPbuffer(IntPtr display, IntPtr buffer);
		//sync
		[DllImport(LIBNAME)] public static extern void glXWaitGL();
		[DllImport(LIBNAME)] public static extern void glXWaitX();
		//context
		[DllImport(LIBNAME)] public static extern bool glXIsDirect(IntPtr display, IntPtr glContext);
		[DllImport(LIBNAME)] public static extern void glXDestroyContext(IntPtr display, IntPtr glContext);
		[DllImport(LIBNAME)] public static extern IntPtr glXGetCurrentDrawable();
		[DllImport(LIBNAME)] public static extern bool glXMakeContextCurrent(IntPtr display, IntPtr drawable, IntPtr readDrawable, IntPtr context);
		[DllImport(LIBNAME)] public static extern IntPtr glXGetCurrentReadDrawable();
		[DllImport(LIBNAME)] public static extern IntPtr glXCreateNewContext(IntPtr display, IntPtr config, enuXGLType renderType, IntPtr context, bool direct);
		/// <summary>
		/// Gls the X get current context.
		/// </summary>
		/// <returns>
		/// The X get current context.
		/// </returns>
		[DllImport(LIBNAME)] public static extern IntPtr glXGetCurrentContext();
		/// <summary>
		/// Gls the X get current display.
		/// </summary>
		/// <returns>
		/// The X get current display.
		/// </returns>
		[DllImport(LIBNAME)] public static extern IntPtr glXGetCurrentDisplay();
		/*
 * Mesa 3-D graphics library
 * Version:  6.5
 * 
 * Copyright (C) 1999-2006  Brian Paul   All Rights Reserved.
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
 * Tokens for glXChooseVisual and glXGetConfig:
 */
internal const int  GLX_USE_GL		=1;
internal const int  GLX_BUFFER_SIZE	=	2;
internal const int  GLX_LEVEL		=3;
internal const int  GLX_RGBA		=4;
internal const int  GLX_DOUBLEBUFFER=	5;
internal const int  GLX_STEREO		=6;
internal const int  GLX_AUX_BUFFERS	=	7;
internal const int  GLX_RED_SIZE		=8;
internal const int  GLX_GREEN_SIZE		=9;
internal const int  GLX_BLUE_SIZE		=10;
internal const int  GLX_ALPHA_SIZE		=11;
internal const int  GLX_DEPTH_SIZE		=12;
internal const int  GLX_STENCIL_SIZE	=13;
internal const int  GLX_ACCUM_RED_SIZE	=14;
internal const int  GLX_ACCUM_GREEN_SIZE=	15;
internal const int  GLX_ACCUM_BLUE_SIZE	=16;
internal const int  GLX_ACCUM_ALPHA_SIZE	=17;
/*
 * Error codes returned by glXGetConfig:
 */
internal const int  GLX_BAD_SCREEN		=1;
internal const int  GLX_BAD_ATTRIBUTE	=2;
internal const int  GLX_NO_EXTENSION	=3;
internal const int  GLX_BAD_VISUAL		=4;
internal const int  GLX_BAD_CONTEXT		=5;
internal const int  GLX_BAD_VALUE      = 	6;
internal const int  GLX_BAD_ENUM	=	7;
/*
 * GLX 1.1 and later:
 */
internal const int  GLX_VENDOR		=1;
internal const int  GLX_VERSION		=2;
internal const int  GLX_EXTENSIONS 		=3;
/*
 * GLX 1.3 and later:
 */
internal const int  GLX_CONFIG_CAVEAT=	0x20;
internal const uint  GLX_DONT_CARE=			0xFFFFFFFF;
internal const int  GLX_X_VISUAL_TYPE		=0x22;
internal const int  GLX_TRANSPARENT_TYPE		=0x23;
internal const int  GLX_TRANSPARENT_INDEX_VALUE	=0x24;
internal const int  GLX_TRANSPARENT_RED_VALUE	=0x25;
internal const int  GLX_TRANSPARENT_GREEN_VALUE	=0x26;
internal const int  GLX_TRANSPARENT_BLUE_VALUE	=0x27;
internal const int  GLX_TRANSPARENT_ALPHA_VALUE	=0x28;
internal const int  GLX_WINDOW_BIT			=0x00000001;
internal const int  GLX_PIXMAP_BIT			=0x00000002;
internal const int  GLX_PBUFFER_BIT			=0x00000004;
internal const int  GLX_AUX_BUFFERS_BIT		=0x00000010;
internal const int  GLX_FRONT_LEFT_BUFFER_BIT	=0x00000001;
internal const int  GLX_FRONT_RIGHT_BUFFER_BIT	=0x00000002;
internal const int  GLX_BACK_LEFT_BUFFER_BIT	=0x00000004;
internal const int  GLX_BACK_RIGHT_BUFFER_BIT	=0x00000008;
internal const int  GLX_DEPTH_BUFFER_BIT		=0x00000020;
internal const int  GLX_STENCIL_BUFFER_BIT		=0x00000040;
internal const int  GLX_ACCUM_BUFFER_BIT		=0x00000080;
internal const int  GLX_NONE			=0x8000;
internal const int  GLX_SLOW_CONFIG			=0x8001;
internal const int  GLX_TRUE_COLOR			=0x8002;
internal const int  GLX_DIRECT_COLOR		=0x8003;
internal const int  GLX_PSEUDO_COLOR		=0x8004;
internal const int  GLX_STATIC_COLOR		=0x8005;
internal const int  GLX_GRAY_SCALE			=0x8006;
internal const int  GLX_STATIC_GRAY			=0x8007;
internal const int  GLX_TRANSPARENT_RGB		=0x8008;
internal const int  GLX_TRANSPARENT_INDEX		=0x8009;
internal const int  GLX_VISUAL_ID			=0x800B;
internal const int  GLX_SCREEN			=0x800C;
internal const int  GLX_NON_CONFORMANT_CONFIG	=0x800D;
internal const int  GLX_DRAWABLE_TYPE		=0x8010;
internal const int  GLX_RENDER_TYPE			=0x8011;
internal const int  GLX_X_RENDERABLE		=0x8012;
internal const int  GLX_FBCONFIG_ID			=0x8013;
internal const int  GLX_RGBA_TYPE			=0x8014;
internal const int  GLX_COLOR_INDEX_TYPE		=0x8015;
internal const int  GLX_MAX_PBUFFER_WIDTH		=0x8016;
internal const int  GLX_MAX_PBUFFER_HEIGHT		=0x8017;
internal const int  GLX_MAX_PBUFFER_PIXELS		=0x8018;
internal const int  GLX_PRESERVED_CONTENTS		=0x801B;
internal const int  GLX_LARGEST_PBUFFER		=0x801C;
internal const int  GLX_WIDTH			=0x801D;
internal const int  GLX_HEIGHT			=0x801E;
internal const int  GLX_EVENT_MASK			=0x801F;
internal const int  GLX_DAMAGED			=0x8020;
internal const int  GLX_SAVED			=0x8021;
internal const int  GLX_WINDOW			=0x8022;
internal const int  GLX_PBUFFER			=0x8023;
internal const int  GLX_PBUFFER_HEIGHT              =0x8040;
internal const int  GLX_PBUFFER_WIDTH               =0x8041;
internal const int  GLX_RGBA_BIT			=0x00000001;
internal const int  GLX_COLOR_INDEX_BIT		=0x00000002;
internal const int  GLX_PBUFFER_CLOBBER_MASK	=0x08000000;
/*
 * GLX 1.4 and later:
 */
internal const int  GLX_SAMPLE_BUFFERS              =0x186a0; /*100000*/
internal const int  GLX_SAMPLES                     =0x186a1; /*100001*/
				/*
typedef struct __GLXcontextRec *GLXContext;
typedef XID GLXPixmap;
typedef XID GLXDrawable;*/
/* GLX 1.3 and later */
/*typedef struct __GLXFBConfigRec *GLXFBConfig;
typedef XID GLXFBConfigID;
typedef XID GLXContextID;
typedef XID GLXWindow;
typedef XID GLXPbuffer;*/
/*
** Events.
** __GLX_NUMBER_EVENTS is set to 17 to account for the BufferClobberSGIX
**  event - this helps initialization if the server supports the pbuffer
**  extension and the client doesn't.
*/
internal const int  GLX_PbufferClobber	=0;
internal const int  GLX_BufferSwapComplete	=1;
internal const int  __GLX_NUMBER_EVENTS =17;
/*
 * #?. GLX_MESA_swap_control
 */
//#ifndef GLX_MESA_swap_control
internal const int  GLX_MESA_swap_control =1;
/*extern int glXSwapIntervalMESA(unsigned int interval);
extern int glXGetSwapIntervalMESA(void);
typedef int (*PFNGLXSWAPINTERVALMESAPROC)(unsigned int interval);
typedef int (*PFNGLXGETSWAPINTERVALMESAPROC)(void);*/
//#endif /* GLX_MESA_swap_control */
/*
 * #?. GLX_EXT_texture_from_pixmap
 * XXX not finished?
 */
//#ifndef GLX_EXT_texture_from_pixmap
internal const int  GLX_EXT_texture_from_pixmap =1;
internal const int  GLX_BIND_TO_TEXTURE_RGB_EXT        =0x20D0;
internal const int  GLX_BIND_TO_TEXTURE_RGBA_EXT       =0x20D1;
internal const int  GLX_BIND_TO_MIPMAP_TEXTURE_EXT     =0x20D2;
internal const int  GLX_BIND_TO_TEXTURE_TARGETS_EXT    =0x20D3;
internal const int  GLX_Y_INVERTED_EXT                 =0x20D4;
internal const int  GLX_TEXTURE_FORMAT_EXT             =0x20D5;
internal const int  GLX_TEXTURE_TARGET_EXT             =0x20D6;
internal const int  GLX_MIPMAP_TEXTURE_EXT             =0x20D7;
internal const int  GLX_TEXTURE_FORMAT_NONE_EXT        =0x20D8;
internal const int  GLX_TEXTURE_FORMAT_RGB_EXT         =0x20D9;
internal const int  GLX_TEXTURE_FORMAT_RGBA_EXT        =0x20DA;
internal const int  GLX_TEXTURE_1D_BIT_EXT             =0x00000001;
internal const int  GLX_TEXTURE_2D_BIT_EXT             =0x00000002;
internal const int  GLX_TEXTURE_RECTANGLE_BIT_EXT      =0x00000004;
internal const int  GLX_TEXTURE_1D_EXT                 =0x20DB;
internal const int  GLX_TEXTURE_2D_EXT                 =0x20DC;
internal const int  GLX_TEXTURE_RECTANGLE_EXT          =0x20DD;
internal const int  GLX_FRONT_LEFT_EXT                 =0x20DE;
internal const int  GLX_FRONT_RIGHT_EXT                =0x20DF;
internal const int  GLX_BACK_LEFT_EXT                  =0x20E0;
internal const int  GLX_BACK_RIGHT_EXT                 =0x20E1;
internal const int  GLX_FRONT_EXT                      =GLX_FRONT_LEFT_EXT;
internal const int  GLX_BACK_EXT                       =GLX_BACK_LEFT_EXT;
internal const int  GLX_AUX0_EXT                       =0x20E2;
internal const int  GLX_AUX1_EXT                       =0x20E3 ;
internal const int  GLX_AUX2_EXT                       =0x20E4 ;
internal const int  GLX_AUX3_EXT                       =0x20E5 ;
internal const int  GLX_AUX4_EXT                       =0x20E6 ;
internal const int  GLX_AUX5_EXT                       =0x20E7 ;
internal const int  GLX_AUX6_EXT                       =0x20E8;
internal const int  GLX_AUX7_EXT                       =0x20E9 ;
internal const int  GLX_AUX8_EXT                       =0x20EA ;
internal const int  GLX_AUX9_EXT                       =0x20EB;
/*** Should these go here, or in another header? */
/*
** GLX Events
*/
		[StructLayout(LayoutKind.Sequential, Pack=1)]
public   struct GLXPbufferClobberEvent{
    int event_type;		/* GLX_DAMAGED or GLX_SAVED */
    int draw_type;		/* GLX_WINDOW or GLX_PBUFFER */
    ulong serial;	/* # of last request processed by server */
    bool send_event;		/* true if this came for SendEvent request */
    IntPtr display;		/* display the event was read from */
    IntPtr  drawable;	/* XID of Drawable */
    uint buffer_mask;	/* mask indicating which buffers are affected */
    uint aux_buffer;	/* which aux buffer was affected */
    int x, y;
    int width, height;
    int count;			/* if nonzero, at least this many more */
} 
		[StructLayout(LayoutKind.Sequential, Pack=1)]
 public struct GLXBufferSwapComplete {
    int type;
    ulong serial;	/* # of last request processed by server */
    bool  send_event;		/* true if this came from a SendEvent request */
    IntPtr display;		/* Display the event was read from */
    IntPtr drawable;	/* drawable on which event was requested in event mask */
    int event_type;
    long ust;
    long msc;
    long sbc;
}
}
}

