

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRibonBar.cs
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
file:IGKXRibonBar.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a ribon bar 
    /// </summary>
    public class IGKXRibonBar : IGKXControl 
    {
        const int BAR_SIZE = 28;
        public override Rectangle DisplayRectangle
        {
            get
            {
                Rectangle r = base.DisplayRectangle;
                r.Inflate(-4, -4);
                return r;
            }
        }
        public IGKXRibonBar()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.MinimumSize = new System.Drawing.Size (0,BAR_SIZE );
            this.MaximumSize = new System.Drawing.Size(0, BAR_SIZE);
            this.Dock = System.Windows.Forms.DockStyle.Top;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush v_br = WinCoreBrushRegister.CreateBrush(
              this.ClientRectangle,
              WinCoreControlRenderer.MainFormRibonBarOuterColor ,
              WinCoreControlRenderer.MainFormRibonBarInnerColor ,              
              90.0f))
            {
                v_br.SetSigmaBellShape(0.5f, 0.5f);
                e.Graphics.FillRectangle(v_br, this.ClientRectangle);
            }      
            base.OnPaint(e);
        }
    }
}

