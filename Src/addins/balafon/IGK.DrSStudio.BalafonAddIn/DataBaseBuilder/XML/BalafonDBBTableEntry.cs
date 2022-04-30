using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBTableEntry : BalafonDBBSchemaElement
    {
        private string m_table;
        internal BalafonDBBTableEntry()
            : base(BalafonDBBConstant.ROWSDEF_TAG) {

        }
        public BalafonDBBTableEntry(string table):
            base(BalafonDBBConstant.ROWSDEF_TAG)
        {
            this["For"] = table;
            this.m_table = table;
        }
        public override string ToString()
        {
            return string.Format("Table #" + this["For"]);
        }
    }
}
