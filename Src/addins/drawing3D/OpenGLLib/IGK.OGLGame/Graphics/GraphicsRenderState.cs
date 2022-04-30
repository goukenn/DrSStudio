

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsRenderState.cs
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
file:GraphicsRenderState.cs
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
using System.ComponentModel;
using System.Drawing;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;
    using IGK.GLLib;
    /// <summary>
    /// represent the graphics device render state
    /// </summary>
    public class GraphicsRenderState
    {
        private OGLGraphicsDevice m_graphicsDevice;
        [Browsable(false)]
        public OGLGraphicsDevice GraphicsDevice {
            get { return this.m_graphicsDevice; }
        }
        public float PointSize {
            get {
                return GraphicsDevice.GetFloatv<float>(GL.GL_POINT_SIZE);
            }
            set {
                GL.glPointSize (value);
            }
        }
        [Category("Buffer")]
        /// <summary>
        /// get or set the reading buffer
        /// </summary>
        public enuBufferMode ReadBuffer {
            get {
                return (enuBufferMode)GraphicsDevice .GetIntegerv <uint>(GL.GL_READ_BUFFER);
            }
            set {
                GL.glReadBuffer((uint)value);
            }
        }
        [Category("Buffer")]
        /// <summary>
        /// get or set the drawing buffer
        /// </summary>
        public enuBufferMode DrawBuffer
        {
            get
            {
                return (enuBufferMode)GraphicsDevice.GetIntegerv<uint>(GL.GL_DRAW_BUFFER);
            }
            set
            {
                GL.glDrawBuffer((uint)value);
            }
        }
      public float LineWidth { 
            get
            {
                return GraphicsDevice.GetFloatv<float>(GL.GL_LINE_WIDTH);
            }
            set {
                GL.glLineWidth (value);
            }
        }
        public ShadeModel ShadeModel
        {
            get {
                return (ShadeModel)GraphicsDevice.GetIntegerv<int>(GL.GL_SHADE_MODEL);
            }
            set {
                GL.glShadeModel((uint)value);
            }
        }
        /// <summary>
        /// get or set the front face
        /// </summary>
        public PolygonFrontFace CullFrontFace {
            get {
                return (PolygonFrontFace)GraphicsDevice.GetIntegerv<int>(GL.GL_FRONT_FACE);
            }
            set {
                GL.glFrontFace((uint)value);
            }
        }
        /// <summary>
        /// get or set the cull face
        /// </summary>
        public PolygonCullFace CullFaceMode
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<PolygonCullFace >(GL.GL_CULL_FACE_MODE);
            }
            set
            {
                GL.glCullFace((uint)value);
            }
        }
        [Category ("AlphaTest")]
        public enuAlphaTestFunc AlphaTestFunction {
            get {
                return (enuAlphaTestFunc)this.GraphicsDevice.GetIntegerv<enuAlphaTestFunc >(GL.GL_ALPHA_TEST_FUNC);
            }
            set {
                GL.glAlphaFunc((uint)value, this.AlphaReference);
            }
        }
        [Category("AlphaTest")]
        public float AlphaReference
        {
            get
            {
                return this.GraphicsDevice.GetFloatv<float>(GL.GL_ALPHA_TEST_REF);
            }
            set {
                GL.glAlphaFunc((uint)AlphaTestFunction, value);
            }
        }
        public ushort LineStipplePattern {
            get
            {
                return this.GraphicsDevice.GetIntegerv<ushort >(GL.GL_LINE_STIPPLE_PATTERN );
            }
            set {
                GL.glLineStipple(this.LineStippleFactor, value);
            }
        }
        public Rectanglei ScissorBox
        {
            get
            {
                return GraphicsDevice.GetIntegerv<Rectanglei>(GL.GL_SCISSOR_BOX);
            }
            set
            {
                GL.glScissor(value.X, value.Y, value.Width, value.Height);
            }
        }
        public int LineStippleFactor
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_LINE_STIPPLE_REPEAT);
            }
            set {
                GL.glLineStipple(value, this.LineStipplePattern);
            }
        }
        public void SetAlphaFunc(enuAlphaTestFunc function, float reference)
        {
            GL.glAlphaFunc((uint)function, reference);
        }
        internal GraphicsRenderState(OGLGraphicsDevice graphicDevice)
        {
            this.m_graphicsDevice = graphicDevice;
        }
        public void SetPolygonMode(PolygonCullFace face , PolygonFaceMode mode)
        {
            GL.glPolygonMode((uint)face, (uint)mode);
        }
        public void SetPolygonOffset(float factor , float unit)
        {
            GL.glPolygonOffset(factor, unit);
        }
        public void SetLineStipple(int factor, ushort pattern)
        {            
            GL.glLineStipple(factor, pattern);
        }
        public LogicOp LogicOpMode {
            get {
                return (LogicOp)GraphicsDevice.GetIntegerv<int>(GL.GL_LOGIC_OP_MODE);
            }
            set {
                this.SetLogicOp(value);
            }
        }
        public void SetLogicOp(LogicOp logicOp)
        {
            GL.glLogicOp((uint)logicOp);
        }
        public float ZoomX {
            get {
                return this.GraphicsDevice.GetFloatv<float>(GL.GL_ZOOM_X);
            }
            set{
                GL.glPixelZoom (value , ZoomY);
            }
        }
        public float ZoomY {
            get {
                return this.GraphicsDevice.GetFloatv <float>(GL.GL_ZOOM_Y); 
            }
            set {
                GL.glPixelZoom(ZoomX, value);
            }
        }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float RedBias  {            get { return               this.GraphicsDevice.GetFloatv<float>(GL.GL_RED_BIAS);             }            set {                GL.glPixelTransferf(GL.GL_RED_BIAS, value);            }        }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float GreenBias { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_GREEN_BIAS); } set { GL.glPixelTransferf(GL.GL_GREEN_BIAS, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float BlueBias { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_BLUE_BIAS); } set { GL.glPixelTransferf(GL.GL_BLUE_BIAS, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float AlphaBias { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_ALPHA_BIAS); } set { GL.glPixelTransferf(GL.GL_ALPHA_BIAS, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float RedScale { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_RED_SCALE); } set { GL.glPixelTransferf(GL.GL_RED_SCALE, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float GreenScale { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_GREEN_SCALE); } set { GL.glPixelTransferf(GL.GL_GREEN_SCALE, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float BlueScale { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_BLUE_SCALE); } set { GL.glPixelTransferf(GL.GL_BLUE_SCALE, value); } }
        [Category(GLConstant.PIXEL_TRANSFER_CAT)]
        public float AlphaScale { get { return this.GraphicsDevice.GetFloatv<float>(GL.GL_ALPHA_SCALE); } set { GL.glPixelTransferf(GL.GL_ALPHA_SCALE, value); } }
        #region MASK
        /// <summary>
        /// get or set the depth buffer write mask
        /// </summary>
        public bool  DepthBufferWriteMask
        {
            get {
                return GraphicsDevice.GetBooleanv<bool>(GL.GL_DEPTH_WRITEMASK);
            }
            set{
                GL.glDepthMask(value);            
            }
        }
        public void SetColorMask(bool r, bool g, bool b, bool a)
        {
            GL.glColorMask(r, g, b, a);
        }
        [Category ("Stencil")]
        public uint StencilMask
        {
            get {
                return GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_WRITEMASK);
            }
            set
            {
                GL.glStencilMask(value);
            }
        }
        [Category("Stencil")]
        public enuStencilFunc StencilFunc
        {
            get
            {
                return (enuStencilFunc)GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_FUNC);
            }
            set
            {
                this.SetStencilFunc((uint)value, this.StencilRef, this.StencilValueMask );
            }
        }
        public void SetStencilFunc(uint func, int @ref, uint value)
        {
            GL.glStencilFunc(func, @ref, value);
        }
        [Category("Stencil")]
        public int StencilRef
        {
            get
            {
                return GraphicsDevice.GetIntegerv<int>(GL.GL_STENCIL_REF );
            }
            set
            {
                this.SetStencilFunc((uint)this.StencilFunc,value , this.StencilValueMask  );
            }
        }
        [Category("Stencil")]
        public uint StencilValueMask
        {
            get
            {
                return GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_VALUE_MASK );
            }
            set
            {
                this.SetStencilFunc((uint)this.StencilFunc, this.StencilRef, value);
            }
        }
        [Category("Stencil")]
        public enuStencilOp StencilFail
        {
            get
            {
                return (enuStencilOp) GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_FAIL);
            }
            set
            {
                this.SetStencilOp(value, this.StencilDepthFail, this.StencilDepthPass);
            }
        }
        [Category("Stencil")]
        public enuStencilOp StencilDepthFail
        {
            get
            {
                return (enuStencilOp)GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_PASS_DEPTH_FAIL );
            }
            set
            {
                this.SetStencilOp(this.StencilFail , value, this.StencilDepthPass);
            }
        }
        [Category("Stencil")]
        public enuStencilOp StencilDepthPass
        {
            get
            {
                return (enuStencilOp)GraphicsDevice.GetIntegerv<uint>(GL.GL_STENCIL_PASS_DEPTH_PASS );
            }
            set
            {
                this.SetStencilOp(this.StencilFail, this.StencilDepthFail, value);
            }
        }
        public void SetStencilOp(enuStencilOp fail, enuStencilOp zfail, enuStencilOp zpass)
        {
            GL.glStencilOp((uint)fail,(uint) zfail, (uint)zpass);
        }
        public void SetIndexMask(uint i) {
            GL.glIndexMask(i);
        }
        #endregion
    }
}

