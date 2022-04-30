

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidIconSurface.cs
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
file:AndroidIconSurface.cs
*/

using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android.WinUI
{
    using ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using System.IO;
    [CoreSurface("AndroidIconDrawingSurface")]
    class AndroidAppIconSurface : 
        AndroidSurfaceBase,
       ICoreWorkingFilemanagerSurface,
        ICore2DDrawingSurface,
        ICoreWorkingToolManagerSurface
    {
        IGKD2DDrawingSurface m_surface;

        public event EventHandler FileNameChanged {
            add {
            m_surface.FileNameChanged+= value;}
            remove 
            {
                m_surface.FileNameChanged -= value;
            }
        }
        public event EventHandler NeedToSaveChanged
        {
            add
            {
                m_surface.FileNameChanged += value;
            }
            remove
            {
                m_surface.FileNameChanged -= value;
            }
        }
        public event EventHandler Saved
        {
            add
            {
                m_surface.Saved += value;
            }
            remove
            {
                m_surface.Saved -= value;
            }
        }
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentAdded
        {
            add
            {
                m_surface.DocumentAdded += value;
            }
            remove
            {
                m_surface.DocumentAdded -= value;
            }
        }
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentRemoved
        {
            add
            {
                m_surface.DocumentRemoved += value;
            }
            remove
            {
                m_surface.DocumentRemoved -= value;
            }
        }
        public event CoreWorkingDocumentChangedEventHandler CurrentDocumentChanged
        {
            add
            {
                m_surface.CurrentDocumentChanged += value;
            }
            remove
            {
                m_surface.CurrentDocumentChanged -= value;
            }
        }
        public event EventHandler ElementToConfigureChanged
        {
            add
            {
                m_surface.ElementToConfigureChanged += value;
            }
            remove
            {
                m_surface.ElementToConfigureChanged -= value;
            }
        }
        public event EventHandler BrushModeChanged
        {
            add
            {
                m_surface.BrushModeChanged += value;
            }
            remove
            {
                m_surface.BrushModeChanged -= value;
            }
        }
        public event EventHandler CurrentColorChanged
        {
            add
            {
                m_surface.CurrentColorChanged += value;
            }
            remove
            {
                m_surface.CurrentColorChanged -= value;
            }
        }
        public event EventHandler ZoomModeChanged
        {
            add
            {
                m_surface.ZoomModeChanged += value;
            }
            remove
            {
                m_surface.ZoomModeChanged -= value;
            }
        }
        public event EventHandler ZoomChanged
        {
            add
            {
                m_surface.ZoomChanged += value;
            }
            remove
            {
                m_surface.ZoomChanged -= value;
            }
        }
        public event EventHandler ShowScrollChanged
        {
            add
            {
                m_surface.ShowScrollChanged += value;
            }
            remove
            {
                m_surface.ShowScrollChanged -= value;
            }
        }
        public event EventHandler CurrentToolChanged
        {
            add
            {
                m_surface.CurrentToolChanged += value;
            }
            remove
            {
                m_surface.CurrentToolChanged -= value;
            }
        }


        public override string Title
        {
            get
            {
                return m_surface.Title;
            }
         
        }
        public string FileName
        {
            get
            {
                return m_surface.FileName;
            }

            set
            {
                m_surface.FileName = value; 
            }
        }

        public bool Saving
        {
            get
            {
               return m_surface.Saving;
            }
        }

        public bool NeedToSave
        {
            get
            {
                return m_surface.NeedToSave ;
            }

            set
            {
                m_surface.NeedToSave = value;
            }
        }

        public bool CanAddDocument => m_surface.CanAddDocument ;

        public ICore2DDrawingDocumentCollections Documents=> m_surface.Documents;

        public ICore2DDrawingDocument CurrentDocument
        {
            get
            {
                return m_surface.CurrentDocument ;
            }

            set
            {
                m_surface.CurrentDocument = value as Core2DDrawingDocumentBase  ;
            }
        }

        public ICore2DDrawingLayer CurrentLayer
        {
            get
            {
                return m_surface.CurrentLayer;
            }

            set
            {
                 m_surface.CurrentLayer = value as Core2DDrawingLayer;
            }
        }

        ICoreWorkingDocument ICoreWorkingDocumentManagerSurface.CurrentDocument
        {
            get
            {
                return this.CurrentDocument as ICoreWorkingDocument;
            }

            set
            {
                this.CurrentDocument = value as Core2DDrawingDocumentBase;
            }
        }

        public ICoreWorkingObject ElementToConfigure
        {
            get
            {
                return m_surface.ElementToConfigure;
            }

            set
            {
                m_surface.ElementToConfigure = value ;
            }
        }

        public IntPtr SceneHandle => m_surface.SceneHandle;
        public enuBrushMode BrushMode
        {
            get
            {
                return m_surface.BrushMode;
            }

            set
            {
                m_surface.BrushMode = value;
            }
        }

        public Colorf CurrentColor
        {
            get
            {
               return this.m_surface.CurrentColor ;
            }

            set
            {
               this.m_surface.CurrentColor = value;
            }
        }

        ICore2DDrawingObject ICore2DDrawingObject.Parent=> this.m_surface.Parent as ICore2DDrawingObject;

        public ICoreBrush FillBrush => this.m_surface.FillBrush;

        public ICorePen StrokeBrush => this.m_surface.StrokeBrush;
        public enuBrushSupport BrushSupport => this.m_surface.BrushSupport;

        public Rectanglei DisplayArea => this.m_surface.DisplayArea;

        public float PosX => this.m_surface.PosX;

        public float PosY => this.m_surface.PosY;

        public enuZoomMode ZoomMode
        {
            get
            {
               return m_surface.ZoomMode ;
            }

            set
            {
               this.m_surface.ZoomMode = value ;
            }
        }

        public float ZoomX => this.m_surface.ZoomX;

        public float ZoomY => this.m_surface.ZoomY;

        public bool ShowScroll
        {
            get
            {
                return this.m_surface.ShowScroll ;
            }

            set
            {
                this.m_surface.ShowScroll =value ;
            }
        }

        public Type DefaultTool
        {
            get
            {
                return this.m_surface.DefaultTool;
            }
        }

        public Type CurrentTool
        {
            get
            {
                return this.m_surface.CurrentTool;
            }

            set
            {
               this.m_surface.CurrentTool = value ;
            }
        }

        public ICoreWorkingMecanism Mecanism => this.m_surface.Mecanism;

        public AndroidAppIconSurface()
        {
            m_surface = IGKD2DDrawingSurface.CreateSurface (72,72);
            m_surface.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add (m_surface);

            this.FileName = "icon.png";
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //var s = CoreSystem.CreateWorkingObject(CoreConstant.DRAWING2D_NAMESPACE + "/drawing2dsurface")
            //    as ICore2DDrawingSurface;
         

        }

        public void RenameTo(string name)
        {
            m_surface.RenameTo(name);
        }

        public void ReloadFileFromDisk()
        {
            m_surface.ReloadFileFromDisk ();
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty (this.FileName) && this.SaveAndroidAppIcon(this.FileName))
                return;
            m_surface.Save();
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
       "title.saveAsAndroidAppIcon".R(),
       $"android app ico | *.png",
       this.FileName);  
        }

        public void SaveAs(string filename)
        {
            if (this.SaveAndroidAppIcon(filename))
                return ;
          
            this.m_surface.SaveAs (filename);


        }

        private bool SaveAndroidAppIcon(string filename)
        {

            string v_ext = Path.GetExtension(filename);

            switch (v_ext.ToLower())
            {
                case "png":
                    //get folder
                    string s = Path.GetDirectoryName(filename); 
                    return true;  
            }
            return false ;
        }

        public bool AddNewDocument()
        {
            return m_surface.AddNewDocument();///throw new NotImplementedException();
        }

        public ICoreCaret CreateCaret()
        {
            return m_surface.CreateCaret ();
        }

        public void RefreshScene(ICore2DDrawingLayeredElement obj)
        {
            m_surface.RefreshScene ();
        }

        public ICoreSnippet CreateSnippet(ICoreWorkingMecanism mecanism, int demand, int index, CoreSnippetRenderProc proc = null)
        {
           return m_surface.CreateSnippet (mecanism, demand, index, proc);
        }

        public ICore2DDrawingDocument CreateNewDocument()
        {
            return (this.m_surface as ICore2DDrawingSurface).CreateNewDocument();
        }

        public void SelectAll()
        {
            this.m_surface.SelectAll ();
        }

        public void RefreshScene()
        {
            this.m_surface.RefreshScene ();
        }

        public void RefreshScene(bool forceUpdate)
        {
            this.m_surface.RefreshScene(forceUpdate);
        }

        public ICoreBrush[] GetBrushes()
        {
           return m_surface.GetBrushes ();
        }

        public CoreGraphicsPath GetPath()
        {
           return m_surface.GetPath();
        }

        public Matrix GetMatrix()
        {
           return  m_surface.GetMatrix();
        }

        public ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            return m_surface.GetBrush (enuBrushMode);
        }

        public Rectanglei GetDocumentBound()
        {
            return m_surface.GetDocumentBound ();
        }

        public void SetZoom(float zoomx, float zoomy)
        {
            m_surface.SetZoom (zoomx, zoomy);
        }

        public float GetZoom()
        {
           return m_surface.GetZoom ();
        }

        public Vector2f GetScreenLocation(Vector2f factorLocation)
        {
            return m_surface.GetScreenLocation (factorLocation);
        }

        public Vector2f GetFactorLocation(Vector2f screenLocation)
        {
            return m_surface.GetFactorLocation (screenLocation);
      }

        public Rectanglef GetScreenBound(Rectanglef zoomRectangle)
        {
            return this.GetScreenBound (zoomRectangle );
        }

        public void SetAttribute(string name, object value)
        {
            this.m_surface.SetAttribute(name, value);
        }

        public object GetAttribute(string name)
        {
            return this.m_surface.GetAttribute(name);
        }

        public bool AttributeExist(string name)
        {
            return this.m_surface.AttributeExist(name);
        }

        public bool IsToolValid(Type t)
        {
            return this.m_surface.IsToolValid (t);
        }
    }
}

