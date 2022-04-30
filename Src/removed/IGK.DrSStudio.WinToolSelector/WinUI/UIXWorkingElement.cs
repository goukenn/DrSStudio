

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXWorkingElement.cs
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
file:UIXWorkingElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D ;
using System.Drawing.Text ;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D;
    class UIXWorkingElement :
        UIXToolConfigControlBase ,
        IWorkingGroupOwner 
    {
        const int GROUP_BOX_ROUND_RADIUS = 4;
        private int currentWidth;
        private int currentHeight;
        private bool m_configScroll;
        private XVerticalScrollBar m_vscroll;
        internal GroupElementCollections m_groups;
        private ICore2DDrawingDocument m_upDocument;
        private ICore2DDrawingDocument m_downDocument;
        new ToolManager Tool { get { return base.Tool as ToolManager; } }
        public  XVerticalScrollBar VScrollBar { get { return this.m_vscroll; } }
        private void InitializeComponent()
        {
            this.m_vscroll = new IGK.DrSStudio.WinUI.XVerticalScrollBar();
            this.SuspendLayout();
            // 
            // xVerticalScrollBar1
            // 
            this.m_vscroll.Location = new System.Drawing.Point(195, 3);
            this.m_vscroll.MinimumSize = new System.Drawing.Size(16, 132);
            this.m_vscroll.Name = "xVerticalScrollBar1";
            this.m_vscroll.Size = new System.Drawing.Size(20, 322);
            this.m_vscroll.TabIndex = 0;
            this.m_vscroll.Value = 0;
            // 
            // UIXWorkingElement
            // 
            this.Controls.Add(this.m_vscroll);
            this.Name = "UIXWorkingElement";
            this.Size = new System.Drawing.Size(218, 325);
            this.ResumeLayout(false);
        }
        public UIXWorkingElement():base(ToolManager.Instance )
        {
            m_groups = new GroupElementCollections(this);
            this.InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.Selectable, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.currentHeight = this.Height;
            this.currentWidth = this.Height;
            this.Controls.Add(m_vscroll);
            HideScroll();
            VScroll = false;
            m_vscroll.Scroll += new ScrollEventHandler(m_vscroll_Scroll);
            this.ParentChanged += new EventHandler(_ParentChanged);
            this.Paint += new PaintEventHandler(_Paint);
            this.m_upDocument = CoreResources.GetDocument("btn_Updocument");
            this.m_downDocument = CoreResources.GetDocument("btn_Downdocument");
            this.InitBound();
        }
        void _Paint(object sender, PaintEventArgs e)
        {
           //clear control background
            e.Graphics.Clear(WorkingItemRenderer.WinToolSelectorBackgroundColor);
            int pos_y = 0;
            if (this.m_vscroll.Visible)
                pos_y = this.m_vscroll.Value - this.m_vscroll.Minimum;
            e.Graphics.TranslateTransform(0, -pos_y, enuMatrixOrder.Append);
            bool draw = (pos_y <= WinToolSelectorConstant.DIM_GROUP_HEIGHT);
            int v_w = 0;
            if (this.m_vscroll.Visible)
            {
                v_w = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth + 1;
            }
            Rectangle rc = new Rectangle(0, 0, this.Width - v_w, WinToolSelectorConstant.DIM_GROUP_HEIGHT);
            int v_visibleGroup = 0;
            foreach (IWorkingItemGroup group in this.Groups)
            {
                if (!group.Visible)
                {
                    continue;
                }
                v_visibleGroup++;
                if (draw)
                {
                    DrawGroup(group, e.Graphics, rc);
                    pos_y += WinToolSelectorConstant.DIM_GROUP_HEIGHT;
                }
                else
                {
                    pos_y -= WinToolSelectorConstant.DIM_GROUP_HEIGHT;
                    draw = (pos_y <= 0);
                    if (draw) pos_y = 0;
                }
                rc.Y += WinToolSelectorConstant.DIM_GROUP_HEIGHT;
                if (group.Collapsed == false)
                {
                    rc.Height = WinToolSelectorConstant.DIM_ITEM_HEIGHT;
                    foreach (IWorkingItem item in group.Items)
                    {
                        if (draw)
                        {
                            DrawItem(item, e.Graphics, rc);
                            pos_y += WinToolSelectorConstant.DIM_ITEM_HEIGHT;
                        }
                        else
                        {
                            pos_y -= WinToolSelectorConstant.DIM_ITEM_HEIGHT;
                            draw = (pos_y <= 0);
                            if (draw) pos_y = 0;
                        }
                        rc.Y += WinToolSelectorConstant.DIM_ITEM_HEIGHT;
                    }
                }
                rc.Height = WinToolSelectorConstant.DIM_GROUP_HEIGHT;
                if (draw)
                {
                    if (pos_y > this.Height)
                        break;
                }
            }
            if (v_visibleGroup == 0)
            {
                e.Graphics.DrawString(
                    CoreSystem.GetString("UIXWorkingElement.NoGroupOrItem"),
                    this.Font,
                    CoreBrushRegister.GetBrush(WorkingItemRenderer.WinToolSelectorForeColor),
                    Point.Empty);
            }
        }
        void _ParentChanged(object sender, EventArgs e)
        {
            ScrollableControl c = this.Parent as ScrollableControl;
            if (c != null)
            {
                if (c.AutoScroll)
                    c.AutoScroll = false;
            }
        }
        void m_vscroll_Scroll(object sender, ScrollEventArgs e)
        {
            if (this.m_configScroll)
                return;
            this.Refresh();
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            this.InitBound();
            base.OnSizeChanged(e);
        }
        private void InitBound()
        {
            this.m_vscroll.Bounds = new System.Drawing.Rectangle(
                this.Width  - System.Windows.Forms.SystemInformation.VerticalScrollBarWidth ,
                0,
                System.Windows.Forms.SystemInformation.VerticalScrollBarWidth ,
                this.Height );                
        }
        private IWorkingItemGroup GetLastVisibleItem()
        {
            for (int i = this.Groups.Count - 1; i >= 0 ; i--)
            {
                if (this.m_groups[i].Visible)
                    return this.m_groups[i];
            }
            return null;
        }
        private void SetupScrollValue()
        {
            if (this.m_configScroll)
                return;
            this.m_configScroll = true;
            IWorkingItemGroup v_group = GetLastVisibleItem();
            if (v_group !=null)
            {
                //get last group                
                Rectangle rc = v_group.Bound;
                int y = rc.Y + rc.Height + WinToolSelectorConstant.DIM_ITEM_HEIGHT;//marge
                int value = this.m_vscroll.Value;
                if (v_group.Collapsed == false)
                {
                    y += v_group.InnerHeight;
                }
                if (this.Height < y)
                {
                    this.m_vscroll.SetupScrollValue(this.Height,
                        y,
                        value);
                    this.ShowScroll();
                }
                else
                {
                    this.m_vscroll.SetupScrollValue(0,
                      0,
                      0);
                    this.HideScroll();
                }
            }
            else
                this.HideScroll();
            this.m_configScroll = false;
        }
        private void HideScroll()
        {
            this.m_vscroll.SetupScrollValue(0, 0, 0);
            this.m_vscroll.Visible = false;
        }
        private void ShowScroll()
        {
            this.m_vscroll.Visible = true;
        }
        #region IWorkingGroupOwner Members
        public IWorkingItemGroupCollections Groups
        {
            get { return this.m_groups; }
        }
        #endregion
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (this.Focused && this.m_vscroll.Visible)
            {
                int i = 0;
                if (e.Delta < 0)
                {
                    i = this.m_vscroll.Value + this.m_vscroll.LargeChange;
                    i = (i > this.m_vscroll.Maximum) ? this.m_vscroll.Maximum : i;
                    this.m_vscroll.Value = i;
                }
                else
                {
                    i = this.m_vscroll.Value - this.m_vscroll.LargeChange;
                    i = (i < this.m_vscroll.Minimum) ? this.m_vscroll.Minimum : i;
                    this.m_vscroll.Value = i;
                }
                this.Invalidate();
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (this.Focused != true)
                this.Focus();
        }
        /// <summary>
        /// draw group 
        /// </summary>
        /// <param name="group"></param>
        /// <param name="graphics"></param>
        /// <param name="rc"></param>
        private void DrawGroup(IWorkingItemGroup group, Graphics graphics, Rectangle rc)
        {
            Color cl1 = WorkingItemRenderer.WinToolSelectorGroupStartColor;
            Color cl2 = WorkingItemRenderer.WinToolSelectorGroupEndColor;
            Color v_clborder = WorkingItemRenderer.WinToolSelectorGroupBorderColor;
            GraphicsPath v_path = new GraphicsPath();
            v_path.AddRectangle(rc);
            GraphicsState v_state = graphics.Save();
            //graphics.Clip = new Region(v_path);
            using (LinearGradientBrush lnb =
                new LinearGradientBrush(rc,
                cl1,
                cl2,
                90.0f))
            {
                graphics.FillRectangle(lnb, rc);
            }
            //draw image icon
            if (!string.IsNullOrEmpty(group.ImageKey))
            {
                ICore2DDrawingDocument doc = CoreResources.GetDocument(group.ImageKey);
                if (doc != null)
                {
                    //graphics.Clear(Colorf.Red);
                    //graphics.FillRectangle(Brushes.Yellow, graphics.VisibleClipBounds);
                    graphics.TranslateTransform(2, 2);
                    doc.Draw(graphics, true, new Rectangle(0,rc.Y,22 , 22), enuFlipMode.None);
                    graphics.TranslateTransform(-2, -2);
                }
            }
            string v_txt = CoreSystem.GetString(group.Title);
            FontStyle ft = FontStyle.Regular | FontStyle.Bold;
            StringFormat sf = new StringFormat();
            Font fnt = new Font(this.Font, ft);
            sf.LineAlignment = StringAlignment.Center;
            sf.FormatFlags = StringFormatFlags.NoClip | StringFormatFlags.NoWrap;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            Rectanglef vrc = rc;
            vrc.X += 24;
            vrc.Width -= (36+24);
            if (!string.IsNullOrEmpty (v_txt) && (vrc.Width > 0))
            {
                graphics.DrawString(
                    v_txt,
                    fnt,
                    CoreBrushRegister.GetBrush(WorkingItemRenderer.WinToolSelectorGroupForeColor),
                    vrc,
                    sf);
            }
            Vector2f pts = new Vector2f(rc.Width - 32,
                rc.Y + 2);
            if (pts.X > 48)
            {
                graphics.TranslateTransform(pts.X, pts.Y, enuMatrixOrder.Append);
                if (group.Collapsed)
                {
                    if (this.m_upDocument != null)
                        this.m_upDocument.Draw(graphics, false, 24, 24, enuFlipMode.None);
                }
                else
                {
                    if (this.m_downDocument != null)
                        this.m_downDocument.Draw(graphics, false, 24, 24, enuFlipMode.None);
                }
            }
            graphics.TranslateTransform(-pts.X, -pts.Y, enuMatrixOrder.Append);
            sf.Dispose();
            fnt.Dispose();
            graphics.Restore(v_state);
            v_state = graphics.Save();
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.DrawPath(CoreBrushRegister.GetPen(v_clborder), v_path);
            graphics.Restore(v_state );
            v_path.Dispose();
        }
        /// <summary>
        /// draw item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="graphics"></param>
        /// <param name="bounds"></param>
        private void DrawItem(IWorkingItem  item, Graphics graphics, Rectangle bounds)
        {
            Rectangle rc = bounds;
            Color v_startCl = WorkingItemRenderer.WinToolSelectorStartColor;
            Color v_endCl = WorkingItemRenderer.WinToolSelectorEndColor;
            Color v_itemForeColor = WorkingItemRenderer.WinToolSelectorForeColor;
            Color v_itemSelectedForeColor = WorkingItemRenderer.WinToolSelectorSelectedForeColor;
            if (item.MouseState == enuButtonState.Over)
            {
                v_startCl = WorkingItemRenderer.WinToolSelectorOverStartColor;
                v_endCl = WorkingItemRenderer.WinToolSelectorOverEndColor;
                v_itemForeColor = WorkingItemRenderer.WinToolSelectorOverForeColor;
            }
            if (this.Tool.SelectedWorkingType == item.Type)
            {
                v_startCl = WorkingItemRenderer.WinToolSelectorSelectedStartColor;
                v_endCl = WorkingItemRenderer.WinToolSelectorSelectedEndColor;
            }
            using (LinearGradientBrush lnb = new LinearGradientBrush(bounds,
               v_startCl, v_endCl, 0.0f))
            {
                graphics.FillRectangle(lnb, bounds);
            }
            //if (item.Index > 0)
            //{
            Vector2f[] pts = CoreMathOperation.GetPoints(bounds);
            Pen p = CoreBrushRegister.GetPen(WorkingItemRenderer.WinToolSelectorBorderColor);
            //graphics.DrawLine(p, pts[0], pts[1]);
            if (item.Group.Items.IndexOf (item as WorkingItem ) >0){
                graphics.DrawLine(p, pts[0], pts[1]);
            //graphics.DrawRectangle(p, bounds.X, bounds.Y, bounds.Width - 1, bounds.Height);
            }
            //}
            FontStyle ft = FontStyle.Regular;
            if (item.Selected)
            {
                ft |= FontStyle.Bold;
            }
            StringFormat sf = new StringFormat();
            Font fnt = new Font(this.Font, ft);
            ICore2DDrawingDocument v_doc = item.DocumentTool;
            Rectanglei v_rc = new Rectanglei(rc.X + 4, rc.Y, bounds.Height, bounds.Height);
            GraphicsState v_state = graphics.Save();
            graphics.Clip = new Region(v_rc);
            if (v_doc != null)
            {
                try
                {  
                   v_doc.Draw(graphics, false, new  Rectanglei(v_rc.X, v_rc.Y, 16,16), enuFlipMode.None);                   
                }
                catch(Exception ex)
                {
                    CoreLog.WriteDebug(ex.Message);
                }
            }
            else 
            {
                Bitmap bmp = CoreSystem.Instance.Resources.GetImage (item.ImageKey) as Bitmap ;
                if (bmp != null)
                {
                    System.Drawing.Imaging.ImageAttributes attr =
                        new System.Drawing.Imaging.ImageAttributes();
                    attr.SetColorKey(
                        Color.Magenta,
                        Color.Magenta);
                    graphics.DrawImage (
                     bmp ,
                     v_rc ,
                             0,0,bmp.Width, bmp .Height ,                             
                             GraphicsUnit .Pixel ,
                             attr );
                }
                else
                {
                    ICore2DDrawingDocument doc = CoreResources.GetDocument("DE_Default");
                    if (doc != null)
                        doc.Draw(graphics, false, v_rc , enuFlipMode.None);
                }
            }
            graphics.Restore(v_state);
            rc.X += 24;
            rc.Width -= 24;
            sf.LineAlignment = StringAlignment.Center;
            string v_str = CoreSystem.GetString(item.Title);
            //draw string 
            Brush br = null;
            if (item.Type == this.Tool.SelectedWorkingType)
            {
                br = CoreBrushRegister.GetBrush(v_itemSelectedForeColor);
            }
            else
                br = CoreBrushRegister.GetBrush(v_itemForeColor);
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.FormatFlags = StringFormatFlags.NoWrap;
            graphics.DrawString(v_str,
                fnt,
                br,
                rc,
                sf);
            sf.Trimming = StringTrimming.None;
            SizeF v_size = graphics.MeasureString(v_str, fnt, rc.Size);
            if (v_size.Width < rc.Width)
            {
                v_str = item.DefaultKey;
                if (string.IsNullOrEmpty(v_str) == false)
                {
                    rc.Width -= (int)(25 + v_size.Width);
                    rc.X += (int)v_size.Width;
                    sf.Alignment = StringAlignment.Far;
                    sf.Trimming = StringTrimming.EllipsisCharacter;
                    sf.FormatFlags |= StringFormatFlags.NoWrap;
                    v_str = "Ctrl+T," + v_str;
                    v_size = graphics.MeasureString(v_str, fnt, rc.Size);
                    graphics.DrawString(
                        v_str
                        ,
                        fnt,
                        br,
                        rc,
                        sf);
                }
            }
            sf.Dispose();
            fnt.Dispose();
        }
        /// <summary>
        /// reprepsent the group element collection
        /// </summary>
        public class GroupElementCollections : IWorkingItemGroupCollections,
            IComparer<IWorkingItemGroup> 
        {
            List<IWorkingItemGroup> m_groups;
            UIXWorkingElement m_owner;
            public GroupElementCollections(UIXWorkingElement owner)
            {
                this.m_groups = new List<IWorkingItemGroup>();
                this.m_owner = owner;
            }
            #region IWorkingItemGroupCollections Members
            public IWorkingItemGroup this[int index]
            {
                get { return this.m_groups [index]; }
            }
            public int IndexOf(IWorkingItemGroup group)
            {
                return this.m_groups.IndexOf(group);
            }
            public int Count
            {
                get { return this.m_groups.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_groups.GetEnumerator();
            }
            #endregion
            /// <summary>
            /// add group to the collection
            /// </summary>
            /// <param name="group"></param>
            public void Add(IWorkingItemGroup group) {
                if ((this.m_owner != null)&& (!this.m_groups .Contains (group)))
                {
                    this.m_groups.Add(group);
                    group.CollapsedChanged += new EventHandler(group_CollapsedChanged);
                }
            }
            void group_CollapsedChanged(object sender, EventArgs e)
            {
                GroupExpandedChangedEventArgs v_e = new GroupExpandedChangedEventArgs
                (sender as IWorkingItemGroup);
                this.m_owner.OnGroupExpandedChanged(v_e);
            }
            #region IComparer<IWorkingItemGroup> Members
            public int Compare(IWorkingItemGroup x, IWorkingItemGroup y)
            {
                return CoreSystem.GetString ( x.Title).CompareTo( CoreSystem.GetString (y.Title));
            }
            #endregion
            #region IWorkingItemGroupCollections Members
            public void Sort()
            {
                this.m_groups.Sort(this);
            }
            #endregion
        }
        #region IWorkingGroupOwner Members
        public event EventHandler SelectedWorkingTypeChanged
        {
            add { this.Tool.SelectedWorkingTypeChanged += value; }
            remove { this.Tool.SelectedWorkingTypeChanged -= value; }
        }
        public event GroupExpandedChangedEventHandler GroupExpandedChanged;
        #endregion
        protected void OnGroupExpandedChanged(GroupExpandedChangedEventArgs e)
        {            
            if (this.GroupExpandedChanged != null)
                this.GroupExpandedChanged(this, e);
            this.SetupScrollValue();
            this.Invalidate ();
        }
        #region IWorkingGroupOwner Members
        Type IWorkingGroupOwner.SelectedWorkingType
        {
            get
            {
                return this.Tool.SelectedWorkingType ;    
            }
            set
            {
                this.Tool.SelectedWorkingType = value ;
            }
        }
        #endregion
    }
}

