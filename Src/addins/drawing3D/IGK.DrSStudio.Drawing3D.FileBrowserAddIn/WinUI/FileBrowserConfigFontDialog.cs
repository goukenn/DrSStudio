

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FileBrowserConfigFontDialog.cs
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
file:FileBrowserConfigFontDialog.cs
*/

using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing3D.FileBrowserAddIn.WinUI
{
    class FileBrowserConfigFontDialog : Form
    {
        IGKXPanel c_panel;
        public FileBrowserConfigFontDialog()
        {
            this.InitializeComponent();
        }
        public IGKXPanel Panel {
            get {
                return this.c_panel;
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileBrowserConfigFontDialog));
            this.c_panel = new IGKXPanel();
            this.SuspendLayout();
            // 
            // c_panel
            // 
            this.c_panel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_panel.Location = new System.Drawing.Point(0, 0);
            this.c_panel.Margin = new System.Windows.Forms.Padding(0);
            this.c_panel.Name = "c_panel";
            this.c_panel.Size = new System.Drawing.Size(379, 162);
            this.c_panel.TabIndex = 0;
            // 
            // FileBrowserConfigFontDialog
            // 
            this.ClientSize = new System.Drawing.Size(379, 162);
            this.Controls.Add(this.c_panel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FileBrowserConfigFontDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);
        }
    }
}

