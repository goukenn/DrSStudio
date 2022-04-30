

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SaveSolutionAs.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Menu.File
{
    [DrSStudioMenu("File.SaveSolutionAs", CoreConstant.SAVE_MENU_INDEX + 4)]
    class _SaveSolutionAs : CoreApplicationSurfaceMenuBase 
    {
        new ICoreWorkingProjectSolutionSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingProjectSolutionSurface;
            }
        }

        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);     
            this.SetupEnableAndVisibility();
        }
        protected override bool IsEnabled()
        {
            return this.CurrentSurface != null;
        }
        protected override bool IsVisible()
        {
            return this.CurrentSurface != null;
        }
        protected override bool PerformAction()
        {
            if (this.CurrentSurface == null)
                return false ;

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                ICoreWorkingProjectSolution s = this.CurrentSurface.Solution;
                string bkdir = Environment.CurrentDirectory;
                ICoreSaveAsInfo info = s.GetSolutionSaveAsInfo();
                if (info != null)
                {
                    sfd.Title = info.Title;
                    sfd.Filter = info.Filter;
                    string dir = PathUtils.GetDirectoryName(info.FileName);
                    if (System.IO.Directory.Exists(dir))
                    {
                        Environment.CurrentDirectory = dir;
                    }
                    sfd.FileName = System.IO.Path.GetFileName(info.FileName);
                } 
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    s.SaveAs(sfd.FileName);
                }
                Environment.CurrentDirectory = bkdir;
            }
            return false;
        }
    }
}
