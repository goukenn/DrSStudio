

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIMP3Player.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.AVIApi.MCI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:MCIMP3Player.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.Players
{
    /// <summary>
    /// Represent a MCIPMP3Player
    /// </summary>
    public class MCIMP3Player : IDisposable 
    {
        const int SIZECONST = 155;
        private string m_identifier;
        private bool m_IsPlaying;
        private MCIWaveManager m_wavManager;
        public enuMP3PlayerMode PlayerMode
        {
            get {
                string v_o = MCI.MCIManager.GetStatus(this.m_identifier , "mode", 155);
                switch (v_o)
                {
                    case "stopped": return enuMP3PlayerMode.Stopped;
                    case "playing": return enuMP3PlayerMode.Playing;
                }
                return  enuMP3PlayerMode.Stopped; }
        }
        public int Volume {
            get {
                return int.Parse(MCI.MCIManager.GetStatus(this.m_identifier, "volume", SIZECONST));
            }
            set { 
            MCI.MCIManager.SendString(string.Format("setaudio {0} volume to {1}",
                this.m_identifier,
                value ));        
            }
        }
        public int LeftVolume
        {
            get
            {
                return int.Parse(MCI.MCIManager.GetStatus(this.m_identifier, "left volume", SIZECONST));
            }
            set
            {
                MCI.MCIManager.SendString(string.Format("setaudio {0} left volume to {1}",
                    this.m_identifier,
                    value));
            }
        }
        public int RightVolume
        {
            get
            {
                return int.Parse(MCI.MCIManager.GetStatus(this.m_identifier, "right volume", SIZECONST));
            }
            set
            {
                MCI.MCIManager.SendString(string.Format("setaudio {0} right volume to {1}",
                    this.m_identifier,
                    value));
            }
        }
        public void SetTreble(int value)
        {
            try
            {
                MCI.MCIManager.SendString(string.Format("setaudio {0} treble to {1}",
                    this.m_identifier,
                    value));
            }
            catch { 
            }
        }
        public void SetBass(int value)
        {
            try
            {
                MCI.MCIManager.SendString(string.Format("setaudio {0} bass to {1}",
                    this.m_identifier,
                    value));
            }
            catch { 
            }
        }
        /// <summary>
        /// set the balance
        /// </summary>
        /// <param name="value">value is -1000 and 1000</param>
        public void SetBalance(int value) 
        {            
                if (Math.Abs(value) <= 1000)
                {
                    if (value < 0)
                    {
                        LeftVolume = 1000;
                        RightVolume = 1000 + value;
                    }
                    else
                    {
                        RightVolume = 1000;
                        LeftVolume = 1000 - value;
                    }
                }            
        }
        public string TimeFormat
        {
            get
            {
                return MCI.MCIManager.GetStatus(this.m_identifier, "time format", SIZECONST);
            }
        }
        public bool IsReady { 
            get {
                return bool.Parse(MCI.MCIManager.GetStatus(this.m_identifier, "ready", SIZECONST));
            }
        }
        public long CurrentPosition
        {
            get
            {
                return long.Parse(MCIManager.GetStatus(this.m_identifier, "position", SIZECONST));
            }
        }
        public bool AudioOn {
            get { 
                return MCI.MCIManager.GetStatus(this.m_identifier, "audio", SIZECONST)=="on";
            }
        }
        /// <summary>
        /// get if this element is playing
        /// </summary>
        public bool IsPlaying
        {
            get { return m_IsPlaying; }
        }
        private MCIMP3Player()
        {
        }
        public void Play()
        {
            MCI.MCIManager.SendString(string.Format("play {0}",this.m_identifier ));
            this.m_IsPlaying = true;
        }
        public void Stop() {
            MCI.MCIManager.SendString(string.Format("stop {0}", this.m_identifier));
            this.m_IsPlaying = false;
        }
        public void Pause() {
            MCI.MCIManager.SendString(string.Format("pause {0}", this.m_identifier));
            this.m_IsPlaying = false;
        }
        public static MCIMP3Player Open(string fileName, string refName)
        {
            MCIMP3Player pm =null;
            try
            {
                //check alias
                MCIManager.SendString(string.Format("open \"{0}\" type mpegvideo alias {1}",
                    fileName, refName));
                pm = new MCIMP3Player();
                pm.m_identifier = refName;
                pm.m_wavManager = MCIWaveManager.Create(refName);
            }
            catch (Exception ex){
                //no open file
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
            return pm;
        }
        public void MuteAll( bool on)
        {
            MCI.MCIManager.SendString(string.Format("setaudio {0} mute all {1}",
                this.m_identifier,
                on?"on":"off"));
        }
        public void MuteLeft(bool on)
        {
            MCI.MCIManager.SendString(string.Format("setaudio {0} mute left {1}",
                this.m_identifier,
                on ? "on" : "off"));
        }
        public void MuteRight(bool on)
        {
            MCI.MCIManager.SendString(string.Format("setaudio {0} mute right {1}",
                this.m_identifier,
                on ? "on" : "off"));
        }
        public void  Dispose()
        {
            this.Stop();
        }
}
}

