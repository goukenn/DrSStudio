using IGK.ICore.Menu;
using IGK.ICore.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu.Tools
{
    [CoreMenu(BalafonDBBConstant.MENU_TOOLS_BALAFON_DATASCHEMA + ".ExportToCSharpCoreDbContract", 0x11)]
    class ExportToCsharpContractMenu : BalafonDBBMenuBase
    {
        protected override bool PerformAction(){
            var table = CurrentSurface.DataSchema;
            string folder = Environment.CurrentDirectory;
            using (CSharpControlExporter c = new CSharpControlExporter(this))
            {
                if (c.ShowDialog())
                {
                    BalafonUtility.ExportSchemaToCSInterfaceFile(c.SelectedFolder,
                        this.CurrentSurface.Document, null);
                }
            }
            return false;
        }
    }

    [ComVisible(true)]
    public class CSharpControlExporter : IDisposable
    {
        private ExportToCsharpContractMenu m_action;

        internal CSharpControlExporter(ExportToCsharpContractMenu exportToCsharpContractMenu)
        {
            m_action = exportToCsharpContractMenu;
        }

        public string SelectedFolder { get; set; }
        public void Dispose()
        {
         
        }
        public bool ShowDialog() {
            return false;
        }
    }
}
