

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioConfirmDialog.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using System.Threading;
    using System.Windows.Forms;
    using IGK.ICore.Settings;
    using IGK.ICore.WinCore.WinUI;

    public  delegate bool ConfirmMouseEventPROC(enuMouseButtons bouton, Vector2f location);

    /// <summary>
    /// represent a config dialog
    /// </summary>
    public sealed class DrSStudioConfirmDialog : XFormDialog ,  ICoreDialogForm 
    {
        private RectangleElement m_header;
        private StringElement m_string;
        private RectangleElement m_footer;
        private RectangleElement m_content;
        private Core2DDrawingLayer m_layer;
        private LayerDispatcher m_dispatcher;
        private DrSStudioButton m_btn;
        


        public LayerDispatcher Dispatcher {
            get {
                return this.m_dispatcher;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_string.Dispose();
                this.m_layer.Dispose();
                this.m_layer = null;
            }
            base.Dispose(disposing);
        }
        protected override void InitLayout()
        {
            base.InitLayout();
            int w = this.ClientSize.Width;
            int h = this.ClientSize.Height;
            this.m_header.SuspendLayout();
            this.m_header.Bounds = new Rectanglef(0, 0, w, 32);
            this.m_header.ResumeLayout();

            this.m_content.SuspendLayout();
            this.m_content.Bounds = new Rectanglef(0, 32, w,  h- 64);
            this.m_content.ResumeLayout();

            this.m_string.SuspendLayout();
            this.m_string.Bounds = this.m_content.Bounds;
            this.m_string.Font.VerticalAlignment = enuStringAlignment.Center;
            this.m_string.Font.HorizontalAlignment = enuStringAlignment.Center;
            this.m_string.ResumeLayout();

            this.m_btn.SuspendLayout();
            Rectanglef rc = new Rectanglef(0, 0, 120, 32);
            //rc.Inflate(-4, -4);
            this.m_btn.Bounds = new Rectanglef (w -  rc.Width - 4, h-rc.Height -4, rc.Width , rc.Height );
            this.m_btn.ResumeLayout();

            this.m_footer.SuspendLayout();
            this.m_footer.Bounds = new Rectanglef(0, h - 32, w, 32);
            this.m_footer.ResumeLayout();
        }
        public DrSStudioConfirmDialog()
        {
            m_dispatcher = new LayerDispatcher();
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);

            m_btn = new DrSStudioButton();

            m_btn.Text = "btn.yes".R ();
            
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.m_string = new StringElement();
            this.m_header = new RectangleElement();
            this.m_footer = new RectangleElement();
            this.m_content = new RectangleElement();

            CoreRenderer.RenderingValueChanged += _RenderingValueChanged;
            this.SetupColor();

            this.m_layer = new Core2DDrawingLayer();
            this.m_layer.Elements.AddRange(new ICore2DDrawingLayeredElement[] {
                this.m_header ,
                this.m_content ,
                this.m_footer ,
                this.m_string ,
                this.m_btn
            });

            this.m_string.SuspendLayout();
            this.m_string.Font.FontName = "Consolas";
            this.m_string.ResumeLayout();

            this.Dispatcher.Register(this.m_btn,LayerDispatcher.MouseClickDispatching, new EventHandler((o,e) =>
            {
                this.FindForm().DialogResult = System.Windows.Forms.DialogResult.Yes;
            }));

            this.Dispatcher.Register(this.m_btn, LayerDispatcher.MouseHoverDispatching, new EventHandler((o, e) =>
            {
                
            }));
            this.Dispatcher.Register(this.m_btn, LayerDispatcher.MouseMoveDispatching, new EventHandler((o, e) =>
            {
                var s = e as IGK.ICore.WinUI.Dispatch.CoreDispatcherEventArgs<CoreMouseEventArgs>;
                if (s == null)
                    return;
                CoreMouseEventArgs v_e = s.Event;
                enuButtonState v_s =  this.m_btn.State;
                if (this.m_btn.Contains(v_e.Location))
                {
                    if (v_s != enuButtonState.Hover)
                    {
                        this.m_btn.State = enuButtonState.Hover;
                        this.Cursor = Cursors.Hand;
                        this.Refresh();
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    if (v_s != enuButtonState.Normal)
                    {
                        this.m_btn.State = enuButtonState.Normal;
                        this.Refresh();
                    }
                }
                
            }));

            this.Paint += _Paint;
            this.UpdateSize();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.SizeChanged += _SizeChanged;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
        }

        
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaintBackground(e);

        }
        protected override void OnCoreMouseClick(CoreMouseEventArgs e)
        {
            
            bool m =  Dispatcher.MouseClick(e);//(enuMouseButtons)e.Button, new Vector2f(e.X , e.Y));
            if (m) {
                return;
            }
            base.OnCoreMouseClick(e);
        }
        protected override void OnCoreMouseMove(CoreMouseEventArgs e)
        {
            bool m = Dispatcher.MouseMove(e);//(enuMouseButtons)e.Button, new Vector2f(e.X, e.Y));
            if (m)
            {
                return;
            }
            base.OnCoreMouseMove(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            var p = PointToClient(Control.MousePosition);
            //bool m = Dispatcher.MouseHover(new Vector2f(p.X, p.Y));
            //if (m)
            //{
            //    return;
            //}
            base.OnMouseHover(e);
        }
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

        }

        private void _RenderingValueChanged(object sender, EventArgs e)
        {
            this.SetupColor();
            ICoreRendererSetting setting = sender as ICoreRendererSetting;
            
        }

        private void SetupColor()
        {
            this.m_header.FillBrush.SetSolidColor(WinCoreControlRenderer.ConfirmHeaderColor);
            this.m_content.FillBrush.SetSolidColor(WinCoreControlRenderer.ConfirmContentColor);
            this.m_footer.FillBrush.SetSolidColor(WinCoreControlRenderer.ConfirmFooterColor);

            this.m_content.StrokeBrush.SetSolidColor(Colorf.Transparent);
            this.m_header.StrokeBrush.SetSolidColor(Colorf.Transparent);
            this.m_footer.StrokeBrush.SetSolidColor(Colorf.Transparent);
        }

        private void _SizeChanged(object sender, EventArgs e)
        {
            this.UpdateSize();
        }

        private void UpdateSize()
        {
            this.InitLayout();

            this.m_string.SuspendLayout();
            this.m_string.Font.FontSize = "8pt".ToPixel();
            this.m_string.ResumeLayout();
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
         //   this.m_string.Draw(e.Graphics);
            this.m_layer.Draw(e.Graphics);
            //this.m_footer.Draw(e.Graphics);
        }

        public string Message { get {
            return this.m_string.Text;
        }
            set {
                this.m_string.Text = value;
            }
        }
    }
}
