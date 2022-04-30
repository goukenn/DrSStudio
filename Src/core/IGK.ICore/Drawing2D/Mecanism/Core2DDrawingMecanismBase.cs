

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingMecanismBase.cs
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
file:Core2DDrawingMecanismBase.cs
*/
using IGK.ICore;using IGK.ICore.Actions;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Mecanism;
using IGK.ICore.MecanismActions;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Tools;

namespace IGK.ICore.Drawing2D.Mecanism
{
    public abstract class Core2DDrawingMecanismBase<T,M> : 
        CoreMecanismBase,
        ICoreWorkingMecanism<T>, 
        ICoreWorkingMecanism,
        ICore2DDrawingFrameRenderer        
        where T :  class, ICore2DDrawingLayeredElement  
        where M :  class, ICore2DDrawingSurface 
    {
        private Vector2f m_StartPoint;
        private Vector2f m_EndPoint;
        private bool m_IsElementChanged;
        protected bool CancelRightClick { get; set; }
        protected bool CancelLeftClick { get; set; }

        /// <summary>
        /// generate and init snippets
        /// </summary>
        protected void UpdateSnippets()
        {
            if (this.Element != null)
            {
                this.GenerateSnippets();
                this.InitSnippetsLocation();
            }
            else
            {
                this.DisposeSnippet();
            }
        }

        /// <summary>
        /// must be call to grand snippet edition to element. 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected virtual bool SnippetResolve(CoreMouseEventArgs e)
        {
            if ((this.Element == null) || (Snippet == null))
            {
                this.Snippet = null;
                this.Element = null;
                this.State = ST_NONE;
                return false;
            }
            return true;
        }
        /// <summary>
        /// application current surface Matraix transofrm
        /// </summary>
        /// <param name="graphics"></param>
        protected void ApplyCurrentSurfaceTransform(ICoreGraphics graphics)
        {            
            graphics.ScaleTransform(this.CurrentSurface.ZoomX, this.CurrentSurface.ZoomY, enuMatrixOrder.Append);
            graphics.TranslateTransform(this.CurrentSurface.PosX, this.CurrentSurface.PosY, enuMatrixOrder.Append);
        }

        public override ICoreWorkingSurface Surface
        {
            get { return base.CurrentSurface; }
        }
        /// <summary>
        /// select only the first element
        /// </summary>
        /// <param name="factor"></param>
        protected void SelectOne(Vector2f factor)
        {
            for (int i = this.CurrentSurface.CurrentLayer.Elements.Count - 1;
                i >= 0; --i)
            {
                if (this.CurrentLayer.Elements[i].Contains(factor))
                {
                    this.CurrentLayer.Select(this.CurrentLayer.Elements[i]);
                    return;
                }
            }
        }

        internal override void Delete(ICoreWorkingObject element)
        {
            this.CurrentLayer.Elements.Remove(element as ICore2DDrawingLayeredElement);            
        }
        protected override ICoreSnippet CreateSnippet(int demand, int index)
        {
            return this.CurrentSurface.CreateSnippet(this, demand, index);
        }

        /// <summary>
        /// retreive the type of the element to created
        /// </summary>
        protected virtual Type ElementType {
            get {
                Type t = GetType().DeclaringType;
                if (t==null)
                    return typeof(T);
                return t;
            }
        }
        protected void MarkElementAsEditing(Core2DDrawingLayeredElement element, bool isEditing) {
            if (element == null) return;

            element.IsEditing = isEditing;
        }
        protected override void GenerateActions()
        {
            base.GenerateActions();
            Type t = ElementType;
            if (typeof(ICore2DFillModeElement).IsAssignableFrom (t))
            {
                this.AddAction(enuKeys.F, new Core2DDToogleFillModeMecanismAction());
            }
            //if (typeof(ICore2DClosableElement).IsAssignableFrom(t))
            //{
                this.AddAction(enuKeys.C, new Core2DDToogleCloseModeMecanismAction());
           // }
            this.AddAction(enuKeys.V, new Core2DDToogleFillModeMecanismAction());
            this.AddAction(enuKeys.Escape, new CoreEndEditionElementMecanismAction());
            //replace action
            this.Actions[enuKeys.Delete] = new Core2DDrawingDeleteElementMecanismAction();
        }
        /// <summary>
        /// Get if the elements(s) changed
        /// </summary>
        public bool IsElementChanged
        {
            get { return m_IsElementChanged; }
            protected set
            {
                if (m_IsElementChanged != value)
                {
                    m_IsElementChanged = value;
                }
            }
        }
        /// <summary>
        /// Get the end point
        /// </summary>
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            protected set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                }
            }
        }
        /// <summary>
        /// Get Base startpoint
        /// </summary>
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            protected set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                }
            }
        }
        /// <summary>
        /// default getting mecanism
        /// </summary>
        public Core2DDrawingMecanismBase()
            : base()
        {
            this.AllowActions = true;
        }

        
   
        private T m_Element;
        public T Element
        {
            get { return m_Element; }
            set
            {
                if (m_Element != value)
                {
                    CoreWorkingElementChangedEventArgs<T> e = new CoreWorkingElementChangedEventArgs<T>(m_Element, value);
                    m_Element = value;
                    OnElementChanged(e);
                }
            }
        }
        public override  void Invalidate()
        {
            if (this.CurrentSurface!=null)
                this.CurrentSurface.RefreshScene();
        }
        protected internal override ICoreWorkingConfigurableObject GetEditElement()
        {
            return this.Element as ICoreWorkingConfigurableObject;
        }
        public event EventHandler<CoreWorkingElementChangedEventArgs<T>> ElementChanged;

        /// <summary>
        /// raise the element change event
        /// </summary>
        /// <param name="e"></param>
        protected virtual  void OnElementChanged(CoreWorkingElementChangedEventArgs<T> e)
        {
            if (e.OldElement != null)
            {
                UnRegisterElementEvent(e.OldElement);
            }
            if (e.NewElement != null)
            {
                RegisterElementEvent(e.NewElement );
            }

            DefineElementProperty(e);

            if (ElementChanged != null)
                this.ElementChanged(this, e);
        }
        /// <summary>
        /// for managing what append when element changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void DefineElementProperty(CoreWorkingElementChangedEventArgs<T> e)
        {
            if (e.NewElement != null)
            {
                this.CurrentLayer.Select(e.NewElement);
                if (this.State != ST_EDITING )
                {
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.Invalidate();
                }
            }
            else
            {
                this.CurrentLayer.Select(null);
                this.DisposeSnippet();
                this.Invalidate();
            }
        }

        protected virtual void RegisterElementEvent(T element)
        {
            element.PropertyChanged += element_PropertyChanged;
        }

       
        protected virtual void UnRegisterElementEvent(T element)
        {
            element.PropertyChanged-= element_PropertyChanged;
        }
      
        protected virtual  void BeginDrawing(CoreMouseEventArgs e)
        {
            this.StartPoint = e.FactorPoint;
            this.EndPoint = e.FactorPoint;
            this.State = ST_CREATING;
            this.DisableSnippet();
        }
        protected virtual  void UpdateDrawing(CoreMouseEventArgs e)
        {
            this.EndPoint = e.FactorPoint;
        }
        protected virtual  void EndDrawing(CoreMouseEventArgs e)
        {
            this.UpdateDrawing(e);            
            this.State = ST_EDITING;
            this.GenerateSnippets();
            this.InitSnippetsLocation();
            this.EnabledSnippet();
        }
        //override these method to handle the snippet edition
        /// <summary>
        /// begin snippet edition
        /// </summary>
        /// <param name="e"></param>
        protected virtual void BeginSnippetEdit(CoreMouseEventArgs e)
        {
            this.State = ST_EDITING;
        }
        /// <summary>
        /// update snippet edition
        /// </summary>
        /// <param name="e"></param>
        protected virtual void UpdateSnippetEdit(CoreMouseEventArgs e)
        { }
        /// <summary>
        /// end snippet edition
        /// </summary>
        /// <param name="e"></param>
        protected virtual void EndSnippetEdit(CoreMouseEventArgs e)
        {
            this.InitSnippetsLocation();
        }    

        /// <summary>
        /// hide the current surface base
        /// </summary>
        public new M CurrentSurface
        {
            get { return base.CurrentSurface as M; }
            protected set {
                base.CurrentSurface = value;
            }
        }
        public ICore2DDrawingLayer CurrentLayer {
            get {
                if (this.CurrentSurface != null)
                    return this.CurrentSurface.CurrentLayer;
                return null;
            }
        }
        public ICore2DDrawingDocument  CurrentDocument
        {
            get
            {
                if (this.CurrentSurface != null)
                    return this.CurrentSurface.CurrentDocument;
                return null;
            }
        }
        protected override  ICoreSnippetCollections CreateSnippetCollections()
        {            
            return new CoreSnippetCollections(this);
        }
        protected override ICoreMecanismActionCollections CreateActionMecanismCollections()
        {
            return new CoreMecanismActionCollections(this);
        }
       /// <summary>
       /// edit the current element
       /// </summary>
       /// <param name="element"></param>
        public virtual void Edit(T element)
        {
            if (element != null)
            {
                this.State = ST_EDITING;
                this.DisableSnippet();
                this.Element = element;
                this.m_editionMode = this.GetEditionMode(element);
                Core2DDrawingLayeredElement  v_e  = this.Element as Core2DDrawingLayeredElement ;
                if (v_e != null) {               
                    v_e.SuspendLayout();
                    v_e.ResetTransform();
                    v_e.ResumeLayout();
                }
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.EnabledSnippet();
            }
            else {
                this.State = ST_NONE;
            }
        }

        protected virtual enuDrawing2DEditionMode GetEditionMode(T element)
        {
            ICore2DDrawingLayeredElement c = element;
            if (c.Parent == null)
                return enuDrawing2DEditionMode.None;


            if (c.ParentDocument != this.CurrentDocument) {
                return enuDrawing2DEditionMode.Document;
            }

            if (c.Parent == element.ParentLayer)
                return enuDrawing2DEditionMode.Local;

            if (c.Parent is ICore2DDrawingLayeredElement)
            {
                return enuDrawing2DEditionMode.Group;
            }

            return enuDrawing2DEditionMode.None;
        }

        /// <summary>
        /// override this method to end the edition
        /// </summary>
        public virtual void EndEdition()
        {
            if (this.Element != null)
            {
                this.Element = null;
            }
            else {
                this.GotoDefaultTool();
            }
        }
        void t_MouseDoubleClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }
        protected virtual void OnMouseDoubleClick(CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Right)
            {
                GotoDefaultTool();
            }
        }
        /// <summary>
        /// go to default tool
        /// </summary>
        public void GotoDefaultTool()
        {
            ICoreWorkingToolManagerSurface m = this.CurrentSurface as ICoreWorkingToolManagerSurface;
            if (m != null)
            {
                m.CurrentTool = m.DefaultTool;
            }
        }
        /// <summary>
        /// add custom snippet
        /// </summary>
        /// <param name="snippet"></param>
        protected void AddSnippet(ICoreSnippet snippet)
        {
            if (snippet == null)
                return;
            this.RegSnippets.Add(snippet.Index, snippet);
        }
        public override bool Register(ICoreWorkingSurface surface){
            
            return this.Register (surface as M);
        }
        public override void Edit(ICoreWorkingObject e) {
            this.Edit(e as T);
        }

        /// <summary>
        /// register surface 
        /// </summary>
        /// <param name="surface"></param>
        /// <returns></returns>
        public virtual bool Register(M surface)
        {
            if (surface == null)
                return false;
            this.CurrentSurface = surface;
            surface.MouseClick += _MouseClick;
            surface.MouseDown += _MouseDown;
            surface.MouseUp += _MouseUp;
            surface.MouseMove += _MouseMove;
            surface.MouseLeave += _MouseLeave;
            surface.MouseDoubleClick += t_MouseDoubleClick;
            surface.CurrentDocumentChanged += t_CurrentDocumentChanged;            
            surface.SizeChanged += t_SizeChanged;
            surface.ZoomModeChanged += t_ZoomModeChanged;
            surface.ZoomChanged += t_ZoomChanged;
            if (surface.CurrentLayer !=null)
            this.RegisterLayerEvent(surface.CurrentLayer);
            if (surface.CurrentDocument!=null)
            this.RegisterDocumentEvent(surface.CurrentDocument);
            CoreActionRegisterTool.Instance.AddFilterMessage(this.Actions);
            string v_help = "Tool name : {0}".R(CoreWorkingObjectAttribute.GetObjectName(
                (surface as ICoreWorkingToolManagerSurface)?.CurrentTool));
            this.SendHelpMessage(v_help);
            return true;
        }

        void t_ZoomChanged(object sender, EventArgs e)
        {
            OnCurrentSurfaceSizeZoomChanged(e);
        }
        void t_SizeChanged(object sender, EventArgs e)
        {
            OnCurrentSurfaceSizeZoomChanged(e);
        }
        protected virtual void OnCurrentSurfaceSizeZoomChanged(EventArgs e)
        {
            if (this.Element != null)
            {
                this.InitSnippetsLocation();
                this.Invalidate();
            }
        }   
        public override bool UnRegister()
        {
            M t = this.CurrentSurface;
            t.MouseClick -= _MouseClick;
            t.MouseDown -= _MouseDown;
            t.MouseUp -= _MouseUp;
            t.MouseMove -= _MouseMove;
            t.MouseLeave -= _MouseLeave;
            t.CurrentDocumentChanged -= t_CurrentDocumentChanged;
            t.SizeChanged -= t_SizeChanged;
            t.ZoomModeChanged -= t_ZoomModeChanged;
            t.ZoomChanged -= t_ZoomChanged;
            this.DisposeSnippet();
            this.UnRegisterDocumentEvent(t.CurrentDocument);
            this.UnRegisterLayerEvent(t.CurrentLayer);
            CoreActionRegisterTool.Instance.RemoveFilterMessage(this.Actions);

            OnFreeFreezed();
            return true;
        }
        protected virtual void OnFreeFreezed() {
            if (this.m_IsFreezed == false)
            {
                this.Element = null;
                this.CurrentSurface?.CurrentLayer.Select(null);
                this.CurrentSurface = null;
            }
        }
        void t_ZoomModeChanged(object sender, EventArgs e)
        {
            OnCurrentSurfaceSizeZoomChanged(e);
        }
        protected virtual void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.CurrentLayerChanged += _currentLayerChanged;
            document.PropertyChanged += document_PropertyChanged;
        }
        protected virtual void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.CurrentLayerChanged -= _currentLayerChanged;
            document.PropertyChanged -= document_PropertyChanged;
        }
        void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnDocumentPropertyChanged(e);            
        }
        void element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnElementPropertyChanged(e);
        }

        protected virtual void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType)e.ID)
            {
                case enu2DPropertyChangedType.MatrixChanged:
                    Core2DDrawingLayeredElement v_l = this.Element as Core2DDrawingLayeredElement;
                    if (v_l != null)
                    {
                        v_l.ResetTransform(false);
                        this.InitSnippetsLocation();
                        this.Invalidate();
                    }
                    break;
            }
        }
       
        /// <summary>
        /// manage the document property changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDocumentPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch ((enu2DPropertyChangedType)e.ID)
            { 
                case enu2DPropertyChangedType.SizeChanged :
                    OnCurrentSurfaceSizeZoomChanged(EventArgs.Empty);
                    break;
            }
        }
        private void _currentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            this.OnCurrentLayerChanged(e);
        }
       
        void t_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            OnCurrentLayerChanged(e);
        }
        protected virtual void OnCurrentLayerChanged(Core2DDrawingLayerChangedEventArgs e)
        {
            this.Element = null;
            this.DisposeSnippet();
            if (e.OldElement != null)
                UnRegisterLayerEvent(e.OldElement);
            if (e.NewElement !=null)
                RegisterLayerEvent(e.NewElement);
        }
        protected virtual void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            if (layer == null)
                return;
            layer.ElementRemoved -= layer_ElementRemoved;
            layer.SelectedElementChanged -= layer_SelectedElementChanged;
        }
        void layer_ElementRemoved(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            if (e.Item == this.Element)
            {
                this.Element = null;
                this.Snippet = null;
                this.State = ST_NONE;
                this.DisposeSnippet();
            }
        }
       
        protected virtual void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            if (layer == null) 
                return;
            layer.ElementRemoved += layer_ElementRemoved;
            layer.SelectedElementChanged += layer_SelectedElementChanged;
        }
        protected virtual void BeginMove(CoreMouseEventArgs e)
        {
            var v_e= this.Element as Core2DDrawingLayeredElement;
            if ((v_e == null) ||(v_e.IsLoading ) || (v_e.SuspendDeep >0))
                return;
            this.DisableSnippet();
            this.Element.SuspendLayout();
            this.CurrentSurface.Cursor = CoreCursors.Hand;
            this.StartPoint = e.FactorPoint;
            this.EndPoint = e.FactorPoint;
            this.State = ST_MOVING;
        }
        protected virtual void Move(Vector2f d, bool temp)
        {
            Core2DDrawingLayeredElement l = this.Element as Core2DDrawingLayeredElement;
            if (l != null)
                l.Translate(d.X, d.Y, enuMatrixOrder.Append, temp);
        }
        protected virtual void UpdateMove(CoreMouseEventArgs e)
        {
            //this.Element.Invalidate(false);
            this.EndPoint = e.FactorPoint;
            Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
            Move(d, true);
            this.Invalidate();
        }

        protected virtual  void EndMove(CoreMouseEventArgs e)
        {
            this.EndPoint = e.FactorPoint;
            Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
            if (this.Element != null)
            {
                Move(d, false);
                this.Element.ResetTransform();
                this.ShowSnippets();
          
            }
            if (this.State == ST_MOVING)
                this.State = ST_EDITING;
            this.CurrentSurface.Cursor = this.GetCursor();
            this.Element.ResumeLayout();
            this.Invalidate();
        }


      
        void layer_SelectedElementChanged(object sender, EventArgs e)
        {
            OnLayerSelectedElementChanged(e);
        }
        protected virtual void OnLayerSelectedElementChanged(EventArgs eventArgs)
        {
            if (this.CurrentLayer.SelectedElements.Count == 1)
            {
                this.Element = this.CurrentLayer.SelectedElements[0] as T;
            }
        }
        void t_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            OnCurrentDocumentChanged(e);
        }
        protected virtual void OnCurrentDocumentChanged(CoreWorkingDocumentChangedEventArgs e)
        {
            if (this.Element != null)
            {
                this.DisposeSnippet();
                this.EndEdition();
            }
            if (e.OldElement is ICore2DDrawingDocument)
            {
                ICore2DDrawingDocument v_doc = e.OldElement as ICore2DDrawingDocument;
                this.UnRegisterLayerEvent(v_doc.CurrentLayer);
                this.UnRegisterDocumentEvent(v_doc);
            }
            if (e.NewElement is ICore2DDrawingDocument)
            {
                ICore2DDrawingDocument v_doc = e.NewElement as ICore2DDrawingDocument;
                this.RegisterDocumentEvent(v_doc);
                this.RegisterLayerEvent(v_doc.CurrentLayer);
            }
        }
        private void _MouseMove(object sender, CoreMouseEventArgs e)
        {
            OnMouseMove(e);
        }
        protected virtual void OnMouseMove(CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.None:
                    break;
                case enuMouseButtons.Left:
                    if (this.Snippet != null) {
                        this.UpdateSnippetEdit(e);
                    }
                    else {
                        switch (this.State) {
                            case ST_CREATING:
                                {
                                    this.UpdateDrawing(e);
                                }
                                break;
                            case ST_MOVING :
                                this.UpdateMove(e);
                                break;
                        }
                    }
                    break;
                case enuMouseButtons.Right:
                    break;
                case enuMouseButtons.Middle:
                    break;              
                default:
                    break;
            }
        }
        private void _MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave(e);
        }
        private void OnMouseLeave(EventArgs e)
        {
        }
        private void _MouseUp(object sender, CoreMouseEventArgs e)
        {
            OnMouseUp(e);
        }
        protected virtual void OnMouseUp(CoreMouseEventArgs e)
        {
            switch (e.Button)
            {
                case enuMouseButtons.None:
                    break;
                case enuMouseButtons.Left:
                    if (this.Snippet != null)
                    {
                        this.EndSnippetEdit(e);
                    }
                    else
                    {
                        switch(this.State)
                        {
                            case ST_CREATING:
                                {
                                    if (this.Element != null)
                                        this.EndDrawing(e);
                                    else
                                        this.State = ST_NONE;
                                }
                                break;
                            case ST_MOVING :
                                if (this.Element!=null)
                                    this.EndMove(e);
                                break;
                            default :
                                this.State = ST_NONE;
                                break;
                        }
                            
                    }
                    break;
                case enuMouseButtons.Right:
                    break;
                case enuMouseButtons.Middle:
                    break;
            }
        }
        private void _MouseDown(object sender, CoreMouseEventArgs e)
        {
            OnMouseDown(e);
        }
        protected virtual void OnMouseDown(CoreMouseEventArgs e)
        {

            switch (e.Button)
            {
                case enuMouseButtons.None:
                    break;
                case enuMouseButtons.Left:
                    switch(this.State)
                    {
                        case ST_NONE:
                        case ST_EDITING :
                            if (this.Element !=null )
                            {
                                if (this.Snippet != null)
                                {
                                    BeginSnippetEdit(e);
                                    return;
                                }
                                else  if  (this.Element.Contains (e.FactorPoint ))
                                { 
                                    BeginMove (e);                                    
                                    return;
                                }
                            }
                                T element = CreateNewElement();
                                if (element != null)
                                {
                                    
                                    InitNewCreatedElement(element, e.FactorPoint);
                                    this.CurrentSurface.CurrentLayer.Elements.Add(element);                                    
                                    this.Element = element;
                                    this.BeginDrawing(e);
                                    this.UpdateDrawing (e);
                                }                            
                        break ;
                    }
                    break;
            }
        }
        protected virtual void InitNewCreatedElement(T element, Vector2f location)
        {
            if (element is ICore2DDualBrushObject)
            {
                ICore2DDualBrushObject c = element as ICore2DDualBrushObject;
                c.FillBrush.Copy(this.CurrentSurface.FillBrush);
                c.StrokeBrush.Copy(this.CurrentSurface.StrokeBrush );
            }
        }
        protected virtual T CreateNewElement()
        {
            Type v_t = GetType().DeclaringType;
            if (v_t != null)
            {
                return v_t.Assembly.CreateInstance(v_t.FullName, true) as T;
            }
            return null;
        }
        private void _MouseClick(object sender, CoreMouseEventArgs e)
        {
            OnMouseClick(e);
        }
        protected virtual void OnMouseClick(CoreMouseEventArgs e)
        {
            if ((e.Button == enuMouseButtons.Right) && !CancelRightClick)
            {
                if ((this.Element != null)&&(this.Snippet == null) && !this.Element.Contains (e.FactorPoint) )
                {
                    this.EndEdition();
                    
                    return;
                }
                if (this.Element == null)
                {
                    this.GotoDefaultTool();
                }
                
            }
            CancelRightClick = false;
        }
       
       
        bool m_IsFreezed;
        private enuDrawing2DEditionMode m_editionMode;


        public enuDrawing2DEditionMode EditionMode {
        get {
                return this.m_editionMode;
            }
        }
        public override  bool IsFreezed
        {
            get {
                return m_IsFreezed;
            }
        }
        /// <summary>
        /// freeze this mecanism
        /// </summary>
        public override  void Freeze()
        {
            if (!this.m_IsFreezed)
            {

                this.m_IsFreezed = true;
                this.UnRegister();
            }
        }
        public override  void UnFreeze()
        {
            if (this.m_IsFreezed)
            {
                this.Register(this.CurrentSurface);
                this.m_IsFreezed = false;
                if (this.Element != null)
                {
                    //this.GenerateSnippets();
                    //this.InitSnippetsLocation();
                    //this.EnabledSnippet();
                    //this.CurrentLayer.Select(this.Element);
                    this.Edit (this.Element);
                }
            }
        }
        bool ICoreWorkingMecanism.Register(ICoreWorkingSurface surface)
        {
            return this.Register(surface as M);
        }
        ICoreWorkingSurface ICoreWorkingSurfaceHost.CurrentSurface
        {
            get { return this.CurrentSurface; }
        }
        public class CoreMecanismActionCollections : CoreMecanismActionBaseCollections<T>
        {
            public CoreMecanismActionCollections(Core2DDrawingMecanismBase<T, M> mecanism):base(mecanism)
            {
            }
            public override int Priority
            {
                get { return CoreConstant.MECANISM_PRIORITY; }
            }
        }
        public class CoreSnippetCollections : CoreSnippetCollectionsBase
        {
            Core2DDrawingMecanismBase<T, M> m_mecanism;
            public override string ToString()
            {
                return "CoreSnippetCollections[Count:" + this.Count + "]";
            }
            public CoreSnippetCollections(Core2DDrawingMecanismBase<T, M> mecanism):base()
            {
                this.m_mecanism = mecanism;
            }
        }
        /// <summary>
        /// override this to render on overload object
        /// </summary>
        /// <param name="device"></param>
        public virtual void Render(ICoreGraphics device)
        {
            var e = this.Element;
            if( (e == null) || (this.EditionMode == enuDrawing2DEditionMode.Local)
                    || (this.EditionMode == enuDrawing2DEditionMode.None))
                return;

           
            object v_state = device.Save();
            this.ApplyCurrentSurfaceTransform(device);
            //ApplyCurrentSurfaceTransform(e);
            new ObjectRenderer(device, e.GetPath () as CoreGraphicsPath).Render(this.EditionMode);
            device.Restore(v_state);
           
        }
        
        /// <summary>
        /// override this to determine if can process message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public override bool CanProcessActionMessage(ICoreMessage message)
        {
            bool v =  ((!this.CurrentSurface.IsDisposed) &&
       (message.HWnd== this.CurrentSurface.SceneHandle) && 
       !MenuActionMessageFiltering && this.AllowActions);
            return v;
        }
        void ICoreWorkingMecanism.Edit(ICoreWorkingObject workingobject)
        {
            if (workingobject is T)
                this.Edit(workingobject as T);
            else
            {
                this.Edit(workingobject);
            }
        }
      

        //public override ICoreWorkingSurface Surface
        //{
        //    get { return this.m_CurrentSurface; }
        //}
    }
}

