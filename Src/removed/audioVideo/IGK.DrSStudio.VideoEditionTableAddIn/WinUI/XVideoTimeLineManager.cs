

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoTimeLineManager.cs
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
file:XVideoTimeLineManager.cs
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
    /// time line manager
    /// </summary>
    public class XVideoTimeLineManager : IGK.DrSStudio.WinUI.XPanel 
    {
        private XVideoEditorSurface m_Surface;
        private VideoTimeLineItemCollections m_TimesLineManager;
        private VideoTimeLineManagerCursor m_Cursor;
        private VideoTimeLineCursorCollections m_TimeLineCursorCollections;
        public IVideoTimeLineCursorCollections TimeLineCursors
        {
            get { return m_TimeLineCursorCollections; }
        }
        public XVideoTimeLineManager()
        {
            this.m_TimesLineManager = new VideoTimeLineItemCollections(this);
            this.m_TimeLineCursorCollections = new VideoTimeLineCursorCollections(this);
            this.m_Cursor = new VideoTimeLineManagerCursor();
            this.Controls.Add(this.m_Cursor);
            this.AutoScroll = true;
        }
        /// <summary>
        /// get the time line collections
        /// </summary>
        public System.Collections .IEnumerable TimesLineManager
        {
            get { return m_TimesLineManager; }
        }
        public XVideoEditorSurface Surface
        {
            get { return m_Surface; }
            set
            {
                if (m_Surface != value)
                {
                    m_Surface = value;
                }
            }
        }
        /// <summary>
        /// represent a base control cursor
        /// </summary>
        public sealed class VideoTimeLineManagerCursor : XControl
        {
           private XVideoTimeLineManager m_VideoTabManager;
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
            public VideoTimeLineManagerCursor()
            {
                this.Width = 2;
                this.Cursor = Cursors.VSplit;
                this.BackColor = Color.Black;
            }
        }
        class VideoTimeLineItemCollections : System.Collections.IEnumerable
        {
            XVideoTimeLineManager m_timeLineManager;
            List<XVideoTimeLineItemBase> m_list; 
            public VideoTimeLineItemCollections(XVideoTimeLineManager owner)
            {
                this.m_timeLineManager = owner;
                this.m_list = new List<XVideoTimeLineItemBase>();
                this.m_timeLineManager.SizeChanged += new EventHandler(m_timeLineManager_SizeChanged);
                this.m_timeLineManager.ControlAdded += new ControlEventHandler(m_timeLineManager_ControlAdded);
                this.m_timeLineManager.ControlRemoved += new ControlEventHandler(m_timeLineManager_ControlRemoved);
            }
            void m_timeLineManager_SizeChanged(object sender, EventArgs e)
            {
                Rectangle rc = Rectangle.Empty;
                int v_y = 0;
                int v_w = this.m_timeLineManager .Width ;
                foreach (var item in m_list)
                {
                    item.Bounds = new Rectangle(0, v_y, v_w, item.Height);
                    v_y += item.Height;
                }
            }
            void m_timeLineManager_ControlRemoved(object sender, ControlEventArgs e)
            {
                if (e.Control is XVideoTimeLineItemBase)
                {
                    this.Remove( e.Control  as XVideoTimeLineItemBase);
                }
            }
            private void Remove(XVideoTimeLineItemBase item)
            {
                if (this.m_list.Contains(item))
                {
                    this.m_list.Remove(item);
                    item.VideoTabManager = null;
                }
            }
            void m_timeLineManager_ControlAdded(object sender, ControlEventArgs e)
            {
                if (e.Control is XVideoTimeLineItemBase)
                {
                    this.Add(e.Control  as XVideoTimeLineItemBase);
                }
            }
            public void Add(XVideoTimeLineItemBase item)
            {
                if ((item == null) || (this.m_list.Contains (item)))
                    return ;
                this.m_list.Add(item);
                item.VideoTabManager = this.m_timeLineManager;
            }
            public override string ToString()
            {
                return string.Format("[{0}]", this.Count);
            }
            public int Count
            {
                get { return this.m_list .Count; }
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            #endregion
        }
        class VideoTimeLineCursorCollections : IVideoTimeLineCursorCollections
        {//maintain cusor on top most
            XVideoTimeLineManager m_timeLineManager;
            List<VideoTimeLineManagerCursor> m_list;
            public VideoTimeLineCursorCollections(XVideoTimeLineManager owner)
            {
                this.m_timeLineManager = owner;
                this.m_list = new List<VideoTimeLineManagerCursor>();
                this.m_timeLineManager.SizeChanged += new EventHandler(m_timeLineManager_SizeChanged);
                this.m_timeLineManager.ControlAdded += new ControlEventHandler(m_timeLineManager_ControlAdded);
                this.m_timeLineManager.ControlRemoved += new ControlEventHandler(m_timeLineManager_ControlRemoved);
            }
            void UpdateTopMostCursor()
            {
                foreach (var item in this.m_list )
                {
                    this.m_timeLineManager.Controls.SetChildIndex(item,
                        this.m_timeLineManager.Controls.Count);
                }
            }
            void m_timeLineManager_SizeChanged(object sender, EventArgs e)
            {
                foreach (var item in this.m_list)
                {
                    item.Height = this.m_timeLineManager.Height;
                }
            }
            void m_timeLineManager_ControlRemoved(object sender, ControlEventArgs e)
            {
                if (e.Control is VideoTimeLineManagerCursor)
                {
                    this.Remove(e.Control as VideoTimeLineManagerCursor);
                }
                UpdateTopMostCursor();
            }
            private void Remove(VideoTimeLineManagerCursor item)
            {
                if (this.m_list.Contains(item))
                {
                    this.m_list.Remove(item);
                    item.VideoTabManager = null;
                }
                UpdateTopMostCursor();
            }
            void m_timeLineManager_ControlAdded(object sender, ControlEventArgs e)
            {
                if (e.Control is VideoTimeLineManagerCursor)
                {
                    this.Add(e.Control as VideoTimeLineManagerCursor);
                }
            }
            public void Add(VideoTimeLineManagerCursor item)
            {
                if ((item == null) || (this.m_list.Contains(item)))
                    return;
                this.m_list.Add(item);
                item.VideoTabManager = this.m_timeLineManager;
            }
            public override string ToString()
            {
                return string.Format("Cursor [{0}]", this.Count);
            }
            public int Count
            {
                get { return this.m_list.Count; }
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator();
            }
            #endregion
        }
    }
}

