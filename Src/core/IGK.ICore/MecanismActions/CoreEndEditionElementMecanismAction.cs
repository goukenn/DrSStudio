

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreEndEditionElementMecanismAction.cs
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
file:CoreEndEditionElementMecanismAction.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.MecanismActions
{
    class CoreEndEditionElementMecanismAction : CoreMecanismActionBase
    {       
        protected override bool PerformAction()
        {
            ICoreEditableWorkingMecanism m = this.Mecanism
                as ICoreEditableWorkingMecanism;
            if (m!=null)
                m.EndEdition();
            return true;
        }
    }
}

