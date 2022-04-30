

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _CropDocumentToImageSizeContextMenu.cs
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
file:_CropDocumentToImageSize.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Image
{
    using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
    using IGK.ICore.ContextMenu ;
    using IGK.DrSStudio.ContextMenu;
    using IGK.ICore;
    [DrSStudioContextMenu("Drawing2D.Image.CropDocumentToImageSize",
        0x100, 
        SeparatorBefore=true )]
    class _CropDocumentToImageSizeContextMenu : ImageContextMenuBase 
    {
        protected override bool PerformAction()
        {
            if ((this.ImageElement!=null) && !this.ImageElement.Locked)
            {
                Rectanglef v_Rc = this.ImageElement.GetBound();
                
                this.CurrentSurface.CurrentDocument.SetSize((int)v_Rc.Width, (int)v_Rc.Height);
                this.ImageElement.Translate(-v_Rc.X, -v_Rc.Y, enuMatrixOrder.Append);
                this.CurrentSurface.RefreshScene();
            }
            return false;
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && ((this.ImageElement != null) && !this.ImageElement.Locked);            
        }
        protected override void OnOpening(EventArgs e)
        {
            base.OnOpening(e);
            this.Enabled = ((this.ImageElement !=null)&&  !this.ImageElement .Locked );
        }
    }
}

