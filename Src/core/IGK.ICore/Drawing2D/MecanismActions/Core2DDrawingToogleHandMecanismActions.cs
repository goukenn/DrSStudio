

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingToogleHandMecanismActions.cs
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
file:Core2DDrawingToogleHandMecanismActions.cs
*/
using IGK.ICore;using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.MecanismActions
{
    /// <summary>
    /// represent a ToogleStyle Mecanism
    /// </summary>
    public sealed class Core2DDrawingToogleHandMecanismActions : CoreMecanismActionBase
    {
        protected override bool PerformAction()
        {
            ICoreHandStyleMecanism m = this.Mecanism as ICoreHandStyleMecanism;
            switch (this.ShortCutDemand)
            { 
                case enuKeys.F :
                    m.HandStyle = enuHandStyle.FreeHand;
                    break;
                case enuKeys.V:
                    m.HandStyle = enuHandStyle.Vertical;
                    break;
                case enuKeys.H:
                    m.HandStyle = enuHandStyle.Horizontal;
                    break;
                default :
                    return false;
            }
            return true;
        }
    }
}

