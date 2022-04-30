

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TextElement.cs
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
file:TextElement.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.GraphicModels;
using IGK.ICore.MecanismActions;
using IGK.ICore.TextEditor;
using IGK.ICore.TextEditor.Actions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Text",
     typeof(Mecanism),
     Keys = enuKeys.T)]
    public class TextElement :
        TextElementBase,
        ICoreTextElement
    {

        /// <summary>
        /// measure string
        /// </summary>
        /// <returns></returns>
        public virtual Size2f MeasureString()
        {
            ICoreResourceManager man = CoreApplicationManager.Application.ResourcesManager;
            if (man != null)
            {
                Size2f p = man.MeasureString(this.Text, this.Font);
                return p;
            }
            return Size2f.Empty;
        }

        [Browsable(false)]
        public override bool IsValid
        {
            get
            {
                return base.IsValid;
            }
            protected set
            {
                base.IsValid = value;
            }
        }
        private CoreGraphicsPath m_background;//background element for selection
        private enuTextElementMode m_RenderMode;
        private bool m_AutoSize;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        [CoreConfigurableProperty()]
        public bool AutoSize
        {
            get { return m_AutoSize; }
            set
            {
                if (m_AutoSize != value)
                {
                    m_AutoSize = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
     


        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuTextElementMode.Path)]
        [CoreConfigurableProperty ()]
        public enuTextElementMode RenderMode
        {
            get { return m_RenderMode; }
            set
            {
                if (m_RenderMode != value)
                {
                    m_RenderMode = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        public override void Dispose()
        {
            if (this.m_background != null) {
                this.m_background.Dispose();
                this.m_background = null;
            }
            base.Dispose();
        }
        public TextElement()
            : base()
        {

        }

        public override object InitializeLifetimeService()
        {
            return base.InitializeLifetimeService();
        }

        private enuPreserveFontType m_PreserveFontSize;
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue(enuPreserveFontType.None)]
        public enuPreserveFontType PreserveFontSize
        {
            get { 
                return m_PreserveFontSize; 
            }
            set
            {
                if (m_PreserveFontSize != value)
                {
                    m_PreserveFontSize = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (((enu2DPropertyChangedType)e.ID == enu2DPropertyChangedType.MatrixChanged))
            {
                Matrix m = this.Matrix;
                if (!m.IsIdentity)
                {
                    switch (this.PreserveFontSize)
                    {
                        case enuPreserveFontType.None:
                            break;
                        case enuPreserveFontType.InitialFontSize:
                            this.ResetTransform();
                            break;
                        case enuPreserveFontType.PreserveScaleMaxXY:
                            float f = Math.Max (m.Elements[5] , m.Elements[0]);
                            this.ResetTransform();
                            this.Font.FontSize = this.Font.FontSize * f;                            
                            break;
                        default:
                            break;
                    }
                    TextElement.Mecanism.CurrentMecanism?.Update(this);
                    return;
                }
            }
            base.OnPropertyChanged(e);
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_background = new CoreGraphicsPath();
            this.m_PreserveFontSize = enuPreserveFontType.None;
            this.m_RenderMode = enuTextElementMode.Path;
            this.FillBrush.SetSolidColor(Colorf.Black);
            this.StrokeBrush.SetSolidColor(Colorf.Transparent);
            this.m_AutoSize = false;
        }

        protected override void RestorePathPreviousMatrix()
        {
            if (this.TempMatrix != null)
            {
                if (this.TempMatrix.IsInvertible)
                {
                    Matrix m = this.TempMatrix.Clone() as Matrix;
                    m.Invert();
                    this.GetPath().Transform(m);
                    m_background.Transform(m);
                    m.Dispose();
                }
            }
            else
            {
                if (this.Matrix.IsInvertible)
                {
                    Matrix m = this.Matrix.Clone() as Matrix;
                    m.Invert();
                    GetPath().Transform(m);
                    m_background.Transform(m);
                    m.Dispose();
                }
            }
        }
        protected override void SetMatrix()
        {
            base.SetMatrix();
            this.m_background.Transform(this.GetMatrix());
        }
        /// <summary>
        /// get the bound of this rectangle element
        /// </summary>
        /// <returns></returns>
        public override Rectanglef GetBound()
        {
            return this.m_background.GetBounds();
        }

        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            m_background.Reset();
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                m_background.AddRectangle(this.Bounds );
                return;
            }

            Rectanglef v_Rc = Rectanglef.Empty;
            if (this.AutoSize)
            {
                //measure string 
                ICoreResourceManager man = CoreApplicationManager.Application.ResourcesManager ;
                v_Rc.Location = this.Bounds.Location;
                v_Rc.Size = man.MeasureString(this.Text, this.Font);
            }
            else
                v_Rc = this.Bounds;

            m_background.AddRectangle(v_Rc);
            if (this.RenderMode == enuTextElementMode.Path)
            {
                p.AddText(
                    this.Text,
                    v_Rc,
                    this.Font);
            }
            else {
                p.AddRectangle(v_Rc);
            }


        }


        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            return parameters;
        }
        public new class Mecanism : RectangleElement.Mecanism, ITextEditorHost
        {
            private int m_PosX; //cursor x position
            private int m_PosY;//cursor y position
            private Rectanglef m_singleLineBound;
            public const int DM_FONTSIZE = ST_CONFIGURING + 100;
            private TextElementEditor m_caretEditor;
            private ITextEditorListener m_listener;
            private ICoreCaret m_caretManager;
            private bool m_configuring;
            public string m_text;


            
            public ICoreCaret CreateCaret(int width, int height)
            {
                this.m_caretManager = this.CurrentSurface.CreateCaret();
                this.m_caretManager.SetSize(width, height);
                return this.m_caretManager;
            }

            public override bool CanProcessActionMessage(ICoreMessage message)
            {

                enuKeys c = (enuKeys)message.WParam | CoreApplicationManager.ModifierKeys;
                //let the m_caretEditor process message
                if (m_caretEditor.Actions.Contains(c))
                    return true;
                if (this.Element != null)
                    return false; 
               return   base.CanProcessActionMessage(message);
            }
            protected override ICore.Actions.ICoreMecanismActionCollections CreateActionMecanismCollections()
            {
                return new MecanismCollectionAction(this);// base.CreateActionMecanismCollections();
            }
            class MecanismCollectionAction : RectangleElement.Mecanism.CoreMecanismActionCollections 
            {
                private Mecanism m_mecanism;
                public MecanismCollectionAction(Mecanism mecanism ):base(mecanism)
                {
                    this.m_mecanism = mecanism;
                }
                public override bool PreFilterMessage(ref ICoreMessage m)
                {
                    if (this.IsNotAvailable(m) || ! this.m_mecanism.CurrentSurface.Focused )
                        return false;
                   enuKeys c = (enuKeys)m.WParam | CoreApplicationManager.ModifierKeys;
                  
                    switch (m.Msg)
                    {
                        case WM_KEYDOWN:
                            if ((m_mecanism.Element != null) &&
                                 (m_mecanism.m_caretEditor.Visible)&&
                                m_mecanism.m_caretEditor.Actions.Contains(c))
                           {
                                m_mecanism.m_caretEditor.Actions[c].DoAction();
                                return true;
                            }
                            if (this.Contains(c))
                            {
                                this[c].ShortCutDemand = c;
                                this[c].DoAction();
                                return true;
                            }
                            return false;
                        case WM_KEYUP:
                            if (this.Contains(c))
                            {
                                //already do action return false                    
                                return true;
                            }
                            break;
                    }
                    return false;
                }
            }

            
            class TextElementEditor : IGK.ICore.TextEditor.TextHostEditor
            {
                private Mecanism m_mecanism;

                public override void Render(ICoreGraphics graphics, Rectanglef rectangle)
                {
                    base.Render(graphics, rectangle);
                }
                sealed class EditElementAction : TextEditorActionBase
                {
                    public override void DoAction()
                    {
                        (this.Editor as TextElementEditor).Visible = false;
                        (this.Editor as TextElementEditor).m_mecanism.Actions[enuKeys.Control | enuKeys.E].DoAction();
                        (this.Editor as TextElementEditor).Visible = true ;
                        //remove edition keys pres
                    }
                }
                sealed class ToggleElementRenderView : TextEditorActionBase
                {
                    public override void DoAction()
                    {
                        var e = (this.Editor as TextElementEditor).m_mecanism.Element;
                        if (e == null)
                            return;
                        if(e.RenderMode  == enuTextElementMode.Text )
                        {
                        e.RenderMode 
                            =  enuTextElementMode.Path;
                        }
                        else{
                            e.RenderMode 
                            =  enuTextElementMode.Text;
                        }
                        e.InitElement();
                        
                    }
                }

                protected override void InitActions(TextHostEditor.ActionCollections actions)
                {
                    base.InitActions(actions);
                    actions.Add(enuKeys.Control | enuKeys.E, new EditElementAction());
                    actions.Add(enuKeys.Control | enuKeys.T, new ToggleElementRenderView());
                 
                }
            
                public TextElementEditor(Mecanism mecanism)
                    : base()
                {
                    this.m_mecanism = mecanism;
                    this.m_mecanism.ElementChanged += mecanism_ElementChanged;
                }
                public override void CancelEdition()
                {
                    var l = this.m_mecanism.Element;                    
                    this.m_mecanism.EndEdition();
                    if (l.Text?.Length == 0)
                    {
                        this.m_mecanism.CurrentLayer.Elements.Remove(l);
                    }
                }

                void mecanism_ElementChanged(object sender, CoreWorkingElementChangedEventArgs<RectangleElement> e)
                {
                    if (this.m_mecanism.Element  != null)
                    {
                        this.Bind(this.m_mecanism);
                        this.Visible = true;                 
                    }
                    else
                    {
                        this.Bind(null);
                        this.Visible = false;
                    }
                }
         
                public override void Dispose()
                {
                    base.Dispose();
                }

                public bool Available { get; set; }
            }
            public Mecanism()
            {
                this.m_caretEditor = new TextElementEditor(this);
            }
            public void OnFontDefinitionChanged(string newFontDefinition)
            {
                if (this.m_configuring || (this.Element == null))
                    return;
                this.m_configuring = true;
                this.Element.Font.CopyDefinition(newFontDefinition);
             //   if (this.Element.IsLoading )
                this.Element.InitElement();
                this.Invalidate();
                this.m_configuring = false;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }

            protected override void OnCurrentSurfaceSizeZoomChanged(EventArgs e)
            {
                base.OnCurrentSurfaceSizeZoomChanged(e);
                if (this.m_caretEditor != null)
                {
                    this.m_caretEditor.Refresh();
                }
            }

            public override bool Register(ICore2DDrawingSurface surface)
            {

                bool v = base.Register(surface);
                if (v)
                {
                    var c = surface as ICore2DDrawingMultiFrameSurface;
                }
                surface.KeyDown += _keyDown;
                surface.KeyPress += surface_KeyPress;
                surface.GotFocus += surface_GotFocus;
                surface.LostFocus += surface_LostFocus;
                return v;
            }

            void surface_LostFocus(object sender, EventArgs e)
            {
                if (this.m_listener != null)
                    this.m_listener.OnLostFocus();
            }

            void surface_GotFocus(object sender, EventArgs e)
            {
                if (this.m_listener != null)
                    this.m_listener.OnGotFocus();
            }

            void surface_KeyPress(object sender, CoreKeyEventArgs e)
            {
                char ch = (char)e.KeyCode;
                if (!char.IsControl(ch) && (this.m_listener != null))
                {
                    this.m_listener.OnKeyPress((int)e.KeyCode, true );
                }

            }

            private void _keyDown(object sender, CoreKeyEventArgs e)
            {
                if (this.m_listener != null)
                {
                    this.m_listener.OnKeyPress((int)(e.KeyCode| e.Modifiers ), false );
                }
            }
            public override bool UnRegister()
            {
                var surface = this.CurrentSurface;
                if (this.m_caretEditor != null)
                {
                    this.m_caretEditor.Bind(null);   
                }
                surface.KeyDown -= _keyDown;
                surface.KeyPress -= surface_KeyPress;
                surface.GotFocus -= surface_GotFocus;
                surface.LostFocus -= surface_LostFocus;
                return base.UnRegister();
            }
            public override void Dispose()
            {
                if (this.m_caretEditor != null)
                {
                    this.m_caretEditor.Dispose();
                    this.m_caretEditor = null;
                }
                base.Dispose();
            }

            /// <summary>
            /// get property of the font size according to current bound definition
            /// </summary>
            internal void BuildSingleLineFromBound(Rectanglef bounds)
            {
                this.setFontSize(bounds);
                int v_lineCount = Math.Max(_getElementLineCount(), 1);
                this.m_singleLineBound = new Rectanglef(
                       bounds.X,
                       bounds.Y,
                       bounds.Width,
                       (bounds.Height / (float)v_lineCount));

            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }
            protected override void BeginMove(CoreMouseEventArgs e)
            {
                base.BeginMove(e);

            }
            protected override void EndMove(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                Vector2f d = CoreMathOperation.GetDistanceP(EndPoint, StartPoint);
                if (this.Element != null)
                {
                    Move(d, false);
                    this.Element.ResetTransform();
                    this.BuildSingleLineFromFont(this.Element.Bounds);
                    this.ShowSnippets();

                }
                if (this.State == ST_MOVING)
                    this.State = ST_EDITING;
                this.CurrentSurface.Cursor = this.GetCursor();
                this.Element.ResumeLayout();
                this.Invalidate();
            }
            public void setFontSize(Rectanglef bounds)
            {
                int v_lineCount = Math.Max (1,_getElementLineCount());// this.Element.Lines.Length;
               
                CoreFont ft = this.Element.Font;
                //get font size in point 
                float v_fontSize = (((bounds.Height / (float)v_lineCount))+"px").ToPixel () * (ft.GetEmHeight() / ft.GetLineSpacing());
                ft.FontSize = v_fontSize;
            }

            private int _getElementLineCount()
            {
                if ((this.Element == null) || (this.Element.Lines == null))
                    return 0;
                return this.Element.Lines.Length;
            }

            /// <summary>
            /// get property of the font size according to current bound definition
            /// </summary>
            internal void BuildSingleLineFromFont(Rectanglef bounds)
            {
                var v_e = this.Element;
                int i = _getElementLineCount();
                
                    
                    if (i == 0)
                    {
                        this.m_singleLineBound = Rectanglef.Empty;
                        return;
                    }
                    CoreFont ft = v_e.Font;
                    //calculate font size from bround
                    float v_height = ft.FontSize * ft.GetLineSpacing() / ft.GetEmHeight();

                    this.m_singleLineBound = new Rectanglef(
                       bounds.X,
                       bounds.Y,
                       bounds.Width,
                       v_height);
                

            }

            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                if (this.Element != null)
                {
                    var sn = CurrentSurface.CreateSnippet(this, DM_FONTSIZE, 5);
                    sn.Shape = enuSnippetShape.Diadmond;
                    this.AddSnippet(sn);
                }
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                if (this.RegSnippets.Count >= 5)
                {
                    this.RegSnippets[5].Location =
                        CurrentSurface.GetScreenLocation(
                        new Vector2f(this.m_singleLineBound.X,
                            this.m_singleLineBound.Bottom));
                }
            }

            protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(e);
                this.BuildSingleLineFromFont(this.Element.Bounds);
                
                if (e.ID == Core2DDrawingChangement.FontChanged.ID)
                {
                    this.m_caretEditor.Font.CopyDefinition(this.Element.Font.GetDefinition());
                }
                else {
                    this.m_caretEditor.Update(false);
                }
                this.Invalidate();
            }

            protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<RectangleElement> e)
            {
                
                base.OnElementChanged(e);
                if (e.OldElement !=null)
                    (e.OldElement as TextElement).Font.FontDefinitionChanged -= Font_FontDefinitionChanged;
                if (this.Element != null)
                {
                    var s = (e.NewElement as TextElement);
                    
                    s.Font.FontDefinitionChanged += Font_FontDefinitionChanged;

                    //get single line bound
                    var bounds = s.Bounds;
                    this.m_singleLineBound = new Rectanglef(
       bounds.X,
       bounds.Y,
       bounds.Width,
       s.Font.FontSize);
                }
                else {
                    this.m_caretEditor.Bind(null);
                    this.m_caretEditor.Visible = false;
                }
                TextElement.Mecanism.CurrentMecanism = this;
            }

            void Font_FontDefinitionChanged(object sender, EventArgs e)
            {
                if (this.Element != null)
                {
                    this.BuildSingleLineFromFont(this.Element.Bounds);
                    this.m_caretEditor.Font.CopyDefinition(this.Element.Font.GetDefinition());
                }
            }
            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
                if (this.Element != null)
                {
                    object o = device.Save();
                    this.ApplyCurrentSurfaceTransform(device);

                    device.DrawRectangle(Colorf.Yellow,
                        m_singleLineBound.X,
                        m_singleLineBound.Y,
                        m_singleLineBound.Width,
                        m_singleLineBound.Height);
                    if (this.m_listener != null)
                    {
                        this.m_listener.Render(device, this.Element.Bounds );
                    }
                    device.Restore(o);

                  
                }
            }

            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                var v_e = this.Element;
                if (v_e ==null)
                return;
                switch (this.Snippet.Demand)
                {
                    case DM_FONTSIZE:
                        //set font size
                        var h = v_e.Lines;
                        int ln = Math.Max( h!=null? h.Length : 0, 1);
                        Rectanglef rc = v_e.Bounds;
                        rc.Height = (e.FactorPoint.Y > rc.Y ? Math.Abs(rc.Y - e.FactorPoint.Y) : 0)
                            * ln;
                        this.setFontSize(rc);
                        rc.Height /= ln;

                        //setup m_singleLineBound manualy
                        this.m_singleLineBound = rc;
                        this.Snippet.Location = e.Location;
                        this.Invalidate();
                        return;

                    default:
                        base.UpdateSnippetEdit(e);
                        this.BuildSingleLineFromFont(v_e.Bounds);
                        break;
                }
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                this.UpdateSnippetEdit(e);
                this.Element.ResumeLayout();
                this.InitSnippetsLocation();
                this.Invalidate();

            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                if (this.Element == null)
                {
                    this.State = ST_NONE; return;
                }
                this.EndPoint = e.FactorPoint;
                Rectanglef b = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.BuildSingleLineFromBound(b);
                b.Height = (float)Math.Round(b.Height)+1;
                this.Element.Bounds = b;

                this.Element.InitElement();
                this.CurrentSurface.RefreshScene();
                this.InitSnippetsLocation();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);
            }
            public override void EndEdition()
            {
                base.EndEdition();
                this.m_caretEditor.Bind(null);
                this.m_caretEditor.Visible =false;//.OnLostFocus();
            }

            public int PosY
            {
                get { return m_PosY; }
                set
                {
                    if (m_PosY != value)
                    {
                        m_PosY = value;
                    }
                }
            }
            public int PosX
            {
                get { return m_PosX; }
                set
                {
                    if (m_PosX != value)
                    {
                        m_PosX = value;
                    }
                }
            }
            public new TextElement Element
            {
                get
                {
                    return base.Element as TextElement;
                }
                set
                {
                    base.Element = value;
                }
            }


            /// <summary>
            /// get the number of lines return -1 if element is null
            /// </summary>
            /// <returns></returns>
            internal int GetLinesCount()
            {
                var e = this.Element;
                if (e == null)
                    return -1;
                return e.Lines.Length;                
            }

            internal int GetLineLength(int line)
            {
                return this.GetLines().CoreGetValue(line, string.Empty).Length;
            }

            private string[] GetLines()
            {
                if (this.Element != null)
                    return this.Element.Lines;
                return null;
            }

            public string Name
            {
                get
                {
                    return this.CurrentSurface.Id;
                }
                set
                {
                    //this. CurrentSurface.Id = value;
                }
            }

            public string Text
            {
                get
                {
                    var e = this.Element;
                    if (e != null)
                    {
                        return e.Text;
                    }
                    return string.Empty;
                }
                set
                {
                    var e = this.Element;
                   if (e != null)
                   {
                       e.SuspendLayout();
                       e.Text = value;
                       e.ResumeLayout();
                   }
                }
            }

            public IntPtr Handle
            {
                get { return this.CurrentSurface.SceneHandle; }
            }

            public Rectanglef TextBound
            {
                get { return //this.CurrentSurface.GetScreenBound (
                    this.Element.Bounds
                    //)
                    ; }
            }

            public void RegisterListener(ITextEditorListener listener)
            {
                this.m_listener = listener;

            }


            public float ZoomY
            {
                get { return this.CurrentSurface.ZoomY; }
            }

            public float ZoomX
            {
                get { return this.CurrentSurface.ZoomX; }
            }
            public Vector2f GetScreenLocation(Vector2f factorLocation)
            {
                return this.CurrentSurface.GetScreenLocation(factorLocation);
            }

            internal void Update(TextElement textElement)
            {
                if (this.Element == textElement)
                {
                    //textElement.BuildBeforeResetTransform();
                    this.DisableSnippet();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                }
                this.Invalidate();
            }

            public string TextFontDefinition
            {
                get {
                    if (this.Element !=null)
                        return this.Element.Font.GetDefinition();
                    return string.Empty;
                }
            }

            public static Mecanism CurrentMecanism { get; private set; }
        }


        /*
         * 
         *
            class TextElementCaretRenderEditor : ICore2DDrawingFrameRenderer
            {
                private Mecanism m_mecanism;
                private ICoreCaret m_caretManager;
                private int m_SelectionStart;
                private int m_CurX;
                private int m_CurY;

                public int CurY
                {
                    get { return m_CurY; }
                    set
                    {
                        if (m_CurY != value)
                        {
                            m_CurY = value;
                            OnCursorChanged(EventArgs.Empty);
                        }
                    }
                }
                public event EventHandler CursorChanged;
                ///<summary>
                ///raise the CursorChanged 
                ///</summary>
                protected virtual void OnCursorChanged(EventArgs e)
                {
                    this.__updateLocation();
                    if (CursorChanged != null)
                        CursorChanged(this, e);
                }

                public int CurX
                {
                    get { return m_CurX; }
                    set
                    {
                        if (m_CurX != value)
                        {
                            m_CurX = value;
                            OnCursorChanged(EventArgs.Empty);
                        }
                    }
                }

                public int SelectionStart
                {
                    get { return m_SelectionStart; }
                    set
                    {
                        if (m_SelectionStart != value)
                        {
                            m_SelectionStart = value;
                            OnSelectionStartChanged(EventArgs.Empty);
                        }
                    }
                }
                public event EventHandler SelectionStartChanged;

                protected virtual void OnSelectionStartChanged(EventArgs e)
                {
                    if (SelectionStartChanged != null)
                    {
                        SelectionStartChanged(this, e);
                    }
                }


                public TextElementCaretRenderEditor(TextElement.Mecanism mecanism, ICoreCaret caret)
                {
                    this.m_mecanism = mecanism;
                    this.m_caretManager = caret;

                    this.m_caretManager.Activate(false);
                    this.m_mecanism.ElementChanged += m_mecanism_ElementChanged;
                    
                    if (this.m_mecanism.Element != null)
                    {
                        RegEvent(this.m_mecanism.Element as TextElement);
                    }
                   
                }
                internal void Register(ICore2DDrawingSurface surface)
                {
                    surface.ZoomChanged += CurrentSurface_ZoomChanged;
                    surface.ZoomModeChanged += surface_ZoomModeChanged;
                    surface.GotFocus += surface_GotFocus;
                    surface.LostFocus += surface_LostFocus;
                    surface.SizeChanged += surface_SizeChanged;
                }

                void surface_SizeChanged(object sender, EventArgs e)
                {
                    __updateInfo();
                }

                void surface_ZoomModeChanged(object sender, EventArgs e)
                {
                    __updateInfo();
                }

                private void __updateInfo()
                {
                    if (this.m_mecanism.Element != null)
                    {
                        this._updateSize();
                        this.__updateLocation();
                        this.m_caretManager.Activate(this.m_mecanism.CurrentSurface.Focused);
                    }
                    else {
                        this.m_caretManager.Activate(false);
                    }
                }
                void CurrentSurface_ZoomChanged(object sender, EventArgs e)
                {
                    __updateInfo();
                }
                void surface_LostFocus(object sender, EventArgs e)
                {
                    this.m_caretManager.Activate(false);
                }

                void surface_GotFocus(object sender, EventArgs e)
                {
                    this.m_caretManager.Activate(true);
                }

                internal void Unregister(ICore2DDrawingSurface surface)
                {
                    surface.ZoomChanged -= CurrentSurface_ZoomChanged;
                    surface.ZoomModeChanged -= surface_ZoomModeChanged;
                    surface.SizeChanged -= surface_SizeChanged;
                    surface.GotFocus -= surface_GotFocus;
                    surface.LostFocus -= surface_LostFocus;
                }
              

                void m_mecanism_ElementChanged(object sender, CoreWorkingElementChangedEventArgs<RectangleElement> e)
                {
                    if (e.OldElement != null)
                    {

                        e.OldElement.PropertyChanged -= _Element_PropertyChanged;
                    }
                    if (e.NewElement != null)
                    {
                        RegEvent(e.NewElement as TextElement);
                        this.m_caretManager.Activate(true);
                    }
                    else {
                        this.m_caretManager.Activate(false);
                    }
                }

                private void RegEvent(TextElement textElement)
                {
                    this.m_SelectionStart = string.IsNullOrEmpty (textElement.Text )?0:  textElement.Text.Length ;
                    this._updateSize();
                    this.__updateLocation();
                    this.m_caretManager.Activate(true);
                    textElement.PropertyChanged += _Element_PropertyChanged;
                }

                void _Element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
                {
                    switch ((enu2DPropertyChangedType)e.ID)
                    {
                        case enu2DPropertyChangedType.MatrixChanged :
                        case enu2DPropertyChangedType.SizeChanged :
                        case enu2DPropertyChangedType.DefinitionChanged:
                            _updateSize();
                            __updateLocation();
                            break;
                    }

                }

                private void _updateSize()
                {
                    CoreFont ft = this.m_mecanism.Element.Font;
                    //calculate font size from bround
                    float v_height =( ft.FontSize * ft.GetLineSpacing() / ft.GetEmHeight()) * 
                        this.m_mecanism.CurrentSurface.ZoomY ;
                    Vector2f c = this.m_mecanism.CurrentSurface.GetScreenLocation(new Vector2f(0, v_height));
                    this.m_caretManager.LineHeight = (int)v_height;
                }
                private void __updateLocation()
                {
                    var e = this.m_mecanism.Element;
                    if (e != null)
                    {
                        string v_text = e.Text;
                        Rectanglef r = e.Bounds;

                        int v_line = this.m_CurY;
                        int v_col = this.m_CurX;
                        string v_text1 = v_text.Split('\n')[v_line];

                        Rectanglef  rs = CoreApplicationManager.Instance.ResourcesManager.MeasureString(
                            //measure text
                        v_text1,
                        e.Font,
                        new Rectanglef(Vector2f.Zero , e.Bounds.Size ),
                        0, v_col>0?v_col :1
                        );
                        var s = rs.Size;
                        float zx = this.m_mecanism.CurrentSurface.ZoomX;
                        float zy = this.m_mecanism.CurrentSurface.ZoomY;
                        float w = v_col ==0? 0.0f :  s.Width * zx;
                        float h = v_line==0? 0.0f : s.Height * zy;

                        r = this.m_mecanism.CurrentSurface.GetScreenBound(r);
                      //  rs = this.m_mecanism.CurrentSurface.GetScreenBound(rs);
                        this.m_caretManager.Location = new Vector2f(
                           //r.X + (rs.X* zx) + w , r.Y + h
                          r.X + (rs.X * zx) + w, r.Y  + h
                            );
                    }
                }

                void m_mecanism_ElementChanged(object sender, CoreWorkingElementChangedEventArgs<TextElement> e)
                {

                }
                public void Render(ICoreGraphics device)
                {
                    var e = this.m_mecanism.Element;
                    if (e != null)
                    {
                        Rectanglef r = this.m_mecanism.Element.Bounds;
                        r = this.m_mecanism.CurrentSurface.GetScreenBound(r);
                        device.SmoothingMode = enuSmoothingMode.None;
                        device.DrawRectangle(Colorf.CornflowerBlue,
                            r.X, r.Y, r.Width, r.Height);

                        Size2f s = CoreApplicationManager.Instance.ResourcesManager.MeasureString(
                            //measure text
                        e.Text,
                        e.Font
                        );
                        float w = s.Width * this.m_mecanism.CurrentSurface.ZoomX;
                        float h = s.Height * this.m_mecanism.CurrentSurface.ZoomY;

               //         device.DrawRectangle(Colorf.Red ,
               //r.X, r.Y, w, h);


                        var v_cs = CoreApplicationManager.Instance.ResourcesManager.MeasureString(
                            //measure text
                        e.Text,
                        e.Font, 
                        this.m_mecanism.Element.Bounds,
                        0, e.Text.Length 
                        );
                        float zx = this.m_mecanism.CurrentSurface.ZoomX;
                        float zy = this.m_mecanism.CurrentSurface.ZoomY;

                        w = v_cs.Width * this.m_mecanism.CurrentSurface.ZoomX;
                        h = v_cs.Height * this.m_mecanism.CurrentSurface.ZoomY;

               //         device.DrawRectangle(Colorf.Aqua,
               //r.X+v_cs.X , r.Y, w, h);


                        var b = this.m_mecanism.Element.GetBound ();

                        var sj = device.Save();
                        this.m_mecanism.ApplyCurrentSurfaceTransform(device);

               //         device.DrawRectangle(Colorf.Maroon,
               //b.X, b.Y,b.Width ,b.Height );


//                        device.DrawRectangle(Colorf.OrangeRed ,
//v_cs.X, v_cs.Y, v_cs.Width, v_cs.Height);

                      //  device.ScaleTransform(zx, zy, enuMatrixOrder.Append);
                        //device.DrawString(e.Text, e.Font, Colorf.Black, 
                        //    b);
                        device.Restore(sj);
                        device.SmoothingMode = enuSmoothingMode.AntiAliazed;
                    }

                }

                internal void Dispose()
                {
                    if (this.m_caretManager != null)
                    {
                        this.m_caretManager.Dispose();
                        this.m_caretManager = null;
                    }
                }



                protected internal virtual void BindActions()
                {
                    this.m_mecanism.Actions.Add(enuKeys.Left, new MoveLeftAction());
                    this.m_mecanism.Actions.Add(enuKeys.Up, new MoveUpAction());
                    this.m_mecanism.Actions.Add(enuKeys.Right , new MoveRightAction());
                    this.m_mecanism.Actions.Add(enuKeys.Down, new MoveDownAction());
                    this.m_mecanism.Actions.Add(enuKeys.Home , new MoveHomeAction());
                    this.m_mecanism.Actions.Add(enuKeys.End, new MoveEndAction());
                }

                public abstract class EditorMecanismAction  : CoreMecanismActionBase
                { 
                    public new Mecanism Mecanism{
                        get {
                            return base.Mecanism as Mecanism;
                        }
                    }
                }
                #region Mecanism Action
                class MoveLeftAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var c = this.Mecanism.m_caretEditor.CurX;
                        var r = this.Mecanism.m_caretEditor.CurY;
                        if (c > 0)
                        {
                            c--;
                        }
                        if (c == 0)
                        {
                            if (r > 0)
                            {
                                r--;
                                c = this.Mecanism.GetLineLength(r);
                            }
                        }
                        this.Mecanism.m_caretEditor.SetCursor(c, r);
                        return true;
                    }
                }
                class MoveUpAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var m = this.Mecanism;
                        var x = m.m_caretEditor.CurX;
                        var y = m.m_caretEditor.CurY;
                        if (y > 0)
                        {
                            y--;
                            x = Math.Min(x, m.GetLineLength(y));
                        }
                        m.m_caretEditor.SetCursor(x, y);
                        return true;
                    }
                }

                class MoveDownAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var m = this.Mecanism;
                        var x = m.m_caretEditor.CurX;
                        var y = m.m_caretEditor.CurY;
                        if ((y >0) && (y< m.GetLinesCount()-1))
                        {
                            y++;
                            x = Math.Min(x, m.GetLineLength(y));
                        }
                        m.m_caretEditor.SetCursor(x, y);
                        return true;
                    }
                }
                class MoveRightAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var m = this.Mecanism;
                        var x = m.m_caretEditor.CurX;
                        var y = m.m_caretEditor.CurY;
                        int g = m.GetLineLength(y);
                        if (x < g - 1)
                        {
                            x++;
                        }
                        else {
                            if (y < m.GetLinesCount()-1)
                            {
                                y++;
                                x = 0;
                            }
                        }
                        
                        

                        m.m_caretEditor.SetCursor(x, y);
                        return true;
                    }
                }

                class MoveHomeAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var m = this.Mecanism;
                        var x = 0;
                        var y = 0;
                        m.m_caretEditor.SetCursor(x, y);
                        return true;
                    }
                }
                class MoveEndAction : EditorMecanismAction
                {
                    protected override bool PerformAction()
                    {
                        var m = this.Mecanism;
                        var x = m.m_caretEditor.CurX;
                        var y = m.m_caretEditor.CurY;                        
                        int line =  m.GetLinesCount();
                        if (line >0){
                            int g = m.GetLineLength(y);
                            y = line - 1;
                            x = g;
                        }
                        m.m_caretEditor.SetCursor(x, y);
                        return true;
                    }
                }
                #endregion


                internal void SetCursor(int x, int y)
                {
                    bool v = (x != this.m_CurX) || (y != this.m_CurY);
                    this.m_CurX = x;
                    this.m_CurY = y;
                    if (v) {
                        OnCursorChanged(EventArgs.Empty);
                    }
                }
            }



            /// <summary>
            /// used to render element
            /// </summary>
            public class TextBuffer
            {
                TextElement.Mecanism m_mecanism;
                List<string> m_text;
                StringBuilder m_stringBuilder; //global string builder
                StringBuilder m_lstringBuilder; //line string builder
                public TextElement.Mecanism Mecanism
                {
                    get
                    {
                        return this.m_mecanism;
                    }
                }
                public TextBuffer(TextElement.Mecanism mecanism)
                {
                    this.m_mecanism = mecanism;
                    this.m_text = new List<string>();
                    this.m_stringBuilder = new StringBuilder();
                }
                public void Populate()
                {
                    this.Clear();
                    this.m_text.AddRange(this.m_mecanism.Element.Lines);
                }
                public void Clear()
                {
                    this.m_text.Clear();
                }
                public void Flush()
                {
                    m_stringBuilder.Length = 0;
                    m_stringBuilder.Append(string.Join(LINE_SEPARATOR.ToString(), m_text.ToArray()));
                    this.m_mecanism.Element.Text = m_stringBuilder.ToString();
                }
                internal void AppendNewLine()
                {
                    this.m_text[this.Mecanism.PosY - 1] = this.m_text[this.Mecanism.PosY - 1].Insert(this.m_mecanism.PosX, LINE_SEPARATOR.ToString());
                    this.m_mecanism.PosX++;
                    this.Flush();
                }
                internal void Append(char p)
                {
                    m_lstringBuilder = new StringBuilder(this.m_text[this.Mecanism.PosY - 1]);
                    m_lstringBuilder.Insert(m_mecanism.PosX, p);
                    this.m_text[this.Mecanism.PosY - 1] = m_lstringBuilder.ToString();
                    this.m_mecanism.PosX++;
                    this.Flush();
                }
                public void Remove(int xpos, int length)
                {
                    m_lstringBuilder = new StringBuilder(this.m_text[this.Mecanism.PosY - 1]);
                    m_lstringBuilder.Remove(xpos, length);
                    this.m_text[this.Mecanism.PosY - 1] = m_lstringBuilder.ToString();
                    this.Flush();
                }
                internal void RemoveCurrentLine()
                {
                    if (this.m_mecanism.PosY > 1)
                    {
                        int index = this.m_mecanism.PosY - 1;
                        this.m_text.RemoveAt(index);
                        this.m_mecanism.PosY = index;
                        this.m_mecanism.PosX = this.m_text[index - 1].Length - 1;
                        this.Flush();
                    }
                }
            }

         */
    }
}


