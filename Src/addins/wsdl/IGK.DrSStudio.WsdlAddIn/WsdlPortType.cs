

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlPortType.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    public class WsdlPortType :WsdlItem 
    {
        /// <summary>
        /// oepration collections
        /// </summary>
        public class WsdlOperationCollection : IEnumerable 
        {
            List<WsdlOperation> m_operations;
            private WsdlPortType m_owner;

            public WsdlOperationCollection(WsdlPortType owner)
            {
                this.m_owner = owner;
                this.m_operations = new List<WsdlOperation>();
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_operations.GetEnumerator();
            }

            public int Count
            {
                get { return this.Count; }
            }
            internal void Add(WsdlOperation operation)
            {
                this.m_operations.Add(operation);
                
            }
            /// <summary>
            /// add a soap operation
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            internal WsdlOperation Add(string name)
            {
                WsdlOperation sp = new WsdlOperation();
                sp.Name = name;
                this.Add(sp);
                return sp;
            }
        }
        private WsdlOperationCollection m_operations;

        public WsdlOperationCollection operations
        {
            get { return m_operations; }
        }
        public override string TagName
        {
            get
            {
                return WsdlConstant.PORTYPE_TAG_NAME;
            }
        }
        public WsdlPortType():base()
        {
            this.m_operations = new WsdlOperationCollection(this);
        }
        public override string ToString()
        {
            return base.ToString();
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            foreach (WsdlItem  item in this.m_operations)
            {
                c.AddChild(item.GetNode());
            }
        }
    }
}
