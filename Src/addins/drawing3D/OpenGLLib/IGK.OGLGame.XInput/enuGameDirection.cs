using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.XInput
{
    [Flags]
    public enum enuGameDirection: int
    {
        None = 0,
        Left = 1,
        Up = 2,
        Right = 4,
        Down = 8
    }
}
