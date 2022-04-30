using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    public class CSImport : CSItemBase
    {
        [CoreXMLAttribute]
        public string Project
        {
            get
            {
                return (string)this[nameof(Project)];
            }
            set
            {
                this[nameof(Project)] = value;
            }
        }

        ///<summary>
        ///public .ctr
        ///</summary>
        public CSImport() : base(CSConstants.IMPORT_TAG)
        {

        }
    }
}
