using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.WebGLLib
{
    public interface IWebGLGameScene
    {
        /// <summary>
        /// get or set the name of the scene
        /// </summary>
        string Name { get; set; }
    }
}
