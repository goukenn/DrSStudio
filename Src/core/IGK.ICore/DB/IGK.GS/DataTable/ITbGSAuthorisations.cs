using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
    [GSDataSystemTable(GSConstant.SYS_TABLE_AUTH)]
    /// <summary>
    /// represent a autorisation model
    /// </summary>
    public interface ITbGSAuthorisations : IGSDataTable
    {
        [GSDataField(TypeName="VARCHAR", Length = 60, Description="Authorisation's Name.(60 caractères)", Binding= enuGSDataField.Unique) ]
        string clName { get; set; }
     
    }
}
