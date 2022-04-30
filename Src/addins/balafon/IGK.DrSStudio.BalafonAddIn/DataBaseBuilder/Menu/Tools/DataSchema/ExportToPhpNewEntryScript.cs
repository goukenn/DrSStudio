using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.Menu.Tools
{
    [CoreMenu(BalafonDBBConstant.MENU_TOOLS_BALAFON_DATASCHEMA + ".ExportToPhpNewEntryScript", 0x10)]
    class ExportToPhpNewEntryScript : BalafonDBBMenuBase 
    {
        protected override bool PerformAction()
        {
            var table = CurrentSurface.DataSchema;
            if (table == null)
                return false;

            StringBuilder sb = new StringBuilder ();
            sb.AppendLine ("<?php");
            sb.AppendLine ("$t = array(");

            
            int i = 0;
            foreach (var s in table.GetColumnKeys()) {
                if (i == 1)
                    sb.AppendLine(",");
                sb.Append("\"" + s + "\"=>''");
                i = 1;
            }
            sb.AppendLine (");");
            sb.AppendLine ("?>");
            string tsb = IGK.ICore.IO.PathUtils.GetTempFileWithExtension("php");
            File.WriteAllText (tsb, sb.ToString());
#if DEBUG
            Process.Start (tsb);
#endif
            return true;
        }
    }
}
