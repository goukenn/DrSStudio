

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMCIWaveOutSupport.cs
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
file:enuMCIWaveOutSupport.cs
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
    public enum enuMCIWaveOutSupport
    {
Pitch = mmSystemAPI . WAVECAPS_PITCH         ,
PlayBackRate = mmSystemAPI.WAVECAPS_PLAYBACKRATE,
Volume = mmSystemAPI.WAVECAPS_VOLUME,
LRVolume = mmSystemAPI.WAVECAPS_LRVOLUME,
Sync = mmSystemAPI.WAVECAPS_SYNC,
SampleAccurate = mmSystemAPI.WAVECAPS_SAMPLEACCURATE,
    }
}

