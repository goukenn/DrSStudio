

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuFormStartPosition.cs
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
file:enuFormStartPosition.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    public enum enuFormStartPosition
    {
        // Résumé :
        //     La position du formulaire est déterminée par la propriété System.Windows.Forms.Control.Location.
        Manual = 0,
        //
        // Résumé :
        //     Le formulaire est centré dans l'affichage actuel et possède les dimensions
        //     spécifiées dans la taille du formulaire.
        CenterScreen = 1,
        //
        // Résumé :
        //     Le formulaire est centré dans les limites de son formulaire parent.
        CenterParent = 4,
    }
}

