

using IGK.ICore.WinCore.WinUI.Controls;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXConfigControlBase.cs
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
file:UIXConfigControlBase.cs
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
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a base control to configure an items
    /// </summary>
    public class UIXConfigControlBase: 
        IGKXUserControl          
    {
        private IButtonControl m_AcceptButton;
        public IButtonControl AcceptButton
        {
            get { return m_AcceptButton; }
            set
            {
                if (m_AcceptButton != value)
                {
                    m_AcceptButton = value;
                }
            }
        }
        private IButtonControl m_CancelButton;
        public IButtonControl CancelButton
        {
            get { return m_CancelButton; }
            set
            {
                if (m_CancelButton != value)
                {
                    m_CancelButton = value;
                }
            }
        }
        public UIXConfigControlBase()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);           
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.ParentForm != null)
            {
                this.ParentForm.AcceptButton = this.AcceptButton;
                this.ParentForm.CancelButton = this.CancelButton;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //no paint background
            //e.Graphics.Clear(CoreRenderer.BackgroundColor);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
          //  e.Graphics.Clear(CoreRenderer.BackgroundColor);
            base.OnPaint(e);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // UIXConfigControlBase
            // 
            this.Name = "UIXConfigControlBase";
            this.Size = new System.Drawing.Size(601, 336);
            this.ResumeLayout(false);
        }
    }
}

