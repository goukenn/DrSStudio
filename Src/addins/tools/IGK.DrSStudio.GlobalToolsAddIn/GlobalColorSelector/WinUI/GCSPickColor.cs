

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GCSPickColor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    class GCSPickColor : IGKXUserControl
    {
        private IGKXButton igkxButton1;
    
        public GCSPickColor()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.igkxButton1 = new IGKXButton();
            this.SuspendLayout();
            // 
            // igkxButton1
            // 
            this.igkxButton1.AppContextMenu = null;
            this.igkxButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.igkxButton1.CaptionKey = null;
            this.igkxButton1.Checked = false;
            this.igkxButton1.DialogResult = enuDialogResult.None;
            this.igkxButton1.Location = new System.Drawing.Point(118, 68);
            this.igkxButton1.Name = "igkxButton1";
            this.igkxButton1.ShowButtonImage = false;
            this.igkxButton1.Size = new System.Drawing.Size(231, 177);
            this.igkxButton1.State = enuButtonState.Normal;
            this.igkxButton1.TabIndex = 0;
            // 
            // GCSPickColor
            // 
            this.Controls.Add(this.igkxButton1);
            this.Name = "GCSPickColor";
            this.Size = new System.Drawing.Size(429, 338);
            this.Load += new System.EventHandler(this.GCSPickColor_Load);
            this.ResumeLayout(false);

        }

        private void GCSPickColor_Load(object sender, EventArgs e)
        {

        }
    }
}
