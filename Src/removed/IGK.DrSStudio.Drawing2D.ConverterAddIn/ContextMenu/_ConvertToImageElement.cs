

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToImageElement.cs
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
file:_ConvertToImageElement.cs
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
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    [IGK.DrSStudio.ContextMenu.CoreContextMenu("Drawing2D.Convert.ToImagElement", 6)]    
    sealed class _ConvertToImageElement : ContextMenu .Core2DContextMenuBase 
    {
        public _ConvertToImageElement()
        {
            this.IsRootMenu = false;
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                ICore2DDrawingLayeredElement l = this.CurrentSurface.CurrentLayer.SelectedElements[0];
                if (l != null)
                {
                    Rectangle rc = Rectanglef.Round (l.GetBound());
                    ImageElement v_imgl = ImageElement.FromImage(CoreBitmapOperation.GetBitmap(l));
                    if (v_imgl != null)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.Remove(l);
                        v_imgl.Translate(rc.X , rc.Y , System.Drawing.Drawing2D.enuMatrixOrder.Append );
                        this.CurrentSurface.CurrentLayer.Elements.Add(v_imgl);
                    }
                }
            }
            return false;
        }
    }
}

