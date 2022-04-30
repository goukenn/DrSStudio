using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    /// <summary>
    /// represent the base class to manage an entry in a file
    /// </summary>
    public abstract class CoreXSDFileEntry : ICoreXSDFileEntry
    {
        public virtual  ICoreXSDObjectType Item
        {
            get;
        }

        public abstract CoreXSDItem[] ValueItems
        {
            get;
        }

        public abstract ICoreXSDFileEntry AddData(CoreXSDItem item);      

        public abstract void SaveData(CoreXmlElement e);
    }
}
