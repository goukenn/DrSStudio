

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _CropDocumentToElementSizeContextMenu.cs
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
file:_CropElementToImageSize.cs
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
﻿
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Image
{
    using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
    using IGK.ICore.ContextMenu;
    using IGK.ICore.WinUI;
    using IGK.DrSStudio.ContextMenu;
    using IGK.ICore;
    [DrSStudioContextMenu("Drawing2D.Edit.CropDocumentToElementSize",
        0x101 
        )]
    class _CropDocumentToElementSizeContextMenu : IGKD2DDrawingContextMenuBase
    {
        public _CropDocumentToElementSizeContextMenu()
        {
            IsRootMenu = true;
        }
        protected override bool PerformAction()
        {   
                if (this.CurrentSurface.CurrentLayer.SelectedElements .Count == 1)
                {
                    Core2DDrawingLayeredElement v_element = this.CurrentSurface.CurrentLayer.SelectedElements[0]
                        as Core2DDrawingLayeredElement ;
                    if (v_element !=null) //is ImageElement))
                    {
                        Rectanglef v_Rc = v_element.GetBound();
                        v_element.Translate(-v_Rc.X, -v_Rc.Y, enuMatrixOrder.Append);
                        this.CurrentSurface.CurrentDocument.SetSize((int)v_Rc.Width, (int)v_Rc.Height);
                        this.CurrentSurface.RefreshScene();
                    }
                }
            return false;
        }
        //protected override bool IsVisible()
        //{
        //    ICoreContextMenu v_ctx = this.OwnerContext;
        //    bool v = false;
        //    if (v_ctx == null)
        //    {
        //        v = false;
        //    }
        //    else
        //    {
        //        v = (( CurrentSurface!=null) && (v_ctx.SourceControl == CurrentSurface) && this.AllowContextMenu)
        //            &&
        //        (this.CurrentSurface !=null)&&(this.CurrentSurface.CurrentLayer.SelectedElements .Count == 1)                
        //        && !(this.CurrentSurface.CurrentLayer.SelectedElements[0] is ImageElement )
        //        && this.AllowContextMenu 
        //            ;
        //    }
        //    return v;
        //}

        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1) && this.CheckOverSingleElement();
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += layer_SelectedElementChanged;
        }

        private void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
    }
}

