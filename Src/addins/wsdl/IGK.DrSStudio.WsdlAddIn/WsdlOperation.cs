

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlOperation.cs
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
    public class WsdlOperation : WsdlItem 
    {
        private WsdlOperationParamsCollections m_fault;
        private WsdlOperationParamsCollections m_output;
        private WsdlOperationParamsCollections m_input;

        public WsdlOperationParamsCollections inputs { get { return m_input; } }
        public WsdlOperationParamsCollections outputs { get { return m_output; } }
        public WsdlOperationParamsCollections faults { get { return m_fault; } }
        public class WsdlOperationParamsCollections : IEnumerable  {
            private WsdlOperation wsdlOperation;
            List<WsdlOperationParam> op;
            private enuWsdlParamType m_operationType;
            public WsdlOperationParamsCollections(WsdlOperation wsdlOperation, enuWsdlParamType  p)
            {
                this.m_operationType = p;
                this.op = new List<WsdlOperationParam>();
                this.wsdlOperation = wsdlOperation;
            }

            public IEnumerator GetEnumerator()
            {
                return this.op.GetEnumerator();
            }
            internal void Add(WsdlOperationParam p)
            {
               this.op.Add(p);
            }
            /// <summary>
            /// add operation message
            /// </summary>
            /// <param name="outMsg"></param>
            internal void Add(string outMsg)
            {
                WsdlOperationParam p = new WsdlOperationParam(m_operationType);
                p.message = outMsg;
                this.op.Add(p);
            }
        }

        public WsdlOperation()
        {
            this.m_input = new WsdlOperationParamsCollections(this, enuWsdlParamType.input );
            this.m_output = new WsdlOperationParamsCollections(this, enuWsdlParamType.output );
            this.m_fault = new WsdlOperationParamsCollections(this, enuWsdlParamType.fault );
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);
            GetData(c, this.m_input);
            GetData(c, this.m_output);
            GetData(c, this.m_fault);
        }

        private void GetData(CoreXmlElement c, WsdlOperationParamsCollections e)
        {
            foreach (WsdlOperationParam item in e)
            {
                c.AddChild(item.GetNode());
            }
        }

        internal bool Support(enuWsdlOperationType enuWsdlOperationType)
        {
            return true;
        }
    }
}
