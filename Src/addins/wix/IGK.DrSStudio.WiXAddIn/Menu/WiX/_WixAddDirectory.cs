

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _WixAddDirectory.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_WixAddDirectory.cs
*/
using IGK.DrSStudio.WiXAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.WiXAddIn.Menu.WiX
{
    [DrSStudioMenu("Wix.AddDir", 11)]
    class _WixAddDirectory : WiXMenuBase 
    {
        protected override bool PerformAction()
        {
            using (ICoreDialogForm d = Workbench.CreateNewDialog())
            {
               WiXNewDirectoryControl c=  new WiXNewDirectoryControl();
               c.Height = 120;
               d.Title = "wix.title.newdirectory".R();
               d.Controls.Add(c);
               d.Owner = this.MainForm;
                if (d.ShowDialog() == enuDialogResult.OK)
                {
                    this.CurrentSurface.AddDir(c.NewDirectory);
                }
            }
            return base.PerformAction();
        }
    }
}

