

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VideoPlayer.cs
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
file:VideoPlayer.cs
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
    using MCI;
    /// <summary>
    /// represent a mci video player
    /// </summary>
    public class VideoPlayer
    {
        string m_id;
        string m_type;
        IntPtr m_hwnd;
        public string ID { get { return this.m_id; } }
        public string VidType { get { return m_type; } }
        public IntPtr WindowHandle { get { return m_hwnd; } }
        public long Length{
            get {
                return 0;
            }
        }
        public bool CanEject { get { return MCIManager.Capabilityb("can eject", this.ID); } }
        public bool CanFreeze { get { return MCIManager.Capabilityb("can freeze", this.ID); } }
        public bool CanLock { get { return MCIManager.Capabilityb("can lock", this.ID); } }
        public bool CanPlay { get { return MCIManager.Capabilityb("can play", this.ID); } }
        public bool CanRecord { get { return MCIManager.Capabilityb("can record", this.ID); } }
        public bool CanReserve { get { return MCIManager.Capabilityb("can reverse", this.ID); } }
        public bool CanSave { get { return MCIManager.Capabilityb("can save", this.ID); } }
        public bool CanStretch { get { return MCIManager.Capabilityb("can stretch", this.ID); } }
        public bool CanStretchInput { get { return MCIManager.Capabilityb("can stretch input", this.ID); } }
        public bool CanTest { get { return MCIManager.Capabilityb("can test", this.ID); } }
        public bool HasVideo { get { return MCIManager.Capabilityb("has video", this.ID); } }
        public bool HasAudio { get { return MCIManager.Capabilityb("has audio", this.ID); } }
        public string DeviceType { get { return MCIManager.Capabilitys("device type", this.ID); } }
        public int MaxWindows { get { return MCIManager.Capabilityi("windows", this.ID); } }
        public void SetWindowHandle(IntPtr hwnd)
        {
            MCIManager .SendString (string.Format ("window {0} handle {1}", 
                this.ID,
                (hwnd == IntPtr.Zero) ? "default" : hwnd.ToInt32().ToString()) );         
            this.m_hwnd = hwnd ;
        }
        public static VideoPlayer Open(string filename, string id)
        {
            if (MCIManager.Open(filename, id, "mpegvideo", false, false))
            {
                VideoPlayer v_vidplayer = new VideoPlayer();
                v_vidplayer.m_id = id;
                v_vidplayer.m_type = "mpegvideo";
                return v_vidplayer;
            }
            return null;
        }
        public void Stop()
        {
            this.SetPosition(0);
            MCIManager.Stop (this.ID, false ,false );
        }
        public void Close()
        {
            MCIManager.Close(this.ID);
        }
        public void Pause()
        {
            MCIManager.Pause(this.ID, false, false);
        }
        public void Play()
        {
            MCIManager.Play(this.ID);
        }
        public void Stretch()
        {
            MCIManager.SendString(string.Format ("put {0} destination", this.ID));
        }
        public void Restore()
        {
            MCIManager.SendString(string.Format("put {0} window client", this.ID));
        }
        public void SetPosition(int position)
        {
            MCIManager.SendString(string.Format("seek {0} to {1}", this.ID, position));
        }
        public void JumpTo(TimeSpan time)
        {
            throw new NotImplementedException();
        }
        public void CaptureTo(string filename)
        {
            MCIManager.SendString(string.Format("capture {0} as \"{1}\"", this.ID, filename ));
        }
        public void SaveTo(string filename)
        {
            MCIManager.SendString(string.Format("save {0} as \"{1}\"", this.ID, filename));
        }
    }
}

