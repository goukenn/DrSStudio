

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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Configuration;
using System.Configuration.Install;
namespace IGK.GkdsFilePreviewHandler
{
    using IGK.ICore;using IGK.PreviewHandlerLib;
    using IGK.PreviewHandlerLib.PreviewHandlers;
    static class Program
    {
        private static void Main(string[] args)
        {
            if ((args != null) && (args.Length == 1))
            {
                switch (args[0].ToLower())
                {
                    case "/i":
                        Utility.InstallAssembly(Assembly.GetExecutingAssembly());
                        break;
                    case "/u":
                        Utility.UninstallAssembly(Assembly.GetExecutingAssembly ());
                        break;
                }
            }
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("End");
            Console.WriteLine("----------------------------------------");
        }
    }
}

