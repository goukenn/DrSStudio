

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlSettingOptions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.ICore.Xml
{
    /// <summary>
    /// Represent the base xml setting options
    /// </summary>
    public class CoreXmlSettingOptions : IXmlOptions 
    {
        private int m_depth;
        private IXmlNameSpaceCollections m_namespces;
        public int Depth
        {
            get { return this.m_depth; }
            set
            {
                if (value >= 0)
                    this.m_depth = value;

            }
        }
        public virtual bool Indent
        {
            get;
            set;
        }
          /// <summary>
          /// get or set the target context
          /// </summary>
        public virtual string Context
        {
            get;
            set;
        }

        public virtual bool MustCloseTag(string tagName)
        {
            return false;
        }


        public IXmlNameSpaceCollections Namespace
        {
            get { return this.m_namespces; }
        }

        public CoreXmlSettingOptions()
        {
            this.m_namespces = CreateNameSpaceCollection();
        }

        protected virtual IXmlNameSpaceCollections CreateNameSpaceCollection()
        {
            return new XmlNameSpaceCollections(this);
        }

        protected class XmlNameSpaceCollections : IXmlNameSpaceCollections
        {
            private CoreXmlSettingOptions m_option;
            private Dictionary<string, string> m_values;
            private Dictionary<CoreXmlElementBase, List<string>> m_parents;

            public XmlNameSpaceCollections(CoreXmlSettingOptions option)
            {
                this.m_option = option;
                this.m_values = new Dictionary<string, string>();
                this.m_parents = new Dictionary<CoreXmlElementBase, List<string>>();
            }

            public int Count
            {
                get { return this.m_values.Count; }
            }

            public bool IsNamespacePrefix(string name)
            {
                return Regex.IsMatch(name, "xmlns(:[a-z0-9]+){0,1}", RegexOptions.IgnoreCase);
            }

            public bool ContainsPrefix(string name)
            {
                return this.m_values.ContainsKey(name);
            }

            public void Add(CoreXmlElement owner, string name, string value)
            {
                if (!string.IsNullOrEmpty (name )&& !string.IsNullOrEmpty (value )  && !ContainsPrefix(name))
                {
                    this.m_values.Add(name, value);
                    if (this.m_parents.ContainsKey(owner))
                        this.m_parents[owner].Add(name);
                    else
                        this.m_parents.Add(owner, new List<string>()
                    {
                        name 
                    });
                }
            }
            public void Remove(CoreXmlElement owner)
            {
        if (this.m_parents.ContainsKey (owner ) ==false )
                return ;
        var s = this.m_parents[owner];
        foreach (string item in s)
        {
            this.m_values.Remove(item);
        }
        this.m_parents.Remove(owner);
            }
        }
    }
}
