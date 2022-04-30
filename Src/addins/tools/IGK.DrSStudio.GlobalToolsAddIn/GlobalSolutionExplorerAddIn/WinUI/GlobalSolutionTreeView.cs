

using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalSolutionTreeView.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.Drawing2D;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore.WinUI;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a solution tree view
    /// </summary>
    public class GlobalSolutionTreeView : IGKXTreeView
    {      
        ImageList m_smallImageList;

        private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
        private const int TVS_EX_DOUBLEBUFFER = 0X4;

        [DllImport("user32.dll")]
        static extern void SendMessage(IntPtr handler, int t, IntPtr lparmam, IntPtr wparam);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (IntPtr)TVS_EX_DOUBLEBUFFER, (IntPtr)TVS_EX_DOUBLEBUFFER);
        }
        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_smallImageList.Dispose();
            }
            base.Dispose(disposing);
        }
        public GlobalSolutionTreeView()
        {
            m_smallImageList = new ImageList();
            m_smallImageList.ImageSize = new System.Drawing.Size(16, 16);
            m_smallImageList.ColorDepth = ColorDepth.Depth32Bit;

            this.ImageList = m_smallImageList;
            this.Font = CoreFont.CreateFont("consolas", 8, enuFontStyle.Regular, enuRenderingMode.Vector).ToGdiFont();


            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.LabelEdit = false ;

            this.DrawMode = TreeViewDrawMode.OwnerDrawAll;
            this.DrawNode += _DrawNode;
            this.MouseDown += _MouseDown;
            this.NodeMouseClick += _NodeMouseClick;
            this.ItemHeight = 18;
            CoreRenderer.RenderingValueChanged += WinCoreRenderer_RenderingValueChanged;
        }

        void WinCoreRenderer_RenderingValueChanged(object sender, EventArgs e)
        {
            if (this.Visible && this.IsHandleCreated)
            {
                
                //OnBackColorChanged(EventArgs.Empty);
                //BackColor = SolutionRenderer.SolutionTreeViewBackgroundColor.ToGdiColor();
               // this.Refresh();
                int i = ColorTranslator.ToWin32(SolutionRenderer.SolutionTreeViewBackgroundColor.ToGdiColor());
                //force background color to invalidate
                SendMessage(this.Handle, 0x111d, IntPtr.Zero  , new IntPtr(i)  );
                //intend message 
                SendMessage(this.Handle, 0x1107,IntPtr.Zero ,  new IntPtr(this.Indent));
                    
            }
        }

        void _NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (CanExpandOrCollapse(e.Node))
                {

                    //e.Node.Toggle();
                }
            }
        }
        

        private void _MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode node = GetNodeAt(e.X, e.Y);
            this.SelectedNode = node;
          
            
        }
        protected override void OnBeforeCollapse(TreeViewCancelEventArgs e)
        {
            e.Cancel = (e.Action != TreeViewAction.Collapse ) && !CanExpandOrCollapse(e.Node);
            base.OnBeforeCollapse(e);
        }
        protected override void OnBeforeExpand(TreeViewCancelEventArgs e)
        {
            e.Cancel = (e.Action != TreeViewAction.Expand) && !CanExpandOrCollapse(e.Node);
            base.OnBeforeExpand(e);
        }
        protected override void OnAfterCollapse(TreeViewEventArgs e)
        {
            this.Invalidate();
            base.OnAfterCollapse(e);
        }
        protected override void OnAfterExpand(TreeViewEventArgs e)
        {
            this.Invalidate();
            base.OnAfterExpand(e);
        }

        

        private bool CanExpandOrCollapse(TreeNode treeNode)
        {
            int depth = treeNode.Level;
            int y = treeNode.Bounds.Y;
            int w = this.ImageList == null ? 16 : this.ImageList.ImageSize.Width;
            Rectangle rc = new Rectangle(w * depth, y, w, w);
            if (rc.Contains(this.PointToClient(Cursor.Position)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.Invalidate();
        }
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }
        public override Color BackColor
        {
            get
            {
                return SolutionRenderer.SolutionTreeViewBackgroundColor.ToGdiColor();
            }
            set
            {
                base.BackColor = value;
            }
        }
     
        void _DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
           
            Colorf v_fColor = Colorf.Black;
            Colorf v_bColor = Colorf.Transparent;
            FontStyle v_ft = FontStyle.Regular;
            int depth = e.Node.Level;

            if ((e.State & TreeNodeStates.Selected) != 0)
            {
                v_bColor = WinCoreControlRenderer.TreeviewSelectedBackgroundColor;
                v_fColor = WinCoreControlRenderer.TreeviewSelectedForeColor;
                v_ft |= FontStyle.Bold;
            }

            e.Graphics.FillRectangle(WinCoreBrushRegister.GetBrush(v_bColor),
                e.Bounds);
            int count = 2;
            int v_ww = (this.ImageList != null) ? this.ImageList.ImageSize.Width : 16;
            int x = v_ww * (count + depth);
            int y = e.Bounds.Y;
            int w = 0;
            int h = e.Bounds.Height;


            TextFormatFlags format = TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis |
                TextFormatFlags.NoClipping |
                TextFormatFlags.NoPadding | TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
            Font ft = new Font(e.Node.NodeFont ?? this.Font, v_ft);  
            Size s = TextRenderer.MeasureText(e.Node.Text, ft, new Size(short.MaxValue, short.MaxValue), format);
            w = Math.Max(s.Width, e.Bounds.Width);


            //draw + 
            int hx = v_ww * depth;
            VisualStyleRenderer renderer = null;
            if (e.Node.IsExpanded)
            {
                 renderer =
             new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Opened);
                 renderer.DrawBackground(e.Graphics, new Rectangle(hx,
     y, v_ww, v_ww));
            }
            else {
                if (e.Node.Nodes.Count > 0)
                {
                    renderer = new VisualStyleRenderer(VisualStyleElement.TreeView.Glyph.Closed);
                    renderer.DrawBackground(e.Graphics, new Rectangle(hx,
        y, v_ww, v_ww));
                }

            }

    //        e.Graphics.DrawRectangle(Pens.Black,
    //hx,
    //y, v_ww, v_ww);

            //draw image
            hx += v_ww + 2;
            ICore2DDrawingDocument doc = CoreResources.GetDocument(e.Node.ImageKey);
            if (doc != null)
            {
                doc.Draw(e.Graphics, new Rectangle(hx,
        y, v_ww, v_ww));
            }
            else
            {
                e.Graphics.DrawRectangle(Pens.Black,
            hx,
            y, v_ww, v_ww);
            }

            //if (e.Node.Nodes.Count > 0)
            //{
            //    return;
            //}

            using (StringFormat v_sf = new StringFormat())
            {

                TextRenderer.DrawText(e.Graphics,
                e.Node.Text,
                    ft,
                    // new Point((int)x, (int)y),
                    Rectangle.Round(new RectangleF(x + 4, y, w, h)),
                    v_fColor.ToGdiColor(),
                    format);

                //e.Graphics.DrawString(e.Node.Text,
                //    ft,
                //     fbr,
                //    Rectangle.Round(new RectangleF(x, y, w, h)),
                //    v_sf);

            }
            ft.Dispose();

        }
        public void RegisterImageList(string name)
        {
            //if ( string.IsNullOrEmpty(name) || this.m_smallImageList.Images.ContainsKey(name))
            //    return;
            //var c = CoreResources.GetDocument(name);
            //if (c != null)
            //{
            //    var bitmap = c.ToBitmap().ToGdiBitmap();
            //    if (bitmap != null)
            //    {
            //        this.m_smallImageList.Images.Add(name, bitmap);
            //    }
            //}
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
    }

     
}
