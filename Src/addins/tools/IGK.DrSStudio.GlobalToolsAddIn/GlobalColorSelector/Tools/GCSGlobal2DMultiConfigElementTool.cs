

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSGlobal2DMultiConfigElementTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.Tools;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Tools
{
    /*
     * 
     * Configure multi selection element
     * */
    [CoreTools("Tool.GCSGlobal2DMultiConfigElementTool")]
    class GCSGlobal2DMultiConfigElementTool : Core2DDrawingToolBase
    {

        private ICoreBrushOwner  m_ElementToConfigure;
        private static GCSGlobal2DMultiConfigElementTool sm_instance;
        private ICoreBrushOwner[] sm_Brushes;
        private GCSGlobal2DMultiConfigElementTool()
        {
        }

        public static GCSGlobal2DMultiConfigElementTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static GCSGlobal2DMultiConfigElementTool()
        {
            sm_instance = new GCSGlobal2DMultiConfigElementTool();

        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }

        public ICoreBrushOwner  ElementToConfigure
        {
            get { return m_ElementToConfigure; }
            set
            {
                if (m_ElementToConfigure != value)
                {
                    if (m_ElementToConfigure != null)
                        UnRegisterElementEvent(m_ElementToConfigure);
                    m_ElementToConfigure = value;
                    if (m_ElementToConfigure != null)
                        RegisterElementEvent(m_ElementToConfigure);
                }
            }
        }

        private void UnRegisterElementEvent(ICoreBrushOwner m_ElementToConfigure)
        {
            m_ElementToConfigure.PropertyChanged -= m_ElementToConfigure_PropertyChanged;
        }

        void m_ElementToConfigure_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (((enu2DPropertyChangedType)e.ID) == enu2DPropertyChangedType.BrushChanged)
            {
                UpdateBrushed();
            }
        }

       

        private void RegisterElementEvent(ICoreBrushOwner m_ElementToConfigure)
        {
            m_ElementToConfigure.PropertyChanged += m_ElementToConfigure_PropertyChanged;
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
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.ElementToConfigureChanged += surface_ElementToConfigureChanged;
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.ElementToConfigureChanged -= surface_ElementToConfigureChanged;
            base.UnRegisterSurfaceEvent(surface);
        }

        void surface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.ElementToConfigure =
            this.CurrentSurface.ElementToConfigure as ICoreBrushOwner;
        }

        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            if (GCSGlobalColorTool.Instance.HostedControl != null)
            {
                enuBrushMode v_mode = GCSGlobalColorTool.Instance.HostedControl.BrushMode;
                this.sm_Brushes = GetBrushElement(v_mode);
            }
        }

        public enuBrushMode BrushMode {
            get {
                return GCSGlobalColorTool.Instance.HostedControl.BrushMode;
            }
        }
        private void UpdateBrushed()
        {
            enuBrushMode v_mode = this.BrushMode;
           if ((GCSGlobalColorTool.Instance.HostedControl.ElementToConfigure != null)
               && (GCSGlobalColorTool.Instance.HostedControl.ElementToConfigure == this.m_ElementToConfigure))
           {
               this.sm_Brushes = GetBrushElement(v_mode);
               this.SetBrush (GCSGlobalColorTool.Instance.HostedControl.GetBrush(v_mode), this.sm_Brushes );
           }
        }
        public ICoreBrushOwner[] GetBrushElement(enuBrushMode v_mode)
        {
            //get brushes from elements 
            //and apply them if brushes surfaces support brush management
            ICore2DDrawingSurface v_surface = this.CurrentSurface;
            List<ICoreBrushOwner> v_brushes = new List<ICoreBrushOwner>();
            if (v_surface != null)
            {
                var s = v_surface.CurrentLayer.SelectedElements.ToArray();
                foreach (ICoreBrushOwner i in s)
                {
                    if ((i == null) || (i == this.m_ElementToConfigure))
                    {
                        //ignore element ToolImageKey configure
                        continue;
                    }

                    ICoreBrush c = i.GetBrush(v_mode);
                    if (c != null)
                    {
                        v_brushes.Add(i);
                    }
                }
            }
            return v_brushes.ToArray();
        }
        public void SetBrush(ICoreBrush brush, params ICoreBrushOwner[] brushes)
        {
            if ((brush == null) || (brushes == null) || (brushes.Length == 0))
                return;
            
            var e =  this.BrushMode ;
            IGKD2DrawingRenderingServiceTool.Instance.SuspendLayout();
            
            for (int i = 0; i < brushes.Length; i++)
            {
                brushes[i].GetBrush (e).Copy(brush);
            }
            IGKD2DrawingRenderingServiceTool.Instance.ResumeLayout();
            this.CurrentSurface.RefreshScene();
        }
    }
}
