

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ShaderCompiler.cs
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
file:ShaderCompiler.cs
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
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics.Shaders
{    
    using IGK.ICore;
    using IGK.GLLib;

    /// <summary>
    /// used as shader utility class
    /// </summary>
    public static class ShaderUtility
    {
        public static Shader CreateShader(OGLGraphicsDevice device, enuShaderType type)
        {
            return Shader.CreateShader(device, type);
        }
        public static bool AttachShaderSource(Shader shader, string source)
        {
            if (string.IsNullOrEmpty (source ))
                return false;
            GL.glShaderSource((uint)shader.Handle, 1, new string[]{source } , null);           
            return true;
        }
        public static bool AttachShaderSourceFile(Shader shader , string filename)
        {
            if (!File.Exists(filename))
                return false;
            return AttachShaderSource(shader, File.ReadAllText (filename));
        }
        public static bool CompileShader(Shader shader)
        {
            shader.InfoLog = string.Empty;
            GL.glCompileShader(shader.Handle);
            bool v = GetShader(shader, GL.GL_COMPILE_STATUS);
            string s = GetShaderInfoLog(shader);
            shader.HasError = !v;
            shader.InfoLog = s;
#if GL_DEBUG_ASSERT
            System.Diagnostics.Debug.Assert(v, s);
#endif
            if (!v)
            System.Diagnostics.Debug.WriteLine(string.Format("shader compilation failed : {0}", s));
            return v;
        }
        public static string GetShaderInfoLog(Shader shader)
        {
            int size = 0;
            string str = string.Empty ;
            IntPtr h = IntPtr.Zero;
            IntPtr hsize = Marshal.AllocCoTaskMem (4);
            //get size
            GL.glGetShaderiv((uint)shader.Handle, GL.GL_INFO_LOG_LENGTH ,hsize );
            size = Marshal.ReadInt32(hsize);
            if (size != 0)
            {
                h = Marshal.AllocCoTaskMem(size);
                Marshal.WriteInt32(hsize, 0);
                GL.glGetShaderInfoLog((uint)shader.Handle, size, hsize, h);
                str = Marshal.PtrToStringAnsi(h);
            }
            Marshal.FreeCoTaskMem(h);
            Marshal.FreeCoTaskMem(hsize);
            return str;
        }
        /// <summary>
        /// get shader param.
        /// </summary>
        /// <param name="shader"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool GetShader(Shader shader, uint param)
        {
            IntPtr p = Marshal.AllocCoTaskMem(1);
            GL.glGetShaderiv((uint)shader.Handle, param, p);
            bool b =Convert.ToBoolean( Marshal.ReadByte(p));
            Marshal.FreeCoTaskMem(p);
            return b; 
        }
        /// <summary>
        /// create the program
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static ShaderProgram CreateProgram(OGLGraphicsDevice device)
        {
            return ShaderProgram.CreateProgram(device);
        }
        /// <summary>
        /// validate the program
        /// </summary>
        /// <param name="program"></param>
        public static bool ValidateProgram(ShaderProgram program)
        {
            //GL.glValidateProgram(program.Handle );
            //var c = Marshal.AllocCoTaskMem(255);
            //GL.glGetProgramiv(program.Handle, GL.GL_VALIDATE_STATUS , c);// program.GetProgramLog (
            //var v  = Marshal.ReadInt32(c);
            ////GL.GL_TRUE;


            IntPtr h = Marshal.AllocCoTaskMem(1);
            GL.glGetProgramiv((uint)program.Handle, GL.GL_VALIDATE_STATUS, h);
            int i = Marshal.ReadByte(h);
            bool vv = Convert.ToBoolean(i);
            //if (!vv) {

            //  var  log =  program.GetProgramLog();
            //}

            //CoreLog.WriteDebug($"Validation status : { Marshal.PtrToStringAnsi(c)}");
            Marshal.FreeCoTaskMem(h);
            return vv;
        }
    }
}

