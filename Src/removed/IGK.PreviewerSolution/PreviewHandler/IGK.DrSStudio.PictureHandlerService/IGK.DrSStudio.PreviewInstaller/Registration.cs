

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Registration.cs
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
file:Registration.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using IGK.ICore;using IGK.PrevHandlerLib;
namespace IGK.DrSStudio.PreviewInstaller
{
    internal static class PreviewHandlerRegistration
    {
        [ComRegisterFunction]
        private static void Register(Type t) { 
            PreviewHandler.Register(t); 
        }
        [ComUnregisterFunction]
        private static void Unregister(Type t) { 
            PreviewHandler.Unregister(t); 
        }
    }
}

