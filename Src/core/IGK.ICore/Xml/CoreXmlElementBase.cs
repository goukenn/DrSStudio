

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXmlElementBase.cs
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
file:CoreXmlElementBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Xml
{
    public abstract class CoreXmlElementBase : CoreWorkingObjectBase, ICloneable, IXmlElement 
    {
        private object m_Tag;
        public virtual IXmlChildCollections Childs { get { return null; } }
        public virtual bool Visible { get { 
            return true;
        } }
        public abstract string TagName{
            get;
        }
        public bool IsChildOf(CoreXmlElementBase c)
        {
            if (c == null)
                return false;

            var q = this.Parent;
            while (q != null) { 
                if(q == c)
                    return true ;
                q = q.Parent;
            }
            return false;
        }


        public abstract bool AddChild(CoreXmlElementBase child);
        /// <summary>
        /// get or set the associated tag
        /// </summary>
        public object Tag
        {
            get { return m_Tag; }
            set
            {
                if (m_Tag != value)
                {
                    m_Tag = value;                    
                }
            }
        }
        private CoreXmlElementBase  m_Parent;

        /// <summary>
        /// get or set the walue of this item
        /// </summary>
        public abstract string Value
        {
            get;
            set;
        }
        public CoreXmlElementBase  Parent
        {
            get { return m_Parent; }
            protected internal set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }
        public static CoreXmlElementBase CreateXmlNode(string name)
        {
            return new CoreXmlElement(name);
        }
        public static CoreXmlElementBase CreateComment(string value)
        {
            CoreXmlCommentElement comment = new CoreXmlCommentElement();
            comment.Value = value;
            return comment;
        }
        public static CoreXmlElement CreateDocType()
        {
            return new CoreXmlDocType();
        }
        public abstract string RenderXML(IXmlOptions option);
        public abstract CoreXmlElementBase[] getElementsByTagName(string tagname);
        public abstract CoreXmlElementBase getElementById(string id);
        public abstract CoreXmlAttributeValue this[string key]{get;set;}
        public virtual bool CanAddChild{get{return false;}}

        public override object  Clone()
        {
            var c = CoreXmlElement.CreateXmlNode("dummy");
            c.LoadString(this.RenderXML(null));
            if (c.HasAttributes || c.HasChild )
                return c.Childs[0];
            return null;
        }


        public void Remove(CoreXmlElementBase xmlElement)
        {
            if ((this.Childs != null) && (xmlElement !=null))
                this.Childs.Remove(xmlElement);
        }
    }
}

