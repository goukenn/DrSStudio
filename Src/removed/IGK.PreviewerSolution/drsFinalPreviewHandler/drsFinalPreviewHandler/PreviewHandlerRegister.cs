

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PreviewHandlerRegister.cs
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
file:PreviewHandlerRegister.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.PreviewHandler
{
    public static class PreviewHandlerRegister
    {
        [ComRegisterFunction]
        public static void Register(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandlerBase)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
#if DEBUG
                    Console.WriteLine("Register " + t.FullName);
#endif
                   PreviewHandlerComInstaller. RegisterPreviewHandler(attr.Name, attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }
        [ComUnregisterFunction]
        public static void Unregister(Type t)
        {
            if (t != null && t.IsSubclassOf(typeof(PreviewHandlerBase)))
            {
                object[] attrs = (object[])t.GetCustomAttributes(typeof(PreviewHandlerAttribute), true);
                if (attrs != null && attrs.Length == 1)
                {
                    PreviewHandlerAttribute attr = attrs[0] as PreviewHandlerAttribute;
                   PreviewHandlerComInstaller. UnregisterPreviewHandler(attr.Extension, t.GUID.ToString("B"), attr.AppId);
                }
            }
        }
    }
}

