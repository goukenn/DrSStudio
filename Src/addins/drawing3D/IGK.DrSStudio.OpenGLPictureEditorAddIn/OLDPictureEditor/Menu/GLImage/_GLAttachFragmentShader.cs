

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _GLAttachFragmentShader.cs
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
file:_GLAttachFragmentShader.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms ;
using System.IO;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Menu.GLImage
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;
    using IGK.OGLGame.Graphics.Shaders;
    using IGK.DrSStudio.GLPictureEditorAddIn.Effect;
    [IGK.DrSStudio.Menu.CoreMenu("GLImage.AttachFragment", 2)]
    class _GLAttachFragmentShader : GLEditorMenuBase
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "All Shaders | *.glvs; *.glfs;|Vertex Shader | *.glvs| Fragment Shader|*.glfs";
                ofd.FileName = "";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    this.CurrentSurface.Device.MakeCurrent();
                    ShaderProgram program = ShaderCompiler.CreateProgram(this.CurrentSurface.Device);
                    Shader shader = null;
                    string v_t = File.ReadAllText(ofd.FileName);
                    switch (Path.GetExtension(ofd.FileName).ToLower())
                    {
                        case ".gkvs"://vertext shader
                            shader = ShaderCompiler.CreateShader(this.CurrentSurface.Device, enuShaderType.VertexShader);
                            break;
                        case ".gkfs"://fragment shader
                            shader = ShaderCompiler.CreateShader(this.CurrentSurface.Device, enuShaderType.FragmentShader);
                            break;
                    }
                    ShaderCompiler.AttachShaderSource(shader, v_t);
                    if (ShaderCompiler.CompileShader(shader) == false)
                    {
                        string[] tag = ShaderCompiler.GetShaderInfoLog(shader).Split('\n');
                        MessageBox.Show(ShaderCompiler.GetShaderInfoLog(shader), "Error LOG");
                        shader.Dispose();
                    }
                    else
                    {
                        program.AttachShader(shader);
                        MessageBox.Show(ShaderCompiler.GetShaderInfoLog(shader), "No Error");
                        GLShaderEffect v_effect = new GLShaderEffect();
                        v_effect.Program = program;
                        this.CurrentSurface.Effects.Add(v_effect);
                    }
                }
            }
            return false;
        }
    }
}

