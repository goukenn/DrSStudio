

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ServiceInstaller.cs
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
file:ServiceInstaller.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.ServiceProcess;
namespace IGK.GdsFilePreviewService
{
    [RunInstaller (true)]
    public class ServiceInstaller : Installer 
    {
        ServiceProcessInstaller m_processInstaller;
        global::System.ServiceProcess.ServiceInstaller m_serviceInstaller;
        public ServiceInstaller()
        {
            m_processInstaller = new ServiceProcessInstaller();
            m_processInstaller.Account = ServiceAccount.LocalSystem;
            m_serviceInstaller = new global::System.ServiceProcess.ServiceInstaller();
            m_serviceInstaller.ServiceName = IGK.GkdsFilePreviewHandler.GkdsFileConstant.SERVICE_NAME;
            m_serviceInstaller.StartType = ServiceStartMode.Manual;
            this.Installers.Add(m_processInstaller);
            this.Installers.Add(m_serviceInstaller);
        }
    }
}

