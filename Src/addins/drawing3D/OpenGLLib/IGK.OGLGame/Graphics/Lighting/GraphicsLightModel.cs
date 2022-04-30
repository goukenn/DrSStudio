

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsLightModel.cs
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
file:GraphicsLightModel.cs
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
using System.Runtime.InteropServices;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    /// <summary>
    /// represent the graphics light mode
    /// </summary>
    public class GraphicsLightModel
    {
        private OGLGraphicsDevice m_graphicsDevice;
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicsDevice;
            }
        }
        internal GraphicsLightModel(OGLGraphicsDevice device)        
        {
            this.m_graphicsDevice = device;
        }
        public Colorf Ambient {
            get { 
                return (Colorf)GetLigthModel (GL.GL_LIGHT_MODEL_AMBIENT , typeof(Colorf));
            }
            set {
                SetLightModel(GL.GL_LIGHT_MODEL_AMBIENT, value);
            }
        }
        public bool LocalViewer
        {
            get
            {
                return (bool)GetLigthModel(GL.GL_LIGHT_MODEL_LOCAL_VIEWER , typeof(bool));
            }
            set
            {
                SetLightModel(GL.GL_LIGHT_MODEL_LOCAL_VIEWER, value);
            }
        }
        public bool TwoSide
        {
            get
            {
                return (bool)GetLigthModel(GL.GL_LIGHT_MODEL_TWO_SIDE, typeof(bool));
            }
            set
            {
                SetLightModel(GL.GL_LIGHT_MODEL_TWO_SIDE, value);
            }
        }
        public LightModelColorControl ColorControl
        {
            get
            {
                return GraphicsDevice.GetIntegerv <LightModelColorControl>(GL.GL_LIGHT_MODEL_COLOR_CONTROL);
            }
            set
            {
                GL.glLightModeli(GL.GL_LIGHT_MODEL_COLOR_CONTROL, (int)value);            
            }
        }
        private static object GetLigthModel(uint target, Type type)
        {
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(type));
            //GL.glGetFloatv(target, alloc);
            Object o = Marshal.PtrToStructure(alloc, type);
            Marshal.FreeCoTaskMem(alloc);
            return o;
        }
        private static void SetLightModel(uint target, object value)
        {
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(value));
            Marshal.StructureToPtr(value, alloc, false);
            //GL.glLightModelfv(target, alloc);
            Marshal.FreeCoTaskMem(alloc);
        }
   }
}

