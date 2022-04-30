

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DTessalationRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

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
﻿using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    class IGKOGL2DTessalationRenderer
    {
            static void ReadDataCombine(IntPtr o, IntPtr vertex_data ,IntPtr weight, out IntPtr r) 
            {
                Vector3f coordf = (Vector3f)Marshal.PtrToStructure(o, typeof(Vector3f));
                Vector3d coords = (Vector3d) Marshal.PtrToStructure(o, typeof(Vector3d));
                float coordsfs = (float)Marshal.PtrToStructure(o, typeof(float));
                float[] coord = o.ReadFloatData(4);

                Vector3d v_dcoords = o.ReadData<Vector3d>();
                float[] v_weight = weight.ReadData<float>(4);
                double[] v_s = vertex_data.ReadData<double>(4 * 6);

                //Vector3d vertex = Vector3d.Zero;
                //int i;
                double[] vertex = new double[6];
                ////vertex = (GLdouble*)malloc(6 * sizeof(GLdouble));
                vertex[0] = v_dcoords.X;
                vertex[1] = v_dcoords.Y;
                vertex[2] = v_dcoords.Z;
                for (int i = 3; i < 6; i++)
                {
                    vertex[i] =
                        v_weight[0] * v_s[0 + i]
                    + v_weight[1] * v_s[1 * 6 + i]
                    + v_weight[2] * v_s[2 * 6 + i]
                    + v_weight[3] * v_s[3 * 6 + i];
                }
                r = Marshal.UnsafeAddrOfPinnedArrayElement(vertex, 0);//.AllocCoTaskMem (Marshal.SizeOf(typeof(double))*3);
                //Marshal.StructureToPtr(v_dcoords, r, true);
            }
        static RectangleElement sm_owner = new RectangleElement();
        static object m_syncobj = new object();
        internal static void RenderPolygon(
            OGLGraphicsDevice device,
            ICoreBrushOwner owner,
            Vector2f[] tab,
            byte[] bytes)
        {

            lock (m_syncobj)
            {
                var tess = IGKOGL2DDrawingResourcesManager.GetTess(device, sm_owner);
                IntPtr tessHandle = tess.TessHandle;


                GLU.gluTessProperty(tessHandle, GLU.GLU_TESS_WINDING_RULE,
                GLU.GLU_TESS_WINDING_ODD);
                ////register test call back
                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Begin, new GLUTessBeginPROC((o) =>
                {
                    GL.glBegin(o);
                }));
                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.End, new GLUTessEndPROC(() =>
                {
                    GL.glEnd();
                }));

                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Vertex, new GLUTessVertexPROC((o) =>
                {
                    GL.glColor3f(0, 0, 1);
                    GL.glVertex3dv(o);
                }));

                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Combine,
                    new GLUTessCombinePROC(ReadDataCombine));

                GLU.gluTessCallback(tessHandle, enuGLUTessCallback.Error, new GLUTessErrorPROC((errorCode) =>
                {
                    string estring = Marshal.PtrToStringAnsi(GLU.gluErrorString(errorCode));
                    CoreLog.WriteDebug(string.Format("Tessellation Error: {0}\n", estring));
                }));


                //render tab object

                GLU.gluTessBeginPolygon(tessHandle, IntPtr.Zero);

                bool beginContour = false;
                Vector3d[] v_tab = new Vector3d[tab.Length];
                for (int i = 0; i < tab.Length; i++)
                {
                    if (!beginContour)
                    {
                        GLU.gluTessBeginContour(tessHandle);
                        beginContour = true;
                    }
                    v_tab[i] = new Vector3d(tab[i].X, tab[i].Y, 0);


                    GLU.gluTessVertex(tessHandle, v_tab.GetPinnedAddress(i), v_tab.GetPinnedAddress(i));

                }
                if (beginContour)
                    GLU.gluTessEndContour(tessHandle);

                GLU.gluTessEndPolygon(tessHandle);
            }
        }
    }
}
