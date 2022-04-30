

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: glew.4.0.partial.cs
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
file:glew.4.0.partial.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
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
using System.Reflection;
using System.Runtime.InteropServices ;
using System.IO;
using System.Windows.Forms;

using IGK.GLLib.WinUI;
using System.Diagnostics;

#pragma warning disable IDE1006 // Naming Styles

namespace IGK.GLLib
{
    //extend basic declaration by create overriding method
    /// <summary>
    /// represent the base class GL wrapper
    /// </summary>
    public static partial class GL
    {
		public delegate IntPtr PFNGETCURRENTDC();
		public delegate IntPtr PFNGETCURRENTCONTEXT();
		public delegate bool PFNMAKECURRENT(IntPtr context);
		public delegate IntPtr PFNGETPROCADDRESS(string name);
		//utils pointer
#if OS_WINDOWS
		public static PFNGETCURRENTDC GetCurrentDC = WGL.wglGetCurrentDC;
		public static PFNGETCURRENTCONTEXT GetCurrentContext = WGL.wglGetCurrentContext;
        public static PFNGETPROCADDRESS GetProcAddress = WGL.wglGetProcAddress;
		 internal static bool MakeCurrent(IntPtr hdc, IntPtr gldc )
        {
            return WGL.wglMakeCurrent(hdc,gldc);
        }
		
#elif UNIX
		public static PFNGETCURRENTDC GetCurrentContext = XGL.glXGetCurrentContext;
		public static PFNMAKECURRENT MakeCurrent = LNX_MakerCurrent;
		static PFNGETPROCADDRESS GetProcAddress = XGL.glXGetProcAddress ;
		static Dictionary <IntPtr , LnxConnexionInfo> sm_connexion = new Dictionary<IntPtr, LnxConnexionInfo> ();
		static bool LNX_MakerCurrent(IntPtr gldc){
			if (sm_connexion.ContainsKey(gldc))
			{
				LnxConnexionInfo v_info = sm_connexion[gldc] ;
				return XGL.glXMakeContextCurrent(v_info.display, v_info.drawable, v_info.drawable,gldc);
			}
			return false;
		}
        [StructLayout (LayoutKind.Sequential )]
		struct LnxConnexionInfo{
			internal IntPtr drawable;
			internal IntPtr display;
		}
#endif
         /// <summary>
         /// Destroy OpenGL Resource
         /// </summary>
         /// <param name="hwnd"></param>
         /// <param name="hdc"></param>
         /// <param name="gldc"></param>
         /// <returns></returns>
         public static bool Destroy(IntPtr hwnd, IntPtr hdc, IntPtr gldc)
         {
#if OS_WINDOWS
             if (MakeCurrent(NULL, NULL))
             {
                 if (WGL.wglDeleteContext(gldc))
                     if (User32Lib.ReleaseDC(hwnd, hdc))
                         return true;
             }
#elif UNIX


#endif
             return false;
         }

        public static readonly IntPtr NULL = IntPtr.Zero;
		#if UNIX 
		private const string GLLIB = "libGL.so.1";
		#elif OS_WINDOWS 
		private const string GLLIB  = "opengl32.dll";
		#endif
        static  GLVersion sm_glversion ;
        static string sm_version;
        static string sm_Renderer;
        static string sm_Vendor;
      // static readonly string sm_ShadingLanguageVersion;
        static string[] sm_ext;
        //static readonly GLDeviceControl sm_deviceControl;//for initialisation
        public static GLVersion GLVersion{get{return sm_glversion;}}
        /// <summary>
        /// get the gl version
        /// </summary>
        public static string Version { get { return sm_version; } }
        /// <summary>
        /// get the vendor version
        /// </summary>
        public static string Vendor { get { return sm_Vendor; } }
        /// <summary>
        /// get the renderer version
        /// </summary>
        public static string Renderer{ get { return sm_Renderer; } }
       // public static string ShadingLanguageVersion { get { return sm_ShadingLanguageVersion; } }
        /// <summary>
        /// get an array of extensions
        /// </summary>
        public static string[] Extensions{get{return sm_ext;}}
        private static Dictionary<string, MulticastDelegate> sm_additionalMethod;
        /// <summary>
        /// get if the additinal method is defined
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        public static bool IsMethodDefine(string methodName)
        {
            if (sm_additionalMethod == null)
                return false;
            if (string.IsNullOrEmpty(methodName))
                return false;
            return sm_additionalMethod.ContainsKey(methodName);
        }
		public static bool SupportMethod(string methodName)
        {
            IntPtr v = GetProcAddress(methodName);
            bool result = (v != NULL);
            return result;
        }
#if OS_WINDOWS
		private static void InitWindowGL()
		{
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Init OpenGL.........");

#endif
            //create a dummy windonw control and init gl in it
            GLDeviceControl v_ctr = new GLDeviceControl();
            v_ctr.CreateControl();
            v_ctr.Show();
            IGLGraphicDevice device = CreateDevice(v_ctr.Handle, 32, 32,
                enuGLFlags.SupportGdi |
                enuGLFlags.DoubleBuffer |
                enuGLFlags.DrawToWindow |
                enuGLFlags.SupportOpenGL,
                enuGLPixelMode.RGBA, enuGLPlane.MainPlane, out IntPtr hdc, out IntPtr gldc, out IntPtr glpxFormat);
            if (device !=null)
            {
                //selection du device courant
                MakeCurrent(hdc, gldc);
                //initialization des methodes
                InitMethod();
                //initialization des variables
                Type t = typeof(IGK.GLLib.GL);
                Type tw = typeof(IGK.GLLib.WGL);
                string v_str = glGetString(GL_VERSION);
                sm_version = v_str;
                sm_Renderer = glGetString(GL_RENDERER);
                sm_Vendor = glGetString(GL_VENDOR);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(string.Format("Version : {0}", GL.glGetString(GL.GL_VERSION)));
#endif

            /*    sm_ShadingLanguageVersion = glGetString(GL_SHADING_LANGUAGE_VERSION);
				if (glGetError() != GL_NO_ERROR)
				{
					sm_ShadingLanguageVersion ="No Shading Language";	
				}*/
                string v_ext = glGetString(GL_EXTENSIONS);
                string v_gext = Marshal.PtrToStringAnsi(GLU.gluGetString(GLU.GLU_EXTENSIONS));
                List<string> l = new List<string>();
                v_ext = v_ext.Trim();
                v_gext = v_gext.Trim();
                //bluil all supported exteant
                l.AddRange(v_ext.Split(' '));
                l.AddRange(v_gext.Split(' '));
                sm_ext = l.ToArray();// (v_ext + " " + v_gext).Split(' ');
                l.Clear();
                v_str = v_str.Remove(3);
                sm_glversion = new GLVersion(v_str);
                FieldInfo f = null;
            //if (v_version>= GLVersion.V_1_1 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_1",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_2 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_2",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_3 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_3",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_4 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_4",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_5 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_5",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_2_0 )
            //{
            //    f = t.GetField ("isGL_VERSION_2_0",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_2_1 )
            //{
            //    f = t.GetField ("isGL_VERSION_2_1",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //if (v_version >= GLVersion.V_3_0)
            //{
            //    f = t.GetField("isGL_VERSION_3_0", BindingFlags.Public | BindingFlags.Static);
            //    if (f != null)
            //    {
            //        f.SetValue(null, true);
            //    }
            //}
#if DEBUG
                Debug.WriteLine("OpenGL Version : " + sm_glversion.ToString ());
#endif
                Debug.WriteLine("Set/Get OpengGL Extension capacity .....");
                foreach(string str in sm_ext)
                {
                    if (string .IsNullOrEmpty (str))
                        continue;
                    f = null;
                    if (str.StartsWith("GL_"))
                    {
                        v_str = str.Replace("GL_", "isGL_");
                        f = t.GetField(v_str, BindingFlags.Public | BindingFlags.Static);                   
                    }
                    else if (str.StartsWith ("WGL_"))
                    {
                        v_str = str.Replace("WGL_", "isWGL_");
                        f = tw.GetField(v_str, BindingFlags.Public | BindingFlags.Static);  
                    }
                     if (f != null)
                     {
                         f.SetValue(null, true);
                     }
#if DEBUG
                     else
                     {
                         System.Diagnostics.Debug.WriteLine("[GLEW] - Not found : " + v_str + " : "+f);
                     }
#endif
                }
                ////dectruction of dummy device
                Destroy(NULL,hdc,gldc);
                device.Dispose();
                v_ctr.Dispose();
                v_ctr = null;
            }
		}
#endif
		private static void InitLinuxGL()
		{
            System.Diagnostics.Debug.WriteLine("Init Linux OpenGL API ...............");
            GLDeviceControl v_sm_deviceControl = new GLDeviceControl();
            v_sm_deviceControl.CreateControl();
            v_sm_deviceControl.Show();
            IGLGraphicDevice  v_dev =  CreateDevice (v_sm_deviceControl.Handle, 32, 32, 
                enuGLFlags.SupportGdi | 
                enuGLFlags.DoubleBuffer | 
                enuGLFlags.DrawToWindow | 
                enuGLFlags.SupportOpenGL ,
                enuGLPixelMode.RGBA,enuGLPlane.MainPlane, out IntPtr hdc, out IntPtr gldc, out IntPtr glpxFormat );
			if (v_dev !=null)
            {
                //selection du device courant
                v_dev.MakeCurrent();
                //initialization des methodes
                InitMethod();
                //initialization des variables
                Type t = typeof(IGK.GLLib.GL);
                Type tw = typeof(IGK.GLLib.WGL);
                string v_str = glGetString(GL_VERSION);
                sm_version = v_str;
                sm_Renderer = glGetString(GL_RENDERER);
                sm_Vendor = glGetString(GL_VENDOR);
            /*    sm_ShadingLanguageVersion = glGetString(GL_SHADING_LANGUAGE_VERSION);
				if (glGetError() != GL_NO_ERROR)
				{
					sm_ShadingLanguageVersion ="No Shading Language";	
				}*/
                string v_ext = glGetString(GL_EXTENSIONS);
                //string v_gext = Marshal.PtrToStringAnsi(GLU.gluGetString(GLU.GLU_EXTENSIONS));
                List<string> l = new List<string>();
                v_ext = v_ext.Trim();
                //v_gext = v_gext.Trim();
                //bluil all supported exteant
                l.AddRange(v_ext.Split(' '));
                //l.AddRange(v_gext.Split(' '));
                sm_ext = l.ToArray();// (v_ext + " " + v_gext).Split(' ');
                l.Clear();
                v_str = v_str.Remove(3);
                sm_glversion = new GLVersion(v_str);
                FieldInfo f = null;
            //if (v_version>= GLVersion.V_1_1 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_1",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_2 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_2",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_3 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_3",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_4 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_4",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_1_5 )
            //{
            //    f = t.GetField ("isGL_VERSION_1_5",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_2_0 )
            //{
            //    f = t.GetField ("isGL_VERSION_2_0",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //    if (v_version>= GLVersion.V_2_1 )
            //{
            //    f = t.GetField ("isGL_VERSION_2_1",BindingFlags.Public| BindingFlags.Static);
            //    f.SetValue (null,true);
            //}
            //if (v_version >= GLVersion.V_3_0)
            //{
            //    f = t.GetField("isGL_VERSION_3_0", BindingFlags.Public | BindingFlags.Static);
            //    if (f != null)
            //    {
            //        f.SetValue(null, true);
            //    }
            //}
#if DEBUG
                System.Diagnostics.Debug.WriteLine("Set Get GL Extension Capaticy");
#endif
                foreach(string str in sm_ext)
                {
                    if (string .IsNullOrEmpty (str))
                        continue;
                    f = null;
                    if (str.StartsWith("GL_"))
                    {
                        v_str = str.Replace("GL_", "isGL_");
                        f = t.GetField(v_str, BindingFlags.Public | BindingFlags.Static);                   
                    }
                    else if (str.StartsWith ("WGL_"))
                    {
                        v_str = str.Replace("WGL_", "isWGL_");
                        f = tw.GetField(v_str, BindingFlags.Public | BindingFlags.Static);  
                    }
                     if (f != null)
                     {
                         f.SetValue(null, true);
                     }
#if DEBUG
                     else
                     {
                         System.Diagnostics.Debug.WriteLine("note found : " + v_str + " : "+f);
                     }
#endif
				v_dev.Dispose();
                }
                ////dectruction of dummy device
                //Destroy(NULL,hdc,gldc);
                v_sm_deviceControl.Dispose();
                v_sm_deviceControl = null;
            }
		}
        /// <summary>
        /// static constructor. to initialze event and method property
        /// </summary>
        static GL()
        {
#if OS_WINDOWS
			InitWindowGL();
#else 
    #if OS_LINUX
			    InitLinuxGL();
    #endif
#endif
        }
        public static string glGetString(uint value)
        {
            return Marshal.PtrToStringAnsi(_glGetString(value));
        }
        /// <summary>
        /// used to init addition OpenGL Method
        /// </summary>
        internal static void InitMethod()
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("InitMethod....");
#endif
            Type t = typeof(IGK.GLLib.GL);
            object v_proc = null;
            string v_procName = string.Empty;
            //init static method
            sm_additionalMethod = new Dictionary<string, MulticastDelegate>();
            FieldInfo[] c = t.GetFields(BindingFlags.NonPublic | BindingFlags.Static);
            Type v_baseType = Type.GetType("System.MulticastDelegate");
            //init delegate
            foreach (FieldInfo i in  c)
            {
                try
                {
                    if (i.FieldType.IsSubclassOf(v_baseType))
                    {
                        v_procName = i.Name.Replace("__", "");
                        v_proc = GetProcedure(v_procName, i.FieldType);
                        if (v_proc != null)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine($"glew : {v_procName} Registrated");
#endif
                            i.SetValue(null, v_proc);
                            if (!sm_additionalMethod.ContainsKey(v_procName))
                            {
                                sm_additionalMethod.Add(v_procName, v_proc as MulticastDelegate);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
#if DEBUG
                    System.Diagnostics.Debug.WriteLine(v_procName + " Not Registrated Registrated"+ ex.Message);
#else 
                    System.Console.WriteLine(v_procName + " Not Registrated Registrated : "+ ex.Message);
#endif
                }
            }
        }
        /// <summary>
        /// Cr�er une interface opengGL 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="bitcount"></param>
        /// <param name="depthmask"></param>
        /// <param name="flags"></param>
        /// <param name="pixel"></param>
        /// <param name="plane"></param>
        /// <param name="hdc"></param>
        /// <param name="gldc"></param>
        /// <returns>graphics device info</returns>
        public static IGLGraphicDevice CreateDevice(
            IntPtr hwnd , 
            byte bitcount, 
            byte depthmask,
            enuGLFlags flags,
            enuGLPixelMode pixel,
            enuGLPlane plane,
            out IntPtr hdc,
            out IntPtr gldc,
            out IntPtr glPxFormat
            )
        {
            gldc = NULL;
            hdc = NULL;
            glPxFormat = NULL;
#if OS_WINDOWS
            //creation du device context
            IntPtr v_currentDC = WGL.wglGetCurrentDC();
            IntPtr v_currentGldc = WGL.wglGetCurrentContext();
            //if (v_currentDC != NULL)
            WGL.wglMakeCurrent(NULL, NULL);
            hdc =User32Lib.GetDC(hwnd);
            if (hdc == NULL)
                return null;
            //selection du format de pixel
            gldc = CreateGLDevice(hdc, 1, bitcount, depthmask, flags, pixel, plane);
            if (gldc !=NULL)
            {
                GLWindowDevice v_deviceInfo = new GLWindowDevice(hwnd, hdc, gldc);
                return v_deviceInfo;
            }
            User32Lib.ReleaseDC(hwnd, hdc);
            gldc = NULL;
            hdc = NULL;
#elif UNIX	
	        //create linux device
            IntPtr v_currentGldc = GetCurrentContext();
			//create gl linux device rendering context
			int major, minor;//version info
			IntPtr displ = NULL; //display
			IntPtr glxWin = NULL;
			int[] doubleBufferConfig = null;
			IntPtr fbConfig = NULL; //config array pointer
			IntPtr config0 = NULL; //first config	
			GLWindowDevice v_deviceInfo = null;
			//connect to display
			displ  = XLib.XOpenDisplay(NULL);
			if (displ == NULL )
			{
#if DEBUG
				Console.WriteLine ("unable to connect to screen display");
#endif 
				return null  ;
			}
			//retrieve current version info
			XGL.glXQueryVersion(displ , out major , out minor );
			Version v_version = new Version (major, minor , 0,0);
			//init double buffer property info
			doubleBufferConfig = new int []{
				(int)enuXGLProperty.DrawableType, (int)enuXGLPropertyBIT.Window,
				(int)enuXGLProperty.RenderType, (int)enuXGLPropertyBIT.RGBA,
				(int)enuXGLProperty.DoubleBuffer,  1,  /* Request a double-buffered color buffer with */
    			(int)enuXGLProperty.RedSize ,      1,     /* the maximum number of bits per component    */
    			(int)enuXGLProperty.GreenSize,    1, 
    			(int)enuXGLProperty.BlueSize , 1,
				(int)enuXGLProperty.None
			};
			//uncomment next line for default setting
			//doubleBufferConfig = new int [0];
			int retNums = 0;
			fbConfig = XGL.glXChooseFBConfig(displ, XLib.XDefaultScreen(displ), doubleBufferConfig, ref retNums);
			if (fbConfig == NULL )
			{
#if DEBUG
				Console.WriteLine ("Can't choose a FB  config");
#endif
				return null ;
			}
			config0 = Marshal.ReadIntPtr (fbConfig );
			//create xwindow
			//create a drawable x window
			glxWin = XGL.glXCreateWindow(displ , config0 , hwnd , IntPtr.Zero  );
			//create a context for rendering
			gldc  = XGL.glXCreateNewContext(
						displ ,//display
					    config0, //selected config
						enuXGLType.RGBA, //rgba mode
				 		IntPtr.Zero ,//sharing context
			     		true//direct rendering
				);
			if( (glxWin != NULL ) && (gldc  != NULL  ))
			{
					if (XGL.glXMakeContextCurrent(displ , glxWin , glxWin , gldc ))
					{
						v_deviceInfo = new GLWindowDevice (displ, glxWin , gldc );
						return v_deviceInfo ;					
					}
#if DEBUG
				Console.WriteLine ("Unable to create a x window");
#endif
			}
#endif
            return null;
        }

        /// <summary>
        /// get real extenstion list be
        /// </summary>
        /// <returns></returns>
        public static string[] GetExtensions()
        {
            int c = 0;
            GL.glGetIntegerv(GL.GL_NUM_EXTENSIONS, ref c);
            if (c > 0)
            {

                // var T =  GL.GetProcedure<PFNGLGETSTRINGIPROC>("glGetStringi");

                string[] v_ext = new string[c];
                for (uint i = 0; i < c; i++)
                {
                    v_ext[i] = Marshal.PtrToStringAnsi(GL.glGetStringi(GL.GL_EXTENSIONS, i));
                }
                //string[] v_ext = (GL.glGetString(GL.GL_EXTENSIONS) + "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                //if (GL.Extensions != null)
                return v_ext;//.AddRange(GL.Extensions);
            }
            return null;
        }

        public static GLWindowDevice CreateDeviceInfo(IntPtr hwnd, IntPtr hdc, IntPtr gldc)
        {
//#if OS_WINDOWS
            return new GLWindowDevice(hwnd, hdc, gldc);
//#else 
//#endif
        }
        internal static IntPtr CreateGLDevice(
            IntPtr hdc,
            ushort version,
            byte bitcount,
            byte depthmask ,
            enuGLFlags flags,
            enuGLPixelMode pixel,
            enuGLPlane plane)
        {
            IntPtr gldc = NULL;
            int winError = 0;
            //gldc = WGLApi.wglCreateContext(hdc);
            STpixelFormatDescriptor pfd = new STpixelFormatDescriptor()
            {
                nSize = (ushort)Marshal.SizeOf(typeof(STpixelFormatDescriptor)),
                nVersion = version,
                dwFlags = (uint)flags,
                iPixelType = (byte)pixel,
                cColorBits = bitcount,
                cDepthBits = depthmask,
                iLayerType = (byte)plane
            };
            IntPtr alloc = Marshal.AllocCoTaskMem(pfd.nSize);
            Marshal.StructureToPtr(pfd, alloc, true);
            int v_index = 0;
            //retreive the current index of the pixel format
            v_index = Gdi32Lib.GetPixelFormat(hdc);
            //choose the pixel format that best match the request
            v_index =  Gdi32Lib.ChoosePixelFormat(hdc, alloc);
             if (v_index == 0)
                 return NULL;         
            bool v = Gdi32Lib.SetPixelFormat(hdc,v_index , alloc);
            v_index = Gdi32Lib.GetPixelFormat(hdc);
            STpixelFormatDescriptor cfd = new STpixelFormatDescriptor();
            Gdi32Lib.DescribePixelFormat(hdc,
                v_index, 
                (ushort)Marshal.SizeOf(typeof(STpixelFormatDescriptor)),
                ref cfd);
            if (!cfd.Equals (pfd))
            {
                pfd = cfd;
            }
            if (v == false)
            {
                winError = Marshal.GetLastWin32Error();             
            }
            gldc = WGL.wglCreateContext(hdc);
           /* if (gldc == NULL)
            {
                //selection du format de pixel
                pfd = new STpixelFormatDescriptor();
                pfd.nSize = (ushort)Marshal.SizeOf(typeof(STpixelFormatDescriptor));
                pfd.nVersion = version;
                pfd.dwFlags = (uint)flags;
                pfd.iPixelType = (byte)pixel;
                pfd.cColorBits = bitcount;
                pfd.cDepthBits = depthmask;
                pfd.iLayerType = (byte)plane;
                alloc = Marshal.AllocCoTaskMem(pfd.nSize);
                Marshal.StructureToPtr(pfd, alloc, true);
                v_index = Gdi32Lib.ChoosePixelFormat(hdc, alloc);
                //bool v = Gdi32Lib.SetPixelFormat(hdc, index, alloc);
                v = Gdi32Lib.SetPixelFormat(hdc, v_index, alloc);
                winError = Marshal.GetLastWin32Error();
                Marshal.FreeCoTaskMem(alloc);
                gldc = WGL.wglCreateContext(hdc);
            }*/
            return gldc;
        }
        public static IGLGraphicDevice CreateFromHdc(
            IntPtr hdc,
            byte bitcount,
            byte depthmask,
            enuGLFlags flags,
            enuGLPixelMode pixel,
            enuGLPlane plane,            
            out IntPtr gldc
            )
        {
#if OS_WINDOWS
            gldc = NULL;            
            if (hdc == NULL)
                return null;
            WGL.wglMakeCurrent(NULL, NULL);
            gldc = CreateGLDevice
                (
                hdc,
                1,
            bitcount,
            depthmask,
            flags,
            pixel,
            plane);
            if (gldc == NULL)
                gldc =  WGL.wglCreateContext(hdc);
            if (gldc != NULL)
            {
                GLWindowDevice winDevice = new GLWindowDevice(
                    IntPtr.Zero,
                    hdc,
                    gldc);
                return winDevice;
            }
            gldc = NULL;
            hdc = NULL;
            return null;
#elif UNIX
            gldc = NULL;
            return null;
#endif
        }
        /// <summary>
        /// get the proceduture delagate
        /// </summary>
        /// <param name="processname">procedure name</param>
        /// <param name="t">type of delagate to be converted</param>
        /// <returns></returns>
        public static Delegate GetProcedure(string processname, Type t)
        {
            IntPtr v = GetProcAddress(processname);
            if (v != NULL)
                return Marshal.GetDelegateForFunctionPointer(v, t);
            return null;
        }

        private static bool sm_failedInitProc = false;
        private static List<string> sm_faileInitProcInfo = new List<string>();

        public static void GetProcedureInit() {
            sm_failedInitProc = false;
            sm_faileInitProcInfo.Clear();

        }
        public static bool GetProcedureInitEnd() {
            return !sm_failedInitProc;
        }
        public static string[] GetProcedureInitErrors() {
            return sm_faileInitProcInfo.ToArray();
        }
        public static T GetProcedure<T>(string processname)where T: class
        {
            Type t = typeof(T);
            if (!t.IsSubclassOf(typeof(Delegate))){
                throw new Exception ("Type is not a delegate");
            }
            IntPtr v = GetProcAddress(processname);
            if (v != NULL)
                return (T)(object)Marshal.GetDelegateForFunctionPointer(v, t) ;


            sm_faileInitProcInfo.Add(processname);
            sm_failedInitProc = true;
            return default(T);
        }


        /// <summary>
        /// check is a procedure is defined
        /// </summary>
        /// <param name="methodName">defined procedure name</param>
        /// <returns></returns>
        public static bool IsProcedureDefine(string methodName)
        {
            IntPtr v = WGL.wglGetProcAddress(methodName);
            bool result = (v != NULL);
            return result;
        }
        public static System.Collections.IEnumerator AdditionalMethods {
            get {
                if (sm_additionalMethod == null)
                    return null;
                return sm_additionalMethod.Keys.GetEnumerator ();
            }
        }
        public static bool SupportExtension(string extension)
        {
            string str = GL.glGetString (GL.GL_EXTENSIONS );
            if (!string.IsNullOrEmpty (str) && str.Contains(extension)) 
                return true ;
            return false;
        }
        #region 
        ////------------------ Surcharge des methodes en fonction des besoins
        [DllImport(GLLIB)]
        static extern public void glGetIntegerv(uint pname, ref int _params);
        [DllImport(GLLIB)]
        static extern public void glGetIntegerv(uint pname,int[] _params);
        [DllImport(GLLIB)]

        static extern public void glGetFloatv(uint pname, ref float _params);

        [DllImport(GLLIB)]
        static extern public void glGetFloatv(uint pname, float[] _params);
        [DllImport(GLLIB)]
        static extern public void glGetDoublev(uint pname, ref double _params);
        [DllImport(GLLIB)]
        static extern public void glGetDoublev(uint pname, double[] _params);
        [DllImport(GLLIB)]
        static extern public void glGetClipPlane(uint plane, ref vect4d equation);
        [DllImport(GLLIB)]
        static extern public void glGetPolygonStipple(out byte[] mask);
        [DllImport(GLLIB)]
        static extern public void glGetBooleanv(uint pname, ref bool _params);
        [DllImport(GLLIB)]
        static extern public void glBitmap(int width, int height, float xorig, float yorig, float xmove, float ymove, IntPtr  bitmap);
        [DllImport(GLLIB)]
        static extern public void glGenTextures(int n, uint[] textures);
        [DllImport(GLLIB)]
        static extern public void glGetTexParameteriv(uint target, uint pname, ref int _params);
        [DllImport(GLLIB)]
        static extern public void glGetTexLevelParameterfv(uint target, int level, uint pname, ref float _params);
        [DllImport(GLLIB)]
        static extern public void glGetTexLevelParameteriv(uint target, int level, uint pname, ref int _params);
        //[DllImport(GLLIB)] static extern public uint[] glGenLists(int range);
        [DllImport(GLLIB)]
        static extern public void glGetTexEnvfv(uint target, uint pname, ref float _params);
        [DllImport(GLLIB)]
        static extern public void glGetTexEnviv(uint target, uint pname, ref int _params);
        [DllImport(GLLIB)]
        static extern public void glGetTexParameterfv(uint target, uint pname, ref float _params);
        [DllImport(GLLIB)]
        static extern public void glGetLightfv(uint id,uint param,float[] v_t );

        [DllImport(GLLIB)]
        public static extern void glTexSubImage2D(uint target, int level, int xoffset, int yoffset, int width, int height, uint format, uint type, byte[] pixels);


        [DllImport(GLLIB)]
        public static extern void glVertex3dv(IntPtr v);
        [DllImport(GLLIB)]
        public static extern void glVertex2fv(IntPtr v);



        //not nativily implement in core

        //[DllImport(GLLIB)]
        //public static extern void glGenBuffers(int count, ref uint[] id);
        //[DllImport(GLLIB)]
        //public static extern void glGenBuffersARB(int count, ref uint[] id);
        //[DllImport(GLLIB)]
        //public static extern void glBufferData(uint target, int size, float[] data, uint usage);
        #endregion
    }
}

