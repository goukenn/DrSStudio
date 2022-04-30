

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VerticalWebLayoutEngine.cs
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
file:VerticalWebLayoutEngine.cs
*/
using IGK.DrSStudio.WebProjectAddIn.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn.Layout
{
    public class VerticalWebLayoutEngine : WebLayoutEngineBase
    {
        public VerticalWebLayoutEngine()
        {
        }
        public override void InitLayout()
        {
            base.InitLayout();
        }
        public override bool Layout(ICore2DDrawingLayer layer)
        {
            Rectanglei v_parentDocument = new Rectanglei(0, 0, layer.Parent.Width, layer.Parent.Height);
            Rectanglei elemBound = Rectanglei.Empty ;
            WebHtmlElementBase v_el = null;
            Vector2i nextControlLocation = v_parentDocument.Location;
            foreach (ICore2DDrawingLayeredElement element in layer.Elements )
            {
                v_el = element as WebHtmlElementBase;
                if (v_el != null)
                { 
                    //order veritcaly
                    nextControlLocation.Offset(
                        (int)v_el.Style.Margin.Left.GetValue(enuWebUnitType.px),
                        (int)v_el.Style.Margin.Top.GetValue(enuWebUnitType.px));
                    elemBound.Location = nextControlLocation;
                    Size2i v_size = v_el.GetPreferredSize(v_parentDocument);
                    elemBound.Size = v_size;
                    v_el.Bound = elemBound;
                    nextControlLocation.X = v_parentDocument.X;
                    nextControlLocation.Y += elemBound.Height + (int) v_el.Style.Margin.Bottom.GetValue(enuWebUnitType.px);
                }
            }
            return base.Layout(layer);
        }
    }
}

