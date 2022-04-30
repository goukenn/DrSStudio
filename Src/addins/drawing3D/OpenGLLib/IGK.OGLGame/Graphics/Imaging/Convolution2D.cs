

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Convolution2D.cs
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
file:Convolution2D.cs
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
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime .InteropServices ;
namespace IGK.OGLGame.Graphics.Imaging
{
    
using IGK.ICore;using IGK.GLLib ;
    public class Convolution2D
    {
        static bool sm_init;
        public static bool Enabled{
            get{
                return GL.glIsEnabled(GL.GL_CONVOLUTION_2D );
            }
            set{
                if (value)
                    GL.glEnable (GL.GL_CONVOLUTION_2D );
                else 
                    GL.glDisable (GL.GL_CONVOLUTION_2D );
            }
        }
        static Convolution2D()
        {
        }
        public static void SetConvolutionFilter2D(enuGLConvolutionType type, int width, int height, float[] data)
        {
            if ((glConvolutionFilter2D == null))
            {
                if (sm_init)
                    return;
                if (GL.IsProcedureDefine("glConvolutionFilter2D"))
            {
                glConvolutionFilter2D =
                    GL.GetProcedure("glConvolutionFilter2D", typeof(glConvolutionFilter2DPROC))
                    as glConvolutionFilter2DPROC;
                glConvolutionFilter2D(
                    GL.GL_CONVOLUTION_2D, (uint)type, width, height,
                    GL.GL_LUMINANCE,
                    GL.GL_UNSIGNED_BYTE,
                    data);
            }
            else if (GL.IsProcedureDefine("glConvolutionFilter2DEXT"))
            {
                glConvolutionFilter2D =
                    GL.GetProcedure("glConvolutionFilter2DEXT", typeof(glConvolutionFilter2DPROC))
                    as glConvolutionFilter2DPROC;
                glConvolutionFilter2D(
                    GL.GL_CONVOLUTION_2D, (uint)type, width, height,
                    GL.GL_LUMINANCE,
                    GL.GL_UNSIGNED_BYTE,
                    data);
            }
                sm_init = true;
            }
            else {
                glConvolutionFilter2D(
                        GL.GL_CONVOLUTION_2D, (uint)type, width, height,
                        GL.GL_LUMINANCE,
                        GL.GL_UNSIGNED_BYTE,
                        data);
            }
        }
        delegate void glConvolutionFilter2DPROC(uint target, uint internalformat, int width, int height, uint format, uint type, float[] data);
        static glConvolutionFilter2DPROC glConvolutionFilter2D;
        //[DllImport("opengl32.dll")]
        //static extern void glConvolutionFilter2D(uint target, uint internalformat, int width, int height, uint format, uint type, float[] data);
    }
}

