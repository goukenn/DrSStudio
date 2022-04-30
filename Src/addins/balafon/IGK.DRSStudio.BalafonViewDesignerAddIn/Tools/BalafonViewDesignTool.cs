using IGK.DrSStudio.BalafonDesigner.Settings;
using IGK.DRSStudio.BalafonDesigner;
using IGK.DRSStudio.BalafonDesigner.WinUI;
using IGK.ICore;
using IGK.ICore.Actions;
using IGK.ICore.Tools;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.BalafonDesigner.Tools
{
    [CoreTools("BalafonDesignerTool")]
    class BalafonViewDesignTool : CoreToolBase, 
        ICoreTool, 
        ICoreFilterMessageAction,
        IBindToolHost
    {

        private PhpServer m_server;
        private int m_port;

        private static BalafonViewDesignTool sm_instance;

        /// <summary>
        /// get the cucrrent solution
        /// </summary>
        private BalafonViewDesignerSolution m_solution;


        /// <summary>
        /// get the port
        /// </summary>
        public int Port { get => m_port; }

        public new BalafonViewDesignerSurface CurrentSurface {

            get {
                return base.CurrentSurface as BalafonViewDesignerSurface;
            }
        }
        public BalafonViewDesignerSolution Solution {

            get => m_solution;
        }
        private BalafonViewDesignTool()
        {
        }

        public static BalafonViewDesignTool Instance
        {
            get
            {
                return sm_instance;
            }
        }

        public int Priority { get { return 0; } }

        static BalafonViewDesignTool()
        {
            sm_instance = new BalafonViewDesignTool();

        }

        protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.RegisterBenchEvent(Workbench);
            if (Workbench is ICoreSolutionManagerWorkbench s ) {
                s.SolutionChanged += S_SolutionChanged;
            }
        }

        protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
        {
            base.UnregisterBenchEvent(Workbench);
            if (Workbench is ICoreSolutionManagerWorkbench s)
            {
                s.SolutionChanged -= S_SolutionChanged;
            }
        }

        private void S_SolutionChanged(object sender, EventArgs e)
        {
            m_solution = 
            (Workbench as ICoreSolutionManagerWorkbench)?.Solution as BalafonViewDesignerSolution;

            if (m_solution != null) {

                this.Initialze(m_solution);
            }

     
        }

        private void Initialze(BalafonViewDesignerSolution m_solution)
        {
            var t = this.Solution.DefaultSurfaceType;
            var v_sin = BalafonViewDesignerSettings.Instance;

            if (t == null)
                throw new BalafonDesignerException("Solution default surface type not Setup");

            if (t != null){
                var v_s = t.Assembly.CreateInstance(t.FullName) as ICoreWorkingSurface;
                Workbench.AddSurface(v_s, true);
              
            }

            if (m_server == null) {

                m_server = new PhpServer() {
                    DocumentRoot = v_sin.DocumentRoot,
                    Port = v_sin.DefaultPort,
                    TargetSDKFolder = (CoreSystem.GetSettings()["ENVIRONMENT"]?["PHPSDKFolder"]?.Value ?? v_sin.DefaulPhpSDKFolder) as string
                };
            

                m_server.SetRespondListener(new PhpServerService(this));
                m_server.StartServer();

                m_port = m_server.Port;

                Application.ApplicationExit += Application_ApplicationExit;

            }
            var _surface = this.CurrentSurface;
            _surface.BindTool = this;
            _surface.Navigate();
            _surface.WebHost.AccelerateKeyEvent += ApplicationKeyHandle;

            //(this.Workbench as ICoreApplicationWorkbench)?.ActionRegister.AddFilterMessage(this);
        }

        private void ApplicationKeyHandle(object sender, KeyEventArgs e)
        {

            var m = WinCoreMessageFilter.WinCoreMessageHost.CreateMessage(this.CurrentSurface.Handle , e);

            (Workbench as ICoreApplicationWorkbench).DispatchMessage(m);



            //CoreSystem.Instance..DispatchAction(e);
            //switch (e.EventType)
            //{
            //    case CoreAcceleratorKeyEventType.KeyUp:
            //        if (e.VirtualKey == VirtualKey.F5)
            //        {
            //            CoreSystem.GetAction("HandleStartMenuF5")?.DoAction();
            //        }
            //        break;
            //}
        }

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            m_server.StopServer();
        }

        public bool PreFilterMessage(ref ICoreMessage m)
        {
            var s = this.CurrentSurface;
            if (s != null) {


            }
            return  false;
        }

        class PhpServerService : IPhpFileServerListener 
        {
            private BalafonViewDesignTool balafonViewDeisgnTool;

            public PhpServerService(BalafonViewDesignTool balafonViewDeisgnTool)
            {
                this.balafonViewDeisgnTool = balafonViewDeisgnTool;
            }

            public void SendResponse(PhpResponseBase phpAsyncResponse)
            {
                string filename = phpAsyncResponse.RequestFile;
                phpAsyncResponse.Execute(filename);
            }
        }
    }
}
