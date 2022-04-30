

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _OpenFileAction.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_OpenFileAction.cs
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
namespace IGK.DrSStudio.Drawing3D.FileBrowser.Actions
{
    
using IGK.DrSStudio.Actions;
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI;
    /// <summary>
    /// open file form the edition surface
    /// </summary>
    sealed class _OpenFileAction : FBAction  
    {
        protected override bool PerformAction()
        {
            ICoreFileManagerWorkbench fb = CoreSystem.Instance.Workbench as
                ICoreFileManagerWorkbench;
            if (fb != null)
            {
                if (this.FileBrowser.SelectedFileIndex != -1)
                {
                    fb.OpenFile(
                        new string[]{
                        this.FileBrowser.Files[this.FileBrowser.SelectedFileIndex]}
                        );
                }
            }
            else {
                string filename = this.FileBrowser.Files[this.FileBrowser.SelectedFileIndex];
                System.Diagnostics.ProcessStartInfo sf = new System.Diagnostics.ProcessStartInfo();
                sf.Arguments = filename;
                sf.FileName = PathUtils.GetStartupFullPath ("drs.exe");
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = sf;
                if ( p.Start())
                {
                    try {
                        if (p.WaitForInputIdle())
                        {
                            CoreSystem.Instance.MainForm.Invoke((MethodInvoker)
                                delegate()
                                {
                                    if ((fb != null) && !fb.IsFileOpened(filename))
                                    {
                                        fb.OpenFile(new string[]{
                                                            filename 
                                        } );
                                    }
                                });
                        }
                    }
                    catch {
                    }
                }               
            }
            return false;
        }
    }
}

