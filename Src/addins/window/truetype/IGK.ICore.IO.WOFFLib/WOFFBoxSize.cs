namespace IGK.ICore.IO
{
    public struct WOFFBoxSize
    {
        public int Width { get; set; }
        public int Height { get; set; }

        ///<summary>
        ///public .ctr
        ///</summary>
        public WOFFBoxSize(int w, int h)
        {
            this.Width = w;
            this.Height = h;
        }
    }
}