

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDocumentMakeBackgroundTransparentMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDocumentMakeBackgroundTransparent.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
       [IGKD2DDocumentMenuAttribute(
           "MakeBackgroundTransparent", 1,
        IsShortcutMenuChild = false,
        ImageKey=CoreImageKeys.MENU_DASH_GKDS)]
    sealed class IGKD2DDocumentMakeBackgroundTransparent : IGKD2DDrawingDocumentMenuBase
    {
           protected override bool PerformAction()
           {
               this.CurrentDocument.BackgroundTransparent = !this.CurrentDocument.BackgroundTransparent;
               return true;
           }
           protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
           {
               base.RegisterDocumentEvent(document);
               this.MenuItem.Checked = document.BackgroundTransparent;
               document.PropertyChanged += document_PropertyChanged;
           }

           void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
           {
               this.MenuItem.Checked = ((ICore2DDrawingDocument) o).BackgroundTransparent;
           }
           protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
           {
               document.PropertyChanged -= document_PropertyChanged;
               base.UnRegisterDocumentEvent(document);
           }
    }
}

