using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.Math
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Quaternion
    {
        private float m_x;
        private float m_y;
        private float m_z;
        private float m_w;

        public readonly static Quaternion Empty;

        static Quaternion() {
            Empty = new Quaternion(0, 0, 0, 0);
        }


        public Quaternion(float x, float y, float z, float w)
        {
            this.m_w = w;
            this.m_x = x;
            this.m_y = y;
            this.m_z = z;
        }
        public Quaternion Normalize()
        {
            float v_ln = this.Length();
            if (v_ln != 0)
                return new Quaternion(this.m_x / v_ln, this.m_y / v_ln, this.m_z / v_ln, this.m_w / v_ln);
            return Empty;
        }
        public Quaternion Conjugate() {
            return new Quaternion(-this.m_x, -this.m_y, -this.m_z, this.m_w);
        }
        public Quaternion Multiply(Quaternion r) {
           float v_x = (this.m_x * r.m_w) + (m_w * r.m_x ) + (m_y * r.m_z ) - ( m_z * r.m_y );
           float v_y = (this.m_y * r.m_w) + (m_w * r.m_y ) + (m_z * r.m_x ) - ( m_x +r.m_z ) ;
           float v_z = (this.m_z * r.m_w) + (m_w * r.m_z ) + (m_x * r.m_y ) - ( m_y * r.m_x ) ;
           float v_w = (this.m_w * r.m_w) - (m_x * r.m_x ) - (m_y * r.m_y) - (m_z * r.m_z );
           return new Quaternion(v_x, v_y, v_z, v_w);
        }
        public Quaternion Multiply(Vector3f vector) {
            float v_x = (m_w * vector.X) + (m_y * vector.Z) - (m_z * vector.Y);
            float v_y = (m_w * vector.Y) + (m_z * vector.X) - (m_x  * vector.Z);
            float v_z = (m_w * vector.Z) + (m_x * vector.Y) - (m_y * vector.X);
            float v_w = -(m_x * vector.X) - (m_y * vector.Y) - (m_z * vector.Z);
            return new Quaternion(v_x, v_y, v_z, v_w);
        }

        private float Length()
        {
            return (float)global::System.Math.Sqrt(m_x * m_x + m_y * m_y + m_z * m_z + m_w + m_w);
        }
    }
}
