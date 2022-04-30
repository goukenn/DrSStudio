

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BezierActionBase.cs
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
    /// <summary>
    /// represent the base action of bezier element
    /// </summary>
    public abstract class BezierActionBase : CoreMecanismActionBase 
    {
        protected internal  new BezierElement.Mecanism Mecanism {
            get {
                return base.Mecanism as BezierElement.Mecanism;
            }
        }


    }
}
