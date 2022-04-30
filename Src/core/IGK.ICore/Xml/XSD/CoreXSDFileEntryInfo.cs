using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{

    /// <summary>
    /// used to store info index
    /// </summary>
    struct CoreXSDFileEntryInfo
    {
        public ICoreXSDType Item { get; set; }
        public int Index { get; set; }

        public static CoreXSDFileEntryInfo Empty;

        static CoreXSDFileEntryInfo()
        {
            Empty = new CoreXSDFileEntryInfo(null, -1);
        }

        public CoreXSDFileEntryInfo(ICoreXSDType item, int index)
        {
            this.Item = item;
            this.Index = index;
        }
    }


}
