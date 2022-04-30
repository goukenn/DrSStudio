

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XGLShaderCodeEditor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XGLShaderCodeEditor.cs
*/
using ICSharpCode.AvalonEdit.Folding;

using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.Effect;
using IGK.DrSStudio.GLPictureEditorAddIn.Tools;
using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.GLPictureEditorAddIn.WinUI
{
    class XGLShaderCodeEditor : IGKXToolConfigControlBase 
    {
        private GLEditorPropertyInfo m_propertyInfo;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Splitter splitter1;
        private Tools.GLLibCodeEditorTool gLLibCodeEditorTool;
        private ICSharpCode.AvalonEdit.TextEditor c_textEditor;
        private XToolStripPanel c_pan;
        private System.Windows.Forms.ToolStripButton btn_buildAndAttach;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton btn_save;
        private IGKXToolStrip c_strip;

        public new GLEditorSurface CurrentSurface {
            get {
                return base.CurrentSurface as GLEditorSurface;
            }
        }
        public override Drawing2D.ICore2DDrawingDocument ToolDocument
        {
            get
            {
                return base.ToolDocument;
            }
        }
        public XGLShaderCodeEditor():base()
        {
            this.InitializeComponent();
        }
        public override void LoadDisplayText()
        {
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XGLShaderCodeEditor));
            this.c_strip = new IGKXToolStrip();
            this.btn_buildAndAttach = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_save = new System.Windows.Forms.ToolStripButton();
            this.c_pan = new IGK.DrSStudio.WinUI.XToolStripPanel();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.c_textEditor = new ICSharpCode.AvalonEdit.TextEditor();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.c_strip.SuspendLayout();
            this.c_pan.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_strip
            // 
            this.c_strip.CaptionKey = null;
            this.c_strip.Dock = System.Windows.Forms.DockStyle.None;
            this.c_strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_buildAndAttach,
            this.toolStripButton1,
            this.toolStripSeparator1,
            this.btn_save});
            this.c_strip.Location = new System.Drawing.Point(0, 0);
            this.c_strip.Name = "c_strip";
            this.c_strip.Size = new System.Drawing.Size(642, 25);
            this.c_strip.Stretch = true;
            this.c_strip.TabIndex = 0;
            // 
            // btn_buildAndAttach
            // 
            this.btn_buildAndAttach.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_buildAndAttach.Image = ((System.Drawing.Image)(resources.GetObject("btn_buildAndAttach.Image")));
            this.btn_buildAndAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_buildAndAttach.Name = "btn_buildAndAttach";
            this.btn_buildAndAttach.Size = new System.Drawing.Size(23, 22);
            this.btn_buildAndAttach.Text = "toolStripButton1";
            this.btn_buildAndAttach.Click += new System.EventHandler(this.btn_buildAndAttach_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_save
            // 
            this.btn_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_save.Image = ((System.Drawing.Image)(resources.GetObject("btn_save.Image")));
            this.btn_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(23, 22);
            this.btn_save.Text = "toolStripButton2";
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // c_pan
            // 
            this.c_pan.Controls.Add(this.c_strip);
            this.c_pan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_pan.Location = new System.Drawing.Point(0, 311);
            this.c_pan.Name = "c_pan";
            this.c_pan.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.c_pan.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.c_pan.Size = new System.Drawing.Size(642, 25);
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid1.Location = new System.Drawing.Point(363, 0);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(279, 311);
            this.propertyGrid1.TabIndex = 7;
            // 
            // elementHost1
            // 
            this.elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(363, 311);
            this.elementHost1.TabIndex = 8;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.c_textEditor;
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter1.Location = new System.Drawing.Point(360, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 311);
            this.splitter1.TabIndex = 9;
            this.splitter1.TabStop = false;
            // 
            // XGLShaderCodeEditor
            // 
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.propertyGrid1);
            this.Controls.Add(this.c_pan);
            this.Name = "XGLShaderCodeEditor";
            this.Size = new System.Drawing.Size(642, 336);
            this.Load += new System.EventHandler(this._Load);
            this.c_strip.ResumeLayout(false);
            this.c_strip.PerformLayout();
            this.c_pan.ResumeLayout(false);
            this.c_pan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        public XGLShaderCodeEditor(GLLibCodeEditorTool gLLibCodeEditorTool):base(gLLibCodeEditorTool )
        {
            this.gLLibCodeEditorTool = gLLibCodeEditorTool;
            this.InitializeComponent();
        }
        private void _Load(object sender, EventArgs e)
        {
            this.m_propertyInfo = new GLEditorPropertyInfo();
            FoldingManager man = FoldingManager.Install(this.c_textEditor.TextArea);
            (new XmlFoldingStrategy()).UpdateFoldings(man, this.c_textEditor.Document);
            //this.propertyGrid1.SelectedObject = Properties.Settings.Default;
            //this.propertyGrid1.PropertyValueChanged += propertyGrid1_PropertyValueChanged;
            //Properties.Settings.Default.SettingChanging += Default_SettingChanging;
            this.propertyGrid1.SelectedObject = m_propertyInfo;
            c_textEditor.FontSize = 12;
            c_textEditor.SyntaxHighlighting =
                ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("C++");
            this.c_textEditor.Text = "void main(){\n}";
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            //Save the 
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter =CoreSystem.GetString ( "{GLEditorRegexFilter}|*.gkvs; *.gkfs");
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(sfd.FileName, this.c_textEditor.Text);
                }
            }
        }
        private void btn_buildAndAttach_Click(object sender, EventArgs e)
        {
            string[] error = null;
            GLShaderEffect f =  GLEditorUtils.CompileShader(
                this.CurrentSurface.Device , this.c_textEditor.Text, this.m_propertyInfo.ShaderType ,
                out error);
            if (f == null)
            {
                //therer are error
            }
            else {
                this.CurrentSurface.Effects.Clear();
                this.CurrentSurface.Effects.Add(f);
            }
        }
    }
}

