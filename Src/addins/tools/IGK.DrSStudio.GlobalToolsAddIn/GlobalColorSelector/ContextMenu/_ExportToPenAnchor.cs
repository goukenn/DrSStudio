

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExportToPenAnchor.cs
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
file:_ExportToPenAnchor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ContextMenu
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Drawing2D;
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinUI.Common;

    [DrSStudioContextMenu("Drawing2DExport.ToPenLineCap", 2)]
    class _ExportToPenAnchor : IGKD2DDrawingContextMenuBase
    {
        public _ExportToPenAnchor()
        {
            this.IsRootMenu = false;
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements .Count == 1)
            {
            PathElement path = this.CurrentSurface.CurrentLayer.SelectedElements[0] as PathElement;
                if (path == null)
                    return false ;
                GCSXSaveCustomLineCap clineCap = new GCSXSaveCustomLineCap ();
                clineCap.CaptionKey = "Custom.LineCap.caption";
            using (ICoreDialogForm frm = Workbench.CreateNewDialog(clineCap ))
            {
                frm.Title = CoreSystem.GetString(clineCap.CaptionKey);
                if (frm.ShowDialog() == enuDialogResult.OK)
                {
                    CorePenLineCap.Save(
                        path.GetPath (),
                        clineCap.CapName,
                        clineCap.DefaultLineCap ,
                        clineCap.CapInset 
                        ); 
                }
            }
            }
            return base.PerformAction();
        }
       
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible() &&
                  (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)&&                    
                 (this.CurrentSurface.CurrentLayer.SelectedElements[0] is PathElement);
        } 
    }
}

