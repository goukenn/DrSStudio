

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DRSXAddExtraTools.cs
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
file:AddExtraTools.cs
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    public partial class DRSXAddExtraTools : IGKXUserControl, ICoreControl
    {
        private string m_ApplicationPath;
        private string m_ApplicatinoExtension;
        /// <summary>
        /// Get or set application extension
        /// </summary>
        public string ApplicationExtension
        {
            get { return m_ApplicatinoExtension; }
            set
            {
                if (m_ApplicatinoExtension != value)
                {
                    m_ApplicatinoExtension = value;
                }
            }
        }
        public string ApplicationPath
        {
            get { return m_ApplicationPath; }
            set
            {
                if (m_ApplicationPath != value)
                {
                    m_ApplicationPath = value;
                }
            }
        }
        public DRSXAddExtraTools()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Applications | *.exe;";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.ApplicationPath = ofd.FileName;
                    this.txb_appPath.Text =this.ApplicationPath ;
                }
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.txb_appPath.Text = this.ApplicationPath;
        }
        private void txb_appPath_TextChanged(object sender, EventArgs e)
        {
        }
        private void txb_Ext_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txb_Ext.Text))
            {
                this.ApplicationExtension = txb_Ext.Text;
            }
            else {
                this.ApplicationExtension = null;
            }
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Form frm = this.FindForm();
            if (frm !=null)
            {
                frm.AcceptButton = this.btn_add;
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            this.FindForm().DialogResult = DialogResult.OK;
        }
    }
}

