

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDToogleFillModeMecanismAction.cs
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
file:Core2DDToogleFillModeMecanismAction.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.MecanismActions
{
    using IGK.ICore;using IGK.ICore.MecanismActions;
    class Core2DDToogleFillModeMecanismAction : CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {
            
            ICore2DFillModeElement v_c = this.Mecanism.GetEditElement() as ICore2DFillModeElement;
            if (v_c != null)
            {
                switch (v_c.FillMode)
                {
                    case enuFillMode.Winding:
                        v_c.FillMode = enuFillMode.Alternate;
                        this.Mecanism.Invalidate();
                        return true;
                    case enuFillMode.Alternate:
                        v_c.FillMode = enuFillMode.Winding;
                        this.Mecanism.Invalidate();
                        return true;                        
                }
            }
            return false;
        }
    }
}

