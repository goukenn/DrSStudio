

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoTimeLine.cs
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
file:XVideoTimeLine.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    /// <summary>
    /// represent a video items time line
    /// </summary>
    class XVideoTimeLine : XVideoTimeLineItemBase
    {
        /// <summary>
        /// represent a video items collection
        /// </summary>
        public class VideoItemCollections : System.Collections.IEnumerable {
            List<IVideoItem> m_items;
            XVideoTimeLine m_ower;
            public VideoItemCollections(XVideoTimeLine owner)
            {
                this.m_items = new List<IVideoItem>();
                this.m_ower = owner;
            }
            internal void Add(IVideoItem item)
            {
                this.m_items.Add(item);
            }
            internal void Remove(IVideoItem item) { this.m_items.Remove(item); }
            internal void Clear() { this.m_items.Clear(); }
            public int Count { get { return this.m_items.Count; } }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
            #endregion
        }
        VideoItemCollections m_items;
        public VideoItemCollections Videos { get { return this.m_items; } }
        public XVideoTimeLine()
        {
            this.Height = 64;
            m_items = new VideoItemCollections(this);
        }
        protected override void RenderBackground(System.Windows.Forms.PaintEventArgs e)
        {
            base.RenderBackground(e);
        }
        protected override void RenderItem(System.Windows.Forms.PaintEventArgs e)
        {
            base.RenderItem(e);
            //data a picture avery single step
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Rectanglef rc = new Rectanglef (0, 0, this.Width, 8);
            System.Drawing.Brush br = CoreBrushRegister.GetBrush(Colorf.Black);
            e.Graphics.FillRectangle(br,
                rc);
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap (8,8);
            Graphics g = Graphics.FromImage (bmp );
            g.FillRectangle (CoreBrushRegister .GetBrush (Colorf.White), new System.Drawing.Rectangle (4,4, 4,4));
            g.Flush ();
            using (System.Drawing.TextureBrush tb = new System.Drawing.TextureBrush (bmp))
            {
                e.Graphics.FillRectangle (tb, rc);
            }
            rc = new Rectanglef (0, this.Height - 8, this.Width, this.Height - 8);
            e.Graphics.FillRectangle(br,
                rc);
            using (System.Drawing.TextureBrush tb = new System.Drawing.TextureBrush (bmp))
            {
                e.Graphics.FillRectangle (tb, rc);
            }
            g.Dispose ();
            bmp.Dispose ();
        }
    }
}

