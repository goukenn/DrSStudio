

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingDocumentCollectionsBase.cs
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
file:CoreWorkingDocumentCollectionsBase.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public abstract class CoreWorkingDocumentCollectionsBase : ICoreWorkingDocumentCollections 
    {
        ICoreWorkingDocumentContainer m_owner;
        List<ICoreWorkingDocument> m_list;
        public ICoreWorkingDocumentContainer Owner { get { return this.m_owner; } }
        public CoreWorkingDocumentCollectionsBase(ICoreWorkingDocumentContainer owner)
        {
            this.m_owner = owner;
            this.m_list = new List<ICoreWorkingDocument>();
        }
        #region ICoreWorkingDocumentCollections Members
        public int Count
        {
            get { return this.m_list.Count; }
        }
        public ICoreWorkingDocument this[int index]
        {
            get { return this.m_list [index]; }
        }
        public void Add(ICoreWorkingDocument document)
        {
            this.m_list.Add(document);
        }
        public void Remove(ICoreWorkingDocument document)
        {
            this.m_list.Remove(document);
        }
        public bool Contains(ICoreWorkingDocument document)
        {
            if (document == null)
                return true;
            return this.m_list.Contains(document);
        }
        public void MoveToFront(ICoreWorkingDocument document)
        {
            if (!Contains(document))
                return;
            int i = this.m_list.IndexOf(document);
            if (i < (Count - 1))
            {
                this.m_list.Remove(document);
                this.m_list.Insert(i + 1, document);
                DocumentPositionChanged(document, i);
                //this.m_owner.OnDocumentZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(
                //    document, i, document.ZIndex));
            }
        }
        protected virtual void DocumentPositionChanged(ICoreWorkingDocument document, int i)
        {
        }
        public void MoveToBack(ICoreWorkingDocument document)
        {
            if (!Contains(document))
                return;
            int i = this.m_list.IndexOf(document);
            if (i > 0)
            {
                this.m_list.Remove(document);
                this.m_list.Insert(i - 1, document);
                DocumentPositionChanged(document, i);
                //this.m_owner.OnDocumentZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(
                //    document, i, document.ZIndex));
            }
        }
        public   void MoveToBegin(ICoreWorkingDocument document)
        {
            if (!Contains(document))
                return;
            int i = this.m_list.IndexOf(document);
            if (i > 0)
            {
                this.m_list.Remove(document);
                this.m_list.Insert(0, document);
                DocumentPositionChanged(document, i);
                //this.m_owner.OnDocumentZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(
                //    document, i, document.ZIndex));
            }
        }
        public void MoveToEnd(ICoreWorkingDocument document)
        {
            if (!Contains(document))
                return;
            int i = this.m_list.IndexOf(document);
            if (i < (Count - 1))
            {
                this.m_list.Remove(document);
                this.m_list.Insert(Count, document);
                DocumentPositionChanged(document, i);
                //this.m_owner.OnDocumentZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(
                //    document, i, document.ZIndex));
            }
        }
        public ICoreWorkingDocument[] ToArray()
        {
            return this.m_list.ToArray();
        }
        public int IndexOf(ICoreWorkingDocument document)
        {
            if (document == null)
            return -1;
            return this.m_list.IndexOf(document);
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_list.GetEnumerator();
        }
        #endregion
        public virtual bool AllowMultiDocument
        {
            get { return true; }
        }
    }
}

