

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLQuadric.cs
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
file:GLQuadric.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// represent a glu quadric element
    /// </summary>
    public class GLQuadric : IDisposable 
    {
        delegate void QuadricERROCPFN(uint code);
        private IntPtr m_Handle;
        private bool m_ThrowErrorException;
        private  GLQuadric()
        {
            this.m_ThrowErrorException = true;
        }
        public bool ThrowErrorException
        {
            get { return m_ThrowErrorException; }
            set
            {
                if (m_ThrowErrorException != value)
                {
                    m_ThrowErrorException = value;
                }
            }
        }
        void HandleError(uint errorCode)
        {
            IntPtr s = GLU.gluErrorString(errorCode);
            string v_s = System.Runtime.InteropServices.Marshal.PtrToStringAnsi(s);
            if (this.m_ThrowErrorException )
            {
                GLQuadricException ex = new GLQuadricException(v_s);
                throw ex;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(v_s);
            }
        }
        public IntPtr Handle
        {
            get { return m_Handle; }
        }
        #region IDisposable Members
        public void Dispose()
        {
            GLU.gluDeleteQuadric(this.Handle);
            this.m_Handle = IntPtr.Zero;
            GC.Collect();
        }
        #endregion
        public static GLQuadric CreateNewQuadric()
        {
            IntPtr v_h = GLU.gluNewQuadric();
            if (v_h == IntPtr.Zero)
                return null;
            GLQuadric quadric = new GLQuadric();
            quadric.m_Handle = v_h;
            GLU.gluQuadricCallback(v_h, GLU.GLU_ERROR, new QuadricERROCPFN(quadric.HandleError));
            return quadric;
        }
        /// <summary>
        /// draw disk
        /// </summary>
        /// <param name="innerradius"></param>
        /// <param name="outerradius"></param>
        /// <param name="ring"></param>
        /// <param name="slices"></param>
        public void DrawDisk(double innerradius, double outerradius, int ring , int slices)
        {
            GLU.gluDisk(this.m_Handle, innerradius, outerradius, ring, slices);
        }
        /// <summary>
        /// draw sphere
        /// </summary>
        /// <param name="radius"></param>
        /// <param name="slices"></param>
        /// <param name="stacks"></param>
        public void DrawSphere(double radius, int slices, int stacks)
        {
            GLU.gluSphere(this.m_Handle, radius, slices, stacks);
        }
        /// <summary>
        /// draw cylinder
        /// </summary>
        /// <param name="baseRadius"></param>
        /// <param name="topRadius"></param>
        /// <param name="height"></param>
        /// <param name="slices"></param>
        /// <param name="stacks"></param>
        public void DrawCylinder(double baseRadius, double topRadius, double height, int slices, int stacks)
        {
            GLU.gluCylinder (this.m_Handle, baseRadius, topRadius, height,  slices, stacks);
        }
        public void DrawPartialDisk(double innerRadius, double outerRadius, int slices, int rings, double startAngle, double sweepAngle)
        {
            GLU.gluPartialDisk(this.m_Handle, innerRadius, outerRadius, slices, rings, startAngle, sweepAngle);
        }
        public void SetQuadricStyle(enuGLQuadricDrawStyle style)
        {
            GLU.gluQuadricDrawStyle(this.m_Handle, (uint)style);
        }
        public void SetQuadricNormalStyle(enuGLQuadricNormalStyle style)
        {
            GLU.gluQuadricNormals(this.m_Handle, (uint)style);
        }
        public void SetQuadricOrientation(enuGLQuadricOrientation orientation)
        {
            GLU.gluQuadricOrientation (this.m_Handle, (uint)orientation);
        }
        public void EnableQuadricTexture(uint coord)
        {
            GLU.gluQuadricTexture(this.m_Handle, coord);
        }
    }
}

