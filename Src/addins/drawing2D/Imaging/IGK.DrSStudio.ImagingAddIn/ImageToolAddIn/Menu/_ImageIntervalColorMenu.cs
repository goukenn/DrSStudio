

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageIntervalColorMenu.cs
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
file:_ImageIntervaleColorMenu.cs
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
using System.Threading ;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.Imaging.Menu
{
    /// <summary>
    /// Transparency color interval menu
    /// </summary>
    [DrSStudioMenu("Image.Tools.TransparencyColorInterval", 2, ImageKey=CoreImageKeys.MENU_INTERVALCOLORMENU_GKDS)]
    sealed class _ImageTransparencyColorIntervaleMenu : ImagingMenuBase
    {
        private Bitmap m_oldBitmap;
        private int m_stride;
        private byte[] m_oldData;
        private byte[] m_newData;
        private UIXSetTransparencyColorKeyInterval m_control;
        private ThreadOperator m_operator;
        private enuCallBackType m_CallBack;
        public enuCallBackType CallBack
        {
            get { return m_CallBack; }
            set
            {
                if (m_CallBack != value)
                {
                    m_CallBack = value;
                }
            }
        }
        /// <summary>
        /// Apply Temporary
        /// </summary>
        internal class ThreadOperator
        {
            internal _ImageTransparencyColorIntervaleMenu owner;
            internal Thread m_thread;
            public ThreadOperator(_ImageTransparencyColorIntervaleMenu owner)
            {
                this.owner = owner;
            }
            internal void DoAction()
            {
                try
                {
                    owner.Apply(true);
                }
                catch (Exception Exception){
                    CoreLog.WriteDebug("Exception " + Exception.Message);
                }
            }
        }
        protected override bool PerformAction()
        {
            this.m_oldBitmap = ImagingUtils.GetClonedBitmap(this.ImageElement.Bitmap);
            WinCoreBitmapData c_data = WinCoreBitmapData.FromBitmap(this.m_oldBitmap);
            this.m_oldData = c_data.Data ;
            this.m_stride = (int)c_data.Stride ;
            this.m_newData = new byte[this.m_oldData.Length];
            m_control = new UIXSetTransparencyColorKeyInterval();
            this.m_operator = new ThreadOperator(this);
            using (ICoreDialogForm frm = Workbench.CreateNewDialog(this.m_control ))
            {
                frm.Title =this.Id.R();
                m_control.PropertyChanged += new EventHandler(ctr_PropertyChanged);
                if (frm.ShowDialog().Equals (enuDialogResult.OK))
                {
                    AbortThread();
                    this.Apply(false);
                }
                else
                {
                    this.ImageElement.SetBitmap(this.m_oldBitmap, false);
                    this.Invalidate();
                }
            }
            this.m_oldData = null;
            this.m_newData = null;
            GC.Collect();
            return false;
        }
        void ctr_PropertyChanged(object sender, EventArgs e)
        {
            AbortThread();
            this.m_operator.m_thread = new Thread(this.m_operator.DoAction);
            //this.m_operator.m_thread.SetApartmentState (ApartmentState.MTA);
            this.m_operator.m_thread.IsBackground = false;
            this.m_operator.m_thread.Start();
        }
        private void AbortThread()
        {
            try
            {
                if (m_operator != null && m_operator.m_thread != null)
                {
                    //lock (this.m_operator.m_thread)
                    //{
                        m_operator.m_thread.Abort();
                        m_operator.m_thread.Join();
                        //realease attachem
                        m_operator.m_thread = null;
                    //}
                }
            }
            catch(Exception ex) {
                CoreLog.WriteDebug("Abort Error : "+ex.Message );
            }
        }
        private void Apply(bool temp)
        {
            //Control ctr = this.CurrentSurface as Control;
            ////if (ctr != null)
            ////{
            ////    if (ctr.InvokeRequired)
            ////    {
            ////        ApplyPROC  d = new ApplyPROC(Apply);
            ////        ctr.BeginInvoke (d, temp );
            ////    }
            ////    else
            ////    {
            lock (this)
            {
                IntervalOperatorProc proc = null;
                this.m_CallBack = m_control.CallBackType;
                switch (this.m_CallBack)
                {
                    case enuCallBackType.GrayScale:
                        proc = GrayScale;
                        break;
                    case enuCallBackType.InvertColor:
                        proc = Invert;
                        break;
                    case enuCallBackType.AddImage:
                        break;
                    case enuCallBackType.SubImage:
                        break;
                    default:
                        break;
                }
                if (proc == null)
                    return;
                LayerTransparencyKey v_lTransk = new LayerTransparencyKey();
                v_lTransk.Color1 = Colorf.FromByteRgb(
                    (byte)m_control.IntervalRed.Min,
                    (byte)m_control.IntervalGreen.Min,
                    (byte)m_control.IntervalBlue.Min
                    );
                v_lTransk.Color2 = Colorf.FromByteRgb(
                    (byte)m_control.IntervalRed.Max,
                    (byte)m_control.IntervalGreen.Max,
                    (byte)m_control.IntervalBlue.Max
                    );
                Bitmap bmp = null;
                try
                {
                    //bmp =
                    //    WinCoreBitmapOperation.SetTransparencyKeyInterval(
                    //    this.m_oldBitmap,
                    //    v_lTransk,
                    //    m_control.Exclude);
                    int w = this.m_oldBitmap.Width;
                    int h = this.m_oldBitmap.Height;
                    int offset = 0;
                    byte r = 0, g = 0, b = 0;
                    byte minr = (byte)m_control.IntervalRed.Min;
                    byte ming = (byte)m_control.IntervalGreen.Min;
                    byte minb =  (byte)m_control.IntervalBlue.Min;
                    byte maxr = (byte)m_control.IntervalRed.Max;
                    byte maxg = (byte)m_control.IntervalGreen.Max;
                    byte maxb =  (byte)m_control.IntervalBlue.Max;
                    for (int j = 0; j < h; j++)
                    {
                        offset = j * this.m_stride;
                        for (int i = 0; i < w; i++)
                        {
                            r = this.m_oldData[offset];
                            g = this.m_oldData[offset +1];
                            b = this.m_oldData[offset +2];
                            if (
                                ((r>= minr) &&(r<=maxr ))&&
                                ((g>= ming) &&(g<=maxg ))&&
                                    ((b>= minb) &&(b<=maxb )))
                            {
                                this.m_newData[offset] = r;
                                this.m_newData[offset + 1] = g;
                                this.m_newData[offset + 2] = b;
                                this.m_newData[offset + 3] = this.m_oldData[offset + 3];
                            }
                            else {
                                if (m_control.Exclude)
                                {
                                    this.m_newData[offset + 3] = 0;
                                }
                                else
                                {
                                    proc(this.m_newData, offset, r, g, b);
                                    this.m_newData[offset + 3] = this.m_oldData[offset + 3];
                                }
                            }
                            offset += 4;
                        }
                    }
                    this.ImageElement.SetBitmap(WinCoreBitmapData.FromData (w,h, this.m_newData).ToBitmap(), temp);
                }
                catch (Exception Exception)
                {
                    CoreLog.WriteDebug(Exception.Message);
                    if (bmp !=null)
                        bmp.Dispose();
                }
                finally { 
                }
                this.CurrentSurface.RefreshScene ();
                }
        }
        static void GrayScale(byte[] newData , int offset, byte r, byte g, byte b)
        {
                r = (byte)(r * 0.298 + g * 0.587 + b * 0.114);
                newData[offset] = newData[offset + 1] = newData[offset + 2] = r;
        }
        static void Invert(byte[] newData , int offset, byte r, byte g, byte b)
        {
            r = (byte)(255 - r);
            g = (byte)(255 - g);
            b = (byte)(255 - b);
            newData[offset] = r;
            newData[offset + 1] = g;
            newData[offset + 2] = b;
        }
    }
}

