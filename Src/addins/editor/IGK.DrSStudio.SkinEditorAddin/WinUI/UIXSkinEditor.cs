

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXSkinEditor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:UIXSkinEditor.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Drawing2D;
using IGK.DrSStudio.SkinEditorAddin.WinUI;

namespace IGK.DrSStudio
{
    using IGK.ICore.WinCore;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Settings;
    using IGK.ICore.Menu;
    using System.Threading;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.DrSStudio.SkinEditorAddin.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinCore.WinUI.Controls;
    /// <summary>
    /// represent application apparence Editor
    /// </summary>
    sealed class UIXSkinEditor : UIXConfigControlBase
    {
        private IUIXSkineEditorPropertyListener m_listener;
        private IGKXButton btn_add;
        private IGKXButton btn_remove;
        private IGKXButton btn_saveskin;
        private IGKXButton btn_reloadskin;
        private ICoreRendererSetting m_skinProperty;
        private IGKXButton btn_loadSkin;
        private IGKXSplitter c_splitter;
        private IGKXButton c_btn_context;
        private UIXSkinFontEditor c_fontEditor;
        private UIXCoreColorPicker c_uxColorPicker;
        private IGKXPanel c_pan_view;
        private IGKXLabel c_lb_title;
        private IGKXPanel c_pan_left;
        private IGKXPanel c_pan_Content;
        private IGKXPanel c_pan_bottom;
        private UIXSkinEditorListBox c_lsb_items;

        private UIXSkinEditorSelectValue c_skinEditorSelectValue;
        private IGKXButton c_btn_saveCompileFile;
        private IGKXButton c_btn_showMenuStrip;
        private TextBox c_txb_search;
        private bool m_configuring;
  
        private Thread m_th;

        public UIXSkinEditor()
        {
            this.InitializeComponent();  
            this.AutoScroll = true;
            this.Load += new EventHandler(_Load);
            this.MinimumSize = new Size(600, 500);
        }
        void _Load(object sender, EventArgs e)
        {

#if DEBUG
            this.c_btn_saveCompileFile.Enabled = true;
            this.c_btn_saveCompileFile.Visible = true;
#else 
            this.c_btn_saveCompileFile.Enabled = false ;
            this.c_btn_saveCompileFile.Visible =  false;
#endif
            this.c_lsb_items = new UIXSkinEditorListBox();
            this.c_lsb_items.Dock = DockStyle.Fill;
            this.c_lsb_items.IntegralHeight = false;
            this.c_lsb_items.SelectedIndexChanged += lsb_items_SelectedIndexChanged;

            this.c_skinEditorSelectValue = new UIXSkinEditorSelectValue();
            this.c_uxColorPicker = new UIXCoreColorPicker();
            this.c_fontEditor = new UIXSkinFontEditor();
            

            this.btn_loadSkin.Enabled = true;
            this.btn_saveskin.Enabled = true;
            this.c_skinEditorSelectValue.Visible = false;

            // 
            // uixSkinEditorSelectValue1
            // 
            this.c_skinEditorSelectValue.AllowDecimalValue = true;
            this.c_skinEditorSelectValue.Dock = DockStyle.Top;
            this.c_skinEditorSelectValue.CaptionKey = null;
            this.c_skinEditorSelectValue.Location = new System.Drawing.Point(6, 22);
            this.c_skinEditorSelectValue.Name = "uixSkinEditorSelectValue1";
            this.c_skinEditorSelectValue.Size = new System.Drawing.Size(317, 28);
            this.c_skinEditorSelectValue.TabIndex = 7;
            this.c_skinEditorSelectValue.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});

            this.c_uxColorPicker.AcceptButton = null;
            this.c_uxColorPicker.CancelButton = null;
            this.c_uxColorPicker.CaptionKey = null;
            this.c_uxColorPicker.Location = new System.Drawing.Point(6, 22);
            this.c_uxColorPicker.MinimumSize = new System.Drawing.Size(0, 116);
            this.c_uxColorPicker.Name = "c_uxColorPicker";
            this.c_uxColorPicker.Size = new System.Drawing.Size(317, 364);
            this.c_uxColorPicker.TabIndex = 0;
            this.c_uxColorPicker.SelectedColorChanged += new System.EventHandler(this.uixColorPicker1_ColorChanged);
            this.c_uxColorPicker.Dock = DockStyle.Fill;

            // 
            // c_uxColorPicker
            // 
            // 
            // c_fontEditor
            // 
            this.c_fontEditor.CaptionKey = null;
            this.c_fontEditor.Location = new System.Drawing.Point(6, 22);
            this.c_fontEditor.MinimumSize = new System.Drawing.Size(0, 116);
            this.c_fontEditor.Name = "c_fontEditor";
            this.c_fontEditor.Size = new System.Drawing.Size(317, 364);
            this.c_fontEditor.TabIndex = 0;
            this.c_fontEditor.Dock = DockStyle.Fill;


            this.c_uxColorPicker.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.c_pan_view.Dock = DockStyle.Fill;
            this.c_pan_view.Controls.Add(this.c_fontEditor);
            this.c_pan_view.Controls.Add(this.c_skinEditorSelectValue);
            this.c_pan_view.Controls.Add(this.c_uxColorPicker);

            this.c_pan_view.AutoScroll = true;
            


            this.c_pan_left.Controls.Add(this.ListBox);
            this.c_pan_left.Controls.SetChildIndex(this.ListBox, 0);

           // new XSplitterDragManager(this.c_splitter, this.c_pan_left );     

            this.c_lb_title.Text = string.Empty;
            this.c_lb_title.Font = new Font(this.c_lb_title.Font.FontFamily , 24, FontStyle.Regular, GraphicsUnit.Point);

            _hideTool();
            this.initProperties();
        }
        public void SetPropertyListener(IUIXSkineEditorPropertyListener listerner)
        {
            this.m_listener = listerner;
            if (this.IsHandleCreated)
            {
                initProperties();
            }
        }

        private void initProperties()
        {
            if (this.m_listener == null)
                return;
            ICoreRendererSetting[] i = this.m_listener.GetRendererSetting();
            
            if (this.c_lsb_items != null)
            {
                this.c_lsb_items.Items.AddRange(i);
                this.c_lsb_items.Sorted = true;
                this.c_lsb_items.DisplayMember = "Name";
                this.m_SettingsList = i;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ListBox ListBox
        {
            get
            {
                return this.c_lsb_items;
            }
        }
        /// <summary>
        /// gets or sets the skin property to edit
        /// </summary>
        public ICoreRendererSetting SkinProperty
        {
            get
            {
                return this.m_skinProperty;
            }
            set
            {
                if (this.m_skinProperty != value)
                {
                    if (this.m_skinProperty != null)
                        this.m_skinProperty.ValueChanged -= new EventHandler(m_skinProperty_ValueChanged);     
                    this.m_skinProperty = value;
                    if (this.m_skinProperty !=null)
                    this.m_skinProperty.ValueChanged += new EventHandler(m_skinProperty_ValueChanged);
                    OnSkinPropertyChanged(EventArgs.Empty);
                }
            }
        }
        void m_skinProperty_ValueChanged(object sender, EventArgs e)
        {
            //refresh the main formlµ
            CoreSystem.Instance.MainForm.Refresh();
            Form frm = this.FindForm ();
            if (frm != null)
            {
                //refresh the current form
                frm.Refresh();
            }
            if (this.m_menuStripEditor!=null)
            {
                this.m_menuStripEditor.Refresh();
            }
            if (this.m_cmenuStripEditor != null)
                this.m_cmenuStripEditor.Refresh();
        }
        private void OnSkinPropertyChanged(EventArgs eventArgs)
        {
            if (this.SkinProperty == null)
            {
                this.c_pan_Content.Enabled = false;
            }
            else
            {
                this.c_pan_Content.Enabled = true;
                this.btn_remove.Enabled = false;
                this.c_pan_Content.SuspendLayout();
                this.m_configuring = true;

                _hideTool();
                switch (this.SkinProperty.Type )
                {
                    case enuRendererSettingType .Color:
                        this.c_uxColorPicker.SelectedColor = (Colorf)SkinProperty.Value;
                        this.c_uxColorPicker.Enabled = true;
                        this.c_uxColorPicker.Visible = true;
                        
                        this.c_skinEditorSelectValue.Visible = false;
                        break;
                    case enuRendererSettingType.Int:
                        this.c_uxColorPicker.Visible = false;
                        
                        this.c_skinEditorSelectValue.Visible = true;
                        this.c_skinEditorSelectValue.AllowDecimalValue = false;
                        this.c_skinEditorSelectValue.Text = this.SkinProperty.Value.ToString();
                        break;
                    case enuRendererSettingType.Float:
                        this.c_skinEditorSelectValue.Visible = true;
                        this.c_skinEditorSelectValue.AllowDecimalValue = true;
                        this.c_skinEditorSelectValue.Text = this.SkinProperty.Value.ToString();                        
                        break;
                    case enuRendererSettingType.File:
                        break;
                    case enuRendererSettingType.Font :
                        this.c_fontEditor.Visible = true;
                        this.c_fontEditor.CoreFont = (CoreFont)SkinProperty.Value;
                        break;
                    default:

                        break;
                }
                this.m_configuring = false;
                this.c_pan_Content.ResumeLayout();
            }
        }

        private void _hideTool()
        {
            this.c_fontEditor.Visible = false;
            this.c_uxColorPicker.Visible = false;
            this.c_skinEditorSelectValue.Visible = false;
        }
        private void InitializeComponent()
        {
            this.btn_reloadskin = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.btn_add = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.btn_remove = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.btn_saveskin = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.btn_loadSkin = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.c_pan_left = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_txb_search = new System.Windows.Forms.TextBox();
            this.c_splitter = new IGK.ICore.WinCore.WinUI.Controls.IGKXSplitter();
            this.c_pan_Content = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_pan_view = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_lb_title = new IGK.ICore.WinCore.WinUI.Controls.IGKXLabel();
            this.c_pan_bottom = new IGK.ICore.WinCore.WinUI.Controls.IGKXPanel();
            this.c_btn_context = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.c_btn_showMenuStrip = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.c_btn_saveCompileFile = new IGK.ICore.WinCore.WinUI.Controls.IGKXButton();
            this.c_pan_left.SuspendLayout();
            this.c_pan_Content.SuspendLayout();
            this.c_pan_bottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_reloadskin
            // 
            this.btn_reloadskin.CaptionKey = "btn.reload";
            this.btn_reloadskin.Checked = false;
            this.btn_reloadskin.Location = new System.Drawing.Point(183, 27);
            this.btn_reloadskin.Name = "btn_reloadskin";
            this.btn_reloadskin.ShowButtonImage = false;
            this.btn_reloadskin.Size = new System.Drawing.Size(97, 20);
            this.btn_reloadskin.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_reloadskin.TabIndex = 0;
            this.btn_reloadskin.Click += new System.EventHandler(this.btn_reloadskin_Click);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_add.CaptionKey = "btn.add.caption";
            this.btn_add.Checked = false;
            this.btn_add.Location = new System.Drawing.Point(94, 3);
            this.btn_add.Name = "btn_add";
            this.btn_add.ShowButtonImage = false;
            this.btn_add.Size = new System.Drawing.Size(83, 19);
            this.btn_add.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_add.TabIndex = 1;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_remove.CaptionKey = "btn.remove.caption";
            this.btn_remove.Checked = false;
            this.btn_remove.Enabled = false;
            this.btn_remove.Location = new System.Drawing.Point(5, 29);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.ShowButtonImage = false;
            this.btn_remove.Size = new System.Drawing.Size(83, 19);
            this.btn_remove.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_remove.TabIndex = 3;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_saveskin
            // 
            this.btn_saveskin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_saveskin.CaptionKey = "btn.saveskinAsXML.caption";
            this.btn_saveskin.Checked = false;
            this.btn_saveskin.Enabled = false;
            this.btn_saveskin.Location = new System.Drawing.Point(94, 28);
            this.btn_saveskin.Name = "btn_saveskin";
            this.btn_saveskin.ShowButtonImage = false;
            this.btn_saveskin.Size = new System.Drawing.Size(83, 19);
            this.btn_saveskin.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_saveskin.TabIndex = 4;
            this.btn_saveskin.Click += new System.EventHandler(this.btn_saveskin_Click);
            // 
            // btn_loadSkin
            // 
            this.btn_loadSkin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_loadSkin.CaptionKey = "btn.loadXMLskin.caption";
            this.btn_loadSkin.Checked = false;
            this.btn_loadSkin.Enabled = false;
            this.btn_loadSkin.Location = new System.Drawing.Point(183, 3);
            this.btn_loadSkin.Name = "btn_loadSkin";
            this.btn_loadSkin.ShowButtonImage = false;
            this.btn_loadSkin.Size = new System.Drawing.Size(83, 19);
            this.btn_loadSkin.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.btn_loadSkin.TabIndex = 0;
            this.btn_loadSkin.Click += new System.EventHandler(this.btn_loadSkin_Click);
            // 
            // c_pan_left
            // 
            this.c_pan_left.AutoScroll = true;
            this.c_pan_left.CaptionKey = null;
            this.c_pan_left.Controls.Add(this.c_txb_search);
            this.c_pan_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_pan_left.Location = new System.Drawing.Point(0, 0);
            this.c_pan_left.Margin = new System.Windows.Forms.Padding(0);
            this.c_pan_left.Name = "c_pan_left";
            this.c_pan_left.Size = new System.Drawing.Size(242, 411);
            this.c_pan_left.TabIndex = 0;
            // 
            // c_txb_search
            // 
            this.c_txb_search.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_txb_search.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.c_txb_search.Location = new System.Drawing.Point(0, 0);
            this.c_txb_search.Name = "c_txb_search";
            this.c_txb_search.Size = new System.Drawing.Size(242, 20);
            this.c_txb_search.TabIndex = 0;
            this.c_txb_search.TextChanged += new System.EventHandler(this.c_txb_search_TextChanged);
            // 
            // c_splitter
            // 
            this.c_splitter.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.c_splitter.Location = new System.Drawing.Point(242, 0);
            this.c_splitter.Name = "c_splitter";
            this.c_splitter.Size = new System.Drawing.Size(4, 411);
            this.c_splitter.TabIndex = 8;
            this.c_splitter.TabStop = false;
            // 
            // c_pan_Content
            // 
            this.c_pan_Content.CaptionKey = null;
            this.c_pan_Content.Controls.Add(this.c_pan_view);
            this.c_pan_Content.Controls.Add(this.c_lb_title);
            this.c_pan_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_Content.Location = new System.Drawing.Point(246, 0);
            this.c_pan_Content.Margin = new System.Windows.Forms.Padding(0);
            this.c_pan_Content.Name = "c_pan_Content";
            this.c_pan_Content.Size = new System.Drawing.Size(301, 411);
            this.c_pan_Content.TabIndex = 9;
            // 
            // c_pan_view
            // 
            this.c_pan_view.CaptionKey = null;
            this.c_pan_view.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_view.Location = new System.Drawing.Point(0, 14);
            this.c_pan_view.Name = "c_pan_view";
            this.c_pan_view.Size = new System.Drawing.Size(301, 397);
            this.c_pan_view.TabIndex = 1;
            // 
            // c_lb_title
            // 
            this.c_lb_title.CaptionKey = "lb.title";
            this.c_lb_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_lb_title.HorizontalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            this.c_lb_title.Location = new System.Drawing.Point(0, 0);
            this.c_lb_title.Name = "c_lb_title";
            this.c_lb_title.Size = new System.Drawing.Size(34, 14);
            this.c_lb_title.TabIndex = 0;
            this.c_lb_title.TabStop = false;
            this.c_lb_title.VerticalAlignment = IGK.ICore.Drawing2D.enuStringAlignment.Near;
            // 
            // c_pan_bottom
            // 
            this.c_pan_bottom.CaptionKey = null;
            this.c_pan_bottom.Controls.Add(this.c_btn_context);
            this.c_pan_bottom.Controls.Add(this.c_btn_showMenuStrip);
            this.c_pan_bottom.Controls.Add(this.c_btn_saveCompileFile);
            this.c_pan_bottom.Controls.Add(this.btn_saveskin);
            this.c_pan_bottom.Controls.Add(this.btn_add);
            this.c_pan_bottom.Controls.Add(this.btn_remove);
            this.c_pan_bottom.Controls.Add(this.btn_loadSkin);
            this.c_pan_bottom.Controls.Add(this.btn_reloadskin);
            this.c_pan_bottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_pan_bottom.Location = new System.Drawing.Point(0, 411);
            this.c_pan_bottom.Margin = new System.Windows.Forms.Padding(0);
            this.c_pan_bottom.MaximumSize = new System.Drawing.Size(0, 62);
            this.c_pan_bottom.MinimumSize = new System.Drawing.Size(150, 62);
            this.c_pan_bottom.Name = "c_pan_bottom";
            this.c_pan_bottom.Size = new System.Drawing.Size(547, 62);
            this.c_pan_bottom.TabIndex = 10;
            // 
            // c_btn_context
            // 
            this.c_btn_context.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_context.CaptionKey = "btn.showContextMenuStrip.caption";
            this.c_btn_context.Checked = false;
            this.c_btn_context.Location = new System.Drawing.Point(451, 29);
            this.c_btn_context.Name = "c_btn_context";
            this.c_btn_context.ShowButtonImage = false;
            this.c_btn_context.Size = new System.Drawing.Size(83, 19);
            this.c_btn_context.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_context.TabIndex = 6;
            this.c_btn_context.Click += new System.EventHandler(this.c_btn_context_Click);
            // 
            // c_btn_showMenuStrip
            // 
            this.c_btn_showMenuStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.c_btn_showMenuStrip.CaptionKey = "btn.showMenuStrip.caption";
            this.c_btn_showMenuStrip.Checked = false;
            this.c_btn_showMenuStrip.Location = new System.Drawing.Point(451, 4);
            this.c_btn_showMenuStrip.Name = "c_btn_showMenuStrip";
            this.c_btn_showMenuStrip.ShowButtonImage = false;
            this.c_btn_showMenuStrip.Size = new System.Drawing.Size(83, 19);
            this.c_btn_showMenuStrip.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_showMenuStrip.TabIndex = 5;
            this.c_btn_showMenuStrip.Click += new System.EventHandler(this.igkxButton1_Click);
            // 
            // c_btn_saveCompileFile
            // 
            this.c_btn_saveCompileFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.c_btn_saveCompileFile.CaptionKey = "btn.saveCSskin.caption";
            this.c_btn_saveCompileFile.Checked = false;
            this.c_btn_saveCompileFile.Location = new System.Drawing.Point(183, 4);
            this.c_btn_saveCompileFile.Name = "c_btn_saveCompileFile";
            this.c_btn_saveCompileFile.ShowButtonImage = false;
            this.c_btn_saveCompileFile.Size = new System.Drawing.Size(83, 19);
            this.c_btn_saveCompileFile.State = IGK.ICore.WinUI.enuButtonState.Normal;
            this.c_btn_saveCompileFile.TabIndex = 2;
            this.c_btn_saveCompileFile.Click += new System.EventHandler(this.c_btn_saveCompileFile_Click);
            // 
            // UIXSkinEditor
            // 
            this.AutoScroll = true;
            this.Controls.Add(this.c_pan_Content);
            this.Controls.Add(this.c_splitter);
            this.Controls.Add(this.c_pan_left);
            this.Controls.Add(this.c_pan_bottom);
            this.Name = "UIXSkinEditor";
            this.Size = new System.Drawing.Size(547, 473);
            this.c_pan_left.ResumeLayout(false);
            this.c_pan_left.PerformLayout();
            this.c_pan_Content.ResumeLayout(false);
            this.c_pan_Content.PerformLayout();
            this.c_pan_bottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        void btn_reloadskin_Click(object sender, EventArgs e)
        {
            this.ListBox.SuspendLayout();
            this.ListBox.Items.Clear();
            this.c_txb_search.ResetText();
            this.initProperties();
            this.ListBox.ResumeLayout();
        }
        void btn_remove_Click(object sender, EventArgs e)
        {
            if (c_lsb_items.SelectedItem != null)
            {
                ICoreRendererSetting pro = c_lsb_items.SelectedItem as ICoreRendererSetting;
                if (pro != null)
                {
                    CoreRenderer.Remove(pro);
                    this.c_lsb_items.Items.Remove(pro);
                }
            }
        }
        
        private void lsb_items_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (c_lsb_items.SelectedItem != null)
            {
                ICoreRendererSetting pro = c_lsb_items.SelectedItem as ICoreRendererSetting;
                if (pro != null)
                {
                    this.SkinProperty = pro;
                }
                else
                    this.SkinProperty = null;
                this.c_lb_title.Text = pro.Name;
            }
        }
        private void uixColorPicker1_ColorChanged(object sender, EventArgs e)
        {
            if ((this.SkinProperty == null)
                || (this.m_configuring) ||
                (this.SkinProperty.Type !=  enuRendererSettingType .Color )
                ) 
                return;
            this.SkinProperty.Value =(Colorf ) c_uxColorPicker.SelectedColor;
        }
        private void btn_saveskin_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.FileName = "primary-skin.xml";
                sfd.Filter = "Skin file |*.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                  CoreRenderer.Save(sfd.FileName);
                }
            }
            this.FindForm().DialogResult = DialogResult.None;
        }
        private void btn_loadSkin_Click(object sender, EventArgs e)
        {
            LoadSkinFromFile();
        }
        private void LoadSkinFromFile()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "title.LoadAFile_1".R();
                ofd.Filter = "Skin file | *.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    CoreRenderer.Configure(ofd.FileName);
                }
            }
            OnSkinPropertyChanged(EventArgs.Empty);
            this.FindForm().DialogResult = DialogResult.None;
        }
        private void xtxb_value_TextChanged(object sender, EventArgs e)
        {
            if ((this.m_configuring) || (this.m_skinProperty == null))
                return;
            switch (this.m_skinProperty.Type)
            {
                case enuRendererSettingType .Int:
                    this.m_skinProperty.Value = Convert.ToInt32(c_skinEditorSelectValue.Value);
                    break;
                default:
                    this.m_skinProperty.Value = c_skinEditorSelectValue.Value;
                    break;
            }
        }
        private void c_btnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.m_skinProperty.Value = ofd.FileName;
                }
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            (CoreSystem.GetMenuAction("File") as CoreMenuActionBase).MenuItem.ShowDropDown();
        }

        private void c_btn_saveCompileFile_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Filter = "csharp source file | *.cs";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    CoreRenderer.SaveToCompile(ofd.FileName);
                }
            }
        }

        SkinContexMenuEditor m_menuStripEditor;
        private ICoreRendererSetting[] m_SettingsList;
        private SkinContexMenuEditor m_cmenuStripEditor;


        private void c_btn_context_Click(object sender, EventArgs e)
        {
            if ((m_menuStripEditor == null) || (m_menuStripEditor.IsDisposed))
            {
                ContextMenuStrip r = CoreSystem.GetMainForm().AppContextMenu as ContextMenuStrip;


                m_cmenuStripEditor = new SkinContexMenuEditor(enuMenuMode.ContextMenuStrip , r.Renderer);
                m_cmenuStripEditor.Owner = this.FindForm();
            }
            if (m_cmenuStripEditor == null)
                return;
            m_cmenuStripEditor.StartPosition = FormStartPosition.CenterParent;
            m_cmenuStripEditor.ShowIcon = false;
            m_cmenuStripEditor.ShowInTaskbar = false;
            m_cmenuStripEditor.AutoSize = true;
            m_cmenuStripEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            m_cmenuStripEditor.Text = "title.viewMenuStripSample".R();
            m_cmenuStripEditor.Show();
            
        }
        private void igkxButton1_Click(object sender, EventArgs e)
        {
            if ((m_menuStripEditor == null) || (m_menuStripEditor.IsDisposed))
            {
                MenuStrip r = CoreSystem.GetMainForm().AppMenu as MenuStrip;


                m_menuStripEditor = new SkinContexMenuEditor(enuMenuMode.MenuStrip  , r.Renderer );
                m_menuStripEditor.Owner = this.FindForm();
            }
            m_menuStripEditor.StartPosition = FormStartPosition.CenterParent;
            m_menuStripEditor.ShowIcon = false;
            m_menuStripEditor.ShowInTaskbar = false;
            m_menuStripEditor.AutoSize = true;
            m_menuStripEditor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly; 
            m_menuStripEditor.Text = "title.viewMenuStripSample".R();
            m_menuStripEditor.Show();
        }

        private void c_txb_search_TextChanged(object sender, EventArgs e)
        {
            //search for new items
          
            this.StartSearch();
        }

        private void StartSearch()
        {
            AbortSearch();
            m_th = new Thread(LoadItem);
            m_th.Name = "SkinEditorLoadItemThread";
            m_th.IsBackground = true;
            m_th.Start();
          
        }

        private void LoadItem()
        {
            if (!string.IsNullOrEmpty(this.c_txb_search.Text))
            {
                string s = this.c_txb_search.Text.ToLower().Replace(".", "\\.").Replace("*", "(.)+");
                List<object> r = new List<object>();

                foreach (var e in this.m_SettingsList)
                {
                    if (Regex.IsMatch(e.Name, s, RegexOptions.IgnoreCase))
                    {
                        r.Add(e);
                    }
                }

                this.ListBox.Invoke((MethodInvoker)delegate()
                {
                    this.ListBox.SuspendLayout();
                    this.ListBox.Items.Clear();
                    this.ListBox.Items.AddRange(r.ToArray());
                    this.ListBox.ResumeLayout();
                });

            }
            else {
                this.ListBox.Invoke((MethodInvoker)delegate()
                {
                    this.ListBox.SuspendLayout();
                    this.ListBox.Items.Clear();
                    this.ListBox.Items.AddRange(m_SettingsList);
                    this.ListBox.ResumeLayout();
                });
            }
           
        }

        private void AbortSearch()
        {
            if (this.m_th != null)
            {
                this.m_th.Abort();
                this.m_th.Join();
            }
        }

       

    }
}

