

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXSkinEditorSelectValue.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.SkinEditorAddin.WinUI
{
    class UIXSkinEditorSelectValue : IGKXUserControl
    {
        private IGKXNumericTextBox xtxb_value;
        private IGKXLabel xlb_value;

        public decimal  Value
        {
            get { return this.xtxb_value.Value; }
            set
            {
                this.xtxb_value.Value = value;
            }
        }
        public UIXSkinEditorSelectValue()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.xtxb_value = new IGKXNumericTextBox();
            this.xlb_value = new IGKXLabel();
            this.SuspendLayout();
            // 
            // xtxb_value
            // 
            this.xtxb_value.AllowDecimalValue = true;
            this.xtxb_value.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtxb_value.Location = new System.Drawing.Point(98, 3);
            this.xtxb_value.MaxLength = 10;
            this.xtxb_value.Name = "xtxb_value";
            this.xtxb_value.Size = new System.Drawing.Size(270, 20);
            this.xtxb_value.TabIndex = 5;
            this.xtxb_value.Value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // xlb_value
            // 
            this.xlb_value.AppContextMenu = null;
            this.xlb_value.CaptionKey = "lb.value.caption";
            this.xlb_value.Location = new System.Drawing.Point(3, 3);
            this.xlb_value.Name = "xlb_value";
            this.xlb_value.Size = new System.Drawing.Size(89, 20);
            this.xlb_value.TabIndex = 4;
            this.xlb_value.TabStop = false;
            
            // 
            // UIXSkinEditorSelectValue
            // 
            this.Controls.Add(this.xtxb_value);
            this.Controls.Add(this.xlb_value);
            this.Name = "UIXSkinEditorSelectValue";
            this.Size = new System.Drawing.Size(371, 29);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public bool AllowDecimalValue { get { return this.xtxb_value.AllowDecimalValue; } set { this.xtxb_value.AllowDecimalValue = value; } }
    }
}
