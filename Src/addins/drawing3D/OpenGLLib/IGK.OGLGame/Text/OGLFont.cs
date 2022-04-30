

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
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.OGLGame.Text
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// represent the font class
    /// </summary>
    internal class OGLFont : IDisposable 
    {
        internal IntPtr m_handle;
        internal IntPtr m_hdc;
        static readonly IntPtr NULL = IntPtr.Zero;
        private string m_name;
        private int m_height;
        private int m_width;
        private int m_espacement;
        private int m_orientation;
        private FontStyle m_style;
        //*properties
        public string Name { get { return this.m_name; } }
        public int Height { get { return m_height; } }
        public FontStyle Style { get { return m_style; } set { m_style = value;  } }
        public int Width { get { return m_width; } set { this.m_width = value;  } }
        public int Orientation { get { return this.m_orientation; } set { this.m_orientation = value; } }
        public int Espacement { get { return this.m_espacement; } set { this.m_espacement = value; } }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HDC">hdc </param>
        /// <param name="ft">drawing font</param>
        /// <param name="height">set the height in pixel</param>
        /// <param name="space">set the caracter space</param>
        /// <returns></returns>
        internal static OGLFont CreateFont(IntPtr HDC, System.Drawing.Font ft, int height, int space)
        {
             IntPtr handle = CreateFont(ft.FontFamily.Name, ft.Style, Convert.ToInt32 (height), space, 0, 0);
            if (handle != NULL)
            {
                OGLFont t = new OGLFont();
                t.m_name = ft.FontFamily.Name;
                t.m_hdc = HDC;
                t.m_handle = handle;
                t.m_height = height;
                t.m_width = space;
                t.MakeCurrent();
                return t;
            }
            return null;            
        }
         private static IntPtr CreateFont(
             string fontname, 
            FontStyle style, 
            int height, 
            int width, 
            int espacement, 
            int orientation)
        {
#if OS_WINDOWS
			return  Gdi32Lib.CreateFont(height, width, espacement , orientation, 
                Convert.ToInt16(((style & FontStyle.Bold ) == FontStyle.Bold)? FontStyle.Bold : FontStyle.Regular ),
                Convert.ToInt16((style & FontStyle.Italic) == FontStyle.Italic),
                Convert.ToInt16((style & FontStyle.Underline) == FontStyle.Underline),
                Convert.ToInt16((style & FontStyle.Strikeout) == FontStyle.Strikeout),
                0, 0, 0, 
                Convert.ToInt16(FontQuality.AntiAlized),
                0,
                fontname);
#else
			return IntPtr.Zero ;
#endif
        }
        /// <summary>
        /// make this font the current font
        /// </summary>
        public void MakeCurrent()
        {
            if ((this.m_handle != GL. NULL) && (this.m_hdc != NULL))
            {
                this.MakeCurrent(this.m_hdc);
            }            
        }
        /// <summary>
        /// make current hdc and set the hdc to this context
        /// </summary>
        /// <param name="hdc"></param>
        public void MakeCurrent(IntPtr hdc)
        {
            IntPtr old = Gdi32Lib.SelectObject(hdc, this.m_handle);
            this.m_hdc = hdc;
        }
        public void Dispose()
        {
            if (this.m_handle != IntPtr.Zero)
            {
                Gdi32Lib.DeleteObject(this.m_handle);
                this.m_handle = IntPtr.Zero;
            }
        }
    }
}

