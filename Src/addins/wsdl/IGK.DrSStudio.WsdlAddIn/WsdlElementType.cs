

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WsdlElementType.cs
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
using System.Text;

namespace IGK.DrSStudio.Wsdl
{
    [WsdlXSDElement ()]
    public class WsdlElementType : WsdlType
    {
        private WsldComplexType m_targetType;

        public WsldComplexType targetType
        {
            get { return m_targetType; }
            set
            {
                if (m_targetType != value)
                {
                    m_targetType = value;
                }
            }
        }
        public override string TagName
        {
            get
            {
                return "s:element";
            }
        }
        protected internal override void LoadProperties(CoreXmlElement c)
        {
            base.LoadProperties(c);

            var e = c.Add("s:complexType").Add ("s:sequence").Add("s:element");

            if (targetType != null)
            {
                e["type"] = "tns:" + targetType.Name;
            }
            e["name"] = this.Name;
            if (this.OutBounded)
            {
                e["minOccurs"] = 0;
                e["maxOccurs"] = WsdlConstant.UNBOUNDED ;
            }
        }
        public WsdlElementType()
        {

        }
        /// <summary>
        /// create a wsdl element tpye
        /// </summary>
        /// <param name="targetCompletType"></param>
        /// <param name="paramName"></param>
        /// <returns></returns>
        internal static WsdlElementType Create(WsldComplexType targetCompletType, string paramName)
        {
            WsdlElementType c = new WsdlElementType();
            c.targetType = targetCompletType;
            c.Name = paramName;
            return c;
        }

        public bool OutBounded { get; set; }
    }
}
