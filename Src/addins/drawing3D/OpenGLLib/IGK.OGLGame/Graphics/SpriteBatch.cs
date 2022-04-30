

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SpriteBatch.cs
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
file:SpriteBatch.cs
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
using System.Runtime.InteropServices;

using IGK.ICore;using IGK.GLLib ;
using IGK.OGLGame.Math;
namespace IGK.OGLGame.Graphics
{
    using IGK.GLLib;
    using IGK.OGLGame.Math;
    public class SpriteBatch
    {
        private OGLGraphicsDevice m_graphicDevice;
        private IOGLGGraphicsView m_graphicView;
        private bool m_doingjob;
        private float m_DepthNear;
        private float m_DepthFar;
        public float DepthFar
        {
            get { return m_DepthFar; }
            set
            {
                if (m_DepthFar != value)
                {
                    m_DepthFar = value;
                }
            }
        }
        public float DepthNear
        {
            get { return m_DepthNear; }
            set
            {
                if (m_DepthNear != value)
                {
                    m_DepthNear = value;
                }
            }
        }
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicDevice;
            }
        }
		public IOGLGGraphicsView View{
			get{
				return this.m_graphicView ;
			}
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="IGK.OGLGame.Graphics.SpriteBatch"/> class.
		/// </summary>
		/// <param name='device'>
		/// Device.
		/// </param>
		/// <param name='view'>
		/// View.
		/// </param>
        public SpriteBatch(OGLGraphicsDevice device, IOGLGGraphicsView view) {
            if (device == null)
                throw new ArgumentNullException("device");
            if (view == null)
                throw new ArgumentNullException("view");
            this.m_graphicDevice = device;
			this.m_graphicView = view;
            this.m_DepthFar = 1;
            this.m_DepthNear = -1;
        }
        public void Begin() 
        {
            CheckJob(false);
            GL.glPushClientAttrib(0xFFFFFFFF);
            GL.glPushAttrib(GL.GL_ALL_ATTRIB_BITS);
			Rectanglei v_rc = this.View.GetViewPort();
			this.GraphicsDevice.Viewport = v_rc ;
            int w = v_rc.Width;
            int h = v_rc.Height;
            this.GraphicsDevice.Projection.MatrixMode = MatrixMode.Projection;
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glOrtho(0, w, h, 0, this.DepthNear , this.DepthFar);
            this.GraphicsDevice.Projection.MatrixMode = MatrixMode.ModelView ;
            GL.glPushMatrix();
            GL.glLoadIdentity();
            this.GraphicsDevice.GraphicsImage.SetPixelZoom(1.0f, -1.0f);
            this.GraphicsDevice.Blending.SetBlendEquation(BlendEquation.Add);
            this.GraphicsDevice.Blending.SetBlendFunc(BlendFactor.SourceAlpha, BlendFactor.OneMinusSourceAlpha);
            this.GraphicsDevice.Capabilities.Blend = true;
            this.GraphicsDevice.Capabilities.DepthTest = false;
            this.m_doingjob = true;
        }
        public void End() {
            CheckJob(true);
            this.GraphicsDevice.Projection.MatrixMode = MatrixMode.Projection;
            GL.glPopMatrix();
            this.GraphicsDevice.Projection.MatrixMode = MatrixMode.ModelView ;
            GL.glPopMatrix();
            this.GraphicsDevice.Capabilities.Blend = false;
            GL.glPopClientAttrib ();
            GL.glPopAttrib();
            this.m_doingjob = false;
        }
        /// <summary>
        /// draw texture at position with require size
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="source"></param>
        public void Draw(Texture2D texture, Vector2f position, Size2f size ,  Colorf color , Rectanglef source, enuGLSpriteEffect effect)
        {
            if (texture == null)
            {
#if !DEBUG
                throw new ArgumentNullException("texture");
#else
                return;
#endif
            }
            CheckJob(true);
            int w = texture.Width;
            int h = texture.Height;
            float v_x  = source.X / (float )w;
            float v_y = source.Y / (float ) h;
            float v_X = source .Right / (float )w ;
            float v_Y = source .Bottom / (float )h;
            OGLGraphicsExtensions.GetFlipValue(effect , ref v_x, ref v_X, ref v_y, ref v_Y);
            float s_w = size.Width;
            float s_h = size.Height;
            this.GraphicsDevice.SetColor(color);
            this.GraphicsDevice.Capabilities.Texture2D = true;
            texture.Bind();
            this.GraphicsDevice.Projection.PushMatrix();
            this.GraphicsDevice.Projection.Translate(position.X, position.Y, 0.0f);
            this.GraphicsDevice.Begin(enuGraphicsPrimitives.Quads);
            this.GraphicsDevice.SetTexCoord(v_x , v_y );
            this.GraphicsDevice.SetVertex(Vector2f.Zero);
            this.GraphicsDevice.SetTexCoord(v_x , v_Y );
            this.GraphicsDevice.SetVertex(new Vector2f(0, s_h ));
            this.GraphicsDevice.SetTexCoord(v_X , v_Y);
            this.GraphicsDevice.SetVertex(new Vector2f(s_w, s_h));
            this.GraphicsDevice.SetTexCoord(v_X , v_y);
            this.GraphicsDevice.SetVertex(new Vector2f(s_w, 0));
            this.GraphicsDevice.End();
            this.GraphicsDevice.Projection.PopMatrix();
            //disable Texture 2D usage
            this.GraphicsDevice.Capabilities.Texture2D = false;
            //this.GraphicsDevice.Flush();
        }
        /// <summary>
        /// draw texture on sprite batch
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        public void Draw(Texture2D texture, Vector2f position, Colorf color) 
        {
            if (texture == null)
            {
                #if !DEBUG
                throw new ArgumentNullException("texture");
                #else
                return;
                #endif
            }
            CheckJob(true);
            Rectanglei v_rc = this.View.GetViewPort ();
            if (v_rc.IsEmpty)
                return;

            int w = v_rc.Width;
            int h = v_rc.Height;
            this.GraphicsDevice.CheckError();
            this.GraphicsDevice.SetColor(color);
            this.GraphicsDevice.Capabilities.Texture2D = true;
            texture.Bind();            
            this.GraphicsDevice.Begin (enuGraphicsPrimitives.Quads);
            this.GraphicsDevice.SetTexCoord(0, 0);
            this.GraphicsDevice.SetVertex(Vector2f.Zero);
            this.GraphicsDevice.SetTexCoord(0, 1);
            this.GraphicsDevice.SetVertex(new  Vector2f(0, h ) );
            this.GraphicsDevice.SetTexCoord(1, 1);
            this.GraphicsDevice.SetVertex(new  Vector2f(w, h));
            this.GraphicsDevice.SetTexCoord(1, 0);
            this.GraphicsDevice.SetVertex(new  Vector2f(w, 0));
            this.GraphicsDevice.End();
            //disable Texture 2D usage
            this.GraphicsDevice.Capabilities.Texture2D = false;
            //this.GraphicsDevice.Flush();
        }
        public void Draw(Texture2D texture, Vector2f position, Colorf color, float w , float h)
        {
            if (texture == null)
            {
#if !DEBUG
                throw new ArgumentNullException("texture");
#else
                return;
#endif
            }
            CheckJob(true);
            this.GraphicsDevice.SetColor(color);
            this.GraphicsDevice.Capabilities.Texture2D = true;
            texture.Bind();
            this.GraphicsDevice.Projection.PushMatrix();
            this.GraphicsDevice.Projection.Translate(position.X, position.Y, 0.0f);
            this.GraphicsDevice.Begin(enuGraphicsPrimitives.Quads);
            this.GraphicsDevice.SetTexCoord(0, 0);
            this.GraphicsDevice.SetVertex(Vector2f.Zero);
            this.GraphicsDevice.SetTexCoord(0, 1);
            this.GraphicsDevice.SetVertex(new Vector2f(0, h));
            this.GraphicsDevice.SetTexCoord(1, 1);
            this.GraphicsDevice.SetVertex(new Vector2f(w, h));
            this.GraphicsDevice.SetTexCoord(1, 0);
            this.GraphicsDevice.SetVertex(new Vector2f(w, 0));
            this.GraphicsDevice.End();
            this.GraphicsDevice.Projection.PopMatrix();
            //disable Texture 2D usage
            this.GraphicsDevice.Capabilities.Texture2D = false;
            //this.GraphicsDevice.Flush();
        }
        public void Draw(GraphicsResources resources, Vector2f location, Colorf color)
        {
            resources.DrawTo(this, location, color);
        }
        public void Draw(Texture2D texture, Rectanglei bound, Colorf color)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");
            CheckJob(true);
            int w = bound.Width;
            int h = bound.Height;
            this.GraphicsDevice.Projection.PushMatrix();
            this.GraphicsDevice.Projection .Translate (bound.X, bound.Y , 0.0f);
            this.GraphicsDevice.SetColor(color);
            this.GraphicsDevice.Capabilities.Texture2D = true;
            texture.Bind();
            this.GraphicsDevice.Begin (enuGraphicsPrimitives.Quads);
            this.GraphicsDevice.SetTexCoord(0, 0);
            this.GraphicsDevice.SetVertex(Vector2f.Zero);
            this.GraphicsDevice.SetTexCoord(0, 1);
            this.GraphicsDevice.SetVertex( new Vector2f(0, h));
            this.GraphicsDevice.SetTexCoord(1, 1);
            this.GraphicsDevice.SetVertex(new Vector2f(w, h));
            this.GraphicsDevice.SetTexCoord(1, 0);
            this.GraphicsDevice.SetVertex(new Vector2f(w, 0));
            this.GraphicsDevice.End();
            this.GraphicsDevice.Capabilities.Texture2D = false;
            this.GraphicsDevice.Projection.PopMatrix();
        }
        private void CheckJob(bool value)
        {
            if (this.m_doingjob != value)
                throw new Exception("Invalid Call must be between begin an end");
        }
        public void DrawLine(Colorf color, Vector2f startPosition, Vector2f endPosition)
        {
            this.GraphicsDevice.SetColor(color);
            GL.glBegin(GL.GL_LINES);
            GL.glVertex2f(startPosition.X, startPosition.Y);
            GL.glVertex2f(endPosition.X, endPosition.Y);
            GL.glEnd();
        }
        public void DrawString(SpriteFont fontSprite, string text, Vector2f vector2, Colorf color)
        {
            if (string.IsNullOrEmpty(text))
                return;
            CheckJob(true);
            fontSprite.Bind();
            
            GL.glColor4f(color.R, color.G, color.B, color.A);
            GL.glPushMatrix();
            GL.glTranslatef (vector2.X, vector2.Y + fontSprite.FontSize, 0);
            GL.glRasterPos2f(0.0f, 0.0f);
            IntPtr v_s = Marshal.StringToCoTaskMemAnsi(text);
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_BYTE, v_s);
            Marshal.FreeCoTaskMem(v_s);

            GL.glPopMatrix();
            
        }
        public void DrawString(GLGdiFont fontSprite, string text, Vector2f vector2, Colorf color)
        {
            if (string.IsNullOrEmpty(text))
                return;
            CheckJob(true);
            fontSprite.Bind();
            IntPtr v_s = Marshal.StringToCoTaskMemAnsi(text);
            this.GraphicsDevice.SetColor(color);
            GL.glPushMatrix();
            GL.glTranslatef(vector2.X, vector2.Y, 0);            
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_BYTE, v_s);
            GL.glPopMatrix();
            Marshal.FreeCoTaskMem(v_s);
        }
        public void Draw(Texture1D text, Vector2f vector2, Colorf color)
        {
            if (text == null)
                throw new ArgumentNullException("text");
            this.GraphicsDevice.Capabilities.Texture1D = true;
            text.Bind();
            this.GraphicsDevice.Projection.PushMatrix();
            this.GraphicsDevice.Projection.Translate(vector2.X, vector2.Y, 0.0f);
            this.GraphicsDevice.SetColor(color);
            this.GraphicsDevice.Begin(enuGraphicsPrimitives.Quads);
            text.SetTextCoord(0.0f);
            this.GraphicsDevice.SetVertex(Vector2f.Zero);
            text.SetTextCoord(0.0f);
            this.GraphicsDevice.SetVertex(new Vector2f(0, 1));
            text.SetTextCoord(1.0f);
            this.GraphicsDevice.SetVertex(new Vector2f(1, 1));
            text.SetTextCoord(1.0f);
            this.GraphicsDevice.SetVertex(new Vector2f(1, 0));
            this.GraphicsDevice.End();
            this.GraphicsDevice.Projection.PopMatrix();
            this.GraphicsDevice.Capabilities.Texture1D = false;
        }
        public void DrawBitmap(System.Drawing.Bitmap bmp, Vector2f vector2)
        {
            GL.glRasterPos2d (vector2.X, vector2.Y );
            System.Drawing.Imaging.BitmapData bdata =bmp.LockBits (
                new System.Drawing.Rectangle (0,0, bmp.Width, bmp.Height ),
                 System.Drawing.Imaging.ImageLockMode .ReadWrite ,
                  System.Drawing.Imaging.PixelFormat.Format32bppPArgb );
            GL.glDrawPixels(bmp.Width, bmp.Height,
                GL.GL_RGBA,
                GL.GL_UNSIGNED_BYTE,
                bdata.Scan0);
            bmp.UnlockBits(bdata);
        }
    }
}

