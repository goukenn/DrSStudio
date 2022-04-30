

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoFileControl.cs
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
file:XVideoFileControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.WinUI
{
    public class XVideoFileControl: UserControl
    {
        public XVideoFileControl()
        {
            this.AutoScroll = true;
            this.m_Items = new VideoFileCollection(this);
            this.Dock = DockStyle.Fill;
            this.Paint += new PaintEventHandler(XVideoFileControl_Paint);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }
        void XVideoFileControl_Paint(object sender, PaintEventArgs e)
        {
            using (LinearGradientBrush lb = new LinearGradientBrush(
                this.ClientRectangle,
                Color.Red, Color.Orange, 90.0f))
            {
                e.Graphics.FillRectangle(lb, this.ClientRectangle);
            }
        }
        private VideoFileCollection m_Items;
        public VideoFileCollection Items
        {
            get { return m_Items; }
        }
        /// <summary>
        /// represent the video file collection
        /// </summary>
        public class VideoFileCollection
        {
            List<XVideoFileItem> m_list;
            XVideoFileControl m_owner;
            public VideoFileCollection(XVideoFileControl owner)
            {
                this.m_list = new List<XVideoFileItem>();
                this.m_owner = owner;
            }
            public int Count { get { return this.m_list.Count; } }
            public XVideoFileItem   Add(VideoFile videoFile)
            {
                if (videoFile == null)
                    return null;
                XVideoFileItem t  = new XVideoFileItem(this.m_owner, videoFile);
                this.m_list.Add(t);
                t.UpdateSize();
                return t;
            }
            public void Remove(XVideoFileItem item)
            {
                if (this.m_list.Contains(item))
                {
                    this.m_list.Remove(item);
                    item.Dispose();
                }
            }
            internal int IndexOf(XVideoFileItem xVideoFileItem)
            {
                return this.m_list.IndexOf(xVideoFileItem);
            }
            internal void Clear()
            {
                foreach (var item in this.m_list)
                {
                    this.m_owner.Controls.Remove(item);
                }
                this.m_list.Clear();
            }
        }
    }
}

