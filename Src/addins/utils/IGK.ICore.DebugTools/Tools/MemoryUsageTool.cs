using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.Tools
{
    [CoreTools("MemomyManagementTool")]
    class MemoryUsageTool : CoreToolBase
    {
        private static MemoryUsageTool sm_instance;
        private Thread mThread;
        private IXCoreStatusText c_memView;        
        private bool m_finish;
        private MemoryUsageTool()
        {
        }

        public static MemoryUsageTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static MemoryUsageTool()
        {
            sm_instance = new MemoryUsageTool();

        }


        public override void Configure()
        {
            base.Configure();
        }
        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreLayoutManagerWorkbench)
            {
                InitTool();
            }
        }

        private void InitTool()
        {
            this.c_memView = (this.Workbench as ICoreLayoutManagerWorkbench)?.LayoutManager.StatusControl.Items.Add(enuStatusItemType.text) as IXCoreStatusText;
            this.c_memView.Bounds = new Rectanglef(0, 0, 300, 10);
            this.c_memView.Spring = true;
            this.c_memView.Index = 0x100;


            //this.c_memView = CoreControlFactory.CreateControl("StatusText") as IXCoreStatusText;
            //this.c_memView2 = CoreControlFactory.CreateControl<IXCoreStatusText>();
            //this.Workbench.LayoutManager.StatusControl.Items.Add(this.c_memView);

            this.mThread = new Thread(UpdateMemory);
            this.mThread.SetApartmentState(ApartmentState.MTA);
            this.mThread.IsBackground = false;
            this.mThread.Start();
            Application.ApplicationExit += Application_ApplicationExit;
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            this.m_finish = true;
        }

        void UpdateMemory() {
            Process p = Process.GetCurrentProcess();
            while (!m_finish) {

                string meme = GetMemoryDisplay(p.VirtualMemorySize64);
                string tmeme = GetMemoryDisplay(GC.GetTotalMemory(true));
                this.c_memView.Text = $"Virtual : {meme} : Total GC : {tmeme}";

                Thread.Sleep(2000);
            }
            
        }

        private string GetMemoryDisplay(long size)
        {

            long[] Tb = new long[]{1099511627776,
        1073741824,
        1048576,
        1024,
        1};
            string[] co = new string[] {
                "To", "Go", "Mo", "Ko", "o"
            };
            for (int i = 0; i < Tb.Length; i++)
            {
                if (size > Tb[i]) {
                    return Math.Round((double)(size / Tb[i])) + "" + co[i];
                }
            }


            return size + "Mo";
        }
    }
}
