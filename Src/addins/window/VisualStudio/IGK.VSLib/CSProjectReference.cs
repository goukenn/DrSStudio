using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    class CSProjectReference : CSReference
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        public CSProjectReference():base(CSConstants.PROJECT_REFERENCE_TAG)
        {

        }
    }
}
