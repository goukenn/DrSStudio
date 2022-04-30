

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoTimeLineItemBase.cs
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
file:XVideoTimeLineItemBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    /// <summary>
    /// represnt a video time line item
    /// </summary>
    public class XVideoTimeLineItemBase : XControl
    {
        private XVideoTimeLineManager m_VideoTabManager;
        /// <summary>
        /// get the tab manager
        /// </summary>
        public XVideoTimeLineManager VideoTabManager
        {
            get { return m_VideoTabManager; }
            internal set
            {
                if (m_VideoTabManager != value)
                {
                    m_VideoTabManager = value;
                }
            }
        }
        protected virtual string ImageKey { get { return null; } }
        public XVideoTimeLineItemBase()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.FixedHeight, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.Paint += new PaintEventHandler(XVideoTimeLineItem_Paint);
        }
        void XVideoTimeLineItem_Paint(object sender, PaintEventArgs e)
        {
            RenderBackground(e);
            RenderItem(e);
            RenderBorder(e);
        }
        protected virtual void RenderBackground(PaintEventArgs e)
        {
            e.Graphics.Clear(XVideoRenderer .XVideoItemBackground );
        }
        protected virtual void RenderBorder(PaintEventArgs e)
        {
            System.Windows.Forms.ControlPaint.DrawBorder(e.Graphics,
                this.ClientRectangle,
                XVideoRenderer.XVideoItemBorderColor ,
                ButtonBorderStyle.Solid);
        }
        protected virtual void RenderItem(PaintEventArgs e)
        {
        }
    }
}

