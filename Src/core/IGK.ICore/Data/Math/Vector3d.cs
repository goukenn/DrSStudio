

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector3d.cs
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
file:Vector3d.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel ;
using System.Runtime.InteropServices;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [Serializable]
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential)]
    [System.ComponentModel.TypeConverter(typeof(VectorfConverter))]
    public struct Vector3d : IVector3d 
    {
        private double m_x;
        private double m_y;
        private double m_z;

        public double X { get { return this.m_x; } set { m_x = value; } }
        public double Y { get { return this.m_y; } set { m_y = value; } }
        public double Z { get { return this.m_z; } set { m_z = value; } }
        private readonly static Vector3d sm_zero;
        private readonly static Vector3d sm_one;
        private readonly static Vector3d sm_upy;
        private readonly static Vector3d sm_upx;
        private readonly static Vector3d sm_upz;
        private readonly static Vector3d sm_up;
        private readonly static Vector3d sm_down;
        private readonly static Vector3d sm_left;
        private readonly static Vector3d sm_right;
        private readonly static Vector3d sm_forward;
        private readonly static Vector3d sm_backward;
        public static Vector3d Zero { get { return sm_zero; } }
        public static Vector3d UpY { get { return sm_upy; } }
        public static Vector3d UpZ { get { return sm_upz; } }
        public static Vector3d UpX { get { return sm_upx; } }
        public static Vector3d Up { get { return sm_up; } }
        public static Vector3d Down { get { return sm_down; } }
        public static Vector3d Left { get { return sm_left; } }
        public static Vector3d Right { get { return sm_right; } }
        public static Vector3d Forward { get { return sm_forward; } }
        public static Vector3d Backward { get { return sm_backward; } }
        public static Vector3d One { get { return sm_one; } }
        public static int SizeInByte { get { return Marshal.SizeOf(typeof(Vector3d)); } }
        static Vector3d()
        {
            sm_zero = new Vector3d(0);
            sm_one = new Vector3d(1);
            sm_upy = new Vector3d(0, 1, 0);
            sm_upx = new Vector3d(1, 0, 0);
            sm_upz = new Vector3d(0, 0, 1);
            sm_up = new Vector3d(0.0f, 1.0f, 0.0f);
            sm_down = new Vector3d(0.0f, -1.0f, 0.0f);
            sm_left = new Vector3d(-1.0f, 0.0f, 0.0f);
            sm_right = new Vector3d(1.0f, 0.0f, 0.0f);
            sm_forward = new Vector3d(0.0f, 0.0f, -1.0f);
            sm_backward = new Vector3d(0.0f, 0.0f, 1.0f);
        }
        public override string ToString()
        {
            return string.Format("X={0};Y={1};Z={2}", X, Y, Z);
        }
        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return m_x;
                    case 1: return m_y;
                    case 2: return m_z;
                }
                return -0;
            }
            set
            {
                switch (index)
                {
                    case 0: m_x = value; break;
                    case 1: m_y = value; break;
                    case 2: m_z = value; break;
                }
            }
        }
        public Vector3d(double x, double y, double z)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_z = z;
        }
        public Vector3d(double v)
        {
            this.m_x = v;
            this.m_y = v;
            this.m_z = v;
        }
        /// <summary>
        /// normalize the vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3d Normalize(Vector3d v)
        {
            float d = (float)System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (d == 0)
            {
                //throw new DivideByZeroException();
                return Vector3d.Zero;
            }
            Vector3d v_vector = new Vector3d(v.X / d, v.Y / d, v.Z / d);
            return v_vector;
        }
        /// <summary>
        /// get the distance Squared
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double DistanceSquared(Vector3d v1, Vector3d v2)
        {
            double a = v2.X - v1.X;
            double b = v2.Y - v1.Y;
            double c = v2.Z - v1.Z;
            double d = a * a + b * b + c * c;
            return d;
        }
        public static double Distance(Vector3d v1, Vector3d v2)
        {
            double d = DistanceSquared(v1, v2);
            return System.Math.Sqrt(d);
        }
        /// <summary>
        /// get the dot product
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static double Dot(Vector3d v1, Vector3d v2)
        {
            return (v1.X * v2.X) +
                    (v1.Y * v2.Y) +
                    (v1.Z * v2.Z);
        }
        public static Vector3d NormCrossProduct(Vector3d v1, Vector3d v2)
        {
            Vector3d v_vector = Vector3d.Zero;
            v_vector.X = v1.Y * v2.Z - v1.Z * v2.Y;
            v_vector.Y = v1.Z * v2.X - v1.X * v2.Z;
            v_vector.Z = v1.X * v2.Y - v1.Y * v2.X;
            return Normalize(v_vector);
        }
        public static implicit operator IntPtr(Vector3d v)
        {
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(v));
            Marshal.StructureToPtr(v, alloc, true);
            return alloc;
        }
        public static Vector3d operator -(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z);
        }
        public static Vector3d operator -(Vector3d v1)
        {
            return new Vector3d(-v1.X,
                -v1.Y,
                -v1.Z);
        }
        public static Vector3d operator +(Vector3d v1, float v2)
        {
            return new Vector3d(v1.X + v2,
                v1.Y + v2,
                v1.Z + v2);
        }
        public static Vector3d operator -(Vector3d v1, float v2)
        {
            return new Vector3d(v1.X - v2,
                v1.Y - v2,
                v1.Z - v2);
        }
        public static Vector3d operator +(Vector3d v1, Vector3d v2)
        {
            return new Vector3d(v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }
        public static implicit operator Vector3d(Vector2f d)
        {
            return new Vector3d(d.X, d.Y, 0.0f);
        }
        public static Vector3d operator *(Vector3d d, float f)
        {
            return new Vector3d(d.X * f, d.Y * f, d.Z * f);
        }
        public static Vector3d operator /(Vector3d d, float f)
        {
            if (f != 0.0f)
                return new Vector3d(d.X / f, d.Y / f, d.Z / f);
            return Vector3d.Zero;
        }
        public static bool operator ==(Vector3d v1, Vector3d v2)
        {
            return (v1.X == v2.X) &&
                (v1.Y == v2.Y) &&
                (v1.Z == v2.Z);
        }
        public static bool operator !=(Vector3d v1, Vector3d v2)
        {
            return !(v1 == v2);
        }
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //public Vector3 GetAngle()
        //{
        //    float x = 0.0f;
        //    float y =0.0f;
        //    float z = 0.0f;
        //    float d = Vector3.Distance(Vector3.Zero, this);
        //    if (d == 0)
        //        return Vector3.Zero;
        //    const float R2D = (float)(180.0f * System.Math.PI );
        //    y = (float)(System.Math.Acos (this.X / d) * R2D);
        //    x = (float)(System.Math.Acos(this.Y / d) * R2D);
        //    y = (float)(System.Math.Acos(this.Y / d) * R2D);
        //    return new Vector3(x, y, z);
        //}
        /// <summary>
        /// get the magentude / distance
        /// </summary>
        /// <param name="StartLocation"></param>
        /// <returns></returns>
        public static float Magnitude(Vector3d v)
        {
            return (float)Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
        }
    }
}

