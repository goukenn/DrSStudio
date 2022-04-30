using IGK.GLLib;
using System;

namespace IGK.OGLGame
{
    public class OGLVertexArrayBuffer : IDisposable
    {
        uint m_bufferid;

        ///<summary>
        ///public .ctr
        ///</summary>
        internal OGLVertexArrayBuffer(uint uid)
        {
            this.m_bufferid = uid;
        }

        public void Dispose()
        {
            if (m_bufferid != 0xFFFF)
            {
                GLGameBuffer.FreeVertexArrays(m_bufferid);
                m_bufferid = 0xFFFF;
            }
        }
    }
}