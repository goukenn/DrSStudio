

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CsvPreviewHandler.cs
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
file:CsvPreviewHandler.cs
*/
using IGK.ICore;using IGK.DrSStudio.PreviewHandler.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.PreviewHandler.Previews
{
    [PreviewHandler(
        Name = "DEMO CSV Preview Handler",
        Extension = ".csv", 
        Guid = "{8CF7761A-E923-470c-926E-8440C06FA8FE}")]
    [ProgId("IGKDEV.CsvPreviewHandler")]   
    [Guid("06E28B89-0C1E-4397-8196-5D64F863F6F6")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class CsvPreviewHandler : StreamPreviewHandlerBase
    {
        protected override IPreviewHandlerControl CreatePreviewHandlerControl()
        {
            return (IPreviewHandlerControl)new CsvPreviewHandler();
        }
        protected override void Load(IPreviewHandlerControl c)
        {
            throw new NotImplementedException();
        }
        class CsvPreviewHandlerControl : PreviewHandlerControl
        {
        }
    }
}

