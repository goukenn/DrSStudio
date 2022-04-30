using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File."+nameof(SwitchEnvironmentMenu), 0x701,
        SeparatorBefore =true)]
    class SwitchEnvironmentMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.initEnvironementMenu();
        }

        private void initEnvironementMenu()
        {
            this.Childs.Add (new OtherSwitchEnvironmentMenu(this));
        }

        class OtherSwitchEnvironmentMenu : CoreApplicationMenu
        {
            private SwitchEnvironmentMenu switchEnvironmentMenu;

            public OtherSwitchEnvironmentMenu(SwitchEnvironmentMenu switchEnvironmentMenu)
            {
                this.switchEnvironmentMenu = switchEnvironmentMenu;

                CoreMenuAttribute v_attr = new CoreMenuAttribute($"{switchEnvironmentMenu.Id}.{nameof(OtherSwitchEnvironmentMenu)}",
                    short.MaxValue)
                {
                    SeparatorBefore = true
                };
                this.SetAttribute(v_attr);
                if (!this.Register(v_attr, this))
                {
                  CoreLog.WriteDebug ($"{nameof(OtherSwitchEnvironmentMenu)} Not Registrated");
                }
            }

            protected override void InitMenu()
            {
                base.InitMenu();
            }
        }


        class EnvironmentMenu : CoreApplicationMenu
        {
            private SwitchEnvironmentMenu switchEnvironmentMenu;
            private ICoreWorkbenchEnvironment m_environment;
            public EnvironmentMenu(SwitchEnvironmentMenu switchEnvironmentMenu,
                ICoreWorkbenchEnvironment environment)
            {
                this.switchEnvironmentMenu = switchEnvironmentMenu;
                this.m_environment = environment;
                //switchEnvironmentMenu.Workbench.EnvironmentChanged += Workbench_EnvironmentChanged;
            }

            //private void Workbench_EnvironmentChanged(object sender, ICore.CoreWorkingElementChangedEventArgs<ICoreWorkbenchEnvironment> e)
            //{
            //    throw new NotImplementedException();
            //}
        }
    }
}
