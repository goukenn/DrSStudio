

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerManager.cs
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
using IGK.ICore.History;
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

namespace IGK.DrSStudio.Drawing2D.Tools
{
    /// <summary>
    /// represent the layer added manager
    /// </summary>
    class LayerManager : HistoryToolManagerBase
    {
        internal LayerManager(HistorySurfaceManager tool)
            : base(tool)
        {
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.LayerAdded += new Core2DDrawingLayerEventHandler(document_LayerAdded);
            document.LayerRemoved += new Core2DDrawingLayerEventHandler(document_LayerRemoved);
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {

            document.LayerAdded -= new Core2DDrawingLayerEventHandler(document_LayerAdded);
            document.LayerRemoved -= new Core2DDrawingLayerEventHandler(document_LayerRemoved);
            base.UnRegisterDocumentEvent(document);
        }
        
        void document_LayerRemoved(object o, Core2DDrawingLayerEventArgs e)
        {

            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {

                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._LayerRemoved(
                    CurrentSurface.CurrentDocument,
                    e.Layer, e.ZIndex));
            }
        }

        void document_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {

                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._LayerAdded(
                    CurrentSurface.CurrentDocument ,
                    e.Layer ));
            }
        }
    }
}
