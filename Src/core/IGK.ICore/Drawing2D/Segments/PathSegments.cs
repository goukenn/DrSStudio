

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathSegments.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.GraphicModels;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PathSegments.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Segments
{
    /// <summary>
    /// represent a multiple path segment
    /// </summary>
    public class PathSegments : CorePathSegmentBase
    {
        private Vector2f[] m_vector2f;
        private byte[] m_p;
        public Vector2f[] Vectors { get { return m_vector2f; } }
        public Byte[] PathType { get { return m_p; } }
        public override void SetPoint(int index, int def)
        {
            this.m_p[index] = (byte)def;
        }
        public override void Invert()
        {
            //List<Vector2f> p = new List<Vector2f>();
            //List<Byte> pb = new List<Byte>();
            CoreGraphicsPath pp = new CoreGraphicsPath();
            pp.AddPath(m_vector2f, m_p);
            pp.Reverse();

            this.m_p = pp.PathTypes;
            this.m_vector2f = pp.PathPoints;
            /*return;
           // pp.Reverse();

            p.AddRange(m_vector2f);
            pb.AddRange(m_p);
            p.Reverse();
            pb.Reverse();
            int loffset = 0;
            for (int i = 0; i < pb.Count; i++)
            {
                int j = pb[i];
               
                if (j == 0)
                    pb[i] =(byte)( 0x80 + loffset);//ct[j];
                else if ((j & 0x80) == 0x80)
                {
                    loffset = (j - 0x80);
                    pb[i] = 0;
                }
                else
                    pb[i] = (byte)j;
            }

            m_vector2f = p.ToArray();*/
        }
        public override void Transform(Matrix matrix)
        {
            m_vector2f = CoreMathOperation.TransformVector2fPoint(matrix, m_vector2f);
        }
        public override enuSegmentType SegmentType
        {
            get { return enuSegmentType.GraphicsPath;  }
        }
        public PathSegments(Vector2f[] vector2f, byte[] p)
        {            
            this.m_vector2f = vector2f;
            this.m_p = p;
        }
      
        public override byte[] GetPathTypes()
        {
            if ((m_p.Length > 0) && (m_p[0] != 0))
                m_p[0] = 0;
            return this.PathType;
        }
        public override Vector2f[] GetPathPoints()
        {
            return this.Vectors;
        }

        public override void CloseFigure()
        {
            //do nothing
            if (this.m_p.Length>0){
                this.m_p[this.m_p.Length-1] |= 0x80;
            }
        }

        public override bool IsClosed
        {
            get {
                return (this.m_p == null) ||

               ( this.m_p[this.m_p.Length  - 1] & (byte)enuGdiGraphicPathType.EndPoint) == (byte)enuGdiGraphicPathType.EndPoint; 
            }
        }
    }
}

