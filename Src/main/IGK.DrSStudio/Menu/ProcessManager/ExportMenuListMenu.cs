

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ExportMenuList.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ExportMenuList.cs
*/
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.Menu.ProcessManager
{
    [DrSStudioMenu("Process.ExportMenuList", 100, SeparatorBefore=true)]
    class ExportMenuListMenu : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter  = "title.xml.filter".R() +" | *.xml";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    CoreSystem.GetMenus().ExportMenuAsXML (sfd.FileName);
                }
            }
            return base.PerformAction();
        }
    }
}

