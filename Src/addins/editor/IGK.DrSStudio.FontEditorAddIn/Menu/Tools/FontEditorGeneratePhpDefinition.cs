using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Editor.FontEditor.Menu.Tools
{
    [CoreMenu("Tools.Font.GeneratePhpDefinition", 0x1F)]
    class FontEditorGeneratePhpDefinition : CoreApplicationMenu
    {
        private class DummyFontDefinition : IFontEditorDefinition
        {
            public string Name { get; set; }
            ///<summary>
            ///public .ctr
            ///</summary>
            public DummyFontDefinition()
            {

            }
        }

        protected override bool PerformAction()
        {
            var s = this.CurrentSurface as ICore2DDrawingSurface;
            if (s == null)
                return false;
            IFontEditorDefinition g = s is IFontEditorDefinition ?
                (IFontEditorDefinition)s :
                (s as ICoreParameterDefinition)?.GetParam<IFontEditorDefinition>();

            if (g == null) {
                g = new DummyFontDefinition() {
                    Name = "Dummy Font"
                };
            }

            if (g != null)
            {
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "php file | *.php";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GenerateBalafonFontDefinition.Generate(sfd.FileName,
                            g,
                            s.Documents.ToArray()
                            );
                    }
                }
            }
            return base.PerformAction();
        }
    }
}
