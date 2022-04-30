

using IGK.GLLib;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Shader.cs
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
file:Shader.cs
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
using System.Text;
namespace IGK.OGLGame.Graphics.Shaders
{
    /// <summary>
    /// represent a shader
    /// </summary>
    public class Shader
    {
        private uint  m_shaderId;
        private OGLGraphicsDevice m_device;
        private bool m_haserror;
        private string m_infoLog;

        public OGLGraphicsDevice Device { get { return this. m_device; } }
        public uint Handle { get { return this.m_shaderId; } }
        public string InfoLog {
            get { return m_infoLog;  }
            internal set { m_infoLog = value; }
        }
        /// <summary>
        /// create a shader
        /// </summary>
        /// <param name="device"></param>
        /// <param name="type">shader type</param>
        /// <returns></returns>
        public static Shader CreateShader(OGLGraphicsDevice device, enuShaderType type)
        {
            device.MakeCurrent();
            uint i =IGK.GLLib.GL.glCreateShader ((uint)type);
            if (i != 0 )
            {
                Shader c = new Shader();
                c.m_shaderId = i;
                c.m_device = device;
                return c;
            }
            return null;
        }
        public void Dispose()
        {
            if (this.m_shaderId == 0)
                return;
            if (!this.m_device.IsCurrent) this.m_device.MakeCurrent();
            IGK.GLLib.GL.glDeleteShader(this.m_shaderId);
            this.m_shaderId = 0;
        }
        /// <summary>
        /// load source string to shader
        /// </summary>
        /// <param name="code"></param>
        public bool LoadSource(string code)
        {
            if (string.IsNullOrEmpty (code))
                return false;
            IGK.GLLib.GL.glShaderSource((uint)this.Handle, 1, new string[] { code }, null);
            return true;
        }

        public int getUnitformLocation(uint progid, string name)  {
            return GL.glGetUniformLocation(progid, name);
        }

        public bool HasError { get { return m_haserror; } internal set { m_haserror = value; } }
    }
}

