using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.Input
{
     public partial class GLGameInput
    {
        public class Keyboard {
            public static void Init() {

                //#if WINDOWS
                System.Windows.Forms.Application.AddMessageFilter(KeyboardInput.Instance);
//#endif

            }
        }
    }
}
