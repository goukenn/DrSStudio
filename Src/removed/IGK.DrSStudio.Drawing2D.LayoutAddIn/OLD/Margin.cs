

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Margin.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:Margin.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.LayoutAddIn
{
    public struct Margin
    {
        private CoreUnit m_Left;
        private CoreUnit m_Right;
        private CoreUnit m_Top;
        private CoreUnit m_Bottom;
        public CoreUnit Bottom
        {
            get { return m_Bottom; }
            set
            {
                if (m_Bottom != value)
                {
                    m_Bottom = value;
                }
            }
        }
        public CoreUnit Top
        {
            get { return m_Top; }
            set
            {
                if (m_Top != value)
                {
                    m_Top = value;
                }
            }
        }
        public CoreUnit Right
        {
            get { return m_Right; }
            set
            {
                if (m_Right != value)
                {
                    m_Right = value;
                }
            }
        }
        public CoreUnit Left
        {
            get { return m_Left; }
            set
            {
                if (m_Left != value)
                {
                    m_Left = value;
                }
            }
        }
        public readonly static Margin Empty;
        static  Margin()
        {
            Empty = new Margin(0, 0, 0, 0);
        }
        public Margin(int left, int top, int right, int bottom)
        {
            this.m_Left  = left +"px";
            this.m_Right = right+"px" ;
            this.m_Top = top +"px";
            this.m_Bottom = bottom+"px";
        }
        public Margin(string value)
        {
            CoreUnit u = value;
            this.m_Left = u;
            this.m_Right = u;
            this.m_Top = u;
            this.m_Bottom = u;
        }
    }
}

