

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Matrix3x3f.cs
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
file:Matrix3x3f.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.ICore
{
    [StructLayout(LayoutKind.Sequential)]
    public class Matrix3x3f
    {
        private float m00, m01, m02;
        private float m10, m11, m12;
        private float m20, m21, m22;
        public float M00 { get { return this.m00; } set { m00 = value; } }
        public float M01 { get { return this.m01; } set { m01 = value; } }
        public float M02 { get { return this.m02; } set { m02 = value; } }
        public float M10 { get { return this.m10; } set { m10 = value; } }
        public float M11 { get { return this.m11; } set { m11 = value; } }
        public float M12 { get { return this.m12; } set { m12 = value; } }
        public float M20 { get { return this.m20; } set { m20 = value; } }
        public float M21 { get { return this.m21; } set { m21 = value; } }
        public float M22 { get { return this.m22; } set { m22 = value; } }
        public Matrix3x3f()
        {
            m00 = m11 = m22 = 1.0f;
            m01 = m02 = 0.0f;
            m10 = m12 = 0.0f;
            m20 = m21 = 0.0f;
        }
        public Matrix3x3f(float[] element)
        {
            if ((element == null) || (element.Length != 9))
                throw new ArgumentException($"{nameof(element)}");
            m00 = element[0]; m01 = element[1]; m02 = element[2];
            m10 = element[3]; m11 = element[4]; m12 = element[5];
            m20 = element[6]; m21 = element[7]; m22 = element[8];
        }
        public bool IsIdentity
        {
            get
            {
                return (m00 == 1.0f) && (m00 == m11) && (m00 == m22) &&
                     (m01 == 0.0f) && (m02 == m01) &&
                (m10 == 0.0f) && (m12 == 0.0f) &&
                (m20 == 0.0f) && (m21 == 0.0f);
            }
        }
        public static Matrix3x3f operator +(Matrix3x3f m1, Matrix3x3f m2)
        {
            Matrix3x3f r = new Matrix3x3f();
            Type t = typeof(Matrix3x3f);
            FieldInfo v_finfo = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    v_finfo = t.GetField(string.Format("m{0}{1}", i, j), BindingFlags.Instance | BindingFlags.NonPublic);
                    v_finfo.SetValue(r,
                        (float)v_finfo.GetValue(m1) +
                        (float)v_finfo.GetValue(m2));
                }
            }
            return r;
        }
        public static Matrix3x3f operator -(Matrix3x3f m1, Matrix3x3f m2)
        {
            Matrix3x3f r = new Matrix3x3f();
            Type t = typeof(Matrix3x3f);
            FieldInfo v_finfo = null;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    v_finfo = t.GetField(string.Format("m{0}{1}", i, j), BindingFlags.Instance | BindingFlags.NonPublic);
                    v_finfo.SetValue(r,
                        (float)v_finfo.GetValue(m1) -
                        (float)v_finfo.GetValue(m2));
                }
            }
            return r;
        }
        public static Matrix3x3f operator *(float a, Matrix3x3f m1)
        {
            Matrix3x3f r = new Matrix3x3f();
            r.m00 = a * m1.m00;
            r.m01 = a * m1.m01;
            r.m02 = a * m1.m02;
            r.m10 = a * m1.m10;
            r.m11 = a * m1.m11;
            r.m12 = a * m1.m12;
            r.m20 = a * m1.m20;
            r.m21 = a * m1.m21;
            r.m22 = a * m1.m22;
            return r;
        }
        public override string ToString()
        {
            return string.Format("{0}\n{1}\n{2}",
            string.Format("{0} {1} {2}", m00, m01, m02),
            string.Format("{0} {1} {2}", m10, m11, m12),
            string.Format("{0} {1} {2}", m20, m21, m22)
            );
        }
    }
}

