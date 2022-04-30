using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.XInput
{
    public class GamePad
    {
        const int PADCOUNT = 4;
        static GamePadState[] sm_GamePads;
        static GamePad() {
            sm_GamePads = new GamePadState[PADCOUNT];

            for (int i = 0; i < PADCOUNT; i++)
            {
                sm_GamePads[i] = new GamePadState();
            }
        }

        /// <summary>
        /// update game pad state
        /// </summary>
        public static void Update() {
            for (int i = 0; i < PADCOUNT; i++)
            {
                var c = GamePadController.RetrieveController(i);
                if (c!=null)
                sm_GamePads[i].UpdateState(c);
            }
        }
        public static GamePadState GetPadState(enuGamePlayer player)
        {
            return sm_GamePads[(int)player - 1];
            //var c = GamePadController.RetrieveController((int)player);
            //if (c != null) {
            //    return new GamePadState(c);
            //}
            //return null;
        }
        /// <summary>
        /// register game pad usage 
        /// </summary>
        public static void Register()
        {
            GamePadController.StartPolling();
        }
        public static void Unregister() {
            GamePadController.StopPolling();
        }
    }
}
