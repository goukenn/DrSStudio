

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreControlRenderer.cs
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
file:WinCoreControlRenderer.cs
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
using System.Windows.Forms;
using System.Reflection;
namespace IGK.DrSStudio.WinUI
{
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.GraphicModels;
    using System.Drawing;
    using IGK.ICore.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Theme;
    /// <summary>
    /// represent the base color renderer
    /// </summary>
    public static class DrSStudioControlRenderer 
    {
        //.ctr init control renderer
        static DrSStudioControlRenderer()
        {            
            CoreRendererBase.InitRenderer(MethodInfo.GetCurrentMethod().DeclaringType);
        }
        public static void DrawDash(ICoreGraphics graphic, Rectanglei rectanglei)
        {            
            ICore2DDrawingDocument v_document = CoreResources.GetDocument(CoreImageKeys.DASH_GKDS);
            if (v_document != null)
            {
                Brush br = WinCoreBrushRegister.GetBrush(v_document);
                if (br!=null)
                graphic.FillRectangle(br, rectanglei.X, rectanglei.Y, rectanglei.Width, rectanglei.Height);
                
            }
        }
    }
}

