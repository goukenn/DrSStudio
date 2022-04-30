

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _DeleteSelectedElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.ContextMenu;
using IGK.ICore.ContextMenu;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.ContextMenu.Edit
{

        [DrSStudioContextMenu("Drawing2D.Delete",
        short.MaxValue,
        ImageKey = "Menu_Delete",
        ShortCut = enuKeys.Delete)]
    class _DeleteSelectedElement : IGKD2DDrawingContextMenuBase
    {
            protected override bool IsVisible()
            {
                return base.IsVisible() && (this.CurrentSurface.CurrentLayer.SelectedElements.Count > 0);
            }
            protected override bool IsEnabled()
            {
                return this.IsVisible();
            }
            protected override bool PerformAction()
            {
                if (this.Visible)
                {
                    var s = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
                    if (s.Length > 0)
                    {
                        this.CurrentSurface.CurrentLayer.Elements.RemoveAll(s);
                        return true;
                    }
                }
                return false;
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
