

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DrawingRenderingServiceTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DrawingRenderingServiceTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    using System.Windows.Forms;
    /// <summary>
    /// used to manage refresh scene on the current surface by calling the RefreshScene method. in a worbench context.
    /// </summary>
    [CoreTools("Tool.IGKD2DrawingRenderingServiceTool")]
    public sealed class IGKD2DrawingRenderingServiceTool : Core2DDrawingToolBase 
    {
        private static IGKD2DrawingRenderingServiceTool sm_instance;
        private bool m_AutoRefresh;
        private int m_AutoRefreshInterval;

        public int AutoRefreshInterval
        {
            get { return m_AutoRefreshInterval; }
            set
            {
                if (m_AutoRefreshInterval != value)
                {
                    m_AutoRefreshInterval = value;
                }
            }
        }
        private Timer m_Timer;
        private bool m_suspendRefresh;
        
        public Timer Timer
        {
            get { return m_Timer; }
            set
            {
                if (m_Timer != value)
                {
                    m_Timer = value;
                }
            }
        }

        public bool AutoRefresh
        {
            get { return m_AutoRefresh; }
            set
            {
                if (m_AutoRefresh != value)
                {
                    m_AutoRefresh = value;
                }
            }
        }
        private IGKD2DrawingRenderingServiceTool()
        {
        }
        public static IGKD2DrawingRenderingServiceTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKD2DrawingRenderingServiceTool()
        {
            sm_instance = new IGKD2DrawingRenderingServiceTool();
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            if (surface is ICoreWorkingToolManagerSurface)
            {
                RegisterSurfaceEvent(surface as ICoreWorkingToolManagerSurface);
            }
            surface.ZoomChanged += surface_ZoomChanged;
            surface.ZoomModeChanged += surface_ZoomModeChanged;
        }
        /// <summary>
        /// 
        /// </summary>
        private void RefreshSurface(){

            if (!this.m_suspendRefresh)
                this.CurrentSurface.RefreshScene ();
        }
        public  void SuspendLayout()
        {
            this.m_suspendRefresh = true;
        }
        public void ResumeLayout()
        {
            this.m_suspendRefresh = false ;
        }
        void surface_ZoomModeChanged(object sender, EventArgs e)
        {

            RefreshSurface();
        }

        void surface_ZoomChanged(object sender, EventArgs e)
        {
            RefreshSurface();
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.ZoomChanged -= surface_ZoomChanged;
            surface.ZoomModeChanged -= surface_ZoomModeChanged;
            if (surface is ICoreWorkingToolManagerSurface)
            {
                UnRegisterSurfaceEvent(surface as ICoreWorkingToolManagerSurface);
            }
            base.UnRegisterSurfaceEvent(surface);
        }
        private void UnRegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
        {
            surface.CurrentToolChanged -= surface_CurrentToolChanged;
        }
        private void RegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
        {
            surface.CurrentToolChanged += surface_CurrentToolChanged;
            
        }
        void surface_CurrentToolChanged(object sender, EventArgs e)
        {
             
            RefreshSurface();
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.PropertyChanged += _PropertyChanged;
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.PropertyChanged -= _PropertyChanged;
            base.UnRegisterDocumentEvent(document);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += layer_SelectedElementChanged;
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.UnRegisterLayerEvent(layer);
            layer.SelectedElementChanged -= layer_SelectedElementChanged;
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            RefreshSurface();
        }
        void _PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            RefreshSurface();
        }
        
    }
}

