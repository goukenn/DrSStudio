

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLDeserializer.cs
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
file:CoreXMLDeserializer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
namespace IGK.ICore.Codec
{
    using IGK.ICore;using IGK.ICore.Resources;
    using IGK.ICore.ComponentModel;
    /// <summary>
    /// represent a deserializer object
    /// </summary>
    public sealed class CoreXMLDeserializer : 
        IXMLDeserializer 
    {
        private XmlReader m_xreader;
        private string m_BaseDir;
        private ICoreLoadingContext m_context;

        /// <summary>
        /// get the source parent contenxt
        /// </summary>
        public IXMLDeserializer Parent
        {
            get {
                if (this.m_context !=null)
                    return this.m_context.Source;
                return null;
            }
        }
        
        public string BaseDir
        {
            get
            {
                return m_BaseDir;

            }
        }
        public XmlReader XmlReader
        {
            get { return this.m_xreader; }

        }
        /// <summary>
        /// .ctr 
        /// </summary>
        private CoreXMLDeserializer()
        {
        }
        /// <summary>
        /// create a string builder from string
        /// </summary>
        /// <param name="sbuilder"></param>
        /// <returns></returns>
        public static CoreXMLDeserializer Create(StringBuilder sbuilder)
        { 
            TextReader txr = new StringReader(sbuilder.ToString());
            System.Xml.XmlReaderSettings v_setting = new XmlReaderSettings();
            v_setting.IgnoreComments = true;
            v_setting.ConformanceLevel = ConformanceLevel.Fragment;
            XmlReader reader = XmlReader.Create(txr, v_setting);
            return Create(reader);
        }
        public static CoreXMLDeserializer Create(XmlReader reader)
        {
            return Create (reader, null);
        }
        public static CoreXMLDeserializer Create(XmlReader reader, string BaseDir)
        {
            CoreXMLDeserializer v_s = new CoreXMLDeserializer();
            v_s.m_xreader = reader;
            v_s.m_BaseDir = BaseDir ;
            return v_s;
        }
        public System.Int32 AttributeCount
        {
            get { return m_xreader.AttributeCount; }
        }
        public System.String BaseURI
        {
            get { return m_xreader.BaseURI; }
        }
        public System.Boolean CanReadBinaryContent
        {
            get { return m_xreader.CanReadBinaryContent; }
        }
        public System.Boolean CanReadValueChunk
        {
            get { return m_xreader.CanReadValueChunk; }
        }
        public System.Boolean CanResolveEntity
        {
            get { return m_xreader.CanResolveEntity; }
        }
        public System.Int32 Depth
        {
            get { return m_xreader.Depth; }
        }
        public System.Boolean EOF
        {
            get { return m_xreader.EOF; }
        }
        public System.Boolean HasAttributes
        {
            get { return m_xreader.HasAttributes; }
        }
        public System.Boolean HasValue
        {
            get { return m_xreader.HasValue; }
        }
        public System.Boolean IsDefault
        {
            get { return m_xreader.IsDefault; }
        }
        public System.Boolean IsEmptyElement
        {
            get { return m_xreader.IsEmptyElement; }
        }
        public System.String LocalName
        {
            get { return m_xreader.LocalName; }
        }
        public System.String Name
        {
            get { return m_xreader.Name; }
        }
        public System.String NamespaceURI
        {
            get { return m_xreader.NamespaceURI; }
        }
        public System.Xml.XmlNameTable NameTable
        {
            get { return m_xreader.NameTable; }
        }
        public System.Xml.XmlNodeType NodeType
        {
            get { return m_xreader.NodeType; }
        }
        public System.String Prefix
        {
            get { return m_xreader.Prefix; }
        }
        public System.Char QuoteChar
        {
            get { return m_xreader.QuoteChar; }
        }
        public System.Xml.ReadState ReadState
        {
            get { return m_xreader.ReadState; }
        }
        public System.Xml.Schema.IXmlSchemaInfo SchemaInfo
        {
            get { return m_xreader.SchemaInfo; }
        }
        public System.Xml.XmlReaderSettings Settings
        {
            get { return m_xreader.Settings; }
        }
        public System.String Value
        {
            get { return m_xreader.Value; }
        }
        public System.Type ValueType
        {
            get { return m_xreader.ValueType; }
        }
        public System.String XmlLang
        {
            get { return m_xreader.XmlLang; }
        }
        public System.Xml.XmlSpace XmlSpace
        {
            get { return m_xreader.XmlSpace; }
        }
        public System.String this[System.Int32 i]
        {
            get { return m_xreader[i]; }
        }
        public System.String this[System.String name]
        {
            get { return m_xreader[name]; }
        }
        public System.String this[System.String name, System.String namespaceURI]
        {
            get { return m_xreader[name, namespaceURI]; }
        }
        public void ReadEndElement(
        )
        {
            m_xreader.ReadEndElement();
        }
        public System.String ReadInnerXml(
        )
        {
            return m_xreader.ReadInnerXml();
        }
        public System.String ReadOuterXml(
        )
        {
            return m_xreader.ReadOuterXml();
        }
        public void ReadStartElement(
        )
        {
            m_xreader.ReadStartElement();
        }
        public void ReadStartElement(
        System.String name)
        {
            m_xreader.ReadStartElement(name);
        }
        public void ReadStartElement(
        System.String localname, System.String ns)
        {
            m_xreader.ReadStartElement(localname, ns);
        }
        public System.String ReadString(
        )
        {
            return m_xreader.ReadString();
        }
        public IXMLDeserializer ReadSubtree(
        )
        {
            CoreXMLDeserializer d = CoreXMLDeserializer.Create(m_xreader.ReadSubtree());
            d.CopyDefinition(this);
            return d;
        }
        private void CopyDefinition(CoreXMLDeserializer coreXmlDeserializer)
        {
            this.m_BaseDir = coreXmlDeserializer.m_BaseDir;
            this.m_context = coreXmlDeserializer.m_context ;
        }
        public System.Boolean ReadToDescendant(
        System.String name)
        {
            bool v = false;
            try
            {
                v = m_xreader.ReadToDescendant(name);
            }
            catch(Exception ex) {
                v = false;
                CoreLog.WriteLine(ex.Message);
            }
            return v;
        }
        public System.Boolean ReadToDescendant(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadToDescendant(localName, namespaceURI);
        }
        public System.Boolean ReadToFollowing(
        System.String name)
        {
            return m_xreader.ReadToFollowing(name);
        }
        public System.Boolean ReadToFollowing(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadToFollowing(localName, namespaceURI);
        }
        public System.Boolean ReadToNextSibling(
        System.String name)
        {
            return m_xreader.ReadToNextSibling(name);
        }
        public System.Boolean ReadToNextSibling(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadToNextSibling(localName, namespaceURI);
        }
        public System.Int32 ReadValueChunk(
        System.Char[] buffer, System.Int32 index, System.Int32 count)
        {
            return m_xreader.ReadValueChunk(buffer, index, count);
        }
        public void ResolveEntity(
        )
        {
            m_xreader.ResolveEntity();
        }
        public void Skip(
        )
        {
            m_xreader.Skip();
        }
        public System.Int64 ReadContentAsLong(
        )
        {
            return m_xreader.ReadContentAsLong();
        }
        public System.Object ReadContentAsObject(
        )
        {
            return m_xreader.ReadContentAsObject();
        }
        public System.String ReadContentAsString(
        )
        {
            return m_xreader.ReadContentAsString();
        }
        public System.Object ReadElementContentAs(
        System.Type returnType, System.Xml.IXmlNamespaceResolver namespaceResolver)
        {
            return m_xreader.ReadElementContentAs(returnType, namespaceResolver);
        }
        public System.Object ReadElementContentAs(
        System.Type returnType, System.Xml.IXmlNamespaceResolver namespaceResolver, System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
        }
        public System.Int32 ReadElementContentAsBase64(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            return m_xreader.ReadElementContentAsBase64(buffer, index, count);
        }
        public System.Int32 ReadElementContentAsBinHex(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            return m_xreader.ReadElementContentAsBinHex(buffer, index, count);
        }
        public System.Boolean ReadElementContentAsBoolean(
        )
        {
            return m_xreader.ReadElementContentAsBoolean();
        }
        public System.Boolean ReadElementContentAsBoolean(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsBoolean(localName, namespaceURI);
        }
        public System.DateTime ReadElementContentAsDateTime(
        )
        {
            return m_xreader.ReadElementContentAsDateTime();
        }
        public System.DateTime ReadElementContentAsDateTime(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsDateTime(localName, namespaceURI);
        }
        public System.Decimal ReadElementContentAsDecimal(
        )
        {
            return m_xreader.ReadElementContentAsDecimal();
        }
        public System.Decimal ReadElementContentAsDecimal(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsDecimal(localName, namespaceURI);
        }
        public System.Double ReadElementContentAsDouble(
        )
        {
            return m_xreader.ReadElementContentAsDouble();
        }
        public System.Double ReadElementContentAsDouble(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsDouble(localName, namespaceURI);
        }
        public System.Single ReadElementContentAsFloat(
        )
        {
            return m_xreader.ReadElementContentAsFloat();
        }
        public System.Single ReadElementContentAsFloat(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsFloat(localName, namespaceURI);
        }
        public System.Int32 ReadElementContentAsInt(
        )
        {
            return m_xreader.ReadElementContentAsInt();
        }
        public System.Int32 ReadElementContentAsInt(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsInt(localName, namespaceURI);
        }
        public System.Int64 ReadElementContentAsLong(
        )
        {
            return m_xreader.ReadElementContentAsLong();
        }
        public System.Int64 ReadElementContentAsLong(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsLong(localName, namespaceURI);
        }
        public System.Object ReadElementContentAsObject(
        )
        {
            return m_xreader.ReadElementContentAsObject();
        }
        public System.Object ReadElementContentAsObject(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsObject(localName, namespaceURI);
        }
        public System.String ReadElementContentAsString(
        )
        {
            return m_xreader.ReadElementContentAsString();
        }
        public System.String ReadElementContentAsString(
        System.String localName, System.String namespaceURI)
        {
            return m_xreader.ReadElementContentAsString(localName, namespaceURI);
        }
        public System.String ReadElementString(
        )
        {
            return m_xreader.ReadElementString();
        }
        public System.String ReadElementString(
        System.String name)
        {
            return m_xreader.ReadElementString(name);
        }
        public System.String ReadElementString(
        System.String localname, System.String ns)
        {
            return m_xreader.ReadElementString(localname, ns);
        }
        public void Close(
        )
        {
            m_xreader.Close();
        }
        public System.String GetAttribute(
        System.Int32 i)
        {
            return m_xreader.GetAttribute(i);
        }
        public System.String GetAttribute(
        System.String name)
        {
            return m_xreader.GetAttribute(name);
        }
        public System.String GetAttribute(
        System.String name, System.String namespaceURI)
        {
            return m_xreader.GetAttribute(name, namespaceURI);
        }
        public System.Boolean IsName(
        System.String str)
        {
            return XmlReader.IsName(str);
        }
        public System.Boolean IsNameToken(
        System.String str)
        {
            return XmlReader.IsNameToken(str);
        }
        public System.Boolean IsStartElement(
        )
        {
            return m_xreader.IsStartElement();
        }
        public System.Boolean IsStartElement(
        System.String name)
        {
            return m_xreader.IsStartElement(name);
        }
        public System.Boolean IsStartElement(
        System.String localname, System.String ns)
        {
            return m_xreader.IsStartElement(localname, ns);
        }
        public System.String LookupNamespace(
        System.String prefix)
        {
            return m_xreader.LookupNamespace(prefix);
        }
        public void MoveToAttribute(
        System.Int32 i)
        {
            m_xreader.MoveToAttribute(i);
        }
        public System.Boolean MoveToAttribute(
        System.String name)
        {
            return m_xreader.MoveToAttribute(name);
        }
        public System.Boolean MoveToAttribute(
        System.String name, System.String ns)
        {
            return m_xreader.MoveToAttribute(name, ns);
        }
        public System.Xml.XmlNodeType MoveToContent()
        {
            return m_xreader.MoveToContent();
        }
        public System.Boolean MoveToElement(
        )
        {
            return m_xreader.MoveToElement();
        }
        public System.Boolean MoveToFirstAttribute(
        )
        {
            return m_xreader.MoveToFirstAttribute();
        }
        public System.Boolean MoveToNextAttribute(  )
        {
            return m_xreader.MoveToNextAttribute();
        }
        public System.Boolean Read( )
        {
            return m_xreader.Read();
        }
        public System.Boolean ReadAttributeValue(
        )
        {
            return m_xreader.ReadAttributeValue();
        }
        public System.Object ReadContentAs(
        System.Type returnType, System.Xml.IXmlNamespaceResolver namespaceResolver)
        {
            return m_xreader.ReadContentAs(returnType, namespaceResolver);
        }
        public System.Int32 ReadContentAsBase64(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            return m_xreader.ReadContentAsBase64(buffer, index, count);
        }
        public System.Int32 ReadContentAsBinHex(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            return m_xreader.ReadContentAsBinHex(buffer, index, count);
        }
        public System.Boolean ReadContentAsBoolean(
        )
        {
            return m_xreader.ReadContentAsBoolean();
        }
        public System.DateTime ReadContentAsDateTime(
        )
        {
            return m_xreader.ReadContentAsDateTime();
        }
        public System.Decimal ReadContentAsDecimal(
        )
        {
            return m_xreader.ReadContentAsDecimal();
        }
        public System.Double ReadContentAsDouble(
        )
        {
            return m_xreader.ReadContentAsDouble();
        }
        public System.Single ReadContentAsFloat(
        )
        {
            return m_xreader.ReadContentAsFloat();
        }
        public System.Int32 ReadContentAsInt(
        )
        {
            return m_xreader.ReadContentAsInt();
        }
        #region ICoreDisposableObject Members
        public bool IsDisposed
        {
            get {
                return (this.m_xreader.ReadState != ReadState.Error);
            }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            this.Close();
        }
        #endregion
        public static IXMLDeserializer Create(Stream mem)
        {
            XmlReaderSettings v_setting = new XmlReaderSettings();
            v_setting.CloseInput = false;
            XmlReader xreader = XmlReader.Create(mem, v_setting);
            return Create(xreader);
        }
        public ICoreWorkingObject CreateWorkingObject(string name)
        {
            ICoreWorkingObject obj = CoreSystem.CreateWorkingObject(name);
            return obj;
        }
        public void ReadToEndElement()
        {
            if (!this.IsEmptyElement)
            {
                int depth = 0;
                while (this.Read())
                {
                    if (this.NodeType == XmlNodeType.EndElement)
                    {
                        if (depth <= 0)
                            break;
                        depth--;
                    }
                    if (this.NodeType == XmlNodeType.Element)
                    {
                        if (!this.IsEmptyElement)
                            depth++;
                    }
                }
            }
        }
        
        /// <summary>
        /// register context
        /// </summary>
        /// <param name="context"></param>
        public  void RegisterLoadingEvent(ICoreLoadingContext context)
        {
            this.m_context = context ;
        }
        public event CoreLoadingCompleteEventHandler LoadingComplete
        {
            add {
                if (this.m_context != null)
                {
                    this.m_context.LoadingComplete += value;
                }
            }
            remove { 
                if (this.m_context !=null)
                    this.m_context.LoadingComplete -= value; }
        }


        public ICoreWorkingObject GetElementByTagName(string name)
        {
            if (this.m_context != null)
            {
                return this.m_context.GetElementByTagName(name);
            }
            return null ;
        }


        public T GetAttribute<T>(string p, T defalutValue=default(T))
        {
            try
            {
                var s = this.GetAttribute(p);
                if (s != null)
                {
                    var conv = CoreTypeDescriptor.GetConverter(typeof(T));
                    return (T)conv.ConvertFromString(s);
                }
            }
            catch {

            }
            return defalutValue;
        }
    }
}

