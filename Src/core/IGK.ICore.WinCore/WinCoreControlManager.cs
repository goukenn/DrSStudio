

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreControlManager.cs
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
file:WinCoreControlManager.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.ICore.WinCore
{
    public class WinCoreControlManager : ICoreControlManager
    {
        public enuKeys ModifierKeys
        {
            get { return (enuKeys)System.Windows.Forms.Control.ModifierKeys; }
        }
        public IXForm ActiveForm
        {
            get {
                return System.Windows.Forms.Form.ActiveForm as IXForm;
            }
        }
        public bool IsShifKey
        {
            get { return (Control.ModifierKeys & Keys.Shift) == Keys.Shift; }
        }
        public bool IsControlKey
        {
            get { return (Control.ModifierKeys & Keys.Control) == Keys.Control; }
        }
        public ICoreFilterToolMessage CreateMenuMessageFilter(ICoreWorkbenchMessageFilter coreWorkbench, 
            CoreShortCutMenuContainerToolBase tool, 
            enuKeys enuKeys)
        {
            if (enuKeys == enuKeys.None)
                return null;
            return new WinCoreMenuMessageFilter(
                coreWorkbench,
                tool,
                (char)enuKeys 
                );
        }
        public Vector2i MouseLocation
        {
            get { return new Vector2i(Control.MousePosition.X , Control.MousePosition.Y ); }
        }
    }
}

