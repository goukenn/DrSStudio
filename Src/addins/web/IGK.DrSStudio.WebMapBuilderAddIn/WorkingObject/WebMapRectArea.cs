

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapRectArea.cs
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
file:WebMapRectArea.cs
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
    [WebMapElementStandardAttribute("MapRectArea",
        typeof(Mecanism)
        , Keys = enuKeys.R)]
    class WebMapRectArea : WebMapRegion, IWebMapRectangleArea
    {
        public override enuWebMapAreaType Type
        {
            get { return enuWebMapAreaType.Rectangle; }
        }
        protected override string GetCoords()
        {
            Rectanglef c = this.Bounds ;
            return string.Format("{0},{1},{2},{3}", c.X, c.Y, c.Right , c.Bottom );
        }
        //[CoreXMLAttribute()]
        //[CoreXMLDefaultAttributeValue("0;0;0;0")]
        //public Rectanglei Bounds
        //{
        //    get
        //    {
        //        return this.m_Bounds;
        //    }
        //    set
        //    {
        //        if (!this.m_Bounds.Equals(value))
        //        {
        //            this.m_Bounds = value;
        //            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        //        }
        //    }
        //}
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            path.AddRectangle(this.Bounds);
        }

        public new class Mecanism : WebMapElementBase.Mecanism<WebMapRectArea>
        { 
        }
    }
}

