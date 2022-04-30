

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DbManagerConstant.cs
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
file:DbManagerConstant.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.DataBaseManagerAddIn
{
    public static class DbManagerConstant
    {
        public readonly static string DbSurfaceVersion = "v1.0";
        public readonly static string DbSurfaceDisplayName = "DbManager"+" "+DbSurfaceVersion;
        public  const string DB_ENVIRONMENT = "DBEnvironemnt";
        public const int DB_MENU_ACTION_INDEX = 0x400;
        public const int DB_MENU_PROPERTY_INDEX = 0x800;
        /*menu item */
        public const string MENU_ADDNEWTABLE = "Data.AddNewTable";
        public const string MENU_ADDCOLUMNTABLE = "Data.AddColumnTable";
    }
}

