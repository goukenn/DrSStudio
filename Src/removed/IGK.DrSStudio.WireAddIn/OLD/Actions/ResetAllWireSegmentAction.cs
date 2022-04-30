

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ResetAllWireSegmentAction.cs
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
file:ResetAllWireSegmentAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WireAddIn.Actions
{
    class ResetAllWireSegmentAction :  IGK.DrSStudio.WireAddIn.WireBezierElement.Mecanism.WireSegmentActionBase
    {
        public ResetAllWireSegmentAction(WireBezierElement.Mecanism mecanism)
            : base(mecanism)
        {
        }
        protected override bool PerformAction()
        {
            if (this.Mecanism.Element == null)
                return false;
            this.Mecanism.Element.ResetAllDefinitions();
            return true;
        }
    }
}

