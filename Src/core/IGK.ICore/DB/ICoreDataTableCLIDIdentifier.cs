using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGK.ICore.DB
{
    /// <summary>
    /// represent an clIdIdentifier
    /// </summary>
    public interface ICoreDataTableCLIDIdentifier : ICoreDataTableColumnIdentifier
    {
        int clId { get; set; }
    }
}
