

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XTrackExplorer.cs
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
file:XTrackExplorer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    public  class XTrackExplorer : XUserControl
    {
        public XTrackExplorer()
        {
            this.InitializeComponent();
            this.AutoScroll = true;
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            XAudioRenderer.FillRectangle(e.Graphics , this.ClientRectangle,
                XAudioRenderer.AudioExplorerStartColor,
                XAudioRenderer.AudioExplorerEndColor,
                90);
            base.OnPaint(e);
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XTrackExplorer
            // 
            this.MaximumSize = new System.Drawing.Size(0, 256);
            this.MinimumSize = new System.Drawing.Size(0, 64);
            this.Name = "XTrackExplorer";
            this.Size = new System.Drawing.Size(291, 92);
            this.ResumeLayout(false);
        }
    }
}

