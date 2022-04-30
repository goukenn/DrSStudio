

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GraphicsMaterial.cs
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
file:GraphicsMaterial.cs
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
namespace IGK.OGLGame.Graphics
{
    
using IGK.ICore;using IGK.GLLib;
    using IGK.OGLGame.Math;
    using IGK.OGLGame.WinUI;
    /// <summary>
    /// represent the graphic material 
    /// </summary>
    public class GraphicsMaterial : IGK.OGLGame.Graphics.IGraphicsMaterial
    {
          private OGLGraphicsDevice m_GraphicsDevice;
        [Browsable(false)]
          public OGLGraphicsDevice GraphicsDevice
          {
              get { return m_GraphicsDevice; }
              set { m_GraphicsDevice = value; }
          }
          public Colorf BackDiffuse
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Back, GL.GL_DIFFUSE );
              }
              set
              {
                  SetDiffuse (MaterialFace.Back, value);
              }
          }
          public Colorf BackAmbient
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Back, GL.GL_AMBIENT );
              }
              set
              {
                  SetAmbient(MaterialFace.Back, value);
              }
          }
          public Colorf BackSpecular {
              get {
                  return GetMaterialColor(MaterialFace.Back, GL.GL_SPECULAR); 
              }
              set {
                  SetSpecular(MaterialFace.Back, value);
              }
          }
          public Colorf BackEmission
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Back, GL.GL_EMISSION);
              }
              set
              {
                  SetEmission(MaterialFace.Back, value);
              }
          }
          public float BackShininess
          {
              get
              {
                  return GetMaterialFloat(MaterialFace.Back, GL.GL_SHININESS );
              }
              set
              {
                  SetShininess (MaterialFace.Back, value);
              }
          }
          public Colorf FrontDiffuse
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Front, GL.GL_DIFFUSE);
              }
              set
              {
                  SetDiffuse(MaterialFace.Front, value);
              }
          }
          public Colorf FrontAmbient
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Front, GL.GL_AMBIENT);
              }
              set
              {
                  SetAmbient(MaterialFace.Front, value);
              }
          }
          public Colorf FrontSpecular
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Front, GL.GL_SPECULAR);
              }
              set
              {
                  SetSpecular(MaterialFace.Front, value);
              }
          }
          public Colorf FrontEmission
          {
              get
              {
                  return GetMaterialColor(MaterialFace.Front, GL.GL_EMISSION);
              }
              set
              {
                  SetEmission(MaterialFace.Front, value);
              }
          }
          public float FrontShininess
          {
              get
              {
                  return GetMaterialFloat(MaterialFace.Front, GL.GL_SHININESS);
              }
              set
              {
                  SetShininess(MaterialFace.Front, value);
              }
          }
          internal GraphicsMaterial(OGLGraphicsDevice graphicsDevice)
          {
              this.m_GraphicsDevice = graphicsDevice;
          }
          public static void SetSpecular(MaterialFace material, Colorf value)
          {
              SetMaterial(material, GL.GL_SPECULAR, new float[] { value.R, value.G, value.B, value.A });
          }
          public static void SetShininess(MaterialFace material, float value)
          {
              SetMaterial(material, GL.GL_SHININESS, new float[]{value});
          }
          public static void SetDiffuse(MaterialFace material, Colorf value)
          {
              SetMaterial(material, GL.GL_DIFFUSE, new float[] { value.R, value.G, value.B, value.A });
          }
          public static void SetEmission(MaterialFace material, Colorf value)
          {
              SetMaterial(material, GL.GL_EMISSION, new float[] { value.R, value.G, value.B, value.A });
          }
          public static void SetAmbientAndDiffuse(MaterialFace material, float value)
          {
              SetMaterial(material, GL.GL_AMBIENT_AND_DIFFUSE, new float[]{value});
          }
          public static void SetAmbient(MaterialFace material, Colorf value)
          {
              SetMaterial(material, GL.GL_AMBIENT, new float[] { value.R, value.G, value.B, value.A  });
          }
          public static void SetColorIndex(MaterialFace material, Vector3f value)
          {
              SetMaterial(material, GL.GL_COLOR_INDEX, new float []{ value.X , value.Y , value.Z });
          }
          static void SetMaterial(MaterialFace material, uint type, float[] value)
          {
              GL.glMaterialfv((uint)material, type, value );
          }
          static Colorf GetMaterialColor(MaterialFace material, uint type)
          {
              //IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(Color)));
              float[] t = new float[4];
              //GL.glGetMaterialfv ((uint)material, type, t);
              Colorf cl = new Colorf(t[0], t[1], t[2], t[3]);// Marshal.PtrToStructure(alloc, typeof(Color));
              //Marshal.FreeCoTaskMem(alloc);
              return cl;
          }
          private float GetMaterialFloat(MaterialFace material, uint type)
          {
              IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(float)));
//              GL.glGetMaterialfv ((uint)material, type, alloc);
              float cl = (float)Marshal.PtrToStructure(alloc, typeof(float));
              Marshal.FreeCoTaskMem(alloc);
              return cl;
          }
    }
}

