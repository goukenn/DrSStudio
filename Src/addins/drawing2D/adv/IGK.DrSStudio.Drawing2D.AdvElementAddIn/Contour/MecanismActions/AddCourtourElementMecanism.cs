

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AddCourtourElementMecanism.cs
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
file:AddCourtourElementMecanism.cs
*/

using IGK.ICore.Actions;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Contour.MecanismActions
{  
    /// <summary>
    /// add contour element mecanism actions
    /// </summary>
    class AddCourtourElementMatrix : ContourElementMecanismActionBase 
    {
        protected override bool PerformAction()
        {
            this.Mecanism.AddContour();
            return true;
        }
    }
}

