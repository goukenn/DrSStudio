

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsImage.cs
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
file:GraphicsImage.cs
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
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;
namespace IGK.OGLGame.Graphics.Imaging
{
    
using IGK.ICore;using IGK.OGLGame.Math ;
    using IGK.GLLib;
    /// <summary>
    /// represent the grahics image maninupaltion class
    /// </summary>
    public class GraphicsImage
    {
        private OGLGraphicsDevice m_GraphicsDevice;
        [Browsable (false )]
        public OGLGraphicsDevice GraphicsDevice
        {
            get { return m_GraphicsDevice; }            
        }
        public GraphicsImage(OGLGraphicsDevice device) {
            this.m_GraphicsDevice = device;
        }
        [Category("Unpack")]
        public bool UnPackSwapByte { get { return this.GraphicsDevice.GetBooleanv<bool>(GL.GL_UNPACK_SWAP_BYTES); }
            set {
                SetPixelStore(GL.GL_UNPACK_SWAP_BYTES, value);
            }
        }
        [Category("Pack")]
        public bool PackSwapByte
        {
            get { return this.GraphicsDevice.GetBooleanv<bool>(GL.GL_PACK_SWAP_BYTES); }
            set
            {
                SetPixelStore(GL.GL_PACK_SWAP_BYTES, value);
            }
        }
        [Category("Unpack")]
        public bool UnPackLSBFirst
        {
            get { return this.GraphicsDevice.GetBooleanv<bool>(GL.GL_UNPACK_LSB_FIRST); }
            set
            {
                SetPixelStore(GL.GL_UNPACK_LSB_FIRST , value);
            }
        }
        [Category("Pack")]
        public bool PackLSBFirst
        {
            get { return this.GraphicsDevice.GetBooleanv<bool>(GL.GL_PACK_LSB_FIRST); }
            set
            {
                SetPixelStore(GL.GL_PACK_LSB_FIRST, value);
            }
        }
        [Category("Unpack")]
        public int UnPackRowLength
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_ROW_LENGTH);
            }
            set
            {
                SetPixelStore(GL.GL_UNPACK_ROW_LENGTH , value);
            }
        }
        [Category("Pack")]
        public int PackRowLength
        {
            get { return this.GraphicsDevice.GetIntegerv<int> (GL.GL_PACK_ROW_LENGTH ); }
            set
            {
                SetPixelStore(GL.GL_PACK_ROW_LENGTH , value);
            }
        }
        [Category("Unpack")]
        public int UnPackSkipRows
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_SKIP_ROWS);
            }
            set
            {
                SetPixelStore(GL.GL_UNPACK_SKIP_ROWS, value);
            }
        }
        [Category("Pack")]
        public int PackSkipRows
        {
            get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_PACK_SKIP_ROWS); }
            set
            {
                SetPixelStore(GL.GL_PACK_SKIP_ROWS, value);
            }
        }
        [Category("Unpack")]
        public int UnPackSkipPixels
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_SKIP_PIXELS);
            }
            set
            {
                SetPixelStore(GL.GL_UNPACK_SKIP_PIXELS, value);
            }
        }
        [Category("Pack")]
        public int PackSkipPixels
        {
            get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_PACK_SKIP_PIXELS); }
            set
            {
                SetPixelStore(GL.GL_PACK_SKIP_PIXELS, value);
            }
        }
        [Category("Unpack")]
        public int UnPackAlignment
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_ALIGNMENT );
            }
            set
            {
                if ((value >=1)&&(value<=4))
                SetPixelStore(GL.GL_UNPACK_ALIGNMENT, value);
            }
        }
        [Category("Pack")]
        public int PackAlignment
        {
            get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_ALIGNMENT); }
            set
            {
                if ((value >= 1) && (value <= 4))
                    SetPixelStore(GL.GL_PACK_ALIGNMENT, value);
            }
        }
        [Category("Unpack")]
        public int UnPackImageHeight
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_IMAGE_HEIGHT);
            }
            set
            {
                SetPixelStore(GL.GL_UNPACK_IMAGE_HEIGHT, value);
            }
        }
        [Category("Pack")]
        public int PackImageHeight
        {
            get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_PACK_IMAGE_HEIGHT); }
            set
            {
                SetPixelStore(GL.GL_PACK_IMAGE_HEIGHT , value);
            }
        }
        [Category("Unpack")]
        public int UnPackSkipImages
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_UNPACK_SKIP_IMAGES);
            }
            set
            {
                SetPixelStore(GL.GL_UNPACK_SKIP_IMAGES, value);
            }
        }
        [Category("Pack")]
        public int PackSkipImages
        {
            get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_PACK_SKIP_IMAGES); }
            set
            {
                SetPixelStore(GL.GL_PACK_SKIP_IMAGES, value);
            }
        }
        private static void SetPixelStore(uint param, bool value)
        {
            GL.glPixelStorei (param, Convert.ToInt32 (value));
        }
        private static void SetPixelStore(uint param, int value)
        {
            GL.glPixelStorei (param, value);
        }
        public static void DrawBitmap(System.Drawing.Bitmap bmp, OGLGraphicsDevice device, Vector2f location)
        {
            global::System.Drawing.Rectangle v_rc = new global::System.Drawing.Rectangle(Point.Empty, bmp.Size);
            BitmapData v_data = bmp.LockBits(v_rc, System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            GL.glRasterPos2f(location.X, location.Y);
            GL.glDrawPixels(v_rc.Width, v_rc.Height, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE, v_data.Scan0);
            bmp.UnlockBits(v_data);
        }
        public void SetPixelZoom(float x, float y)
        {
            GL.glPixelZoom(x, y);
        }
        public static void CopyPixel(int x,
            int y, 
            int width,
            int height,
            Vector2f location,
            enuDeviceBuffer deviceBuffer)
        {
            GL.glRasterPos2f(location.X, location.Y);
            GL.glCopyPixels(x, y, width, height, (uint)deviceBuffer);
        }
        public static void SetColorBias(float r, float g, float b, float a)
        {
            GL.glPixelTransferf(GL.GL_RED_BIAS, r);
            GL.glPixelTransferf(GL.GL_GREEN_BIAS, g);
            GL.glPixelTransferf(GL.GL_BLUE_BIAS, b);
            GL.glPixelTransferf(GL.GL_ALPHA_BIAS, a);
        }
        public static void SetColorScale(float r, float g, float b, float a)
        {
            GL.glPixelTransferf(GL.GL_RED_SCALE, r);
            GL.glPixelTransferf(GL.GL_GREEN_SCALE, g);
            GL.glPixelTransferf(GL.GL_BLUE_SCALE, b);
            GL.glPixelTransferf(GL.GL_ALPHA_SCALE, a);
        }
        public static void SetDepth(float scale, float bias)
        {
            GL.glPixelTransferf(GL.GL_DEPTH_BIAS, bias);
            GL.glPixelTransferf(GL.GL_DEPTH_SCALE, scale);
        }
    }
}

