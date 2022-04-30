

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionExtractLib.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Web.Menu
{
    using IGK.ICore.WinCore;

    [DrSStudioMenu("Tools.ExtractLibFolder", 0x5000, SeparatorBefore=true)]
    class WebSolutionExtractLib : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            var obj = Properties.Resources.iGKWeb;
            if (obj == null)
                return false;

           using (FolderBrowserDialog fbd = new FolderBrowserDialog ())
           {
               if (fbd.ShowDialog () == DialogResult.OK )
               {
                   var zip = CoreApplicationManager.Application.ResourcesManager.GetZipReader ();
                   if (zip != null)
                   {
                       MainForm.SetCursor(Cursors.WaitCursor);
                       try
                       {
                           zip.ExtractZipData(Properties.Resources.iGKWeb,
                               fbd.SelectedPath);
                       }
                       catch(Exception ex) {
                           CoreMessageBox.Show("e.error_append_1".R(ex.Message));
                       }
                       MainForm.SetCursor(Cursors.Default);
                   }
                   else {
                       CoreMessageBox.Show("e.noezipReader".R());
                   }
               }
           }
 	         return true;
        }
    }
}
