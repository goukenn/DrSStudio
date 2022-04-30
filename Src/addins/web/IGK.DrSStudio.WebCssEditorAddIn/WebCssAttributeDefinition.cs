

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssAttributeDefinition.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Xml;

namespace IGK.DrSStudio.WebCssEditorAddIn
{
    /// <summary>
    /// represent attribute definitions
    /// </summary>
    public class WebCssAttributeDefinition : IWebCssProperty 
    {
        private string m_Name;
        private string m_PropertyType;
        private string m_Description;
        private string m_Version;
        private string m_Category;
        private string[] m_values;

        /// <summary>
        /// get the supported values;
        /// </summary>
        /// <returns></returns>
        public string[] GetValues()
        {
            return this.m_values;
        }
        public string Category
        {
            get { return m_Category; }
            set
            {
                if (m_Category != value)
                {
                    m_Category = value;
                }
            }
        }
        public string Version
        {
            get { return m_Version; }
            set
            {
                if (m_Version != value)
                {
                    m_Version = value;
                }
            }
        }
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                }
            }
        }
        public string PropertyType
        {
            get { return m_PropertyType; }
            set
            {
                if (m_PropertyType != value)
                {
                    m_PropertyType = value;
                }
            }
        }
        /// <summary>
        /// get or set the name of the css property
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        internal virtual void UpdateValue(Dictionary<string, WebCssAttributeDefinition> sm_attribs)
        {
            /*
             * 
             * 
             * update properties requirement values
             * 
             * */

            if (this.Name == "display")
            { 
            }

            if (this.PropertyType == null)
                return;
            string[] v_tt = this.PropertyType.Split ('|');
            string v_t = string.Empty ;
            if ((v_tt !=null) && (v_tt.Length > 0))
            {
                List<string> rv = new List<string>();
                for (int i = 0; i < v_tt.Length; i++)
                {
                    v_t = v_tt[i];
                    if (!string.IsNullOrEmpty(v_t) &&
              sm_attribs.ContainsKey(v_t))
                    {
                        WebCssEnumValueAttributeDefinition d = sm_attribs[v_t] as WebCssEnumValueAttributeDefinition;
                        if (d != null)
                        {
                            rv.AddRange(d.m_values);
                        }

                    }
                }
                this.m_values = rv.ToArray();
          
            }
        }


        internal class WebCssEnumValueAttributeDefinition : WebCssAttributeDefinition 
        {
            public WebCssEnumValueAttributeDefinition(string name)
            {
                this.Name = name;
                this.PropertyType = "cssEnumValues";

               
            }
            internal override void UpdateValue(Dictionary<string, WebCssAttributeDefinition> sm_attribs)
            {                
                //do nothing
            }
            public override string ToString()
            {
                return base.ToString();
            }
            internal void Load(IGK.ICore.Xml.CoreXmlElement element)
            {
                if (element.HasChild)
                {
                    this.m_values = new string[element.Childs.Count];
                    int i = 0;
                    foreach (CoreXmlElement item in element.Childs)
                    {
                        this.m_values[i] = CoreXmlUtility.GetStringValue(item.Content);
                        i++;

                    }
                }
            }
        }
    }
}
