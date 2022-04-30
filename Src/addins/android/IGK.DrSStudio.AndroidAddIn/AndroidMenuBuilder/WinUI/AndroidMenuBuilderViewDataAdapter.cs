
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI
{
    using IGK.DrSStudio.Android.Resources;
    using IGK.DrSStudio.Android.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Resources;
    using IGK.ICore.WinCore.Dispatch;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Dispatch;
    using IGK.ICore.Xml;

    public sealed class AndroidMenuBuilderViewDataAdapter : IAndroidResourceViewAdapter
    {
        private IAndroidResourceViewAdapterListener m_listener;
        private List<AndroidResourceItemBase> m_resources;
        private int m_nextNumber;
        private IAndroidResourceItemSelectedListener m_itemSelectedListener;
        /// <summary>
        /// get the new element creation number
        /// </summary>
        /// <returns></returns>
        internal int getNextItemNumber()
        {
            m_nextNumber++;
            return Math.Max(1, m_nextNumber);
        }
        private string GetFullName(CoreXmlElement item)
        {
            if (item == null)
                return string.Empty;
            var s = m_resources[0];
            if (item == s)
            {
                return s.TagName;
            }
            var e = item.Parent as CoreXmlElement;
            if (e != null)
            {
                int i = e.Childs.IndexOf(item);
                return GetFullName(item.Parent as CoreXmlElement) + "." + item.TagName + "_" + i;
            }
            return string.Empty;

        }

        public AndroidMenuBuilderViewDataAdapter()
        {
            this.m_resources = new List<AndroidResourceItemBase>();
            this.m_resources.Add(new AndroidMenuResource());
        }
        public int Count
        {
            get { return this.m_resources.Count; }
        }

        public object GetObject(int position)
        {
            return this.m_resources[position];
        }

        public void SetNotifyChangedListener(IAndroidResourceViewAdapterListener listerner)
        {
            this.m_listener = listerner;
        }
        public void SortResources() {
            this.m_resources.Sort((a, b) =>
            {
                string aa = this.GetFullName(a);
                string bb = this.GetFullName(b);
                return aa.CompareTo(bb);
            });
        }
        public ICore2DDrawingLayeredElement GetView(ICoreWorkingApplicationContextSurface context, ICore.Drawing2D.ICore2DDrawingLayeredElement createdview, int position)
        {
            AndroidResourceItemBase v_item = this.m_resources[position];
            GroupElement v_group = (v_item.Host ?? createdview) as GroupElement;

            if (v_group == null)
            {
                //load and create host
                using (var v_doc = CoreResources.GetDocument(this, AndroidConstant.ANDROID_BULLET_MENU_ITEM).CloneThis())// as ICore2DDrawingDocument;
                {
                    if (v_doc == null) {
                        return null;
                    }
                    v_group = GroupElement.CreateElement(v_doc);
                    v_doc.Clear();
                }
                //initialize
                initGroup(context, v_item, v_group, position);
                v_item.Host = v_group;

                v_group.SetAttribute("Tag", position);
                v_group.Disposed += v_group_Disposed;

                //setup info      
                v_group.ShowBorder = false;
             
            }
            else {
                //setup item
                var txt = v_group.GetElementById<TextRoundBulletElement>("text");
                txt.Text = v_item.GetAttributeValue<string>("android:title") ?? v_item.GetProperty("Name");
            }
            v_group.ClearTransform();

            int v_depth = 0;
            var q = v_item.Parent;
            while (q != null)
            {
                q = q.Parent;
                v_depth++;
            }
            v_group.Translate(20 * v_depth, 50 * position, enuMatrixOrder.Append);
            return v_group;
        }

        void v_group_Disposed(object sender, EventArgs e)
        {
            GroupElement g = sender as GroupElement;
            var i = g.GetAttribute<int>("Tag");

        }

        private void initGroup(ICoreWorkingApplicationContextSurface context, AndroidResourceItemBase v_item, GroupElement g, int position)
        {
            var txt = g.GetElementById<TextRoundBulletElement>("text");
            var attach = g.GetElementById<CircleElement>("add_menu_item");
            var submenu = g.GetElementById<CircleElement>("add_group_item");
            var close = g.GetElementById<Core2DDrawingDualBrushElement>("close") ;

            var v_group = new GroupElement();
            v_group.SuspendLayout();
            v_group.Add(attach);
            v_group.Add(submenu);
            v_group.Add(close);
            v_group.ResumeLayout();

            g.Add(v_group);

            new AndroidMenuGroupLayoutManager(txt, v_group);

            bool selected = false;

            if (close != null)
            {
                if (position == 0)
                {
                    close.View = false;
                }
                else
                {
                    context.Dispatcher.Register(close, WinCoreControlDispatcher.MouseClickDispatching,
                      new EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>(
                      new DropItemClickEvent(context, this, v_item, close).Click)
                      );

                    context.Dispatcher.Register(close, WinCoreControlDispatcher.MouseMoveDispatching,
             (EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>)delegate(object sender, CoreDispatcherEventArgs<CoreMouseEventArgs> e)
             {
                 if (close.Contains(e.Event.FactorPoint))
                 {                   
                         close.FillBrush.SetSolidColor(Colorf.Blue);        
                         (context as ICore2DDrawingSurface).RefreshScene();                  
                 }
                 else
                 {
                         close.FillBrush.SetSolidColor(Colorf.Red);                         
                         (context as ICore2DDrawingSurface).RefreshScene();
                  
                 }
             });
                }
            }

            txt.Text = v_item.GetAttributeValue<string>("android:title") ?? v_item.GetProperty("Name");
            attach.FillBrush.SetSolidColor(Colorf.Red);

            var v_s = new AddItemClickEvent(this, attach, v_item);

            bool disp = context.Dispatcher.Register(attach, WinCoreControlDispatcher.MouseClickDispatching,
                  new EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>(v_s.Click));

            disp = context.Dispatcher.Register(submenu, WinCoreControlDispatcher.MouseClickDispatching,
                   new EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>(v_s.Click));
        
            context.Dispatcher.Register(attach, WinCoreControlDispatcher.MouseMoveDispatching,
             (EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>)delegate(object sender,
             CoreDispatcherEventArgs<CoreMouseEventArgs> e)
             {
                 if (attach.Contains(e.Event.FactorPoint))
                 {
                     if (!selected)
                     {
                         attach.FillBrush.SetSolidColor(Colorf.DarkGreen );
                         selected = true;
                         (context as ICore2DDrawingSurface).RefreshScene();
                     }
                 }
                 else
                 {
                     if (selected)
                     {
                         attach.FillBrush.SetSolidColor(Colorf.Red);
                         selected = false;
                         (context as ICore2DDrawingSurface).RefreshScene();
                     }
                 }
             });

            context.Dispatcher.Register(attach, WinCoreControlDispatcher.MouseMoveDispatching,
                (EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>)delegate(object sender,
                CoreDispatcherEventArgs<CoreMouseEventArgs> e)
                {
                    if (attach.Contains(e.Event.FactorPoint))
                    {
                        if (!selected)
                        {
                            attach.FillBrush.SetSolidColor(Colorf.Magenta);
                            selected = true;
                            (context as ICore2DDrawingSurface).RefreshScene();
                        }
                    }
                    else
                    {
                        if (selected)
                        {
                            attach.FillBrush.SetSolidColor(Colorf.Red);
                            selected = false;
                            (context as ICore2DDrawingSurface).RefreshScene();
                        }
                    }
                });
            context.Dispatcher.Register(txt, WinCoreControlDispatcher.MouseClickDispatching,
             (EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>)delegate(object o,
             CoreDispatcherEventArgs<CoreMouseEventArgs> ee)
             {
                 OnItemSelected(v_item);
                 ee.StopPropagation = true;
             });
        }

        private void OnItemSelected(CoreXmlElement item)
        { 
            if (this.m_itemSelectedListener != null)
                this.m_itemSelectedListener.OnItemSelected(item);       
        }
        
        public void SetSelectedChangedListener(IAndroidResourceItemSelectedListener listener)
        {
            m_itemSelectedListener = listener;
        }
        public void Clear()
        {
            this.m_resources.Clear();
            this.m_resources.Add(new AndroidMenuResource());            
            OnNotifyDataChanged(enuChangedType.DataLoaded, 0);
        }
        /// <summary>
        /// load data
        /// </summary>
        /// <param name="element"></param>
        public void LoadData(CoreXmlElement element)
        {
            this.m_resources.Clear();

            if (element.TagName == "menu")
            {
                var c = new AndroidMenuResource();
                var s = element.RenderInnerHTML(null);
                c.LoadString(s);
                this.m_resources.Add(c);
                foreach (AndroidResourceItemBase  k in c.getElementsByTagName("item"))
                {
                    this.m_resources.Add(k);
                }
            }
            else
            {
                this.m_resources.Add(new AndroidMenuResource());
            }
            //
            OnNotifyDataChanged(enuChangedType.DataLoaded, 0);
        }
        public void Remove(AndroidResourceItemBase element)
        {
            var index = this.m_resources.IndexOf(element);
            if (__removeChild(element))            
            {
                this.SortResources();
                OnNotifyDataChanged(enuChangedType.DataRemoved, index);
            }
        }

        private bool __removeChild(AndroidResourceItemBase item)
        {
            if (this.m_resources.Contains(item))
            {
                foreach (AndroidResourceItemBase vitem in item.Childs)
                {
                    __removeChild(vitem);
                }
                this.m_resources.Remove(item);
                //var e =  ((GroupElement)this.m_resources[1].Host).GetElementById("text");
                //var e2 = ((GroupElement)this.m_resources[0].Host).GetElementById("text");
              
                item.Host.Dispose();
                item.Host = null;
                return true;
            }
            return false;
        }
      
        internal void OnNotifyDataChanged(enuChangedType enuChangedType, int position)
        {
            if (this.m_listener  != null)
                this.m_listener.OnDataChanged(enuChangedType, position );
        }
        public int IndexOf(object n)
        {
            return this.m_resources.IndexOf(n as AndroidResourceItemBase);
        }

        sealed class AddItemClickEvent
        {
            private AndroidMenuBuilderViewDataAdapter m_adapter;
            private CircleElement m_attach;
            private AndroidResourceItemBase v_item;
            public AddItemClickEvent(AndroidMenuBuilderViewDataAdapter androidMenuBuilderViewDataAdapter, CircleElement attach, AndroidResourceItemBase v_item1)
            {
                this.m_adapter = androidMenuBuilderViewDataAdapter;
                this.m_attach = attach;
                this.v_item = v_item1;
            }
            public void Click(object sender, CoreDispatcherEventArgs<CoreMouseEventArgs> e)
            {
                var c = this.m_adapter.getNextItemNumber();
                AndroidItemResource ri = null;          
                if (sender == m_attach)
                {
                    //add menu item
                    if (v_item != this.m_adapter.m_resources[0])
                    {//add a submenu
                        AndroidMenuResource pi = v_item.GetPropertyValue<AndroidMenuResource>("submenu") ??
                            new AndroidMenuResource();
                        ri = new AndroidItemResource();
                        ri.ParentSubMenu = pi;
                        pi.AddChild(ri);
                        v_item.AddChild(pi);
                        v_item.SetProperty("submenu", pi);
                    }
                    else
                    {
                        ri = new AndroidItemResource();
                        v_item.AddChild(ri);
                    }
                    ri.SetProperty("android:AttrName", "MenuItem");
                    ri.SetProperty("Name", "MenuItem_" + c);
                    ri["android:title"] = "MenuItem_" + c;
                    ri["android:id"] = "@+id/menuitem_" + c;
                    this.m_adapter.m_resources.Add(ri);
                }
                else
                {
                    ////add group item
                    //AndroidGroupMenu group = new AndroidGroupMenu();
                    //v_item.AddChild(group);
                    //this.m_adapter.m_resources.Add(group);

                }
                this.m_adapter.SortResources();
                e.StopPropagation = true;
                this.m_adapter.OnNotifyDataChanged(enuChangedType.DataAdded, this.m_adapter.m_resources.IndexOf(ri));
            }

          

        }
        sealed class DropItemClickEvent
        {
            private AndroidResourceItemBase m_item;
            private AndroidMenuBuilderViewDataAdapter m_owner;
            private ICoreWorkingApplicationContextSurface m_context;
            private ICoreWorkingObject m_workingObject;

            public DropItemClickEvent(
                ICoreWorkingApplicationContextSurface context, 
                AndroidMenuBuilderViewDataAdapter owner, 
                AndroidResourceItemBase item, 
                ICoreWorkingObject obj)
            {
                this.m_owner = owner;
                this.m_item = item;
                this.m_context = context;
                this.m_workingObject = obj;
            }
            internal void Click(object sender, CoreDispatcherEventArgs<CoreMouseEventArgs> e)
            {
                ICore2DDrawingLayeredElement l = sender as ICore2DDrawingLayeredElement;
                if (l.View )
                {
                    this.m_owner.Remove(this.m_item);
                    //this.m_context.Dispatcher.UnRegister(this.m_workingObject, WinCoreControlDispatcher.MouseClickDispatching,
                    //    (EventHandler<CoreDispatcherEventArgs<CoreMouseEventArgs>>)this.Click);
                    e.StopPropagation = true;
                }
                else
                {
                }
           
            }
        }

        sealed class AndroidMenuGroupLayoutManager
        {
            private TextRoundBulletElement m_txt;
            private GroupElement m_group;
            private Rectanglef m_groupBound;
            private Rectanglef m_textBound;

            public AndroidMenuGroupLayoutManager(TextRoundBulletElement txt, GroupElement group)
            {
                this.m_txt = txt;
                this.m_group = group;
                this.m_txt.PropertyChanged += m_txt_PropertyChanged;
                this.m_groupBound = this.m_group.GetBound();
                this.m_textBound = txt.GetBound();
            }

            void m_txt_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                switch ((enu2DPropertyChangedType)e.ID)
                {
                    case enu2DPropertyChangedType.MatrixChanged:
                    case enu2DPropertyChangedType.SizeChanged:
                    case enu2DPropertyChangedType.DefinitionChanged:
                        this.m_group.ClearTransform();
                        float dx = 0.0f;
                        float dy = 0.0f;
                        Rectanglef v_rc = this.m_txt.GetBound();
                        dx = v_rc.Width - m_textBound.Width;
                        dy = v_rc.Height - m_textBound.Height;

                        this.m_group.Translate(dx, dy, enuMatrixOrder.Append);
                        break;
                }
            }
            public override string ToString()
            {
                return "AndroidMenuGroupLayoutManager";
            }
        }
    }
}
