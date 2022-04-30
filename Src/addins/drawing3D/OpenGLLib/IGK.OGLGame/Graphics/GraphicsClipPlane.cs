

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsClipPlane.cs
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
file:GraphicsClipPlane.cs
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
    using IGK.OGLGame.Math;
    /// <summary>
    /// represent the clip plane class
    /// </summary>
    public class GraphicsClipPlane
    {
        private OGLGraphicsDevice m_graphicsDevice;
        private uint m_id;
        [System.ComponentModel.Browsable(false)]
        /// <summary>
        /// get the graphics device
        /// </summary>
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicsDevice;
            }
        }
        internal GraphicsClipPlane(OGLGraphicsDevice device, uint id)
        {
            m_graphicsDevice = device;
            m_id = id;
        }
        /// <summary>
        /// gets or set the clicp lpale enalape
        /// </summary>
        public bool Enabled {
            get {
                return GL.glIsEnabled(m_id);
            }
            set {
                if (value)
                    GL.glEnable(m_id);
                else
                    GL.glDisable(m_id);
            }
        }
        public Vector4d Equation {
            get {
                return this.GraphicsDevice.GetFloatv<Vector4d>(this.m_id);
            }
            set{
                GL.glClipPlane (this.m_id ,new double []{value.X, value.Y,value.Z ,value.T });
            }
        }
        public override string ToString()
        {
            return "CLIP_PLANE_" + (this.m_id - GL.GL_CLIP_PLANE0);
        }
    }
}

