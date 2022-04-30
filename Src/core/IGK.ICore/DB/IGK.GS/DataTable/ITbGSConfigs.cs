using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
     [GSDataSystemTable(GSSystemDataTables.Configs)]
    public interface ITbGSConfigs : IGSDataTable
    {
        [GSDataField(Binding = enuGSDataField.IsNotNull, TypeName = GSConstant.VARCHAR, Length = 30)]
        string clName { get; set; }
        string clValue { get; set; }
        string clDescription { get; set; }
    }
}
