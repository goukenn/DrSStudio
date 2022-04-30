

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _WixLoadFiles.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.WiXAddIn.Menu.WiX
{
    [DrSStudioMenu("Wix.LoadFiles", 12)]
    class _WixLoadFilesFromFolderMenu : WiXMenuBase
    {
        protected override bool PerformAction()
        {
            if (string.IsNullOrEmpty(this.CurrentSurface.SelectedDirectory))
            {
                CoreMessageBox.Show("Msg.Wix.InstallDirectoryMustBeSelected".R(), "title.WiX.SelectedFileRequirement".R());
            }
            else
            {
                
                using (IGK.ICore.WinUI.Common.FolderNamePicker t =
                    Workbench.CreateCommonDialog < FolderNamePicker>(CoreCommonDialogs.FolderNamePicker ))                    
                {
                    if (t != null)
                    {
                        t.Title = "title.selectfoldertoload".R();
                        t.SelectedFolder = Environment.CurrentDirectory;
                        if (t.ShowDialog() == enuDialogResult.OK)
                        {
                            this.CurrentSurface.LoadDir(t.SelectedFolder);
                        }
                    }
                    else
                    {

                        using (FolderBrowserDialog fbd = new FolderBrowserDialog())
                        {
                            fbd.SelectedPath = Environment.CurrentDirectory;
                            if (fbd.ShowDialog() == DialogResult.OK)
                            {
                                //this.CurrentSurface.Project.LoadFolderFeature());
                                this.CurrentSurface.LoadDir(fbd.SelectedPath);
                            }
                        }
                    }
                }
            }      
            return base.PerformAction();
        }
    }
}
