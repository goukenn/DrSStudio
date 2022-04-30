using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// for rendering selection view
    /// </summary>
    public interface ICore2DDrawingSelectionView
    {
        void RenderSelection(ICore2DDrawingSelectionHost host);
    }
}
