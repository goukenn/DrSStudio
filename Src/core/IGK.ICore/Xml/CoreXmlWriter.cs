

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlWriter.cs
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
file:XmlWriter.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.ICore.Xml
{
    using System.Reflection;
    using System.Text.RegularExpressions;
    using drsXML = IGK.ICore.Xml;



    /// <summary>
    /// represent the default xml  writer mecanism to serialize element ;
    /// </summary>
    public class CoreXmlWriter : CoreXmlSettingOptions
{
        private StringBuilder m_sb;
        private System.Xml.XmlWriterSettings setting;
        private CoreXmlElementBase m_cnode;
        private CoreXmlElementBase m_crootNode;
        private IGK.ICore.Xml.CoreXmlDeclaration m_declaration;
        /// <summary>
        /// get the root node
        /// </summary>
        public CoreXmlElementBase RootNode { get { return this.m_crootNode; } }
        /// <summary>
        /// get the current node
        /// </summary>
        public CoreXmlElementBase CurrentNode { get { return this.m_cnode; } }
        /// <summary>
        /// get the setting
        /// </summary>
        public XmlWriterSettings Settings { get { return this.setting; } }
        /// <summary>
        /// get the string builder. used internally
        /// </summary>
        public StringBuilder StringBuilder { get { return this.m_sb;  } }
        static Dictionary<string, Type> sm_writerTypes;
        internal protected  void AddNode(CoreXmlDeclaration declaration)
        {
            this.m_declaration = declaration;
        }
        /// <summary>
        /// add name space
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="value"></param>
        public void AddNameSpace(string prefix, string value)
        {
            if (this.RootNode !=null)
                this.RootNode["xmlns:" + prefix] = value;
        }
        static CoreXmlWriter()
        {
            //load writer
            sm_writerTypes = new Dictionary<string, Type>();
            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies ())
            {
                SearchForWriter(asm);
            }
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }
        private static void SearchForWriter(System.Reflection.Assembly asm)
        {
            string v_name = string.Empty;
            foreach (Type item in asm.GetTypes ())
            {
                if (item.IsSubclassOf(
                    MethodInfo.GetCurrentMethod().DeclaringType) &&
                    !item.IsAbstract)
                {
                    try
                    {
                        v_name = new Regex("(?<name>(.)+)Writer").Matches(item.Name)[0].Groups["name"].Value;
                        sm_writerTypes.Add(v_name.ToUpper(), item);
                    }
                    catch { 
                    }
                }
            }
        }
        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            SearchForWriter(args.LoadedAssembly);
        }
        /// <summary>
        /// Create a writer for saving data
        /// </summary>
        /// <param name="name">Name of the formatter</param>
        /// <param name="sb">string builder </param>
        /// <param name="setting">setting</param>
        /// <returns></returns>
        public static CoreXmlWriter CreateWriter(string name, StringBuilder sb, XmlWriterSettings setting)
        {
            if (string.IsNullOrEmpty(name) || !(sm_writerTypes.ContainsKey(name.ToUpper())))
                return null;
            Type t = sm_writerTypes[name.ToUpper()];
            MethodInfo v_minfo = t.GetMethod ("Create", System.Reflection.BindingFlags .Static |  System.Reflection.BindingFlags.Public
                );
            if (v_minfo !=null)
            return v_minfo.Invoke (null, new object[]{
                sb,
                setting 
            }) as CoreXmlWriter;
            return null;
        }
        public void WriteDocType(string doc)
        {
            m_sb.Append(doc);
            if (this.Indent)
                m_sb.Append("\n");
        }
        internal protected CoreXmlWriter(StringBuilder sb, System.Xml.XmlWriterSettings setting)
        {
            this.m_sb = sb;
            this.setting = setting;
            if (this.setting.OmitXmlDeclaration == false)
            {
                this.m_declaration = new CoreXmlDeclaration();
                this.m_declaration["version"] = "1.0";
                this.m_declaration["encoding"] = "utf-8";
            }
        }
        public static CoreXmlWriter Create(StringBuilder sb, System.Xml.XmlWriterSettings setting)
        {
            return new CoreXmlWriter(sb, setting);
        }
        public void Flush()
        {
            if (this.m_declaration != null)
                this.m_sb.Append(m_declaration.RenderXML(this));
            if (m_crootNode != null)
            {

                this.m_sb.Append(m_crootNode.RenderXML(this));
            }
        }
        public void WriteStartElement(string name)
        {
            CoreXmlElementBase c = drsXML.CoreXmlElement.CreateXmlNode(name);
            if (m_crootNode == null)
                m_crootNode = c;
            if (m_cnode == null)
                m_cnode = c;
            else
            {
                if (m_cnode.AddChild(c))
                {
                    m_cnode = c;
                }
            }
        }
        public void WriteEndElement()
        {
            if (this.m_cnode != null)
            {
                this.m_cnode = this.m_cnode.Parent;
            }
        }
        public void WriteComment(string comment)
        {
            if (this.m_cnode is drsXML.CoreXmlElement)
                (this.m_cnode as drsXML.CoreXmlElement).AddChild(drsXML.CoreXmlElement.CreateComment(comment));
            else
            {
                this.m_sb.Append(drsXML.CoreXmlElement.CreateComment(comment).RenderXML(this));
                if (this.Indent)
                    this.m_sb.AppendLine();
            }
        }
      
        public override bool Indent
        {
            get { return this.setting.Indent; }
            set { this.setting.Indent = value; }
        }
        public override  bool MustCloseTag(string tagName)
        {
            return true ;
        }
        public void WriteAttributeString(string p1, string p2)
        {
            if (this.m_cnode != null)
            {
                this.m_cnode[p1] = p2;
            }
        }
        public void WriteAttributeString(string p1, object  p2)
        {
            if ((this.m_cnode != null) && (p2 !=null))
            {
                this.m_cnode[p1] = p2.ToString();
            }
        }
        public virtual void WriteElementString(string name, string value)
        {
            if ((this.m_cnode != null) && (name != null))
            {
                this.WriteStartElement(name);
                this.AddNode(new IGK.ICore.Xml.CoreXmlElementText() { Text = value });
                this.WriteEndElement();                
            }
        }
        /// <summary>
        /// add node to element
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(CoreXmlElementBase node)
        {
            if (node == null)
                return;
            if ((this.m_cnode != null) && this.m_cnode.CanAddChild )
                this.m_cnode.AddChild(node);
            else if (this.m_crootNode != null)
                this.m_crootNode = node;
            else
            {
                this.m_crootNode = node;
                this.m_cnode = node;
            }
        }
        public void WriteString(string p)
        {
            if (m_cnode == null)
            {
                this.m_sb.Append(p);
                if (this.Indent)
                    this.m_sb.AppendLine();
            }
            else
            {
                var v_tx = new drsXML.CoreXmlElementText();
                v_tx.Text = p;
                this.m_cnode.AddChild(v_tx);
            }
        }
        public void WriteCData(object p)
        { 
            if ((p == null) ||(m_cnode == null)) return;
            CoreXmlCData d = new CoreXmlCData();
            d.Value = p.ToString();
            this.m_cnode.AddChild(d);
        }


     

}
}

