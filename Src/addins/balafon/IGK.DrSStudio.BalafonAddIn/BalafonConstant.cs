using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    /// <summary>
    /// represent constant used in balafon system
    /// </summary>
    public static class BalafonConstant
    {

        
        internal const string PROJECT_NAME = "Balafon";
        internal const string PROJECT_TAG = "BalafonProject";
        internal const string PROJECT_ITEM_TAG = "Item";
        internal const string PROJECT_ITEM_GROUP_TAG = "ItemGroup";
        internal const string PROJECT_FILE_TAG = "File";
        internal const string PROJECT_FOLDER_TAG = "Folder";
        internal const string PROJECT_CONTROLER_TAG = "Controler";
        internal const string NEW_FILENAME = "new_project" ;
        internal const string NEW_FILENAME_EXTENSION = ".workbench.balproj";
        internal const string NEW_FILENAME_EXT = "workbench.balproj";
        internal const string PROJECT_PROPERTY_GROUP_TAG = "PropertyGroup";
        internal const int BALAFON_MANAGER_PORT = 8332;

#if DEBUG
        internal const string RES_FOLDER = CoreConstant.DRS_SRC + @"\addins\web\IGK.DrSStudio.BalafonAddIn\Resources";
#else 
        internal const string RES_FOLDER = null;
#endif
        
    }
}
