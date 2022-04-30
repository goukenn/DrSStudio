using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.XInput
{
    class GamePadControllerStateChangedEventArgs : EventArgs 
    {
        public Native.XInputState CurrentInputState { get; set; }

        public Native.XInputState PreviousInputState { get; set; }
    }
}
