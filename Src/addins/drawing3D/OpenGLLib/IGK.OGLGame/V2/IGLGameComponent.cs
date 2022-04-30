using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.OGLGame.V2
{
    public interface  IGLGameComponent
    {
        void Init();
        void Update();
        void Render();
    }
}
