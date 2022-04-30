using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// pen tool segment
    /// </summary>
    public class PenToolSegment: ICorePenToolSegment
    {
        public Vector2f Point { get; set; }
        public Vector2f HandleIn { get; set; }
        public Vector2f HandleOut { get; set; }
        /// <summary>
        /// indicate that the segment is used to close the path
        /// </summary>
        public bool Close { get; internal set; }
    }
}
