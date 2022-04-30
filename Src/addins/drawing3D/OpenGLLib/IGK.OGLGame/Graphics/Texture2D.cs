

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Texture2D.cs
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
file:Texture2D.cs
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using IGK.ICore;using IGK.GLLib;
using IGK.OGLGame.Math;
namespace IGK.OGLGame.Graphics
{
    public class Texture2D : GraphicsResources
    {       
        private uint m_id;
        private int m_width;
        private int m_height;
        private bool m_forceAdjust;
        private uint m_internalFormat;
        /// <summary>
        /// get the current size of the texture
        /// </summary>
        public Size Size { get { return new Size(m_width, m_height); } }
        private TextureWrapMethod m_wrapS;
        private TextureWrapMethod m_wrapT;
        private TextureMagFilter m_minFilter;
        private TextureMagFilter m_magFilter;
        private enuTextureEnvironmentMode m_environmentMode;
        public static uint CurrentBindingTexture
        {
            get
            {
                int[] d = new int[1];
                GL.glGetIntegerv(GL.GL_TEXTURE_2D_BINDING_EXT, d);
                return (uint)d[0];
            }
        }
        /// <summary>
        /// get if the texture is force adjusted
        /// </summary>
        public bool ForceAdjust
        {
            get { return this.m_forceAdjust; }
        }
        /// <summary>
        /// get or set the texture environment mode
        /// </summary>
        [Description("get or set the environment mode")]
        public enuTextureEnvironmentMode TextureEnvironmentMode {
            get {
                return this.m_environmentMode;
            }
            set {
                this.m_environmentMode = value;
            }
        }
        /// <summary>
        /// get or set the texture wrapS
        /// </summary>
        [Description("get or set texture WrapS")]
        public TextureWrapMethod WrapS {
            get {
                return this.m_wrapS;
            }
            set {
                this.m_wrapS  = value;
            }
        }
        [Description("get or set texture WrapT")]
        public TextureWrapMethod WrapT
        {
            get
            {
                return this.m_wrapT;
            }
            set
            {
                this.m_wrapT = value;
            }
        }
        [Description("get or set texture MagFilter")]
        public TextureMagFilter MagFilter {
            get {
                return this.m_magFilter;
            }
            set {
                this.m_magFilter = value;
            }
        }
        [Description("get or set texture MinFilter")]
        public TextureMagFilter MinFilter
        {
            get
            {
                return this.m_minFilter;
            }
            set
            {
                this.m_minFilter = value;
            }
        }
        [Browsable(false)]
        /// <summary>
        /// get the texture id
        /// </summary>
        public uint Id { get { return this.m_id; } }
        /// <summary>
        /// get thte width 
        /// </summary>
        public int Width { get { return this.m_width; } }
        /// <summary>
        /// get the height
        /// </summary>
        public int Height { get { return this.m_height; } }
        public uint InternalFormat {
            get { return this.m_internalFormat; }
        }
        public bool IsValid {
            get {
                return GL.glIsTexture(this.Id);
            }
        }
        private Texture2D(OGLGraphicsDevice graphicsDevice) :base(graphicsDevice, string.Empty )
        {
            this.m_environmentMode = enuTextureEnvironmentMode.Modulate ;
            this.m_wrapS = TextureWrapMethod.Repeat;
            this.m_wrapT = TextureWrapMethod.Repeat;
            this.m_minFilter = TextureMagFilter.Linear ;
            this.m_magFilter = TextureMagFilter.Linear;
        }
        public static Texture2D Load(OGLGraphicsDevice device, int width, int height)
        {
            using (Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb))
            {
                return Load(device, bmp);
            }
        }
        public static Texture2D  Load(OGLGraphicsDevice device, int width, int height, Byte[] data, bool min)
        {
            if ((device == null) || !device.IsCurrent || (data== null))
                return null;
            int w = width;
            int h = height;
            bool v_adjusted = false;
            uint v_index = device.GenTexture();
            if (v_index <= 0)
                return null;
            GL.glBindTexture(GL.GL_TEXTURE_2D, v_index);
            try
            {
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4, width, height, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    Marshal.UnsafeAddrOfPinnedArrayElement(data, 0));                
                device.CheckError();
            }
            catch
            {
                using (Bitmap v_cbmp = new Bitmap(w, h))
                {
                    BitmapData v_data = v_cbmp.LockBits(new Rectangle(Point.Empty, v_cbmp.Size), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                    Marshal.Copy(data, 0, v_data.Scan0, data.Length);
                    v_cbmp.UnlockBits(v_data);
                    v_adjusted = true;
                    if (min)
                    {
                        w = (int)width.MinLog(2);
                        h = (int)height.MinLog(2);
                    }
                    else
                    {
                        w = (int)width.MaxLog(2);
                        h = (int)height.MaxLog(2);
                    }
                    using (Bitmap v_cpbmp = new Bitmap(w, h))
                    {
                        global::System.Drawing.Graphics g = global::System.Drawing.Graphics.FromImage(v_cpbmp);
                        g.DrawImage(v_cbmp, 0, 0, v_cpbmp.Width, v_cpbmp.Height);
                        g.Dispose();
                        v_data = v_cpbmp.LockBits(new Rectangle(Point.Empty, v_cpbmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
                        GL.glTexImage2D(GL.GL_TEXTURE_2D,
                            0,
                            4, w, h, 0,
                            GL.GL_BGRA,
                            GL.GL_UNSIGNED_BYTE,
                            v_data.Scan0);
                        v_cpbmp.UnlockBits(v_data);
                    }
                }
            }
            device.CheckError();
            Texture2D v_out = new Texture2D(device)
            {
                m_id = v_index,
                m_height = w,
                m_width = h,
                m_forceAdjust = v_adjusted
            };
            v_out.Bind();
            if (!GL.glIsTexture(v_index))
            {
                return null;// throw new Exception("not a valid out set texture");
            }
            return v_out;
        }
        public static Texture2D Load(GLGame gLGame, string filename)
        {
            if (!System.IO.File.Exists (filename))
                return null;
            Texture2D v_out = new Texture2D(gLGame.GD );
            //uint[] t = new uint []{1};
            gLGame.Disposing += new EventHandler(v_out.gLGame_Disposing);
            v_out.m_id = gLGame.GD.GenTexture();   
            v_out.Bind();
            Bitmap bmp = Bitmap.FromFile(filename) as Bitmap ;
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            BitmapData data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size ), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);            
            try
            {

               // texImage2D(gl.TEXTURE_2D, 0, gl.RGBA, gl.RGBA, gl.UNSIGNED_BYTE, img);

                //GL.glTexImage2D(GL.GL_TEXTURE_2D,
                //    0,
                //    4, //internal format
                //    bmp.Width, bmp.Height, 0,
                //    GL.GL_BGRA,
                //    GL.GL_UNSIGNED_BYTE,
                //    data.Scan0);

                GL.glTexImage2D(GL.GL_TEXTURE_2D,
        0,
       (int)GL.GL_BGRA,
        bmp.Width, bmp.Height,
        0,
        GL.GL_BGRA,
        GL.GL_UNSIGNED_BYTE,
        data.Scan0);


                gLGame.GD.CheckError();
                bmp.UnlockBits(data);
                v_out.m_forceAdjust =false;
            }
            catch 
            {
                int w = (int)bmp.Width.MinLog(2);
                int h = (int)bmp.Height.MinLog(2);
                bmp.UnlockBits(data);
                Bitmap vbmp = new Bitmap(bmp, new Size(w, h));
                bmp.Dispose();
                bmp = vbmp;
                data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4,w,h, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);
                gLGame.GD.CheckError();
                bmp.UnlockBits(data);
                v_out.m_forceAdjust = true;
            }
            v_out.m_height = bmp.Height;
            v_out.m_width = bmp.Width;
            v_out.m_internalFormat = 4;
            bmp.Dispose ();
            return v_out;
        }
        void gLGame_Disposing(object sender, EventArgs e)
        {
            this.Dispose();    
        }
        #region IDisposable Members
        public override void Dispose()
        {
            try
            {
                if (GL.glIsTexture(this.m_id))
                {
                    GL.glDeleteTextures(1, new uint[] { this.m_id });
                    this.m_id = 0;
                }
            }
            catch { 
            }
        }
        #endregion
        /// <summary>
        /// Bind the current texture. and set it's properties
        /// </summary>
        public void Bind()
        {
            var GD = this.GraphicsDevice;

            GD.CheckError();
            GL.glBindTexture(GL.GL_TEXTURE_2D, (uint)this.Id);
            

            //  GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_TEXTURE_ENV_MODE, (int)this.TextureEnvironmentMode);
            //GL.glTexEnvi(GL.GL_TEXTURE_ENV, GL.GL_COMBINE_RGB, (int)GL.GL_INTERPOLATE);

            this.m_wrapS = TextureWrapMethod.ClampToEdge;
            this.m_wrapT = TextureWrapMethod.ClampToEdge;

            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_S, (int)this.m_wrapS );
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_WRAP_T, (int)this.m_wrapT);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MAG_FILTER, (int)this.m_magFilter);
            GL.glTexParameteri(GL.GL_TEXTURE_2D, GL.GL_TEXTURE_MIN_FILTER, (int)this.m_minFilter);
            GD.CheckError();
        }
        /// <summary>
        /// bind texture at active index
        /// </summary>
        /// <param name="index"></param>
        public void BindActive(uint index) {
            GL.glActiveTexture(GL.GL_TEXTURE0 + index);
            this.Bind();
        }
        #region IGraphicResouces Members
        public override string ResourceType
        {
            get { return "Texture2D";  }
        }
        #endregion
        public static Texture2D Load(OGLGraphicsDevice graphicsDevice, string filename)
        {
            if ((filename ==null) || !System.IO.File.Exists(filename))
                return null;
            Bitmap bmp = null;
            Texture2D t = null;
            try
            {
                bmp = Bitmap.FromFile(filename) as Bitmap;
                t = Load(graphicsDevice, bmp);
            }
            catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return t;
        }
        private static bool LoadBitmap(OGLGraphicsDevice device,
            Bitmap bitmap ,
            bool min,
            out bool forceAdjust, out Size size)
        {
            forceAdjust = false;
            size = Size.Empty;
            if ((bitmap == null) || (bitmap.PixelFormat == PixelFormat.Undefined))
                return false;

            Bitmap bmp = bitmap.Clone() as Bitmap;
            BitmapData data = null;
            size = Size.Empty;
            try
            {
                data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4, bmp.Width, bmp.Height, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);
                bmp.UnlockBits(data);
                data = null;
                device.CheckError();
                forceAdjust = false;
            }
            catch
            {
                int w = 0;
                int h = 0;
                forceAdjust = true;
                if (min)
                {
                    w = (int)bmp.Width.MinLog(2);
                    h = (int)bmp.Height.MinLog(2);
                }
                else
                {
                    w = (int)bmp.Width.MaxLog(2);
                    h = (int)bmp.Height.MaxLog(2);
                }
                Bitmap vbmp = new Bitmap(bmp, new Size(w, h));
                bmp.Dispose();
                bmp = vbmp;
                data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4, w, h, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);
                bmp.UnlockBits(data);
            }
            finally {
                if (data != null)
                {
                    bmp.UnlockBits(data);
                }
            }
            size = bmp.Size ;
            bmp.Dispose ();
            return true ;
        }
        /// <summary>
        /// create an load bitmap
        /// </summary>
        /// <param name="graphicsDevice">device</param>
        /// <param name="bmp">bitmap data</param>
        /// <param name="min">true to get log minimum, false</param>
        /// <returns>create bitmap</returns>
        public static Texture2D Load(OGLGraphicsDevice graphicsDevice,
            Bitmap bmp,
            bool min)
        {
            if (!graphicsDevice.IsCurrent || (graphicsDevice == null) || (bmp == null) || (bmp.PixelFormat == PixelFormat.Undefined))
                return null;
            Texture2D v_out = new Texture2D(graphicsDevice);
            //uint[] t = new uint[] { 1 };
            uint v_index = graphicsDevice.GenTexture();           
            v_out.m_id = v_index;
            v_out.Bind();
            BitmapData data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            try
            {
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4, bmp.Width, bmp.Height, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);
                bmp.UnlockBits(data);
                graphicsDevice.CheckError();
                v_out.m_forceAdjust = false;
            }
            catch
            {
                /*
                 * impossible de create a texture with the require size 
                 * try to create a texture with a log2 size
                 * 
                 * */
                int w = 0;
                int h = 0;
                v_out.m_forceAdjust = true;
                if (min)
                {
                    w = (int)bmp.Width.MinLog(2);
                    h = (int)bmp.Height.MinLog(2);
                }
                else
                {
                    w = (int)bmp.Width.MaxLog(2);
                    h = (int)bmp.Height.MaxLog(2); 
                }
                Bitmap vbmp = new Bitmap(bmp, new Size(w, h));
                bmp.Dispose();
                bmp = vbmp;
                data = bmp.LockBits(new global::System.Drawing.Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadOnly,
                 System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                GL.glTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    4, w, h, 0,
                    GL.GL_BGRA,
                    GL.GL_UNSIGNED_BYTE,
                    data.Scan0);
                bmp.UnlockBits(data);
            }
            try
            {
                graphicsDevice.CheckError();
                v_out.m_height = bmp.Height;
                v_out.m_width = bmp.Width;
                if (!GL.glIsTexture(v_out.Id))
                {
                    return null;// throw new Exception("not a valid out set texture");
                }
            }
            catch {
                v_out = null;
            }
            return v_out;
        }
        /// <summary>
        /// load bitmap on maximun value
        /// </summary>
        /// <param name="graphicsDevice">device</param>
        /// <param name="bmp">bitmap data</param>
        /// <returns></returns>
        public static Texture2D Load(OGLGraphicsDevice graphicsDevice, Bitmap bmp)
        {
            return Texture2D.Load(graphicsDevice, bmp, false);
        }
        /// <summary>
        /// Replace  the current texture image width the new bitmap
        /// </summary>
        /// <param name="bmp"></param>
        /// <param name="p"></param>
        /// <param name="p_3"></param>
        /// <param name="p_4"></param>
        /// <param name="p_5"></param>
        public void ReplaceTexture(Bitmap bmp, int x, int y, int width, int height)
        {
            if (this.Id != CurrentBindingTexture )
                this.Bind();
            BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(Point.Empty,
                bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            GL.glTexSubImage2D(GL.GL_TEXTURE_2D,
                0,
                x,
                y,
                width,
                height,
                GL.GL_BGRA,
                GL.GL_UNSIGNED_BYTE,
                data.Scan0);
            bmp.UnlockBits(data);
        }
        public void ReplaceTexture(Byte[] data, int x, int y, int width, int height)
        {
            if (this.Id != CurrentBindingTexture)
                this.Bind();
            GL.glTexSubImage2D(GL.GL_TEXTURE_2D,
             0,
             x,
             y,
             width,
             height,
             GL.GL_BGRA,
             GL.GL_UNSIGNED_BYTE,
             data);
        }
        /// <summary>
        /// create a 2D texture
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Texture2D Create(OGLGraphicsDevice device, int width, int height)
        {
            if ((width ==0)|| (height==0))
                return null;
            Bitmap bmp = new Bitmap(width, height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            g.Clear(System.Drawing.Color.Empty );
            g.Dispose();
            Texture2D v_texture = Texture2D.Load(device, bmp);
            bmp.Dispose();
            return v_texture;
        }
        public override void DrawTo(SpriteBatch spriteBatch, Vector2f location, Colorf color)
        {
            spriteBatch.Draw(this, location, color);
        }
        /// <summary>
        /// replace the current texture with the taget one
        /// </summary>
        /// <param name="texture"></param>
        public void ReplaceTexture(Texture2D texture)
        {
            if ((this.Width == texture.Width) && (this.Height == texture.Height))
            { 
                //same with
                GL.glCopyTexImage2D(GL.GL_TEXTURE_2D,
                    0,
                    this.InternalFormat,
                    0, 0,
                    this.Width,
                    this.Height,
                    0);
            }
        }
        public void ReplaceTexture(Bitmap bmp)
        {
           // if (this.Id != CurrentBindingTexture)
            if ((bmp == null) || (bmp.PixelFormat == PixelFormat.Undefined))
                return;
             this.Bind();
           // bool f = false ;
            Size s = Size.Empty ;
            if (!LoadBitmap(this.GraphicsDevice, bmp, true, out bool f, out s))
            {
                return;
            }
            this.m_width = s.Width;
            this.m_height = s.Height;
         //   BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(Point.Empty,
         //bmp.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
         //   GL.glTexImage2D(GL.GL_TEXTURE_2D,
         //      0,
         //      4, bmp.Width, bmp.Height, 0,
         //      GL.GL_BGRA,
         //      GL.GL_UNSIGNED_BYTE,
         //      data.Scan0);
         //   //GL.glTexSubImage2D(GL.GL_TEXTURE_2D,
         //   //    0,
         //   //    x,
         //   //    y,
         //   //    width,
         //   //    height,
         //   //    GL.GL_BGRA,
         //   //    GL.GL_UNSIGNED_BYTE,
         //   //    data.Scan0);
         //   bmp.UnlockBits(data);
        }
    }
}

