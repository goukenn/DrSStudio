

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XVideoSoundTimeLine.cs
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
file:XVideoSoundTimeLine.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.VideoEditionTableAddIn.WinUI
{
    using IGK.ICore;using IGK.AudioVideo.ACM;
    using IGK.AudioVideo.AVI;
    /// <summary>
    /// represent a sound video time line
    /// </summary>
    class XVideoSoundTimeLine : XVideoTimeLineItemBase 
    {
        public class SoundCollections : System.Collections.IEnumerable
        {
            List<IVideoSoundItem> m_sounds;
            XVideoSoundTimeLine m_owner;
            public int Count { get { return this.m_sounds.Count; } }
            public SoundCollections(XVideoSoundTimeLine owner)
            {
                this.m_sounds = new List<IVideoSoundItem>();
                this.m_owner = owner;
            }
            internal void Add(IVideoSoundItem item)
            { this.m_sounds.Add(item); }
            internal void Remove(IVideoSoundItem item) {
                this.m_sounds.Remove(item);
            }
            internal void Clear() {
                this.m_sounds.Clear();
            }
            public IVideoSoundItem[] ToArray() { return this.m_sounds.ToArray(); }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_sounds.GetEnumerator();
            }
            #endregion
        }
        SoundCollections m_sounds;
        /// <summary>
        /// get the sounds ite this track
        /// </summary>
        public SoundCollections Sounds { get { return this.m_sounds; } }
        public XVideoSoundTimeLine()
        {
            this.Height = 32;
            this.AllowDrop = true;
            this.m_sounds = new SoundCollections(this);
        }
        protected override void RenderBorder(System.Windows.Forms.PaintEventArgs e)
        {
            base.RenderBorder(e);
        }
        protected override void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);
        }
        protected override void RenderItem(System.Windows.Forms.PaintEventArgs e)
        {
            base.RenderItem(e);
            using (System.Drawing.Pen p = new System.Drawing.Pen (Colorf.White ))
            {
                p.Width =1.0F;
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash ;
                float y = this.Height - 16;
                e.Graphics.DrawLine(p, 0, y, this.Width, y);
            }
        }
    }
}

