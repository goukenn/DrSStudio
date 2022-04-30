

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXButton.cs
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
file:IGKXButton.cs
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.ICore.WinCore.WinUI.Controls
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore;

    [Designer("System.Windows.Forms.Design.ControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]  
    public class IGKXButton : IGKXControl , 
        ICoreControl , 
        ICoreButtonControl,
        ICoreWindowKeyEvent ,
        IButtonControl 
    {
        private bool m_Checked;
        private bool m_DrawBorder;
        private bool m_ShowButtonImage;
        private bool m_AutoButtonSize;
        [DefaultValue(true)]
        public bool AutoButtonSize
        {
            get { return m_AutoButtonSize; }
            set
            {
                if (m_AutoButtonSize != value)
                {
                    m_AutoButtonSize = value;
                }
            }
        }
        public void SetLocation(Vector2i i)
        {
            this.Location = new Point(i.X, i.Y);
        }
        protected override Size DefaultSize
        {
            get
            {
                return new Size(32, 32);
            }
        }
        private Size2i m_ButtonImageSize;
        [System.ComponentModel.Category("View")]
        [System.ComponentModel.DefaultValue("32;32")]
        public Size2i ButtonImageSize
        {
            get { return m_ButtonImageSize; }
            set
            {
                if (!m_ButtonImageSize.Equals(value))
                {
                    m_ButtonImageSize = value;
                    this.Invalidate();
                }
            }
        }
        [System.ComponentModel.Category("View")]
        [System.ComponentModel.DefaultValue(true)]
        public bool ShowButtonImage
        {
            get { return m_ShowButtonImage; }
            set
            {
                if (m_ShowButtonImage != value)
                {
                    m_ShowButtonImage = value;
                    this.Invalidate();
                }
            }
        }
        [System.ComponentModel.Category("View")]
        [System.ComponentModel.DefaultValue(true)]
        /// <summary>
        /// get or set if the button will draw the border
        /// </summary>
        public bool DrawBorder
        {
            get { return m_DrawBorder; }
            set
            {
                if (m_DrawBorder != value)
                {
                    m_DrawBorder = value;
                }
            }
        }
        public event EventHandler CheckedChanged;
        public bool Checked
        {
            get { return m_Checked; }
            set
            {
                if (m_Checked != value)
                {
                    m_Checked = value;
                    this.Invalidate();
                    OnCheckedChagned(EventArgs.Empty);
                }
            }
        }
        private bool m_DocumentUsedAsBrush;
        [System.ComponentModel.Category("View")]
        [System.ComponentModel.DefaultValue(false)]
        /// <summary>
        /// get or set if the button document will be renderer as texture brush
        /// </summary>
        public bool DocumentUsedAsBrush
        {
            get { return m_DocumentUsedAsBrush; }
            set
            {
                if (m_DocumentUsedAsBrush != value)
                {
                    m_DocumentUsedAsBrush = value;
                }
            }
        }
        protected virtual void OnCheckedChagned(EventArgs eventArgs)
        {
            this.CheckedChanged?.Invoke(this, eventArgs);
        }
        public IGKXButton()
        {
            
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            this.m_State = enuButtonState.Normal;
            this.m_AutoButtonSize = true;

            this.ShowButtonImage = false;            
            this.ButtonImageSize = new Size2i(32, 32);
            this.DrawBorder = true;
            
            this.ButtonDocumentChanged += _ButtonDocumentChanged;
            this.CaptionChanged += (o, e) => { this.Invalidate(); };
            this.EnabledChanged += (o, e) => { this.Invalidate(); };
        }

        
        void _ButtonDocumentChanged(object sender, EventArgs e)
        {
            if ((this.ButtonDocument != null) && (this.AutoButtonSize))
            {
                this.Size = new Size (
                    this.ButtonDocument.Normal.Width,
                    this.ButtonDocument.Normal.Height);
            }
        }
        Rectanglef GetTextBound()
        {
            if (this.ShowButtonImage)
            {
                Rectangle v_rc = this.ClientRectangle;
                Rectanglef rc = new Rectanglef(v_rc.X + 4 + this.ButtonImageSize.Width,
         v_rc.Y, 0, 0);
                rc.Width = this.ClientRectangle.Right - rc.X;
                rc.Height = this.Height;
                if ((rc.Width > 0) && (rc.Height > 0))
                    return rc;
                return Rectanglef.Empty;
            }
            return this.ClientRectangle.CoreConvertFrom<Rectanglef>();
        }
     
        protected override void OnClick(EventArgs e)
        {
            if (Enabled)
            {
                Form frm = this.FindForm();
                if (frm != null)
                {
                    frm.DialogResult =(DialogResult) this.DialogResult;
                }
                base.OnClick(e);
               
            }
        }
        protected override void OnEnabledChanged(EventArgs e)
        {
            if (!this.Enabled)
            {
                this.State = enuButtonState.Disabled;
            }
            else
            {
                //restaure state
                this.State = enuButtonState.None;
            }
            base.OnEnabledChanged(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            if (this.Enabled)
            {
                this.State = enuButtonState.Hover;
            }
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.Enabled)
            {
                this.State = enuButtonState.Normal;
            }
            base.OnMouseLeave(e);
        }
        protected override void OnCoreMouseDown(CoreMouseEventArgs e)
        {
            if (this.Enabled)
                this.State = enuButtonState.Down;
            base.OnCoreMouseDown(e);
        }
        protected override void OnCoreMouseUp(CoreMouseEventArgs e)
        {
            if (this.Enabled)
            {
                if (this.State == enuButtonState.Normal)
                {
                }
                else
                {
                    if (this.ClientRectangle.Contains(e.Location))
                    {
                        this.State = enuButtonState.Hover;
                    }
                    else
                        this.State = enuButtonState.Up;
                }
            }
            base.OnCoreMouseUp(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Invalidate();
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color BackColor
        {
            get
            {
                return WinCoreControlRenderer.ButtonBackgroundColor.ToGdiColor();
            }
            set
            {
                base.BackColor = value;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //don't paint background to avoid flickering
            base.OnPaintBackground(pevent);
        }
        protected override void OnCorePaint(CorePaintEventArgs e)
        {
            if (DesignMode)
                return;
            //e.Graphics.Clear(Color.Red);
            if (!this.DesignMode && (this.ButtonDocument != null))
            {
                RenderButton(e.Graphics);
            }
            else
            {

                Colorf v_fcl = Colorf.Empty;
                Colorf v_scl = Colorf.Empty;
                GetStateColor(ref v_fcl, ref v_scl);
                e.Graphics.Clear(v_fcl);
                string txt = this.CaptionKey.R();
                if (!string.IsNullOrEmpty(txt))
                {                    
                        Brush br = WinCoreBrushRegister.GetBrush(CoreRenderer.ForeColor);
                        CoreFont ft = WinCoreControlRenderer.ButtonFont;

                        if (this.Focused)
                        {
                            ft.HotKeyPrefix = enuHotKeyPrefix.Show;
                        }
                        else
                        {
                            ft.HotKeyPrefix = enuHotKeyPrefix.Hide;
                        }
                        ft.HorizontalAlignment = enuStringAlignment.Center;
                        ft.VerticalAlignment = enuStringAlignment.Center;
                        var v_rc = new Rectanglef(0, 0, this.Width, this.Height);
                        v_rc.Inflate(-4, -4);

                        e.Graphics.DrawString(
                           txt,
                            ft,
                            v_scl,
                            v_rc
                            );
                    
                }
            }
            base.OnCorePaint(e);
        }
     
        protected virtual void RenderButton(ICoreGraphics g)
        {
            Rectanglei v_rc = new Rectanglei(
                this.ClientRectangle.X,
                this.ClientRectangle.Y,
                this.ClientRectangle.Width,
                this.ClientRectangle.Height);
            //v_rc.Width -= 2;
            //v_rc.Height -= 2;
            if ((v_rc.Width <= 0) || (v_rc.Height <= 0) || (g == null))
                return;
            object  v_state = null;
            //Colorf cl1 = WinCoreControlRenderer.ButtonBackgroundStartColor;
            //Colorf cl2 = WinCoreControlRenderer.ButtonBackgroundEndColor;
            //Colorf cl3 = this.State == enuButtonState.Hover ? WinCoreControlRenderer.ButtonBorderOverColor : WinCoreControlRenderer.ButtonBorderColor;
            //Colorf cl4 = this.State == enuButtonState.Hover ? WinCoreControlRenderer.ButtonHightLightOverColor : WinCoreControlRenderer.ButtonHightLightColor;
            ICoreGraphics v_g = g;
            v_state =g.Save();
            if (this.ButtonDocument != null)
            {
                ICore2DDrawingDocument v_doc = null;
                v_doc = GetVisibleButton(this.ButtonDocument);
                if (this.DocumentUsedAsBrush)
                {
                    Bitmap bmp = new Bitmap(v_doc.Width, v_doc.Height);
                    Graphics v_dg = Graphics.FromImage(bmp);                    
                    v_doc.Draw(v_g);
                    v_g.Flush();
                    using (TextureBrush txb = new TextureBrush(bmp))
                    {
                        g.FillRectangle(txb, v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);
                    }
                    bmp.Dispose();
                }
                else
                {
                    try
                    {
                      
                        v_doc.Draw(v_g, v_rc);;
                        v_g.Flush();                        
                    }
                    catch (Exception)
                    {
                    }
                }
                if (this.ShowButtonImage)
                {
                   _DrawImageButton(g, v_rc.X ,v_rc.Y, v_rc.Width, v_rc.Height );
                }
            }
            else
            {
                g.PixelOffsetMode = enuPixelOffset.Half;
                if (!this.Enabled)
                {
                    //cl1 = Colorf.DarkGray;
                    //cl2 = Colorf.SlateGrey;
                }
                Rectanglef v_rcf = v_rc;
                //using (LinearGradientBrush lb = WinCoreBrushRegister.CreateBrush<LinearGradientBrush>(
                //    v_rcf,
                //    cl1,
                //    cl2,
                //    90.0f))
                //{
                Colorf v_fcl = Colorf.Empty;
                Colorf v_scl = Colorf.Empty;
                GetStateColor(ref v_fcl, ref v_scl);
                v_g.FillRectangle(v_fcl, v_rcf);
                //}
                if (this.ShowButtonImage)
                {
                    //_DrawImageButton(g, v_rcf.X, v_rcf.Y, v_rcf.Width, v_rcf.Height);
                }
                Rectanglei v_trc = v_rc;
                v_trc.Height = System.Math.Max(v_rc.Height / 3, 8);
                v_trc.Width++;
                //g.FillRectangle(CoreBrushRegister.GetBrush<SolidBrush>(cl4), v_trc);
                //g.PixelOffsetMode = PixelOffsetMode.Default;
            }
            g.Restore(v_state);
            v_state = null;
            if (this.Checked && this.DrawBorder)
            {
                //_DrawBorder(g, this.ClientRectangle, WinCoreControlRenderer.ButtonCheckedBorderColor, DashStyle.Solid);
            }
            else if (DrawBorder)
            {
                //_DrawBorder(g, this.ClientRectangle, cl3, DashStyle.Solid);
            }
            string txt = this.CaptionKey.R();
            //render text
            if (!string.IsNullOrEmpty(txt))
            {

                StringFormat sf = new StringFormat(StringFormat.GenericTypographic)
                {
                    Trimming = StringTrimming.EllipsisWord
                };

                Size2f s = g.MeasureString(txt, this.Font, new Size2f (short.MaxValue, short.MaxValue ), sf);
                RectangleF v_r = new RectangleF(0, 0, s.Width, s.Height);
                
                if (this.ShowKeyboardCues)
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Show;
                else
                    sf.HotkeyPrefix = System.Drawing.Text.HotkeyPrefix.Hide;
                Rectanglef v_tbound = this.GetTextBound();
               
                sf.Dispose();
            }         
        }
        /// <summary>
        /// get the state color
        /// </summary>
        /// <returns></returns>
        private void GetStateColor(ref Colorf fillColor, ref Colorf strokeColor)
        {
            if (!this.Enabled)
            {
                fillColor = WinCoreControlRenderer.ButtonDisabledBackgroundColor;
                strokeColor = WinCoreControlRenderer.ButtonDisabledTextColor;
                return;
            }

            switch (this.State)
            {

                case enuButtonState.Hover:
                    fillColor = WinCoreControlRenderer.ButtonHighLightColor;
                    strokeColor = WinCoreControlRenderer.ButtonTextHighLightColor;
                    break;
                case enuButtonState.Disabled:
                    fillColor = Colorf.FromFloat(0.4f);
                    strokeColor = Colorf.FromFloat(0.7f);
               
                    break;
                case enuButtonState.None:
                case enuButtonState.Normal:
                case enuButtonState.Down:
                case enuButtonState.Up:
                default:
                    fillColor = Focused?WinCoreControlRenderer.ButtonFocusNormalBackgroundColor  : WinCoreControlRenderer.ButtonNormalColor;
                    strokeColor = Focused ? WinCoreControlRenderer.ButtonFocusNormalStrokeColor : WinCoreControlRenderer.ButtonTextNormalColor;
                    break;

            }
        }
        protected ICore2DDrawingDocument GetVisibleButton(ICoreButtonDocument document)
        {
            ICore2DDrawingDocument v_doc = null;
            switch (this.State)
            {
                case enuButtonState.None:
                case enuButtonState.Normal:
                    v_doc = document.Normal;
                    break;
                case enuButtonState.Hover:
                    v_doc = document.Hover;
                    break;
                case enuButtonState.Down:
                    v_doc = this.ButtonDocument.Down;
                    break;
                case enuButtonState.Up:
                    v_doc = document.Up;
                    break;
                case enuButtonState.Disabled:
                    v_doc = document.Disabled;
                    break;
                default:
                    break;
            }
            return v_doc;
        }
        protected void _DrawImageButton(Graphics graphics,
            float x, float y, float width, float height)
        {
            _DrawImageButton(graphics,
                (int)x,
                (int)y,
                (int)width,
                (int)height);
        }
        protected void _DrawImageButton(ICoreGraphics graphics, 
            int x, int y, int width, int height)
        {
            Rectanglei rc = new Rectanglei(
                x + 4 + this.ButtonImageSize.Width / 2,
                y + (height  / 2), 
                0, 
                0);
            rc.Inflate(this.ButtonImageSize.Width / 2, this.ButtonImageSize.Height / 2);
            if (this.m_ButtonImageDocument != null){
                ICore2DDrawingDocument v_doc = GetVisibleButton(this.m_ButtonImageDocument);
               // v_doc.Draw(graphics, rc);
            }
        }
        protected void _DrawBorder(ICoreGraphics graphics, Rectangle rc, Colorf cl4, enuDashStyle style)
        {
            if (!DrawBorder)
                return;
            graphics.DrawRectangle(cl4, rc.X, rc.Y, rc.Width, rc.Height);            
        }
        #region ICoreButton Members
        private enuButtonState m_State;
        [System.ComponentModel.Browsable(false)]
        public enuButtonState State
        {
            get { return m_State; }
            set
            {
                if (m_State != value)
                {
                    m_State = value;
                    this.Invalidate();
                    OnStateChanged(EventArgs.Empty);
                }
            }
        }
        private void OnStateChanged(EventArgs eventArgs)
        {
            this.ButtonStateChanged?.Invoke(this, eventArgs);
        }
        private ICoreButtonDocument m_ButtonDocument;
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ICoreButtonDocument ButtonDocument
        {
            get { return m_ButtonDocument; }
            set
            {
                if (m_ButtonDocument != value)
                {
                    m_ButtonDocument = value;
                    OnButtonDocumentChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ButtonDocumentChanged;
        protected virtual void OnButtonDocumentChanged(EventArgs eventArgs)
        {
            this.ButtonDocumentChanged?.Invoke(this, eventArgs);
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }
        private ICoreButtonDocument m_ButtonImageDocument;
        /// <summary>
        /// get or set the button image document
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public ICoreButtonDocument ButtonImageDocument
        {
            get { return m_ButtonImageDocument; }
            set
            {
                if (m_ButtonImageDocument != value)
                {
                    m_ButtonImageDocument = value;
                }
            }
        }
        public event EventHandler ButtonStateChanged;
        #endregion
        #region ICoreIdentifier Members
        string ICoreIdentifier.Id
        {
            get { return this.Name; }
        }
        #endregion
        #region ICoreButtonControl Members
        enuDialogResult m_DialogResult;
        [DefaultValue(enuDialogResult.None )]
        public enuDialogResult DialogResult
        {
            get
            {
                return this.m_DialogResult;
            }
            set
            {
                this.m_DialogResult = value;
            }
        }
        public void NotifyDefault(bool value)
        {
        }
        /// <summary>
        /// raise the perform click event if this button is enabled;
        /// </summary>
        public void PerformClick()
        {
            if (this.Enabled)
            {
                OnClick(EventArgs.Empty);
            }
        }
        #endregion        
        #region ICoreCaptionItem Members
        public override void LoadDisplayText()
        {            
            this.Text = this.CaptionKey.R();
        }
        #endregion
        protected override bool ProcessMnemonic(char charCode)
        {
            if (CanSelect && IsMnemonic(charCode, this.Text))
            {
                this.PerformClick();
                return true;
            }
            bool v = base.ProcessMnemonic(charCode);
            return v;
        }
        protected override  void OnCaptionKeyChanged(EventArgs e)
        {
            base.OnCaptionKeyChanged(e);
        }
      
     
        
        DialogResult IButtonControl.DialogResult
        {
            get
            {
                return (DialogResult)this.DialogResult;
            }
            set
            {
                this.DialogResult = (enuDialogResult)value;
            }
        }
    }
}

