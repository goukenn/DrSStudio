using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.IO;

namespace IGK.ICore.WinCore.WinUI.Controls
{

  /// <summary>
  /// represent drsstudio attribute editor
  /// </summary>
    public class IGKXAttributeEditor: IGKXUserControl 
    {

     
      
        private AttributeCollection m_Attributes;
        private IGKXLabel c_lb_title;

        private IGKXPanel c_pan_left;
        private IGKXPanel c_pan_content;
        private IGKXPanel c_pan_footer;
        private IGKXPanel c_pan_top;

        private AttributeViewer c_viewer;
        private IGKXSplitter c_splitter;
        private IGKXLabel c_lb_description;
        private TreeView c_trv_nodes;
        private string m_SearchKey;

        private Control c_splitter2;
        private IAttributeEditorLoader m_attributeLoader;
        private IAttributeEditorStoreListener m_storeListener;
        private CoreXmlElement m_CurrentNode;
        private CoreXmlElement m_RootNode;



        public event EventHandler CurrentNodeChanged;
        public event EventHandler<AttributeValueChangedEventArgs> AttributeValueChanged;


        public string SearchKey
        {
            get { return m_SearchKey; }
            set
            {
                if (m_SearchKey != value)
                {
                    m_SearchKey = value;
                    OnSearchKeyChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SearchKeyChanged;

        protected virtual void OnSearchKeyChanged(EventArgs e)
        {
            if (SearchKeyChanged != null)
            {
                SearchKeyChanged(this, e);
            }
        }



        /// <summary>
        /// load a android or resources files
        /// </summary>
        /// <param name="filename"></param>
        public void LoadFile(string filename) 
        {
            CoreXmlElement c = CoreXmlElement.LoadFile(filename);
            if (c!=null)
            {
                this.LoadNode(c);
                this.CurrentNode = c;
            }
        }

        /// <summary>
        /// Load the current node
        /// </summary>
        /// <param name="node"></param>
        public void LoadNode(CoreXmlElement node)
        {
            this.SuspendLayout();
            this.c_trv_nodes.Nodes.Clear();
            this.m_RootNode = null;
            this.m_CurrentNode = null;
            this.m_RootNode = node;
            var tnode = this.c_trv_nodes.Nodes.Add(node.TagName);
            tnode.Tag = node;
            BuildNode(tnode, node);            
            this.ResumeLayout();
            this.PerformLayout();
            this.CurrentNode = node;
        }

        private void BuildNode(TreeNode tnode, CoreXmlElement node)
        {
            foreach (CoreXmlElement  item in node.Childs)
            {
                if (item == null) continue;
                
                TreeNode v_gc = tnode.Nodes.Add(item.TagName);
                v_gc.Tag = item;
                BuildNode(v_gc, item);
            }
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
          
            base.OnLayout(e);
        }
        public int ItemDefaultHeight {
            get {
                return this.c_viewer.ItemHeight;
            }
            set {
                this.c_viewer.ItemHeight = value;
            }
        }
        /// <summary>
        /// get the attributes
        /// </summary>
        public AttributeCollection Attributes
        {
            get { 
                return m_Attributes; 
            }
        }
        
        
        /// <summary>
        /// get the attributes
        /// </summary>
        protected override System.Drawing.Size DefaultSize
        {
            get
            {
                return new System.Drawing.Size(300, 200);
            }
        }
  
        /// <summary>
        /// get the root node element
        /// </summary>
        public CoreXmlElement RootNode
        {
            get { return m_RootNode; }
        }
        [Browsable(false )]
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
        
        /// <summary>
        /// get or set the current node
        /// </summary>
        public CoreXmlElement CurrentNode
        {
            get { return m_CurrentNode; }
            set
            {
                if (m_CurrentNode != value)
                {
                    m_CurrentNode = value;
                    OnCurrentNodeChanged(EventArgs.Empty);
                }
            }
        }


        protected virtual void OnCurrentNodeChanged(EventArgs e)
        {
            this.LoadAttributes();
            if (CurrentNodeChanged != null)
            {
                CurrentNodeChanged(this, e);
            }
        }


        public void OnAttributeValueChanged(AttributeValueChangedEventArgs e)
        {
            if (AttributeValueChanged != null)
                AttributeValueChanged(this, e);
        }
        public IGKXAttributeEditor()
        {
            this.m_ShowTreeView = true;
            this.m_Attributes = CreateAttributeCollections();

            this.SuspendLayout ();
            this.c_trv_nodes = new TreeView();
            this.c_trv_nodes.Dock = DockStyle.Fill;
            this.c_trv_nodes.AfterSelect += c_trv_nodes_AfterSelect;
            //
            //pan left
            //
            this.c_pan_left = new IGKXPanel();
            this.c_pan_left.Width = 180;
            this.c_pan_left.Dock = DockStyle.Left;
            this.c_pan_left.Controls.Add(c_trv_nodes);

            this.c_lb_title = new IGKXLabel();
            this.c_lb_title.Dock = DockStyle.Fill;
            this.c_lb_title.AutoSize = false;
            this.c_lb_title.Font = new Font("Arial", 25, FontStyle.Regular, GraphicsUnit.Point );
            this.c_lb_title.Text = null;



            this.c_lb_description= new IGKXLabel();
            this.c_lb_description.Dock = DockStyle.Fill;
            this.c_lb_description.AutoSize = false;
            this.c_lb_description.Font = new Font("Consolas", 12, FontStyle.Regular, GraphicsUnit.Point );
            this.c_lb_description.Text = null;

            this.c_pan_top = new IGKXPanel();

            this.c_pan_top.Controls.Add(this.c_lb_title);
            this.c_pan_content = new IGKXPanel();
            this.c_pan_footer = new IGKXPanel();
            this.c_viewer = new AttributeViewer(this);
            this.c_viewer.Dock = DockStyle.Fill;
            this.c_splitter = new IGKXSplitter();
            this.c_splitter2 = new IGKXSplitter();
            this.c_splitter2.Dock = DockStyle.Left;
            this.c_splitter.Dock = DockStyle.Bottom ;

            this.c_pan_content.Dock = DockStyle.Fill;
            this.c_pan_footer.Dock = DockStyle.Bottom;
            this.c_pan_top.Dock = DockStyle.Top;

            this.c_pan_top.Height = (int)"12mm".ToPixel();
            this.c_pan_footer.Height = (int)"20mm".ToPixel();
            this.c_pan_footer.Controls.Add(c_lb_description);


            this.c_pan_content.Controls.Add(c_viewer);
            this.Controls.AddRange(new Control[] { 
                this.c_pan_content ,
                this.c_pan_top ,
                this.c_splitter ,
                this.c_pan_footer ,
                this.c_splitter2,
                this.c_pan_left ,
                
            });
            this.ResumeLayout();

            this.AttributeValueChanged += (o, ee) =>
            {
                if (this.CurrentNode != null)
                {
                    this.CurrentNode[ee.Item.Name] = ee.Item.Value;
                }
            };
        }

        void c_trv_nodes_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.CurrentNode = e.Node.Tag as CoreXmlElement;
        }

        protected virtual AttributeCollection CreateAttributeCollections()
        {
            return new AttributeCollection(this);
        }
        public virtual  string DisplayText
        {
            get
            {
                return this.Name;
            }
        }
        /// <summary>
        /// represent a attribute item base class
        /// </summary>
        public class AttributeItem :ICloneable 
        {
            private CoreXmlAttributeValue m_Value;

            public virtual  string DisplayText
            {
                get
                {
                    return this.Name;
                }
            }

            /// <summary>
            /// get or set the attribute description
            /// </summary>
            public virtual string Description { get {
                return string.Format("Category: {0}\nType:{1}", this.Category, this.Type);
            } }
            /// <summary>
            /// get or set the title
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// get or set the name
            /// </summary>
            public virtual  string Name { get; set; }
            /// <summary>
            /// get or set the category of this attribute item
            /// </summary>
            public string Category { get; set; }
            /// <summary>
            /// get or set the type
            /// </summary>
            public string Type { get; set; }
            public CoreXmlAttributeValue Value { get {
                return this.m_Value;
            }
                set {
                    if (this.m_Value != value)
                    {
                        this.m_Value = value;
                        OnValueChanged(EventArgs.Empty);
                    }
                }
            }
            protected internal virtual void RenderValue(ICoreGraphics graphics,
                Rectanglef Bounds,
                Colorf foreColor)
            {
                string v = this.Value;

                if (!string.IsNullOrEmpty(v))
                {                    
                    CoreFont ft = CoreFontRegister.GetFontById(IGKXAttributeEditorThemes.AttributeEditorItemValueFont);

                    graphics.DrawString(v, ft, foreColor, Bounds);
                }
            }
            public event EventHandler ValueChanged;
            ///<summary>
            ///raise the ValueChanged 
            ///</summary>
            protected virtual void OnValueChanged(EventArgs e)
            {
                if (ValueChanged != null)
                    ValueChanged(this, e);
            }


            /// <summary>
            /// return the supported values of this attribute
            /// </summary>
            /// <returns></returns>
            public virtual string[] GetSupportedValues(){
                return null;
            }
            public object[] DefaultValue { get; set; }

            public virtual object Clone()
            {
                return this.MemberwiseClone();
            }
        }
        /// <summary>
        /// represent the attribute collection
        /// </summary>
        public class AttributeCollection : IEnumerable
        {
            private IGKXAttributeEditor owner;
            private List<AttributeItem> m_attribs;
            private List<string> m_keys;

            public AttributeItem this[string key]
            {
                get {
                    foreach (AttributeItem s in m_attribs)
                    {
                        if (s.Name == key)
                            return s;
                    }
                    return null;
                }
            }
            public AttributeCollection(IGKXAttributeEditor owner)
            {                
                this.owner = owner;
                this.m_attribs = new List<AttributeItem>();
                this.m_keys = new List<string>();
            }
            public int Count {
                get {
                    return this.m_attribs.Count;
                }
            }

            /// <summary>
            /// clear attribute collection
            /// </summary>
            public void Clear()
            {
                this.m_keys.Clear();
                this.m_attribs.Clear();
                this.owner.c_viewer.ClearItems();
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_attribs.GetEnumerator();
            }
            /// <summary>
            /// add attribute 
            /// </summary>
            /// <param name="name">type of the attribute</param>
            /// <param name="type">pipe separated attribute type</param>
            public void Add(string name,string type, string value=null, object[] defaultValue=null)
            {
                if (!this.Contains(name))
                {
                    AttributeItem i = new AttributeItem();
                    i.Name = name;
                    i.Title = name;
                    i.Value = value;
                    i.DefaultValue = defaultValue;
                    this.Add(i);
                    
                }
            }
            public void Remove(AttributeItem item)
            {
                if (item ==null)
                    return ;
                if (this.m_attribs.Contains(item)) {
                    this.m_attribs.Remove(item);
                    this.m_keys.Remove(item.Name);
                    item.ValueChanged -= i_ValueChanged;
                }
            }

            public void Add(AttributeItem item)
            {
                this.m_keys.Add(item.Name);
                this.m_attribs.Add(item);
                item.ValueChanged += i_ValueChanged;
                this.owner.c_viewer.AddItem(new AttributeItemHost(this.owner.c_viewer , item));
            }

            void i_ValueChanged(object sender, EventArgs e)
            {
                AttributeItem i = sender as AttributeItem;
                this.owner.OnAttributeValueChanged (
                    new  AttributeValueChangedEventArgs(this.owner,
                        i));
            }
            

            public bool Contains(string name)
            {
                return this.m_keys.Contains(name);
            }

            public void Sort()
            {
                this.m_keys.Sort();
                this.owner.c_viewer.Sort();
            }
        }


       

        public class AttributeViewer : IGKXUserControl
        {
            private AttributeEditAttributeControl c_itemControl;
            private int m_ImageWidth;

            public void UpdateView()
            {
                this.m_ImageWidth = 16;
                c_attributeViewerPanel.PerformLayout();
                c_attributeViewerPanel.Refresh();
            }
            internal class AttribSplitter : Control
            {
                public AttribSplitter()
                {
                    this.Cursor = Cursors.VSplit;
                    this.SetStyle(ControlStyles.Selectable, false);
                    this.SetStyle(ControlStyles.SupportsTransparentBackColor, false);
                    this.SetStyle(ControlStyles.UserPaint, true);
                }
                protected override void OnPaintBackground(PaintEventArgs pevent)
                {
                    //base.OnPaintBackground(pevent);
                }
                protected override void OnPaint(PaintEventArgs e)
                {
                    e.Graphics.Clear(AttributeValueRenderer.AttributeSplitter);
                    base.OnPaint(e);
                    
                }
                protected override void OnMouseDown(MouseEventArgs e)
                {
                    if (e.Button == System.Windows.Forms.MouseButtons.Left)
                    {
                        AttributeViewer v = this.Parent as AttributeViewer;
                        this.Capture = false;
                        v.Capture = true;
                        v.MouseMove += _parentMove;
                        v.MouseUp += _parentUp;
                    }
                    base.OnMouseDown(e);
                }

                private void _parentUp(object sender, CoreMouseEventArgs e)
                {
                    AttributeViewer v = this.Parent as AttributeViewer;
            
                    v.MouseMove -= _parentMove;
                    v.MouseUp -= _parentUp;
                }

                private void _parentMove(object sender, CoreMouseEventArgs e)
                {
                 //mouse move
                    AttributeViewer v = this.Parent as AttributeViewer;
                    int s = v.m_SplitXPosition;
                    int h = v.m_ImageWidth;
                    int x =  ( e.X < h)? h : (e.X > v.Width) ? v.Width : e.X ;
                    if (s != x)
                    {

                        v.m_SplitXPosition = x;
                        v.UpdateView(true);
                        v.OnSplitXPositionChanged(EventArgs.Empty);
                    }
                }
             
            }
            internal class AttributeViewerPanel : IGKXUserControl
            {
                private VScrollBar c_VScroll;
                private AttributeViewerLayoutEngine m_itemLayoutEngine;
                private AttributeViewer m_viewer;
                private List<AttributeItemHost> m_list;

                class AttributeViewerLayoutEngine : LayoutEngine 
                {
                    private AttributeViewerPanel m_owner;
                    public AttributeViewerLayoutEngine(AttributeViewerPanel owner)
                    {
                        this.m_owner = owner;
                        
                    }

                    public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
                    {
                        this.Layout();
                        return false;
                    }
                    public override void InitLayout(object child, BoundsSpecified specified)
                    {
                         base.InitLayout(child, specified);
                    }

                    public void Layout()
                    {
                        AttributeViewerPanel viewerPanel = m_owner;// container as AttributeViewerPanel;
                        //always not visible

                        int v_sw = SystemInformation.VerticalScrollBarWidth;
                        int offset = viewerPanel.c_VScroll.Visible ? SystemInformation.VerticalScrollBarWidth+ 1 : 0;
                        int offsety = viewerPanel.c_VScroll.Visible ? -viewerPanel.c_VScroll.Value : 0;
                        int offsetx = viewerPanel.HorizontalScroll.Visible ? -viewerPanel.HorizontalScroll.Value : 0;
                        int x = viewerPanel.HorizontalScroll.Visible ? -viewerPanel.HorizontalScroll.Value : 0;
                        int y = offsety;
                        int h = viewerPanel.ItemHeight;
                        int w = viewerPanel.Width - offset;
                        Padding noPadding = Padding.Empty;
                        viewerPanel.Padding = noPadding;
                        foreach (AttributeItemHost item in viewerPanel.m_list )
                        {
                            //item.Margin = noPadding;
                            item.Bounds = new Rectanglei (new Vector2i(x, y), new Size2i(w, Math.Max(item.Height, h)));                            
                            y += item.Height;
                        }

                        if (viewerPanel.c_VScroll.Visible)
                        {
                            viewerPanel.c_VScroll.Bounds = new Rectangle(
                                w,
                                0, v_sw, 
                                viewerPanel.Height);
                        }
                    }
                    
                }

                protected override void OnMouseWheel(MouseEventArgs e)
                {
                    base.OnMouseWheel(e);
                    if (c_VScroll.Visible)
                    {
                        int i = (this.c_VScroll.Value + ((this.c_VScroll.LargeChange ) * (-e.Delta/Math.Abs (e.Delta)) ));
                        if (i <= this.c_VScroll.Minimum)
                            i = this.c_VScroll.Minimum;
                        else if (i >= this.c_VScroll.Maximum)
                            i = this.c_VScroll.Maximum;
                        this.c_VScroll.Value = i;
                        m_itemLayoutEngine.Layout();
                        this.OnScroll(new ScrollEventArgs (ScrollEventType.LargeIncrement, i) );
                    }
                }

                public AttributeViewer ParentViewer {
                    get {
                        return m_viewer;
                    }
                }
              
                public override LayoutEngine LayoutEngine
                {
                    get
                    {
                        if (this.m_itemLayoutEngine != null)
                            this.m_itemLayoutEngine = new AttributeViewerLayoutEngine(this);
                        return this.m_itemLayoutEngine;
                    }
                }

                const string BOUNDS = "Bounds";
                private AttributeItemHost m_selectedItem;

                protected override void OnLayout(LayoutEventArgs levent)
                {
                    //CoreLog.WriteDebug(levent.ToString() + " : " + levent.AffectedProperty );
                    switch (levent.AffectedProperty)
                    {

                        default:
                        case BOUNDS  :
                        case "Visible":
                            //get affected properties
                            bool c = this.c_VScroll.Visible;
                        int x = 0;
                        int y = 0;
                        int h = this.ItemHeight;
                        int w = this.Width;
                        Rectanglei rc = Rectanglei.Empty;
                        foreach (AttributeItemHost item in this.m_list )
                        {
                            rc = new Rectanglei(new Vector2i(x, y), new Size2i(w, Math.Max(item.Height, h)));
                            item.Bounds = rc;
                              y += rc.Height;
                        }
                        bool v =  (y > this.Height);
                            if (v)
                            {
                                this.c_VScroll.Visible = v;
                                this.c_VScroll.Maximum = y - this.Height+24;
                                this.c_VScroll.Minimum = 0;
                            }
                            else 
                            {
                                this.c_VScroll.Visible = false;
                            }
                            if (v != c)
                            {
                                this.m_itemLayoutEngine.Layout();
                              //  this.Invalidate();
                            }
                            break;
                    }
                    base.OnLayout(levent);
                }
                public AttributeViewerPanel(AttributeViewer viewer)
                {
                    m_itemLayoutEngine = new AttributeViewerLayoutEngine(this);
                    c_VScroll = new VScrollBar();
                    c_VScroll.Dock = DockStyle.Right;
                    c_VScroll.Width = SystemInformation.VerticalScrollBarWidth;

                    this.SetStyle(ControlStyles.UserPaint, true);
                    this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                    this.SetStyle(ControlStyles.ResizeRedraw, true);
                    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                    this.SetStyle(ControlStyles.Selectable, true);
                    this.m_list = new List<AttributeItemHost>();
                    this.m_viewer = viewer;
                    this.Dock = DockStyle.Fill;
                    this.AutoScroll = false;

             
                    this.Controls.Add(c_VScroll);
                    this.ItemHeight = 24;
                    this.m_viewer.SplitXPositionChanged += m_viewer_SplitXPositionChanged;
                    this.c_VScroll.Scroll +=(o,e)=>{
                        m_itemLayoutEngine.Layout();
                        this.OnScroll (e);
                    };
                    this.Scroll += _Scroll;
                    this.MouseClick += _MouseClick;


                }

                private void _MouseClick(object sender, CoreMouseEventArgs e)
                {
                    if (e.Button == enuMouseButtons.Left)
                    {
                        int y = c_VScroll.Visible ? -c_VScroll.Value : 0 ;
                        var v = this.m_viewer;
                        var pt = new Vector2i(e.X, e.Y);
                        bool s = false;
                        foreach (var item in this.m_list)
                        {
                            if (item.Bounds.Contains(pt))
                            {
                                Rectanglei rc = item.GetControlBound();
                                if (rc.Contains(new Vector2i(e.X, e.Y)))
                                {
                                    var c = v.BeginEdit(item);
                                    item.setUp(c);
                                    this.Controls.Add(c);
                                    
                                }
                                else {
                                    v.CancelEdit();
                                }
                                s = true;
                                this.SelectedItem = item;
                                break;
                                
                            }
                            y+= item.Height;
                            if (y > this.Height)
                                break;

                        }
                        if (!s)
                        {
                            v.CancelEdit();
                            this.SelectedItem = null;
                        }

                    }
                }

                void _Scroll(object sender, ScrollEventArgs e)
                {
                    this.Invalidate();
                    this.Update();
                    this.m_viewer.OnScroll(e);
                }
           

                void m_viewer_SplitXPositionChanged(object sender, EventArgs e)
                {
                    this.Invalidate();
                }
                protected override void OnPaintBackground(PaintEventArgs e)
                {
                    //base.OnPaintBackground(e);
                }

                protected override void OnCorePaint(CorePaintEventArgs e)
                {
                    if (DesignMode)
                        return;
                    var t = ParentViewer;
                    if (t != null)
                    {
                        e.Graphics.Clear(AttributeValueRenderer.AttributePanColor2);
                        var v_rc1 = new Rectanglef(0, 0, t.SplitXPosition, this.Height);
                        e.Graphics.FillRectangle(AttributeValueRenderer.AttributePanColor1, v_rc1);

                        int y = c_VScroll.Visible ? -c_VScroll .Value : 0;

                        //draw only visible item
                        foreach (var item in this.m_list)
                        {
                            if ((y+ item.Height) > 0)
                            {
                                if (item == this.SelectedItem)
                                {
                                    item.PaintSelected(e.Graphics);
                                }
                                else
                                {
                                    item.Paint(e.Graphics);
                                }
                            }
                            y += item.Height;
                            if (y > this.Height)
                                break;
                        }
                        e.Graphics.FillRectangle(AttributeValueRenderer.AttributePanImageMarginColor, 0, 0, m_viewer.ImageWidth, this.Height);
                    }
                }

                public int ItemHeight { get { return this.m_itemHeight; } set {
                    if (this.m_itemHeight != value)
                    {
                        this.m_itemHeight = value;
                        OnItemHeightChanged(EventArgs.Empty);
                    }
                } }
                public event EventHandler ItemHeightChanged;
                ///<summary>
                ///raise the ItemHeightChanged 
                ///</summary>
                protected virtual void OnItemHeightChanged(EventArgs e)
                {
                    if (ItemHeightChanged != null)
                        ItemHeightChanged(this, e);
                }

                private void OnItemHightChanged(EventArgs eventArgs)
                {
                    
                }

                protected override Point ScrollToControl(Control activeControl)
                {
                    return base.ScrollToControl(activeControl);
                }

                

                protected override ControlCollection CreateControlsInstance()
                {
                    return new PanelControlInstance(this);
                }
                class PanelControlInstance : ControlCollection
                {
                    AttributeViewerPanel m_panel;

                    public PanelControlInstance(AttributeViewerPanel attributeViewerPanel):base(attributeViewerPanel)
                    {
                        this.m_panel = attributeViewerPanel;
                    }
                    public override void Add(Control value)
                    {
                        //if (value == this.m_panel.c_VScroll )
                            base.Add(value);
                    }
                    public override void Remove(Control value)
                    {
                        base.Remove(value);
                    }

                }

                internal void Add(AttributeItemHost item)
                {
                    this.m_list.Add(item);
              
                }

                internal void Clear()
                {
                    this.m_list.Clear();
                }

                internal void Remove(AttributeItemHost item)
                {
                    this.m_list.Remove(item);
                }
                internal int IndexOf(AttributeItemHost item)
                {
                    return m_list.IndexOf(item);
                }
                internal void Sort()
                {
                    this.m_list.Sort(new Comparison<AttributeItemHost>((a, b) =>
                    {
                        return a.Item.Name.CompareTo(b.Item.Name);
                    }));
                }
                /// <summary>
                /// get or set the selected item host
                /// </summary>
                public AttributeItemHost SelectedItem { get {
                    return this.m_selectedItem;
                }
                    set {
                        if (this.m_selectedItem != value)
                        {
                            this.m_selectedItem = value;
                            Invalidate();
                            OnSelectedItemChanged(EventArgs.Empty);
                        }
                    }
                }

                public event EventHandler SelectedItemChanged;
                private int m_itemHeight;
                ///<summary>
                ///raise the SelectedItemChanged 
                ///</summary>
                protected virtual void OnSelectedItemChanged(EventArgs e)
                {
                    if (SelectedItemChanged != null)
                        SelectedItemChanged(this, e);
                }


              
            }
         
            private AttribSplitter c_attribSplitter;
            private int m_SplitXPosition;
            private AttributeViewerPanel c_attributeViewerPanel;

            /// <summary>
            /// get or set the item height
            /// </summary>
            public int ItemHeight {
                get {
                    return this.c_attributeViewerPanel.ItemHeight;
                }
                set {
                    this.c_attributeViewerPanel.ItemHeight = value;
                }
            }
            public event EventHandler  ItemHeightChanged{
                add {
                    this.c_attributeViewerPanel.ItemHeightChanged += value;
                }
                remove {
                    this.c_attributeViewerPanel.ItemHeightChanged -= value;
                }
            }
            /// <summary>
            /// get the xsplit position
            /// </summary>
            public int SplitXPosition
            {
                get { return m_SplitXPosition; }
              
            }
            public event EventHandler SplitXPositionChanged;
            private IGKXAttributeEditor c_editor;

            protected virtual void OnSplitXPositionChanged(EventArgs e)
            {
                if (SplitXPositionChanged != null)
                {
                    SplitXPositionChanged(this, e);
                }
            }

            protected override System.Drawing.Size DefaultSize
            {
                get
                {
                    return new System.Drawing.Size(300, 300);
                }
            }
            public AttributeViewer()
            {

                c_attribSplitter = new AttribSplitter();
                c_attributeViewerPanel = new AttributeViewerPanel(this);
                c_attribSplitter.Size = new System.Drawing.Size(2, this.Height);
                this.Controls.Add(c_attribSplitter);
                this.Controls.Add(c_attributeViewerPanel);

                this.SizeChanged += _SizeChanged;
                this.m_SplitXPosition = this.Width / 2;
                this.AutoScroll = false;
                
            }
           
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
            }

            public AttributeViewer(IGKXAttributeEditor iGKXAttributeEditor):this()
            {
                this.c_editor = iGKXAttributeEditor;
            }
            void _SizeChanged(object sender, EventArgs e)
            {
                this.UpdateView(false);
            }
            protected override void OnCorePaint(CorePaintEventArgs e)
            {
                //e.Graphics.Clear(AttributeValueRenderer.AttributePanColor2);
                //var v_rc1 = new Rectanglef(0, 0, this.SplitXPosition, this.Height);
                ////var v_rc2 = new Rectanglef(this.SplitXPosition, 0, this.Width - this.SplitXPosition, this.Height);
                //e.Graphics.FillRectangle(AttributeValueRenderer.AttributePanColor1, v_rc1);
                //e.Graphics.FillRectangle(AttributeValueRenderer.AttributePanColor2, v_rc2);
            }
      
            internal void UpdateView(bool invalidate)
            {
                //update splitter size
                this.SuspendLayout();
                int w = this.VerticalScroll.Maximum;
                int h = Math.Max(this.VerticalScroll.Maximum, this.Height);
                c_attribSplitter.Size = new System.Drawing.Size(2, h);
                c_attribSplitter.Location = new System.Drawing.Point(
                    this.SplitXPosition - (c_attribSplitter.Width / 2)
                    , 
                      0
                    );
                if ((c_itemControl != null) &&  (c_itemControl.Parent!= null))
                {
                    //(c_itemControl.Parent as AttributeItemHost).setUp(c_itemControl);
                }
                this.ResumeLayout();
                //if (invalidate)
                //{
                //    this.Update();
                //    this.Invalidate();
                //}
            }

            internal void AddItem(AttributeItemHost item)
            {
                if (item != null)
                {
                    this.c_attributeViewerPanel.Add(item);
                }
            }
           internal void ClearItems()
            {
                this.c_attributeViewerPanel.Clear();
            }
            internal void Remove(AttributeItemHost item)
            {
                this.c_attributeViewerPanel.Remove(item);
            }
            internal int IndexOf(AttributeItemHost item)
            {
                return this.c_attributeViewerPanel.IndexOf(item);
            }
      
            internal Control  BeginEdit(AttributeItemHost attributeItem)
            {
                if( (c_itemControl == null)||(c_itemControl .IsDisposed ))
                    c_itemControl = new AttributeEditAttributeControl(this);
                c_itemControl.Item = attributeItem;
                c_itemControl.FocusTextBox();
                this.c_editor.c_lb_description.Text  = attributeItem.Item.Description;
                return this.c_itemControl;
            }
            internal void CancelEdit() {
                if ((c_itemControl != null) && (c_itemControl.Parent!=null))
                {
                    c_itemControl.Item = null;
                    c_itemControl.Parent.Controls.Remove(c_itemControl);
                    c_itemControl = null;
                    this.c_editor.c_lb_description.Text = string.Empty;
                }
            }



            internal void Sort()
            {
                this.c_attributeViewerPanel.Sort();
            }

            public int ImageWidth
            {
                get { return this.m_ImageWidth; }
                set
                {
                    this.m_ImageWidth = value;
                }
            }

          
        }

      
        /// <summary>
        /// used to edit a single attribute
        /// </summary>
        private class AttributeEditAttributeControl : Control
        {
            private IGKXButton c_btn_attribButton;
            private IGKAttributeEditorTextBox c_txt_editValue;
            private enuDropDowtype m_dropdownType;
            private bool m_ShowButton;
            private bool m_configuring;
            private AttributeViewer c_viewer;
            private AttributeItemHost m_Item;
            private TopLevelPanel c_topLevelPanel = new TopLevelPanel();


            public bool ShowButton
            {
                get { return m_ShowButton; }
                set
                {
                    if (m_ShowButton != value)
                    {
                        m_ShowButton = value;
                        OnShowButtonChanged(EventArgs.Empty);
                    }
                }
            }
           
            public event EventHandler ShowButtonChanged;
  

            /// <summary>
            /// get or set the text of this value
            /// </summary>
            public override string Text
            {
	              get 
	            { 
		             return base.Text;
	            }
	              set 
	            { 
		            base.Text = value;
	            }
            }

            protected virtual void OnShowButtonChanged(EventArgs e)
            {
                if (ShowButtonChanged != null)
                {
                    ShowButtonChanged(this, e);
                }
            }

            public void FocusTextBox() {
                this.c_txt_editValue.Focus();
            }
            public AttributeEditAttributeControl(AttributeViewer viewer)
            {
                this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);

                this.BackColor = Color.White;
                this.c_viewer = viewer;
                this.c_btn_attribButton = new IGKXButton();
                this.c_txt_editValue = new IGKAttributeEditorTextBox(this.c_viewer);
                this.c_txt_editValue.Multiline = false;

                this.c_btn_attribButton.Dock = DockStyle.None;
                this.c_btn_attribButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments("android_btn_browse"));

                this.c_txt_editValue.Dock = DockStyle.None;
                this.c_txt_editValue.TextChanged += c_text_editValue_TextChanged;
                this.c_txt_editValue.KeyPress += c_text_editValue_KeyPress;

                this.c_btn_attribButton.Click += c_btn_attribButton_Click;
                this.c_btn_attribButton.Width = (int)"10mm".ToPixel();
                this.Controls.Add(this.c_btn_attribButton);
                this.Controls.Add(this.c_txt_editValue);
   
                this.ItemChanged += AttributeEditAttributeControl_ItemChanged;
                this.c_viewer.SplitXPositionChanged += c_viewer_SplitXPositionChanged;
                this.c_viewer.SizeChanged += c_viewer_SizeChanged;
                this.c_viewer.Scroll += c_viewer_Scroll;
                this.SizeChanged += _SizeChanged;
            }

            void _SizeChanged(object sender, EventArgs e)
            {
                //setup bound
                int w = Math.Min(this.Width, this.Height);

                this.c_txt_editValue.Bounds = new Rectangle(0, 0, this.Width - w, this.Height);

                this.c_btn_attribButton.Bounds = new Rectangle(this.Width -w-4
                    ,
                    0,  w-1,w-1);
            }
            protected override void Dispose(bool disposing)
            {
                //unregister event
                this.c_viewer.SplitXPositionChanged -= c_viewer_SplitXPositionChanged;
                this.c_viewer.SizeChanged -= c_viewer_SizeChanged;
                this.c_viewer.Scroll -= c_viewer_Scroll;

                if (this.c_topLevelPanel != null)
                    this.c_topLevelPanel.Dispose();

                base.Dispose(disposing);
            }

            void c_viewer_Scroll(object sender, ScrollEventArgs e)
            {
                this.SetupItem();
            }

            private void SetupItem()
            {

                if (this.Item != null)
                {
                    this.Item.setUp(this);
                }
            }

           

            void c_text_editValue_KeyPress(object sender, KeyPressEventArgs e)
            {
                if ((Keys)e.KeyChar == Keys.Enter)
                {
                    this.c_viewer.CancelEdit();
                    e.Handled = true;
                }
            }

            void c_viewer_SizeChanged(object sender, EventArgs e)
            {
                this.SetupItem();
            }
            void c_viewer_SplitXPositionChanged(object sender, EventArgs e)
            {
                this.SetupItem();
            }
            void AttributeEditAttributeControl_ItemChanged(object sender, EventArgs e)
            {
                if (this.Item != null)
                {
                    this.Enabled = true;
                    m_configuring = true ;
                    this.c_txt_editValue.Text = this.Item.Item.Value;
                    this.m_dropdownType = enuDropDowtype.DropDown;
                    m_configuring = false;
                }
                else {
                    this.Enabled = false;
                }
            }
            protected override void OnParentChanged(EventArgs e)
            {
                base.OnParentChanged(e);
             
            }
            class TopLevelPanel : Form
            {
                public TopLevelPanel()
                {
                    this.SetTopLevel(true);
                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    this.ShowInTaskbar = false;
                    this.Icon = null;
                    this.StartPosition = FormStartPosition.Manual;

                }
            }
            void c_btn_attribButton_Click(object sender, EventArgs e)
            {
                if (this.Item == null)
                    return;
                var c = this.c_topLevelPanel;
                c.Controls.Clear();
               
                ListBox lsb = new ListBox();
                lsb.Dock = DockStyle.Fill;
                lsb.IntegralHeight = false;
                lsb.Margin = new System.Windows.Forms.Padding(4);
                lsb.SelectedIndexChanged += (o,ee) => {
                    this.c_txt_editValue.Text = lsb.SelectedItem.ToString();
                    this.c_topLevelPanel.Hide();
                };
                c.Controls.Add(lsb);

                var r = this.Item.Item.GetSupportedValues();
                if (r!=null){
                lsb.Items.AddRange (r);
                switch (this.DropDownType)
                {
                    case enuDropDowtype.Modal:
                        break;
                    case enuDropDowtype.DropDown :
                        break;
                }
           
                }
                c.Owner = this.FindForm();
                Rectangle rc = this.RectangleToScreen(this.ClientRectangle);
                rc.Y += this.Height;
                rc.Height = 100;

                rc.Width = Math.Max(this.Width, 300);
                c.Bounds = rc;
                c.AutoScroll = true;
                c.Show ();
                c.Capture = true;
                lsb.Focus();
            }

            void c_text_editValue_TextChanged(object sender, EventArgs e)
            {
                if (m_configuring) return;
                if (Item != null)
                    Item.Item.Value = c_txt_editValue.Text;
            }

            public AttributeItemHost Item
            {
                get { return m_Item; }
                set
                {
                    if (m_Item != value)
                    {
                        m_Item = value;
                        OnItemChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler ItemChanged;
  

            protected virtual void OnItemChanged(EventArgs e)
            {

                if (this.c_topLevelPanel != null)
                {
                    this.c_topLevelPanel.Hide();
                }
                if (ItemChanged != null)
                {
                    ItemChanged(this, e);
                }
            }



            public enuDropDowtype DropDownType { get { return this.m_dropdownType; } }
        }


        public class AttributeItemHost  
        {
            private AttributeItem item;
            private Rectanglei m_Bound;
            private IGKXAttributeEditor.AttributeViewer m_viewer;
            private AttributeItemHost m_previous;
            private AttributeItemHost m_next;

            public  AttributeItemHost Previous { get { return this.m_previous; } internal set { this.m_previous = value; } }
            public AttributeItemHost Next { get { return this.m_next; } internal set { this.m_next = value; } }
            public int Height { get { return this.Bounds.Height; } }
            public int Width { get { return this.Bounds.Width; } }
            public AttributeItem Item { get { return this.item; } }
            public Rectanglei Bounds
            {
                get { return m_Bound; }
                set
                {
                    if (m_Bound != value)
                    {
                        m_Bound = value;
                        OnBoundChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler BoundChanged;


            protected virtual void OnBoundChanged(EventArgs e)
            {
                if (BoundChanged != null)
                {
                    BoundChanged(this, e);
                }
            }



            protected Size DefaultSize
            {
                get
                {
                    return new Size(100, 16);
                }
            }
            public AttributeViewer AttributeViewer {
                get {
                    return m_viewer;
                }
            }
            internal AttributeItemHost(AttributeViewer viewer, AttributeItem item)
            {
                this.item = item;
                this.m_viewer = viewer;
            }
            private void Render(ICoreGraphics g, Colorf panColor,
                Colorf fpanColor, 
                Colorf foreColor,
                Colorf valueColor)
            {

                CoreFont ft = CoreFontRegister.GetFontById(IGKXAttributeEditorThemes.AttributeEditorItemFont);
                ft.VerticalAlignment = enuStringAlignment.Center;
                AttributeViewer viewer = AttributeViewer;
                var v_rc1 = GetLabelBound();
                var v_rc2 = GetControlBound();
                g.FillRectangle(panColor , v_rc1);
                g.FillRectangle(fpanColor , v_rc2);

                int v_index = this.m_viewer.IndexOf(this);
                bool f = (v_index == 0) || ((v_index % 2) == 0);
                g.FillRectangle(
                    Colorf.FromFloat(0.2f, f ? 1.0f : 0.0f),
                    new Rectanglef(0, v_rc1.Y, v_rc1.Width + v_rc2.Width, v_rc1.Height));


                g.SetClip(v_rc1);
                g.DrawString(
                    item.DisplayText,
                    ft,
                    foreColor,
                    new Rectanglef(m_viewer.ImageWidth,
                    v_rc1.Y, v_rc1.Width, v_rc1.Height));
                g.SetClip(v_rc2);
                var s = g.Save();

                g.TranslateTransform(v_rc2.X, v_rc2.Y, enuMatrixOrder.Append);
                item.RenderValue(g, new Rectanglef(0, 0, v_rc2.Width, v_rc2.Height), valueColor);
                g.Restore(s);
                ft.Dispose();
                g.ResetClip();
            }

            internal void Paint(ICoreGraphics g)
            {
                Render(g, 
                    AttributeValueRenderer.AttributePanColor1,
                    AttributeValueRenderer.AttributePanColor2,
                    AttributeValueRenderer.AttributePanForeColor,
                    AttributeValueRenderer.AttributePanValueForeColor
                    );
            }
            internal void PaintSelected(ICoreGraphics g)
            {
                Render(g, 
                    AttributeValueRenderer.AttributePanSelectedColor1,
                    AttributeValueRenderer.AttributePanSelectedColor2,
                    AttributeValueRenderer.AttributePanSelectedForeColor,
                    AttributeValueRenderer.AttributePanSelectedValueForeColor
                    );
            }

            internal  Rectanglei GetControlBound()
            {
                AttributeViewer viewer = this.m_viewer;
                return new Rectanglei(viewer.SplitXPosition, this.Bounds.Y, this.Width - viewer.SplitXPosition, this.Height);
            }

            internal Rectanglei GetLabelBound()
            {
                AttributeViewer viewer = this.m_viewer;
                return new Rectanglei(0, this.Bounds.Y , viewer.SplitXPosition, this.Height); 
            }
         
            internal void setUp(Control c_itemControl)
            {
                var rc = this.GetControlBound();
                rc.Width -= 2;
                rc.X += 1;
                c_itemControl.Bounds = rc.CoreConvertTo<Rectangle>();
            }

           
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override string CaptionKey
        {
            get
            {
                return base.CaptionKey;
            }
            set
            {
                base.CaptionKey = value;
            }
        }
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        [Browsable(false )]
        [EditorBrowsable( EditorBrowsableState.Never)]
        /// <summary>
        /// description
        /// </summary>
        public string Description
        {
            get { return this.c_lb_description .Text; }
            set
            {
                this.c_lb_description.Text = value;
            }
        }
        /// <summary>
        /// title
        /// </summary>
        public string Title { get {
            return this.c_lb_title.Text;
        }
            set {
                this.c_lb_title.Text = value;
            }
        }

        public void Sort()
        {
            this.c_viewer.Sort();
        }
        /// <summary>
        /// add root node 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public TreeNode AddRootNode(string rootName)
        {
            if (this.m_RootNode != null)
                return null;
            var t = this.c_trv_nodes.Nodes.Add(rootName);
            CoreXmlElement.CreateXmlNode(rootName);
            this.m_RootNode = CoreXmlElement.CreateXmlNode(rootName);
            t.Tag = this.m_RootNode;
            return t;
        }
        /// <summary>
        /// clear the node item
        /// </summary>
        public void ClearNode() {
            this.c_trv_nodes.Nodes.Clear();
            this.m_RootNode = null;
            this.CurrentNode = null;
            
        }
        public void Save(string filename)
        {
            if (this.m_storeListener != null)
            {
                this.m_storeListener.StoreAttribute(this, filename);
            }
            else
            {
                CoreXmlElement res = this.c_trv_nodes.Nodes[0].Tag as CoreXmlElement;
                File.WriteAllText(filename, res.RenderXML(null));
            }
        }

        public void AddStyle(string styleName, string parent)
        {
            var t = AddNode("style");
            t.SetProperty ("name", styleName);
            t.SetProperty("parent", parent);
        }

        private CoreXmlElement AddNode(string p)
        {

            TreeNode node = null;
            if (this.m_RootNode == null)
                node = AddRootNode("resources");
            else
                node = this.c_trv_nodes.Nodes[0];

            var t = RootNode.Add(p);
            TreeNode b = node.Nodes.Add(p);
            b.Tag = t;
            return t;
        }
        /// <summary>
        /// bind attribute loader
        /// </summary>
        /// <param name="editor"></param>
        public  void SetAttributeLoaderListener(IAttributeEditorLoader editor)
        {
            this.m_attributeLoader = editor;
        }
        /// <summary>
        /// set store attribute listener 
        /// </summary>
        /// <param name="mStoreListener"></param>
        public void SetStoreAttributeListener(IAttributeEditorStoreListener mStoreListener)
        {
            this.m_storeListener = mStoreListener ;
        }
        private void LoadAttributes() {
            if (m_attributeLoader != null)
            {
               m_attributeLoader.LoadAttribute(this, this.CurrentNode);
               this.c_viewer.UpdateView();
            }
        }
        /// <summary>
        /// add class tag
        /// </summary>
        /// <param name="name"></param>
        public void Add(string name)
        {
            TreeNode snode = this.c_trv_nodes.SelectedNode;
            if (snode == null)
            {
                if (this.RootNode == null)
                {
                    this.AddRootNode(name);
                }
                else
                {
                    var c = this.RootNode;
                    c = c.Add(name);
                    this.AddNode(this.c_trv_nodes.Nodes[0], c);
                }
            }
            else
            {
                var c = this.CurrentNode ?? this.RootNode;
                c = c.Add(name);
                this.AddNode(snode, c);
            }

            
        }

        private void AddNode(TreeNode node, CoreXmlElement c)
        {
            TreeNode v_gc = node.Nodes.Add(c.TagName);
            v_gc.Tag = c;
            this.c_trv_nodes.SelectedNode = v_gc;
        }

        private bool m_ShowTreeView;

        [DefaultValue(true)]
        public bool ShowTreeView
        {
            get { return m_ShowTreeView; }
            set
            {
                if (m_ShowTreeView != value)
                {
                    m_ShowTreeView = value;
                    OnShowTreeViewChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler ShowTreeViewChanged;

        protected virtual void OnShowTreeViewChanged(EventArgs e)
        {
            this.c_trv_nodes.Visible = this.ShowTreeView;
            this.c_splitter2.Visible = this.ShowTreeView;
            this.c_pan_left.Visible = this.ShowTreeView;
            if (ShowTreeViewChanged != null)
            {
                ShowTreeViewChanged(this, e);
            }
        }

        /// <summary>
        /// editor textbox
        /// </summary>
        sealed class IGKAttributeEditorTextBox : TextBox
        {
            private AttributeViewer attributeViewer;
            private CoreFont m_ft;
            protected override void Dispose(bool disposing)
            {
                if (m_ft != null)
                {
                    m_ft.Dispose();
                }
                base.Dispose(disposing);
            }
            public IGKAttributeEditorTextBox()
            {             
            }
            public IGKAttributeEditorTextBox(AttributeViewer attributeViewer)
                : this()
            {
                this.attributeViewer = attributeViewer;
                this.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.attributeViewer.ItemHeightChanged += _itemChanged;
                this.m_ft = string.Format("fontName:consolas; Size: {0}", this.attributeViewer.ItemHeight * 0.75f);
                this.Font = this.m_ft.ToGdiFont();
            }
            protected override void OnSizeChanged(EventArgs e)
            {
                base.OnSizeChanged(e);
                this.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.m_ft = string.Format("fontName:consolas; Size: {0}px", this.attributeViewer.ItemHeight);
                float ftsize = (float)(Math.Ceiling(this.attributeViewer.ItemHeight * this.m_ft.GetEmHeight() / this.m_ft.GetLineSpacing()) - 6);
                this.Font = new Font(this.Font.FontFamily, ftsize, FontStyle.Regular, GraphicsUnit.Pixel);

            }

            private void _itemChanged(object sender, EventArgs e)
            {
                this.m_ft = string.Format("fontName:consolas; Size: {0}", this.attributeViewer.ItemHeight);
                this.Font = this.m_ft.ToGdiFont();

            }
        }

    }


}
