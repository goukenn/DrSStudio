using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Menu.Tools
{
    /// <summary>
    /// tool font convert .woff to .TTF font menu
    /// </summary>
    [CoreMenu("Tools.Font.ConvertWoffToTTF", 1)]
    class ConvertWOFFToTTFMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "woff file | *.woff";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (WOFFFile.ConvertToTTF(ofd.FileName,
                         Path.Combine(
                             PathUtils.GetDirectoryName(ofd.FileName),
                             Path.GetFileNameWithoutExtension(ofd.FileName) + ".ttf")))
                    {
                        MessageBox.Show("msg.convertion.complete.ok".R(), "title.convert.result".R(),
                             MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else {
                        MessageBox.Show("msg.convertion.complete.failed".R(), "title.convert.result".R(),
                             MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            return false;
        }
    }
}
