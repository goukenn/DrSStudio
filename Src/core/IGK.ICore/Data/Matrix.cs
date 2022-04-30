

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Matrix.cs
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
file:Matrix.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore;
using IGK.ICore.Data;
using System.ComponentModel;
using IGK.ICore.ComponentModel;
using System.Runtime.InteropServices;   

namespace IGK.ICore
{
    /// <summary>
    /// represent the matrix 4x4 in the DRSStudio Coordinate system. 
    /// Row first
    /// </summary>
    //[TypeConverter(typeof(WinCoreMatrixConverter))]  
    //[StructLayout(LayoutKind.Sequential)]
    public sealed class Matrix : MarshalByRefObject, IDisposable, ICloneable 
    {
        static Matrix sm_identity;

        public static Matrix Identity {
            get {
                return sm_identity;
            }
        }
        const int MATRIX_LENGTH = 16;

        public bool IsDisposed {
            get { 
                return (this.m_elements ==null);
            }
        }
        static Matrix(){
            sm_identity = new Matrix();
        }
        public override object InitializeLifetimeService()
        {
            return null;
        }
        public static implicit operator Matrix  (float[] tab)
        {
            Matrix m = new Matrix ();
            for (int i = 0; i < MATRIX_LENGTH ; i++)
			{
			 m.m_elements[i] = tab[i];
			}
            return m;
        }
        public static implicit operator Matrix  (double[] tab)
        {
            Matrix m = new Matrix();
            for (int i = 0; i < MATRIX_LENGTH; i++)
            {
                m.m_elements[i] = (float)tab[i];
            }
            return m;
        }
        public static Vector3f operator *(Vector3f vect, Matrix matrix)
        {
            float[] tb = matrix.Elements;
            Vector3f v_out = Vector3f.Zero;
            v_out.X = vect.X * tb[0] + vect.Y * tb[4] + vect.Z * tb[8] + tb[12];
            v_out.Y = vect.X * tb[1] + vect.Y * tb[5] + vect.Z * tb[9] + tb[13];
            v_out.Z = vect.X * tb[2] + vect.Y * tb[6] + vect.Z * tb[10] + tb[14];
            return v_out;
        }
        public static Vector2f operator *(Vector2f vect, Matrix matrix)
        {
            float[] tb = matrix.Elements;
            Vector2f v_out = Vector2f.Zero;
            v_out.X = vect.X * tb[0] + vect.Y * tb[4] + tb[12];
            v_out.Y = vect.X * tb[1] + vect.Y * tb[5] + tb[13];
            return v_out;
        }
        private static float[] mult_matrix(float[] tb1, float[] tb2)
        {    
            float[] rtb = new float[MATRIX_LENGTH];
            int k = 0;
            int offsetx = 0;
            int offsety = 0;
            float v_som = 0;
            for (k = 0; k < MATRIX_LENGTH; )
            {
                for (int i = 0; i < 4; i++)
                {//columns
                    v_som = 0.0f;
                    for (int j = 0; j < 4; j++)
                    {
                        offsety = (4 * j) + i;//calculate column index
                        v_som += tb1[offsetx + j] * tb2[offsety];
                    }
                    rtb[k] = v_som;
                    k++;
                }
                offsetx += 4;
            }
            return rtb;
        }
        public static Matrix operator *(Matrix matrix1, Matrix matrix2)
        {
            float[] rtb = mult_matrix(matrix1.Elements , matrix2.Elements );          
            Matrix c = new Matrix();
            c.m_elements = rtb;
            return c;
        }
        public static Matrix operator *(Matrix matrix1,float scale)
        {
            float[] tb = matrix1.Elements;
            float[] rtb = new float[MATRIX_LENGTH];
            for (int i = 0; i < tb.Length; i++)
            {
                rtb[i] = tb[i] * scale;
            }
            Matrix c = new Matrix();
            c.m_elements = rtb;
            return c;
        }
        public void Translate(float dx, float dy, enuMatrixOrder order) {
            this.Translate(dx, dy, 0, order);
        }
        public void Share(float dx, float dy) { }
        public void Rotate(float angle) {
            Rotate(angle, enuMatrixOrder.Append);
        }
        public void Rotate(float angle, enuMatrixOrder order)
        {
            float v_a = angle * CoreMathOperation.ConvDgToRadian;
            Matrix v_tmat = new Matrix();
            v_tmat.m_elements[0] = (float)Math.Cos(v_a);
            v_tmat.m_elements[1] = (float)-Math.Sin(v_a);
            v_tmat.m_elements[4] = (float)Math.Sin(v_a);
            v_tmat.m_elements[5] = (float)Math.Cos(v_a);
            float[] tr = mult_matrix(v_tmat.m_elements, order);
            this.m_elements = tr;
            v_tmat.Dispose();
        }
        public void Rotate(float angle, Vector2f at)
        {
            this.Translate(-at.X, -at.Y, enuMatrixOrder.Append);
            this.Rotate(angle, enuMatrixOrder.Append);
            this.Translate(at.X, at.Y, enuMatrixOrder.Append);
        }
        public float OffsetX { get { return this.m_elements[12]; } set { this.m_elements[12] = value; } }
        public float OffsetY { get { return this.m_elements[13]; } set { this.m_elements[13] = value; } }
        public float OffsetZ { get { return this.m_elements[14]; } set { this.m_elements[14] = value; } }
        float[] m_elements;
        public float[] Elements { get { return m_elements; }
            set {
                this.m_elements = value;
            }
        }
        public float[] GetColumnFirstElement() {
            var t = this.m_elements;
            var g = new float[MATRIX_LENGTH];

            g[0] = t[0];  g[1] = t[4];  g[2] = t[8]; g[3] = t[12];
            g[4] = t[1];  g[5] = t[5];  g[6] = t[9]; g[7] = t[13];
            g[8] = t[2];  g[9] = t[6];  g[10] = t[10]; g[11] = t[14];
            g[12] = t[3]; g[13] = t[7]; g[14] = t[11]; g[15] = t[15];
        
            return g;
        }
        public  Vector2f[] TransformPoints(Vector2f[] t)
        {
            t = CoreMathOperation.TransformVector2fPoint(this, t);
            return t;
        }
        public Vector2f[] TransformVectors(Vector2f[] t)
        {
            t = CoreMathOperation.TransformVector2fVector(this, t);
            return t;
        }
        public void Multiply(Matrix mat2)
        {
            if (mat2 == null) return;
            this.m_elements = mult_matrix(mat2.m_elements, enuMatrixOrder.Append);
        }
        public Matrix()
        {
            this.m_elements = new float[] { 
                1, 0,0,0,
                0, 1,0,0,
                0, 0,1,0,
                0, 0,0, 1
            };
        }
        object _sync = new object();
        public Matrix(float x1, float y1, float x2, float y2, float dx, float dy)
        {
            this.m_elements = new float[] { 
                x1, y1,0,0,
                x2, y2,0,0,
                0, 0 , 1, 0,
                dx, dy,0,0
            };
        }
        public Matrix(float[] elements)
        {
            if ((elements == null) || (elements .Length != MATRIX_LENGTH ))
                throw new ArgumentException($"{nameof(elements)}");
            this.m_elements = elements;
        }
        public bool IsIdentity
        {
            get
            {
                if (this.m_elements ==null)
                    return false;
                return (m_elements[0] == 1) &&
                       (m_elements[5] == 1) &&
                       (m_elements[10] == 1) &&
                       (m_elements[15] == 1) &&
                       (m_elements[1] == 0) &&
                       (m_elements[2] == 0) &&
                       (m_elements[3] == 0) &&
                       (m_elements[4] == 0) &&
                       (m_elements[6] == 0) &&
                       (m_elements[7] == 0) &&
                       (m_elements[8] == 0) &&
                       (m_elements[9] == 0) &&
                       (m_elements[11] == 0) &&
                       (m_elements[12] == 0) &&
                       (m_elements[13] == 0) &&
                       (m_elements[14] == 0);
            }
        }
        public void Dispose()
        {
            //free resources
            this._sync = null;
            this.m_elements = null;
        }
        public object Clone()
        {
            if (this.m_elements == null)
                return null;
            float[] t = this.m_elements.Clone() as float[];
            Matrix c = new Matrix();
            c.m_elements = t;
            return c;
        }
        internal static Rectanglef Tranform(Matrix m, Rectanglef cvp)
        {
            throw new NotImplementedException();
        }
        public void Invert()
        {
            if (invertMatrix(this.m_elements, out float[] outE))
            {
                this.m_elements = outE;
            }
        }
        bool invertMatrix(float[] m , out float[] invOut )
        {
            float det = 0;
    int i;
    float[] inv = Getinv(m);            
    det = GetDeterminant(m, inv);
    invOut = null;
    if (det == 0)
        return false;
    det = 1.0f / det;
    invOut = new float[16];
    for (i = 0; i < 16; i++)
        invOut[i] = inv[i] * det;
    return true;
        }
        public bool IsInvertible { get {            
            return ((this.m_elements !=null) && GetDeterminant(this.m_elements  )!= 0.0f);
        } }
        /// <summary>
        /// calculate determinant
        /// </summary>
        /// <returns></returns>
        private float GetDeterminant(float[] m)
        {
            if (m == null)
                return 0;
            float det = 0.0f;
            float[] inv = Getinv(m);            
            det = m[0] * inv[0] + m[1] * inv[4] + m[2] * inv[8] + m[3] * inv[12];
            return det;
        }
        private float GetDeterminant(float[] m, float[] inv)
        {
            float det = 0.0f;
            det = m[0] * inv[0] + m[1] * inv[4] + m[2] * inv[8] + m[3] * inv[12];
            return det;
        }
        private float[] Getinv(float[] m)
        {
            if (m == null)
                return null;
            float[] inv = new float[16];
            inv[0] = m[5] * m[10] * m[15] -
                     m[5] * m[11] * m[14] -
                     m[9] * m[6] * m[15] +
                     m[9] * m[7] * m[14] +
                     m[13] * m[6] * m[11] -
                     m[13] * m[7] * m[10];
            inv[4] = -m[4] * m[10] * m[15] +
                      m[4] * m[11] * m[14] +
                      m[8] * m[6] * m[15] -
                      m[8] * m[7] * m[14] -
                      m[12] * m[6] * m[11] +
                      m[12] * m[7] * m[10];
            inv[8] = m[4] * m[9] * m[15] -
                     m[4] * m[11] * m[13] -
                     m[8] * m[5] * m[15] +
                     m[8] * m[7] * m[13] +
                     m[12] * m[5] * m[11] -
                     m[12] * m[7] * m[9];
            inv[12] = -m[4] * m[9] * m[14] +
                       m[4] * m[10] * m[13] +
                       m[8] * m[5] * m[14] -
                       m[8] * m[6] * m[13] -
                       m[12] * m[5] * m[10] +
                       m[12] * m[6] * m[9];
            inv[1] = -m[1] * m[10] * m[15] +
                      m[1] * m[11] * m[14] +
                      m[9] * m[2] * m[15] -
                      m[9] * m[3] * m[14] -
                      m[13] * m[2] * m[11] +
                      m[13] * m[3] * m[10];
            inv[5] = m[0] * m[10] * m[15] -
                     m[0] * m[11] * m[14] -
                     m[8] * m[2] * m[15] +
                     m[8] * m[3] * m[14] +
                     m[12] * m[2] * m[11] -
                     m[12] * m[3] * m[10];
            inv[9] = -m[0] * m[9] * m[15] +
                      m[0] * m[11] * m[13] +
                      m[8] * m[1] * m[15] -
                      m[8] * m[3] * m[13] -
                      m[12] * m[1] * m[11] +
                      m[12] * m[3] * m[9];
            inv[13] = m[0] * m[9] * m[14] -
                      m[0] * m[10] * m[13] -
                      m[8] * m[1] * m[14] +
                      m[8] * m[2] * m[13] +
                      m[12] * m[1] * m[10] -
                      m[12] * m[2] * m[9];
            inv[2] = m[1] * m[6] * m[15] -
                     m[1] * m[7] * m[14] -
                     m[5] * m[2] * m[15] +
                     m[5] * m[3] * m[14] +
                     m[13] * m[2] * m[7] -
                     m[13] * m[3] * m[6];
            inv[6] = -m[0] * m[6] * m[15] +
                      m[0] * m[7] * m[14] +
                      m[4] * m[2] * m[15] -
                      m[4] * m[3] * m[14] -
                      m[12] * m[2] * m[7] +
                      m[12] * m[3] * m[6];
            inv[10] = m[0] * m[5] * m[15] -
                      m[0] * m[7] * m[13] -
                      m[4] * m[1] * m[15] +
                      m[4] * m[3] * m[13] +
                      m[12] * m[1] * m[7] -
                      m[12] * m[3] * m[5];
            inv[14] = -m[0] * m[5] * m[14] +
                       m[0] * m[6] * m[13] +
                       m[4] * m[1] * m[14] -
                       m[4] * m[2] * m[13] -
                       m[12] * m[1] * m[6] +
                       m[12] * m[2] * m[5];
            inv[3] = -m[1] * m[6] * m[11] +
                      m[1] * m[7] * m[10] +
                      m[5] * m[2] * m[11] -
                      m[5] * m[3] * m[10] -
                      m[9] * m[2] * m[7] +
                      m[9] * m[3] * m[6];
            inv[7] = m[0] * m[6] * m[11] -
                     m[0] * m[7] * m[10] -
                     m[4] * m[2] * m[11] +
                     m[4] * m[3] * m[10] +
                     m[8] * m[2] * m[7] -
                     m[8] * m[3] * m[6];
            inv[11] = -m[0] * m[5] * m[11] +
                       m[0] * m[7] * m[9] +
                       m[4] * m[1] * m[11] -
                       m[4] * m[3] * m[9] -
                       m[8] * m[1] * m[7] +
                       m[8] * m[3] * m[5];
            inv[15] = m[0] * m[5] * m[10] -
                      m[0] * m[6] * m[9] -
                      m[4] * m[1] * m[10] +
                      m[4] * m[2] * m[9] +
                      m[8] * m[1] * m[6] -
                      m[8] * m[2] * m[5];
            return inv;
        }
        public void Multiply(Matrix v_m, enuMatrixOrder enuMatrixOrder)
        {
            Matrix m = null;
            if (this.m_elements == null) {
                this.m_elements =(float[]) v_m.m_elements.Clone();
                return;
            }
            if (enuMatrixOrder == ICore.enuMatrixOrder.Append)
                m = this * v_m;
            else
                m = v_m * this;
            this.m_elements = m.m_elements.Clone() as float[];
            m.Dispose();

        }
        public void Multiply(float[] matrixTab, enuMatrixOrder order)
        {
            if ((matrixTab != null) && (matrixTab.Length == MATRIX_LENGTH))
                this.m_elements = mult_matrix(matrixTab, order);
           

        }
        public void Scale(float ex, float ey, enuMatrixOrder order)
        {
            this.Scale(ex, ey, 1.0f, order);
        }
        public void Scale(float ex, float ey, float ez, enuMatrixOrder order)
        {
            Matrix v_tmat = new Matrix();
            v_tmat.m_elements[0] *= ex;
            v_tmat.m_elements[5] *= ey;
            v_tmat.m_elements[10] *= ez;
            float[] tr = mult_matrix(v_tmat.m_elements , order  );
            this.m_elements = tr;
            v_tmat.Dispose();
        }
        /// <summary>
        /// multiply matrix
        /// </summary>
        /// <param name="mat1">p matrix</param>
        /// <param name="mat2">this matrix</param>
        /// <param name="order"></param>
        /// <returns></returns>
        private  float[] mult_matrix(float[] mat1, enuMatrixOrder order)
        {
            if (this.m_elements == null)
                return null;
            float[] v_tmat = null;
            if (order == enuMatrixOrder.Prepend)
                v_tmat = mult_matrix(mat1, this.m_elements );
            else
                v_tmat = mult_matrix(this.m_elements, mat1);
            return v_tmat;
        }
        private void Translate(float dx, float dy, float dz, enuMatrixOrder order)
        {
            if (this.IsDisposed)
                return;
            Matrix v_tmat = new Matrix();
            v_tmat.m_elements[12] = dx;
            v_tmat.m_elements[13] = dy;
            v_tmat.m_elements[14] = dz;

           // v_tmat.Transpose();
            float[] tr = mult_matrix(v_tmat.m_elements, order);
            this.m_elements = tr;

            v_tmat.Dispose();
        }
        public void Translate(float dx, float dy)
        {
            this.Translate(dx, dy, 0, enuMatrixOrder.Append );
        }
        public static Matrix ConvertFromString(string[] v_t)
        {
            if ((v_t == null) || ((v_t.Length != 6) || (v_t.Length == 16)))
                return Matrix.Identity.Clone() as Matrix;
            int k = 0;
            Matrix c = new Matrix();
            if (v_t.Length == 6)
            {
                //for (int i = 0; i < 2; i++)
                //{
                //    for (int j = 0; j < 3; j++)
                //    {
                //        c.m_elements[(j * 4) + i] = float.Parse(v_t[k]);
                //        k++;
                //    }
                //}
                c.m_elements[0] = float.Parse(v_t[0]);
                c.m_elements[1] = float.Parse(v_t[1]);
                c.m_elements[4] = float.Parse(v_t[2]);
                c.m_elements[5] = float.Parse(v_t[3]);
                c.m_elements[12] = float.Parse(v_t[4]);
                c.m_elements[13] = float.Parse(v_t[5]);
                //c.m_elements[0] = float.Parse(v_t[0]);

            }
            else
            if (v_t.Length == 16)
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        c.m_elements[(j * 4) + i] = float.Parse(v_t[k]);
                        k++;
                    }
                }
            }
            return c;
        }
        public static string ToString(Matrix matrix)
        {   
            if (matrix.m_elements !=null)
                return string.Join (",", matrix.m_elements);
            return null;
        }
        public override string ToString()
        {
            return string.Format (GetType().Name +"["+ Matrix.ToString(this)+"]");
        }
        public void Reset()
        {
            if (this.IsDisposed)
                return;
            this.m_elements[0] = this.m_elements[5]=this.m_elements[10] = this.m_elements[15]=1;
            this.m_elements[1] = this.m_elements[2] =this.m_elements[3]=this.m_elements[4]=0;
            this.m_elements[6] = this.m_elements[7] = this.m_elements[8] = this.m_elements[9] = 0;
            this.m_elements[11] = this.m_elements[12] = this.m_elements[13] = this.m_elements[14] = 0;
        }
        /// <summary>
        /// for ortho transform
        /// </summary>
        /// <param name="x"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <param name="near"></param>
        /// <param name="far"></param>
        public void Ortho(float x, float x1, int y1, int y2, int near, int far)
        {
        }
        public static implicit operator Matrix(string value)
        { 
            return ConvertFromString(value.Split (new char[]{','},  StringSplitOptions.RemoveEmptyEntries ));
        }
        public override bool Equals(object obj)
        {
            Matrix m = obj as Matrix;
            if ((m!=null) && (m.m_elements !=null) && (this.m_elements != null))
            {
                
                for (int i = 0; i < m.m_elements.Length; i++)
                {
                    if (this.m_elements[i] != m.m_elements[i])
                        return false;
                }
                return true;
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //public static bool operator == (Matrix m1 , Matrix m2)
        //{
        //    for (int i = 0; i < m1.m_elements.Length; i++)
        //    {
        //        if (m1.m_elements[i] != m2.m_elements[i])
        //            return false ;
        //    }
        //    return true ;
        //}
        //public static bool operator !=(Matrix m1, Matrix m2)
        //{
        //    if ((m1 != null) && (m2 != null))
        //    {
        //        for (int i = 0; i < m1.m_elements.Length; i++)
        //        {
        //            if (m1.m_elements[i] == m2.m_elements[i])
        //                return false;
        //        }
        //    }
        //    return true;
        //}
        /// <summary>
        /// rotate in 
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="center"></param>
        /// <param name="mat"></param>
        public  void Rotate(float angle, Vector2f center, enuMatrixOrder mat)
        {
            Matrix v_tmp = new Matrix();
            v_tmp.Translate(-center.X, -center.Y, enuMatrixOrder.Append);
            v_tmp.Rotate(-angle, enuMatrixOrder.Append);
            v_tmp.Translate(center.X, center.Y, enuMatrixOrder.Append);
            this.m_elements = mult_matrix(v_tmp.m_elements, mat);
            v_tmp.Dispose();            
        }
        public void Scale(float ex, float ey, float ez)
        {
            this.Scale(ex, ey, ez, enuMatrixOrder.Prepend);
        }
        public void Scale(float ex, float ey){
            this.Scale(ex, ey, enuMatrixOrder.Append);
        }
        public void Shear(float shearx, float sheary)
        {
            this.Shear(shearx, sheary, 0.0f, enuMatrixOrder.Append);
        }
        public void Shear(float shearx, float sheary, enuMatrixOrder order)
        {
            this.Shear(shearx, sheary, 0.0f, order);
        }
        public void Shear(float shearx, float sheary, float shearz, enuMatrixOrder order)
        {
            Matrix v_tmat = new Matrix();
            v_tmat.m_elements[2] += shearx;
            v_tmat.m_elements[4] += sheary;
            //v_tmat.m_elements[14] += dz;
            float[] tr = mult_matrix(v_tmat.m_elements, order);
            this.m_elements = tr;
            v_tmat.Dispose();
        }
        /// <summary>
        /// load matrix from string
        /// </summary>
        /// <param name="m"></param>
        public void LoadString(string m)
        {
            string[] t = m.Split(new string[]{";",","}, StringSplitOptions.RemoveEmptyEntries  );
            
            switch  (t.Length)
            { 
                case 6:
                    this.Reset();
                    this.m_elements[0] = Convert.ToSingle(t[0]);
                    this.m_elements[1] = Convert.ToSingle(t[1]);
                    this.m_elements[4] = Convert.ToSingle(t[2]);
                    this.m_elements[5] = Convert.ToSingle(t[3]);
                    this.m_elements[12] = Convert.ToSingle(t[4]);
                    this.m_elements[13] = Convert.ToSingle(t[5]);
                    break;
                case 16:
                    this.Reset();
                    for (int i = 0; i < t.Length; i++)
                    {
                    this.m_elements[i] = Convert.ToSingle(t[i]);    
                    }
                    break;
                default :
                    break; 

            }
            
        }

        public static Matrix CreateFrustum(float xmin, float xmax, float ymin, float ymax, float near, float far)
        {
            Matrix m = new Matrix();
            m.m_elements[0] = 2 * near / (xmax - xmin);
            m.m_elements[5] = 2 * near / (ymax - ymin);
            m.m_elements[8] = (xmax  + xmin) / (xmax - xmin);
            m.m_elements[9] = (ymax + ymin) / (ymax - ymin);
            m.m_elements[10] = -(far + near) / (far - near);
            m.m_elements[11] = -1;
            m.m_elements[14] = -(2 * far * near) / (far - near);
            m.m_elements[15] = 0;
            return m;
        }
        public static Matrix CreateFrustum(float fovY, float aspect, float front, float back)
        {
            float tangent =(float) Math.Tan(fovY / 2 * CoreMathOperation.ConvDgToRadian); // tangent of half fovY
            float height = front * tangent;         // half height of near plane
            float width = height * aspect;          // half width of near plane

            // params: left, right, bottom, top, near, far
            return CreateFrustum(-width, width, -height, height, front, back);
        }
        /// <summary>
        /// create ortho 
        /// </summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <param name="b"></param>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static Matrix CreateOrthoFrustum(float l, float r, float b, float t, float n, float f)
        {
            Matrix v_mat = new Matrix ();
            var mat = v_mat.m_elements;
            mat[0] = 2 / (r - l);
            mat[5] = 2 / (t - b);
            mat[10] = -2 / (f - n);
            mat[12] = -(r + l) / (r - l);
            mat[13] = -(t + b) / (t - b);
            mat[14] = -(f + n) / (f - n);
            return v_mat;
        }

        public void Translate(float x, float y, float  z)        {
            this.Translate(x, y, z, enuMatrixOrder.Prepend  );
        }
        public void Transpose()
        {
            //transpose matrix
            float[] t = new float[this.m_elements.Length ];
            this.m_elements.CopyTo(t,0);
            //this.m_elements[0] = t[0];
            //this.m_elements[10] = t[10];
            //this.m_elements[5] = t[5];
            //this.m_elements[15] = t[15];

            this.m_elements[1] = t[4];
            this.m_elements[2] = t[8];
            this.m_elements[3] = t[12];
            this.m_elements[4] = t[1];
            this.m_elements[6] = t[9];
            this.m_elements[7] = t[13];
            this.m_elements[8] = t[2];
            this.m_elements[9] = t[6];
            this.m_elements[11] = t[14];
            this.m_elements[12] = t[3];
            this.m_elements[13] = t[7];
            this.m_elements[14] = t[11];

        }
        /// <summary>
        /// rotate this matrix to current angle
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="vector3f"></param>
        public void Rotate(float angle, Vector3f vector3f)
        {
            var n = vector3f;
            n.Normalize();
            var a = angle * CoreMathOperation.ConvDgToRadian;
            var c = (float)Math.Cos(a);
            var s = (float)Math.Sin(a);

            var t = new float[]{
                (n.X * n.X )*(1-c) + c, (n.X*n.Y*(1-c)) -( n.Z*s), (n.X*n.Z*(1-c)) +( n.Y *s) , 0,
                (n.Y * n.X )*(1-c) + (n.Z*s), (n.Y*n.Y*(1-c)) +(c), (n.Y*n.Z*(1-c)) +( n.X *s) , 0,
                (n.X * n.Z )*(1-c) -(n.Y *s), (n.Y*n.Z*(1-c)) -( n.X*s), (n.Z*n.Z*(1-c)) +( c) , 0,
                0, 0, 0, 1
            
            };
            this.m_elements = mult_matrix(t, enuMatrixOrder.Prepend);
        }

        public static Matrix Transpose(Matrix matrix)
        {
            Matrix m = new Matrix();
            //copy element
             matrix.m_elements .CopyTo (m.m_elements,0);
             m.Transpose();
            return m;
        }
    }
}

