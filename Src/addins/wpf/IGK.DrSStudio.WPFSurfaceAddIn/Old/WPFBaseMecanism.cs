

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFBaseMecanism.cs
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
file:WPFBaseMecanism.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Input;
namespace IGK.DrSStudio.WPFSurfaceAddIn
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.Mecanism;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WPFSurfaceAddIn.Actions;
    using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
    using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects;
    /// <summary>
    /// represent the base wpf mecanism for creating element
    /// </summary>
    public abstract class WPFBaseMecanism : 
        CoreMecanismBase
        , ICoreWorkingMecanism 
    {     
        MecanimsManagerBase m_mecanismManager;
        internal const int ST_MOVING = 20;
        internal const int ST_ROTATING = ST_MOVING + 1;
        internal const int ST_RESIZING = ST_MOVING + 2;
        private Vector2d  m_StartPoint;
        private Vector2d m_EndPoint;
        public Vector2d EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                }
            }
        }
        public Vector2d  StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                }
            }
        }
        /// <summary>
        /// get if the shift key pressed
        /// </summary>
        public new bool IsShiftKey { get {
            if (Keyboard.FocusedElement == null)
            {
                Keyboard.Focus(this.CurrentSurface.RootElement);
            }
            return Keyboard.IsKeyDown (Key.LeftShift ) || Keyboard.IsKeyDown (Key.RightShift );
             }
        }
        /// <summary>
        /// get if the control key pressed
        /// </summary>
        public new bool IsControlKey { get {
            if (Keyboard.FocusedElement == null)
            {
                Keyboard.Focus(this.CurrentSurface.RootElement);
            }
            return Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl);
        } }
        public WPFBaseMecanism():base()
        {
            this.m_mecanismManager = new MecanimsManagerBase(this);
        }
        public WorkingObjects.WPFDocument CurrentDocument { get { return this.CurrentSurface.CurrentDocument; } }
        public WorkingObjects.WPFLayer CurrentLayer { get { return this.CurrentDocument.CurrentLayer; } }
        protected override ICoreSnippetCollections CreateSnippetCollections()
        {
            return new WPFSnippetCollections(this);
        }
        public override ICoreMecanismActionCollections CreateActionCollections()
        {            
            return new MecanismActionCollections(this);
        }
        #region ICoreWorkingMecanism Members
        protected virtual void BeginCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFMouseButtonEventArgs e)
        {
        }
        public new WPFHostSurface CurrentSurface {
            get {
                return base.CurrentSurface as WPFHostSurface;
            }
        }
        public  override  void EndEdition()
        {
            this.Element = null;
            this.CurrentLayer.Select(null);
            this.RegSnippets.Disabled();
            this.Snippet = null;
            this.State = ST_NONE;
        }
        protected void GotoDefaultTool()
        {
            if (this.CurrentSurface.CurrentTool != this.CurrentSurface.DefaultTool)
                this.CurrentSurface.CurrentTool = this.CurrentSurface.DefaultTool;
        }
        public override  void Edit(ICoreWorkingObject element)
        {
            IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayeredElement l
                = element as IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayeredElement;
            if (l != null)
            {
                this.State = ST_NONE;
                this.CurrentLayer.Select(l);
                this.Element = l;
                if (l is  WPFTransformableElement )
                 (l as WPFTransformableElement).ResetTransform();
                this.StartPoint = this.StartPoint;
                this.EndPoint = this.EndPoint;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.State = ST_EDITING;
            }
        }
        /// <summary>
        /// generate snippet
        /// </summary>
        protected override void GenerateSnippets()
        {
            this.RegSnippets.Disabled ();
        }
        /// <summary>
        /// init snippet location
        /// </summary>
        protected override  void InitSnippetsLocation()
        { 
        }
        public override bool Register(ICoreWorkingSurface surface)
        {
            WinUI.WPFHostSurface c = surface as WinUI.WPFHostSurface;
            if (c == null)
                return false;
            c.CanvasMouseDown += c_CanvasMouseDown;
            c.CanvasMouseMove += c_CanvasMouseMove;
            c.CanvasMouseUp += c_CanvasMouseUp;
            c.CanvasKeyDown += new System.Windows.Input.KeyEventHandler(c_CanvasKeyDown);
            c.CanvasKeyUp += new System.Windows.Input.KeyEventHandler(c_CanvasKeyUp);
            base.CurrentSurface = c;
            this.GenerateActions();
            this.m_mecanismManager.Register();
            System.Windows.Input.Mouse.OverrideCursor = GetCursor();
            return true;
        }

        public override bool UnRegister()
        {
            WinUI.WPFHostSurface c = this.CurrentSurface;
            this.m_mecanismManager.UnRegister();
            c.CanvasMouseDown -= c_CanvasMouseDown;
            c.CanvasMouseMove -= c_CanvasMouseMove;
            c.CanvasMouseUp -= c_CanvasMouseUp;
            c.CanvasKeyDown -= new System.Windows.Input.KeyEventHandler(c_CanvasKeyDown);
            c.CanvasKeyUp -= new System.Windows.Input.KeyEventHandler(c_CanvasKeyUp);
            this.EndEdition();
            return true;
        }
        private System.Windows.Input.Cursor GetCursor()
        {
            Type t = this.GetType().DeclaringType;
            if (t != null)
            {
                WPFElementAttribute obj = Attribute.GetCustomAttribute(
                    t, typeof(CoreWorkingObjectAttribute), false) as WPFElementAttribute ;
                if (obj != null)
                {
                    System.Windows.Forms.Cursor ctr = CoreSystem.Instance.Resources.GetCursor(obj.CursorKey);
                    System.Windows.Input.Cursor c =
                       Utils.CursorHelper.FromHandle(ctr.Handle);// new System.Windows.Input.Cursor(mem);
                    //ctr.Dispose();
                    return c;
                }
            }
            return System.Windows.Input.Cursors.Cross;
        }
        void c_CanvasKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnKeyUp(e);
        }
        protected virtual void OnKeyUp(System.Windows.Input.KeyEventArgs e)
        {
            if (!e.Handled)
            {
                System.Windows.Forms.Keys c = GetKeys(e);
                if (this.Actions.Contains(c))
                {
                    this.Actions[c].DoAction();
                    e.Handled = true;
                }
            }
        }
        private System.Windows.Forms.Keys GetKeys(System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Forms.Keys k = Keys.None;
            Type t =k.GetType ();
            if (Enum.IsDefined(t, e.Key.ToString()))
            {
                k = (System.Windows.Forms.Keys)Enum.Parse(typeof(System.Windows.Forms.Keys), e.Key.ToString());
                if ((e.KeyboardDevice.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                    k |= Keys.Control;
                if ((e.KeyboardDevice.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift)
                    k |= Keys.Shift;
            }
            return k;
        }
        void c_CanvasKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            OnKeyDown(e);
        }
        protected virtual void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
        }
        protected override void GenerateActions()
        {
            this.Actions.Add(enuKeys.Control | Keys.E, new Actions.EditItem());
            this.Actions.Add(enuKeys.Delete , new Actions.RemoveItemAction ());
            this.Actions.Add(enuKeys.Control | Keys.A , new Actions.SelectAllItemsAction ());
        }
        void c_CanvasMouseUp(object sender, WPFMouseButtonEventArgs  e)
        {
            OnMouseUp(e);
        }
        void c_CanvasMouseMove(object sender, WPFMouseEventArgs e)
        {
            OnMouseMove(e);
        }
        void c_CanvasMouseDown(object sender, WPFMouseButtonEventArgs e)
        {
            OnMouseDown(e);
        }
        protected virtual void OnMouseDown(WPFMouseButtonEventArgs e)
        {
        }
        protected virtual void OnMouseMove(WPFMouseEventArgs e)
        {
        }
        protected virtual void OnMouseUp(WPFMouseButtonEventArgs e)
        {
        }
        public virtual WorkingObjects.WPFElementBase CreateElement()
        {
            Type t = this.GetType().DeclaringType;
            if (t == null)
                return null;
            WorkingObjects.WPFElementBase v_element = t.Assembly.CreateInstance(t.FullName)
                as WorkingObjects.WPFElementBase;
            if (v_element != null)
                InitNewCreateElement(v_element);
            return v_element;
        }
        protected virtual void InitNewCreateElement(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFElementBase v_element)
        {
        }
        public override void Freeze()
        {
            this.IsFreezed = true;
        }
        public override void UnFreeze()
        {
            this.IsFreezed = false;
        }
        #endregion
        protected virtual void UpdateCreateElement(WPFMouseEventArgs e)
        {
            this.EndPoint = e.Location;
        }
        protected virtual void UpdateSnippetElement(WPFMouseEventArgs e)
        {
        }
        public bool SelectSingleElement(Vector2d e)
        {
            foreach(WorkingObjects.WPFLayeredElement l in this.CurrentLayer .Elements )
            {
                if (l.Contains(e))
                {
                    this.CurrentLayer.Select(l);
                    return true;
                }
            }
            return false;
        }
        public bool MultiSelect(Rectangled rc)
        {
            List<WorkingObjects.WPFLayeredElement> m = new List<IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayeredElement>();
            foreach (WorkingObjects.WPFLayeredElement l in this.CurrentLayer.Elements)
            {
                if (l.Intersect(rc))
                {
                    m.Add(l);                                        
                }
            }
            if (m.Count > 0)
            {
                this.CurrentLayer.Select(m.ToArray());
                return true;
            }
            return false;
        }
        /// <summary>
        /// represent the reg snippet
        /// </summary>
        public class WPFSnippetCollections : CoreSnippetCollectionsBase 
        {
            public WPFSnippetCollections( WPFBaseMecanism mecanism):base()
            {                 
            }         
        }
        /// <summary>
        /// register action to be performed by key dow 
        /// </summary>
        public class MecanismActionCollections :
            CoreMecanismActionBaseCollections<WPFElementBase >,
            ICoreMecanismActionCollections,
            IMessageFilter
        {
            public MecanismActionCollections(WPFBaseMecanism mecanism)
                : base(mecanism)
            {
              }         
        }
        public class MecanimsManagerBase
        {
            WPFBaseMecanism m_mecanism;
            public MecanimsManagerBase(WPFBaseMecanism mecanism)
            {
                this.m_mecanism = mecanism;
            }
            internal void Register()
            {
                this.RegisterDocument(this.m_mecanism .CurrentSurface.CurrentDocument );                
            }
            internal void UnRegister()
            {
                this.UnRegisterDocument(this.m_mecanism.CurrentSurface.CurrentDocument);                
            }
            private void RegisterDocument(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument wPFDocument)
            {
                RegisterLayer(wPFDocument.CurrentLayer);
            }
            private void RegisterLayer(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged += new EventHandler(wPFLayer_SelectedElementChanged);
            }
            void wPFLayer_SelectedElementChanged(object sender, EventArgs e)
            {
                m_mecanism.OnLayerSelectedElementChanged(e);
            }
            private void UnRegisterDocument(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument wPFDocument)
            {
                UnRegisterLayer(wPFDocument.CurrentLayer);
            }
            private void UnRegisterLayer(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
            {
                wPFLayer.SelectedElementChanged -= new EventHandler(wPFLayer_SelectedElementChanged);
            }
        }
        protected virtual void OnLayerSelectedElementChanged(EventArgs e)
        {
        }
    }
}

