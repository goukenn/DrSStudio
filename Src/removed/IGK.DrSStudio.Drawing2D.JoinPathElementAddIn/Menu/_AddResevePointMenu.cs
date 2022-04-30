

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _AddResevePointMenu.cs
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
file:_AddResevePointMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu ("Tools.Drawing2DPath.AddReversedPoint", 4, ImageKey="Menu_Joint")]   
    class _AddResevePointMenu: Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
            PathElement p = this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements[0] as PathElement ;
            if (p == null)
                return false;
            GraphicsPath  v_p = p.GetPath();
            PathOperator.AddReversePoint(v_p, true);
            v_p.CloseAllFigures();
            p.SetPathDefinition(v_p.PathPoints, v_p.PathTypes);
            this.CurrentSurface.Invalidate();
            return true;
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count == 1);                
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}

