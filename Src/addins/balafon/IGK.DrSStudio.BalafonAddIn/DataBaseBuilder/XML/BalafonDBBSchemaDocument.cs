using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    [CoreWorkingObject(BalafonDBBConstant.DATA_SCHEMAS_TAG )]
    public class BalafonDBBSchemaDocument : BalafonDBBSchemaElement
    {
        private BalafonDBBSchemaEntries m_entries;
        private int m_Version;
        private List<BalafonDBBSchemaDataDefinition> m_tables;
        private event Action<string, string> TableNameChanged;

        public int Version
        {
            get { return m_Version; }
            set
            {
                if (m_Version != value)
                {
                    m_Version = value;
                    OnVersionChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler VersionChanged;

        protected virtual void OnVersionChanged(EventArgs e)
        {
            if (VersionChanged != null)
            {
                VersionChanged(this, e);
            }
        }


        
        /// <summary>
        /// number of table
        /// </summary>
        public int Count {
            get {
                return this.m_tables.Count;
            }
        }
        /// <summary>
        /// number of entries
        /// </summary>
        public BalafonDBBSchemaEntries Entries{
            get {
                return this.m_entries;
            }
        }
        public BalafonDBBSchemaDocument():base(BalafonDBBConstant.DATA_SCHEMAS_TAG )
        {
            this.m_tables = new List<BalafonDBBSchemaDataDefinition>();
            this.m_entries = this.Add(BalafonDBBConstant.ENTRIES_TAG)
                as BalafonDBBSchemaEntries;
            //first version of the file
            this.m_Version = 1;
            this["xmlns"] = BalafonDBBConstant.SCHEMA_NS;
        }

        internal BalafonDBBSchemaDataDefinition AddTable(string tablename)
        {
            var b = this.Add(BalafonDBBConstant.DATADEFINITION_TAG) as BalafonDBBSchemaDataDefinition;
            if (b != null)
            {
                b["TableName"] = tablename;
                return b;
            }
            return null;
        }
        
        public override bool AddChild(CoreXmlElementBase c)
        {
            if (c is BalafonDBBSchemaDataDefinition)
            {
                var e = c as BalafonDBBSchemaDataDefinition;
                if (!this.m_tables.Contains(e)) { 
                    this.m_tables.Add (e);
                }
            }
            return base.AddChild(c);
        }

        internal void ClearTable()
        {
            this.m_tables.Clear();
            this.m_entries.Clear();
            this.ClearChilds();
        }

        protected override void OnChildRemoved(CoreItemEventArgs<CoreXmlElementBase> item)
        {
            base.OnChildRemoved(item);

            BalafonDBBSchemaDataDefinition t = item.Item  as BalafonDBBSchemaDataDefinition;
            if ( (t!=null) && this.m_tables.Contains(t))
            {
                this.m_tables.Remove (t);
            }
        }
        protected override void OnChildClear(EventArgs eventArgs)
        {
            base.OnChildClear(eventArgs);
            //and always entries
            this.AddChild(this.m_entries);
        }
        public void Load(CoreXmlElement element) {
            var s = element["version"];
            if ((s != null) && CoreRegexUtils.IsInteger(s.ToString()))
            {
                this.m_Version = int.Parse(s.ToString ()); 
            }
            foreach (var i in element.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
                this.LoadString(i.RenderXML(null));
            }
            this.m_entries.Clear();
            string hs = string.Empty;
            foreach (CoreXmlElement i in element.getElementsByTagName(BalafonDBBConstant.ENTRIES_TAG))
            {
               
                hs = i.RenderInnerHTML(null);
                this.m_entries.LoadString(hs);
                
            }
        }
        protected void LoadTables() {
            this.m_tables.Clear();
            foreach (BalafonDBBSchemaDataDefinition i in this.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
                this.m_tables.Add(i);
            }
        }

        internal BalafonDBBSchemaDataDefinition GetTableDefinition(string p)
        {
            foreach (BalafonDBBSchemaDataDefinition i in this.getElementsByTagName(BalafonDBBConstant.DATADEFINITION_TAG))
            {
                if (i["TableName"] == p)
                    return i;
                
            }
            return null;
        }

        public string FileName { get; set; }

        internal void ChangeTableName(BalafonDBBSchemaDataDefinition t, string newTableName)
        {
            if (string.IsNullOrEmpty (newTableName ) ||
                    !BalafonUtility.ValidTableName(newTableName )                  
                 || (this.GetTableDefinition(newTableName) != null) )
                return;
            if (t.IsChildOf(this))
            {
                var old = t["TableName"];
                var c = Entries.getElementsByTagName(t.TagName).CoreGetValue<CoreXmlElementBase> (0) as CoreXmlElement;
                if (c != null) {
                    c.ChangeTagName(newTableName);
                }
                t["TableName"]  = newTableName;
                this.TableNameChanged?.Invoke(old, newTableName);
            }
        }
    }
}
