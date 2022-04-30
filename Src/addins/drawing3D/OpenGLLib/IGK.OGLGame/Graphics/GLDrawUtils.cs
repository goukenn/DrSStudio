

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLDrawUtils.cs
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
file:GLDrawUtils.cs
*/
using System;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using IGK ;
using IGK.GLLib;
using IGK.ICore.WinUI;
using IGK.OGLGame.Graphics;
namespace IGK.OGLGame.Graphics 
{
	using Math = System.Math;
    using IGK.ICore;
	public static class GLDrawUtils
	{
        const float CONV_TODEGREE = (float)(180.0f / Math.PI);
        const float CONV_TORADIAN = (float)(Math.PI / 180.0f);
		readonly static double M_PI = System.Math.PI ;
        private static IntPtr QuadricOBJ;
        static GLDrawUtils()
        {
            try
            {
                IntPtr ptr = GLU.gluNewQuadric();
                QuadricOBJ = ptr;
                System .Windows.Forms.Application.ApplicationExit +=new EventHandler(DisposeQuadric);
            }
            catch {
                QuadricOBJ = IntPtr.Zero;
            }
        }
        private static bool IsValidQuadric { get { return (QuadricOBJ != IntPtr.Zero); } }
        private static void DisposeQuadric(object sender, EventArgs e)
        {
            GLU.gluDeleteQuadric(QuadricOBJ);
            QuadricOBJ = IntPtr.Zero;
        }
        /// <summary>
        /// return a 2D rectangle according to bound
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        //public static Rectanglef GetBound(this Vector3f startPoint, Vector3f point)
        //{
        //    return new Rectanglef(
        //        Math .Min(startPoint .X , point.X ),
        //        Math .Min(startPoint .Y , point.Y ),
        //        Math .Abs (startPoint .X - point.X ),
        //        Math .Abs (startPoint .Y - point.Y )
        //        );
        //}
            public static void DrawDisc(OGLGraphicsDevice device, float innerRadius, float outerRadius, int slice, int rings)
            {
                if (IsValidQuadric) {
                    GLU.gluQuadricDrawStyle(QuadricOBJ, GLU.GLU_LINE );
                    GLU.gluDisk(QuadricOBJ, innerRadius, outerRadius, slice, rings);
                }
            }
            public static void DrawTeapot(OGLGraphicsDevice device, Vector3f position, float scalex, float scaleY, float scalez)
            {
                    device.Projection.PushMatrix();
                    device.Projection.Translate(position.X, position.Y, position.Z);
                    device.Projection.Scale(scalex, scaleY, scalez);                    
                    GLUT.glutWireTeapot(1.0f);
                    device.Projection.PopMatrix();
            }
            public static void FillTeapot(OGLGraphicsDevice device, Vector3f position, float scalex, float scaley, float scalez)
            {
                device.Projection.PushMatrix();
                device.Projection.Translate(position.X, position.Y, position.Z);
                device.Projection.Scale(scalex, scaley, scalez);
                GLUT.glutSolidTeapot(1.0f);
                device.Projection.PopMatrix();
            }
		public static void DrawRoundRect(OGLGraphicsDevice device, Colorf color, Rectanglef rc, float radius)
		{
			float x = rc.X ;
			float y = rc.Y ;
			float w = rc.Width ;
			float h = rc.Height ;
			device.SetColor(color );
			GL.glBegin(GL.GL_LINE_STRIP);
		for(float i=(float)M_PI;i<=1.5f*M_PI;i+=0.1f)
			GL.glVertex2f((float)(radius* Math.Cos(i))+x+radius,(float)(radius*Math.Sin(i))+y+radius);
		for(float i=1.5f*(float)M_PI;i<=2*M_PI; i+=0.1f)
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+w-radius,(float)(radius*Math.Sin(i))+y+radius);
		for(float i=0;i<=0.5f*M_PI; i+=0.1f)
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+w-radius,(float)(radius*Math.Sin(i))+y+h-radius);
		for(float i=0.5f*(float)M_PI;i<=M_PI;i+=0.1f) 
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+radius,(float)(radius*Math.Sin(i))+y+h-radius);
		GL.glVertex2f(x,y+radius);
	GL.glEnd();
		}
		public static void DrawRect(OGLGraphicsDevice device, Colorf color, int lineWidth, float x, float y, float width, float height)
		{
			device.PushAttrib (enuAttribBit.All );
            device.RenderState.LineWidth = lineWidth;
			device.RenderState.SetPolygonMode(PolygonCullFace.FrontAndBack , PolygonFaceMode.Line );
			device.SetColor (color);
			device.Begin(enuGraphicsPrimitives.Quads );
			device.SetVertex(x, y);
			device.SetVertex(x + width , y);
			device.SetVertex(x + width , y+height );
			device.SetVertex(x , y+height );
			device.End ();
			device.PopAttrib ();
		}
        public static void DrawRect(OGLGraphicsDevice device, Colorf color, int lineWidth, Rectanglei bound)
        {
            DrawRect(device, color, lineWidth, bound.X, bound.Y, bound.Width, bound.Height);
        }
		public static void DrawEllipse(OGLGraphicsDevice device, Vector2f center, float rx, float ry)
		{			
			device.Begin (enuGraphicsPrimitives.LineLoop);
			float angle = 0.0f;
			for(float i = 0.0f; i < 360.0f; i++)
			{
				angle = i * CONV_TORADIAN ;
				device.SetVertex (
					center.X + (rx *Math.Cos( angle)),
					center.Y + (ry *Math.Sin( angle)));
			}
			device.End ();
		}
        public static void FillEllipse(OGLGraphicsDevice device, Vector2f center, float rx, float ry)
        {
            device.Begin(enuGraphicsPrimitives.Polygon );
            float angle = 0.0f;
            for (float i = 0.0f; i < 360.0f; i++)
            {
                angle = i * CONV_TORADIAN;
                device.SetVertex(
                    center.X + (rx * Math.Cos(angle)),
                    center.Y + (ry * Math.Sin(angle)));
            }
            device.End();
        }
		//fill method
		public static void FillRect(OGLGraphicsDevice device, Colorf color, Rectanglei rc)
		{
			device.SetColor (color );
			device.Begin (enuGraphicsPrimitives .Quads );
			device.SetVertex(rc.Location);
			device.SetVertex (rc.X + rc.Width , rc.Y );
			device.SetVertex (rc.X + rc.Width , rc.Y + rc.Height );
			device.SetVertex (rc.X, rc.Y +rc.Height );
			device.End ();
		}
			public static void FillRoundRect(OGLGraphicsDevice device, Colorf color, Rectanglef rc, float radius)
		{
			float x = rc.X ;
			float y = rc.Y ;
			float w = rc.Width ;
			float h = rc.Height ;
			device.SetColor(color );
			device.Begin (enuGraphicsPrimitives.Polygon );
		for(float i=(float)M_PI;i<=1.5f*M_PI;i+=0.1f)
			GL.glVertex2f((float)(radius* Math.Cos(i))+x+radius,(float)(radius*Math.Sin(i))+y+radius);
		for(float i=1.5f*(float)M_PI;i<=2*M_PI; i+=0.1f)
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+w-radius,(float)(radius*Math.Sin(i))+y+radius);
		for(float i=0;i<=0.5f*M_PI; i+=0.1f)
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+w-radius,(float)(radius*Math.Sin(i))+y+h-radius);
		for(float i=0.5f*(float)M_PI;i<=M_PI;i+=0.1f) 
			GL.glVertex2f((float)(radius*Math.Cos(i))+x+radius,(float)(radius*Math.Sin(i))+y+h-radius);
		GL.glVertex2f(x,y+radius);
	device.End ();
		}
        /// <summary>
        /// draw graphics path width the graphics device
        /// </summary>
        /// <param name="device"></param>
        /// <param name="path"></param>
            public static void DrawPath(OGLGraphicsDevice device, GraphicsPath path)
            { 
                //draw graphics path
                if ((path == null) || (path.PointCount == 0))
                    return;
                Vector2d[][] v_tab = GLUtilitys.BuildGraphicsOutLine(path);
                    if ((v_tab != null) && (v_tab.Length > 0))
            {
                //GL.glNewList((uint)(v_base + i), GL.GL_COMPILE);
                for (int j = 0; j < v_tab.Length; j++)
                {
                    GL.glBegin(GL.GL_LINE_LOOP );
                    for (int k = 0; k < v_tab[j].Length; k++)
                    {
                        GL.glVertex2d(v_tab[j][k].X,
                            v_tab[j][k].Y);
                    }
                    GL.glEnd();
                }
                //GL.glEndList();
            }
            }
            /// <summary>
            /// draw a polygonal font string
            /// </summary>
            /// <param name="device">device where to draw </param>
            /// <param name="text">text to draw</param>
            /// <param name="fontFamily">font family </param>
            /// <param name="fontstyle">font style</param>
            public static void DrawPolyString(OGLGraphicsDevice device, string text, FontFamily fontFamily, FontStyle fontstyle)
            {
                Vector2d[][] v_tab = GLUtilitys.BuildFontOutLine(text, fontFamily, fontstyle);
                double[] t = null;
                if ((v_tab == null) || (v_tab.Length == 0))
                    return;
                using (GLTessalation v_tess = GLTessalation.CreateNewTess())
                {
                    v_tess.RegisterCallBack(enuGLUTessCallback.Begin, (GLTessalation.BeginTessPFN)delegate(uint ri) { GL.glBegin(ri); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.End, (GLTessalation.EndTessPFN)delegate() { GL.glEnd(); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.Vertex, (GLTessalation.VertexPFN)delegate(IntPtr o)
                    {
                        Vector3d d = (Vector3d)System.Runtime.InteropServices.Marshal.PtrToStructure(o,
                            typeof(Vector3d));
                        GL.glVertex3d(d.X, d.Y, d.Z);
                    });
                    //Front face
                    v_tess.BeginPolygon(null);
                    for (int j = 0; j < v_tab.Length; j++)
                    {
                        v_tess.BeginContour();
                        for (int k = 0; k < v_tab[j].Length; k++)
                        {
                            t = new double[] { 
                                    v_tab[j][k].X,
                                    v_tab[j][k].Y, 0.0
                                };
                            v_tess.SetVertex(t, t);
                        }
                        v_tess.EndContour();
                    }
                    v_tess.EndPolygon();
                    //v_tess.BeginPolygon(null);
                    //for (int j = 0; j < v_tab.Length; j++)
                    //{
                    //    v_tess.BeginContour();
                    //    for (int k = 0; k < v_tab[j].Length; k++)
                    //    {
                    //        t = new double[] { 
                    //                v_tab[j][k].X,
                    //                v_tab[j][k].Y, 1.0
                    //            };
                    //        v_tess.SetVertex(t, t);
                    //    }
                    //    v_tess.EndContour();
                    //}
                    //v_tess.EndPolygon();
                    //----------------------------------------------------------
                    //Jointment
                    //----------------------------------------------------------
                    //Vector3d d1, d2;
                    //v_tess.BeginPolygon(null);
                    //double[][] r = null;
                    //for (int j = 0; j < v_tab.Length; j++)
                    //{
                    //    for (int k = 0; k < v_tab[j].Length-1; k+=2)
                    //    {
                    //        v_tess.BeginContour();
                    //        d1 = new Vector3d (v_tab[j][k].X,
                    //                v_tab[j][k].Y, 0.0);
                    //        d2 = new Vector3d (v_tab[j][k+1].X,
                    //                v_tab[j][k+1].Y, 0.0);
                    //        r = new double[4][];
                    //        r[0] = new double[] { d1.X, d1.Y, 0 };
                    //        r[1] = new double[] { d1.X, d1.Y, 1 };
                    //        r[2] = new double[] { d2.X, d2.Y, 1 };
                    //        r[3] = new double[] { d2.X, d2.Y, 0 };
                    //        v_tess.SetVertex(r[0], r[0]);
                    //        v_tess.SetVertex(r[1], r[1]);
                    //        v_tess.SetVertex(r[2], r[2]);
                    //        v_tess.SetVertex(r[3], r[3]);
                    //        v_tess.EndContour();
                    //    }
                    //}
                    //v_tess.EndPolygon();
                }
            }
            delegate void combineOperatorCallbackFunc(double[] coords,//[3] 
                         double[] vertex_data,//[4],
                         float[] weight,//[4], 
                        out IntPtr dataOut);
        static void combineOperatorCallback(double[] coords,//[3] 
                     double[] vertex_data,//[4],
                     float[] weight,//[4], 
                    out IntPtr dataOut )
            {
                Vector3f vec = new Vector3f(coords[0],
                    coords[1],
                    coords[2]);
                dataOut = Marshal.AllocCoTaskMem(Marshal.SizeOf(vec));//c.GetType ()));
                Marshal.StructureToPtr(vec, dataOut, true);
            }
        static void combineOperator(IntPtr coords, IntPtr data, IntPtr weight, out IntPtr dataOut)
            {
                Vector3f vec = coords.ConvertTo<Vector3f>();
                vec = Vector3f.Zero;
                //Vector3fColor c = new Vector3fColor();
                //c.Vector = new Vector3f(vec.X, vec.Y, vec.Z);
                //c.Color = Colorf.CornflowerBlue;
                //ocoord = c;
                dataOut = Marshal.AllocCoTaskMem(Marshal.SizeOf(vec));//c.GetType ()));
                Marshal.StructureToPtr(vec, dataOut, true);
            }
            /// <summary>
            /// render tessalation 2D
            /// </summary>
            /// <param name="device"></param>
            /// <param name="windingRule"></param>
            /// <param name="tolerance"></param>
            /// <param name="points"></param>
            public static void DrawTess2D(OGLGraphicsDevice device, enuGLUTessWindingRule windingRule, double tolerance, Vector2f[] points)
            {
                double[] v_t = null;
                using (GLTessalation v_tess = GLTessalation.CreateNewTess())
                {
                    v_tess.RegisterCallBack(enuGLUTessCallback.Begin, (GLTessalation.BeginTessPFN)delegate(uint ri) { GL.glBegin(ri); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.End, (GLTessalation.EndTessPFN)delegate() { GL.glEnd(); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.Vertex, (GLTessalation.VertexPFN)delegate(IntPtr o)
                    {
                        Vector3d d = (Vector3d)System.Runtime.InteropServices.Marshal.PtrToStructure(o,
                            typeof(Vector3d));
                        GL.glVertex3d(d.X, d.Y, d.Z);
                    });
                    v_tess.RegisterCallBack(enuGLUTessCallback.Combine,
                        //new combineOperatorCallbackFunc(combineOperatorCalllback ));
                        new GLTessalation.CombineDataCallPFN(combineOperator));
                    v_tess.BeginPolygon(null);
                    v_tess.BeginContour();
                    for (int i = 0; i < points.Length; i++)
                    {
                        //1.points
                        v_t = new double[] { 
                            points[i].X,
                            points[i].Y,
                            0,                    
                       };
                        v_tess.SetVertex(v_t, v_t);
                    }
                    v_tess.EndContour();
                    v_tess.EndPolygon();
                }
            }
            public static void DrawTessZ(OGLGraphicsDevice device, enuGLUTessWindingRule windingRule, double tolerance, Vector2f[] points)
            {
                double[] v_t =  null;
                using (GLTessalation v_tess = GLTessalation.CreateNewTess())
                {
                     v_tess.RegisterCallBack(enuGLUTessCallback.Begin, (GLTessalation.BeginTessPFN)delegate(uint ri) { GL.glBegin( ri); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.End, (GLTessalation.EndTessPFN)delegate() { GL.glEnd(); });
                    v_tess.RegisterCallBack(enuGLUTessCallback.Vertex, (GLTessalation.VertexPFN)delegate(IntPtr o)
                    {
                        Vector3d d = (Vector3d)System.Runtime.InteropServices.Marshal.PtrToStructure(o,
                            typeof(Vector3d));
                        GL.glVertex3d(d.X, d.Y, d.Z);
                    });
                    //v_tess.TessWindingRule = windingRule;
                    //v_tess.Tolerance = tolerance;
                    v_tess.BeginPolygon( null);
                    for (int i = 0; i < points.Length-1; i+=2)
			        {
                        v_tess.BeginContour();
                        //1.points
                        v_t = new double[] { 
                            points[i].X,
                            points[i].Y,
                            0,                    
                       };
                        v_tess.SetVertex(v_t, v_t);
                        v_t = new double[] { 
                            points[i].X,
                            points[i].Y,
                            1.0,                    
                       };
                        v_tess.SetVertex(v_t, v_t);
                        v_t = new double[] { 
                            points[i+1].X,
                            points[i+1].Y,
                            1.0,                    
                       };
                        v_tess.SetVertex(v_t, v_t);
                        v_t = new double[] { 
                            points[i+1].X,
                            points[i+1].Y,
                            0.0,                    
                       };
                        v_tess.SetVertex(v_t, v_t);
                        v_tess.EndContour();
			        }
                    v_tess.EndPolygon();
                }
            }
        /// <summary>
        /// Draw bezier figure
        /// </summary>
        /// <param name="device"></param>
        /// <param name="drawType"></param>
        /// <param name="color"></param>
        /// <param name="step"></param>
        /// <param name="beziersPoints"></param>
            public static void DrawBezier(OGLGraphicsDevice device,
                enuGraphicsPrimitives  drawType,
                Colorf color, int step,
                Vector3f[] beziersPoints)
            {
                if ((beziersPoints == null) || (beziersPoints.Length != 4))
                        return ;
                float[] t = new float[beziersPoints.Length * 3];
                for (int i = 0; i < beziersPoints.Length ; i++)
                {
                    t[(i*3)] = beziersPoints[i].X;
                    t[(i*3)+1] = beziersPoints[i].Y;
                    t[(i*3)+2] = beziersPoints[i].Z;
                }
                GL.glMap1f (GL.GL_MAP1_VERTEX_3 , 0.0f, 1.0f, 3, 4, t);
                device.Capabilities.Map1Vertex_3 = true;
                device.SetColor(color);
                device.Begin(drawType);
                for (int i = 0; i < step; i++)
                {
                    GL.glEvalCoord1f(i / (float)step);
                }
                device.End();
                device.Capabilities.Map1Vertex_3 = false;
            }
            public static void DrawCube(OGLGraphicsDevice device, Vector3f position, float scalex, float scaley, float scalez)
            {
                device.Projection.PushMatrix();
                device.Projection.Translate(position.X, position.Y, position.Z);
                device.Projection.Scale(scalex, scaley, scalez);
                GLUT.glutWireCube(1.0f);
                device.Projection.PopMatrix();
            }
            public static void FillCube(OGLGraphicsDevice device, Vector3f position, float scalex, float scaley, float scalez)
            {
                device.Projection.PushMatrix();
                device.Projection.Translate(position.X, position.Y, position.Z);
                device.Projection.Scale(scalex, scaley, scalez);
                GLUT.glutSolidCube(1.0f);
                device.Projection.PopMatrix();
            }
            public static void DrawCylinder(OGLGraphicsDevice device,
                enuCylinderType type,
                double baseRadius,
                double topRadius,
                double height,
                int Slices,
                int stacks)
            {
                if (QuadricOBJ != null)
                {
                    if (type == enuCylinderType.Line)
                    {
                        GLU.gluQuadricDrawStyle(QuadricOBJ, GLU.GLU_LINE);
                    }
                    else 
                        GLU.gluQuadricDrawStyle(QuadricOBJ, GLU.GLU_FILL);
                    GLU.gluCylinder(QuadricOBJ,
                        baseRadius,
                        topRadius,
                        height,
                        Slices,
                        stacks);
                }
            }
            public static void DrawTorus(OGLGraphicsDevice device, float innerRadius, float outerRadius, int sides, int rings)
            {
                GLUT.glutWireTorus(innerRadius, outerRadius, sides, rings);
            }
            public static void FillTorus(OGLGraphicsDevice device, float innerRadius, float outerRadius, int sides, int rings)
            {
                GLUT.glutSolidTorus(innerRadius, outerRadius, sides, rings);
            }
            public static void DrawSphere(OGLGraphicsDevice device, float radius, int Slice, int nices)
            {
                GLUT.glutWireSphere(radius, Slice, nices);
            }
            public static void FillSphere(OGLGraphicsDevice device, float radius, int Slice, int nices)
            {
                GLUT.glutSolidSphere(radius, Slice, nices);
            }
        /// <summary>
        /// draw test . Bind the Sprite font first
        /// </summary>
        /// <param name="device"></param>
        /// <param name="Content"></param>
            public static void DrawText(OGLGraphicsDevice device, string Content)
            {
                if (string.IsNullOrEmpty(Content))
                    return;
                IntPtr v_s = Marshal.StringToCoTaskMemAnsi(Content);
                GL.glCallLists(Content.Length, GL.GL_UNSIGNED_BYTE, v_s);                
                Marshal.FreeCoTaskMem(v_s);
                device.CheckError();
            }
            public static void DrawGraphicsPath(OGLGraphicsDevice device, Vector3f[] points, byte[] PathType, Colorf color)
            {
                Vector3f v_stpoint = Vector3f.Zero;
                Vector3f v_enpoint = Vector3f.Zero;
               // bool v_spoint = true;
                bool v_closefigure =false ;
                for (int i = 0; i < points.Length; i++)
                {
                    if (PathType[i] == 0) 
                    {
                        v_closefigure = false ;
                        GL.glBegin (GL.GL_LINE_STRIP);
                    }
                    else {
                        if ((PathType[i] & 0x80) == 0x80){
                                v_closefigure = true ;
                                device.End ();
                        }
                    }
                    if (!v_closefigure )
                        device.SetVertex(points[i]);
                }
                if (!v_closefigure) {
                    GL.glEnd();
                }
            }
    }
}

