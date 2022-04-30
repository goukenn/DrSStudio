

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GL3DFont.cs
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
file:GL3DFont.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Text
{
    
using IGK.GLLib;
    using IGK.GLLib.Pointers;
    using IGK.ICore;
    /// <summary>
    /// represent a GL 3D Font
    /// </summary>
    public class GL3DFont : GLFont , IDisposable 
    {
        private float m_fontsize;
        private GraphicsUnit m_unit;
        private OGLFont m_font;
        private int m_RangeMin;
        private int m_RangeMax;
        private enuGL3DFontFormat m_fontFormat;
        private uint m_listbase;
        private int m_lineSpacing;
        public int RangeMin { get { return this.m_RangeMin; } }
        public int RangeMax { get { return this.m_RangeMax; } }
        public enuGL3DFontFormat FontFormat {
            get {
                return this.m_fontFormat;
            }
        }
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
        private GL3DFont()
        {
            this.m_fontFormat = enuGL3DFontFormat.FillPolygon;
        }
        public static GL3DFont Load(IGK.OGLGame.Graphics.OGLGraphicsDevice device,
            string fontname,
            enuFontStyle fontStyle,
            enuGL3DFontFormat fontformat,
            float extrusion,
            float deviation
            )
        {
            Font ft = new Font(fontname, 14, (FontStyle)fontStyle, GraphicsUnit.Pixel);
            IntPtr HDC = device.HDC;
            OGLFont v_font = OGLFont.CreateFont(HDC, ft, 14, 10);
            uint v_lbase = device.GenList(256);
            GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
            IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
            GL3DFont s = null;
            if (WGL.wglUseFontOutlines(HDC, 0, 255, v_lbase, deviation, extrusion, (int)fontformat, alloc))
            {
                s = new GL3DFont();
                s.Name = ft.Name;
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
        /// load a gl font string
        /// </summary>
        /// <param name="game"></param>
        /// <param name="fontname"></param>
        /// <param name="fontStyle"></param>
        /// <param name="fontformat"></param>
        /// <param name="extrusion"></param>
        /// <param name="deviation"></param>
        /// <returns></returns>
        public static GL3DFont  Load(GLGame game, 
            string fontname, 
            enuFontStyle fontStyle, 
            enuGL3DFontFormat fontformat,
            float extrusion,
            float deviation
            )
        {
            return Load(game.GD, fontname, fontStyle, fontformat, extrusion, deviation);
        }
        ///// <summary>
        ///// create a new sprite font list used to draw string
        ///// </summary>
        ///// <param name="device"></param>
        ///// <param name="fontName"></param>
        ///// <param name="format"></param>
        ///// <returns></returns>
        //public static SpriteFont Create(GraphicsDevice device, 
        //    string fontName,
        //    float size,
        //    enuFontStyle fontStyle, FontFormat format, int wString, int hString)
        //{
        //    Font ft = new Font(fontName, size ,(FontStyle )(int) fontStyle, GraphicsUnit.Pixel);
        //    IntPtr HDC = device.HDC;
        //    OGLFont v_font = OGLFont.CreateFont(HDC, ft, wString, hString);
        //    uint v_lbase = device.GenList(256);
        //    GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
        //    IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
        //    SpriteFont s = null;
        //    if (WGL.wglUseFontBitmaps(HDC, 0, 255, v_lbase))
        //    {
        //        s = new SpriteFont();
        //        s.m_name = ft.Name;
        //        s.m_fontsize = ft.Size;
        //        s.m_unit = ft.Unit;
        //        s.m_font = v_font;
        //        s.m_listbase = v_lbase;
        //        s.m_RangeMin = 0;
        //        s.m_RangeMax = 255;
        //        s.m_lineSpacing = (int)ft.GetHeight();
        //    }
        //    else
        //    {
        //        GL.glDeleteLists(v_lbase, 256);
        //    }
        //    Marshal.FreeCoTaskMem(alloc);
        //    ft.Dispose();
        //    return s;
        //}
        //public static SpriteFont Load(GLGame game, string fontname, float height, int style)
        //{
        //    Font ft = new Font(fontname, height,(FontStyle) style, GraphicsUnit.Pixel);
        //    IntPtr HDC = game.GraphicsDevice.HDC;
        //    int w = System.Windows.Forms.TextRenderer.MeasureText("_", ft).Width; 
        //    OGLFont v_font = OGLFont.CreateFont(HDC, ft, ft.Height , 10);
        //    uint v_lbase = game.GraphicsDevice.GenList(256);
        //    GLYPHMETRICSFLOAT[] gv = new GLYPHMETRICSFLOAT[256];
        //    IntPtr alloc = Marshal.AllocCoTaskMem(256 * Marshal.SizeOf(typeof(GLYPHMETRICSFLOAT)));
        //    SpriteFont s = null;
        //    if (WGL.wglUseFontBitmaps(HDC, 0, 255, v_lbase))
        //    {
        //        s = new SpriteFont();
        //        s.m_name = ft.Name;
        //        s.m_fontsize = ft.Size;
        //        s.m_unit = ft.Unit;
        //        s.m_font = v_font;
        //        s.m_listbase = v_lbase;
        //        s.m_RangeMin = 0;
        //        s.m_RangeMax = 255;
        //        s.m_lineSpacing = (int)ft.GetHeight();
        //    }
        //    else
        //    {
        //        GL.glDeleteLists(v_lbase, 256);
        //    }
        //    Marshal.FreeCoTaskMem(alloc);
        //    ft.Dispose();
        //    return s;
        //}
        //public Vector2f MeasureString(string text)
        //{
        //    Vector2f v_d = Vector2f.Zero;
        //    return v_d;
        //}
        public void Bind()
        {
            this.m_font.MakeCurrent();
            GL.glListBase(this.m_listbase);
        }
        #region IDisposable Members
        public override void Dispose()
        {
            this.m_font.Dispose();
            GL.glDeleteLists(this.m_listbase, 256); 
        }
        #endregion
    }
}

