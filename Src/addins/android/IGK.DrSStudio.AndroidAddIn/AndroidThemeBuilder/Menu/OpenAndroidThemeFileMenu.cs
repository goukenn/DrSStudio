
using IGK.ICore;using IGK.DrSStudio.Android.AndroidThemeBuilder.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.AndroidThemeBuilder
{
    [CoreMenu ("File.Open.Android.AndroidThemeFile",0x5) ]
    class OpenAndroitThemeFileMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ATBThemeFile f = ATBThemeFile.LoadFile(ofd.FileName);
                    if (f != null)
                    {
                        var s = AndroidThemeFileEditorSurface.Create(f);
                        Workbench.AddSurface (s,true );
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
