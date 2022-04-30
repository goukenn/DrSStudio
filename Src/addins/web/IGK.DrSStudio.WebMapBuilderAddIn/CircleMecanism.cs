

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleMecanism.cs
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
file:CircleMecanism.cs
*/
using IGK.DrSStudio.Web;
using IGK.DrSStudio.Web.WorkingObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Mecanism
{
    /// <summary>
    /// represent a circle Mecanism
    /// </summary>
    class CircleMecanism : WebMecanismBase<WebMapCircle>
    {     
      
        protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
        {
            IWebMapCircleArea c = this.Element;
            switch (this.Snippet.Index)
            { 
                case 0:
                    c.Center = Vector2i.Round(e.FactorPoint);
                    break;
                case 1:
                  c.Radius = (int)CoreMathOperation.GetDistance(
                new Vector2d ( c.Center.X, c.Center.Y ),
                new Vector2d(e.FactorPoint .X , e.FactorPoint .Y )
                );
                    break;
            }
            this.CurrentSurface.RefreshScene();
        }
        protected override void BeginDrawing(CoreMouseEventArgs e)
        {

            IWebMapCircleArea c = this.Element;
            c.Center = Vector2i.Round(e.FactorPoint);
        }
        protected override void UpdateDrawing(CoreMouseEventArgs e)
        {
            IWebMapCircleArea c = this.Element;
            this.Element.Radius = (int)CoreMathOperation.GetDistance(
                new Vector2d ( c.Center.X, c.Center.Y ),
                new Vector2d(e.FactorPoint .X , e.FactorPoint .Y )
                );
        }
        protected override void InitSnippetsLocation()
        {
            WebMapCircle c = this.Element;
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(
                  new Vector2f ( c.Center.X +  (int)(this.Element.Radius * Math.Sqrt(2.0f) / 2.0f),
                      c.Center.Y +  (int)(this.Element.Radius * Math.Sqrt(2.0f) / 2.0f)
                      ));
            //center point
            RegSnippets[0].Location = CurrentSurface.GetScreenLocation(this.Element.Center);
        }
        protected override void GenerateSnippets()
        {
            base.GenerateSnippets();
            AddSnippet(CurrentSurface.CreateSnippet(this, 0, 0));
            AddSnippet(CurrentSurface.CreateSnippet(this, 1, 1));
        }
    }
}

