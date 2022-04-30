

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XAudioVideoPlayerTool.cs
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
file:XAudioVideoPlayerTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    public class XAudioVideoPlayerTool  : IGK.DrSStudio .WinUI.XToolStripCoreToolHost 
    {
        private ITrack m_Track;
        public ITrack Track
        {
            get { return m_Track; }
            set
            {
                if (m_Track != value)
                {
                    m_Track = value;
                    this.Enable((value != null));
                }
            }
        }
        public XAudioVideoPlayerTool():base(Tools.AudioVideoPlayerTool.Instance )
        {
            InitializeComponent();
            InitControl();
            this.Enable(false);
        }
        XToolStripButton c_previous;
        XToolStripButton c_play;
        XToolStripButton c_stop;
        XToolStripButton c_next;
        private void InitControl()
        {
            c_next = new XToolStripButton();
            c_play = new XToolStripButton();
            c_previous = new XToolStripButton();
            c_stop = new XToolStripButton();
            c_stop.Click += new EventHandler(c_stop_Click);
            c_stop.ImageDocument = CoreResources.GetDocument("anim_Stop");
            c_previous.ImageDocument = CoreResources.GetDocument("anim_prev");
            c_previous.Click += new EventHandler(c_previous_Click);
            c_play.ImageDocument = CoreResources.GetDocument("anim_playDoc");
            c_play.Click += new EventHandler(c_play_Click);
            c_next.ImageDocument = CoreResources.GetDocument("anim_next");
            c_next.Click += new EventHandler(c_next_Click);
            this.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                c_previous ,
                c_play ,
                c_stop ,
                c_next 
            });
            this.AddRemoveButton(null);
            this.LoadDisplayText();
        }
        void c_next_Click(object sender, EventArgs e)
        {
            this.Track.Next();
        }
        void c_play_Click(object sender, EventArgs e)
        {
            this.Track.Play();
        }
        void c_previous_Click(object sender, EventArgs e)
        {
            this.Track.Previous();
        }
        void c_stop_Click(object sender, EventArgs e)
        {
            this.Track.Stop(); 
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }
        private void InitializeComponent()
        {
        }
    }
}

