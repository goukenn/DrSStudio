using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Drawing2D;

    public class Core2DDrawingBrushElementAttribute : Core2DDrawingStandardElementAttribute 
    {
        public override string GroupName
        {
            get
            {
                return "Brush";
            }
        }
        public Core2DDrawingBrushElementAttribute(string n, Type t):base(n, t)
        {

        }
    }
}
