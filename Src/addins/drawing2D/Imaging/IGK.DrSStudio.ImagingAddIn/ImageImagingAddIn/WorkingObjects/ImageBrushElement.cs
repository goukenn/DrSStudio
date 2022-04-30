using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.Imaging.Mecanism;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// represent a single brush element editor
    /// </summary>
    [Core2DDrawingImageItemAttribute(
        "ImageBrush",
        typeof(Mecanism),
        ImageKey = "img_copy",
        IsVisible = true)]
    class ImageBrushElement : RectangleElement
    {

        new class Mecanism : ImageMecanismBase<ImageBrushElement>
        {
        }
    }
}
