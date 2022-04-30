

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToBase64StringMenu.cs
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
﻿using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore;
using System.Threading;

namespace IGK.DrSStudio.WebAddIn.Menu.Tools
{
    [DrSStudioMenu("Tools.Web.GetBase64String", 0x304)]
    class _ConvertToBase64StringMenu : CoreApplicationMenu
    {
        protected override bool PerformAction()
        {
            new Thread(() =>
            {
                var c = new CoreBase64StringClipBoard()
                {
                    Name = "yourstring"
                };
                if (this.Workbench.ConfigureWorkingObject(c, "title.convertobase64string".R(), false, Size2i.Empty) == enuDialogResult.OK)
                {
                    Byte[] t = UTF8Encoding.Default.GetBytes(c.Name);
                    WinCoreClipBoard.CopyToClipBoard(Convert.ToBase64String(t));                    
                }
                
            }).Start();
            return false;
        }

        class CoreBase64StringClipBoard : CoreConfigurableObjectBase
        {
            private string m_Name;

            [CoreConfigurableProperty()]            
            public string Name
            {
                get { return m_Name; }
                set
                {
                    if (m_Name != value)
                    {
                        m_Name = value;
                    }
                }
            }
        }
    }
}
