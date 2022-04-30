using IGK.DrSStudio.FileBrowser.WinUI;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.FileBrowser.Menu.Tools
{
    [CoreTools ("FBTool")]
    class FBTool : CoreToolBase
    {
        private static FBTool sm_instance;
        private FBTool()
        {
        }

        public static FBTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static FBTool()
        {
            sm_instance = new FBTool();

        }
        FBSurface cb; 
        internal FBSurface GetSurface()
        {
            if (cb == null)
            {
                cb = new FBSurface();
                cb.Disposed += cb_Disposed;
            }
            return cb;
        }

        void cb_Disposed(object sender, EventArgs e)
        {
            cb = null;
        }
    }
}
