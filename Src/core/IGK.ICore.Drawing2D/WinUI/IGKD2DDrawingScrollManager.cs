

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingScrollManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    class IGKD2DDrawingScrollManager
    {
        private IGKD2DDrawingSurfaceBase c_surface;
        private IGKXHorizontalScrollBar c_hscroll;
        private IGKXVerticalScrollBar c_vscroll;
        private float  m_PosX;
        private float  m_PosY;
        private bool m_configuring;
        const int SCROLL_OFFSET = 200; // space in scroll size


        public IGKD2DDrawingScrollManager(IGKD2DDrawingSurfaceBase surface)
        {
            
            this.c_surface = surface;
            this.c_hscroll = new IGKXHorizontalScrollBar();
            this.c_vscroll = new IGKXVerticalScrollBar();
            this.c_surface.ShowScrollChanged += m_surface_ShowScrollChanged;
            this.c_surface.ShowRuleChanged += m_surface_ShowRuleChanged;
            this.c_surface.Disposed += m_surface_Disposed;
            this.c_surface.SizeChanged += m_surface_SizeChanged;
            this.c_surface.ZoomChanged += m_surface_ZoomChanged;
            this.c_surface.ZoomModeChanged += m_surface_ZoomModeChanged;
            this.c_hscroll.ValueChanged += _ValueChanged;
            this.c_vscroll.ValueChanged += _ValueChanged;
            this._initScroll();
        }

     

        void _ValueChanged(object sender, EventArgs e)
        {
            UpdatePostValue();
        }

        private void UpdatePostValue()
        {

            if (this.c_surface.ShowScroll && !this.m_configuring)
            {
                this.m_configuring = true;
                this.c_surface.ScrollTo(
                this.m_PosX -this.c_hscroll.Value,
                this.m_PosY -this.c_vscroll.Value );
                this.m_configuring = false;
                
            }
        }

        void m_surface_ShowRuleChanged(object sender, EventArgs e)
        {
            this._initBound();
        }

        void m_surface_ZoomModeChanged(object sender, EventArgs e)
        {
            this._initBound();
        }

        void m_surface_ZoomChanged(object sender, EventArgs e)
        {
            this._initBound();
        }

        void m_surface_SizeChanged(object sender, EventArgs e)
        {
            this._initBound();
        }

        void m_surface_Disposed(object sender, EventArgs e)
        {
            this.c_vscroll.Dispose();
            this.c_hscroll.Dispose();
        }

        void m_surface_ShowScrollChanged(object sender, EventArgs e)
        {
            this._initScroll();
        }
        /// <summary>
        /// init scroll value. on surface
        /// </summary>
        private void _initScroll()
        {
            if (this.c_surface.ShowScroll)
            {
                this.c_surface.Controls.Add(c_vscroll);
                this.c_surface.Controls.Add(c_hscroll);
                this._initBound();
            }
            else
            {
                this.c_surface.Controls.Remove(c_vscroll);
                this.c_surface.Controls.Remove(c_hscroll);

            }
        }

        private void _initBound()
        {
            if (c_surface.ShowScroll)
            {
                int w = c_surface.Width;
                int h = c_surface.Height;
                int rh = c_surface.ShowRules ? c_surface.RuleHeight : 0;
                int rw = c_surface.ShowRules ? c_surface.RuleWidth  : 0;
                c_vscroll.Bounds = new System.Drawing.Rectangle(
                    w - c_surface.ScrollWidth , rh,
                    c_surface.ScrollWidth , h - c_surface.ScrollHeight - rh 
                    );
                c_hscroll.Bounds = new System.Drawing.Rectangle(
                    rw, h - c_surface.ScrollHeight              ,
                    w - c_surface.ScrollWidth - rw,c_surface.ScrollHeight 
                    );
                this.m_setupScrollValue();
            }
        }
        /*
         * configure the values of this scroll bounds
         * */
        private void m_setupScrollValue()
        {
            if (this.m_configuring)
                return;

            this.m_configuring = true;
            //setup scroll value
            float v_zoomX = this.c_surface.ZoomX;
            float v_zoomY = this.c_surface.ZoomY;
            //save offset position
            this.m_PosX = this.c_surface.PosX;
            this.m_PosY = this.c_surface.PosY;
            Rectanglef rc = this.c_surface.DisplayArea;
            //rc.Inflate (-16,-16);
            c_vscroll.MinValue = 0;
            c_vscroll.MaxValue = 0;
            c_vscroll.Value = 0;

            c_hscroll.MinValue = 0;
            c_hscroll.MaxValue = 0;
            c_hscroll.Value = 0;
            float H = rc.Height;
            float W = rc.Width;
            float v_docw = this.c_surface.CurrentDocument.Width;
            float v_doch = this.c_surface.CurrentDocument.Height;
            switch (this.c_surface.ZoomMode)
            {
                case enuZoomMode.Normal:
                    
                    break;
                case enuZoomMode.NormalCenter:
                     float hh, ww;
                        //trouver la moitié
                     ww = (W - (v_docw * v_zoomX)) / 2.0f;
                     hh = (H - (v_doch * v_zoomY)) / 2.0f;
                        if (hh < 0.0f)
                        {
                            int xh = (int)(Math.Ceiling(hh - SCROLL_OFFSET));
                            this.c_vscroll.MinValue = xh;
                            this.c_vscroll.MaxValue = -xh;

                        }
                        if (ww < 0.0f)
                        {
                            int xh = (int)(Math.Ceiling(ww - SCROLL_OFFSET));
                            this.c_hscroll.MinValue = xh;
                            this.c_hscroll.MaxValue = -xh;
                        }
                    break;
                case enuZoomMode.Stretch:
                    break;
                case enuZoomMode.ZoomCenter:

                 
                    break;
                case enuZoomMode.ZoomNormal:
                    break;
                default:
                    break;
            }
            this.m_configuring = false;
        }
    }
}
