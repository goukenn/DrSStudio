

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CloneElement.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CloneElement.cs
*/
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
using IGK.ICore.GraphicModels;
namespace IGK.DrSStudio.Drawing2D.CloneAddIn
{
    [Core2DDrawingStandardElement ("Clone", null, IsVisible = false )]
    public class CloneElement : Core2DDrawingLayeredElement 
    {
        ICore2DDrawingLayeredElement[] m_Targets;
        private Rectanglef  m_Bounds;
        public Rectanglef  Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (!m_Bounds.Equals (value))
                {
                    m_Bounds = value;
                }
            }
        }
        //public void Draw(ICoreGraphics g)
        //{
            //Matrix mat = GetMatrix();
            //GraphicsState s = g.Save();
            //this.SetGraphicsProperty(g);           
            //g.MultiplyTransform(mat, MatrixOrder.Prepend);
            //foreach (ICore2DDrawingLayeredElement l in this.m_Targets)
            //{
            //    if (l.View)
            //        l.Render(g);
            //}
            //g.Restore(s);
        //}
        public override bool Contains(Vector2f position)
        {
            IGK.ICore.Matrix m = this.GetMatrix().Clone () as IGK.ICore.Matrix ;
            if (m.IsInvertible)
            {
                m.Invert();
                Vector2f[] t = new Vector2f[] { position };
                    m.TransformPoints(t);
                    position = t[0];
                    m.Dispose();
            }
            foreach (ICore2DDrawingLayeredElement l in this.m_Targets)
            {
                if (l.Contains(position))
                    return true;
            }
            return false  ;
        }
        public static CloneElement Create(ICore2DDrawingLayeredElement[] targets)
        {
            if ((targets == null) || (targets.Length == 0))
                return null;
            CloneElement c = new CloneElement();
            c.m_Targets = targets;
            c.RegisterTargetEvent();
            c.InitElement();
            return c;
        }
        private void RegisterTargetEvent()
        {
            foreach (var item in this.m_Targets)
            {
                item.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(item_PropertyChanged);
            }
        }
        void item_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            switch (e.ID)
            {
                case enuPropertyChanged.Definition:
                case (enuPropertyChanged)enu2DPropertyChangedType .MatrixChanged :
                    InitBound();
                    break;
            }
        }
       
        private void InitBound()
        {
            this.m_Bounds = Rectanglef.Empty;
            bool i = false;
            foreach (var item in this.m_Targets)
            {
                if (!i)
                {
                    this.m_Bounds = item.GetBound();
                    i = true;
                }
                else this.m_Bounds = CoreMathOperation.GetBounds(this.m_Bounds, item.GetBound());
            }
        }
        public override enuBrushSupport BrushSupport
        {
            get {
                return enuBrushSupport.None;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        public override Rectanglef GetBound()
        {
            return CoreMathOperation.ApplyMatrix(this.m_Bounds, this.GetMatrix());
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            this.InitBound();
            path.Reset();
            path.AddRectangle(this.Bounds);
        }
    }
}

