
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddInLayerTools.Menu
{
    using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    using IGK.ICore.Menu;
    [IGKD2DDrawingLayerMenuAttribute("MergeAllLayers", 0x081)]
    class LayerMergeAllLayersMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentDocument.Layers.Count <= 1) {
                return false;
            }

            var t = this.CurrentSurface.CurrentDocument.Layers.ToArray();
            var l = new Core2DDrawingLayer();
            foreach (var item in t)
            {
                l.Elements.AddRange(item.Elements.ToArray());
            }
            this.CurrentSurface.CurrentDocument.Layers.Replace(new ICore2DDrawingLayer[] { 
                l
            });

            return true;
        }
    }
}
