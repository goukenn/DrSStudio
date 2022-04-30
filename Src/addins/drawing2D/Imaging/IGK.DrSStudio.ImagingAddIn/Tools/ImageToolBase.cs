

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageToolBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Tools;

namespace IGK.DrSStudio.Tools
{
    public class ImageToolBase : CoreToolBase 
    {

        private ImageElement m_ImageElement;
        /// <summary>
        /// get or set the image element
        /// </summary>
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                }
            }
        }

        protected virtual void RegisterImageEvent()
        {}
        protected virtual void UnRegisterImageEvent()
        {}

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += _CurrentSurfaceChanged;

        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
            base.UnregisterBenchEvent(Workbench);
        }

        private void _CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {

            if (e.OldElement != null) UnRegisterSurfaceEvent(e.OldElement);

            if (e.NewElement != null) RegisterSurfaceEvent(e.NewElement);
        }

        protected virtual void UnRegisterSurfaceEvent(ICoreWorkingSurface surface)
        {
            if (surface is ICore2DDrawingSurface)
                UnRegisterSurfaceEvent(surface as ICore2DDrawingSurface);

        }
        protected virtual void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
            surface.CurrentDocument.CurrentLayerChanged += CurrentDocument_CurrentLayerChanged;
            RegisterDocumentEvent(surface.CurrentDocument);
            RegisterLayerEvent(surface.CurrentLayer);
        }

        protected virtual void RegisterLayerEvent(ICore2DDrawingLayer core2DDrawingLayer)
        {
            
        }

        protected virtual void RegisterDocumentEvent(ICore2DDrawingDocument core2DDrawingDocument)
        {
            
        }

        void CurrentDocument_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            
        }

        void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            
        }
        protected virtual void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            UnRegisterLayerEvent(surface.CurrentLayer);
            UnRegisterDocumentEvent(surface.CurrentDocument);            
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
            surface.CurrentDocument.CurrentLayerChanged -= CurrentDocument_CurrentLayerChanged;
            
        }

        protected virtual  void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
          
        }

        protected  void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
          
        }

        protected virtual void RegisterSurfaceEvent(ICoreWorkingSurface surface)
        {
            if (surface is ICore2DDrawingSurface)
                RegisterSurfaceEvent(surface as ICore2DDrawingSurface);
        }

        protected virtual void OnImagePropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
           // Workbench.CurrentSurfaceChanged -= _CurrentSurfaceChanged;
        }

    }
}
