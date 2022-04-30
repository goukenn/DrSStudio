

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorPropertyInfo.cs
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
file:GLEditorPropertyInfo.cs
*/

using IGK.ICore;using IGK.OGLGame.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    class GLEditorPropertyInfo
    {
        private enuShaderType m_ShaderType;
        [DefaultValue (enuShaderType.FragmentShader )]
        [Category("Definition")]
        public enuShaderType ShaderType
        {
            get { return m_ShaderType; }
            set
            {
                if (m_ShaderType != value)
                {
                    m_ShaderType = value;
                }
            }
        }
        public GLEditorPropertyInfo()
        {
            this.m_ShaderType = enuShaderType.FragmentShader;
        }
    }
}

