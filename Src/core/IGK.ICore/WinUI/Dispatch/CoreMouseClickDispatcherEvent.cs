

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MouseClickDispatcher.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.WinUI.Dispatch
{
    public sealed class CoreMouseClickDispatcherEvent : CoreMouseDispatcherEventBase
    {     
        
        public bool CanProcess(ICore2DDrawingLayeredElement element, enuMouseButtons button, Vector2f location)
        {
            if (element.View && element.Contains(location))
                return true;
            return false;
        }

        public bool CanProcess(ICore2DDrawingLayeredElement element, CoreMouseEventArgs e)
        {
            return CanProcess(element, e.Button, e.FactorPoint);
        }

       
      
    }
}
