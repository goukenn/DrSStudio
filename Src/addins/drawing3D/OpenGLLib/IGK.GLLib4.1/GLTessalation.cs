

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLTessalation.cs
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
file:GLTessalation.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// represent a tessalator object
    /// </summary>
    public class GLTessalation : IDisposable
    {
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct TestCombineData
        {
            internal vect4f data1;
            internal vect4f data2;
            internal vect4f data3;
            internal vect4f data4;
            public vect4f Data1 { get { return data1; } }
            public vect4f Data2 { get { return data2; } }
            public vect4f Data3 { get { return data3; } }
            public vect4f Data4 { get { return data4; } }
        }
        public delegate void BeginTessPFN(uint mode);
        public delegate void EndTessPFN();
        public delegate void VertexPFN(IntPtr vertex);
        public delegate void CombineDataCallPFN(IntPtr  coords, IntPtr data, IntPtr weight, out IntPtr coord);
        private IntPtr m_Handle;
        private bool m_ThrowExceptionOnError;
        public bool ThrowExceptionOnError
        {
            get { return m_ThrowExceptionOnError; }
            set
            {
                if (m_ThrowExceptionOnError != value)
                {
                    m_ThrowExceptionOnError = value;
                }
            }
        }
        public IntPtr Handle
        {
            get { return m_Handle; }
        }
        #region IDisposable Members
        private GLTessalation()
        {
            this.m_ThrowExceptionOnError = true;
        }
        /// <summary>
        /// create a new tessalation object
        /// </summary>
        /// <returns></returns>
        public static GLTessalation CreateNewTess()
        {
            IntPtr h = GLU.gluNewTess();
            if (h == IntPtr.Zero)
                return null;
            GLTessalation v_tess = new GLTessalation();
            v_tess.m_Handle = h;
            v_tess.RegisterError();
            return v_tess;
        }
        /// <summary>
        /// register call back error
        /// </summary>
        private void RegisterError()
        {
            GLU.gluTessCallback(this.Handle, enuGLUTessCallback.Error, new TessErrorPROC(TessError));
        }
        delegate void TessErrorPROC(uint errorCode);
        void TessError(uint errorCode)
        {
            IntPtr s = GLU.gluErrorString(errorCode);
            string v_s = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(s);
            if (m_ThrowExceptionOnError)
            {
                GLTessException ex = new GLTessException(v_s);
                throw ex;
            }
            else {
                System.Diagnostics.Debug.WriteLine(v_s);
            }
        }
        public void Dispose()
        {
            //free resources an call collect
            GLU.gluDeleteTess(this.Handle);
            GC.Collect();
            //free handle
            this.m_Handle = IntPtr.Zero;
        }
        #endregion
        public double Tolerance
        {
            set {
            GLU.gluTessProperty(this.Handle, GLU.GLU_TESS_TOLERANCE, value);
            }
            get {
                double value = 0.0f;
                GLU.gluGetTessProperty(this.Handle, GLU.GLU_TESS_TOLERANCE, ref value);
                return value;
            }
        }
        public bool  BoundaryOnly
        {
            set
            {
                GLU.gluTessProperty(this.Handle, GLU.GLU_TESS_BOUNDARY_ONLY, value);
            }
            get
            {
                bool value = false;
                GLU.gluGetTessProperty(this.Handle, GLU.GLU_TESS_BOUNDARY_ONLY, ref value);
                return value;
            }
        }
        public enuGLUTessWindingRule TessWindingRule
        {
            set
            {
                GLU.gluTessProperty(this.Handle, GLU.GLU_TESS_WINDING_RULE, (uint)value);
            }
            get{
                uint  value = 0;
                GLU.gluGetTessProperty(this.Handle, GLU.GLU_TESS_WINDING_RULE, ref value);
                return (enuGLUTessWindingRule) value;
            }
        }
        public void BeginPolygon(object syncObject)
        {
            IntPtr c = IntPtr.Zero;            
            GLU.gluTessBeginPolygon(this.Handle, c);
        }
        public void EndPolygon()
        {
            GLU.gluTessEndPolygon(this.Handle);
        }
        public void BeginContour()
        {
            GLU.gluTessBeginContour(this.Handle);
        }
        public void EndContour()
        {
            GLU.gluTessEndContour(this.Handle);
        }
        public void RegisterVertexCallback(Delegate del)
        {
            GLU.gluTessCallback(this.Handle, enuGLUTessCallback.Vertex, del);
        }
        public void RegisterVertexDataCallback(Delegate del)
        {
            GLU.gluTessCallback(this.Handle, enuGLUTessCallback.VertexData, del);
        }
        public void RegisterCallBack(enuGLUTessCallback callbackType, Delegate del)
        {
            if (callbackType == enuGLUTessCallback.Error)
                return;
            GLU.gluTessCallback(this.Handle, callbackType, del);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="coords"></param>
        /// <param name="vertexData"></param>
        public void SetVertex(double[] coords, double[] vertexData)
        {
            if ((coords != null) && (coords.Length == 3))
            {
                GLU.gluTessVertex(this.Handle, coords, vertexData);
            }
        }
        public void SetNormal(double x, double y, double z)
        {
            GLU.gluTessNormal(this.Handle, x, y, z);            
        }
    }
}

