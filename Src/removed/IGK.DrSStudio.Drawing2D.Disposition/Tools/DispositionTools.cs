

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DispositionTools.cs
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
file:DispositionTools.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.Disposition.Tools
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [IGK.DrSStudio.CoreTools("Drawing2D.DispositionTool")]
    sealed partial class DispositionTools : IGK.DrSStudio.Drawing2D.Tools.Core2DDrawingToolBase 
    {
        private static DispositionTools sm_instance;
        private DispositionLayer m_dispositionLayer;
        private bool m_ShowDisposition;
        /// <summary>
        /// get or set the disposition
        /// </summary>
        public bool ShowDisposition
        {
            get { return m_ShowDisposition; }
            set
            {
                if (m_ShowDisposition != value)
                {
                    m_ShowDisposition = value;
                }
            }
        }
        private DispositionTools()
        {
            this.m_dispositionLayer = new DispositionLayer(this);
        }
        public static DispositionTools Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static DispositionTools()
        {
            sm_instance = new DispositionTools();
        }
        protected override void RegisterSurfaceEvent(WinUI.ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            XDrawing2DSurface v_s = surface as XDrawing2DSurface;
            v_s.DesignLayers.Add(this.m_dispositionLayer);
        }
        protected override void UnRegisterSurfaceEvent(WinUI.ICore2DDrawingSurface surface)
        {
            XDrawing2DSurface v_s = surface as XDrawing2DSurface;
            v_s.DesignLayers.Remove(this.m_dispositionLayer);
            base.UnRegisterSurfaceEvent(surface);
        }
      }
}

