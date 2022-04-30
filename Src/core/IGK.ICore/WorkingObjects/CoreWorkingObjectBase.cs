

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingObjectBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
 * 
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreWorkingObjectBase.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using IGK.ICore.WorkingObjects;
using IGK.ICore.ComponentModel;

namespace IGK.ICore
{
    [Serializable ()]
    /// <summary>
    /// represent a core working object base
    /// </summary>
    public abstract class CoreWorkingObjectBase : 
        MarshalByRefObject, 
        ICoreWorkingObject, 
        ICoreDisposableObject ,
        ICoreLoadableComponent,
        ICoreSerializerService,
        ICoreSerializable ,
        ICoreDeserializable,
        ICoreWorkingLayoutElement,
        ICoreWorkingDisposableObject
        //ICoreWorkingAttachedPropertyObject
    {
        //private Dictionary<string, object> m_attachedProperties;//used to store attached properties obseleted.
        private Dictionary<string, object> m_attributes;

        private string m_id;
        private bool m_disposed;
        private bool m_IsLoading;
        private int m_suspendDepth; //used for getting the depth of the suspending behaviour
        private bool m_isValid;

        /// <summary>
        /// deepth reading data
        /// </summary>
        [Browsable(false)]
        public int SuspendDeep {
            get {
                return this.m_suspendDepth;
            }
        }
        [Browsable(false)]
        public virtual bool IsValid {
            get {
                return this.m_isValid;
            }
            protected set {
                this.m_isValid = value;
            }
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(m_id))
            {
                return string.Format ("{0}{1}", 
                    this.GetObjectName()??"unknow",
                    "#"+this.m_id);
            }
            return base.ToString();
        }
        /// <summary>
        /// get the id manager
        /// </summary>
        /// <returns></returns>
        protected virtual ICoreWorkingIdManager GetIdManager()
        {
            return null;
        }
        /// <summary>
        /// resume layout. Call the loading complete
        /// </summary>
        public virtual void ResumeLayout()
        {
            if (this.m_IsLoading)
            {
                this.m_suspendDepth--;
                if (this.m_suspendDepth <= 0)
                {
                    this.m_IsLoading = false;
                   
                    OnLoadingComplete(EventArgs.Empty);
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// suspend layout
        /// </summary>
        public virtual void SuspendLayout()
        {
            if (!this.m_IsLoading)
            {
                this.m_IsLoading = true;
            }
            this.m_suspendDepth++;
        }
        [Browsable(false)]
        public bool IsDisposed
        {
            get { return this.m_disposed; }
            protected set { this.m_disposed = value; }
        }
        /// <summary>
        /// get or set the id of the item
        /// </summary>
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (null)]
        public virtual string Id
        {
            get
            {
                if (string.IsNullOrEmpty(this.m_id))
                {
                    this.m_id = string.Format("{0}_{1}", this.GetObjectName(), this.GetHashCode());
                }
                return this.m_id;
            }
            set
            {
                if (this.m_id != value)
                {
                    if (!string.IsNullOrEmpty(value) && System.Xml.XmlReader.IsName(value))
                    {
                        this.m_id = value;
                    }
                    else
                    {
                        this.m_id = string.Empty;
                    }
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Id);
                }
            }
        }
        [Browsable(false)]
        /// <summary>
        /// get if this element is on a editing state
        /// </summary>
        public bool IsEditing {
            get {
                return this.m_isEditing;
            }
            protected internal set {

                this.m_isEditing = value;
            }
        }
        
        [Browsable(false)]
        [ReadOnly(true )]
        /// <summary>
        /// indicate that the current object is currently on loading state
        /// </summary>
        public bool IsLoading
        {
            get { return this.m_IsLoading; }
            protected set {

                this.m_IsLoading = value;
            }
        }
        public event EventHandler Disposed;
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e){
            if (!this.m_IsLoading){
                this.PropertyChanged?.Invoke(this, e);
            }
        }
        public string GetObjectName()
        {
            CoreWorkingObjectAttribute obj =
                Attribute.GetCustomAttribute(this.GetType(),
                typeof(CoreWorkingObjectAttribute), false) as CoreWorkingObjectAttribute;
            if (obj != null)
                return obj.Name;
            return null;
        }
        /// <summary>
        /// .ctr represent a base object abtract class
        /// </summary>
        public CoreWorkingObjectBase()
        {
          //  this.m_attachedProperties = new Dictionary<string, object>();
            this.m_attributes = new Dictionary<string, object>();
        }
        ///// <summary>
        ///// attach an additional property to the current item        
        ///// </summary>
        ///// <param name="propertyName">property name to attach</param>
        ///// <param name="parentObject">parent provider of the property</param>
        ///// <param name="typeofProperty">type of the propery</param>
        //public void AttachProperty(string propertyName, CoreWorkingObjectBase parentObject, Type typeofProperty)
        //{
        //    string key = CoreWorkingObjectAttribute.GetObjectName(parentObject)+"."+propertyName ;
        //    var c = new { Parent = parentObject, Type = typeofProperty };
        //    if (!m_attachedProperties.ContainsKey(key))
        //        m_attachedProperties.Add(key, c);
        //    else
        //        m_attachedProperties[key] = c;
        //}
        ///// <summary>
        ///// detach property 
        ///// </summary>
        ///// <param name="propertyName">property name</param>
        ///// <param name="parentObject">parent object</param>
        //public void DetachProperty(string propertyName, CoreWorkingObjectBase parentObject)
        //{
        //    string key = CoreWorkingObjectAttribute.GetObjectName(parentObject) + "." + propertyName;
        //    if (m_attachedProperties.ContainsKey(key))
        //        m_attachedProperties.Remove(key);
        //}

        public void DetachProperties(CoreWorkingObjectBase parentObject)
        { 
            //1.get attached properties
            string key = CoreWorkingObjectAttribute.GetObjectName(parentObject);

        }
      
        ///// <summary>
        ///// get attached properties used in spécified context
        ///// </summary>
        ///// <param name="propertyName"></param>
        ///// <returns></returns>
        //public object GetAttachedProperty(string propertyName)
        //{
        //    string key =  propertyName;
        //    if (m_attachedProperties.ContainsKey(key))
        //    {
        //       var  a = m_attachedProperties[key].Cast(new {Parent=default(CoreWorkingObjectBase), 
        //           Type =default (Type) });
        //       try
        //       {
        //           return a.Parent.GetType().GetMethod("Get" + key.Split('.').Last()).Invoke(a.Parent , new object[]{this});
        //       }
        //       catch(Exception ex) {
        //           CoreLog.WriteDebug(ex.Message);
        //       }
        //    }
        //    return null;
        //}
      
        //public void SetAttachedProperty(string propertyName, object value)
        //{
        //    string key = propertyName;
        //    if (m_attachedProperties.ContainsKey(key))
        //    {
        //        var a = m_attachedProperties[key].Cast(new { Parent = default(CoreWorkingObjectBase), Type = default(Type) });
        //        if (value.GetType () == a.Type)
        //            a.Parent.GetType().GetMethod("Set" + key.Split('.').Last()).Invoke(a.Parent, new object[]{this, value });
        //    }
        //}
        //#region IDisposable Members
        
        //#endregion
        //public IEnumerable GetAttachedProperties()
        //{
        //    return m_attachedProperties;
        //}

        public virtual void Dispose()
        {
            m_disposed = true;//mark flag as disposed
            this.Disposed?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void WriteAttributes(IXMLSerializer xwriter)
        {
            IGK.ICore.Codec.CoreXMLSerializerUtility.WriteAttributes(this, xwriter);
        }
        protected virtual void WriteElements(IXMLSerializer xwriter)
        {
            IGK.ICore.Codec.CoreXMLSerializerUtility.WriteElements(this, xwriter);
        }
        void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
        {            
            this.SuspendLayout();
            this.IsValid = true;
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent();
            this.ReadAttributes(xreader);
            this.ReadElements(xreader);
            this.ResumeLayout();
        }
        void ICoreSerializable.Serialize(IXMLSerializer xwriter)
        {
            this.Serialize(xwriter);
    
        }
        internal protected virtual void Serialize(IXMLSerializer xwriter)
        {
            IGK.ICore.Codec.CoreXMLSerializerUtility.WriteStartElement(this, xwriter);
            this.WriteAttributes(xwriter);
            this.WriteElements(xwriter);
            xwriter.WriteEndElement();
        }
        /// <summary>
        /// event raise when loading complete
        /// </summary>
        public event EventHandler LoadingComplete;
        private bool m_isEditing;

        
        protected virtual void OnLoadingComplete(EventArgs eventArgs)
        {
            LoadingComplete?.Invoke(this, eventArgs);
        }
        protected virtual void ReadAttributes(IXMLDeserializer xreader)
        {
            CoreXMLSerializerUtility.ReadAttributes (this, xreader, SetAttributeValue);
        }
        /// <summary>
        /// used only to set custom attribute
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        internal protected virtual void SetAttributeValue(string name, string value)
        {
            CoreXMLSerializerUtility.ReadAttributeValue(this, name, value);
        }
        /// <summary>
        /// read element sub node
        /// </summary>
        /// <param name="xreader"></param>
        protected virtual void ReadElements(IXMLDeserializer xreader)
        {
            IGK.ICore.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, null);
        }
        /// <summary>
        /// render the current working object and return his representation 
        /// </summary>
        /// <returns></returns>
        public virtual string Render()
        {
            StringBuilder sb = new StringBuilder();
            CoreXMLSerializer seri = CoreXMLSerializer.Create (sb);
            this.Serialize(seri);
            seri.Flush();
            return sb.ToString();
        }
        

        /// <summary>
        /// clone the current object
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            //no meber clone 
            ICoreSerializerService r = this as ICoreSerializerService;
            StringBuilder sb = new StringBuilder ();
            CoreXMLSerializer v = CoreXMLSerializer.Create (sb);
                r.Serialize(v);
                v.Dispose();
            v.Flush();

            CoreXMLDeserializer b = CoreXMLDeserializer.Create(sb);

            var e = IGK.ICore.Codec.CoreXMLSerializerUtility.GetAllObjects (b);
            if (e.Length == 1)
            {
                //return the cloned object
                var c = e[0] as CoreWorkingObjectBase;
                if (c != null)
                    c.m_id = null;
                return c;
            }
            return MemberwiseClone();
        }
        /// <summary>
        /// set attribute a the core working object
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public virtual  void SetAttribute(string name, object value)
        {
            if (this.m_attributes.ContainsKey(name))
            {
                if (value == null)
                    this.m_attributes.Remove(name);
                else
                    this.m_attributes[name] = value;
            }
            else {
                if (value != null)
                    this.m_attributes.Add(name, value);
            }
        }
        /// <summary>
        /// get the attribute associate to this working object. used to store value for particular purpose
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual object GetAttribute(string name)
        {
            if (this.m_attributes.ContainsKey(name))
                return this.m_attributes[name];
            return null;
        }
        public T  GetAttribute<T>(string name)
        {
            object b = this.GetAttribute(name);
            if (b is T)
                return (T)b;
            return default(T);
        }
        /// <summary>
        /// check if attribute exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual bool AttributeExist(string name)
        {
            return this.GetAttribute(name) !=null;
        }
    }
}

