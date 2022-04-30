

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Texture1D.cs
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
file:Texture1D.cs
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
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    public class Texture1D : 
        IDisposable, IGraphicResource
    {
        private uint m_id;
        private int m_width;
        private enuTextureEnvironmentMode m_environmentMode;
        public enuTextureEnvironmentMode TextureEnvironmentMode
        {
            get
            {
                return this.m_environmentMode;
            }
            set
            {
                this.m_environmentMode = value;
            }
        }
        public static uint CurrentBindingTexture
        {
            get
            {
                int[] d = new int[1];
                GL.glGetIntegerv(GL.GL_TEXTURE_1D_BINDING_EXT, d);
                return (uint)d[0];
            }
        }
        public uint Id
        {
            get { return m_id; }            
        }
        private Texture1D()
        {
            this.m_environmentMode = enuTextureEnvironmentMode.Replace;
        }


        /// <summary>
        /// load texture 1d to device
        /// </summary>
        /// <param name="device">GD</param>
        /// <param name="Width">with used </param>
        /// <param name="data">RGBA array bytes data</param>
        /// <returns></returns>
        public static Texture1D Load(OGLGraphicsDevice device,int Width, byte[] data)
        {
            Texture1D text = new Texture1D();
            uint id = device.GenTexture();
            text.m_id = id;
            text.m_width = Width;
            text.Bind();
            IntPtr alloc = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(
                data.Length * Marshal.SizeOf (typeof(byte)));
            Marshal.Copy(data, 0, alloc, data.Length);
            GL.glTexImage1D(GL.GL_TEXTURE_1D,
                0,
                (int)GL.GL_RGBA,
                Width,
                0,
                GL.GL_RGBA,
                GL.GL_UNSIGNED_BYTE,
                alloc);
            Marshal.FreeCoTaskMem(alloc);
            return text;
        }
        public void Bind()
        {
            GL.glBindTexture(GL.GL_TEXTURE_1D, (uint)this.Id);
            //GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)this.TextureEnvironmentMode);
            GL.glTexParameteri(GL.GL_TEXTURE_1D, GL.GL_TEXTURE_WRAP_S, (int)GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_1D, GL.GL_TEXTURE_WRAP_T, (int)GL.GL_REPEAT);
            GL.glTexParameteri(GL.GL_TEXTURE_1D, GL.GL_TEXTURE_MAG_FILTER, (int)GL.GL_NEAREST);
            GL.glTexParameteri(GL.GL_TEXTURE_1D, GL.GL_TEXTURE_MIN_FILTER, (int)GL.GL_NEAREST);                       
        }
        public void UnBind() {
            GL.glBindTexture(GL.GL_TEXTURE_1D, 0);
        }
        #region IGraphicResource Members
        const string RS_TYPE = "Texture1D";
        public string ResourceType
        {
            get { return RS_TYPE; }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            GL.glDeleteTextures(1, new uint[] { this.m_id });
        }
        #endregion
        internal void SetTextCoord(float p)
        {
            GL.glTexCoord1f(p);
        }
    }
}

