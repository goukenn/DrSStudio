using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu.Tools
{
    [CoreMenu("Tools.Balafon", 0xc100)]
    sealed class BalafonToolMenu : BalafonDBBMenuBase 
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
    }

    [CoreMenu(BalafonDBBConstant.MENU_TOOLS_BALAFON_DATASCHEMA, 0)]
    sealed class BalafonSchemaToolMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction()
        {
            return base.PerformAction();
        }
    }
}
