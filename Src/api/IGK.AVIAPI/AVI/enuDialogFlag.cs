

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuDialogFlag.cs
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
file:enuDialogFlag.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.AVIApi.AVI
{
    [Flags()]
   public enum enuDialogFlag : uint
    {
       None = 0,
      DataRate = AVIApi.ICMF_CHOOSE_DATARATE,
      Preview = AVIApi.ICMF_CHOOSE_PREVIEW ,
      KeyFrame = AVIApi.ICMF_CHOOSE_KEYFRAME,
        All = DataRate | enuDialogFlag.KeyFrame | enuDialogFlag.Preview 
    }
}

