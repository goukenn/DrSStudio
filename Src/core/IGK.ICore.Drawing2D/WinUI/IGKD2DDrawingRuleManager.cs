

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingRuleManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WinUI
{
    /// <summary>
    /// drawing rule manager
    /// </summary>
    class IGKD2DDrawingRuleManager
    {
        private IGKD2DDrawingSurfaceBase m_surface;
        private IGKXHRule c_hRule;
        private IGKXVRule c_vRule;
        public IGKD2DDrawingRuleManager(IGKD2DDrawingSurfaceBase iGKD2DDrawingSurfaceBase)
        {
            
            this.m_surface = iGKD2DDrawingSurfaceBase;

            c_vRule = new IGKXVRule(this.m_surface as ICore2DDrawingRuleSurface);
            c_hRule = new IGKXHRule(this.m_surface as ICore2DDrawingRuleSurface);
            this.m_surface.ShowRuleChanged += m_surface_ShowRuleChanged;
            this.m_surface.SizeChanged += m_surface_SizeChanged;
            this.m_surface.Disposed += m_surface_Disposed;            
            this.InitRule();
        }

        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.c_vRule.Dispose();
            this.c_hRule.Dispose();
        }

        void m_surface_SizeChanged(object sender, EventArgs e)
        {
            this.InitBound();
        }

        private void InitBound()
        {
            if (this.m_surface.ShowRules)
            {
                this.c_vRule.Bounds = new System.Drawing.Rectangle(0, m_surface.RuleHeight,
                    m_surface.RuleWidth, m_surface.Height);

                this.c_hRule.Bounds = new System.Drawing.Rectangle(m_surface.RuleWidth, 0,
                    m_surface.Width , m_surface.RuleHeight);
            }
        }

        void m_surface_ShowRuleChanged(object sender, EventArgs e)
        {
            InitRule();
        }

        private void InitRule()
        {
            if (this.m_surface.ShowRules)
            {
                this.m_surface.Controls.Add(c_hRule);
                this.m_surface.Controls.Add(c_vRule);
                this.InitBound();
            }
            else
            {
                this.m_surface.Controls.Remove(c_hRule);
                this.m_surface.Controls.Remove(c_vRule);
            }
        }
    }
}
