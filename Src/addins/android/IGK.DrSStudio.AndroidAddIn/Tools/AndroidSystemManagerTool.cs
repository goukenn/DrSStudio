using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("AndroidSystemManager", Description="init environment")]
    class AndroidSystemManagerTool : AndroidToolBase 
    {
        private static AndroidSystemManagerTool sm_instance;
        private AndroidSystemManagerTool()
        {
        }

        public static AndroidSystemManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidSystemManagerTool()
        {
            sm_instance = new AndroidSystemManagerTool();

        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            this.Available = false;
            AndroidSystemManager.LoadEnvironment(() => {
                this.Available = true;
            });            
        }

        public bool Available { get; private set; }
    }
}
