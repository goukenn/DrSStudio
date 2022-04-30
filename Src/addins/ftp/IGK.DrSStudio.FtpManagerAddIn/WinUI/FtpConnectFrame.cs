

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpConnectFrame.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;using IGK.DrSStudio.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:FtpConnectFrame.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    /// <summary>
    /// represent a connection frame
    /// </summary>
    class FtpConnectFrame : IGKXUserControl
    {
        private IGKXTextBox c_txb_server;
        private IGKXTextBox c_txb_login;
        private IGKXLabel xLabel2;
        private IGKXTextBox c_txb_password;
        private IGKXLabel xLabel3;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXCheckBox xCheckBox1;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXButton xButton1;
        private IGKXTextBox c_txb_port;
        private IGKXLabel xLabel4;
        private IGKXLabel xLabel1;
        private int m_Port;
        public int Port
        {
            get { return m_Port; }
            set
            {
                if (m_Port != value)
                {
                    m_Port = value;
                    OnPortChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler PortChanged;
        ///<summary>
        ///raise the PortChanged 
        ///</summary>
        protected virtual void OnPortChanged(EventArgs e)
        {
            if (PortChanged != null)
                PortChanged(this, e);
        }
        public string PassWord
        {
            get { return this.c_txb_password.Text ; }
            set
            {
                this.c_txb_password.Text = value;
            }
        }
        public string Login
        {
            get { return this.c_txb_login .Text ; }
            set
            {
                this.c_txb_login.Text = value;
            }
        }
        public string Server
        {
            get { return this.c_txb_server .Text ; }
            set
            {
                this.c_txb_server.Text = value;
            }
        }
        public bool SaveConnectionEntry
        {
            get { return xCheckBox1.Checked; }
            set { this.xCheckBox1.Checked = value; }
        }
        public FtpConnectFrame()
        {
            this.Port = 21;
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.xLabel1 = new IGKXLabel();
            this.c_txb_server = new IGKXTextBox();
            this.c_txb_login = new IGKXTextBox();
            this.xLabel2 = new IGKXLabel();
            this.c_txb_password = new IGKXTextBox();
            this.xLabel3 = new IGKXLabel();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xCheckBox1 = new IGKXCheckBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.xButton1 = new IGKXButton();
            this.c_txb_port = new IGKXTextBox();
            this.xLabel4 = new IGKXLabel();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.server.Caption";
            this.xLabel1.Location = new System.Drawing.Point(16, 19);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(131, 21);
            this.xLabel1.TabIndex = 0;
            // 
            // c_txb_server
            // 
            this.c_txb_server.Location = new System.Drawing.Point(194, 19);
            this.c_txb_server.Name = "c_txb_server";
            this.c_txb_server.Size = new System.Drawing.Size(152, 20);
            this.c_txb_server.TabIndex = 1;
            this.c_txb_server.Text = "www.igkdev.be";
            // 
            // c_txb_login
            // 
            this.c_txb_login.Location = new System.Drawing.Point(194, 56);
            this.c_txb_login.Name = "c_txb_login";
            this.c_txb_login.Size = new System.Drawing.Size(152, 20);
            this.c_txb_login.TabIndex = 3;
            this.c_txb_login.Text = "igkdevbe";
            // 
            // xLabel2
            // 
            this.xLabel2.CaptionKey = "lb.login.Caption";
            this.xLabel2.Location = new System.Drawing.Point(16, 56);
            this.xLabel2.Name = "xLabel2";
            this.xLabel2.Size = new System.Drawing.Size(131, 21);
            this.xLabel2.TabIndex = 2;
            // 
            // c_txb_password
            // 
            this.c_txb_password.Location = new System.Drawing.Point(194, 93);
            this.c_txb_password.Name = "c_txb_password";
            this.c_txb_password.Size = new System.Drawing.Size(152, 20);
            this.c_txb_password.TabIndex = 5;
            this.c_txb_password.Text = "bonajehost1983";
            this.c_txb_password.UseSystemPasswordChar = true;
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.password.Caption";
            this.xLabel3.Location = new System.Drawing.Point(16, 93);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(131, 21);
            this.xLabel3.TabIndex = 4;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 164);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(393, 20);
            this.xRuleLabel1.TabIndex = 6;
            // 
            // xCheckBox1
            // 
            this.xCheckBox1.CaptionKey = "lb.FtpSaveconnectionInfo.caption";
            this.xCheckBox1.Location = new System.Drawing.Point(16, 187);
            this.xCheckBox1.Name = "xCheckBox1";
            this.xCheckBox1.Size = new System.Drawing.Size(370, 24);
            this.xCheckBox1.TabIndex = 7;
            this.xCheckBox1.Checked = true;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.CaptionKey = null;
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 225);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(393, 20);
            this.xRuleLabel2.TabIndex = 8;
            // 
            // xButton1
            // 
            this.xButton1.ButtonImageSize = new Size2i(32, 32);
            this.xButton1.CaptionKey = null;
            this.xButton1.Checked = false;
            this.xButton1.Location = new System.Drawing.Point(236, 257);
            this.xButton1.Name = "xButton1";
            this.xButton1.ShowButtonImage = false;
            this.xButton1.Size = new System.Drawing.Size(150, 32);
            this.xButton1.State = enuButtonState.Normal;
            this.xButton1.TabIndex = 9;
            this.xButton1.Click += new System.EventHandler(this.xButton1_Click);
            // 
            // c_txb_port
            // 
            this.c_txb_port.Location = new System.Drawing.Point(194, 130);
            this.c_txb_port.Name = "c_txb_port";
            this.c_txb_port.Size = new System.Drawing.Size(152, 20);
            this.c_txb_port.TabIndex = 11;
            this.c_txb_port.Text = "21";
            this.c_txb_port.TextChanged += new System.EventHandler(this.c_txb_port_TextChanged);
            // 
            // xLabel4
            // 
            this.xLabel4.CaptionKey = "lb.port.Caption";
            this.xLabel4.Location = new System.Drawing.Point(16, 130);
            this.xLabel4.Name = "xLabel4";
            this.xLabel4.Size = new System.Drawing.Size(131, 21);
            this.xLabel4.TabIndex = 10;
            // 
            // FtpConnectFrame
            // 
            this.Controls.Add(this.c_txb_port);
            this.Controls.Add(this.xLabel4);
            this.Controls.Add(this.xButton1);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.xCheckBox1);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.c_txb_password);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.c_txb_login);
            this.Controls.Add(this.xLabel2);
            this.Controls.Add(this.c_txb_server);
            this.Controls.Add(this.xLabel1);
            this.Name = "FtpConnectFrame";
            this.Size = new System.Drawing.Size(399, 311);
            this.Load += new System.EventHandler(this.FtpConnectFrame_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xButton1_Click(object sender, EventArgs e)
        {
        }
        private void FtpConnectFrame_Load(object sender, EventArgs e)
        {
        }
        private void c_txb_port_TextChanged(object sender, EventArgs e)
        {
            int f = 21;
            if (int.TryParse(this.c_txb_port.Text, out f))
            {
                this.Port = f;
            }
        }
    }
}

