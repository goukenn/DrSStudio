

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TrackEventArgs.cs
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
file:TrackEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.AudioBuilder
{
    public delegate void TrackEventHandler(object sender, TrackEventArgs e);
    public class TrackEventArgs : 
        EventArgs 
    {
        private IAudioTrack m_Tracks;
        /// <summary>
        /// get or set the tracks
        /// </summary>
        public IAudioTrack Tracks
        {
            get { return m_Tracks; }            
        }
        public TrackEventArgs(IAudioTrack track)
        {
            this.m_Tracks = track;
        }
    }
}

