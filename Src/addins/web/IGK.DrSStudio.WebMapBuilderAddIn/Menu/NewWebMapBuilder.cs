

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: NewWebMapBuilder.cs
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
file:NewWebMapBuilder.cs
*/
using IGK.DrSStudio.Web.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Web.Menu
{
    [DrSStudioMenu("File.New.WebMapBuilder", 500)]
    class NewWebMapBuilder : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
           using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = CoreServices.GetString ( WebMapConstant.PICTURE_FILTER);
                if ((ofd.ShowDialog() == DialogResult.OK) && (File.Exists(ofd.FileName )))
                {
                    WebMapSurface v_s = new WebMapSurface();              
                    v_s.CurrentDocument.LoadImage(ofd.FileName);
                    this.Workbench.AddSurface (v_s, true );
                }
            }
            return base.PerformAction();
        }
    }
}

