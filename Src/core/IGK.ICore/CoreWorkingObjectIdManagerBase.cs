

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectIdManagerBase.cs
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
file:CoreWorkingObjectIdManagerBase.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    public class CoreWorkingObjectIdManagerBase : ICoreWorkingObjectIdManager
    {
        Dictionary<string, ICoreWorkingObject> m_items;
        Dictionary<ICoreWorkingObject, string> m_currentIds;
        public int Count {
            get {
                return m_items.Count;
            }
        }
        public CoreWorkingObjectIdManagerBase()
        {
            this.m_items = new Dictionary<string, ICoreWorkingObject>();
            this.m_currentIds = new Dictionary<ICoreWorkingObject, string>();
        }
        public virtual bool ChangeId(ICoreWorkingObject obj, string newId)
        {
            if (m_items.ContainsKey(newId))
            {
                return false;
            }
            //retreive the preview id;
            string old = m_currentIds[obj];
            if (old == newId)
                return false;
            this.m_items.Remove(old);
            this.m_items.Add(newId, obj);
            m_currentIds[obj] = newId;
            return true;
        }
        public ICoreWorkingObject[] GetAllElements()
        {
            return this.m_items.Values.ToArray();
        }
        public ICoreWorkingObject GetElementById(string id)
        {
            if (m_items.ContainsKey(id))
                return m_items[id];
            return null;
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_items.GetEnumerator();
        }
        /// <summary>
        /// register a working object 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Register(ICoreWorkingObject obj)
        {
            if ((obj == null) || (this.m_currentIds.ContainsKey (obj)) ||this.m_items .ContainsKey (obj.Id ))
                return false;
            this.m_currentIds.Add(obj, obj.Id);
            this.m_items.Add(obj.Id, obj);
            OnElementAdded(new CoreWorkingObjectEventArgs<ICoreWorkingObject>(obj));
            if (obj is ICoreWorkingObjectIdElementContainer)
            {
                ICoreWorkingObjectIdManager man = (obj as ICoreWorkingObjectIdElementContainer).IdManager;
                if (man != this)
                {
                    foreach (ICoreWorkingObject doc in man.GetAllElements())
                    {
                        this.Register(doc);
                    }
                    man.ElementAdded += man_ElementAdded;
                    man.ElementRemoved += man_ElementRemoved;
                }
            }
            return true;
        }
        void man_ElementRemoved(object sender, CoreWorkingObjectEventArgs<ICoreWorkingObject> e)
        {
            this.Unregister(e.Element);
        }
        void man_ElementAdded(object sender, CoreWorkingObjectEventArgs<ICoreWorkingObject> e)
        {
            this.Register(e.Element);
        }
        public bool Unregister(ICoreWorkingObject obj)
        {
            this.m_items.Remove(obj.Id);
            this.m_currentIds.Remove(obj);
            OnElementRemoved(new CoreWorkingObjectEventArgs<ICoreWorkingObject>(obj));
            if (obj is ICoreWorkingObjectIdElementContainer)
            {
                ICoreWorkingObjectIdManager man = (obj as ICoreWorkingObjectIdElementContainer).IdManager;
                if (man != this)
                {
                    foreach (ICoreWorkingObject doc in man.GetAllElements())
                    {
                        this.Unregister(doc);
                    }
                    man.ElementAdded -= man_ElementAdded;
                    man.ElementRemoved -= man_ElementRemoved;
                }
            }
            return true;
        }
        public event EventHandler<CoreWorkingObjectEventArgs<ICoreWorkingObject>> ElementAdded;
        public event EventHandler<CoreWorkingObjectEventArgs<ICoreWorkingObject>> ElementRemoved;
        ///<summary>
        ///raise the ElementRemoved; 
        ///</summary>
        protected virtual void OnElementRemoved(CoreWorkingObjectEventArgs<ICoreWorkingObject> e)
        {
            if (ElementRemoved != null)
                ElementRemoved(this, e);
        }
        ///<summary>
        ///raise the ElementAdded 
        ///</summary>
        protected virtual void OnElementAdded(CoreWorkingObjectEventArgs<ICoreWorkingObject> e)
        {
            if (ElementAdded != null)
                ElementAdded(this, e);
        }
    }
}

