

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLInvertEffect.cs
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
file:GLInvertEffect.cs
*/

using IGK.ICore;using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.GLPictureEditorAddIn.Effect
{
    public sealed class GLInvertEffect : GLEffectBase 
    {
        IGK.OGLGame.Graphics.GraphicsPixelTransfer m_PixelTransfer;
        public GLInvertEffect()
        {
            m_PixelTransfer = new GraphicsPixelTransfer();
            m_PixelTransfer.RedBias = 1;
            m_PixelTransfer.GreenBias = 1;
            m_PixelTransfer.BlueBias = 1;
            m_PixelTransfer.RedScale = -1;
            m_PixelTransfer.GreenScale = -1;
            m_PixelTransfer.BlueScale = -1;
        }
        public override void Bind(OGLGraphicsDevice graphicsDevice)
        {
            m_PixelTransfer.Bind(graphicsDevice);
        }
        public override void UnBind(OGLGraphicsDevice graphicsDevice)
        {
        }
    }
}

