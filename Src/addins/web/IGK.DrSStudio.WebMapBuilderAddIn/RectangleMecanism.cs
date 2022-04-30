

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RectangleMecanism.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:RectangleMecanism.cs
*/
using IGK.DrSStudio.Web;
using IGK.DrSStudio.Web.WorkingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Mecanism
{
    class RectangleMecanism : WebMecanismBase<WebMapRectArea>
    {
        protected override void InitSnippetsLocation()
        {
            base.InitSnippetsLocation();
            Vector2f[] t = CoreMathOperation.GetResizePoints(this.Element.Bounds);
            this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
            this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
            this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
            this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
        }
        protected override void UpdateDrawing(CoreMouseEventArgs e)
        {
            this.Element.Bounds  =Rectanglei.Ceiling ( CoreMathOperation.GetBounds(this.StartPoint, e.FactorPoint));
            this.CurrentSurface.RefreshScene();
        }
        protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
        {
            base.UpdateSnippetEdit(e);
            Rectanglef v_rc = this.Element.Bounds;
            this.Snippet.Location = e.Location;
            switch (this.Snippet.Index)
            {
                case 3:
                    if (e.FactorPoint.X < v_rc.Right)
                    {
                        v_rc.Width = Math.Abs(v_rc.Right - e.FactorPoint.X);
                        v_rc.X = e.FactorPoint.X;
                    }
                    break;
                case 0:
                    if (e.FactorPoint.Y < v_rc.Bottom)
                    {
                        v_rc.Height = Math.Abs(v_rc.Bottom - e.FactorPoint.Y);
                        v_rc.Y = e.FactorPoint.Y;
                    }
                    break;
                case 2:
                    if (e.FactorPoint.Y > v_rc.Top)
                    {
                        v_rc.Height = Math.Abs(e.FactorPoint.Y - v_rc.Top);
                    }
                    break;
                case 1:
                    if (e.FactorPoint.X > v_rc.Left)
                    {
                        v_rc.Width = Math.Abs(e.FactorPoint.X - v_rc.Left);
                        //v_rc.X = e.FactorPoint.X;
                    }
                    break;
            }
            this.Element.Bounds  = Rectanglei.Ceiling (v_rc);
            this.CurrentSurface.RefreshScene();
        }
        protected override void GenerateSnippets()
        {
            base.GenerateSnippets();
            AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
            AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
            AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
            AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
        }
        protected override void OnMouseDown(CoreMouseEventArgs e)
        {
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(CoreMouseEventArgs e)
        {
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(CoreMouseEventArgs e)
        {
            base.OnMouseUp(e);
        }
    }
}

