using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.Xml
{
    /// <summary>
    /// reprensent a base balafon xml item element
    /// </summary>
    public abstract class BalafonCoreXmlElement : CoreXmlElement, ICoreXmlValueElementContainer<BalafonCoreXmlValueElement>
    {
        public BalafonCoreXmlElement(string tagname):base(tagname )
        {

        }

        Dictionary<string, BalafonCoreXmlValueElement> m_velements = new Dictionary<string, BalafonCoreXmlValueElement>();

        public bool IsChildElementProperty(string name)
        {
            var s = GetType().GetProperty(name);
            if (s == null)
                return false;
            return true;
        }
        public BalafonCoreXmlValueElement GetElementPropertyChild(string name)
        {
            if (!IsChildElementProperty(name))
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
        public string GetElementProperty(string name)
        {
            if (!IsChildElementProperty(name))
                return null;
            if (m_velements.ContainsKey(name))
            {
                var p = m_velements[name].Content;
                return p != null ? p.ToString() : null;
            }
            return null;
        }
        /// <summary>
        /// set element property. property must be also defined in the class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void SetElementProperty(string name, string value)
        {
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
                    var t = new BalafonCoreXmlValueElement(name)
                    {
                        Content = value
                    };
                    this.Childs.Add(t);
                    m_velements.Add(name, t);
                }
            }
        }
        /// <summary>
        /// remove all properties group group this project
        /// </summary>
        protected virtual void ClearPropertyGroup()
        {
           
        }
        
    }
}
