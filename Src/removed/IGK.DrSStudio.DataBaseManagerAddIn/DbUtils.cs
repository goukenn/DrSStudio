

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:DbUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace IGK.DrSStudio.DataBaseManagerAddIn
{
    static class DbUtils
    {
        public static DataTable[] GetTables(DataTableCollection tables)
        {
            DataTable[] v_tables = new DataTable[tables.Count];
            tables.CopyTo(v_tables, 0);
            return v_tables;
        }
    }
}

