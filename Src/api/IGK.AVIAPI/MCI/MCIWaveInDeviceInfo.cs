

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MCIWaveInDeviceInfo.cs
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
file:MCIWaveInDeviceInfo.cs
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
    /// retrieve the device info
    /// </summary>
    public class MCIWaveInDeviceInfo
    {
        private MCIWaveInApi.WAVEINCAPS m_caps;
        public int ManufacturerId{get{return m_caps.wMid;}}
        public int ProducId { get { return m_caps.wPid ; } }
        public Version Version { get { return new Version(m_caps.vDriverVersion.wHight, m_caps.vDriverVersion.wLow); } }
        public string DisplayName { get { return this.m_caps.szPname; } }
        public enuMCIWaveInFormat Format { get { return (enuMCIWaveInFormat )this.m_caps.dwFormats; } }
        public int Channel { get { return this.m_caps.wChannels; } }
        private MCIWaveInDeviceInfo()
        {
        }
        /// <summary>
        /// get device info
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static MCIWaveInDeviceInfo GetInfo(int id)
        {
            MCIWaveInApi .WAVEINCAPS cap = new MCIWaveInApi.WAVEINCAPS ();
            int i = MCIWaveInApi.waveInGetDevCaps(
                new IntPtr(id),
                ref cap,
                System.Runtime.InteropServices.Marshal.SizeOf(cap));
            if (i == 0)
            {
                MCIWaveInDeviceInfo info = new MCIWaveInDeviceInfo();
                info.m_caps = cap;
                return info;
            }
            return null;
        }
    }
}

