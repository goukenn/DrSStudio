using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent a single application
    /// </summary>
    public abstract class CoreSingleApp : CoreApplicationBase 
    {
        public CoreSingleApp():base()
        {
            this.m_brushRegister = CreateBrushRegister();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected virtual ICoreBrushRegister CreateBrushRegister()
        {
            return new CoreBrushRegister();
        }
        /// <summary>
        /// override this to close your application
        /// </summary>
        public override void Close()
        {          
        }
        public override bool RegisterServerSystem(Func<CoreSystem> __initInstance)
        {
            __initInstance();
            return true;
        }

        public override bool RegisterClientSystem(Func<CoreSystem> __initInstance)
        {
            return false;
        }
#pragma warning disable 67
        public override event EventHandler ApplicationExit;
        private ICoreBrushRegister m_brushRegister;

        public override ICoreD2DPathUtils GraphicsPathUtils
        {
            get { return null; }
        }

        public override ICoreControlManager ControlManager
        {
            get { return null; }
        }

        public override ICoreResourceManager ResourcesManager
        {
            get { return null; }
        }

        public override ICoreBrushRegister BrushRegister
        {
            get {
                return this.m_brushRegister;
            }
        }

        public override string StartupPath
        {
            get { return Path.GetDirectoryName(GetType().Assembly.Location); }
        }

        public override string CurrentWorkingPath
        {
            get { return this.StartupPath; }
        }

        public override string UserAppDataPath
        {
            get { return this.StartupPath; }
        }

        public override bool IsTransparentProxy(object obj)
        {
            return false;
        }

        public override ICoreScreenInfo GetScreenInfo()
        {
            return null;
        }

        public override string PrivateFontsDirectory
        {
            get { return Path.Combine(this.StartupPath, "Fonts"); }
        }

      
    }
}
