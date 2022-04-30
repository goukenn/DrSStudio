


using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLShapeRectangle.cs
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
file:GLShapeRectangle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.OGLGame.WinUI;

namespace IGK.OGLGame.WinUI.GLControls
{
    /// <summary>
    /// represent the rectangle shape
    /// </summary>
    public class GLShapeRectangle : GLShape 
    {
        private Colorf m_FillColor;
        private Colorf m_StrokeColor;
        public Colorf StrokeColor
        {
            get { return m_StrokeColor; }
            set
            {
                if (!m_StrokeColor.Equals ( value))
                {
                    m_StrokeColor = value;
                }
            }
        }
        public Colorf FillColor
        {
            get { return m_FillColor; }
            set
            {
                if (m_FillColor.Equals (value)==false)
                {
                    m_FillColor = value;
                }
            }
        }
        public GLShapeRectangle()
        {
            this.m_FillColor = Colorf.White;
            this.m_StrokeColor = Colorf.Black;
        }
        protected internal override void Render(GLControlTime ControlTime)
        {
            IGK.OGLGame.Graphics.GLDrawUtils.FillRect(this.GraphicsDevice, this.FillColor , this.Bounds);
            IGK.OGLGame.Graphics.GLDrawUtils.DrawRect(this.GraphicsDevice, this.StrokeColor, 1  , this.Bounds);
        }
    }
}

