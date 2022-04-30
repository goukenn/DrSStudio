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
    abstract class SelecteElementMenuBase : Core2DDrawingMenuBase
    {
        private ICoreWorkingObject m_element;

        protected virtual ICoreWorkingObject Element => m_element;

        protected override bool IsEnabled()
        {
            return Element != null;
        }
        protected override bool IsVisible()
        {
            return true;
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
            if (e.NewElement != null)
            {
                _UpdatePath((e.NewElement as ICore2DDrawingDocument).CurrentLayer);
            }
            else
                this.m_element = null;
        }

        private void _UpdatePath(ICore2DDrawingLayer l)
        {
            var t= l.SelectedElements.Count == 1
               ? l.SelectedElements[0] : null;

            if (t != m_element) {
                m_element = t;
                this.SetupEnableAndVisibility();
            }
        }
    }
}
