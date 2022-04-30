

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImageLayer.cs
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
file:ImageLayer.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingObjectAttribute("BitmapLayer")]
    /// <summary>
    /// represent a image layer
    /// </summary>
    public sealed class ImageLayer : Core2DDrawingLayer 
    {
        Bitmap m_layerBitmap;
        ICore2DDrawingDocument m_cparent;
        protected override void OnParentChanged(EventArgs eventArgs)
        {
            base.OnParentChanged(eventArgs);
            if (this.Parent != null)
            {
                if (m_cparent != null)
                    m_cparent.PropertyChanged -= Parent_PropertyChanged;
                this.Parent.PropertyChanged += Parent_PropertyChanged;
                m_cparent = this.Parent ;
                _UpdateBitmapSize();
            }
        }
        void Parent_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if ((enu2DPropertyChangedType)e.ID == enu2DPropertyChangedType.SizeChanged)
            {
                _UpdateBitmapSize();
            }
        }
        private void _UpdateBitmapSize()
        {
            if (this.Parent == null)
                return;
            Bitmap v_bitmap = new Bitmap(this.Parent.Width, this.Parent.Height);
            Graphics g = Graphics.FromImage(v_bitmap);
            if (m_layerBitmap != null)
            {
                g.DrawImage(v_bitmap, Point.Empty);
                m_layerBitmap.Dispose();
            }
            g.Dispose();
            m_layerBitmap = v_bitmap;
        }
        public void Append(Core2DDrawingLayeredElement  element)
        {
            ///draw the element to layers
            if (this.m_layerBitmap != null)
            {
                if (this.Elements.Contains(element))
                {
                    using (Graphics g = Graphics.FromImage(this.m_layerBitmap))
                    {
                        element.Draw(g);
                    }
                    this.Elements.Remove(element);
                }                
            }
        }
        public override void Draw(Graphics graphic)
        {
            GraphicsState st = graphic.Save();
            if (this.IsClipped)
            {
                graphic.Clip = this.GetClippedRegion();
            }
            if ((this.m_layerBitmap != null)&&(this.Parent!=null))
            {
                Rectangle rc = new Rectangle(0, 0, this.Parent.Width, this.Parent.Height);
                graphic.DrawImage(this.m_layerBitmap,rc,rc.X,rc.Y,rc.Width ,rc.Height ,
                    GraphicsUnit.Pixel ,
                    GetImageAttributes());
            }
            //draw overlay element
            foreach (ICore2DDrawingLayeredElement item in this.Elements)
            {
                if (item.View)
                {
                    item.Render(graphic);
                }
            }
            graphic.Restore(st);
        }
        /// <summary>
        /// create a element collection
        /// </summary>
        /// <returns></returns>
        protected override Core2DDrawingLayer.ElementCollections CreateElementCollections()
        {
            return new ImageLayerElementCollection(this);
        }
        /// <summary>
        /// represent a pixel image layer collection
        /// </summary>
        class ImageLayerElementCollection : Core2DDrawingLayer.ElementCollections
        {
            public ImageLayerElementCollection(ImageLayer layer):base(layer)
            {
            }
        }
    }
}

