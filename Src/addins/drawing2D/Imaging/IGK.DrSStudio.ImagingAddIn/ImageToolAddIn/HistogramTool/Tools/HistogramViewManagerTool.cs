

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HistogramViewManagerTool.cs
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
file:HistogramViewManagerTool.cs
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
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Drawing.Imaging;
namespace IGK.DrSStudio.Imaging.Tools
{
    using IGK.DrSStudio.Data;
    using IGK.DrSStudio.Tools;
    /// <summary>
    /// get historgram tools 
    /// </summary>
    [IGK.DrSStudio.CoreTools ("Tools.Image.Histogram", 
        ImageKey="Menu_Histogram")]
    sealed class HistogramViewManagerTool :
        ImageToolBase  
    {
        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (this.Visible)
            {
                //check for item
                if ((this.CurrentSurface != null) && (this.CurrentSurface.CurrentLayer != null))
                {
                    GetSelection(this.CurrentSurface.CurrentLayer);
                }
                else
                {
                    if (this.ImageElement != null)
                        this.StartGetting();
                }
            }
            else{
                //free image view
                this.ImageElement = null;
            }
        }
        protected override void OnImagePropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {            
            switch ((enu2DPropertyChangedType ) e.ID)
            { 
                case enu2DPropertyChangedType.BitmapChanged :
                    if (this.Visible )
                        this.StartGetting ();         
                    break;
            }
        }
        private static HistogramViewManagerTool sm_instance;
        private HistogramViewManagerTool()
        {
        }
        protected override void RegisterImageEvent()
        {
            base.RegisterImageEvent();
            if (this.Visible)
            {
                this.StartGetting();
            }
        }
        public static HistogramViewManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        public new UIXHistogramView HostedControl {
            get {
                return base.HostedControl as UIXHistogramView;
            }
            set{
                base.HostedControl = value;
        }
        }
        static HistogramViewManagerTool()
        {
            sm_instance = new HistogramViewManagerTool();
        }
        protected override void GenerateHostedControl()
        {
            this.HostedControl = new UIXHistogramView();
            this.HostedControl.ModeChanged += new EventHandler(HostedControl_ModeChanged);
        }
        void HostedControl_ModeChanged(object sender, EventArgs e)
        {
            if ((this.ImageElement != null)&&(this.Visible ))
                this.StartGetting();
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
            layer.SelectedElementChanged += new EventHandler(layer_SelectedElementChanged);
        }
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            if (!this.Visible )
                return;
            GetSelection(this.CurrentSurface.CurrentLayer);
        }
        private void GetSelection(ICore2DDrawingLayer layer)
        {
            ICore2DDrawingLayer l = layer;
            if ((l.SelectedElements.Count == 1)
            && (l.SelectedElements[0] is ImageElement))
            {
                this.ImageElement = l.SelectedElements[0] as ImageElement;
                this.HostedControl.Enabled = true;
                this.StartGetting();
            }
            else
            {
                this.ImageElement = null;
                this.HostedControl.Enabled = false;
                this.HostedControl.SetHistogram(null);
            }
        }
        private void GetHistogram()
        {
            if ((this.ImageElement  == null) || (!this.Visible))
                return;
            Bitmap bmp = null;
            lock (this.ImageElement.Bitmap )
            {
                bmp = this.ImageElement.Bitmap.Clone() as Bitmap;
            }
            Int32[][] tab = null;
            try
            {
                tab = GetHistogramData(bmp);
            }
            catch
            {
                bmp.Dispose();
                return;
            }
            finally
            {
                // bmp.Dispose();
            }
            if (tab == null)
                return;
            float size = bmp.Width * bmp.Height;
            Bitmap vout = new Bitmap(300, 140);
            Graphics g = Graphics.FromImage(vout);
            g.Clear(Color.DarkGray);
            g.Transform = new Matrix(1, 0,
                0, -1, 0, 0);
            g.TranslateTransform(0, 130, MatrixOrder.Append);
            int p = 0;
            float d = 0;
            for (int i = 0; i < 255; ++i)
            {
                p = i + 10;
                switch (this.HostedControl.Mode)
                {
                    case enuRGBMode.R :
                        d = (float)((Math.Log(tab[0][i], size) / Math.Log(size, size)) * 100);
                        g.DrawLine(Pens.Red, p, d, p, 0);
                        break;
                    case enuRGBMode.G :
                        //green
                        d = (float)(Math.Log(tab[1][i], size) / Math.Log(size, size)) * 100;
                        g.DrawLine(Pens.Lime, p, d, p, 0);
                        break;
                    case enuRGBMode.B :
                        //blue
                        d = (float)(Math.Log(tab[2][i], size) / Math.Log(size, size)) * 100;
                        g.DrawLine(Pens.Blue, p, d, p, 0);
                        break;
                    case enuRGBMode.RGB:
                        {
                            //red 
                            d = (float)((Math.Log(tab[0][i], size) / Math.Log(size, size)) * 100);                            
                            g.DrawLine(Pens.Red, p, d, p, 0);
                            //green
                            d = (float)(Math.Log(tab[1][i], size) / Math.Log(size, size)) * 100;
                            g.DrawLine(Pens.Lime, p, d, p, 0);
                            //blue
                            d = (float)(Math.Log(tab[2][i], size) / Math.Log(size, size)) * 100;
                            g.DrawLine(Pens.Blue, p, d, p, 0);
                        }
                        break;
                    default:
                        {
                            d = (float)(Math.Log(tab[3][i], size) / Math.Log(size, size)) * 100;
                            g.DrawLine(Pens.Gray, p, d, p, 0);
                        }
                        break;
                }
            }
            g.DrawLine(Pens.Black, 10, 0, 10, 300);
            g.DrawLine(Pens.Black, 10, 0, 300, 0);
            g.Flush();
            g.Dispose();
            bmp.Dispose();
            this.HostedControl .SetHistogram (vout);
        }
        static Int32[][] GetHistogramData(Bitmap bmp)
        {
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            Int32[] rTab = new int[256];
            Int32[] gTab = new int[256];
            Int32[] bTab = new int[256];
            Int32[] grTab = new int[256];
            int offset = 0;
            int r = 0;
            int g = 0;
            int b = 0;
            for (int y = 0; y < bmp.Height; ++y)
            {
                for (int x = 0; x < bmp.Width; ++x)
                {
                    r = Marshal.ReadByte(data.Scan0, offset + 2);
                    g = Marshal.ReadByte(data.Scan0, offset + 1);
                    b = Marshal.ReadByte(data.Scan0, offset);
                    ++rTab[r];
                    ++gTab[g];
                    ++bTab[b];
                    ++grTab[(r + g + b) / 3];
                    offset = offset + 3;
                }
                offset = y * data.Stride;
            }
            bmp.UnlockBits(data);
            return new Int32[][] { rTab, gTab, bTab, grTab };
        }
        //thread to get async histogram
        System.Threading.Thread thHistogram;
        AsyncGetHistogram thObject;
        void AbortGetHistogram()
        {
            lock (this)
            {
                if (thHistogram != null)
                {
                    thHistogram.Abort();
                    thHistogram.Join();
                    if (thObject != null)
                    {
                        thObject.FreeData();
                    }
                    thObject = null;
                }
            }
        }
  void StartGetting()
        {
            AbortGetHistogram();
            if (this.ImageElement != null)
            {
                thObject = new AsyncGetHistogram(this.ImageElement.Bitmap.Clone() as Bitmap, this);
                thHistogram = new System.Threading.Thread(this.thObject.GetHistogram);
                thHistogram.Name = "GetHistogramThread";
                thHistogram.Start();
            }
        }
        //async Get Histogram
        public class AsyncGetHistogram
        {
            internal Bitmap bmp;
            HistogramViewManagerTool  manager;
            public AsyncGetHistogram(Bitmap bmp, HistogramViewManagerTool manager)
            {
                this.manager = manager;
                this.bmp = bmp;
            }
            void Application_ThreadExit(object sender, EventArgs e)
            {
                FreeData();
            }
            internal void FreeData()
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                }
            }
            internal void GetHistogram()
            {
                //System.Windows.Forms.Application.ThreadExit += new EventHandler(Application_ThreadExit);          
                lock (manager)
                {
                    manager.GetHistogram();
                    if (manager.thHistogram == System.Threading.Thread.CurrentThread)
                    {
                        manager.thHistogram = null;//free resource thread
                        GC.Collect();
                    }
                }
            }
        }        
    }
}

