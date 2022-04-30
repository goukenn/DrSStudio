

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageSelectionContextMenuBase.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:_ImageSelectionContextMenuBase.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.ImageSelection.ContextMenu
{
    using IGK.DrSStudio.Drawing2D.ContextMenu;
    public abstract class _ImageSelectionContextMenuBase : IGKD2DDrawingContextMenuBase 
    {
        private ImageSelectionElement.Mecanism m_mecanism;
        public ImageSelectionElement.Mecanism Mecanism
        {
            get { return m_mecanism; }
            set
            {
                if (value != m_mecanism)
                {
                    m_mecanism = value;
                    OnMecanismChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler MecanismChanged;
        private void OnMecanismChanged(EventArgs eventArgs)
        {
            if (this.MecanismChanged != null)
                this.MecanismChanged(this, eventArgs);
        }
        public _ImageSelectionContextMenuBase()
        {
            this.MecanismChanged += new EventHandler(_ImageSelectionContextMenuBase_MecanismChanged);
        }
        void _ImageSelectionContextMenuBase_MecanismChanged(object sender, EventArgs e)
        {
            if (m_mecanism != null)
            {
                this.Enabled = true;
            }
            else
                this.Enabled = false;   
        }
        protected override void InitContextMenu()
        {
            base.InitContextMenu();
            this.Visible = false;
            this.Enabled = false;
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentToolChanged  += new EventHandler(DrawingManager_CurrentDrawingTypeChanged);
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentToolChanged -= new EventHandler(DrawingManager_CurrentDrawingTypeChanged);
            base.UnRegisterSurfaceEvent(surface);
        }
        void DrawingManager_CurrentDrawingTypeChanged(object sender, EventArgs e)
        {
            if (CurrentSurface.CurrentTool  == typeof(ImageSelectionElement))
            {
                this.Visible = true;
            }
            else
            {
                this.Visible = false;
            }
        }
        protected override void OnOpening(EventArgs e)
        {
            if ((this.Mecanism !=null) && this.CurrentSurface .CurrentTool == (typeof(ImageSelectionElement)))
            {
                ImageElement img = this.CurrentSurface.Mecanism.Element as ImageElement;
                IGK.Vector2f vf =
                   this.CurrentSurface.GetFactorLocation ( this.CurrentSurface.PointToClient(
                              Vector2f.Round (this.OwnerContext.MouseOpeningLocation)));
                if (img != null)
                {
                    this.Visible = (img.Contains(vf));
                    this.Enabled = this.Visible;
                }
                else
                {
                    this.Visible = false;
                    this.Enabled = false;
                }
            }
            else
            {
                this.Visible = false;
                this.Enabled = false;
            }
        }
    }
}

