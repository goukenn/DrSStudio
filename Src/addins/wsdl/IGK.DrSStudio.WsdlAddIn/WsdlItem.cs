

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    /// <summary>
    /// represent a wdsl item
    /// </summary>
    public abstract class WsdlItem
    {
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public virtual string TagName
        {
            get { return GetType().Name.Substring(4).ToLower(); }
        }
        public WsdlItem(string name):this()
        {
            this.m_Name = name;
        }
        public WsdlItem():base()
        {
            this.m_Name = this.GetType().Name + "_" + this.GetHashCode() ;
        }

        public CoreXmlElement GetNode()
        {
            if (string.IsNullOrEmpty(this.TagName))
                return null;
            CoreXmlElement c = CoreXmlElement.CreateXmlNode(this.TagName);
            c["name"] = this.Name;
            this.LoadProperties(c);
            return c;

        }

        protected internal virtual void LoadProperties(CoreXmlElement c)
        {
            foreach (PropertyInfo prInfo in this.GetType().GetProperties())
            {
                if (WsdlAttributeAttribute.IsWsdlAttribute(prInfo))
                { 
                    object o = prInfo.GetValue (this);
                    if (o is WsdlItem)
                        c[prInfo.Name] = "tns:" + (o as WsdlItem).Name;
                    else if (o != null)
                        c[prInfo.Name] = o.ToString();
                }else
                    if (WsdlElementAttribute.IsWsdlElement(prInfo))
                    {
                        {
                            WsdlItem r = prInfo.GetValue(this) as WsdlItem;
                            if (r != null)
                                c.AddChild(r.GetNode());
                        }
                    }

            }
        }
    }
}
