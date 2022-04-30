

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:PreviewDocumentMenu.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
    [CoreMenu("Document.PreviewDocument", 0xF001)]
    class PreviewDocumentMenu : Core2DMenuBase
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
                dial.Caption = R.ngets("title.PreviewDocument");
                doc.CurrentDocument = core2DDrawingDocument;
                doc.PreviewHeight = core2DDrawingDocument.Height  ;
                doc.PreviewWidth = core2DDrawingDocument.Width ;
                doc.Dock = System.Windows.Forms.DockStyle.Fill;
                dial.ShowDialog();
            }
        }
    }
}

