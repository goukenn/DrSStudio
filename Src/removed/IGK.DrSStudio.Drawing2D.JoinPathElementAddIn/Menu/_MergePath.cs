

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MergePath.cs
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
file:_MergePath.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Tools.Drawing2DPath.MergePath", 4, ImageKey = "Menu_Joint")]
    class _MergePath : Core2DMenuBase
    {
        protected override bool PerformAction()
        {
            GraphicsPath v_p = null;
            List<GraphicsPath> v_lpath = new List<GraphicsPath>();
            foreach (ICore2DDrawingLayeredElement l in this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements)
            {
                v_lpath.Add(l.GetPath().Clone() as GraphicsPath);
            }
            v_p = PathOperator.MergePath(v_lpath.ToArray ());
            if (v_p != null)
            {
                this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(
                      PathElement.Create(v_p)
                      );
            }
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count >= 2);
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

