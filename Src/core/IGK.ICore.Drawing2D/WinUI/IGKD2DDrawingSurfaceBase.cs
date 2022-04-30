

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingSurfaceBase.cs
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
file:IGKD2DDrawingSurfaceBase.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.ICore.Drawing2D.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.IO;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.WinCore.Caret;
    using IGK.ICore.WinCore.WinUI.Controls;

    /// <summary>
    /// Represent the base IGKD2DDrawingSurface.
    /// </summary>
    public partial  class IGKD2DDrawingSurfaceBase : 
        IGKXWinCoreWorkingSurface,
        ICore2DDrawingSurface,
        ICoreWorkingFilemanagerSurface,
        ICoreWorkingInsertItemSurface,
        ICoreWorkingToolManagerSurface,
        ICoreWorkingRenderingSurface,
        ICoreWorkingPrintingSurface ,
        ICoreWorkingConfigElementSurface,
        ICoreWorkingRulesSurface,
        ICore2DDrawingRuleSurface,
        ICore2DDrawingPositionnableDocumentSurface   ,
        ICore2DDrawingObject ,
        ICore2DDrawingMultiFrameSurface
    {
        private IGKD2DDrawingDocumentCollections m_Documents; //document collection
        private Core2DDrawingDocumentBase  m_CurrentDocument; // current document
        private IGKD2DDrawingScene m_Scene; //current scnece
        private Colorf m_CurrentColor; // current Color
        private enuBrushMode m_BrushMode; //current brush mode
        private bool m_showRules; //show rule 
        private bool m_Saving;
        private IIGKD2DrawingSurfaceListener m_refresh_service;


        /// <summary>
        /// register 
        /// </summary>
        /// <param name="listener"></param>
        public virtual void OnRefreshServiceListener(IIGKD2DrawingSurfaceListener listener)
        {
            if (m_refresh_service != null)
                m_refresh_service.UnregisterService(this);
            if (listener != null)
            {
                listener.RegisterService(this);                
            }
            this.m_refresh_service = listener;
        }
        public override bool Focused
        {
            get
            {
                return this.m_Scene.Focused ;
            }
        }
     

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_Scene !=null) this.m_Scene.Dispose();
                foreach (IDisposable item in this.Documents)
                {
                    item.Dispose();
                }
            }
            base.Dispose(disposing);
        }
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
        /// determine if the surface can support a core message
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override bool CanProcess(ICoreMessage msg)
        {
            if (this.m_Scene.Handle == msg.HWnd)
            {
                return true;
            }
            return base.CanProcess(msg);
        }
        /// <summary>
        /// get the surface scene
        /// </summary>
        public IGKD2DDrawingScene Scene { 
            get{
                return m_Scene;
            }
        }
        /// <summary>
        /// get the current document
        /// </summary>
        public Core2DDrawingDocumentBase  CurrentDocument
        {
            get { return m_CurrentDocument; }
            set
            {
                if ((value != null) && (m_CurrentDocument != value) && ((this.m_Documents ==null) ||this.m_Documents.Contains(value)))
                {
                    CoreWorkingDocumentChangedEventArgs e = new CoreWorkingDocumentChangedEventArgs(this.m_CurrentDocument, value);
                    m_CurrentDocument = value;
                    OnCurrentDocumentChanged(e);
                }
            }
        }
        /// <summary>
        /// get if this current surface allow multi document edition
        /// </summary>
        public virtual  bool AllowMultiDocument
        {
            get
            {
                return true;
            }
        }
        /// <summary>
        /// refresh the current scene
        /// </summary>
        public void RefreshScene()
        {
            if (this.InvokeRequired) {
                this.Invoke((MethodInvoker)RefreshScene);
                return;
            }
            RefreshScene(true );
        }
        public virtual void RefreshScene(bool forceUpdate)
        {
            this.m_Scene.Invalidate();
            if (forceUpdate )
            {
                this.m_Scene.Update();
            }
        }
        void ICore2DDrawingSurface.RefreshScene(ICore2DDrawingLayeredElement element)
        {
            this.m_Scene.Invalidate();
        }
        #region Events
        public event CoreWorkingDocumentChangedEventHandler CurrentDocumentChanged;
        #endregion
        protected virtual void OnCurrentDocumentChanged(CoreWorkingDocumentChangedEventArgs e)
        {
            if (this.CurrentDocumentChanged != null)
                this.CurrentDocumentChanged(this, e);
        }
        /// <summary>
        /// get the documents collections
        /// </summary>
        public IGKD2DDrawingDocumentCollections Documents
        {
            get { return m_Documents; }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public IGKD2DDrawingSurfaceBase()
        {
            this.m_SurfaceManager = CreateSurfaceManager(); //surface manager
            this.m_Documents = CreateDocumentCollections();//document collection

            this.m_Scene = CreateScene();
            this.Title = CoreConstant.EMPTY_FILE;
            this.Controls.Add(this.m_Scene);
            this.CurrentTool = DefaultTool;
            //init surface brush
            this.m_FillBrush = new CoreBrush(this);
            this.m_FillBrush.SetSolidColor(Colorf.FromFloat(1.0f));
            this.m_StrokeBrush = new CorePen(this);
            this.m_StrokeBrush.SetSolidColor(Colorf.FromFloat(0.0f));

            this.CreateElementToConfigureManager();
            this.FileNameChanged += (o, e) => { this.Title = Path.GetFileName(this.FileName); };
            this.CurrentDocumentChanged += _CurrentDocumentChanged;

            new IGKD2DDrawingScrollManager(this);
            new IGKD2DDrawingRuleManager(this);
            this.m_NeedToSave = false;
        }

        protected virtual SurfaceDocumentManager CreateSurfaceManager()
        {
            return  new SurfaceDocumentManager(this);
        }

        /// <summary>
        /// override this method to create a new scene
        /// </summary>
        /// <returns></returns>
        protected virtual IGKD2DDrawingScene CreateScene()
        {
            return new IGKD2DDrawingScene(this);
        }
        protected override void InitLayout()
        {
            base.InitLayout();
        }
        void _CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            Core2DDrawingDocumentBase v_o = e.OldElement as Core2DDrawingDocumentBase;
            Core2DDrawingDocumentBase v_n = e.NewElement  as Core2DDrawingDocumentBase;
            if (v_o!=null )
                UnregisterDocumentEvent(v_o);
            if (v_n !=null)
                RegisterDocumentEvent(v_n);
            
        }

        
        protected virtual void UnregisterDocumentEvent(Core2DDrawingDocumentBase document)
        {
            document.PropertyChanged -= _documentPropertyChanged;
        }
        protected virtual void RegisterDocumentEvent(Core2DDrawingDocumentBase document)
        {
            document.PropertyChanged += _documentPropertyChanged;
        }

        private void _documentPropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.NeedToSave = true;
        }
        public virtual void SelectAll()
        {
            this.CurrentLayer.Select(this.CurrentLayer.Elements.ToArray());
        }
        protected virtual IGKD2DDrawingDocumentCollections CreateDocumentCollections(){
            return new IGKD2DDrawingDocumentCollections(this);
        }
      
        protected virtual Core2DDrawingDocumentBase CreateNewDocument()
        {
            return new Core2DDrawingLayerDocument("400px", "300px");
        }
        ICore2DDrawingDocument ICore2DDrawingSurface.CreateNewDocument()
        {
            return this.CreateNewDocument();
        }
        private string m_FileName;
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
                this.FileNameChanged(this, eventArgs);
        }
        public event EventHandler FileNameChanged;
        /// <summary>
        /// rename the file
        /// </summary>
        /// <param name="p"></param>
        public void RenameTo(string p)
        {
            if (File.Exists(this.FileName))
                File.Delete(this.FileName);
            this.FileName = p;
            this.Save();
        }
        public string GetDefaultFilter()
        {
            StringBuilder sb = new StringBuilder();
            string v_filter = CoreEncoderBase.GetFilter(CoreConstant.CAT_PICTURE);
            sb.Append("filter.allfiles".R()+"|" + CoreCodecUtils.GetExtensions(v_filter) );
            sb.Append("|"+v_filter);
            return sb.ToString();
        }
        /// <summary>
        /// get the save as information setting
        /// </summary>
        /// <returns></returns>
        public virtual ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo( "title.save2DdrawingSurfaceAs".R(),
            GetDefaultFilter(),
            this.FileName); 
        }
        private bool m_NeedToSave;
        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            set
            {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
                    OnNeedToSave(EventArgs.Empty);
                }
            }
        }
        private void OnNeedToSave(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }
        public event EventHandler NeedToSaveChanged;


        ///// <summary>
        ///// save to filename
        ///// </summary>
        ///// <param name="encoder"></param>
        ///// <param name="filename"></param>
        //protected bool SaveToFile(ICoreEncoder encoder, string filename)
        //{
        //    if (encoder == null)
        //        return false;
          
        //}
        public virtual  void Save()
        {
            ICoreEncoder v_enc = null;
            bool v_notifyf = false;
            if (System.IO.File.Exists(this.FileName))
            {
                ICoreCodec[] codec = CoreSystem.GetEncodersByExtension(System.IO.Path.GetExtension(FileName));
                if (codec.Length == 1)
                {
                    v_enc = codec[0] as ICoreEncoder;
                }
                else if (codec.Length >1){
                    var s = this .GetParam(CoreConstant.PARAM_CURRENT_ENCODER );
                   v_enc = CoreEncoderBase .GetEncoderById(s);
                    if (v_enc ==null)
                        v_notifyf = true ;
                }
                if (v_enc !=null){
                    this.Saving = true;
                    bool f = CoreEncoderBase.SaveToFile(this,
                        v_enc,
                        FileName,
                        this.Documents.ToArray());
                    if (f)
                    {
                        this.NeedToSave = false;
                        OnSaved(EventArgs.Empty);
                    }
                    this.Saving = false;
                }
                else if (v_notifyf )
                    CoreMessageBox.NotifyMessage("SaveAsRequired", "There are more than one codec to save this document. please use save As Button instead");
                }
            
            else
            {
                CoreSystem.Instance.Workbench.CallAction("File.SaveAs");
            }
        }
        public virtual  void SaveAs(string filename)
        {
            string v_folder = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty(this.FileName))
            {
                string v_dir = PathUtils.GetDirectoryName(filename);
                if (!string.IsNullOrWhiteSpace(v_dir))
                {
                    Environment.CurrentDirectory = v_dir;
                }
            }
            ICoreCodec[] codec = CoreSystem.GetEncodersByExtension(System.IO.Path.GetExtension(filename));
            this.Saving = true;
            bool r = false;
            switch (codec.Length)
            {
                case 1:
                    ICoreEncoder v_c = (codec[0] as ICoreEncoder);
                    if (v_c != null)
                    {
                       
                        r = CoreEncoderBase.SaveToFile(this,
                            v_c,
                            filename,
                            this.Documents.ToArray());
                    }
                    break;
                default:
                    if (codec.Length > 1)
                    {
                        //choose a codec to use for saving
                        CoreCodecSelector c = new CoreCodecSelector(codec);
                        if (Workbench.ConfigureWorkingObject(c,"title.choosecodec".R(),false, Size2i.Empty  ) == enuDialogResult.OK)
                        {
                            var v_m = c.SelectedCodec as ICoreEncoder;
                            if (v_m != null)
                            {
                                //r = CoreEncoderBase.SaveToFile(
                                //    this,
                                //     codec[0] as ICoreEncoder,
                                //     filename,
                                //     this.Documents.ToArray());
                                r = CoreEncoderBase.SaveToFile(
                            this,
                           v_m,
                             filename,
                             this.Documents.ToArray());
                            }
                            if (r)
                            {
                               this.SetParam("sys://CurrentEncoder", v_m.Id);
                            }
                        }
                    }
                    else
                    {
                        CoreMessageBox.NotifyMessage("title.CodecNotFound".R(),"MSG.UnableToSave.NoCodecFound".R());
                    }
                    break;
            }
            //restore folder 
            if (System.IO.Directory.Exists(v_folder))
                Environment.CurrentDirectory = v_folder;
            this.Saving = false;
            if (r)
            {
                this.FileName = filename;
                this.NeedToSave = false;
                this.OnSaved(EventArgs.Empty);
            }

        }
        public ICoreCaret CreateCaret()
        {
            if (this.m_Scene != null)
            {
                CaretManager c = new CaretManager(this.m_Scene, false);
                c.SetPosition(0, 0);
                return c;
            }
            return null;
        }
        ICore2DDrawingDocument ICore2DDrawingSurface.CurrentDocument
        {
            get
            {
                return this.CurrentDocument;
            }
            set
            {
                this.CurrentDocument = value as Core2DDrawingDocumentBase;
            }
        }
        public ICore2DDrawingLayer CurrentLayer
        {
            get
            {
                if (this.m_CurrentDocument !=null)
                    return this.m_CurrentDocument.CurrentLayer;
                return null;
            }
            set
            {
                this.m_CurrentDocument.CurrentLayer = value as Core2DDrawingLayer ;
            }
        }
 
        ICore2DDrawingDocumentCollections ICore2DDrawingSurface.Documents
        {
            get { return this.m_Documents; }
        }
        public ICoreBrush FillBrush
        {
            get { return this.m_FillBrush; }
        }
        public void Invalidate(ICore2DDrawingLayeredElement element)
        {
        }
        public ICorePen StrokeBrush
        {
            get { return this.m_StrokeBrush; }
        }
        public enuZoomMode ZoomMode
        {
            get
            {
                return this.m_Scene.ZoomMode;
            }
            set
            {
                this.m_Scene.ZoomMode = value;
            }
        }
        public event EventHandler ZoomModeChanged {
            add { this.m_Scene.ZoomModeChanged += value; }
            remove {
                this.m_Scene.ZoomModeChanged -= value;
            }
        }
        ICoreWorkingDocument ICoreWorkingDocumentManagerSurface.CurrentDocument
        {
            get
            {
                return this.CurrentDocument;
            }
            set
            {
                this.CurrentDocument = this.CurrentDocument as Core2DDrawingDocumentBase;
            }
        }
        public virtual ICoreBrush GetBrush(enuBrushMode enuBrushMode)
        {
            switch (enuBrushMode)
            {
                case enuBrushMode.Fill:
                    return this.FillBrush;                  
            }
            return this.StrokeBrush;
        }
        public Matrix GetMatrix()
        {
            if (this.m_Scene == null)
                return null;
            return this.m_Scene.GetMarix();
        }
        public CoreGraphicsPath GetPath()
        {
            return m_Scene.GetPath();
        }
        public virtual void Insert(ICoreWorkingObject obj)
        {
            new IGKD2DDrawingInsertMethod(this).Insert(obj);
        }
        public virtual void Remove(ICoreWorkingObject obj)
        {
            new IGKD2DDrawingInsertMethod(this).Remove(obj);
        }
        private Type m_CurrentTool;
        /// <summary>
        /// get or set the current tool
        /// </summary>
        public Type CurrentTool
        {
            get { return m_CurrentTool; }
            set
            {
                if ((value != null) && (value != this.m_CurrentTool))
                {
                    ICoreWorkingMecanism bck = this.m_Mecanism;
                    ICoreWorkingMecanism nmk = this.GetToolMecanism(value);
                    if (nmk != null)
                    {
                        if (bck != null)
                        {
                            bck.UnRegister();
                        }
                        this.m_Mecanism = nmk;
                        this.m_CurrentTool = value;
                        this.m_Mecanism.Register(this);
                        OnCurrentToolChanged(EventArgs.Empty);
                    }
                    //create new mecanism
                }
            }
        }

        public virtual ICoreWorkingMecanism GetToolMecanism(Type t)
        {
            if (IsToolValid(t))
            {
                Core2DDrawingGroupAttribute v_attr = Core2DDrawingObjectAttribute.GetCustomAttribute
                (t, typeof(Core2DDrawingGroupAttribute)) as Core2DDrawingGroupAttribute;
                Type v_t = v_attr.MecanismType;
                return v_t.Assembly.CreateInstance(v_t.FullName) as ICoreWorkingMecanism;
            }
            return null;
        }
        public virtual bool IsToolValid(Type t)
        {
            return false;
        }
        public event EventHandler CurrentToolChanged;
        ///<summary>
        ///raise the CurrentToolChanged 
        ///</summary>
        protected virtual void OnCurrentToolChanged(EventArgs e) =>
                CurrentToolChanged?.Invoke(this, e);
        
        public virtual Type DefaultTool
        {
            get { 
#if DEBUG
                return typeof(SelectionElement);
#else
                return typeof(SelectionElement);
#endif
            }
        }
        ICoreWorkingMecanism m_Mecanism;
        private ICorePen m_StrokeBrush;
        private ICoreBrush m_FillBrush;
        public ICoreWorkingMecanism Mecanism
        {
            get {
                return this.m_Mecanism;
            }
        }
        public ICoreSnippet CreateSnippet(ICoreWorkingMecanism  mecanism, int demand, int index, CoreSnippetRenderProc proc=null)
        {
            IGK2DDAnimatedSnippetLayerItem v_snippet = new IGK2DDAnimatedSnippetLayerItem(this, index, demand, mecanism, proc);
            this.m_Scene.SnippetLayerFrame.Add(v_snippet);
            return v_snippet; 
        }
        /// <summary>
        /// get the zoom x factor
        /// </summary>
        public float ZoomX
        {
            get { return this.m_Scene.ZoomX; }
        }
        /// <summary>
        /// get the zoom y factor
        /// </summary>
        public float ZoomY
        {
            get { return this.m_Scene.ZoomY; }
        }
        public Vector2f GetScreenLocation(Vector2f factorLocation)
        {
            return this.m_Scene.GetScreenLocation(factorLocation);
        }
        public IntPtr SceneHandle
        {
            get { return this.m_Scene.Handle; }
        }
        public virtual bool AddNewDocument()
        {
            if (this.CanAddDocument)
            {
                Core2DDrawingDocumentBase doc = this.CreateNewDocument();
                if (doc != null)
                {
                    this.Documents.Add(doc);
                    this.CurrentDocument = doc;
                    return true;
                }
            }
            return false;
        }
        public virtual bool CanAddDocument { get { return true; } }
        protected virtual void OnDocumentRemoved(CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            if (this.DocumentRemoved!=null)
            {
                this.DocumentRemoved(this, e);
            }
        }
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentRemoved;
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingDocument>> DocumentAdded;
        protected virtual void  OnDocumentAdded(CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            if (this.DocumentAdded !=null)
            {
                this.DocumentAdded(this, e);
            }
        }
        public virtual bool CanPrint {
            get {
                return false;
            }
        }
        public virtual   void Print()
        {
            throw new NotImplementedException();
        }
        public virtual void PrintPreview()
        {
            throw new NotImplementedException();
        }
        private ICoreWorkingObject m_ElementToConfigure;
        /// <summary>
        /// get or set the element to configure
        /// </summary>
        public ICoreWorkingObject ElementToConfigure
        {
            get { return m_ElementToConfigure; }
            set
            {
                if (m_ElementToConfigure != value)
                {
                    m_ElementToConfigure = value;
                    OnElementToConfigureChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ElementToConfigureChanged;
        ///<summary>
        ///raise the ElementToConfigureChanged 
        ///</summary>
        protected virtual void OnElementToConfigureChanged(EventArgs e)
        {
            CoreLog.WriteLine("ElementToConfigureChanged: " + this.ElementToConfigure?.ToString());
            if (ElementToConfigureChanged != null)
                ElementToConfigureChanged(this, e);
        }
        public Vector2f GetFactorLocation(Vector2f location)
        {
            return this.m_Scene.GetFactorLocation(location);
        }
        public Rectanglef GetScreenBound(Rectanglef zoomBound)
        {
            return this.m_Scene.GetScreenBound(zoomBound);
        }

        protected virtual void CreateElementToConfigureManager()
        {
        }

      
        
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new ControlCollection(this);
        }


     
        /// <summary>
        /// get or set the scene  position
        /// </summary>
        public float PosX
        {
            get { return this.m_Scene.PosX; }
            set { this.m_Scene.PosX = value; }
        }
        /// <summary>
        /// get or set the position y of the scene
        /// </summary>
        public float PosY
        {
            get { return this.m_Scene.PosY; }
            set { this.m_Scene.PosY = value; }
        }
        [Browsable(false)]
        public enuBrushSupport BrushSupport => enuBrushSupport.All;

        [Browsable(false)]
        public enuBrushMode BrushMode
        {
            get { return m_BrushMode; }
            set
            {
                if (m_BrushMode != value)
                {
                    m_BrushMode = value;
                    OnBrushModeChanged (EventArgs.Empty );
                }
            }
        }

        public event EventHandler BrushModeChanged;
        ///<summary>
        ///raise the BrushModeChanged 
        ///</summary>
        protected virtual void OnBrushModeChanged(EventArgs e)
        {
            if (BrushModeChanged != null)
                BrushModeChanged(this, e);
        }

        

        /// <summary>
        /// get or set the current color
        /// </summary>
        public Colorf CurrentColor
        {
            get { return m_CurrentColor; }
            set
            {
                if (!m_CurrentColor.Equals (value))
                {
                    m_CurrentColor = value;
                    OnCurrentColorChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CurrentColorChanged;
        ///<summary>
        ///raise the CurrentColorChanged 
        ///</summary>
        protected virtual void OnCurrentColorChanged(EventArgs e)
        {
            if (CurrentColorChanged != null)
                CurrentColorChanged(this, e);
        }

        
        ICore2DDrawingObject ICore2DDrawingObject.Parent 
        {
            get { 
                //no parent
                return null; 
            }
        }

        public ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[] { 
                this.FillBrush,
                this.StrokeBrush 
            };
        }

        protected internal virtual int RuleWidth { get {
            return 16;
        } }
        protected internal virtual int RuleHeight
        {
            get
            {
                return 16;
            }
        }
        protected internal virtual int ScrollWidth
        {
            get
            {
                return System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            }
        }
        protected internal virtual int ScrollHeight
        {
            get
            {
                return System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;
            }
        }
        public Rectanglei DisplayArea
        {
            get { 
                return this.m_Scene.DisplayArea; 
            }
        }

        public Rectanglei GetDocumentBound()
        {
            return new Rectanglei(0, 0, (int) this.CurrentDocument.Width,(int) this.CurrentDocument.Height);
        }

        public float GetZoom()
        {
            return (ZoomX + ZoomY) / 2.0f;
        }
        /// <summary>
        /// setup the zoom factor
        /// </summary>
        /// <param name="factorx"></param>
        /// <param name="factory"></param>
        public void SetZoom(float factorx, float factory)
        {
            this.m_Scene.SetZoom(factorx, factory);
        }


        public event EventHandler ZoomChanged {
            add {
                this.m_Scene.ZoomChanged += value;
            }
            remove {
                this.m_Scene.ZoomChanged -= value;
            }
        }
      
        public bool ShowScroll
        {
            get
            {
                return this.m_showScroll;
            }
            set
            {
                if (this.m_showScroll != value)
                {
                    this.m_showScroll = value;
                    OnShowScrollChanged(EventArgs.Empty);
                }
            }
        }


        public event EventHandler ShowScrollChanged;
        ///<summary>
        ///raise the ShowScrollChanged 
        ///</summary>
        protected virtual void OnShowScrollChanged(EventArgs e)
        {
            if (ShowScrollChanged != null)
                ShowScrollChanged(this, e);
        }


        private bool m_showScroll;

        public virtual bool AttributeExist(string name)
        {
            var p = GetType().GetProperty(name);
            return p != null;
        }

        public virtual object GetAttribute(string name)
        {
            var p = GetType().GetProperty(name);
            return p.GetValue(this);
        }

        public virtual void SetAttribute(string name, object value)
        {
            var p = GetType().GetProperty (name);
            if (p!=null)
                p.SetValue (this, value );
            
        }
        /// <summary>
        /// get or set if show the rules
        /// </summary>
        public bool ShowRules
        {
            get
            {
                return this.m_showRules;
            }
            set
            {
                if (this.m_showRules != value)
                {
                    this.m_showRules = value;
                    OnShowRuleChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ShowRuleChanged;
        private SurfaceDocumentManager m_SurfaceManager;
        ///<summary>
        ///raise the ShowRuleChanged 
        ///</summary>
        protected virtual void OnShowRuleChanged(EventArgs e)
        {
            if (ShowRuleChanged != null)
                ShowRuleChanged(this, e);
        }



        public virtual void ReloadFileFromDisk()
        {
             try
            {
                if (File.Exists(this.FileName) && !this.Saving )
                {
                    Core2DDrawingDocumentBase[] v_docs =
                        CoreDecoder.Instance.OpenFileDocument(this.FileName).ConvertTo<Core2DDrawingDocumentBase>();
                    if ((v_docs != null) && (v_docs.Length > 0))
                    {
                        if (this.InvokeRequired)
                        {
                            this.Invoke((MethodInvoker)delegate()
                            {
                                this.Documents.Replace(v_docs);
                                this.CurrentDocument = v_docs[0];
                                this.NeedToSave = false;
                            });
                        }
                        else
                        {
                            this.Documents.Replace(v_docs);
                            this.CurrentDocument = v_docs[0];
                            this.NeedToSave = false;
                        }
                    }
                }
            }
            catch(Exception ex) {
                CoreLog.WriteLine("Can't reload file from disk " + this.FileName + "\n" + ex.Message);
            }
        }

        internal void ScrollTo(float posX, float posY)
        {
            this.m_Scene.ScrollTo(posX, posY);
            
        }

        /// <summary>
        /// represent a surface owner control collections
        /// </summary>
        public new class ControlCollection : Control.ControlCollection
        {
            new IGKD2DDrawingSurfaceBase Owner
            {
                get
                {
                    return base.Owner as IGKD2DDrawingSurfaceBase;
                }
            }
            public ControlCollection(IGKD2DDrawingSurfaceBase ctrl)
                : base(ctrl)
            {

            }
            public override void Add(Control value)
            {
                if (value == null)
                    return; 
                if (value is IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene)
                {
                    base.Add(value);
                }
                else
                {
                    this.Owner.Scene.Controls.Add(value);
                }
            }
            public override void Remove(Control value)
            {
                if (this.Contains(value))
                {
                    base.Remove(value);
                }
                else
                {
                    this.Owner.Scene.Controls.Remove(value);
                }
            }
        }

        /// <summary>
        /// represent a drawing object container
        /// </summary>
        public class IGKD2DDrawingObjectContainer :
            Core2DDrawingObjectBase, 
            ICore2DDrawingObjectContainer, 
            IEnumerable
        {
            private Core2DDrawingObjectBase.Core2DDrawingObjectCollection m_objcol;
            private IGKD2DDrawingSurfaceBase m_surface;

            public IGKD2DDrawingObjectContainer(IGKD2DDrawingSurfaceBase surface)
            {
                this.m_objcol = new Core2DDrawingObjectCollection(this);
                this.m_surface = surface;
            }
            public Core2DDrawingObjectBase this[int index]
            {
                get
                {
                    return this.m_objcol[index];
                }
            }


            public int IndexOf(Core2DDrawingObjectBase obj)
            {
                return m_objcol.IndexOf(obj);
            }

            int ICore2DDrawingObjectContainer.IndexOf(ICore2DDrawingObject obj)
            {
                return m_objcol.IndexOf(obj as Core2DDrawingObjectBase);
            }
            public int Count { get { return this.m_objcol.Count; } }

            public IEnumerator GetEnumerator()
            {
                return this.m_objcol.GetEnumerator();
            }

            public void Clear()
            {
                this.m_objcol.Clear();
            }
            public bool Add(Core2DDrawingObjectBase item)
            {
                if ((item != null ) && !this.m_objcol.Contains (item))
                {
                    this.m_objcol.Add(item);
                    return true;
                }
                return false;
            }
            public bool Remove(Core2DDrawingObjectBase item)
            {
                if ((item != null) && this.m_objcol.Contains(item))
                {
                    this.m_objcol.Remove(item);
                    return true;
                }
                return false;
            }

            internal bool Contains(Core2DDrawingObjectBase value)
            {
                return this.m_objcol.Contains(value);
            }

            internal bool InsertAt(Core2DDrawingObjectBase item, int index)
            {
                return this.m_objcol.InsertAt (item, index );
            }

            internal bool MoveToEnd(Core2DDrawingObjectBase item)
            {
                return this.m_objcol.MoveToEnd (item);
            }           
            internal bool MoveToBegin(Core2DDrawingObjectBase item)
            {
                return this.m_objcol.MoveToBegin(item);
            }
            internal bool MoveToFront(Core2DDrawingObjectBase item)
            {
                return this.m_objcol.MoveToFront(item);
            }
            internal bool MoveToBack(Core2DDrawingObjectBase item)
            {
                return this.m_objcol.MoveToBack(item);
            }

            internal Core2DDrawingObjectBase[] ToArray()
            {
                return this.m_objcol.ToArray();
            }

            internal void AddRange(Core2DDrawingObjectBase[] items)
            {
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        this.Add(item);
                    }
                }
                
            }

            public T GetElementById<T>(string id) where T : class
            {
                return (T)this.GetElementById(id);
            }

            public object GetElementById(string id)
            {
                return CoreCollectionExtension.GetElementById ( this.m_objcol, id);                
            }

            public ICoreWorkingElementCollections Elements
            {
                get {
                    return this.m_objcol;
                 }
            }
        }


        /// <summary>
        /// represent the document collections
        /// </summary>
        public class IGKD2DDrawingDocumentCollections :
          ICoreWorkingDocumentCollections,
          ICore2DDrawingDocumentCollections,
          IEnumerable
        {
            private IGKD2DDrawingSurfaceBase m_surface;//surface that host
            private IGKD2DDrawingObjectContainer m_container; //collection list 
            /// <summary>
            /// get the owner
            /// </summary>
            protected IGKD2DDrawingSurfaceBase Owner { get { return this.m_surface; } }
            /// <summary>
            /// get the container
            /// </summary>
            protected IGKD2DDrawingObjectContainer Container { get { return this.m_container; } }

            public bool AllowMultiDocument
            {
                get
                {
                    return this.m_surface.AllowMultiDocument;
                }
            }
            public int Count
            {
                get
                {
                    return m_container.Count;
                }
            }
            
            public IGKD2DDrawingDocumentCollections(IGKD2DDrawingSurfaceBase surface)
            {
                this.m_surface = surface;
                this.m_container = new IGKD2DDrawingObjectContainer(surface);                
                this.InitDocumentCollection();                
            }

            protected  virtual void InitDocumentCollection()
            {
                Core2DDrawingDocumentBase v_doc = this.Owner.CreateNewDocument();
                Debug.Assert(v_doc != null, "Document not created");
                this.m_container.Add(v_doc);
                this.m_surface.CurrentDocument = v_doc;
            }

            void ICoreWorkingDocumentCollections.Add(ICoreWorkingDocument document)
            {
                this.Add(document as Core2DDrawingDocumentBase);
            }
            private int IndexOf(Core2DDrawingDocumentBase document)
            {
                return this.m_container.IndexOf(document);
            }
            /// <summary>
            /// add document to the surface
            /// </summary>
            /// <param name="document"></param>
            public virtual void Add(Core2DDrawingDocumentBase document)
            {
                if ((document != null) && (!this.Contains(document)))
                {
                    if (this.m_container.Add(document))
                    {
                        this.m_surface.OnDocumentAdded(new CoreItemEventArgs<ICore2DDrawingDocument>(document));
                    }
                }
            }
            /// <summary>
            /// remove document form this surface
            /// </summary>
            /// <param name="document"></param>
            public virtual void Remove(Core2DDrawingDocumentBase document)
            {
                if (this.Contains(document) && (this.Count > 1))
                {
                    if (this.m_container.Remove(document))
                    {
                        this.m_surface.OnDocumentRemoved(
                            new CoreItemEventArgs<ICore2DDrawingDocument>(document)
                            );
                            
                    }
                }
            }
            void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.m_surface.NeedToSave = true;
            }
            int ICoreWorkingDocumentCollections.Count
            {
                get { return this.m_container.Count; }
            }
            int ICoreWorkingDocumentCollections.IndexOf(ICoreWorkingDocument document)
            {
                return this.IndexOf(document as Core2DDrawingDocumentBase);
            }
            void ICoreWorkingDocumentCollections.MoveToBack(ICoreWorkingDocument document)
            {
                this.MoveToBack(document as Core2DDrawingDocumentBase);
            }

        
         
            void ICoreWorkingDocumentCollections.MoveToBegin(ICoreWorkingDocument document)
            {
                this.MoveToBegin(document as Core2DDrawingDocumentBase);
            }
            void ICoreWorkingDocumentCollections.MoveToEnd(ICoreWorkingDocument document)
            {
                this.MoveToEnd(document as Core2DDrawingDocumentBase);
            }
            void ICoreWorkingDocumentCollections.MoveToFront(ICoreWorkingDocument document)
            {
                this.MoveToFront (document as Core2DDrawingDocumentBase);
            }
            void ICoreWorkingDocumentCollections.Remove(ICoreWorkingDocument document)
            {
                this.Remove(document as Core2DDrawingDocumentBase);
            }
            ICoreWorkingDocument[] ICoreWorkingDocumentCollections.ToArray()
            {
                return this.m_container.ToArray().ConvertTo<ICoreWorkingDocument >();
            }
            ICoreWorkingDocument ICoreWorkingDocumentCollections.this[int index]
            {
                get { return this[index]; }
            }
            void ICore2DDrawingDocumentCollections.Add(ICore2DDrawingDocument document)
            {
                this.Add(document as Core2DDrawingDocumentBase);
            }
            int ICore2DDrawingDocumentCollections.IndexOf(ICore2DDrawingDocument document)
            {
                return this.IndexOf(document as Core2DDrawingDocumentBase);
            }
            void ICore2DDrawingDocumentCollections.InsertAt(int p, ICore2DDrawingDocument Document)
            {
                this.InsertAt(Document as Core2DDrawingDocumentBase, p);
            }
            /// <summary>
            /// insert document at position
            /// </summary>
            /// <param name="document"></param>
            public void InsertAt(Core2DDrawingDocumentBase document, int index)
            {
                this.m_container.InsertAt(document, index);
            }
            private void MoveToFront(Core2DDrawingDocumentBase document)
            {
                int i = this.IndexOf(document);
                if (this.m_container.MoveToFront(document))
                {
                    this.m_surface.OnDocumentZIndexChanged(
                          new CoreWorkingObjectZIndexChangedEventArgs(
                                  document,
                                  i,
                                  document.ZIndex));
                }
            }
            private void MoveToBegin(Core2DDrawingDocumentBase document)
            {
                int i = this.IndexOf(document);
                if (this.m_container.MoveToBegin(document))
                {
                    this.m_surface.OnDocumentZIndexChanged(
                          new CoreWorkingObjectZIndexChangedEventArgs(
                                  document,
                                  i,
                                  document.ZIndex));
                }
            }
            private void MoveToEnd(Core2DDrawingDocumentBase document)
            {
                int i = this.IndexOf(document);

                if (this.m_container.MoveToEnd(document))
                {
                    this.m_surface.OnDocumentZIndexChanged(
                          new CoreWorkingObjectZIndexChangedEventArgs(
                                  document,
                                  i,
                                  document.ZIndex));
                }
            }
            private void MoveToBack(Core2DDrawingDocumentBase document)
            {
                int i = this.IndexOf(document);
                if (this.m_container.MoveToBack(document))
                {
                    this.m_surface.OnDocumentZIndexChanged(
                          new CoreWorkingObjectZIndexChangedEventArgs(
                                  document,
                                  i,
                                  document.ZIndex));

                }
            }
            void ICore2DDrawingDocumentCollections.MoveToBack(ICore2DDrawingDocument document)
            {
                this.MoveToBack(document as Core2DDrawingDocumentBase);
            }
           
            
          

            void ICore2DDrawingDocumentCollections.MoveToBegin(ICore2DDrawingDocument document)
            {
                this.MoveToBegin(document as Core2DDrawingDocumentBase);
            }
            void ICore2DDrawingDocumentCollections.MoveToEnd(ICore2DDrawingDocument document)
            {
                this.MoveToEnd(document as Core2DDrawingDocumentBase );
            }
            void ICore2DDrawingDocumentCollections.MoveToFront(ICore2DDrawingDocument document)
            {
                this.MoveToFront(document as Core2DDrawingDocumentBase);
            }
            void ICore2DDrawingDocumentCollections.Remove(ICore2DDrawingDocument document)
            {
                this.Remove(document as Core2DDrawingDocumentBase);
            }

            ICore2DDrawingDocument[] ICore2DDrawingDocumentCollections.ToArray()
            {
                return this.m_container.ToArray().ConvertTo<ICore2DDrawingDocument>();
            }
            ICore2DDrawingDocument ICore2DDrawingDocumentCollections.this[int index]
            {
                get { return this[index]; }
            }
            internal bool Contains(Core2DDrawingDocumentBase value)
            {
                return this.m_container.Contains(value);
            }
            public Core2DDrawingDocumentBase this[int index]
            {
                get
                {
                    return this.m_container[index] as Core2DDrawingDocumentBase;
                }
            }
            public Core2DDrawingDocumentBase[] ToArray()
            {
                return this.m_container.ToArray().ConvertTo<Core2DDrawingDocumentBase>();
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_container.GetEnumerator();
            }
            /// <summary>
            /// represent with working documents. used on initialization context
            /// </summary>
            /// <param name="documents"></param>
            public virtual void Replace(Core2DDrawingDocumentBase[] documents)
            {
                foreach (IDisposable item in this.m_container)
                {
                    if (item != null)
                    {
                        item.Dispose();
                    }
                }
                this.m_container.Clear();
                this.m_container.AddRange(documents);//******without raising event***
                this.m_surface.CurrentDocument = documents[0];
            }
        }

        /// <summary>
        /// manage setting of the surface for document
        /// </summary>
        public class SurfaceDocumentManager
        {
            private IGKD2DDrawingSurfaceBase m_surface;
            public SurfaceDocumentManager(IGKD2DDrawingSurfaceBase surface )
            {
                this.m_surface = surface;
                this.m_surface.DocumentAdded += m_surface_DocumentAdded;
                this.m_surface.DocumentRemoved += m_surface_DocumentRemoved;
                this.m_surface.CurrentDocumentChanged += m_surface_CurrentDocumentChanged;
            }

            void m_surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
            {
                UnRegisterDocumentEvent(e.OldElement  as Core2DDrawingDocumentBase);
                RegisterDocumentEvent(e.NewElement as Core2DDrawingDocumentBase);
            }
            protected void UnRegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                if (document == null) return;
                document.PropertyChanged -= document_PropertyChanged;
            }
            protected void RegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                if (document == null) return;
                document.PropertyChanged += document_PropertyChanged;
            }

            void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.m_surface.NeedToSave = true;
            }
            void m_surface_DocumentRemoved(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
            {
                
                this.m_surface.NeedToSave = true;
                this.m_surface.CurrentDocument = this.m_surface.Documents[0];
            }

            void m_surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
            {
                this.m_surface.NeedToSave = true;
            }
        }

        public event CoreWorkingObjectZIndexChangedHandler DocumentZIndexChanged;

        protected virtual void OnDocumentZIndexChanged(CoreWorkingObjectZIndexChangedEventArgs e)
        {
            
            if (this.DocumentZIndexChanged != null)
            { 
                this.DocumentZIndexChanged(this, e);
            }
        }



        public ICore2DDrawingFrameRendererCollections Frames
        {
            get { return this.m_Scene.Frames; }
        }

        public ICore2DDrawingFrameRendererCollections OverlayFrames
        {
            get { return this.m_Scene.OverlayFrames; }
        }
    }
}

