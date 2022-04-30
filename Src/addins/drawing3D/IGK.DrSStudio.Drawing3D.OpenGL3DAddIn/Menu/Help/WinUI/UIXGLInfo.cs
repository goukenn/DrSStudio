

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXGLInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
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
file:UIXGLInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing3D.OpenGL.WinUI
{
    public class UIXGLInfo : UIXConfigControlBase 
    {
        private IGKXRuleLabel xRuleLabel1;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXRuleLabel xRuleLabel3;
        private IGKXListBox c_lsb_ext;
        private IGKXListBox c_lsb_func;
        private IGKXLabel c_lb_geninfo;


        public UIXGLInfo()
        {
            this.InitializeComponent();
            this.AutoScroll = true;
           this.Dock = System.Windows.Forms.DockStyle.Fill;
        }
        private void InitializeComponent()
        {
            this.c_lb_geninfo =new IGKXLabel();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.c_lsb_ext =new IGKXListBox();
            this.c_lsb_func = new IGKXListBox();
            this.SuspendLayout();
            // 
            // c_lb_geninfo
            // 
            this.c_lb_geninfo.CaptionKey = "lb.oglgeneralinfo.caption";
            this.c_lb_geninfo.Location = new System.Drawing.Point(6, 48);
            this.c_lb_geninfo.Name = "c_lb_geninfo";
            this.c_lb_geninfo.Size = new System.Drawing.Size(269, 87);
            this.c_lb_geninfo.TabIndex = 0;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel1.CaptionKey = "lb.oglgeneralinfo.caption";
            this.xRuleLabel1.Location = new System.Drawing.Point(6, 25);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(269, 32);
            this.xRuleLabel1.TabIndex = 1;
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = "lb.oglextensions.caption";
            this.xRuleLabel2.Location = new System.Drawing.Point(6, 164);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(269, 32);
            this.xRuleLabel2.TabIndex = 2;
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel3.CaptionKey = "lb.oglfunctions.caption";
            this.xRuleLabel3.Location = new System.Drawing.Point(3, 274);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(272, 32);
            this.xRuleLabel3.TabIndex = 3;
            // 
            // c_lsb_ext
            // 
            this.c_lsb_ext.FormattingEnabled = true;
            this.c_lsb_ext.Location = new System.Drawing.Point(6, 195);
            this.c_lsb_ext.Name = "c_lsb_ext";
            this.c_lsb_ext.Size = new System.Drawing.Size(269, 69);
            this.c_lsb_ext.TabIndex = 4;
            // 
            // c_lsb_func
            // 
            this.c_lsb_func.FormattingEnabled = true;
            this.c_lsb_func.Location = new System.Drawing.Point(6, 295);
            this.c_lsb_func.Name = "c_lsb_func";
            this.c_lsb_func.Size = new System.Drawing.Size(269, 95);
            this.c_lsb_func.TabIndex = 5;
            this.c_lsb_func.SelectedIndexChanged += new System.EventHandler(this.xListBox2_SelectedIndexChanged);
            // 
            // UIXGLInfo
            // 
            this.Controls.Add(this.c_lsb_func);
            this.Controls.Add(this.c_lsb_ext);
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.c_lb_geninfo);
            this.Controls.Add(this.xRuleLabel1);
            this.Name = "UIXGLInfo";
            this.Size = new System.Drawing.Size(285, 404);
            this.Load += new System.EventHandler(this.UIXGLInfo_Load);
            this.ResumeLayout(false);
        }
        private void xListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void UIXGLInfo_Load(object sender, EventArgs e)
        {
            //init function 
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine(
                string.Format("\nVersion : {0}\n\nVendor:{1}\n\nRenderer : {2}",
                IGK.GLLib.GL.Version,
                IGK.GLLib.GL.Vendor,
                IGK.GLLib.GL.Renderer
                )
            );
            sb.AppendLine(string.Format("SUPPORT Version: {0}", IGK.GLLib.GL.SupportExtension("GL_VERSION_3_0")));
            sb.AppendLine(string.Format ("Shading Version: {0}", IGK.GLLib.GL.glGetString(GLLib.GL.GL_SHADING_LANGUAGE_VERSION)));

            this.c_lb_geninfo.Text = sb.ToString();
            this.c_lsb_ext.Items.AddRange(GLLib.GL.Extensions);
            this.c_lsb_ext.Sorted = true;            
            System.Collections.IEnumerator v_se = GLLib.GL.AdditionalMethods;
            while (v_se.MoveNext ())            
            {
                this.c_lsb_func.Items.Add(v_se.Current);    
            }
            this.c_lsb_func.Sorted = true;
        }
    }
}

