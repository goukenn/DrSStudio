

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsLight.cs
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
file:GraphicsLight.cs
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
using System.ComponentModel ;
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    using IGK.OGLGame.Math;
    /// <summary>
    /// represent a graphics light
    /// </summary>
    public class GraphicsLight
    {
        private uint m_id;
        private OGLGraphicsDevice m_graphicsDevice;
        public string Name {
            get {
                return "LIGHT_" + (this.m_id - GL.GL_LIGHT0);
            }
        }
        [Browsable(false)]
        [Description("the graphics device")]
        public OGLGraphicsDevice GraphicsDevice {
            get {
                return this.m_graphicsDevice;
            }
        }
        internal GraphicsLight(OGLGraphicsDevice device, uint id)
        {
            this.m_id = id;
            this.m_graphicsDevice = device;
        }
        public override string ToString()
        {
            return this.Name;
        }
        public bool Enabled {
            get {
                return GL.glIsEnabled(this.m_id);
            }
            set {
                if (value)
                {
                    GL.glEnable(this.m_id);
                }
                else
                    GL.glDisable(this.m_id);
            }
        }
        public Colorf Diffuse {
            get {
                return (Colorf)GetLightfv(this.m_id, GL.GL_DIFFUSE, typeof(Colorf));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_DIFFUSE, new float[] { value.R, value.G, value.B, value.A });
            }
        }
        public Colorf Ambient
        {
            get
            {
                return (Colorf)GetLightfv(this.m_id, GL.GL_AMBIENT, typeof(Colorf));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_AMBIENT, new float[] { value.R, value.G, value.B, value.A });
            }
        }
        public Colorf Specular
        {
            get
            {
                return (Colorf)GetLightfv(this.m_id, GL.GL_SPECULAR, typeof(Colorf));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_SPECULAR, new float[] { value.R, value.G, value.B, value.A });
            }
        }
        public Vector4f  Position
        {
            get
            {
                GL.glPushMatrix();
                GL.glLoadIdentity();
                Vector4f pos = (Vector4f)GetLightfv(this.m_id, GL.GL_POSITION, typeof(Vector4f));
                GL.glPopMatrix();
                return pos;
            }
            set
            {
                //GL.glPushMatrix();
                //GL.glLoadIdentity();
                SetLightfv(this.m_id, GL.GL_POSITION, new float[]{ value.X, value.Y , value.Z , value.W });                
                //GL.glPopMatrix();
            }
        }
        public Vector3f SpotDirection
        {
            get
            {
                MatrixMode v_mat = this.GraphicsDevice.Projection.MatrixMode;
                this.GraphicsDevice.Projection.MatrixMode = MatrixMode.Projection;
                GL.glPushMatrix();
                GL.glLoadIdentity();
                Vector3f v_pos = (Vector3f)GetLightfv(this.m_id, GL.GL_SPOT_DIRECTION, typeof(Vector3f));
                GL.glPopMatrix();
                this.GraphicsDevice.Projection.MatrixMode = v_mat;
                return v_pos;
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_SPOT_DIRECTION, new float[] { value.X, value.Y, value.Z});
            }
        }
        public float SpotExponent
        {
            get
            {
                return (float)GetLightfv(this.m_id, GL.GL_SPOT_EXPONENT, typeof(float));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_SPOT_EXPONENT,new float[]{ value});
            }
        }
        public float SpotCutOff
        {
            get
            {
                return (float)GetLightfv(this.m_id, GL.GL_SPOT_CUTOFF, typeof(float));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_SPOT_CUTOFF, new float[]{value});
            }
        }
        public float LinearAttenuation
        {
            get
            {
                return (float)GetLightfv(this.m_id, GL.GL_LINEAR_ATTENUATION, typeof(float));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_LINEAR_ATTENUATION ,new float[]{ value});
            }
        }
        /// <summary>
        /// get or set the constant attenuation
        /// </summary>
        public float ConstantAttenuation
        {
            get
            {
                return (float)GetLightfv(this.m_id, GL.GL_CONSTANT_ATTENUATION, typeof(float));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_CONSTANT_ATTENUATION, new float[]{value});
            }
        }
        /// <summary>
        /// get or set the quadratic attenuation
        /// </summary>
        public float QuadraticAttenuation
        {
            get
            {
                return (float)GetLightfv(this.m_id, GL.GL_QUADRATIC_ATTENUATION , typeof(float));
            }
            set
            {
                SetLightfv(this.m_id, GL.GL_QUADRATIC_ATTENUATION, new float[]{value});
            }
        }
        static object GetLightfv(uint id, uint param , Type type)
        {
            //IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf (type));
            float[] v_t = new float[Marshal.SizeOf(type) / Marshal.SizeOf(typeof(float))];
            GL.glGetLightfv(id, param, v_t );
            Object o = Marshal.PtrToStructure(Marshal.UnsafeAddrOfPinnedArrayElement (v_t,0), type);
            //Marshal.FreeCoTaskMem(alloc);
            return o;
        }
        static void SetLightfv(uint id, uint param, float[] value)
        {
            GL.glLightfv(id, param, value );            
        }
    }
}

