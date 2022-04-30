using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public enum enuPreserveFontType
    {
        /// <summary>
        /// aucun ajustement nécessaire. la police sera étirer en fonction de x, y
        /// </summary>
        None, 
        /// <summary>
        /// la taille initiale de la police est préserver
        /// </summary>
        InitialFontSize,
        /// <summary>
        /// la taille de la police est mis en echelle. en fonction de maxima entre x, et y
        /// </summary>
        PreserveScaleMaxXY
    }
}
