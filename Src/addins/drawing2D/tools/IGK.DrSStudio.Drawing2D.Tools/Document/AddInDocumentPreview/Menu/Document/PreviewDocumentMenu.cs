

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewDocumentMenu.cs
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
using IGK.ICore.Resources;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
    [IGKD2DDocumentMenuAttribute("PreviewDocument", 0xF001)]
    sealed class PreviewDocumentMenu : Core2DDrawingMenuBase 
    {
        protected override bool PerformAction()
        {
            PreviewCurrentDocument(this.CurrentSurface.CurrentDocument);
            return base.PerformAction();
        }

        private void PreviewCurrentDocument(ICore2DDrawingDocument core2DDrawingDocument)
        {
            UIPreviewDocument doc = new UIPreviewDocument();
            using (ICoreDialogForm dial = Workbench.CreateNewDialog(doc))
            {
                dial.Title = "title.PreviewDocument".R();
                dial.Size = new Size2i(700,600);
                doc.CurrentDocument = core2DDrawingDocument;
                doc.PreviewHeight = core2DDrawingDocument.Height  ;
                doc.PreviewWidth = core2DDrawingDocument.Width ;
                doc.Dock = System.Windows.Forms.DockStyle.Fill;
                dial.ShowDialog();
            }
        }
    }
}
