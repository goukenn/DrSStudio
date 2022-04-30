

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreTool_LayerOutline.cs
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
file:CoreTool_LayerOutline.cs
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
using IGK.DrSStudio.Drawing2D.Tools;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Tools;
using IGK.DrSStudio.WinUI;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Core.Layers;
    [CoreTools("Tool.LayerExplorer", ImageKey="Menu_LayerExplorer")]
    sealed class CoreTool_LayerOutline : 
        Core2DDrawingToolBase
    {
         private static CoreTool_LayerOutline _instance;
        public static CoreTool_LayerOutline Instance
        {
            get
            {
                return _instance;
            }
        }
        public new UIXLayerOutlineTreeView HostedControl
        {
            get { return base.HostedControl as UIXLayerOutlineTreeView; }
            set { base.HostedControl = value; }
        }
        static CoreTool_LayerOutline()
        {
            _instance = new CoreTool_LayerOutline();
        }
        private CoreTool_LayerOutline()
        {
            this.VisibleChanged += new EventHandler(_VisibleChanged);
        }
        void _VisibleChanged(object sender, EventArgs e)
        {
            if (this.CurrentSurface != null)
            {
                if (this.CurrentSurface.CurrentLayer != null)
                {
                    ConfigureHost(false, this.CurrentSurface.CurrentLayer);
                    return;
                }
            }
            ConfigureHost(true, null);
        }
        protected override void GenerateHostedControl()
        {
            UIXLayerOutlineTreeView v_hostcontrol = new UIXLayerOutlineTreeView(this);
            //v_hostcontrol.CaptionKey = "Tool.LayerOutiline";
            this.HostedControl = v_hostcontrol;
        }
        private void ConfigureHost(bool disable,ICore2DDrawingLayer layer)
        {
            UIXLayerOutlineTreeView host = this.HostedControl;
            host.Diseable(false);
            host.Configure(layer );
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            if (this.Visible)
            {
                ConfigureHost(false, layer);
            }
            layer.ElementZIndexChanged += new CoreWorkingObjectZIndexChangedHandler(layer_ElementZIndexChanged);
        }
        void layer_ElementZIndexChanged(object o, CoreWorkingObjectZIndexChangedArgs e)
        {
            if (this.Visible)
            {
                HostedControl.ToggleItem(e);
            }
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.ElementZIndexChanged -= new CoreWorkingObjectZIndexChangedHandler(layer_ElementZIndexChanged);
            base.UnRegisterLayerEvent(layer);
        }
    }
}

