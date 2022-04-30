

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsDeviceCapabilities.cs
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
file:GraphicsDeviceCapabilities.cs
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
using System.ComponentModel;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

using IGK.ICore;using IGK.GLLib;
namespace IGK.OGLGame.Graphics
{
    public class GraphicsDeviceCapabilities
    {
        const string CAPS = "Capabilities";
        const string INF = "Informations";
        const string STATES = "States";
        const string BITS = "Bits";


        private OGLGraphicsDevice m_graphicsDevice;
        private List<string> m_extension;
        [Category(INF)]
        [Browsable (false)]
        /// <summary>
        /// get the graphics device
        /// </summary>
        public OGLGraphicsDevice GraphicsDevice { get { return this.m_graphicsDevice; } }
        [Category(INF)]
        /// <summary>
        /// get all supported extensions list
        /// </summary>
        public string[] Extensions {
            get {
                return m_extension.ToArray ();// GL.Extensions;
            }
        }
         public string[] GetAdditionalMedhods()
         {
             System.Collections.IEnumerator d =  GL.AdditionalMethods;
             List<string> v_methods = new List<string> ();
             while (d.MoveNext())
             {
                 v_methods.Add(d.Current.ToString());
             }
             return v_methods.ToArray();
        }
        [Category (INF)]
        public string Version {
            get {
                return GL.glGetString(GL.GL_VERSION);
            }
        }
        [Category(INF)]
        public string Renderer {
            get {
                return GL.glGetString(GL.GL_RENDERER);
            }
        }
        [Category(INF)]
        public string Vendor{
            get
            {
                return GL.glGetString(GL.GL_VENDOR );
            }
        }
        [Category(INF)]
        public string ShadingLanguageVersion
        {
            get
            {
                return GL.glGetString(GL.GL_SHADING_LANGUAGE_VERSION);
            }
        }
        [Category(CAPS)]
        public bool ColorLogicOp
        {
            get
            {
                return GL.glIsEnabled(GL.GL_COLOR_LOGIC_OP);
            }
            set
            {
                Activate(GL.GL_COLOR_LOGIC_OP, value);
            }
        }
        [Category(CAPS)]
        public bool IndexLogicOp
        {
            get
            {
                return GL.glIsEnabled(GL.GL_INDEX_LOGIC_OP);
            }
            set
            {
                Activate(GL.GL_INDEX_LOGIC_OP, value);
            }
        }
        [Category(CAPS)]
        public bool Texture2D {
            get {
                return GL.glIsEnabled(GL.GL_TEXTURE_2D);
            }
            set {
                Activate(GL.GL_TEXTURE_2D, value);
            }
        }
        [Category(CAPS)]
        public bool Texture1D
        {
            get
            {
                return GL.glIsEnabled(GL.GL_TEXTURE_1D);
            }
            set
            {
                Activate(GL.GL_TEXTURE_1D, value);
            }
        }
        [Category(CAPS)]
        public bool Texture3D
        {
            get
            {
                return GL.glIsEnabled(GL.GL_TEXTURE_3D);
            }
            set
            {
                Activate(GL.GL_TEXTURE_3D, value);
            }
        }
        [Category(CAPS)]
        public bool TextureCubeMap
        {
            get
            {
                return GL.glIsEnabled(GL.GL_TEXTURE_CUBE_MAP);
            }
            set
            {
                Activate(GL.GL_TEXTURE_CUBE_MAP, value);
            }
        }
        [Category(CAPS)]
        public bool PointSmooth
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POINT_SMOOTH );
            }
            set
            {
                Activate(GL.GL_POINT_SMOOTH, value);
            }
        }
        [Category(CAPS)]
        public bool Blend {
            get {
                return GL.glIsEnabled(GL.GL_BLEND);
            }
            set {
                Activate(GL.GL_BLEND, value);                
            }
        }
        [Category(CAPS)]
        public bool Normalize {
            get {
                return GL.glIsEnabled(GL.GL_NORMALIZE );
            }
            set
            {
                Activate(GL.GL_NORMALIZE, value);
            }
        }
        /// <summary>
        /// get or set the scissor test capabilities.
        /// </summary>
        [Category(CAPS)]
        public bool ScissorTest
        {
            get
            {
                return GL.glIsEnabled(GL.GL_SCISSOR_TEST);
            }
            set
            {
                Activate(GL.GL_SCISSOR_TEST, value);
            }
        }
        #region MAP1
        /// <summary>
        /// get set set the Map1Vertex_3. used for draw spline.
        /// </summary>
        [Category(CAPS)]
        public bool Map1Vertex_3
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_VERTEX_3);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_VERTEX_3);
                else
                    GL.glDisable(GL.GL_MAP1_VERTEX_3);
            }
        }
        [Category(CAPS)]
        public bool Map1Vertex_4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_VERTEX_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_VERTEX_4);
                else
                    GL.glDisable(GL.GL_MAP1_VERTEX_4);
            }
        }
        [Category(CAPS)]
        public bool Map1Index
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_INDEX );
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_INDEX);
                else
                    GL.glDisable(GL.GL_MAP1_INDEX);
            }
        }
        [Category(CAPS)]
        public bool Map1Color4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_COLOR_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_COLOR_4 );
                else
                    GL.glDisable(GL.GL_MAP1_COLOR_4);
            }
        }
        [Category(CAPS)]
        public bool Map1Normal
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_NORMAL );
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_NORMAL);
                else
                    GL.glDisable(GL.GL_MAP1_NORMAL);
            }
        }
        [Category(CAPS)]
        public bool Map1TextureCoord1
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_TEXTURE_COORD_1 );
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_TEXTURE_COORD_1);
                else
                    GL.glDisable(GL.GL_MAP1_TEXTURE_COORD_1);
            }
        }
        [Category(CAPS)]
        public bool Map1TextureCoord2
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_TEXTURE_COORD_2);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_TEXTURE_COORD_2);
                else
                    GL.glDisable(GL.GL_MAP1_TEXTURE_COORD_2);
            }
        }
        [Category(CAPS)]
        public bool Map1TextureCoord3
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_TEXTURE_COORD_3);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_TEXTURE_COORD_3);
                else
                    GL.glDisable(GL.GL_MAP1_TEXTURE_COORD_3);
            }
        }
        [Category(CAPS)]
        public bool Map1TextureCoord4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP1_TEXTURE_COORD_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP1_TEXTURE_COORD_4);
                else
                    GL.glDisable(GL.GL_MAP1_TEXTURE_COORD_4);
            }
        }
        #endregion
        #region MAP2
        /// <summary>
        /// get set set the Map2Vertex_3. used for draw spline.
        /// </summary>
        [Category(CAPS)]
        public bool Map2Vertex_3
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_VERTEX_3);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_VERTEX_3);
                else
                    GL.glDisable(GL.GL_MAP2_VERTEX_3);
            }
        }
        [Category(CAPS)]
        public bool Map2Vertex_4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_VERTEX_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_VERTEX_4);
                else
                    GL.glDisable(GL.GL_MAP2_VERTEX_4);
            }
        }
        [Category(CAPS)]
        public bool Map2Index
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_INDEX);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_INDEX);
                else
                    GL.glDisable(GL.GL_MAP2_INDEX);
            }
        }
        [Category(CAPS)]
        public bool Map2Color4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_COLOR_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_COLOR_4);
                else
                    GL.glDisable(GL.GL_MAP2_COLOR_4);
            }
        }
        [Category(CAPS)]
        public bool Map2Normal
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_NORMAL);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_NORMAL);
                else
                    GL.glDisable(GL.GL_MAP2_NORMAL);
            }
        }
        [Category(CAPS)]
        public bool Map2TextureCoord1
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_TEXTURE_COORD_1);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_TEXTURE_COORD_1);
                else
                    GL.glDisable(GL.GL_MAP2_TEXTURE_COORD_1);
            }
        }
        [Category(CAPS)]
        public bool Map2TextureCoord2
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_TEXTURE_COORD_2);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_TEXTURE_COORD_2);
                else
                    GL.glDisable(GL.GL_MAP2_TEXTURE_COORD_2);
            }
        }
        [Category(CAPS)]
        public bool Map2TextureCoord3
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_TEXTURE_COORD_3);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_TEXTURE_COORD_3);
                else
                    GL.glDisable(GL.GL_MAP2_TEXTURE_COORD_3);
            }
        }
        [Category(CAPS)]
        public bool Map2TextureCoord4
        {
            get
            {
                return GL.glIsEnabled(GL.GL_MAP2_TEXTURE_COORD_4);
            }
            set
            {
                if (value)
                    GL.glEnable(GL.GL_MAP2_TEXTURE_COORD_4);
                else
                    GL.glDisable(GL.GL_MAP2_TEXTURE_COORD_4);
            }
        }
        #endregion
        [Category(CAPS)]
        public bool Lighting {
            get
            {
                return GL.glIsEnabled(GL.GL_LIGHTING);
            }
            set
            {
                Activate(GL.GL_LIGHTING, value);
            }
        }
        [Category(CAPS)]
        public bool LineStipple {
            get
            {
                return GL.glIsEnabled(GL.GL_LINE_STIPPLE);
            }
            set
            {
                Activate(GL.GL_LINE_STIPPLE, value);
            }
        }
        [Category(CAPS)]
        public bool LineSmooth
        {
            get
            {
                return GL.glIsEnabled(GL.GL_LINE_SMOOTH );
            }
            set
            {
                Activate(GL.GL_LINE_SMOOTH, value);
            }
        }
        [Category(CAPS)]
        public bool AlphaTest {
            get
            {
                return GL.glIsEnabled(GL.GL_ALPHA_TEST);
            }
            set
            {
                Activate(GL.GL_ALPHA_TEST, value);
            }
        }
        [Category(CAPS)]
        public bool PolygonStipple
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POLYGON_STIPPLE);
            }
            set
            {
                Activate(GL.GL_POLYGON_STIPPLE, value);
            }
        }
        [Category(CAPS)]
        public bool PolygonSmooth
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POLYGON_SMOOTH);
            }
            set
            {
                Activate(GL.GL_POLYGON_SMOOTH, value);
            }
        }
        [Category(CAPS)]
        public bool PolygonOffsetFill
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POLYGON_OFFSET_FILL);
            }
            set
            {
                Activate(GL.GL_POLYGON_OFFSET_FILL, value);
            }
        }
        [Category(CAPS)]
        public bool PolygonOffsetLine
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POLYGON_OFFSET_LINE);
            }
            set
            {
                Activate(GL.GL_POLYGON_OFFSET_LINE, value);
            }
        }
        [Category(CAPS)]
        public bool PolygonOffsetPoint
        {
            get
            {
                return GL.glIsEnabled(GL.GL_POLYGON_OFFSET_POINT);
            }
            set
            {
                Activate(GL.GL_POLYGON_OFFSET_POINT, value);
            }
        }
        /// <summary>
        /// get or set the Depth test
        /// </summary>
        [Category(CAPS)]
        public bool DepthTest {
            get
            {
                return GL.glIsEnabled(GL.GL_DEPTH_TEST);
            }
            set
            {
                Activate(GL.GL_DEPTH_TEST, value);
            }
        }
        /// <summary>
        /// get or set the Dither
        /// </summary>
        [Description("enabled or not dithering")]
        [Category(CAPS)]
        public bool Dither
        {
            get
            {
                return GL.glIsEnabled(GL.GL_DITHER);
            }
            set
            {
                Activate(GL.GL_DITHER, value);
            }
        }
        [Category(CAPS)]
        public bool CullFace
        {
            get
            {
                return GL.glIsEnabled(GL.GL_CULL_FACE);
            }
            set
            {
                Activate(GL.GL_CULL_FACE, value);
            }
        }
        [Category(CAPS)]
        public bool Fog { 
            get{
                return GL.glIsEnabled(GL.GL_FOG);
            }
            set {
                Activate(GL.GL_FOG, value);
            }
        }
        /// <summary>
        /// get or set Multisample
        /// </summary>
        [Category(CAPS)]
        public bool MultiSample {
            get
            {
                return GL.glIsEnabled(GL.GL_MULTISAMPLE );
            }
            set
            {
                Activate(GL.GL_MULTISAMPLE , value);
            }
        }
        [Category(CAPS)]
        public bool RescaleNormal {
            get {
                return GL.glIsEnabled(GL.GL_RESCALE_NORMAL);
            }
            set {
                Activate(GL.GL_RESCALE_NORMAL, value);
            }
        }
        [Category(STATES)]
        public int TextureStackDepth
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_TEXTURE_STACK_DEPTH);
            }
        }
        [Category(STATES)]
        public int TextureMaxSize
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_TEXTURE_SIZE);
            }
        }
        [Category(STATES)]
        public int Texture3DMaxSize
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_3D_TEXTURE_SIZE);
            }
        }
        [Category(STATES)]
        public int MaxLights {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_LIGHTS);
            }
        }
        [Category(STATES)]
        public int MaxAuxBuffers
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_AUX_BUFFERS);
            }
        }
        [Category(STATES)]
        /// <summary>
        /// get the max projection stact depth
        /// </summary>
        public int MaxProjectViewStactDeth
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_PROJECTION_STACK_DEPTH);
            }
        }
        [Category(STATES)]
        /// <summary>
        /// get the max clip plane
        /// </summary>
        public int MaxClipPlanes
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_MAX_CLIP_PLANES);
            }
        }
        [Category(STATES)]
        /// <summary>
        /// Get the max view port dimension
        /// </summary>
        public Size  MaxViewPortDims {
            get { 
                return GraphicsDevice.GetIntegerv<Size>(GL.GL_MAX_VIEWPORT_DIMS);
            }
        }



        internal GraphicsDeviceCapabilities(OGLGraphicsDevice graphicDevice)
        {
            this.m_graphicsDevice = graphicDevice;
            this.m_extension = new List<string>();

            string[] v_ext  = GL.GetExtensions();
            //int c = 0;
            //GL.glGetIntegerv(GL.GL_NUM_EXTENSIONS, ref c);
            //if (c > 0)
            //{

            //   // var T =  GL.GetProcedure<PFNGLGETSTRINGIPROC>("glGetStringi");

            //    string[] v_ext = new string[c];
            //    for (uint i = 0; i < c; i++)
            //    {
            //        v_ext[i] =  Marshal.PtrToStringAnsi (GL.glGetStringi(GL.GL_EXTENSIONS, i));
            //    }
            //    //string[] v_ext = (GL.glGetString(GL.GL_EXTENSIONS) + "").Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            //    //if (GL.Extensions != null)
            if (v_ext !=null)
                    this.m_extension.AddRange(v_ext);//.AddRange(GL.Extensions);
           // }
            this.m_extension.Sort();
        }
        private void Activate(uint p, bool value)
        {
            if (value)
            {
                GL.glEnable(p);
            }
            else
                GL.glDisable(p);
        }
        public void Enable(uint capability)
        {
            GL.glEnable(capability);
        }
        public void Disable(uint capability)
        {
            GL.glDisable(capability);
        }
        public bool IsExtensionSupported(string ExtensionName)
        {
            return this.m_extension.Contains(ExtensionName);
        }
        [Category(CAPS)]
        //array
        public bool VertexArray {
            get {
                return IsArrayEnabled(GL.GL_VERTEX_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_VERTEX_ARRAY, value);
            }
        }
        [Category(CAPS)]
        public bool ColorArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_COLOR_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_COLOR_ARRAY, value);
            }
        }
        [Category(CAPS)]
        public bool SecondaryColorArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_SECONDARY_COLOR_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_SECONDARY_COLOR_ARRAY, value);
            }
        }
        [Category(CAPS)]
        public bool IndexArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_INDEX_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_INDEX_ARRAY, value);
            }
        }
        [Category(CAPS)]
        public bool NormalArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_NORMAL_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_NORMAL_ARRAY , value);
            }
        }
        [Category(CAPS)]
        public bool FogCoordArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_FOG_COORDINATE_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_FOG_COORDINATE_ARRAY , value);
            }
        }
        [Category(CAPS)]
        public bool TextureCoordArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_TEXTURE_COORD_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_TEXTURE_COORD_ARRAY, value);
            }
        }
        [Category(CAPS)]
        public bool EdgeFlagArray
        {
            get
            {
                return IsArrayEnabled(GL.GL_EDGE_FLAG_ARRAY);
            }
            set
            {
                this.ActivateArray(GL.GL_EDGE_FLAG_ARRAY, value);
            }
        }
        private bool IsArrayEnabled(uint cap)
        {
            return GraphicsDevice.GetBooleanv<bool>(cap);
        }
        private void ActivateArray(uint cap, bool value)
        {
            if (value)
            {
                GL.glEnableClientState(cap);
            }
            else {
                GL.glDisableClientState(cap);
            }
        }
        [Category(BITS)]
        public int RedBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_RED_BITS); } }
        [Category(BITS)]
        public int GreenBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_GREEN_BITS); } }
        [Category(BITS)]
        public int BlueBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_BLUE_BITS); } }
        [Category(BITS)]
        public int AlphaBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_ALPHA_BITS); } }
        [Category(BITS)]
        public int IndexBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_INDEX_BITS); } }
        [Category(BITS)]
        public int DepthBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_DEPTH_BITS); } }
        [Category(BITS)]
        public int StencilBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_STENCIL_BITS); } }
        [Category(BITS)]
        public int AccumRedBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_ACCUM_RED_BITS); } }
        [Category(BITS)]
        public int AccumGreenBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_ACCUM_GREEN_BITS); } }
        [Category(BITS)]
        public int AccumBlueBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_ACCUM_BLUE_BITS); } }
        [Category(BITS)]
        public int AccumAlphaBits { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_ACCUM_ALPHA_BITS); } }
        /// <summary>
        /// get the scissor bit in use
        /// </summary>
        [Category(BITS)]
        public int ScissorBit { get { return this.GraphicsDevice.GetIntegerv<int>(GL.GL_SCISSOR_BIT); } }
        //sample - buffer 
        [Category(GLConstant.SAMPLE_BUFFERS_CAT)]
        public int SampleBuffer { get { return IGK.GLLib.GLUtils.GetIntegerv<int>(GL.GL_SAMPLE_BUFFERS); } }
        [Category(GLConstant.SAMPLE_BUFFERS_CAT)]
        public int Samples { get { return IGK.GLLib.GLUtils.GetIntegerv<int>(GL.GL_SAMPLES); } }
        #region Buffers
        /// <summary>
        /// get if device support stereo 
        /// </summary>
        [Category("Buffers")]
        public bool Stereo{ get { return IGK.GLLib.GLUtils.GetBooleanv <bool>(GL.GL_STEREO); } }
        /// <summary>
        /// get if device support double buffer
        /// </summary>
        [Category("Buffers")]
        public bool DoubleBuffer { get { return IGK.GLLib.GLUtils.GetBooleanv<bool>(GL.GL_DOUBLEBUFFER); } }
        #endregion
    }
}

