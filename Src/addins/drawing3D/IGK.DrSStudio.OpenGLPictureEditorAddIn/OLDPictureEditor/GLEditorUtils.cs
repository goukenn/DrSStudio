

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorUtils.cs
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
file:GLEditorUtils.cs
*/

using IGK.ICore;using IGK.DrSStudio.GLPictureEditorAddIn.Effect;
using IGK.OGLGame.Graphics;
using IGK.OGLGame.Graphics.Shaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
namespace IGK.DrSStudio.GLPictureEditorAddIn
{
    class GLEditorUtils
    {
        public static GLShaderEffect CompileShader(OGLGraphicsDevice device, string source, enuShaderType shadertype, out string[] error)
        {
            error = null;
            if (device == null)
                return null;
            device.MakeCurrent();
            ShaderProgram program = ShaderCompiler.CreateProgram(device);
            Shader shader = null;
           shader = ShaderCompiler.CreateShader(device, shadertype);
            ShaderCompiler.AttachShaderSource(shader, source);
            if (ShaderCompiler.CompileShader(shader) == false)
            {
                error = ShaderCompiler.GetShaderInfoLog(shader).Split('\n');
#if DEBUG
                MessageBox.Show(ShaderCompiler.GetShaderInfoLog(shader), "Error LOG");
#endif
                shader.Dispose();
            }
            else
            {
                program.AttachShader(shader);
#if DEBUG
                MessageBox.Show(ShaderCompiler.GetShaderInfoLog(shader), "No Error");
#endif
                GLShaderEffect v_effect = new GLShaderEffect();
                v_effect.Program = program;
                return v_effect;
                //this.CurrentSurface.Effects.Add(v_effect);
            }
            return null;
        }
    }
}

