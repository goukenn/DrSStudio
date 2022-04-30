

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingDocumentManagerSurface.cs
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
file:ICoreWorkingDocumentManagerSurface.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent surface that host one or multiple document
    /// </summary>
    public interface ICoreWorkingDocumentManagerSurface : ICoreWorkingSurface        
    {
        ICoreWorkingDocument CurrentDocument { get; set; }
        event CoreWorkingDocumentChangedEventHandler CurrentDocumentChanged;
    }
}

