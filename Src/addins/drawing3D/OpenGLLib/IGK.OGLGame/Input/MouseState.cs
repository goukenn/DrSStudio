using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.Input
{
    public struct MouseState
    {
        internal enuGLGameMouseButton m_button;
        internal enuMouseState m_state;
        public static readonly MouseState Empty;

        static MouseState()
        {
            Empty = new MouseState(enuGLGameMouseButton.None, enuMouseState.Up);

        }

        public MouseState(enuGLGameMouseButton enuMouseButton, enuMouseState enuMouseState)
        {
            this.m_button = enuMouseButton;
            this.m_state = enuMouseState;
        }

        public enuGLGameMouseButton Button {get{return m_button; } }
        public enuMouseState State {get{return m_state; } }
    }
}
