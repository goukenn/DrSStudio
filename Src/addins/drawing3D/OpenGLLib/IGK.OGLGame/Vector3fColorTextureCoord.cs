

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector3fColorTextureCoord.cs
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
file:Vector3fColorTextureCoord.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
namespace IGK.OGLGame
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;
    [StructLayout (LayoutKind.Sequential )]
    public struct Vector3fColorTextureCoord 
    {
        private Vector3f m_Vector;
        private Colorf m_Color;
        private TexCoord2D m_TexCoord;
        public TexCoord2D TexCoord
        {
            get { return m_TexCoord; }
            set
            {
                if (m_TexCoord != value)
                {
                    m_TexCoord = value;
                }
            }
        }
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
        public Vector3f Vector
        {
            get { return m_Vector; }
            set
            {
                if (m_Vector != value)
                {
                    m_Vector = value;
                }
            }
        }
        public Vector3fColorTextureCoord(Vector3f vector, Colorf color, TexCoord2D coord)
        {
            this.m_Vector = vector;
            this.m_TexCoord = coord;
            this.m_Color = color;
        }
        public override string ToString()
        {
            return base.ToString();
        }
        public static readonly int SizeInByte;
        public static readonly GLVertexDefinition[] Declaration;
        public static readonly Vector3fColorTextureCoord Empty;
        static Vector3fColorTextureCoord()
        {
            SizeInByte = System.Runtime.InteropServices.Marshal.SizeOf(Empty);
            Declaration = new GLVertexDefinition[3];
            Declaration[0] = new GLVertexDefinition(3, 3 * Marshal.SizeOf(typeof(float)), 0, enuGLVertexUsage.Vertex, enuGLDataType.Float);
            Declaration[1] = new GLVertexDefinition(4, 4 * Marshal.SizeOf(typeof(float)), Declaration[0].SizeInByte, enuGLVertexUsage.Color, enuGLDataType.Float);
            Declaration[2] = new GLVertexDefinition(2, 2 * Marshal.SizeOf(typeof(float)), 
                Declaration[1].Offset +
                Declaration[1].SizeInByte, enuGLVertexUsage.Texture, enuGLDataType.Float);
        }
    }
    static class Vector3fColorTextureCoordExtension
    {
        public static void SetVertex(this Vector3fColorTextureCoord[] data, int index, Vector3f v)
        {
            if (index == -1)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    Vector3fColorTextureCoord v_v = data[i];
                    v_v.Vector = v;
                    data[i] = v_v;
                }
            }
            else if ((index >= 0) && (index < data.Length))
            {
                Vector3fColorTextureCoord v_v = data[index];
                v_v.Vector = v;
                data[index] = v_v;
            }
        }
    }
}

