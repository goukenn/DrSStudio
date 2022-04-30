using System;
using System.Collections;
using System.Collections.Generic;

namespace IGK.ICore.Xml.XSD
{
    public class CoreXSDTypeBase : ICoreXSDType
    {
        private string m_Name;
        private CoreXSDTypeBase m_parent;
        public Dictionary<string, string> Infos { get; internal set; }
        private CoreXSDItemDescription m_Annotation;
        private Dictionary<string, CoreXSDItemAttribute> m_attributes;
        private List<CoreXSDTypeBase> m_items;
        private ICoreXSDItemChain m_chain;
        private ICoreXSDRestrictionInfo m_restriction;
        public CoreXSDItemDescription Annotation => m_Annotation;

        public CoreXSDTypeBase[] Items => m_items.ToArray();
        public CoreXSDTypeBase Parent => m_parent;
        public int Index { get; internal protected set; }
        public override string ToString()
        {
            return $"{GetType().Name }:[{this.Name}]";

        }

        /// <summary>
        /// init or get the chain because to avoid coast of not used items. required sources
        /// /// </summary>
        /// <returns></returns>
        public ICoreXSDItemChain GetChain(ICoreXSDSourceType sourceType) {
            if (this.m_chain == null)
            {
                this.m_chain = CoreXSDItem.BuildChain(this, sourceType, null);
            }
            return this.m_chain ;
        }
        public ICoreXSDAttribute[] GetAttributes()
        {
            ICoreXSDAttribute[] attr = new ICoreXSDAttribute[this.m_attributes.Count];
            int i = 0;
            foreach (ICoreXSDAttribute h in m_attributes.Values)
            {
                attr[i] = h;
                i++;

            }
            // m_attributes.Values.CopyTo (attr,0);//.ConvertTo<ICoreXSDAttribute>();
            return attr;//.ConvertTo< ICoreXSDAttribute>();
        }

        public bool HasAttribute => this.m_attributes.Count > 0;
        public virtual bool IsRequired => false;


        ICoreXSDType[] ICoreXSDType.Items
        {
            get
            {
                return this.Items.ConvertTo<ICoreXSDType>();
            }
        }


        public CoreXSDTypeBase()
        {
            this.m_attributes = new Dictionary<string, CoreXSDItemAttribute>();
            this.m_Annotation = new CoreXSDItemDescription();
            this.m_items = new List<CoreXSDTypeBase>();
            this.Index = -1;
        }

      
        /// <summary>
        /// enumerate items
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetItems()
        {
            return m_items.GetEnumerator();
        }

        /// <summary>
        /// get or set the name of this collada item
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public virtual bool IsInSequence =>
                this.m_parent is ICoreXSDSequence;
        public virtual bool IsInChoice =>
               this.m_parent is ICoreXSDChoice;

        public virtual string TypeName
        {
            get
            {
                return (this.Infos != null) && this.Infos.ContainsKey("type") ? this.Infos["type"] : null;
            }
        }

        public int MinOccurs => this.Infos != null && this.Infos.ContainsKey("minoccurs") ? Int32.Parse(this.Infos["minoccurs"]) : 1;
        public int MaxOccurs => this.Infos != null && this.Infos.ContainsKey("maxoccurs") ?
            ((this.Infos["maxoccurs"] != "unbounded") ?
            Int32.Parse(this.Infos["maxoccurs"]) : -1)
            : 1;

        public bool HasRestriction => m_restriction != null;

        ICoreXSDType ICoreXSDType.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        public bool HasContainer { get; private set; }

        /// <summary>
        /// when override add attribute 
        /// </summary>
        public virtual void addAttribute(string v_n)
        {
        }
        public virtual void addAttribute(CoreXSDItemAttribute attr)
        {

            var k = attr.Name == null ? attr.Ref : attr.Name;
            if (k == null)
            {
                throw new Exception($"key not valid");
            }
            if (this.m_attributes.ContainsKey(k))
            {
                CoreLog.WriteDebug($"attribute list already contains {k}");//this.m_attributes[k]

                this.m_attributes[k] = AttributeList(this.m_attributes[k], attr);
                return ;
            }
            this.m_attributes.Add(k, attr);
        }

        private CoreXSDItemAttribute AttributeList(CoreXSDItemAttribute cl, CoreXSDItemAttribute attr)
        {
            if (cl is CoreXSDItemAttributeList) {

                (cl as CoreXSDItemAttributeList).Add(attr);
                return cl;
            }
            var g = new CoreXSDItemAttributeList(cl );
            g.Add (attr);
            return g;
        }

        internal void addChild(CoreXSDTypeBase item)
        {
            this.m_items.Add(item);
            item.m_parent = this;
            item.Index = this.m_items.Count - 1;
            this.HasContainer = this.HasContainer || (item is ICoreXSDItemContainer);
        }
        public virtual void SetRestriction(string baseType, IEnumerable<KeyValuePair<string, object>> r)
        {
            this.m_restriction = CoreXSDItem.CreateRestrictionInfo(baseType, r);
        }
        public virtual IEnumerable<KeyValuePair<string, object>> GetRestriction()
        {
            return this.m_restriction;
        }

        public ICoreXSDAttribute GetAttribute(string attrName)
        {
            if (this.m_attributes.ContainsKey(attrName))
                return this.m_attributes[attrName];
            return null;
        }


        class CoreXSDItemAttributeList : CoreXSDItemAttribute
        {
            private CoreXSDItemAttribute attr;
            List<CoreXSDItemAttribute> m_list;

            
            internal CoreXSDItemAttributeList(CoreXSDItemAttribute attr):base()
            {
                this.attr = attr;
                this.Name = attr.Name;
                this.m_list = new List<CoreXSDItemAttribute> ();
                this.m_list.Add (attr);


            }

            internal void Add(CoreXSDItemAttribute attr)
            {
                if (this.m_list.Contains(attr))
                    return ;
                this.m_list .Add(attr);
            }
        }
    }
}