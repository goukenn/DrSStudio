using IGK.CssPropertiesBuilder.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.CssPropertiesBuilder.Menu.File
{
    using IGK.ICore;
    using IGK.ICore.WinUI ;
    [CoreMenu ("File.New.CssFileBuilder", 0x40)]
    class NewCssFileBuilderMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            CssEditorSurface c = new CssEditorSurface();
            this.Workbench.AddSurface (c, true);
            return false;
        }
    }
}
