using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    public abstract class Core2DPathUtilsBase : ICoreD2DPathUtils
    {
        public abstract bool IsVisible(Vector2f[] points, byte[] tab, enuFillMode mode, Vector2f point);

        public abstract bool IsVisible(ICoreGraphicsPath path, Vector2f point);

        public abstract bool IsOutlineVisible(ICoreGraphicsPath path, Vector2f point);

        public abstract  bool IsOutlineVisible(Vector2f[] points, byte[] tab, enuFillMode mode, Vector2f point, float PenWidth);

        public abstract ICore2DPathDefinition Widen(CoreGraphicsPath coreGraphicsPath, ICorePen pen, Matrix matrix, float flatness);

        public abstract  ICore2DPathDefinition Flatten(CoreGraphicsPath coreGraphicsPath, ICorePen pen, Matrix matrix, float flatness);

        public abstract ICore2DPathDefinition Warp(CoreGraphicsPath path, Vector2f[] tab, Rectanglef bounds, Matrix matrix, enuWarpMode warpMode, float Flatness);        

        public abstract  ICoreDrawingPath CreateNewPath();

        public abstract ICore2DDrawingPathMeasurer Measurer
        {
            get;
        }


        public abstract ICoreGraphicsPath Reverse(ICoreGraphicsPath coreGraphicsPath);

        public abstract void CreateNewRegion(CoreGraphicsPath cp, ICoreRegionBuildListener coreRegionData);
    }
}
