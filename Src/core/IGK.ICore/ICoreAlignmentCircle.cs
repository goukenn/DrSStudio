namespace IGK.ICore
{
    public interface ICoreAlignmentCircle
    {
        Matrix GetMatrix();
        Rectanglef GetAlignmentBound();
        Vector2f Center { get; }

    }
}