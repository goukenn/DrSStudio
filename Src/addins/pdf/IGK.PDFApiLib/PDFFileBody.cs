

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFFileBody.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PDFFileBody.cs
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
namespace IGK.PDFApi
{
    public class PDFFileBody : PDFDataBase , IPDFFileBody
    {
        PDFDocument m_document;
        PDFObjectCollections m_objects;
        /// <summary>
        /// get the pdf object collection
        /// </summary>
        public IPDFObjectCollections Objects {
            get {
                return this.m_objects;
            }
        }
        internal PDFFileBody(PDFDocument document)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            m_document = document;
            m_objects = new PDFObjectCollections(this);
        }
        public event PDFObjectEventHandler ObjectAdded;
        public event PDFObjectEventHandler ObjectRemove;
        ///<summary>
        ///raise the ObjectRemove 
        ///</summary>
        protected virtual void OnObjectRemove(PDFObjectEventArgs e)
        {
            if (ObjectRemove != null)
                ObjectRemove(this, e);
        }
        ///<summary>
        ///raise the ObjectAdded 
        ///</summary>
        protected virtual void OnObjectAdded(PDFObjectEventArgs e)
        {
            if (ObjectAdded != null)
                ObjectAdded(this, e);
        }
        public class PDFObjectCollections : IPDFObjectCollections
        {
            PDFFileBody m_body;
            List<IPDFObject> m_objects;
            public PDFObjectCollections(PDFFileBody body)
            {
                this.m_objects = new List<IPDFObject>();
                this.m_body  = body;
            }
            #region IPDFObjectCollections Members
            public int Count
            {
                get { return this.m_objects.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_objects.GetEnumerator();
            }
            #endregion
            #region IPDFObjectCollections Members
            void IPDFObjectCollections.Add(IPDFObject sObject)
            {
                this.Add(sObject as PDFObject);
            }
            public void Add(PDFObject sObject)
            {
                if ((sObject != null) && (!this.m_objects.Contains(sObject)))
                {
                    sObject.Document = this.m_body.m_document;
                    if (sObject.Id == null)
                        sObject.Id  = this.Count + 1;
                    if (sObject.Update == null)
                        sObject.Update = 0;
                    this.m_objects.Add(sObject);
                    this.m_body.OnObjectAdded(new PDFObjectEventArgs(sObject));
                }
            }
            public void Remove(IPDFObject sObject)
            {
                this.m_objects.Remove(sObject);
                this.m_body.OnObjectRemove(new PDFObjectEventArgs(sObject));
            }
            #endregion
        }
        public override void Render(System.IO.Stream stream)
        {
            foreach (IPDFObject  obj in this.m_objects )
            {
                obj.Render(stream);             
            }
        }
    }
}

