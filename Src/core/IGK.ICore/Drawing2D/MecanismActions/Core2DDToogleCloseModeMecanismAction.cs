

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDToogleCloseModeMecanismAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.MecanismActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.MecanismActions
{
    class Core2DDToogleCloseModeMecanismAction : CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {
            ICore2DClosableElement c = this.Mecanism.GetEditElement() as ICore2DClosableElement;
            if (c == null)
                return false;
            c.Closed = !c.Closed;
            Mecanism.Invalidate();
            return true;
        }
    }
}
