using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.IO.Font
{
    public struct CoreFontGlyphPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public override string ToString()
        {
            return string.Format("{0};{1}", this.X, this.Y);
        }
    }
}
