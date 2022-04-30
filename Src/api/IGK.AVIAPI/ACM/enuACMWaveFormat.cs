

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuACMWaveFormat.cs
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
file:enuACMWaveFormat.cs
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
namespace IGK.AVIApi.ACM
{
    using IGK.ICore;using IGK.AVIApi.MMSYSTEM;
    public enum enuACMWaveFormat
    {
        Unknown = mmSystemAPI.  WAVE_FORMAT_UNKNOWN ,/* Microsoft Corporation */
        MpegLayer1 = mmSystemAPI . ACM_MPEG_LAYER1,
        ADPCM = mmSystemAPI. WAVE_FORMAT_ADPCM ,/* Microsoft Corporation */
        IEEE_Float= mmSystemAPI. WAVE_FORMAT_IEEE_FLOAT ,/* Microsoft Corporation */
        VSELP = mmSystemAPI. WAVE_FORMAT_VSELP ,/* Compaq Computer Corp. */
        IBM_CVSD = mmSystemAPI. WAVE_FORMAT_IBM_CVSD ,/* IBM Corporation */
        ALAW = mmSystemAPI. WAVE_FORMAT_ALAW ,/* Microsoft Corporation */
        MULAW = mmSystemAPI. WAVE_FORMAT_MULAW ,/* Microsoft Corporation */
        DTS = mmSystemAPI. WAVE_FORMAT_DTS ,/* Microsoft Corporation */
        DRM = mmSystemAPI. WAVE_FORMAT_DRM ,/* Microsoft Corporation */
        WMAVoice9 = mmSystemAPI. WAVE_FORMAT_WMAVOICE9 ,/* Microsoft Corporation */
        WMAVoice10= mmSystemAPI. WAVE_FORMAT_WMAVOICE10 ,/* Microsoft Corporation */
        OKI_ADPCM = mmSystemAPI. WAVE_FORMAT_OKI_ADPCM ,/* OKI */
        DVI_ADPCM= mmSystemAPI. WAVE_FORMAT_DVI_ADPCM ,/* Intel Corporation */
        IMA_ADPCM= mmSystemAPI. WAVE_FORMAT_IMA_ADPCM ,/*  Intel Corporation */
        MediaSpace_ADOCM = mmSystemAPI. WAVE_FORMAT_MEDIASPACE_ADPCM ,/* Videologic */
        SIERRA_ADPCM = mmSystemAPI. WAVE_FORMAT_SIERRA_ADPCM ,/* Sierra Semiconductor Corp */
        G723_ADPCM = mmSystemAPI. WAVE_FORMAT_G723_ADPCM ,/* Antex Electronics Corporation */
        DIGISTD = mmSystemAPI. WAVE_FORMAT_DIGISTD ,/* DSP Solutions, Inc. */
        DIGIFIX = mmSystemAPI. WAVE_FORMAT_DIGIFIX ,/* DSP Solutions, Inc. */
        DIALOGIC_OKI_ADPCM = mmSystemAPI. WAVE_FORMAT_DIALOGIC_OKI_ADPCM ,/* Dialogic Corporation */
        MediaVision_ADPCM = mmSystemAPI. WAVE_FORMAT_MEDIAVISION_ADPCM ,/* Media Vision, Inc. */
        CU_CODEC= mmSystemAPI. WAVE_FORMAT_CU_CODEC ,/* Hewlett-Packard Company */
        YAMAHA_ADPCM = mmSystemAPI. WAVE_FORMAT_YAMAHA_ADPCM ,/* Yamaha Corporation of America */
        SONARC = mmSystemAPI. WAVE_FORMAT_SONARC ,/* Speech Compression */
       DSPGROUP_TRUESPEECH  = mmSystemAPI. WAVE_FORMAT_DSPGROUP_TRUESPEECH ,/* DSP Group, Inc */
       ECHOSC1 = mmSystemAPI. WAVE_FORMAT_ECHOSC1 ,/* Echo Speech Corporation */
        AUDIOFILE_AF36 = mmSystemAPI. WAVE_FORMAT_AUDIOFILE_AF36 ,/* Virtual Music, Inc. */
        APTX = mmSystemAPI. WAVE_FORMAT_APTX ,/* Audio Processing Technology */
        AUDIOFILE_AF10= mmSystemAPI. WAVE_FORMAT_AUDIOFILE_AF10 ,/* Virtual Music, Inc. */
        PROSODY_1612= mmSystemAPI. WAVE_FORMAT_PROSODY_1612 ,/* Aculab plc */
       LRC = mmSystemAPI. WAVE_FORMAT_LRC ,/* Merging Technologies S.A. */
        DolbyAC2 = mmSystemAPI. WAVE_FORMAT_DOLBY_AC2 ,/* Dolby Laboratories */
        GSM610 = mmSystemAPI. WAVE_FORMAT_GSM610 ,/* Microsoft Corporation */
        MSAUDIO = mmSystemAPI. WAVE_FORMAT_MSNAUDIO ,/* Microsoft Corporation */
        ANTEX_ADPCME = mmSystemAPI. WAVE_FORMAT_ANTEX_ADPCME ,/* Antex Electronics Corporation */
        RES_VQLPC = mmSystemAPI. WAVE_FORMAT_CONTROL_RES_VQLPC ,/* Control Resources Limited */
        DIGI_REAL = mmSystemAPI. WAVE_FORMAT_DIGIREAL ,/* DSP Solutions, Inc. */
        DIGI_ADPCM= mmSystemAPI. WAVE_FORMAT_DIGIADPCM ,/* DSP Solutions, Inc. */
        Control_RES_CR10 = mmSystemAPI. WAVE_FORMAT_CONTROL_RES_CR10 ,/* Control Resources Limited */
        NMS_XBX_ADPCM= mmSystemAPI. WAVE_FORMAT_NMS_VBXADPCM ,/* Natural MicroSystems */
        CS_IMAADPCM= mmSystemAPI. WAVE_FORMAT_CS_IMAADPCM ,/* Crystal Semiconductor IMA ADPCM */
        ECHOSC3= mmSystemAPI. WAVE_FORMAT_ECHOSC3 ,/* Echo Speech Corporation */
        ROCKWELL_ADPCM= mmSystemAPI. WAVE_FORMAT_ROCKWELL_ADPCM ,/* Rockwell International */
        ROCKWELL_DIGITALK= mmSystemAPI. WAVE_FORMAT_ROCKWELL_DIGITALK ,/* Rockwell International */
        XEBEC= mmSystemAPI. WAVE_FORMAT_XEBEC ,/* Xebec Multimedia Solutions Limited */
        G721_ADPCM= mmSystemAPI. WAVE_FORMAT_G721_ADPCM ,/* Antex Electronics Corporation */
        G728_CELP= mmSystemAPI. WAVE_FORMAT_G728_CELP ,/* Antex Electronics Corporation */
        MSG723= mmSystemAPI. WAVE_FORMAT_MSG723 ,/* Microsoft Corporation */
        MPEG= mmSystemAPI. WAVE_FORMAT_MPEG ,/* Microsoft Corporation */
        RT24= mmSystemAPI. WAVE_FORMAT_RT24 ,/* InSoft, Inc. */
        PAC= mmSystemAPI. WAVE_FORMAT_PAC ,/* InSoft, Inc. */
        MPEGLAYER3= mmSystemAPI. WAVE_FORMAT_MPEGLAYER3 ,/* ISO/MPEG Layer3 Format Tag */
        LUCENT_G723= mmSystemAPI. WAVE_FORMAT_LUCENT_G723 ,/* Lucent Technologies */
        CIRRUS= mmSystemAPI. WAVE_FORMAT_CIRRUS ,/* Cirrus Logic */
        ESPCM= mmSystemAPI. WAVE_FORMAT_ESPCM ,/* ESS Technology */
        VOXWARE= mmSystemAPI. WAVE_FORMAT_VOXWARE ,/* Voxware Inc */
        CANOPUS_ATRAC= mmSystemAPI. WAVE_FORMAT_CANOPUS_ATRAC ,/* Canopus, co., Ltd. */
        G726_ADPCM= mmSystemAPI. WAVE_FORMAT_G726_ADPCM ,/* APICOM */
        G722_ADPCM= mmSystemAPI. WAVE_FORMAT_G722_ADPCM ,/* APICOM */
        DSAT_DISPLAY= mmSystemAPI. WAVE_FORMAT_DSAT_DISPLAY ,/* Microsoft Corporation */
        VOXWARE_BYTE_ALIGNED= mmSystemAPI. WAVE_FORMAT_VOXWARE_BYTE_ALIGNED ,/* Voxware Inc */
        VOXWARE_AC8= mmSystemAPI. WAVE_FORMAT_VOXWARE_AC8 ,/* Voxware Inc */
        VOXWARE_AC10= mmSystemAPI. WAVE_FORMAT_VOXWARE_AC10 ,/* Voxware Inc */
        VOXWARE_AC16= mmSystemAPI. WAVE_FORMAT_VOXWARE_AC16 ,/* Voxware Inc */
        VOXWARE_AC20= mmSystemAPI. WAVE_FORMAT_VOXWARE_AC20 ,/* Voxware Inc */
        VOXWARE_RT24= mmSystemAPI. WAVE_FORMAT_VOXWARE_RT24 ,/* Voxware Inc */
        VOXWARE_RT29= mmSystemAPI. WAVE_FORMAT_VOXWARE_RT29 ,/* Voxware Inc */
        VOXWARE_RT29HW= mmSystemAPI. WAVE_FORMAT_VOXWARE_RT29HW ,/* Voxware Inc */
        VOXWARE_VR12= mmSystemAPI. WAVE_FORMAT_VOXWARE_VR12 ,/* Voxware Inc */
        VOXWARE_VR18= mmSystemAPI. WAVE_FORMAT_VOXWARE_VR18 ,/* Voxware Inc */
        VOXWARE_TQ40= mmSystemAPI. WAVE_FORMAT_VOXWARE_TQ40 ,/* Voxware Inc */
        SOFTSOUND= mmSystemAPI. WAVE_FORMAT_SOFTSOUND ,/* Softsound, Ltd. */
        VOXWARE_TQ60= mmSystemAPI. WAVE_FORMAT_VOXWARE_TQ60 ,/* Voxware Inc */
        MSRT24= mmSystemAPI. WAVE_FORMAT_MSRT24 ,/* Microsoft Corporation */
        G729A= mmSystemAPI. WAVE_FORMAT_G729A ,/* AT&T Labs, Inc. */
        MVI_MVI2= mmSystemAPI. WAVE_FORMAT_MVI_MVI2 ,/* Motion Pixels */
        DF_G726= mmSystemAPI. WAVE_FORMAT_DF_G726 ,/* DataFusion Systems (Pty) (Ltd) */
        DF_GSM610= mmSystemAPI. WAVE_FORMAT_DF_GSM610 ,/* DataFusion Systems (Pty) (Ltd) */
        ISIAUDIO= mmSystemAPI. WAVE_FORMAT_ISIAUDIO ,/* Iterated Systems, Inc. */
        ONLIVE= mmSystemAPI. WAVE_FORMAT_ONLIVE ,/* OnLive! Technologies, Inc. */
        SBC24= mmSystemAPI. WAVE_FORMAT_SBC24 ,/* Siemens Business Communications Sys */
        DOLBY_AC3_SPDIF= mmSystemAPI. WAVE_FORMAT_DOLBY_AC3_SPDIF ,/* Sonic Foundry */
        MEDIASONIC_G723= mmSystemAPI. WAVE_FORMAT_MEDIASONIC_G723 ,/* MediaSonic */
        PROSODY_8KBPS= mmSystemAPI. WAVE_FORMAT_PROSODY_8KBPS ,/* Aculab plc */
        ZYXEL_ADPCM= mmSystemAPI. WAVE_FORMAT_ZYXEL_ADPCM ,/* ZyXEL Communications, Inc. */
        PHILIPS_LPCBB= mmSystemAPI. WAVE_FORMAT_PHILIPS_LPCBB ,/* Philips Speech Processing */
        PACKED= mmSystemAPI. WAVE_FORMAT_PACKED ,/* Studer Professional Audio AG */
        MALDEN_PHONYTALK= mmSystemAPI. WAVE_FORMAT_MALDEN_PHONYTALK ,/* Malden Electronics Ltd. */
        RHETOREX_ADPCM= mmSystemAPI. WAVE_FORMAT_RHETOREX_ADPCM ,/* Rhetorex Inc. */
        IRAT= mmSystemAPI. WAVE_FORMAT_IRAT ,/* BeCubed Software Inc. */
        VIVO_G723= mmSystemAPI. WAVE_FORMAT_VIVO_G723 ,/* Vivo Software */
        VIVO_SIREN= mmSystemAPI. WAVE_FORMAT_VIVO_SIREN ,/* Vivo Software */
        DIGITAL_G723= mmSystemAPI. WAVE_FORMAT_DIGITAL_G723 ,/* Digital Equipment Corporation */
        SANYO_LD_ADPCM= mmSystemAPI. WAVE_FORMAT_SANYO_LD_ADPCM ,/* Sanyo Electric Co., Ltd. */
        SIPROLAB_ACEPLNET= mmSystemAPI. WAVE_FORMAT_SIPROLAB_ACEPLNET ,/* Sipro Lab Telecom Inc. */
        SIPROLAB_ACELP4800= mmSystemAPI. WAVE_FORMAT_SIPROLAB_ACELP4800 ,/* Sipro Lab Telecom Inc. */
        SIPROLAB_ACELP8V3= mmSystemAPI. WAVE_FORMAT_SIPROLAB_ACELP8V3 ,/* Sipro Lab Telecom Inc. */
        SIPROLAB_G729= mmSystemAPI. WAVE_FORMAT_SIPROLAB_G729 ,/* Sipro Lab Telecom Inc. */
        SIPROLAB_G729A= mmSystemAPI. WAVE_FORMAT_SIPROLAB_G729A ,/* Sipro Lab Telecom Inc. */
        SIPROLAB_KELVIN= mmSystemAPI. WAVE_FORMAT_SIPROLAB_KELVIN ,/* Sipro Lab Telecom Inc. */
        G726ADPCM= mmSystemAPI. WAVE_FORMAT_G726ADPCM ,/* Dictaphone Corporation */
        QUALCOMM_PUREVOICE= mmSystemAPI. WAVE_FORMAT_QUALCOMM_PUREVOICE ,/* Qualcomm, Inc. */
        QUALCOMM_HALFRATE= mmSystemAPI. WAVE_FORMAT_QUALCOMM_HALFRATE ,/* Qualcomm, Inc. */
        TUBGSM= mmSystemAPI. WAVE_FORMAT_TUBGSM ,/* Ring Zero Systems, Inc. */
        MSAUDIO1= mmSystemAPI. WAVE_FORMAT_MSAUDIO1 ,/* Microsoft Corporation */
        WMAUDIO2= mmSystemAPI. WAVE_FORMAT_WMAUDIO2 ,/* Microsoft Corporation */
        WMAUDIO3= mmSystemAPI. WAVE_FORMAT_WMAUDIO3 ,/* Microsoft Corporation */
        WMAUDIO_LOSSLESS= mmSystemAPI. WAVE_FORMAT_WMAUDIO_LOSSLESS ,/* Microsoft Corporation */
        WMASPDIF= mmSystemAPI. WAVE_FORMAT_WMASPDIF ,/* Microsoft Corporation */
        UNISYS_NAP_ADPCM= mmSystemAPI. WAVE_FORMAT_UNISYS_NAP_ADPCM ,/* Unisys Corp. */
        UNISYS_NAP_ULAW= mmSystemAPI. WAVE_FORMAT_UNISYS_NAP_ULAW ,/* Unisys Corp. */
        UNISYS_NAP_ALAW= mmSystemAPI. WAVE_FORMAT_UNISYS_NAP_ALAW ,/* Unisys Corp. */
        UNISYS_NAP_16K= mmSystemAPI. WAVE_FORMAT_UNISYS_NAP_16K ,/* Unisys Corp. */
        CREATIVE_ADPCM= mmSystemAPI. WAVE_FORMAT_CREATIVE_ADPCM ,/* Creative Labs, Inc */
        CREATIVE_FASTSPEECH8= mmSystemAPI. WAVE_FORMAT_CREATIVE_FASTSPEECH8 ,/* Creative Labs, Inc */
        CREATIVE_FASTSPEECH10= mmSystemAPI. WAVE_FORMAT_CREATIVE_FASTSPEECH10 ,/* Creative Labs, Inc */
        UHER_ADPCM= mmSystemAPI. WAVE_FORMAT_UHER_ADPCM ,/* UHER informatic GmbH */
        QUARTERDECK= mmSystemAPI. WAVE_FORMAT_QUARTERDECK ,/* Quarterdeck Corporation */
        ILINK_VC= mmSystemAPI. WAVE_FORMAT_ILINK_VC ,/* I-link Worldwide */
        RAW_SPORT= mmSystemAPI. WAVE_FORMAT_RAW_SPORT ,/* Aureal Semiconductor */
        ESST_AC3= mmSystemAPI. WAVE_FORMAT_ESST_AC3 ,/* ESS Technology, Inc. */
        GENERIC_PASSTHRU= mmSystemAPI. WAVE_FORMAT_GENERIC_PASSTHRU ,
        IPI_HSX= mmSystemAPI. WAVE_FORMAT_IPI_HSX ,/* Interactive Products, Inc. */
        IPI_RPELP= mmSystemAPI. WAVE_FORMAT_IPI_RPELP ,/* Interactive Products, Inc. */
        CS2= mmSystemAPI. WAVE_FORMAT_CS2 ,/* Consistent Software */
        SONY_SCX= mmSystemAPI. WAVE_FORMAT_SONY_SCX ,/* Sony Corp. */
        FM_TOWNS_SND= mmSystemAPI. WAVE_FORMAT_FM_TOWNS_SND ,/* Fujitsu Corp. */
        BTV_DIGITAL= mmSystemAPI. WAVE_FORMAT_BTV_DIGITAL ,/* Brooktree Corporation */
        QDESIGN_MUSIC= mmSystemAPI. WAVE_FORMAT_QDESIGN_MUSIC ,/* QDesign Corporation */
        VME_VMPCM= mmSystemAPI. WAVE_FORMAT_VME_VMPCM ,/* AT&T Labs, Inc. */
        TPC= mmSystemAPI. WAVE_FORMAT_TPC ,/* AT&T Labs, Inc. */
        OLIGSM= mmSystemAPI. WAVE_FORMAT_OLIGSM ,/* Ing C. Olivetti & C., S.p.A. */
        OLIADPCM= mmSystemAPI. WAVE_FORMAT_OLIADPCM ,/* Ing C. Olivetti & C., S.p.A. */
        OLICELP= mmSystemAPI. WAVE_FORMAT_OLICELP ,/* Ing C. Olivetti & C., S.p.A. */
        OLISBC= mmSystemAPI. WAVE_FORMAT_OLISBC ,/* Ing C. Olivetti & C., S.p.A. */
        OLIOPR= mmSystemAPI. WAVE_FORMAT_OLIOPR ,/* Ing C. Olivetti & C., S.p.A. */
        LH_CODEC= mmSystemAPI. WAVE_FORMAT_LH_CODEC ,/* Lernout & Hauspie */
        NORRIS= mmSystemAPI. WAVE_FORMAT_NORRIS ,/* Norris Communications, Inc. */
        SOUNDSPACE_MUSICOMPRESS= mmSystemAPI. WAVE_FORMAT_SOUNDSPACE_MUSICOMPRESS ,/* AT&T Labs, Inc. */
        MPEG_ADTS_AAC= mmSystemAPI. WAVE_FORMAT_MPEG_ADTS_AAC ,/* Microsoft Corporation */
        MPEG_RAW_AAC= mmSystemAPI. WAVE_FORMAT_MPEG_RAW_AAC ,/* Microsoft Corporation */
        NOKIA_MPEG_ADTS_AAC= mmSystemAPI. WAVE_FORMAT_NOKIA_MPEG_ADTS_AAC ,/* Microsoft Corporation */
        NOKIA_MPEG_RAW_AAC= mmSystemAPI. WAVE_FORMAT_NOKIA_MPEG_RAW_AAC ,/* Microsoft Corporation */
        VODAFONE_MPEG_ADTS_AAC= mmSystemAPI. WAVE_FORMAT_VODAFONE_MPEG_ADTS_AAC ,/* Microsoft Corporation */
        VODAFONE_MPEG_RAW_AAC= mmSystemAPI. WAVE_FORMAT_VODAFONE_MPEG_RAW_AAC ,/* Microsoft Corporation */
        DVM= mmSystemAPI. WAVE_FORMAT_DVM ,/* FAST Multimedia AG */
    }
}

