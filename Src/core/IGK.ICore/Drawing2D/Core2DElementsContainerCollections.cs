

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DElementsContainerCollections.cs
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
file:Core2DElementsContainerCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public class Core2DElementsContainerCollections : ICore2DElementsContainerCollections
    {
        ICore2DElementsContainer m_owner;
        List<ICore2DDrawingElement> m_elements;
        /// <summary>
        /// get the owner
        /// </summary>
        public ICore2DElementsContainer Owner
        {
            get {
                return this.m_owner;
            }
        }
        protected List<ICore2DDrawingElement> Elements { get { return  this.m_elements; } }
        public ICore2DDrawingElement this[int index] { get { return this.m_elements[index]; } }
        public Core2DElementsContainerCollections(ICore2DElementsContainer owner)
        {
            this.m_owner = owner ;
            this.m_elements = new List<ICore2DDrawingElement>();
        }
        #region ICore2DElementsContainerCollections Members
        public ICore2DDrawingElement[] ToArray()
        {
            return this.m_elements.ToArray();
        }
        public T[] ToArray<T>()
        {
            T[] t = new T[this.Count];
            Array.Copy(this.ToArray(), t, t.Length);
            return t;
        }
        public override string ToString()
        {
            return string.Format("Count : {0}", this.Count);
        }
        public virtual bool Remove(ICore2DDrawingElement element)
        {
            if (m_elements.Contains(element))
            {
                m_elements.Remove(element);
                return true;
            }
            return false;
        }
        #endregion
        #region ICoreCountEnumerable Members
        public int Count
        {
            get { return this.m_elements.Count; }
        }
        #endregion
        #region IEnumerable Members
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_elements.GetEnumerator();
        }
        #endregion
        public virtual void Clear()
        {
            this.m_elements.Clear();
        }
        public virtual void AddRange(params ICore2DDrawingElement[] element)
        {
            this.m_elements.AddRange(element);
        }
    }
}

