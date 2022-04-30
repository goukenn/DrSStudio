using IGK.DrSStudio.Drawing2D.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.AddIn2DLayer.Menu
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WorkingObjects.Standard;

    [IGKD2DDrawingLayerMenuAttribute("FlattentLayerMenu", 0x50)]
    /// <summary>
    /// create a layer object that with only content a single object 
    /// </summary>
    class FlattenLayerMenu : IGKD2DDrawingLayerMenuBase
    {
        protected override bool PerformAction()
        {
            var v_layers = this.CurrentSurface.CurrentDocument.Layers;
            if (v_layers.Count > 1) {
                LayerObjectElement element = new LayerObjectElement();
                Core2DDrawingLayer layer = new Core2DDrawingLayer();

                foreach (Core2DDrawingLayer i in v_layers.ToArray()) {

                    element.AddLayer(i);
                }
                layer.Elements.Add(element);

                this.CurrentDocument.Layers.Replace(
                   new Core2DDrawingLayer[]{ layer}
                 );
            }
            return false;
        }
    }
}
