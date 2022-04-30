using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBSchemaEntries : BalafonDBBSchemaElement
    {
        public BalafonDBBSchemaEntries():
            base(BalafonDBBConstant.ENTRIES_TAG )
        {

        }

        public BalafonDBBTableEntry[] GetEntries(string tableName)
        {
            List<BalafonDBBTableEntry> e = new List<BalafonDBBTableEntry>();
            foreach (CoreXmlElement item in this.Childs)
            {
                if (item.TagName == BalafonDBBConstant.ROWSDEF_TAG) {
                    if (item["For"] == tableName) {

                        var b = item as BalafonDBBTableEntry;
                        if (item!=null)
                         e.Add(b);
                    }                
                }
            }
            return e.ToArray();
        }
        public override ICore.Xml.CoreXmlElement CreateChildNode(string tagName)
        {
            if (tagName == BalafonDBBConstant.ROWSDEF_TAG)
            {
                return new BalafonDBBTableEntry();    
            }
            var s  = base.CreateChildNode(tagName);
            if (s != null) {
                return s;
            }
            return new BalafonDBBTableEntry(tagName);       
        }
    }
}
