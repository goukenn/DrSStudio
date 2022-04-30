

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AudioVideoProgressProgressCallBack.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;





﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinCore;

namespace IGK.DrSStudio.AudioVideo
{
    /// <summary>
    /// represent a drawing 2d document progress call back
    /// </summary>
    public class AudioVideoProgressCallBack : IDisposable 
    {
        private ICore2DDrawingDocument m_document;
        private WinCoreBitmap m_cbitmap;
        private ICoreGraphics m_device;
        private Colorf m_BackgroundColor;

        public Colorf BackgroundColor
        {
            get { return m_BackgroundColor; }
            set
            {
                if (m_BackgroundColor != value)
                {
                    m_BackgroundColor = value;
                }
            }
        }

        public AudioVideoProgressCallBack(ICore2DDrawingDocument document)
        {
            this.m_BackgroundColor = Colorf.Black;
            this.m_document = document;
            this.m_cbitmap = WinCoreBitmap.Create(document.Width, document.Height, enuPixelFormat.Format24bpprgb );
            this.InitDevice();
        
        }

        public virtual void InitDevice()
        {            
            this.m_device = 
                CoreApplicationManager.Application.ResourcesManager.CreateDevice(
                Graphics.FromImage(this.m_cbitmap.Bitmap));
        }
        public virtual Bitmap Update(AudioVideoProgressInfo update)
        {
            CoreLog.WriteLine("[AVIProgress] - " +string.Format ("{0}/{1}", update.TimeSpan  , update.TotalTimeSpan ));
            return this.m_cbitmap.Bitmap;
        }
        public virtual Bitmap Update(int frame, TimeSpan span)
        {
            CoreLog.WriteLine("[AVIProgress] - Update " +(frame)+"/"+( span));
            ICoreTextElement c = this.m_document.GetElementById("frame") as ICoreTextElement;
            if (c != null)
                c.Text = span.ToString();
            this.m_device.Clear(this.BackgroundColor);
            this.m_device.Visit(this.m_document);
            this.m_device.Flush();
            this.m_device.Flush();
            return this.m_cbitmap.Bitmap;
        }

        public virtual  void Dispose()
        {
            this.m_device.Dispose();
            this.m_cbitmap.Dispose();
        }
    }
}
