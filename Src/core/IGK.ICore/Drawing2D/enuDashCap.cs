

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuDashCap.cs
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
file:enuDashCap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enuDashCap : uint
    {
        // Résumé :
        //     Spécifie un bout carré qui termine les deux extrémités de chaque tiret.
        Flat = 0,
        //
        // Résumé :
        //     Spécifie un bout circulaire qui termine les deux extrémités de chaque tiret.
        Round = 2,
        //
        // Résumé :
        //     Spécifie un bout triangulaire qui termine les deux extrémités de chaque tiret.
        Triangle = 3,
    }
}

