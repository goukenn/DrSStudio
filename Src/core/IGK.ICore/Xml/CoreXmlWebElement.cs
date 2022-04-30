using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Xml
{
    public class CoreXmlWebElement : CoreXmlElement 
    {
        protected CoreXmlWebElement()
            : base()
        { 
        }
        public CoreXmlWebElement(string s):base(s)
        {

        }
        protected override IXmlOptions CreateXmlOptions()
        {
            return new CoreXmlWebOptions();
        }

        /// <summary>
        /// create a xml node
        /// </summary>
        /// <param name="tagName">tag name. empty or null is not allowed </param>
        /// <returns></returns>
        public new static CoreXmlWebElement CreateXmlNode(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return null;
            Type t = Type.GetType(string.Format(CoreConstant.XMLWEBELEMENT_CLASS_FORMAT , tagName), false, true);
            if ((t != null) && !t.IsAbstract)
            {
                return t.Assembly.CreateInstance(t.FullName) as CoreXmlWebElement;
            }
            switch (tagName.ToLower())
            {
                case "script":
                    return new CoreXmlWebScriptElement();
                case "link":
                    return new CoreXmlWebLinkElement();

            }
            return new CoreXmlWebElement(tagName);
        }
        public override CoreXmlElement CreateChildNode(string tagName)
        {
            return CreateXmlNode(tagName);
        }
    }
}
