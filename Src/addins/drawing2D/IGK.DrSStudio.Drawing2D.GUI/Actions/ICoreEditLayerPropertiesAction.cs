using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.Actions
{
    public interface ICoreEditLayerPropertiesAction : ICoreAction
    {
        ICore2DDrawingLayer Layer { get; set; }
    }
}
