

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageMenuBase.cs
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
file:ImageMenuBase.cs
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
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.Menu;
    /// <summary>
    /// represent the base menu of image element
    /// </summary>
    public abstract class ImageMenuBase : Core2DDrawingMenuBase
    {
        private ImageElement m_ImageElement;
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                    OnImageElementChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ImageElementChanged;
        /// <summary>
        /// invalidate the current surface
        /// </summary>
        protected void Invalidate()
        {
            if (this.CurrentSurface != null)
                this.CurrentSurface.RefreshScene();
        }
        protected virtual void OnImageElementChanged(EventArgs eventArgs)
        {
            SetupEnableAndVisibility();
            if (this.ImageElementChanged != null)
                this.ImageElementChanged(this, eventArgs);
        }
        protected override bool IsEnabled()
        {
            return (this.ImageElement != null);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.Visible = false;
            this.Enabled = false;
        }
        protected ICoreWorkingConfigElementSurface ConfigSurface {
            get {
                return Workbench.CurrentSurface as ICoreWorkingConfigElementSurface;
            }
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            ICoreWorkingSurface v_o = e.OldElement;
            ICoreWorkingSurface v_n = e.NewElement;
            if (v_o  is ICore2DDrawingSurface)
                UnRegisterSurfaceEvent(e.OldElement  as ICore2DDrawingSurface);
            else if (v_o is ICoreWorkingConfigElementSurface)
                UnRegisterSurfaceEvent(v_o as ICoreWorkingConfigElementSurface);
            if (v_n is ICore2DDrawingSurface)
                RegisterSurfaceEvent(v_n as ICore2DDrawingSurface);
            else if (v_n is ICoreWorkingConfigElementSurface )
                RegisterSurfaceEvent(v_n as ICoreWorkingConfigElementSurface );
            if (this.CurrentSurface != null)
            {
                this.GetSelected(this.CurrentSurface.CurrentLayer);
            }
            else UpdateElement();
            this.SetupEnableAndVisibility();
        }
        private void UpdateElement()
        {
            if (this.ConfigSurface != null)
            {
                this.ImageElement = ConfigSurface.ElementToConfigure as ImageElement;
            }
        }
        /// <summary>
        /// call only for single element
        /// </summary>
        /// <param name="iCoreWorkingConfigElementSurface"></param>
        protected virtual void RegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged += new EventHandler(surface_ElementToConfigureChanged);
        }
        void surface_ElementToConfigureChanged(object sender, EventArgs e)
        {
            this.UpdateElement();
        }
        protected virtual void UnRegisterSurfaceEvent(ICoreWorkingConfigElementSurface surface)
        {
            surface.ElementToConfigureChanged -= new EventHandler(surface_ElementToConfigureChanged);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            ICore2DDrawingLayer l = sender as ICore2DDrawingLayer;
            this.GetSelected(l);
        }
        private void GetSelected(ICore2DDrawingLayer l)
        {
            if ((l.SelectedElements.Count == 1) && (l.SelectedElements[0] is ImageElement))
            {
                this.ImageElement = l.SelectedElements[0] as ImageElement;
            }
            else
                this.ImageElement = null;
        }
    }
}

