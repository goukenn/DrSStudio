using IGK.OGLGame.XInput.Native;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.XInput
{
    /// <summary>
    /// get game state
    /// </summary>
    public  class GamePadState
    {
        private XInputState m_currentState;
        private XInputState m_prevState;
        private GamePadController m_pad;
        private bool m_stateChanged;
        internal GamePadState()
        {
        }
       

        internal void UpdateState(GamePadController c)
        {
            if (this.m_pad == null)
            {
                this.m_pad = c;
                this.m_pad.StateChanged += m_pad_StateChanged;
                this.m_stateChanged = true;
            }

            m_prevState = m_currentState;
            ///copy the current state
            if (this.m_stateChanged)
            {                
                m_currentState = c.CurrentState;
                //m_prevState = c.PrevState;
                this.m_stateChanged = false;
            }
        }

        void m_pad_StateChanged(object sender, GamePadControllerStateChangedEventArgs e)
        {
            this.m_stateChanged = true;
        }

        public bool IsPressed(enuGameButton button)
        {
            return m_currentState.Gamepad.IsButtonPressed((int)button);
        }

        public bool IsRelease(enuGameButton button)
        {
            return 
                m_prevState.Gamepad.IsButtonPresent ((int)button) && 
                !m_currentState.Gamepad.IsButtonPressed((int)button);
        }
        public float ThumbLX { get { return this.m_currentState.Gamepad.sThumbLX; } }
        public float ThumbLY { get { return this.m_currentState.Gamepad.sThumbLY; } }
        public float ThumbRX { get { return this.m_currentState.Gamepad.sThumbRX; } }
        public float ThumbRY { get { return this.m_currentState.Gamepad.sThumbRY; } }


        /// <summary>
        /// get the direction
        /// </summary>
        public enuGameDirection Direction{get{
            enuGameDirection c = enuGameDirection.None;
            if (IsPressed(enuGameButton.Left) || (ThumbLX < -100))
                c |= enuGameDirection.Left;
            if (IsPressed(enuGameButton.Right ) || (ThumbLX > 100))
                c |= enuGameDirection.Right;
            if (IsPressed(enuGameButton.Up) || (ThumbLY > 100))
                c |= enuGameDirection.Up ;
            if (IsPressed(enuGameButton.Down) || (ThumbLY < -100))
                c |= enuGameDirection.Down;
            return c;
        }}

        public int LeftTrigger
        {
            get { return (int)this.m_currentState.Gamepad.bLeftTrigger; }
        }

        public int RightTrigger
        {
            get { return (int)this.m_currentState.Gamepad.bRightTrigger; }
        }
        /// <summary>
        /// send vibration command to gamepad
        /// </summary>
        /// <param name="motor1"></param>
        /// <param name="motor2"></param>
        /// <param name="timeSpan"></param>
        public void Vibrate(int motor1, int motor2, TimeSpan timeSpan)
        {
            if (this.m_pad !=null)
            this.m_pad.Vibrate(motor1, motor2, timeSpan);
        }
    }
}
