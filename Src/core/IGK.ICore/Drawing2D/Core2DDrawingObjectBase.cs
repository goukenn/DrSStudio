

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingObjectBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;
using IGK.ICore.Codec;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Core2DDrawingObjectBase.cs
*/
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D.Actions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a base class of drawing 2d object
    /// </summary>
    public abstract class Core2DDrawingObjectBase : 
        Core2DDrawingComponentObjectBase, 
        ICore2DDrawingObject,
        ICoreWorkingConfigurableObject,
        ICoreWorkingDisposableObject
    {
        private Core2DDrawingObjectBase  m_Parent;
        [Browsable(false)]
        /// <summary>
        /// get the parent
        /// </summary>
        public Core2DDrawingObjectBase  Parent
        {
            get { return m_Parent; }
            set { // set the parent of this drawing object
                if (m_Parent != value) {
                    m_Parent = value;
                    this.OnParentChanged(EventArgs.Empty);
                }
            }
        }

        public EventHandler<EventArgs> ParentChanged;

        private void OnParentChanged(EventArgs e) => ParentChanged?.Invoke(this, e);  

        ICore2DDrawingObject ICore2DDrawingObject.Parent
        {
            get
            {
                return this.Parent;
            } 
        }
        public virtual  enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            if (this.GetConfigType() == enuParamConfigType.ParameterConfig)
            {
                var group = parameters.AddGroup("Definition");
                group.AddItem(this.GetType().GetProperty("Id"));
                CoreParameterConfigUtility.LoadConfigurationUtility(this, parameters, this.GetType());              
            }
            return parameters;
        }
        public virtual ICoreControl GetConfigControl()
        {
            return null;
        }
    
        /// <summary>
        /// represent a drawing object collection base
        /// </summary>
        public class Core2DDrawingObjectCollection : IEnumerable, ICoreWorkingElementCollections
        {
            List<Core2DDrawingObjectBase> m_objects;
            private Core2DDrawingObjectBase m_owner;

            public int Count { get { return this.m_objects.Count; } }

            public Core2DDrawingObjectBase this[int index] { get {
                if (this.m_objects.IndexExists (index))
                return this.m_objects[index];
                return null;
            }
            }

            public virtual bool MoveAt(Core2DDrawingObjectBase obj, int index)
            {
                return CorePositionnableUtility.MoveAt<Core2DDrawingObjectBase>(this.m_objects, obj, index );
            }
            
            public virtual bool MoveToBegin(Core2DDrawingObjectBase obj)
            {
                return CorePositionnableUtility.MoveToBegin<Core2DDrawingObjectBase>(this.m_objects, obj);
            }
            public virtual bool MoveToEnd(Core2DDrawingObjectBase obj)
            {
                return CorePositionnableUtility.MoveToEnd<Core2DDrawingObjectBase>(this.m_objects, obj);
            }
            public virtual bool MoveToBack(Core2DDrawingObjectBase obj)
            {
                return CorePositionnableUtility.MoveToBack<Core2DDrawingObjectBase>(this.m_objects, obj);
            }
            public virtual bool MoveToFront(Core2DDrawingObjectBase obj)
            {
                return CorePositionnableUtility.MoveToUp<Core2DDrawingObjectBase>(this.m_objects, obj);
            }
            public bool Contains(Core2DDrawingObjectBase obj)
            {
                return this.m_objects.Contains(obj);
            }
            public int IndexOf(Core2DDrawingObjectBase obj)
            {
                return this.m_objects.IndexOf(obj);
            }
            public void Clear()
            {
                this.m_objects.Clear();
            }

            public virtual void AddRange(Core2DDrawingObjectBase[] objects)
            {
                if (objects == null)
                foreach (var item in objects)
                {
                    this.Add(item);
                }
            }
            /// <summary>
            /// .ctr
            /// </summary>
            /// <param name="owner"></param>
            public Core2DDrawingObjectCollection(Core2DDrawingObjectBase owner)
            {
                this.m_objects = new List<Core2DDrawingObjectBase>();
                this.m_owner = owner;
            }
         
            public virtual void Add(Core2DDrawingObjectBase obj)
            {
                if ((obj == null) || (this.m_objects.Contains(obj)))
                    return;
                this.m_objects.Add(obj);
                obj.Parent = this.m_owner;
            }
            public virtual void Remove(Core2DDrawingObjectBase obj)
            {
                if (this.m_objects.Contains(obj))
                {
                    this.m_objects.Remove(obj);
                    obj.Parent = null;
                }
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_objects.GetEnumerator();
            }
            public Core2DDrawingObjectBase[] ToArray()
            {
                return m_objects.ToArray();
            }

            public bool InsertAt(Core2DDrawingObjectBase document, int index)
            {
                if (!this.m_objects.Contains(document))
                {
                    this.m_objects.Insert(index, document);
                    document.Parent = this.m_owner;
                    return true;
                }
                return false;
            }


            ICoreWorkingObject ICoreWorkingElementCollections.this[int index]
            {
                get { return this[index];  }
            }

            public bool IsReadOnly
            {
                get { return false; }
            }            
        }

        public virtual void LoadXml(string text)
        {
            StringBuilder sb = new StringBuilder(text);
            var g = CoreXMLDeserializer.Create(sb);
            (this as ICoreSerializerService).Deserialize(g);
            g.Close();
        }
    }
}

