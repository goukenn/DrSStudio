using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Menu;
using System.Drawing.Drawing2D;

namespace IGK.DrSStudio.Drawing2D.AddInPathFinderTool.Menu
{
    [CoreMenu("Path.Unite", 12,
        Description ="Unite Graphics 2D path element Menu")]
    class PathFinderUniteMenu : Core2DDrawingMenuBase
    {
        protected override bool IsVisible()
        {
            return base.IsVisible() && this.CurrentSurface?.CurrentLayer.SelectedElements.Count > 1;
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && this.CurrentSurface?.CurrentLayer.SelectedElements.Count >1;
        }
        protected override bool PerformAction()
        {
            var tab = this.CurrentSurface.CurrentLayer.SelectedElements.ToArray();
            if (tab.Length < 2)
                return false;

            var v_finder = new PathFinderOperator();
            foreach (var item in tab)
            {                
                v_finder.Add ( item.GetPath().ToGdiPGraphicsPath());
            }

            var g = v_finder.Unite();
            if (g != null)
            {
                PathElement vg = PathElement.CreateElement(g);
                this.CurrentSurface.CurrentLayer.Elements.Add(vg);
                this.CurrentSurface.Invalidate();
            }
            return false;
        }

        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.UnRegisterLayerEvent(layer);
            layer.SelectedElementChanged -= Layer_SelectedElementChanged;
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged += Layer_SelectedElementChanged;
            base.RegisterLayerEvent(layer);
        }

        private void Layer_SelectedElementChanged(object sender, System.EventArgs e)
        {
            this.SetupEnableAndVisibility();
        }
    }
}
