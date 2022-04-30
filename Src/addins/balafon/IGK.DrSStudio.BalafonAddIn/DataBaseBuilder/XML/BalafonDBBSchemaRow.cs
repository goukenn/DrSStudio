using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBSchemaRow : BalafonDBBSchemaElement
    {
        public BalafonDBBSchemaRow()
            : base(BalafonDBBConstant.ROWDEF_TAG)
        {

        }
        /// <summary>
        /// get the identifier
        /// </summary>
        /// <returns></returns>
        internal string GetIdentifierKey()
        {
            return string.Format("Row_{0}", this.GetHashCode());
        }
    }
}
