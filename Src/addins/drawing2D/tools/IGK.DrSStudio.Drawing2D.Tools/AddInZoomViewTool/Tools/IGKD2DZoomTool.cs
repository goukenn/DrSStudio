

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DZoomTool.cs
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
    [CoreTools("Tool.Drawing2D.ZoomTool", ImageKey=CoreImageKeys.MENU_ZOOM_S_GKDS)]
    class IGKD2DZoomTool : Core2DDrawingToolBase 
    {
        private static IGKD2DZoomTool sm_instance;
        private IGKD2DZoomTool()
        {
        }

        public static IGKD2DZoomTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        public new IGKD2DZoomToolGUI HostedControl {
            get {
                return base.HostedControl as IGKD2DZoomToolGUI;
            }
        }
        static IGKD2DZoomTool()
        {
            sm_instance = new IGKD2DZoomTool();
        }
        protected override void GenerateHostedControl()
        {
            base.HostedControl = new IGKD2DZoomToolGUI(this);
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            
            base.RegisterSurfaceEvent(surface);
            surface.ZoomModeChanged += surface_ZoomModeChanged;
            this.HostedControl.UpdateZoomMode();
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.ZoomModeChanged += surface_ZoomModeChanged;
            base.UnRegisterSurfaceEvent(surface);
        }

        void surface_ZoomModeChanged(object sender, EventArgs e)
        {
            this.HostedControl.UpdateZoomMode();
        }
    }
}
