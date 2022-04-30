

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFDocumentEventArgs.cs
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
file:WPFDocumentEventArgs.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects ;
    public delegate void WPFDocumentEventHandler (object sender, WPFDocumentEventArgs e);
    /// <summary>
    /// reprsent the document event args
    /// </summary>
    public class WPFDocumentEventArgs : CoreDocumentEventArgs 
    {
        public new WPFDocument Document { get { return base.Document as WPFDocument; } }
        public WPFDocumentEventArgs(WPFDocument document):base(document)
        {
        }
    }
}

