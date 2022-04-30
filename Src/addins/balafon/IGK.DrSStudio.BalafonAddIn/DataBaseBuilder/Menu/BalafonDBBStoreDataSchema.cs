using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu
{
    [CoreMenu("File.ExportTo.StoreDataSchema", 0x13, Description = "save the content of surface as")]
    class BalafonDBBStoreDataSchema : BalafonDBBMenuBase 
    {
        protected override bool PerformAction()
        {
            this.Workbench.CallAction ("File.SaveAs");
            return false;
        }       
    }
}
