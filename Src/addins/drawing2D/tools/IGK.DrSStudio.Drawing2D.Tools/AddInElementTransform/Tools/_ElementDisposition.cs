

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ElementDisposition.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ElementDisposition.cs
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.ElementTransform.Tools
{
    using IGK.ICore.Drawing2D.Tools;
    using IGK.DrSStudio.ElementTransform.WinUI;
    using IGK.ICore.Tools;
    [CoreTools("Tool.D2DElementDispotions")]
    class _ElementDisposition : Core2DDrawingToolBase 
    {
        private static _ElementDisposition sm_instance;
        private _ElementDisposition()
        {
        }
        public static _ElementDisposition Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static _ElementDisposition()
        {
            sm_instance = new _ElementDisposition();
        }
        public new ICore2DDrawingSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new XElementDispositionToolStrip(this);
        }
    }
}

