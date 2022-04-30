

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingToolBase.cs
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
file:Core2DDrawingToolBase.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D.Tools
{
    using IGK.ICore;
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent the base tool for drawing 2d Element 
    /// </summary>
    public abstract class Core2DDrawingToolBase : CoreToolBase
    {
        protected new ICore2DDrawingSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (workbench is ICoreWorkingSurfaceHandler s)
            s.CurrentSurfaceChanged += _OnCurrentSurfaceChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench workbench)
        {
            if (workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged -= _OnCurrentSurfaceChanged;
        }
        void _OnCurrentSurfaceChanged(object o, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            OnCurrentSurfaceChanged(e);
        }
        protected virtual void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            if (e.OldElement is ICore2DDrawingSurface)
                UnRegisterSurfaceEvent(e.OldElement as ICore2DDrawingSurface);
            if (e.NewElement is ICore2DDrawingSurface)
                RegisterSurfaceEvent(e.NewElement as ICore2DDrawingSurface);
        }
        protected virtual void RegisterSurfaceEvent(ICore2DDrawingSurface  surface) {
            if (surface .CurrentDocument !=null)
            RegisterDocumentEvent(surface.CurrentDocument);
            surface.CurrentDocumentChanged += _CurrentDocumentChanged;
        }
        void _CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs  e)
        {
            if (e.OldElement != null)
                this.UnRegisterDocumentEvent(e.OldElement as ICore2DDrawingDocument);
            if (e.NewElement != null)
                this.RegisterDocumentEvent(e.NewElement as ICore2DDrawingDocument);
        }
        protected virtual void UnRegisterSurfaceEvent(ICore2DDrawingSurface  surface) {
            UnRegisterDocumentEvent(surface.CurrentDocument);
            surface.CurrentDocumentChanged -= _CurrentDocumentChanged;
        }
        protected virtual void RegisterDocumentEvent(ICore2DDrawingDocument document) {
            if (document.CurrentLayer !=null)
            RegisterLayerEvent(document.CurrentLayer);
            document.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler(document_CurrentLayerChanged);
        }
        protected virtual void UnRegisterDocumentEvent(ICore2DDrawingDocument document) {
            UnRegisterLayerEvent(document.CurrentLayer);
            document.CurrentLayerChanged -= document_CurrentLayerChanged;
        }
        void document_CurrentLayerChanged(object o, Core2DDrawingLayerChangedEventArgs e)
        {
            if (e.OldElement !=null)
               UnRegisterLayerEvent(e.OldElement);
            if (e.NewElement != null)
                RegisterLayerEvent(e.NewElement);
        }
        protected virtual void RegisterLayerEvent(ICore2DDrawingLayer layer) { }
        protected virtual void UnRegisterLayerEvent(ICore2DDrawingLayer layer) { }
    }
}

