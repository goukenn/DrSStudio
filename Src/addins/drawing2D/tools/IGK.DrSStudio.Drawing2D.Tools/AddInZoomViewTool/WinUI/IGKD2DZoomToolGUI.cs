

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DZoomToolGUI.cs
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
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.ICore.Actions;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.WinUI.Controls;
    sealed class IGKD2DZoomToolGUI : IGKXToolStripCoreToolHost
    {
        public IGKD2DZoomToolGUI(IGKD2DZoomTool tool):base(tool)
        {
            this.InitControl();
        }
        

        private void InitControl()
        {
            foreach(enuZoomMode mode in Enum.GetValues(typeof(enuZoomMode )))
            {
                IGKXToolStripButton c = null;
                c = new IGKXToolStripButton()
                  {
                      Tag = mode,
                      Name = mode.ToString()
                  };
                c.ImageDocument = CoreResources.GetDocument($"btn_zoom_{mode}_gkds");
                c.Action = new ZoomAction(this, mode, c);
                this.Items.Add(c);
            }
            this.Items.Add(new XToolStripMenuItemSeparator());
            var q = new FitSizeAction();
            this.Items.Add (new IGKXToolStripButton()
                  {
                      Name = "FitSize",
                      ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_FITSIZE_GKDS),
                      Action = q
                  } );              
                
            this.AddRemoveButton(null);
        }
        protected override void InitLayout()
        {
            base.InitLayout();
        }
        class FitSizeAction : CoreActionBase 
        {
            protected override bool PerformAction()
            {
                CoreSystem.GetWorkbench().CallAction ("View.2DZoomMode.FitSize");
                return false;
            }
        }
        class ZoomAction : CoreActionBase {
            private IGKD2DZoomToolGUI host;
            private enuZoomMode mode;
            private IGKXToolStripButton c_toolButton;

            public ZoomAction(IGKD2DZoomToolGUI host, enuZoomMode mode, IGKXToolStripButton button):base()
            {
                if ((host == null) || (button == null))
                    throw new ArgumentNullException();
                this.host = host;
                this.mode = mode;
                this.c_toolButton = button;
                this.host.ZoomModeChanged +=  this._zoomModeChanged;
            }

            private void _zoomModeChanged(object sender, EventArgs e)
            {
                      IGKD2DZoomTool h = host.Tool as IGKD2DZoomTool;
                ICore2DDrawingSurface v = h.CurrentSurface as ICore2DDrawingSurface ;
                if (v != null)
                    this.c_toolButton.Checked = this.mode == v.ZoomMode;
                else
                    this.c_toolButton.Checked = false;
            }
            protected override bool PerformAction()
            {
                IGKD2DZoomTool h = host.Tool as IGKD2DZoomTool;
                ICore2DDrawingSurface v = h.CurrentSurface as ICore2DDrawingSurface ;
                if (v != null)
                {
                    v.ZoomMode = this.mode;
                }
                return false;
            }
        }

        internal void UpdateZoomMode()
        {
            OnUpdateZoomMode(EventArgs.Empty);
        }
        public event EventHandler ZoomModeChanged;

        private void OnUpdateZoomMode(EventArgs eventArgs)
        {
            if (ZoomModeChanged != null)
                this.ZoomModeChanged(this, eventArgs);
        }
    }
}
