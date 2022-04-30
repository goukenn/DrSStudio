

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXSuperCopy.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:UIXSuperCopy.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
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
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging ;
using System.Windows.Forms;

using IGK.DrSStudio.Drawing2D;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    class UIXSuperCopy : UIXConfigControlBase 
    {
        private enum enumCopyMode { 
            translate,
            rotate
        }
        private ICore2DDrawingSurface m_surface;
        private ICore2DDrawingLayeredElement m_elementToCopy;
        private enumCopyMode m_copyMode;
        private int m_Quantity;
        private Vector2f  m_distance;
        private IGKXLabel lb_dx;
        private IGKXTextBox txb_dx;
        private IGKXLabel xLabel3;
        private IGKXTextBox txb_dy;
        private IGKXRuleLabel xRuleLabel3;
        private float m_angle;
        /// <summary>
        /// get or set the element to copy
        /// </summary>
        public ICore2DDrawingLayeredElement ElementToCopy {
            get {
                return this.m_elementToCopy;
            }
            set {
                if (this.m_elementToCopy != value)
                {
                    this.m_elementToCopy = value;
                    OnElementToCopyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set the surface 
        /// </summary>
        public ICore2DDrawingSurface Surface {
            get {
                return m_surface;
            }
            set
            {
                m_surface = value;
            }
        }
        private void OnElementToCopyChanged(EventArgs eventArgs)
        {
            this.Enabled = (this.m_elementToCopy != null);
            if (this.Enabled)
            {
                this.txb_id .Text = this.m_elementToCopy.Id;
            }
        }
        private IGKXLabel xLabel1;
        private IGKXTextBox txb_id;
        private IGKXRuleLabel xRuleLabel1;
        private IGKXTextBox txb_qte;
        private IGKXLabel lb_nbrcopy;
        private IGKXGroupBox xGroupBox1;
        private IGKXRadioButton rd_angle;
        private IGKXLabel lb_angle;
        private IGKXTextBox txb_angle;
        private IGKXRuleLabel xRuleLabel2;
        private IGKXButton btn_ok;
        private IGKXButton btn_cancel;
        private IGKXRadioButton rd_translate;
        public UIXSuperCopy()
        {
            this.InitializeComponent();
            this.rd_angle.Tag = enumCopyMode.rotate;
            this.rd_translate.Tag = enumCopyMode.translate;
            m_angle = 1.0f;
            this.m_copyMode = enumCopyMode.rotate;
            m_Quantity = 1;
            m_distance = new Vector2f(1, 1);
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.rd_angle.Text = "angle".R();
            this.rd_translate.Text = "translate".R();
        }
        private void InitializeComponent()
        {
            this.xLabel1 = new IGKXLabel();
            this.txb_id = new IGKXTextBox();
            this.xRuleLabel1 = new IGKXRuleLabel();
            this.txb_qte = new IGKXTextBox();
            this.lb_nbrcopy = new IGKXLabel();
            this.xGroupBox1 = new IGKXGroupBox();
            this.rd_angle = new IGKXRadioButton();
            this.rd_translate = new IGKXRadioButton();
            this.lb_angle = new IGKXLabel();
            this.txb_angle = new IGKXTextBox();
            this.xRuleLabel2 = new IGKXRuleLabel();
            this.btn_ok = new IGKXButton();
            this.btn_cancel = new IGKXButton();
            this.lb_dx = new IGKXLabel();
            this.txb_dx = new IGKXTextBox();
            this.xLabel3 = new IGKXLabel();
            this.txb_dy = new IGKXTextBox();
            this.xRuleLabel3 = new IGKXRuleLabel();
            this.xGroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xLabel1
            // 
            this.xLabel1.CaptionKey = "lb.id.caption";
            this.xLabel1.Location = new System.Drawing.Point(3, 12);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(63, 16);
            this.xLabel1.TabIndex = 0;
            // 
            // txb_id
            // 
            this.txb_id.Location = new System.Drawing.Point(72, 5);
            this.txb_id.Name = "txb_id";
            this.txb_id.ReadOnly = true;
            this.txb_id.Size = new System.Drawing.Size(194, 20);
            this.txb_id.TabIndex = 1;
            // 
            // xRuleLabel1
            // 
            this.xRuleLabel1.Alignment = System.Drawing.StringAlignment.Near;            
            this.xRuleLabel1.CaptionKey = null;
            this.xRuleLabel1.Location = new System.Drawing.Point(3, 28);
            this.xRuleLabel1.Name = "xRuleLabel1";
            this.xRuleLabel1.Size = new System.Drawing.Size(263, 16);
            this.xRuleLabel1.TabIndex = 2;
            this.xRuleLabel1.TabStop = false;
            // 
            // txb_qte
            // 
            this.txb_qte.Location = new System.Drawing.Point(72, 50);
            this.txb_qte.Name = "txb_qte";
            this.txb_qte.Size = new System.Drawing.Size(90, 20);
            this.txb_qte.TabIndex = 3;
            this.txb_qte.Text = "1";
            this.txb_qte.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_qte.TextChanged += new System.EventHandler(this.txb_qte_TextChanged);
            // 
            // lb_nbrcopy
            // 
            this.lb_nbrcopy.CaptionKey = "lb.nbrcopy.caption";
            this.lb_nbrcopy.Location = new System.Drawing.Point(3, 57);
            this.lb_nbrcopy.Name = "lb_nbrcopy";
            this.lb_nbrcopy.Size = new System.Drawing.Size(63, 13);
            this.lb_nbrcopy.TabIndex = 4;
            // 
            // xGroupBox1
            // 
            this.xGroupBox1.Controls.Add(this.rd_angle);
            this.xGroupBox1.Controls.Add(this.rd_translate);
            this.xGroupBox1.Location = new System.Drawing.Point(3, 89);
            this.xGroupBox1.Name = "xGroupBox1";
            this.xGroupBox1.Size = new System.Drawing.Size(263, 51);
            this.xGroupBox1.TabIndex = 5;
            this.xGroupBox1.TabStop = false;
            this.xGroupBox1.Text = "grb.mode";
            // 
            // rd_angle
            // 
            this.rd_angle.AutoSize = true;
            this.rd_angle.Checked = true;
            this.rd_angle.Location = new System.Drawing.Point(143, 28);
            this.rd_angle.Name = "rd_angle";
            this.rd_angle.Size = new System.Drawing.Size(65, 17);
            this.rd_angle.TabIndex = 1;
            this.rd_angle.TabStop = true;
            this.rd_angle.CheckedChanged += new System.EventHandler(this.rd_angle_CheckedChanged);
            // 
            // rd_translate
            // 
            this.rd_translate.AutoSize = true;
            this.rd_translate.Location = new System.Drawing.Point(16, 28);
            this.rd_translate.Name = "rd_translate";
            this.rd_translate.Size = new System.Drawing.Size(77, 17);
            this.rd_translate.TabIndex = 0;
            this.rd_translate.Text = "Translation";
            this.rd_translate.CheckedChanged += new System.EventHandler(this.rd_angle_CheckedChanged);
            // 
            // lb_angle
            // 
            this.lb_angle.CaptionKey = "lb.angle.caption";
            this.lb_angle.Location = new System.Drawing.Point(3, 171);
            this.lb_angle.Name = "lb_angle";
            this.lb_angle.Size = new System.Drawing.Size(63, 13);
            this.lb_angle.TabIndex = 7;
            // 
            // txb_angle
            // 
            this.txb_angle.Location = new System.Drawing.Point(72, 164);
            this.txb_angle.Name = "txb_angle";
            this.txb_angle.Size = new System.Drawing.Size(90, 20);
            this.txb_angle.TabIndex = 6;
            this.txb_angle.Text = "1";
            this.txb_angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_angle.TextChanged += new System.EventHandler(this.xTextBox3_TextChanged);
            // 
            // xRuleLabel2
            // 
            this.xRuleLabel2.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.xRuleLabel2.CaptionKey = null;
            this.xRuleLabel2.Location = new System.Drawing.Point(3, 278);
            this.xRuleLabel2.Name = "xRuleLabel2";
            this.xRuleLabel2.Size = new System.Drawing.Size(264, 10);
            this.xRuleLabel2.TabIndex = 8;
            this.xRuleLabel2.TabStop = false;
            // 
            // btn_ok
            // 
            this.btn_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ok.CaptionKey = CoreConstant.BTN_OK;
            this.btn_ok.DialogResult = enuDialogResult.OK;
            this.btn_ok.Location = new System.Drawing.Point(127, 294);
            this.btn_ok.Name = CoreConstant.BTN_OK;
            this.btn_ok.Size = new System.Drawing.Size(67, 32);
            this.btn_ok.TabIndex = 9;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));            
            this.btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            this.btn_cancel.DialogResult = enuDialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(200, 294);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(67, 32);
            this.btn_cancel.TabIndex = 10;
            // 
            // lb_dx
            // 
            this.lb_dx.CaptionKey = "lb.X.caption";
            this.lb_dx.Location = new System.Drawing.Point(3, 220);
            this.lb_dx.Name = "lb_dx";
            this.lb_dx.Size = new System.Drawing.Size(63, 13);
            this.lb_dx.TabIndex = 12;
            // 
            // txb_dx
            // 
            this.txb_dx.Enabled = false;
            this.txb_dx.Location = new System.Drawing.Point(72, 213);
            this.txb_dx.Name = "txb_dx";
            this.txb_dx.Size = new System.Drawing.Size(90, 20);
            this.txb_dx.TabIndex = 11;
            this.txb_dx.Text = "1";
            this.txb_dx.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_dx.TextChanged += new System.EventHandler(this.txb_dy_TextChanged);
            // 
            // xLabel3
            // 
            this.xLabel3.CaptionKey = "lb.Y.caption";
            this.xLabel3.Location = new System.Drawing.Point(3, 246);
            this.xLabel3.Name = "xLabel3";
            this.xLabel3.Size = new System.Drawing.Size(63, 13);
            this.xLabel3.TabIndex = 14;
            // 
            // txb_dy
            // 
            this.txb_dy.Enabled = false;
            this.txb_dy.Location = new System.Drawing.Point(72, 239);
            this.txb_dy.Name = "txb_dy";
            this.txb_dy.Size = new System.Drawing.Size(90, 20);
            this.txb_dy.TabIndex = 13;
            this.txb_dy.Text = "1";
            this.txb_dy.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txb_dy.TextChanged += new System.EventHandler(this.txb_dy_TextChanged);
            // 
            // xRuleLabel3
            // 
            this.xRuleLabel3.Alignment = System.Drawing.StringAlignment.Near;
            this.xRuleLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));            
            this.xRuleLabel3.CaptionKey = null;
            this.xRuleLabel3.Location = new System.Drawing.Point(3, 190);
            this.xRuleLabel3.Name = "xRuleLabel3";
            this.xRuleLabel3.Size = new System.Drawing.Size(264, 10);
            this.xRuleLabel3.TabIndex = 15;
            this.xRuleLabel3.TabStop = false;
            // 
            // UIXSuperCopy
            // 
            this.Controls.Add(this.xRuleLabel3);
            this.Controls.Add(this.xLabel3);
            this.Controls.Add(this.txb_dy);
            this.Controls.Add(this.lb_dx);
            this.Controls.Add(this.txb_dx);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.xRuleLabel2);
            this.Controls.Add(this.lb_angle);
            this.Controls.Add(this.txb_angle);
            this.Controls.Add(this.xGroupBox1);
            this.Controls.Add(this.lb_nbrcopy);
            this.Controls.Add(this.txb_qte);
            this.Controls.Add(this.xRuleLabel1);
            this.Controls.Add(this.txb_id);
            this.Controls.Add(this.xLabel1);
            this.Name = "UIXSuperCopy";
            this.Size = new System.Drawing.Size(270, 330);
            this.xGroupBox1.ResumeLayout(false);
            this.xGroupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            Form frm = this.Parent as Form;
            if (frm != null)
            {
                frm.AcceptButton = this.btn_ok;
                frm.CancelButton = this.btn_cancel;
            }
        }
        private void rd_angle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rd = sender as RadioButton;
            if (rd.Checked)
            {
                this.m_copyMode  = (enumCopyMode)rd.Tag;
                switch (m_copyMode)
                {
                    case enumCopyMode.translate:
                        txb_angle.Enabled = false;
                        txb_dx.Enabled = true;
                        txb_dy.Enabled = true;
                        break;
                    case enumCopyMode.rotate :
                        txb_angle.Enabled = true ;
                        txb_dx.Enabled = false ;
                        txb_dy.Enabled = false ;
                        break;
                }
            }
        }
        private void btn_ok_Click(object sender, EventArgs e)
        {
            this.CopyAndPaste();
        }
        /// <summary>
        /// apply copy and paste
        /// </summary>
        private void CopyAndPaste()
        {
            if (this.m_elementToCopy == null)
                return;
            Rectanglef rc = this.m_surface .CurrentDocument .Bounds ;
            Vector2f center = CoreMathOperation.GetCenter (rc);
            ICore2DDrawingLayeredElement[] v_obj = new ICore2DDrawingLayeredElement[this.m_Quantity ];
            ICore2DDrawingLayeredElement v_cp = null;
            float v_angle = m_angle;
            switch (m_copyMode )
            {
                case enumCopyMode.translate:
                    Vector2f pts = m_distance;
                    for (int i = 0; i < this.m_Quantity; ++i)
                    {
                        v_cp = this.m_elementToCopy.Clone() as ICore2DDrawingLayeredElement;
                        v_cp.Translate (pts.X, pts.Y, enuMatrixOrder.Append);
                        v_obj[i] = v_cp;
                        pts = new Vector2f(pts.X + m_distance.X , pts.Y + m_distance.Y);
                    }
                    break;
                case enumCopyMode.rotate:
                    for (int i = 0; i < this.m_Quantity ;++i)
                    {
                        v_cp = this.m_elementToCopy.Clone() as ICore2DDrawingLayeredElement ;
                        v_cp.Rotate(v_angle , center, enuMatrixOrder.Append);
                        v_obj[i] = v_cp;
                        v_angle += m_angle;
                    }
                    break;
            }
            if (v_obj.Length > 0)
                this.m_surface.CurrentLayer.Elements.AddRange (v_obj);
        }
        private void xTextBox3_TextChanged(object sender, EventArgs e)
        {
            float t = 0;
            if (float.TryParse(txb_angle.Text, out t))
            {
                if ((t > 0)&&(t<180.0f))
                {
                    m_angle = t;
                }
            }
        }
        private void txb_qte_TextChanged(object sender, EventArgs e)
        {
            int i = 0;
            if (int.TryParse(txb_qte .Text , out i))
            {
                m_Quantity = i;
            }
        }
        private void txb_dy_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            if (int.TryParse(this.txb_dx .Text , out x)&&
                int.TryParse(this.txb_dy.Text , out y)
                )
            {
                m_distance = new Vector2f(x, y);
            }
        }
    }
}

