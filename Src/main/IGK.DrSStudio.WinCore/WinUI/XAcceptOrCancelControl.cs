

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAcceptOrCancelControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XAcceptOrCancelControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
namespace IGK.DrSStudio.WinUI
{
    public class XAcceptOrCancelControl : IGKXUserControl 
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
        public XAcceptOrCancelControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
           // this.BackColor = System.Drawing.Color.Transparent;
        }
        [System.ComponentModel.Browsable(false)]
        public ICoreMainForm MainForm {
            get {
                return CoreSystem.GetMainForm();
            }
        }
        [System.ComponentModel.Browsable (false )]
        public ICoreWorkingSurface CurrentSurface {
            get {
                if (this.MainForm !=null)
                return MainForm.Workbench.CurrentSurface;
                return null;
            }
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            if (this.ParentForm != null)
            {
               // this.ParentForm.AcceptButton = this.AcceptButton;
                this.ParentForm.CancelButton = this.CancelButton;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //no paint background
            e.Graphics.Clear(CoreRenderer.BackgroundColor);
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
            this.Size = new System.Drawing.Size(368, 243);
            this.ResumeLayout(false);
        }
    }
}

