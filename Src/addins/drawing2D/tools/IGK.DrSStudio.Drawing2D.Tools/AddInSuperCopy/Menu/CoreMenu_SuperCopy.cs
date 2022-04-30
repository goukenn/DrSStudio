

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenu_SuperCopy.cs
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
file:CoreMenu_SuperCopy.cs
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
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Drawing2D.Menu
{
    [DrSStudioMenu("Edit.SuperCopy", 4)]
    class CoreMenu_SuperCopy : Core2DDrawingMenuBase 
    {
        UIXSuperCopy m_tool;

        protected override bool IsEnabled()
        {
            return base.IsEnabled();
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.Enabled = false;
            this.Visible = true;
        }
        protected override bool PerformAction()
        {
            if (this.m_tool == null)
            {
                m_tool = new UIXSuperCopy();        
            }
            m_tool.Surface = this.CurrentSurface;
            m_tool.ElementToCopy = this.CurrentSurface.CurrentLayer.SelectedElements[0];
            using (ICoreDialogForm dialog = Workbench.CreateNewDialog(m_tool))
            {
                dialog.Title = CoreSystem.GetString ("DLG.superCopy.caption");
                dialog.ShowInTaskbar = false;
                dialog.StartPosition = enuFormStartPosition.CenterParent;
                dialog.CanMaximize = false;
                dialog.CanReduce  = false;
                this.CurrentSurface.Paint += _Paint;
                dialog.ShowDialog();
                this.CurrentSurface.Paint -= _Paint;
                //to avoid disposing for this tool 
                dialog.Controls.Remove(this.m_tool);
            }
            return false;
        }
        //paint tempory preseenttaion
        void _Paint(object sender, CorePaintEventArgs e)
        {
            float w = this.CurrentSurface.CurrentDocument .Width ;
            float h = this.CurrentSurface.CurrentDocument .Height ;
            Rectanglef v_rc = new Rectanglef(0, 0, w, h);
            Vector2f c = CoreMathOperation.GetCenter(v_rc);
            c = this.CurrentSurface.GetScreenLocation (c);
            v_rc = new Rectanglef(c.X, c.Y, 0, 0);
            v_rc.Inflate(3, 3);
            e.Graphics.FillRectangle(WinCoreBrushRegister.GetBrush(Colorf.Red), v_rc);
            v_rc.Inflate(-1, -1);
            e.Graphics.FillRectangle(WinCoreBrushRegister.GetBrush(Colorf.White), v_rc);
            v_rc.Inflate(-1, -1);
            e.Graphics.FillRectangle(WinCoreBrushRegister.GetBrush(Colorf.SkyBlue ), v_rc);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.SelectedElementChanged -= new EventHandler(layer_SelectedElementChanged);
            base.UnRegisterLayerEvent(layer);
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
        {
            base.OnCurrentSurfaceChanged(e);
            CheckConfigure(); 
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            CheckConfigure();            
        }
        private void CheckConfigure()
        {
            if (this.CurrentSurface == null)
                this.Enabled = false;
            else 
                this.Enabled = (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1);
        }
    }
}

