

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GkdsFilePreviewHandler.cs
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
file:GkdsFilePreviewHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;
namespace IGK.GkdsFilePreviewHandler
{
    using IGK.ICore;using IGK.PreviewHandlerLib;
    using IGK.PreviewHandlerLib.WinUI;
    using IGK.GkdsFilePreviewHandler.WinUI;
    [PreviewHandler("IGKDEV DrSStudio Preview Handler", ".gkds", "{0A5B9F49-5102-48CB-A0CE-9D902CF1320A}")]
    [ProgId("IGKDEV.GKDSFilePreviewHandler1")]
    [Guid("3CEE831D-D01E-4B5C-A5DB-E49055356CCE")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public sealed class GkdsFilePreviewHandler : FilePreviewHandlerBase 
    {
        protected override PreviewHandlerControl CreatePreviewHandlerControl()
        {
            return new XGkdsFilePreviewHandler();
        }
        protected override void Load(PreviewHandlerControl c)
        {
            base.Load(c);
        }
    }
}

