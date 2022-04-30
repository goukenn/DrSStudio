using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SymbolEditorAddIn.Menu.ConvertTo
{
    [CoreMenu(ConverterConstant.MENU_CONVERTTO_COMBINEPATH_ELEMENT, 0x21)]
    class SymConvertElementsToSymbolsMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {

            var g =(Core2DDrawingLayeredElement[]) this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
       
            if ((g != null) && (g.Length >0)) {
                this.CurrentSurface.CurrentLayer.Select(null);
                List<Core2DDrawingLayeredElement> p = new List<Core2DDrawingLayeredElement>();
                for (int i = 0; i < g.Length; i++)
                {
                    var gi = g[i];
                    this.CurrentSurface.CurrentLayer.Elements.Remove(gi);
                    var ce = PathElement.CreateElement(gi.GetPath());
                    p.Add(ce);

                }
                var e = CombinedPathElement.CreateElement(p.ToArray());
                this.CurrentSurface.CurrentLayer.Elements.Add(e);
            }
            return base.PerformAction();
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.CurrentSurface.CurrentLayer.SelectedElements .Count > 1) ;
        }
        protected override void OnCurrentSurfaceChanged(ICore.CoreWorkingElementChangedEventArgs<ICore.WinUI.ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
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
