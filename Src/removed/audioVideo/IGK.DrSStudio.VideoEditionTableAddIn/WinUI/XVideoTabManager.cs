

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoTabManager.cs
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
file:XVideoTabManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    public class XVideoTabManager : System.Windows.Forms.Panel 
    {
        IGKXButton c_PrevButton;
        IGKXButton c_PlayButton;
        IGKXButton c_ForwardButton;
        private XVideoPlayer  m_Player;
        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams p =  base.CreateParams;
                if (!this.DesignMode)
                {
                    p.Style |= 0x20;
                }
                return p;
            }
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
            e.Graphics.FillRectangle (
                CoreBrushRegister .GetBrush (Colorf .FromFloat (0.4f, Colorf.Black )),
                this.ClientRectangle );
        }
        public XVideoPlayer  Player
        {
            get { return m_Player; }
            set
            {
                if (m_Player != value)
                {
                    m_Player = value;
                }
            }
        }
        public XVideoTabManager()
        {
            this.InitializeComponent();
            this.SetStyle(System.Windows.Forms.ControlStyles.FixedHeight, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint , true);
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint , true);
            c_PlayButton = new IGKXButton();
            c_PrevButton = new IGKXButton();
            c_ForwardButton = new IGKXButton();
            c_PrevButton.ButtonDocument = CoreButtonDocument.Create(CoreResourcesCollections.GetAllDocument (Properties.Resources.previousButton_42x42 ));
            c_PlayButton.ButtonDocument = CoreButtonDocument.Create(CoreResourcesCollections.GetAllDocument(Properties.Resources.PlayButton_42x42));
            c_ForwardButton.ButtonDocument = CoreButtonDocument.Create(CoreResourcesCollections.GetAllDocument(Properties.Resources.forwardButton_42x42));
            this.Height = 48;
            this.BackColor = System.Drawing.Color.FromArgb(128, Colorf.Black);
            c_PrevButton.Size = new System.Drawing.Size(32, 32);
            c_ForwardButton.Size = new System.Drawing.Size(32, 32);
            c_PlayButton.Size = new System.Drawing.Size(42, 42);
            this.c_PlayButton.Click += new EventHandler(c_PlayButton_Click);
            this.Controls.Add(c_PrevButton );
            this.Controls.Add(c_PlayButton);
            this.Controls.Add(c_ForwardButton);
            SetupButtonBound();
            this.SizeChanged += new EventHandler(XVideoTabManager_SizeChanged);
        }
        void XVideoTabManager_SizeChanged(object sender, EventArgs e)
        {
            SetupButtonBound();
        }
        private void SetupButtonBound()
        {
            int v_midH = this.Height / 2;
            Vector2i p = new Vector2i (10, 0);
            this.c_PrevButton.Location = new System.Drawing.Point(p.X, v_midH - this.c_PrevButton.Height / 2);
            this.c_PlayButton.Location = new System.Drawing.Point(p.X + this.c_PrevButton.Right, v_midH - this.c_PlayButton.Height / 2);
            this.c_ForwardButton .Location = new System.Drawing.Point(p.X + this.c_PlayButton.Right, v_midH - this.c_ForwardButton.Height / 2);
        }
        void c_PlayButton_Click(object sender, EventArgs e)
        {
            this.Player.Play();    
        }
        private void InitializeComponent()
        {
        } 
    }
}

