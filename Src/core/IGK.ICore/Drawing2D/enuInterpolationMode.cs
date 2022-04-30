

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuInterpolationMode.cs
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
file:enuInterpolationMode.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent the gdi interpolation mode
    /// </summary>
    public enum enuInterpolationMode
    {
        None = 0,
        Bicubic = 4,
        Bilinear = 3,
        Hight = 2,
        Low =1,
        HightQualityBicubic = 7,
        HightQualityBilinear = 6,
        Pixel = 5

        //        // Résumé :
        ////     Équivaut aux éléments System.Drawing.Drawing2D.QualityMode.Invalid de l'énumération
        ////     System.Drawing.Drawing2D.QualityMode.
        //Invalid = -1,
        ////
        //// Résumé :
        ////     Spécifie le mode par défaut.
        //Default = 0,
        ////
        //// Résumé :
        ////     Spécifie une interpolation de qualité inférieure.
        //Low = 1,
        ////
        //// Résumé :
        ////     Spécifie une interpolation haute qualité.
        //High = 2,
        ////
        //// Résumé :
        ////     Spécifie une interpolation bilinéaire. Aucun préfiltrage n'est effectué.
        ////     Ce mode n'est pas adapté à la réduction d'une image au-dessous de 50 pour
        ////     cent de sa taille d'origine.
        //Bilinear = 3,
        ////
        //// Résumé :
        ////     Spécifie une interpolation bicubique. Aucun préfiltrage n'est effectué. Ce
        ////     mode n'est pas adapté à la réduction d'une image au-dessous de 25 pour cent
        ////     de sa taille d'origine.
        //Bicubic = 4,
        ////
        //// Résumé :
        ////     Spécifie l'interpolation du voisin le plus proche.
        //NearestNeighbor = 5,
        ////
        //// Résumé :
        ////     Spécifie une interpolation bilinéaire haute qualité. Le préfiltrage est effectué
        ////     afin de garantir une réduction de haute qualité.
        //HighQualityBilinear = 6,
        ////
        //// Résumé :
        ////     Spécifie une interpolation bicubique haute qualité. Le préfiltrage est effectué
        ////     afin de garantir une réduction de haute qualité. Ce mode produit la qualité
        ////     de transformation d'images la plus élevée.
        //HighQualityBicubic = 7,
    }
}

