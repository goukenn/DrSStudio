

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: EditElementMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu.Edit
{
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu("Edit.EditElementProperty", 0x1040, Shortcut = enuKeys.Control | enuKeys.E, SeparatorBefore=true)]
    class EditElementPropertyMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            var s = GetSelectedObject(0) as ICoreWorkingConfigurableObject;
            if (s != null)
            {
                CoreSystem.Instance.Workbench.ConfigureWorkingObject(
                  s,
                  "title.editElementProperty".R(s.Id),
                  false,
                  new Size2i (460, 580));
            }
            return false ;
        }
        protected override bool IsEnabled()
        {
            return (this.CurrentSurface !=null) && 
                    (this.CurrentSurface .CurrentLayer !=null) 
                        &&
                   (this.CurrentSurface.CurrentLayer.SelectedElements.Count>0);
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
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.SetupEnableAndVisibility();
        }

        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
        private ICore2DDrawingLayeredElement GetSelectedObject(int p)
        {
            if (this.Enabled && (p >= 0) && (p < this.CurrentSurface.CurrentLayer.SelectedElements.Count))
            {
                return this.CurrentSurface.CurrentLayer.SelectedElements[p];
            }
            return null;
        }

    }
}
