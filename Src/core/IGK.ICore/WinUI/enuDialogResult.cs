

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuDialogResult.cs
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
file:enuDialogResult.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent the dialog result
    /// </summary>
    public enum enuDialogResult
    {
        // Résumé :
        //     Nothing est retourné à partir de la boîte de dialogue. Cela signifie que
        //     l'exécution de la boîte de dialogue modale se poursuit.
        None = 0,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est OK (généralement
        //     transmise par un bouton intitulé OK).
        OK = 1,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est Cancel (généralement
        //     transmise par un bouton intitulé Annuler).
        Cancel = 2,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est Abort (généralement
        //     transmise par un bouton intitulé Abandonner).
        Abort = 3,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est Retry (généralement
        //     transmise par un bouton intitulé Réessayer).
        Retry = 4,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est Ignore (généralement
        //     transmise par un bouton intitulé Ignorer).
        Ignore = 5,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est Yes (généralement
        //     transmise par un bouton intitulé Oui).
        Yes = 6,
        //
        // Résumé :
        //     La valeur de retour à partir de la boîte de dialogue est No (généralement
        //     transmise par un bouton intitulé Non).
        No = 7,
    }
}

