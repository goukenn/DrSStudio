

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MultilineElement.cs
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
file:MultilineElement.cs
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
using System; 
using IGK.ICore;  using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.Standard
{

    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D;

    [Core2DDrawingStandardElement(
        "Multilines",
        typeof(Mecanism),
        ImageKey=CoreImageKeys.DE_LINES_GKDS,
        Keys=enuKeys.M )]
    public sealed class MultilineElement : CustomPolygonElement 
    {
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {

            path.Reset();
            if (Points == null)
                return;
            Vector2f[] v_tp = new Vector2f[Points.Length];
            //copyp array to point array
            for (int i = 0; i < v_tp.Length; i++)
            {
                v_tp[i] = Points[i];
            }
            if (v_tp.Length == 2)
                path.AddLine(v_tp[0], v_tp[1]);
            else
                path.AddLines(v_tp);
            path.FillMode = this.FillMode;
        }
        
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        new class Mecanism : CustomPolygonElement.Mecanism 
        {
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                base.UpdateDrawing(e);
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
            }
            protected override void UpdateMove(CoreMouseEventArgs e)
            {
                base.UpdateMove(e);
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

