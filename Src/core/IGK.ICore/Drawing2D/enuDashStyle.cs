

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuDashStyle.cs
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
file:enuDashStyle.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enuDashStyle
    {
        // Résumé :
        //     Spécifie une ligne pleine.
        Solid = 0,
        //
        // Résumé :
        //     Spécifie une ligne constituée de tirets.
        Dash = 1,
        //
        // Résumé :
        //     Spécifie une ligne constituée de points.
        Dot = 2,
        //
        // Résumé :
        //     Spécifie une ligne constituée d'un motif tiret-point répété.
        DashDot = 3,
        //
        // Résumé :
        //     Spécifie une ligne constituée d'un motif tiret-point-point répété.
        DashDotDot = 4,
        //
        // Résumé :
        //     Spécifie un style de ligne personnalisé défini par l'utilisateur.
        Custom = 5,
    }
}

