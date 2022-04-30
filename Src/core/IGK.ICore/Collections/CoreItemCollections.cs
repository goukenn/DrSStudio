

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreItemCollections.cs
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
file:CoreItemCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent an items collection
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CoreItemCollections<T> : IEnumerable where T : class 
    {
        List<T> m_items;
        public T this[int index] {
            get {
                if ((index >=0) && (index < Count ))
                    return this.m_items[index];
                return null;
            }
        }
        protected List<T> Items {
            get {
                return this.m_items;
            }
        }
        public virtual void Add(T item)
        {
            if ((item == null) || this.Items.Contains(item) )
                return;
            this.Items.Add(item);
        }
        public virtual void Remove(T item)
        {
            if (this.Items.Contains (item))
            this.Items.Remove(item);
        }
        public virtual void AddRange(params T[] items)
        { 
            if ((items ==null) || (items.Length == 0))
                return ;
            this.m_items.AddRange (items);
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public CoreItemCollections()
        {
            this.m_items = new List<T>();
        }
        public virtual bool Contains(T e)
        {
            return this.m_items.Contains(e);
        }
        public int Count
        {
            get { return this.m_items.Count; }
        }
        public IEnumerator GetEnumerator()
        {
            return this.m_items.GetEnumerator();
        }
        public T[] ToArray()
        {
            return this.m_items.ToArray();
        }
    }
}

