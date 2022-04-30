

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuMouseButtons.cs
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
file:enuMouseButtons.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    [Flags()]
    public enum enuMouseButtons
    {
                // Résumé :
        //     Aucun bouton de la souris n'a été enfoncé.
        None = 0,
        //
        // Résumé :
        //     Le bouton gauche de la souris a été enfoncé.
        Left = 1048576,
        //
        // Résumé :
        //     Le bouton droit de la souris a été enfoncé.
        Right = 2097152,
        //
        // Résumé :
        //     Le bouton central de la souris a été enfoncé.
        Middle = 4194304,
        //
        // Résumé :
        //     Le premier IGKXButton a été enfoncé.
        XButton1 = 8388608,
        //
        // Résumé :
        //     Le second IGKXButton a été enfoncé.
        XButton2 = 16777216,
    }
}

