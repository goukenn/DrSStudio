using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.AudioVideo
{
    public class AudioVideoProgressInfo
    {
        private int m_Frame;
        private int m_TotalFrame;
        private TimeSpan m_TimeSpan;
        private TimeSpan  m_TotalTimeSpan;

        public TimeSpan  TotalTimeSpan
        {
            get { return m_TotalTimeSpan; }
        
        }
      

        public TimeSpan TimeSpan
        {
            get { return m_TimeSpan; }
        }
        public int TotalFrame
        {
            get { return m_TotalFrame; }
        }
        public int Frame
        {
            get { return m_Frame; }
        }
        public AudioVideoProgressInfo(int totalFrame, int frame, TimeSpan TotalSpan, TimeSpan currentSpan)
        {
            this.m_Frame = frame;
            this.m_TotalFrame = totalFrame;
            this.m_TotalTimeSpan = TotalSpan;
            this.m_TimeSpan = currentSpan;
        }
    }
}
