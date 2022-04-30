

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapSelection.cs
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
file:WebMapSelection.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [WebMapSelectionAttribute("WebMapSelection", typeof(Mecanism)
        , Keys = enuKeys.S)]
    class WebMapSelection : WebMapElementBase 
    {
        class Mecanism : WebMapElementBase.Mecanism<WebMapSelection>
        {
            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                base.OnMouseClick(e);
            }
            protected override void OnMouseDoubleClick(CoreMouseEventArgs e)
            {
                base.OnMouseDoubleClick(e);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
        }
        public override string Render(IWebMapRendererOption option)
        {
            return string.Empty;
        }

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            
        }
    }
}

