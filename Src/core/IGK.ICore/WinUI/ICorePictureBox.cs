using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI
{
    public interface ICorePictureBox : ICoreControl
    {
        ICoreBitmap Bitmap { get; set; }
        enuZoomMode ZoomMode{ get; set; }
    }
}
