

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuContextCloseReason.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a context close reason
    /// </summary>
    public enum enuContextCloseReason
    {
        // Résumé :
        //     Spécifie que le contrôle System.Windows.Forms.ToolStripDropDown a été fermé
        //     parce qu'une autre application a reçu le focus.
        AppFocusChange = 0,
        //
        // Résumé :
        //     Spécifie que le contrôle System.Windows.Forms.ToolStripDropDown a été fermé
        //     parce qu'une application a été lancée.
        AppClicked = 1,
        //
        // Résumé :
        //     Spécifie que le contrôle System.Windows.Forms.ToolStripDropDown a été fermé
        //     parce qu'un clic a été effectué sur l'un de ses éléments.
        ItemClicked = 2,
        //
        // Résumé :
        //     Spécifie que le contrôle System.Windows.Forms.ToolStripDropDown a été fermé
        //     suite à une activité du clavier, par exemple si la touche ÉCHAP est enfoncée.
        Keyboard = 3,
        //
        // Résumé :
        //     Spécifie que le contrôle System.Windows.Forms.ToolStripDropDown a été fermé
        //     parce que la méthode System.Windows.Forms.ToolStripDropDown.Close() a été
        //     appelée.
        CloseCalled = 4,
    }
}
