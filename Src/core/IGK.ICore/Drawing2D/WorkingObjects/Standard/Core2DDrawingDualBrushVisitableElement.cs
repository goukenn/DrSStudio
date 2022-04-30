using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{
    public abstract class Core2DDrawingDualBrushVisitableElement : Core2DDrawingDualBrushElement,
        ICore2DDrawingVisitable 
    {

        public virtual bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public virtual void Visit(ICore2DDrawingVisitor visitor)
        {
            
        }
    }
}
