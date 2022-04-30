

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportToHtmlDocument.cs
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
file:ExportToHtmlDocument.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.DrSStudio.Drawing2D.Menu;
namespace IGK.DrSStudio.WebAddIn.Menu.Tools.Web
{
    [DrSStudioMenu("Tools.Web.ExportToHtmlDocument", 0x200)]
    class WebExportToHtmlDocumentMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = $"{"msg.exportToHtmlFile".R()} | *.html";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    WebUtils.ExportToHtmlDocument(sfd.FileName, CurrentSurface.Documents.ToArray());
                    System.Diagnostics.Process.Start(sfd.FileName);
                    return true;
                }
            }
            return false;
        }
    }
}

