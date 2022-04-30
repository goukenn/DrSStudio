

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXRuleBase.cs
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
file:XRuleBase.cs
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
namespace IGK.ICore.WinCore
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI.Theme;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinCore.WinUI;
    public abstract class IGKXRuleBase : IGKXControl
    {
        private ICore2DDrawingRuleSurface m_surface;
        private Point m_mouseLocation;
        public ICore2DDrawingRuleSurface Surface { get { return m_surface; } }
        protected Point MouseLocation { get { return m_mouseLocation; } }

        

        protected  IGKXRuleBase(ICore2DDrawingRuleSurface surface) {
            if (surface == null)
                throw new CoreException( enuExceptionType.ArgumentIsNull, "surface");
            //avoid flickering
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true  );
            this.SetStyle(ControlStyles.ResizeRedraw, true );
            this.SetStyle(ControlStyles.AllPaintingInWmPaint,true);
            this.SetStyle(ControlStyles.UserPaint, true);
            m_surface = surface;
            m_surface.ShowRuleChanged  += new EventHandler(m_surface_ShowRulesChanged);
            m_surface.ShowScrollChanged += new EventHandler(m_surface_ShowScrollChanged);
            m_surface.ZoomChanged += new EventHandler(m_surface_ZoomChanged);
            m_surface.SizeChanged += new EventHandler(m_surface_SizeChanged);
            m_surface.MouseMove += m_surface_CoreMouseMove;
            this.BackColor = Color.White;
            var r = WinCoreControlRenderer.RuleTextFont;
            this.Font = r.ToGdiFont();
            r.FontDefinitionChanged += rt_ValueChanged;
            //rt = CoreThemeManager.GetSetting<CoreFont>("RuleTextFont", "FontName:Tahoma; size:8pt;" );
            //if (rt != null)
            //{
            //    this.Font = (rt.Value as CoreFont).ToGdiFont();
            //    rt.ValueChanged += rt_ValueChanged;
            //}
        }

        void rt_ValueChanged(object sender, EventArgs e)
        {
            this.Font = (sender as CoreFont).ToGdiFont();
            this.Invalidate();
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.m_surface.Focus();
        }
        void m_surface_CoreMouseMove(object o, CoreMouseEventArgs e)
        {
            m_mouseLocation = new Point (e.Location.X - Bounds.X ,
                e.Location.Y - Bounds.Y);
            //this.NotifyInvalidate(this.ClientRectangle);
            //this.Invalidate();
            //this.Update();//.Invalidate(false);
            this.Refresh();
        }
        void m_surface_SizeChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }
        protected virtual void SetUpBounds()
        {
        }
        void m_surface_ZoomChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.Invalidate();
                this.Update();
            }
        }
        void m_surface_ShowScrollChanged(object sender, EventArgs e)
        {
            this.Invalidate();
            this.Update();
        }
        void m_surface_ShowRulesChanged(object sender, EventArgs e)
        {
            this.Visible = this.m_surface.ShowRules;
            this.Invalidate();
        }
        protected abstract void OnShowRuleChanged(EventArgs eventArgs);
    }
}

