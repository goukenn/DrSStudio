using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.Input
{
    /// <summary>
    /// used to store keyboard current state for game engine
    /// </summary>
    public class GLGameKeyboardStates
    {
        private static Dictionary<enuKeyboardButton, KeyboardState> sm_states;

        static GLGameKeyboardStates() {
            sm_states = new Dictionary<enuKeyboardButton, KeyboardState>();
            foreach (enuKeyboardButton key in Enum.GetValues(typeof(enuKeyboardButton)))
            {
                sm_states.Add(key, new KeyboardState(key, enuKeyState.Up));
            }
        }
        public static void Update() { 
            //copy state for KeyboardInput
            var v_states = KeyboardInput.GetState();
            foreach (KeyValuePair<enuKeyboardButton, KeyboardState> i in v_states) {

                if (i.Value.IsKeyDown(i.Key))
                {
                    sm_states[i.Key] = i.Value;
                }
                else if (i.Value.IsKeyUp(i.Key)){ 
                    if (sm_states[i.Key].IsKeyDown(i.Key)){
                        var s = sm_states[i.Key];
                        s.m_state = enuKeyState.Released;
                        sm_states[i.Key] = s ;
                    }
                    else{
                        sm_states[i.Key] = i.Value;
                    }
                }
            }
        }


        public static bool IsKeyPressed(enuKeyboardButton key)
        {
            return sm_states[key].IsKeyDown(key);
        }

        public static bool IsKeyRelease(enuKeyboardButton key)
        {
            return sm_states[key].IsKeyReleased(key);
        }
    }
}
