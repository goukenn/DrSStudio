

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingSnipperLayerFrame.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingSnipperLayerFrame.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D.WinUI
{
    public class IGKD2DDrawingSnipperLayerFrame : IGKD2DAnimatedSnippetLayer, ICore2DDrawingFrameRenderer
    {
        private IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene m_scene;
        public IGKD2DDrawingSnipperLayerFrame(IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene iGKD2DDrawingScene):base( )
        {
            this.m_scene = iGKD2DDrawingScene;
        }
        public void Render(ICoreGraphics device)
        {
            foreach (var item in this.Snippets)
            {
                if (item.Visible )
                item.Render(device);
            }
        }
    }
}

