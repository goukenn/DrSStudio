

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXEditBrush.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GCSXEditBrush.cs
*/
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;


namespace IGK.DrSStudio.WinUI
{
    public sealed class GCSXEditBrush : IGKXUserControl 
    {
        private ICoreBrush m_brush;
        private enuBrushSupport m_support;
        public GCSXEditBrush()
        {
            this.InitializeComponent();
        }
        public GCSXEditBrush(ICoreBrush brush, enuBrushSupport support):base()
        {
            this.InitializeComponent();
            this.m_brush = brush;
            this.m_support = support;
            this.BrushMode = (brush is ICorePen) ? enuBrushMode.Stroke : enuBrushMode.Fill;
            this.BrushSupport = support;
            //this.ColorChanged += UIXEditBrush_ColorChanged;
            //this.BrushTypeChanged += UIXEditBrush_BrushTypeChanged;
            //this.BrushChanged += delegate(object o , EventArgs e) {
            //    updateBrushInfo();                
            //};
            ////this.PreferredSize = new System.Drawing.Size(300, 600);
            //this.Size = this.DefaultSize;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // GCSXEditBrush
            // 
            this.Name = "GCSXEditBrush";
            this.Size = new System.Drawing.Size(332, 338);
            this.ResumeLayout(false);

        }
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(350, 600);
            }
        }
        protected override System.Drawing.Size SizeFromClientSize(System.Drawing.Size clientSize)
        {
            return base.SizeFromClientSize(clientSize);
        }
   
        //public override System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
        //{
        //    //return base.GetPreferredSize(proposedSize);
        //    return new System.Drawing.Size(300, 800);
        //}
        //void UIXEditBrush_ColorChanged(object sender, EventArgs e)
        //{
        //    if (this.BrushType == enuBrushType.Solid)
        //    {
        //        this.Brush.SetSolidColor (this.Color);
        //    }
        //    else
        //    {
        //        if (this.BrushSelector != null)
        //            this.BrushSelector.SetColor(this.Color);
        //    }
        //}
        //void UIXEditBrush_BrushTypeChanged(object sender, EventArgs e)
        //{
        //    if (this.BrushSelector != null)
        //    {
        //        //set up the color
        //        this.BrushSelector.SetColor(this.Color);
        //    }
        //}

        public enuBrushMode BrushMode { get; set; }

        public enuBrushSupport BrushSupport { get; set; }
    }
}

