using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D.Mecanism;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Imaging.Mecanism
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;

    public class ImageMecanismBase<T> : Core2DDrawingRectangleMecanismBase<T>
        where T : class, ICore2DDrawingLayeredElement
    {
        private ImageElement m_ImageElement;
        
        /// <summary>
        /// Get or set the image element to configure
        /// </summary>
        public ImageElement ImageElement
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                }
            }
        }
        protected Vector2i GetImageElementLocation(Vector2i vector)
        {
            if (this.ImageElement == null)
                return Vector2i.Zero;
            IGK.ICore.Matrix m = this.ImageElement.GetMatrix().Clone() as IGK.ICore.Matrix;
            Vector2i pt = Vector2i.Zero;

            if (m.IsInvertible && !m.IsIdentity)
            {
                Vector2f[] tps = new Vector2f[] { vector };
                m.Invert();
                var h = m.TransformPoints(tps);
                pt = new Vector2i((int)h[0].X, (int)h[0].Y);
            }
            else
            {
                pt = new Vector2i((int)vector.X, (int)vector.Y);
            }
            m.Dispose();
            return pt;
        }

        protected virtual bool CheckElement(CoreMouseEventArgs e)
        {
            //check first image element
            ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
            for (int i = l.Elements.Count - 1; i >= 0; --i)
            {
                if (l.Elements[i].Contains(e.FactorPoint) && (l.Elements[i] is ImageElement))
                {
                    this.ImageElement = l.Elements[i] as ImageElement;
                    //select the element in the layer
                    l.Select(this.ImageElement );
                    //toggle element to configure
                    return true;
                }
            }
            return false;
        }

    }
}
