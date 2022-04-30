

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ReplaceColorInAllDocumentTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Menu.Tools
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.WinUI.Common;

    [DrSStudioMenu("Tools.Document.ReplaceColorInAllDocument", 0x100)]
    class ReplaceColorInAllDocument : Core2DDrawingMenuBase  
    {
        protected override bool PerformAction()
        {
            Colorf oldColor = this.CurrentSurface.CurrentColor;

            Colorf newColor = Colorf.Empty ;
            using (var clDial  = Workbench.CreateColorDialog ())
            {
                if (clDial.ShowDialog() == enuDialogResult.OK)
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
                foreach (ICoreBrushContainer  element in l.Elements)
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
