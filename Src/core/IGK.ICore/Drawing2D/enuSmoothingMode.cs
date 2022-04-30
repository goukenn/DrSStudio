

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuSmoothingMode.cs
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
file:enuSmoothingMode.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    [DefaultValue(enuSmoothingMode.AntiAliazed )]
    public enum enuSmoothingMode : int
    {
        None =0 ,
        //HightQuality = SmoothingMode.HighQuality ,
        //HightSpeed = SmoothingMode.HighSpeed ,
        AntiAliazed = 4 
            /*
          //
        // Résumé :
        //     Spécifie un rendu sans anticrénelage.
        Default = 0,
        //
        // Résumé :
        //     Spécifie un rendu sans anticrénelage.
        HighSpeed = 1,
        //
        // Résumé :
        //     Spécifie un rendu utilisant l'anticrénelage.
        HighQuality = 2,
        //
        // Résumé :
        //     Spécifie un rendu sans anticrénelage.
        None = 3,
        //
        // Résumé :
        //     Spécifie un rendu utilisant l'anticrénelage.
        AntiAlias = 4,*/
    }
}

