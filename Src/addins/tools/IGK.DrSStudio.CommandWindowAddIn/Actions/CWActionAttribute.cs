using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.CommandWindow.Actions
{
    /// <summary>
    /// represent the default CW action attribute
    /// </summary>
    public class CWActionAttribute : CoreActionAttribute
    {
        public CWActionAttribute(string name):base(name)
        {

        }
    }
}
