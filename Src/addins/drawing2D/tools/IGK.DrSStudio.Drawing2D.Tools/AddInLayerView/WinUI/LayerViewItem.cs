

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerViewItem.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.ICore.GraphicModels;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.DrSStudio.Drawing2D.Actions;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    class LayerViewItem : IGKXControl 
    {
        private Core2DDrawingLayer m_Layer;
        private StringElement c_textElement;
        private Timer m_timer;


        /// <summary>
        /// Get or set the layer attached to a layer item
        /// </summary>
        public Core2DDrawingLayer Layer
        {
            get { return m_Layer; }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_timer.Enabled = false;
                m_timer.Dispose();
                c_textElement.Dispose();
            }
            base.Dispose(disposing);
        }
        public LayerViewItem(Core2DDrawingLayer layer)
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.m_timer = new Timer();
            this.m_timer.Interval = 1000;
            this.m_timer.Tick += m_timer_Tick;
            c_textElement = new StringElement();
            c_textElement.SuspendLayout();
            c_textElement.Font.HorizontalAlignment = enuStringAlignment.Center;
            c_textElement.Font.VerticalAlignment = enuStringAlignment.Center ;
            c_textElement.Font.FontSize = "11pt".ToPixel();
            c_textElement.Font.FontName = "Consolas";
            //c_textElement.SmoothingMode = enuSmoothingMode.None;
            
            c_textElement.FillBrush.SetSolidColor(Colorf.Black);
            c_textElement.StrokeBrush.SetSolidColor(Colorf.Transparent);
            c_textElement.ResumeLayout();
            this.Paint += _Paint;
            this.m_Layer = layer;
            this.m_Layer.PropertyChanged += m_Layer_PropertyChanged;
            this.c_textElement.Text = this.m_Layer.Id;
            this.DoubleClick += LayerViewItem_DoubleClick;
            this.MouseDoubleClick += LayerViewItem_MouseDoubleClick;
        }

        void LayerViewItem_MouseDoubleClick(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Left)
            {
                ICoreEditLayerPropertiesAction ak = 
                    CoreSystem.GetAction( IGKD2DrawingConstant.MENU_LAYER+".Properties")
                    as ICoreEditLayerPropertiesAction;
                if (ak != null)
                {
                    ak.Layer = this.m_Layer;
                    ak.DoAction();
                    ak.Layer = null;
                }
            }
        }

        void LayerViewItem_DoubleClick(object sender, EventArgs e)
        {
            
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            this.m_timer.Enabled = false;
            lock (this.m_Layer)
            {
                this.Invalidate();
                this.Update();
            }
        }

        void m_Layer_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.c_textElement.Text = this.m_Layer.Id;
            if (this.m_timer.Enabled)
                this.m_timer.Enabled = false;//reset timer
            this.m_timer.Enabled = true;
        }

        private void _Paint(object sender, CorePaintEventArgs e)
        {
            this.RenderItem(e.Graphics);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.c_textElement.SuspendLayout();
            this.c_textElement.Bounds = new Rectanglef(50, 0, this.Width-55, this.Height);
            
            this.c_textElement.ResumeLayout();
        }


        

        private void RenderItem(ICoreGraphics g)
        {
            Colorf bgcl = ((this.m_Layer.ParentDocument != null) && (this.m_Layer == this.m_Layer.ParentDocument.CurrentLayer)) ?
                LayerViewRenderer.LayerViewItemSelectedBackGrounColor : LayerViewRenderer.LayerViewItemBackGrounColor;
            
                g.FillRectangle(bgcl , 0, 0, this.Width, this.Height);
            int h = this.Height - 6;
            Rectanglei v_lbox = new Rectanglei(2, 4, 44, h);
            g.SetClip(v_lbox);
            g.FillRectangle(Colorf.FromFloat(0.8f), v_lbox);
            g.Draw (this.m_Layer, true, v_lbox ,  enuFlipMode.None);
            g.ResetClip();

            g.DrawLine(Colorf.FromFloat(0.3f), 0, 0.0F, this.Width, 0.0f);
            g.DrawLine(Colorf.FromFloat (0.9f), 0, 1.0f, this.Width, 1.0f);
            //g.DrawLine(Colorf.FromFloat (0.3f), 0,1.0F , this.Width, 1.0f);

            g.DrawLine(Colorf.FromFloat(0.3f), 48, 4.0F, 48, this.Height - 4);
            g.DrawLine(Colorf.FromFloat(0.9f), 49, 4.0f, 49, this.Height - 4);

            this.c_textElement.Draw(g);
        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            var doc = this.m_Layer.ParentDocument;
            if (doc != null)
            {
                doc.CurrentLayer = this.m_Layer; 
            }
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
        }
      

    }
}
