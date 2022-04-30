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
    public partial class AddImage : UserControl
    {
        public string Title {
            get {
                return this.txb_Title.Text ;
            }
            set {
                this.txb_Title.Text = value ;
            }
        }

        public string FileName {
            get {
                return this.txb_Value.Text;
            }
        }
        public AddImage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Pictures Files |" +
                    "*.jpg; *.bmp;| all files | *.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.txb_Value.Text = ofd.FileName;
                }
            }
        }
    }
}
