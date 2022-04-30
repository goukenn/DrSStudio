

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreTool_LayerIconView.cs
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
file:CoreTool_LayerIconView.cs
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
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.Tools;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [CoreTools("Tool.DocumentLayerIconView", ImageKey="Menu_Layers")]
    public sealed class CoreTool_LayerIconView : Core2DDrawingToolBase
    {
       private UIXDocumentLayerOutlineControl m_hostcontrol;
       private static CoreTool_LayerIconView _instance;
        public static CoreTool_LayerIconView Instance
        {
            get
            {
                return _instance;
            }
        }
        static CoreTool_LayerIconView()
        {
            _instance = new CoreTool_LayerIconView();
        }
        private CoreTool_LayerIconView()
        {
            this.VisibleChanged += new EventHandler(_VisibleChanged);
        }
        void _VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && (this.CurrentSurface != null))
            {
                ConfigureControl();
            }
        }
        protected override void GenerateHostedControl()
        {
            m_hostcontrol = new UIXDocumentLayerOutlineControl(this);
            m_hostcontrol.CaptionKey = "Tool.DocumentLayerIconView";
            this.HostedControl = m_hostcontrol;
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.DocumentRemoved += new Core2DDrawingDocumentEventHandler(surface_DocumentRemove);
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.DocumentRemoved -= new Core2DDrawingDocumentEventHandler(surface_DocumentRemove);
            base.UnRegisterSurfaceEvent(surface);
        }
        void surface_DocumentRemove(object o, Core2DDrawingDocumentEventArgs e)
        {
            if (m_hostcontrol.Document == e.Document )
            {
                ConfigureControl();
            }
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingSurfaceChangedEventArgs e)
        {
            base.OnCurrentSurfaceChanged(e);
            if (this.CurrentSurface != null)
            {
                this.ConfigureControl();
            }
            else {
                m_hostcontrol.Diseable(true);
                m_hostcontrol.Configure(null);
            }
        }
        private void ConfigureControl()
        {
            if (this.Visible )
            {
                if (this.CurrentSurface.CurrentDocument != null)
                {
                    m_hostcontrol.Diseable(false);
                     m_hostcontrol.Configure(this.CurrentSurface.CurrentDocument);
                }
                else
                {
                    m_hostcontrol.Diseable(true);
                    m_hostcontrol.Configure(null);
                }
            }
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            ConfigureControl();
        }
    }
}

