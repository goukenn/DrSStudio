

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: OGLGraphicsDevice.cs
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
file:GraphicsDevice.cs
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
using System.Runtime.InteropServices ;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;

using IGK.GLLib;
using System.Threading;
namespace IGK.OGLGame.Graphics
{
    using IGK.ICore;
    using IGK.OGLGame.Math;
    using IGK.OGLGame.Graphics.Imaging;
    using IGK.OGLGame.Text;
    using System.Windows.Forms;
    /// <summary>
    /// represent the open Graphic Device used to render opengl
    /// </summary>
    public sealed class OGLGraphicsDevice :
        MarshalByRefObject ,
        IDeviceContext,
		IDisposable 
    {
        private IntPtr m_hdc; //get the hdc
        private IntPtr m_gldc; //get the gldc
        private IntPtr m_hwnd; //parent window handle
        private GraphicsPixelFormat m_pixelFormat;
        private bool m_destroyed;
		private IGK.GLLib.IGLGraphicDevice m_IGLGraphicsDevice;
		static readonly IntPtr NULL = IntPtr.Zero ;
        private GLVertexDeclaration  m_VertexDeclaration;
        /// <summary>
        /// Get the vertex declaration
        /// </summary>
        public GLVertexDeclaration  VertexDeclaration
        {
            get { return m_VertexDeclaration; }
            set
            {
                if (m_VertexDeclaration != value)
                {
                    m_VertexDeclaration = value;
                }
            }
        }
        /// <summary>
        /// get the window dc
        /// </summary>
        public IntPtr HWND { get { return this.m_hwnd; } }
        /// <summary>
        /// get the hdc
        /// </summary>
        public IntPtr HDC { get { return this.m_hdc; } }
        /// <summary>
        /// get the gldc
        /// </summary>
        public IntPtr HGLDC { get { return this.m_gldc; } }
        /// <summary>
        /// get the prixel format data description
        /// </summary>
        public GraphicsPixelFormat PixelFormat {get{return this.m_pixelFormat;}}
        /// <summary>
        /// get the index of the current pixel format
        /// </summary>
        public int PixelFormatIndex { get {
            return Gdi32Lib.GetPixelFormat(this.HDC);
        } }
        private GraphicsDeviceCapabilities m_graphicCapabilities;
        private GraphicsProjection m_projection;
        private GraphicsRenderState m_graphicsRenderState;
        private ClipPlaneCollection m_clipPlaneCollection;
        private LightCollection m_lightCollection;
        private VertexArrayCollection m_vertices;
        //----------------------------------------------------------
        //Events
        //----------------------------------------------------------
        /// <summary>
        /// event raised when device handle is disposed
        /// </summary>
        public event EventHandler Disposed;
        /// <summary>
        /// event raise before the current device is disposed
        /// </summary>
        public event EventHandler Disposing;// 
        public event EventHandler ReceiveContext;
        public VertexArrayCollection Vertices {
            get {
                if (m_vertices == null)
                    this.m_vertices = new VertexArrayCollection(this);
                return m_vertices;
            }
        }
        private GraphicsLightModel m_graphicLightModel;
        private GraphicsBlending m_graphicsBlending;
        private GraphicsHint m_graphicsHint;
        private GraphicsFog m_graphicsfog;
        private GraphicsImage m_graphicsImage;
        private GraphicsMaterial m_GraphicsMaterial;
        [Browsable(false)]
        public bool IsDisposed {
            get {
                return this.IsDestroy;
            }
        }
        [Browsable(false)]
        public bool IsCurrent {
            get {
                return this.m_IGLGraphicsDevice.IsCurrent;
            }
        }
        [Browsable(false)]
        public bool IsDestroy { 
            get{
                return m_destroyed;
            }
        }
        /// <summary>
        /// represent the material attached to this device
        /// </summary>
        public GraphicsMaterial Material
        {
            get { return m_GraphicsMaterial; }
        }
        /// <summary>
        /// represent the default graphics image properties attached to this device
        /// </summary>
        public GraphicsImage GraphicsImage
        {
            get { return m_graphicsImage; }            
        }
        public Rectanglei Viewport 
        {
            get {
                return this.GetIntegerv<Rectanglei>(GL.GL_VIEWPORT);
            }
            set {
                GL.glViewport (value.X , 
                    value.Y, 
                    value.Width,
                     value.Height );
            }
        }
        public GraphicsDeviceCapabilities Capabilities {
            get {
                return this.m_graphicCapabilities;
            }
        }
        public GraphicsRenderState RenderState{
            get {
                return this.m_graphicsRenderState;
            }
        }
        public GraphicsBlending Blending {
            get {
                return this.m_graphicsBlending;
            }
        }
        /// <summary>
        /// get the project object
        /// </summary>
        public GraphicsProjection Projection {
            get {
                return this.m_projection;
            }
        }
        /// <summary>
        /// get the clip plane
        /// </summary>
        public ClipPlaneCollection ClipPlanes
        {
            get
            {
                return this.m_clipPlaneCollection;
            }
        }
        public GraphicsHint Hint {
            get {
                return this.m_graphicsHint;
            }
        }
        [Category("Light")]
        /// <summary>
        /// get the light collection
        /// </summary>
        public LightCollection Lights { 
            get{
                return this.m_lightCollection;
            }
        }
        [Category("Light")]
        public GraphicsLightModel LightModel {
            get {
                return this.m_graphicLightModel;
            }
        }
        public GraphicsFog Fog {
            get {
                return this.m_graphicsfog;
            }
        }
		/// <summary>
		/// .ctr
		/// </summary>
        private OGLGraphicsDevice()
        {          
        }
		public SpriteFontInfo CreateSpriteFont(string fontname , int fontsize, int fontStyle)
		{
			SpriteFontInfo v_info = (SpriteFontInfo )
			 this.m_IGLGraphicsDevice .CreateSpriteFont (fontname , fontsize,fontStyle );
			return v_info ;
		}
        public static OGLGraphicsDevice CreateDeviceFromHDC(IntPtr hdc, byte bitcount, byte depth ,enuGLFlags flag)
        {
            IntPtr v_hdc = hdc;
            IntPtr gldc = GL.NULL;
            WGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
            int v_index = Gdi32Lib.GetPixelFormat(hdc);
            if (v_index == 0)
            {
                //IntPtr pfd = Marshal.AllocCoTaskMem (Marshal.SizeOf (Gdi32Lib.
                //Gdi32Lib.ChoosePixelFormat (hdc , IntPtr 
            }
            IGLGraphicDevice v_idevice = GL.CreateFromHdc(
                hdc,
                bitcount ,
                depth 
                ,
                flag,
                enuGLPixelMode.RGBA,
                enuGLPlane.MainPlane,
                out gldc);
            if (gldc == GL.NULL)
                return null;
            //build the gl device out
            OGLGraphicsDevice v_out = new OGLGraphicsDevice();
            v_out.m_IGLGraphicsDevice = v_idevice;
            v_out.m_gldc = gldc;
            v_out.m_hdc = hdc;
            v_out.m_hwnd = IntPtr.Zero;
            v_out.MakeCurrent();
            v_index = IGK.GLLib.Gdi32Lib.GetPixelFormat(hdc);
            int v_size = (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat));
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_size);
            Gdi32Lib.DescribePixelFormat(hdc,
               v_index,
                (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat)),
                v_alloc);
            v_out.m_pixelFormat = (GraphicsPixelFormat)Marshal.PtrToStructure(v_alloc, typeof(GraphicsPixelFormat));
            v_out.InitDevice();
            return v_out;
        }

        public static OGLGraphicsDevice CreateDevice(IntPtr handle, Version targetVersion, int flag) {
            var g = OGLGraphicsDevice.CreateDeviceFromHWND(handle);
            if (g != null)
            {
                int[] list = new int[]{
                    (int)WGL.WGL_CONTEXT_MAJOR_VERSION_ARB , targetVersion.Major ,
                    (int)WGL.WGL_CONTEXT_MINOR_VERSION_ARB , targetVersion.Minor,
                    (int)WGL.WGL_CONTEXT_FLAGS_ARB , flag,
                    0
                };
                WGL.wglMakeCurrent(IntPtr.Zero, IntPtr.Zero);
          
                g.Dispose();
                var hdc = OGLGraphicsDevice.CreateHDCFromHwnd(handle);
                var shared = IntPtr.Zero;
                var c = WGL.wglCreateContextAttribsARB(hdc, shared, list);
                if (c != IntPtr.Zero)
                {
                    return OGLGraphicsDevice.CreateFromGLDC(handle, hdc, c);

                    //ShaderProgram p = ShaderProgram.CreateProgram(this.m_device);
                    //Shader fs = Shader.CreateShader(this.m_device, enuShaderType.FragmentShader);
                    //Shader vs = Shader.CreateShader(this.m_device, enuShaderType.VertexShader);

                    ////                    ShaderCompiler.AttachShaderSource(fs, @"
                    ////#version 150
                    ////void main(){gl_FragColor = vec4(1.0,.50,0.0,1.0); }");
                    //fs.LoadSource(File.ReadAllText("shader.fs"));
                    //vs.LoadSource(File.ReadAllText("shader.vs"));
                    ////                    ShaderCompiler.AttachShaderSource(vs, @"
                    ////#version 150
                    ////void main(){gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex; }");

                    //bool tc = ShaderCompiler.CompileShader(fs);
                    //bool tr = ShaderCompiler.CompileShader(vs);

                    //p.AttachShader(fs);
                    //p.AttachShader(vs);

                    //ShaderCompiler.ValidateProgram(p);

                    //this.m_shaderProgram = p;
                    ////link the program to create 
                    //this.m_shaderProgram.Link();

                }
            }

            return null;
        }

        public static OGLGraphicsDevice CreateDeviceFromHDC(IntPtr hdc)
        {
            return CreateDeviceFromHDC (hdc, 32,32,             
                enuGLFlags.SupportOpenGL | 
                enuGLFlags.DrawToWindow | enuGLFlags.DrawToBitmap | enuGLFlags.SupportGdi );
        }
        /// <summary>
        /// Create the Graphics device with DoubleBuffer and support OpenGL Flags
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static OGLGraphicsDevice CreateDeviceFromHWND(IntPtr handle)
        {
            return CreateDeviceFromHWND(
                handle,
                32,
                0,
                enuGLCreationFlag.DoubleBuffer | enuGLCreationFlag.SupportOpenGL ,
                enuGLPixelMode.RGBA ,
                enuGLPlane.MainPlane);
        }
/// <summary>
/// Creates the device from HWN.
/// </summary>
/// <returns>
/// The device from HWN.
/// </returns>
/// <param name='handle'>
/// Handle.
/// </param>
/// <param name='biteCount'>
/// Bite count.
/// </param>
/// <param name='dephtest'>
/// Dephtest.
/// </param>
/// <param name='flag'>
/// Flag.
/// </param>
        public static OGLGraphicsDevice CreateDeviceFromHWND(IntPtr handle,
            byte biteCount,
            byte dephtest,
            enuGLCreationFlag flag)
        {
            return CreateDeviceFromHWND(handle,
            biteCount,
            dephtest,
            flag,
            enuGLPixelMode.RGBA ,
            enuGLPlane.MainPlane  );
        }
        /// <summary>
        /// Create the Graphics device with DoubleBuffer and support OpenGL Flags
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public static OGLGraphicsDevice CreateDeviceFromHWND(IntPtr handle,
            byte biteCount,
            byte dephtest,
            enuGLCreationFlag  flag, 
            enuGLPixelMode format, 
            enuGLPlane plane)
        {
            IntPtr hdc, gldc, glpxformat;
            IGK.GLLib.IGLGraphicDevice v_device = GL.CreateDevice(
                handle,
                biteCount,
                dephtest ,
                 (enuGLFlags ) flag,
                format ,
                plane,
                out hdc,
                out gldc,
                out glpxformat );
            if ((gldc == GL.NULL) && (v_device !=null))
                return null;
            OGLGraphicsDevice v_out = new OGLGraphicsDevice();
			v_out.m_IGLGraphicsDevice = v_device ;
            v_out.m_gldc = gldc;
            v_out.m_hdc = hdc;
            v_out.m_hwnd = handle;
			if (hdc != NULL )
			{
            int v_index = IGK.GLLib.Gdi32Lib.GetPixelFormat(hdc);
            int v_size=   (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat));
            IntPtr v_alloc = Marshal.AllocCoTaskMem (v_size );            
            Gdi32Lib.DescribePixelFormat(hdc,
               v_index ,
                (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat)),
                v_alloc );
            v_out.m_pixelFormat = (GraphicsPixelFormat)Marshal.PtrToStructure(v_alloc, typeof(GraphicsPixelFormat));
            Marshal.FreeCoTaskMem(v_alloc);
			}
            v_out.MakeCurrent();
            v_out.InitDevice();
            return v_out;
        }
        /// <summary>
        /// create device from parameter
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hdc"></param>
        /// <param name="gldc"></param>
        /// <returns></returns>
        public  static OGLGraphicsDevice CreateFromGLDC(IntPtr hwnd, IntPtr hdc, IntPtr gldc)
        {
            OGLGraphicsDevice v_out = new OGLGraphicsDevice();
            GLWindowDevice v_device = GL.CreateDeviceInfo(hwnd, hdc, gldc);
          
            v_out.m_IGLGraphicsDevice = v_device;
            v_out.m_gldc = gldc;
            v_out.m_hdc = hdc;
            v_out.m_hwnd = hwnd;
            v_out._initPixelFormat(hdc);



            if (v_out.MakeCurrent())
            {
               
                v_out.InitDevice();
            }
            else
            {
                v_out.Dispose();
                return null;
            }
            return v_out;
        }

        private void _initPixelFormat(IntPtr hdc)
        {
            int v_index = IGK.GLLib.Gdi32Lib.GetPixelFormat(hdc);
            int v_size = (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat));
            IntPtr v_alloc = Marshal.AllocCoTaskMem(v_size);
            Gdi32Lib.DescribePixelFormat(hdc,
               v_index,
                (ushort)Marshal.SizeOf(typeof(GraphicsPixelFormat)),
                v_alloc);
            this.m_pixelFormat = (GraphicsPixelFormat)Marshal.PtrToStructure(v_alloc, typeof(GraphicsPixelFormat));
            Marshal.FreeCoTaskMem(v_alloc);
        }
        //public static GraphicsDevice CreateLayeredDevice(IntPtr hdc,int pixindex , GraphicsPixelFormat pixelformat,  int idLayer)
        //{
        //    pixelformat.iLayerType = (byte)idLayer;
        //    pixelformat.bReserved = (byte)idLayer;
        //    IntPtr p = Marshal.AllocCoTaskMem (Marshal.SizeOf (pixelformat ));
        //        Marshal.StructureToPtr (pixelformat ,p, true  );
        //    bool c = Gdi32Lib.SetPixelFormat(hdc, pixindex, p);
        //    Marshal.FreeCoTaskMem(p);
        //    IntPtr h = WGL.wglCreateLayerContext(hdc, idLayer);
        //    GraphicsDevice device = null;
        //    if (h != IntPtr.Zero)
        //    {
        //    }
        //    else {
        //        device = new GraphicsDevice();
        //        device.m_gldc = h;
        //        device.m_hdc = hdc;
        //        device.m_hwnd = IntPtr.Zero ;
        //        device.InitDevice();
        //    }
        //    return device;
        //}
        private void InitDevice()
        {
          
            this.m_graphicCapabilities = new GraphicsDeviceCapabilities(this);
          
            this.m_projection = new GraphicsProjection(this);
            this.m_graphicsRenderState = new GraphicsRenderState(this);
            this.m_clipPlaneCollection = new ClipPlaneCollection(this);
            this.m_lightCollection = new LightCollection(this);
            //configure device property
            this.m_GraphicsMaterial = new GraphicsMaterial(this);
            this.m_graphicLightModel = new GraphicsLightModel(this);
            this.m_graphicsBlending = new GraphicsBlending(this);
            this.m_graphicsHint = new GraphicsHint(this);
            this.m_graphicsfog = new GraphicsFog(this);
            this.m_graphicsImage = new GraphicsImage(this);
        }
        public void CheckError()
        {
            uint error = 0;
            if (this.IsCurrent)
            {
                error = GL.glGetError();
                if (error != 0)   {
#if !DEBUG
                    throw new GLException((enuGLError)error, "CheckError");
#else

                    System.Windows.Forms.MessageBox.Show("Error: " + (enuGLError)error, "Error");
                    Application.Exit();
#endif
                }
            }
        }
        public void Clear(Colorf color)
        {
            GL.glClearColor  (color.R, color.G, color.B, color.A);
            GL.glClear (GL.GL_COLOR_BUFFER_BIT );            
        }
        public void Clear(enuBufferBit buffer,Colorf color)
        {
            GL.glClearColor  (color.R, color.G, color.B, color.A);
            GL.glClear((uint)GL.GL_COLOR_BUFFER_BIT | ((uint)buffer));
        }
        public void Clear(enuBufferBit buffer)
        {
            GL.glClear((uint)buffer);
        }
        public void EndScene()
        {
            EndScene(false);
        }
        public void EndScene(bool finish)
        {
            EndScene(finish, this.HDC);
        }
        /// <summary>
        /// end scene to hdc
        /// </summary>
        /// <param name="finish"></param>
        /// <param name="Hdc"></param>
        /// <returns></returns>
        public bool EndScene(bool finish, IntPtr Hdc)
        {
            if (finish)
                GL.glFinish();
            else
                GL.glFlush();
			return this.m_IGLGraphicsDevice.SwapBuffers(Hdc );
        }
        /// <summary>
        /// set the color 
        /// </summary>
        /// <param name="color"></param>
        public void SetColor(Colorf color)
        {
            GL.glColor4f(color.R, color.G, color.B, color.A);
        }
        public void SetColor(global::System.Drawing.Color color)
        {
            GL.glColor4f(color.R/255.0F, color.G/255.0F, 
                color.B/255.0f,
                color.A/255.0f);
        }
        public void SetColor(double r, double g, double b, double a)
        {
            GL.glColor4d(r, g, b, a);
        }
        /// <summary>
        /// set the current contant
        /// </summary>
        public bool MakeCurrent()
        {
			if (this.m_IGLGraphicsDevice !=null)
			return this.m_IGLGraphicsDevice.MakeCurrent();
            return false;
        }
        /// <summary>
        /// raise the ReceiveContext
        /// </summary>
        /// <param name="eventArgs"></param>
        private void OnReceiveContext(EventArgs eventArgs)
        {
            if (this.ReceiveContext != null)
            {
                this.ReceiveContext(this, eventArgs);
            }
        }
        public void DrawWireSphere(Colorf color, float size, int slice, int nice)
        {
            SetColor(color);
            GLUT.glutWireSphere(size, slice, nice);
        }
        public void DrawSolidSphere(Colorf color, float size, int slice, int nice)
        {
            SetColor(color);
            GLUT.glutSolidSphere(size, slice, nice);
        }
        public void DrawSolidTeapot(float scale)
        {
            GLUT.glutSolidTeapot(scale);
        }
        public void DrawWiredTeapot(float scale)
        {
            GLUT.glutWireTeapot(scale);
        }
        public void DrawSolidTorus(float innerRadius, float outerRadius, int nsides, int rings)
        {
            GLUT.glutSolidTorus(innerRadius, outerRadius, nsides, rings);
        }
        public void DrawWiredTorus(float innerRadius, float outerRadius, int nsides, int rings)
        {
            GLUT.glutWireTorus(innerRadius, outerRadius, nsides, rings);
        }
        public void DrawPrimitive(enuGraphicsPrimitives graphicsPrimitives, Vector2f[] vector)
        {
            GL.glBegin((uint)graphicsPrimitives);
            for (int i = 0; i < vector.Length ; i++)
            {
                GL.glVertex2f(vector[i].X, vector[i].Y);
            }
            GL.glEnd();
        }
        public void DrawPrimitive(enuGraphicsPrimitives graphicsPrimitives, Vector3f[] vector)
        {
            GL.glBegin((uint)graphicsPrimitives);
            for (int i = 0; i < vector.Length; i++)
            {
                GL.glVertex3f(vector[i].X, vector[i].Y, vector[i].Z );
            }
            GL.glEnd();
        }
        public void DrawPrimitive(enuGraphicsPrimitives graphicsPrimitives,Colorf color, Vector3f[] vector)
        {
            GL.glBegin((uint)graphicsPrimitives);
            this.SetColor(color);
            for (int i = 0; i < vector.Length; i++)
            {
                GL.glVertex3f(vector[i].X, vector[i].Y, vector[i].Z );
            }
            GL.glEnd();
        }
        public void DrawRectangle(Colorf color, float x, float y, float w, float h)
        {
            this.SetColor(color);
            GL.glRectf(x, y, x+w, y+h);
        }
        public void DrawLine(Colorf color, Vector2f vector1, Vector2f vector2)
        {
            GL.glBegin(GL.GL_LINES);            
            GL.glColor4f(color.R, color.G, color.B, color.A);
            GL.glVertex2f(vector1.X, vector1.Y);
            GL.glVertex2f(vector2.X, vector2.Y);            
            GL.glEnd();
        }
        public void DrawLine(Colorf color, Vector3f vector1, Vector3f vector2)
        {
            GL.glBegin(GL.GL_LINES);
            GL.glColor4f(color.R, color.G, color.B, color.A);
            GL.glVertex3f(vector1.X, vector1.Y, vector1 .Z );
            GL.glVertex3f(vector2.X, vector2.Y , vector2 .Z );
            GL.glEnd();
        }
        public void BeginNewList(uint gridlist, ListMode enuListMode)
        {
           GL.glNewList(gridlist, (uint)enuListMode);
        }
        public uint GenList(int p)
        {
            return GL.glGenLists(p);
        }
        public void EndList()
        {
            GL.glEndList();
        }
        public void CallList(uint listId)
        {
            if (GL.glIsList(listId))
            {
                GL.glCallList(listId);
            }
        }
        public uint GenLists(int size)
        {
            return this.GenList(size);
        }
        public void SetListBase(uint p)
        {
            GL.glListBase(p);
        }
        /// <summary>
        /// Get the hdc
        /// </summary>
        /// <returns></returns>
        public IntPtr GetHDC()
        {
            return this.HDC;
        }
        /// <summary>
        /// dessiner les primitives charger � l'aide du tableau d'indice sp�cifier.
        /// </summary>
        /// <param name="primitive">type de primitives</param>
        /// <param name="indices">array of indices</param>
        /// <param name="offset">offset </param>
        /// <param name="count">number of element. must be greater that offset</param>
        public void DrawIndexesPrimitive(enuGraphicsPrimitives primitive, byte[] indices,int offset, int count)
        {
            GL.glBegin((uint)primitive);
            for (int i = offset; i < count ; i++)
            {
               GL.glArrayElement(indices[i]);
            }
            GL.glEnd();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="primitive"></param>
        /// <param name="vector3"></param>
        /// <param name="indices"></param>
        /// <param name="offset"></param>
        /// <param name="count">Numbre of element to render in the indices array</param>
        public void DrawIndexesPrimitive<T>(enuGraphicsPrimitives primitive, T[] vector3, int[] indices, int count)
        {
            IntPtr c = Marshal.UnsafeAddrOfPinnedArrayElement(vector3, 0);
            //int i = T.SizeInByte;
            GL.glColorPointer(4, GL.GL_FLOAT, 7* Marshal.SizeOf (typeof(float)) , c);
            GL.glVertexPointer(3, GL.GL_FLOAT, 7 * Marshal.SizeOf(typeof(float)), new IntPtr(c.ToInt32() + 4* Marshal.SizeOf (typeof(float))) );           
            GL.glDrawElements ((uint)primitive, count, GL.GL_UNSIGNED_INT, Marshal.UnsafeAddrOfPinnedArrayElement (indices,0));                                    
        }
        public void PopAttrib()
        {
            GL.glPopAttrib();
        }
        public void PushAttrib(enuAttribBit attrib)
        {
            GL.glPushAttrib((uint)attrib);
        }
        public void DeleteList(uint id)
        {
            GL.glDeleteLists(id, 1);
        }
        public void Begin(enuGraphicsPrimitives primitive)
        {
            GL.glBegin((uint)primitive);
        }
        public void End()
        {
            GL.glEnd();
        }
        public bool SwapBuffer()
        {
            this.m_IGLGraphicsDevice.SwapBuffers(this.m_hdc);
//#if OS_WINDOWS
//            return Gdi32Lib.SwapBuffers(this.HDC);
//#else
                     if (this.IsCurrent)
            {
                //GL.glFlush();
                //if (this.RenderState.DrawBuffer == BufferMode.Back)
                //{
                //    GL.glReadBuffer(GL.GL_BACK);
                //    GL.glDrawBuffer(GL.GL_FRONT);
                //}
                //else
                //{
                //    GL.glReadBuffer(GL.GL_FRONT);
                //    GL.glDrawBuffer(GL.GL_BACK);
                //}
                //if (this.RenderState.ReadBuffer == BufferMode.Front)
                //{
                //    GL.glReadBuffer(GL.GL_FRONT);
                //    GL.glDrawBuffer(GL.GL_BACK);
                //}
                //else
                //{
                //    GL.glReadBuffer(GL.GL_BACK);
                //    GL.glDrawBuffer(GL.GL_FRONT);
                //}
               // GL.glCopyPixels(0, 0, Viewport.Width, Viewport.Height, GL.GL_COLOR);
                this.CheckError();
                return true;
            }
            return false;
//#endif
        }
        public void SetVertex(Vector3f v)
        {
            GL.glVertex3f(v.X, v.Y, v.Z);
        }
        public void SetTexCoord(float s, float t)
        {
            GL.glTexCoord2f(s, t);
        }
        public void SetTexCoord(float s) {
            GL.glTexCoord1f(s);
        }
        public void SetTexCoord(float s, float t, float r) { 
            GL.glTexCoord3f(s,t,r);
        }
        public void Flush()
        {
            GL.glFlush();
        }
        public void ReadPixels(Rectanglei rectangle, GraphicsPixelColorMode mode, GraphicsPixelColorType type, IntPtr bitmapScan0)
        {
            GL.glReadPixels(rectangle.X,
                rectangle.Y,
                rectangle.Width,
                rectangle.Height,
                (uint)mode,
                (uint)type,
                bitmapScan0);
        }
        internal T GetIntegerv<T>(uint p)
        {
            T d = default(T);
            Type t = typeof(T);
            if (t.IsEnum)
            {
                int[] v_t = new int[1];
                GL.glGetIntegerv(p, v_t);
                d = (T)Enum.ToObject(t, v_t[0]);
            }
            else { 
                int size = Marshal.SizeOf (t);
                int[] v_t = new int[size / Marshal.SizeOf (typeof(int))];
                IntPtr v_alloc = Marshal.AllocCoTaskMem (size );
                GL.glGetIntegerv (p, v_t );
                Marshal.Copy (v_t, 0, v_alloc , v_t .Length );
                d = (T)Marshal.PtrToStructure(v_alloc, t);
                Marshal.FreeCoTaskMem(v_alloc );
            }          
            return d;
        }
        public T GetFloatv<T>(uint param)
        {
            if (!this.IsCurrent )
                MakeCurrent();
            float[] v_t = null;
            int size = Marshal.SizeOf(typeof(T));            
            v_t = new float[size / Marshal.SizeOf(typeof(float))];
            GL.glGetFloatv(param, v_t);
            T o = (T)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(v_t, 0),
                typeof(T));
            return o;
        }
        public  T GetDoublev<T>(uint param)
        {
                MakeCurrent();
            double[] v_t = null;
            int size = Marshal.SizeOf(typeof(T));
            v_t = new double[size / Marshal.SizeOf(typeof(double))];
            GL.glGetDoublev (param, v_t);
            T o = (T)Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement(v_t, 0),
                typeof(T));
            return o;
        }
        internal T GetBooleanv<T>(uint p)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T)));
            T c = (T)Marshal.PtrToStructure(alloc, typeof(T));
            Marshal.FreeCoTaskMem(alloc);
            return c;
        }
        public void SetVertex(float x, float y, float z)
        {
            GL.glVertex3f(x, y, z);
        }
        public void SetVertex(float x, float y)
        {
            GL.glVertex2f(x, y);
        }
        public void SetVertex(double  x, double y)
        {
            GL.glVertex2d(x, y);
        }
        public void SetVertex(Vector2i point)
        {
            GL.glVertex2i(point.X, point.Y);
        }
        public void SetVertex(Vector2f point)
        {
            GL.glVertex2f(point.X, point.Y);
        }
        public void SetVertex(Vector2d point)
        {
            GL.glVertex2d(point.X , point.Y );
        }
        public void SetVertex(Vector3d point)
        {
            GL.glVertex3d(point.X, point.Y, point.Z );
        }
        /// <summary>
        /// generate a single textures files
        /// </summary>
        /// <returns></returns>
        public uint GenTexture()
        {
            uint[] i = new uint [1];
            GL.glGenTextures(1, i);
            return i[0];
        }
        /// <summary>
        /// get the doublie presicions value
        /// </summary>
        /// <typeparam name="?">return array of double</typeparam>
        /// <param name="p"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        internal static double[] GetDoublev(uint p, int size)
        {
            double[] d = new double[size];            
            GL.glGetDoublev(p, d);
            return d;
        }
        /// <summary>
        /// get float value
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static float[] GetFloatv(uint data, int length)
        {
            float[] d = new float[length];
            GL.glGetFloatv(data, d);
            return d;
        }
        #region IDisposable Members
        private void OnDisposing(EventArgs e)
        {
            if (this.Disposing != null)
            {
                this.Disposing(this, e);
            }
        }
        public void Dispose()
        {
            OnDisposing(EventArgs.Empty);
            if (this.m_IGLGraphicsDevice != null)
            {
                this.m_IGLGraphicsDevice.Dispose();
            }
            this.m_destroyed = true;
            
            OnDisposed(EventArgs.Empty);
            this.m_gldc = IntPtr.Zero;
            this.m_hdc = IntPtr.Zero;
            this.m_hwnd = IntPtr.Zero;
        }

        private void OnDisposed(EventArgs e)
        {
            if (this.Disposed != null)
                this.Disposed(this, e);
        }
        #endregion
        /// <summary>
        /// call the finish operation
        /// </summary>
        public void Finish()
        {
            GL.glFinish();
        }
        public void DrawSolidCube(Vector3f center, Vector3f size)
        {
            GL.glPushMatrix();
            GL.glScalef(size.X , size.Y , size.Z );
            GLUT.glutSolidCube(1.0f);
            GL.glPopMatrix();
        }
        public void DrawWiredCube(Vector3f center, Vector3f size)
        {
            GL.glPushMatrix();
            GL.glScalef(size.X , size.Y , size.Z );
            GLUT.glutWireCube(1.0f);
            GL.glPopMatrix();
        }
        public string[] GetExtensions()
        {
            return GL.Extensions;
        }
        #region "QUADRIC MANAGEMENT"
        IntPtr m_quadric;
        public IntPtr GLUGetQuadric()
        {
            if (this.m_quadric == IntPtr.Zero)
            {
                this.Disposed -= new EventHandler(GraphicsDevice_Disposed);
                this.m_quadric = GLU.gluNewQuadric();
                this.Disposed += new EventHandler(GraphicsDevice_Disposed);
            }
            return this.m_quadric;
        }
        void GraphicsDevice_Disposed(object sender, EventArgs e)
        {
            if (this.m_quadric != IntPtr.Zero )
            {
                IGK.GLLib.GLU.gluDeleteQuadric(this.m_quadric);
                this.Disposed -= new EventHandler(GraphicsDevice_Disposed);
            }
        }
        #endregion
        public void SetNormal(Vector3f normal)
        {
            IGK.GLLib.GL.glNormal3f(normal.X, normal.Y, normal.Z);
        }
        public void SetScissorBox(Rectanglei rectanglei)
        {
            IGK.GLLib.GL.glScissor(rectanglei.X, rectanglei.Y, rectanglei.Width, rectanglei.Height);
        }
        public void DrawBitmap(Bitmap bmp)
        {
            if ((bmp == null) || (bmp.PixelFormat == System.Drawing.Imaging.PixelFormat.Undefined))
                return;
            System.Drawing.Imaging.BitmapData data =
                bmp.LockBits(new Rectangle(Point.Empty,
                    bmp.Size),
                     System.Drawing.Imaging.ImageLockMode.ReadOnly,
                      System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            IGK.GLLib.GL.glDrawPixels(
                bmp.Width, bmp.Height, GL.GL_BGRA, GL.GL_UNSIGNED_BYTE,
                data.Scan0);
            bmp.UnlockBits(data);
        }
        public void DrawBitmap(Bitmap bmp, Vector2f vector2f)
        {
            IGK.GLLib.GL.glRasterPos2f(vector2f.X, vector2f.Y);
            this.DrawBitmap(bmp);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="w"></param>
        /// <param name="height"></param>
        /// <param name="Accum">GL_COLOR, GL_DEPTH, GL_STENCIL</param>
        public void CopyPixel(int x, int y,int destx, int desty, int width, int height, enuGLCopyPixelFlag Accum)
        {
            GL.glRasterPos2d(destx, desty);
            GL.glCopyPixels(x, y, width, height,(uint) Accum);
        }
        /// <summary>
        /// set color that will be used for clearing the accumulation buffer
        /// </summary>
        /// <param name="colorf"></param>
        public void SetClearAccum(Colorf colorf)
        {
            GL.glClearAccum(colorf.R, colorf.G, colorf.B, colorf.A);
        }
        /// <summary>
        /// clear the stencil buffer
        /// </summary>
        /// <param name="stencil"></param>
        public void SetClearStencil(int stencil)
        {
            GL.glClearStencil(stencil);
        }
        /// <summary>
        /// set the depth that will be used to clear the z-buffer
        /// </summary>
        /// <param name="depth"></param>
        public void SetClearDepth(double depth)
        {
            GL.glClearDepth(depth);
        }
        public void SetClearIndex(float index)
        {
            GL.glClearIndex(index);
        }
        //buffert
        public void SetRasterPos(float  x, float y)
        {
            GL.glRasterPos2f(x, y);
        }
        public void SetDepthTest(float min, float max)
        {
            GL.glDepthRange(min, max);
        }
        public void Draw3DString(GL3DFont _3dfont, string text, Vector3f position, Colorf color)
        {         
            _3dfont.Bind();
            IntPtr v_s = Marshal.StringToCoTaskMemAnsi(text);
            this.SetColor(color);
            GL.glPushMatrix();
            GL.glTranslatef(position.X, position.Y, position.Z);
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_BYTE, v_s);   
            GL.glPopMatrix();
            Marshal.FreeCoTaskMem(v_s);
        }
        public void EvalCoord2(float u, float v)
        {
            GL.glEvalCoord2f(u, v);
        }
        public void EvalCoord2(double  u, double  v)
        {
            GL.glEvalCoord2d(u, v);
        }
        public void CallProc(string p, object[] p_2)
        {
           System.Reflection.MethodInfo m =  typeof(GL).GetMethod(p, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
           if (m == null) return;
           m.Invoke(null, p_2);
        }
        [Obsolete("for OpenGL core profile used GL.glDrawArray")]
        public void DrawUserIndexPrimitive<T>(enuGraphicsPrimitives primitive, 
            T[] VertexElements, int start, int end)
        {
            var v_d = VertexDeclaration;
            if (v_d == null) {
            throw new OGLGameException (enuGLError.InvalidOperation , "You must first set a VertexDeclaration ");

            }
            //enable declaration
            IntPtr t = VertexElements.ToPointer();
            int offset = 0;
            foreach (GLVertexDefinition  item in v_d.GetDefinition ())
            {
                EnableClientState(item.Usage);
                SetArrayPointer(item, VertexDeclaration .Stride, new IntPtr (t.ToInt32 ()+offset));
                offset += item.SizeInByte;
            }
            Begin(primitive);
            for (int i = start; i < end; i++)
            {
                GL.glArrayElement(i);
            }
            End();
            foreach (GLVertexDefinition item in VertexDeclaration.GetDefinition())
            {
                DisableClientState(item.Usage);
            } 
        }
        private void SetArrayPointer(GLVertexDefinition item, int stride, IntPtr VertexElements)
        {
            switch (item.Usage)
            {
                case enuGLVertexUsage.Vertex:
                    GL.glVertexPointer(item.Size,
                        item.DataType.GetGLDataType(),
                        stride,
                        VertexElements);
                    break;
                case enuGLVertexUsage.Color:
                    GL.glColorPointer(item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.SecondaryColor:
                    GL.glSecondaryColorPointer(item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.Index:
                    GL.glIndexPointer(
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.Normal:
                    GL.glNormalPointer(
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.Fog:
                    GL.glFogCoordPointer(
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.Texture:
                    GL.glTexCoordPointer(item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements);
                    break;
                case enuGLVertexUsage.EdgeFlag:
                    GL.glEdgeFlagPointer(
                  stride,
                  VertexElements);
                    break;
                default:
                    break;
            }
        }
        private void SetArrayPointer<T>(GLVertexDefinition item, int stride, T[] VertexElements)
        {
            switch (item.Usage)
            {
                case enuGLVertexUsage.Vertex:
                    GL.glVertexPointer(item.Size,
                        item.DataType.GetGLDataType (),
                        stride,
                        VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.Color:
                    GL.glColorPointer (item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.SecondaryColor:
                    GL.glSecondaryColorPointer(item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.Index:
                    GL.glIndexPointer(
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.Normal:
                    GL.glNormalPointer(
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.Fog:
                    GL.glFogCoordPointer (
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.Texture:
                    GL.glTexCoordPointer(item.Size,
                  item.DataType.GetGLDataType(),
                  stride,
                  VertexElements.ToPointer());
                    break;
                case enuGLVertexUsage.EdgeFlag:
                    GL.glEdgeFlagPointer (
                  stride,
                  VertexElements.ToPointer());
                    break;
                default:
                    break;
            }
        }
        private void DisableClientState(enuGLVertexUsage usage)
        {
            GL.glDisableClientState(usage.GetClientStateType());
        }
        private void EnableClientState(enuGLVertexUsage usage)
        {
            GL.glEnableClientState(usage.GetClientStateType());
        }
        public IntPtr GetHdc()
        {
            return this.m_hdc;
        }
        public void ReleaseHdc()
        {
        }

        /// <summary>
        /// draw string on device
        /// </summary>
        /// <param name="fontSprite">sprite font</param>
        /// <param name="text">text to draw</param>
        /// <param name="vector2">vector 2f </param>
        /// <param name="color">color of the text</param>
        public void DrawString(SpriteFont fontSprite, string text, Vector2f vector2, Colorf color)
        {
            if (string.IsNullOrEmpty(text))
                return;
            fontSprite.Bind();
            IntPtr v_s = Marshal.StringToCoTaskMemAnsi(text);
            this.SetColor(color );
            GL.glPushMatrix();
            GL.glTranslatef (vector2.X, vector2.Y + fontSprite.FontSize, 0);
            GL.glRasterPos2f(0.0f, 0.0f);
            GL.glCallLists(text.Length, GL.GL_UNSIGNED_BYTE, v_s);
            GL.glPopMatrix();
            Marshal.FreeCoTaskMem(v_s);
        
        }

        public void Draw(Texture2D texture, Vector2f position, Size2f size, Colorf color) 
        {
            if (texture == null)
            {
                #if !DEBUG
                throw new ArgumentNullException("texture");
                #else
                return;
                #endif
            }
            //CheckJob(true);
            float w = size.Width;
            float h = size.Height;
            float posx = position.X;
            float posy = position.Y;
            this.SetColor(color);
            this.Capabilities.Texture2D = true;
            texture.Bind();
            this.Projection.Translate(posx, posy, 0);
            this.Begin (enuGraphicsPrimitives.Quads);
            this.SetTexCoord(0, 0);
            this.SetVertex(Vector2f.Zero);
            this.SetTexCoord(0, 1);
            this.SetVertex(new  Vector2f(0, h ) );
            this.SetTexCoord(1, 1);
            this.SetVertex(new  Vector2f(w, h));
            this.SetTexCoord(1, 0);
            this.SetVertex(new  Vector2f(w, 0));
            this.End();
            this.Projection.Translate(-posx, -posy, 0);
            //disable Texture 2D usage
            this.Capabilities.Texture2D = false;
         
        }
        /// <summary>
        /// this will raise the disposing event for resources to be freed
        /// </summary>
        public void FreeResources()
        {
            this.OnDisposing(EventArgs.Empty);
        }

        public static IntPtr CreateHDCFromHwnd(IntPtr hwnd)
        {
			#if OS_WINDOWS
            return IGK.OGLGame.Native.User32Lib.GetWindowDC(hwnd);
			#elif UNIX
			return IntPtr.Zero;
			#else
			return IntPtr.Zero;
			#endif
        }
    }
}

