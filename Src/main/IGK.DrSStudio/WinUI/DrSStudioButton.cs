

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioButton.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    class DrSStudioButton : RectangleElement, ICore2DDrawingVisitable 
    {
        private string m_Text;
        private ButtonElement m_btn;

        public enuButtonState State {
            get {
                return this.m_btn.ButtonState;
            }
            set
            {
                if (this.m_btn.ButtonState != value)
                {
                    this.m_btn.ButtonState = value;
                }
            }
        }
        public string Text
        {
            get { return m_Text; }
            set
            {
                if (m_Text != value)
                {
                    m_Text = value;
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }

        private void OnTextChanged(EventArgs eventArgs)
        {
            if (this.m_btn.ButtonDocument != null)
            {
                this.UpdateInfo();
            }
        }

        private void UpdateInfo()
        {

            if (this.m_btn.ButtonDocument != null)
            {
                foreach (ICore2DDrawingDocument doc in this.m_btn.ButtonDocument.GetDocuments())
                {
                    var t = doc.GetElementById("text") as StringElement;
                    if (t != null)
                    {
                        t.SuspendLayout();
                        t.Text = this.Text;
                        t.IsMnemonic = true;
                        t.Trimming = enuTextTrimming.Char;
                        t.ResumeLayout();

                    }

                }
            }
        }
        public DrSStudioButton()
        {
        
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            m_btn = new ButtonElement()
            {
                ButtonDocument =
                CoreButtonDocument.Create(CoreResources.GetAllDocuments(Resources.Keys.button_120x24))
            };
            UpdateInfo();
        }

        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            p.AddRectangle(this.Bounds);
            if (this.m_btn != null)
            {
                this.m_btn.Bounds = this.Bounds;
            }

        }



        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            Rectanglef v_rc = this.Bounds;
            visitor.DrawRectangle(Colorf.Black,
                v_rc.X,
                v_rc.Y,
                v_rc.Width,
                v_rc.Height);

            this.m_btn.Visit(visitor);
        }
    }
}
