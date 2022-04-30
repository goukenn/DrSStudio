using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// used for property setting configuration.
    /// </summary>
    public interface ICoreConfigurableEnumProperty
    {
        /// <summary>
        /// get default properties value of the item
        /// </summary>
        /// <param name="Name">Name</param>
        /// <returns></returns>
        string[] GetProperties(object source, string Name);
    }
}
