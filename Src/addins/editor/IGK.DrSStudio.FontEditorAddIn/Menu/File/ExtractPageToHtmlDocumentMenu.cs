using IGK.DrSStudio.Editor.FontEditor.WinUI;
using IGK.DrSStudio.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Editor.FontEditor.Menu.File
{
    [CoreMenu("File.ExtractFontDocument", 0x5003)]
    class ExtractPageToHtmlDocumentMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface is FontEditorSurface f) {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "html file |*.html";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        f.SaveHtmlDpcument(sfd.FileName);
                    }
                }
            }
            return base.PerformAction();
        }
    }
}
