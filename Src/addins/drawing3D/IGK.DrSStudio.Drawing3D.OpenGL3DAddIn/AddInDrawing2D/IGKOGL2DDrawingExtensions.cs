

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDrawingExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.OGLGame.Graphics;
using IGK.ICore.WinCore;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// add operation extension to utility
    /// </summary>
    public static class IGKOGL2DDrawingExtensions
    {
        public static byte[] RTextureData(this string data) {

          WinCoreBitmapData r =   WinCoreBitmapData.FromBitmap(CoreResources.GetBitmapResources(data).ToGdiBitmap(true));
             byte[] c = r.Data ;
             return c;
        }
        /// <summary>
        /// create a sprite font device from font
        /// </summary>
        /// <param name="font"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public static SpriteFont ToSpriteFont(this ICoreFont font, OGLGraphicsDevice device)
        {
            SpriteFont ft = SpriteFont.Create(device, font.FontName, font.FontSize, font.FontStyle, 100, 100);
            return ft;
        }
    }
}
