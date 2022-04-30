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
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Collections;
using System.Collections.Generic ;
using System;
using System.Drawing.Design;


namespace WinSnippetBuilder
{

    class SnippetBuilder
    {
        private VSSnippetBuilder m_snippetBuilder;
        private int m_currentIndex;

        [Browsable(false)]
        public ISnippetHeader Header
        {
            get
            {
                return this.m_snippetBuilder.CodeSnippets[m_currentIndex].Header;
            }
        }
        [Browsable(false)]
        public ISnippetDefinition SnippetDefinition
        {
            get
            {
                return this.m_snippetBuilder.CodeSnippets[m_currentIndex].Snippet;
            }
        }

        [Browsable(false)]
        public ICodeSnippet Snippet
        {
            get
            {
                return this.m_snippetBuilder.CodeSnippets[m_currentIndex];
            }
        }


        [Browsable(false)]
        public VSSnippetBuilder VSSnippet
        {
            get
            {
                return this.m_snippetBuilder;
            }
        }

        [Category("Snippet")]
        [Description("Get or set the name of the snippet")]
        public string Name
        {
            get
            {
                return this.m_snippetBuilder.Name;
            }
            set
            {
                this.m_snippetBuilder.Name = value;
            }
        }
        [Category("Snippet")]
        [Description("Get or set the format version of snippet")]
        public string Format
        {
            get
            {
                return this.Snippet.Format;
            }
            set
            {
                this.Snippet.Format = value;
            }
        }
        [Category("Snippet")]
        [Description("Get or set the declaration")]
        [TypeConverter(typeof(SnippetReferenceCollectionTypeConverter))]
        [Editor(typeof(SnippetDeclarationCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public ISnippetDeclarationCollection Declaration
        {
            get
            {
                return this.SnippetDefinition.Declarations;
            }
        }
        [Category("Snippet")]
        [Description("Get or set importations")]
        [Editor (typeof (SnippetImportCollectionEditor),typeof (System.Drawing.Design .UITypeEditor ))]
        [TypeConverter(typeof(SnippetReferenceCollectionTypeConverter))]
        public ISnippetImportCollection Imports
        {
            get
            {
                return this.SnippetDefinition.Imports;
            }
        }
        [Category("Snippet")]
        [Description("Get or set references")]
        [Editor(typeof(SnippetReferenceCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [TypeConverter(typeof(SnippetReferenceCollectionTypeConverter))]
        public ISnippetReferenceCollection References{
            get
            {
                return this.SnippetDefinition.References;
            }
        }


        [Category("Header")]
        [Description("Title of the snippet. this title will be visible on Ctrl+K, Ctrl+B dialog")]
        public string Title
        {
            get
            {
                return this.Header.Title;
            }
            set
            {
                this.Header.Title = value;
            }
        }

        [Category("Header")]
        [Description("Description of the snippet")]
        public string Description
        {
            get
            {
                return this.Header.Description;
            }
            set
            {
                this.Header.Description = value;
            }
        }

        [Category("Header")]
        [Description("Author's name of the snippet")]
        public string Author
        {
            get
            {
                return this.Header.Author;
            }
            set
            {
                this.Header.Author = value;
            }
        }

        [Category("Header")]
        [Description("Uri for help")]
        public string HelpUrl
        {
            get
            {
                return this.Header.HelpUrl;
            }
            set
            {
                this.Header.HelpUrl = value;
            }
        }
        [Category("Header")]
        [Description("Shortcut key to enter on VS studio")]
        public string Shortcut
        {
            get
            {
                return this.Header.Shortcut;
            }
            set
            {
                this.Header.Shortcut = value;
            }
        }


        [Description("Keyword that help searching the snippet")]
        [Category("Header")]
        [TypeConverter(typeof(SnippetReferenceCollectionTypeConverter))]
        [Editor(typeof(SnippetKeywordCollectionEditor), typeof(UITypeEditor))]
        public ISnippetKeywordCollection Keywords
        {
            get
            {
                return this.Header.Keywords;
            }
        }

        [Description("Snippets type category: Expansion, SurroundsWith")]
        [Category("Header")]
        [TypeConverter(typeof(SnippetReferenceCollectionTypeConverter))]
         [Editor (typeof (SnippetTypeCollectionEditor ),typeof (UITypeEditor ))]
        public ISnippetTypeCollection SnippetTypes
        {
            get
            {
                return this.Header.SnippetTypes;
            }
        }


        [Category("Code")]
        /// <summary>
        /// get or set the code of the value
        /// </summary>
        [Description("Get or set the code of the value")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]    
        public string Code
        {            
            get
            {                
                return this.SnippetDefinition.Code.Text;
            }
            set
            {
                this.SnippetDefinition.Code.Text = value;
            }
        }
        [Category("Code")]
        /// <summary>
        /// get or set the code language
        /// </summary>
        public enuSnippetCode CodeType
        {
            get
            {
                return this.SnippetDefinition.Code.Language;
            }
            set
            {
                this.SnippetDefinition.Code.Language = value;
            }
        }
        public SnippetBuilder(VSSnippetBuilder vsSnippet)
        {
            this.m_snippetBuilder = vsSnippet;
            this.m_currentIndex = 0;
        }



        class SnippetReferenceCollectionEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                System.Windows.Forms.Design.IWindowsFormsEditorService v_service = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
                UIXReferenceDesignerControl<ISnippetReferenceCollection> ctr = new UIXReferenceDesignerControl<ISnippetReferenceCollection>(value as ISnippetReferenceCollection);
                v_service.DropDownControl(ctr);
                return value;
            }
        }
        class SnippetImportCollectionEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                System.Windows.Forms.Design.IWindowsFormsEditorService v_service = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
                UIXImportDesignerControl ctr = new UIXImportDesignerControl(value as ISnippetImportCollection);
                v_service.DropDownControl(ctr);
                return value;
            }
        }
        class SnippetDeclarationCollectionEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                System.Windows.Forms.Design.IWindowsFormsEditorService v_service = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
                using (WinDeclarationDesigner frm = new WinDeclarationDesigner(value as ISnippetDeclarationCollection))
                {                    
                    v_service.ShowDialog (frm);
                }
                return value;
            }
        }

        class SnippetReferenceCollectionTypeConverter : System.ComponentModel.TypeConverter
        {
            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                return true;// base.CanConvertFrom(context, sourceType);
            }
            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                return true;// base.CanConvertTo(context, destinationType);
            }
            public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
            {
                return base.GetStandardValues(context);
            }
            public override bool GetPropertiesSupported(ITypeDescriptorContext context)
            {
                return false;
            }
            public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
            {
                return false;
            }
        }
        class SnippetTypeCollectionEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                System.Windows.Forms.Design.IWindowsFormsEditorService v_service = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
                UIXReferenceDesignerControl<ISnippetTypeCollection> ctr = new UIXReferenceDesignerControl<ISnippetTypeCollection>(value as ISnippetTypeCollection);
                v_service.DropDownControl(ctr);
                return value;
            }
        }
        class SnippetKeywordCollectionEditor : System.Drawing.Design.UITypeEditor
        {
            public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
            {
                return System.Drawing.Design.UITypeEditorEditStyle.Modal;
            }
            public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
            {
                System.Windows.Forms.Design.IWindowsFormsEditorService v_service = provider.GetService(typeof(System.Windows.Forms.Design.IWindowsFormsEditorService)) as System.Windows.Forms.Design.IWindowsFormsEditorService;
                UIXReferenceDesignerControl<ISnippetKeywordCollection> ctr = new UIXReferenceDesignerControl<ISnippetKeywordCollection>(value as ISnippetKeywordCollection);
                v_service.DropDownControl(ctr);
                return value;
            }
        }
    }
}