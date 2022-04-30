using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu
{
    public abstract class PathElementMenuBase : Core2DDrawingMenuBase
    {
        private PathElement m_PathElement;

        public PathElement PathElement
        {
            get { return m_PathElement; }
            set
            {
                if (m_PathElement != value)
                {
                    m_PathElement = value;
                    this.SetupEnableAndVisibility();
                }
            }
        }
        public PathElementMenuBase()
        {

        }
        protected override bool IsEnabled()
        {
            return this.PathElement != null;
        }
        protected override bool IsVisible()
        {
            return this.PathElement != null;
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
            ICore2DDrawingLayer l = sender as ICore2DDrawingLayer;
            _UpdatePath(l);
          
                
        }
        protected override void OnCurrentDocumentChanged(CoreWorkingDocumentChangedEventArgs e)
        {
            if (e.NewElement != null) {
                _UpdatePath((e.NewElement as ICore2DDrawingDocument).CurrentLayer);
            }else
            this.PathElement = null; 
        }

        private void _UpdatePath(ICore2DDrawingLayer l)
        {
            this.PathElement = l.SelectedElements.Count == 1
               ? l.SelectedElements[0] as PathElement : null;
        }
       
    }
}
