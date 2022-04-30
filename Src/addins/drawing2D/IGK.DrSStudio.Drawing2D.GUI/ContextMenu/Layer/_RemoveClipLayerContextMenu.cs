

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RemoveClipLayerContextMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.ContextMenu;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.ContextMenu.Layer
{
      [
    CoreContextMenu("Drawing2D.Clip.RemoveClipLayer", 0x41)
    ]
    class _RemoveClipLayerContextMenu : IGKD2DDrawingContextMenuBase
    {
          public _RemoveClipLayerContextMenu()
          {
              this.IsRootMenu = false;
          }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                ICore2DDrawingLayeredElement v_element = this.CurrentSurface.CurrentLayer.SelectedElements[0];
                ICore2DDrawingLayer v_layer = this.CurrentSurface.CurrentDocument.CurrentLayer;
                if (v_layer.IsClipped)
                {
                    v_layer.SetClip (null);
                    this.CurrentSurface.RefreshScene();
                }
                return true;
            }
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && this.CurrentSurface.CurrentDocument.CurrentLayer.IsClipped;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && this.CurrentSurface.CurrentDocument.CurrentLayer.IsClipped;
        }
    }
}
