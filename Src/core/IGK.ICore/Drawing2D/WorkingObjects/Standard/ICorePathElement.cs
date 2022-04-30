namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a path element
    /// </summary>
    public interface ICorePathElement
    {
        Rectanglef GetBound();
        Vector2f[] Points { get; }
        byte[] PointTypes { get; }
        CoreGraphicsPath GetPath();
        bool Contains(Vector2f point);
        void SetDefinition(CoreGraphicsPath path);
    }
}