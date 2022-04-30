

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CVSelectionFrame.cs
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
file:CVSelectionFrame.cs
*/

using IGK.ICore; using IGK.ICore.Drawing2D; using IGK.DrSStudio.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio
{
    class CVSelectionFrame : ICore2DDrawingFrameRenderer
    {
        CVSurface surface;
        public CVSelectionFrame(CVSurface surface):base()
        {
            this.surface = surface;
        }
        public void Render(ICoreGraphics device)
        {
            //public override void RenderSelection(Graphics graphics, ICore2DDrawingSurface surface)
            //{
            //    base.RenderSelection(graphics, surface);
            //    CoreUnit unit = "3 mm";
            //    float z = ((ICoreUnitPixel)unit).Value;
            //    Rectanglef v_r = this.Bounds;
            //    //Rectanglef vc = v_r;
            //    //vc.Inflate(-z, -z);
            //    v_r.Inflate(-z, -z);
            //    v_r = surface.GetScreenBound (v_r);
            //    Pen v_pen = CoreBrushRegister.GetPen(Color.Black);
            //    v_pen.DashStyle = DashStyle.Dash;
            //    graphics.DrawRectangle(v_pen, v_r.X, v_r.Y, v_r.Width, v_r.Height);
            //    //restaure dash style
            //    v_pen.DashStyle = DashStyle.Solid;
            //}  
        }
    }
}

