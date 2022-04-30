

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsBuffer.cs
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
file:GraphicsBuffer.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// represent a graphics buffer available since OpenGL 1.4
    /// </summary>
    public class GraphicsBuffer
    {
        public static bool IsAvailable {
            get {
                return GLVersionChecker.IsGreaterOrEqual(GLConstant.VERSION_1_4);
            }
        }
        /// <summary>
        /// get a single buffer
        /// </summary>
        /// <returns></returns>
        public static uint GenBuffer()
        { 
            IntPtr v_h = Marshal.AllocCoTaskMem (Marshal.SizeOf (typeof (uint)));
            GL.glGenBuffers(1, v_h);
            uint c =(uint) Marshal.ReadInt32(v_h);
            Marshal.FreeCoTaskMem(v_h);
            return c;
        }
        public static bool IsBindBuffer(uint id)
        {
            return GL.glIsBuffer(id);
        }
        public static void BindBuffer(uint buffer)
        {
            GL.glBindBuffer(GL.GL_ARRAY_BUFFER, buffer);
        }
        public static void SetBufferData(int[] data)
        { 
        }
    }
}

