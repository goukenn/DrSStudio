

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMFormatInfo.cs
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
file:ACMFormatInfo.cs
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
using System.Windows.Forms ;
using System.Runtime.InteropServices ;
namespace IGK.AVIApi.ACM
{
    using IGK.ICore;using IGK.AVIApi.Native;
    /// <summary>
    /// represent a acm format info
    /// </summary>
    public class ACMFormatInfo
    {
        private ACMDriverInfo m_DriverInfo;
        private ACMApi.ACMFORMATDETAILS m_details;
        private byte[] m_data;
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="details"></param>
        private ACMFormatInfo(ACMDriverInfo owner, ACMApi.ACMFORMATDETAILS details)
        {
            if (owner == null)
                throw new ArgumentNullException("owner");
            this.m_DriverInfo = owner;
            this.m_details = details;
        }
        internal static ACMFormatInfo CreateInfo(
            ACMDriverInfo owner, 
            ACMApi.ACMFORMATDETAILS details)
        {
            ACMFormatInfo v_c = new ACMFormatInfo(owner  , details);
            byte[] v_t = new byte[details.cbwfx];
            System.Runtime.InteropServices.Marshal.Copy ( details.pwfx, v_t , 0 ,v_t .Length );
            v_c.m_data = v_t;
//            WAVEFORMATEX mt =  
//(WAVEFORMATEX )v_c.GetFormatEXInfo (typeof (WAVEFORMATEX ));
            return v_c;
        }
        public object GetFormatEXInfo(Type t)
        {
            IntPtr v_alloc = Marshal.AllocCoTaskMem (m_data.Length );
            Marshal.Copy (m_data , 0, v_alloc , m_data.Length );
            Object obj = Marshal .PtrToStructure (v_alloc, t);
            Marshal.FreeCoTaskMem (v_alloc);
            return obj;
        }
        public T GetFormatEXInfo<T>()
        {
            Type t = default(T).GetType ();
            IntPtr v_alloc = Marshal.AllocCoTaskMem(m_data.Length);
            Marshal.Copy(m_data, 0, v_alloc, m_data.Length);
            Object obj = Marshal.PtrToStructure(v_alloc, t);
            Marshal.FreeCoTaskMem(v_alloc);
            return (T)obj ;
        }
        public ACMDriverInfo DriverInfo
        {
            get { return m_DriverInfo; }
        }
        public string DisplayName {
            get {
                return string.Format ("{0}, {1} Kbpps",
                    this.m_details.szFormat,BitsPerSecond 
                    );
            }
        }
        public int BitsPerSecond {
            get {
                WAVEFORMATEX t =
                  (WAVEFORMATEX)
                  this.GetFormatEXInfo(typeof(WAVEFORMATEX));
                return
                    t.nAvgBytesPerSec * 8 / 1000;
            }
        }
        /// <summary>
        /// get the Wave format handle
        /// </summary>
        public IntPtr WaveFormatHandle
        {
            get { return this.m_details.pwfx; }
        }
        /// <summary>
        /// get the support flag
        /// </summary>
        public enuACMSupportFlag Support {
            get {
                return (enuACMSupportFlag)this.m_details.fdwSupport;
            }
        }
        public enuACMWaveFormat Format {
            get {
                return (enuACMWaveFormat)this.m_details.dwFormatTag;
            }
        }
        public int DetailSize {
            get { return this.m_details.cbwfx; }
        }
        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}

