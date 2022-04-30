using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.ColladaEngine
{
    /// <summary>
    /// represent mode for export scene
    /// </summary>
    public enum enuExportProfile
    {
        /// <summary>
        /// in global gl shading language
        /// </summary>
        GLSL,
        /// <summary>
        /// used in embeded shadidng language 2
        /// </summary>
        GLESL2,
        /// <summary>
        /// used for web gl gaming surface
        /// </summary>
        GLEWebGL
    }
}
