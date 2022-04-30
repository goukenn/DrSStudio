

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuGLUTessCallback.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:enuGLUTessCallback.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    public enum enuGLUTessCallback : uint
    {
        ///* TessCallback */
        Begin = GLU.GLU_TESS_BEGIN,
        Vertex = GLU.GLU_TESS_VERTEX,
        End = GLU.GLU_TESS_END,
        Error = GLU.GLU_TESS_ERROR,
        EdgeFlag = GLU.GLU_TESS_EDGE_FLAG,
        Combine = GLU.GLU_TESS_COMBINE,
        BeginData = GLU.GLU_TESS_BEGIN_DATA,
        VertexData = GLU.GLU_TESS_VERTEX_DATA,
        EndData = GLU.GLU_TESS_END_DATA,
        ErrorData = GLU.GLU_TESS_ERROR_DATA,
        FlagData = GLU.GLU_TESS_EDGE_FLAG_DATA,
        CombineData = GLU.GLU_TESS_COMBINE_DATA
    }
}

