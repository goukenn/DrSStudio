

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DocumentElement.cs
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
file:DocumentElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.ICore.WorkingObjects;

namespace IGK.ICore
{
    using IGK.ICore.Codec;
    /// <summary>
    /// represent a document element
    /// </summary>
    [CoreWorkingObject(CoreConstant.TAG_DOCUMENTS )]
    public sealed class DocumentElement : GKDSElementItemBase, ICoreSerializerService
    {
        private CoreDocumentCollections m_documents;
       
        public CoreDocumentCollections Documents {
            get {
                return this.m_documents;
            }
        }
        public DocumentElement()
        {
            this.m_documents = new CoreDocumentCollections(this);
        }
        /// <summary>
        /// get the documents tag collections
        /// </summary>
        public class CoreDocumentCollections : System.Collections.IEnumerable {
            DocumentElement m_document;
            List<ICoreWorkingDocument> m_ldocuments;
            public ICoreWorkingDocument this[int index] {
                get {
                    return this.m_ldocuments[index];
                }
            }
            internal  CoreDocumentCollections(DocumentElement document)
            {
                this.m_document = document;
                this.m_ldocuments = new List<ICoreWorkingDocument>();
            }
            public ICoreWorkingDocument[] ToArray() {
                return this.m_ldocuments.ToArray();
            }
            public void Add(ICoreWorkingDocument document)
            {
                this.m_ldocuments.Add(document);
            }
            public void Remove(ICoreWorkingDocument document)
            {
                this.m_ldocuments.Remove(document);
            }
            public void Clear() { this.m_ldocuments.Clear(); }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_ldocuments.GetEnumerator();
            }
            public  void AddRange(ICoreWorkingDocument[] documents)
            {
                this.m_ldocuments.AddRange(documents);
            }
            public int Count { get { return this.m_ldocuments.Count; } }
        }
        /// <summary>
        /// serialialize document
        /// </summary>
        /// <param name="xwriter"></param>
        void ICoreSerializable.Serialize(IXMLSerializer xwriter)
        {
            IGK.ICore.Codec.CoreXMLSerializerUtility.WriteStartElement(this, xwriter);
            foreach(ICoreWorkingDocument doc in this.Documents)
            {
                    doc.Serialize(xwriter);
            }
            xwriter.WriteEndElement();
        }
        void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
        {
            //this.m_IsLoading = true;
            this.Documents.Clear();
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
            {
                xreader.MoveToContent();
            }
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        ICoreWorkingDocument
                                        obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                        as ICoreWorkingDocument;
                        if (obj is ICoreWorkingDocument)
                        {
                            this.Documents.Add(obj);
                            obj.Deserialize(xreader.ReadSubtree());
                        }
                        else
                            xreader.ReadToEndElement();
                        break;
                }
            }
        }
        public static explicit operator DocumentElement(ICoreWorkingObject[] objs)
        {
            DocumentElement doc = new DocumentElement();
            ICoreWorkingDocument v_doc = null;
            for (int i = 0; i < objs.Length; i++)
            {
                v_doc = objs[i] as ICoreWorkingDocument ;
                if (v_doc !=null)
                    doc.m_documents.Add(v_doc);
            }
            return doc;
        }
    }
}

