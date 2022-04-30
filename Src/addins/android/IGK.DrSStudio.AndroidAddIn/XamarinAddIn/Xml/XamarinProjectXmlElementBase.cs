using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    using IGK.ICore;
    public abstract class XamarinProjectXmlElementBase : CoreXmlElement, ICoreXmlValueElementContainer<XamarinSingleValueXmlElement>
    {
        Dictionary<string, XamarinSingleValueXmlElement> m_velements = new Dictionary<string, XamarinSingleValueXmlElement>();

        public bool IsChildElementProperty(string name)
        {
            var s = GetType().GetProperty(name);
            if (s == null)
                return false ;
            return true;
        }
        public XamarinSingleValueXmlElement GetElementPropertyChild(string name) {
            if (!IsChildElementProperty(name ))
                return null;
            if (m_velements.ContainsKey(name))
            {
                return m_velements[name];
            }
            return null;
        }
        /// <summary>
        /// get element property. properties attribute must be also in the class
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetElementProperty(string name) {
            if (!IsChildElementProperty(name))
                return null;
            if (m_velements.ContainsKey(name))
            {
                var p = m_velements[name].Content;
                return p!=null? p.ToString() : null;
            }
            return null;
        }
        /// <summary>
        /// set element property. property must be also defined in the class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetElementProperty(string name, string value) {
            if (!IsChildElementProperty(name))
                return;
            if (m_velements.ContainsKey(name))
            {
                if (string.IsNullOrEmpty(value))
                {
                    this.Childs.Remove(m_velements[name]);
                    m_velements.Remove(name);
                }
                else 
                    m_velements[name].Content = value;
            }
            else
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var t =new XamarinSingleValueXmlElement(name)
                    {
                        Content = value
                    };
                    this.Childs.Add ( t);
                    m_velements.Add(name, t);
                }
            } 
        }
        
        
        public XamarinProjectXmlElementBase(string tagname):base(tagname)
        {
            
        }
        public override string ToString()
        {
            return string.Format("Xamarin:[{0}]", this.TagName);
        }
       

        /// <summary>
        /// create a xml node
        /// </summary>
        /// <param name="tagName">tag name. empty or null is not allowed </param>
        /// <returns></returns>
        public new static XamarinProjectXmlElementBase CreateXmlNode(string tagName)
        {
            if (string.IsNullOrEmpty(tagName))
                return null;
            Type t = Type.GetType(string.Format(XamarinConstant.XMLELEMENT_CLASS_FORMAT, tagName), false, true);
            if ((t != null) && !t.IsAbstract)
            {
                return t.Assembly.CreateInstance(t.FullName) as XamarinProjectXmlElementBase;
            }
            return new XamarinProjectItemElement(tagName);
        }
        public override CoreXmlElement CreateChildNode(string tagName)
        {
            if (IsChildElementProperty(tagName))
            {
              
                var r = this.GetElementPropertyChild(tagName );
                if (r != null)
                {
                    //clear this child for loading 
                    r.Clear();
                    return r;
                }
                r = new XamarinSingleValueXmlElement(tagName) { 
                    Value = null
                };
                this.m_velements.Add(tagName, r);
                return r; 

            }

            return CreateXmlNode(tagName);
        }

       
    }
}
