

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidAppPage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Android.Settings;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.WinUI
{

    using IGK.DrSStudio.Android.Settings;
    using IGK.DrSStudio.Android.Tools;
    
    using System.Text.RegularExpressions;
    using IGK.ICore.WinCore.WinUI.Controls;

    class AndroidAppPage : IGKXUserControl, ICoreWorkingConfigurableObject
    {
        private System.Windows.Forms.TextBox c_txb_appName;
        private System.Windows.Forms.TextBox c_txb_packageName;
        private IGKXLabel igkxLabel2;
        private IGKXLabel igkxLabel3;
        private System.Windows.Forms.ComboBox c_cmb_targetVersion;
        private System.Windows.Forms.ComboBox c_cmb_minVersion;
        
        private IGKXLabel igkxLabel1;
        
        private IGKXLabel igkxLabel4;
        private System.Windows.Forms.TextBox c_txb_mainActivity;
        private IGKXLabel igkxLabel5;
        private IGKXRuleLabel igkxRuleLabel1;
        private System.Windows.Forms.TextBox c_txb_targetDir;
        private IGKXLabel igkxLabel6;
        private IGKXButton c_btn_ok;
        private ErrorProvider c_error_provider;
        private System.ComponentModel.IContainer components;
        private AndroidWizardGUI m_androidWizardGUI;
    
        public AndroidAppPage()
        {
            this.InitializeComponent();
            this.Load += _Load;
        }

        public AndroidAppPage(AndroidWizardGUI androidWizardGUI):this()
        {
            this.m_androidWizardGUI = androidWizardGUI;
        }

        void _Load(object sender, EventArgs e)
        {
            this.c_txb_targetDir.TextChanged += c_txb_targetDir_TextChanged;
            this.c_txb_targetDir.Text = AndroidSetting.Instance.AndroidWorkingDirectory;
            //load targetversion
            this.c_cmb_targetVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            AndroidTargetInfo[] t = AndroidTool.Instance.GetAndroidTargets();
            if ((t != null) && (t.Length > 0))
            {
                AndroidCmbTargetItem[] r = new  AndroidCmbTargetItem[t.Length];
                int i =0;
                foreach (AndroidTargetInfo item in t)
                {
                    r[i]=new AndroidCmbTargetItem(item);
                    i++;    
                }
                this.c_cmb_minVersion.DataSource = r;
                this.c_cmb_minVersion.DisplayMember = "Display";
                this.c_cmb_minVersion.ValueMember = "Item";
                this.c_cmb_minVersion.SelectedValue = r[0].Item ;
                this.c_cmb_minVersion.DropDownStyle = ComboBoxStyle.DropDownList;

                AndroidCmbTargetItem[] d = (AndroidCmbTargetItem[]) r.Clone () ;
                this.c_cmb_targetVersion.DataSource =d;
                this.c_cmb_targetVersion.DisplayMember = "Display";
                this.c_cmb_targetVersion.ValueMember = "Item";
                this.c_cmb_targetVersion.SelectedValue = d[d.Length - 1].Item;
                this.c_cmb_targetVersion.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            this.c_cmb_targetVersion.SelectedIndexChanged += c_cmb_targetVersion_SelectedIndexChanged;
            this.c_cmb_minVersion.SelectedIndexChanged += c_cmb_targetVersion_SelectedIndexChanged;
            if (this.c_cmb_targetVersion.Items.Count > 0)
                this.c_cmb_targetVersion.SelectedIndex = 0;
            this.c_txb_mainActivity.TextChanged += c_txb_mainActivity_TextChanged;
            this.c_txb_packageName.TextChanged += c_txb_packageName_TextChanged;
            this.c_txb_appName.TextChanged += c_txb_appName_TextChanged;

            this.InitInfo();
        }

        private void InitInfo()
        {
            this.m_androidWizardGUI.ProjectName = this.c_txb_appName.Text;
            this.m_androidWizardGUI.PackageName = this.c_txb_packageName.Text;
            this.m_androidWizardGUI.MainActivity = this.c_txb_mainActivity.Text;
            this.m_androidWizardGUI.ProjectLocation = this.c_txb_targetDir.Text;
        }

        void c_txb_appName_TextChanged(object sender, EventArgs e)
        {
            this.m_androidWizardGUI.ProjectName = this.c_txb_appName.Text;
        }

        void c_txb_packageName_TextChanged(object sender, EventArgs e)
        {
            string s = this.c_txb_packageName.Text;
            if (checkPackageName(s))
            {
                this.m_androidWizardGUI.PackageName = this.c_txb_packageName.Text;
                this.c_error_provider.SetError(this.c_txb_packageName, null);
            }
            else {
                this.c_error_provider.SetError(this.c_txb_packageName, "android.err.packagenamenotvalid".R());
            }
        }

        private bool checkPackageName(string s)
        {
            bool r = Regex.IsMatch(s, @"^[a-z_]+(\.{0,1}[a-z_]+)*$", RegexOptions.IgnoreCase);
            return r;// Regex.IsMatch(s, @"^(([a-z_]+(\.{0,1}))+)[^\.]$", RegexOptions.IgnoreCase);
        }

        void c_txb_mainActivity_TextChanged(object sender, EventArgs e)
        {
            this.m_androidWizardGUI.MainActivity = this.c_txb_mainActivity.Text;
        }

        void c_txb_targetDir_TextChanged(object sender, EventArgs e)
        {
            this.m_androidWizardGUI.ProjectLocation = this.c_txb_targetDir.Text;
        }

        void c_cmb_targetVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.m_androidWizardGUI.AndroidPlatformTarget =
                ((AndroidCmbTargetItem)this.c_cmb_targetVersion.SelectedItem).Item;
            this.m_androidWizardGUI.AndroidMinTarget =
    ((AndroidCmbTargetItem)this.c_cmb_minVersion.SelectedItem).Item;
        }
        

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.igkxLabel1 = new IGKXLabel();
            this.c_txb_appName = new System.Windows.Forms.TextBox();
            this.c_txb_packageName = new System.Windows.Forms.TextBox();
            this.igkxLabel2 = new IGKXLabel();
            this.igkxLabel3 = new IGKXLabel();
            this.c_cmb_targetVersion = new System.Windows.Forms.ComboBox();
            this.c_cmb_minVersion = new System.Windows.Forms.ComboBox();
            this.igkxLabel4 = new IGKXLabel();
            this.c_txb_mainActivity = new System.Windows.Forms.TextBox();
            this.igkxLabel5 = new IGKXLabel();
            this.igkxRuleLabel1 = new IGKXRuleLabel();
            this.c_txb_targetDir = new System.Windows.Forms.TextBox();
            this.igkxLabel6 = new IGKXLabel();
            this.c_btn_ok = new IGKXButton();
            this.c_error_provider = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.c_error_provider)).BeginInit();
            this.SuspendLayout();
            // 
            // igkxLabel1
            // 
            this.igkxLabel1.CaptionKey = "lb.appnamem";
            this.igkxLabel1.Location = new System.Drawing.Point(20, 26);
            this.igkxLabel1.Name = "igkxLabel1";
            this.igkxLabel1.Size = new System.Drawing.Size(73, 14);
            this.igkxLabel1.TabIndex = 0;
            this.igkxLabel1.TabStop = false;
            // 
            // c_txb_appName
            // 
            this.c_txb_appName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.c_txb_appName.Location = new System.Drawing.Point(140, 26);
            this.c_txb_appName.Name = "c_txb_appName";
            this.c_txb_appName.Size = new System.Drawing.Size(214, 20);
            this.c_txb_appName.TabIndex = 1;
            this.c_txb_appName.Text = "app_name";
            // 
            // c_txb_packageName
            // 
            this.c_txb_packageName.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.c_txb_packageName.Location = new System.Drawing.Point(140, 52);
            this.c_txb_packageName.Name = "c_txb_packageName";
            this.c_txb_packageName.Size = new System.Drawing.Size(214, 20);
            this.c_txb_packageName.TabIndex = 3;
            this.c_txb_packageName.Text = "com.application";
            // 
            // igkxLabel2
            // 
            this.igkxLabel2.CaptionKey = "lb.packagename";
            this.igkxLabel2.Location = new System.Drawing.Point(20, 52);
            this.igkxLabel2.Name = "igkxLabel2";
            this.igkxLabel2.Size = new System.Drawing.Size(87, 14);
            this.igkxLabel2.TabIndex = 2;
            this.igkxLabel2.TabStop = false;
            // 
            // igkxLabel3
            // 
            this.igkxLabel3.CaptionKey = "lb.tagetversion";
            this.igkxLabel3.Location = new System.Drawing.Point(20, 110);
            this.igkxLabel3.Name = "igkxLabel3";
            this.igkxLabel3.Size = new System.Drawing.Size(78, 14);
            this.igkxLabel3.TabIndex = 4;
            this.igkxLabel3.TabStop = false;
            // 
            // c_cmb_targetVersion
            // 
            this.c_cmb_targetVersion.FormattingEnabled = true;
            this.c_cmb_targetVersion.Location = new System.Drawing.Point(140, 108);
            this.c_cmb_targetVersion.Name = "c_cmb_targetVersion";
            this.c_cmb_targetVersion.Size = new System.Drawing.Size(214, 21);
            this.c_cmb_targetVersion.TabIndex = 5;
            // 
            // c_minVersion
            // 
            this.c_cmb_minVersion.FormattingEnabled = true;
            this.c_cmb_minVersion.Location = new System.Drawing.Point(140, 202);
            this.c_cmb_minVersion.Name = "c_minVersion";
            this.c_cmb_minVersion.Size = new System.Drawing.Size(214, 21);
            this.c_cmb_minVersion.TabIndex = 7;
            // 
            // igkxLabel4
            // 
            this.igkxLabel4.CaptionKey = "lb.minversion";
            this.igkxLabel4.Location = new System.Drawing.Point(20, 204);
            this.igkxLabel4.Name = "igkxLabel4";
            this.igkxLabel4.Size = new System.Drawing.Size(71, 14);
            this.igkxLabel4.TabIndex = 6;
            this.igkxLabel4.TabStop = false;
            // 
            // c_txb_mainActivity
            // 
            this.c_txb_mainActivity.Location = new System.Drawing.Point(140, 78);
            this.c_txb_mainActivity.Name = "c_txb_mainActivity";
            this.c_txb_mainActivity.Size = new System.Drawing.Size(214, 20);
            this.c_txb_mainActivity.TabIndex = 9;
            this.c_txb_mainActivity.Text = "mainActivity";
            // 
            // igkxLabel5
            // 
            this.igkxLabel5.CaptionKey = "lb.mainactivity";
            this.igkxLabel5.Location = new System.Drawing.Point(20, 78);
            this.igkxLabel5.Name = "igkxLabel5";
            this.igkxLabel5.Size = new System.Drawing.Size(76, 14);
            this.igkxLabel5.TabIndex = 8;
            this.igkxLabel5.TabStop = false;
            // 
            // igkxRuleLabel1
            // 
            this.igkxRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.igkxRuleLabel1.CaptionKey = null;
            this.igkxRuleLabel1.Location = new System.Drawing.Point(20, 175);
            this.igkxRuleLabel1.Name = "igkxRuleLabel1";
            this.igkxRuleLabel1.Size = new System.Drawing.Size(334, 23);
            this.igkxRuleLabel1.TabIndex = 10;
            this.igkxRuleLabel1.TabStop = false;
            // 
            // c_txb_targetDir
            // 
            this.c_txb_targetDir.Location = new System.Drawing.Point(140, 141);
            this.c_txb_targetDir.Name = "c_txb_targetDir";
            this.c_txb_targetDir.Size = new System.Drawing.Size(171, 20);
            this.c_txb_targetDir.TabIndex = 12;
            this.c_txb_targetDir.Text = "app_name";
            // 
            // igkxLabel6
            // 
            this.igkxLabel6.CaptionKey = "lb.appnamem";
            this.igkxLabel6.Location = new System.Drawing.Point(20, 141);
            this.igkxLabel6.Name = "igkxLabel6";
            this.igkxLabel6.Size = new System.Drawing.Size(73, 14);
            this.igkxLabel6.TabIndex = 11;
            this.igkxLabel6.TabStop = false;
            // 
            // igkxButton1
            // 
            this.c_btn_ok.CaptionKey = null;
            this.c_btn_ok.Checked = false;
            this.c_btn_ok.DialogResult = enuDialogResult.None;
            this.c_btn_ok.Location = new System.Drawing.Point(322, 141);
            this.c_btn_ok.Name = "igkxButton1";
            this.c_btn_ok.ShowButtonImage = false;
            this.c_btn_ok.Size = new System.Drawing.Size(32, 20);
            this.c_btn_ok.State = enuButtonState.Normal;
            this.c_btn_ok.TabIndex = 13;
            this.c_btn_ok.Click += new System.EventHandler(this.igkxButton1_Click);
            // 
            // c_error_provider
            // 
            this.c_error_provider.ContainerControl = this;
            // 
            // AndroidAppPage
            // 
            this.Controls.Add(this.c_btn_ok);
            this.Controls.Add(this.c_txb_targetDir);
            this.Controls.Add(this.igkxLabel6);
            this.Controls.Add(this.igkxRuleLabel1);
            this.Controls.Add(this.c_txb_mainActivity);
            this.Controls.Add(this.igkxLabel5);
            this.Controls.Add(this.c_cmb_minVersion);
            this.Controls.Add(this.igkxLabel4);
            this.Controls.Add(this.c_cmb_targetVersion);
            this.Controls.Add(this.igkxLabel3);
            this.Controls.Add(this.c_txb_packageName);
            this.Controls.Add(this.igkxLabel2);
            this.Controls.Add(this.c_txb_appName);
            this.Controls.Add(this.igkxLabel1);
            this.Name = "AndroidAppPage";
            this.Size = new System.Drawing.Size(376, 270);
            ((System.ComponentModel.ISupportInitialize)(this.c_error_provider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public ICoreControl GetConfigControl()
        {
            return null;
        }

        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }

        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {

            return parameters;
        }

        private void igkxButton1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = Environment.CurrentDirectory;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.c_txb_targetDir.Text = fbd.SelectedPath;
                }
            }
        }

        /// <summary>
        /// represent the Target Item
        /// </summary>
        public sealed class AndroidCmbTargetItem
        {
            private AndroidTargetInfo m_item;
            private string m_name; //name of this
            private string m_displayName; //display name
            
            public AndroidTargetInfo Item { get { return this.m_item; } }
            public string Name{get{return this.m_name;}}
            public string DisplayName { get { return this.m_displayName;  } }
            public string Display { get { return this.ToString(); } }
            public AndroidCmbTargetItem(AndroidTargetInfo item)
            {
                this.m_item = item;
                this.m_name = item.GetInfo("Name");
                var i = item.GetVersion();
                this.m_displayName = i!=null?i.Name : string.Empty ;
            }

            public override string ToString()
            {
                return string.Format("{0}  - ({1})",  m_name, m_displayName);
            }
        }
    }
}
