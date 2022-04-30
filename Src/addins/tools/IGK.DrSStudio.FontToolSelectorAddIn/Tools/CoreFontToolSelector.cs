

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFontToolSelector.cs
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
file:CoreFontToolSelector.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Tools;
    using IGK.ICore;
    using IGK.DrSStudio.WinUI;
    [CoreTools("Tool.FontToolSelector")]
    class CoreFontToolSelector : CoreToolBase 
    {
        private ICoreTextElement m_Element;
        private ICoreTextElement[] m_Elements;
        private CoreToolFontSetting m_Setting;
        private static CoreFontToolSelector sm_instance;
        ICoreFont m_ft = null;
        private bool m_configuring;
        public CoreToolFontSetting Setting {
            get { 
                if (m_Setting == null)
                    m_Setting = CoreToolFontSetting.Instance ;
                return m_Setting ;
            }
        }
        private CoreFontToolSelector()
        {
        }
        public static CoreFontToolSelector Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreFontToolSelector()
        {
            sm_instance = new CoreFontToolSelector();
        }
        public new FontSelectorToolStrip HostedControl
        {
            get { return base.HostedControl as FontSelectorToolStrip; }
            set { base.HostedControl = value; }
        }
        public new ICoreWorkingConfigElementSurface CurrentSurface {
            get { return base.CurrentSurface as ICoreWorkingConfigElementSurface; }
        }
        public ICoreTextElement Element
        {
            get { return m_Element; }
            private set
            {
                if (m_Element != value)
                {
                    if (m_Element != null)
                        UnRegisterElementEvent(m_Element);
                    m_Element = value;
                    if (m_Element != null)
                        RegisterElementEvent(m_Element);
                    OnElementChanged(EventArgs.Empty);
                }
            }
        }
        private void RegisterElementEvent(ICoreTextElement element)
        {
            element.Font.FontDefinitionChanged += new EventHandler(Font_FontDefinitionChanged);
        }
        private void UnRegisterElementEvent(ICoreTextElement element)
        {
            element.Font.FontDefinitionChanged -= new EventHandler(Font_FontDefinitionChanged);
        }
        void Font_FontDefinitionChanged(object sender, EventArgs e)
        {
            if (!m_configuring)
            {
                this.HostedControl.Configure(this.m_Element.Font);
            }
        }
        private void OnElementChanged(EventArgs eventArgs)
        {
            this.Enabled = (this.Element != null);
            if (this.Element != null)
            {
                this.HostedControl.Configure(this.Element.Font);
            }
            else {
                this.HostedControl.Configure(null);
            }
            //this.HostedControl.Enabled = 
            //(this.Element != null);
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new FontSelectorToolStrip() { 
                Tool = this
            };
           // if (this.HostedControl.FontName)
            this.m_ft = CoreFont.CreateFrom (this.Setting.DefaultFont, null );
            this.HostedControl.InitTools();
            this.HostedControl.FontDefinitionChanged += new EventHandler(HostedControl_FontDefinitionChanged);        
        }
        public override void Configure()
        {
            GetElement();    
        }
        void HostedControl_FontDefinitionChanged(object sender, EventArgs e)
        {
            if ((this.Element != null)&&(!m_configuring ))
            {
                FontSelectorToolStrip t = this.HostedControl ;
                this.m_ft.FontName = t.FontName;
                this.m_ft.FontSize =((ICoreUnitPixel) t.FontSize).Value;
                this.m_ft.VerticalAlignment = t.FontVercalAlignment;
                this.m_ft.HorizontalAlignment = t.FontHorizontalAlignment;
                this.m_ft.FontStyle = t.FontStyle;
                //disable the edition of the font property
                this.m_configuring = true;
                string m_def = this.m_ft.GetDefinition();
                bool v_d = false;
                if ((this.m_Elements != null) && (this.m_Elements.Length > 0))
                {
                    for (int i = 0; i < this.m_Elements .Length; i++)
                    {
                        if (m_Elements[i] == this.m_Element)
                            v_d = true;
                        this.m_Elements[i].Font.CopyDefinition(m_def);
                    }
                }
                if (!v_d)
                {
                    this.Element.Font.CopyDefinition(m_def);
                }
                this.HostedControl.Update (this.Element.Font);
                this.m_configuring = false;
                ICoreWorkingRenderingSurface v_s = this.CurrentSurface as ICoreWorkingRenderingSurface;
                if (v_s !=null)
                    v_s.RefreshScene();
            }
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            base.RegisterBenchEvent(workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += workbench_CurrentSurfaceChanged;
        }

      
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {

            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= workbench_CurrentSurfaceChanged;
            base.UnregisterBenchEvent(workbench);
        }
        void workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
           if (e.OldElement  is ICoreWorkingConfigElementSurface)
               UnregisterSurface(e.OldElement as ICoreWorkingConfigElementSurface);
            if (e.NewElement is ICoreWorkingConfigElementSurface)
                RegisterSurfaceEvent(e.NewElement as ICoreWorkingConfigElementSurface); 
        }
        private void UnregisterSurface(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged -= new EventHandler(surface_ElementToConfigureChanged);
            UnRegisterMultiElement(surface as ICore2DDrawingSurface);
        }
        private void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged += new EventHandler(surface_ElementToConfigureChanged);
            RegisterMultiElement(surface as ICore2DDrawingSurface);
        }
        private void RegisterMultiElement(ICore2DDrawingSurface s)
        {
            if (s == null)
                return;
            s.CurrentDocumentChanged += s_CurrentDocumentChanged;
            RegisterDocumentEvent(s.CurrentDocument);
            if (s.CurrentLayer != null)
            {
                RegisterLayerEvent(s.CurrentLayer);
                GetElement(s.CurrentLayer);
            }
        }
        private void s_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            if (e.OldElement is ICore2DDrawingDocument)
            UnRegiseterDocumentEvent(e.OldElement as ICore2DDrawingDocument);
            if (e.NewElement is ICore2DDrawingDocument)
            {
                ICore2DDrawingDocument c = e.NewElement as ICore2DDrawingDocument;
                RegisterDocumentEvent(c);
                this.GetElement(c.CurrentLayer );
            }
        }
        private void UnRegiseterDocumentEvent(ICore2DDrawingDocument doc)
        {
            doc.CurrentLayerChanged -= doc_CurrentLayerChanged;
        }
        private void RegisterDocumentEvent(ICore2DDrawingDocument doc)
        {
            doc.CurrentLayerChanged += doc_CurrentLayerChanged;
        }
        void doc_CurrentLayerChanged(object o, Core2DDrawingLayerChangedEventArgs e)
        {
            this.GetElement(e.NewElement );
        }
        private void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        private void UnRegisterMultiElement(ICore2DDrawingSurface s)
        {
            if (s == null)
                return;
            UnRegisterLayerEvent(s.CurrentLayer);
            this.m_Elements = null;
        }
        private void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            GetElement(sender as ICore2DDrawingLayer);
        }
        private void GetElement(ICore2DDrawingLayer layer)
        {
            this.m_Elements = null;
            if (layer.SelectedElements.Count > 0)
            {
                List<ICoreTextElement> m_l = new List<ICoreTextElement>();
                foreach (var item in layer.SelectedElements )
                {
                    if (item is ICoreTextElement)
                        m_l.Add(item as ICoreTextElement );
                }
                if (m_l.Count > 0)
                this.m_Elements = m_l.ToArray();
            }
        }
        void surface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            GetElement();
        }
        void GetElement()
        {
            if (!this.Visible || (this.CurrentSurface == null))
                this.Element = null;
            else
            {
                this.Element = this.CurrentSurface.ElementToConfigure as ICoreTextElement;      
            }
        }
    }
}

