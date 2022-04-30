

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ClearContourAction.cs
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
file:ClearContourAction.cs
*/

using IGK.ICore.Actions;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Contour.Actions
{
    class ClearContourAction : CoreActionBase
    {
        private ContourElement contourElement;
        public ClearContourAction(ContourElement contourElement)
        {
            this.contourElement = contourElement;
        }
        protected override bool PerformAction()
        {
            this.contourElement.ClearContour();
            return true;
        }
        public override string Id
        {
            get { return "{6C6AAEAE-7C6E-4A7F-AC32-D0EB0DDD9A5B}"; }
        }
    }
}

