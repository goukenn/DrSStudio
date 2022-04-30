

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXNewShortcutControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXNewShortcutControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WiXAddIn.WinUI
{
    /// <summary>
    /// represent a shortcut info
    /// </summary>
    class WiXNewShortcutControl : IGKXUserControl
    {
        private IGKXPanel xPanel1;
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private IGKXButton xButton1;
        private IGKXPanel xPanel2;
        public WiXNewShortcutControl()
        {
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.xPanel1 = new IGKXPanel();
            this.xPanel2 = new IGKXPanel();
            this.xButton1 = new IGKXButton();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.xPanel1.SuspendLayout();
            this.xPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xPanel1
            // 
            this.xPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel1.Controls.Add(this.propertyGrid1);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel1.Location = new System.Drawing.Point(0, 0);
            this.xPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Size = new System.Drawing.Size(236, 293);
            this.xPanel1.TabIndex = 0;
            // 
            // xPanel2
            // 
            this.xPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel2.Controls.Add(this.xButton1);
            this.xPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xPanel2.Location = new System.Drawing.Point(0, 293);
            this.xPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel2.Name = "xPanel2";
            this.xPanel2.Size = new System.Drawing.Size(236, 38);
            this.xPanel2.TabIndex = 0;
            // 
            // xButton1
            // 
            this.xButton1.CaptionKey = "btn.ok.caption";
            this.xButton1.Checked = false;
            this.xButton1.DialogResult = enuDialogResult.None;
            this.xButton1.Location = new System.Drawing.Point(156, 10);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(75, 23);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 0;
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid1.Location = new System.Drawing.Point(2, 2);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(232, 289);
            this.propertyGrid1.TabIndex = 0;
            // 
            // WiXNewShortcutControl
            // 
            this.Controls.Add(this.xPanel1);
            this.Controls.Add(this.xPanel2);
            this.Name = "WiXNewShortcutControl";
            this.Size = new System.Drawing.Size(236, 331);
            this.Load += new System.EventHandler(this._Load);
            this.xPanel1.ResumeLayout(false);
            this.xPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        IWixShortcutInfo m_Shortcut;
        public IWixShortcutInfo Shortcut { get { return m_Shortcut; } set { m_Shortcut = value;
        this.propertyGrid1.SelectedObject = value;
        } }
       internal  class WixShortcutInfo : IWixShortcutInfo
        {
           public  string Name { get; set; }
           public string WorkingDir { get; set; }
           public string Target { get; set; }
           public string Description { get; set; }
        }
        private void _Load(object sender, EventArgs e)
        {
            if (this.Shortcut == null)
            {
                WixShortcutInfo info = new WixShortcutInfo();
                info.Name = "shortcut";
                info.Target = "[APPDIR]";
                info.WorkingDir = "APPDIR";                
                this.Shortcut = info;
            }
            this.xButton1.DialogResult = enuDialogResult.OK;
            System.Windows.Forms.Form frm = this.FindForm();
            if (frm !=null)
                frm.AcceptButton = this.xButton1;
        }
    }
}

