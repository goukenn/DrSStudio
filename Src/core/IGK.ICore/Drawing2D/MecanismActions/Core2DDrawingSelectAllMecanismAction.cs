

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingSelectAllMecanismAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.MecanismActions
{
    using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.MecanismActions;
    using IGK.ICore.WinUI;

    class Core2DDrawingSelectAllMecanismAction : CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {
            ICoreWorkingSelectionSurface v_s = this.Mecanism.Surface as ICoreWorkingSelectionSurface;
            if (v_s != null)
            {
                v_s.SelectAll();
                return true;
            }
            return false;
            
        }
    }
}
