

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _WiXAddFilesMenu.cs
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
file:_WiXAddFilesMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WiXAddIn.Menu.WiX
{
    [DrSStudioMenu("Wix.AddFiles" , 10, SeparatorBefore=true)]
    class _WiXAddFiles : WiXMenuBase 
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Files | *.*";
                ofd.Multiselect = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    List<WiXProjectFile> cfile = new List<WiXProjectFile>();
                    foreach (string item in ofd.FileNames )
                    {
                        WiXProjectFile p = new WiXProjectFile(item);
                        cfile.Add(p);
                    }
                    this.CurrentSurface.AddFiles(cfile.ToArray());
                }
            }
            return base.PerformAction();
        }
    }
}

