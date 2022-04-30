
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a ICoreCodeBarElement
    /// </summary>
    public interface ICoreCodeBarElement :
        ICore2DDrawingLayeredElement, 
        ICodeBar,
        ICore2DRectangleElement 
    {
    }
}
