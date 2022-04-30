/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace IGK.ResMan
{
    public partial class AddText : UserControl
    {
        public string ResTitle {
            get {
                return this.txb_Title.Text;
            }
            set
            {
                this.txb_Title.Text = value;
            }
        }
        public string ResValue {
            get {
                return this.txb_Value.Text;
            }
            set {
                this.txb_Value.Text = value;
            }
        }
        public AddText()
        {
            InitializeComponent();
        }

        private void bt_add_Click(object sender, EventArgs e)
        {

        }
        internal void SetClearFocus()
        {
            this.txb_Title.Text = string.Empty;
            this.txb_Value.Text = string.Empty;
            this.txb_Title.Focus();
        }

        private void AddText_Load(object sender, EventArgs e)
        {
            
        }
    }
}
