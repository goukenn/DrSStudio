

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewImageSurface.cs
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
file:ViewImageSurface.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinUI
{
    class ViewImageSurface : XDrawing2DSurface 
    {
        //public override Type DefaultTool{
        //    get {
        //        return null;
        //    }
        //}
        public override ICoreWorkingMecanism IsToolValid(Type t)
        {
            return base.IsToolValid(t);
        }
        public override Type DefaultTool
        {
            get
            {
                return null;
            }
        }
        public override Type CurrentTool
        {
            get
            {                
                return base.CurrentTool;
            }
            set
            {
                base.CurrentTool = null;
            }
        }
    }
}

