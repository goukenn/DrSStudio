

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerInstaller.cs
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
file:PreviewHandlerInstaller.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration.Install ;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics ;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.PreviewHandlerAddIn.Installer
{
    /// <summary>
    /// used to register this assembly
    /// </summary>
    [RunInstaller(true)]
    class PreviewHandlerInstaller : System.Configuration.Install.Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                base.Install(stateSaver);
                RegistrationServices regsrv = new RegistrationServices();
                if (!regsrv.RegisterAssembly(this.GetType().Assembly, AssemblyRegistrationFlags.None))
                {
                    throw new InstallException("Failed to register for COM interop.");
                }
            }
            catch (Exception exc)
            {
                Trace.WriteLine(exc.ToString());
                throw;
            }
        }
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
            RegistrationServices regsrv = new RegistrationServices();
            if (!regsrv.UnregisterAssembly(this.GetType().Assembly))
            {
                throw new InstallException("Failed to unregister for COM interop.");
            }
        }
    }
}

