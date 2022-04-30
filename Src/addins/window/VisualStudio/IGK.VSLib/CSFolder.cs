using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    /// <summary>
    /// represent a reference to folder
    /// </summary>
    class CSFolder: CSReference
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        public CSFolder():base(CSConstants.FOLDER_TAG)
        {

        }
    }
}
