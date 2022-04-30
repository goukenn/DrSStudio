

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLFont.cs
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
file:OGLFont.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms ;
using System.Drawing ;
namespace IGK.GLLib
{
    /// <summary>
    /// represent class to manage Opengl Font text
    /// </summary>
    internal  class OGLFont : IDisposable 
    {
        internal IntPtr handle;
        internal IntPtr hdc;
        readonly IntPtr NULL = IntPtr.Zero;
        private string m_name;
        private int m_height;
        private int m_width;
        private int m_espacement;
        private int m_orientation;
        private FontStyle m_style;
        //propri�t�
        public string Name { get { return this.m_name; } }
        public int Height { get { return m_height; } }
        public FontStyle Style { get { return m_style; } set { m_style = value; OnGLFontPropertieChange(); } }
        public int Width { get { return m_width; } set { this.m_width = value; OnGLFontPropertieChange(); } }
        public int Orientation { get { return this.m_orientation; } set { this.m_orientation = value; } }
        public int Espacement { get { return this.m_espacement; } set { this.m_espacement = value; } }
        private void OnGLFontPropertieChange()
        {
            if (this.handle != NULL)
            {
                Gdi32Lib.DeleteObject(this.handle);
                handle = NULL;
                this.handle = CreateFont(m_name, m_style, m_width, m_height, m_espacement, m_orientation);
            }
        }
        //.ctr
        internal OGLFont()
        {
        }
        ~OGLFont()
        {
            if (Gdi32Lib.DeleteObject(this.handle))
            {
                this.handle = NULL;
            }
        }
        /// <summary>
        /// make the font current
        /// </summary>
        public void MakeCurrent()
        {            
            if ((this.handle != NULL) && (this.hdc != NULL))
            {
                this.MakeCurrent(this.hdc);
            }
        }
        public OGLFont(IntPtr hdc, Font ft, int height, int width)
        {
            handle = Gdi32Lib.CreateFont(height, width, 0, 0, 0,
                Convert.ToInt16 ((ft.Style & FontStyle.Italic )== FontStyle.Italic ),
                Convert.ToInt16((ft.Style & FontStyle.Underline ) == FontStyle.Underline),
                Convert.ToInt16((ft.Style & FontStyle.Strikeout ) == FontStyle.Strikeout ),
                0, 0, 0, 0, 0,ft.FontFamily.Name );
            if (handle != NULL)
            {
                this.m_name = ft.FontFamily.Name;
                this.hdc = hdc;
                this.m_height = height;
                this.m_width = height;
                this.MakeCurrent();
            }
        }
        public OGLFont(Font ft, int height, int width, int espacement, int orientation)
        {
            handle = CreateFont(ft.FontFamily.Name, ft.Style, width, height, espacement, orientation);
            if (handle != NULL)
            {
                this.m_name = ft.FontFamily.Name;
                this.m_height = height;
                this.m_width = height;
            }
        }
        /// <summary>
        /// Create a System font
        /// </summary>
        /// <param name="fontname"></param>
        /// <param name="style"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="espacement"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        public static IntPtr CreateFont(string fontname, FontStyle style, int width, int height, int espacement, int orientation)
        {
            return  Gdi32Lib.CreateFont(height, width, espacement , orientation,  0,
                Convert.ToInt16((style & FontStyle.Italic) == FontStyle.Italic),
                Convert.ToInt16((style & FontStyle.Underline) == FontStyle.Underline),
                Convert.ToInt16((style & FontStyle.Strikeout) == FontStyle.Strikeout),
                0, 0, 0, 0, 0, fontname);
        }
        /// <summary>
        /// make current hdc and set the hdc to this context
        /// </summary>
        /// <param name="hdc"></param>
        public void MakeCurrent(IntPtr hdc)
        {
            IntPtr old = Gdi32Lib.SelectObject(hdc, this.handle);
            this.hdc = hdc;
        }
        #region IDisposable Members
        public void Dispose()
        {
            if (this.handle != IntPtr.Zero)
            {
                Gdi32Lib.DeleteObject(this.handle);
                this.handle = IntPtr.Zero;
            }
        }
        #endregion
    }
}

