

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXWriter.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXWriter.cs
*/
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a wix writer object
    /// </summary>
    public class WiXWriter : IGK.ICore.Xml.CoreXmlWriter
    {
        private IGK.ICore.Xml.CoreXmlElement  m_xmlNode;
        public string GetNameSpacePrefix(string uri)
        {
            string prefix = string.Empty;
            foreach (KeyValuePair<string, CoreXmlAttributeValue> item in m_xmlNode.Attributes)
            {
                if (item.Key.StartsWith("xmlns") && (item.Value.GetValue() == uri))
                {
                        prefix = item.Key.Split(':')[1];
                        break ;
                }
            }
            return prefix;

        }
        public override bool MustCloseTag(string tagName)
        {
            if (tagName.ToLower () == "wix")
                return true;
            return false;
        }
        private WiXWriter(StringBuilder sb , XmlWriterSettings setting):base(sb, setting)
        { 
        }
        public new static WiXWriter Create(StringBuilder sb, XmlWriterSettings setting)
        {
            WiXWriter v_c = new WiXWriter (sb, setting);
            if (!setting.OmitXmlDeclaration)
            {
                IGK.ICore.Xml.CoreXmlDeclaration v_p = new IGK.ICore.Xml.CoreXmlDeclaration();
                v_p["version"] = "1.0";
                v_c.AddNode(v_p);
            }
            CoreXmlElementBase v_b =  CoreXmlElementBase.CreateXmlNode ("Wix");
            //save the wix definition
            v_b["xmlns"] = WiXConstant.WIX_URI;
            v_b["xmlns:netfx"] = WiXConstant.UTIL_NETFX_URI;
            v_b["xmlns:util"] = WiXConstant.UTIL_EXTENSION_URI;
            v_c.m_xmlNode = v_b as IGK.ICore.Xml.CoreXmlElement ;
            v_c.AddNode(v_b);
            return v_c;
        }
        public static WiXWriter Create(StringBuilder sb)
        {
            XmlWriterSettings setting = new XmlWriterSettings ();
            setting.OmitXmlDeclaration = false;
            setting.Indent = true;
            return Create(sb, setting);
        }
        bool Accept(object obj)
        { 
            if (obj == null)return false ;
            MethodInfo v_m = this.GetType().GetMethod ("Visit",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public ,
                null,
               new Type[]{obj.GetType()}, null
                   );
            return v_m !=null;
        }
        /// <summary>
        /// visit object
        /// </summary>
        /// <param name="obj"></param>
        public void Visit(object obj)
        {
            MethodInfo v_m = this.GetType().GetMethod("Visit",
              BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public,
              null,
             new Type[] { obj.GetType() }, null
                 );
            if ((v_m != null) && (!v_m.MethodHandle.Equals(MethodInfo.GetCurrentMethod().MethodHandle)))
            {
                v_m.Invoke(this, new object[] { obj });
            }
            else{
                CoreLog.WriteDebug("Can't visit " + obj);
            }
        }
        public void Visit(IEnumerable features)
        {
            foreach (var item in features)
            {
                Visit((object)item);
            }
        }
        public void Visit(WiXDocument.WiXDocumentFeatureCollections features)
        {
            foreach (WiXFeature item in features )
            {
                Visit((object)item);
            }
        }
        public void Visit(WiXStringElement element)
        {
            this.WriteString(element.Value);
        }
        public void Visit(WiXFeature feature)
        {
            this.WriteStartElement(WiXDisplayNameAttribte.GetName(feature.GetType(), this));
            WriteInfo(feature);
            this.WriteEndElement();
        }
        public void Visit(WiXEntry dir)
        {
            this.WriteStartElement(WiXDisplayNameAttribte.GetName(dir.GetType(), this));
            WriteInfo(dir);
            this.WriteEndElement();
        }
        public void Visit(WiXShortCut shortcut) {
            this.WriteStartElement(WiXDisplayNameAttribte.GetName(shortcut.GetType(), this));
            WriteInfo(shortcut);
            this.WriteEndElement();
        }
        public void Visit(WiXFileEntry  dir)
        {
            this.WriteStartElement("File");
            WriteInfo(dir);
            this.WriteEndElement();
        }
        public void Visit(WiXDirectory dir)
        {
            this.WriteStartElement(WiXDisplayNameAttribte.GetName(dir.GetType(),this));
            WriteInfo(dir);
            this.WriteEndElement();
        }
        public void Visit(WiXFeatureEntry dir)
        {
            this.WriteStartElement("Feature");
            WriteInfo(dir);
            this.WriteEndElement();
        }
        public void Visit(WiXPackage  dir)
        {
            this.WriteStartElement("Package");
            WriteInfo(dir);
            this.WriteEndElement();
        }
        /// <summary>
        /// visit wix document
        /// </summary>
        /// <param name="v_document"></param>
        internal void Visit(params WiXDocument[] v_document)
        {
            foreach (var item in v_document)
            {
                this.WriteStartElement("Product");
                this.WriteInfo(item);
                this.WriteEndElement();
            }
        }
        private void WriteInfo(object dir)
        {
            PropertyInfo[] t = dir.GetType().GetProperties();
            List<PropertyInfo> v_elements = new List<PropertyInfo>();
            foreach (PropertyInfo prInfo in dir.GetType().GetProperties())
            {
                if (WiXAttributeAttribute.IsWixAttribute(prInfo))
                {
                    this.WriteAttributeString(WiXDisplayNameAttribte.GetName(prInfo), WiXUtils.GetStringValue(prInfo.GetValue(dir)));
                }
                else if (WiXCDATAValueAttribute.IsWixCDATAValueAttribute (prInfo ))
                {
                    this.WriteCData(prInfo.GetValue(dir));
                }
                else if (WiXElementAttribute.IsWixElement(prInfo))
                {
                    v_elements.Add(prInfo);
                }
            }
            object obj = null;
            foreach (PropertyInfo prInfo in v_elements)
            {
                obj = prInfo.GetValue(dir);
                if (this.Accept(obj))
                    this.Visit(obj);
            }
        }
    }
}

