

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebHtmlLayoutLayer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WebHtmlLayoutLayer.cs
*/
using IGK.DrSStudio.WebProjectAddIn.Layout;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn.WorkingObjects
{
    using IGK.DrSStudio.XmlTextEditor.WinUI;
    [CoreWorkingObject ("WebHtmlLayoutLayer")]
    /// <summary>
    /// reprensent the layer document
    /// </summary>
    public class WebHtmlLayoutLayer : Core2DDrawingLayer
    {
        private WebLayoutEngineBase m_LayoutEngine;
        private string m_DefaultOutpuTag;

        public string DefaultOutpuTag
        {
            get { return m_DefaultOutpuTag; }
            set
            {
                if (m_DefaultOutpuTag != value)
                {
                    m_DefaultOutpuTag = value;
                }
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections defaultParam)
        {
            defaultParam =  base.GetParameters(defaultParam);
            var group = defaultParam.AddGroup("default");
            group.AddItem(this.GetType().GetProperty("DefaultOutpuTag"));
            return defaultParam;
        }
        ///// <summary>
        ///// suspend the layout engine
        ///// </summary>
        //public void SuspendLayout() {
        //    this.m_layoutSuspended = true;
        //}
        //public void ResumeLayout() {
        //    this.ResumeLayout(false);
        //}
        //public void ResumeLayout(bool performlayout)
        //{
        //    this.m_layoutSuspended = false;
        //    if (performlayout)
        //    {
        //        this.PerformLayout();
        //        this.InvalidateParentSurface();
        //    }
        //}
        //private void InvalidateParentSurface()
        //{
        //    var v = this.GetParentSurface();
        //    if (v != null)
        //        v.Invalidate();
        //}
        public void PerformLayout()
        {
            if (LayoutEngine == null)
            {
                LayoutEngine.Layout(this);
            }
        }
        public virtual WebLayoutEngineBase LayoutEngine
        {
            get {
                return m_LayoutEngine;
            }
        }
        public WebHtmlLayoutLayer():base()
        {
            this.m_LayoutEngine = new VerticalWebLayoutEngine();
            this.m_DefaultOutpuTag = "form";
        }
        protected override Core2DDrawingLayer.Core2DDrawingLayerCollections CreateElementCollections()
        {
            return new HtmlWebElementCollections(this);
        }
        //protected override ElementCollections CreateElementCollections()
        //{
        //    return new HtmlWebElementCollections(this);
        //}
        protected override void OnElementAdded(CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            this.__registerElementEvent(e.Item );
            base.OnElementAdded(e);
        }
        protected override void OnElementRemoved(CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            this.__unregisterElementEvent(e.Item );
            base.OnElementRemoved(e);
        }
        void __registerElementEvent(ICore2DDrawingLayeredElement element)
        {
            if (element is WebHtmlElementBase)
            {
                WebHtmlElementBase b = element as WebHtmlElementBase;
                b.PropertyChanged += b_PropertyChanged;
            }
        }
        void b_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (!this.IsLoading )
            UpdateLayout();
        }
        void __unregisterElementEvent(ICore2DDrawingLayeredElement element)
        {
            if (element is WebHtmlElementBase)
            {
                WebHtmlElementBase b = element as WebHtmlElementBase;
                b.PropertyChanged -= b_PropertyChanged;
            }
        }
        void UpdateLayout()
        {
            if (this.LayoutEngine !=null)
                this.LayoutEngine.Layout(this);
        }
        /// <summary>
        /// represent a layout layer element collection
        /// </summary>
        class HtmlWebElementCollections : Core2DDrawingLayer.Core2DDrawingLayerCollections
        {
            public HtmlWebElementCollections(WebHtmlLayoutLayer layer):base(layer )
            {
            }
            public override string ToString()
            {
                return "HtmlWebElementCollections ["+this.Count+"] ";
            }
        }
    }
}

