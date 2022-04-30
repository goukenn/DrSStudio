

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextEditorRenderingEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:TextEditorRenderingEventArgs.cs
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
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.XMLEditorAddIn.Segment
{
    public class TextEditorRenderingEventArgs : EventArgs 
    {
        private Graphics m_Graphics;
        private int m_OffsetY;
        private int m_OffsetX;
        private int m_TabWidth;
        private Font m_Font;
        private int m_LineHeight;
        public int TabWidth
        {
            get { return m_TabWidth; }
        }
        /// <summary>
        /// Get or set the x offset
        /// </summary>
        public int OffsetX
        {
            get { return m_OffsetX; }
            set
            {
                if (m_OffsetX != value)
                {
                    m_OffsetX = value;
                }
            }
        }
        /// <summary>
        /// get or set the y offset
        /// </summary>
        public int OffsetY
        {
            get { return m_OffsetY; }
            set
            {
                if (m_OffsetY != value)
                {
                    m_OffsetY = value;
                }
            }
        }
        public Graphics Graphics
        {
            get { return m_Graphics; }
        }
        /// <summary>
        /// get the line height
        /// </summary>
        public int LineHeight
        {
            get { return m_LineHeight; }
        }
        public Font Font
        {
            get { return m_Font; }
        }
        private Rectangle  m_Rectangle;
        public Rectangle  Rectangle
        {
            get { return m_Rectangle; }
        }
        public TextEditorRenderingEventArgs(Graphics g, Font font, 
            int lineHeight, 
            int tabWidth,
            Rectangle rc)
        {
            this.m_Graphics = g;
            this.m_LineHeight = lineHeight;
            this.m_Font = font;
            this.m_Rectangle = rc;
            this.m_TabWidth = tabWidth;
        }
    }
}

