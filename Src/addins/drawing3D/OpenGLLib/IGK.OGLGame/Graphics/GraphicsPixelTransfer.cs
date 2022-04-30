

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsPixelTransfer.cs
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
file:GraphicsPixelTransfer.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// represent a group gl property for pixel transfert operation 
    /// </summary>
    public class GraphicsPixelTransfer : GLProperty 
    {
        public static readonly GLProperty glRedScaleProperty = GLPropertyRegister.Register("RedScale", 1.0f);
        public static readonly GLProperty glGreenScaleProperty = GLPropertyRegister.Register("GreenScale", 1.0f);
        public static readonly GLProperty glBlueScaleProperty = GLPropertyRegister.Register("BlueScale", 1.0f);
        public static readonly GLProperty glAlphaScaleProperty = GLPropertyRegister.Register("AlphaScale", 1.0f);
        public static readonly GLProperty glRedBiasProperty = GLPropertyRegister.Register("RedBias", 0.0f);
        public static readonly GLProperty glGreenBiasProperty = GLPropertyRegister.Register("GreenBias", 0.0f);
        public static readonly GLProperty glBlueBiasProperty = GLPropertyRegister.Register("BlueBias", 0.0f);
        public static readonly GLProperty glAlphaBiasProperty = GLPropertyRegister.Register("AlphaBias", 0.0f);
        public float RedScale
        {
            get { return (float) this.GetValue(glRedScaleProperty); }
            set
            {
                this. SetValue(glRedScaleProperty, value);
            }
        }
        public float GreenScale
        {
            get { return (float)this.GetValue(glGreenScaleProperty); }
            set
            {
                this. SetValue(glGreenScaleProperty, value);
            }
        }
        public float BlueScale
        {
            get { return (float)this.GetValue(glBlueScaleProperty); }
            set
            {
                this. SetValue(glBlueScaleProperty, value);
            }
        }
        public float AlphaScale
        {
            get { return (float)this.GetValue(glAlphaScaleProperty ); }
            set
            {
                this.SetValue(glAlphaScaleProperty , value);
            }
        }
        public float RedBias
        {
            get { return (float)this.GetValue(glRedBiasProperty); }
            set
            {
                this.SetValue(glRedBiasProperty, value);
            }
        }
        public float GreenBias
        {
            get { return (float)this.GetValue(glGreenBiasProperty); }
            set
            {
                this.SetValue(glGreenBiasProperty, value);
            }
        }
        public float BlueBias
        {
            get { return (float)this.GetValue(glBlueBiasProperty); }
            set
            {
                this.SetValue(glBlueBiasProperty , value);
            }
        }
        public float AlphaBias
        {
            get { return (float)this.GetValue(glAlphaBiasProperty ); }
            set
            {
                this.SetValue(glAlphaBiasProperty , value);
            }
        }
        public override void Bind(IGK.OGLGame.Graphics.OGLGraphicsDevice device)
        {
            IGK.GLLib.GL.glPixelTransferf(GL.GL_RED_SCALE, this.RedScale);            
            IGK.GLLib.GL.glPixelTransferf(GL.GL_GREEN_SCALE, this.GreenScale);
            IGK.GLLib.GL.glPixelTransferf(GL.GL_BLUE_SCALE, this.BlueScale);
            IGK.GLLib.GL.glPixelTransferf(GL.GL_ALPHA_SCALE, this.AlphaScale);
            IGK.GLLib.GL.glPixelTransferf(GL.GL_RED_BIAS , this.RedBias );
            IGK.GLLib.GL.glPixelTransferf(GL.GL_GREEN_BIAS , this.GreenBias);
            IGK.GLLib.GL.glPixelTransferf(GL.GL_BLUE_BIAS , this.BlueBias);
            IGK.GLLib.GL.glPixelTransferf(GL.GL_ALPHA_BIAS , this.AlphaBias);
        }
    }
}

