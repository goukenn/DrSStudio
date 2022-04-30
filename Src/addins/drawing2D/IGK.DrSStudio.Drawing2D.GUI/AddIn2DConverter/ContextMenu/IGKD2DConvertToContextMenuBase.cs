

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DConvertToContextMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    
    public abstract class IGKD2DConvertToContextMenuBase : IGKD2DDrawingContextMenuBase
    {
        public IGKD2DConvertToContextMenuBase()
        {

        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += layer_SelectedElementChanged;
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {

            layer.SelectedElementChanged -= layer_SelectedElementChanged;
            base.UnRegisterLayerEvent(layer);
        }

        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
