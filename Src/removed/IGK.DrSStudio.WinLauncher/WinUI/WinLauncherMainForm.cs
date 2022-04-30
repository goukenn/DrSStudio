

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherMainForm.cs
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
file:WinLauncherMainForm.cs
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
using System.Windows.Forms;
namespace IGK.DrSStudio.WinLauncher
{
    using IGK.ICore;using IGK.DrSStudio;
    using IGK.DrSStudio.Native;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represent the application laucher MainForm
    /// </summary>
    sealed class WinLaucherMainForm : XMainForm
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams p = base.CreateParams;
                //p.ExStyle |= User32.WS_EX_TOOLWINDOW;
                //p.Style = p.Style | User32.WS_BORDER |
                //    User32.WS_SIZEBOX |
                //    User32.WS_SYSMENU;
                return p;
            }
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle v_rc = base.DisplayRectangle;
                if (this.WindowState == FormWindowState.Normal )
                    v_rc.Inflate(-4, -4);
                return v_rc;
            }
        }
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }
        protected override IGK.DrSStudio.WinUI.ICoreWorkbench CreateWorkbench()
        {
            return new WinLauncherWorkBench (this);
        }
        protected override void BuildClientRectangle(ref System.Drawing.Rectangle rc, bool maximized)
        {
            base.BuildClientRectangle(ref rc, maximized);
        }
        public WinLaucherMainForm():base()
        {
            this.InitializeComponent();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.MinimumSize = new System.Drawing.Size(660, 400);
            WinLaucherMainFormSetting.Instance.Bind(this);
            this.Paint += new PaintEventHandler(_Paint);
        }
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //no background painting
            //base.OnPaintBackground(e);
        }
        void _Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(CoreRenderer.GetColor("MainformBgColor", Colorf.Transparent));
            //IGK.DrSStudio.Drawing2D.ICore2DDrawingDocument v_document =CoreResources.GetDocument("MainformBackground");
            //if (v_document != null)
            //{
            //    Rectanglei v_rc = this.DisplayRectangle;
            //    v_rc.Inflate(-4, -5);
            //    v_document.Draw(e.Graphics, true, v_rc, IGK.DrSStudio.Drawing2D.enuFlipMode.None);
            //}
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WinLaucherMainForm));
            this.SuspendLayout();
            // 
            // WinLaucherMainForm
            // 
            this.ClientSize = new System.Drawing.Size(760, 500);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WinLaucherMainForm";
            this.ResumeLayout(false);
        }
    }
}

