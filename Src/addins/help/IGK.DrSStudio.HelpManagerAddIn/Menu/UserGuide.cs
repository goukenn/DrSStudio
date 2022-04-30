

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UserGuide.cs
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
file:UserGuide.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.HelpManagerAddIn.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("Help.UserGuide", -10, SeparatorAfter=true)]
    sealed class UserGuide : IGK.DrSStudio.Menu.CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            string v_file = IGK.DrSStudio.IO.PathUtils.GetPath("%startup%/Help/help.chm");
            System.Windows.Forms.Help.ShowHelp(Workbench.MainForm as System.Windows.Forms.Control,
                v_file );
            return false;
        }
    }
}

