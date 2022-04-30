

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXTextBox.cs
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
file:IGKXTextBox.cs
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
using System.Windows.Forms;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
using IGK.ICore;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent drs studio textbox
    /// </summary>
    public class IGKXTextBox : TextBox , IXTextControl 
    {
        private string m_TipText;

        /// <summary>
        /// Get or set the tip text. 
        /// </summary>
        public string TipText
        {
            get { return m_TipText; }
            set { this.m_TipText = value; }
        }
        public IGKXTextBox()
        {
            this.SetStyle(ControlStyles.FixedHeight, false);
            this.TextChanged += IGKXTextBox_TextChanged;
            
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

        }

        void IGKXTextBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.Text))
            { 

            }
        }


        Rectanglef IXTextControl.Bounds
        {
            get
            {
                Rectangle rc = base.Bounds;
                return new Rectanglef(rc.X, rc.Y, rc.Width, rc.Height);
            }
            set
            {
                base.Bounds = new Rectangle(
                    (int)value.X ,
                    (int)value.Y ,
                    (int)value.Width ,
                    (int)value.Height

                    );
            }
        }
        protected override bool IsInputChar(char charCode)
        {
            return base.IsInputChar(charCode);
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            return base.IsInputKey(keyData);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }

        public void SetFont(ICoreFont ft)
        {
            this.Font =  ft.ToGdiFont();
        }
        public void SetFont(ICoreFont ft, float size,  enuFontStyle style)
        {
            this.Font = ft.ToGdiFont(size, style);
        }
    }
}

