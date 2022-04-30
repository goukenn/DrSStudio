using IGK.ICore;
using IGK.ICore.Xml;
using IGK.ICore.Xml.XSD;
using System;
using System.Collections;
using System.Collections.Generic;

namespace IGK.DrSStudio.ColladaEngine
{
    public abstract class ColladaTypeBase: ICoreXSDType
    {
        public int Index { get; internal protected set; }
        private string m_Name;
        private ColladaTypeBase m_parent;
        public Dictionary<string, string> Infos { get; internal set; }
        private ColladaTypeDescription m_Annotation;
        Dictionary<string, ColladaTypeAttribute> m_attributes;
        List<ColladaTypeBase> m_items;
        private ICoreXSDItemChain m_chain;
        private ICoreXSDRestrictionInfo m_restriction;
        private ICoreXSDSourceType m_sourceType;

        public ColladaTypeDescription Annotation => m_Annotation;
        public ICoreXSDSourceType SourceType
        {
            get { return m_sourceType; }
           
        }

        public ColladaTypeBase[] Items => m_items.ToArray ();
        public ColladaTypeBase Parent=> m_parent;


        public ICoreXSDAttribute[] GetAttributes()
        {
            ICoreXSDAttribute[] attr = new ICoreXSDAttribute[this.m_attributes.Count];
            int i = 0;
            foreach(ICoreXSDAttribute h in m_attributes.Values)
            {
                attr[i] = h;
                i++;

            }
           // m_attributes.Values.CopyTo (attr,0);//.ConvertTo<ICoreXSDAttribute>();
            return attr;//.ConvertTo< ICoreXSDAttribute>();
        }

        public bool HasAttribute => this.m_attributes.Count > 0;
        public virtual bool IsRequired=>false;


        ICoreXSDType[] ICoreXSDType.Items
        {
            get
            {
                return this.Items.ConvertTo<ICoreXSDType>();
            }
        }

        public ICoreXSDItemChain GetChain(ICoreXSDSourceType source)
        {
            if (this.m_chain==null)
            {
                this.m_chain = CoreXSDItem.BuildChain(this, source, null);          
            }
            return this.m_chain;
        }
        public ColladaTypeBase()
        {
            this.m_attributes = new Dictionary<string, ColladaTypeAttribute> ();
            this.m_Annotation = new ColladaTypeDescription ();
            this.m_items  = new List<ColladaTypeBase> ();
            this.m_sourceType = null;
            this.Index = -1;
        }

        //public ICoreXSDItemChain GetChainsElements() {
        //    if (this.m_chain ==null)
        //        this.m_chain =  CoreXSDItem.BuildChain(this as ICoreXSDType );
        //    return this.m_chain;
        //}
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
                this.m_parent is ColladaTypeSequence;
         public virtual bool IsInChoice =>
                this.m_parent is ColladaTypeChoice ;

        public virtual string TypeName{
            get {
                return (this.Infos!=null) && this.Infos.ContainsKey("type") ? this.Infos["type"] : null; }
            }

        public int MinOccurs => this.Infos!=null && this.Infos.ContainsKey("minoccurs")? Int32.Parse ( this.Infos["minoccurs"]) : 1;
        public int MaxOccurs => this.Infos != null && this.Infos.ContainsKey("maxoccurs")?
            ( (this.Infos["maxoccurs"] != "unbounded") ? 
            Int32.Parse(this.Infos["maxoccurs"]) : -1) 
            : 1;

        public bool HasRestriction => m_restriction !=null;

        ICoreXSDType ICoreXSDType.Parent
        {
            get
            {
                return this.Parent;
            }
        }

        /// <summary>
        /// when override add attribute 
        /// </summary>
        public virtual void addAttribute(string v_n)
        {
        }
        public virtual void addAttribute(ColladaTypeAttribute attr) {
            
            var k = attr.Name == null? attr.Ref : attr.Name;
            if (k == null) {
                throw new Exception ($"key not valid");
            }
            this.m_attributes.Add(k, attr);
        }

        internal void addChild(ColladaTypeBase item)
        {
            this.m_items.Add(item);
            item.m_parent = this;
            item.Index = this.m_items.Count-1;
        }

       


        public void SetRestriction(string baseType, IEnumerable<KeyValuePair<string, object>> r)
        {
            this.m_restriction =  CoreXSDItem.CreateRestrictionInfo(baseType, r);
           
        }

        public IEnumerable<KeyValuePair<string, object>> GetRestriction()
        {
           return this.m_restriction ;
        }

        public ICoreXSDAttribute GetAttribute(string attrName)
        {
            if (this.m_attributes.ContainsKey(attrName))
                return this.m_attributes[attrName];
            return null;
        }
    }
}