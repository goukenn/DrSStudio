

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContextMenu_SuperCopy.cs
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
file:ContextMenu_SuperCopy.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.SuperCopyAddin
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.ContextMenu;
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    using IGK.DrSStudio.ContextMenu;
    
    [DrSStudioContextMenu("Edit.SuperCopy",5, CaptionKey="Edit.SuperCopy")]
    class ContextMenu_SuperCopy : IGKD2DDrawingContextMenuBase  
    {
        CoreMenu_SuperCopy cp;
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
        }
        protected override void OnWorkbenchChanged(EventArgs eventArgs)
        {
            base.OnWorkbenchChanged(eventArgs);       
            cp = (CoreMenu_SuperCopy)CoreSystem.GetMenu ("Edit.SuperCopy");
            if (cp == null)
                throw new CoreException(enuExceptionType.ArgumentIsNull, "copy menu is missing");
            cp.EnabledChanged += new EventHandler(cp_EnabledChanged);
            this.Enabled = cp.Enabled;
        }
        void cp_EnabledChanged(object sender, EventArgs e)
        {
            
            this.Enabled = cp.Enabled;
        }
        protected override bool PerformAction()
        {
            cp.DoAction();
            return false;
        }
        protected override bool IsVisible()
        {
            ICoreWorkingToolManagerSurface v_toolSurface = this.CurrentSurface
                as ICoreWorkingToolManagerSurface;
            bool c = base.IsVisible() && (v_toolSurface != null) &&
              (v_toolSurface.CurrentTool == typeof(SelectionElement))
              && this.AllowContextMenu;
              

            return c;
        }
       
    }
}

