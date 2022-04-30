

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XMLEditorDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XMLEditorDocument.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Xml;
namespace IGK.DrSStudio.XMLEditorAddIn
{
    using IGK.ICore.Codec;
    using IGK.DrSStudio.XMLEditorAddIn.Configuration;
    using IGK.DrSStudio.XMLEditorAddIn.WinUI;
    /// <summary>
    /// represent the implementation of xml document
    /// </summary>
    class XMLEditorDocument : IXMLEditorDocument 
    {
        string m_id;
        private IXMLDocumentLineCollections  m_lines;
        public IXMLEditorSetting EditorSetting { get { return XMLEditorSetting.Instance; } }
        public bool IsValid
        {
            get { return true; }
        }
        public XMLEditorDocument()
        {
        }
        public IXMLDocumentLineCollections Lines
        {
            get {
                if (this.m_lines == null)
                {
                    this.m_lines = CreateLineCollection();
                }
                return this.m_lines;
            }
        }
        public static XMLEditorDocument Load(string filename)
        {
            XMLEditorDocument doc = new XMLEditorDocument();
            XmlReader xreader = XmlReader.Create(filename);
            while (xreader.Read())
            { 
                //load element document
            }
            //XmlDocument d = new XmlDocument();
            //try
            //{
            //    d.Load(filename);
            //    if (d.ChildNodes.Count > 0)
            //    {
            //        BuildNodes(doc, d);
            //    }
            //}
            //catch (Exception ex){
            //    CoreLog.WriteDebug(ex.Message);
            //}
            return doc;
        }
        protected IXMLDocumentLineCollections CreateLineCollection()
        {
            return new XMLEditorDocumentLineCollection(this);
        }
        private static void BuildNodes(XMLEditorDocument document, XmlDocument d)
        {
            foreach (XmlNode n in d.ChildNodes)
            {
                BuildNodes (document, n);
            }
        }
        private static void BuildNodes(XMLEditorDocument document, XmlNode n)
        {
            IXMLDocumentLine line = null;
                switch (n.NodeType)
                {
                    case XmlNodeType.Text:
                        if(string.IsNullOrEmpty (n.Value ))
                            return ;
                        foreach( string v_s in  n.Value.Split ('\n'))
                        {
                            IXMLDocumentLine v_line = document.Lines.AddNew();
                        string[] t = System.Text.RegularExpressions.Regex.Split(v_s, "(\\w+)|([\\.,;\\-\\+=%\\*])|(\\<!--)|(--\\>)",
                             System.Text.RegularExpressions.RegexOptions.Compiled);
                        for (int i = 0; i < t.Length; i++)
                        {
                            if (!string.IsNullOrEmpty(t[i]))
                            {
                                v_line.Segments.Add(new XMLSegment(t[i]));
                            }
                        }
                        }
                        break;
                    case XmlNodeType.XmlDeclaration :
                        line = document.Lines.AddNew();
                        line.Segments.Add(new XMLSegment("<"));
                        line.Segments.Add(new XMLSegment("?"));
                        line.Segments.Add(new XMLSegment(n.Name));
                        if (!string.IsNullOrEmpty(n.Value))
                        {
                            string t = n.Value.Trim();
                            System.Text.RegularExpressions.Regex rg = new System.Text.RegularExpressions.Regex(
                                "(?<name>([_\\w\\d\\D\\W])+)\\s*=\\s*[\"'](?<value>([])+)[\"']"
                                );
                        }
                        line.Segments.Add(new XMLSegment("?"));
                        line.Segments.Add(new XMLSegment(">"));
                        break;
                    default:
                        line = document.Lines.AddNew();
                        line.Segments.Add(new XMLSegment("<"));
                        line.Segments.Add(new XMLSegment(n.Name));
                        for (int i = 0; i < n.Attributes.Count; i++)
                        {
                            line.Segments.Add(new XMLSpaceSegment());
                            line.Segments.AddAttribute(n.Attributes[i].Name, n.Attributes[i].Value);
                        }
                        if (n.HasChildNodes)
                        {
                            line.Segments.Add(new XMLSegment(">"));
                            for (int i = 0; i < n.ChildNodes.Count; i++)
                            {
                                BuildNodes(document, n.ChildNodes[i]);
                            }
                            line = document.Lines.AddNew();
                            line.Segments.Add(new XMLSegment("<"));
                            line.Segments.Add(new XMLSegment("/"));
                            line.Segments.Add(new XMLSegment(n.Name));
                            line.Segments.Add(new XMLSegment(">"));
                        }
                        else
                        {//add it he edn of the line
                            line.Segments.Add(new XMLSegment("/"));
                            line.Segments.Add(new XMLSegment(">"));
                        }
                        break;
                }
        }
        #region IXMLEditorDocument Members
        public void Save(string filename)
        {
            Stream v_stream = System.IO.File.Create(filename);
            v_stream.Flush();
            v_stream.Close();
        }
        public void Draw(Graphics g)
        {
        }
        #endregion
        #region ICoreIdentifier Members
        public string Id
        {
            get {
                if (string.IsNullOrEmpty(this.m_id))
                {
                    this.m_id = String.Format ("XMLDOC_{0}",this.GetHashCode());
                }
                return this.m_id; }
        }
        #endregion
        #region ICoreSerializerService Members
        public void Serialize(IXMLSerializer xwriter)
        {            
        }
        public void Deserialize(IXMLDeserializer xreader)
        {
            this.m_IsLoading = true;
        }
        private bool m_IsLoading;
        public bool IsLoading
        {
            get { return m_IsLoading; }
        }
        public event EventHandler LoadingComplete;
        ///<summary>
        ///raise the LoadingComplete 
        ///</summary>
        protected virtual void OnLoadingComplete(EventArgs e)
        {
            if (LoadingComplete != null)
                LoadingComplete(this, e);
        }
        #endregion
        #region IXMLEditorDocument Members
        public void Draw(XMLRendereringEventArgs e)
        {
            for (int i = 0; i < this.Lines.Count; i++)
            {
                e.OffsetY += this.Lines[i].Draw(e);
                if (e.OffsetY > e.Rectangle.Height)
                    break;
            }
            //e.Graphics.DrawString("items", 
            //    e.Font,
            //    WinCoreBrushRegister.GetBrush (Configuration.XMLEditorSetting.Instance.DefaultColor),
            //    System.Drawing.Point.Empty);
           // e.OffsetY += e.LineHeight;
        }
        #endregion
        /// <summary>
        /// get the editor document
        /// </summary>
        class XMLEditorDocumentLineCollection : IXMLDocumentLineCollections 
        {
            XMLEditorDocument m_document;
            List<IXMLDocumentLine> m_lines;
            public XMLEditorDocumentLineCollection(XMLEditorDocument document)
            {
                this.m_document = document;
                this.m_lines = new List<IXMLDocumentLine>();
            }
            #region IXMLDocumentLineCollections Members
            public void Add(IXMLDocumentLine line)
            {
                this.m_lines.Add(line);
            }
            public void Remove(IXMLDocumentLine line)
            {
                this.m_lines.Remove(line);
            }
            public int Count
            {
                get { return this.m_lines.Count ; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_lines.GetEnumerator();
            }
            #endregion
            #region IXMLDocumentLineCollections Members
            public IXMLDocumentLine this[int index]
            {
                get { return this.m_lines[index]; }
            }
            #endregion
            #region IXMLDocumentLineCollections Members
            public IXMLDocumentLine AddNew()
            {
                IXMLDocumentLine line = m_document.CreateNewLine();
                this.Add(line);
                return line;
            }
            #endregion
            #region IXMLDocumentLineCollections Members
            public void Add(string text)
            {
                XmlDocument v_document = new XmlDocument ();
                try
                {                    
                    v_document.LoadXml(text);
                    BuildNodes(this.m_document, v_document);
                }
                catch(Exception ex) {
                    CoreLog.WriteDebug(ex.Message);
                }
                finally {
                }
            }
            #endregion
        }
        internal virtual IXMLDocumentLine CreateNewLine()
        {
            return new XMLDocumentLine();
        }
        public Type DefaultSurfaceType
        {
            get { return typeof(XMLEditorSurface); }
        }
    }
}

