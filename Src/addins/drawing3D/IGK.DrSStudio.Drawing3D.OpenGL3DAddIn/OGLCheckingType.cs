

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLCheckingType.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:OGLCheckingType.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices ;
using System.IO;
using System.Reflection;
using IGK.ICore.IO;
namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    /// <summary>
    /// reprensen t
    /// </summary>
    public  class OGLCheckingType : CoreAddInInitializerBase
    {
        public static bool InitAssembly()
        {
            return true;
        }
        static OGLCheckingType() 
        {
        }
        public static bool Check(bool showLog)
        {
            try
            {
                IntPtr oglLib = Native.LoadLibrary("opengl32.dll");
                if (oglLib == IntPtr.Zero)
                {
                    CoreLog.WriteDebug("opengl32.dll not found");
                    return false;
                }
                CoreLog.WriteDebug("GL Version: " + GLLib.GL.Version);
                Native.FreeLibrary(oglLib);
                StringBuilder logMessage = new StringBuilder  ();
                string cdir = PathUtils.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string file = cdir + "/glut32.dll";
                if (!File.Exists(file))
                {
                    logMessage.AppendLine(string.Format("{0} doesn't exist", file));
                }
                IntPtr h = Native.LoadLibrary(file);
                if (h == IntPtr.Zero)
                {
                    logMessage.AppendLine(" Bad image format ");
                }
                Native.FreeLibrary(h);
                return true;
            }
            catch(Exception ex) {
                CoreLog.WriteDebug(ex.Message);
            }
            return false;
        }
      
        public override bool Initialize(CoreApplicationContext context)
        {
            return true; 
        }
        public override bool UnInitilize(CoreApplicationContext context)
        {
            return true;
        }
    }
}

