

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKGVTXSelectionGUI.cs
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
using System.Windows.Forms;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.Controls;
using IGK.ICore.WinCore.WinUI;


namespace IGK.DrSStudio.WinUI
{
    class IGKGVTXSelectionGUI : IGKXToolHostedControl 
    {
        private IGKXExpenderBox c_expenderBox;

        public IGKXExpenderBox HostedControl
        {
            get {
                return c_expenderBox;
            }
        }
        public IGKGVTXSelectionGUI()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.c_expenderBox = new IGKXExpenderBox();
            this.SuspendLayout();
            // 
            // c_expenderBox
            // 
            this.c_expenderBox.Animate = false;
            this.c_expenderBox.CaptionKey = null;
            this.c_expenderBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_expenderBox.Location = new System.Drawing.Point(0, 0);
            this.c_expenderBox.Margin = new System.Windows.Forms.Padding(0);
            this.c_expenderBox.MouseState = enuMouseState.None;
            this.c_expenderBox.Name = "c_expenderBox";
            this.c_expenderBox.SelectedGroup = null;
            this.c_expenderBox.Size = new System.Drawing.Size(279, 267);
            this.c_expenderBox.TabIndex = 0;
            // 
            // IGKGVTXSelectionSurface
            // 
            this.Controls.Add(this.c_expenderBox);
            this.Name = "IGKGVTXSelectionSurface";
            this.Size = new System.Drawing.Size(279, 267);
            this.ResumeLayout(false);

        }
    }
}
