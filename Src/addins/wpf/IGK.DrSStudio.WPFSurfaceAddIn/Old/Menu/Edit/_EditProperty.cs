

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _EditProperty.cs
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
file:_EditProperty.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu.Edit
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI.Configuration;
    [IGK.DrSStudio.Menu .CoreMenu("Edit.Properties", 
        Int32.MaxValue ,
        SeparatorBefore = true )]
    sealed class _EditProperty : WPFMenuBase 
    {
        protected override bool PerformAction()
        {
            ICoreWorkingConfigurableObject v_obj
                =
                this.CurrentSurface.ElementToConfigure as ICoreWorkingConfigurableObject;
            if (v_obj != null)
            {
                this.CurrentSurface.Workbench.ConfigureWorkingObject(v_obj
                     );
            }
            return base.PerformAction();
        }
    }
}

