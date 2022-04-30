

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:LayerManager.cs
*/
using System;
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

