
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXmlElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreXmlElement.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using IGK.ICore.ComponentModel;
using IGK.ICore.Web;
using IGK.ICore.Codec;

namespace IGK.ICore.Xml
{
    /// <summary>
    /// represent a base xml element
    /// </summary>
    public class CoreXmlElement : CoreXmlElementBase, System.Collections.IEnumerable, IXmlElement
    {
        internal protected static bool DEBUG = false; //for debugging purpose
        private object m_Content;
        private CoreXmlAttributeCollections m_attributes;
        private XmlChildsCollections m_childs;
        private string m_TagName;
        private Dictionary<string, CoreXmlAttributeValue> m_properties; //not stored propeties
        protected bool AllowRenderProperties { get { return false; } }
        /// <summary>
        /// get non stored properties. you must override the child Render method to store those properties as xmlement
        /// </summary>
        protected Dictionary<string, CoreXmlAttributeValue> Properties
        {
            get
            {
                return this.m_properties;
            }
        }
        public object GetAttributeValue(string name, object defaultvalue = null)
        {
            if (this.m_attributes.Contains(name))
            {
                return this.m_attributes[name].Value;
            }
            return defaultvalue;
        }
        public T GetElementValue<T>(string tagname, T defaultValue = default(T))
        {
            var s = this.getElementsByTagName(tagname);
            if ((s != null) && (s.Length == 1))
            {
                return (s[0] as CoreXmlElement).Content.CoreConvertTo<T>();
            }
            return defaultValue;
        }
        public T GetAttributeValue<T>(string name, T defaultvalue = default(T))
        {

            if (this.m_attributes.Contains(name))
            {
                object obj = this.m_attributes[name].Value;
                if (obj is T)
                {
                    return (T)obj;
                }
                else
                {
                    var c = CoreTypeDescriptor.GetConverter(typeof(T));
                    T s = (T)c.ConvertFrom(obj);
                    return s;
                }
            }
            return defaultvalue;
        }
        protected virtual void CopyAttributes(CoreXmlElement element)
        {
            if ((element == null) && !element.HasAttributes)
                return;

            foreach (KeyValuePair<string, CoreXmlAttributeValue> item in element.Attributes)
            {
                this[item.Key] = item.Value.GetValue();
            }
        }

        /// <summary>
        /// load xml file into xml element
        /// </summary>
        /// <param name="filename"></param>
        public static CoreXmlElement LoadFile(string filename)
        {
            if (File.Exists(filename))
            {
                CoreXmlElement d = CoreXmlElement.CreateXmlNode("dummy");
                d.LoadString(File.ReadAllText(filename));
                if (d.HasChild)
                {
                    if (d.Childs.Count == 1)
                    {
                        var e = d.Childs[0] as CoreXmlElement;
                        d.ClearChilds();
                        return e;
                    }
                }
            }
            return null;
        }
        public override string Value
        {
            get
            {
                return this.m_Content != null ? this.m_Content.ToString() : null;
            }
            set
            {
                this.m_Content = value;
            }
        }

        /// <summary>
        /// get the childs collections
        /// </summary>
        public override IXmlChildCollections Childs
        {
            get
            {
                return this.m_childs;
            }
        }
        /// <summary>
        /// override to load attribute from another xml element. default is a reference value copy.
        /// </summary>
        /// <param name="d"></param>
        public virtual void LoadAttributes(CoreXmlElement d)
        {
            if (d == null)
                return;
            foreach (KeyValuePair<string, CoreXmlAttributeValue> dd in d.Attributes)
            {
                this[dd.Key] = dd.Value;
            }
        }
        /// <summary>
        /// protected load attributes from this attributes
        /// </summary>
        /// <param name="e"></param>
        /// <param name="innamespace"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static bool LoadAttribute(CoreXmlElementBase e, List<string> innamespace, string name, string value)
        {
            if (name == "xmlns")
            {
                if (!innamespace.Contains(value))
                {
                    innamespace.Add(value);
                }
                else
                    return false;
            }
            e[name] = value;
            return true;
        }

        /// <summary>
        /// load string to this element
        /// </summary>
        /// <param name="text"></param>
        public void LoadString(string text)
        {
            XmlReader r = null;
            if (string.IsNullOrEmpty(text))
                return;
            string bck = text; // backup the string
                               //#if DEBUG
                               //            CoreLog.WriteDebug("Try loading : " + bck);
                               //#endif
                               //remove xml or doctype
            text = System.Text.RegularExpressions.Regex.Replace(text.Trim(),
                //"^\\<\\?xml(.)+\\?\\>", //start with xlm
                "\\<\\?xml(.)+\\?\\>",//in the document
                "");

            text = System.Text.RegularExpressions.Regex.Replace(text.Trim(),
                "\\<\\!DOCTYPE(.)+\\>", "");
            //remove comment
            text = System.Text.RegularExpressions.Regex.Replace(text.Trim(),
                "\\<\\!\\-\\-((.)+)\\-\\-\\>", "");
            text = System.Text.RegularExpressions.Regex.Replace(text,
                "&(([^; ])*;){0,1}", (s) =>
                {
                    if (s.Value == "&nbsp;")//entity not declared
                         return " ";
                    if (s.Value == "&")//replace with the real value 
                         return "&amp;";
                    return s.Value;
                });

            //if loading for web context

            //if web document . so script will be loaded
            text = CoreWebUtils.TreatHtmlText("script", text, (v) =>
               {
                   v = System.Text.RegularExpressions.Regex.Replace(v,
                           "&(([^;])*;){0,1}", (s) =>
                           {
                               if ((s.Value == "&") || (s.Value == "&amp;"))
                                   return "__&amp;__";

                               return s.Value;
                           });
                   return v.Replace("<", "__&lt;__").Replace(">", "__&gt;__");
               });
            //System.Text.RegularExpressions.Regex.Replace(text.Trim(),
            // @"\<script([^\>])*\>(?<value>[.\s\w\d\(\)\[\]\{\}\\""';,:+_\?&\|!\-=%\*@$#\<\>/]+)\</script\s*>", (s) =>
            // { 
            //     var k = s.Groups ["value"].Value.Replace ("<", "__&lt;__").Replace (">","__&gt;__");
            //     var h = s.Value.Replace (s.Groups ["value"].Value, k);
            //     return h;
            // }, RegexOptions.IgnoreCase | RegexOptions.Multiline  ); 

            string dummy = string.Format("<dummy {1}>{0}</dummy>", text, getLoadStringNamespace());
            XmlReaderSettings v_settings = new XmlReaderSettings();
            v_settings.IgnoreComments = true;
            v_settings.IgnoreWhitespace = true;
            v_settings.IgnoreProcessingInstructions = true;
            v_settings.DtdProcessing = DtdProcessing.Ignore;
            v_settings.ConformanceLevel = ConformanceLevel.Fragment;

            //diseable checking character 
            v_settings.CheckCharacters = true;


            List<string> v_namespace = new List<string>();

            r = XmlReader.Create(new StringReader(dummy), v_settings);
            r.ReadToDescendant("dummy");
            try
            {
                LoadData(r, v_namespace, dummy);
                //#if DEBUG
                //                while (r.Read()) {
                //                    }
                //#endif
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
            finally
            {
                if (r != null)
                    r.Close();
            }
        }
        /// <summary>
        /// get string load space format
        /// </summary>
        /// <returns></returns>
        protected virtual string getLoadStringNamespace()
        {
            return string.Format("xmlns:igk=\"{0}\" ", CoreConstant.XML_SCHEMA_WEBSITE);
        }
        /// <summary>
        /// used to load data from xml reader
        /// </summary>
        /// <param name="r"></param>
        /// <param name="v_namespace"></param>
        public virtual void LoadData(XmlReader r, List<string> v_namespace, string source = null)
        {
            try
            {
                while (r.Read())
                {
                    switch (r.NodeType)
                    {
                        case XmlNodeType.Element:

                            var g = this.CreateChildNode(r.Name);

                            if (g != null)
                            {
                                if (r.Name == "script")
                                {
                                    var s = g as CoreXmlWebScriptElement;
                                    if (s != null)
                                    {
                                        var vsubm = r.ReadSubtree();
                                        if (vsubm.Read())
                                        {
                                            var script = vsubm.ReadInnerXml();

                                            s.Content = script.Replace("__&gt;__", ">")
                                                .Replace("__&lt;__", "<")
                                            .Replace("__&amp;__", "&");
                                            this.AddChild(g);
                                        }
                                        break;
                                    }
                                }

                                //load attribute
                                if (r.HasAttributes)
                                {
                                    LoadAttributes(g, r, v_namespace);

                                }
                                r.MoveToElement();
                                if (!r.IsEmptyElement)
                                {
                                    XmlReader v_subReader = r.ReadSubtree();
                                    if (v_subReader.Read())
                                    {//move to node
                                        g.LoadData(v_subReader, v_namespace, source);
                                    }
                                }
                                g.OnLoadingComplete(EventArgs.Empty);
                                this.AddChild(g);
                            }
                            else
                                r.Skip();
                            break;
                        case XmlNodeType.Text:
                            string v_c = CoreXmlUtility.GetStringValue(this.Content);
                            if (string.IsNullOrEmpty(v_c))
                                this.Content = string.IsNullOrWhiteSpace(r.Value.Trim()) ? string.Empty : r.Value;
                            else
                            {
                                CoreXmlElementText t = new CoreXmlElementText();
                                t.Text = string.IsNullOrWhiteSpace(r.Value.Trim()) ? string.Empty : r.Value;
                                this.Childs.Add(t);
                            }
                            break;
                        case XmlNodeType.EndElement:
                            break;
                        default:
#if DEBUG
                            CoreLog.WriteLine("Not readed : " + r.NodeType);
#endif
                            break;
                    }
                }
            }
            catch (XmlException xexception)
            {
                CoreLog.WriteLine(this.GetType().Name + " :: " + xexception.Message);// + "\n"+source);
                if (xexception != null)
                {
                    var d = this.addDiv().setClass("igk-notify igk-notify-error");
                    d.addDiv().Content = "Exception";
                    d.addDiv().Content = "Error on script : " + xexception.Message;
                    d.add("prev").Content = "Name:" + r.Name;
                    if (source != null)
                    {
                        //apend line view
                        d.add("prev").setClass("igk-info-box").Content =
                            source.Split('\r').CoreGetValue<string>(xexception.LineNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
                if (ex != null)
                {
                    this.addDiv().setClass("igk-notify igk-notify-error").Content = "Error on script : " + ex.Message;
                }
            }
        }

        protected static void LoadAttributes(CoreXmlElementBase g, XmlReader r, List<string> v_namespace)
        {
            int c = r.AttributeCount;
            r.MoveToFirstAttribute();
            LoadAttribute(g, v_namespace, r.Name, r.Value);
            for (int i = 1; i < c; i++)
            {
                r.MoveToNextAttribute();
                LoadAttribute(g, v_namespace, r.Name, r.Value);
            }
        }

        /// <summary>
        /// create a xml node
        /// </summary>
        /// <param name="tagName">tag name. empty or null is not allowed </param>
        /// <returns></returns>
        public new static CoreXmlElement CreateXmlNode(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return null;
            Type t = Type.GetType(string.Format(CoreConstant.XMLELEMENT_CLASS_FORMAT, tagName), false, true);
            if ((t != null) && !t.IsAbstract)
            {
                return t.Assembly.CreateInstance(t.FullName) as CoreXmlElement;
            }
            switch (tagName.ToLower())
            {
                case "script":
                    return new CoreXmlWebScriptElement();
                case "link":
                    return new CoreXmlWebLinkElement();

            }
            return CoreXmlElementBase.CreateXmlNode(tagName) as CoreXmlElement;
        }
        public static CoreXmlElement CreateFromString(string content)
        {
            CoreXmlElement c = new CoreXmlElement("dummy");
            c.LoadString(content);
            if (c.HasChild)
                return c.Childs[0] as CoreXmlElement;
            return null;
        }
        public CoreXmlAttributeCollections Attributes
        {
            get
            {
                return this.m_attributes;
            }
        }
        public override string TagName
        {
            get { return m_TagName; }

        }
        protected void setTagName(string name)
        {
            m_TagName = name;
        }
        public bool HasChild
        {
            get
            {
                return (this.m_childs.Count > 0);
            }
        }
        public bool HasAttributes
        {
            get
            {
                return (this.m_attributes.Count > 0);
            }
        }
        public override bool CanAddChild
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// .Ctr
        /// </summary>
        /// <param name="name"></param>
        internal protected CoreXmlElement(string name) : this()
        {
            this.m_TagName = name;
        }
        internal protected CoreXmlElement()
        {
            this.m_properties = new Dictionary<string, CoreXmlAttributeValue>();
            this.m_attributes = new CoreXmlAttributeCollections(this);
            this.m_childs = new XmlChildsCollections(this);
        }
        public void ChangeTagName(string newTagName)
        {
            this.m_TagName = newTagName;
        }
        public object Content
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
        /// <summary>
        /// set the content in hierarchie chain
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public CoreXmlElement setContent(object value)
        {
            this.Content = value;
            return this;
        }
        /// <summary>
        /// get or set the attribute of this xml element
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public override CoreXmlAttributeValue this[string attribute]
        {
            get
            {
                if (this.m_attributes.Contains(attribute))
                    return this.m_attributes[attribute];
                return null;
            }
            set
            {
                if (value == null)
                {
                    if (this.m_attributes.Contains(attribute))
                        this.m_attributes.Remove(attribute);
                }
                else
                {
                    if (this.m_attributes.Contains(attribute))
                    {
                        if ((value != null) && (value.Value == null))
                        {
                            this.m_attributes.Remove(attribute);
                        }
                        else
                            this.m_attributes[attribute] = value;
                    }
                    else
                    {
                        this.m_attributes.Add(attribute, value);
                    }
                }
            }
        }
        /// <summary>
        /// represent a collection of CoreXmlAttributeValue 
        /// </summary>
        public class CoreXmlAttributeCollections : System.Collections.IEnumerable
        {
            private Dictionary<string, CoreXmlAttributeValue> m_lattribs;
            private CoreXmlElement element;
            public int Count { get { return this.m_lattribs.Count; } }
            public CoreXmlAttributeCollections(CoreXmlElement element)
            {
                this.element = element;
                this.m_lattribs = new Dictionary<string, CoreXmlAttributeValue>();
            }
            internal CoreXmlAttributeValue this[string key]
            {
                get
                {
                    if (this.m_lattribs.ContainsKey(key))
                        return this.m_lattribs[key];
                    return null;
                }
                set
                {
                    this.m_lattribs[key] = value;
                }
            }
            internal void Add(string attribute, string value)
            {
                this.m_lattribs.Add(attribute, value);
            }
            internal void Add(string attribute, CoreXmlAttributeValue value)
            {
                this.m_lattribs.Add(attribute, value);
            }
            internal void Remove(string attribute)
            {
                this.m_lattribs.Remove(attribute);
            }
            internal bool Contains(string attribute)
            {
                return this.m_lattribs.ContainsKey(attribute);
            }
            internal void Clear()
            {
                this.m_lattribs.Clear();
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_lattribs.GetEnumerator();
            }
        }
        public class XmlChildsCollections :
            IXmlChildCollections,
            System.Collections.IEnumerable
        {
            private List<CoreXmlElementBase> m_list;
            private CoreXmlElement m_element;
            public int Count { get { return this.m_list.Count; } }


            public override string ToString()
            {
                return "XmlChilds[" + this.Count + "]";
            }
            /// <summary>
            /// get the element
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public CoreXmlElementBase this[int index]
            {
                get
                {
                    if (this.m_list.IndexExists(index))
                        return this.m_list[index];
                    return null;
                }
            }
            public CoreXmlElement GetElementByAttribute(string attribname, string value)
            {
                foreach (CoreXmlElement l in this.m_list)
                {
                    if (l == null) continue;
                    CoreXmlAttributeValue r = l[attribname];
                    if (r != null)
                    {
                        if (r.GetValue() == value)
                            return l;
                    }
                }
                return null;
            }
            internal XmlChildsCollections(CoreXmlElement element)
            {
                this.m_list = new List<CoreXmlElementBase>();
                this.m_element = element;
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            public void Add(CoreXmlElementBase element)
            {
                if (this.m_element.CanAddChild && (element != null) && !this.m_list.Contains(element))
                {
                    this.m_list.Add(element);
                    element.Parent = this.m_element;
                    this.m_element.OnChildAdded(new CoreItemEventArgs<CoreXmlElementBase>(element));
                }
            }
            public void Remove(CoreXmlElementBase element)
            {
                if (this.m_list.Contains(element))
                {
                    this.m_list.Remove(element);
                    this.m_element.OnChildRemoved(new CoreItemEventArgs<CoreXmlElementBase>(element));
                    element.Parent = null;
                }
            }
            public void Clear()
            {
                if (this.m_list.Count > 0)
                {
                    var t = this.m_list.ToArray();
                    for (int i = 0; i < t.Length; i++)
                    {
                        this.Remove(t[i]);
                    }
                    this.m_list.Clear();
                    this.m_element.OnChildClear(EventArgs.Empty);
                }
            }
            public bool Contains(CoreXmlElementBase c)
            {
                return this.m_list.Contains(c);
            }

            public int IndexOf(CoreXmlElementBase element)
            {
                return this.m_list.IndexOf(element);
            }


            void IXmlChildCollections.Remove(CoreXmlElementBase xmlElement)
            {
                this.Remove(xmlElement);
            }


            public void Sort(Comparison<CoreXmlElementBase> callback)
            {
                this.m_list.Sort(callback);
            }
        }
        /// <summary>
        /// clear all
        /// </summary>
        public virtual void Clear()
        {
            this.m_attributes.Clear();
            this.m_childs.Clear();
            this.m_Content = null;
        }
        public virtual void ClearChilds()
        {
            this.m_childs.Clear();
        }
        public void ClearAttributes()
        {
            this.m_attributes.Clear();
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_childs.GetEnumerator();
        }
        public virtual string RenderInnerHTML(IXmlOptions option)
        {
            if (option == null)
            {
                option = this.CreateXmlOptions();
            }
            bool htmlContext = option.Context == "Html";
            bool v_indent = option.Indent;
            int v_dep = GetDetph(option);


            StringBuilder sb = new StringBuilder();

            if (this.Content != null)
            {
                sb.Append(CoreXmlUtility.GetStringValue(this.Content));
            }
            option.Depth++;
            string k = CoreXmlUtility.WriteElements(this, option);
            bool v_haschild = this.HasChild;
            if (!string.IsNullOrEmpty(k))
            {
                sb.Append(k);
                v_haschild = true;
            }
            //render this childs
            if (this.HasChild)
            {
                v_haschild = true;
                foreach (CoreXmlElementBase item in this)
                {
                    if (!item.Visible)
                        continue;
                    if (v_indent)
                        sb.AppendLine();
                    sb.Append(item.RenderXML(option));
                }
            }
            if (this.AllowRenderProperties)
            {
                if (this.Properties.Count > 0)
                {
                    bool t = !(sb.Length > 0);
                    foreach (var item in this.Properties)
                    {
                        CoreXmlElement d = CoreXmlElement.CreateXmlNode(item.Key);
                        d.Content = item.Value;
                        if (t && option.Indent)
                            sb.AppendLine();
                        if (t == false)
                        {
                            option.Depth--;
                            sb.Append(d.RenderXML(option));
                            option.Depth++;
                        }
                        else
                        {
                            sb.Append(d.RenderXML(option));
                        }
                        t = true;
                    }
                }
            }

            option.Depth--;
            if (v_indent)
            {
                if (v_haschild)
                {
                    sb.AppendLine();
                    if (option.Depth > 0)
                        WriteDepth(sb, option.Depth);
                }
            }
            return sb.ToString();
        }

        protected virtual IXmlOptions CreateXmlOptions()
        {

            return new CoreXmlSettingOptions()
            {
                Indent = true,
                Depth = 0,
                Context = "Html"
            };
        }
        protected virtual string RenderAttributes(IXmlOptions option)
        {
            StringBuilder sb = new StringBuilder();
            string v_c = null;
            if (this.HasAttributes)
            {
                foreach (KeyValuePair<string, CoreXmlAttributeValue> item in this.m_attributes)
                {
                    v_c = item.Value.GetValue();
                    if (v_c == "null")
                    {
                    }
                    if (!string.IsNullOrEmpty(v_c))
                    {
                        if ((option != null) && (option.Namespace.IsNamespacePrefix(item.Key))
                            )
                        {
                            if (option.Namespace.ContainsPrefix(item.Key))
                                continue;
                            else
                            {
                                option.Namespace.Add(this, item.Key, item.Value);
                            }
                        }
                        var q = CoreXmlUtility.TreatAttributeValue(v_c);
                        sb.Append(string.Format(" {0}=\"{1}\"", item.Key,
                            q));

                    }
                }
            }
            return sb.ToString();
        }

        public void RenderXmlTo(CoreXMLSerializer seri, IXmlOptions option)
        {
            option = option ??
                CreateXmlOptions();
            //task overflow
            //to avoid recursion
            var q = this;
            Stack<CoreXmlElement> bp = new Stack<CoreXmlElement>();
            bp.Push(q);
            CoreXmlElement p = null;
            //inline function
            void endWrite(CoreXmlElement sq)
            {
                if (sq.Content != null)
                {
                    CoreXMLSerializerUtility.WriteValue(seri, q);
                }
                seri.WriteEndElement();
            };
            while (bp.Count > 0)
            {
                q = bp.Pop();

                if ((p != null) && (q.Parent != p))
                {

                    endWrite(q);

                    p = q.Parent as CoreXmlElement;
                    continue;
                }
                var ns = q["xmlns"];
                seri.WriteStartElement(q.TagName, ns);
                q.WriteAttributes(seri);
                q.WriteElements(seri);
                if (q.Childs.Count > 0)
                {
                    bp.Push(q);
                    for (int i = q.Childs.Count - 1; i >= 0; i--)
                    {
                        bp.Push(q.Childs[i] as CoreXmlElement);
                    }
                    p = q;
                    continue;
                }
                //finish the node
                endWrite(q);

            }
        }

        
        /// <summary>
        /// render as string builder
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        public override string RenderXML(IXmlOptions option)
        {
            if (!this.Visible)
                return string.Empty;
            if (option == null)
            {
                option = CreateXmlOptions();
            }
            StringBuilder sb = new StringBuilder();
            bool htmlContext =  option.Context == "Html";
            bool v_indent =  option.Indent;
            bool v_haschild = this.HasChild;
            int v_dep = this.GetDetph(option);
            CoreXmlElementBase b  = this.Parent ;
            if (v_indent && (v_dep > 0))
            {
                WriteDepth(sb, v_dep);
            }
            sb.Append(string.Format("<{0}", this.TagName));
            sb.Append(this.RenderAttributes(option));            
            string v_s = this.RenderInnerHTML(option);
            //if(!string.IsNullOrEmpty (v_k)){
            //    StringBuilder ksb = new StringBuilder(v_s);
            //    if (v_indent)
            //        ksb.AppendLine();
            //    ksb.Append(v_k);
            //    v_s = ksb.ToString();
            //    v_haschild = true;
            //}
            v_haschild |= !string.IsNullOrEmpty(v_s);

            if ( v_haschild || (option.MustCloseTag(this.TagName)))
            {
                sb.Append(">");              
                sb.Append(v_s);               
                sb.Append(string.Format("</{0}>", this.TagName));
            }
            else{
                sb.Append(" />");              
            }        
           /*     if (v_haschild && (this.m_childs.Count == 1) && (this.m_childs[0] is CoreXmlElementText))
                {
                    CoreXmlElementText t = this.m_childs[0] as CoreXmlElementText;
                    sb.Append(t.Text);
                    sb.Append(string.Format("</{0}>", this.TagName));
                }
                else
                {
                    //if (v_indent)
                    //{
                    //    sb.AppendLine();
                    //   // this.writeDepth(sb, v_dep);
                    //}
                    //if (v_haschild)
                    //{
                    //    foreach (CoreXmlElementBase item in this)
                    //    {
                    //        if (item.Visible)
                    //        {
                    //            sb.Append(item.Render(option));
                    //        }
                    //    }
                    //    if (v_indent)
                    //    {
                    //        writeDepth(sb, v_dep);
                    //    }
                    //}
                    //else {
                        //if (v_indent)
                        //{
                        //    writeDepth(sb, v_dep);
                        //}
                   // }
                    //sb.Append(string.Format("</{0}>", this.TagName));
                }
            }
            else {
                sb.Append(string.Format(" />"));        
            }*/        
            if (option != null) { option.Namespace.Remove(this); }
            return sb.ToString();
        }
        /// <summary>
        /// get depth
        /// </summary>
        /// <returns></returns>
        private int GetDetph(IXmlOptions options)
        {
            if (options != null)
                return options.Depth;
            var q = this.Parent;
            int s = 0;
            while (q != null)
            {
                q = q.Parent;
                s++;
            }
            return s;
        }
        protected  void WriteDepth(StringBuilder sb, int v_dep)
        {
            for (int i = 0; i < v_dep; i++)
            {
                sb.Append("\t");
            }
        }
        public CoreXmlElement AddInput(string name, string type="text", string value="")
        {
            var c = this.Add("input", new Dictionary<string, string>() { 
                {"type",type},
                {"value", value}
            });
            c["name"] = c["id"] = name;
            return c;
        }
        public CoreXmlElement Add(string tagName, Dictionary<string, string> attributes)
        {
            var c = this.Add(tagName);
            if (c != null)
            {
                foreach (var item in attributes)
                {
                    c[item.Key] = item.Value;
                }
            }
            return c;
        }
        public CoreXmlElement Add(string tagName)
        {
            CoreXmlElement h = this.CreateChildNode(tagName);
            if (this.AddChild(h))
                return h;
            return null;
        }
        /// <summary>
        /// add child
        /// </summary>
        /// <param name="c"></param>
        public override bool  AddChild(CoreXmlElementBase c)
        {
            if (c == null)
                return false ;
            if (this.CanAddChild && !this.m_childs.Contains(c))
            {
                this.m_childs.Add(c);
                return true;
            }
            return false;
        }
        /// <summary>
        /// remove child
        /// </summary>
        /// <param name="c"></param>
        public void RemoveChild(CoreXmlElementBase c)
        {
            this.m_childs.Remove(c);
        }
        public override CoreXmlElementBase[] getElementsByTagName(string tagname)
        {
            if (string.IsNullOrEmpty (tagname ))
                return null;

            List<CoreXmlElementBase> m_element = new List<CoreXmlElementBase>();
            if (tagname == "*")
            {
                foreach (CoreXmlElementBase item in this.m_childs)
                {
                    m_element.Add (item);
                    m_element.AddRange(item.getElementsByTagName(tagname));
                }
                return m_element.ToArray();
            }

            CoreXmlElement c=null;
            string check = tagname .ToLower ();
            foreach (CoreXmlElementBase  item in this.m_childs)
            {
                c = item as CoreXmlElement;
                if (c == null) continue;
                if (c.TagName.ToLower() == check)
                {
                    m_element.Add(c);
                }
                CoreXmlElementBase[] cb = c.getElementsByTagName(check);
                if ((cb != null) && (cb.Length > 0)){
                    m_element.AddRange(cb);
                }
            }
            return m_element.ToArray ();
        }
        public override CoreXmlElementBase getElementById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            CoreXmlElement c = null;
            foreach (CoreXmlElementBase item in this.m_childs)
            {
                c = item as CoreXmlElement;
                if (c == null) continue;
                if (c["id"] == id)
                {
                    return c;
                }
                CoreXmlElementBase cb = c.getElementById(id);
                if (cb != null)
                {
                    return cb;
                }
            }
            return null;
        }

        /// <summary>
        /// get a configurable properties
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CoreXmlAttributeValue GetProperty(string name)
        {
            if (this.m_properties.ContainsKey(name))
                return this.m_properties[name];
             return null;
        }
        /// <summary>
        /// set a configurable properties.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetProperty(string name, object value)
        {
            if (this.m_properties.ContainsKey(name))
            {
                if (value == null) {
                    this.m_properties.Remove(name);
                }
                else 
                    this.m_properties[name].Value = value;
            }
            else
            {
                this.m_properties.Add(name, new CoreXmlAttributeValue() { Value = value });
            }
             
        }

        /// <summary>
        /// set attribute by preserving the chain
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public new CoreXmlElement SetAttribute(string name, object  value) {
            this[name] = value==null? null: value .ToString();
            return this;
        }
        
        public void ClearProperties() {
            this.m_properties.Clear();
        }

        public object GetPropertyValue(string name)
        {
            var attr = this.GetProperty(name);
            return attr != null ? attr.Value : null;            
        }
        public T GetPropertyValue<T>(string name) where T : class
        {
            var attr = this.GetProperty(name);
            return attr != null ? attr.Value as T: null;
        }

        IXmlChildCollections IXmlElement.Childs
        {
            get { return this.m_childs; }
        }

        

        /// <summary>
        /// create a child node
        /// </summary>
        /// <param name="tagName"></param>
        /// <returns></returns>
        public virtual CoreXmlElement CreateChildNode(string tagName)
        {
            return CoreXmlElement.CreateXmlNode(tagName);
        }
        /// <summary>
        /// override to get action on item clear
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnChildClear(EventArgs eventArgs)
        {
        }

        protected virtual void OnChildRemoved(CoreItemEventArgs<CoreXmlElementBase> item)
        {
            
        }
        protected virtual void OnChildAdded(CoreItemEventArgs<CoreXmlElementBase> item)
        {

        }

        public CoreXmlQuerySelector Select(string pattern)
        {
            return CoreXmlQuerySelector.Select(this, pattern);            
        }
    }
}

