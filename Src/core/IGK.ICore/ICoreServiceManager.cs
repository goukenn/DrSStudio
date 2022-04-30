using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    /// <summary>
    /// represent the base handle of the in core service
    /// </summary>
    public interface ICoreServiceManager
    {
        /// <summary>
        /// get the information of this service
        /// </summary>
        string Description { get; }
    }
}
