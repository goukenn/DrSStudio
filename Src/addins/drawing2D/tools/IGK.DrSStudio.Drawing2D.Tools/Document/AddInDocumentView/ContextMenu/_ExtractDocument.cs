

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractDocument.cs
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
using IGK.DrSStudio.ContextMenu;
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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    
    [DrSStudioContextMenu ("Document.ExtractDocumentBlock", 130, SeparatorBefore=true)]
    sealed class _ExtractDocument : IGKD2DDrawingContextMenuBase  
    {
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
        protected override bool PerformAction()
        {
            
                DocumentBlockElement element = this.CurrentSurface.CurrentLayer.SelectedElements[0] as DocumentBlockElement;
                ICore2DDrawingLayer v_l =null;
             foreach(ICore2DDrawingLayer l in  element .Document .Layers )
             {
                 v_l = l.Clone () as ICore2DDrawingLayer ;
                 this.CurrentSurface.CurrentDocument .Layers.Add (v_l );
             }
            
            return true;
        }
        protected override bool IsVisible()
        {
            bool v = (this.CurrentSurface != null) && (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1) &&
                (this.CurrentSurface.CurrentLayer.SelectedElements[0] is DocumentBlockElement);
            return base.IsVisible() && v;
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible();
        }
    }
}
