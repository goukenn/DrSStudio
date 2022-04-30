

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GridElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GridElement.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    [Core2DDrawingStandardElement("GridElement", typeof(Mecanism))]
    public class GridElement : RectangleElement 
    {
        private int m_GridX;
        private int m_GridY;

        public override bool CanRenderShadow
        {
            get
            {
                return base.CanRenderShadow;
            }
        }
        [CoreXMLAttribute (),
        CoreXMLDefaultAttributeValue (1)]
        public int GridY
        {
            get { return m_GridY; }
            set
            {
                if (m_GridY != value)
                {
                    m_GridY = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(1)]
        public int  GridX
        {
            get { return m_GridX; }
            set
            {
                if (m_GridX != value)
                {
                    m_GridX = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public GridElement()
        {
            this.m_GridX = 1;
            this.m_GridY = 1;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters.AddGroup("Definition");
            g.AddItem(this.GetType().GetProperty("GridX"), "lb.x.Caption");
            g.AddItem(this.GetType().GetProperty("GridY"), "lb.y.Caption");
            return parameters;
        }
       protected override void InitGraphicPath(CoreGraphicsPath p)
{


    p.Reset();
            //p.SetMarkers();
            p.AddRectangle(this.Bounds);
            p.CloseFigure();
            GenRectX(p, this.Bounds, this.GridX);
            GenRectY(p, this.Bounds, this.GridY);
            
        }
       private void GenRectY(CoreGraphicsPath path, Rectanglef rc, int p)
        {
            if (p <= 0)
                return;
#pragma warning disable IDE0054 // Use compound assignment
            p = p - 1;
#pragma warning restore IDE0054 // Use compound assignment
            float h = rc.Height / 2.0f;
            if (h <= 1.0f)
                return;
            //path.SetMarkers();
            path.AddLine(rc.X, rc.Y + h, rc.Right, rc.Y + h);
            path.CloseFigure();
            GenRectY(path, new Rectanglef(rc.X, rc.Y, rc.Width, h), p);
            GenRectY(path, new Rectanglef(rc.X, rc.Y + h, rc.Width, h), p);
        }
       private void GenRectX(CoreGraphicsPath path, Rectanglef rc, int p)
        {
            if (p <= 0)
                return;
#pragma warning disable IDE0054 // Use compound assignment
            p = p - 1;
#pragma warning restore IDE0054 // Use compound assignment
            float w = rc.Width / 2;
            if (w <= 1.0f)
                return;
            //path.SetMarkers();
            path.AddLine(rc.X + w, rc.Y, rc.X + w, rc.Bottom);
            path.CloseFigure();
            GenRectX(path, new Rectanglef(rc.X, rc.Y, w, rc.Height), p);
            GenRectX(path, new Rectanglef(rc.X + w, rc.Y, w, rc.Height), p);
        }
        protected new class Mecanism : RectangleElement.Mecanism
        { }
    }
}

