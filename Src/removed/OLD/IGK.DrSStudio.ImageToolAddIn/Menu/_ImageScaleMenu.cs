

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageScaleMenu.cs
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
file:_ImageScaleMenu.cs
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
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    /// <summary>
    /// base class of the tools element
    /// </summary>
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.ScaleAdjust", 0, ImageKey="Menu_ScaleClMatrix")]
    sealed class _ImageSacaleAdjustMenu : ImageMenuBase
    {
        UIXColorScaleAdjust  m_adj;
        Bitmap m_oldBitmap;
        System.Windows.Forms.Timer m_tim;
        WinCoreBitmapData m_oldData;
        protected override bool PerformAction()
        {
            m_oldBitmap = (Bitmap)this.ImageElement.Bitmap.Clone();
            m_oldData = WinCoreBitmapData.FromBitmap(m_oldBitmap);
            m_tim = new System.Windows.Forms.Timer();
            m_tim.Interval = 200;
            m_tim.Tick += new EventHandler(tim_Tick);
            m_adj = new UIXColorScaleAdjust ();
            using (ICoreDialogForm frm = (ICoreDialogForm)this.Workbench.CreateNewDialog(m_adj))
            {
                // frm.Opacity = WorkBench.DialogOpacity;
                frm.Title = CoreSystem.GetString("Title.AdjustImageLevel");
                m_adj.PropertyChanged += new EventHandler(adj_PropertyChanged);
                frm.StartPosition = enuFormStartPosition.CenterParent;
                frm.CanMaximize = false;
                Apply();
                if (frm.ShowDialog() == enuDialogResult.OK)
                {
                    m_oldBitmap.Dispose();
                    m_oldBitmap = this.ImageElement.Bitmap.Clone() as Bitmap;
                    this.ImageElement.SetBitmap(m_oldBitmap, false);
                }
                else
                {
                    this.ImageElement.SetBitmap(m_oldBitmap, true);
                }
                this.ImageElement.Invalidate(true);
            }
            m_adj.Dispose();
            m_tim.Dispose();
            m_tim = null;
            return true;
        }
        void tim_Tick(object sender, EventArgs e)
        {
            m_tim.Enabled = false;
            Apply();
        }
        void adj_PropertyChanged(object sender, EventArgs e)
        {
            this.m_tim.Enabled = true;
        }
        void Apply()
        {        
            ColorMatrix mat = null;
            float c = 1.0f;
            float v = 1.0f;
            if (!this.m_adj.GlobalAlpha)
            {
                c = m_adj.AlphaAdjust;
                v = 0;
            }
            else
            {
                c = 0;
                v = m_adj.AlphaAdjust;
            }
            mat = new ColorMatrix(new float[][] { 
                new float[]{m_adj.RedAdjust,0,0,0,0},
                new float[]{0, m_adj.GreenAdjust  , 0,0,0},
                new float[]{0,0,m_adj.BlueAdjust,0,0 },
                new float[]{0,0,0, c, 0},
                new float[]{0,0,0, v, 1}
            });
            //(this.CurrentSurface as Control).Invoke((MethodInvoker)delegate()
            //{
                Bitmap bmp = WinCoreBitmapOperation.ApplyColorMatrix(
                    this.m_oldBitmap, mat);
                this.ImageElement.SetBitmap(bmp, true);
                this.ImageElement.Invalidate(true);
            //});
        }
    }
}

