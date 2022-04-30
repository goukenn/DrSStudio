using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.VSLib
{
    class CSConfiguration : CSItemBase
    {
        [CoreXMLAttribute]
        public string Condition
        {
            get
            {
                return (string)this[nameof(Condition)];
            }
            set
            {
                this[nameof(Condition)] = value;
            }
        }

        ///<summary>
        ///public .ctr
        ///</summary>
        public CSConfiguration():base("Configuration")
        {

        }

        public CSConfiguration(string v):base(v)
        {
            
        }
    }
}
