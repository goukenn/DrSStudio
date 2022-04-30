

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD3DOGLSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 


namespace IGK.DrSStudio.Drawing3D.OpenGL.WinUI
{
    using IGK.ICore.Codec;
    using IGK.OGLGame;
    using IGK.OGLGame.Graphics;
    using System.Drawing;
    using System.IO;
    using IGK.ICore.WinCore.WinUI.Controls;
    
    [CoreSurface ("IGKD3DOGLSurface", EnvironmentName=OGLConstant.ENVIRONMENT_NAME )]
    /// <summary>
    /// Drawing surface 
    /// </summary>
    public class IGKD3DOGLSurface : 
        IGKXWinCoreWorkingSurface ,
        ICoreWorkingFilemanagerSurface 
    {
        private OGLGraphicsDevice m_device; //ogl graphics device
        private Timer m_timer; //for refreshing
        private IGKD3DOGLDocument m_Document;
        private bool m_Saving;

        /// <summary>
        /// get if this surface is in saving mode
        /// </summary>
        public bool Saving
        {
            get { return m_Saving; }
            protected set
            {
                if (m_Saving != value)
                {
                    m_Saving = value;
                }
            }
        }

        public event EventHandler Saved;

        /// <summary>
        /// raise the saved eventhandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            this.NeedToSave = false;
            if (this.Saved != null)
                this.Saved(this, e);
        }
        /// <summary>
        /// get the current document
        /// </summary>
        public IGKD3DOGLDocument CurrentDocument {
            get {
                return m_Document;
            }
        }
        public OGLGraphicsDevice Device {
            get {
                return this.m_device;
            }
        }

        public IGKD3DOGLSurface()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, false);
            this.m_timer = new Timer();
            this.m_timer.Tick += m_timer_Tick;
            this.m_timer.Interval = 20;
            this.FileName  = "empty_scene.gkds";
            this.m_Document = new IGKD3DOGLDocument();
            this.m_Document.Elements.Add(new IGKD3DOGLRectangle() { });
            this.FileNameChanged += _fileNameChanged;
            _fileNameChanged(this, EventArgs.Empty);
            this.VisibleChanged += _VisibleChanged;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (m_device != null)
            {
                this.m_device.Viewport = new Rectanglei(0, 0, this.Width, this.Height);
                this.Render();
            }
        }
        protected override void OnResize(EventArgs e)
        {
            if (m_device != null)
            {
                this.Render();
            }
            base.OnResize(e);

        }

        void _VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                this.m_timer.Enabled = true;
                this.Render();
            }
            else {
                this.m_timer.Enabled = false;
            }
        }

        private void _fileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.m_fileName);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                this.m_timer.Enabled = false;
                this.m_timer.Dispose();
            }
            base.Dispose(disposing);
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            this.Render();
        }
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (!this.DesignMode)
            {
                this.m_device = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle);
                if (this.m_device == null)
                {
                    throw new CoreException("No Device created");
                }
                this.m_timer.Enabled = true;
            }
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            if (this.m_device != null)
                this.m_device.Dispose();
        }

        public void Render() {

            if (this.m_device == null)
                return;
            this.m_device.MakeCurrent();
            this.m_device.Clear(IGKOGLWinCoreRendering.OGLSurfaceBackgroundColor);

            this.m_Document.Render(this.m_device);
            
            this.m_device.EndScene(true);
        }

        public string FileName
        {
            get
            {
                return this.m_fileName;
            }
            set
            {
                if (this.m_fileName != value)
                {
                    this.m_fileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }

        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, eventArgs);
            }
        }

        public event EventHandler FileNameChanged;

        public void RenameTo(string name)
        {
            throw new NotImplementedException();
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.IGKD3D.SaveSceneAs".R(),
                "bitmap | *.bmp;*.png;*jpeg",
                this.FileName);
        }

        public bool NeedToSave
        {
            get {
                return this.m_needToSave;
            }
            set {
                if (this.m_needToSave != value)
                {
                    this.m_needToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }

        protected void OnNeedToSaveChanged(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }
        public event EventHandler NeedToSaveChanged;
        private bool m_needToSave;
        private string m_fileName;

        public void Save()
        {
            if (File.Exists(this.FileName))
            {
                this.SaveAs(this.FileName);
            }
            else {
                CoreSystem.GetAction("File.SaveAs").DoAction();
            }
        }

        public void SaveAs(string filename)
        {
            //save document
            int width = 1024;
            int height = 768;
            switch (Path.GetExtension(filename).ToLower())
            {
                case ".gkds":
                    CoreEncoder.Instance.Save(this, filename, this.CurrentDocument);
                    break;
                default:
                    using (Bitmap bmp = new Bitmap(width, height))
                    {
                        Graphics g = Graphics.FromImage(bmp);
                        IntPtr v_hdc = g.GetHdc();
                        OGLGraphicsDevice dev = OGLGraphicsDevice.CreateDeviceFromHDC(v_hdc);

                        if (dev != null)
                        {
                            dev.MakeCurrent();
                            dev.Viewport = new Rectanglei(0, 0, bmp.Height, bmp.Height);
                            //dev.Clear(enuBufferBit.Color);//, Colorf.Transparent);
                            this.m_Document.Render(dev);
                            dev.Flush();

                        }
                        g.ReleaseHdc(v_hdc);
                        g.Flush();
                        bmp.Save(filename, System.Drawing.Imaging.ImageFormat.Png);

                    }
                    break;
            }           

            this.FileName = filename;
            this.NeedToSave = false;
        }


        public virtual  void ReloadFileFromDisk()
        {
            try
            {
                if (File.Exists(this.FileName))
                {
                    IGKD3DOGLDocument[] v_docs =
                        CoreDecoder.Instance.OpenFileDocument(this.FileName).ConvertTo<IGKD3DOGLDocument>();
                    if ((v_docs != null) && (v_docs.Length == 1))
                    {
                        this.m_Document = v_docs[0];
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine("Can't reload file from disk " + this.FileName + "\n" + ex.Message);
            }
        }
    }
}
