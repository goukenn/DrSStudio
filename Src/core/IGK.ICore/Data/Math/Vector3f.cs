

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Vector3f.cs
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
file:Vector3f.cs
*/

/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection ;
using System.Runtime.InteropServices ;
using IGK.ICore;using IGK.ICore.ComponentModel;
namespace IGK.ICore
{
    [Serializable]
    [ComVisible(true)]
    [StructLayout(LayoutKind.Sequential )]
    [System.ComponentModel.TypeConverter(typeof(VectorfConverter))]
    public struct Vector3f
    {      
        private float m_x;
        private float m_y;
        private float m_z;
        public float X { get { return this.m_x; } set { m_x = value; } }
        public float Y { get { return this.m_y; } set { m_y = value; } }
        public float Z { get { return this.m_z; } set { m_z = value; } }
        private readonly static Vector3f sm_zero;
        private readonly static Vector3f sm_one;
        private readonly static Vector3f sm_upy;
        private readonly static Vector3f sm_upx;
        private readonly static Vector3f sm_upz;
        private readonly static Vector3f sm_up;
        private readonly static Vector3f sm_down;
        private readonly static Vector3f sm_left;
        private readonly static Vector3f sm_right;
        private readonly static Vector3f sm_forward;
        private readonly static Vector3f sm_backward;
        public static Vector3f Zero { get { return sm_zero; } }
        public static Vector3f UpY { get { return sm_upy; } }
        public static Vector3f UpZ { get { return sm_upz; } }
        public static Vector3f UpX { get { return sm_upx; } }
        public static Vector3f Up { get { return sm_up; } }
        public static Vector3f Down { get { return sm_down; } }
        public static Vector3f Left { get { return sm_left; } }
        public static Vector3f Right { get { return sm_right; } }
        public static Vector3f Forward { get { return sm_forward; } }
        public static Vector3f Backward { get { return sm_backward; } }
        public static Vector3f One { get { return sm_one; } }
        public static int Size2iInByte { get { return Marshal.SizeOf(typeof(Vector3f)); } }
        static Vector3f() {
            sm_zero = new Vector3f(0);
            sm_one = new Vector3f(1);
            sm_upy = new Vector3f(0, 1, 0);
            sm_upx = new Vector3f(1, 0, 0);
            sm_upz = new Vector3f(0, 0, 1);
            sm_up = new Vector3f(0.0f, 1.0f, 0.0f);
            sm_down = new Vector3f(0.0f, -1.0f, 0.0f);
            sm_left = new Vector3f(-1.0f, 0.0f, 0.0f);
            sm_right = new Vector3f(1.0f, 0.0f, 0.0f);
            sm_forward = new Vector3f(0.0f, 0.0f, -1.0f);
            sm_backward = new Vector3f(0.0f, 0.0f, 1.0f);
        }
        public override string ToString()
        {
            return string.Format("X={0};Y={1};Z={2}", X, Y, Z);
        }
        /// <summary>
        /// normalize the vector
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3f Normalize(Vector3f v)
        {
            float d = (float)System.Math.Sqrt(v.X * v.X + v.Y * v.Y + v.Z * v.Z);
            if (d == 0)
            {
                //throw new DivideByZeroException();
                return Vector3f.Zero;
            }
            Vector3f v_vector = new Vector3f(v.X / d, v.Y / d, v.Z / d);
            return v_vector;
        }
        public float this[int index] {
            get {
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
        public Vector3f(float x, float y, float z)
        {
            this.m_x = x;
            this.m_y = y;
            this.m_z = z;
        }
        public Vector3f(double x, double y,double z)
        {
            this.m_x = (float)x;
            this.m_y = (float)y;
            this.m_z = (float)z;
        }
        public Vector3f(float v)
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
        public void Normalize()
        {
            Vector3f v = Normalize(this);
            this.m_x = v.m_x;
            this.m_y = v.m_y;
            this.m_z = v.m_z;            
        }
        /// <summary>
        /// get the distance Squared
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float DistanceSquared(Vector3f v1, Vector3f v2)
        { 
            float a = v2.X-v1.X ;
            float b = v2.Y - v1.Y;
            float c = v2.Z - v1.Z;
            float d = a * a + b * b + c * c;
            return d;
        }
        public static float Distance(Vector3f v1, Vector3f v2)
        {
            float d = DistanceSquared(v1, v2);
            return (float)System.Math.Sqrt(d);
        }
        /// <summary>
        /// get the dot product
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static float Dot(Vector3f v1, Vector3f v2)
        {
            return  (v1.X * v2.X) +
                    (v1.Y * v2.Y) +
                    (v1.Z * v2.Z);
        }
        public static Vector3f CrossProduct(Vector3f v1, Vector3f v2)
        {
            Vector3f v_vector = Vector3f.Zero;
            v_vector.X = v1.Y * v2.Z - v1.Z * v2.Y;
            v_vector.Y = v1.Z * v2.X - v1.X * v2.Z;
            v_vector.Z = v1.X * v2.Y - v1.Y * v2.X;
            return v_vector;
        }
        /// <summary>
        /// return a normalize cross product
        /// </summary>
        /// <param name="v1"></param>
        /// <param name="v2"></param>
        /// <returns></returns>
        public static Vector3f NormCrossProduct(Vector3f v1, Vector3f v2)
        {
            Vector3f v_vector = Vector3f.Zero;
            v_vector.X = v1.Y* v2.Z  - v1.Z * v2.Y;
            v_vector.Y = v1.Z * v2.X - v1.X * v2.Z;
            v_vector.Z = v1.X * v2.Y - v1.Y * v2.X;
            return Normalize(v_vector);
        }
        public static implicit operator IntPtr(Vector3f v)
        { 
            IntPtr  alloc = Marshal.AllocCoTaskMem (Marshal.SizeOf (v));
            Marshal.StructureToPtr(v, alloc, true);
            return alloc;
        }
        public static Vector3f operator -(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.X - v2.X,
                v1.Y - v2.Y,
                v1.Z - v2.Z);
        }
        public static Vector3f operator -(Vector3f v1)
        {
            return new Vector3f(-v1.X ,
                -v1.Y ,
                -v1.Z );
        }
        public static Vector3f operator +(Vector3f v1, float v2)
        {
            return new Vector3f(v1.X + v2,
                v1.Y + v2,
                v1.Z + v2);
        }
        public static Vector3f operator -(Vector3f v1, float v2)
        {
            return new Vector3f(v1.X - v2,
                v1.Y - v2,
                v1.Z - v2);
        }
        public static Vector3f operator +(Vector3f v1, Vector3f v2)
        {
            return new Vector3f(v1.X + v2.X,
                v1.Y + v2.Y,
                v1.Z + v2.Z);
        }
        public static implicit operator Vector3f(Vector2f d)
        {
            return new Vector3f(d.X, d.Y, 0.0f);
        }
        public static Vector3f  operator *(Vector3f d, float f)
        {
            return new Vector3f(d.X * f, d.Y * f, d.Z * f);
        }
        public static Vector3f operator /(Vector3f d, float f)
        {
               if (f != 0.0f)
                        return new Vector3f(d.X /f, d.Y / f, d.Z / f);
               return Vector3f.Zero;
        }
        public static bool operator == (Vector3f v1, Vector3f v2)
        {
            return (v1.X == v2.X) &&
                (v1.Y == v2.Y) &&
                (v1.Z == v2.Z);
        }
        public static bool operator !=(Vector3f v1, Vector3f v2)
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
        /// <summary>
        /// get the magentude / distance
        /// </summary>
        /// <param name="StartLocation"></param>
        /// <returns></returns>
        public static float Magnitude(Vector3f v)
        {
            return (float)Math.Sqrt (v.X * v.X + v.Y * v.Y + v.Z * v.Z); 
        }
    }
}

