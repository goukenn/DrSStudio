

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImagePickerForm.cs
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
file:ImagePickerForm.cs
*/
using IGK.ICore.WinCore;
using IGK.DrSStudio.PickSystemColorAddin.Tools;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.PickSystemColorAddin.WinUI
{
    internal class ImagePickerForm : Form
    {
         private Screen screen;
            public Screen Screen { get { return this.screen; } }
            private Rectanglei m_SelectedBound;
            public Rectanglei SelectedBound
            {
                get { return m_SelectedBound; }
                set
                {
                    if (m_SelectedBound != value)
                    {
                        m_SelectedBound = value;
                    }
                }
            }
            public ImagePickerForm()
            {
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.BackColor = Color.Black;
                this.Opacity = 0.1f;
                this.StartPosition = FormStartPosition.Manual;
                this.Click += FormPicker_Click;
            }
            void FormPicker_Click(object sender, EventArgs e)
            {
            }
            void UpdateSelection()
            {
                PickSCGlobalTool.Instance.SelectedRectangle = this.SelectedBound;
                PickSCGlobalTool.Instance.ClosePick(this);
            }
            private Vector2f m_startP;
            private Vector2f m_endP;
            protected override void OnMouseDown(MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.m_startP = new Vector2f(e.X, e.Y);
                        this.m_endP = this.m_startP;
                        break;
                }
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(MouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.m_endP = new Vector2f(e.X, e.Y);
                        this.Invalidate();
                        break;
                }
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(MouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case MouseButtons.Left :
                        this.m_endP = new Vector2f(e.X, e.Y);
                        Rectanglef v_rc = CoreMathOperation.GetBounds(this.m_startP, this.m_endP);
                            v_rc.X += this.Bounds.Location.X;
                            v_rc.Y += this.Bounds.Location.Y;
                        this.SelectedBound =Rectanglei.Round (v_rc );
                        break;
                }
                base.OnMouseUp(e);
            }
            protected override void OnKeyUp(KeyEventArgs e)
            {
                switch (e.KeyCode)
                { 
                    case Keys.Enter :
                        this.UpdateSelection();
                        break;
                }
                base.OnKeyUp(e);
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Rectanglef v_rc = CoreMathOperation.GetBounds (this.m_startP , this.m_endP );
                if (v_rc.IsEmpty == false)
                {
                    e.Graphics.FillRectangle(Brushes.SkyBlue, v_rc);
                    e.Graphics.DrawRectangle(Pens.Black, v_rc.X, v_rc.Y , v_rc.Width, v_rc.Height );
                }
            }
            public ImagePickerForm(Screen screen)
                : this()
            {
                this.screen = screen;
                this.Bounds = screen.Bounds;
            }
    }
}

