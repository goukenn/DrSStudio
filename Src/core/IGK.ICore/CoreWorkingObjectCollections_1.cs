

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectCollections.cs
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
file:CoreWorkingObjectCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    public class CoreWorkingObjectCollections<T> : IEnumerable 
        where T : ICoreWorkingObject 
    {
        List<T> m_elements;
        protected List<T> Elements { get { return this.m_elements; } }
        public int Count {
            get {
                return this.m_elements.Count;
            }
        }
        public CoreWorkingObjectCollections()
        {
            this.m_elements = new List<T>();
        }
        public bool Contains(T element)
        {
            return m_elements.Contains(element);
        }
        public T[] ToArray() {
            return m_elements.ToArray();
        }
        public virtual void Add(T element)
        {
            if ((element == null) || this.m_elements.Contains (element )) 
                return;
            this.m_elements.Add(element);
        }
        public virtual void Remove(T element)
        {
            if (element == null) return;
            this.m_elements.Remove(element);
        }
        public IEnumerator GetEnumerator()
        {
            return this.m_elements.GetEnumerator();
        }
        public virtual void Clear()
        {
            this.m_elements.Clear();
        }
    }
}

