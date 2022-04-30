

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _MenuViewLayerIconView.cs
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
file:_MenuViewLayerIconView.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Tools;
using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.WinUI.Menu
{
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Drawing2D.Menu;
    [CoreMenu("View.DocumentLayerIconView", 
       32,
        ImageKey="Menu_Layers", 
        Shortcut = System.Windows.Forms.Keys.D,
        ShortcutText="D", SeparatorBefore=true )]
    class _MenuViewLayerIconView : Core2DMenuViewToolBase
    {
        public _MenuViewLayerIconView():
            base(IGK.DrSStudio.Drawing2D.Tools.CoreTool_LayerIconView.Instance)
        {
        }
        protected override void OnSurfaceChanged(EventArgs eventArgs)
        {
            if (this.CurrentSurface == null)
            {
                this.Visible = false;
                this.Enabled = false;
                return;
            }
            else {
                CheckVisible();
            }
        }
        private void CheckVisible()
        {
            bool v = (this.CurrentSurface != null);
            this.Visible = v;
            this.Enabled = v;
        }
    }
}

