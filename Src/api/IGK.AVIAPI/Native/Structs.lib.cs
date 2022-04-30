

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Structs.lib.cs
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
file:Structs.lib.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
namespace IGK.AVIApi.Native
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    internal struct WAVEFORMATEX : IWAVEFORMAT
    {
        public short wFormatTag;
        public short nChannels;
        public int nSamplesPerSec;
        public int nAvgBytesPerSec;
        public short nBlockAlign;
        public short wBitsPerSample;
        public short cbSize;
        #region IAVIFormat Members
        public int Size
        {
            get { return cbSize; }
        }
        public int Channel { get { return this.nChannels; } }
        public int AvgBytesPerSec { get { return this.nAvgBytesPerSec; } }
        public int FormatTag
        {
            get { return this.wFormatTag; }
        }
        #endregion

        internal static WAVEFORMATEX GetFromHandle(IntPtr h)
        {
            return (WAVEFORMATEX)Marshal.PtrToStructure(h, typeof(WAVEFORMATEX));
        }
    }
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MPEGLAYER3WAVEFORMAT : IWAVEFORMAT
    {
        internal  WAVEFORMATEX wfx;
        //public short wFormatTag;
        //public short nChannels;
        //public int nSamplesPerSec;
        //public int nAvgBytesPerSec;
        //public short nBlockAlign;
        //public short wBitsPerSample;
        //public short cbSize;
        public short wID;
        public int fdwFlags;
        public short nBlockSize;
        public short nFramesPerBlock;
        public short nCodecDelay;
        public int Size
        {
            get { return Marshal.SizeOf(GetType()); }
        }
        internal static MPEGLAYER3WAVEFORMAT GetFromHandle(IntPtr h)
        {
            return (MPEGLAYER3WAVEFORMAT)Marshal.PtrToStructure(h, typeof(MPEGLAYER3WAVEFORMAT));
        }
        public override string ToString()
        {
            return string.Format("blocksize[{0}], channel[{1}], delay [{2}]", nBlockSize, wfx.nChannels, nCodecDelay);
        }
        #region IWAVEFORMAT Members
        public int Channel
        {
            get { return this.wfx.nChannels; }
        }
        public int AvgBytesPerSec
        {
            get { return this.wfx.nAvgBytesPerSec; }
        }
        public int FormatTag
        {
            get { return this.wfx.wFormatTag; }
        }
        #endregion
    }
    
    [StructLayout(LayoutKind.Sequential)]
    public struct LPWAVEFILTER
    {
        internal int cbStruct;
        internal int dwFilterTag;
        internal int fdwFilter;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        internal int[] dwReserved;
    }
}

