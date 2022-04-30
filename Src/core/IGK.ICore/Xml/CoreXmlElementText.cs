

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlElementText.cs
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
file:XmlElementText.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Xml
{
    public class CoreXmlElementText : CoreXmlElementBase 
    {
        private string m_Text;
        public override bool CanAddChild
        {
            get
            {
                return false;
            }
        }
        public override string Value
        {
            get { return this.m_Text; }
            set {
                this.m_Text = value;
            }
        }
        /// <summary>
        /// get or set the text
        /// </summary>
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                }
            }
        }
        public override string RenderXML(IXmlOptions option)
        {
            return this.m_Text;
        }
        public override CoreXmlElementBase[] getElementsByTagName(string tagname)
        {
            return null;
        }
        public override CoreXmlElementBase getElementById(string id)
        {
            return null;
        }
        public override CoreXmlAttributeValue  this[string key]
        {
            get
            {
                return null;
            }
            set
            {
            }
        }
        public override bool AddChild(CoreXmlElementBase child)
        {
            return false;
        }

        public override string TagName
        {
            get { return string.Empty; }
        }
    }
}

