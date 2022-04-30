
using IGK.ICore;
using IGK.ICore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using IGK.DrSStudio.Drawing3D.FileBrowser.WinUI;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.Tools
{
    [CoreTools("Tool.FBViewControlTool")]
    class FBViewControlTool : CoreToolBase
    {
        private static FBViewControlTool sm_instance;
        private FBControlSurface c_fileBrowser;
        private FBViewControlTool()
        {
        }
        public static FBViewControlTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static FBViewControlTool()
        {
            sm_instance = new FBViewControlTool();

        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }

        public FBControlSurface Surface {
            get {
                return this.c_fileBrowser;
            }
        }


        internal void ShowFileBrowser()
        {
            if (this.c_fileBrowser == null)
            {
                this.c_fileBrowser = new FBControlSurface();
                this.c_fileBrowser.Disposed += c_fileBrowser_Disposed;
                
            }
            this.Workbench.AddSurface (this.c_fileBrowser, true );
        }

        void c_fileBrowser_Disposed(object sender, EventArgs e)
        {
            this.c_fileBrowser = null;
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreWorkingSurfaceHandler s)
                s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
            __RegisterBenchEvent(Workbench as ICoreApplicationWorkbench);    
        }

        private void __RegisterBenchEvent(ICoreApplicationWorkbench Workbench)
        {
            if (Workbench == null)
                return;
            Workbench.MainForm.SizeChanged += MainForm_SizeChanged;
        }
        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            if (Workbench is ICoreWorkingSurfaceHandler s )
            s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
            __UnRegisterBenchEvent(Workbench as ICoreApplicationWorkbench);    
 
            base.UnregisterBenchEvent(Workbench);
        }

        private void __UnRegisterBenchEvent(ICoreApplicationWorkbench Workbench)
        {
            if (Workbench == null)
                return;
            Workbench.MainForm.SizeChanged -= MainForm_SizeChanged;
        }

        void MainForm_SizeChanged(object sender, EventArgs e)
        {
            var frm  = sender as ICoreMainForm;
            var s = this.Surface;
            if (( s!=null) && (this.Workbench.CurrentSurface == s))
            {
                Size2i minimunSize = new Size2i(160, 28);//minimized
                if (frm.Size.Equals(Size2i.Empty) ||
                    frm.Size.Equals (minimunSize ))
                {
                    this.Surface.EnabledRendering = false;

                }
                else
                {
                    this.Surface.EnabledRendering = true;
                }
            }
        }

        void Workbench_CurrentSurfaceChanged(object sender, ICore.CoreWorkingElementChangedEventArgs<ICore.WinUI.ICoreWorkingSurface> e)
        {
            if (this.Surface != null)
            {
                this.Surface.EnabledRendering = this.Surface == e.NewElement;
            }
            
        }
    }
}
