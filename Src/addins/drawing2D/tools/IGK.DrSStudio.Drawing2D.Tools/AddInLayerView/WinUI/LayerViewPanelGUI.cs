

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerViewPanelGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    public class LayerViewPanelGUI : IGKXUserControl
    {
        private System.Windows.Forms.Layout.LayoutEngine m_lEngine;

        class LayerViewPanelGUILayoutEngine : LayoutEngine
        {
            public override bool Layout(object container, System.Windows.Forms.LayoutEventArgs layoutEventArgs)
            {
                LayerViewPanelGUI c = container as LayerViewPanelGUI;
                if (c != null)
                {
                    int x = 0;
                    int y = 0;
                    int height = c.ItemHeight;
                    int width = c.Width;

                    int w = c.VScroll ? SystemInformation.VerticalScrollBarWidth : 0;
                    int h = c.HScroll ? SystemInformation.HorizontalScrollBarHeight : 0;
#pragma warning disable IDE0054 // Use compound assignment
                    y = y - (c.AutoScrollOffset.Y + c.VerticalScroll.Value);
#pragma warning restore IDE0054 // Use compound assignment
#pragma warning disable IDE0054 // Use compound assignment
                    x = x - (c.AutoScrollOffset.X + c.HorizontalScroll.Value);
#pragma warning restore IDE0054 // Use compound assignment
             
                    width -= w;
                    height -= h;
                    foreach (LayerViewItem i in c.Controls)
                    {
                        i.Bounds = new System.Drawing.Rectangle(x, y, width , height );
                        y += c.DefaultItemHeight;
                    }
                }
                if (c.VScroll)
                    return true;
                return false;
            }
        }

        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (this.m_lEngine == null)
                    this.m_lEngine = new LayerViewPanelGUILayoutEngine();
                    return this.m_lEngine;
            }
        }
        public LayerViewPanelGUI()
        {
            this.InitializeComponent();
            this.ItemHeight = this.DefaultItemHeight;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // LayerViewPanelGUI
            // 
            this.Name = "LayerViewPanelGUI";
            this.Size = new System.Drawing.Size(213, 307);
            this.ResumeLayout(false);

        }
        public int ItemHeight { get; set; }
        public virtual int DefaultItemHeight { get { return 32; } }

        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new LayerViewControlCollections(this);
        }
        class LayerViewControlCollections : ControlCollection
        {
            Dictionary<ICore2DDrawingLayer, LayerViewItem> m_dic;
            public LayerViewControlCollections(LayerViewPanelGUI owner)
                : base(owner)
            {
                m_dic = new Dictionary<ICore2DDrawingLayer, LayerViewItem>();
            }
            public override void Add(Control value)
            {
                if (value is LayerViewItem)
                {
                    LayerViewItem r = value as LayerViewItem;
                    base.Add(value);
                    m_dic.Add(r.Layer, r);
                }
            }
            public override void Remove(Control value)
            {
                if (value is LayerViewItem)
                {
                    LayerViewItem r = value as LayerViewItem;
                    base.Remove(value);
                    m_dic.Remove(r.Layer );
                }
            }
            public override void Clear()
            {
                m_dic.Clear();
                base.Clear();
            }


            internal void UpdateIndex(ICore2DDrawingLayer layer, int currentIndex)
            {
                LayerViewItem i = this.m_dic[layer];
                this.SetChildIndex(i, currentIndex);
            }
        }

        internal void UpdateIndex(ICore2DDrawingLayer layer, int currentIndex)
        {
            (this.Controls as LayerViewControlCollections).UpdateIndex(layer, currentIndex);
        }
    }
}
