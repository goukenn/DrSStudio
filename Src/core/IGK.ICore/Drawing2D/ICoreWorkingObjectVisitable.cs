using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public interface  ICoreWorkingObjectVisitable
    {
        bool Accept(ICoreWorkingObject visitor);

        void Visit(ICoreWorkingObject visitor);
    }
}
