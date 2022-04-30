

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
using System.Windows.Forms;
using System.Text;
using System.IO;
using IGK.ICore.IO;
namespace IGK.DrSStudio.WebProjectAddIn.Menu.File
{
    [DrSStudioMenu("File.ExportTo.WebDocument", 300)]
    class ExportToHtmlDocument : Core2DDrawingMenuBase 
    {
        protected override bool PerformAction()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "htmldocument (*.html) |*.html;";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                   ICore2DDrawingDocument[] v_doc =  this.CurrentSurface.Documents.ToArray();
                   using (WebVisitor visitor = new WebVisitor())
                   {
                       if (Workbench.ConfigureWorkingObject(visitor, "title.saveAsHtmlDocument".R(), false, new Size2i(400, 500)) == 
                           enuDialogResult.OK)
                       {
                           visitor.OutputDir = PathUtils.GetDirectoryName(sfd.FileName);
                           visitor.Visit(v_doc);
                           visitor.Save(sfd.FileName);
                           visitor.Dispose();
                           System.Diagnostics.Process.Start(sfd.FileName);
                       }
                   }
                }
            }
            return base.PerformAction();
        }
    }
}

