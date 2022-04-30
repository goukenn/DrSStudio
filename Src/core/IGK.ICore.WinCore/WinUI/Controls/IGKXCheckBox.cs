

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXCheckBox.cs
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
file:IGKXCheckBox.cs
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
using System.Text;
using System.Windows.Forms ;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;



namespace IGK.ICore.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinCore.WinUI;

    public class IGKXCheckBox : IGKXControl, IXCheckProperty
    {
        private bool m_Checked;
        private enuCheckState m_CheckState;
        const int TEXT_OFFSET = 22;
        [DefaultValue(true)]
        [Description("get or set checkbox autosize")]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                this.updateSize();
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(100, 24);
            }
        }

        [Category ("CheckBoxProperties")]
        public enuCheckState CheckState
        {
            get { return m_CheckState; }
            set
            {
                if (m_CheckState != value)
                {
                    m_CheckState = value;
                    OnCheckStateChanged(EventArgs.Empty);
                }

            }
        }
        public event EventHandler CheckStateChanged;
        ///<summary>
        ///raise the CheckStateChanged 
        ///</summary>
        protected virtual void OnCheckStateChanged(EventArgs e)
        {
            this.Invalidate();
            if (CheckStateChanged != null)
                CheckStateChanged(this, e);
        }

        [Category("CheckBoxProperties")]
        public bool Checked
        {
            get { return m_Checked; }
            set
            {
                if (m_Checked != value)
                {
                    m_Checked = value;
                    Invalidate();
                    OnCheckedChanged(EventArgs.Empty);
                }
            }
        }
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                updateSize();
            }
        }

        private void updateSize()
        {
                if (this.AutoSize && !this.IsDisposed)
                {
                    Graphics g = Graphics.FromHwnd(this.Handle);
                    SizeF c = g.MeasureString(this.Text, this.Font, new Size(short.MaxValue, short.MaxValue), new StringFormat());
                    this.Size = new System.Drawing.Size((int)Math.Ceiling(c.Width) + TEXT_OFFSET , (int)Math.Ceiling(c.Height));
                    g.Dispose();
                }
            
        }
        /// <summary>
        /// raised when checkvalue changed
        /// </summary>
        public event EventHandler CheckedChanged;
        ///<summary>
        ///raise the CheckedChanged  event
        ///</summary>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            this.Invalidate();
            if (CheckedChanged != null)
                CheckedChanged(this, e);
        }
        public IGKXCheckBox()
        {
            this.m_Checked = false;
            this.AutoSize = true;
            this.Paint += _Paint;
            this.Click += IGKXCheckBox_Click;
            
            this.updateSize();
        }

        void IGKXCheckBox_Click(object sender, EventArgs e)
        {
            this.Checked = !this.Checked;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int x = 4;
            int y = (this.Height - 16) / 2;
            ControlPaint.DrawCheckBox(e.Graphics, new Rectangle(x, y, 16, 16),
                this.Checked ? ButtonState.Checked : ButtonState.Normal);
         
                using (StringFormat v_sf = new StringFormat())
                {
                    v_sf.Alignment = StringAlignment.Near;
                    v_sf.LineAlignment = StringAlignment.Center ;
                    e.Graphics.DrawString(CoreResources.GetString(this.CaptionKey),
                        this.Font,
                        DesignMode? Brushes.Black : WinCoreBrushRegister.GetBrush(WinCoreControlRenderer.ForeColor),
                        new Rectangle(TEXT_OFFSET, 0, this.Width - 22, this.Height),
                        v_sf);
                }
            
           
        }
        void _Paint(object sender, CorePaintEventArgs e)
        {
            RenderCheckBox(e);
      
        }

        protected void RenderCheckBox(CorePaintEventArgs e)
        {
            e.Graphics.Clear(CoreRenderer.BackgroundColor);
            float x = 4;
            float y = (this.Height - 16) / 2; 


            e.Graphics.FillRectangle (Colorf.White , x, y , 16,16);
            e.Graphics.DrawRectangle (Colorf.Black , x, y , 16,16);
            if (this.Enabled)
            {
                if (this.Checked)
                {
                    e.Graphics.FillEllipse(Colorf.DeepSkyBlue, x+4, y, 8, 8);            
                }
                else
                    e.Graphics.FillEllipse(Colorf.Olive , x + 4, y, 8, 8);            
            }
            else {
                if (this.Checked)
                { 
                }
            }
        }

    }
}

