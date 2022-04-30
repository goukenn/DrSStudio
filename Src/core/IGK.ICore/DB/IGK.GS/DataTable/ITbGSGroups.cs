using IGK.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{

    [GSDataSystemTable("system:://groups")]
    public interface ITbGSGroups : IGSDataTable
    {
        [GSDataField(enuGSDataField.IsNotNull | enuGSDataField.Unique, TypeName= GSConstant.VARCHAR, Length = 50)]
        string clName { get; set; }
    }
}
