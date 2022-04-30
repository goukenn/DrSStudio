

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Enumerations.cs
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
file:Enumerations.cs
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
/* This file is part of IGK-DRAWING SOFT.
*    IGK-DRAWING FOFT is free software: you can redistribute it and/or modify
*    it under the terms of the GNU General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    IGK-DRAWING FOFT is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// represent standard opengl capability 
    /// </summary>
    public enum enuCapability : uint
    {
        AlphaTest= GL.GL_ALPHA_TEST,
        AutoNormal= GL.GL_AUTO_NORMAL,
        Blend= GL.GL_BLEND,
        //ClipPlanei= GLApi.GL_CLIP_PLANEi,
        ColorArray= GL.GL_COLOR_ARRAY,
        ColorLogicOp= GL.GL_COLOR_LOGIC_OP,
        ColorMaterial= GL.GL_COLOR_MATERIAL,
        CullFace= GL.GL_CULL_FACE,
        DepthTest= GL.GL_DEPTH_TEST,
        Dither= GL.GL_DITHER,
        EdgeFlagArray= GL.GL_EDGE_FLAG_ARRAY,
        Fog= GL.GL_FOG,
        IndexArray= GL.GL_INDEX_ARRAY,
        IndexLogicOp= GL.GL_INDEX_LOGIC_OP,
        Lighting= GL.GL_LIGHTING,
        LineSmooth= GL.GL_LINE_SMOOTH,
        LineStipple= GL.GL_LINE_STIPPLE,
        Map1Color4= GL.GL_MAP1_COLOR_4,
        Map2TextureCoord2= GL.GL_MAP2_TEXTURE_COORD_2,
        Map2TextureCoord3= GL.GL_MAP2_TEXTURE_COORD_3,
        Map2TextureCoord4= GL.GL_MAP2_TEXTURE_COORD_4,
        Map2Vertex3= GL.GL_MAP2_VERTEX_3,
        Map2Vertex4= GL.GL_MAP2_VERTEX_4,
        NormalArray= GL.GL_NORMAL_ARRAY,
        Normalize= GL.GL_NORMALIZE,
        PointSmooth= GL.GL_POINT_SMOOTH,
        PolygonSmooth= GL.GL_POLYGON_SMOOTH,
        PolygonOffsetFill= GL.GL_POLYGON_OFFSET_FILL,
        PolygonOffsetLine= GL.GL_POLYGON_OFFSET_LINE,
        PolygonOffsetPoint= GL.GL_POLYGON_OFFSET_POINT,
        PolygonStipple= GL.GL_POLYGON_STIPPLE,
        ScissorTest= GL.GL_SCISSOR_TEST,
        StencilTest= GL.GL_STENCIL_TEST,
        Texture1d= GL.GL_TEXTURE_1D,
        Texture2d= GL.GL_TEXTURE_2D,
        TextureCoordArray= GL.GL_TEXTURE_COORD_ARRAY,
        TextureGenQ= GL.GL_TEXTURE_GEN_Q,
        TextureGenR= GL.GL_TEXTURE_GEN_R,
        TextureGenS= GL.GL_TEXTURE_GEN_S,
        TextureGenT= GL.GL_TEXTURE_GEN_T,
        VertexArray= GL.GL_VERTEX_ARRAY,
        Multisample = GL.GL_MULTISAMPLE
    }
    public enum enuLightName : uint
    {
        Light0 = GL.GL_LIGHT0 ,
        Light1 = GL.GL_LIGHT1 ,
        Light2 = GL.GL_LIGHT2 ,
        Light3 = GL.GL_LIGHT3 ,
        Light4 = GL.GL_LIGHT4 ,
        Light5 = GL.GL_LIGHT5 ,
        Light6 = GL.GL_LIGHT6 ,
        Light7 = GL.GL_LIGHT7 ,
    }
    public enum GLClipPlaneName : uint
    {
        Plane0 = GL.GL_CLIP_PLANE0 ,
        Plane1 = GL.GL_CLIP_PLANE1 ,
        Plane2 = GL.GL_CLIP_PLANE2 ,
        Plane3 = GL.GL_CLIP_PLANE3 ,
        Plane4 = GL.GL_CLIP_PLANE4 ,
        Plane5 = GL.GL_CLIP_PLANE5 ,
    }
    public enum enuPolygonFace : uint
    {
        FrontAndBack = GL.GL_FRONT_AND_BACK ,
        Front= GL.GL_FRONT,
        Back= GL.GL_BACK 
    }
    /// <summary>
    /// represent the polygon representation mode
    /// </summary>
    public enum enuPolygonFaceMode : uint
    {
        Fill= GL.GL_FILL,
        Line = GL.GL_LINE,
        Point = GL.GL_POINT,
    }
    /// <summary>
    /// represent the polygon orientation
    /// </summary>
    public enum enuPolygonOrientation : uint
    {
        /// <summary>
        /// represent the anti-clock wise orientation
        /// </summary>
        CCW = GL.GL_CCW,
        /// <summary>
        /// represent the clock wise orientation
        /// </summary>
        CW = GL.GL_CW
    }
    /// <summary>
    /// represent the client state
    /// </summary>
    public enum enuClientState : uint
    {
        NormalArray = GL.GL_NORMAL_ARRAY,
        VertexArray = GL.GL_VERTEX_ARRAY,
        ColorArray = GL.GL_COLOR_ARRAY,
        SecondaryColor = GL.GL_SECONDARY_COLOR_ARRAY,
        IndexArray = GL.GL_INDEX_ARRAY,
        TextureCoordArray = GL.GL_TEXTURE_COORD_ARRAY,
        EdgeFlagArray = GL.GL_EDGE_FLAG_ARRAY,
        FogArray = GL.GL_FOG_COORD_ARRAY
    }
    public enum enuGLDataType : uint
    {
        Boolean = GL.GL_BOOL,
        Byte = GL.GL_BYTE,
        UByte = GL.GL_UNSIGNED_BYTE,
        Byte2 = GL.GL_2_BYTES,
        Byte3 = GL.GL_3_BYTES,
        Byte4 = GL.GL_4_BYTES,
        Short = GL.GL_SHORT,
        UShort = GL.GL_UNSIGNED_SHORT,
        Int = GL.GL_INT,
        UInt = GL.GL_UNSIGNED_INT,
        Float = GL.GL_FLOAT,
        Double = GL.GL_DOUBLE,
    }
    public enum GLDrawElementDataType : uint
    {
        UByte = GL.GL_UNSIGNED_BYTE,
        UShort = GL.GL_UNSIGNED_SHORT,       
        UInt = GL.GL_UNSIGNED_INT
    }
    public enum enuGLDataStructure : uint
    {
        RGB = GL.GL_RGB ,
        RGBA = GL.GL_RGBA ,
        RED = GL.GL_RED ,
        GREEN = GL.GL_GREEN ,
        BLUE = GL.GL_BLUE ,
    }
    public enum enuGLShadeModel: uint
    {
        Flat = GL.GL_FLAT ,
        Smooth = GL.GL_SMOOTH 
    }
    public enum enuGLRenderMode : uint
    {
        Render =GL.GL_RENDER ,
        Select = GL.GL_SELECT,
        FeedBack = GL.GL_FEEDBACK 
    }
    public enum GLLightModelControl : uint
    {
        SingleColor  = GL.GL_SINGLE_COLOR ,
        SeparateSpecularColor = GL.GL_SEPARATE_SPECULAR_COLOR 
    }
    public enum GLMaterialProperty : uint
    {
        Ambient  =  GL.GL_AMBIENT,
        Diffuse  =  GL.GL_DIFFUSE,
        AmbientAndDiffuse = GL.GL_AMBIENT_AND_DIFFUSE,
        Specular =  GL.GL_SPECULAR,
        Shininess =  GL.GL_SHININESS,
        Emission =  GL.GL_EMISSION,
        ColorIndexes = GL.GL_COLOR_INDEXES
    }
    public enum GLColorMaterialParameter : uint
    { 
        Ambient  =  GL.GL_AMBIENT,
        Diffuse  =  GL.GL_DIFFUSE,
        AmbientAndDiffuse = GL.GL_AMBIENT_AND_DIFFUSE,
        Specular =  GL.GL_SPECULAR,        
        Emission =  GL.GL_EMISSION,        
    }
    public enum enuGLBlendFunc : uint
    {
        Zero = GL.GL_ZERO,
        One = GL.GL_ONE,
        SrcColor = GL.GL_SRC_COLOR,
        OneMinusSrcColor = GL.GL_ONE_MINUS_SRC_COLOR,
        DstColor = GL.GL_DST_COLOR,
        OneMinusDstColor = GL.GL_ONE_MINUS_DST_COLOR,
        SrcAlpha = GL.GL_SRC_ALPHA,
        OneMinusSrcAlpha = GL.GL_ONE_MINUS_SRC_ALPHA,
        DstAlpha = GL.GL_DST_ALPHA,
        OneMinusDstAlpha = GL.GL_ONE_MINUS_DST_ALPHA,
        /// <summary>
        /// use SetBlendColor to sp�cify color
        /// </summary>
        ConstantColor = GL.GL_CONSTANT_COLOR,
        /// <summary>
        /// use SetBlendColor to sp�cify color
        /// </summary>
        OneMinusConstantColor = GL.GL_ONE_MINUS_CONSTANT_COLOR,
        /// <summary>
        /// use SetBlendColor to sp�cify color
        /// </summary>
        ConstantAlpha = GL.GL_CONSTANT_ALPHA,
        /// <summary>
        /// use SetBlendColor to sp�cify color
        /// </summary>
        OneMinusConstantAlpha = GL.GL_ONE_MINUS_CONSTANT_ALPHA,
        /// <summary>
        /// use it only for blending source
        /// </summary>
        SrcAlphaSaturate = GL.GL_SRC_ALPHA_SATURATE
    }
    public enum enuGLBlendEquation : uint
    {
        FuncAdd = GL.GL_FUNC_ADD,
        FuncSubtract= GL.GL_FUNC_SUBTRACT ,
        FuncReverseSustract= GL.GL_FUNC_REVERSE_SUBTRACT,
        Min= GL.GL_MIN ,
        Max = GL.GL_MAX 
    }
    public enum GLHintTarget : uint
    {
        PointSmooth = GL.GL_POINT_SMOOTH_HINT ,
        LineSmooth = GL.GL_LINE_SMOOTH_HINT,
        PolygonSmooth = GL.GL_POLYGON_SMOOTH_HINT,
        Fog = GL.GL_FOG_HINT,
        PerpectiveCorrection = GL.GL_PERSPECTIVE_CORRECTION_HINT,
        GenerateMipMap = GL.GL_GENERATE_MIPMAP_HINT,
        TextureCompression = GL.GL_TEXTURE_COMPRESSION_HINT
    }
    public enum GLHint :uint
    {
        Fastest = GL.GL_FASTEST,
        Nicest = GL.GL_NICEST,
        DontCare = GL.GL_DONT_CARE
    }
    public enum GLFog: uint
    {
        Mode = GL.GL_FOG_MODE,
        Index = GL.GL_FOG_INDEX,
        Color = GL.GL_FOG_COLOR,
        Density = GL.GL_FOG_DENSITY,
        Start =GL.GL_FOG_START ,
        End=GL.GL_FOG_END,
    }
    public enum GLFogMode :uint
    {
        Exp = GL.GL_EXP,
        Exp2 = GL.GL_EXP2,
        Linear = GL.GL_LINEAR,
    }
    /// <summary>
    /// represent the list mode execution
    /// </summary>
	public enum enuListMode :uint
	{
        /// <summary>
        /// compile only the new list
        /// </summary>
	    Compile = GL.GL_COMPILE,
        /// <summary>
        /// compile and ex�cute the new list
        /// </summary>
	    CompileAndExectue = GL.GL_COMPILE_AND_EXECUTE
	}
    [Flags()]
    public enum GLAttribBit : uint
    {
        AccumBuffer = GL.GL_ACCUM_BUFFER_BIT,
        AllAttrib = GL.GL_ALL_ATTRIB_BITS,
        ColorBuffer = GL.GL_COLOR_BUFFER_BIT,
        Current = GL.GL_CURRENT_BIT,
        DepthBuffer = GL.GL_DEPTH_BUFFER_BIT,
        Enable = GL.GL_ENABLE_BIT,
        Eval = GL.GL_EVAL_BIT,
        Fog = GL.GL_FOG_BIT,
        Hint = GL.GL_HINT_BIT,
        Lighting = GL.GL_LIGHTING_BIT,
        Line = GL.GL_LINE_BIT,
        List = GL.GL_LIST_BIT,
        Multisample = GL.GL_MULTISAMPLE_BIT,
        PixelMode = GL.GL_PIXEL_MODE_BIT,
        Point = GL.GL_POINT_BIT,
        Polygon = GL.GL_POLYGON_BIT,
        PolygonStipple = GL.GL_POLYGON_STIPPLE,
        Scissor = GL.GL_SCISSOR_BIT,
        StencilBuffer = GL.GL_STENCIL_BUFFER_BIT,
        Texture = GL.GL_TEXTURE_BIT,
        Transform = GL.GL_TRANSFORM_BIT,
        Viewport = GL.GL_VIEWPORT_BIT
    }
    [Flags()]
    public enum GLClientAttribBit : uint
    {
        ClientPixelStore = GL.GL_CLIENT_PIXEL_STORE_BIT,
        ClientVertexArray = GL.GL_CLIENT_VERTEX_ARRAY_BIT,
        AllClientAttrib = ClientPixelStore | ClientVertexArray 
    }
    //public enum GLPixelFormat : uint
    //{
    //    ColorIndex = GL.GL_COLOR_INDEX,
    //    RGB = GL.GL_RGB,
    //    RGBA = GL.GL_RGBA,
    //    BGR = GL.GL_BGR,
    //    BGRA = GL.GL_BGRA,
    //    Red = GL.GL_RED,
    //    Green = GL.GL_GREEN,
    //    Blue = GL.GL_BLUE,
    //    Alpha = GL.GL_ALPHA,
    //    Luminance = GL.GL_LUMINANCE,
    //    LuminanceAlpha = GL.GL_LUMINANCE_ALPHA,
    //    StencilIndex = GL.GL_STENCIL_INDEX,
    //    DepthComponent = GL.GL_DEPTH_COMPONENT
    //}
    public enum GLPixelDataType : uint
    {
        UByte = GL.GL_UNSIGNED_BYTE,
        Byte = GL.GL_BYTE,
        Bitmap = GL.GL_BITMAP,
        UShort = GL.GL_UNSIGNED_SHORT,
        Short = GL.GL_SHORT,
        UInt = GL.GL_UNSIGNED_INT,
        Int = GL.GL_INT,
        Float = GL.GL_FLOAT,
        Ubyte_3_3_2 = GL.GL_UNSIGNED_BYTE_3_3_2,
        Ubyte_2_3_3_REV = GL.GL_UNSIGNED_BYTE_2_3_3_REV,
        UShort_5_6_5 = GL.GL_UNSIGNED_SHORT_5_6_5,
        UShort_5_6_5_REV = GL.GL_UNSIGNED_SHORT_5_6_5_REV,
        UShort_4_4_4_4 = GL.GL_UNSIGNED_SHORT_4_4_4_4,
        UShort_4_4_4_4_REV = GL.GL_UNSIGNED_SHORT_4_4_4_4_REV ,
        UShort_5_5_5_1 = GL.GL_UNSIGNED_SHORT_5_5_5_1,
        UShort_1_5_5_5_REV = GL.GL_UNSIGNED_SHORT_1_5_5_5_REV,
        UInt_8_8_8_8 = GL.GL_UNSIGNED_INT_8_8_8_8,
        UInt_8_8_8_8_REV = GL.GL_UNSIGNED_INT_8_8_8_8_REV,
        UInt_10_10_10_2 = GL.GL_UNSIGNED_INT_10_10_10_2,
        UInt_2_10_10_10_REV = GL.GL_UNSIGNED_INT_2_10_10_10_REV,
    }
    public enum GLPixelStore : uint
    {
        UnpackSwapBytes = GL.GL_UNPACK_SWAP_BYTES,
        UnpackLsbFirst = GL.GL_UNPACK_LSB_FIRST,
        UnpackRowLength = GL.GL_UNPACK_ROW_LENGTH,
        UnpackSkipRows = GL.GL_UNPACK_SKIP_ROWS,
        UnpackSkipPixels = GL.GL_UNPACK_SKIP_PIXELS,
        UnpackAlignment = GL.GL_UNPACK_ALIGNMENT,
        UnpackImageHeight = GL.GL_UNPACK_IMAGE_HEIGHT,
        UnpackSkipImages = GL.GL_UNPACK_SKIP_IMAGES,
        PackSwapBytes = GL.GL_PACK_SWAP_BYTES,
        PackLsbFirst = GL.GL_PACK_LSB_FIRST,
        PackRowLength = GL.GL_PACK_ROW_LENGTH,
        PackSkipRows = GL.GL_PACK_SKIP_ROWS,
        PackSkipPixels = GL.GL_PACK_SKIP_PIXELS,
        PackAlignment = GL.GL_PACK_ALIGNMENT,
        PackImageHeight = GL.GL_PACK_IMAGE_HEIGHT,
        PackSkipImages = GL.GL_PACK_SKIP_IMAGES
    }
    public enum GLCopyBuffer : uint
    {
        Color = GL.GL_COLOR ,
        Stencil = GL.GL_STENCIL,
        Depth = GL.GL_DEPTH
    }
    public enum GLPixelTransfert : uint
    {
        MapColor = GL.GL_MAP_COLOR ,
        MapStencil = GL.GL_MAP_STENCIL,
        IndexShift = GL.GL_INDEX_SHIFT,
        IndexOffset = GL.GL_INDEX_OFFSET,
        RedScale = GL.GL_RED_SCALE,
        GreenScale = GL.GL_GREEN_SCALE,
        BlueScale = GL.GL_BLUE_SCALE,
        AlphaScale = GL.GL_ALPHA_SCALE,
        DepthScale = GL.GL_DEPTH_SCALE,
        RedBias = GL.GL_RED_BIAS,
        GreenBias = GL.GL_GREEN_BIAS,
        BlueBias = GL.GL_BLUE_BIAS,
        AlphaBias = GL.GL_ALPHA_BIAS,
        DepthBias = GL.GL_DEPTH_BIAS,
        /// <summary>
        /// verifier si l'imagin sub set est actif.
        /// </summary>
        PostConvolutionRedScale = GL.GL_POST_CONVOLUTION_RED_SCALE,
        PostConvolutionGreenScale = GL.GL_POST_CONVOLUTION_GREEN_SCALE,
        PostConvolutionBlueScale = GL.GL_POST_CONVOLUTION_BLUE_SCALE,
        PostConvolutionAlphaScale = GL.GL_POST_CONVOLUTION_ALPHA_SCALE,
        PostConvolutionRedBias = GL.GL_POST_CONVOLUTION_RED_BIAS,
        PostConvolutionGreenBias = GL.GL_POST_CONVOLUTION_GREEN_BIAS,
        PostConvolutionBlueBias = GL.GL_POST_CONVOLUTION_BLUE_BIAS,
        PostConvolutionAlphaBias = GL.GL_POST_CONVOLUTION_ALPHA_BIAS,
        PostColorMatrixRedScale = GL.GL_POST_COLOR_MATRIX_RED_SCALE,
        PostColorMatrixGreenScale = GL.GL_POST_COLOR_MATRIX_GREEN_SCALE,
        PostColorMatrixBlueScale = GL.GL_POST_COLOR_MATRIX_BLUE_SCALE,
        PostColorMatrixAlphaScale = GL.GL_POST_COLOR_MATRIX_ALPHA_SCALE,
        PostColorMatrixRedBias = GL.GL_POST_COLOR_MATRIX_RED_BIAS,
        PostColorMatrixGreenBias = GL.GL_POST_COLOR_MATRIX_GREEN_BIAS,
        PostColorMatrixBlueBias = GL.GL_POST_COLOR_MATRIX_BLUE_BIAS,
        PostColorMatrixAlphaBias = GL.GL_POST_COLOR_MATRIX_ALPHA_BIAS,
    }
    public enum GLPixelMap : uint
    {
        MapItoI = GL.GL_PIXEL_MAP_I_TO_I,
        MapStoS = GL.GL_PIXEL_MAP_S_TO_S,
        MapItoR = GL.GL_PIXEL_MAP_I_TO_R,
        MapItoG = GL.GL_PIXEL_MAP_I_TO_G,
        MapItoB = GL.GL_PIXEL_MAP_I_TO_B,
        MapItoA = GL.GL_PIXEL_MAP_I_TO_A,
        MapRtoR = GL.GL_PIXEL_MAP_R_TO_R,
        MapBtoB = GL.GL_PIXEL_MAP_B_TO_B,
        MapGtoG = GL.GL_PIXEL_MAP_G_TO_G,
        MapAtoA = GL.GL_PIXEL_MAP_A_TO_A,
    }
    public enum GLTextureType : uint
    {        
        Texture1D = GL.GL_TEXTURE_1D,
        Texture2D = GL.GL_TEXTURE_2D,
        Texture3D = GL.GL_TEXTURE_3D
    }
    public enum GLTextureParameterType : uint
    {
        Texture1D = GL.GL_TEXTURE_1D,
        Texture2D = GL.GL_TEXTURE_2D,
        Texture3D = GL.GL_TEXTURE_3D,
        CubeMap =  GL.GL_TEXTURE_CUBE_MAP
    }
    public enum GLTex2DTarget : uint
    {        
        Texture2D = GL.GL_TEXTURE_2D,
        ProxyTexure2D = GL.GL_PROXY_TEXTURE_2D ,
        ProxyCubeTexture2D = GL.GL_PROXY_TEXTURE_CUBE_MAP,
        MapPositiveX = GL.GL_TEXTURE_CUBE_MAP_POSITIVE_X,
        MapPositiveY = GL.GL_TEXTURE_CUBE_MAP_POSITIVE_Y,
        MapPositiveZ = GL.GL_TEXTURE_CUBE_MAP_POSITIVE_Z,
        MapNegativeX = GL.GL_TEXTURE_CUBE_MAP_NEGATIVE_X,
        MapNegativeY = GL.GL_TEXTURE_CUBE_MAP_NEGATIVE_Y,
        MapNegativeZ = GL.GL_TEXTURE_CUBE_MAP_NEGATIVE_Z
    }
    public enum GLTex1DTarget : uint
    {
        Texture1D = GL.GL_TEXTURE_1D,
        Proxy1D = GL.GL_PROXY_TEXTURE_1D,
    }
    public enum GLTex3DTarget : uint
    {
        Texture3D = GL.GL_TEXTURE_3D,
        Proxy3D = GL.GL_PROXY_TEXTURE_3D
    }
    public enum GLTexInternalFormat : uint
    {
        One = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Alpha = GL.GL_ALPHA,
        Alpha4 = GL.GL_ALPHA4,
        Alpha8 = GL.GL_ALPHA8,
        Alpha12 = GL.GL_ALPHA12,
        Alpha16 = GL.GL_ALPHA16,
        Luminance = GL.GL_LUMINANCE,
        Luminance4 = GL.GL_LUMINANCE4,
        Luminance8 = GL.GL_LUMINANCE8,
        Luminance12 = GL.GL_LUMINANCE12,
        Luminance16 = GL.GL_LUMINANCE16,
        LuminanceAlpha = GL.GL_LUMINANCE_ALPHA,
        Luminance4Alpha4 = GL.GL_LUMINANCE4_ALPHA4,
        Luminance6Alpha2 = GL.GL_LUMINANCE6_ALPHA2,
        Luminance8Alpha8 = GL.GL_LUMINANCE8_ALPHA8,
        Luminance12Alpha4 = GL.GL_LUMINANCE12_ALPHA4,
        Luminance12Alpha12 = GL.GL_LUMINANCE12_ALPHA12,
        Luminance16Alpha16 = GL.GL_LUMINANCE16_ALPHA16,
        Intensity = GL.GL_INTENSITY,
        Intensity4 = GL.GL_INTENSITY4,
        Intensity8 = GL.GL_INTENSITY8,
        Intensity12 = GL.GL_INTENSITY12,
        Intensity16 = GL.GL_INTENSITY16,
        RGB = GL.GL_RGB,
        R3G3B2 = GL.GL_R3_G3_B2,
        RGB4 = GL.GL_RGB4,
        RGB5 = GL.GL_RGB5,
        RGB8 = GL.GL_RGB8,
        RGB10 = GL.GL_RGB10,
        RGB12 = GL.GL_RGB12,
        RGB16 = GL.GL_RGB16,
        RGBA = GL.GL_RGBA,
        RGBA2 = GL.GL_RGBA2,
        RGBA4 = GL.GL_RGBA4,
        RGB5A1 = GL.GL_RGB5_A1,
        RGBA8 = GL.GL_RGBA8,
        RGB10A2 = GL.GL_RGB10_A2,
        RGBA12 = GL.GL_RGBA12,
        RGBA16 = GL.GL_RGBA16,
    }
    public enum GLTexPixelFormat : uint
    {
        ColorIndex = GL.GL_COLOR_INDEX,
        RGB = GL.GL_RGB,
        RGBA = GL.GL_RGBA,
        Red = GL.GL_RED,
        Green = GL.GL_GREEN,
        Blue = GL.GL_BLUE,
        Alpha = GL.GL_ALPHA,
        Luminance = GL.GL_LUMINANCE,
        LuminanceAlpha = GL.GL_LUMINANCE_ALPHA
        //StencilIndex = GLApi.GL_STENCIL_INDEX,
        //DepthComponent = GLApi.GL_DEPTH_COMPONENT
    }
    public enum GLTexDataType : uint
    {
        UByte = GL.GL_UNSIGNED_BYTE,
        Byte = GL.GL_BYTE,
        Bitmap = GL.GL_BITMAP,
        UShort = GL.GL_UNSIGNED_SHORT,
        Short = GL.GL_SHORT,
        UInt = GL.GL_UNSIGNED_INT,
        Int = GL.GL_INT,
        Float = GL.GL_FLOAT,
    }
    public enum GLTexEnvTarget : uint
    {
        TextureFilterControl = GL.GL_TEXTURE_FILTER_CONTROL,
        TextureEnv = GL.GL_TEXTURE_ENV
    }
    public enum GLTexFilterValue : uint
    {
        TextureLodBias = GL.GL_TEXTURE_LOD_BIAS ,
    }
    public enum GLTexEnvValue : uint
    {
        TextureEnvMode = GL.GL_TEXTURE_ENV_MODE,
        TextureEnvColor = GL.GL_TEXTURE_ENV_COLOR,
        CombineRGB = GL.GL_COMBINE_RGB,
        CombineAlpha = GL.GL_COMBINE_ALPHA,
        Source0RGB = GL.GL_SOURCE0_RGB,
        Source1RGB = GL.GL_SOURCE1_RGB,
        Source2RGB = GL.GL_SOURCE2_RGB,
        Source0ALPHA = GL.GL_SOURCE0_ALPHA,
        Source1ALPHA = GL.GL_SOURCE1_ALPHA,
        Source2ALPHA = GL.GL_SOURCE2_ALPHA,
        Operand0RGB = GL.GL_OPERAND0_RGB,
        Operand1RGB = GL.GL_OPERAND1_RGB,
        Operand2RGB = GL.GL_OPERAND2_RGB,
        Operand0ALPHA = GL.GL_OPERAND0_ALPHA,
        Operand1ALPHA = GL.GL_OPERAND1_ALPHA,
        Operand2ALPHA = GL.GL_OPERAND2_ALPHA,
        RGBScale = GL.GL_RGB_SCALE,
        ALPHAScale = GL.GL_ALPHA_SCALE,
    }
    public enum GLCombineRGB : uint
    {
        Replace = GL.GL_REPLACE,
        Modulate = GL.GL_MODULATE,        
        Add = GL.GL_ADD,
        AddSigned = GL.GL_ADD_SIGNED,
        Interpolate = GL.GL_INTERPOLATE,
        Substract = GL.GL_SUBTRACT,
        Dot3RGB = GL.GL_DOT3_RGB,
        Dot3RGBA = GL.GL_DOT3_RGBA        
    }
    public enum GLCombineALPHA : uint
    {
        Replace = GL.GL_REPLACE,
        Modulate = GL.GL_MODULATE,
        Add = GL.GL_ADD,
        AddSigned = GL.GL_ADD_SIGNED,
        Interpolate = GL.GL_INTERPOLATE,
        Substract = GL.GL_SUBTRACT        
    }
    public enum GLTextureSource : uint
    {
        Texture = GL.GL_TEXTURE,
        Constant = GL.GL_CONSTANT,
        PrimaryColor = GL.GL_PRIMARY_COLOR,
        Previous = GL.GL_PREVIOUS
    }
    public enum GLTextureOpRGB : uint
    {
        SrcColor = GL.GL_SRC_COLOR,
        OneMinusSrcColor = GL.GL_ONE_MINUS_SRC_COLOR, 
        SrcAlpha = GL.GL_SRC_ALPHA,
        OneMinusSrcAlpha = GL.GL_ONE_MINUS_SRC_ALPHA    
    }
    public enum GLTextureOpALPHA : uint
    {
        SrcAlpha = GL.GL_SRC_ALPHA,
        OneMinusSrcAlpha = GL.GL_ONE_MINUS_SRC_ALPHA,
    }
    public enum GLTextureEnvMode : uint
    {
        Modulate = GL.GL_MODULATE,
        Decal = GL.GL_DECAL,
        Replace = GL.GL_REPLACE,
        Blend = GL.GL_BLEND,
        Add = GL.GL_ADD,
        Combine = GL.GL_COMBINE,
    }
    public enum GLTextureWrap : uint
    {
        Clamp = GL.GL_CLAMP,
        ClampToEdge = GL.GL_CLAMP_TO_EDGE,
        ClampToBorder = GL.GL_CLAMP_TO_BORDER,
        Repeat = GL.GL_REPEAT,
        MirroredRepeat = GL.GL_MIRRORED_REPEAT,
    }
    public enum GLTextureMagFilter : uint
    {
        Nearest = GL.GL_NEAREST,
        Linear = GL.GL_LINEAR,
    }
    public enum GLTextureMinFilter : uint
    {
        Nearest = GL.GL_NEAREST,
        NearestMipmapNearest = GL.GL_NEAREST_MIPMAP_NEAREST,
        NearestMipmapLinear = GL.GL_NEAREST_MIPMAP_LINEAR,
        Linear = GL.GL_LINEAR,
        LinearMipmapLinear = GL.GL_LINEAR_MIPMAP_LINEAR,
        LinearMipmapNearest = GL.GL_LINEAR_MIPMAP_NEAREST
    }
    public enum GLTextureCompareFunc : uint
    {
        LEqual = GL.GL_LEQUAL,
        GEqual = GL.GL_GEQUAL,
    }
    public enum GLTextureDepthMode : uint
    {
        Alpha = GL.GL_ALPHA,
        Luminance = GL.GL_LUMINANCE,
        Intensity = GL.GL_INTENSITY
    }
    public enum GLTextureCompareMode : uint
    {
        None = GL.GL_NONE,
        CompareRToTexuture = GL.GL_COMPARE_R_TO_TEXTURE,
    }
    public enum GLTextureParameter : uint
    {
        WrapS = GL.GL_TEXTURE_WRAP_S,
        WrapT = GL.GL_TEXTURE_WRAP_T,
        WrapR = GL.GL_TEXTURE_WRAP_R,
        MagFilter = GL.GL_TEXTURE_MAG_FILTER,
        MinFilter = GL.GL_TEXTURE_MIN_FILTER,
        BorderColor = GL.GL_TEXTURE_BORDER_COLOR,
        Priority = GL.GL_TEXTURE_PRIORITY,
        MinLod = GL.GL_TEXTURE_MIN_LOD,
        MaxLod = GL.GL_TEXTURE_MAX_LOD,
        BaseLeveL = GL.GL_TEXTURE_BASE_LEVEL,
        MaxLeveL = GL.GL_TEXTURE_MAX_LEVEL,
        DepthTextureMode =  GL.GL_DEPTH_TEXTURE_MODE,
        CompareMode =GL.GL_TEXTURE_COMPARE_MODE,
        CompareFunc = GL.GL_TEXTURE_COMPARE_FUNC,
        GenerateMipmap = GL.GL_GENERATE_MIPMAP
    }
    public enum GLTexLevelParameterTarget : uint
    {
        Texture1D = GL.GL_TEXTURE_1D,
        Texture2D = GL.GL_TEXTURE_2D,
        Texture3D = GL.GL_TEXTURE_3D,
        ProxyTexture1D = GL.GL_PROXY_TEXTURE_1D,
        ProxyTexture2D = GL.GL_PROXY_TEXTURE_2D,
        ProxyTexture3D = GL.GL_PROXY_TEXTURE_3D
    }
    public enum GLTexLevelParameter : uint
    {
        Width = GL.GL_TEXTURE_WIDTH,
        Height = GL.GL_TEXTURE_HEIGHT,
        Depth = GL.GL_TEXTURE_DEPTH,
        Border = GL.GL_TEXTURE_BORDER,
        InternalFormat = GL.GL_TEXTURE_INTERNAL_FORMAT,
        RedSize = GL.GL_TEXTURE_RED_SIZE,
        GreenSize = GL.GL_TEXTURE_GREEN_SIZE,
        BlueSize = GL.GL_TEXTURE_BLUE_SIZE,
        AlphaSize = GL.GL_TEXTURE_ALPHA_SIZE,
        LuminanceSize = GL.GL_TEXTURE_LUMINANCE_SIZE,
        IntensitySize = GL.GL_TEXTURE_INTENSITY_SIZE
    }
    //Texuture Generator
    public enum GLTexGenCoordTarget : uint
    {
        S = GL.GL_S,
        T = GL.GL_T,
        R = GL.GL_R,
        Q = GL.GL_Q
    }
    public enum GLTexGenMode : uint
    {
        GenMode = GL.GL_TEXTURE_GEN_MODE,
        ObjectPlane = GL.GL_OBJECT_PLANE,
        EyePlane = GL.GL_EYE_PLANE
    }
    public enum GLTexGenCommand : uint
    {
        ObjectLinear = GL.GL_OBJECT_LINEAR,
        EyeLinear = GL.GL_EYE_LINEAR,
        SphereMap = GL.GL_SPHERE_MAP,
        NormalMap = GL.GL_NORMAL_MAP
    }
    //chapter 10:
    public enum GLDrawBuffer : uint
    {
        Front  = GL.GL_FRONT,
        Back = GL.GL_BACK,
        Left = GL.GL_LEFT,
        Right = GL.GL_RIGHT,
        FrontLeft = GL.GL_FRONT_LEFT,
        FrontRight = GL.GL_FRONT_RIGHT,
        BackLeft = GL.GL_BACK_LEFT,
        BackRight = GL.GL_BACK_RIGHT,
        Aux0 = GL.GL_AUX0,
        FrontAndBack = GL.GL_FRONT_AND_BACK,
        None = GL.GL_NONE
    }
    public enum GLCompareFunc : uint
    {
        Never = GL.GL_NEVER,
        Always = GL.GL_ALWAYS,
        Less = GL.GL_LESS,
        LessOrEqual = GL.GL_LEQUAL,
        Equal = GL.GL_EQUAL,
        GreateOrEqual = GL.GL_GEQUAL,
        Greater = GL.GL_GREATER,
        NotEqual = GL.GL_NOTEQUAL
    }
    public enum GLStencilOp : uint
    {
        Keep = GL.GL_KEEP,
        Zero = GL.GL_ZERO,
        Replace = GL.GL_REPLACE,
        Incr = GL.GL_INCR,
        IncrWrap = GL.GL_INCR_WRAP ,
        Decr = GL.GL_DECR,
        DecrWrap = GL.GL_DECR_WRAP,
        Invert = GL.GL_INVERT
    }
    public enum GLLogicOp : uint
    {
        Clear = GL.GL_CLEAR,
        Copy = GL.GL_COPY,
        Noop = GL.GL_NOOP,
        Set = GL.GL_SET,
        CopyInverted = GL.GL_COPY_INVERTED,
        Invert = GL.GL_INVERT,
        AndReverse = GL.GL_AND_REVERSE,
        OrReverse = GL.GL_OR_REVERSE,
        And = GL.GL_AND,
        Or = GL.GL_OR,
        Nand = GL.GL_NAND,
        Nor = GL.GL_NOR,
        Xor = GL.GL_XOR,
        Equiv = GL.GL_EQUIV,
        AndInverted = GL.GL_AND_INVERTED,
        OrInverted = GL.GL_OR_INVERTED
    }
    //accumulation
    public enum enuGLAccumOp : uint
    {
        Accum = GL.GL_ACCUM,
        Load = GL.GL_LOAD,
        Return = GL.GL_RETURN,
        Add = GL.GL_ADD,
        Mult = GL.GL_MULT
    }
    public enum enuGLTexture : uint
    {
        Texture0  = GL.GL_TEXTURE0,
        Texture1 = GL.GL_TEXTURE1,
        Texture2 = GL.GL_TEXTURE2,
        Texture3 = GL.GL_TEXTURE2,
        Texture4 = GL.GL_TEXTURE3,
        Texture5 = GL.GL_TEXTURE4,
        Texture6 = GL.GL_TEXTURE5,
        Texture7 = GL.GL_TEXTURE6,
    }
    public enum GLStringFormat : uint
    {
        PolygonFont = WGL .WGL_FONT_POLYGONS,
        LinePolygon = WGL.WGL_FONT_LINES 
    }
    public enum GLFeedBackDataType : uint
    {
        _2D = GL.GL_2D,
        _3D = GL.GL_2D,
        _3D_Color = GL.GL_3D_COLOR,
        _3D_ColorTexture = GL.GL_3D_COLOR_TEXTURE,
        _4D_ColorTexture = GL.GL_4D_COLOR_TEXTURE,
    }
    public enum GLMap1Control : uint
    {
        Vertex3 = GL.GL_MAP1_VERTEX_3,
        Vertex4 = GL.GL_MAP1_VERTEX_4,
        Index = GL.GL_MAP1_INDEX,
        Color4 = GL.GL_MAP1_COLOR_4,
        Normal = GL.GL_MAP1_NORMAL,
        Coord1  = GL.GL_MAP1_TEXTURE_COORD_1,
        Coord2 = GL.GL_MAP1_TEXTURE_COORD_2,
        Coord3 = GL.GL_MAP1_TEXTURE_COORD_3,
        Coord4 = GL.GL_MAP1_TEXTURE_COORD_4,
    }
    public enum GLMap2Control : uint
    {
        Vertex3 = GL.GL_MAP2_VERTEX_3,
        Vertex4 = GL.GL_MAP2_VERTEX_4,
        Index = GL.GL_MAP2_INDEX,
        Color4 = GL.GL_MAP2_COLOR_4,
        Normal = GL.GL_MAP2_NORMAL,
        Coord1 = GL.GL_MAP2_TEXTURE_COORD_1,
        Coord2 = GL.GL_MAP2_TEXTURE_COORD_2,
        Coord3 = GL.GL_MAP2_TEXTURE_COORD_3,
        Coord4 = GL.GL_MAP2_TEXTURE_COORD_4,
    }
    public enum GLEvalMode1 : uint
    {
        Point = GL.GL_POINT,
        Line = GL.GL_LINE
    }
}

