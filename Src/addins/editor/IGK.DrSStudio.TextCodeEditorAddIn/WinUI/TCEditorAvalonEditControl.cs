

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TCEditorAvalonEditControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.TextCodeEditorAddIn.WinUI
{
    class TCEditorAvalonEditControl : IGKXUserControl
    {
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private ICSharpCode.AvalonEdit.TextEditor c_textEditor;

       /// <summary>
       /// get or set the show line number
       /// </summary>
        public bool ShowLineNumbers
        {
            get { return c_textEditor.ShowLineNumbers ; }
            set
            {
                c_textEditor.ShowLineNumbers = value;
            }
        }
        public bool WordWrap { get { return c_textEditor.WordWrap ; }
            set
            {
                c_textEditor.WordWrap = value;
            }
        }
        public int SelectionStart
        {
            get { return c_textEditor.SelectionStart; }
            set
            {
                c_textEditor.SelectionStart  = value;
            }
        }
        public int SelectionLength
        {
            get { return c_textEditor.SelectionLength; }
            set
            {
                c_textEditor.SelectionLength = value;
            }
        }
        public string SelectedText
        {
            get { return c_textEditor.SelectedText ; }
            set
            {
                c_textEditor.SelectedText = value;
            }
        }
      

        [Browsable(true )]
        public override string Text
        {
            get
            {
                return c_textEditor.Text;
            }
            set
            {
                c_textEditor.Text = value;
            }
        }
     
        public TCEditorAvalonEditControl()
        {
            InitializeComponent();
            c_textEditor = new ICSharpCode.AvalonEdit.TextEditor();
            c_textEditor.ShowLineNumbers = true;
            c_textEditor.FontSize = 14;
            //setting wpf property by codes
            c_textEditor.FontStyle = System.Windows.FontStyles.Normal;
            c_textEditor.FontWeight = System.Windows.FontWeights.Normal;
            c_textEditor.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(".cs");
            this.elementHost1.Child = c_textEditor;
            
        }
        /// <summary>
        /// 
        /// </summary>
        public  event EventHandler TextEditorChanged {
            add {
                this.c_textEditor.TextChanged += value;
            }
            remove {
                this.c_textEditor.TextChanged -= value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        private void InitializeComponent()
        {
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(420, 270);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // TCEditorAvalonEditControl
            // 
            this.Controls.Add(this.elementHost1);
            this.Name = "TCEditorAvalonEditControl";
            this.Size = new System.Drawing.Size(420, 270);
            this.ResumeLayout(false);

        }

        public ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition HightLight { get {
            return this.c_textEditor.SyntaxHighlighting;
        }
            set {
                this.c_textEditor.SyntaxHighlighting = value;
            }
        }
    }
}
