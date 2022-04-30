

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _2DViewZoomModeMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_2DViewZoomModeMenu.cs
*/
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.View
{
    using IGK.DrSStudio.Menu;
    using IGK.ICore.Drawing2D.Menu;

    [DrSStudioMenu("View.2DZoomMode", 0, SeparatorAfter=true) ]
    sealed class _2DViewZoomModeMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            int i = 0;
            CoreMenuAttribute v_attr = null;
            ZoomModeSubMenu s = null;
            foreach(enuZoomMode zoomMode in  Enum.GetValues(typeof(enuZoomMode )))
            {
                s = new ZoomModeSubMenu(zoomMode);
                v_attr = new CoreMenuAttribute (string.Format (this.Id +"."+zoomMode.ToString()), i);
                if (this.Register (v_attr, s ))
                {
                    s.SetAttribute(v_attr);                    
                    this.Childs.Add(s);
                    i++;
                }
            }
            //add option
            v_attr = new CoreMenuAttribute(string.Format(this.Id + ".FitSize"), i++);
            v_attr.SeparatorBefore = true;
            v_attr.ImageKey = "menu_fitSize";
            var rs = new FitSizeZoomMenu(this);
            if (this.Register(v_attr, rs))
            {
                rs.SetAttribute(v_attr);
                this.Childs.Add(rs);
                i++;
            }

        }
        /// <summary>
        /// fit zoom menu
        /// </summary>
        sealed class FitSizeZoomMenu : Core2DDrawingMenuBase
        {
            private _2DViewZoomModeMenu _2DViewZoomModeMenu;

            public FitSizeZoomMenu(_2DViewZoomModeMenu _2DViewZoomModeMenu)
            {                
                this._2DViewZoomModeMenu = _2DViewZoomModeMenu;
            }
            protected override bool PerformAction()
            {
                this.CurrentSurface.SetZoom(1.0F,1.0f);
                this.CurrentSurface.RefreshScene();
                return true;
            }

        }
        sealed class ZoomModeSubMenu : Core2DDrawingMenuBase 
        {
            private enuZoomMode zoomMode;
            public ZoomModeSubMenu(enuZoomMode zoomMode):base()
            {                
                this.zoomMode = zoomMode;
            }
            protected override bool PerformAction()
            {
                this.CurrentSurface.ZoomMode = this.zoomMode;
                return true;
            }
            protected override void InitMenu()
            {
                base.InitMenu();
            }
            protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
            {
                surface.ZoomModeChanged -= surface_ZoomModeChanged;
                base.UnRegisterSurfaceEvent(surface);
            }
            protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
            {
                base.RegisterSurfaceEvent(surface);
                this.MenuItem.Checked = (surface.ZoomMode == this.zoomMode);
                surface.ZoomModeChanged += surface_ZoomModeChanged;
            }

            void surface_ZoomModeChanged(object sender, EventArgs e)
            {
                this.MenuItem.Checked = (this.CurrentSurface .ZoomMode == this.zoomMode);
            }
        }
    }
}

