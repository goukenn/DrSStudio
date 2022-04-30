

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsProjection.cs
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
file:GraphicsProjection.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
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
using System.Runtime.InteropServices;

using IGK.GLLib;
namespace IGK.OGLGame.Graphics
{
    using IGK.OGLGame.Math ;
    using IGK.ICore;
    public class GraphicsProjection
    {
        OGLGraphicsDevice m_graphicsDevice;
        public OGLGraphicsDevice GraphicsDevice { get { return this.m_graphicsDevice; } }
        //constructor
        internal GraphicsProjection(OGLGraphicsDevice device)
        {
            this.m_graphicsDevice = device;
        }
        /// <summary>
        /// get or set the matrix mode
        /// </summary>
        public MatrixMode MatrixMode{
            get{
                return GraphicsDevice.GetIntegerv<MatrixMode >(GL.GL_MATRIX_MODE);
            }
            set {
                GL.glMatrixMode((uint)value);
            }
        }
        public void PushMatrix()
        {
            GL.glPushMatrix();
        }
        public void PopMatrix()
        {
            GL.glPopMatrix();
        }
        public void LoadIdentity()
        {
            GL.glLoadIdentity();
        }
        public void Rotate(float angle, float x, float y, float z)
        {
            GL.glRotatef(angle, x, y, z);
        }
        public void Rotate(float angle, Vector3f vector)
        {
            GL.glRotatef(angle, vector.X , vector.Y, vector.Z);
        }
        public void Translate(float x, float y, float z)
        {
            GL.glTranslatef(x, y, z);
        }
        public void Scale(float x, float y, float z)
        {
            GL.glScalef(x, y, z);
        }
        public void SetOrtho2D(double  minx, double maxx, double miny, double maxy)
        {            
            GLU.gluOrtho2D(minx, maxx, miny, maxy);         
        }
        public void SetFrustum(float minx, float maxx, float miny, float maxy, float znear, float zfar)
        {
            GL.glFrustum(minx, maxx, miny, maxy, znear, zfar);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="posx">position x</param>
        /// <param name="posy">position y</param>
        /// <param name="posz">position z</param>
        /// <param name="ex">eye look at x</param>
        /// <param name="ey">eye look at y</param>
        /// <param name="ez">eye look at z</param>
        /// <param name="orientationx">camera orientation x</param>
        /// <param name="orientationy">camera orientation y</param>
        /// <param name="orientationz">camera orientation z</param>
        public void LookAt(float posx, float posy, float posz, 
            float ex, float ey, float ez,
            float orientationx, float orientationy, float orientationz)
        {
            GLU.gluLookAt(posx, posy, posz,
                ex, ey, ez, 
                orientationx, orientationy, orientationz);
        }
        public void LookAt(Vector3f position,
            Vector3f eye,
            Vector3f orientation)
        {
            GLU.gluLookAt(position .X, position .Y, position .Z ,
                eye.X, eye.Y , eye.Z ,
                orientation.X, orientation.Y, orientation.Z );
        }
        public void SetPerspective(float fovy, float aspect, float near, float far)
        {
            GLU.gluPerspective(fovy, aspect, near, far);
        }
        public void LoadMatrix(Matrix matrix)
        { 
            GL.glLoadMatrixf(matrix.Elements );
        }
        public void MultMatrix(Matrix matrix)
        {
            MultMatrix(matrix, enuMatrixOrder.Append);    
        }
        public void MultMatrix(Matrix matrix, enuMatrixOrder order)
        {
            GL.glMultMatrixf(matrix.Elements);
        }
        public void LoadTransposeMatrix(Matrix matrix)
        {
            GL.glLoadTransposeMatrixf(matrix.Elements);
        }
        public void MultTransposeMatrix(Matrix matrix)
        {
            GL.glMultTransposeMatrixf (matrix.Elements);            
        }
        /// <summary>
        /// get the max modelview stack depth
        /// </summary>
        public int MaxModelViewStackDepth {
            get { 
                return this .GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_MODELVIEW_STACK_DEPTH );            
            }
        }
        /// <summary>
        /// get the max projection stact depth
        /// </summary>
        public int MaxProjectViewStactDeth {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_PROJECTION_STACK_DEPTH);
            }
        }
        public int MaxClipPlanes {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_CLIP_PLANES);
            }
        }
        public void SetOrtho(float xmin, float xmax, float ymin, float ymax, float znear, float zfar)
        {
            GL.glOrtho(xmin, xmax, ymin, ymax, znear, zfar);
        }
        public Matrix GetModelViewMatrix()
        {
            float[] v_data =  OGLGraphicsDevice.GetFloatv(GL.GL_MODELVIEW_MATRIX, 16);
            return new Matrix(v_data);
        }
        public Matrix GetProjectionMatrix()
        {
            return GraphicsDevice.GetFloatv<Matrix>(GL.GL_PROJECTION_MATRIX);
        }
        public double[] GetModelViewMatrixd()
        {
            return OGLGraphicsDevice.GetDoublev(GL.GL_MODELVIEW_MATRIX , 16);
        }
        public double[] GetProjectionMatrixd()
        {
            return OGLGraphicsDevice.GetDoublev(GL.GL_PROJECTION_MATRIX,16);
        }
        /// <summary>
        /// get world transform from screen position
        /// </summary>
        /// <param name="screenPos"></param>
        /// <returns></returns>
        public Vector3f[] GetWordLocation(Vector2f screenPos)
        {
            Vector3f globalNearLocation = Vector3f.Zero;
            Vector3f globalFarLocation = Vector3f.Zero;
            Vector3f cameraLocation = Vector3f.Zero;
            Rectanglei v_rcviewport = this.m_graphicsDevice.Viewport;
            //get the current model view matrix
            double[] v_modelMat = this.m_graphicsDevice .Projection.GetModelViewMatrixd();
            double[] v_projMat = this.m_graphicsDevice .Projection.GetProjectionMatrixd();
            float realy = v_rcviewport.Height - screenPos.Y -1;
            Matrix m =  v_modelMat;
            Matrix pm = v_projMat;
            pm.Invert();
            m.Invert();
            m.Multiply (pm);
            cameraLocation = Vector3f.Zero * m;
            Vector3f result = new Vector3f();
            //double gx = 0;
            //double gy = 0;
            //double gz = 0;
            Vector3f eye = new Vector3f(screenPos.X, realy, 0.0f);
            MatrixExtension.GluUnProject(
                eye,
                v_modelMat,
                v_projMat,
                v_rcviewport,
                ref result);
            globalNearLocation = result;
            eye = new Vector3f(screenPos.X, realy, 1.0f);
            MatrixExtension.GluUnProject(
                eye,
                v_modelMat,
                v_projMat,
                v_rcviewport,
                ref result);
            globalFarLocation = result;
            return new Vector3f[] { 
                globalNearLocation ,
                globalFarLocation,
                cameraLocation
            };
        }
        /// <summary>
        /// return the screen location
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Vector2f GetScreenLocation(Vector3f wordpoint)
        {
            Rectanglei v_rcviewport = this.m_graphicsDevice.Viewport;
            double[] v_modelMat = this.m_graphicsDevice .Projection.GetModelViewMatrixd();
            double[] v_projMat = this.m_graphicsDevice .Projection.GetProjectionMatrixd();
            Vector3f v = Vector3f.Zero  ;
            MatrixExtension.GluProject(
                wordpoint ,
                v_modelMat,
                v_projMat ,
                v_rcviewport ,
                ref v);
            return new Vector2f (v.X, v_rcviewport .Height - v.Y );
        }
        public Vector3f GetWordNearLocation(Vector2f screenPos)
        {
            return GetWordLocation(screenPos)[0];
        }
        public Vector3f GetWordFarLocation(Vector2f screenPos)
        {
            return GetWordLocation(screenPos)[1];
        }
        public Vector3f GetWordCameraLocation()
        {
            Rectanglei v_rcviewport = this.m_graphicsDevice.Viewport;
            return GetWordLocation(new Vector2f(
                v_rcviewport.X + 
                v_rcviewport.Width/2.0f , 
                v_rcviewport .Y + 
                v_rcviewport.Height /2.0f))[2];
        }
        /// <summary>
        /// set the matrix mode and push it . the matrix mode remain
        /// </summary>
        /// <param name="matrixMode"></param>
        public void PushMatrix(MatrixMode matrixMode)
        {
            this.MatrixMode = matrixMode;
            this.PushMatrix();
        }
/// <summary>
/// pop the matrix mode
/// </summary>
/// <param name="matrixMode"></param>
        public void PopMatrix(MatrixMode matrixMode)
        {
            this.MatrixMode = matrixMode;
            this.PopMatrix();
        }
        public Vector3f ProjectCoordToScreen(Vector3f vector)
        {
            double x = 0.0f;
            double  y = 0.0f;
            double z = 0.0f;
            double[] model = this.GetModelViewMatrixd ();
            double[] proj =  this.GetProjectionMatrixd ();
            Rectanglei rc = this.GraphicsDevice.Viewport;
            int[] t = new int[] { rc.X, rc.Y, rc.Width, rc.Height };
            IntPtr v_hrc = Marshal.AllocCoTaskMem (4 * Marshal.SizeOf (typeof(int)));
            IntPtr v_hmodel = Marshal.UnsafeAddrOfPinnedArrayElement (model,0 );
            IntPtr v_hproj = Marshal.UnsafeAddrOfPinnedArrayElement (proj,0 );
            Marshal.StructureToPtr(rc, v_hrc, true);
            bool r = GLU.gluProject(vector.X, vector.Y, vector.Z,
                v_hmodel, v_hproj,
                v_hrc , ref x, ref y, ref z);
            Marshal.FreeCoTaskMem(v_hrc);
            if (r)
            {
                return new Vector3f(x, y, z);
            }
            return Vector3f.Zero;
        }
        /// <summary>
        /// get a current viewport bound
        /// </summary>
        /// <param name="rc"></param>
        /// <returns></returns>
        public Rectanglef GetViewPortBound(Rectanglef rc)
        {
            Vector3f screenf = this.ProjectCoordToScreen(rc.Location);
            Vector3f pc = this.ProjectCoordToScreen(rc.Location + new Vector2f(rc.Width, rc.Height));
            return screenf.GetBound (pc);
        }
    }
}

