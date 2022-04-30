

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ComputerBrowserItem.cs
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
file:ComputerBrowserItem.cs
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    internal class FBComputerDirectoryBrowserItem : FBDirectoryBrowserItem
    {
        string m_display;
        public override string DisplayName
        {
            get
            {
                return m_display;
            }
        }
        public FBComputerDirectoryBrowserItem(string display)
            : base(null)
        {
            this.m_display = display;
            foreach (string i in  Environment.GetLogicalDrives ())
            {
                this.Nodes.Add ( new FBComputerDriveDirectoryBrowserItem (new DriveInfo (i)));
            }
        }        
        class FBComputerDriveDirectoryBrowserItem : FBDirectoryBrowserItem 
        {
            DriveInfo m_drive;
            public override string DisplayName
            {
                get
                {
                    if (m_drive.IsReady)
                        return string.Format("{0}({1})", this.m_drive.Name, this.m_drive.VolumeLabel);
                    return m_drive.Name;
                }
            }
            public FBComputerDriveDirectoryBrowserItem(DriveInfo drive)
                : base(drive.ToString())
            {
                this.m_drive = drive;
            }
        }
    }
}

