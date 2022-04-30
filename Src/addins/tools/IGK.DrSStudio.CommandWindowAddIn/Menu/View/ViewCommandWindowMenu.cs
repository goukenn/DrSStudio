using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Menu.View
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.CommandWindow.Tools;
    using IGK.DrSStudio.Tools;
    using IGK.ICore.Menu;
    using IGK.ICore.WinUI;
    [CoreViewMenuAttribute("CommandWindow", 3, Shortcut= enuKeys.M ,
        ImageKey = CoreImageKeys.MENU_TOOL_EXEC_GKDS )]
    class ViewCommandWindowMenu : CoreViewToolMenuBase
    {
        public ViewCommandWindowMenu():base(CommandWindowTool.Instance)
        {

        }
    }
}
