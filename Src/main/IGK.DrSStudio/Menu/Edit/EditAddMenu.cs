using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.Edit
{
    [CoreMenu("Edit.Add", 0x01,IsNoRegistered=true)]
    sealed class EditAddMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
        protected override bool IsVisible()
        {
            return (this.Childs.Count > 0);// base.IsVisible();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.SetupEnableAndVisibility();
        }
    }
}
