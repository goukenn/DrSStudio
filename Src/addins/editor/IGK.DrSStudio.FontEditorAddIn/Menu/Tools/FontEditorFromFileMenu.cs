using IGK.DrSStudio.WinUI;
using IGK.ICore.Menu;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.DrSStudio.Editor.FontEditor.WinUI;

namespace IGK.DrSStudio.Editor.FontEditor.Menu
{
    [CoreMenu("Tools.Font.FontEditorFromFile", 0x5004)]
    class FontEditorFromFileMenu: CoreApplicationMenu
    {

        protected override void InitMenu()
        {
            base.InitMenu();
        }
        protected override bool PerformAction()
        {
            using (var dial = this.Workbench.CreateOpenFileDialog())
            {
                dial.Title = "title.selectfontfile".R();
                dial.Filter = "true type font |*.ttf| Open Type Font | *.otf| supported font | *.ttf;*.otf|all files|*.*";
                if (dial.ShowDialog() == ICore.WinUI.enuDialogResult.OK)
                {

                    var s = new FontEditorSurface(dial.FileName);
                    this.Workbench.AddSurface(s, true);
                }

            }
            return false;
        }
    }
}
