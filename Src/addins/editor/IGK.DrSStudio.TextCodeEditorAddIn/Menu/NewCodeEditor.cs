

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewCodeEditor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore;using IGK.DrSStudio.Menu;
using IGK.DrSStudio.TextCodeEditorAddIn.WinUI;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.TextCodeEditorAddIn.Menu
{
    [DrSStudioMenu("File.New.NewCodeFile", 0x50)]
    public class NewCodeEditor : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            TCEditorSurface v_surface = new TCEditorSurface();            
            this.Workbench.AddSurface (v_surface,true );
            //this.Workbench.SetCurrentSurface(v_surface);
            v_surface.Title = "new".R();
            v_surface.Text = "//Enter your text here-----------------------------";
            return false;
        }
    }
}
