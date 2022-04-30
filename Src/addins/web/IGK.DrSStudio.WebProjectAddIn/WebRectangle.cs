

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebRectangle.cs
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
file:WebRectangle.cs
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
    public struct WebRectangle
    {
        private WebLengthUnit m_X;
        private WebLengthUnit m_Y;
        private WebLengthUnit m_Width;
        private WebLengthUnit m_Height;
        public override bool Equals(object obj)
        {
            WebRectangle c = (WebRectangle)obj;
            return (this.m_X.GetValue(enuWebUnitType.px) == c.X.GetValue(enuWebUnitType.px) &&
                this.m_Y.GetValue(enuWebUnitType.px) == c.Y.GetValue(enuWebUnitType.px) &&
                this.m_Width.GetValue(enuWebUnitType.px) == c.Width.GetValue(enuWebUnitType.px) &&
                this.m_Height.GetValue(enuWebUnitType.px) == c.Height .GetValue(enuWebUnitType.px));
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public readonly static WebRectangle Empty;
        public bool IsEmpty {
            get {
                return ((this.Width.Value <= 0) || (this.Height.Value <= 0));
            }
        }
        static WebRectangle()
        {
            Empty = new WebRectangle();
        }
        public override string ToString()
        {
            return string.Format("{0};{1};{2};{3}", this.X, this.Y, this.Width, this.Height);
        }
        public WebLengthUnit Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                }
            }
        }
        public WebLengthUnit Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                }
            }
        }
        public WebLengthUnit Y
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
        public WebLengthUnit X
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
        public WebRectangle(Rectanglei rc)
        {
            this.m_X = rc.X + "px";
            this.m_Y = rc.Y + "px";
            this.m_Width = rc.Width + "px";
            this.m_Height = rc.Height + "px";
        }
        public static implicit operator WebRectangle(Rectanglei rc)
        {
            return new WebRectangle(rc);
        }
        public Rectanglei GetRect(Rectanglei parentRectangle)
        {
            Rectanglei rc = new Rectanglei();
            if (this.X.UnitType == enuWebUnitType.percent)
            {
                rc.X = (int)(parentRectangle.X + (parentRectangle.Width * this.X.Value / 100.0f));
            }
            else {
                rc.X = (int)(parentRectangle.X + this.X.GetValue(enuWebUnitType.px));
            }
            if (this.Y.UnitType == enuWebUnitType.percent)
            {
                rc.Y = (int)(parentRectangle.Y + (parentRectangle.Height * this.Y.Value / 100.0f));
            }
            else
            {
                rc.Y = (int)(parentRectangle.Y + this.Y.GetValue(enuWebUnitType.px));
            }
            if (this.Width.UnitType == enuWebUnitType.percent)
            {
                rc.Width = (int)((parentRectangle.Width * this.Width.Value / 100.0f));
            }
            else
            {
                rc.Width = (int)(this.Width.GetValue(enuWebUnitType.px));
            }
            if (this.Height.UnitType == enuWebUnitType.percent)
            {
                rc.Height = (int)((parentRectangle.Height * this.Height.Value / 100.0f));
            }
            else
            {
                rc.Height = (int)(this.Height.GetValue(enuWebUnitType.px));
            }
            return rc;
        }
    }
}

