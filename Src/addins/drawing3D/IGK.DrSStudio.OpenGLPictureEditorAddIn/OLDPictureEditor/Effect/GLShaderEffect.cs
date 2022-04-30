

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLShaderEffect.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GLShaderEffect.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Effect
{
    
using IGK.ICore;using IGK.OGLGame.Graphics.Shaders ;
    public class GLShaderEffect : GLEffectBase 
    {
        private ShaderProgram m_program;
        public ShaderProgram Program
        {
            get { return m_program; }
            set
            {
                if (m_program != value)
                {
                    m_program = value;
                }
            }
        }
        public GLShaderEffect()
        {
        }
        public override void Bind(IGK.OGLGame.Graphics.OGLGraphicsDevice graphicsDevice)
        {
            m_program.Link();
            m_program.UseIt();
        }
        public override void Dispose()
        {            
            m_program.Dispose();
        }
        public override void UnBind(OGLGame.Graphics.OGLGraphicsDevice graphicsDevice)
        {
            m_program.Free();
        }
    }
}

