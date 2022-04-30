

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXMLSerializer.cs
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
file:CoreXMLSerializer.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace IGK.ICore.Codec
{
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using System.IO;

    public sealed class CoreXMLSerializer : IXMLSerializer 
    {
        private global::System.Xml.XmlWriter m_xwriter;
        private CoreResourceSerializer m_resources;
        private string m_BaseDir;
        private string m_ErrorMessage;
        private bool m_Error;
        /// <summary>
        /// get or set the error
        /// </summary>
        public bool Error
        {
            get { return m_Error; }
            set
            {
                if (m_Error != value)
                {
                    m_Error = value;
                }
            }
        }
        /// <summary>
        /// get or set the error message
        /// </summary>
        public string ErrorMessage
        {
            get { return m_ErrorMessage; }
            set
            {
                if (m_ErrorMessage != value)
                {
                    m_ErrorMessage = value;
                }
            }
        }

        public string BaseDir
        {
            get { return m_BaseDir; }
        }
        public XmlWriter XmlWriter { get { return this.m_xwriter; } }
        /// <summary>
        /// represent the serializer resources info
        /// </summary>
        public ICoreResourceSerializer Resources { get { return m_resources; } }

        private bool m_EmbedBitmap;
        /// <summary>
        /// get or set if the bitmap data must be embed inline
        /// </summary>
        public bool EmbedBitmap
        {
            get { return m_EmbedBitmap; }
            set
            {
                if (m_EmbedBitmap != value)
                {
                    m_EmbedBitmap = value;
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        private CoreXMLSerializer() 
        {
           this.m_BaseDir = CoreConstant.TEMP_FOLDER; // PathUtils.GetPath("%startup%/temp");
           this.m_resources = new CoreResourceSerializer(this);
           this.m_EmbedBitmap = true;
        }
        public static CoreXMLSerializer Create(XmlWriter writer)  {
            return Create(writer, string.Empty );
        }
        public static CoreXMLSerializer Create(XmlWriter xwriter, string baseDir)
        {
            CoreXMLSerializer xmwriter = new CoreXMLSerializer();
            xmwriter.m_xwriter = xwriter;
            xmwriter.m_BaseDir = baseDir;
            return xmwriter;
        }
        public System.Xml.XmlWriterSettings Settings
        {
            get { return m_xwriter.Settings; }
        }
        public System.Xml.WriteState WriteState
        {
            get { return m_xwriter.WriteState; }
        }
        public System.String XmlLang
        {
            get { return m_xwriter.XmlLang; }
        }
        public System.Xml.XmlSpace XmlSpace
        {
            get { return m_xwriter.XmlSpace; }
        }
        public void WriteNode(
        System.Xml.XmlReader reader, System.Boolean defattr)
        {
            m_xwriter.WriteNode(reader, defattr);
        }
        public void WriteNode(
        System.Xml.XPath.XPathNavigator navigator, System.Boolean defattr)
        {
            m_xwriter.WriteNode(navigator, defattr);
        }
        public void WriteProcessingInstruction(
        System.String name, System.String text)
        {
            m_xwriter.WriteProcessingInstruction(name, text);
        }
        public void WriteQualifiedName(
        System.String localName, System.String ns)
        {
            m_xwriter.WriteQualifiedName(localName, ns);
        }
        public void WriteRaw(
        System.String data)
        {
            m_xwriter.WriteRaw(data);
        }
        public void WriteRaw(
        System.Char[] buffer, System.Int32 index, System.Int32 count)
        {
            m_xwriter.WriteRaw(buffer, index, count);
        }
        public void WriteStartAttribute(
        System.String localName)
        {
            m_xwriter.WriteStartAttribute(localName);
        }
        public void WriteStartAttribute(
        System.String localName, System.String ns)
        {
            m_xwriter.WriteStartAttribute(localName, ns);
        }
        public void WriteStartAttribute(
        System.String prefix, System.String localName, System.String ns)
        {
            m_xwriter.WriteStartAttribute(prefix, localName, ns);
        }
        public void WriteStartDocument(
        )
        {
            m_xwriter.WriteStartDocument();
        }
        public void WriteStartDocument(
        System.Boolean standalone)
        {
            m_xwriter.WriteStartDocument(standalone);
        }
        public void WriteStartElement(
        System.String localName)
        {
            m_xwriter.WriteStartElement(localName);
        }
        public void WriteStartElement(
        System.String localName, System.String ns)
        {
            m_xwriter.WriteStartElement(localName, ns);
        }
        public void WriteStartElement(
        System.String prefix, System.String localName, System.String ns)
        {
            m_xwriter.WriteStartElement(prefix, localName, ns);
        }
        public void WriteString(
        System.String text)
        {
            m_xwriter.WriteString(text);
        }
        public void WriteSurrogateCharEntity(
        System.Char lowChar, System.Char highChar)
        {
            m_xwriter.WriteSurrogateCharEntity(lowChar, highChar);
        }
        public void WriteValue(
        System.Boolean value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.DateTime value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Decimal value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Double value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Single value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Int32 value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Int64 value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.Object value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteValue(
        System.String value)
        {
            m_xwriter.WriteValue(value);
        }
        public void WriteWhitespace(
        System.String ws)
        {
            m_xwriter.WriteWhitespace(ws);
        }
        public void Close(
        )
        {
            m_xwriter.Close();
        }
        public void Flush(
        )
        {
            m_xwriter.Flush();
        }
        public System.String LookupPrefix(
        System.String ns)
        {
            return m_xwriter.LookupPrefix(ns);
        }
        public void WriteAttributes(
        System.Xml.XmlReader reader, System.Boolean defattr)
        {
            m_xwriter.WriteAttributes(reader, defattr);
        }
        public void WriteAttributeString(
        System.String localName, System.String value)
        {
            m_xwriter.WriteAttributeString(localName, value);
        }
        public void WriteAttributeString(
        System.String localName, System.String ns, System.String value)
        {
            m_xwriter.WriteAttributeString(localName, ns, value);
        }
        public void WriteAttributeString(
        System.String prefix, System.String localName, System.String ns, System.String value)
        {
            m_xwriter.WriteAttributeString(prefix, localName, ns, value);
        }
        public void WriteBase64(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            m_xwriter.WriteBase64(buffer, index, count);
        }
        public void WriteBinHex(
        System.Byte[] buffer, System.Int32 index, System.Int32 count)
        {
            m_xwriter.WriteBinHex(buffer, index, count);
        }
        public void WriteCData(
        System.String text)
        {
            m_xwriter.WriteCData(text);
        }
        public void WriteCharEntity(
        System.Char ch)
        {
            m_xwriter.WriteCharEntity(ch);
        }
        public void WriteChars(
        System.Char[] buffer, System.Int32 index, System.Int32 count)
        {
            m_xwriter.WriteChars(buffer, index, count);
        }
        public void WriteComment(
        System.String text)
        {
            m_xwriter.WriteComment(text);
        }
        public void WriteDocType(
        System.String name, System.String pubid, System.String sysid, System.String subset)
        {
            m_xwriter.WriteDocType(name, pubid, sysid, subset);
        }
        public void WriteElementString(
        System.String localName, System.String value)
        {
            m_xwriter.WriteElementString(localName, value);
        }
        public void WriteElementString(
        System.String localName, System.String ns, System.String value)
        {
            m_xwriter.WriteElementString(localName, ns, value);
        }
        public void WriteElementString(
        System.String prefix, System.String localName, System.String ns, System.String value)
        {
            m_xwriter.WriteElementString(prefix, localName, ns, value);
        }
        public void WriteEndAttribute(
        )
        {
            m_xwriter.WriteEndAttribute();
        }
        public void WriteEndDocument(
        )
        {
            m_xwriter.WriteEndDocument();
        }
        public void WriteEndElement(
        )
        {
            m_xwriter.WriteEndElement();
        }
        public void WriteEntityRef(
        System.String name)
        {
            m_xwriter.WriteEntityRef(name);
        }
        public void WriteFullEndElement(
        )
        {
            m_xwriter.WriteFullEndElement();
        }
        public void WriteName(
        System.String name)
        {
            m_xwriter.WriteName(name);
        }
        public void WriteNmToken(
        System.String name)
        {
            m_xwriter.WriteNmToken(name);
        }
        #region ICoreDisposableObject Members
        public bool IsDisposed
        {
            get { return (this.m_xwriter.WriteState != WriteState.Error); }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            this.Close();
        }
        #endregion
        public static CoreXMLSerializer Create(Stream mem)
        {
            XmlWriterSettings xsetting = new XmlWriterSettings();
            xsetting.CloseOutput = false;
            xsetting.Indent = true;
            
            XmlWriter xwriter = XmlWriter.Create(mem, xsetting );
            return Create(xwriter);
        }
        public static CoreXMLSerializer Create(StringBuilder sb)
        {
            XmlWriterSettings xsetting = new XmlWriterSettings();
            xsetting.CloseOutput = false;
            xsetting.OmitXmlDeclaration = true;
            XmlWriter xwriter = XmlWriter.Create(sb, xsetting);
            CoreXMLSerializer xmwriter = new CoreXMLSerializer();
            xmwriter.m_xwriter = xwriter;
         
            return xmwriter;
        }
        public static CoreXMLSerializer Create(StringBuilder sb, IXMLSerializer seri)
        {
            XmlWriterSettings xsetting = new XmlWriterSettings();
            xsetting.CloseOutput = false;
            xsetting.OmitXmlDeclaration = true;
          
            
            CoreXMLSerializer xmwriter = new CoreXMLSerializer();
            
            if (seri != null)
            {
                xsetting.Indent = seri.Settings.Indent;
                xmwriter.m_BaseDir = seri.BaseDir;
                xmwriter.m_EmbedBitmap = seri.EmbedBitmap;
                xmwriter.m_resources = seri.Resources as CoreResourceSerializer;
            }
            XmlWriter xwriter = XmlWriter.Create(sb, xsetting);
            xmwriter.m_xwriter = xwriter;

            return xmwriter;
        }
        public static IXMLSerializer Create(string file)
        {
            XmlWriter xwriter = XmlWriter.Create(file);
            return Create(xwriter);
        }
        public static CoreXMLSerializer Create(XmlWriter writer, ICoreResourceSerializer sharedResources)
        {
            if ((sharedResources == null) || !(sharedResources is CoreResourceSerializer))
                return null;
            CoreXMLSerializer v_seri = Create(writer);
            v_seri.m_resources = sharedResources as CoreResourceSerializer  ;
            return v_seri;
        }

        public static string Serialize(ICoreSerializable i)
        {
            if (i == null)
                return string.Empty;
            StringBuilder sb = new StringBuilder();
            CoreXMLSerializer g = Create(sb, null);
            i.Serialize(g);
            g.Flush();
            return sb.ToString();
            
        }
        /// <summary>
        /// create a stream writer
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="baseDirectory"></param>
        /// <returns></returns>
        public static CoreXMLSerializer Create(Stream writer, string baseDirectory)
        {
            var g = Create(writer);
            g.m_BaseDir = baseDirectory;
            return g;
        }
    }
}

