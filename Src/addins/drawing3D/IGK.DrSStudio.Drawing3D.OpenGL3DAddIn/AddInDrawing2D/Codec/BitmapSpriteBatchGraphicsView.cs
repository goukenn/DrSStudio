

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BitmapSpriteBatchGraphicsView.cs
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
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.OGLGame;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing3D.OpenGL.Codec
{
    class BitmapSpriteBatchGraphicsView : IOGLGGraphicsView
    {
        private Bitmap bmp;

        public BitmapSpriteBatchGraphicsView(System.Drawing.Bitmap bmp)
        {           
            this.bmp = bmp;
        }

        public Rectanglei GetViewPort()
        {
            return new Rectanglei(0, 0, this.bmp.Width, this.bmp.Height);
        }
    }
}