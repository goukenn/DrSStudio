

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PWAVEFORMAT.cs
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
file:PWAVEFORMAT.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.AVIApi
{
    using IGK.ICore;using IGK.AVIApi.AVI;
    using IGK.AVIApi.ACM;
    /// <summary>
    /// represent a pointer to a wave format
    /// </summary>
    public class PWAVEFORMAT : IWAVEFORMATEX
    {
        private IntPtr m_handle; //pointer to a waveex format
        private int m_size;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public PWAVEFORMAT(IWAVEFORMAT obj)
        {
            this.m_size = ACMInfo.MaxSizeFormat;
            // this.m_size = Marshal.SizeOf(obj);
            IntPtr v_alloc = Marshal.AllocCoTaskMem(this.m_size);
            Marshal.StructureToPtr(obj, v_alloc, true);
            this.m_handle = v_alloc;
            // Marshal.SizeOf(obj);
        }
        #region IWAVEFORMATEX Members
        public int FormatSize
        {
            get { return this.m_size; }
        }
        public IntPtr Handle
        {
            get { return this.m_handle; }
        }
        #endregion
        #region IDisposable Members
        public void Dispose()
        {
            if (this.m_handle != IntPtr.Zero)
            {
                Marshal.FreeCoTaskMem(this.m_handle);
                this.m_handle = IntPtr.Zero;
            }
        }
        ~PWAVEFORMAT()
        {
            this.Dispose();
        }
        #endregion
    }
}

