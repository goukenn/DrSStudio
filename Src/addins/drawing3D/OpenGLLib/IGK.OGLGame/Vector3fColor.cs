

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector3fColor.cs
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
file:Vector3fColor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.OGLGame
{
    
    using IGK.ICore;
    using IGK.OGLGame;
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    /// <summary>
    /// represent a association with vertex3f and color
    /// </summary>
    public struct Vector3fColor
    {
        private Vector3f  m_Vector;
        private Colorf m_Color;
        public Colorf Color
        {
            get { return m_Color; }
            set
            {
                if (!m_Color.Equals ( value))
                {
                    m_Color = value;
                }
            }
        }
        public Vector3f  Vector
        {
            get { return m_Vector; }
            set
            {
                if (m_Vector.Equals (value)==false )
                {
                    m_Vector = value;
                }
            }
        }
        public static readonly int SizeInByte;
        public static readonly GLVertexDefinition[] VertexDefinition;
        public static readonly Vector3fColor  Empty;
        private static GLVertexDeclaration m_vertexDeclaration;

        public static GLVertexDeclaration VertexDeclaration=>m_vertexDeclaration;

        static Vector3fColor()
        {
            Empty = new Vector3fColor(Vector3f.Zero, Colorf.Transparent);
            SizeInByte = System.Runtime.InteropServices.Marshal.SizeOf(Empty);
            VertexDefinition = new  GLVertexDefinition[2];
            VertexDefinition[0] = new GLVertexDefinition(3, 3* Marshal.SizeOf (typeof (float )), 0, enuGLVertexUsage.Vertex, enuGLDataType.Float );
            VertexDefinition[1] = new GLVertexDefinition(4,4* Marshal.SizeOf (typeof (float )) , VertexDefinition[0].SizeInByte , enuGLVertexUsage.Color, enuGLDataType.Float );

            m_vertexDeclaration = new GLVertexDeclaration (VertexDefinition);
        }
        public Vector3fColor(Vector3f vector, Colorf color)
        {
            this.m_Vector = vector;
            this.m_Color = color;
        }
    }
}

