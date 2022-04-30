

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ListViewAnimationModel.cs
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
file:ListViewAnimationModel.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.AnimationModel
{
    
using IGK.ICore;using IGK.OGLGame.Graphics;
    
    /// <summary>
    /// list view animation model
    /// </summary>
    public sealed class ListViewAnimationModel : AnimationModelBase, IDisposable, I3DModel
    {
        private const int DEF_WIDTH = 512;
        private const int DEF_HEIGHT = 512;
        Texture2D Face1;
        public override void Initialize(OGLGraphicsDevice device, Rectanglei view)
        {
            Bitmap cbmp = new Bitmap(DEF_WIDTH, DEF_HEIGHT);
            Face1 = Texture2D.Load(device, cbmp);
            cbmp.Dispose();
        }
        protected internal override void InitView(OGLGraphicsDevice Device, Rectanglei view)
        {          
            Device.Projection.MatrixMode = MatrixMode.Projection;
            Device.Projection.LoadIdentity();
            Device.Projection.SetOrtho2D(0, 500, 0, 500);
            Device.Projection.MatrixMode = MatrixMode.ModelView;
            Device.Projection.LoadIdentity();
        }
        public override void OrganiseTextures(OGLGraphicsDevice Device)
        {
        }
        public override void Render(OGLGraphicsDevice Device)
        {
            Device.Projection.PushMatrix();
            float x = 0;
            float y = 0;
            float w = 90;
            float h = 90;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    x = 5+ (i * 100);
                    y = 5 +(j * 100);
                    Device.DrawRectangle(Colorf.White, x, y, w, h);
                    Device.Begin(enuGraphicsPrimitives.Quads);
                    //face 5
                    Device.Capabilities.Texture2D = true;
                    this.Face1.Bind();
                    this.Face1.TextureEnvironmentMode = enuTextureEnvironmentMode.Replace;
                    Device.SetColor(Colorf.Black);
                    Device.SetTexCoord(0, 0); Device.SetVertex(0f, 0f);
                    Device.SetTexCoord(1, 0); Device.SetVertex(0.0f, 100f);
                    Device.SetTexCoord(1, 1); Device.SetVertex(100f, 100f);
                    Device.SetTexCoord(0, 1); Device.SetVertex(100.0f, 0);
                    Device.End();
                    Device.Capabilities.Texture2D = false;
                }
            }
            Device.Projection.PopMatrix();
        }
        public override void GoUp()
        {
        }
        public override void GoDown()
        {
        }
        public override void GoLeft()
        {
        }
        public override void GoRight()
        {
        }
    }
}

