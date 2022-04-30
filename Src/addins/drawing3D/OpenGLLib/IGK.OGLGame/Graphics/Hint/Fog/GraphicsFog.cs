

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsFog.cs
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
file:GraphicsFog.cs
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
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    public class GraphicsFog
    {
        private OGLGraphicsDevice m_graphicsDevice;
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicsDevice;
            }
        }
        /// <summary>
        /// get or set if the fog is enabled
        /// </summary>
        public bool Enabled {
            get {
                return GraphicsDevice.Capabilities.Fog;
            }
            set {
                GraphicsDevice.Capabilities.Fog = value ;
            }
        }
        public GraphicsFog(OGLGraphicsDevice graphicsDevice) {
            this.m_graphicsDevice = graphicsDevice;
        }
        public FogMode FogMode {
            get {
                return this.GraphicsDevice.GetIntegerv<FogMode >(GL.GL_FOG_MODE);
            }
            set{
                GL.glFogi (GL.GL_FOG_MODE , (int) value);
            }
        }
        public float FogStart {
            get {
                return GraphicsDevice.GetFloatv<float>(GL.GL_FOG_START);
            }
            set{
                GL.glFogf (GL.GL_FOG_START , value);
            }
        }
        public float FogEnd {
            get {
                return GraphicsDevice.GetFloatv<float>(GL.GL_FOG_END);
            }
            set{
                GL.glFogf (GL.GL_FOG_END, value);
            }
        }
        public float FogDensity {
            get {
                return GraphicsDevice.GetFloatv<float>(GL.GL_FOG_DENSITY);
            }
            set{
                GL.glFogf (GL.GL_FOG_DENSITY , value);
            }
        }
        public Colorf FogColor {
            get {
                return GraphicsDevice.GetFloatv<Colorf>(GL.GL_FOG_COLOR);
            }
            set {
                GL.glFogfv(GL.GL_FOG_COLOR, new float[]{value.R, value.G, value.B , value.A });
            }
        }
        public int FogIndex {
            get {
                return this.GraphicsDevice.GetIntegerv<int>(GL.GL_FOG_INDEX);
            }
            set {
                GL.glFogi(GL.GL_FOG_INDEX, value);
            }
        }
        public FogCoordMode FogCoordSrc {
            get {
                return this.GraphicsDevice.GetIntegerv<FogCoordMode>(GL.GL_FOG_COORDINATE );
            }
            set {
                GL.glFogi(GL.GL_FOG_COORDINATE , (int)value);
            }
        }
        public void SetFogCoord(float f)
        {
            GL.glFogCoordf(f);
        }
    }
}

