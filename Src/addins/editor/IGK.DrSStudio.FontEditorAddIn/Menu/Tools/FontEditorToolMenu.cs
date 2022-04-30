using IGK.DrSStudio.Editor.FontEditor.WinUI;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Editor.FontEditor.Menu
{
    [CoreMenu ("Tools.Font.FontEditor", 0x5003)]
    class FontEditorToolMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            FontEditorSurface v_surface = null;
            using (FontDialog ft = new FontDialog())
            {
                try
                {
                    if (ft.ShowDialog() == DialogResult.OK)
                    {
                         v_surface = new FontEditorSurface(ft.Font);
                        this.Workbench.AddSurface(v_surface,true);                        
                    }
                }
                catch (Exception Exception){
#if DEBUG
                    IGK.ICore.WinUI.CoreMessageBox.Show(Exception);
#else
                    CoreLog.WriteLine(Exception.Message);
#endif

                    (this.Workbench as ICoreWorbenchMultisurface).Surfaces.Remove(v_surface);
                    v_surface = null;
                }
            }
            return false;
        }
    }
}
