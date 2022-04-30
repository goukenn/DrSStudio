using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS.DataTable
{
    [GSDataSystemTable(GSConstant.SYS_TABLE_USER_GROUPS)]
    public interface ITbGSUserGroups : IGSDataTable
    {
        /// <summary>
        /// get the user group
        /// </summary>
        ITbGSUsers clUserId { get; set; }
        /// <summary>
        /// get the group id
        /// </summary>
        ITbGSGroups clGroupId { get; set; }
    }
    
}
