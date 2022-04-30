using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.Xml
{
    public static class GSXmlExtensions
    {
        public static void LoadDataAsAttribute(this CoreXmlElement e, IGSDataRow row)
        {
            if (e == null) return;

            for (int i = 0; i < row.FieldCount; i++)
            { 
                var n = row.GetName (i);
               
                e[n] = row.GetValue<string>(n);
            }
        }
    }
}
