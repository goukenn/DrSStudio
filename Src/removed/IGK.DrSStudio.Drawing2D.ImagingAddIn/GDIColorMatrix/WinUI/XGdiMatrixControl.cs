

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XGdiMatrixControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XGdiMatrixControl.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows .Forms ;
using System.Drawing ;
using System.Drawing.Imaging ;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    sealed class XGdiMatrixControl
: UIXConfigControlBase
    {
        const int NUMBER = 5;
        float[] m_values;
        private ImageElement m_ImageElement;
        private IGKXComboBox cmb_MatFlag;
        private Bitmap m_oldBitmap;
        protected override void Dispose(bool disposing)
        {
            if (m_oldBitmap != null)
            {
                m_oldBitmap.Dispose();
            }
            base.Dispose(disposing);
        }
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                    if (value != null)
                    {
                        this.m_oldBitmap = value.Bitmap.Clone() as Bitmap;
                    }
                }
            }
        }
        public XGdiMatrixControl()
        {
            this.InitializeComponent();
            InitControl();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XGdiMatrixControl
            // 
            this.Name = "XGdiMatrixControl";
            this.Size = new System.Drawing.Size(318, 394);
            this.ResumeLayout(false);
        }
        private void InitControl()
        {
            m_values = new float[25];
            //init the filrs diagonal
            m_values[12] = 1;
            for (int i = 0, j=0; i < 5; i++, j++)
			{
			    m_values[i*5+ j] = 1; 
			}
            this.SuspendLayout();
            IGKXNumericTextBox txb = null;
            int offsetX = 10;
            int offsetY = 10;
            for (int i = 0; i < 5; i++)
            {
                offsetX = 10;
                for (int j = 0; j < 5; j++)
                {
                    offsetX += 30;
                    txb = new IGKXNumericTextBox();
                    txb.Text = m_values[i * 5 + j].ToString();
                    txb.Width = 25;
                    txb.Tag = (i * 5) + j;
                    txb.AllowDecimalValue = true;
                    txb.AllowNegativeValue = true;
                    txb.Location = new System.Drawing.Point(offsetX, offsetY);
                    txb.ValueChanged += new EventHandler(txb_ValueChanged);
                    this.Controls.Add(txb);
                }
                offsetY += 32;
            }
            offsetX = 10;
            offsetY += 32;
            IGKXRuleLabel rlab = new IGKXRuleLabel();
            rlab.Width = this.Width;
            rlab.Location = new Point(offsetX, offsetY);
            rlab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(rlab);
            offsetY += 32;
            IGKXLabel lb = new IGKXLabel();
            lb.CaptionKey = "lb.ColorMatrixFlag";
            lb.Location  = new Point (offsetX, offsetY);
            lb.Width = 110;
            offsetX += 126;
            IGKXComboBox cmb = new IGKXComboBox()
            {
                Width = 150,
                Anchor = AnchorStyles.Left |  AnchorStyles.Top,
                Location = new Point(offsetX, offsetY),
                DropDownStyle = ComboBoxStyle.DropDownList 
            };
            cmb.DataSource = Enum.GetValues(typeof(System.Drawing.Imaging.ColorMatrixFlag));
            cmb.SelectedItem = System.Drawing.Imaging.ColorMatrixFlag.Default;
            cmb.SelectedValueChanged += new EventHandler(cmb_SelectedValueChanged);
            this.Controls.Add(lb);
            this.Controls.Add(cmb);
            this.cmb_MatFlag = cmb;
            offsetY += 32;
            offsetX = 10;
            rlab = new IGKXRuleLabel();
            rlab.Width = this.Width;
            rlab.Location = new Point(offsetX, offsetY);
            rlab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(rlab);
            IGKXButton btn_ok = new IGKXButton() { DialogResult = DialogResult.OK };
            IGKXButton btn_cancel = new IGKXButton() { DialogResult = DialogResult.Cancel };
            offsetY += 40;
            btn_ok.Location = new Point(offsetX, offsetY);
            btn_ok.Size = new Size(64, 32);
            btn_ok.CaptionKey = CoreConstant.BTN_OK;
            offsetX += 64 + 8;
            btn_cancel.Location = new Point(offsetX, offsetY);
            btn_cancel.Size = new Size(64, 32);
            btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            offsetY += 38;
            this.Controls.Add(btn_ok);
            this.Controls.Add(btn_cancel);
            this.AcceptButton = btn_ok;
            this.CancelButton = btn_cancel;
            this.ResumeLayout();
        }
        void cmb_SelectedValueChanged(object sender, EventArgs e)
        {
            this.ApplyMatrix(true);
        }
        void txb_ValueChanged(object sender, EventArgs e)
        {
            IGKXNumericTextBox txb = sender as IGKXNumericTextBox;
            int index = (int)txb.Tag;
            this.m_values[index] = (float)txb.Value;
            ApplyMatrix(true);
        }
        internal void ApplyMatrix(bool temp)
        {
            try
            {
             ColorMatrix matrix = new ColorMatrix ();
             matrix.Matrix00 = m_values[0];
             matrix.Matrix01 = m_values[1];
             matrix.Matrix02 = m_values[2];
             matrix.Matrix03 = m_values[3];
             matrix.Matrix04 = m_values[4];
             matrix.Matrix10 = m_values[5];
             matrix.Matrix11 = m_values[6];
             matrix.Matrix12 = m_values[7];
             matrix.Matrix13 = m_values[8];
             matrix.Matrix14 = m_values[9];
             matrix.Matrix20 = m_values[10];
             matrix.Matrix21 = m_values[11];
             matrix.Matrix22 = m_values[12];
             matrix.Matrix23 = m_values[13];
             matrix.Matrix24 = m_values[14];
             matrix.Matrix30 = m_values[15];
             matrix.Matrix31 = m_values[16];
             matrix.Matrix32 = m_values[17];
             matrix.Matrix33 = m_values[18];
             matrix.Matrix34 = m_values[19];
             matrix.Matrix40 = m_values[20];
             matrix.Matrix41 = m_values[21];
             matrix.Matrix42 = m_values[22];
             matrix.Matrix43 = m_values[23];
             matrix.Matrix44 = m_values[24];
             this.ImageElement.SetBitmap(
                    CoreBitmapOperation .ApplyColorMatrix (
                    this.m_oldBitmap , 
                    matrix,
                    this.ColorMatrixFlag ,
                     ColorAdjustType.Bitmap ), temp
                    );
             this.ImageElement.Invalidate(true);
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug("Error When Applying custom convolution : " + ex.Message);
            }
        }
        public ColorMatrixFlag ColorMatrixFlag { get {
            if (this.cmb_MatFlag.SelectedValue !=null)
            return (ColorMatrixFlag)this.cmb_MatFlag.SelectedValue;
            return ColorMatrixFlag.Default;
        } }
        internal void Restore()
        {
            this.ImageElement.SetBitmap(this.m_oldBitmap.Clone() as Bitmap, true);
        }
    }
}

