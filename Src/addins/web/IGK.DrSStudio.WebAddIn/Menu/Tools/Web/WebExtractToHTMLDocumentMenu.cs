

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExtractToHTMLDocument.cs
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
file:_ExtractToHTMLDocument.cs
*/
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using IGK.ICore.IO;
namespace IGK.DrSStudio.WebAddIn.Menu.Tools
{
    [DrSStudioMenu("Tools.Web.ExtractToHTMLDocument", 0x202)]
    class _ExtractToHTMLDocument : WebDrawing2DMenuBase 
    {
        protected override bool PerformAction()
        {
            /*
             * 
             *   //@@@ ATTENTION IL Y A UNE LIMITATION DE 32KB POUR LES ANCIENS NAVIGATEUR IE EXPLORER
             * 
             * */
                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "HTML FILE| *.html";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string v = string.Empty;
                        if (this.CurrentSurface.Documents.Count > 1)
                            v = WebUtils.BuildFlyersDocument (PathUtils.GetDirectoryName(sfd.FileName ),this.CurrentSurface.Documents.ToArray().ConvertTo<Core2DDrawingDocumentBase>());
                        else
                            v = WebUtils.BuildDocument(PathUtils.GetDirectoryName(sfd.FileName), this.CurrentSurface.CurrentDocument as Core2DDrawingDocumentBase, true);
                        global::System.IO.File.WriteAllText(sfd.FileName, v);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            return false;
        }
      }
}

