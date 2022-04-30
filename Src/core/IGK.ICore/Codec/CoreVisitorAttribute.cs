using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Codec
{
    [AttributeUsage(AttributeTargets.Class , AllowMultiple =false , Inherited =false )]
    public class CoreVisitorAttribute : CoreAttribute
    {
        ///<summary>
        ///public .ctr
        ///</summary>
        public CoreVisitorAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; set; }
    }
}
