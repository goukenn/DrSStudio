

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsBlending.cs
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
file:GraphicsBlending.cs
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
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// represent the graphic Blending property
    /// </summary>
    public class GraphicsBlending : IOGLBending
    {
        uint m_ConstBlendEquation;
        uint m_ConstBlendColor;
        delegate void PFNBlendColor(float r, float g, float b, float a);
        delegate void PFNBlendFunction(uint i, uint h);
        delegate void PFNBlendEquation(uint mode);
        private OGLGraphicsDevice m_graphicsDevice;
        private PFNBlendColor glBlENDCOLORFUNC;
        private PFNBlendFunction glBLENDFUNCTIONFUNC;
        private PFNBlendEquation glBLENDEQUATIONFUNC;
        [Browsable (false )]
        /// <summary>
        /// get the graphics device
        /// </summary>
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicsDevice;
            }
        }
        public bool Enabled
        {
            get {
                return this.m_graphicsDevice.Capabilities.Blend  ; }
            set {this.m_graphicsDevice.Capabilities.Blend = value; }
        }
        internal GraphicsBlending(OGLGraphicsDevice graphicsDevice)
        {
            this.m_graphicsDevice = graphicsDevice;
            if (GL.SupportMethod ("glBlendColor"))
            {
                glBlENDCOLORFUNC = GL.glBlendColor;
                m_ConstBlendColor = GL.GL_BLEND_COLOR;
            }
            else if (GL.SupportMethod ("glBlendColorEXT"))
            {
                glBlENDCOLORFUNC = GL.glBlendColorEXT;
                m_ConstBlendColor = GL.GL_BLEND_COLOR_EXT;
            }
            glBLENDFUNCTIONFUNC = GL.glBlendFunc;
            if (GL.SupportMethod ("glBlendEquation"))
            {
                glBLENDEQUATIONFUNC = GL.glBlendEquation;
                this.m_ConstBlendEquation = GL.GL_BLEND_EQUATION;
            }
            else if (GL.SupportMethod ("glBlendEquationEXT"))
            {
                glBLENDEQUATIONFUNC = GL.glBlendEquationEXT;
                this.m_ConstBlendEquation = GL.GL_BLEND_EQUATION_EXT;
            }
        }
        public bool IsBlendSupported {
            get {
                return GL.SupportExtension("GL_EXT_blend_color") ;
            }
        }
        public bool CanSetBlendColor { get { return this.glBlENDCOLORFUNC != null; } }
        public bool CanSetBlendFunc { get { return glBLENDFUNCTIONFUNC != null; } }
        public bool CanSetBlendEquation { get { return this.glBLENDEQUATIONFUNC != null; } }
        /// <summary>
        /// set blending color 
        /// </summary>
        /// <param name="color"></param>
        public void SetBlendColor(Colorf color)
        {
            if (glBlENDCOLORFUNC!=null)
             glBlENDCOLORFUNC(color.R, color.G, color.B, color.A);            
        }
        /// <summary>
        /// set the blending factor
        /// </summary>
        /// <param name="source">set the blend source factor : default is BlendFactor.One</param>
        /// <param name="destination">set the blendd destination factor: default is BlendFactor.Zero</param>
        public void SetBlendFunc(BlendFactor source, BlendFactor destination)
        {
            if (glBLENDFUNCTIONFUNC == null)
                return;
            if (destination == BlendFactor.SourceAlphaSaturate)
            {
              glBLENDFUNCTIONFUNC((uint)source, (uint)BlendFactor.One);
            }
            else
            {
              glBLENDFUNCTIONFUNC((uint)source, (uint)destination);
            }
        }
        public void SetBlendEquation(BlendEquation mode)
        {
            if (this.glBLENDEQUATIONFUNC !=null)
            this.glBLENDEQUATIONFUNC((uint)mode);
        }
        public BlendEquation BlendEquation {
            get {
                return this.GraphicsDevice.GetIntegerv <BlendEquation>(this.m_ConstBlendEquation);
            }
            set {
                SetBlendEquation(value);
            }
        }
        public Colorf BlendColor {
            get {
                return GraphicsDevice.GetFloatv<Colorf>(this.m_ConstBlendColor);
            }
            set {
                SetBlendColor(value);
            }
        }
        public BlendFactor BlendSource {
            get {
                return this.GraphicsDevice.GetIntegerv<BlendFactor >(GL.GL_BLEND_SRC );
            }
            set {
                SetBlendFunc( value, BlendDestination );
            }
        }
        public BlendFactor BlendDestination
        {
            get
            {
                return this.GraphicsDevice.GetIntegerv<BlendFactor >(GL.GL_BLEND_DST);
            }
            set {
                SetBlendFunc(BlendSource, value);
            }
        }
    }
}

