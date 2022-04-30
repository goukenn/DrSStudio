

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Installation.cs
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
file:Installation.cs
*/
// Stephen Toub
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;
namespace 
IGK.DrSStudio.PreviewInstaller
{
    using IGK.ICore;using IGK.DrSStudio.PicturePreviewHandler;
    [RunInstaller(true)]
    public class ComInstaller : Installer
    {
        public override void Install(IDictionary stateSaver)
        {
            try
            {
                base.Install(stateSaver);
                RegisterComponent();
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
            UnregisterComponent();
        }
        private static void RegisterComponent()
        {
            RegistrationServices regService = new RegistrationServices();
            Assembly asm = typeof(DrsStudioPrevHandler).Assembly;
            Type[] t = regService.GetRegistrableTypesInAssembly(asm);
            Console.WriteLine("--------------------------------------------");
            foreach (Type item in t)
            {
                Console.WriteLine("RegisterType  : " + item.FullName);
            }
            Console.WriteLine("--------------------------------------------");
            try
            {
                if (regService.RegisterAssembly(asm, AssemblyRegistrationFlags.SetCodeBase ))
                {
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Registered");
                    Console.WriteLine("--------------------------------------------");
                }
                else
                {
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine(asm.FullName + " : Not registered");
                    Console.WriteLine("--------------------------------------------");
                }
            }
            catch (Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
        }
        private static void UnregisterComponent()
        {
            RegistrationServices regService = new RegistrationServices();
            Assembly asm = typeof(DrsStudioPrevHandler).Assembly;
            try
            {
                if (regService.UnregisterAssembly(asm))
                {
                    Console.WriteLine("UnRegistered");
                }
            }
            catch (Exception Exception)
            {
                Console.WriteLine(Exception.Message);
            }
        }
    }
}

