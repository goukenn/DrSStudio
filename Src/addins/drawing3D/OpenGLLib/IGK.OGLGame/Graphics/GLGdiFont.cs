

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLGdiFont.cs
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
file:GLGdiFont.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib ;
    /// <summary>
    /// represent a GL gdi font
    /// </summary>
    public class GLGdiFont : IDisposable , IGLListElement
    {
        struct CharInfo{
            internal char bchar;
            internal float bwidth;
            internal float bheight;
            internal uint blistid;
        }
        uint m_listbase;
        OGLGraphicsDevice m_graphicsDevice;
        uint m_range;
        FontFamily m_fontFamily;
        bool m_hasCharInfo;
        Dictionary<char, CharInfo> m_charInfo;
       //static readonly char EmptyChar = '_';
        /// <summary>
        /// get the list base
        /// </summary>
        public uint ListId{
            get{
                return this.m_listbase ;
            }
        }
        /// <summary>
        /// get if has char info
        /// </summary>
        public bool HasCharInfo {
            get {
                return this.m_hasCharInfo ;
            }
        }
        public FontFamily FontFamily {
            get {
                return this.m_fontFamily;
            }
        }
        /// <summary>
        /// bind the current font
        /// </summary>
        public void Bind()
        {
            GL.glListBase (this.m_listbase );
        }
        /// <summary>
        /// create a new gdi font
        /// </summary>
        /// <param name="device"></param>
        /// <param name="font"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public static GLGdiFont CreateFont(OGLGraphicsDevice device, FontFamily  font, FontStyle style)
        {
            if (font == null)
                return null;
            device.MakeCurrent ();
            uint v_base = GL.glGenLists(256);
            if (v_base  == 0) return null;
            Vector2d[][] v_tab = null;
            for (int i = 0; i < 256; i++)
            {///create new list
                v_tab = GLUtilitys.BuildFontOutLine((char)i, font, style);
                if ((v_tab !=null) && (v_tab.Length > 0))
                {
                    GL.glNewList((uint)(v_base  + i), GL.GL_COMPILE);
                    for (int j = 0; j < v_tab.Length; j++)
                    {
                        GL.glBegin(GL.GL_LINE_LOOP);
                        for (int k = 0; k < v_tab[j].Length; k++)
                        {
                            GL.glVertex2d(v_tab[j][k].X,
                                v_tab[j][k].Y);
                        }
                        GL.glEnd();
                    }
                    GL.glEndList();
                }
            }
            GLGdiFont v_font = new GLGdiFont();
            v_font.m_graphicsDevice = device;
            v_font.m_listbase = v_base;
            v_font.m_range = 256;
            v_font.m_fontFamily = font;
            return v_font;
        }
        /// <summary>
        /// Create GL GDI font width char info
        /// </summary>
        /// <param name="device"></param>
        /// <param name="font"></param>
        /// <param name="style"></param>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static GLGdiFont CreateFontWithCharInfo(OGLGraphicsDevice device, FontFamily font, FontStyle style, float Size)
        {
            if (font == null)
                return null;
            device.MakeCurrent();
            uint v_base = GL.glGenLists(256);
            if (v_base == 0) return null;
            Vector2d[][] v_tab = null;
            GraphicsPath v_path = new GraphicsPath();
            System.Drawing.StringFormat v_sf = new StringFormat();
            Dictionary<char, CharInfo> v_infos = new Dictionary<char, CharInfo>();
            char v_c = (char)(0);
            CharInfo v_info = new CharInfo();
            int i = 0;
            System.Action v_action = delegate()
            {
                if ((v_tab != null) && (v_tab.Length > 0))
                {
                    v_info = new CharInfo();
                    v_info.blistid = (uint)(v_base + i);
                    GL.glNewList(v_info.blistid, GL.GL_COMPILE);
                    for (int j = 0; j < v_tab.Length; j++)
                    {
                        GL.glBegin(GL.GL_LINE_LOOP);
                        for (int k = 0; k < v_tab[j].Length; k++)
                        {
                            GL.glVertex2d(v_tab[j][k].X,
                                v_tab[j][k].Y);
                        }
                        GL.glEnd();
                    }
                    RectangleF v_rc = v_path.GetBounds();
                    GL.glTranslated(v_rc.Width, 0, 0);
                    GL.glEndList();
                    v_info.bwidth = v_rc.Width;
                    v_info.bheight = v_rc.Height;
                    v_info.bchar = v_c;
                    v_infos.Add(v_c, v_info);
                }
            };
            for (; i < 256; i++)
            {///create new list
                v_c = (char)i;
                if (v_infos.ContainsKey (v_c))
                    continue ;
                v_path.Reset();
                v_path.AddString(v_c.ToString(), font, (int)style, Size, Point.Empty, v_sf);
                v_path.CloseFigure();
                if (v_path.PointCount == 0)
                {
                    //empty one
                    if (!v_infos.ContainsKey(' '))
                    {//register empty list
                        v_info = new CharInfo();
                        v_info .blistid =(uint)(v_base +' ');
                         v_path.Reset();
                        v_path.AddString('_'.ToString (), font, (int)style, Size, Point.Empty, v_sf);
                        v_path.CloseFigure();
                       
                        RectangleF v_rc = v_path.GetBounds();
                        GL.glNewList(v_info .blistid, GL.GL_COMPILE);                        
                        GL.glTranslated(v_rc.Width, 0, 0);
                        GL.glEndList();
                        v_info.bchar = ' ';
                        v_info.bwidth = v_rc.Width;
                        v_info.bheight = v_rc.Height;
                        v_infos.Add(' ', v_info);
                    }
                        GL.glNewList((uint)(v_base +i), GL.GL_COMPILE);
                        GL.glCallList(v_infos[' '].blistid);
                        GL.glEndList();
                        //copy info
                        v_info = v_infos[' '];
                        v_info.blistid = (uint)(v_base + i);
                        v_info.bchar = v_c;
                        v_infos[v_c] = v_info;
                        continue;
                }
                v_tab = GLUtilitys.BuildGraphicsOutLine(v_path);
                v_action();
            }
            GLGdiFont v_font = new GLGdiFont();
            v_font.m_hasCharInfo = true;
            v_font.m_graphicsDevice = device;
            v_font.m_listbase = v_base;
            v_font.m_range = 256;
            v_font.m_fontFamily = font;
            v_font.m_charInfo = v_infos;
            return v_font;
        }
        #region IDisposable Members
        public void Dispose()
        {
            GL.glDeleteLists(this.m_listbase,(int) this.m_range);
        }
        #endregion
        /// <summary>
        /// .private constructor
        /// </summary>
        private GLGdiFont()
        {
            this.m_hasCharInfo = false;
            this.m_charInfo = new Dictionary<char, CharInfo>();
        }
    }
}

