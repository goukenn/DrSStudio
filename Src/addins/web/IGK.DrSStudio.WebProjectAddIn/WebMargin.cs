

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMargin.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebMargin.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn
{
    [StructLayout(LayoutKind.Sequential )]
    /// <summary>
    /// represent a web margin
    /// </summary>
    public class WebMargin
    {
        private WebLengthUnit m_Left = "0px";
        private WebLengthUnit m_Top = "0px";
        private WebLengthUnit m_Right = "0px";
        private WebLengthUnit m_Bottom = "0px";
        public WebMargin()
        {
        }
        public WebMargin(WebLengthUnit unit)
        {
            m_Left = unit;
            m_Top = unit;
            m_Right = unit;
            m_Bottom = unit;
        }
        /// <summary>
        /// get or set margin bottom
        /// </summary>
        public WebLengthUnit Bottom
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
        /// <summary>
        /// get or set right margin right
        /// </summary>
        public WebLengthUnit Right
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
        /// <summary>
        /// uo
        /// </summary>
        public WebLengthUnit Top
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
        public WebLengthUnit Left
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
        public string ToString(bool css)
        { 
            StringBuilder sb = new StringBuilder ();
            sb.Append(this.m_Left.ToString());
            return sb.ToString();
        }
    }
}

