using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu
{
    [CoreMenu("Edit.Invert", 0x30)]
    class InvertElementMenu : SelecteElementMenuBase
    {
        public new ICore2DDrawingInvetibleElement Element => base.Element as ICore2DDrawingInvetibleElement;

        protected override bool PerformAction()
        {
            this.Element?.Invert();
            return false;
        }
    }
}
