

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXPictureViewBrowserFileItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// 
    /// </summary>
    public class IGKXPictureViewBrowserFileItem : IGKXControl 
    {
        private string m_FileName;
        private ICore2DDrawingDocument m_document;

        
        public new IGKXPictureViewBrowser  Parent
        {
            get { return base.Parent as IGKXPictureViewBrowser; }
            set
            {
                base.Parent = value;
            }
        }

        public bool IsSelected
        {
            get { return this.Parent.IsSelected(this) ; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(32, 32);
            }
        }
        public void RenameTo(string filename)
        { 
        }
        internal IGKXPictureViewBrowserFileItem()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.Paint += IGKXPictureFileItem_Paint;
            this.BackColor = Color.Transparent;
        }
        [Browsable(false)]
        [DesignerSerializationVisibility( DesignerSerializationVisibility.Hidden )]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
        public IGKXPictureViewBrowser ViewBrowser
        {
            get {
                return Parent as IGKXPictureViewBrowser;
            }
        }
        public IGKXPictureViewBrowserFileItem(string fileName, ICore2DDrawingDocument document ) :this()
        {
            this.m_document = document;
            this.m_FileName = fileName;
        }
        

        void IGKXPictureFileItem_Paint(object sender, CorePaintEventArgs e)
        {
            Rectanglei rc = new Rectanglei(0, 0,
                this.Width,
                this.Height);
            //e.Graphics.Clear(Colorf.Transparent);
            e.Graphics.FillRectangle(
                WinCoreControlRenderer.PictureViewBrowserFileItemBackgroundColor,
                rc.X,
                rc.Y,
                rc.Width,
                rc.Height);
            e.Graphics.DrawRectangle(
                CoreRenderer.ForeColor,1, enuDashStyle.Dot ,
                rc.X,
                rc.Y,
                rc.Width-1,
                rc.Height-1);
            if (this.m_document != null)
            {
                Rectanglei v_docrc = rc;
                v_docrc.Inflate (-2,-2);
                this.m_document.Draw(e.Graphics, true , v_docrc, enuFlipMode.None  );

                if (this.IsSelected)
                {
                    e.Graphics.DrawRectangle(WinCoreControlRenderer.PictureViewBrowserBorderColor,
                        rc.X,
                        rc.Y,
                        rc.Width - 1,
                        rc.Height - 1);

                }
            }
        }


        public System.Windows.Forms.ControlStyles ControlStyle { get; set; }
    }
}
