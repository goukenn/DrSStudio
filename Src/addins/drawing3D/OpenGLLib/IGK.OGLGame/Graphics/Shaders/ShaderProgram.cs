

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ShaderProgram.cs
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
file:ShaderProgram.cs
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
using System.Runtime.InteropServices;


namespace IGK.OGLGame.Graphics.Shaders
{
    
    using IGK.ICore;
    using IGK.GLLib;
    /// <summary>
    /// represent a shader program
    /// </summary>
    public class ShaderProgram : IDisposable 
    {
        private uint m_ProgId;
        Dictionary<string, int> m_uniforms;
        Dictionary<string, int> m_attributes;


        /// <summary>
        /// initialize uniforms and check this program
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public bool InitUniforms(params string[] tn)
        {
            if (m_uniforms == null)
                m_uniforms = new Dictionary<string, int>();
            this.m_uniforms.Clear();

            if (tn == null) {
                int v_idx = GL.glGetUniformLocation(this.Handle, "\0");



            }
            var r = true;
            var k = 0;
            for (var i = 0; i < tn.Length; i++)
            {
                k = GL.glGetUniformLocation(this.Handle, tn[i]);
                this.m_uniforms[tn[i]] = k;
                r = r && (k != -1);
            }
            return r;

          
        }

        /// <summary>
        /// initialize attribute and check the program
        /// </summary>
        /// <param name="tn"></param>
        /// <returns></returns>
        public bool InitAttribLocation(params string[] tn)
        {
            if (m_attributes  == null)
                m_attributes = new Dictionary<string, int>();
            this.m_attributes.Clear();
            var r = true;
            var k = 0;
            for (var i = 0; i < tn.Length; i++)
            {
                k = GL.glGetAttribLocation(this.Handle, tn[i]);
                this.m_attributes[tn[i]] = k;
                r = r && (k != -1);
            }
            return r;
        }

        private OGLGraphicsDevice  m_Device;
        private List<Shader> m_shaders;
        private static string sm_errors; //store the error happend on create program

        delegate void linkPROC(IntPtr progH);
        delegate void glLinkProgramPROC(uint prog);


        private ShaderProgram()
        {
            m_shaders = new List<Shader>();
        }
        public OGLGraphicsDevice  Device
        {
            get { return m_Device; }
        }
        public uint Handle
        {
            get { return m_ProgId; }
        }
        /// <summary>
        /// create program
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public static ShaderProgram CreateProgram(OGLGraphicsDevice device)
        {
            if (device == null)
                return null;
            uint progId = GL.glCreateProgram ();
            if (progId != 0)
            {                
                ShaderProgram v_prog = new ShaderProgram();
                v_prog.m_ProgId = progId;
                v_prog.m_Device = device;
                return v_prog;
            }
            return null;
        }
        /// <summary>
        /// attach shader to grogram
        /// </summary>
        /// <param name="shader"></param>
        public void AttachShader(Shader shader)
        {
            if (shader != null)
            {
                GL.glAttachShader (this.Handle, shader.Handle);
                this.m_shaders.Add(shader);
            }
        }
        /// <summary>
        /// use the program
        /// </summary>
        public bool UseIt(){
            if (this.ShaderErrorCount == 0){
                GL.glUseProgram(this.Handle);
                return true;
            }
            return false;
        }
        /// <summary>
        /// free the use of this program
        /// </summary>
        public void Free(){
            GL.glUseProgramObjectARB(IntPtr.Zero );
        }

        /// <summary>
        /// bind the attrib location 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <remarks>you must call link after this function to make it available </remarks>
        public void BindAttribLocation(string name, uint id)
        {
            GL.glBindAttribLocation(this.Handle, id, name);
        }

        /// <summary>
        /// detach program
        /// </summary>
        /// <param name="shader"></param>
        public void DetachShader(Shader shader)
        {
            if (this.m_shaders.Contains(shader))
            {
                GL.glDetachShader(this.Handle, shader.Handle  );
                this.m_shaders.Remove(shader);
            }
        }
        /// <summary>
        /// link the current program
        /// </summary>
        public bool Link()
        {
            GL.glLinkProgram((uint)this.Handle);
            return this.GetLinkStatus();
        }
        /// <summary>
        /// get the program link status
        /// </summary>
        /// <returns></returns>
        public bool GetLinkStatus()
        {
            bool v = false ;
            IntPtr h = Marshal.AllocCoTaskMem(1);
            GL.glGetProgramiv ((uint)this.Handle, GL.GL_LINK_STATUS, h);
            int i = Marshal.ReadByte(h);
            v = Convert.ToBoolean(i);
            Marshal.FreeCoTaskMem(h);
            return v;
        }
        public static bool IsProgram(IntPtr id)
        {
            return GL.glIsProgram((uint)id.ToInt32());
            //return false;// GL.glIsProgramARB(id);
        }
        #region IDisposable Members
        public void Dispose()
        {
            Shader[] v_items  = m_shaders.ToArray();
            foreach (Shader item in v_items)
            {
                this.DetachShader(item);
            }
            GL.glDeleteProgram((uint)this.Handle);
        }
        #endregion
        public string GetProgramLog()
        {
            int size = 255;
            IntPtr h = Marshal.AllocCoTaskMem(size);
            IntPtr hsize = Marshal.AllocCoTaskMem(4);
            GL.glGetProgramInfoLog ((uint)this.Handle , size, hsize, h);
            int rh = Marshal.ReadInt32(hsize);
            string str = Marshal.PtrToStringAnsi(h);
            Marshal.FreeCoTaskMem(h);
            Marshal.FreeCoTaskMem(hsize);
            return str;
        }

        public int GetActiveUniform()
        {
            IntPtr h = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(int)));
            GL.glGetProgramiv((uint)this.Handle, GL.GL_ACTIVE_UNIFORMS, h);
            int i = Marshal.ReadInt32 (h);            
            Marshal.FreeCoTaskMem(h);
            return i;
        }

        public void LinkAttribf(uint attribId, int count, int stride=0, int offset=0) 
        {
            GL.glEnableVertexAttribArray(attribId);
            //i = GL.glGetError();

            GL.glVertexAttribPointer(
               attribId,                  // attribute 0. No particular reason for 0, but must match the layout in the shader.
               count,                  // size
               GL.GL_FLOAT,           // type
               false,           // normalized?
               stride,                  // stride
              IntPtr.Zero + offset           // array buffer offset
            );
        }

        private static T GetProgramValue<T>(uint program, uint param)
        {
            IntPtr h = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T)));
            GL.glGetProgramiv(program, param, h);
            var c = default(T);
            c = (T)Marshal.PtrToStructure (h, typeof(T));
            Marshal.FreeCoTaskMem(h);
            return c;
        }
        public int ShaderErrorCount { get {
            int i=0;
            foreach (var item in this.m_shaders)
            {
                if (item.HasError)
                    i++;
            }
            return i;
        } }

        public void SetUniformi(string name, int value)
        {
            var h = GL.glGetUniformLocation(this.Handle, name);
            if (h != 0) {
                GL.glUniform1i(h, value);
            }        
        }
        public void SetUniformMatrix(string name, Matrix mat) {
            this.SetUniformMatrix(name, mat.GetColumnFirstElement(), false);
        }
        public void SetUniformMatrix(string name, float[] value, bool transpose=false) {
            var h = GL.glGetUniformLocation(this.Handle, name);
            if (h != -1)
            {
               // GL.glUniform4fv(h, 1 ,  value);
                GL.glUniformMatrix4fv(h, 1, transpose , value);
            }
        }
        private OGLVertexArrayBuffer m_vArrayBuffer = null;
        public void SetAttribute(string name, float[] data, int sizePerElement) {
            int h = GL.glGetAttribLocation(this.Handle, name);
            if (h != -1) {
                uint c = (uint)h;


                //gl.bindBuffer(gl.ARRAY_BUFFER, vertexBufferObject);
                //gl.bufferData(gl.ARRAY_BUFFER, new Float32Array(vertices), gl.STATIC_DRAW);

                //var vbo = ShaderUtility.CreateBuffer();


                var vboI = m_vArrayBuffer ?? GLGameBuffer.GenVertexArray();
                var vbo = GLGameBuffer.Create();
                //vbo.SetData(data);

                vbo.BindVertextAttribData( c, sizePerElement, data);               

                m_vArrayBuffer = vboI;                
            }
        }

        public int GetActiveAttribute()
        {
            return GetProgramValue<int>((uint)this.Handle, GL.GL_ACTIVE_ATTRIBUTES );
        }
        public int GetMaxActiveUniform()
        {
            return GetProgramValue<int>((uint)this.Handle, GL.GL_ACTIVE_UNIFORM_MAX_LENGTH);
        }
        public int GetMaxActiveAttribute()
        {
            return GetProgramValue<int>((uint)this.Handle, GL.GL_ACTIVE_ATTRIBUTE_MAX_LENGTH );
        }

        /// <summary>
        /// get an array of string of active uniform names
        /// </summary>
        /// <returns></returns>
        public string[] GetActiveUniformNames()
        {
            var l = GetActiveUniform();
            List<string> g = new List<string>();
            IntPtr s = Marshal.AllocCoTaskMem (255);
            
            for (uint i = 0; i < l; i++)
            {
                GL.glGetActiveUniform(this.Handle, i, 255,
                    IntPtr.Zero, IntPtr.Zero, IntPtr.Zero,
                    s);

                g.Add(Marshal.PtrToStringAnsi(s));
            }
            Marshal.FreeCoTaskMem(s);
                return g.ToArray ();
        }
        /// <summary>
        /// get an array of string of active Attributes names
        /// </summary>
        /// <returns></returns>
        public string[] GetActiveAttribNames()
        {
            var l = GetActiveAttribute();
            List<string> g = new List<string>();
            IntPtr s = Marshal.AllocCoTaskMem(255);
            IntPtr st1 = Marshal.AllocCoTaskMem(4);
            IntPtr st2 = Marshal.AllocCoTaskMem(4);
            for (uint i = 0; i < l; i++)
            {
                GL.glGetActiveAttrib(this.Handle, i, 255,
                    IntPtr.Zero, st1, st2,
                    s);

                g.Add(Marshal.PtrToStringAnsi(s));
            }
            Marshal.FreeCoTaskMem(s);
            Marshal.FreeCoTaskMem(st1);
            Marshal.FreeCoTaskMem(st2);
            return g.ToArray();
        }

        /// <summary>
        /// get attrib location
        /// </summary>
        /// <param name="pname">name of the property</param>
        /// <returns>-1 if not defined</returns>
        public uint? GetAttribLocation(string pname)
        {
            int i = GL.glGetAttribLocation(this.Handle, pname);
            if (i == -1)
                return null;
            return (uint)i;// GL.glGetAttribLocation(this.Handle, pname);
        }

        public static string GetErrors()
        {
            return sm_errors;
        }

        public static ShaderProgram CreateProgram(OGLGraphicsDevice graphicsDevice, string[] vertexShaderSource, 
            string[] fragmentShaderSource)
        {
            sm_errors = string.Empty;


            var p = CreateProgram(graphicsDevice );
            if (p == null)
                return null;
            var vsh = Shader.CreateShader(graphicsDevice, enuShaderType.VertexShader);
            for (int i = 0; i < vertexShaderSource.Length; i++)
            {
                vsh.LoadSource(vertexShaderSource[i]);
            }
            if (!ShaderUtility.CompileShader(vsh)) {
                
                    sm_errors = "Vertext Shader " + vsh.InfoLog;
                    return null;
                
            }
            
            var fsh = Shader.CreateShader(graphicsDevice, enuShaderType.FragmentShader);
            for (int i = 0; i < vertexShaderSource.Length; i++)
            {
                fsh.LoadSource(fragmentShaderSource[i]);
            }

            if (!ShaderUtility.CompileShader(fsh)) {
                sm_errors = "Fragment Shader " + fsh.InfoLog;
                return null;
            }

            p.AttachShader(vsh);
            p.AttachShader(fsh);


            ShaderUtility.ValidateProgram(p);

            if (!p.Link())
            {
                p.Dispose();
                sm_errors = "Program not linked";
                return null;
            }
            return p;
        }
    }
}

