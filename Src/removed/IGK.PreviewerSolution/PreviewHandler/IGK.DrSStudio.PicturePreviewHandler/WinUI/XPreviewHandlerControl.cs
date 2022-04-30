

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XPreviewHandlerControl.cs
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
file:XPreviewHandlerControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.PicturePreviewHandler.WinUI
{
    using IGK.ICore;using IGK.PrevHandlerLib;
    class XPreviewHandlerControl : FilePreviewHandlerControlBase
    {
        public XPreviewHandlerControl()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XPreviewHandlerControl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.ClientSize = new System.Drawing.Size(682, 460);
            this.Name = "XPreviewHandlerControl";
            this.ResumeLayout(false);
        }
        public override void Load(System.IO.FileInfo file)
        {
            Label lb = new Label();
            this.BackColor = Color.Yellow ;
            lb.Text = file.FullName;
            lb.AutoSize = true;
            this.Controls.Add(lb);
        }
    }
}

