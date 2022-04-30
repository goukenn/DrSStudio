

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreXmlAttributeValue.cs
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
file:CoreXmlAttributeValue.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Xml
{
    public class CoreXmlAttributeValue : ICoreXmlGetValueMethod
    {
        private object m_value;
        public object Value
        {
            get { return m_value; }
            set
            {
                if (m_value != value)
                {
                    m_value = value;
                }
            }
        }
        
    /// <summary>
    /// .ctr
    /// </summary>
        public CoreXmlAttributeValue()
        { 
        }
        public static implicit operator CoreXmlAttributeValue(string value)
        {
            CoreXmlAttributeValue v = new CoreXmlAttributeValue();
            v.m_value = value;
            return v;
        }
        public static implicit operator CoreXmlAttributeValue(int value)
        {
            CoreXmlAttributeValue v = new CoreXmlAttributeValue();
            v.m_value = value;
            return v;
        }
        public static implicit operator CoreXmlAttributeValue(bool value)
        {
            CoreXmlAttributeValue v = new CoreXmlAttributeValue();
            v.m_value = value.ToString();
            return v;
        }
        public static implicit operator CoreXmlAttributeValue(float value)
        {
            CoreXmlAttributeValue v = new CoreXmlAttributeValue();
            v.m_value = value;
            return v;
        }
       
        public static implicit operator string (CoreXmlAttributeValue value)
        {
            if (value != null)
                return value.GetValue();
            return null;
        }
        public virtual string GetValue()
        {
            if (this.m_value !=null)
            return this.m_value.ToString ();
            return null;
        }
        public override string ToString()
        {
            if (m_value !=null)
                  return this.m_value.ToString () ;
            return base.ToString();
        }
    }
}

