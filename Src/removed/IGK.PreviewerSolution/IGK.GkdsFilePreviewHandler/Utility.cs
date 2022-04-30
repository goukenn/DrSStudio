

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Utility.cs
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
file:Utility.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Collections;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Configuration;
namespace IGK.GkdsFilePreviewHandler
{
    public static class Utility
    {
        public static void UninstallAssembly(Assembly asm)
        {
            IDictionary sm_state = new Hashtable();
            AssemblyInstaller asmI = new AssemblyInstaller(asm, new string[] { });
            try
            {
                asmI.Uninstall(sm_state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void InstallAssembly(Assembly asm)
        {
            IDictionary sm_state = new Hashtable();
            AssemblyInstaller asmI = new AssemblyInstaller(asm, new string[] { });
            try
            {
                asmI.Install(sm_state);
                asmI.Commit(sm_state);
            }
            catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine (ex.Message);
            }
        }
    }
}

