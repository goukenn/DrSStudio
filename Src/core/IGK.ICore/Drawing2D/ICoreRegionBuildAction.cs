using IGK.ICore.GraphicModels;

namespace IGK.ICore.Drawing2D
{
    public interface ICoreRegionBuildAction
    {
        void Complement(ICoreGraphicsPath data);
        void Intersect(ICoreGraphicsPath data);
        void Exclude(ICoreGraphicsPath data);
        void Init(ICoreGraphicsPath data);
        void Union(ICoreGraphicsPath data);
        void Xor(ICoreGraphicsPath data);
    }
}