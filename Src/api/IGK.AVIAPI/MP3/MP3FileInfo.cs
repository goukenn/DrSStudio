

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MP3FileInfo.cs
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
file:MP3FileInfo.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.MP3
{
    
    using IGK.ICore;using IGK.AVIApi.ACM;
    using IGK.AVIApi.MMIO;
    using IGK.AVIApi.AVI;
    using IGK.AVIApi.Native;
    /// <summary>
    /// represent an mp3 file info
    /// </summary>
    public class MP3FileInfo
    {
        static Dictionary<MP3MPEG1LayerBitsRate, int> smBitsRate;
        const string MP3_HEADER = "ID3";
        static readonly int[][] smBitRates =  
            { 
             new int[]{32,64,96,128,160,192,224,256,288,320,352,384,416,448},//mp1 - Layer1
             new int[]{32,48,56,64,80,96,112,128,160,192,224,256,320,384},//mp1 - Layer2
             new int[]{32,40,48,56,64,80,96,112,128,160,192,224,256,320},//mp1 - Layer3
             new int[]{32,64,96,128,160,192,224,256,288,320,352,384,416,448},//mp2 - Layer1
             new int[]{32,48,56,64,80,96,112,128,160,192,224,256,320,384},//mp2 - Layer2            
             new int[]{   8,16,24,32,64,80,56,64,128,160,112,128,256,320       }//mp2 - Layer3                
            };
        static readonly int[][] smFrequencies =  
            { 
             new int[]{44100,48000,32000},//mp1
             new int[]{22050,24000,16000},//mp2
             new int[]{11025,12000,8000}//mp1 - Layer3
             };
        private enuMP3LayerType m_LayerType;
        private enuMP3Type m_Type;
        private int m_BitRate;
        private int m_SampleRate;
        private enuMP3ChannelType m_ChannelType;
        private bool m_CopyRighted;
        private bool m_Original;
        private string m_FileName;
        private enuMP3TargetType m_TargetType;
        public enuMP3TargetType TargetType
        {
            get { return m_TargetType; }
        }
        public string FileName
        {
            get { return m_FileName; }
        }
        public bool Original
        {
            get { return m_Original; }
        }
        public bool CopyRighted
        {
            get { return m_CopyRighted; }
        }
        public enuMP3ChannelType ChannelType
        {
            get { return m_ChannelType; }
        }
        public int SampleRate
        {
            get { return m_SampleRate; }
        }
        public int BitRate
        {
            get { return m_BitRate; }
        }
        private short GetChannel()
        {
            return 2;
        }
        public enuMP3Type Type
        {
            get { return m_Type; }
        }
        public enuMP3LayerType LayerType
        {
            get { return m_LayerType; }
        }
        static MP3FileInfo()
        {
            //initialize bit rate info
            smBitsRate = new Dictionary<MP3MPEG1LayerBitsRate, int>();
            MP3MPEG1LayerBitsRate[] v_vtypes = {
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG1,enuMP3LayerType.LayerI ),
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG1,enuMP3LayerType.LayerII ),
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG1,enuMP3LayerType.LayerIII ),
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG2,enuMP3LayerType.LayerI ),
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG2,enuMP3LayerType.LayerII ),
new MP3MPEG1LayerBitsRate (enuMP3Type.MPEG2,enuMP3LayerType.LayerIII )
};
            for (int j = 0; j < 6; j++)
            {
                for (int i = 0; i < 14; i++)
                {
                    v_vtypes[j].index = i + 1;
                    smBitsRate.Add(v_vtypes[j], smBitRates[j][i]);
                }
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        private MP3FileInfo()
        {
        }

        public static void Save(string filename)
        {
            FileStream sf = File.Create(filename);
            Save(sf);
            sf.Close();
        }
        public static void Save(Stream stream)
        {
            BinaryWriter v_binw = new BinaryWriter(stream);

            //write header
            v_binw.Write(MP3_HEADER.ToCharArray());
            //write version
            v_binw.Write((byte)3);
            v_binw.Write((byte)0);

            //write 


            v_binw.Flush();
            
            
        }
        public static MP3FileInfo OpenFile(string filename)
        {
            if (!File.Exists(filename))
                return null;
            MP3FileInfo v_info = null;
            string v_h = string.Empty;
            long v_offset = 0;
            bool v_tagFound = false;
            bool v_headerFound = false;
            int v_minor;
            int v_major;
            char[] v_tchar = null;
            try
            {
                using (BinaryReader v_binR = new BinaryReader(File.Open(filename, FileMode.Open, FileAccess.Read)))
                {
                    //retrieve header
                    while ((!v_tagFound) && ((v_offset + 3) < v_binR.BaseStream.Length))
                    {
                        v_offset = v_binR.BaseStream.Position;
                        v_tchar = v_binR.ReadChars(3);
                        v_tagFound = new string(v_tchar) == MP3_HEADER;
                        v_offset++;
                        if (!v_tagFound)
                        {
                            v_binR.BaseStream.Seek(v_offset, SeekOrigin.Begin);
                        }
                    }
                    //
                    if (v_tagFound)
                    {
                        v_minor = v_binR.ReadByte();
                        v_major = v_binR.ReadByte();
                    }
                    else
                        return null;
                    v_binR.BaseStream.Seek(0, SeekOrigin.Begin);
                    while (!v_headerFound && v_binR.BaseStream.CanRead)
                    {
                        if (v_binR.ReadByte() == 255)
                        {
                            byte t = (byte)unchecked(v_binR.ReadByte() / 16);
                            if (t == 15)
                            {
                                v_headerFound = true;
                                break;
                            }
                        }
                    }
                    if (v_headerFound)
                    {
                        v_info = new MP3FileInfo();
                        v_binR.BaseStream.Seek(-2, SeekOrigin.Current);
                        uint s = (uint)unchecked(v_binR.ReadByte() << 24 |
                        v_binR.ReadByte() << 16 |
                        v_binR.ReadByte() << 8 |
                        v_binR.ReadByte());
                        short sync = (short)((s & 0xFFF00000) >> 20);//12 bits
                        int id = (int)(s & 0x00180000) >> 19;//2 bits for 11 bits sync
                        int layer = (int)(s & 0x00060000) >> 17;//2 bits
                        int cbProtection = (int)(s & 0x00010000) >> 16;//1 bits
                        int cbBitrate = (int)(s & 0x0000f000) >> 12;//4 bit
                        int cbSamplingRate = (int)(s & 0x0c00) >> 10;//2 bit
                        int cbPaddingBit = (int)(s & 0x0200) >> 9;//1 bit
                        int cbPrivateBit = (int)(s & 0x0100) >> 8;//1 bit
                        int cbChannelMode = (int)(s & 0x00c0) >> 6;//2 bit , 00 stereo, 01 joint stereo , 10 dualchannel, 11 single channel
                        int cbModeExtension = (int)(s & 0x0030) >> 4;//2bit
                        int cbCopyrighted = (int)(s & 0x0008) >> 3;//1 bit
                        int cbOriginal = (int)(s & 0x0004) >> 2;//1 bit
                        int cbEmphasis = (int)(s & 0x0003);//2 bit
                        MP3FileInfo.CreateInfo(v_info,
                 sync,
                 id,
                 layer,
                 cbProtection,
                 cbBitrate,
                 cbSamplingRate,
                 cbPaddingBit,
                 cbPrivateBit,
                 cbChannelMode,
                 cbModeExtension,
                 cbCopyrighted,
                 cbOriginal,
                 cbEmphasis);
                        v_info.m_FileName = filename;
                        v_info.m_TargetType = enuMP3TargetType.File;
                    }
                }
                return v_info;
            }
            catch
            {
            }
            return null;
        }
        /// <summary>
        /// create info
        /// </summary>
        /// <param name="info"></param>
        /// <param name="sync"></param>
        /// <param name="id"></param>
        /// <param name="layer"></param>
        /// <param name="cbProtection"></param>
        /// <param name="cbBitrate"></param>
        /// <param name="cbSamplingRate"></param>
        /// <param name="cbPaddingBit"></param>
        /// <param name="cbPrivateBit"></param>
        /// <param name="cbChannelMode"></param>
        /// <param name="cbModeExtension"></param>
        /// <param name="cbCopyrighted"></param>
        /// <param name="cbOriginal"></param>
        /// <param name="cbEmphasis"></param>
        internal static void CreateInfo(
            MP3FileInfo info,
            short sync,
            int id,
            int layer,
            int cbProtection,
            int cbBitrate,
            int cbSamplingRate,
            int cbPaddingBit,
            int cbPrivateBit,
            int cbChannelMode,
            int cbModeExtension,
            int cbCopyrighted,
            int cbOriginal,
            int cbEmphasis
            )
        {
            info.m_LayerType = (enuMP3LayerType)layer;
            info.m_Type = (enuMP3Type)id;
            MP3MPEG1LayerBitsRate key = new MP3MPEG1LayerBitsRate
                (info.m_Type, info.m_LayerType);
            key.index = cbBitrate;
            info.m_BitRate = smBitsRate[key] * 1000;
            info.m_ChannelType = (enuMP3ChannelType)cbChannelMode;
            info.m_CopyRighted = (cbCopyrighted & 1) != 0;
            info.m_Original = (cbOriginal & 1) != 0;
            switch (info.m_Type)
            {
                case enuMP3Type.MPEG1:
                    info.m_SampleRate = smFrequencies[0][cbSamplingRate];
                    break;
                case enuMP3Type.MPEG2_5:
                    info.m_SampleRate = smFrequencies[3][cbSamplingRate];
                    break;
                case enuMP3Type.MPEG2:
                    info.m_SampleRate = smFrequencies[1][cbSamplingRate];
                    break;
                case enuMP3Type.Reserved:
                    break;
                default:
                    break;
            }
        }
        public override string ToString()
        {
            return base.ToString();
        }
        [StructLayout(LayoutKind.Sequential)]
        struct MP3MPEG1LayerBitsRate
        {
            internal enuMP3Type mp3type;
            internal enuMP3LayerType mp3ltype;
            internal int index;
            public MP3MPEG1LayerBitsRate(enuMP3Type type, enuMP3LayerType layer)
            {
                this.mp3type = type;
                this.mp3ltype = layer;
                this.index = 0;
            }
        }

        /// <summary>
        /// Convert MP3 file to wave file
        /// </summary>
        /// <param name="inputfile"></param>
        /// <param name="outputfile"></param>
        /// <param name="progressEventHandler"></param>
        public static void ConvertToWav(string inputfile, string outputfile, AVIApiProgressEventHandler progressEventHandler)
        {
            if (!File.Exists(inputfile))
                return;
            MP3FileInfo mp3 = MP3FileInfo.OpenFile(inputfile);
            if (mp3 == null)
                return;
            mp3.ConvertToWave(outputfile);
        }
        /// <summary>
        /// Converte mp3 file to wave
        /// </summary>
        /// <param name="outfile"></param>
        /// <returns></returns>
        public bool ConvertToWave(string outfile)
        {
            return ConvertToWave(outfile,
                4, 2, 16, 44100, 176400);
        }
        public bool ConvertToWave(string outfile,
            int blockCount,//4
            int channel,//2
            int bits,//16
            int frequency, //44100
            int average  //176400
            )
        {
            ACMDriverInfo[] tdriver = ACMManager.GetMP3Driver();
            if (tdriver.Length > 0)
            {
                //wav wav header chunck to out put file
                MMIO.MMIOStream mem =
                    MMIO.MMIOManager.OpenFile(outfile, MMIO.enuMMIOAccess.Create | MMIO.enuMMIOAccess.ReadWrite);
                if (mem == null)
                    return false;
                // good one
                // mem.AddWaveChunck(1, 1, 8, 11025, 11025);
                mem.AddWaveChunck();//4, 2, 16, 44100, 176400);
                mem.AddPCMData(new byte[0]);
                mem.Close();
                mem.Dispose();
                ACMDriverInfo v_driver = tdriver[0];
                ACMFormatInfo[] formats = v_driver.GetFormats();
                ACMFormatTagInfo v_tagf = v_driver.GetFormatTag(enuACMWaveFormat.MPEGLAYER3);
                //MPEGLAYER3WAVEFORMAT mp3Format = GetMP3Info();
                IWAVEFORMATEX mp3Format = this.GetWaveFormat();
                WAVEFORMATEX waveFormat = new WAVEFORMATEX();
                waveFormat.cbSize = (short)Marshal.SizeOf(waveFormat);
                waveFormat.nAvgBytesPerSec = average;
                waveFormat.nSamplesPerSec = frequency;
                waveFormat.nChannels = (short)channel;
                waveFormat.wBitsPerSample = (short)bits;
                waveFormat.wFormatTag = AVI.AVIApi.WAVE_FORMAT_PCM;// ACMApi .MPEGLAYER3_ID_MPEG 
                waveFormat.nBlockAlign = (short)blockCount;
                v_driver.Open();


                Stream outStream = File.Open(outfile, FileMode.Open);
                Stream inStream = File.Open(this.FileName, FileMode.Open);

                outStream.Seek(0, SeekOrigin.End);

                bool r = ACMManager.Convert
                   (
                    v_driver,
                    inStream,
                    outStream,
                    mp3Format,
                    new PWAVEFORMAT(waveFormat),
                    enuACMSuggest.BitsPerSec);

                BinaryWriter binW = new BinaryWriter(outStream);
                binW.Seek(4, SeekOrigin.Begin);
                binW.Write((int)outStream.Length - 8);
                binW.Seek(40, SeekOrigin.Begin);
                binW.Write((int)outStream.Length - 40);
                mp3Format.Dispose();
                outStream.Flush();
                outStream.Close();
                inStream.Close();
                v_driver.Close();
                return r;
            }
            return false;
        }
        public bool ConvertToPCM(string outfile, bool chooseCodec,
            int blockCount,//4
            int channel,//2
            int bits,//16
            int frequency, //44100
            int average  //176400
            )
        {
            ACMDriverInfo[] tdriver = ACMManager.GetMP3Driver();
            if (tdriver.Length > 0)
            {
                ACMDriverInfo v_driver = tdriver[0];
                ACMFormatInfo[] formats = v_driver.GetFormats();
                ACMFormatTagInfo v_tagf = v_driver.GetFormatTag(enuACMWaveFormat.MPEGLAYER3);
                MPEGLAYER3WAVEFORMAT mp3Format =
                    new MPEGLAYER3WAVEFORMAT();
                //get source info codectype
                if (chooseCodec)
                {
                    mp3Format = (MPEGLAYER3WAVEFORMAT)ACMManager.ChoosePCMFormat("Choose codec", typeof(MPEGLAYER3WAVEFORMAT));
                }
                else
                {
                    mp3Format = this.GetMP3Info();
                }
                //setup destination layer                
                WAVEFORMATEX waveFormat = new WAVEFORMATEX();
                waveFormat.cbSize = (short)Marshal.SizeOf(waveFormat);
                waveFormat.nAvgBytesPerSec = average;
                waveFormat.nSamplesPerSec = frequency;
                waveFormat.nChannels = (short)channel;
                waveFormat.wBitsPerSample = (short)bits;
                waveFormat.wFormatTag = AVI.AVIApi.WAVE_FORMAT_PCM;// ACMApi .MPEGLAYER3_ID_MPEG 
                v_driver.Open();
                bool r = ACMManager.Convert(
                    v_driver,
                    this.FileName, outfile,
                    new PWAVEFORMAT(mp3Format),
                    new PWAVEFORMAT(waveFormat),
                     enuACMSuggest.WFormat);
                v_driver.Close();
                return r;
            }
            return false;
        }
        private MPEGLAYER3WAVEFORMAT GetMP3Info()
        {
            MPEGLAYER3WAVEFORMAT mp3Format = new MPEGLAYER3WAVEFORMAT();
            // mp3Format = c;
            //build the output format
            // WAVEFORMATEX v_sourmp3Format = mp3Format.wfx;
            mp3Format.wfx.cbSize = (short)12;//extra byte
            mp3Format.wfx.wFormatTag = MMSYSTEM.mmSystemAPI.WAVE_FORMAT_MPEGLAYER3;
            mp3Format.wfx.nChannels = this.GetChannel();
            mp3Format.wfx.nAvgBytesPerSec = 64 * (1024 / 8);//
            mp3Format.wfx.wBitsPerSample = 0;//Must be zero
            mp3Format.wfx.nBlockAlign = 1;   // Must be one                
            mp3Format.wfx.nSamplesPerSec = this.SampleRate; //c.nAvgBytesPerSec;
            //c.wFormatTag;
            // mp3Format.wfx = v_sourmp3Format;
            mp3Format.fdwFlags = ACMApi.MPEGLAYER3_FLAG_PADDING_OFF;// 2;// c.fdwFlags;
            mp3Format.nBlockSize = ACMApi.MP3_BLOCK_SIZE;// c.nBlockSize;  //voodoo value
            mp3Format.nCodecDelay = ACMApi.MP3_STD_CODE_DELAY;// c.nCodecDelay; // 1393;/*voodoo value*/
            mp3Format.nFramesPerBlock = 1;// must be one
            mp3Format.wID = ACMApi.MPEGLAYER3_ID_MPEG;
            //1;// c.wID;
            return mp3Format;
        }
        /// <summary>
        /// retreive the mp3 wave ex format
        /// </summary>
        /// <returns></returns>
        public IWAVEFORMATEX GetWaveFormat()
        {
            return new PWAVEFORMAT(this.GetMP3Info());
        }
        /// <summary>
        /// get the wave format from ACMFormatInfo
        /// </summary>
        /// <param name="info">infor to get </param>
        /// <returns>IWAVEFORMATEX to pass to encoder</returns>
        public static IWAVEFORMATEX GetWaveFormat(ACMFormatInfo info)
        {
            MPEGLAYER3WAVEFORMAT frm = info.GetFormatEXInfo<MPEGLAYER3WAVEFORMAT>();
            return new PWAVEFORMAT(frm);
        }
        /// <summary>
        /// convert to a PCM
        /// </summary>
        /// <param name="waveFile"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool ConvertToPCM(string waveFile, IWAVEFORMATEX dest)
        {
            IWAVEFORMATEX src = GetWaveFormat();
            if (dest == null) return false;
            ACMDriverInfo v_drv = ACMManager.GetMP3Driver()[0];
            v_drv.Open();
            bool ret = ACMManager.Convert(v_drv, this.FileName, waveFile, src, dest, enuACMSuggest.WFormat);
            v_drv.Close();
            return ret;
        }
        /// <summary>
        /// covert to wave file with bitrate
        /// </summary>
        /// <param name="outfile"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public bool ConvertToWave(string outfile, IWAVEFORMATEX dest)
        {
            ACMDriverInfo[] tdriver = ACMManager.GetMP3Driver();
            if (tdriver.Length > 0)
            {
                //wav wav header chunck to out put file
                //preapre wave for input
                MMIO.MMIOStream mem = MMIO.MMIOManager.OpenFile(outfile, MMIO.enuMMIOAccess.Create | MMIO.enuMMIOAccess.ReadWrite);
                if (mem == null)
                    return false;
                // good one
                // mem.AddWaveChunck(1, 1, 8, 11025, 11025);
                mem.AddWaveChunck();//4, 2, 16, 44100, 176400);
                mem.AddPCMData(new byte[0]);
                mem.Close();
                mem.Dispose();
                ACMDriverInfo v_driver = tdriver[0];
                IWAVEFORMATEX mp3Format = this.GetWaveFormat();
                v_driver.Open();
                Stream outStream = File.Open(outfile, FileMode.Open);
                outStream.Seek(0, SeekOrigin.End);
                //write header
                Stream inStream = File.Open(this.FileName, FileMode.Open);
                bool r = ACMManager.Convert(
                    v_driver,
                    inStream,
                    outStream,
                    mp3Format,
                    dest,
                     enuACMSuggest.WFormat);
                BinaryWriter binW = new BinaryWriter(outStream);
                binW.Seek(4, SeekOrigin.Begin);
                binW.Write((int)outStream.Length - 8);
                binW.Seek(40, SeekOrigin.Begin);
                binW.Write((int)outStream.Length - 40);
                mp3Format.Dispose();
                outStream.Flush();
                outStream.Close();
                inStream.Close();
                v_driver.Close();
                return r;
            }
            return false;
        }
        /// <summary>
        /// convert a wav file to mp3. not working yet
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        public static bool ConvertFromWav(string inputFile, string outfile, bool chooseCodec)
        {
            throw new NotImplementedException();

            //MP3FileInfo rf = new MP3FileInfo();
            //ACMDriverInfo[] tdriver = ACMManager.GetMP3Driver();
            //if ((tdriver == null) || (tdriver.Length == 0))
            //{
            //    CoreLog.WriteDebug("no mp3 driver found");
            //    return false;
            //}
            //AVIFile f =  AVIFile.Open(inputFile);
            //if (f == null)
            //{
            //    CoreLog.WriteDebug("can't open wav file");
            //    return false;
            //}
            //AVIFile.AudioStream aud = f.GetAudioStream();

            //WAVEFORMATEX wformat = new WAVEFORMATEX();
            //wformat.cbSize =(short ) Marshal.SizeOf(typeof(WAVEFORMATEX));
            //var sformat = aud.GetStreamInfo ();
            //wformat.nAvgBytesPerSec = 0;
            //wformat.nBlockAlign = (short)4;
            //wformat.nChannels = (short)2;
            //wformat.wBitsPerSample = (short)sformat.dwRate ;
            //wformat.nAvgBytesPerSec = (short)sformat.dwRate;
            //wformat.wFormatTag = (short)AVIApi.WAVE_FORMAT_PCM;
            

            //MPEGLAYER3WAVEFORMAT rformat = new MPEGLAYER3WAVEFORMAT();// (MPEGLAYER3WAVEFORMAT)ACM.ACMManager.ChoosePCMFormat("title.ChooseAudioFormat".R(), typeof(MPEGLAYER3WAVEFORMAT));
            //var o = AVIApiUtility.ChooseAudioCodec(IntPtr.Zero, aud.Handle);

            //if (o.cbFormat > 0)
            //{
            //    CoreLog.WriteDebug("get format " + o.cbFormat );
            //    //get choose format
            //    rformat = MPEGLAYER3WAVEFORMAT.GetFromHandle(o.lpFormat);
            //}
            //aud.Dispose();
            //f.Close();

   

            //{
            //    //create mp3 files
            //    MMIO.MMIOStream mem =
            //        MMIO.MMIOManager.OpenFile(outfile, MMIO.enuMMIOAccess.Create | MMIO.enuMMIOAccess.ReadWrite);
            //    if (mem == null)
            //        return false;
            //    // good one
            //    //mem.AddWaveChunck(1, 1, 8, 11025, 11025);
            //    //mem.AddWaveChunck();//4, 2, 16, 44100, 176400);
            //    mem.AddPCMData(new byte[0]);
            //    mem.Close();
            //    mem.Dispose();
            //    ACMDriverInfo v_driver = tdriver[0];
            //    ACMFormatInfo[] formats = v_driver.GetFormats();
            //    ACMFormatTagInfo v_tagf = v_driver.GetFormatTag(enuACMWaveFormat.MPEGLAYER3);
            //    //MPEGLAYER3WAVEFORMAT mp3Format = GetMP3Info();
            //    IWAVEFORMATEX mp3Format = rf.GetWaveFormat();
            //    //WAVEFORMATEX waveFormat = WAVEFORMATEX.GetFromHandle(v_opts.Handle);
            //    //waveFormat.cbSize = (short)Marshal.SizeOf(waveFormat);
            //    //waveFormat.nAvgBytesPerSec = average;
            //    //waveFormat.nSamplesPerSec = frequency;
            //    //waveFormat.nChannels = (short)channel;
            //    //waveFormat.wBitsPerSample = (short)bits;
            //    //waveFormat.wFormatTag = AVI.AVIApi.WAVE_FORMAT_PCM;// ACMApi .MPEGLAYER3_ID_MPEG 
            //    //waveFormat.nBlockAlign = (short)blockCount;
            //    v_driver.Open();


            //    Stream outStream = File.Open(outfile, FileMode.Open);
            //    Stream inStream = File.Open(inputFile, FileMode.Open);

            //    outStream.Seek(0, SeekOrigin.End);

            //    bool r = ACMManager.Convert
            //       (
            //        v_driver,
            //        inStream,
            //        outStream,
            //        new PWAVEFORMAT( wformat), //wave format.
            //        new PWAVEFORMAT(rformat), //mp3 format
            //        //new PWAVEFORMAT(waveFormat),
            //        enuACMSuggest.BitsPerSec);

            //    BinaryWriter binW = new BinaryWriter(outStream);
            //    binW.Seek(4, SeekOrigin.Begin);
            //    binW.Write((int)outStream.Length - 8);
            //    binW.Seek(40, SeekOrigin.Begin);
            //    binW.Write((int)outStream.Length - 40);
            //    mp3Format.Dispose();
            //    outStream.Flush();
            //    outStream.Close();
            //    inStream.Close();
            //    v_driver.Close();
            //    return r;
            //}
        }
        /// <summary>
        /// return pcm data the current mp3 file
        /// </summary>
        /// <returns></returns>
        public byte[] GetPCMData()
        {
            ACMDriverInfo[] v_tdriver = ACMManager.GetMP3Driver();
            if ((v_tdriver ==null) && (v_tdriver.Length == 0))
                return null;
            Byte[] tab = null;
            var mp3drv = ACMManager.GetMP3Driver()[0];
            mp3drv.Open();
            MemoryStream mem = new MemoryStream();
            FileStream fstream = File.Open(this.FileName, FileMode.Open);
            bool r = ACMManager.Convert(mp3drv, 
                fstream, mem,
                this.GetWaveFormat(), null, enuACMSuggest.None);
            mp3drv.Close();
            fstream.Close();

            //mem.Seek(0, SeekOrigin.Begin);
            //var fs = File.Create(CoreConstant.DEBUG_TEMP_FOLDER+"\\mp3.pcm");
            //mem.WriteTo(fs);
            //fs.Close();
            //get pcm data
            mem.Seek(0, SeekOrigin.Begin);
            tab = new byte[mem.Length];
            mem.Read(tab, 0, tab.Length);
            mem.Dispose();
            return tab;
        }
    }
}

