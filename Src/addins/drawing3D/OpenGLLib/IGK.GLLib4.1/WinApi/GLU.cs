

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLU.cs
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
file:GLU.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
namespace IGK.GLLib
{
    public enum GLTessPrimitives : uint
    {
        Triangles = enuGLPrimitives.Triangles ,
        TriangleFans = enuGLPrimitives.TrianglesFan ,
        TriangleStrip = enuGLPrimitives.TrianglesStrip ,
        LineLoop = enuGLPrimitives.LineLoop 
    }
    public enum GLUQuadricCallBack : uint
    {
        ///* QuadricCallback */   
        Error = GLU.GLU_TESS_ERROR,
    }
    /* TessProperty */
    public enum GLUTessProperty : uint
    {
        BoundaryOnly = GLU.GLU_TESS_BOUNDARY_ONLY,
        WindingRule = GLU.GLU_TESS_WINDING_RULE ,
        Tolerance = GLU.GLU_TESS_TOLERANCE
    }
    public enum GLUTessValue : uint
    {
        True = GL.GL_TRUE,
        False = GL.GL_TRUE,
        /* TessWinding */       
        WindingODD = GLU.GLU_TESS_WINDING_ODD ,
        WindingNonZero= GLU. GLU_TESS_WINDING_NONZERO    ,
        WindingPositive= GLU.GLU_TESS_WINDING_POSITIVE    ,
        WindingNegative= GLU.GLU_TESS_WINDING_NEGATIVE   ,
        WindingAbsGEQTwo= GLU. GLU_TESS_WINDING_ABS_GEQ_TWO   
    }
    public enum GLUQuadricStyle : uint
    {
        Point =   GLU.GLU_POINT     ,
        Line = GLU.GLU_LINE            ,
        Fill = GLU.GLU_FILL           ,
        Silhouette = GLU.GLU_SILHOUETTE    
    }
    public enum GLUQuadricNormal : uint
    {
        Smooth = GLU.GLU_SMOOTH   ,
        Flat = GLU.GLU_FLAT       ,
        None = GLU.GLU_NONE       
    }
    public enum GLUQuadricOrientation : uint
    {
          OutSide = GLU. GLU_OUTSIDE,
         Inside  = GLU.GLU_INSIDE 
    }
    public enum GLUError : uint
    {
        InvalidEnum = GLU. GLU_INVALID_ENUM,
        InvalidValue = GLU.GLU_INVALID_VALUE ,
        OutOfMemory = GLU.GLU_OUT_OF_MEMORY ,
        IncompatibleVersion = GLU. GLU_INCOMPATIBLE_GL_VERSION ,
    }
    public enum GLUBoolean : uint
    {
        True = GL.GL_TRUE ,
        False = GL.GL_FALSE
    }


    public delegate void GLUTessBeginPROC(uint begin);
    public delegate void GLUTessEndPROC();
    public delegate void GLUTessEndDataPROC(IntPtr handle);
    public delegate void GLUTessEdgeFlagPROC(bool flag, IntPtr userData);
    public delegate void GLUTessEdgeFlagDataPROC(bool flag);
    public delegate void GLUTessVertexPROC(IntPtr handle);
    public delegate void GLUTessVertexDataPROC(IntPtr vertexData, IntPtr userData);
    public delegate void GLUTessErrorPROC(uint error);
    public delegate void GLUTessErrorDataPROC(uint error, IntPtr handle);
    public delegate void GLUTessCombinePROC(
    IntPtr  coords,//coord is 3 doubles
    IntPtr vertex_data, //data is 4 values
    IntPtr weight, //weight is 4 values
    out IntPtr outData);//out coord data

public delegate void GLUTessCombineDataPROC(double[] coords,
IntPtr[] vertex_data, //data 4
float[]weight, //weight is 4
IntPtr outData,
IntPtr user_data);


    public sealed class GLU
    {
#if OS_WINDOWS
        public const string LIBNAME = "glu32.dll";
#elif UNIX
        const string LIBNAME ="libGLU.so.1";// "glu32.dll";
#endif
        //-----------------------------------------------------------------------------------------------------------
        public const uint GLU_VERSION = 100800;
        public const uint GLU_EXTENSIONS = 100801;
        public const uint GLU_ERROR = 100103;
        /*quadric normals styles*/
        public const uint  GLU_SMOOTH = 100000;
        public const uint  GLU_FLAT              =  100001;
        public const uint  GLU_NONE              =  100002;
        /* QuadricDrawStyle */
        public const uint  GLU_POINT             =  100010;
        public const uint  GLU_LINE               = 100011;
        public const uint  GLU_FILL              =  100012;
        public const uint  GLU_SILHOUETTE        =  100013;
        /* QuadricOrientation */
        public const uint GLU_OUTSIDE           =  100020;
        public const uint GLU_INSIDE            = 100021;
        /* Errors: (return value 0 = no error) */
        public const uint GLU_INVALID_ENUM      =  100900;
        public const uint GLU_INVALID_VALUE      = 100901;
        public const uint GLU_OUT_OF_MEMORY      = 100902;
        public const uint GLU_INCOMPATIBLE_GL_VERSION   =  100903;
        /*TessProperty*/
        public const uint  GLU_TESS_WINDING_RULE      =     100140;
public const uint  GLU_TESS_BOUNDARY_ONLY       =   100141;
public const uint  GLU_TESS_TOLERANCE           =   100142;
/* TessWinding */
public const uint  GLU_TESS_WINDING_ODD          =  100130;
public const uint  GLU_TESS_WINDING_NONZERO      =  100131;
public const uint  GLU_TESS_WINDING_POSITIVE     =  100132;
public const uint  GLU_TESS_WINDING_NEGATIVE     =  100133;
public const uint  GLU_TESS_WINDING_ABS_GEQ_TWO  =  100134;
        [DllImport(LIBNAME)]  public static extern void gluWireSphere(float x, int y, int z);
        [DllImport(LIBNAME)]  public static extern void gluPerspective(double angle, double ration, double znear, double zfar);
        [DllImport(LIBNAME)]  public static extern void gluOrtho2D(double minx, double maxx, double miny, double maxy);
        [DllImport(LIBNAME)]  public static extern void gluLookAt(double eyex, double eyey, double eyez, double centerx, double centery, double centerz, double upx, double upy, double upz);
        [DllImport(LIBNAME)]  public static extern void gluPickMatrix(double x, double y, double width, double height, IntPtr viewport);
        //>>>>>>>>>>>>Quadric
        [DllImport(LIBNAME)] public static extern IntPtr gluNewQuadric();
        [DllImport(LIBNAME)] public static extern bool gluDeleteQuadric(IntPtr quadric);
        [DllImport(LIBNAME)] public static extern IntPtr gluQuadricCallback(IntPtr quadric, uint pname, Delegate  method);
        [DllImport(LIBNAME)] public static extern IntPtr gluQuadricDrawStyle(IntPtr quadric, uint style);
        [DllImport(LIBNAME)] public static extern IntPtr gluQuadricNormals(IntPtr quadric, uint style);
        [DllImport(LIBNAME)] public static extern IntPtr gluQuadricOrientation(IntPtr quadric, uint style);
        [DllImport(LIBNAME)] public static extern IntPtr gluQuadricTexture(IntPtr quadric, uint textureCoords);
        [DllImport(LIBNAME)] public static extern void gluSphere(IntPtr quadric, double x, int slices, int stacks);
        [DllImport(LIBNAME)] public static extern void gluCylinder(IntPtr quadric, double baseradius, double topradius, double height, int slices, int stacks);
        [DllImport(LIBNAME)] public static extern void gluDisk(IntPtr quadric, double innerradius, double outerradius, int slices, int rings);
        [DllImport(LIBNAME)] public static extern void gluPartialDisk(IntPtr quadric, double innerradius, double outerradius, int slices, int rings, double startAnge, double sweepAngle);
        //>>>>>>>>>>>> Tessalation
        [DllImport(LIBNAME)] public static extern IntPtr gluNewTess();
        [DllImport(LIBNAME)] public static extern bool gluDeleteTess(IntPtr tess);
        [DllImport(LIBNAME)]
        public static extern void gluTessProperty(IntPtr tess, uint property, double value);
        [DllImport(LIBNAME)]  public static extern void gluTessProperty(IntPtr tess, uint property, bool value);
        //[DllImport(LIBNAME)] public static extern void gluTessProperty(IntPtr tess, uint property, double value);
        [DllImport(LIBNAME)] public static extern bool gluGetTessProperty(IntPtr tess, uint property, ref double value);
        [DllImport(LIBNAME)]
        public static extern bool gluGetTessProperty(IntPtr tess, uint property, ref
           bool value);
        [DllImport(LIBNAME)]
        public static extern bool gluGetTessProperty(IntPtr tess, uint property, ref 
           uint value);
        [DllImport(LIBNAME)] public static extern bool gluTessNormal(IntPtr tess, double x, double y, double z);
        [DllImport(LIBNAME)] public static extern IntPtr gluTessCallback(IntPtr tessalor, enuGLUTessCallback type, Delegate value);
        [DllImport(LIBNAME)] public static extern IntPtr gluTessBeginPolygon(IntPtr tessalor, IntPtr type);
        [DllImport(LIBNAME)] public static extern IntPtr gluTessEndPolygon(IntPtr tessalor);
        [DllImport(LIBNAME)] public static extern IntPtr gluTessBeginContour(IntPtr tessalor);
        [DllImport(LIBNAME)] public static extern IntPtr gluTessEndContour(IntPtr tessalor);
        //[DllImport(LIBNAME)] public static extern void gluTessVertex(IntPtr tessalor, IntPtr h1 ,  IntPtr h2);
        [DllImport(LIBNAME)] public static extern void gluTessVertex(IntPtr tessalor, IntPtr h1, IntPtr h2);
        [DllImport(LIBNAME)]
        public static extern void gluTessVertex(IntPtr tessalor, double[] h1, double[] h2);
        [DllImport(LIBNAME)]
        public static extern void gluTessVertex(IntPtr tessalor, vect3d  h1, IntPtr h2);
        [DllImport(LIBNAME)]
        public extern static bool gluUnProject(double winx, double winy, double winz, IntPtr modelMatrix,
            IntPtr projMatrix, IntPtr viewPort, ref double x, ref double y, ref double z);
        [DllImport(LIBNAME)]
        public extern static bool gluUnProject4(double winx, double winy, double winz, double clipw, IntPtr modelMatrix,
            IntPtr projMatrix, IntPtr viewPort, double clampZnear, double clampZfar, ref double x, ref double y, ref double z);
        [DllImport(LIBNAME)]
        public extern static bool gluProject(double x, double y, double z, IntPtr modelMatrix,
            IntPtr projMatrix, IntPtr viewPort, ref double winx, ref double winy, ref double winz);
        public delegate void TessBegin(GLTessPrimitives type);
        public delegate void TessBeginData(uint type, IntPtr userdata);
        public delegate void TessEdgeFlag(bool flag);
        public delegate void TessEdgeFlagData(bool flag, IntPtr data);
        public delegate void TessVertex(IntPtr vertex_data);
        public delegate void TessVertex3d(vect3d  vertex_data);
        public delegate void TessVertexData(IntPtr vertex_dat, IntPtr user_data);
        public delegate void TessEnd();
        public delegate void TessEndData(IntPtr data);
        public delegate void TessCombine(vect3d coords, IntPtr vertex_data, float[] weight, IntPtr outdaa);
        public delegate void TessCombineData(vect3d coords, IntPtr vertex_data, float[] weight, IntPtr outda, IntPtr user_data);
        public delegate void TessError(uint error);
        public delegate void TessErrorData(uint error, IntPtr user_data);
        [DllImport(LIBNAME)] public static extern IntPtr gluGetString(uint pname);
        [DllImport(LIBNAME)] public static extern IntPtr gluErrorString(uint errorcode);
        /* TessError */
        public const uint GLU_TESS_ERROR1     =100151;
        public const uint GLU_TESS_ERROR2     =100152;
        public const uint GLU_TESS_ERROR3     =100153;
        public const uint GLU_TESS_ERROR4     =100154;
        public const uint GLU_TESS_ERROR5     =100155;
        public const uint GLU_TESS_ERROR6     =100156;
        public const uint GLU_TESS_ERROR7     =100157;
        public const uint GLU_TESS_ERROR8     =100158;
        ///* TessCallback */
        public const uint GLU_TESS_BEGIN = 100100;  /* void (CALLBACK*)(GLenum    type)  */
        public const uint GLU_TESS_VERTEX = 100101; /* void (CALLBACK*)(void      *data) */
        public const uint GLU_TESS_END = 100102;    /* void (CALLBACK*)(void)            */
        public const uint GLU_TESS_ERROR = 100103;  /* void (CALLBACK*)(GLenum    errno) */
        public const uint GLU_TESS_EDGE_FLAG = 100104; /* void (CALLBACK*)(GLboolean boundaryEdge)  */
        public const uint GLU_TESS_COMBINE = 100105; /* void (CALLBACK*)(GLdouble  coords[3],
                                                            void      *data[4],
                                                            GLfloat   weight[4],
                                                            void      **dataOut) */
        public const uint GLU_TESS_BEGIN_DATA = 100106; /* void (CALLBACK*)(GLenum    type,  
                                                            void      *polygon_data) */
        public const uint GLU_TESS_VERTEX_DATA = 100107; /* void (CALLBACK*)(void      *data, 
                                                            void      *polygon_data) */
        public const uint GLU_TESS_END_DATA = 100108; /* void (CALLBACK*)(void      *polygon_data) */
        public const uint GLU_TESS_ERROR_DATA = 100109; /* void (CALLBACK*)(GLenum    errno, 
                                                            void      *polygon_data) */
        public const uint GLU_TESS_EDGE_FLAG_DATA = 100110; /* void (CALLBACK*)(GLboolean boundaryEdge,                                                            void      *polygon_data) */
        public const uint GLU_TESS_COMBINE_DATA = 100111; /* void (CALLBACK*)(GLdouble  coords[3],
                                                            void      *data[4],
                                                            GLfloat   weight[4],
                                                            void      **dataOut,
                                                            void      *polygon_data) */
    }
}

