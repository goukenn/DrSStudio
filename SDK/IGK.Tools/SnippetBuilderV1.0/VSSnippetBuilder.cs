/* 
-------------------------------------------------------------------
Company: IGKDEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGKDEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGKDEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
App : DrSStudio
powered by IGKDEV 2008-2011
------------------------------------------------------------------- 
*/

/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Xml;
namespace WinSnippetBuilder
{
   
   


    public static class SnippetUri
    {
        internal const string VS2005 = "http://schemas.microsoft.com/VisualStudio/2005/CodeSnippet";
    }



    /// <summary>
    /// represnet the VS Snippet Builder class
    /// </summary>
    public class VSSnippetBuilder : ISnippetBuilder
    {
        private string m_name;
        private string m_version;
        private CodeSnippetCollection m_codeSnippets;

        [Description ("Obtient ou définie le nom du snippet")]
        public string Name { get { return m_name; } set { m_name = value; } }
        [Description("Obtient ou définie la version") ]

        
        /// <summary>
        /// gets or set the version
        /// </summary>
        public string Version { get { return m_version; } set { m_version = value; } }
        
        /// <summary>
        /// protected constructor 
        /// </summary>
        protected VSSnippetBuilder()
        {
            m_version = "1.0.0.0";
            m_name = "Snippet";
            m_codeSnippets = new CodeSnippetCollection(this);
            m_codeSnippets.Add(new CodeSnippet());
        }

        #region ISnippetBuilder Members

        public void Save(string filename)
        {
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.Indent = true;
            v_setting.Encoding = System.Text.Encoding.UTF8;
            v_setting.CloseOutput = true;

            XmlWriter xwriter = XmlWriter.Create(filename, v_setting );

            xwriter.WriteStartElement("CodeSnippets",SnippetUri .VS2005);
            foreach (ICodeSnippet snippet in this.CodeSnippets)
            {
                snippet.WriteXml(xwriter);
            }
            xwriter.WriteEndElement();
            xwriter.Flush();
            xwriter.Close();
        }

        public void Open(string filename)
        {
            XmlReader xreader = XmlReader.Create(filename);

            if (xreader.ReadToDescendant("CodeSnippets"))
            { 
                //read snippet item
                this.CodeSnippets.Clear();
                while (xreader.Read())
                {
                    switch (xreader.NodeType)
                    {
                        case XmlNodeType.Element :
                            if (xreader.Name == "CodeSnippet")
                            {
                                CodeSnippet v_cd = new CodeSnippet();
                                v_cd.ReadXml(xreader);
                                this.CodeSnippets.Add(v_cd);
                            }
                            break;
                    }
                }

            }
            xreader.Close();


        }
        /// <summary>
        /// build a snippet from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static VSSnippetBuilder OpenFromFile(string filename)
        {
            VSSnippetBuilder vs = new VSSnippetBuilder();
            vs.Open(filename);
            return vs;
        }

        #endregion

        internal static VSSnippetBuilder CreateNew()
        {
            return new VSSnippetBuilder();
        }

        #region ISnippetBuilder Members

        public ICodeSnippetCollection CodeSnippets
        {
            get {
                return m_codeSnippets;
            }
        }

        #endregion

        public class CodeSnippetCollection : ICodeSnippetCollection
        {
            private List<ICodeSnippet> m_lists;
            private VSSnippetBuilder m_owner;

            public override string ToString()
            {
                return "CodeSnippet Builder";
            }
            internal CodeSnippetCollection(VSSnippetBuilder owner)            
            {
                m_owner = owner;
                m_lists = new List<ICodeSnippet>();
            }

            #region ICodeSnippetCollection Members

            public void Add(ICodeSnippet snippet)
            {
                this.m_lists.Add(snippet);
            }

            public void Remove(ICodeSnippet snippet)
            {
                this.m_lists.Remove(snippet);
            }

            public int Count
            {
                get {
                    return this.m_lists.Count;
                }
            }

            public ICodeSnippet this[int index]
            {
                get {
                    return this.m_lists[index];
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return m_lists.GetEnumerator();
            }

            #endregion

            #region ICodeSnippetCollection Members


            public void Clear()
            {
                this.m_lists.Clear();
            }

            #endregion
        }
    }

    #region Attribute
    [AttributeUsage (AttributeTargets.Property)]
    public class SnippetPropertyAttribute : Attribute
    {
        private bool m_isProperty;

        /// <summary>
        /// get is property
        /// </summary>
        public bool IsProperty {
            get { return this.m_isProperty; }
        }
        public SnippetPropertyAttribute(bool isProperty)
        {
            this.m_isProperty = isProperty;
        }
    }
    [AttributeUsage(AttributeTargets.Property)]
    public class SnippetElementPropertyAttribute : Attribute
    {
        private bool m_isElementProperty;

        /// <summary>
        /// get is property
        /// </summary>
        public bool IsElementProperty
        {
            get { return this.m_isElementProperty; }
        }
        public SnippetElementPropertyAttribute(bool isProperty)
        {
            this.m_isElementProperty = isProperty;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class SnippetDefaultValue : Attribute {
        private object m_default;

        /// <summary>
        /// get the default value
        /// </summary>
            public object Default{
                get{
                    return this.m_default ;
                }
            }
        public SnippetDefaultValue (object @default)
        {
            this.m_default = @default;
        }
    }

    #endregion

    public class CodeSnippet : ICodeSnippet
    {
        private string m_format;
        private CodeSnippetHeader m_header;
        private SnippetDefinition m_snippet;

        internal CodeSnippet()
        {
            m_format = "1.0.0";
            m_header = new CodeSnippetHeader();
            m_snippet = new SnippetDefinition();
        }
        #region ICodeSnippet Members

        [SnippetProperty (true)]
        /// <summary>
        /// get or set the format of the snippet
        /// </summary>
        public string Format
        {
            get
            {
                return m_format;
            }
            set
            {
                m_format = value;
            }
        }

        #endregion

        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement("CodeSnippet");
            
            //write Attribute
            SnippetPropertyAttribute v_snAttrib = null;
            SnippetElementPropertyAttribute v_elAttrib = null;
            object v_obj = null;
            List<PropertyInfo> v_elementProperty = new List<PropertyInfo>();
            foreach (PropertyInfo v_prInfo in GetType().GetProperties())
            {
               v_snAttrib =  SnippetPropertyAttribute.GetCustomAttribute (v_prInfo, typeof (SnippetPropertyAttribute )) as SnippetPropertyAttribute;


               v_elAttrib = SnippetElementPropertyAttribute.GetCustomAttribute(v_prInfo, typeof(SnippetElementPropertyAttribute)) as SnippetElementPropertyAttribute;

               if ((v_elAttrib != null)&& (v_elAttrib .IsElementProperty ))
               {
                   v_elementProperty.Add(v_prInfo);
               }
               if (v_snAttrib == null) 
                   continue;

               if (v_snAttrib.IsProperty)
               {
                   v_obj = v_prInfo.GetValue(this, null);
                   if (v_obj !=null)
                   write.WriteAttributeString(v_prInfo .Name,  this.Format);
               }
            }            

            //write element
            foreach (PropertyInfo prInfo in v_elementProperty)
            {
                v_obj = prInfo.GetValue(this, null);

                if (v_obj is ISnippetReader)
                {
                    (v_obj as ISnippetReader).WriteXml(write);
                }
                else if (v_obj !=null)
                {
                    write.WriteElementString(prInfo.Name, v_obj.ToString());
                }
            }
            
            write.WriteEndElement();
        }

        public void ReadXml(XmlReader xreader)
        {
            PropertyInfo v_prInfo = null;
            if (xreader.HasAttributes)
            {
                xreader.MoveToFirstAttribute();
                for (int i = 0; i < xreader.AttributeCount ; i++)
                {
                    v_prInfo = GetType().GetProperty(xreader.Name);
                    v_prInfo.SetValue(this, xreader.Value, null);

                    xreader.MoveToNextAttribute();
                }
                
            }
            xreader.MoveToElement();
            if (xreader.IsEmptyElement) return;

            XmlReader v_rd  = xreader.ReadSubtree();
            ISnippetReader v_snReader = null;
            while (v_rd.Read())
            {
                switch (v_rd.NodeType)
                {
                    case XmlNodeType.Element :
                        
                        v_prInfo = GetType().GetProperty(v_rd.Name);
                        if ((v_prInfo) == null) 
                            continue;
                        v_snReader = v_prInfo.GetValue(this, null) as ISnippetReader ;
                        if (v_snReader !=null)
                        {
                            v_snReader.ReadXml(v_rd);
                        }
                        else
                        {
                            v_prInfo.SetValue(this, v_rd.ReadElementContentAsString(), null);
                        }
                        break;
                }
            }

        }

        #endregion

        #region ICodeSnippet Members

        [SnippetElementProperty (true )]
        public ISnippetHeader Header
        {
            get {
                return this.m_header;
            }
        }

        [SnippetElementProperty (true )]
        public ISnippetDefinition Snippet
        {
            get {
                return this.m_snippet;
            }
        }

        #endregion
    }

    class CodeSnippetHeader : ISnippetHeader
    {
        #region ISnippetReader Members

        private string m_author;
        private string m_title;
        private string m_Description;
        private string m_helpUrl;
        private string m_shortcut;
        private SnippetTypeCollection m_snippetTypes;
        private SnippetKeywordCollection m_keyswords;

        /// <summary>
        /// get or set the description
        /// </summary>
        public string Description
        {
            get {
                return this.m_Description;
            }
            set {
                this.m_Description = value;
            }
        }
        /// <summary>
        /// get or set the help url
        /// </summary>
        public string HelpUrl {
            get {
                return this.m_helpUrl;
            }
            set {
                this.m_helpUrl = value;
            }
        }
        /// <summary>
        /// get or set the key
        /// </summary>
        public ISnippetKeywordCollection  Keywords {
            get {
                return this.m_keyswords;
            }
        }
        /// <summary>
        /// get or set the short cut
        /// </summary>
        public string Shortcut {
            get {
                return this.m_shortcut;
            }
            set {
                this.m_shortcut = value;
            }
        }
        public string Title {
            get {
                return this.m_title;
            }
            set {
                this.m_title = value;
            }
        }
        public string Author {
            get {
                return this.m_author;
            }
            set {
                this.m_author = value;
            }
        }




        public ISnippetTypeCollection SnippetTypes
        {
            get
            {                
                return this.m_snippetTypes;
            }
        }

        public override string ToString()
        {
            return "Header";
        }
        
        internal CodeSnippetHeader()
        {
            this.m_title = "NewSnippet";
            this.m_author = "C.A.D. BONDJE DOUE";
            this.m_shortcut = "mySnippet";
            this.m_snippetTypes = new SnippetTypeCollection(this);
            this.m_keyswords = new SnippetKeywordCollection(this);
        }
        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement("Header");

            object v_obj = null;
            foreach (PropertyInfo v_prInfo in this.GetType().GetProperties())
            {
                if (v_prInfo.CanWrite && v_prInfo.CanWrite)
                {
                    v_obj = v_prInfo.GetValue(this, null);
                    if (v_obj == null) continue;

                    write.WriteElementString (v_prInfo.Name, v_obj.ToString());
                    
                }
            }             

            if (this.m_snippetTypes.Count > 0)
            {
                write.WriteStartElement("SnippetTypes");

                foreach (ISnippetType v_t in this.m_snippetTypes)
                {
                    v_t.WriteXml(write);
                }
                write.WriteEndElement ();
            }



            if (this.m_keyswords.Count > 0)
            {
                write.WriteStartElement("Keywords");

                foreach (ISnippetKeyWord v_t in this.m_keyswords )
                {
                    v_t.WriteXml(write);
                }
                write.WriteEndElement();
            }

            write.WriteEndElement();

        }

        public void ReadXml(XmlReader read)
        {
            XmlReader v_xread = read.ReadSubtree();
            PropertyInfo v_prInfo = null;
            ISnippetReader v_item = null;
            while (v_xread.Read())
            {
                switch (v_xread.NodeType)
                {
                    
                    case XmlNodeType.Element:
                        switch (v_xread.Name)
                        {
                            case "SnippetTypes":
                                {
                                    this.m_snippetTypes.Clear();
                                    XmlReader v_xread1 = read.ReadSubtree();
                                    if (!v_xread1.IsEmptyElement)
                                    {
                                        string v = null;    while (v_xread1.Read())
                                        {
                                            if (v_xread1.Name == "SnippetType")
                                            {
                                                v = v_xread1.ReadElementContentAsString();
                                                SnippetType sn = new SnippetType(v);
                                                this.m_snippetTypes.Add(sn);
                                            }
                                        }
                                    }
                                }
                                break;
                            case "Keywords":
                                {
                                    this.m_keyswords.Clear();
                                    XmlReader v_xread1 = read.ReadSubtree();
                                    if (!v_xread1.IsEmptyElement)
                                    {
                                        string v = null;
                                           while (v_xread1.Read())
                                        {
                                            if (v_xread1.Name == "Keyword")
                                            {
                                                v = v_xread1.ReadElementContentAsString();
                                                SnippetKeyword sn =
                                                    new SnippetKeyword(v);

                                                this.m_keyswords.Add(sn);
                                            }
                                        }
                                    }                                    
                                }
                                break;
                            default :
                                v_prInfo = GetType().GetProperty(v_xread.Name);
                                if (v_prInfo == null) continue;

                        v_item =         v_prInfo.GetValue(this, null) as ISnippetReader ;
                        if (v_item != null)
                            v_item.ReadXml(v_xread);
                        else
                            v_prInfo.SetValue(this, v_xread.ReadElementContentAsString(), null);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:

                        break;                    
                    
                }
            }
        }

        #endregion

        class SnippetTypeCollection : ISnippetTypeCollection 
        {
            private CodeSnippetHeader m_header;
            private List<ISnippetType > m_list;
            internal SnippetTypeCollection(CodeSnippetHeader header)
            {
                this.m_header = header;
                this.m_list = new List<ISnippetType>();
            }

            #region ISnippetTypeCollection Members

            public override string ToString()
            {
                return "SnippetType : [" + this.Count + "]";
            }
            public void Add(ISnippetType snippettype)
            {
                if (snippettype == null) return;

                this.m_list.Add(snippettype);
            }

            public void Remove(ISnippetType snippettype)
            {
                this.m_list.Remove(snippettype);
            }

            public int Count
            {
                get { return this.m_list.Count; }
            }

            public ISnippetType this[int index]
            {
                get { return this.m_list[index ] ; }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }

            #endregion           

            public void Clear() { 
                this.m_list .Clear();
            }

            #region ISnippetItemCollection Members

            void ISnippetItemCollection.Add(ISnippetReader item)
            {
                SnippetItem vi = item as SnippetItem;
                if (vi != null)
                {
                    SnippetType t = new SnippetType(vi.Value);
                    this.Add(t);
                }
            }

            void ISnippetItemCollection.Remove(ISnippetReader item)
            {
                this.Remove(item as ISnippetType);
            }

            #endregion
        }

        class SnippetKeywordCollection : ISnippetKeywordCollection, ISnippetItemCollection  
        {

            private List<ISnippetKeyWord> m_list;
            private CodeSnippetHeader m_header;

            public override string ToString()
            {
                return "Keywords : ["+this.Count+"]";
            }
            internal SnippetKeywordCollection(CodeSnippetHeader header)
            {
                this.m_header = header;
                this.m_list = new List<ISnippetKeyWord>();
            }

            #region ISnippetKeywordCollection Members

            public void Add(ISnippetKeyWord snippettype)
            {
                this.m_list.Add(snippettype);
            }
            public void Add(string keyword)
            {
                if (string.IsNullOrEmpty(keyword)) return;


                this.m_list.Add(new SnippetKeyword(keyword));
            }

            public void Remove(ISnippetKeyWord snippettype)
            {
                this.m_list.Remove(snippettype);
            }

            public int Count
            {
                get {
                    return this.m_list.Count;
                }
            }

            public ISnippetKeyWord this[int index]
            {
                get {
                    return this.m_list[index];
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }

            #endregion

            #region ISnippetItemCollection Members

            void ISnippetItemCollection.Add(ISnippetReader item)
            {
                SnippetItem v_item = item as SnippetItem;
                if (v_item != null)
                {
                    this.Add(new SnippetKeyword(v_item.Value));
                }
            }

            void ISnippetItemCollection.Remove(ISnippetReader item)
            {
                this.Remove(item as ISnippetKeyWord);
            }


            
            #endregion


            #region ISnippetItemCollection Members


            public void Clear()
            {
                this.m_list.Clear();
            }

            #endregion
        }

        public sealed class SnippetKeyword : ISnippetKeyWord
        {
            private string m_value;
            #region ISnippetKeyWord Members

            public string Value
            {
                get
                {
                    return this.m_value;
                }
                set
                {                    
                    this.m_value = value;
                }
            }

            #endregion

            internal SnippetKeyword(string value)
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("value");
                this.m_value = value;
            }
            #region ISnippetReader Members

            public void WriteXml(XmlWriter write)
            {
                write.WriteElementString("Keyword", this.m_value);
            }

            public void ReadXml(XmlReader read)
            {
                throw new NotImplementedException();
            }

            #endregion

            public override string ToString()
            {
                return this.Value;
            }
        }
    }


    class SnippetType : ISnippetType
    {
        private string m_value;

        public string Value {
            get {
                return this.m_value;
            }
            set {
                this.m_value = value;
            }
        }


        public SnippetType(string value) {

            this.m_value = value;
        }
        public override string ToString()
        {
            return Value;
        }
        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            if (string.IsNullOrEmpty(this.m_value))
                return;

            write.WriteElementString("SnippetType", this.m_value);
           // write.WriteAttributeString("SnippetType", this.m_value);
        }

        public void ReadXml(XmlReader read)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class SnippetItem : ISnippetReader
    {
        string m_value;

        public string Value { get { return this.m_value; } set { this.m_value = value; } }

        public override string ToString()
        {
            return Value;
        }
        internal SnippetItem(string value)
        {
            this.m_value = value;
        }

        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            throw new NotImplementedException();
        }

        public void ReadXml(XmlReader read)
        {
            throw new NotImplementedException();
        }

        #endregion
    }

    class SnippetDefinition : ISnippetDefinition
    {
        public static SnippetItem CreateItem(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            return new SnippetItem(name);
        }
        private SnippetReferenceCollection m_references;
        private SnippetImportCollection m_imports;
        private SnippetCodeDefinition  m_code;
        private SnippetDeclarationCollection m_declaration;

        internal SnippetDefinition()
        {
            m_references = new SnippetReferenceCollection(this);
            m_imports = new SnippetImportCollection(this);
            m_declaration = new SnippetDeclarationCollection(this);

            //SnippetItemLitteral literal = new SnippetItemLitteral ();
            //literal.Editable = true;
            //literal.ID = "divi";
            //literal.ToolTip = "Message Additionnel";            
            //literal.Default = "Default";

            
            //m_declaration .Add (literal );
            //m_references.Add(new SnippetAssemblyReference("System.Windows.Forms.Dll"));
            //this.m_imports .Add (new SnippetImport ("System.Windows.Forms"));
            m_code =new SnippetCodeDefinition (enuSnippetCode .CSharp , "Your Code ...");
        }
        public override string ToString()
        {
            return "Definition";
        }
        #region ISnippetDefinition Members

        public ISnippetReferenceCollection References
        {
            get
            {
                return m_references;
            }
        }

        #endregion

        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement ("Snippet");

            if (this.References.Count > 0)
            {
                write.WriteStartElement("References");
                foreach (ISnippetReference v_ref in this.References)
                {
                    v_ref.WriteXml(write);
                }
                write.WriteEndElement();
            }
            if (this.Imports.Count > 0)
            {
                write.WriteStartElement("Imports");
                foreach (ISnippetImport v_ref in this.Imports)
                {
                    v_ref.WriteXml(write);
                }
                write.WriteEndElement();
            }

            if (this.Declarations.Count > 0)
            {
                write.WriteStartElement("Declarations");
                foreach (ISnippetDeclarationItem  v_ref in this.Declarations )
                {
                    v_ref.WriteXml(write);
                }
                write.WriteEndElement();
            }

            m_code.WriteXml(write);

            write.WriteEndElement();
        }

        public void ReadXml(XmlReader read)
        {
            XmlReader v_xread = read.ReadSubtree();
            PropertyInfo v_prInfo = null;
            ISnippetReader v_item = null;
            while (v_xread.Read())
            {
                switch (v_xread.NodeType)
                {

                    case XmlNodeType.Element:
                        switch (v_xread.Name)
                        {
                            case "Code":
                                this.m_code.ReadXml(read);
                                break;
                            case "References":
                                {
                                    this.m_references.Clear();
                                    XmlReader v_xread1 = read.ReadSubtree();
                                    if (!v_xread1.IsEmptyElement)
                                    {
                                        string v = null;
                                        while (v_xread1.Read())
                                        {
                                            if (v_xread1.NodeType == XmlNodeType.Element)
                                            {
                                                switch (v_xread1.Name)
                                                {
                                                    case "Reference":
                                                        break;
                                                    case "Assembly":
                                                        v = v_xread1.ReadElementContentAsString();
                                                        SnippetAssemblyReference sn = new SnippetAssemblyReference(v);
                                                        this.m_references.Add(sn);
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "Imports":
                                {
                                    this.m_imports.Clear();
                                    XmlReader v_xread1 = read.ReadSubtree();
                                    if (!v_xread1.IsEmptyElement)
                                    {
                                        string v = null;
                                        while (v_xread1.Read())
                                        {
                                            if (v_xread1.NodeType == XmlNodeType.Element)
                                            {
                                                switch (v_xread1.Name)
                                                {
                                                    case "Import":
                                                        break;
                                                    case "Namespace":
                                                        {
                                                            v = v_xread1.ReadElementContentAsString();
                                                            SnippetImport sn =
                                                                new SnippetImport(v);

                                                            this.m_imports.Add(sn);
                                                        }
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                            case "Declarations":
                                {
                                    this.m_declaration .Clear();
                                    XmlReader v_xread1 = read.ReadSubtree();
                                    if (!v_xread1.IsEmptyElement)
                                    {
                                        while (v_xread1.Read())
                                        {
                                            if (v_xread1 .NodeType == XmlNodeType.Element )
                                            switch (v_xread1.Name)
                                            {
                                                case "Literal":
                                                    SnippetItemLitteral litteral = new SnippetItemLitteral();
                                                    litteral.ReadXml(v_xread1);
                                                    this.m_declaration.Add(litteral);
                                                    break;
                                                case "Object":
                                                    SnippetItemObject @obj = new SnippetItemObject();
                                                    @obj.ReadXml(v_xread1);
                                                    this.m_declaration.Add(@obj);
                                                    break;
                                            }

                                            
                                        }
                                    }
                                }
                                break ;
                            default:
                                v_prInfo = GetType().GetProperty(v_xread.Name);
                                if (v_prInfo == null) continue;

                                v_item = v_prInfo.GetValue(this, null) as ISnippetReader;
                                if (v_item != null)
                                    v_item.ReadXml(v_xread);
                                else
                                    v_prInfo.SetValue(this, v_xread.ReadElementContentAsString(), null);
                                break;
                        }
                        break;
                    case XmlNodeType.EndElement:

                        break;

                }
            }
        }

        #endregion

        /// <summary>
        /// represent the reference collection
        /// </summary>
        class SnippetReferenceCollection : ISnippetReferenceCollection
        {
            List<ISnippetReference> m_references;
            SnippetDefinition m_owner;

            internal SnippetReferenceCollection(SnippetDefinition owner)
            {
                m_owner = owner;
                m_references = new List<ISnippetReference>();
                
            }

            public override string ToString()
            {
                return "SnippetRefencesCollection [" + this.Count + "]";
            }

            #region ISnippetReferenceCollection Members

            public void Add(ISnippetReference @ref)
            {
                this.m_references.Add(@ref); 
            }
            public void Add(string value)
            {
                if (string.IsNullOrEmpty(value)) return;

                this.m_references.Add(new SnippetAssemblyReference ( value));
            }

            public bool Contains(string value)
            {
                foreach (ISnippetReference r in this)
                {
                    if (r.Value.ToLower() == value.ToLower())
                        return true;
                }
                return false;
            }

            public void Remove(ISnippetReference @ref)
            {
                this.m_references.Remove(@ref);
            }

            public int Count
            {
                get { return this.m_references.Count; }
            }

            public ISnippetReference this[int index]
            {
                get {
                    return this.m_references[index];
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this.m_references.GetEnumerator();
            }

            #endregion

            #region ISnippetReferenceCollection Members


            public void Clear()
            {
                this.m_references.Clear();
            }

            #endregion


            #region ISnippetItemCollection Members

            void ISnippetItemCollection.Add(ISnippetReader item)
            {
                SnippetItem i = item as SnippetItem ;
                if (i != null)
                {
                    SnippetDefinition.SnippetAssemblyReference v_a = new SnippetDefinition.SnippetAssemblyReference(i.Value);
                    this.Add(v_a);
                }
            }

            void ISnippetItemCollection.Remove(ISnippetReader item)
            {
                this.Remove(item as ISnippetReference);
            }

            #endregion
        }


        class SnippetImportCollection : ISnippetImportCollection
        {
            private List<ISnippetImport> m_items;
            private SnippetDefinition m_owner;
            internal SnippetImportCollection(SnippetDefinition  owner)
            {
                this.m_owner  = owner;
                m_items = new List<ISnippetImport>();
            }
            public override string ToString()
            {
                return "Imports : [" + this.Count + "]";
            }
            #region ISnippetImportCollection Members

            public void Add(ISnippetImport item)
            {
                m_items.Add(item);
            }
            public void Add(string @namespace)
            {
                this.Add (new SnippetImport (@namespace ));
            }

            public void Remove(ISnippetImport item)
            {
                m_items.Remove(item);
            }

            public int Count
            {
                get
                {
                    return m_items.Count;
                }
            }

            public ISnippetImport this[int index]
            {
                get {
                    return this.m_items[index];
                }
            }

            #endregion

            #region IEnumerable Members

            public IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }

            #endregion

            #region ISnippetImportCollection Members


            public void Clear()
            {
                this.m_items.Clear();
            }

            #endregion
        }

        /// <summary>
        /// represent the base class of the snippet reference
        /// </summary>
        public abstract class SnippetReferenceBase : ISnippetReference
        {
            private string m_value;

            #region ISnippetReference Members

            public abstract string Type
            {
                get;
            }

            #endregion

            #region ISnippetReader Members

            public void WriteXml(XmlWriter write)
            {
                write.WriteStartElement("Reference");

                write.WriteElementString (this.Type, this.Value);

                write .WriteEndElement ();
            }

            public void ReadXml(XmlReader read)
            {
                throw new NotImplementedException();
            }

            #endregion

            #region ISnippetReference Members


            public string Value
            {
                get
                {
                    return m_value;
                }
                set
                {
                    m_value = value;
                }
            }

            #endregion

            public override string ToString()
            {
                return this.Value;
            }
        }

        public class SnippetAssemblyReference : SnippetReferenceBase 
        {

            public SnippetAssemblyReference(string Value)
            {
                this.Value = Value;
            }
            public override string Type
            {
                get { return "Assembly"; }
            }
        }

        #region ISnippetDefinition Members


        public ISnippetCode Code
        {
            get {
                return m_code;
            }
        }
        public ISnippetImportCollection Imports
        {
            get { return this.m_imports; }
        }

        #endregion

        #region ISnippetDefinition Members


        public ISnippetDeclarationCollection Declarations
        {
            get { return this.m_declaration; }
        }

        #endregion
    }


    class SnippetCodeDefinition : ISnippetCode
    {
        private enuSnippetCode m_language;
        private string m_text;

        internal SnippetCodeDefinition(enuSnippetCode m_language, string content) 
        {
            this.m_language = m_language;
            this.m_text = content;
        }

        #region ISnippetCode Members

        public enuSnippetCode Language
        {
            get
            {
                return m_language;
            }
            set
            {
                m_language = value;
            }
        }

        /// <summary>
        /// get or set the text of the the snippet
        /// </summary>
        public string Text
        {
            get
            {
                return this.m_text;
            }
            set
            {
                m_text = value;
            }
        }

        #endregion

        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement("Code");
            write.WriteAttributeString("Language", this.m_language.ToString());
            write.WriteCData(this.Text);
            write.WriteEndElement();
        }

        public void ReadXml(XmlReader read)
        {
            XmlReader v_read1 = read.ReadSubtree();

            if (read.HasAttributes)
            {
                string v = read.GetAttribute (0);//"Language");
                this.m_language = (enuSnippetCode)Enum.Parse(typeof(enuSnippetCode), v, true );
            }

            while (v_read1.Read())
            {
                if (v_read1.NodeType == XmlNodeType.CDATA)
                    this.m_text = v_read1.Value;
            }
        }

        #endregion


    
    }
    
    public class SnippetImport : ISnippetImport
    {
        private string m_value;

        internal SnippetImport(string value) 
        {
            
            this.m_value = value;
        }

        #region ISnippetReader Members

        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement("Import");

            write.WriteElementString("Namespace", this.Value);

            write.WriteEndElement();
        }

        public override string ToString()
        {
            return this.Value;
        }

        public void ReadXml(XmlReader read)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ISnippetImport Members

        public string Value
        {
            get
            {
                return this.m_value;
            }
            set
            {
                m_value = value;
            }
        }

        #endregion
    }


    public abstract class SnippetDeclarationItemBase : ISnippetDeclarationItem 
    {
        private string m_id;
        private string m_default;
        private bool  m_editable;
        private string m_tooltip;
        private string m_type;
        private string m_function;

        public override string ToString()
        {
            if (string.IsNullOrEmpty(ID))
                return "ItemDeclaration";
            return this.ID;
        }
        protected SnippetDeclarationItemBase()
        {
            this.m_editable = true;
        }
        #region ISnippetDeclarationItem Members

        public string ID
        {
            get
            {
                return m_id;
            }
            set
            {
                m_id = value;
            }
        }
        /*
        Function  Description  Languages  
GenerateSwitchCases( EnumerationLiteral ) 
 Generates a switch statement and a set of case statements for the members of the enumeration specified by the EnumerationLiteral parameter. The EnumerationLiteral parameter must be either a reference to an enumeration literal or an enumeration type.
 Visual C#, Visual J#
 
ClassName() 
 Returns the name of the class that contains the inserted snippet.
 Visual C#, Visual J#
 
SimpleTypeName( TypeName ) 
 Reduces the TypeName parameter to its simplest form in the context in which the snippet was invoked.
 Visual C# 
 */
        [Description("Gets or sets the Function Type for replacement . Editor must be set to false. value surpported are"+
            "GenerateSwitchCases(EnumerationLiteral) ; ClassName() : get the snippet name class ; SimpleTypeName(typename)")]
        public string Function
        {
            get
            {
                return m_function;
            }
            set
            {
                m_function  = value;
            }
        }
        public string ToolTip
        {
            get
            {
                return m_tooltip;
            }
            set
            {
                m_tooltip = value;
            }
        }

        public string Default
        {
            get
            {
                return m_default;
            }
            set
            {
                m_default = value;
            }
        }

        public string Type
        {
            get
            {
                return m_type;
            }
            set
            {
                m_type = value;
            }
        }

        public abstract string MainType { get; }

        [SnippetProperty (true )]
        [SnippetDefaultValue (true )]
        public bool Editable {
            get {
                return m_editable;
            }
            set {
                m_editable = value;

            }
        }
        #endregion

        #region ISnippetReader Members


        public void WriteXml(XmlWriter write)
        {
            write.WriteStartElement (this.MainType);
            Object obj = null;
            Dictionary<string, string> v_eltDic = new Dictionary<string, string>();
            foreach (PropertyInfo v_prInfo in GetType().GetProperties())
            {
                if (v_prInfo.CanRead && v_prInfo.CanWrite)
                {
                    

                    obj = v_prInfo.GetValue(this, null);

                    if (obj != null)
                    {
                        SnippetPropertyAttribute pr = SnippetPropertyAttribute.GetCustomAttribute(v_prInfo, typeof(SnippetPropertyAttribute)) as SnippetPropertyAttribute;
                        SnippetDefaultValue v_def = SnippetDefaultValue.GetCustomAttribute(v_prInfo, typeof(SnippetDefaultValue)) as SnippetDefaultValue;

                        if ((v_def != null) && (v_def.Default.Equals(obj)))
                            continue;
                        if (pr != null)
                        {
                            write.WriteAttributeString(v_prInfo.Name, obj.ToString());
                        }
                        else
                        {
                            v_eltDic.Add(v_prInfo.Name, obj.ToString());
                        }
                    }
                } 
            }
            foreach (KeyValuePair <string,string > k in v_eltDic )
            {
                 write.WriteElementString(k.Key , k.Value );
            }
            write.WriteEndElement();
        }

        public  void ReadXml(XmlReader read)
        {
            XmlReader xread = read.ReadSubtree();
            if (xread.HasAttributes )
            {
                this.m_editable = Convert.ToBoolean (xread.GetAttribute ("Editable"));
            }
            while (xread.Read())
            {
                if (xread.NodeType == XmlNodeType.Element)
                {
                    switch (xread.Name)
                    {
                        case "ID": this.m_id = xread.ReadElementContentAsString (); 
                            break ;
                        case "ToolTip": this.m_tooltip  = xread.ReadElementContentAsString (); 
                            break ;
                        case "Default": this.m_default  = xread.ReadElementContentAsString (); 
                            break ;
                        case "Type": this.m_type  = xread.ReadElementContentAsString (); 
                            break ;
                        case "Function": this.m_function = xread.ReadElementContentAsString();
                            break;
                    }
                }
            }
        }
        #endregion
    }

    class SnippetItemLitteral : SnippetDeclarationItemBase 
    {
        public override string MainType
        {
            get { return "Literal"; }
        }
    }
    class SnippetItemObject : SnippetDeclarationItemBase
    {
        public override string MainType
        {
            get { return "Object"; }
        }

        internal SnippetItemObject() { }
        public SnippetItemObject(Type type)
        {
            this.Type = type.ToString ();
        }
        public SnippetItemObject(string name)
        {
            this.Type = name;
        }

        
    }


    class SnippetDeclarationCollection : ISnippetDeclarationCollection
    {
        List<ISnippetDeclarationItem> m_list;
        SnippetDefinition m_owner;

        public override string ToString()
        {
            return "Declarations : [" + this.Count + "]";
        }
        internal SnippetDeclarationCollection(SnippetDefinition def)
        {
            m_owner = def;
            m_list = new List<ISnippetDeclarationItem>();
        }

        #region ISnippetDeclarationCollection Members

        public void Add(ISnippetDeclarationItem item)
        {
            this.m_list.Add(item);
        }

        public void Remove(ISnippetDeclarationItem item)
        {
            this.m_list.Remove(item);
        }

        public int Count
        {
            get { return this.m_list.Count; }
        }

        public ISnippetDeclarationItem this[int index]
        {
            get { return this.m_list[index]; }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return this.m_list.GetEnumerator();
        }

        #endregion

        internal void Clear()
        {
            this.m_list.Clear();
        }
    }
}
