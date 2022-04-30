

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingMecanismActionBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.MecanismActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.MecanismActions
{
    /// <summary>
    /// reprent a 2d drawing mecanism
    /// </summary>
    public abstract class Core2DDrawingMecanismActionBase : CoreMecanismActionBase
    {
        public ICore2DDrawingSurface Surface {
            get {
                return this.Mecanism.Surface as ICore2DDrawingSurface;
            }
        }
    }
}
