

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFHostSurface.cs
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
file:WPFHostSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Windows.Forms;
using System.Windows.Documents;
using System.Windows;
using System.Windows.Markup;
using System.Xml;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects;
    using IGK.DrSStudio.Drawing2D;
    [CoreSurface(WPFConstant.SURFACE_NAME,
        EnvironmentName = WPFConstant.SURFACE_ENVIRONMENT )]
    public class WPFHostSurface : 
        IGKXWinCoreWorkingSurface ,
        ICoreWorkingSurface ,
        ICoreWorkingRecordableSurface ,
        ICoreBrushOwner ,
        ICoreWorkingToolManagerSurface 
    {
        private System.Windows.Forms.Integration.ElementHost c_elementHost1;        
        private Colorf m_currentColor;
        private IWPFDocumentCollections m_documents;
        private System.Windows.Controls.Canvas m_SurfaceLayer;
        private System.Windows.Controls.Canvas m_SelectionLayer;
        private System.Windows.Controls.Canvas m_SnippetLayer;
        private System.Windows.Controls.Border m_BorderLayer;
        private IGKXVerticalScrollBar  c_VScroll;
        private IGKXHorizontalScrollBar c_HScroll;
        private bool m_ShowScroll;
        private double m_ZoomX = 1.0;
        private double m_ZoomY = 1.0;
        public ICoreBrush[] GetBrushes()
        {
            return null;
        }
        public double ZoomY
        {
            get { return m_ZoomY; }
            set
            {
                if (m_ZoomY != value)
                {
                    m_ZoomY = value;
                }
            }
        }
        public double ZoomX
        {
            get { return m_ZoomX; }
            set
            {
                if (m_ZoomX != value)
                {
                    m_ZoomX = value;
                }
            }
        }
        public Rectanglef GetDefaultBound()
        {
            return this.DisplayRectangle;
        }
        /// <summary>
        /// get the surface layer
        /// </summary>
        public System.Windows.Controls.Canvas SurfaceLayer { get { return this.m_SurfaceLayer; } }
        /// <summary>
        /// get the surface sniptter layer
        /// </summary>
        public System.Windows.Controls.Canvas SnippetLayer { get { return this.m_SnippetLayer ; } }
        public bool ShowScroll
        {
            get { return m_ShowScroll; }
            set
            {
                if (m_ShowScroll != value)
                {
                    m_ShowScroll = value;
                    this.c_VScroll.Visible = value;
                    this.c_HScroll.Visible = value;
                    this.InitBound();
                    OnShowScrollChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ShowScrollChanged;
        protected virtual void OnShowScrollChanged(EventArgs eventArgs)
        {
            if (this.ShowScrollChanged != null)
                this.ShowScrollChanged(this, eventArgs);
        }
        public  IGKXHorizontalScrollBar HScroll
        {
            get { return c_HScroll; }
        }
        public  IGKXVerticalScrollBar VScroll
        {
            get { return c_VScroll; }
        }
        private WPFDocument m_CurrentDocument;
        public WPFDocument CurrentDocument
        {
            get { return m_CurrentDocument; }
            set
            {
                if ((m_CurrentDocument != value)&& (value !=null))
                {
                    CoreWorkingElementChangedEventArgs<WPFDocument> e =
                        new CoreWorkingElementChangedEventArgs<WPFDocument>(m_CurrentDocument, value);
                    m_CurrentDocument = value;                  
                    OnCurrentDocumentChanged(e);
                }
            }
        }
        public IWPFDocumentCollections Documents {
            get {
                return this.m_documents;
            }
        }
        public event EventHandler<CoreWorkingElementChangedEventArgs< WPFDocument>> CurrentDocumentChanged;
        ///<summary>
        ///raise the CurrentDocumentChanged 
        ///</summary>
        protected virtual void OnCurrentDocumentChanged(CoreWorkingElementChangedEventArgs<WPFDocument > e)
        {
            if (this.m_BorderLayer != null)
            {
                this.m_BorderLayer.Child = null;
                this.m_BorderLayer.Child = e.NewElement.RootElement;            
            }
            if (CurrentDocumentChanged != null)
                CurrentDocumentChanged(this, e);
        }
        public virtual IWPFSnippet  CreateSnippet(WPFBaseMecanism mecanism, int index, int Demand)
        {
            WPFSnippet sn = new WPFSnippet(m_SnippetLayer,mecanism, index, Demand);
            return sn;
        }
        public System.Windows.UIElement RootElement
        {
            get { return this.CurrentDocument .RootElement; }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            //translate focus to child
            base.OnGotFocus(e);
            this.RootElement.Focus();
        }
        public WPFHostSurface()
        {
            this.InitializeComponent();
            this.InitControl();
            this.m_projects = CreateProject();
            this.m_documents = CreateDocumentCollections();
            this.m_FillBrush = new IGK.DrSStudio.Drawing2D.CoreBrush(this);
            this.m_StrokeBrush = new IGK.DrSStudio.Drawing2D.CorePen(this);
            this.m_SurfaceLayer = new System.Windows.Controls.Canvas();
            this.m_SnippetLayer = new Canvas();
            this.m_SurfaceLayer.Focusable = false;
            this.m_SurfaceLayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            this.m_SurfaceLayer.VerticalAlignment = VerticalAlignment.Center;
            this.m_SelectionLayer = new System.Windows.Controls.Canvas();
            this.m_SelectionLayer.Focusable = false;
            this.m_SelectionLayer.SetValue(
              Canvas.ZIndexProperty, 100);
            System.Windows.Controls.Canvas c_canvas =
                 this.CurrentDocument.RootElement as System.Windows.Controls.Canvas;
            c_canvas.Background = Brushes.White;
            c_canvas.Width = 400;
            c_canvas.Height = 300;
            //add the too event
            //this.m_SurfaceLayer.Children.Add(c_canvas);
            //this.m_SurfaceLayer.Children.Add (this.m_SelectionLayer );
            m_BorderLayer = new Border();
            m_BorderLayer.BorderBrush = Brushes.Black;
            m_BorderLayer.BorderThickness = new Thickness(1);
            m_BorderLayer.Child = c_canvas;
            m_BorderLayer.VerticalAlignment = VerticalAlignment.Center;
            m_SurfaceLayer.Children.Add(m_BorderLayer);
            m_SurfaceLayer.Children.Add(m_SelectionLayer);
            m_SurfaceLayer.Children.Add(m_SnippetLayer);
            m_SurfaceLayer.Background = Brushes.White;
            m_SurfaceLayer.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
            m_SurfaceLayer.VerticalAlignment = VerticalAlignment.Stretch;
            m_SurfaceLayer.Width = 0;
            m_SurfaceLayer.Height = 0;
            this.c_elementHost1.Child = m_SurfaceLayer;
            this.c_elementHost1.BackColorTransparent = true;
            this.c_elementHost1.BackgroundImage = CoreSystem.GetDocumentImage("dash");
            this.c_elementHost1.Select();
            this.CurrentTool = DefaultTool;
            this.FileName = "Empty.xaml";
            this.RootElement.Focus();
            RegisterCanvasEvent();
            this.InitManager();
        }
        private void InitControl()
        {
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            c_HScroll = new IGKXHorizontalScrollBar();
            c_VScroll = new IGKXVerticalScrollBar();
            this.Controls.Add(c_VScroll);
            this.Controls.Add(c_HScroll);
            this.SizeChanged += new EventHandler(_SizeChanged);
            this.ShowScroll = true;
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            InitBound();
        }
        private void InitBound()
        {
            if (this.ShowScroll)
            {
                int w = System.Windows.Forms .SystemInformation.VerticalScrollBarWidth ;
                int h = System.Windows.Forms .SystemInformation.HorizontalScrollBarHeight ;
                this.c_HScroll.Bounds = new System.Drawing.Rectangle(
                    0, this.Height - h,
                    this.Width - w,
                    h);
                this.c_VScroll.Bounds = new System.Drawing.Rectangle(
                    this.Width - w,
                    0,
                    w,
                    this.Height - h
                    );
            }
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                System.Drawing.Rectangle rc = base.DisplayRectangle;
                if (this.ShowScroll)
                {
                    rc.Width -= System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
                    rc.Height -= System.Windows.Forms.SystemInformation.HorizontalScrollBarHeight;
                }
                return rc;
            }
        }
        protected virtual void InitManager()
        {
            new LayerManagerSurface(this);
            new ConfigObjectManager(this);
        }
        protected virtual IWPFDocumentCollections CreateDocumentCollections()
        {
            return new WPFDocumentCollections(this);
        }
        protected virtual WPFDocument CreateNewDocument()
        {
            return new WPFDocument();
        }
        protected virtual void RegisterCanvasEvent()
        {
            this.RootElement.Focusable = true;
            System.Windows.UIElement  c = this.m_SurfaceLayer as System.Windows.Controls.Canvas;
            c.MouseMove += new System.Windows.Input.MouseEventHandler(_canvas_MouseMove);
            c.MouseUp += new System.Windows.Input.MouseButtonEventHandler(_canvas_MouseUp);
            c.MouseDown += new System.Windows.Input.MouseButtonEventHandler(_canvas_MouseDown);
            c.KeyDown += new System.Windows.Input.KeyEventHandler(_canvas_KeyDown);
            c.KeyUp += new System.Windows.Input.KeyEventHandler(_canvas_KeyUp);           
        }
        protected virtual void UnRegisterCanvasEvent()
        {
            System.Windows.Controls.Canvas c = this.m_SurfaceLayer  as System.Windows.Controls.Canvas;
           c.Focusable = true;
           c.MouseMove -= new System.Windows.Input.MouseEventHandler(_canvas_MouseMove);
           c.MouseUp -= new System.Windows.Input.MouseButtonEventHandler(_canvas_MouseUp);
           c.MouseDown -= new System.Windows.Input.MouseButtonEventHandler(_canvas_MouseDown);
           c.KeyDown -= new System.Windows.Input.KeyEventHandler(_canvas_KeyDown);
           c.KeyUp -= new System.Windows.Input.KeyEventHandler(_canvas_KeyUp);
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            return true;
        }
        void _canvas_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnCanvasKeyDown(e);
        }
        protected virtual void OnCanvasKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (this.CanvasKeyDown != null)
                this.CanvasKeyDown(this, e);
        }
        void _canvas_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (this.CanvasKeyUp != null)
                this.CanvasKeyUp(this, e);
        }
        void _canvas_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnCanvasMouseDown( new WPFMouseButtonEventArgs( this.RootElement as IInputElement , e));
        }
        void _canvas_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OnCanvasMouseUp(new WPFMouseButtonEventArgs(this.RootElement as IInputElement, e));
        }
        void _canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            OnCanvasMouseMove(new WPFMouseEventArgs(this.RootElement as IInputElement, e));
        }
        public event WPFMouseEventHandler CanvasMouseMove;
        public event WPFMouseButtonEventHandler CanvasMouseDown;
        public event WPFMouseButtonEventHandler CanvasMouseUp;
        public event System.Windows.Input.KeyEventHandler CanvasKeyUp;
        public event System.Windows.Input.KeyEventHandler CanvasKeyDown;
        private void OnCanvasMouseMove(WPFMouseEventArgs e)
        {
            if (CanvasMouseMove != null)
                this.CanvasMouseMove(this, e);
        }
        protected virtual void OnCanvasMouseUp(WPFMouseButtonEventArgs e)
        {
            if (CanvasMouseUp != null)
                this.CanvasMouseUp(this, e);
        }
        protected virtual void OnCanvasMouseDown(WPFMouseButtonEventArgs e)
        {
            if (CanvasMouseDown != null)
                this.CanvasMouseDown(this, e);
        }
        public override string ToString()
        {
            return WPFConstant.SURFACE_NAME;
        }
        private void InitializeComponent()
        {
            this.c_elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.c_elementHost1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_elementHost1.Location = new System.Drawing.Point(0, 0);
            this.c_elementHost1.Name = "elementHost1";
            this.c_elementHost1.Size = new System.Drawing.Size(150, 150);
            this.c_elementHost1.TabIndex = 0;
            this.c_elementHost1.Text = "elementHost1";
            this.c_elementHost1.Child = null;
            // 
            // WPFSurfaceHostSurface
            // 
            this.Controls.Add(this.c_elementHost1);
            this.Name = "WPFSurfaceHostSurface";
            this.ResumeLayout(false);
        }
        #region ICoreWorkingSurface Members
        public override string SurfaceEnvironment
        {
            get {
                return WPFConstant.SURFACE_ENVIRONMENT ;
            }
        }
        public override string DisplayName
        {
            get { return WPFConstant.SURFACE_NAME ; }
        }
        #endregion
        #region ICoreWorkingToolManagerSurface Members
        private Type m_CurrentTool;
        private ICoreWorkingMecanism m_Mecanism;
        public Type DefaultTool
        {
            get { return typeof(WorkingObjects.WPFSelectionElement); }
        }
        public Type CurrentTool
        {
            get
            {
                return m_CurrentTool;
            }
            set
            {
                if ((value != null) && (value != this.m_CurrentTool))
                {
                    ICoreWorkingMecanism bck = this.m_Mecanism;
                    ICoreWorkingMecanism nmk = this.IsToolValid(value);
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
                //if (this.m_CurrentTool != value)
                //{
                //    this.m_CurrentTool = value;
                //    OnCurrentToolChanged(EventArgs.Empty);
                //}
            }
        }
        public virtual ICoreWorkingMecanism IsToolValid(Type t)
        {
            WPFElementAttribute v_attr =
                //CoreSystem.Instance.WorkingObjects.GetAttribute(t) as WPFElementAttribute ;
                WPFElementAttribute.GetCustomAttribute
                (t, typeof(WPFElementAttribute)) as WPFElementAttribute;
            if (v_attr == null)
                return null;
            if (v_attr.MecanismType == null)
                return null;
            Type v_t = v_attr.MecanismType;
            return v_t.Assembly.CreateInstance(v_t.FullName) as ICoreWorkingMecanism;
        }
        private void OnCurrentToolChanged(EventArgs eventArgs)
        {
            if (this.CurrentToolChanged != null)
                this.CurrentToolChanged(this, eventArgs);
        }
        public ICoreWorkingMecanism Mecanism
        {
            get { return this.m_Mecanism; }
        }
        public event EventHandler CurrentToolChanged;
        #endregion
        #region ICoreWorkingPrintingSurface Members
        public void Print()
        {
        }
        public void PrintPreview()
        {
        }
        #endregion
        #region ICoreWorkingColorSurface Members
        public Colorf CurrentColor
        {
            get
            {
                return this.m_currentColor;
            }
            set
            {
                if (!this.m_currentColor.Equals(value))
                {
                    this.m_currentColor = value;
                    OnCurrentColorChanged(EventArgs.Empty);
                }
            }
        }
        private void OnCurrentColorChanged(EventArgs eventArgs)
        {
            if (CurrentColorChanged != null)
                CurrentColorChanged(this, eventArgs);
        }
        public event EventHandler CurrentColorChanged;
        #endregion
        #region ICoreWorkingFilemanagerSurface Members
        string m_filename;
        public string FileName
        {
            get { return m_filename; }
            set {
                this.m_filename = value;
                OnFileNameChanged(EventArgs.Empty);
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
        public void RenameTo(string p)
        {
        }
        #endregion
        #region ICoreWorkingRecordableSurface Members
        bool m_needToSave;
        public bool NeedToSave
        {
            get { return m_needToSave; }
            set { m_needToSave = value; OnNeedToSave(EventArgs.Empty); }
        }
        protected virtual void OnNeedToSave(EventArgs eventArgs)
        {
            if (NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }
        public event EventHandler NeedToSaveChanged;
        public void Save()
        {
            if (string.IsNullOrEmpty(this.FileName) || !System.IO.File.Exists(this.FileName))
            {
                SaveAs(this.FileName);
            }
            else {
                SaveTo(FileName);
            }
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
             StringBuilder sb = new StringBuilder();
                StringBuilder sb1 = new StringBuilder();
                sb.Append("XAML file | *.xaml; | "+ IGK.DrSStudio.Codec.CoreEncoder .Instance.GetFilter ());
                foreach(ICoreEncoder enc in  IGK.DrSStudio.Codec.CoreEncoderBase.GetEncoders ())
                {
                    if (enc is IGK.DrSStudio.Drawing2D.Codec.ICoreBitmapEncoder)
                    {                        
                        sb1.Append("|"+enc.GetFilter());
                    }
                }
                sb.Append (sb1 .ToString());
                return new CoreSaveAsInfo("Save WPFSurface",
                    sb.ToString(),
                    this.FileName);
        }
        public void SaveAs(string filename)
        {
              SaveTo(filename);               
        }
        private void SaveTo(string filename)
        {
            ICoreCodec[] codec = CoreSystem.GetEncoders(System.IO.Path.GetExtension(filename));
            switch (codec.Length)
            {
                case 1:
                    this.SaveTo(filename, codec[0]);
                    this.FileName = filename;
                    this.NeedToSave = false;
                    break;
                default:
                    if (codec.Length > 1)
                    {
                        //choose a codec to use for saving
                        CoreCodecSelector c = new CoreCodecSelector(codec);
                        if (Workbench.ConfigureWorkingObject(c).Equals (enuDialogResult.OK))
                        {
                            if (c != null)
                            {
                                this.SaveTo(filename, c.SelectedCodec as ICoreEncoder);
                                this.FileName = filename;
                                this.NeedToSave = false;
                            }
                        }
                    }
                    else
                    {
                        CoreServices.ShowError(CoreSystem.GetString("MSG.UnableToSave.NoCodecFound"));
                    }
                    break;
            }
        }
        private void SaveTo(string filename, ICoreCodec iCoreCodec)
        {
            ICoreEncoder v_c = iCoreCodec as ICoreEncoder ;
            if (v_c == null) return;
            if (v_c is IGK.DrSStudio.Drawing2D.Codec.ICoreBitmapEncoder)
            {
                (v_c as IGK.DrSStudio.Drawing2D.Codec.ICoreBitmapEncoder).SavePicture(
                    filename, Utils.BitmapUtils.GetBitmap(this.RootElement));
            }
            else
            {
                IGK.DrSStudio.WPFSurfaceAddIn.Codec.XAMLEncoder c =
                    v_c as IGK.DrSStudio.WPFSurfaceAddIn.Codec.XAMLEncoder;
                if (c != null)
                    c.Save(this, filename, null);
                else
                    v_c.Save(this, filename, this.Documents.ToArray());
            }
        }
        #endregion
        internal void LoadObject(object obj)
        {
            XAMLFileElement f = XAMLFileElement.Create(obj as UIElement);
            if (f != null)
            {
                this.CurrentDocument.CurrentLayer.Clear();
                this.CurrentDocument.CurrentLayer.Elements.Add(f);
                this.CurrentDocument.Width = (double)f.Shape.GetValue(System.Windows.Controls.Canvas.WidthProperty);
                this.CurrentDocument.Height  =(double ) f.Shape.GetValue(System.Windows.Controls.Canvas.HeightProperty);
            }
        }
        /// <summary>
        /// represent the wpf element collection
        /// </summary>
        public class WPFDocumentCollections : 
            System.Collections.IEnumerable,
            IWPFDocumentCollections
        {
            private List<WPFDocument> m_List;
            private WPFHostSurface m_Owner;
            public WPFHostSurface Owner
            {
                get { return m_Owner; }            
            }
            public int Count
            {
                get { return this.m_List.Count ; }
            }
            public WPFDocument this[int index] {
                get {
                    if ((index >= 0) &&(index < Count ))
                    return this.m_List[index];
                    return null;
                }
            }
            public WPFDocument[] ToArray()
            {
                return this.m_List.ToArray();
            }
            public int IndexOf(WPFDocument document)
            { 
                if (document == null)
                    return -1;
                return this.m_List.IndexOf(document);
            }
            public WPFDocumentCollections(WPFHostSurface owner)
            {
                this.m_List = new List<WPFDocument>();
                this.m_Owner = owner;
                this.m_List.Add(owner.CreateNewDocument());
                this.m_Owner .CurrentDocument = this.m_List[0];
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_List.GetEnumerator();
            }
            #endregion
            #region ICoreWorkingDocumentCollections Members
            ICoreWorkingDocument ICoreWorkingDocumentCollections.this[int index]
            {
                get { return this[index]; }
            }
            void ICoreWorkingDocumentCollections.Add(ICoreWorkingDocument document)
            {
                this.Add(document as WPFDocument);
            }
            void ICoreWorkingDocumentCollections.Remove(ICoreWorkingDocument document)
            {
                this.Remove(document as WPFDocument);
            }
            void ICoreWorkingDocumentCollections.MoveToFront(ICoreWorkingDocument document)
            {
                this.MoveToFront(document as WPFDocument);
            }
            void ICoreWorkingDocumentCollections.MoveToBack(ICoreWorkingDocument document)
            {
                this.MoveToBack(document as WPFDocument);
            }
            void ICoreWorkingDocumentCollections.MoveToBegin(ICoreWorkingDocument document)
            {
                this.MoveToBegin(document as WPFDocument);
            }
            void ICoreWorkingDocumentCollections.MoveToEnd(ICoreWorkingDocument document)
            {
                this.MoveToEnd(document as WPFDocument);
            }
            ICoreWorkingDocument[] ICoreWorkingDocumentCollections.ToArray()
            {
                return this.ToArray();
            }
            int ICoreWorkingDocumentCollections.IndexOf(ICoreWorkingDocument document)
            {
                return this.IndexOf(document as WPFDocument);
            }
            #endregion
            #region IWPFDocumentCollections Members
            public void Add(WPFDocument document)
            {
                if ((document == null) || 
                this.m_List.Contains(document))
                {
                    return ;
                }
                this.m_List.Add(document);
                this.m_Owner.OnDocumentAdded( new WPFElementEventArgs<WPFDocument>(document));
            }
            void IWPFDocumentCollections.Remove(IWPFDocument document)
            {
                this.Remove(document as WPFDocument);   
            }
            IWPFDocument IWPFDocumentCollections.this[int index]
            {
                get { return m_List[index]; }
            }
            void IWPFDocumentCollections.MoveToFront(IWPFDocument document)
            {
                this.MoveToFront(document as WPFDocument);    
            }
            void IWPFDocumentCollections.MoveToBack(IWPFDocument document)
            {
                this.MoveToBack(document as WPFDocument);
            }
            void IWPFDocumentCollections.MoveToBegin(IWPFDocument document)
            {
                this.MoveToBegin(document as WPFDocument);
            }
            void IWPFDocumentCollections.MoveToEnd(IWPFDocument document)
            {
                this.MoveToEnd(document as WPFDocument);
            }
            int IWPFDocumentCollections.IndexOf(IWPFDocument document)
            {
                return this.m_List.IndexOf(document as WPFDocument);
            }
            #endregion
            public void Remove(WPFDocument document)
            {
                if ((document == null) || (this.m_List.Count == 1) || !this.m_List.Contains(document)) return;
                    this.m_List.Remove (document);
                    this.m_Owner.OnDocumentRemoved( new WPFElementEventArgs<WPFDocument> (document ));
            }
            public override string ToString()
            {
                return "Documents [" + this.m_List.ToString() + "]";
            }
            public void MoveToEnd(WPFDocument document)
            { }
            public void MoveToFront(WPFDocument document) { }
            public void MoveToBegin(WPFDocument document) { }
            public void MoveToBack(WPFDocument document) { }
            #region IWPFDocumentCollections Members
            void IWPFDocumentCollections.Add(IWPFDocument document)
            {
                this.Add(document as WPFDocument);
            }
            #endregion     
            public bool AllowMultiDocument
            {
                get { throw new NotImplementedException(); }
            }
        }
        event WPFElementEventHandler<WPFDocument> DocumentAdded;
        event WPFElementEventHandler<WPFDocument> DocumentRemoved;
        protected virtual  void OnDocumentRemoved(WPFElementEventArgs <WPFDocument > e)
        {
            if (this.DocumentRemoved != null)
                this.DocumentRemoved(this, e);
        }
        protected virtual void OnDocumentAdded(WPFElementEventArgs <WPFDocument > e)
        {
            if (this.DocumentAdded != null)
                this.DocumentAdded(this, e);
        }
        #region ICoreWorkingConfigElementSurface Members
        ICoreWorkingObject m_ElementToConfigure;
        public ICoreWorkingObject ElementToConfigure
        {
            get
            {
                return this.m_ElementToConfigure ;
            }
            set
            {
                if (this.m_ElementToConfigure != value)
                {
                    this.m_ElementToConfigure = value;
                    this.OnElementToConfigureChanged(EventArgs.Empty);
                }
            }
        }
        protected  void OnElementToConfigureChanged(EventArgs eventArgs)
        {
            if (this.ElementToConfigureChanged != null)
            {
                this.ElementToConfigureChanged(this, eventArgs);
            }
        }
        public event EventHandler ElementToConfigureChanged;
        #endregion
        #region ICoreWorkingDualBrushSelectorSurface Members
        enuBrushMode m_BrushMode;
        public enuBrushMode BrushMode
        {
            get
            {
                return this.m_BrushMode;
            }
            set
            {
                if (this.m_BrushMode != value)
                {
                    this.m_BrushMode = value;
                    this.OnBrushModeChanged(EventArgs.Empty);
                }
            }
        }
        protected void OnBrushModeChanged(EventArgs eventArgs)
        {
            if (this.BrushModeChanged != null)
                this.BrushModeChanged(this, eventArgs);
        }
        public event EventHandler BrushModeChanged;
        #endregion
        #region ICore2DDrawingDualBrushElement Members
        public IGK.DrSStudio.Drawing2D.ICoreBrush GetBrush(enuBrushMode mode)
        {
            switch (mode)
            {
                case enuBrushMode.Fill :
                    return this.FillBrush;
                case enuBrushMode.Stroke :
                default :
                    return this.StrokeBrush;
            }
        }
        #endregion
        #region ICore2DDrawingObject Members
        public new IGK.DrSStudio.Drawing2D.ICore2DDrawingObject Parent
        {
            get
            {
                return null;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion
        #region ICore2DDualBrushObject Members
        IGK.DrSStudio.Drawing2D.ICoreBrush m_FillBrush;
        IGK.DrSStudio.Drawing2D.ICorePen m_StrokeBrush;
        public IGK.DrSStudio.Drawing2D.ICoreBrush FillBrush
        {
            get { return this.m_FillBrush; }
        }
        public IGK.DrSStudio.Drawing2D.ICorePen StrokeBrush
        {
            get { return this.m_StrokeBrush; }
        }
        #endregion
        #region ICoreBrushOwner Members
        public Rectanglef GetBound()
        {
            return this.Bounds;
        }
        
        /// <summary>
        /// dispose the new create matrix
        /// </summary>
        /// <returns></returns>
        public Matrix GetMatrix()
        {
            return Matrix.Identity;
        }
        #endregion
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        #endregion
        #region ICore2DDrawingBrushSupportElement Members
        public IGK.DrSStudio.Drawing2D.enuBrushSupport BrushSupport
        {
            get {
                ICoreBrushOwner
                    v_l =
                    this.ElementToConfigure as
                    ICoreBrushOwner;
                if (v_l  == null)
                    return IGK.DrSStudio.Drawing2D.enuBrushSupport.All;
                else
                {
                    return v_l.BrushSupport;
                }
            }
        }
        #endregion
        abstract class WPFSurfaceManagerBase
        {
            private WPFHostSurface m_Surface;
            public WPFHostSurface Surface
            {
                get { return m_Surface; }
            }
            public WPFSurfaceManagerBase(WPFHostSurface surface)
            {
                this.m_Surface = surface;
                surface.CurrentDocumentChanged += (surface_CurrentDocumentChanged);
                this.RegisterDocumentEvent(surface.CurrentDocument);                
            }
            void surface_CurrentDocumentChanged(object o, CoreWorkingElementChangedEventArgs <WPFDocument > e)
            {
                if (e.OldElement != null) UnregisterDocument(e.OldElement);
                if (e.NewElement!= null) RegisterDocumentEvent(e.NewElement);
            }
            protected virtual  void UnregisterDocument(WPFDocument wPFDocument)
            {
                this.UnregisterLayerEvent(wPFDocument.CurrentLayer);
                wPFDocument.CurrentLayerChanged -= new WPFLayerChangeEventHandler(wPFDocument_CurrentLayerChanged);
            }
            protected virtual void wPFDocument_CurrentLayerChanged(object o, WPFLayerChangeEventArgs e)
            {
                if (e.OldLayer != null) UnregisterLayerEvent(e.OldLayer);
                if (e.NewLayer != null) RegisterLayerEvent(e.NewLayer);
            }
            private void RegisterDocumentEvent(WPFDocument wPFDocument)
            {
                wPFDocument.CurrentLayerChanged += new WPFLayerChangeEventHandler(wPFDocument_CurrentLayerChanged);
                this.RegisterLayerEvent(wPFDocument.CurrentLayer);
            }
            protected virtual void RegisterLayerEvent(WPFLayer wPFLayer)
            { }
            protected virtual void UnregisterLayerEvent(WPFLayer wPFLayer)
            {
            }
        }
        sealed class ConfigObjectManager : WPFSurfaceManagerBase
        {
            public ConfigObjectManager(WPFHostSurface surface ) :base(surface)
            {
            }
            protected override void RegisterLayerEvent(WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged += new EventHandler(wPFLayer_SelectedElementChanged);
            }
            protected override void UnregisterLayerEvent(WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged -= new EventHandler(wPFLayer_SelectedElementChanged);
            }
            void wPFLayer_SelectedElementChanged(object sender, EventArgs e)
            {
                WPFLayer l = sender as WPFLayer;
                if (l.SelectedElements.Count >= 1)
                {
                    this.Surface.ElementToConfigure = l.SelectedElements[0] as ICoreWorkingObject;
                }
                else
                    this.Surface.ElementToConfigure = null;
            }
        }
        sealed class LayerManagerSurface : WPFSurfaceManagerBase
        {
            public LayerManagerSurface(WPFHostSurface surface):base(surface )
            {
            }
            void wPFLayer_SelectedElementChanged(object sender, EventArgs e)
            {
                OnClear(EventArgs.Empty);
                WPFLayer l = sender as WPFLayer ;
                this.Surface.m_SelectionLayer.Children.Clear();
                this.Surface.m_SelectionLayer.SetValue(
                    Canvas.ZIndexProperty, 100);
                System.Windows.UIElement i = null;
                foreach (var item in l.SelectedElements.ToArray() )
                {
                    i = item.Shape as System.Windows.UIElement;
                    if (i != null)
                    {
                        RectangleZone rz = new RectangleZone(item, this);
                        System.Windows.Shapes.Rectangle rc = rz.rc;
                        this.Surface.m_SelectionLayer.Children.Add(
                            rc);
                    }
                }
            }
            protected override void UnregisterLayerEvent(WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged -= new EventHandler(wPFLayer_SelectedElementChanged);
            }
            protected override void RegisterLayerEvent(WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged += new EventHandler(wPFLayer_SelectedElementChanged);
            }
            public event EventHandler Clear;
            ///<summary>
            ///raise the OnClear 
            ///</summary>
            void OnClear(EventArgs e)
            {
                if (Clear != null)
                    Clear(this, e);
            }
            internal class RectangleZone {
                internal IWPFLayeredBoundElement element;
                internal LayerManagerSurface parent;
                internal System.Windows.Shapes.Rectangle rc;
                public RectangleZone(IWPFLayeredBoundElement element,LayerManagerSurface parent)
                {
                    this.element = element;
                    this.parent = parent;
                    this.rc = new System.Windows.Shapes.Rectangle();
                    element.PropertyChanged +=new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
                    this.parent.Clear += new EventHandler(parent_Clear);
                    Rectangled v_rc = element.GetBound();
                    this.UpdateInfo();
                }
                void parent_Clear(object sender, EventArgs e)
                {
                    element.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
                    this.parent.Clear -= new EventHandler(parent_Clear);  
                }
                void UpdateInfo()
                {
                    Rectangled v_rc  = this.element.GetTransformBound();//.GetBound();
                    rc.SetValue(Canvas.TopProperty, v_rc .Y );
                        rc.SetValue(Canvas.LeftProperty, v_rc.X );
                        rc.Width = v_rc.Width;
                        rc.Height = v_rc.Height;
                    rc.Stroke = Brushes.Black;
                    rc.StrokeDashCap = PenLineCap.Round;
                    rc.StrokeThickness = 1.0;                   
                    rc.StrokeDashArray = new DoubleCollection();
                    rc.StrokeDashArray.Add(2);
                    rc.StrokeDashArray.Add(4);
                    rc.Fill = Brushes.Transparent;
                    //rc.Opacity = 0.2;
                }
                void  element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
                {
                    UpdateInfo(); 
                  }
                }
        }
        #region ICoreWorkingProjectManagerSurface Members
        ICoreProject m_projects;
        public ICoreProject ProjectInfo
        {
            get { return this.m_projects; }
        }
        public void SetParam(ICoreInitializatorParam p)
        {
        }
        #endregion
        protected virtual ICoreProject CreateProject()
        {
            return null;// new WPFSurfaceProject(this);
        }
        public static WPFHostSurface  CreateSurface(ICoreProject prInfo, ICoreWorkingDocument[] documents)
        {
            WPFHostSurface v_surface = new WPFHostSurface();
            bool v = (documents != null) && (documents.Length > 0);
            for (int i = 0; i < documents.Length; i++)
            {
                v_surface.Documents.Add(documents[i]);
            }
            v_surface.Documents.Remove(v_surface.CurrentDocument);
            v_surface.m_projects = prInfo;
            if (v)
            v_surface.CurrentDocument = documents[0] as WPFDocument ;
            //v_surface.InitSurface(prInfo, documents);
            return v_surface;
        }
    }
}

