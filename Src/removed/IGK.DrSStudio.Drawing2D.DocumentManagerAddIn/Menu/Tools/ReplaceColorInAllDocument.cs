

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ReplaceColorInAllDocument.cs
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
file:ReplaceColorInAllDocument.cs
*/
using IGK.ICore;using IGK.DrSStudio.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Tools
{
    [CoreMenu("Tools.Document.ReplaceColorInAllDocument", 0x100)]
    class ReplaceColorInAllDocument : Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
            Colorf oldColor = this.CurrentSurface.CurrentColor;
            Colorf newColor = Colorf.Empty ;
            using (System.Windows.Forms.ColorDialog clDial = new System.Windows.Forms.ColorDialog())
            {
                if (clDial.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    newColor = clDial.Color;
                    foreach (ICore2DDrawingDocument d in this.CurrentSurface.Documents)
                    {
                        ReplaceColor(d, oldColor, newColor);
                    }
                    this.CurrentSurface.Invalidate();
                }
            }
            return base.PerformAction();
        }
        private void ReplaceColor(ICore2DDrawingDocument d, Colorf oldColor, Colorf newColor)
        {
            foreach (ICore2DDrawingLayer l in d.Layers)
            {
                foreach (ICore2DBrushOwner element in l.Elements)
                {
                    if (element == null) continue;
                    foreach (ICoreBrush b in element.GetBrushes())
                    {
                        b.ReplaceColor(oldColor, newColor);
                    }
                }
            }
        }
    }
}

