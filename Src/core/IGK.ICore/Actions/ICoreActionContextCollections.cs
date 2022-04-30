using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Actions
{
    public interface  ICoreActionContextCollections 
    {
        ICoreActionContext this[string name]
        {
            get;
        }
    }
}
