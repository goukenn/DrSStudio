

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DeleteElementAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:DeleteElementAction.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Actions
{
    class DeleteElementAction : Core2DDrawingMecanismAction
    {
        protected override bool PerformAction()
        {
            ICore2DDrawingLayer v_layer = this.Mecanism.CurrentLayer;
            if (v_layer.SelectedElements.Count > 0)
            {
                ICore2DDrawingLayeredElement[] v_elts = v_layer.SelectedElements.ToArray();
                v_layer.Select(null);
                v_layer.Elements.Remove(v_elts);
                this.Mecanism.CurrentSurface.Invalidate();
                this.Mecanism.DisableSnippet();
                return true;
            }
            return false;
        }
    }
}

