

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlTypes.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlTypes : WsdlItem 
    {
        WsdlSchema m_schema;

        [WsdlElementAttribute()]
        public WsdlSchema Schema { get { return this.m_schema; } }
        public WsdlTypes()
        {
            this.m_schema = new WsdlSchema();
        }
        
        public override string TagName
        {
            get
            {
                return  WsdlConstant .TYPES_TAG_NAME;
            }
        }
        protected internal override void LoadProperties(IGK.ICore.Xml.CoreXmlElement c)
        {
            base.LoadProperties(c);            
        }
        /// <summary>
        /// add a empty types with no message response
        /// </summary>
        /// <param name="p"></param>
        public void Add(string p)
        {
            WsdlType c = new WsdlEmptyType();
            c.Name = p;
            this.m_schema.AddType(c);
            
        }
        /// <summary>
        /// add a type 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        public void Add(string name, Type type, string paramName)
        {
            if (type == null)
                return;
            WsdlType c = null;
            if (type.IsPrimitive || (type == typeof (string)))
            {
                c = WsdlPrimitiveType.Create(type, paramName);
            }
            else
            {
            }
            if (c != null)
            {
                c.Name = name;
                this.m_schema.AddType(c);
            }
        }

        public void AddString(string messageName, string paramName)
        {
            Add(messageName, typeof(string), paramName );
        }

        public void AddArrayString(string messageName, string paramName)
        {
            WsdlPrimitiveType c = WsdlPrimitiveType.Create(typeof(string), paramName);
           
            if (c != null)
            {
                c.Name = messageName;
                c.OutBounded = true;
                this.m_schema.AddType(c);
            }
        }
        /// <summary>
        /// add  an array complex type to document
        /// </summary>
        /// <param name="nameOftheElement">display name</param>
        /// <param name="targetCompletType">taget type </param>
        /// <param name="paramName">argument name</param>
        public void AddArray(string nameOftheElement, WsldComplexType targetCompletType, string paramName)
        {
            WsdlElementType c = WsdlElementType.Create(targetCompletType, paramName);

            if (c != null)
            {
                c.Name = nameOftheElement;
                c.OutBounded = true;
                this.m_schema.AddType(c);
            }
        }
        /// <summary>
        /// add  a complex type to document
        /// </summary>
        /// <param name="nameOftheElement">display name</param>
        /// <param name="targetCompletType">taget type </param>
        /// <param name="paramName">argument name</param>
        public void Add(string nameOftheElement, WsldComplexType targetCompletType, string paramName)
        {
            WsdlElementType c = WsdlElementType.Create(targetCompletType, paramName);

            if (c != null)
            {
                c.Name = nameOftheElement;
                c.OutBounded = false;
                this.m_schema.AddType(c);
            }
        }
        public WsldComplexType addComplexType(string p)
        {
            WsldComplexType t = new WsldComplexType();
            t.Name = p;
            this.m_schema.AddType(t);
            return t;
        }


      
    }
}
