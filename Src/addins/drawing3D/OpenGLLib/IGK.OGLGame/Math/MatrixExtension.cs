

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MatrixUtils.cs
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
file:MatrixUtils.cs
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
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using IGK.GLLib;
namespace IGK.OGLGame.Math
{
    using IGK.OGLGame.Graphics;
    using IGK.ICore;
    using IGK.ICore.Data.Math;
    public static class MatrixExtension
    {
        public static void LookAt(this Matrix matrix, Vector3f position, Vector3f target, Vector3f upvector) { 
            //left H
//            zaxis = normal(cameraTarget - cameraPosition)
//xaxis = normal(cross(cameraUpVector, zaxis))
//yaxis = cross(zaxis, xaxis)

// xaxis.x           yaxis.x           zaxis.x          0
// xaxis.y           yaxis.y           zaxis.y          0
// xaxis.z           yaxis.z           zaxis.z          0
//-dot(xaxis, cameraPosition)  -dot(yaxis, cameraPosition)  -dot(zaxis, cameraPosition)  1

            var zaxis = Vector3f.Normalize (target  - position);
var xaxis = Vector3f.NormCrossProduct (upvector, zaxis);
var yaxis = Vector3f.CrossProduct (zaxis, xaxis);
            var t = new float[]{
 xaxis.X  ,        yaxis.X     ,    zaxis.X     ,  0      ,
 xaxis.Y  ,        yaxis.Y     ,    zaxis.Y     ,  0      ,
 xaxis.Z  ,        yaxis.Z     ,    zaxis.Z     ,  0      ,
-Vector3f.Dot (xaxis, position ) , -Vector3f.Dot (yaxis, position ) , -Vector3f.Dot (zaxis, position) , 1
        };

            matrix.Multiply (t, enuMatrixOrder.Append   );

        }
        public static void Perspective(this Matrix matrix, float width, float height, float near, float far){
//            var t = new float[]{
// xaxis.X  ,        yaxis.X     ,    zaxis.X     ,  0      ,
// xaxis.Y  ,        yaxis.Y     ,    zaxis.Y     ,  0      ,
// xaxis.Z  ,        yaxis.Z     ,    zaxis.Z     ,  0      ,
//-Vector3f.Dot (xaxis, position ) , -Vector3f.Dot (yaxis, position ) , -Vector3f.Dot (zaxis, position) , 1
//        };
//            matrix.Multiply(new Matrix(t), enuMatrixOrder.Prepend);
        }
        public static void PerspectiveFov(this Matrix matrix, float fov, float aspect, float near, float far) {

            //LH
            float h = cot( (fov / 2.0f) * CoreMathOperation.ConvDgToRadian );
            float w =- aspect * h;
            var t = new float[]{
 w  ,   0  ,    0                    ,                         0,
0   ,  h   ,   0                    ,                         0,
0   ,  0   ,   far /(far -near)       ,       1,
0   ,  0   ,   -near *far /(far-near ) , 0
        };

            //RH
//            var t = new float[]{
//w    ,  0   ,   0           ,                                  0  ,
//0    ,  h   ,   0           ,                                  0  ,
//0    ,  0   ,   far/(near-far)            ,  -1  ,
//0    ,  0   ,   near*far/(near-far)  ,  0
//            };
            matrix.Multiply(t, enuMatrixOrder.Prepend );
        }
        public static float cot(float x) {
            var b = global::System.Math.Sin(x);
            if (b != 0.0)
                return (float)(global::System.Math.Cos(x) / b);
            return 0.0f;
            //return (float)(global::System.Math.Sin(x) / global::System.Math.Cos(x));
            //return (float)global::System.Math.Tan((global::System.Math.PI * 4) - x);
        }
        public static Matrix CreateOrtho2D(OGLGraphicsDevice graphicsDevice, float minx, float maxx, float miny, float maxy){
            MatrixMode mode = graphicsDevice.Projection.MatrixMode;
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GLU.gluOrtho2D(minx, maxx, miny, maxy);
            Matrix4x4f m = graphicsDevice.GetFloatv<Matrix4x4f>(GL.GL_MODELVIEW_MATRIX);
            GL.glPopMatrix();
            graphicsDevice.Projection.MatrixMode = mode;
            return new Matrix (m.Elements);
        }
        public static Matrix CreateOrtho(OGLGraphicsDevice graphicsDevice, float minx, float maxx, float miny, float maxy, float znear, float zfar)
        {
            MatrixMode mode = graphicsDevice.Projection.MatrixMode;
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glOrtho(minx, maxx, miny, maxy, znear, zfar);
            Matrix m = graphicsDevice.GetFloatv<Matrix>(GL.GL_MODELVIEW_MATRIX);
            GL.glPopMatrix();
            graphicsDevice.Projection.MatrixMode = mode;
            return m;
        }
        public static Matrix CreateFrustum(OGLGraphicsDevice graphicsDevice, float left, float right, float miny, float maxy, float znear, float zfar)
        {
            MatrixMode mode = graphicsDevice.Projection.MatrixMode;
            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GL.glFrustum(left, right, miny, maxy, znear, zfar);
            Matrix4x4f m = graphicsDevice.GetFloatv<Matrix4x4f>(GL.GL_MODELVIEW_MATRIX);
            GL.glPopMatrix();
            graphicsDevice.Projection.MatrixMode = mode;
            return new Matrix(m.Elements);
        }
        public static Matrix CreateLookAtMatrix(OGLGraphicsDevice graphicsDevice, Vector3f eye, Vector3f center, Vector3f direction)
        {
            MatrixMode mode = graphicsDevice.Projection.MatrixMode;
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glPushMatrix();
            GL.glLoadIdentity();
            GLU.gluLookAt(eye.X, eye.Y, eye.Z,
                center.X, center.Y, center.Z,
                direction.X, direction.Y, direction.Z);
            Matrix m = graphicsDevice.GetFloatv<Matrix>(GL.GL_PROJECTION_MATRIX);
            GL.glPopMatrix();
            graphicsDevice.Projection.MatrixMode = mode;
            return m;
        }
        /// <summary>
        /// get the Gly Unproject
        /// </summary>
        /// <param name="eye"></param>
        /// <param name="modelViewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="result"></param>
        public static void GluUnProject(Vector3f eye,
            double[] modelViewMatrix,
            double[] projectionMatrix,
            Rectanglei viewport,
            ref Vector3f result)
        {
            IntPtr view = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(double)) * 16);
            IntPtr proj = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(double)) * 16);
            IntPtr port = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(System.Drawing.Rectangle)));
            Marshal.StructureToPtr(viewport, port, false);
            Marshal.Copy(modelViewMatrix, 0, view, 16);
            Marshal.Copy(projectionMatrix, 0, proj, 16);
            double x = 0;
            double y = 0;
            double z = 0;
            GLU.gluUnProject(eye.X, eye.Y, eye.Z,
                view,
                proj,
                port,
                ref x,
                ref y,
                ref z);
            result = new Vector3f(x, y, z);
            Marshal.FreeCoTaskMem(port);
            Marshal.FreeCoTaskMem(view);
            Marshal.FreeCoTaskMem(proj);
        }
        /// <summary>
        /// get the projection vieuw
        /// </summary>
        /// <param name="eye"></param>
        /// <param name="modelViewMatrix"></param>
        /// <param name="projectionMatrix"></param>
        /// <param name="viewport"></param>
        /// <param name="result"></param>
        public static void GluProject(Vector3f eye,
            double[] modelViewMatrix,
            double[] projectionMatrix,
            Rectanglei viewport,
            ref Vector3f result)
        {
            IntPtr view = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(double)) * 16);
            IntPtr proj = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(double)) * 16);
            IntPtr port = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(System.Drawing.Rectangle)));
            Marshal.StructureToPtr(viewport, port, false);
            Marshal.Copy(modelViewMatrix, 0, view, 16);
            Marshal.Copy(projectionMatrix, 0, proj, 16);
            double x = 0;
            double y = 0;
            double z = 0;
            if (GLU.gluProject(eye.X, eye.Y, eye.Z,
                view,
                proj,
                port,
                ref x,
                ref y,
                ref z))
            {
                result = new Vector3f(x, y, z);
            }
            else {
                result = Vector3f.Zero;
            }
            Marshal.FreeCoTaskMem(port);
            Marshal.FreeCoTaskMem(view);
            Marshal.FreeCoTaskMem(proj);
        }
        /// <summary>
        /// look at l formula
        /// </summary>
        /// <param name="cameraTarget"></param>
        /// <param name="cameraPosition"></param>
        /// <param name="cameraUpVector"></param>
        /// <returns></returns>
        public static Matrix LookAt(Vector3f cameraTarget, Vector3f cameraPosition, Vector3f cameraUpVector) {

                           //var  zaxis = normal(cameraTarget - cameraPosition);
            var  zaxis = Vector3f.Normalize (cameraTarget - cameraPosition);
            
var xaxis = Vector3f.Normalize (Vector3f.CrossProduct (cameraUpVector, zaxis));
var yaxis =Vector3f.CrossProduct (zaxis, xaxis);

            Matrix c = new Matrix(new float []{



 xaxis.X   ,        yaxis.X  ,         zaxis.X    ,      0,
 xaxis.Y  ,         yaxis.Y ,          zaxis.Y     ,     0,
 xaxis.Z,           yaxis.Z,           zaxis.Z      ,    0,
- Vector3f.Dot (xaxis, cameraPosition),  -Vector3f.Dot(yaxis, cameraPosition) , -Vector3f.Dot(zaxis, cameraPosition)  ,1
            });

            return c;
        
        }
    
        //look at R formula
//        zaxis = normal(cameraPosition - cameraTarget)
//xaxis = normal(cross(cameraUpVector, zaxis))
//yaxis = cross(zaxis, xaxis)

// xaxis.x           yaxis.x           zaxis.x          0
// xaxis.y           yaxis.y           zaxis.y          0
// xaxis.z           yaxis.z           zaxis.z          0
//-dot(xaxis, cameraPosition)  -dot(yaxis, cameraPosition)  -dot(zaxis, cameraPosition)  1
    }
}

