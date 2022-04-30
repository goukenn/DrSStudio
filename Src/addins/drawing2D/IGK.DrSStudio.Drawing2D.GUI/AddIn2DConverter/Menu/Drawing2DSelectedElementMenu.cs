

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Drawing2DSelectedElementMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Drawing2DSelectedElementMenu.cs
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
using IGK.ICore.Drawing2D.Menu;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    public abstract class Drawing2DSelectedElementMenu : Core2DDrawingMenuBase
    {
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
            base.UnRegisterSurfaceEvent(surface);
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.CurrentLayerChanged -= new Core2DDrawingLayerChangedEventHandler(document_CurrentLayerChanged);
            base.UnRegisterDocumentEvent(document);
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler(document_CurrentLayerChanged);
        }
        void document_CurrentLayerChanged(object o, Core2DDrawingLayerChangedEventArgs e)
        {
            SetupEnableAndVisibility();
        }
        private void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {       
            SetupEnableAndVisibility();
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
            SetupEnableAndVisibility();
        }
    }
}

