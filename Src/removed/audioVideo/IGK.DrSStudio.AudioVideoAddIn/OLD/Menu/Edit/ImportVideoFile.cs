

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImportVideoFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:ImportVideoFile.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.AudioVideo.Menu.Edit
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu ("Edit.VideoImportFile", 500)]
    public sealed class ImportVideoFile : EditorVideoMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface == null)
                return false ;
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = CoreSystem.GetString("Title.ImportVideo");
                ofd.Filter = "avi files|*.avi";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    return this.CurrentSurface.ImportFile(ofd.FileName);
                }
            }
            return false;
        }
    }
}

