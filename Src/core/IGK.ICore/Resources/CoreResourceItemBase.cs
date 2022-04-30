

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreResourceItemBase.cs
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
file:CoreResourceItemBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
namespace IGK.ICore.Resources
{
    /// <summary>
    /// represent a resource factory that manage creation of resources items
    /// </summary>
    public abstract class CoreResourceItemBase : CoreWorkingObjectBase ,  ICoreResourceItem
    {
        private ICoreResourceContainer m_resourceContainer;
        private CoreResourceReferenceCollections m_references;
        /// <summary>
        /// get the resource container
        /// </summary>
        public ICoreResourceContainer ResourceContainer {
            get {
                return this.m_resourceContainer;
            }
        }
        internal CoreResourceReferenceCollections References
        {
            get { return this.m_references; }
        }
        ICoreResourceReferenceCollections ICoreResourceItem.References
        {
            get { return this.m_references; }
        }
        /// <summary>
        /// get if this resource item is registered
        /// </summary>
        /// <returns></returns>
        public bool IsRegistered(ICoreResourceContainer container)
        {
            //if (((this.m_resourceContainer.Contains(container)) && container.Contains(this.Id)))
            //{
                return (container.GetResourceById(this.Id) == this);
            //}
            //return false;
        }
        public bool IsRegistered()
        {
            return (this.m_resourceContainer?.Count > 0);
        }
        internal protected virtual void Load(IGK.ICore.Codec.IXMLDeserializer xreader)
        {
        }
        protected CoreResourceItemBase() {
            this.m_references = new CoreResourceReferenceCollections(this);
            this.m_resourceContainer = null;// new List<ICoreResourceContainer>();
        }
        public static CoreResourceItemBase CreateResource(string name, string id)
        {
            CoreResourceItemBase fc = null;
            string v_t = string.Format(string.Format("{0}.{1}Resource", MethodInfo.GetCurrentMethod().DeclaringType.Namespace,
                name));
            Type t = Type.GetType(v_t, false, true);
            if (t != null)
            {
                ConstructorInfo ctr = t.GetConstructor(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance,
                    null, new Type[0], null);
                if (ctr!=null)
                    fc = ctr.Invoke(null) as CoreResourceItemBase;
                else 
                    fc = t.Assembly.CreateInstance(t.FullName) as CoreResourceItemBase;
                fc.m_id = id;
            }
            else
            {
                if (Enum.IsDefined(typeof(enuCoreResourceType), name))
                {
                    switch ((enuCoreResourceType)Enum.Parse(typeof(enuCoreResourceType), name))
                    {
                        case enuCoreResourceType.Bitmap:
                            break;
                        case enuCoreResourceType.Brush:
                            break;
                        case enuCoreResourceType.SoundFile:
                            break;
                        case enuCoreResourceType.VideoFile:
                            break;
                        case enuCoreResourceType.File:
                            break;
                        case enuCoreResourceType.BinaryData:
                            break;
                        case enuCoreResourceType.String:
                            fc = new StringResource();
                            break;
                        case enuCoreResourceType.Font:
                            break;
                        default:
                            break;
                    }
                }
            }
            return fc;
        }
        /// <summary>
        /// create a resource items
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static CoreResourceItemBase CreateResource(enuCoreResourceType type, string id)
        {
            return CreateResource(type.ToString(), id);
        }
        public abstract enuCoreResourceType ResourceType
        {
            get;
        }
        public abstract object GetData();
        private string m_id;
        /// <summary>
        /// get the id of this resources
        /// </summary>
        public override string Id
        {
            get {
                if (this.m_id == null)
                    this.m_id = "Resource_"+this.GetHashCode();
                return this.m_id;}
            set { this.m_id = value; }
        }
        /// <summary>
        /// register a resource container. pass null to unregister
        /// </summary>
        /// <param name="container"></param>
        /// <returns>true if operation done</returns>
        public virtual bool Register(ICoreResourceContainer container)
        {
            if (container == null)
                return false;
            if (this.IsRegistered(container))
                return true;
            this.m_resourceContainer = container;
             return container.Register(this);
        }
        public abstract string GetDefinition();
        /// <summary>
        /// get the reference collection
        /// </summary>
        internal class CoreResourceReferenceCollections :  ICoreResourceReferenceCollections
        {
            CoreResourceItemBase owner;
            List<ICoreWorkingObject> m_references;
            public CoreResourceReferenceCollections(CoreResourceItemBase owner)
            {
                this.owner = owner;
                this.m_references = new List<ICoreWorkingObject>();
            }
            public int Count
            {
                get { return this.m_references.Count; }
            }
            public bool IsReference(ICoreWorkingObject obj)
            {
                if (obj == null)
                    return false;
                return this.m_references.Contains(obj);
            }
            internal void Add(ICoreWorkingObject target)
            {
                if ((target == null)||(this.m_references.Contains (target )))
                    return;
                this.m_references.Add(target);
            }
            internal void Remove(ICoreWorkingObject target)
            {
                if (target == null) return;
                if (this.m_references.Contains(target))
                this.m_references.Remove(target);
            }
            public override string ToString()
            {
                return string.Format("References [#{0}]", this.m_references.Count);
            }
        }
        /// <summary>
        /// clone objetct
        /// </summary>
        /// <returns></returns>
        public override  object Clone()
        {
            return this.MemberwiseClone();
        }

        internal protected virtual  void SetValue(string readvalue)
        {

        }


        public virtual  bool IsMatch(string stringDataIdentifier)
        {
            return this.GetDefinition() == stringDataIdentifier;
        }
    }
}

