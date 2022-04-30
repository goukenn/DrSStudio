

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SpriteFont.cs
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
file:SpriteFont.cs
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
using System.Drawing.Drawing2D ;
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics
{
    
using IGK.GLLib;    
    using IGK.OGLGame.Math;
    using IGK.ICore;
    /// <summary>
    /// sprite font
    /// </summary>
    public class SpriteFont : IDisposable
    {
        private string m_name;
        private float m_fontsize;
        private GraphicsUnit m_unit;
        private IGK.OGLGame.Text.OGLFont m_font;
        private int m_RangeMin;
        private int m_RangeMax;
        //private FontFormat m_stringFormat;
        private uint m_listbase;
        private int m_lineSpacing;
        public string Name {
            get {
                return this.m_name;
            }
        }
        public int RangeMin { get { return this.m_RangeMin; } }
        public int RangeMax { get { return this.m_RangeMax; } }
        //public FontFormat FontFormat {
        //    get {
        //        return this.m_stringFormat;
        //    }
        //}
        public float FontSize {
            get {
                return this.m_fontsize;
            }
        }
        public int LineSpacing {
            get {
                return m_lineSpacing;
            }
            set {
                this.m_lineSpacing = value;
            }
        }
        public GraphicsUnit Unit {
            get {
                return this.m_unit;
            }
        }
        private SpriteFont()
        {
        }
        public static SpriteFont Load(GLGame game, string fontname, FontStyle fontStyle)//, FontFormat format)
        {
			int fontsize = 14;
			Font v_ft = new Font (fontname , fontsize , fontStyle , GraphicsUnit.Pixel );
			IntPtr cFont = v_ft.ToHfont();
			uint v_base = GL.glGenLists (256);
			XGL.glXUseXFont (cFont, 0, 256, (int)v_base);
			SpriteFontInfo v_info = game.GD.CreateSpriteFont(fontname , fontsize, (int)fontStyle);// , GraphicsUnit.Pixel );
			if (v_info !=null)
			{
				SpriteFont ft = new SpriteFont  ();
				ft.m_font = null;
				ft.m_listbase = v_base;
				ft.m_name = fontname ;
				ft.Bind ();
				return ft;
			}
			return null;
        }
        /// <summary>
        /// create a new sprite font list used to draw string        
        /// </summary>
        /// <param name="device"></param>
        /// <param name="fontName">font name</param>
        /// <param name="size">original font size</param>
        /// <param name="fontStyle">font style</param>
        /// <param name="width">require with</param>
        /// <param name="height">require height</param>
        /// <returns></returns>
        public static SpriteFont Create(OGLGraphicsDevice device, 
            string fontName,
            float size,
            enuFontStyle fontStyle,             
            int width ,
            int height)
        {
            Font ft = new Font(fontName, size ,(FontStyle )(int) fontStyle, GraphicsUnit.Pixel);
            IntPtr HDC = device.HDC;
            IGK.OGLGame.Text.OGLFont v_font = IGK.OGLGame.Text.OGLFont.CreateFont(HDC, ft, height, width);
            uint v_lbase = device.GenList(256);
            GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
            IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
            SpriteFont s = null;
            if (WGL.wglUseFontBitmaps(HDC, 0, 255, v_lbase))
            {
                s = new SpriteFont();
                s.m_name = ft.Name;
                s.m_fontsize = ft.Size;
                s.m_unit = ft.Unit;
                s.m_font = v_font;
                s.m_listbase = v_lbase;
                s.m_RangeMin = 0;
                s.m_RangeMax = 255;
                s.m_lineSpacing = (int)ft.GetHeight();
            }
            else
            {
                GL.glDeleteLists(v_lbase, 256);
            }
            Marshal.FreeCoTaskMem(alloc);
            ft.Dispose();
            return s;
        }
        /// <summary>
        /// load font device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="fontname"></param>
        /// <param name="height"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static SpriteFont Load(OGLGraphicsDevice device, string fontname, float height, int style)
        {
            if (device == null)
                return null;
            Font ft = new Font(fontname, height,(FontStyle) style, GraphicsUnit.Pixel);
            IntPtr HDC = device.HDC;
            int w = System.Windows.Forms.TextRenderer.MeasureText("_", ft).Width;
            IGK.OGLGame.Text.OGLFont v_font = IGK.OGLGame.Text.OGLFont.CreateFont(HDC, ft, ft.Height, 10);
            uint v_lbase = device.GenList(256);
            GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
            IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
            SpriteFont s = null;
            if (WGL.wglUseFontBitmaps(HDC, 0, 255, v_lbase))
            {
                s = new SpriteFont();
                s.m_name = ft.Name;
                s.m_fontsize = ft.Size;
                s.m_unit = ft.Unit;
                s.m_font = v_font;
                s.m_listbase = v_lbase;
                s.m_RangeMin = 0;
                s.m_RangeMax = 255;
                s.m_lineSpacing = (int)ft.GetHeight();
            }
            else
            {
                GL.glDeleteLists(v_lbase, 256);
            }
            Marshal.FreeCoTaskMem(alloc);
            ft.Dispose();
            return s;
        }
        public Vector2f MeasureString(string text)
        {
            Vector2f v_d = Vector2f.Zero;
            return v_d;
        }
		/// <summary>
		/// Bind this current font
		/// </summary>
        public void Bind()
        {
			if (this.m_font !=null)
            	this.m_font.MakeCurrent();
            GL.glListBase(this.m_listbase);
        }
        #region IDisposable Members
        public void Dispose()
        {
			if (this.m_font != null)
            this.m_font.Dispose();
            GL.glDeleteLists(this.m_listbase, 256); 
        }
        #endregion
    }
}

