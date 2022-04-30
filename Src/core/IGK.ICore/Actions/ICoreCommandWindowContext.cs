using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
    public interface ICoreCommandWindowContext : ICoreActionContext 
    {
        void WriteLine(string message, Colorf color);
        /// <summary>
        /// write standard output
        /// </summary>
        /// <param name="message"></param>
        void CWriteLine(string message);
        /// <summary>
        /// write error output
        /// </summary>
        /// <param name="message"></param>
        void EWriteLine(string message);
        /// <summary>
        /// write info output
        /// </summary>
        /// <param name="message"></param>
        void IWriteLine(string message);
    }
}
