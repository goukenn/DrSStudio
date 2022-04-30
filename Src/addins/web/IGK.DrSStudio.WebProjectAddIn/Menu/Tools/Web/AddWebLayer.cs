

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AddWebLayer.cs
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
file:AddWebLayer.cs
*/
using IGK.DrSStudio.WebProjectAddIn.WorkingObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebProjectAddIn.Menu.Tools.Web
{
     [DrSStudioMenu("Tools.Web.AddWebLayer", 300, SeparatorBefore  = true )]
    sealed class AddWebLayer : Core2DDrawingMenuBase 
    {
         public AddWebLayer()
         {
         }
         //
         protected override bool PerformAction()
         {
             WebHtmlLayoutLayer c = new WebHtmlLayoutLayer();
             this.CurrentSurface.CurrentDocument.Layers.Add(c);
             this.CurrentSurface.CurrentDocument.CurrentLayer = c;
             return false;
         }
    }
}

