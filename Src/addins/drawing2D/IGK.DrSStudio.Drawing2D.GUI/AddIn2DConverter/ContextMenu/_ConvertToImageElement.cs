

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
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
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
using IGK.ICore;
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    [IGKD2DConvertToContextMenuAttribute("ImagElement", 6)]
    sealed class _ConvertToImageElement : IGKD2DConvertToContextMenuBase 
    {
        public _ConvertToImageElement()
        {
            this.IsRootMenu = false;
        }
        protected override bool IsVisible()
        {
            return (this.CurrentSurface != null);
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface != null) &&
                  (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 0)
                  &&
                  CheckOverSingleElement(); 
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                ICore2DDrawingLayeredElement l = this.CurrentSurface.CurrentLayer.SelectedElements[0];
                if (l != null)
                {
                    Rectanglef rc = Rectanglef.Round (l.GetBound());
                    ImageElement v_imgl = ImageElement.CreateFromBitmap (
                        WinCoreBitmapOperation.GetBitmap(l)
                        );
                    if (v_imgl != null)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.Remove(l);
                        v_imgl.Translate(rc.X , rc.Y , enuMatrixOrder.Append );
                        this.CurrentSurface.CurrentLayer.Elements.Add(v_imgl);
                    }
                }
            }
            return false;
        }
    }
}

