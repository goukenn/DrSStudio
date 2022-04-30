using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.AudioVideo
{
    public struct AVISequenceUpdateInfo : IAVISequenceUpdateInfo
    {
        private int m_Frame;
        private int m_TotalFrame;
        private TimeSpan  m_TimeSpan;
        private TimeSpan m_TotalTimeSpan;

        public AVISequenceUpdateInfo(int totalFrame, TimeSpan totalSpan)
        {
            m_Frame = 0;
            m_TotalFrame = totalFrame;
            m_TimeSpan = TimeSpan.FromSeconds(0);
            m_TotalTimeSpan = totalSpan;
        }
        public TimeSpan TotalTimeSpan
        {
            get { return m_TotalTimeSpan; }
        }
        public TimeSpan  TimeSpan
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
        public void Update(int frame, TimeSpan span, TimeSpan totalTimeSpan)
        {
            this.m_Frame = frame;
            this.m_TimeSpan = span;
            this.m_TotalTimeSpan = totalTimeSpan;
        }
    }
}
