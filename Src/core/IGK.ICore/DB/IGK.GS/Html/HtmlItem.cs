using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Settings;






﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.GS.Html
{
    public class HtmlItem : IHtmlItem
    {
        private HtmlItemAttributeCollection m_attributes;
        private HtmlItemCollection m_childs;
        private string m_tagName;
        private HtmlItem m_parent;
        private string m_Content;

        static List<string> sm_closedTag = new List<string>(new String[] {
            "title",
            "script"
        });
        public  HtmlItemCollection Childs{get{return this.m_childs; }}

        private bool m_Visible;

        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                }
            }
        }

        public virtual  bool MustHaveCloseTag
        {
            get
            {
                return sm_closedTag.Contains(this.TagName);
            }
        }
        public string Content
        {
            get { return m_Content; }
            set
            {
                if (m_Content != value)
                {
                    m_Content = value;
                }
            }
        }
        public void ClearChilds() {
            this.m_childs.Clear();
        }

        protected virtual bool CanAddItem {
            get { return true; }
        }
        public virtual void add(HtmlItem htmlItem)
        {
            this.m_childs.Add(htmlItem);
        }
        public virtual HtmlItem  add(string tagName)
        {
            if (this.CanAddItem)
            {
                HtmlItem c = HtmlItem.CreateWebNode(tagName);
                this.add(c);
                return c;
            }
            return null;
        }
        public HtmlItem Parent {
            get {
                return this.m_parent;
            }
            set {
                this.m_parent = value;
            }
        }

        public string TagName {
            get {
                return this.m_tagName;
            }
        }
        protected string indentLine(IHtmlItemRenderOptions option)
        {
            if ((option != null) && (option.Indent))
                return "\n";
            return string.Empty;
        }
        protected string getDepthIndent(IHtmlItemRenderOptions options)
	{
		if ((options==null) || (!options.Indent))
			return null;		
			
		
		var q = this;
		var s = "";
		if (options.ParentDepth!=null){
			
			while( (q!=null) && ((q = q.Parent)!=null) && (options.ParentDepth != q))
			{				
				s += "\t";
			}			
			
		}
		else{		
			while((q = q.Parent)!=null)
			{
				s += "\t";
			}
		}
		return s;
	}
        public override string ToString()
        {
            return "HtmlItem [" + this.TagName + "]";
        }
        public void Load(string content)
        { 
            //StringBuilder sb = new StringBuilder ();
            //sb.Append (content );
            StringReader v_sreader = new StringReader(content);
            global::System.Xml.XmlReader v_reader =
                global::System.Xml.XmlReader.Create(v_sreader);
            HtmlItem v_currentitem = null;
            while (v_reader.Read())
            {
                switch (v_reader.NodeType)
                {
                    case System.Xml.XmlNodeType.Attribute:
                        this[v_reader.Name] = v_reader.Value;
                        break;
                    case System.Xml.XmlNodeType.CDATA:
                        break;
                    case System.Xml.XmlNodeType.Comment:
                        break;
                    case System.Xml.XmlNodeType.Document:
                        break;
                    case System.Xml.XmlNodeType.DocumentFragment:
                        break;
                    case System.Xml.XmlNodeType.DocumentType:
                        break;
                    case System.Xml.XmlNodeType.Element:                        
                        HtmlItem i = new HtmlItem(v_reader.Name);
                        if (v_currentitem != null)
                            v_currentitem.add(i);
                        else
                        {
                            this.add(i);
                        }
                         v_currentitem = i;
                        if (v_reader.HasAttributes)
                        {
                            int c = v_reader.AttributeCount;
                            if (v_reader.MoveToFirstAttribute())
                            {
                                this[v_reader.Name] = v_reader.Value;
                                while (v_reader.MoveToNextAttribute())
                                {
                                    this[v_reader.Name] = v_reader.Value;
                                }
                                v_reader.MoveToElement();
                            }
                        }
                        //string v = v_reader.ReadInnerXml();
                        //i.Load(v);
                        //this.m_childs.Add(i);
                        break;
                    case System.Xml.XmlNodeType.EndElement:
                        if (v_currentitem != null)
                            v_currentitem = v_currentitem.Parent;
                        break;
                    case System.Xml.XmlNodeType.EndEntity:
                        break;
                    case System.Xml.XmlNodeType.Entity:
                        break;
                    case System.Xml.XmlNodeType.EntityReference:
                        break;
                    case System.Xml.XmlNodeType.None:
                        break;
                    case System.Xml.XmlNodeType.Notation:
                        break;
                    case System.Xml.XmlNodeType.ProcessingInstruction:
                        break;
                    case System.Xml.XmlNodeType.SignificantWhitespace:
                        break;
                    case System.Xml.XmlNodeType.Text:
                        this.m_Content = v_reader.Value;
                        break;
                    case System.Xml.XmlNodeType.Whitespace:
                        break;
                    case System.Xml.XmlNodeType.XmlDeclaration:
                        break;
                    default:
                        break;
                }
            }
        }

        public HtmlItem(string tagName)
        {
            this.m_attributes = new HtmlItemAttributeCollection(this);
            this.m_childs = new HtmlItemCollection(this);
            this.m_Visible = true;
            this.m_tagName = tagName;
        }
        public object  this[string key]{
            get{
                if (this.m_attributes.Contains(key))
                    return this.m_attributes[key].Value;
                return null;
            }
            set {
                if (this.m_attributes.Contains(key))
                {
                    if (value == null)
                    {
                        this.m_attributes.Remove(key);
                    }
                    else
                        this.m_attributes[key].Value = value;
                }
                else if (value !=null) {
                    this.m_attributes.Add(key, new HtmlItemAttribute(key, value));
                }
            }
        }
        public class HtmlItemAttributeCollection : IEnumerable
        {
            Dictionary<string, HtmlItemAttribute> m_attribute;
            private HtmlItem m_owner;

            public override string ToString()
            {
                return GetType().Name + string.Format(" - [{0}]", this.Count);
            }
            public HtmlItemAttribute this[string key]
            {
                get { 
                    if (this.m_attribute .ContainsKey (key))
                        return this.m_attribute[key];
                    return null;
                }
              
            }
            public HtmlItemAttributeCollection(HtmlItem owner)
            {
                this.m_owner = owner;
                this.m_attribute = new Dictionary<string, HtmlItemAttribute>();
            }
            public int Count {
                get {
                    return this.m_attribute.Count;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_attribute.Values.GetEnumerator();
            }

            internal bool Contains(string key)
            {
                return this.m_attribute.ContainsKey(key);
            }

            internal void Remove(string key)
            {
                if (this.m_attribute.ContainsKey (key))
                    this.m_attribute.Remove(key);
            }

            internal void Add(string key, HtmlItemAttribute htmlItemAttribute)
            {
                this.m_attribute.Add(key, htmlItemAttribute);
            }
        }
        public class HtmlItemCollection : IEnumerable
        {
            List<HtmlItem> m_childs;
            private HtmlItem m_owner;
            public override string ToString()
            {
                 return GetType().Name + string.Format(" - [{0}]", this.Count);
            }
            public HtmlItemCollection(HtmlItem owner)
            {
                this.m_childs = new List<HtmlItem>();
                this.m_owner = owner;
            }
            /// <summary>
            /// add child
            /// </summary>
            /// <param name="item"></param>
            public void Add(HtmlItem item) {
                if (this.m_owner.CanAddItem && (item != null) && !this.m_childs.Contains(item))
                {
                    this.m_childs.Add(item);
                    item.m_parent = this.m_owner;
                }
            }
            /// <summary>
            /// remove child
            /// </summary>
            /// <param name="item"></param>
            public void Remove(HtmlItem item) {
                if (this.m_childs.Contains(item))
                {
                    this.m_childs.Remove(item);
                    item.m_parent = null;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_childs.GetEnumerator();
            }
            public int Count { get { return this.m_childs.Count; } }

            internal void Clear()
            {
                foreach (var item in this.m_childs)
                {
                    item.m_parent = null;
                }
                this.m_childs.Clear();
            }
        }
        
        
        public virtual string Render(IHtmlItemRenderOptions option)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append ("<"+this.TagName);
            if (this.HasAttributes)
            {
                string s = string.Empty;
                
                foreach (HtmlItemAttribute item in this.m_attributes)
                {
                    s = GetAttribValueString(item);
                    if (!string.IsNullOrEmpty(s))
                    {
                        sb.Append(" ");
                        sb.Append(s);                     
                    }
                }
            }
            if (!string.IsNullOrEmpty (this.Content) || this.HasChilds || this.MustHaveCloseTag)
            {
                //render inner HTML
                sb.Append(">");
                if (!string.IsNullOrEmpty(this.Content))
                    sb.Append(this.getDepthIndent(option)+this.Content+this.indentLine(option));
                foreach (HtmlItem item in this.m_childs)
                {
                    if (item.Visible)
                    {
                        sb.Append(item.Render(option));
                    }
                }
                sb.Append("</"+this.TagName+">");
            }
            else
                sb.Append("/>");
            return sb.ToString();
        }

        private string GetAttribValueString(HtmlItemAttribute item)
        {
            return string.Format("{0}=\"{1}\"", item.Name, item.Value);
        }



        public bool HasChilds { get { return (this.m_childs.Count > 0); } }
        public bool HasAttributes { get { return (this.m_attributes.Count > 0); } }

        /// <summary>
        /// create web node
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static HtmlItem CreateWebNode(string p)
        {
            switch (p.ToLower())
            {      
                case "script":
                    return new HtmlScript();
                default:
                    return new HtmlItem(p);
                    
            }
        }

        
    }
}
