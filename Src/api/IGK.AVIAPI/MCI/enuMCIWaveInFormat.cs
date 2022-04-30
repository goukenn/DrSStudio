

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMCIWaveInFormat.cs
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
file:enuMCIWaveInFormat.cs
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
    using MMSYSTEM;
    [Flags ()]
    public enum enuMCIWaveInFormat : int
    {
InvalidFormat = mmSystemAPI . WAVE_INVALIDFORMAT,
F108= mmSystemAPI .WAVE_FORMAT_1M08 ,
FS08= mmSystemAPI .WAVE_FORMAT_1S08 ,
F1M16= mmSystemAPI .WAVE_FORMAT_1M16 ,
F1S16= mmSystemAPI .WAVE_FORMAT_1S16 ,
F2M08= mmSystemAPI .WAVE_FORMAT_2M08 ,
F2S08= mmSystemAPI .WAVE_FORMAT_2S08 ,
F2M16= mmSystemAPI .WAVE_FORMAT_2M16 ,
F2S16= mmSystemAPI. WAVE_FORMAT_2S16 ,
F4M08= mmSystemAPI . WAVE_FORMAT_4M08 ,
F4S08 =mmSystemAPI. WAVE_FORMAT_4S08 ,
F4M16=mmSystemAPI.WAVE_FORMAT_4M16 ,
F4S16=mmSystemAPI.WAVE_FORMAT_4S16 ,
F44M08= mmSystemAPI .WAVE_FORMAT_44M08,
F44S08= mmSystemAPI .WAVE_FORMAT_44S08,
F44M16= mmSystemAPI .WAVE_FORMAT_44M16,
F44S16= mmSystemAPI .WAVE_FORMAT_44S16,
F48M08= mmSystemAPI .WAVE_FORMAT_48M08,
F48S08= mmSystemAPI .WAVE_FORMAT_48S08,
F48M16= mmSystemAPI .WAVE_FORMAT_48M16,
F48S16= mmSystemAPI .WAVE_FORMAT_48S16,
F96M08= mmSystemAPI .WAVE_FORMAT_96M08,
F96S08= mmSystemAPI .WAVE_FORMAT_96S08,
F96M16= mmSystemAPI .WAVE_FORMAT_96M16,
F96S16 = mmSystemAPI.WAVE_FORMAT_96S16
    }
}

