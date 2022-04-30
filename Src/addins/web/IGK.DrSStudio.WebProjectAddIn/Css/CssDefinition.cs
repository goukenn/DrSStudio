

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CssDefinition.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CssDefinition.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using IGK.ICore.Resources;
namespace IGK.DrSStudio.WebProjectAddIn.Css
{
    /// <summary>
    /// repersent a css definition loader
    /// </summary>
    public class CssDefinition
    {
        static CssDefinitionCollection sm_properties;
        static CssEnumDefinitionCollection sm_enums;
        public CssDefinition()
        {
        }
        static CssDefinition() {
            sm_properties = new CssDefinitionCollection();
            sm_enums = new CssEnumDefinitionCollection(); 
            Load();
        }
        public static CssDefinitionCollection Properties 
        {
            get {
                return sm_properties;
            }
        }
        public static CssEnumDefinitionCollection Enums
        {
            get
            {
                return sm_enums ;
            }
        }
        internal static void Load() {
            using (MemoryStream mem = new MemoryStream())
            {
                Byte[] tab = CoreResources.GetResource ("css/CssDefinition");
                if (tab == null)
                    return;

                mem.Write(tab, 0, tab.Length);
                mem.Seek(0, SeekOrigin.Begin);
                XmlReader v_reader = XmlReader.Create(mem);
                sm_properties.Clear();
                while (v_reader.Read())
                {
                    switch (v_reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (v_reader.Name)
                            {
                                case "cssProperty":
                                    //create a new css property declaration 
                                    CssPropertyDeclaration prop = new CssPropertyDeclaration(v_reader.GetAttribute("name"));
                                    prop.Description = v_reader.GetAttribute("description");
                                    prop.Type = v_reader.GetAttribute("type");
                                    sm_properties.Add(prop.Name, prop);
                                    break;
                                case "cssEnum":
                                    XmlReader subreader = v_reader.ReadSubtree();
                                    LoadEnumType(subreader);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        private static void LoadEnumType(XmlReader subreader)
        {
            CssEnumDeclarationType type = null;
            List<string> v_values = new List<string> ();
            string v_n = null;
            while (subreader.Read())
            {
                switch (subreader.NodeType)
                {
                    case XmlNodeType.Element :
                    if (subreader.Name == "cssEnum")continue ;
                    v_values.Clear ();
                    v_n = subreader.Name;
                    v_values.AddRange (ReadItems(subreader.ReadSubtree()));
                    type = new CssEnumDeclarationType(v_n, v_values.ToArray());
                    sm_enums.Add(type.Name, type);
                        break;
                    case XmlNodeType.EndElement :
                        break ;
                }
            }
        }
        private static string[] ReadItems(XmlReader xmlReader)
        {
            List<string> v_values = new List<string>();
            while (xmlReader.Read())
            {
                switch (xmlReader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (xmlReader.Name == "Item")
                        v_values.Add(xmlReader.ReadElementContentAsString());
                        break;
                    case XmlNodeType.EndElement:
                        break;
                }
            }
            return v_values.ToArray();
        }
        public class CssDefinitionCollection : IEnumerable
        {
            Dictionary<string, CssPropertyDeclaration> m_properties;
            internal CssDefinitionCollection()
            {
                this.m_properties = new Dictionary<string, CssPropertyDeclaration>();
            }
            public CssPropertyDeclaration this[string key] {
                get {
                    return this.m_properties[key];
                }
            }
            public override string ToString()
            {
                return "CssDefinitionCollection ["+m_properties.Count +"]";
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_properties.GetEnumerator();
            }
            internal void Clear()
            {
                this.m_properties.Clear();
            }
            internal void Add(string key, CssPropertyDeclaration prop)
            {
                if (string.IsNullOrEmpty(key) || (prop == null))
                    return ;
                if (!this.m_properties .ContainsKey (key))
                    this.m_properties.Add(key, prop);
            }
        }
    }
    /// <summary>
    /// get enums 
    /// </summary>
    public class CssEnumDeclarationType
    {
        private string m_Name;
        private string[] m_Values;
        public string[] Values
        {
            get { return m_Values; }
        }
        public string Name
        {
            get { return m_Name; }
        }
        internal CssEnumDeclarationType(string name, string[] t )
        {
            this.m_Name = name;
            this.m_Values = t;
        }
        public override string ToString()
        {
            return string.Format("{0} : [" + this.m_Values.Length + "]", this.Name );
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class CssPropertyDeclaration
    {
        private string m_Name;
        private string m_Type;
        private string m_Description;
        /// <summary>
        /// get or set the property description
        /// </summary>
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
        /// <summary>
        /// get or set the css
        /// </summary>
        public string Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type != value)
                {
                    m_Type = value;
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
        public CssPropertyDeclaration()
        {
        }
        public CssPropertyDeclaration(string name)
        {
            this.Name = name;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
    public class CssEnumDefinitionCollection : IEnumerable 
    {
        Dictionary<string, CssEnumDeclarationType> m_type;
        public CssEnumDefinitionCollection()
        {
            this.m_type = new Dictionary<string, CssEnumDeclarationType>();
        }
        public CssEnumDeclarationType  this[string key]
        {
            get { return m_type[key]; }
        }
        public int Count { get { return this.m_type.Count ; } }
        public void Add(string key, CssEnumDeclarationType type)
        {
            if (string.IsNullOrEmpty(key) || (type == null))
                return;
            if (!this.m_type.ContainsKey(key))
                this.m_type.Add(key, type);
        }
        public override string ToString()
        {
            return "CssEnums [" + this.Count + "]";
        }
        public IEnumerator GetEnumerator()
        {
            return this.m_type.GetEnumerator();
        }
    }
}

