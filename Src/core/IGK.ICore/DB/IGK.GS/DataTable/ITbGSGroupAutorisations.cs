using IGK.GS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS.DataTable
{
    [GSDataSystemTable(GSConstant.SYS_TABLE_GROUP_AUTH)]
    public interface ITbGSGroupAutorisations : IGSDataTable
    {
        /// <summary>
        /// group id
        /// </summary>
        [GSDataField(enuGSDataField.UniqueColumnMember, MemberIndex = 0)]
        ITbGSGroups clGroupId { get; set; }

        /// <summary>
        /// authorisation id
        /// </summary>
        [GSDataField(enuGSDataField.UniqueColumnMember, MemberIndex = 0)]        
        ITbGSAuthorisations clAuthId { get; set; }
        /// <summary>
        /// get or set the autorisation // Allow = 1 , Deny = 0
        /// </summary>
        int clGrant { get; set; }
    }
}
