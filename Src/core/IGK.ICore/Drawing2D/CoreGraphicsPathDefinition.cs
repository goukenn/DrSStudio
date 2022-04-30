using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    public struct CoreGraphicsPathDefinition
    {
        public byte[] Types { get; set; }

        public Vector2f[] Points { get; set; }
    }
}
