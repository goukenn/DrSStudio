using IGK.DrSStudio.Balafon.DataBaseBuilder.Menu;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Balafon.Menu.Tools
{
#if DEBUG
    [CoreMenu("Tools.Balafon.StoreCurrentDocDB",0 )]
    class StoreBalafonDocumentMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction()
        {
            var d = this.CurrentSurface.WebDocument;
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "html | *.html";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    System.IO.File.WriteAllText(sfd.FileName, d.RenderXML(new IGK.ICore.Web.CoreXmlHtmlOptions()));
                }
            }
            return false;
        }
    }
#endif
}
