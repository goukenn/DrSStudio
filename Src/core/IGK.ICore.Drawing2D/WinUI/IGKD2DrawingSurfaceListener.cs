using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.WinUI
{
    public interface IIGKD2DrawingSurfaceListener
    {
        void UnregisterService(IGKD2DDrawingSurfaceBase surface);
        void RegisterService(IGKD2DDrawingSurfaceBase surface);
    }
}
