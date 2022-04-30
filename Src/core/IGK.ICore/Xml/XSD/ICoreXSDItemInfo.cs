using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Xml.XSD
{
    public interface ICoreXSDItemInfo
    {
        /// <summary>
        /// get available attributes name
        /// </summary>
        string[] Attributes { get; }
        /// <summary>
        /// get vailable elements name
        /// </summary>
        string[] Elements { get; }
    }
}
