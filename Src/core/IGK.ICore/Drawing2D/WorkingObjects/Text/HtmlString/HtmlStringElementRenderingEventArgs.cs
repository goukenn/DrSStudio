

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlStringElementRenderingEventArgs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public class HtmlStringElementRenderingEventArgs : EventArgs 
    {
        private float m_X;
        private float m_Y;
        private Rectanglef m_Bounds;
        private float m_VirtualLineHeight;
        private float m_defaultLineHeight;
        /// <summary>
        /// get or set the virtual line height
        /// </summary>
        public float VirtualLineHeight
        {
            get { return m_VirtualLineHeight; }
            set
            {
                if (m_VirtualLineHeight != value)
                {
                    m_VirtualLineHeight = value;
                }
            }
        }

        /// <summary>
        /// Get the default line height
        /// </summary>
        public float DefaultLineHeight {
            get {
                return this.m_defaultLineHeight;
            }
        }
        public Rectanglef Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds != value)
                {
                    m_Bounds = value;
                }
            }
        }

        public float Y
        {
            get { return m_Y; }
            set
            {
                if (m_Y != value)
                {
                    m_Y = value;
                }
            }
        }
        public float X
        {
            get { return m_X; }
            set
            {
                if (m_X != value)
                {
                    m_X = value;
                }
            }
        }
        public HtmlStringElementRenderingEventArgs(
            Rectanglef rc , float defaultLineHeight, float x, float y)
        {
            this.m_Bounds = rc;
            this.m_defaultLineHeight = defaultLineHeight;
            this.m_X = x;
            this.m_Y = y;
        }
    }
}
