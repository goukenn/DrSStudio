

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGWriter.cs
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
file:SVGWriter.cs
*/

using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using drsXML = IGK.ICore.Xml;
namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// represent a svg writer
    /// </summary>
    public class SVGWriter : CoreXmlSettingOptions
    {
       
        private StringBuilder sb;
        private System.Xml.XmlWriterSettings setting;
        private drsXML.CoreXmlElementBase m_cnode;
        private drsXML.CoreXmlElementBase m_crootNode;
        static List<string> m_mustCloseTag = new List<string>();
        public XmlWriterSettings Settings { get { return this.setting; } }
        static SVGWriter()
        {
            m_mustCloseTag.Add("svg");
        }
        public void WriteDocType(string doc)
        {
            sb.Append(doc);
            if (this.Indent)
                sb.Append("\n");
        }
        internal SVGWriter(StringBuilder sb, System.Xml.XmlWriterSettings setting)
        {
            this.sb = sb;
            this.setting = setting;
        }
        public static SVGWriter Create(StringBuilder sb, System.Xml.XmlWriterSettings setting)
        {
            return new SVGWriter(sb, setting);
        }
        public void Flush()
        {
            if (m_crootNode != null)
            {
                this.sb.Append(m_crootNode.RenderXML(this));
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
                this.sb.Append(drsXML.CoreXmlElement.CreateComment(comment).RenderXML(this));
                if (this.Indent)
                    this.sb.AppendLine();
            }
        }
        public override bool Indent
        {
            get { return  this.setting.Indent; }
            set {
                this.setting.Indent = value;
            }
        }

        public string OutputFolder { get; internal set; }

        public override bool MustCloseTag(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return false;
            return m_mustCloseTag.Contains(tagName.ToLower());
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
        /// <summary>
        /// add node to element
        /// </summary>
        /// <param name="node"></param>
        public void AddNode(CoreXmlElementBase node)
        {
            if (node == null)
                return;
            if (this.m_cnode != null)
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
                this.sb.Append(p);
                if (this.Indent)
                    this.sb.AppendLine();
            }
            else
            {
                var v_tx = new drsXML.CoreXmlElementText();
                v_tx.Text = p;
                this.m_cnode.AddChild(v_tx);
            }
        }

        internal string GetOutput()
        {
            return this.sb.ToString();
        }
    }
}

