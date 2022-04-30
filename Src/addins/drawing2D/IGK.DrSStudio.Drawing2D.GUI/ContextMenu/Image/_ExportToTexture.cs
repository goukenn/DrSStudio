

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ExportToTexture.cs
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
file:_ExportToTexture.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IGK.ICore.WinCore;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System.Drawing.Drawing2D;
using IGK.DrSStudio.ContextMenu;
using IGK.ICore.Drawing2D; 
namespace IGK.DrSStudio.Drawing2D.ContextMenu.Image
{

    using IGK.ICore;

    [DrSStudioContextMenu("Drawing2DExport.ExportToTexture", 11)]
    sealed class _ExportToTexture : IGKD2DDrawingContextMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
            {
                ICore2DDrawingLayeredElement l = this.CurrentSurface .CurrentLayer.SelectedElements[0];
                Rectanglei rc = Rectanglef.Round(l.GetBound());
                if ((rc.Width == 0) || (rc.Height <= 0))
                    return false;
                using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(rc.Width, rc.Height))
                {
                    Graphics g = Graphics.FromImage(bmp);
                    g.TranslateTransform(-rc.X, -rc.Y, MatrixOrder.Append);                    
                    l.Draw(g);
                    g.Flush();
                    //Drawing2D.Utils.Textures.AddImage(
                    //    string.Format ("#texture{0}",
                    //    Drawing2D.Utils.Textures.Count ),
                    //    bmp);
                }
            }
            return false;
        }
        //protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        //{
        //    ICore2DDrawingSurface v_s = this.CurrentSurface;              
        //    this.Visible =(this.OwnerContext.SourceControl == CurrentSurface) && 
        //        (v_s.CurrentTool == typeof (SelectionElement ) ) && 
        //        (v_s.Mecanism.AllowContextMenu);
        //    this.Enabled = this.Visible;
        //}
        protected override void OnOpening(EventArgs e)
        {
            base.OnOpening(e);
        }
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);
        }
        protected override bool IsVisible()
        {
            ICoreContextMenu v_ctx = this.OwnerContext;
            bool v = false;
            if (v_ctx == null)
            {
                v = false;
            }
            else
            {
                ICore2DDrawingSurface v_s = this.CurrentSurface;
                v = ( (v_s!=null) && (v_ctx.SourceControl == CurrentSurface) && this.AllowContextMenu)
                &&                         
                (this.OwnerContext.SourceControl == CurrentSurface) && 
                (CurrentTool == typeof (SelectionElement ) ) && 
                (this.AllowContextMenu);
                ;
            }
            return v;
        }
        protected override bool IsEnabled()
        {
            return this.IsVisible () && (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1);
        }
        public Type CurrentTool { get {
            ICoreWorkingToolManagerSurface v_tool = this.CurrentSurface as ICoreWorkingToolManagerSurface;
            if (v_tool != null)
                return v_tool.CurrentTool;
            return null;
        } }
    }
}

