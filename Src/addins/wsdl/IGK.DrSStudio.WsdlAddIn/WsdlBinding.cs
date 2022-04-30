

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlBinding.cs
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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    /// <summary>
    /// represent a binding element
    /// </summary>
    public class WsdlBinding : WsdlItem
    {
        private WsdlBindingOperationCollection m_operations;
        public override string TagName
        {
            get { return WsdlConstant.BINDING_TAG_NAME; }
        }
        [WsdlAttribute ()]
        public WsdlPortType type { get; set; }
        [WsdlElement ()]
        public WsdlSoapBinding soap { get; set; }

        public WsdlBinding()
        {
            this.m_operations = new WsdlBindingOperationCollection(this);
        }

        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            foreach (WsdlBindingOperation item in this.m_operations)
            {
                c.AddChild(item.GetNode());
            }
        }
        public class WsdlBindingOperationCollection : IEnumerable
        {
            private WsdlBinding wsdlBinding;
            List<WsdlBindingOperation> m_operations;
            public WsdlBindingOperationCollection(WsdlBinding wsdlBinding)
            {
                
                this.m_operations = new List<WsdlBindingOperation>();
                this.wsdlBinding = wsdlBinding;
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_operations.GetEnumerator();
            }
            /// <summary>
            /// add operation by name
            /// </summary>
            /// <param name="p"></param>
            internal void Add(WsdlBindingOperation p)
            {
                this.m_operations.Add(p);
            }
        }
        /// <summary>
        /// get the operations collections
        /// </summary>
        public WsdlBindingOperationCollection operations {
            get {
                return this.m_operations;
            }
        }
    }
}
