

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: mmSystemAPI.cs
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
file:mmSystemAPI.cs
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
namespace IGK.AVIApi.MMSYSTEM
{
    internal static class mmSystemAPI
    {
        internal const int TIME_MS = 0x0001;  /* time in milliseconds */
internal const int  TIME_SAMPLES    =0x0002;  /* number of wave samples */
internal const int  TIME_BYTES      =0x0004;  /* current byte offset */
internal const int  TIME_SMPTE      =0x0008;  /* SMPTE time */
internal const int  TIME_MIDI       =0x0010;  /* MIDI time */
internal const int  TIME_TICKS      =0x0020;  /* Ticks within MIDI stream */
      /* flags for dwFlags field of WAVEHDR */
internal const int  WHDR_DONE       =0x00000001 ; /* done bit */
internal const int  WHDR_PREPARED   =0x00000002 ; /* set if this header has been prepared */
internal const int  WHDR_BEGINLOOP  =0x00000004 ; /* loop start block */
internal const int  WHDR_ENDLOOP    =0x00000008 ; /* loop end block */
internal const int  WHDR_INQUEUE    =0x00000010 ; /* reserved for driver */
        internal const int MAXPNAMELEN = 32;
        internal const int MAXERRORLENGTH = 256;
        internal const int MMSYSERR_BASE = MMSYSERR_NOERROR;
        internal const int MMSYSERR_NOERROR = 0;             /* no error */
        internal const int MMSYSERR_ERROR = (MMSYSERR_BASE + 1);  /* unspecified error */
        internal const int MMSYSERR_BADDEVICEID = (MMSYSERR_BASE + 2);  /* device ID out of range */
        internal const int MMSYSERR_NOTENABLED = (MMSYSERR_BASE + 3);  /* driver failed enable */
        internal const int MMSYSERR_ALLOCATED = (MMSYSERR_BASE + 4);  /* device already allocated */
        internal const int MMSYSERR_INVALHANDLE = (MMSYSERR_BASE + 5);  /* device handle is invalid */
        internal const int MMSYSERR_NODRIVER = (MMSYSERR_BASE + 6);  /* no device driver present */
        internal const int MMSYSERR_NOMEM = (MMSYSERR_BASE + 7);  /* memory allocation error */
        internal const int MMSYSERR_NOTSUPPORTED = (MMSYSERR_BASE + 8);  /* function isn't supported */
        internal const int MMSYSERR_BADERRNUM = (MMSYSERR_BASE + 9);  /* error value out of range */
        internal const int MMSYSERR_INVALFLAG = (MMSYSERR_BASE + 10); /* invalid flag passed */
        internal const int MMSYSERR_INVALPARAM = (MMSYSERR_BASE + 11); /* invalid parameter passed */
        internal const int MMSYSERR_HANDLEBUSY = (MMSYSERR_BASE + 12); /* handle being used */
        /* simultaneously on another */
        /* thread =(eg callback); */
        internal const int MMSYSERR_INVALIDALIAS = (MMSYSERR_BASE + 13); /* specified alias not found */
        internal const int MMSYSERR_BADDB = (MMSYSERR_BASE + 14); /* bad registry database */
        internal const int MMSYSERR_KEYNOTFOUND = (MMSYSERR_BASE + 15); /* registry key not found */
        internal const int MMSYSERR_READERROR = (MMSYSERR_BASE + 16); /* registry read error */
        internal const int MMSYSERR_WRITEERROR = (MMSYSERR_BASE + 17); /* registry write error */
        internal const int MMSYSERR_DELETEERROR = (MMSYSERR_BASE + 18); /* registry delete error */
        internal const int MMSYSERR_VALNOTFOUND = (MMSYSERR_BASE + 19); /* registry value not found */
        internal const int MMSYSERR_NODRIVERCB = (MMSYSERR_BASE + 20); /* driver does not call DriverCallback */
        internal const int MMSYSERR_MOREDATA = (MMSYSERR_BASE + 21); /* more data to be returned */
        internal const int MMSYSERR_LASTERROR = (MMSYSERR_BASE + 21); /* last error in range */
        /*wave format */
        internal const int WAVE_FORMAT_UNKNOWN = 0x0000;/* Microsoft Corporation */
        internal const int ACM_MPEG_LAYER1 = 0x0001;/*mpged layer*/
        internal const int WAVE_FORMAT_ADPCM = 0x0002;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_IEEE_FLOAT = 0x0003;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_VSELP = 0x0004;/* Compaq Computer Corp. */
        internal const int WAVE_FORMAT_IBM_CVSD = 0x0005;/* IBM Corporation */
        internal const int WAVE_FORMAT_ALAW = 0x0006;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_MULAW = 0x0007;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_DTS = 0x0008;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_DRM = 0x0009;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMAVOICE9 = 0x000A;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMAVOICE10 = 0x000B;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_OKI_ADPCM = 0x0010;/* OKI */
        internal const int WAVE_FORMAT_DVI_ADPCM = 0x0011;/* Intel Corporation */
        internal const int WAVE_FORMAT_IMA_ADPCM = (WAVE_FORMAT_DVI_ADPCM);/*  Intel Corporation */
        internal const int WAVE_FORMAT_MEDIASPACE_ADPCM = 0x0012;/* Videologic */
        internal const int WAVE_FORMAT_SIERRA_ADPCM = 0x0013;/* Sierra Semiconductor Corp */
        internal const int WAVE_FORMAT_G723_ADPCM = 0x0014;/* Antex Electronics Corporation */
        internal const int WAVE_FORMAT_DIGISTD = 0x0015;/* DSP Solutions, Inc. */
        internal const int WAVE_FORMAT_DIGIFIX = 0x0016;/* DSP Solutions, Inc. */
        internal const int WAVE_FORMAT_DIALOGIC_OKI_ADPCM = 0x0017;/* Dialogic Corporation */
        internal const int WAVE_FORMAT_MEDIAVISION_ADPCM = 0x0018;/* Media Vision, Inc. */
        internal const int WAVE_FORMAT_CU_CODEC = 0x0019;/* Hewlett-Packard Company */
        internal const int WAVE_FORMAT_YAMAHA_ADPCM = 0x0020;/* Yamaha Corporation of America */
        internal const int WAVE_FORMAT_SONARC = 0x0021;/* Speech Compression */
        internal const int WAVE_FORMAT_DSPGROUP_TRUESPEECH = 0x0022;/* DSP Group, Inc */
        internal const int WAVE_FORMAT_ECHOSC1 = 0x0023;/* Echo Speech Corporation */
        internal const int WAVE_FORMAT_AUDIOFILE_AF36 = 0x0024;/* Virtual Music, Inc. */
        internal const int WAVE_FORMAT_APTX = 0x0025;/* Audio Processing Technology */
        internal const int WAVE_FORMAT_AUDIOFILE_AF10 = 0x0026;/* Virtual Music, Inc. */
        internal const int WAVE_FORMAT_PROSODY_1612 = 0x0027;/* Aculab plc */
        internal const int WAVE_FORMAT_LRC = 0x0028;/* Merging Technologies S.A. */
        internal const int WAVE_FORMAT_DOLBY_AC2 = 0x0030;/* Dolby Laboratories */
        internal const int WAVE_FORMAT_GSM610 = 0x0031;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_MSNAUDIO = 0x0032;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_ANTEX_ADPCME = 0x0033;/* Antex Electronics Corporation */
        internal const int WAVE_FORMAT_CONTROL_RES_VQLPC = 0x0034;/* Control Resources Limited */
        internal const int WAVE_FORMAT_DIGIREAL = 0x0035;/* DSP Solutions, Inc. */
        internal const int WAVE_FORMAT_DIGIADPCM = 0x0036;/* DSP Solutions, Inc. */
        internal const int WAVE_FORMAT_CONTROL_RES_CR10 = 0x0037;/* Control Resources Limited */
        internal const int WAVE_FORMAT_NMS_VBXADPCM = 0x0038;/* Natural MicroSystems */
        internal const int WAVE_FORMAT_CS_IMAADPCM = 0x0039;/* Crystal Semiconductor IMA ADPCM */
        internal const int WAVE_FORMAT_ECHOSC3 = 0x003A;/* Echo Speech Corporation */
        internal const int WAVE_FORMAT_ROCKWELL_ADPCM = 0x003B;/* Rockwell International */
        internal const int WAVE_FORMAT_ROCKWELL_DIGITALK = 0x003C;/* Rockwell International */
        internal const int WAVE_FORMAT_XEBEC = 0x003D;/* Xebec Multimedia Solutions Limited */
        internal const int WAVE_FORMAT_G721_ADPCM = 0x0040;/* Antex Electronics Corporation */
        internal const int WAVE_FORMAT_G728_CELP = 0x0041;/* Antex Electronics Corporation */
        internal const int WAVE_FORMAT_MSG723 = 0x0042;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_MPEG = 0x0050;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_RT24 = 0x0052;/* InSoft, Inc. */
        internal const int WAVE_FORMAT_PAC = 0x0053;/* InSoft, Inc. */
        internal const int WAVE_FORMAT_MPEGLAYER3 = 0x0055;/* ISO/MPEG Layer3 Format Tag */
        internal const int WAVE_FORMAT_LUCENT_G723 = 0x0059;/* Lucent Technologies */
        internal const int WAVE_FORMAT_CIRRUS = 0x0060;/* Cirrus Logic */
        internal const int WAVE_FORMAT_ESPCM = 0x0061;/* ESS Technology */
        internal const int WAVE_FORMAT_VOXWARE = 0x0062;/* Voxware Inc */
        internal const int WAVE_FORMAT_CANOPUS_ATRAC = 0x0063;/* Canopus, co., Ltd. */
        internal const int WAVE_FORMAT_G726_ADPCM = 0x0064;/* APICOM */
        internal const int WAVE_FORMAT_G722_ADPCM = 0x0065;/* APICOM */
        internal const int WAVE_FORMAT_DSAT_DISPLAY = 0x0067;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_VOXWARE_BYTE_ALIGNED = 0x0069;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_AC8 = 0x0070;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_AC10 = 0x0071;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_AC16 = 0x0072;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_AC20 = 0x0073;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_RT24 = 0x0074;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_RT29 = 0x0075;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_RT29HW = 0x0076;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_VR12 = 0x0077;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_VR18 = 0x0078;/* Voxware Inc */
        internal const int WAVE_FORMAT_VOXWARE_TQ40 = 0x0079;/* Voxware Inc */
        internal const int WAVE_FORMAT_SOFTSOUND = 0x0080;/* Softsound, Ltd. */
        internal const int WAVE_FORMAT_VOXWARE_TQ60 = 0x0081;/* Voxware Inc */
        internal const int WAVE_FORMAT_MSRT24 = 0x0082;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_G729A = 0x0083;/* AT&T Labs, Inc. */
        internal const int WAVE_FORMAT_MVI_MVI2 = 0x0084;/* Motion Pixels */
        internal const int WAVE_FORMAT_DF_G726 = 0x0085;/* DataFusion Systems (Pty) (Ltd) */
        internal const int WAVE_FORMAT_DF_GSM610 = 0x0086;/* DataFusion Systems (Pty) (Ltd) */
        internal const int WAVE_FORMAT_ISIAUDIO = 0x0088;/* Iterated Systems, Inc. */
        internal const int WAVE_FORMAT_ONLIVE = 0x0089;/* OnLive! Technologies, Inc. */
        internal const int WAVE_FORMAT_SBC24 = 0x0091;/* Siemens Business Communications Sys */
        internal const int WAVE_FORMAT_DOLBY_AC3_SPDIF = 0x0092;/* Sonic Foundry */
        internal const int WAVE_FORMAT_MEDIASONIC_G723 = 0x0093;/* MediaSonic */
        internal const int WAVE_FORMAT_PROSODY_8KBPS = 0x0094;/* Aculab plc */
        internal const int WAVE_FORMAT_ZYXEL_ADPCM = 0x0097;/* ZyXEL Communications, Inc. */
        internal const int WAVE_FORMAT_PHILIPS_LPCBB = 0x0098;/* Philips Speech Processing */
        internal const int WAVE_FORMAT_PACKED = 0x0099;/* Studer Professional Audio AG */
        internal const int WAVE_FORMAT_MALDEN_PHONYTALK = 0x00A0;/* Malden Electronics Ltd. */
        internal const int WAVE_FORMAT_RHETOREX_ADPCM = 0x0100;/* Rhetorex Inc. */
        internal const int WAVE_FORMAT_IRAT = 0x0101;/* BeCubed Software Inc. */
        internal const int WAVE_FORMAT_VIVO_G723 = 0x0111;/* Vivo Software */
        internal const int WAVE_FORMAT_VIVO_SIREN = 0x0112;/* Vivo Software */
        internal const int WAVE_FORMAT_DIGITAL_G723 = 0x0123;/* Digital Equipment Corporation */
        internal const int WAVE_FORMAT_SANYO_LD_ADPCM = 0x0125;/* Sanyo Electric Co., Ltd. */
        internal const int WAVE_FORMAT_SIPROLAB_ACEPLNET = 0x0130;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_SIPROLAB_ACELP4800 = 0x0131;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_SIPROLAB_ACELP8V3 = 0x0132;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_SIPROLAB_G729 = 0x0133;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_SIPROLAB_G729A = 0x0134;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_SIPROLAB_KELVIN = 0x0135;/* Sipro Lab Telecom Inc. */
        internal const int WAVE_FORMAT_G726ADPCM = 0x0140;/* Dictaphone Corporation */
        internal const int WAVE_FORMAT_QUALCOMM_PUREVOICE = 0x0150;/* Qualcomm, Inc. */
        internal const int WAVE_FORMAT_QUALCOMM_HALFRATE = 0x0151;/* Qualcomm, Inc. */
        internal const int WAVE_FORMAT_TUBGSM = 0x0155;/* Ring Zero Systems, Inc. */
        internal const int WAVE_FORMAT_MSAUDIO1 = 0x0160;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMAUDIO2 = 0x0161;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMAUDIO3 = 0x0162;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMAUDIO_LOSSLESS = 0x0163;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_WMASPDIF = 0x0164;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_UNISYS_NAP_ADPCM = 0x0170;/* Unisys Corp. */
        internal const int WAVE_FORMAT_UNISYS_NAP_ULAW = 0x0171;/* Unisys Corp. */
        internal const int WAVE_FORMAT_UNISYS_NAP_ALAW = 0x0172;/* Unisys Corp. */
        internal const int WAVE_FORMAT_UNISYS_NAP_16K = 0x0173;/* Unisys Corp. */
        internal const int WAVE_FORMAT_CREATIVE_ADPCM = 0x0200;/* Creative Labs, Inc */
        internal const int WAVE_FORMAT_CREATIVE_FASTSPEECH8 = 0x0202;/* Creative Labs, Inc */
        internal const int WAVE_FORMAT_CREATIVE_FASTSPEECH10 = 0x0203;/* Creative Labs, Inc */
        internal const int WAVE_FORMAT_UHER_ADPCM = 0x0210;/* UHER informatic GmbH */
        internal const int WAVE_FORMAT_QUARTERDECK = 0x0220;/* Quarterdeck Corporation */
        internal const int WAVE_FORMAT_ILINK_VC = 0x0230;/* I-link Worldwide */
        internal const int WAVE_FORMAT_RAW_SPORT = 0x0240;/* Aureal Semiconductor */
        internal const int WAVE_FORMAT_ESST_AC3 = 0x0241;/* ESS Technology, Inc. */
        internal const int WAVE_FORMAT_GENERIC_PASSTHRU = 0x0249;
        internal const int WAVE_FORMAT_IPI_HSX = 0x0250;/* Interactive Products, Inc. */
        internal const int WAVE_FORMAT_IPI_RPELP = 0x0251;/* Interactive Products, Inc. */
        internal const int WAVE_FORMAT_CS2 = 0x0260;/* Consistent Software */
        internal const int WAVE_FORMAT_SONY_SCX = 0x0270;/* Sony Corp. */
        internal const int WAVE_FORMAT_FM_TOWNS_SND = 0x0300;/* Fujitsu Corp. */
        internal const int WAVE_FORMAT_BTV_DIGITAL = 0x0400;/* Brooktree Corporation */
        internal const int WAVE_FORMAT_QDESIGN_MUSIC = 0x0450;/* QDesign Corporation */
        internal const int WAVE_FORMAT_VME_VMPCM = 0x0680;/* AT&T Labs, Inc. */
        internal const int WAVE_FORMAT_TPC = 0x0681;/* AT&T Labs, Inc. */
        internal const int WAVE_FORMAT_OLIGSM = 0x1000;/* Ing C. Olivetti & C., S.p.A. */
        internal const int WAVE_FORMAT_OLIADPCM = 0x1001;/* Ing C. Olivetti & C., S.p.A. */
        internal const int WAVE_FORMAT_OLICELP = 0x1002;/* Ing C. Olivetti & C., S.p.A. */
        internal const int WAVE_FORMAT_OLISBC = 0x1003;/* Ing C. Olivetti & C., S.p.A. */
        internal const int WAVE_FORMAT_OLIOPR = 0x1004;/* Ing C. Olivetti & C., S.p.A. */
        internal const int WAVE_FORMAT_LH_CODEC = 0x1100;/* Lernout & Hauspie */
        internal const int WAVE_FORMAT_NORRIS = 0x1400;/* Norris Communications, Inc. */
        internal const int WAVE_FORMAT_SOUNDSPACE_MUSICOMPRESS = 0x1500;/* AT&T Labs, Inc. */
        internal const int WAVE_FORMAT_MPEG_ADTS_AAC = 0x1600;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_MPEG_RAW_AAC = 0x1601;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_NOKIA_MPEG_ADTS_AAC = 0x1608;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_NOKIA_MPEG_RAW_AAC = 0x1609;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_VODAFONE_MPEG_ADTS_AAC = 0x160A;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_VODAFONE_MPEG_RAW_AAC = 0x160B;/* Microsoft Corporation */
        internal const int WAVE_FORMAT_DVM = 0x2000;/* FAST Multimedia AG */
        /*wave filter*/
        internal const int WAVE_FILTER_UNKNOWN = 0x0000;
        internal const int WAVE_FILTER_DEVELOPMENT = (0xFFFF);
        internal const int WAVE_FILTER_VOLUME = 0x0001;
        internal const int WAVE_FILTER_ECHO = 0x0002;
        /*wave in and wave out params*/
        internal const int WAVE_FORMAT_QUERY = 0x0001;
        internal const int WAVE_ALLOWSYNC = 0x0002;
        internal const int WAVE_MAPPED = 0x0004;
        internal const int WAVE_FORMAT_DIRECT = 0x0008;
        internal const int WAVE_FORMAT_DIRECT_QUERY = (WAVE_FORMAT_QUERY | WAVE_FORMAT_DIRECT);
        /*global param*/
        internal const int CALLBACK_TYPEMASK = 0x00070000;    /* callback type mask */
        internal const int CALLBACK_NULL = 0x00000000;    /* no callback */
        internal const int CALLBACK_WINDOW = 0x00010000;    /* dwCallback is a HWND */
        internal const int CALLBACK_TASK = 0x00020000;   /* dwCallback is a HTASK */
        internal const int CALLBACK_FUNCTION = 0x00030000;   /* dwCallback is a FARPROC */
        internal const int CALLBACK_THREAD = (CALLBACK_TASK);/* thread ID replaces 16 bit task */
        internal const int CALLBACK_EVENT = 0x00050000; /* dwCallback is an EVENT Handle */
        /*wave in format*/
        internal const int WAVE_INVALIDFORMAT = 0x00000000;      /* invalid format */
        internal const int WAVE_FORMAT_1M08 = 0x00000001;       /* 11.025 kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_1S08 = 0x00000002;       /* 11.025 kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_1M16 = 0x00000004;       /* 11.025 kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_1S16 = 0x00000008;      /* 11.025 kHz, Stereo, 16-bit */
        internal const int WAVE_FORMAT_2M08 = 0x00000010;      /* 22.05  kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_2S08 = 0x00000020;     /* 22.05  kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_2M16 = 0x00000040;      /* 22.05  kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_2S16 = 0x00000080;      /* 22.05  kHz, Stereo, 16-bit */
        internal const int WAVE_FORMAT_4M08 = 0x00000100;      /* 44.1   kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_4S08 = 0x00000200;      /* 44.1   kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_4M16 = 0x00000400;      /* 44.1   kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_4S16 = 0x00000800;      /* 44.1   kHz, Stereo, 16-bit */
        internal const int WAVE_FORMAT_44M08 = 0x00000100;     /* 44.1   kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_44S08 = 0x00000200;     /* 44.1   kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_44M16 = 0x00000400;     /* 44.1   kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_44S16 = 0x00000800;     /* 44.1   kHz, Stereo, 16-bit */
        internal const int WAVE_FORMAT_48M08 = 0x00001000;     /* 48     kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_48S08 = 0x00002000;     /* 48     kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_48M16 = 0x00004000;     /* 48     kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_48S16 = 0x00008000;     /* 48     kHz, Stereo, 16-bit */
        internal const int WAVE_FORMAT_96M08 = 0x00010000;     /* 96     kHz, Mono,   8-bit  */
        internal const int WAVE_FORMAT_96S08 = 0x00020000;     /* 96     kHz, Stereo, 8-bit  */
        internal const int WAVE_FORMAT_96M16 = 0x00040000;     /* 96     kHz, Mono,   16-bit */
        internal const int WAVE_FORMAT_96S16 = 0x00080000;     /* 96     kHz, Stereo, 16-bit */
        internal const uint  MM_WIM_OPEN       =  0x3BE  ;         /* waveform input */
internal const uint  MM_WIM_CLOSE      =  0x3BF;
internal const uint  MM_WIM_DATA        = 0x3C0;
        internal const uint MM_WOM_OPEN     =    0x3BB           ;/* waveform output */
        internal const int MM_WOM_CLOSE        =0x3BC;
        internal const int MM_WOM_DONE         =0x3BD;
        internal const uint WAVE_MAPPER = unchecked(((uint) - 1));
/* flags for dwSupport field of WAVEOUTCAPS */
        internal const int WAVECAPS_PITCH           = 0x0001;   /* supports pitch control */
        internal const int WAVECAPS_PLAYBACKRATE   =0x0002;   /* supports playback rate control */
        internal const int WAVECAPS_VOLUME         =0x0004;   /* supports volume control */
        internal const int WAVECAPS_LRVOLUME       =0x0008;   /* separate left-right volume control */
        internal const int WAVECAPS_SYNC           =0x0010;
        internal const int WAVECAPS_SAMPLEACCURATE =0x0020;
        internal const int WAVERR_BASE            = 32;
/* waveform audio error return values */
        internal const int WAVERR_BADFORMAT = (WAVERR_BASE + 0);    /* unsupported wave format */
        internal const int  WAVERR_STILLPLAYING  = (WAVERR_BASE + 1);    /* still something playing */
        internal const int  WAVERR_UNPREPARED    = (WAVERR_BASE + 2);    /* header not prepared */
        internal const int  WAVERR_SYNC          = (WAVERR_BASE + 3);    /* device is synchronous */
        internal const int  WAVERR_LASTERROR     = (WAVERR_BASE + 3);    /* last error in range */
        internal const int DRV_LOAD             =   0x0001;
internal const int DRV_ENABLE              =0x0002;
internal const int DRV_OPEN                =0x0003;
internal const int DRV_CLOSE               =0x0004;
internal const int DRV_DISABLE             =0x0005;
internal const int DRV_FREE                =0x0006;
internal const int DRV_CONFIGURE           =0x0007;
internal const int DRV_QUERYCONFIGURE      =0x0008;
internal const int DRV_INSTALL             =0x0009;
internal const int DRV_REMOVE              =0x000A;
internal const int DRV_EXITSESSION         =0x000B;
internal const int DRV_POWER = 0x000F;
internal const int DRV_RESERVED            =0x0800;
internal const int DRV_USER                =0x4000;
    }
}

