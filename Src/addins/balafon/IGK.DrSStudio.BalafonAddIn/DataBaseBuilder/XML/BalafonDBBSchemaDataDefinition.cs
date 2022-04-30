using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon.DataBaseBuilder.XML
{
    public class BalafonDBBSchemaDataDefinition : BalafonDBBSchemaElement
    {
        //public event Action<string> ColumnChanged;
        public event Action<string, string> ColumnRenamed;
        public event Action<string> ColumnAdded;
        public event Action<string> ColumnRemoved;


        private Dictionary<string, BalafonDBBSchemaColumn >  m_columnKeys;
        public  BalafonDBBSchemaDataDefinition()
            : base(BalafonDBBConstant.DATADEFINITION_TAG)
        {
            m_columnKeys = new Dictionary<string, BalafonDBBSchemaColumn>();
        }
        protected override void OnLoadingComplete(EventArgs eventArgs)
        {
            base.OnLoadingComplete(eventArgs);
            buildLoadingKeys();
        }
        public void RemoveColumn(string name) {
            if (m_columnKeys.ContainsKey(name))
            {
                var h = this.m_columnKeys[name];
                this.Remove(h);
                this.m_columnKeys.Remove(name);
                this.ColumnRemoved?.Invoke(name);
            }
            
        }

        private void buildLoadingKeys()
        {
            m_columnKeys.Clear();
            foreach (BalafonDBBSchemaColumn item in this.getElementsByTagName(BalafonDBBConstant.COLUMNDEF_TAG))
            {
                var n = item["clName"];
                if (m_columnKeys.ContainsKey(n))
                {//replace
                    m_columnKeys[n] =  item;
                }
                else 
                    m_columnKeys.Add(n, item);
            }
        }
        internal bool AddColumn(string name)
        {
            if (string.IsNullOrEmpty(name ) || this.m_columnKeys.ContainsKey(name))
                    return false;
            BalafonDBBSchemaColumn b = this.Add(BalafonDBBConstant.COLUMNDEF_TAG) as BalafonDBBSchemaColumn;
            b["clName"] = name;

            this.m_columnKeys.Add(name, b);
            this.ColumnAdded?.Invoke(name);
            return true;
         
        }
        /// <summary>
        /// get column definition
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal BalafonDBBSchemaColumn GetColumn(string name)
        {
            if (!string.IsNullOrEmpty (name) &&  this.m_columnKeys.ContainsKey(name))
            {
                return this.m_columnKeys[name];
            }
            return null;
        }

        internal void AppendNewColumn(string name =null)
        {
            if (name == null)
            {
                int id = CoreUtils.GetMaxId("clColumnName_(?<v>([0-9]+))", this.m_columnKeys.Keys, (v, m) =>
                {
                    //success
                    return Math.Max(v, m.Groups["v"].Value.CoreGetValue<int>());
                });

                this.AddColumn("clColumnName_" + id);
            }
            else {
                this.AddColumn(name);
            }
        }

        internal void ChangeColumnName(BalafonDBBSchemaColumn cd, string p)
        {
            if (!this.m_columnKeys.ContainsKey(p))
            {
                string s =  cd["clName"];

                if (string.IsNullOrEmpty(s) || !this.m_columnKeys.ContainsKey (s))
                    return;
                if (this.m_columnKeys[s] != cd)
                    return;

                this.m_columnKeys.Remove(s);
                this.m_columnKeys.Add(p, cd);
                cd["clName"] = p;
                this.ColumnRenamed?.Invoke(s, p);

            }
        }

        internal IEnumerable<string> GetColumnKeys()
        {
            return this.m_columnKeys.Keys;
        }

        
    }
}
