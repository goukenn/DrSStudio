using IGK.Management.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Management.Tools
{
    using IGK.ICore;
    using IGK.ICore.Tools;

    [CoreTools("WMITool")]
    class WMITool : WMIToolBase 
    {
        private static WMITool sm_instance;
        private WMIEditorSurface m_surface;
        private WMITool()
        {
        }

        public static WMITool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static WMITool()
        {
            sm_instance = new WMITool();

        }

        internal void ShowSurface()
        {
            if (this.m_surface == null){
                this.m_surface = new WMIEditorSurface();
                this.m_surface.Disposed += m_surface_Disposed;
            }
            this.Workbench.AddSurface(this.m_surface, true);
        }

        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.m_surface = null;
        }
    }
}
