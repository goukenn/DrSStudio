using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
    public interface ITbGSEnumDataTable : IGSDataTable 
    {
         [GSDataField(enuGSDataField.Unique ,
             TypeName = GSConstant.VARCHAR,
             Length=33)]
        string clName{get; set;}
    }
}
