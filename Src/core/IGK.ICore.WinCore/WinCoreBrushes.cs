using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinCore
{
    public static class WinCoreBrushes
    {
        public static Brush GetBrushes(string name)
        {
            ICore2DDrawingDocument sm_doc = CoreResources.GetDocument(name);// "dash_gkds");
            if (sm_doc != null)
            {
                return WinCoreBrushRegister.GetBrush(sm_doc);
            }
            return null;
        }
    }
}
