

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AddWireSegmentAction.cs
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
file:AddWireSegmentAction.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WireAddIn.Actions
{
    class AddWireSegmentAction : IGK.DrSStudio.WireAddIn.WireBezierElement.Mecanism.WireSegmentActionBase
    {
        public AddWireSegmentAction(WireBezierElement.Mecanism mecanism):base(mecanism )
        {
        }
        protected override bool PerformAction()
        {
            if (this.Mecanism.Element == null)
                return false ;
            WireBezierElement wb = this.Mecanism.Element;
           Vector2f mid = CoreMathOperation.GetMiddlePoint  (wb.StartPoint, wb.EndPoint );
             this.Mecanism.Element.AddDefinition (mid, mid  );
             return true;
        }
    }
}

