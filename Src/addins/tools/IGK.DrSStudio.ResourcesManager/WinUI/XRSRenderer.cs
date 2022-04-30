

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XRSRenderer.cs
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
file:XRSRenderer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ResourcesManager.WinUI
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinUI;
    static class XRSRenderer 
    {
        static XRSRenderer() {
            CoreRendererBase.InitRenderer(System.Reflection.MethodInfo.GetCurrentMethod().DeclaringType);
        }
        public static Colorf RSBackgroundColor { get { return CoreRenderer.GetColor("RSBackgroundColor", Colorf.FromFloat (0.3f)); } }
        public static Colorf RSBackgroundItem2Color { get { return CoreRenderer.GetColor("RSBackgroundItem2Color", Colorf.FromFloat (0.8f)); } }
        public static Colorf RSBackgroundItem1Color { get { return CoreRenderer.GetColor("RSBackgroundItem1Color", Colorf.FromFloat (0.6f)); } }
    }
}

