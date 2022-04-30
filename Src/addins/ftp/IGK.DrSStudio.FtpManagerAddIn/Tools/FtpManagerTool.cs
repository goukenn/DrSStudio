

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpManagerTool.cs
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
file:FtpManagerTool.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.FtpManagerAddIn.Tools
{
    
using IGK.ICore;using IGK.ICore.Tools;
    using IGK.DrSStudio.FtpManagerAddIn.WinUI;
    [CoreTools("Tool.FtpManagerTool")]
    class FtpManagerTool : CoreToolBase
    {
        private static FtpManagerTool sm_instance;
        private FtpManagerSurface m_ftpSurface;
        public FtpManagerSurface FtpSurface {
            get {
                return this.m_ftpSurface;
            }
        }
        private FtpManagerTool()
        {
        }
        public static FtpManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static FtpManagerTool()
        {
            sm_instance = new FtpManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = null;
        }
        internal void CreateNewSurface()
        {
            this.m_ftpSurface = new FtpManagerSurface();
            this.m_ftpSurface.Disposed += m_ftpSurface_Disposed;
        }
        void m_ftpSurface_Disposed(object sender, EventArgs e)
        {
            this.m_ftpSurface = null;
        }
    }
}

