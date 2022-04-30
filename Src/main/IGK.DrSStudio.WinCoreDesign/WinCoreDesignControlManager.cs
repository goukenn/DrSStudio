

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreDesignControlManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.WinUI.Design
{
    class WinCoreDesignControlManager : ICoreControlManager
    {
        public Vector2i MouseLocation
        {
            get { return Vector2i.Zero; }
        }

        public enuKeys ModifierKeys
        {
            get { return enuKeys.None; }
        }

        public IXForm ActiveForm
        {
            get { return null; }
        }

        public bool IsShifKey
        {
            get { return false; }
        }

        public bool IsControlKey
        {
            get { return false; }
        }

        public ICoreFilterToolMessage CreateMenuMessageFilter(ICoreWorkbench coreWorkbench, CoreShortCutMenuContainerToolBase coreShortCutMenuContainerToolBase, enuKeys enuKeys)
        {
            return null;
        }

        public ICoreFilterToolMessage CreateMenuMessageFilter(ICoreWorkbenchMessageFilter coreWorkbench, CoreShortCutMenuContainerToolBase coreShortCutMenuContainerToolBase, enuKeys enuKeys)
        {
            return null;
        }
    }
}
