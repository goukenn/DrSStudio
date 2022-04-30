

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSelectionElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    [AndroidGroup("Selection", typeof (Mecanism))]
    class AndroidSelectionElement : AndroidToolElement
    {
        class Mecanism : AndroidToolMecanismBase
        {
            private const int ST_SELECTING = 0x100;

            public override void Render(ICoreGraphics g)
            {
                base.Render(g);
                switch (this.State)
                {
                    default:
                        break;
                    case ST_SELECTING:
                        Rectanglef rc = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                        rc = this.CurrentSurface.GetScreenBound(rc);
                        g.DrawRectangle(Colorf.Black,
                            rc.X,
                            rc.Y,
                            rc.Width,
                            rc.Height);
                        g.DrawRectangle(Colorf.White ,
                             1.0f, enuDashStyle.Dot,
                     rc.X,
                     rc.Y,
                     rc.Width,
                     rc.Height);
                        break;
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;

                switch (e.Button )
                {
                    case enuMouseButtons.Left:
                        this.State = ST_SELECTING;
                        break;
                    case enuMouseButtons.Middle:
                        break;
                    case enuMouseButtons.None:
                        break;
                    case enuMouseButtons.Right:
                        break;
                    case enuMouseButtons.XButton1:
                        break;
                    case enuMouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        this.EndPoint = e.FactorPoint;
                        this.Invalidate();
                        break;
                    case enuMouseButtons.Middle:
                        break;
                    case enuMouseButtons.None:
                        break;
                    case enuMouseButtons.Right:
                        break;
                    case enuMouseButtons.XButton1:
                        break;
                    case enuMouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        this.State = ST_NONE;
                        this.Invalidate();
                        break;
                    case enuMouseButtons.Middle:
                        break;
                    case enuMouseButtons.None:
                        break;
                    case enuMouseButtons.Right:
                        break;
                    case enuMouseButtons.XButton1:
                        break;
                    case enuMouseButtons.XButton2:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
