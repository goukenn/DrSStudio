

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Program.cs
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
file:Program.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Configuration.Install;
namespace IGK.PreviewHandlerLib
{
    class Program
    {
        static IDictionary sm_state;
        static void Main(string[] args)
        {
            if ((args != null) && (args.Length == 1))
            {
                switch (args[0].ToLower())
                {
                    case "/i":
                        InstallAssembly();
                        break;
                    case "/u":
                        UninstallAssembly();
                        break;
                }
            }
            Console.WriteLine("end");
            Console.Read();
        }
        private static void UninstallAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            sm_state = new Hashtable();
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
        private static void InstallAssembly()
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            sm_state = new Hashtable();
            AssemblyInstaller asmI = new AssemblyInstaller(asm, new string[] { });
            asmI.Install(sm_state);
            asmI.Commit(sm_state);
        }
    }
}

