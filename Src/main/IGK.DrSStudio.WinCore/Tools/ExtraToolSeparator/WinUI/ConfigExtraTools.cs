

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConfigExtraTools.cs
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
file:ConfigExtraTools.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.DrSStudio.Settings;
    using IGK.DrSStudio.Tools;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI.Common;

    class UIXConfigExtraTools : IGKXUserControl 
    {
        public UIXConfigExtraTools()
        {
            this.InitializeComponent();
            lsb.Dock = DockStyle.Fill;
            this.lsb.IntegralHeight = false;
        }
        private void InitializeComponent()
        {
            this.lsb = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_remove = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.lb_info = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsb
            // 
            this.lsb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsb.Location = new System.Drawing.Point(0, 0);
            this.lsb.Name = "lsb";
            this.lsb.Size = new System.Drawing.Size(203, 316);
            this.lsb.TabIndex = 0;
            this.lsb.SelectedIndexChanged += new System.EventHandler(this.lsb_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_remove);
            this.panel1.Controls.Add(this.btn_add);
            this.panel1.Controls.Add(this.lb_info);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(203, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 318);
            this.panel1.TabIndex = 1;
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(6, 144);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(78, 28);
            this.btn_remove.TabIndex = 2;
            this.btn_remove.Text = "Remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(6, 110);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(78, 28);
            this.btn_add.TabIndex = 1;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lb_info
            // 
            this.lb_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_info.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_info.Location = new System.Drawing.Point(6, 12);
            this.lb_info.Name = "lb_info";
            this.lb_info.Size = new System.Drawing.Size(216, 95);
            this.lb_info.TabIndex = 0;
            // 
            // ConfigExtraTools
            // 
            this.Controls.Add(this.lsb);
            this.Controls.Add(this.panel1);
            this.Name = "ConfigExtraTools";
            this.Size = new System.Drawing.Size(428, 318);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        private ListBox lsb;
        private Panel panel1;
        private Button btn_remove;
        private Button btn_add;
        private Label lb_info;
        List<object> m_values;
        internal void LoadProperty(string name, object value)
        {
            if (m_values == null)
                m_values
                    = new List<object>();
            int i = this.lsb.Items.Add(name);
            m_values.Add(value);
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            using (IGK.DrSStudio.WinUI.DRSXAddExtraTools ctr = new DRSXAddExtraTools())
            {
                if (CoreSystem.Instance.Workbench.ShowDialog(
                     CoreSystem.GetString("Title.AddExtraApplication"),
                     ctr) == enuDialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(ctr.ApplicationExtension)
                        && System.IO.File.Exists (ctr.ApplicationPath ))                        
                    {
                        if (!CoreExternalToolSetting.Instance.Contains(ctr.ApplicationExtension))
                        {
                            this.LoadProperty(ctr.ApplicationExtension, ctr.ApplicationPath);
                            CoreExternalToolSetting.Instance[ctr.ApplicationExtension].Value = ctr.ApplicationPath;
                            CoreExternalTools.ClearExtraCodec();
                            CoreExternalTools.RegisterExtraCodec();
                        }
                    }
                }
            }
        }
        private void btn_remove_Click(object sender, EventArgs e)
        {
            if (this.lsb.SelectedItem != null)
            {
                CoreExternalToolSetting tool=                 CoreExternalToolSetting.Instance;
                CoreExternalTools.ClearExtraCodec();
                int index = this.lsb.SelectedIndex;
                tool[this.lsb.SelectedItem.ToString()].Value  = null;
                this.lsb.Items.Remove(this.lsb.SelectedItem);
                this.m_values.RemoveAt(index);
                CoreExternalTools.RegisterExtraCodec();
            }
        }
        private void lsb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsb.SelectedItem != null)
            {
                this.lb_info.Text = this.m_values[this.lsb.SelectedIndex] as string;
            }
            else {
                this.lb_info.Text = null;
            }
        }
    }
}

