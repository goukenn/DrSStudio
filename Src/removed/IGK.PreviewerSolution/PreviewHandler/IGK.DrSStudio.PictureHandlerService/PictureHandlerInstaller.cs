

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PictureHandlerInstaller.cs
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
file:PictureHandlerInstaller.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
namespace IGK.DrSStudio.PictureHandlerService
{
    [RunInstaller (true)]
    /// <summary>
    /// represent a picture handler installer
    /// </summary>
    public class PictureHandlerInstaller  : Installer 
    {
        ServiceProcessInstaller m_processInstaller;
        ServiceInstaller m_srvInstaller;
        public PictureHandlerInstaller()
        {
            m_processInstaller = new ServiceProcessInstaller();
            m_srvInstaller = new ServiceInstaller();
            m_processInstaller.Account = ServiceAccount.LocalSystem;
            m_srvInstaller.StartType = ServiceStartMode.Automatic ;
            m_srvInstaller.ServiceName = PictureHandlerConstant.SERVICE_NAME;
            this.Installers.Add(m_srvInstaller);
            this.Installers.Add(m_processInstaller);
        }
    }
}

