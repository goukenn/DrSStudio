

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKGVTViewToolWindowTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.Tools
{
    /// <summary>
    /// IGK GLOBAL VIEW TOOL WINDOW represent the base tool 
    /// </summary>
    [CoreTools("Tool.ViewToolWindow", ImageKey=CoreImageKeys.MENU_TOOLS_GKDS)]
    sealed class ViewToolWindowTool :
        CoreToolBase       
    {
        private static ViewToolWindowTool sm_instance;
        Dictionary<string, IGKGVTExpenderGroupManager> m_groups = new Dictionary<string, IGKGVTExpenderGroupManager>();
        private ViewToolWindowTool()
        {
        }

        public static ViewToolWindowTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ViewToolWindowTool()
        {
            sm_instance = new ViewToolWindowTool();

        }
        protected override void GenerateHostedControl()
        {
            IGKGVTXSelectionGUI  c = new IGKGVTXSelectionGUI()
            {
                Tool = this,
                ToolDocument = CoreResources.GetDocument(this.ToolImageKey ),
                CaptionKey = "title."+this.Id,
                
            };
            //init hosted control

            this.HostedControl = c;
            this.InitHost(c);
        }
        
        private void InitHost(IGKGVTXSelectionGUI c)
        {
            c.HostedControl.Renderer = new IGKGVTViewToolRenderer(this, c.HostedControl );
            c.HostedControl.Animate = true;
            foreach (IWorkingItemGroup group in IGKGTShortcutManager.Instance.GetGroups())
            {
                IGKXExpenderBoxGroup  g =  c.HostedControl.Groups.Add(group.Name);
                if (g != null)
                {
                    g.CaptionKey = string.Format ("toolgroup.{0}",group.Name);
                    g.Tag = group;
                    g.ImageKey = "group_" + group.Name;
                   IGKGVTExpenderGroupManager p =  new IGKGVTExpenderGroupManager(group, g, this);
                    p.Load(group.Items);
                    this.m_groups.Add(group.Name,p);
                }
                else {
                    g = c.HostedControl.Groups[group.Name];
                    IGKGVTExpenderGroupManager d = this.m_groups[group.Name];
                    d.Load(group.Items);

                    //IGKXExpenderBoxGroup v_group = new IGKXExpenderBoxGroup();
                    //g.CaptionKey = group.Name;
                    //g.Tag = group;
                    //g.ImageKey = "group_" + group.Name;
                    //new IGKGVTExpenderGroupManager(group, g, this).Load(group.Items);
                }
            }
        }
       
        
        /// <summary>
        /// represent the tool renderer
        /// </summary>
        class IGKGVTViewToolRenderer : IGKXExpenderBoxRenderer 
        {
            private ViewToolWindowTool m_tool;
            public ICoreWorkingToolManagerSurface CurrentSurface
            {
                get
                {
                    return m_tool.CurrentSurface as ICoreWorkingToolManagerSurface;
                }
            }
            public IGKGVTViewToolRenderer(ViewToolWindowTool tool, IGKXExpenderBox box):base(box)
            {                
                this.m_tool = tool;
            }
            public override void RenderBoxGroup(IGKXExpenderBoxGroup group, CorePaintEventArgs e)
            {
                base.RenderBoxGroup(group, e);
            }
            public override void RenderBoxItem(IGKXExpenderBoxItem item, CorePaintEventArgs e)
            {
                Rectanglei rc = new Rectanglei(0, 0, item.Width, item.Height);
                  ICoreWorkingToolManagerSurface v_s = this.CurrentSurface;
                  IGKGVTViewExpenderToolItem s = item as IGKGVTViewExpenderToolItem;
                  Rectanglef v_shortcutrc = Rectanglef.Empty;
                  //draw caption string
                  rc.X += 16;
                  rc.Width -= 16;

                  v_shortcutrc = rc;
                  bool v_callparent = true;
                if (v_s !=null)
                {

                    Type t = (Type)item.Tag;
                    
                    if (v_s.CurrentTool == (Type)item.Tag )
                    {
                        e.Graphics.Clear(WinCoreControlRenderer.WindowToolSelectedBackgroundColor);
                        bool v_isselected = (box.SelectedGroup == item.ParentGroup);
                        bool v_isover = (item.MouseState == enuMouseState.Hover);
                        Colorf cl1 = !v_isover ? WinCoreControlRenderer.ExpenderItemColor : WinCoreControlRenderer.ExpenderItemOverColor;
                        Colorf cl2 = !v_isover ? WinCoreControlRenderer.WindowToolSelectedForeColor : WinCoreControlRenderer.WindowToolSelectedOverForeColor;
                        
                        //e.Graphics.FillRectangle(cl1, 0, 0, item.Width, item.Height);


                        StringFormat sf = new StringFormat();
                        sf.LineAlignment = StringAlignment.Center;
                        sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.MeasureTrailingSpaces;
                        sf.Trimming = StringTrimming.EllipsisWord;


                        ICore2DDrawingDocument d = CoreResources.GetDocument(item.ImageKey);
                        if (d != null)
                        {
                            d.Draw(e.Graphics, new Rectanglei(2, 2, item.Height - 4, item.Height - 4));
                        }

                        e.Graphics.DrawString(
                            item.CaptionKey.R(),
                            item.Font,
                            WinCoreBrushRegister.GetBrush(cl2),
                            rc,
                            sf);

                        //if (s.ShortCut != enuKeys.None)
                        //{
                        //    v_shortcutrc = rc;
                        //    e.Graphics.DrawString(
                        //       CoreResources.GetShortcutText(s.ShortCut),
                        //       item.Font,
                        //       WinCoreBrushRegister.GetBrush(cl2),
                        //       v_shortcutrc,
                        //       sf);
                        //}

                        sf.Dispose();
                        e.Graphics.DrawRectangle(WinCoreControlRenderer.WindowToolSelectedtemBorderColor, 0, 0, item.Width - 1, item.Height - 1);
                        v_callparent = false;
                    }
                }
                if (v_callparent )
                base.RenderBoxItem(item, e);
                if (s.ShortCut != enuKeys.None)
                {
                    bool v_isselected = (box.SelectedGroup == item.ParentGroup);
                    bool v_isover = (item.MouseState == enuMouseState.Hover);

                    Colorf cl1 = !v_isover ? WinCoreControlRenderer.ExpenderItemColor : WinCoreControlRenderer.ExpenderItemOverColor;
                    Colorf cl2 = !v_isover ? WinCoreControlRenderer.ExpenderItemForeColor : WinCoreControlRenderer.ExpenderItemOverForeColor;

                    StringFormat sf = new StringFormat();
                    sf.LineAlignment = StringAlignment.Center;
                    sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap | StringFormatFlags.FitBlackBox | StringFormatFlags.MeasureTrailingSpaces;
                    sf.Trimming = StringTrimming.None;
                    sf.Alignment = StringAlignment.Far;

                    e.Graphics.DrawString(
                       CoreResources.GetShortcutText(s.ShortCut),
                       item.Font,
                       WinCoreBrushRegister.GetBrush(cl2),
                       v_shortcutrc,
                       sf);
                }
            }            
        }
      
        /// <summary>
        /// represent the group item menu manager
        /// </summary>
        class IGKGVTExpenderGroupItemMenuManager : CoreToolBase 
        {
            private IGKXExpenderBoxItem c_expenderBoxItem;
            private ICoreGTWorkingItem m_item;
            
            private bool m_IsSelected;

            public bool IsSelected
            {
                get { return m_IsSelected; }
                set
                {
                    if (m_IsSelected != value)
                    {
                        m_IsSelected = value;
                        this.c_expenderBoxItem.Invalidate();
                    }
                }
            }
            public IGKGVTExpenderGroupItemMenuManager(ICoreGTWorkingItem i, IGKXExpenderBoxItem c, ICoreSystemWorkbench coreWorkbench)
            {
                
                this.m_item = i;
                this.c_expenderBoxItem = c;
                this.Workbench  = coreWorkbench;
            }

            public new ICoreWorkingToolManagerSurface CurrentSurface {
                get {
                    return base.CurrentSurface as ICoreWorkingToolManagerSurface;
                }
            }


            protected override void RegisterBenchEvent(ICoreSystemWorkbench Workbench)
            {
                base.RegisterBenchEvent(Workbench);
                if (Workbench is ICoreWorkingSurfaceHandler s)
                    s.CurrentSurfaceChanged += Workbench_CurrentSurfaceChanged;
            }
            protected override void UnregisterBenchEvent(ICoreSystemWorkbench Workbench)
            {
                if (Workbench is ICoreWorkingSurfaceHandler s)
                    s.CurrentSurfaceChanged -= Workbench_CurrentSurfaceChanged;
                base.UnregisterBenchEvent(Workbench);
            }
            

            void Workbench_CurrentSurfaceChanged(object sender, CoreWorkingElementChangedEventArgs<ICoreWorkingSurface> e)
            {
                if (e.OldElement  is ICoreWorkingToolManagerSurface)
                {
                    UnRegisterSurfaceEvent(e.OldElement  as ICoreWorkingToolManagerSurface);
                }
                if (e.NewElement is ICoreWorkingToolManagerSurface)
                {
                    RegisterSurfaceEvent(e.NewElement as ICoreWorkingToolManagerSurface);
                }
                UpdateTool();
            }

         
            void surface_CurrentToolChanged(object sender, EventArgs e)
            {
                UpdateTool();
          
            }

            private void UpdateTool()
            {
                var g = this.c_expenderBoxItem.ParentGroup;
                var s = this.CurrentSurface;
                if ((s!=null) && (s.CurrentTool == this.m_item.ToolType))
                {
                    if (this.c_expenderBoxItem.ParentGroup.SelectedGroupItem != this.c_expenderBoxItem)
                        this.c_expenderBoxItem.ParentGroup.SelectedGroupItem = this.c_expenderBoxItem;
                    g.ExpenderBox.SelectedGroup = g;
                    this.IsSelected = true;
                }
                else 
                    this.IsSelected = false;
            }

            private void RegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
            {
                surface.CurrentToolChanged += surface_CurrentToolChanged;
            }
            private void UnRegisterSurfaceEvent(ICoreWorkingToolManagerSurface surface)
            {
                surface.CurrentToolChanged -= surface_CurrentToolChanged;
            }


        }
        class IGKGVTExpenderGroupManager
        {
            private IGKXExpenderBoxGroup c_xgroup;
            private ViewToolWindowTool m_viewToolWindowTool;
            private IWorkingItemGroup m_group;

     

            public IGKGVTExpenderGroupManager(IWorkingItemGroup group, 
                IGKXExpenderBoxGroup g, 
                ViewToolWindowTool tool)
            {
                this.c_xgroup = g;
                this.m_viewToolWindowTool = tool;
                this.m_group = group;                
                this.m_group.VisibleChanged += group_VisibleChanged;
                this.c_xgroup.Visible = this.m_group.Visible;
                this.m_viewToolWindowTool.VisibleChanged += _VisibleChanged;
           }


            void _VisibleChanged(object sender, EventArgs e)
            {
                this.updateVisibility();
            }
            void updateVisibility()
            {
                if (this.c_xgroup.Visible != this.m_group.Visible)
                    this.c_xgroup.Visible = this.m_group.Visible;
            }
            void group_VisibleChanged(object sender, EventArgs e)
            {
                this.updateVisibility();
            }

         
           
            internal void Load(IWorkingItemCollections items)
            {
                foreach (ICoreGTWorkingItem i in items)
                {
                    IGKXExpenderBoxGroupItem c =
                    new IGKGVTViewExpenderToolItem()
                    { 
                        ImageKey = i.ImageKey ,
                        CaptionKey = i.CaptionKey ,
                        Tag = i.ToolType ,
                        ShortCut = i.Keys
                    };
                    c.Click +=c_Click;
                    new IGKGVTExpenderGroupItemMenuManager(i,
                        c,
                        this.m_viewToolWindowTool.Workbench );
                    this.c_xgroup.Items.Add(c);
                }
            }

            void c_Click(object sender, EventArgs e)
            {
                ICoreWorkingToolManagerSurface c = this.m_viewToolWindowTool.CurrentSurface as ICoreWorkingToolManagerSurface;
                if (c != null)
                {
                    c.CurrentTool = (sender as Control).Tag as Type;
                }
            }


         
        }
       
        /// <summary>
        /// represent a class that expend group item
        /// </summary>
        sealed class IGKGVTViewExpenderToolItem : IGKXExpenderBoxGroupItem
        {
            public enuKeys ShortCut { get; set; }
        }
    }
}
