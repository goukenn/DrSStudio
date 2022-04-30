

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuLineCap.cs
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
file:enuLineCap.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a line cap
    /// </summary>
    public enum enuLineCap
    {
        // Résumé :
        //     Spécifie une extrémité de ligne à deux dimensions (flat).
        Flat = 0,
        //
        // Résumé :
        //     Spécifie un embout de ligne carré.
        Square = 1,
        //
        // Résumé :
        //     Spécifie un embout de ligne arrondi.
        Round = 2,
        //
        // Résumé :
        //     Spécifie un embout de ligne triangulaire.
        Triangle = 3,
        //
        // Résumé :
        //     Spécifie qu'il n'y a pas d'ancrage.
        NoAnchor = 16,
        //
        // Résumé :
        //     Spécifie un embout d'ancrage carré.
        SquareAnchor = 17,
        //
        // Résumé :
        //     Spécifie un embout d'ancrage arrondi.
        RoundAnchor = 18,
        //
        // Résumé :
        //     Spécifie un embout d'ancrage en forme de losange.
        DiamondAnchor = 19,
        //
        // Résumé :
        //     Spécifie un embout d'ancrage en forme de flèche.
        ArrowAnchor = 20,
        //
        // Résumé :
        //     Spécifie un masque utilisé pour vérifier si un embout de ligne est un embout
        //     d'ancrage.
        AnchorMask = 240,
        //
        // Résumé :
        //     Spécifie un embout de ligne personnalisé.
        Custom = 255,
    }
}

