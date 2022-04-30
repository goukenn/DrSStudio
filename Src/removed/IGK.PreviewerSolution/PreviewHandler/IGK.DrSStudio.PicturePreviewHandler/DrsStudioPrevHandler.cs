

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrsStudioPrevHandler.cs
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
file:DrsStudioPrevHandler.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections;
using System.Configuration;
using System.Configuration.Install;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.PicturePreviewHandler
{
    using IGK.ICore;using IGK.PrevHandlerLib;
    using IGK.DrSStudio.PicturePreviewHandler.WinUI;
    [PreviewHandler("IGKDEV DrSStudio Preview Handler", ".gkds;", "{C0F06CA4-24E7-4068-9DB9-FB5CEF3BFAD1}")]
    [ProgId("IGKDEV.GKDSFilePreviewHandler")]
    [Guid("AF34D5AF-9C99-4768-93C9-B35B8CF8210C")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class DrsStudioPrevHandler : IGK.PrevHandlerLib.FilePreviewHandlerBase
    {
        public DrsStudioPrevHandler()
        {
        }
        protected override PreviewHandlerControl CreatePreviewHandlerControl()
        {
            return new XPreviewHandlerControl ();
        }
    }
}

