

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVITimeInfo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AVITimeInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.AVI
{
    /// <summary>
    /// represent a avi time item
    /// </summary>
    public struct AVITimeInfo
    {
        TimeSpan m_span;
        public TimeSpan TimeSpan { get { return m_span; } }
        public int Seconds { get { return m_span.Seconds; } }
        public int Minutes { get { return m_span.Minutes; } }
        public int Hours { get { return this.m_span.Hours; } }
        public static AVITimeInfo GetTime(long nSample)
        {
            return new AVITimeInfo() { 
                m_span  = new TimeSpan (0,0,0,0,(int)nSample )
            };
        }
        public override string ToString()
        {
            return TimeSpan.ToString();
        }
    }
}

