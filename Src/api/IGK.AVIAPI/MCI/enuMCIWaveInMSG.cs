

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMCIWaveInMSG.cs
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
file:enuMCIWaveInMSG.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.MCI
{
    public enum enuMCIWaveInMSG
    {
        Open = MCIWaveInApi .WIM_OPEN,
        Data = MCIWaveInApi.WIM_DATA,
        Close = MCIWaveInApi.WIM_CLOSE,
    }
}

