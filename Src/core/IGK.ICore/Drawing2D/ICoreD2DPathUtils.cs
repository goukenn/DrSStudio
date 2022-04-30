

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreD2DPathUtils.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICore2DDPathUtils.cs
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICoreD2DPathUtils
    {
        ICore2DDrawingPathMeasurer Measurer { get; }
        bool IsVisible(Vector2f[] points, byte[] tab, enuFillMode mode, Vector2f point);        
        bool IsVisible(ICoreGraphicsPath path, Vector2f point);

        bool IsOutlineVisible(ICoreGraphicsPath path, Vector2f point);
        bool IsOutlineVisible(Vector2f[] points, byte[] tab, enuFillMode mode, Vector2f point, float PenWidth);

        ICore2DPathDefinition Widen(CoreGraphicsPath coreGraphicsPath, ICorePen pen, Matrix matrix, float flatness);
        ICore2DPathDefinition Flatten(CoreGraphicsPath coreGraphicsPath, ICorePen pen, Matrix matrix, float flatness);

        ICore2DPathDefinition Warp(CoreGraphicsPath path, Vector2f[] tab,
            Rectanglef bounds, Matrix matrix, enuWarpMode warpMode, float Flatness);


        ICoreDrawingPath CreateNewPath();

        ICoreGraphicsPath Reverse(ICoreGraphicsPath coreGraphicsPath);
        void CreateNewRegion(CoreGraphicsPath cp, ICoreRegionBuildListener coreRegionData);
    }

   
}

