

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDeleteElementMecanismAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.MecanismActions
{
    /// <summary>
    /// remove selected element from the current layer
    /// </summary>
    public class Core2DDrawingDeleteElementMecanismAction : Core2DDrawingMecanismActionBase
    {
        protected override bool PerformAction()
        {
            ICore2DDrawingLayeredElement[] t = this.Surface.CurrentLayer.SelectedElements.ToArray();
            if (t.Length > 0)
            {
                this.Surface.CurrentLayer.Select(null);
                for (int i = 0; i < t.Length; i++)
                {
                    this.Surface.CurrentLayer.Elements.Remove (t[i]);
                }
                return true;
            }
            return false;
        }
    }
}
