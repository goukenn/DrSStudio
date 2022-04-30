using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    public  interface ICore2DDrawingInvetibleElement : ICore2DDrawingElement
    {
        void Invert();
    }
}
