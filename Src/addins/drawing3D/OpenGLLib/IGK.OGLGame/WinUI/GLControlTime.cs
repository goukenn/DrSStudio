

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLControlTime.cs
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
file:GLControlTime.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
namespace IGK.OGLGame.WinUI.GLControls
{
    public struct GLControlTime : IGLControlTime 
    {
        internal int m_time;
        internal int m_starttime;
        //internal Stopwatch m_watch;
        #region IGLControlTime Members
        /// <summary>
        /// get the time in ms
        /// </summary>
        public int Time
        {
            get { return m_time; }
        }
        #endregion
        public TimeSpan TimeSpan { get { return new TimeSpan(0, 0, 0, 0, m_time); } }
        public void Tick()
        {
            this.m_time = Environment.TickCount - this.m_starttime;
        }
        internal static GLControlTime NewTimeControl()
        {
            GLControlTime t = new GLControlTime();
            t.m_starttime = Environment.TickCount;
            t.m_time = 0;
            return t;
        }
    }
}

