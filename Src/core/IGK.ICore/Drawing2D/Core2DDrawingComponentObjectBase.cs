
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingComponentObjectBase.cs
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
file:Core2DDrawingComponentObjectBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;
using IGK.ICore.Resources;
using System.ComponentModel;
using IGK.ICore.Dependency;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a base component base object
    /// </summary>
    public class Core2DDrawingComponentObjectBase : CoreDependencyObject, ICoreResourceItem
    {
        private ICoreResourceReferenceCollections m_resourceReferences;

        public Core2DDrawingComponentObjectBase():base()
        {
            this.m_resourceReferences = new CoreResourceReferenceCollections(this);
        }

        /// <summary>
        /// get the reference collection
        /// </summary>
        internal class CoreResourceReferenceCollections : IGK.ICore.Resources.CoreResourceReferenceCollections, 
            ICoreResourceReferenceCollections
        {
            private Core2DDrawingComponentObjectBase owner;
            private List<ICoreWorkingObject> m_references;
            public CoreResourceReferenceCollections(Core2DDrawingComponentObjectBase owner): base(owner )
                
            {
                this.owner = owner;
                this.m_references = new List<ICoreWorkingObject>();
            }
        }
        /// <summary>
        /// get the parent resources container. not yet implement
        /// </summary>
        [Browsable(false)]
        public virtual ICoreResourceContainer ResourceContainer
        {
            get {
                return null;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        /// <summary>
        /// get the reference collection
        /// </summary>
        public ICoreResourceReferenceCollections References
        {
            get {
                return m_resourceReferences;
            }
        }
        /// <summary>
        /// indicate the resources types
        /// </summary>
        [Browsable(false)]
        public virtual enuCoreResourceType ResourceType
        {
            get { return enuCoreResourceType.CoreObject; }
        }

        public object GetData()
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered()
        {
            return false;
        }

        public bool IsRegistered(ICoreResourceContainer container)
        {
            return (container.Contains(this.Id) &&
                container.GetResourceById(this.Id) == this);
        }

        public virtual bool Register(ICoreResourceContainer container)
        {
            if (container == null)
                return false;
            if (this.IsRegistered(container))
                return true;
            //if (!this.m_resourceContainer.Contains(container))
            //    this.m_resourceContainer.Add(container);
            return container.Register(this);
        }

        public virtual string GetDefinition()
        {
            return string.Empty;
        }
        public virtual bool IsMatch(string stringDataIdentifier)
        {
            return false;
        }
    }
}

