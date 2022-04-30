using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore
{
    [StructLayout(LayoutKind.Sequential)]
    public class Matrix4x4f
    {
        private float m00, m01, m02, m03;
        private float m10, m11, m12, m13;
        private float m20, m21, m22, m23;
        private float m30, m31, m32, m33;
        public float M00 { get { return this.m00; } set { m00 = value; } }
        public float M01 { get { return this.m01; } set { m01 = value; } }
        public float M02 { get { return this.m02; } set { m02 = value; } }
        public float M10 { get { return this.m10; } set { m10 = value; } }
        public float M11 { get { return this.m11; } set { m11 = value; } }
        public float M12 { get { return this.m12; } set { m12 = value; } }
        public float M20 { get { return this.m20; } set { m20 = value; } }
        public float M21 { get { return this.m21; } set { m21 = value; } }
        public float M22 { get { return this.m22; } set { m22 = value; } }
        public Matrix4x4f()
        {
            m00 = m11 = m22 = 1.0f;
            m01 = m02 = 0.0f;
            m10 = m12 = 0.0f;
            m20 = m21 = 0.0f;
        }
        public Matrix4x4f(float[] element)
        {
            if ((element == null) || (element.Length != 16))
                throw new ArgumentException($"{nameof(element)}");
            m00 = element[0]; m01 = element[1]; m02 = element[2]; m03 = element[3];
            m10 = element[4]; m11 = element[5]; m12 = element[6]; m13 = element[7];
            m20 = element[8]; m21 = element[9]; m22 = element[10]; m23 = element[11];
            m30 = element[12]; m31 = element[13]; m32 = element[14]; m33 = element[15];
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
        public static Matrix4x4f operator +(Matrix4x4f m1, Matrix4x4f m2)
        {
            Matrix4x4f r = new Matrix4x4f();
            Type t = typeof(Matrix4x4f);
            FieldInfo v_finfo = null;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    v_finfo = t.GetField(string.Format("m{0}{1}", i, j), BindingFlags.Instance | BindingFlags.NonPublic);
                    v_finfo.SetValue(r,
                        (float)v_finfo.GetValue(m1) +
                        (float)v_finfo.GetValue(m2));
                }
            }
            return r;
        }
        public static Matrix4x4f operator -(Matrix4x4f m1, Matrix4x4f m2)
        {
            Matrix4x4f r = new Matrix4x4f();
            Type t = typeof(Matrix4x4f);
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
        public static Matrix4x4f operator *(float a, Matrix4x4f m1)
        {
            Matrix4x4f r = new Matrix4x4f();
            r.m00 = a * m1.m00;
            r.m01 = a * m1.m01;
            r.m02 = a * m1.m02;
            r.m03 = a * m1.m03;

            r.m10 = a * m1.m10;
            r.m11 = a * m1.m11;
            r.m12 = a * m1.m12;
            r.m13 = a * m1.m13;

            r.m20 = a * m1.m20;
            r.m21 = a * m1.m21;
            r.m22 = a * m1.m22;
            r.m23 = a * m1.m23;

            r.m30 = a * m1.m30;
            r.m31 = a * m1.m31;
            r.m32 = a * m1.m32;
            r.m33 = a * m1.m33;

            return r;
        }
        public override string ToString()
        {
            return string.Format(
                "{0} \n{1} \n{2} \n{3}",
            string.Format("{0} {1} {2} {3}", m00, m01, m02, m03),
            string.Format("{0} {1} {2} {3}", m10, m11, m12, m13),
            string.Format("{0} {1} {2} {3}", m20, m21, m22, m23),
            string.Format("{0} {1} {2} {3}", m30, m31, m32, m33)
            );
        }

        public float[] Elements { get{
            return new float[] { 
                 m00, m01, m02, m03,
                 m10, m11, m12, m13,
                 m20, m21, m22, m23,
                 m30, m31, m32, m33
            };
        } }
    }
}
