using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// indicate that a item support a selection element mecanism
    /// </summary>
    public interface  ICore2DDrawingSelectionContainer
    {
        ICoreWorkingElementCollections Elements { get; } 
    }
}
