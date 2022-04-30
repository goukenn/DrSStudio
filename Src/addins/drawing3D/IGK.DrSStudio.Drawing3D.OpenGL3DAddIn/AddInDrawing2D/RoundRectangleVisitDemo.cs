

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RoundRectangleVisitDemo.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.Drawing2D.OpenGL;
using IGK.GLLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace IGK.DrSStudio.Drawing3D.OpenGL.AddInDrawing2D
{
    class RoundRectangleVisitDemo
    {
        private RoundRectangleElement rcElement;
        [DllImport(GLU.LIBNAME)]
        public static extern void gluTessVertex(IntPtr tessalor,
                [MarshalAs(UnmanagedType.Struct)] Vector3d vector,
               [MarshalAs(UnmanagedType.Struct)]  Vector3d data
            );
        [DllImport(GLU.LIBNAME)]
        public static extern void gluTessVertex(IntPtr tessalor,
            double[] vector,
            double[]  data
            );
        public RoundRectangleVisitDemo(RoundRectangleElement rcElement)
        {
            
            this.rcElement = rcElement;
        }
        object m_syncObj = new object();
        internal void Visit(IGKOGL2DDrawingDeviceVisitor visitor)
        {
            lock (m_syncObj)
            {
                object obj = visitor.Save();

                visitor.SetupGraphicsDevice(rcElement);
                visitor.MultiplyTransform(rcElement.GetMatrix(), enuMatrixOrder.Prepend);
                visitor.Device.RenderState.LineWidth = rcElement.StrokeBrush.Width;
                visitor.Device.Capabilities.LineSmooth = true;

                ////create test
                var tess = IGKOGL2DDrawingResourcesManager.GetTess(visitor.Device, rcElement);
                IntPtr tessHandle = tess.TessHandle;

                ////register test call back
                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Begin, new GLUTessBeginPROC((o) =>
                {
                    visitor.Device.SetColor(rcElement.FillBrush.Colors[0]);
                    GL.glBegin(o);
                }));
                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.End, new GLUTessEndPROC(() =>
                {
                    GL.glEnd();
                }));

                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Vertex, new GLUTessVertexPROC((o) =>
                {
                    GL.glVertex3dv(o);
                }));
                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Error, new GLUTessErrorPROC((errorCode) =>
                {
                    string estring = Marshal.PtrToStringAnsi(GLU.gluErrorString(errorCode));
                    CoreLog.WriteDebug(string.Format("Tessellation Error: {0}\n", estring));
                }));

                int w = 10;
                int h = 10;

                Rectanglef rc = rcElement.Bounds;
                Vector3d[] rect = new Vector3d[]{
                    new Vector3d(rc.X,rc.Y,0),
                    new Vector3d(rc.X ,rc.Bottom ,0),
                    new Vector3d(rc.Right,rc.Bottom ,0),
                    new Vector3d(rc.Right,rc.Y,0),
                    new Vector3d(rc.X +w ,rc.Y+h,0),
                    new Vector3d(rc.X +w,rc.Bottom -h,0),
                    new Vector3d(rc.Right-w,rc.Bottom -h,0),
                    new Vector3d(rc.Right-w,rc.Y+h,0)
            };
                //int size = Marshal.SizeOf(typeof(Vector3d));
                //IntPtr hr = Marshal.AllocCoTaskMem(size * 4);
                IntPtr[] tab = new IntPtr[8];
                for (int i = 0/*, j = 0*/; i < 8; i++)
                {
                    //    tab[j] = new IntPtr(hr.ToInt32() + (i * size));
                    //    Marshal.StructureToPtr(rect[j], tab[j], true);
                    //    j++;
                    tab[i] = Marshal.UnsafeAddrOfPinnedArrayElement(rect, i);
                }

                Vector3d s = rect[0];
                double[] r = new double[3];
                //gluTessVertex(tessHandle, Vector3d.Zero, Vector3d.UpX);
                ////gluTessVertex(tessHandle, rect[1], out s);
                ////gluTessVertex(tessHandle, rect[2], out s);
                ////gluTessVertex(tessHandle, rect[3], out s);


                ////render object
                GLU.gluTessBeginPolygon(tessHandle, IntPtr.Zero);

                GLU.gluTessBeginContour(tessHandle);
                GLU.gluTessVertex(tessHandle, tab[0], tab[0]);
                GLU.gluTessVertex(tessHandle, tab[1], tab[1]);
                GLU.gluTessVertex(tessHandle, tab[2], tab[2]);
                GLU.gluTessVertex(tessHandle, tab[3], tab[3]);

                GLU.gluTessEndContour(tessHandle);

                GLU.gluTessBeginContour(tessHandle);


                GLU.gluTessVertex(tessHandle, tab[4], tab[4]);
                GLU.gluTessVertex(tessHandle, tab[5], tab[5]);
                GLU.gluTessVertex(tessHandle, tab[6], tab[6]);
                GLU.gluTessVertex(tessHandle, tab[7], tab[7]);

                GLU.gluTessEndContour(tessHandle);

                GLU.gluTessEndPolygon(tessHandle);

                //Marshal.FreeCoTaskMem(hr);

                //visitor.Device.Capabilities.LineSmooth = false;
                visitor.Restore(obj);
            }
        }
    }
}
