

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XCustomConvolutionControl.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XCustomConvolutionControl.cs
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
using System.Windows.Forms ;
using System.Drawing;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    /// <summary>
    /// represent the custom convolution program
    /// </summary>
    sealed class XCustomConvolutionControl : UIXConfigControlBase 
    {
        float[] m_values;
        private ImageElement m_ImageElement;
        private float m_Moyenne;
        private int m_Offset;
        private Bitmap m_oldBitmap;
        protected override void Dispose(bool disposing)
        {
            if (m_oldBitmap != null)
            {
                m_oldBitmap.Dispose();
            }
            base.Dispose(disposing);
        }
        public int Offset
        {
            get { return m_Offset; }
            set
            {
                if (m_Offset != value)
                {
                    m_Offset = value;
                }
            }
        }
        public float Moyenne
        {
            get { return m_Moyenne; }
            set
            {
                if (m_Moyenne != value)
                {
                    m_Moyenne = value;
                }
            }
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
        public XCustomConvolutionControl()
        {
            InitControl();
        }
        private void InitControl()
        {
            m_values = new float[25];
            this.Offset = 127;
            this.Moyenne = 9;
            m_values[12] = 1;
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
                    txb.Tag = (i*5) + j;
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
            IGKXRuleLabel rlab = new IGKXRuleLabel  ();
            rlab.Width = this.Width ;
            rlab.Location = new Point (offsetX , offsetY );
            rlab.Anchor = AnchorStyles.Left| AnchorStyles.Right | AnchorStyles.Top ;
            this.Controls.Add (rlab );
            Control[] ctr = new Control[] { 
                new IGKXLabel (){CaptionKey = "lb.average.caption"},
                new IGKXNumericTextBox (){Value = (decimal)this.Moyenne, AllowDecimalValue =true},
                new IGKXLabel (){CaptionKey = "lb.offset.caption"},
                new IGKXNumericTextBox (){Value = (decimal)this.Offset, AllowDecimalValue = false },
            };
            offsetY += 32;
            for (int i = 0; i < ctr.Length; i+=2)
            {
                offsetX = 10;
                ctr[i].Location = new Point(offsetX, offsetY);
                offsetX += 100;
                ctr[i+1].Location = new Point(offsetX, offsetY);
                offsetY += 32;
            }
            ((IGKXNumericTextBox)ctr[1]).ValueChanged += new EventHandler(_aValueChanged);
            ((IGKXNumericTextBox)ctr[3]).ValueChanged += new EventHandler(_oValueChanged);
            this.Controls.AddRange(ctr);
            offsetX = 10;
            rlab = new IGKXRuleLabel();
            rlab.Width = this.Width;
            rlab.Location = new Point(offsetX, offsetY);
            rlab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.Controls.Add(rlab);
            IGKXButton btn_ok = new IGKXButton() { DialogResult= enuDialogResult.OK };
            IGKXButton btn_cancel = new IGKXButton() { DialogResult = enuDialogResult.Cancel };
            offsetY += 40;
            btn_ok.Location = new Point(offsetX, offsetY);
            btn_ok.Size = new Size(80, 24);
            btn_ok.CaptionKey = CoreConstant.BTN_OK;
            offsetX += 100;
            btn_cancel.Location = new Point(offsetX, offsetY);
            btn_cancel.Size = new Size(80, 24);
            btn_cancel.CaptionKey = CoreConstant.BTN_CANCEL;
            offsetY += 38;
            this.Controls.Add(btn_ok );
            this.Controls.Add(btn_cancel);
            this.AcceptButton = btn_ok;
            this.CancelButton = btn_cancel;
            this.Size = new Size(350, 500);            
            this.ResumeLayout();
        }
        void _aValueChanged(object sender, EventArgs e)
        {
            this.Moyenne  = (float)((IGKXNumericTextBox)sender).Value;
            ApplyConvolution(true);
        }
          void _oValueChanged(object sender, EventArgs e)
        {
            this.Offset = (int)((IGKXNumericTextBox)sender).Value;
            ApplyConvolution(true);
        }
        void txb_ValueChanged(object sender, EventArgs e)
        {
            IGKXNumericTextBox txb = sender as IGKXNumericTextBox;
            int index = (int)txb.Tag;
            this.m_values[index] =(float) txb.Value;
            ApplyConvolution(true);
        }
        internal void ApplyConvolution(bool temp)
        {
            try
            {
                CoreConvolutionParam p = new CoreConvolutionParam(
                    Moyenne, m_values, Offset);
                this.ImageElement.SetBitmap(
                    p.ApplyConvolution(
                    m_oldBitmap, null).ToCoreBitmap (), temp);
                this.CurrentSurface.RefreshScene();
            }
            catch (Exception ex){
                CoreLog.WriteDebug("Error When Applying custom convolution : " + ex.Message);
            }
        }
        internal void Restore()
        {
            if (this.m_oldBitmap != null)
            {
                ICoreBitmap bmp = (this.m_oldBitmap.Clone() as Bitmap).ToCoreBitmap();
                this.ImageElement.SetBitmap(bmp, true);
                this.m_oldBitmap.Dispose();
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XCustomConvolutionControl
            // 
            this.Name = "XCustomConvolutionControl";
            this.Size = new System.Drawing.Size(334, 269);
            this.ResumeLayout(false);

        }
        /// <summary>
        /// get or set the current surface
        /// </summary>
        public ICore2DDrawingSurface CurrentSurface { get; set; }
    }
}

