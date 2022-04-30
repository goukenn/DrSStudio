

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DDrawingSurface.cs
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
﻿using IGK.ICore.Codec;
using IGK.DrSStudio.Drawing3D.OpenGL.Codec;
using IGK.OGLGame;
using IGK.OGLGame.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.DrSStudio.Drawing2D.OpenGL.Codec;

namespace IGK.DrSStudio.Drawing2D.OpenGL.WinUI
{
    [CoreSurface ("OGLDrawing2DSurface", EnvironmentName=CoreConstant.DRAWING2D_ENVIRONMENT)]
    class IGKOGL2DDrawingSurface : IGKD2DDrawingSurface
    {
        
        public IGKOGL2DDrawingSurface()
        {
         
        }
        public override bool IsToolValid(Type t)
        {
            var r = base.IsToolValid(t);
            if (r == false )
                return r;

          
            IGKOGL2DItemAttribute v_attr = IGKOGL2DItemAttribute.GetCustomAttribute
                (t, typeof(IGKOGL2DItemAttribute)) as IGKOGL2DItemAttribute;
            if (v_attr == null)
                return false ;
            if (v_attr.MecanismType == null)
                return false ;
            Type v_t = v_attr.MecanismType;
            return true;//,v_t.Assembly.CreateInstance(v_t.FullName) as ICoreWorkingMecanism;
        
        }
        public override ICoreWorkingMecanism GetToolMecanism(Type t)
        {
            var r = base.GetToolMecanism(t);
            if (r !=null)
                return r;


            IGKOGL2DItemAttribute v_attr = IGKOGL2DItemAttribute.GetCustomAttribute
                (t, typeof(IGKOGL2DItemAttribute)) as IGKOGL2DItemAttribute;
            if (v_attr == null)
                return null ;
            if (v_attr.MecanismType == null)
                return null;
            Type v_t = v_attr.MecanismType;
            return v_t.Assembly.CreateInstance(v_t.FullName) as ICoreWorkingMecanism;
        
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
        }
        public new static IGKOGL2DDrawingSurface CreateSurface(GKDSElement element)
        {
            IGKOGL2DDrawingSurface v_surface = new IGKOGL2DDrawingSurface();
            //v_surface.Project = element;
            DocumentElement doc = element.GetDocument();
            ProjectElement prj = element.GetProject();
            if (doc!=null)
            {
                v_surface.Documents.Replace(doc.Documents.ToArray().ConvertTo<Core2DDrawingDocumentBase>());
            }
            if (prj != null)
            {
                if (prj.AttributeExist ("FileName"))
                    v_surface.FileName = prj["FileName"].Value;
            }
            v_surface.NeedToSave = false;
            return v_surface;

        }
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene CreateScene()
        {
            return new IGKOGL2DDrawingSurfaceScene(this);
        }

        public override void Save()
        {
            if (File.Exists(this.FileName))
            {
                this.SaveAs(this.FileName);
            }
            else
            {
                CoreSystem.Instance.Workbench.CallAction("File.SaveAs");
            }
        }

        public new IGKOGL2DDrawingSurfaceScene Scene {
            get {
                return base.Scene as IGKOGL2DDrawingSurfaceScene;
            }
        }
        public override void SaveAs(string filename)
        {

            //base.SaveAs(filename);
            bool v_saved = false;
            this.Saving = true;
            switch (Path.GetExtension(filename).ToLower())
            {
                case ".gkds":
                    v_saved = CoreEncoder.Instance.Save(this, filename, this.Documents.ToArray());
                    break;
                default:
                    this.Scene.Pause();
                    

                    if (IGKD3DRasterBitmapEncoder.Instance.SaveToFile(filename, this.CurrentDocument))
                    {
                        v_saved = true;
                    }
                    this.Scene.Play();
                    break;
            }
            if (v_saved)
            {
                this.NeedToSave = false;
                this.FileName = filename;
                OnSaved(EventArgs.Empty);
            }
            this.Saving = false;
        }
        /// <summary>
        /// get the save as information setting
        /// </summary>
        /// <returns></returns>
        public override ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.save2DOGLDrawingSurfaceAs".R(),
            "project|*.gkds; | pictures| *.png; *.jpeg;*.jpg;*.bmp;",
            this.FileName);
        }

        /// <summary>
        /// rerpresent a gl surface scene
        /// </summary>
        public class IGKOGL2DDrawingSurfaceScene : IGKD2DDrawingScene, IOGLGGraphicsView
        {
            private OGLGraphicsDevice m_device; //open gl graphics device
            private IGKOGL2DDrawingDeviceVisitor m_RenderingDevice;
            private Timer m_timer;
            private SpriteBatch m_batch;

            protected override void Dispose(bool disposing)
            {
                if (this.m_device != null)
                {
                    this.m_device.Dispose();
                }
                if (this.m_timer != null)
                {
                    this.m_timer.Enabled = false;
                    this.m_timer.Dispose();
                }
                base.Dispose(disposing);
            }
            public IGKOGL2DDrawingSurfaceScene(IGKOGL2DDrawingSurface surface):base(surface )
            {
                this.SetStyle(ControlStyles.ResizeRedraw, false);
                this.SetStyle(ControlStyles.UserPaint, false);
                this.SetStyle(ControlStyles.Opaque, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);
                this.SizeChanged += _SizeChanged;
                
                this.m_timer = new Timer();
                m_timer.Tick += m_timer_Tick;
                this.m_timer.Interval = 20;

                //RectangleElement rc = new RectangleElement();
                //rc.SuspendLayout();
                //rc.Bounds = new Rectanglef(0, 0, 200, 10);

                //rc.FillBrush.SetSolidColor(Colorf.Black);
                //rc.StrokeBrush.SetSolidColor(Colorf.Red);
                //rc.ResumeLayout();
                //this.CurrentDocument.CurrentLayer.Elements.Add(rc);

                surface.VisibleChanged += _VisibleChanged;
                this.VisibleChanged += _VisibleChanged;
                surface.ParentChanged += _VisibleChanged;
                this.updateVisibility();
            }

            private void updateVisibility()
            {
                this.m_timer.Enabled = 
                    (this.m_device !=null) && 
                    this.Visible ;// && this.Owner.Visible && (this.Owner.Parent != null);
  
            }
            private void _VisibleChanged(object sender, EventArgs e)
            {
                this.updateVisibility();
            }

            void _SizeChanged(object sender, EventArgs e)
            {
                //init size changed view port
                if (this.m_device == null)
                    return;
                this.m_device.Viewport = this.GetViewPort();

            }

            void m_timer_Tick(object sender, EventArgs e)
            {
                if (this.m_device != null)
                {
                    this.m_device.MakeCurrent();
                    this.Render();
                }
            }
            protected override void OnHandleCreated(EventArgs e)
            {
                base.OnHandleCreated(e);
                this.m_device = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle);
                if (this.m_device == null)
                    throw new InvalidOperationException("device can't be create");
                this.m_RenderingDevice = IGKOGL2DDrawingDeviceVisitor.Create(this.m_device);

                InitDevice();
                this.Render();
                this.m_timer.Enabled = true;
            }

            private void InitDevice()
            {
                this.m_batch = new SpriteBatch(this.m_device, this);

            }
            /// <summary>
            /// pause animation
            /// </summary>
            public void Pause() {
                this.m_timer.Enabled = false;
            }
            public void Play()
            {
                this.m_timer.Enabled = true;
            }
            
            protected override void OnHandleDestroyed(EventArgs e)
            {
                this.m_device.Dispose();
                base.OnHandleDestroyed(e);

            }
            private void Render()
            {
                //begin scene
                this.m_device.Clear(enuBufferBit.Color , Colorf.CornflowerBlue);
                this.m_batch.Begin();
                this.RenderFrames(this.RenderingDevice);
                this.m_batch.End();
                this.m_device.Flush();
                this.m_device.EndScene();
            }

            public IGKOGL2DDrawingDeviceVisitor RenderingDevice { get {
                return this.m_RenderingDevice;
            } }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
            }
            public override void Refresh()
            {
                base.Refresh();
                this.Render();
            }


            /// <summary>
            /// get the current view port
            /// </summary>
            /// <returns></returns>
            public Rectanglei GetViewPort()
            {
                return new Rectanglei(0, 0, this.Width, this.Height);
            }

          
        }


      
    }
}
