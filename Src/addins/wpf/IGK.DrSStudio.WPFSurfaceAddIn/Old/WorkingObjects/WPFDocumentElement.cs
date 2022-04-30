

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFDocumentElement.cs
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
file:WPFDocumentElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [WPFElement ("DocumentElement", null, IsVisible=false)]
    sealed class WPFDocumentElement : WPFLayeredElement 
    {
        public WPFDocumentElement()
        {
        }
        protected override void InitPath()
        {
        }
        public static WPFDocumentElement Create(System.Windows.DependencyObject obj)
        {
            if (obj == null) return null;
            WPFDocumentElement el = new WPFDocumentElement();
            el.Shape = obj;
            return el;
        }
    }
}

