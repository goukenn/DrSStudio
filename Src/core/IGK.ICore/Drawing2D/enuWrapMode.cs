

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuWrapMode.cs
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
file:enuWrapMode.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enuWrapMode
    {
        // Résumé :
        //     Dispose le dégradé ou la texture en mosaïque.
        Tile = 0,
        //
        // Résumé :
        //     Retourne la texture ou le dégradé horizontalement, puis le dispose en mosaïque.
        TileFlipX = 1,
        //
        // Résumé :
        //     Retourne la texture ou le dégradé verticalement, puis le dispose en mosaïque.
        TileFlipY = 2,
        //
        // Résumé :
        //     Retourne la texture ou le dégradé horizontalement et verticalement, puis
        //     le dispose en mosaïque.
        TileFlipXY = 3,
        //
        // Résumé :
        //     La texture ou le dégradé n'est pas disposé en mosaïque.
        Clamp = 4,
    }
}

