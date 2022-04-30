

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsHint.cs
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
file:GraphicsHint.cs
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
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    public  class GraphicsHint
    {
           private OGLGraphicsDevice m_graphicsDevice;
        public OGLGraphicsDevice GraphicsDevice { get { return m_graphicsDevice; } }
        internal GraphicsHint(OGLGraphicsDevice graphicsDevice)
        {
            this.m_graphicsDevice = graphicsDevice;
        }
           public Hint PointSmoothHint {
               get {
                   return GraphicsDevice.GetIntegerv<Hint>(GL.GL_POINT_SMOOTH_HINT);
               }
               set {
                   GL.glHint(GL.GL_POINT_SMOOTH_HINT, (uint)value);
               }
           }
           public Hint LineSmoothHint
           {
               get
               {
                   return (Hint)GraphicsDevice.GetIntegerv<Hint>(GL.GL_LINE_SMOOTH_HINT);
               }
               set
               {
                   GL.glHint(GL.GL_LINE_SMOOTH_HINT, (uint)value);
               }
           }
           public Hint PolygonSmoothHint
           {
               get
               {
                   return GraphicsDevice.GetIntegerv<Hint>(GL.GL_POLYGON_SMOOTH_HINT);
               }
               set
               {
                   GL.glHint(GL.GL_POLYGON_SMOOTH_HINT, (uint)value);
               }
           }
           public Hint FogHint
           {
               get
               {
                   return GraphicsDevice.GetIntegerv<Hint >(GL.GL_FOG_HINT);
               }
               set
               {
                   GL.glHint(GL.GL_FOG_HINT, (uint)value);
               }
           }
           public Hint PerspectiveCorrectionHint
           {
               get
               {
                   return (Hint)GraphicsDevice.GetIntegerv<Hint>(GL.GL_PERSPECTIVE_CORRECTION_HINT );
               }
               set
               {
                   GL.glHint(GL.GL_PERSPECTIVE_CORRECTION_HINT, (uint)value);
               }
           }
           public Hint GenerateMipmapHint
           {
               get
               {
                   return (Hint)GraphicsDevice.GetIntegerv<Hint>(GL.GL_GENERATE_MIPMAP_HINT );
               }
               set
               {
                   GL.glHint(GL.GL_GENERATE_MIPMAP_HINT, (uint)value);
               }
           }
           public Hint TextureCompressionHint
           {
               get
               {
                   return GraphicsDevice.GetIntegerv<Hint>(GL.GL_TEXTURE_COMPRESSION_HINT );
               }
               set
               {
                   GL.glHint(GL.GL_TEXTURE_COMPRESSION_HINT, (uint)value);
               }
           }
           public bool LineSmooth { get { return GraphicsDevice.Capabilities.LineSmooth; } set { GraphicsDevice.Capabilities.LineSmooth = value; } }
           public bool PolygonSmooth { get { return GraphicsDevice.Capabilities.PolygonSmooth; } set { this.GraphicsDevice.Capabilities.PolygonSmooth = value; } }
           public bool PointSmooth { get { return GraphicsDevice.Capabilities.PointSmooth; } set { this.GraphicsDevice.Capabilities.PointSmooth = value; } }
    }
}

