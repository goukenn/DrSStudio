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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WinSnippetBuilder
{
    public partial class UIXNameSelection : UserControl
    {
        public String ReturnName {
            get {
                return this.c_txbName.Text;
            }
            set {
                this.c_txbName.Text = value;
            }

        }
        public UIXNameSelection()
        {
            InitializeComponent();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.Parent is Form)
            {
                this.ParentForm.AcceptButton = c_btnOk;
            }
        }
    }
}
