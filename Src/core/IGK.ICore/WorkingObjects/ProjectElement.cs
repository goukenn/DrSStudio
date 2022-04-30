

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ProjectElement.cs
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
file:ProjectElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Codec;
    using IGK.ICore.WinUI ;
    using IGK.ICore.Xml;
    /// <summary>
    /// represent a project item. That allow the loading of the creation.
    /// </summary>
    [Serializable ()]
    [CoreWorkingObject (CoreConstant.TAG_PROJECT )]
    public class ProjectElement : GKDSElementItemBase, ICoreProject, ICoreSerializerService
    {
        private Dictionary<string, ICoreProjectItem> m_projectItems;
        private ICoreWorkingSurface m_Owner;
        /// <summary>
        /// get the contained value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetValue(string key)
        {
            if (this.m_projectItems.ContainsKey(key))
            {
                return m_projectItems[key].Value;
            } return string.Empty;
        }
        /// <summary>
        /// get the owner of the property element
        /// </summary>
        public ICoreWorkingSurface Owner {
            get {
                return this.m_Owner;
            }
        }
        public string SurfaceType
        {
            get { 
                if (this.Contains ( CoreConstant.TAG_SURFACETYPE ))
                    return this[CoreConstant.TAG_SURFACETYPE ].Value ;
                return null;
            }
        }
        internal  ProjectElement()
        {
            m_projectItems = new Dictionary<string, ICoreProjectItem>();
        }
        internal protected ProjectElement(ICoreWorkingSurface surface):this()
        {
            if (surface == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "surface");
            this.m_Owner = surface;
            this.Add(CoreConstant.TAG_SURFACETYPE, CoreWorkingObjectAttribute.GetObjectName(surface.GetType()),null);
        }
        public override string ToString()
        {
            return string.Format ("{0}[#{1}]",CoreConstant.TAG_PROJECT, this.m_projectItems.Count);
        }
        /// <summary>
        /// create a copy of this 
        /// </summary>
        /// <param name="projectInfo"></param>
        /// <returns></returns>
        internal static ProjectElement Create(ICoreProject projectInfo)
        {
            if (projectInfo == null) return null;
            ProjectElement v_pr = new ProjectElement();
            v_pr.CopyDefinition(projectInfo);
            return v_pr ;
        }
        protected virtual void CopyDefinition(ICoreProject projectInfo)
        {
            if (projectInfo == null)
                return;
            foreach (KeyValuePair<string,ICoreProjectItem> item in projectInfo)
            {
                //don't change the surface type
                if( (this.m_Owner !=null) && (item.Key == "SurfaceType"))
                    continue;
                switch (item.Key.ToLower())
                {
                    case "filename":
                        this._addKey(item.Key, System.IO.Path.GetFileName(item.Value.Value.ToString()));
                        break;
                    default:
                        this._addKey(item.Key, item.Value);
                        break;
                }
            }
        }
        private void _addKey(string key, ICoreProjectItem value)
        {
            if (this.m_projectItems.ContainsKey(key))
            {
                this.m_projectItems[key].Value = value.Value;
            }
            else
                this.m_projectItems.Add(key, value.Clone() as ICoreProjectItem);
        }
        private void _addKey(string key, string value)
        {
            if (this.m_projectItems.ContainsKey(key))
            {
                this.m_projectItems[key].Value = value;
            }
            else
                this.m_projectItems.Add(key, new CoreProjectItem(this, key, value));
        }
        protected void SetValue(string name, string Value)
        {
            if (this.Contains (name))
                this[name].Value = Value ;
        }
        void ICoreSerializable.Serialize(IXMLSerializer xwriter)
        {
            xwriter.WriteStartElement(CoreWorkingObjectAttribute.GetObjectName(this.GetType ()));
           // xwriter.WriteElementString(CoreConstant.TAG_SURFACETYPE, this.SurfaceType);
            ICoreProjectItem v_val;
            ICoreProjectItemContainer v_cont;
            foreach (KeyValuePair<string, ICoreProjectItem> item in this.m_projectItems)
            {
                if (item.Value is ICoreProjectItemContainer )
                {
                    v_cont = item.Value as ICoreProjectItemContainer;
                    if (v_cont.HasChild || (v_cont.Value == null))
                    {
                        xwriter.WriteStartElement(item.Key);
                        v_cont.Serialize(xwriter);
                        xwriter.WriteEndElement();
                    }
                    else
                    {
                            xwriter.WriteStartElement(item.Key);
                            xwriter.WriteValue(v_cont.Value);
                            xwriter.WriteEndElement();
                    }
                }
                else if  (item.Value is ICoreProjectItem)
                {
                    v_val = item.Value as ICoreProjectItem;
                    if (!string.IsNullOrEmpty (v_val .Value ))
                    xwriter.WriteElementString (item.Key, v_val.Value.ToString ());
                }
            }
            xwriter.WriteEndElement();
        }
        void ICoreDeserializable.Deserialize(IXMLDeserializer xreader)
        {
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent(); 
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        switch(xreader.Name)
                        {
                            case "TargetSurface":
                            case CoreConstant.TAG_SURFACETYPE:
                                {
                                    this.Add(CoreConstant.TAG_SURFACETYPE,
                                    xreader.ReadString(),null);
                                }
                                break;
                            default :
                                ICoreProjectItem c = xreader.CreateWorkingObject(xreader.Name) as ICoreProjectItem;
                                if (c == null)
                                {
                                    ProjectElement.DummyProjectContainer cont = new ProjectElement.DummyProjectContainer(xreader.Name, this);
                                    cont.Deserialize(xreader.ReadSubtree());
                                    this.Add(cont.Id, cont);
                                }
                                else {
                                    c.Deserialize(xreader.ReadSubtree());
                                    this.Add(c.Id, c);
                                }
                                break;
                        }
                        break;
                    case System.Xml.XmlNodeType.EndElement:
                        break;
                }
            }
        }
        public int Count
        {
            get { return this.m_projectItems.Count; }
        }
   
        
        /// <summary>
        /// get the project item setting
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public ICoreProjectItem this[string key]
        {
            get {
                if (this.m_projectItems.ContainsKey(key))
                {
                    return this.m_projectItems[key];
                }
                else {
                    this.Add(key, string.Empty, null);
                    return this.m_projectItems[key];
                }
            }
        }
        /// <summary>
        /// get if this project contains
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key){
            return (this.m_projectItems .ContainsKey (key));
        }
        /// <summary>
        /// override this to create project actions list
        /// </summary>
        public virtual ICoreProjectActions Actions
        {
            get { return null; }
        }
        public void Add(string key, ICoreProjectItem item)
        {
            if (this.m_projectItems.ContainsKey(key))
            {
                this.m_projectItems[key] = item;
            }
            else
                this.m_projectItems.Add(key, item);
        }
        /// <summary>
        /// add string key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value, EventHandler valuechanged)
        {
            if (value == null)
            {
                if (this.m_projectItems.ContainsKey(key))
                {
                    this.m_projectItems[key].Release();
                    this.m_projectItems.Remove(key);
                }
            }
            else {
                if (this.m_projectItems.ContainsKey(key))
                {
                    this.m_projectItems[key].Value = value;
                }
                else {
                    this.m_projectItems.Add(key, new CoreValueProjectItem(key, value, this, valuechanged));
                }
            }
        }
        public void Clear()
        {
            this.m_projectItems .Clear ();
        }
        public void Copy(ICoreProject prInfo)
        {
            this.CopyDefinition(prInfo);
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_projectItems.GetEnumerator ();
        }
        /// <summary>
        /// represent a base item project
        /// </summary>
        public class CoreProjectItemBase : CoreWorkingObjectBase, ICoreProjectItem  
        {
            ICoreProject m_Owner;
            private string m_value;
            string m_id;
            /// <summary>
            /// get the owner project
            /// </summary>
            public ICoreProject Owner
            {
                get { return this.m_Owner; }
                internal set { this.m_Owner = value; }
            }
            internal protected CoreProjectItemBase(string id, ICoreProject project) : this()
            {
                this.m_Owner = project;                                
                this.m_id = id;
            }
            protected CoreProjectItemBase()
            {
                this.m_value = null;
            }
            public override string ToString()
            {
                return string.Format("ProjectItem:[{0},{1}]", this.Id, this.Value );
            }
            public virtual string Value
            {
                get
                {
                    return m_value;
                }
                set
                {
                    if (this.m_value != value)
                    {
                        this.m_value = value;
                        OnValueChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler ValueChanged;
            ///<summary>
            ///raise the ValueChanged 
            ///</summary>
            private void OnValueChanged(EventArgs e)
            {
                if (ValueChanged != null)
                    ValueChanged(this, e);
            }
            public event EventHandler ChildAdded;
            public event EventHandler ChildRemoved;
            ///<summary>
            ///raise the ChildRemoved 
            ///</summary>
            protected virtual void OnChildRemoved(EventArgs e)
            {
                if (ChildRemoved != null)
                    ChildRemoved(this, e);
            }
            ///<summary>
            ///raise the ChildAdded 
            ///</summary>
            protected virtual void OnChildAdded(EventArgs e)
            {
                if (ChildAdded != null)
                    ChildAdded(this, e);
            }
            public ICoreProjectItemCollections this[string key]
            {
                get { return null; }
            }
            protected internal override void Serialize(IXMLSerializer xwriter)
            {
                xwriter.WriteValue(this.Value);
            }
            public virtual void Deserialize(IXMLDeserializer xreader)
            {
                this.m_value = xreader.ReadString();
            }
            public override string Id
            {
                get { return this.m_id; }
                set { this.m_id = value; }
            }
            public override  object Clone()
            { 
                CoreProjectItemBase i = new CoreProjectItemBase();
                i.m_id = this.m_id ;
                if (!string.IsNullOrEmpty(this.Value))
                    i.m_value = this.Value.Clone() as string;
                    return i;
            }
            public virtual void Release()
            {
                //free value changed
                this.ValueChanged = null;
                this.Owner = null;
            }
        }
        public class CoreProjectContainerItemBase : CoreProjectItemBase , ICoreProjectItemContainer 
        {
            Dictionary<string, ICoreProjectItem> m_items;
            protected  CoreProjectContainerItemBase(string name, ICoreProject project):base(name, project )
            {
                this.m_items = new Dictionary<string, ICoreProjectItem>();
            }
            public CoreProjectContainerItemBase()
                : base()
            {
                this.m_items = new Dictionary<string, ICoreProjectItem>();
            }
            public void Add(string key, string value)
            {
                if (this.m_items.ContainsKey(key))
                    return;
                CoreProjectItemBase sr = new CoreProjectItemBase(key, this.Owner);
                sr.Value = value;
                this.m_items.Add(key, sr);
            }
            public ICoreProjectItem[] GetChildsToArray(string key)
            {
                if (this.ContainsKey(key))
                    return this[key].ToArray();
                return null;
            }
            public void Add(string key, ICoreProjectItem item)
            {
                if (!string.IsNullOrEmpty (key) && (item != null) && !this.ContainsKey(key))
                    this.m_items.Add(key, item);
            }
            public void Remove(string key)
            {
                this.m_items.Remove(key);
            }
            public string[] GetKeys()
            {
                return this.m_items.Keys.ToArray<string>();
            }
            public int Count
            {
                get { return this.m_items.Count; }
            }
            public bool HasChild
            {
                get { return (this.Count >0); }
            }
            public bool ContainsKey(string key)
            {
                if (string.IsNullOrEmpty(key))
                    return false;
                return this.m_items.ContainsKey (key );
            }
            public override object Clone()
            {
                CoreProjectContainerItemBase i = new CoreProjectContainerItemBase();
                i.Id = this.Id ;
                 if (!string.IsNullOrEmpty (this.Value ))
                i.Value = this.Value.Clone() as string ;
                foreach (var item in this.m_items)
                {
                    i.m_items.Add(item.Key, item.Value.Clone() as ICoreProjectItem);
                }
                return i;            
            }
        }
        /// <summary>
        /// 
        /// </summary>
        internal sealed class DummyProjectContainer : CoreProjectItemBase
        {
            CoreXmlElement node;
            internal  DummyProjectContainer(string name, ICoreProject project)
                : base(name, project)
            {
            }
            public override void Deserialize(IXMLDeserializer xreader)
            {
                node = new CoreXmlElement("dummy");
                CoreXmlElement c = null;
                if (xreader.NodeType== System.Xml.XmlNodeType.None )
                    xreader.MoveToContent();
                while (xreader.Read ())
                {
                    switch (xreader.NodeType)
                    { 
                        case  System.Xml.XmlNodeType.Element :
                            c = CoreXmlElementBase.CreateXmlNode(xreader.Name) as CoreXmlElement ;
                            node.AddChild(c);
                            if (xreader.HasAttributes) {
                                xreader.MoveToFirstAttribute();
                                for (int i = 0; i < xreader.AttributeCount; i++)
                                {
                                    c[xreader.Name] = xreader.Value;
                                    xreader.MoveToNextAttribute();
                                }
                                xreader.MoveToElement ();
                            }
                            if (!xreader.IsEmptyElement)
                            {
                                c.Content  = xreader.ReadString();
                            }
                           // this.Add (c["Id"] , new XmlPropertyItem(c));
                            break;
                        case System.Xml.XmlNodeType.Text :
                            if (c!=null)
                            c.Content = xreader.Value;
                            break;
                    }
                }
            }
            public override object Clone()
            {
                return base.Clone();
            }
        }
        /// <summary>
        /// represent a xml property item
        /// </summary>
        sealed class XmlPropertyItem : CoreProjectItemBase, IXmlProjectPropertyItem
        {
            CoreXmlElement m_node;
            public XmlPropertyItem(CoreXmlElement node):base(node.TagName,null)
            {
                this.m_node = node;
            }
            public override string ToString()
            {
                return base.ToString();
            }
            public new string this[string attribute]
            {
                get
                {
                    return this.m_node[attribute];
                }
                set
                {
                    this.m_node[attribute] = value;
                }
            }
        }
        sealed class CoreValueProjectItem : CoreProjectItemBase 
        {
            public CoreValueProjectItem(string key, string value, ICoreProject project, EventHandler valuechanged):base(key, project )
            {
                this.Value = value;
                if (valuechanged !=null)
                this.ValueChanged += valuechanged;
            }
        }
        /// <summary>
        /// represent a project item
        /// </summary>
        class CoreProjectItem : CoreWorkingObjectBase ,  ICoreProjectItem
        {
            public ICoreProject Owner
            {
                get {return this.projectElement; }
            }
            public event EventHandler ValueChanged;
            private ProjectElement projectElement;
            private string key;
            private string m_value;
            public CoreProjectItem(ProjectElement projectElement, string key, string value)
            {
                this.projectElement = projectElement;
                this.key = key;
                this.m_value = value;
            }
            public string Value
            {
                get
                {
                    return m_value;
                }
                set
                {
                    this.m_value = value;
                    OnValueChanged(EventArgs.Empty);
                }
            }
            private void OnValueChanged(EventArgs eventArgs)
            {
                if (this.ValueChanged != null)
                    this.ValueChanged(this, eventArgs);
            }
            public void Release()
            {
            }
            protected internal override void Serialize(IXMLSerializer xwriter)
            {
                base.Serialize(xwriter);
            }
            public void Deserialize(IXMLDeserializer xreader)
            {
            }
       
           
        }
    }
}

