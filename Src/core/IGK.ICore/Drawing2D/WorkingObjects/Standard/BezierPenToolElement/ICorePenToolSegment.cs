namespace IGK.ICore.Drawing2D
{
    public interface ICorePenToolSegment
    {
        Vector2f Point { get; set; }
        Vector2f HandleIn { get; set; }
        Vector2f HandleOut { get; set; }
    }
}