

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapCircle.cs
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
file:WebMapCircle.cs
*/

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Web.WorkingObject
{
    using IGK.DrSStudio.Drawing2D.Mecanism;
    using IGK.ICore.Drawing2D.Mecanism;
    [WebMapElementStandardAttribute("MapCircleArea",
        typeof(CircleMecanism)
        ,Keys = enuKeys.C)]
    class WebMapCircle : WebMapRegion, IWebMapCircleArea
    {
        private float m_Radius;
        private Vector2i  m_Center;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("0;0")]
        public Vector2i  Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (0)]
        public float Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public WebMapCircle():base()
        {
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            path.AddEllipse(this.Center, new Vector2f ( this.Radius, this.Radius ));
            
        }
        public override enuWebMapAreaType Type
        {
            get { return enuWebMapAreaType.Circle; }
        }
        protected override string GetCoords()
        {
            Vector2i c = this.Center;
            return string.Format("{0},{1},{2}", c.X, c.Y, this.Radius );
        }
    }
}

