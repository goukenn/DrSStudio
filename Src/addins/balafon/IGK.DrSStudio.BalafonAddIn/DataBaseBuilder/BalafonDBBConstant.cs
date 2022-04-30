using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder
{
    internal static class BalafonDBBConstant
    {
        //constant file used
        public const string SCHEMA_NS = "https://shcema.igkdev.com/db-schema";
        public const string DATA_SCHEMAS_TAG = "data-schemas";
        public const string DATADEFINITION_TAG = "DataDefinition";
        public const string ENTRIES_TAG = "Entries";
        public const string RES_DATA = "databasebuilder";
        public const string COLUMNDEF_TAG = "Column";
        public const string ROWDEF_TAG = "Row";
        public const string ROWSDEF_TAG = "Rows";

        public const string FILE_NAME = "data.schema.xml";
        public const string BTN_SCRIPT_1  = "javascript: {0}; return false;";
        public const string RES_DATA_1 ="databasebuilder/resources/{0}";
        public const string MAIN_RESFILE = "balafon_primary.html";
        public const string RES_NEW_COLUMN = "balafon_add_new_column.html";
        public const string RES_EDIT_TABLE_NAME = "balafon_edit_table_name.html";

        public const string MENU_TOOLS_BALAFON = "Tools.Balafon";
        public const string MENU_TOOLS_BALAFON_DATASCHEMA = MENU_TOOLS_BALAFON+".DataSchema";

#if DEBUG
        internal const string BLFADDIN_FOLDER = CoreConstant.DRS_SRC + @"\addins\balafon\IGK.DrSStudio.BalafonAddIn\";
#else
        internal const string BLFADDIN_FOLDER = null;
#endif

    }
}
