using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.XInput
{
    public enum enuGameButton
    {
        A = Native.ButtonFlags.XINPUT_GAMEPAD_A,
        B= Native.ButtonFlags.XINPUT_GAMEPAD_B,
        X = Native.ButtonFlags.XINPUT_GAMEPAD_X,
        Y = Native.ButtonFlags.XINPUT_GAMEPAD_Y,
        Back = Native.ButtonFlags.XINPUT_GAMEPAD_BACK,
        Start = Native.ButtonFlags.XINPUT_GAMEPAD_START,
        Up = Native.ButtonFlags.XINPUT_GAMEPAD_DPAD_UP ,
        Down = Native.ButtonFlags.XINPUT_GAMEPAD_DPAD_DOWN,
        Left = Native.ButtonFlags.XINPUT_GAMEPAD_DPAD_LEFT,
        Right = Native.ButtonFlags.XINPUT_GAMEPAD_DPAD_RIGHT,
        LThumb = Native.ButtonFlags.XINPUT_GAMEPAD_LEFT_THUMB,
        RThumb = Native.ButtonFlags.XINPUT_GAMEPAD_RIGHT_THUMB,
        LShoulder = Native.ButtonFlags.XINPUT_GAMEPAD_LEFT_SHOULDER,
        RShoulder = Native.ButtonFlags.XINPUT_GAMEPAD_RIGHT_SHOULDER 
    }
}
