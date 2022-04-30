

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageGrayLevelTool.cs
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
file:_ImageGrayLevelTool.cs
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
using System.Drawing.Imaging;
using System.Threading;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.DrSStudio.Imaging;
    using IGK.ICore.Threading;
    using IGK.ICore.WinUI.Common;

    /// <summary>
    /// base class of the tools element
    /// </summary>
    [DrSStudioMenu("Image.Tools.GrayLevelMenu", 5, ImageKey = "Menu_GrayLevel")]
    public sealed class _ImageGrayLevelMenu : ImageMenuBase
    {
        UIXAdjustGrayLevel adj;
        Bitmap m_oldBitmap;
        WinCoreBitmapData m_oldData;
        System.Windows.Forms.Timer tim;
        protected override bool PerformAction()
        {
            m_oldBitmap = ImagingUtils.GetClonedBitmap(this.ImageElement.Bitmap);
            m_oldData = WinCoreBitmapData.FromBitmap(m_oldBitmap);
            tim = new System.Windows.Forms.Timer();
            tim.Interval = 200;
            tim.Tick += new EventHandler(tim_Tick);
            m_JobProgress = null;
            adj = new UIXAdjustGrayLevel();
            using (ICoreDialogForm frm = this.Workbench.CreateNewDialog (adj))
            {
                adj.PropertyChanged += new EventHandler(adj_PropertyChanged);
                frm.StartPosition = enuFormStartPosition.CenterParent;
                frm.Title = this.Id.R();
                Apply();
                if (frm.ShowDialog().Equals ( enuDialogResult.OK))
                {
                    m_oldBitmap.Dispose();
                    m_oldBitmap = this.ImageElement.Bitmap.Clone() as Bitmap;
                    this.ImageElement.SetBitmap(m_oldBitmap, false);
                }
                else
                {
                    this.ImageElement.SetBitmap(m_oldBitmap, true);
                }
                this.CurrentSurface.RefreshScene();
            }
            adj.Dispose();
            tim.Dispose();
            tim = null;
            AbortJob();
            return true;
        }
        Thread m_thJob;
        CoreJobProgressEventHandler m_JobProgress;
        void AbortJob()
        {
            if (m_thJob != null)
            {
                OnJobProgress(100);
                m_thJob.Abort();
                //m_thJob.Join();
                m_thJob = null;
            }
        }
        void OnJobProgress(float percent)
        {
            if (m_JobProgress != null)
                m_JobProgress.BeginInvoke(this, (int)percent, null, null);
        }
        void StartJob()
        {
            AbortJob();
            m_thJob = CoreThreadManager.CreateThread(Apply, "ImageKeyLevel");            
            m_thJob.Start();
        }
        void tim_Tick(object sender, EventArgs e)
        {
            tim.Enabled = false;
            StartJob();
            //Apply();            
        }
        void adj_PropertyChanged(object sender, EventArgs e)
        {
            //StartJob();// 
            this.tim.Enabled = true;
        }
        void Apply()
        {
            //this.MainForm.Invoke((MethodInvoker)delegate()
            //{
            OnJobProgress(0);
            Bitmap bmp = WinCoreBitmapOperation.MakeBW(this.m_oldData.Clone (),
            adj.RedAdjust,
            adj.GreenAdjust,
            adj.BlueAdjust);
            this.ImageElement.SetBitmap(bmp, true);
            this.CurrentSurface.RefreshScene();
            //});
        }
    }
}

