using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    public static class CoreCollectionExtension
    {
        public static object GetElementById(this ICoreWorkingElementCollections c, string id)
        {
            
            foreach (ICoreWorkingObject item in c)
            {
                if (item.Id == id)
                    return item;
                if (item is ICoreWorkingElementContainer)
                {
                    var b = (item as ICoreWorkingElementContainer).GetElementById(id);
                    if (b != null)
                        return b;
                }
            }
            return null;
        }
    }
}
