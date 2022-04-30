

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLGameTime.cs
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
file:GLGameTime.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame
{
    /// <summary>
    /// represent the open GL Game Time
    /// </summary>
    public class GLGameTime
    {
        private long m_applicationStartTick; //game start frame tick
        private long m_oldTick;   //old tick
        private long m_nTick;     //new tick
        //diagnostics stop watch
        private System.Diagnostics.Stopwatch m_stopwatch;
        private TimeSpan m_startTime;
        private TimeSpan m_elapsed;
        private TimeSpan m_totalGameTime;

        public static GLGameTime Start()
        {
            GLGameTime t = new GLGameTime();
            t.Tick();
            return t;
        }

        private long m_gameStartTick; // game start tick
        private long m_ellapseTick;   // ellapse tick
        private long m_frameTick;     // frame tick
        private long m_frameCount; //count the number of frame update on each call of update method
        private float m_fps;        
        private float m_Frequency;
        /// <summary>
        /// get the ellapse time from game start up
        /// </summary>
        public TimeSpan ElapsedTime {
            get{
                return this.m_elapsed;// this.m_stopwatch.Elapsed;
                //return new TimeSpan ( (m_nTick - m_gameStartTick)*TimeSpan.TicksPerMillisecond);
            }
        }
        public TimeSpan ElapsedGameTime
        {
            get {
                return new TimeSpan(m_nTick);
            }
        }
        /// <summary>
        /// Frame per second
        /// </summary>
        public float FPS {
            get {
                return m_fps;
            }
        }
        /// <summary>
        /// frequency
        /// </summary>
        public float Frequency {
            get {
                return this.m_Frequency;
            }
        }
        public TimeSpan TotalGameTime {
            get {
                return this.m_totalGameTime;// TimeSpan.FromTicks((m_nTick - m_applicationStartTick) * TimeSpan.TicksPerMillisecond);
            }
        }
        public TimeSpan TotalRealGameTime
        {
            get
            {
                return this.TotalGameTime - this.m_startTime;// this.m_stopwatch.Elapsed;// TimeSpan.FromTicks((m_nTick - m_applicationStartTick) * TimeSpan.TicksPerMillisecond);
            }
        }
        internal GLGameTime()
        { 
            this.m_Frequency = (TimeSpan.TicksPerSecond * 1000 / 60.0f) ;
            this.m_stopwatch = new System.Diagnostics.Stopwatch();            
        }
        public void Init()
        {
            long v_tick = Environment.TickCount;
            this.m_applicationStartTick = v_tick;
            this.m_gameStartTick = v_tick;
            this.m_nTick = v_tick;
            this.m_oldTick = v_tick;
            this.m_frameTick = 0;
            this.m_startTime = new TimeSpan(v_tick);
            this.m_stopwatch.Start();
        }
        public void Tick()
        {
            //get current tick
            long v_tick = Environment.TickCount;
            long v_d = v_tick - this.m_oldTick;
            TimeSpan e = this.m_stopwatch.Elapsed;
            this.m_ellapseTick += v_d;
            this.m_frameTick += (v_tick - m_oldTick);
            if (v_d >= 1000 )
            {
                //this.m_fps = 1 / ((float)(new TimeSpan(0, 0, 0, 0, (int)v_d)).TotalSeconds);
                this.m_nTick = v_tick;
                this.m_oldTick = v_tick;
                this.m_fps = (this.m_frameCount);// * 1000.0f) / 60.0f;//(this.m_frameCount);// / 1000.0f) * 60.0f;
                //this.m_fps = (float)(v_d)*1000;
                //float d = v_tick - m_oldTick;
                //if (d != 0)
                //    this.m_fps = (d * 1000.0f);
                //else
                //    this.m_fps = 1;
                this.m_frameTick = 0;
                this.m_frameCount = 0;
            }
            else
            {
                this.m_frameCount++;
            }
            this.m_elapsed = e;// this.TotalGameTime - this.m_elapsed;
            this.m_totalGameTime = e;
        }
        public void Reset()
        {
            this.m_stopwatch.Reset();
        }
        public void Pause()
        {
            this.m_stopwatch.Stop();            
        }
    }
}

