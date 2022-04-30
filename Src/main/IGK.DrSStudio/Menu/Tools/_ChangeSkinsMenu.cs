

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ChangeSkins.cs
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
file:_ChangeSkins.cs
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
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace IGK.DrSStudio.Menu.Tools
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Menu;
    using IGK.DrSStudio.WinUI;
    [DrSStudioMenu("Tools.ChangeSkins", 90, SeparatorBefore = true)]
    class _ChangeSkinsMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Skins Files | *.xml";
                ofd.FileName = "sking.xml";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (global::System.IO.File.Exists(ofd.FileName))
                    {
                        CoreRenderer.Configure(ofd.FileName);
                    }
                }
            }
            return false;
        }
    }
}

