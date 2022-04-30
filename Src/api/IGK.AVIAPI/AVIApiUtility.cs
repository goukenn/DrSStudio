

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AVIApiUtility.cs
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
file:Utils.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace IGK.AVIApi
{
    using IGK.ICore;using IGK.AVIApi.MMIO;
    using IGK.AVIApi.AVI;
    using IGK.AVIApi.MP3;
    using IGK.AVIApi.ACM;
    using System.IO;

    /// <summary>
    /// reprensent global utility file
    /// </summary>
    public static class AVIApiUtility
    {

        /// <summary>
        /// get pcm in data file
        /// </summary>
        /// <param name="wavFile">wave filename</param>
        /// <returns>return pcm data from file</returns>
        public static Byte[] GetPCMDataFromFile(string wavFile)
        {
            switch (System.IO.Path.GetExtension(wavFile ).ToLower())
            {
                    case ".mp3":
                    Byte[] tab = null;
                    MP3FileInfo mp3 = MP3FileInfo.OpenFile(wavFile);
                    if (mp3 != null)
                    {
                        tab = mp3.GetPCMData();
                    }
                    return tab;
                default:
                    break;
            }
            return MMIOManager.GetPCMData(wavFile);
        }
        /// <summary>
        /// create a wave file from pcm data
        /// </summary>
        /// <param name="targetFile">destination</param>
        /// <param name="pcmData"PCM data></param>
        /// <returns></returns>
        public static bool CreateWaveFile(string targetFile, byte[] pcmData)
        {
            return CreateWaveFile(targetFile, pcmData, 2, 4, 16, 44100);
        }
        private static bool CreateWaveFile(string targetFile, byte[] pcmData, 
            int channel, int blockCount, int bitPerSample, int frequency)
        {
            MMIOStream v_stream = MMIOStream.CreateNewFile(targetFile, enuMMIOAccess.ReadWrite);
            if (v_stream == null)
                return false;
            int average = frequency * blockCount;                                    
            IntPtr intPtr = v_stream.Handle;
            MMIOApi.MMIOChunkINFO chunck = new MMIOApi.MMIOChunkINFO();
            MMIOApi.MMIOChunkINFO p = new MMIOApi.MMIOChunkINFO();
            //create a wave chunk
            chunck.ckid = 0;// MMIOApi.FOURCC_RIFF;
            chunck.fccType = MMIOApi.FOURCC_WAVE;
            MMIOManager.CreateChunck(intPtr, ref chunck, enuMMIOCreateChunkParam.Riff);
            MMIOManager.Ascend(intPtr, ref  chunck);
            //save parent chunck
            p = chunck;
            // create fmt chuck sub id
            chunck = new MMIOApi.MMIOChunkINFO();
            chunck.ckid = MMIOApi.FOURCC_fmt;
            chunck.fccType = MMIOApi.FOURCC_WAVE;
            MMIOManager.Handle((enuMMIOError)MMIOApi.mmioCreateChunk(intPtr,
                ref chunck,
                (uint)enuMMIOCreateChunkParam.Data), "mmioCreateChunk");
            //write pcm header info
            MMIOApi.PCMWAVEFORMAT pcm = new MMIOApi.PCMWAVEFORMAT();
            pcm.wFormatTag = MMIOApi.WAVE_FORMAT_PCM;
            pcm.nAvgBytesPerSec = average;
            pcm.nChannels = (short)channel; //stereo
            pcm.nBlockAlign = (short)blockCount;// block aling
            pcm.wBitsPerSample = (short)bitPerSample;//bit per sample 16,8,
            pcm.nSamplesPerSec = frequency;//
            v_stream.Seek(chunck.dwDataOffset, enuMMIOSeek.Origin);
            IntPtr v_h = MMIOManager.CreateHandle(pcm);
            int i = 0;
            i = (int)MMIOApi.mmioWrite(intPtr,
            v_h,
            Marshal.SizeOf(pcm));
            //i = MMIOApi.mmioWrite(intPtr, pcmData, pcmData.Length);
            MMIOManager.CloseHandle(v_h);
            if (i == -1)
                MMIOManager.Handle((enuMMIOError)(-1), "mmioWrite");
            //go to top
            //update the size of the container data
            MMIOManager.Ascend(intPtr, ref chunck);
            v_stream.AddPCMData(pcmData);
            //update the size of the parent data
            MMIOManager.Ascend(intPtr, ref p);
            v_stream.Close();
            return true;
        }


        internal static AVIApi.AVICOMPRESSOPTIONS_CLASS ChooseAudioCodec(IntPtr hwnd, IntPtr streamHandle)
        {
            IntPtr aviStream = streamHandle==IntPtr.Zero ? new IntPtr(-1): streamHandle;
            AVIApi.AVICOMPRESSOPTIONS_CLASS opts = new AVIApi.AVICOMPRESSOPTIONS_CLASS();
            if (AVIApi.AVISaveOptions(hwnd,
                AVIApi.ICMF_CHOOSE_KEYFRAME |
                AVIApi.ICMF_CHOOSE_PREVIEW |
                AVIApi.ICMF_CHOOSE_DATARATE,
                streamHandle == IntPtr.Zero?0:1,
                ref aviStream,
                ref opts))
                return opts;  
            return null;
        }
    }
}

