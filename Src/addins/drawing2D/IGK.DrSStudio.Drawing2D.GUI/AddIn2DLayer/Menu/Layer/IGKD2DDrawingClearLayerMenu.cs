

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingClearLayerMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingClearLayerMenu.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Layer
{
    [IGKD2DDrawingLayerMenuAttribute("ClearLayer", 2,
        IsShortcutMenuChild = true,
        Shortcut = enuKeys.Shift | enuKeys.Delete,
        ImageKey=CoreImageKeys.MENU_CLEAR2_GKDS)]
    class IGKD2DDrawingClearLayerMenu : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            this.CurrentLayer.Clear();
            return true;
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible() && (this.CurrentLayer.Elements.Count > 0);
        }
        protected override bool IsVisible()
        {
            return this.DefaultVisible && (this.CurrentSurface != null);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.ElementAdded +=_updateLayerView;
            layer.ElementRemoved  += _updateLayerView;
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.ElementAdded -= _updateLayerView;
            layer.ElementRemoved -= _updateLayerView;
            base.UnRegisterLayerEvent(layer);
        }
        private void _updateLayerView(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}

