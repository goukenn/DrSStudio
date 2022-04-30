

/*
IGKDEV @ 2008 - 2014
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
/*
IGKDEV - [2008 - 2014]
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
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.WinUI;
    /// <summary>
    /// base class of the tools element
    /// </summary>
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.GrayLevelMenu", 5, ImageKey = "Menu_GrayLevel")]
    sealed class _GrayLevelMenu : ImageMenuBase
    {
        UIXAdjustGrayLevel adj;
        Bitmap m_oldBitmap;
        WinCoreBitmapData m_oldData;
        System.Windows.Forms.Timer tim;
        protected override bool PerformAction()
        {
            m_oldBitmap = (Bitmap)this.ImageElement.Bitmap.Clone();
            m_oldData = WinCoreBitmapData.FromBitmap(m_oldBitmap);
            tim = new System.Windows.Forms.Timer();
            tim.Interval = 200;
            tim.Tick += new EventHandler(tim_Tick);
            m_JobProgress = null;
            adj = new UIXAdjustGrayLevel();
            using (ICoreDialogForm frm = this.Workbench.CreateNewDialog (adj))
            {
                adj.PropertyChanged += new EventHandler(adj_PropertyChanged);
                frm.StartPosition = FormStartPosition.CenterParent;
                frm.Title = CoreSystem.GetString(this.Id);
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
                m_JobProgress.BeginInvoke(percent, null, null);
        }
        void StartJob()
        {
            AbortJob();
            m_thJob = IGK.DrSStudio.Threading.CoreThreadManager.CreateThread(Apply, "ImageKeyLevel");            
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
            this.ImageElement.Invalidate(true);
            //});
        }
    }
}

