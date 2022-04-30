

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapPolyArea.cs
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
file:WebMapPolyArea.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [WebMapElementStandardAttribute("MapPolyArea", 
        typeof(IGK.DrSStudio.Drawing2D.Mecanism.PolyMecanism)
        ,Keys = enuKeys.Shift | enuKeys.C)]
    class WebMapPolyArea : WebMapRegion, ICore2DDrawingLayeredElement
    {
        private Vector2i[] m_Coords;
        [CoreXMLAttribute ()]
        public Vector2i[] Coords
        {
            get { return m_Coords; }
            set
            {
                if (m_Coords != value)
                {
                    m_Coords = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override enuWebMapAreaType Type
        {
            get { return enuWebMapAreaType.Poly; }
        }
        protected override string GetCoords()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Coords.Length; i++)
            {
                Vector2i c = this.Coords[i];
                if (i > 0)
                    sb.Append(",");
                sb.Append(string.Format("{0},{1}", c.X, c.Y));
            }
            return sb.ToString();
        }
    
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            List<Vector2f> v_l = new List<Vector2f>();
            for (int i = 0; i < this.Coords.Length; i++)
            {
                v_l.Add(this.Coords[i]);
            }
            path.AddPolygon(v_l.ToArray());
        }
    }
}

