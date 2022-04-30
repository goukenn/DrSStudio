using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Codec
{
    [AttributeUsage(AttributeTargets.Property , AllowMultiple =false , Inherited=false )]
    public class CoreXMLResourceElementAttribute : CoreXMLElementAttribute 
    {
        public CoreXMLResourceElementAttribute()
        {
        }

    }
}
