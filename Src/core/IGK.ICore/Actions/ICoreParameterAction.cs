using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
    public interface  ICoreParameterAction : ICoreAction 
    {
        /// <summary>
        /// Get or set the parameters
        /// </summary>
        object Params { get; set; }
    }
}
