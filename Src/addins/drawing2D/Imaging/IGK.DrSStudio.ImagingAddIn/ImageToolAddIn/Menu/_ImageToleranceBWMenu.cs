

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageToleranceBWMenu.cs
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_ImageToleranceBWMenu.cs
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
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Imaging.Menu
{
    /// <summary>
    /// base class of the tools element
    /// </summary>
    [DrSStudioMenu("Image.Tools.ToleranceBWMenu", 4)]
    sealed class _ToleranceBWMenu : ImagingMenuBase
    {
        UIXToleranceBW adj;
        int m_Red;
        int m_Green;
        int m_Blue;
        int m_Alpha;
        bool m_ConsiderAlpha;
        WinCoreBitmapData m_oldBimap;
        protected override bool PerformAction()
        {
            m_oldBimap = WinCoreBitmapData.FromBitmap(this.ImageElement.Bitmap);
            adj = new UIXToleranceBW();                        
            adj.PropertyChanged += new EventHandler(adj_PropertyChanged);
            using (ICoreDialogForm frm = Workbench.CreateNewDialog(adj))
            {
                frm.Title = "title.BWTolerance".R();
                frm.AutoSize = false;
                frm.Size =  new  Size2i(adj.Size.Width, adj.Size.Height);
                if (frm.ShowDialog().Equals ( enuDialogResult.OK))
                {
                    this.CurrentSurface.Cursor = CoreCursors.Wait;
                    this.Apply(false);
                    this.CurrentSurface.Cursor = CoreCursors.Normal;
                    this.CurrentSurface.RefreshScene();
                    return true;
                }
                else {
                    //resetore the previous picture
                    this.ImageElement.SetBitmap(this.m_oldBimap.ToBitmap(), false);
                    this.CurrentSurface .RefreshScene();
                }
            }
            return false;
        }
        void adj_PropertyChanged(object sender, EventArgs e)
        {
            Apply(true);
        }
        private void GetProperty()
        {
            m_Red = adj.RedAdjust;
            m_Green = adj.GreenAdjust;
            m_Blue = adj.BlueAdjust;
            m_Alpha = adj.AlphaAdjust;
            m_ConsiderAlpha = adj.ConsiderBlack;
        }
        void Apply(bool temp)
        {
            if (adj.InvokeRequired)
            {
                adj.Invoke(new ApplyPROC(Apply) );
                return;
            }
            else
                GetProperty();
            Bitmap bmp = WinCoreBitmapOperation.SetBlackAndWhiteSeuil(
                this.m_oldBimap .Clone(),
            m_Red,
            m_Green,
            m_Blue,
            m_Alpha,
            m_ConsiderAlpha);
            this.ImageElement.SetBitmap(bmp, temp);
            this.CurrentSurface.RefreshScene();
        }    
    }
}

