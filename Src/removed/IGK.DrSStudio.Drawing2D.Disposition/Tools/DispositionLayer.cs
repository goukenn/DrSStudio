

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DispositionLayer.cs
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
file:DispositionLayer.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Disposition.Tools
{
    sealed partial class DispositionTools
    {
        class DispositionLayer : IGK.DrSStudio.Drawing2D.WinUI.IXDrawing2DSurfaceLayer
        {
            DispositionTools m_tool;
            public DispositionLayer(DispositionTools tool)
            {
                this.m_tool = tool;
            }
            public void Draw(WinUI.ICore2DDrawingSurface surface, Graphics g)
            {
                if (!DispositionTools.Instance.ShowDisposition || (surface.CurrentLayer.SelectedElements.Count != 1))
                    return;
                //render disposition layer
                Rectanglef v_rc = surface.GetScreenBound(surface.CurrentLayer.SelectedElements[0].GetBound());
                Vector2f v_pt = surface.GetScreenLocation(Vector2f.Zero);
                Vector2f v_bottom = v_rc.BottomRight;
                Vector2f v_docbottom = surface.GetScreenLocation(new Vector2f(surface.CurrentDocument.Width, surface.CurrentDocument.Height));
                if (v_rc.Y.IsInRangeOf(v_pt.Y, v_docbottom.Y) && v_rc.X.IsInRangeOf(v_pt.X, v_docbottom.X))
                    g.DrawLine(Pens.BlueViolet, v_rc.X, v_pt.Y, v_rc.X, v_rc.Y);
                if (v_rc.Y.IsInRangeOf(v_pt.Y, v_docbottom.Y) && v_rc.X.IsInRangeOf(v_pt.X, v_docbottom.X))
                    g.DrawLine(Pens.BlueViolet, v_pt.X, v_rc.Y, v_rc.X, v_rc.Y);
                if (v_bottom.Y.IsInRangeOf(v_pt.Y, v_docbottom.Y) && v_bottom.X.IsInRangeOf(v_pt.X, v_docbottom.X))
                    g.DrawLine(Pens.BlueViolet, v_bottom.X, v_bottom.Y, v_bottom.X, v_docbottom.Y);
                if (v_bottom.X.IsInRangeOf(v_pt.X, v_docbottom.X) && v_bottom.Y.IsInRangeOf(v_pt.Y, v_docbottom.Y))
                    g.DrawLine(Pens.BlueViolet, v_bottom.X, v_bottom.Y, v_docbottom.X, v_bottom.Y);
                //g.DrawRectangle(Pens.BlueViolet, v_rc.X, v_rc.Y, v_rc.Width ,v_rc.Height );
                //g.DrawLine(Pens.BlueViolet, v_rc.BottomRight.X  , v_rc.BottomRight.Y ,  v_pt.Y, v_rc.X, v_rc.Y);
                //g.DrawLine(Pens.BlueViolet, v_pt.X, v_rc.Y, v_rc.X, v_rc.Y);
            }
        }
    }
}

