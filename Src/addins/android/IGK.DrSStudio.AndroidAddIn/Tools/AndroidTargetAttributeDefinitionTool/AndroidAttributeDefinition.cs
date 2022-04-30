

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidAttributeDefinition.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.DrSStudio.Android.Tools
{
    /// <summary>
    /// android attribute definitions
    /// </summary>
    public sealed class AndroidAttributeDefinition : IComparable
    {
        private string m_Name;
        private string m_Format; 
        private AndroidAttributeEnumValueCollection m_values;
        private List<AndroidAttributeDefinition> m_Childs;
        private string m_type;
        private string m_Parent;

        public string Parent
        {
            get { return m_Parent; }
            set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                }
            }
        }

        /// <summary>
        /// get or set the definition.
        /// </summary>
        public AndroidAttributeDefinition[] Childs
        {
            get { return m_Childs.ToArray(); }          
        }
        /// <summary>
        /// get the attribute definition types
        /// </summary>
        public string Type { get { return this.m_type; } }
        /// <summary>
        /// primitive default enum value
        /// </summary>
        public class AndroidAttributeEnumValueCollection : IEnumerable
        {
            List<AndroidAttributeEnumValue> m_t;
            private AndroidAttributeDefinition m_owner;

            public AndroidAttributeEnumValueCollection(AndroidAttributeDefinition def)
            {
                this.m_owner = def;
                this.m_t = new List<AndroidAttributeEnumValue>();
            }
            public void Add(AndroidAttributeEnumValue v)
            {
                this.m_t.Add(v);
            }
            public void Remove(AndroidAttributeEnumValue v)
            {
                this.m_t.Remove(v);
            }
            public int Count { get { return this.m_t.Count; } }
            public AndroidAttributeEnumValue[] ToArray() { return this.m_t.ToArray(); }


            public IEnumerator GetEnumerator()
            {
                return this.m_t.GetEnumerator();
            }
            public override string ToString()
            {
                return "AttirubtesDefinition :[Count: " + this.Count + "]";
            }

        }

        public string Format
        {
            get { return m_Format; }
            set
            {
                if (m_Format != value)
                {
                    m_Format = value;
                }
            }
        }
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
        public AndroidAttributeDefinition()
        {
            this.m_values = new AndroidAttributeEnumValueCollection(this);
            this.m_Childs = new List<AndroidAttributeDefinition>();
        }
        internal void UpdateInfo(Dictionary<string, AndroidAttributeDefinition> def)
        {
            var rc = this.m_Childs.ToArray();
            int index = 0;
            foreach (var item in rc)
            {
                if (def.ContainsKey(item.Name))
                {
                    index = this.m_Childs.IndexOf(item);
                    this.m_Childs.Remove(item);
                    this.m_Childs.Insert(index, def[item.Name]);
                }
                else {
                    System.Diagnostics.Debug.WriteLine("item : " + item.Name + " not found");
                }
            }
        }
        internal  void Load(XmlReader reader)
        {
            this.m_Format = reader.GetAttribute ("format");
            this.m_Name = reader.GetAttribute ("name");
            this.m_Parent = reader.GetAttribute("parent");
            if (!string.IsNullOrEmpty(this.m_Parent))
            { 
                this.m_Parent = string.Join ("|", Regex.Split (this.m_Parent , "\\s", RegexOptions.IgnorePatternWhitespace));
            }
            reader.MoveToElement();
            this.m_type = reader.Name;
            if (!reader.IsEmptyElement)
            {
                while (reader.Read())
                {
                    switch (reader.NodeType )
                    {
                        case XmlNodeType.Element:
                            switch(reader.Name)
                            {
                                case "enum":
                                case "flag":
                                    this.m_values.Add(new AndroidAttributeEnumValue() { 
                                        Type  = reader.Name ,
                                        Name  = reader.GetAttribute ("name"),
                                        Value = reader.GetAttribute ("value")
                                    });
                                    break;
                                case "eat-comment":
                                    break;
                                case "attr":
                                    var r = reader.ReadSubtree ();                                    
                                    var k = new AndroidAttributeDefinition();
                                    if (r.Read())
                                    {
                                        k.Load(r);
                                    }
                                    this.m_Childs.Add(k);
                                    break;
                                default :
                                    break;
                            
                            
                            }
                            break;
                    }
                }
            }

        }
        public override string ToString()
        {
            string s = this.Name + " : ";

            if (string.IsNullOrEmpty(this.Format))
                s += this.GetValues();
            else
                s += this.Format;

            return s;
        }

        public string GetValues()
        {
            if (this.m_values.Count == 0)
            {
                if (this.Format == null)
                {
                    var f = AndroidAttributeDefinitionTool.Instance.GetDefinition(this.Name);
                    if (f != null)
                    {
                        return f.GetValues();
                    }
                }

                return string.Empty;
            }
            StringBuilder sb = new StringBuilder();
            bool r = false;
            foreach (AndroidAttributeEnumValue S in this.m_values)
            {
                if (r)
                    sb.Append("|");
                sb.Append(S.Name);
                r = true;
            }
            return sb.ToString();
        }

        public string GetDescription()
        {
            return string.Empty;
        }

        public int CompareTo(object obj)
        {
            AndroidAttributeDefinition def = (AndroidAttributeDefinition)obj;
            return this.m_Name.CompareTo(def.Name);
        }
    }
}
