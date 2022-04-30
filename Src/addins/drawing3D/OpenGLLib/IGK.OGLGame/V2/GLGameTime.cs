using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.V2
{
    /// <summary>
    /// reprensent a game time
    /// </summary>
    public class GLGameTime
    {
        //update the game time logic
        internal void Update()
        {
        }
        /// <summary>
        /// createt and start a game time
        /// </summary>
        /// <returns></returns>
        internal static GLGameTime Start()
        {
            GLGameTime t = new GLGameTime();
            return t;
        }
    }
}
