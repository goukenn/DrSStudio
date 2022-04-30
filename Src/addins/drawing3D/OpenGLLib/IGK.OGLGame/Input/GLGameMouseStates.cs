using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.Input
{
    /// <summary>
    /// represent a game input mouse state
    /// </summary>
    public static class GLGameMouseStates
    {
        static Dictionary<enuGLGameMouseButton, MouseState> sm_states;
        private static ICore.Vector2i sm_Location;
        static GLGameMouseStates() {
            sm_states = new Dictionary<enuGLGameMouseButton, MouseState>();
            foreach (enuGLGameMouseButton item in Enum.GetValues(typeof(enuGLGameMouseButton)))
            {
                sm_states[item] = new MouseState(item, enuMouseState.Up);
            }
        }
        /// <summary>
        /// get the stated mouse location
        /// </summary>
        public static Vector2i Location {
            get
            {
                return sm_Location;
            }
        }
        public static void Update() {
            
            foreach (enuGLGameMouseButton item in Enum.GetValues(typeof(enuGLGameMouseButton)))
            {
                var v_mState = MouseDeviceInput.GetState(item);
                if (v_mState.m_state == enuMouseState.Pressed)
                    sm_states[item] = v_mState;
                else {
                    if (v_mState.m_state == enuMouseState.Up)
                    {
                        if (sm_states[item].m_state == enuMouseState.Pressed)
                        {
                            var v = new MouseState(item, enuMouseState.Released);
                            sm_states[item] = v;
                        }
                        else
                            sm_states[item] = v_mState;
                    }
                    else
                        sm_states[item] = v_mState;
                }
            }
            sm_Location = MouseDeviceInput.Location;
        }

        public static bool IsPressed(enuGLGameMouseButton btn)
        {
            return sm_states[btn].m_state == enuMouseState.Pressed;
        }
        public static bool IsReleased(enuGLGameMouseButton btn)
        {
            return sm_states[btn].m_state == enuMouseState.Released ;
        }
        public static bool IsUp(enuGLGameMouseButton btn)
        {
            return sm_states[btn].m_state == enuMouseState.Up;
        }

       
    }
}
