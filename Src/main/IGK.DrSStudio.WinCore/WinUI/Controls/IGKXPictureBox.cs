

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXPictureBox.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;

    public class IGKXPictureBox : IGKXControl
    {
        private ICoreBitmap m_Image;

        public ICoreBitmap Image
        {
            get { return m_Image; }
            set
            {
                if (m_Image != value)
                {
                    m_Image = value;
                    this.Invalidate();
                    OnImageChanged(EventArgs.Empty);
                }
            }
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(200, 200);
            }
        }
        protected override System.Windows.Forms.Padding DefaultMargin
        {
            get
            {
                return  System.Windows.Forms.Padding.Empty;
            }
        }
        protected override System.Windows.Forms.Padding DefaultPadding
        {
            get
            {
                return System.Windows.Forms.Padding.Empty;
            }
        }
        public event EventHandler ImageChanged;
        ///<summary>
        ///raise the ImageChanged 
        ///</summary>
        protected virtual void OnImageChanged(EventArgs e)
        {
            if (ImageChanged != null)
                ImageChanged(this, e);
        }

        public IGKXPictureBox()
        {
            this.Paint += _Paint;
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            this.RenderPixtureBox(e);
        }

        protected void RenderPixtureBox(CorePaintEventArgs e)
        {
            
            e.Graphics.FillRectangle(Colorf.WhiteSmoke, 0, 0, this.Width - 1, this.Height - 1);
            if (this.m_Image != null)
            {
                this.Image.Draw(e.Graphics, new Rectanglei( 0, 0, this.Width - 1, this.Height - 1), true , enuFlipMode.None );
            }
            e.Graphics.DrawRectangle(Colorf.Black, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}
