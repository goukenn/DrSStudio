

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLVertexDeclaration.cs
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
file:GLVertexDeclaration.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.OGLGame
{
    
using IGK.ICore;
    using IGK.OGLGame.Graphics;
    /// <summary>
    /// represent a vertext declaration
    /// </summary>
    public class GLVertexDeclaration
    {
        GLVertexDefinition[] m_definition;
        private int m_stride;
        public GLVertexDefinition[] GetDefinition() {            
                return m_definition;
        }
        public int Stride {
            get {
                return m_stride;
            }
        }
        /// <summary>
        /// uses this constructor for auto stride
        /// </summary>
        /// <param name="device"></param>
        /// <param name="definition"></param>
        public GLVertexDeclaration(GLVertexDefinition[] definition )
        {
            this.m_definition = definition;
            this.m_stride = 0;
            for (int i = 0; i < definition.Length ; i++)
            {
                this.m_stride += definition[i].SizeInByte;
            }
        }
        public GLVertexDeclaration(int stride,  params GLVertexDefinition[] definition )
        {
            if ((definition == null) || (definition.Length == 0))
                throw new GLGameException("definition");
            this.m_definition = definition;
            this.m_stride = stride;
        }
        public override string ToString()
        {
            return string.Format("Declaration [{0}]", this.Stride);
        }
    }
}

