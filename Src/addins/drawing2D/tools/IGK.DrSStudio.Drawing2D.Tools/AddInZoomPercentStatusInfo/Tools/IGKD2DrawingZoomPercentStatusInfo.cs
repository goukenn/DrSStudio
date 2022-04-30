

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DrawingZoomPercentStatusInfo.cs
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
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Tools;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Tools
{
    [CoreTools("Tool.Drawing2D.ZoomPercentTool")]
    class IGKD2DrawingZoomStatusInfo : Core2DDrawingToolBase 
    {
        private static IGKD2DrawingZoomStatusInfo sm_instance;
        private IGKWinCoreStatusTextItem m_info;
        private IGKD2DrawingZoomStatusInfo()
        {
        }

        public static IGKD2DrawingZoomStatusInfo Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static IGKD2DrawingZoomStatusInfo()
        {
            sm_instance = new IGKD2DrawingZoomStatusInfo();

        }
        protected override void GenerateHostedControl()
        {
            m_info = new IGKWinCoreStatusTextItem();
            m_info.Bounds = new Rectanglef(0, 0, 80, 24);
            m_info.Index = 0x0045;
            m_info.Visible = false;
            this.Workbench.GetLayoutManager()?.StatusControl.Items.Add(m_info);
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.ZoomModeChanged += surface_ZoomModeChanged;
            surface.ZoomChanged += surface_ZoomChanged;
            surface.SizeChanged += surface_SizeChanged;
        }

        void surface_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateZoomInfo();
        }

        void surface_ZoomChanged(object sender, EventArgs e)
        {
            this.UpdateZoomInfo();
        }

        private void UpdateZoomInfo()
        {
            if ((this.m_info.Visible) && (this.CurrentSurface!=null))
            {
                this.m_info.Text = string.Format("{0:###.00}%", this.CurrentSurface.GetZoom() * 100.0f);
            }
        }

        void surface_ZoomModeChanged(object sender, EventArgs e)
        {
            this.UpdateZoomInfo();
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.m_info.Visible = (this.CurrentSurface != null);
            this.UpdateZoomInfo();

        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.ZoomModeChanged -= surface_ZoomModeChanged;
            surface.ZoomChanged -= surface_ZoomChanged;
            surface.SizeChanged -= surface_SizeChanged;
            base.UnRegisterSurfaceEvent(surface);
        }
    }
}
