

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveManager.cs
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
file:MCIWaveManager.cs
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
namespace IGK.AVIApi.MCI
{
    /// <summary>
    /// represent the wav manager
    /// </summary>
    public sealed class MCIWaveManager
    {
        private string m_identifier;
        const int DEVICETYPE_BUFFER_SIZE = 15;
        const int STATUS_BUFFER_SIZE  = 255;
        /// <summary>
        /// private constructor
        /// </summary>
        private MCIWaveManager()
        { 
        }
        public bool CanEject { get { return MCIManager.Capabilityb("can eject", m_identifier ); } }
        public bool CanPlay { get { return MCIManager.Capabilityb("can play", m_identifier ); } }
        public bool CanRecord { get { return MCIManager.Capabilityb("can record", m_identifier); } }
        public bool CanSave { get { return MCIManager.Capabilityb("can record", m_identifier); } }
        public bool HasAudio { get { return MCIManager.Capabilityb("has audio", m_identifier); } }
        public bool HasVideo{ get { return MCIManager.Capabilityb("has video", m_identifier); } }
        public bool CompoundDevice { get { return MCIManager.Capabilityb("compound device", m_identifier); } }
        public int Inputs  { get { return MCIManager.Capabilityi("inputs", m_identifier); }
            set { setStatus("input", value.ToString()); }
        }
        public int Outputs { get { return MCIManager.Capabilityi("outputs", m_identifier); }
            set { setStatus("outputs", value.ToString()); }
        }
        public bool UseFiles { get { return MCIManager.Capabilityb("uses files", m_identifier); } }
        public string DeviceType { get { return MCIManager.GetString (string.Format ("capability {0} device type", this.m_identifier ), 
            DEVICETYPE_BUFFER_SIZE ); } }
        public string Unsaved { get { return getInfo("unsaved"); } }
        public string FileInfo { get { return getInfo("file"); } }
        public string InputInfo { get { return getInfo("input"); } }
        public string OutputInfo { get { return getInfo("output"); } }
        public string ProducInfo { get { return getInfo("file"); } }
        public int Alignment { get { return int.Parse (getStatus("alignment")); }
            set{ setStatus ("alignment",value.ToString());}
        }
public int BitperSample { get{  return   int.Parse (getStatus("bitspersample"));}
       set{ setStatus ("bitspersample",value.ToString());}
}
public int BitperSec { get{ return int.Parse ( getStatus("bytespersec"));}
    set{ setStatus ("bytespersec",value.ToString());}
}
public int Channels { get{ return  int.Parse (getStatus("channels"));}
    set{ setStatus ("channels",value.ToString());}
}
public string CurrentTrack { get{ return  getStatus("current track");}}
public string FormatTag { get{ return  getStatus("format tag");}}
        /// <summary>
        /// get or set the input channel in used
        /// </summary>
public string Input { get{ 
    return  getStatus("input");}
}
public string Length{ get{ return  getStatus("length");}}
public string Level { get{ return  getStatus("level");}}
public string MediaPresent{ get { return getStatus("media present"); } }
public string Mode{ get{ return  getStatus("mode");}}
public string NumberOfTrack { get{ return  getStatus("number of tracks");}}
        /// <summary>
        /// get or set the out channel in used
        /// </summary>
public string Output { get{ return  getStatus("output");}    
}
public string Position { get{ return  getStatus("position");}}
public string Ready { get{ return  getStatus("ready");}}
        /// <summary>
        /// get or set the samplespersec in used
        /// </summary>
public string SamplePerSec { get{ return  getStatus("samplespersec");}
        set{ setStatus ("samplespersec",value.ToString());}
}
public string StartPosition{ get{ return  getStatus("start position");}}
public string TimerFormat { get{ return  getStatus("time format");}}
public void SetAnyInput(){ setStatus("any input","");}
public void SetAnyOuput(){ setStatus("any output","");}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">on or off</param>
public void SetAllAudio(string command){ setStatus("audio all ",command );}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">on or off</param>
public void SetLeftAudio(string command){ setStatus("audio left ", command);}
public void SetRightAudio(string command){ setStatus("audio right", command);}
        /// <summary>
        /// set door driver
        /// </summary>
        /// <param name="command">open or closed</param>
        public void SetDoor(string command){ setStatus("door", command );}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commmand">pcm, avi,avss,dib,jfif,jpeg,mpeg,rdib,rjpeg or tag integer</param>
        public void SetSetFormatTag(string commmand){setStatus("format tag", commmand );}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commmand">bytes,milliseconds, samples</param>     
        public void SetTimeFormat(string command) {setStatus ("time format",command );}
public int GetPositionTrackNumber(int number)
{
    return int.Parse (getStatus(string.Format ("position track {0}", number )));
}
public int GetLengthTrackNumber(int number)
{
    return int.Parse(getStatus(string.Format("length track {0}", number)));
}
    private string getStatus(string command)
        {
            return MCIManager.GetString (string.Format ("status {0} {1}",
                this.m_identifier ,
                command ),
                STATUS_BUFFER_SIZE );
        }
    private string getInfo(string command)
    {
        return MCIManager.GetString(string.Format("info {0} {1}",
            this.m_identifier,
            command),
            STATUS_BUFFER_SIZE);
    }
        private void setStatus(string command, string value)
        {
             MCIManager.SendString(string.Format ("set {0} {1} {2}",
                this.m_identifier ,
                command,
                value ));
        }
        public void SeekToEnd()
        {
            MCIManager.SendString(string.Format("seek {0} to end",
                this.m_identifier));
        }
        public void SeekToStart()
        {
            MCIManager.SendString(string.Format("seek {0} to start",
                this.m_identifier));
        }
        public void SeekTo(string command)
        {
            MCIManager.SendString(string.Format("seek {0} to {1}",
                this.m_identifier,
                command ));        
        }
        public void Save(string filename)
        {
            MCIManager.SendString(string.Format("save {0} {1}",
                this.m_identifier,
                filename ));
        }
        public void Resume()
        {
            MCIManager.SendString(string.Format("resume {0}",
                this.m_identifier
                ));
        }
        public void Record(bool overwrite)
        {
            MCIManager.SendString(string.Format("record {0}{1}",
                this.m_identifier,
                overwrite ? " overwrite":string.Empty 
                ));
        }
        public void Record(string from, bool overwrite)
        {
            MCIManager.SendString(string.Format("record {0} from {1}{2}",
                this.m_identifier,
                from ,
                overwrite ? " overwrite":string.Empty 
                ));
        }
        public void Record(string from ,string to, bool overwrite)
        {
            MCIManager.SendString(string.Format("record {0} from {1} to {2}{3}",
                this.m_identifier,
                from,
                to,
                overwrite ? " overwrite":string.Empty 
                ));
        }
        public void Play()
        {
            Play(null, null, false , false );
        }
        public void Play(string from, string to, bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("play {0} {1}",
                this.m_identifier,
                string.Format ("{0}{1}{2}{3}",
                !string.IsNullOrEmpty (from )? " from "+from:string.Empty ,
                !string.IsNullOrEmpty (to)? " to "+to:string.Empty ,
                notify ? " notify": string.Empty,
                wait ? " wait":string.Empty 
                ))
                );
        }
        public void Pause()
        { 
                 MCIManager.SendString(string.Format("pause {0} ",
                this.m_identifier));
        }
        public void Pause(bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("pause {0} {1}",
                this.m_identifier,
                string.Format("{0}{1}",
                notify ? " notify" : string.Empty,
                wait ? " wait" : string.Empty
                ))
                );
        }
        public void Stop()
        {
            this.Stop(false, false);
        }
        public void Stop(bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("stop {0} {1}",
                this.m_identifier,
                string.Format("{0}{1}",
                notify ? " notify" : string.Empty,
                wait ? " wait" : string.Empty
                ))
                );
            SeekToStart();
        }
        public void Delete(string from, string to, bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("delete {0} {1}",
                this.m_identifier,
                string.Format("{0}{1}{2}{3}",
                !string.IsNullOrEmpty(from) ? " from " + from : string.Empty,
                !string.IsNullOrEmpty(to) ? " to " + to : string.Empty,
                notify ? " notify" : string.Empty,
                wait ? " wait" : string.Empty
                ))
                );
        }
        public void Close()
        {
            MCIManager.SendString(string.Format("close {0}",
                this.m_identifier
                ));
        }
        public void  Copy(string from, string to, bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("copy {0} {1}",
                this.m_identifier,
                string.Format("{0}{1}{2}{3}",
                !string.IsNullOrEmpty(from) ? " from " + from : string.Empty,
                !string.IsNullOrEmpty(to) ? " to " + to : string.Empty,
                notify ? " notify" : string.Empty,
                wait ? " wait" : string.Empty
                ))
                );
        }
        public void Paste(string from, string to, bool notify, bool wait)
        {
            MCIManager.SendString(string.Format("copy {0} {1}",
                this.m_identifier,
                string.Format("{0}{1}{2}{3}",
                !string.IsNullOrEmpty(from) ? " from " + from : string.Empty,
                !string.IsNullOrEmpty(to) ? " to " + to : string.Empty,
                notify ? " notify" : string.Empty,
                wait ? " wait" : string.Empty
                ))
                );
        }
        public string Identifier {
            get { return this.m_identifier; }
        }
        public static MCIWaveManager Create(string identifier)
        {
            try
            {
                string i = MCIManager.GetStatus(identifier, "mode", 155);               
                MCIWaveManager manager = new MCIWaveManager();
                manager.m_identifier = identifier;
                return manager;
            }
            catch { 
            }
            return null;
        }
    }
}

