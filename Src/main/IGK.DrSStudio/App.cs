

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: App.cs
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
file:App.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio
{
    using IGK.ICore;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore;
    using IGK.ICore.WinUI;
    [CoreApplication("DrStudio")]
    /// <summary>
    /// DrSStudio Entry Application class
    /// </summary>
    class App : DrSStudioWinCoreApp, ICoreApplication
    {
        protected override ICoreResourceManager CreateResourceManager()
        {
            return new DrSStudioResourceManager();
        }
        protected override ICoreBrushRegister CreateBrushRegister()
        {
            return new WinCoreBrushRegister();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
    }
}

