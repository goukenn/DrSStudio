

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WGL.cs
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
file:WGL.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
using IGK.GLLib.WinUI;
#pragma warning disable
namespace IGK.GLLib
{
    [StructLayout(LayoutKind.Sequential )]
     internal struct WGLSWAP
    {
        IntPtr hdc;
        uint uiFlags;
    } 
    public static partial class WGL
    {       

        static Dictionary<string, MulticastDelegate> sm_additionalMethod;
        static Dictionary<string, string[]> procName;
		#if UNIX
		    const string LIBNAME = "libGL.so.1";
		    const string PROC_CREATE_CONTEXT = "glXglCreateContext";
        #else
		    const string LIBNAME  = "opengl32.dll";
		    const string PROC_CREATE_CONTEXT = "wglCreateContext";
        #endif

        static WGL() {
            procName = new Dictionary<string, string[]>();

            procName.Add("isWGL_ARB_create_context", new string[] { 
                "wglCreateContextAttribsARB"
            });


            InitMethods();
            Type t = typeof(IGK.GLLib.WGL);
            foreach (var item in procName)
            {
                bool b = true;
                foreach (string it2 in item.Value)
                {
                    if (!sm_additionalMethod.ContainsKey(it2)) {
                        b = false;
                        break;
                    }
                }
                var ff = t.GetField(item.Key, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Static);
                if (ff != null)
                    ff.SetValue(null, b);
            }
        }
        //public static void InitWGL(){
        //    //InitMethods ();
        //}
        private static void InitMethods()
        {
			sm_additionalMethod = new Dictionary<string, MulticastDelegate>();
            GLDeviceControl v_ctr = new GLDeviceControl();
            v_ctr.CreateControl();
            v_ctr.Show();
            IntPtr hwnd = v_ctr.Handle;
            IntPtr NULL = IntPtr.Zero;
            IntPtr hdc = IntPtr.Zero;
			#if OS_WINDOWS
            WGL.wglMakeCurrent(NULL, NULL);
            hdc = User32Lib.GetDC(hwnd);
            IntPtr gldc = GL.CreateGLDevice(hdc,
                1,
                32,
                24,
                0,
                enuGLPixelMode.RGBA,
                enuGLPlane.MainPlane);
		
            // /!\ important to get available method procedure
            WGL.wglMakeCurrent(hdc, gldc);

            Type t = typeof(IGK.GLLib.WGL);
            IntPtr v_proc = IntPtr.Zero ;
            string v_procName = string.Empty;
            //init static method
          
            FieldInfo[] c = t.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            Type v_baseType = Type.GetType("System.MulticastDelegate");
            //init delegate
            foreach (FieldInfo i in c)
            {
                try
                {
                    if (i.FieldType.IsSubclassOf(v_baseType))
                    {
                        v_procName = i.Name.Replace("_", "");
                        v_proc = wglGetProcAddress(v_procName);
                        if (v_proc != IntPtr.Zero )
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine(v_procName + " Registrated");
#endif

                            var v_del = Marshal.GetDelegateForFunctionPointer(v_proc , i.FieldType);
                            i.SetValue(null, v_del);// v_proc);
                            if (!sm_additionalMethod.ContainsKey(v_procName))
                            {
                                sm_additionalMethod.Add(v_procName, v_del as MulticastDelegate);
                             
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(v_procName + " Not Registrated Registrated" + ex.Message);
#else 
                    System.Console.WriteLine(v_procName + " Not Registrated Registrated : "+ ex.Message);
#endif
                }
            }
            WGL.wglMakeCurrent(NULL, NULL);
            GL.Destroy(hwnd, hdc, gldc);
            bool ccc = User32Lib.ReleaseDC(hwnd, hdc);
            v_ctr.Dispose();
#endif
        }
        [DllImport(LIBNAME, EntryPoint =PROC_CREATE_CONTEXT)]
        public  static extern IntPtr wglCreateContext(IntPtr hdc);
        [DllImport(LIBNAME)]
        public  static extern IntPtr wglGetCurrentContext();
        [DllImport(LIBNAME)]
        public  static extern bool wglDeleteContext(IntPtr hopenGLdc);
#if OS_WINDOWS
        [DllImport(LIBNAME)]
        public  static extern bool wglMakeCurrent(IntPtr hdc, IntPtr hopenGLdc);

#elif UNIX
public static bool wglMakeCurrent(IntPtr c, IntPtr gldc){
	var disp = IntPtr.Zero;
	bool o =  XGL.glXMakeContextCurrent (disp, c, c, gldc);
	return o;
}
#endif
        [DllImport(LIBNAME)]
        public  static extern bool wglUseFontBitmaps(IntPtr hdc, uint first, uint count, uint listbase);        
        [DllImport(LIBNAME)]
        public  static extern bool wglUseFontOutlines(IntPtr hdc, uint first, uint count, uint listbase, float deviation, float extrusion, int format, IntPtr tab);         
         [DllImport(LIBNAME)]
        internal static extern bool wglUseFontOutlines(IntPtr hdc, uint first, uint count, uint listbase, float deviation, float extrusion, int format,ref GLYPHMETRICSFLOAT[] tab);
        [DllImport(LIBNAME)]
        public  static extern IntPtr wglGetProcAddress(string methodname);
         [DllImport(LIBNAME)]
        public  static extern IntPtr wglCreateLayerContext(IntPtr HDC, int id);
        [DllImport(LIBNAME)]
        public  static extern IntPtr wglGetCurrentDC();
        [DllImport(LIBNAME)]
        public  static extern bool wglShareLists(IntPtr HGLRC, IntPtr HGLToShareRC);
         /* wglSwapLayerBuffers flags */
        internal const int  WGL_SWAP_MAIN_PLANE =    0x00000001;
        internal const int  WGL_SWAP_OVERLAY1  =     0x00000002;
        internal const int  WGL_SWAP_OVERLAY2 =      0x00000004;
        internal const int  WGL_SWAP_OVERLAY3=       0x00000008;
        internal const int  WGL_SWAP_OVERLAY4      = 0x00000010;
        internal const int  WGL_SWAP_OVERLAY5     =  0x00000020;
        internal const int  WGL_SWAP_OVERLAY6    =   0x00000040;
        internal const int  WGL_SWAP_OVERLAY7   =    0x00000080;
        internal const int  WGL_SWAP_OVERLAY8  =     0x00000100;
        internal const int  WGL_SWAP_OVERLAY9 =      0x00000200;
        internal const int  WGL_SWAP_OVERLAY10     = 0x00000400;
        internal const int  WGL_SWAP_OVERLAY11    =  0x00000800;
        internal const int  WGL_SWAP_OVERLAY12   =   0x00001000;
        internal const int  WGL_SWAP_OVERLAY13  =    0x00002000;
        internal const int  WGL_SWAP_OVERLAY14 =     0x00004000;
        internal const int  WGL_SWAP_OVERLAY15=      0x00008000;
        internal const int  WGL_SWAP_UNDERLAY1     = 0x00010000;
        internal const int  WGL_SWAP_UNDERLAY2     = 0x00020000;
        internal const int  WGL_SWAP_UNDERLAY3     = 0x00040000;
        internal const int  WGL_SWAP_UNDERLAY4     = 0x00080000;
        internal const int  WGL_SWAP_UNDERLAY5     = 0x00100000;
        internal const int  WGL_SWAP_UNDERLAY6     = 0x00200000;
        internal const int  WGL_SWAP_UNDERLAY7     = 0x00400000;
        internal const int  WGL_SWAP_UNDERLAY8     = 0x00800000;
        internal const int  WGL_SWAP_UNDERLAY9     = 0x01000000;
        internal const int  WGL_SWAP_UNDERLAY10   =  0x02000000;
        internal const int  WGL_SWAP_UNDERLAY11   =  0x04000000;
        internal const int  WGL_SWAP_UNDERLAY12   =  0x08000000;
        internal const int  WGL_SWAP_UNDERLAY13   =  0x10000000;
        internal const int  WGL_SWAP_UNDERLAY14  =   0x20000000;
        internal const int  WGL_SWAP_UNDERLAY15      =   0x40000000;
        [DllImport(LIBNAME)]
        internal static extern bool  wglDescribeLayerPlane(IntPtr HDC, int c, int a, int d ,ref LAYERPLANEDESCRIPTOR layerplane);
        [DllImport(LIBNAME)]
        internal static extern int  wglSetLayerPaletteEntries(IntPtr HDC, int v1, int v2, int v3, System.Drawing.Color [] colors);
        [DllImport(LIBNAME)]
        internal static extern int wglGetLayerPaletteEntries(IntPtr HDC, int v1, int v2, int v3, ref System.Drawing.Color[] color);
        [DllImport(LIBNAME)]
        internal  static extern bool wglRealizeLayerPalette(IntPtr HDC, int v1, bool v2);
        [DllImport(LIBNAME)]
        internal static extern bool wglSwapLayerBuffers(IntPtr HDC, uint layerindex);
        internal const int  WGL_SWAPMULTIPLE_MAX = 16;
        [DllImport(LIBNAME)]
        internal static extern int wglSwapMultipleBuffers(uint index, WGLSWAP[] sa);
        public  const int WGL_FONT_POLYGONS = 1;
        public  const int WGL_FONT_LINES = 0;
    }
}

