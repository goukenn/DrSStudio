

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GdiBlendingRealBrushElement.cs
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
file:GdiBlendingRealBrushElement.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingImageBrushItemAttribute("GdiBlendRealBrushMarker", typeof(Mecanism))]
    class GdiBlendingRealBrushElement : GdiBlendingBrushElement
    {
        private enuGdiBlendingBrushOperation m_BlendingOperation;
        private bool m_Additive;
        public GdiBlendingRealBrushElement()
        {
            this.m_Additive = false;
            this.m_BlendingOperation = enuGdiBlendingBrushOperation.None;
        }
        /// <summary>
        /// get or set if this element is additive
        /// </summary>
        public bool Additive
        {
            get { return m_Additive; }
            set
            {
                if (m_Additive != value)
                {
                    m_Additive = value;
                }
            }
        }
        /// <summary>
        /// get or set the blending mode
        /// </summary>
        public enuGdiBlendingBrushOperation BlendingOperation{
        get{ return m_BlendingOperation;}
        set{ 
        if (m_BlendingOperation !=value)
        {
        m_BlendingOperation =value;
            OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
        }
        }
        } 
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
 	         DrSStudio.WinUI.ICoreParameterConfigCollections p =  base.GetParameters(parameters);
            Type t = this.GetType ();
                     var group = p.AddGroup("MarkerDefinition");
            group.AddItem(t.GetProperty("BlendingOperation"));
            group.AddItem(t.GetProperty("Additive"),"gdibrush_additive_caption");
            return p;
        }
        new class Mecanism : GdiBlendingBrushElement.Mecanism
        {
            CoreBitmapData sourceData = null;
            new GdiBlendingRealBrushElement MarkerElement{
                get{
                    return base.MarkerElement as GdiBlendingRealBrushElement;
                }
            }
            public Mecanism():base()
            {
            }
            protected override void OnImageElementChanged(EventArgs eventArgs)
            {
                sourceData = null;//free old resources
                sourceData = CoreBitmapData.FromBitmap(this.SelectedImageElement.Bitmap);
            }
            protected override GdiBlendingBrushElement CreateMarker()
            {
                return new GdiBlendingRealBrushElement();
            }
            protected override void ApplyBrush(Vector2f EndPoint)
            {
                if (sourceData == null)
                {
                    sourceData = CoreBitmapData.FromBitmap(this.SelectedImageElement.Bitmap);
                }
                Vector2f v_c = EndPoint;
                Matrix m = this.SelectedImageElement.GetMatrix().Clone();
                if (!m.IsIdentity && m.IsInvertible)
                {
                    m.Invert();
                    Vector2f[] t = new Vector2f[] { this.EndPoint };
                    m.TransformPoints(t);
                    v_c = t[0];
                }
                GdiBlendingRealBrushElement v_marker = this.MarkerElement ;
                using (Bitmap offBitmap = new Bitmap(Size, Size))
                {
                    Graphics v_g = Graphics.FromImage(offBitmap);
                    Bitmap v_bmp = this.SelectedImageElement.Bitmap;
                    //destination graphics
                    using (Graphics g = Graphics.FromImage(v_bmp))
                    {
                        Rectanglei v_r = new Rectanglei(Point.Round (new Vector2f(v_c.X - (Size / 2),
                                v_c.Y - (Size / 2))),
                                new Size2i(Size, Size));
                        ImageAttributes attr = new ImageAttributes();
                        //this.m_markerElement.Opacity
                        attr.SetColorMatrix(new ColorMatrix(
                            new float[][]{
                                new float []{1,0,0,0,0},
                                new float []{0,1,0,0,0},
                                new float []{0,0,1,0,0},
                                new float []{0,0,0,1,0},
                                new float []{0,0,0,0,1}
                            }
                            ));
                        GraphicsPath p = new GraphicsPath();
                        v_g.Clear(Color.Transparent);
                        p.AddEllipse(new Rectangle(Point.Empty, offBitmap.Size));
                        v_g.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.None;
                        v_g.FillPath(this.MarkerElement.FillBrush.GetBrush(), p);// new Rectangle(Point.Empty, bmp.Size));
                        v_g.Flush();
                        CoreBitmapData destData = CoreBitmapData.FromBitmap(offBitmap);
                        int startx = v_r.X ;
                        int starty = v_r.Y;
                        long offset_s = 0; 
                        long offset_d = 0;
                        long pd = 0;
                        long ps = 0;
                        float opacity = this.MarkerElement.Opacity;
                        //blending operation
                        if (v_marker.BlendingOperation != enuGdiBlendingBrushOperation.None)
                        {
                            for (int i = 0; i < destData.Height; i++)
                            {
                                offset_d = i * destData.Stride;
                                offset_s = (i + starty) * sourceData.Stride + (startx * 4);
                                for (int w = 0; w < destData.Width; w++)
                                {
                                    pd = offset_d + (w * 4);
                                    ps = offset_s + (w * 4);
                                    if ((ps >= 0) && (ps < ((sourceData.Height * sourceData.Stride) - 4)))
                                    {
                                        //COLOR : ARGB 
                                        switch (v_marker.BlendingOperation)
                                        {
                                            case enuGdiBlendingBrushOperation.SubDest:
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps]) - (destData.Data[pd] * opacity)));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 1]) - (destData.Data[pd + 1] * opacity)));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 2]) - (destData.Data[pd + 2] * opacity)));
                                                destData.Data[pd + 3] =  CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 3]) - (destData.Data[pd + 3] * opacity)));
                                                break;
                                            case enuGdiBlendingBrushOperation.Add:
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps]) + (destData.Data[pd] * opacity)));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 1]) + (destData.Data[pd + 1] * opacity)));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 2]) + (destData.Data[pd + 2] * opacity)));
                                                destData.Data[pd + 3] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 3]) - (destData.Data[pd + 3] * opacity)));
                                                break;
                                            case enuGdiBlendingBrushOperation.SubSource:
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte((int)((-sourceData.Data[ps]) + (destData.Data[pd] * opacity)));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte((int)((-sourceData.Data[ps + 1]) + (destData.Data[pd + 1] * opacity)));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte((int)((-sourceData.Data[ps + 2]) + (destData.Data[pd + 2] * opacity)));
                                                destData.Data[pd + 3] =  CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 3]) - (destData.Data[pd + 3] * opacity)));
                                                break;
                                            case enuGdiBlendingBrushOperation.LightOnly :
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte(Math.Max(sourceData.Data[ps], destData.Data[pd]));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte(Math.Max(sourceData.Data[ps+1], destData.Data[pd+1]));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte(Math.Max(sourceData.Data[ps+2], destData.Data[pd+2]));
                                                destData.Data[pd + 3] = CoreBitmapOperation.TrimByte(Math.Max(sourceData.Data[ps + 3], destData.Data[pd + 3]));
                                                break;
                                            case  enuGdiBlendingBrushOperation.DarkOnly :
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte(Math.Min(sourceData.Data[ps], destData.Data[pd]));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte(Math.Min(sourceData.Data[ps+1], destData.Data[pd+1]));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte(Math.Min(sourceData.Data[ps+2], destData.Data[pd+2]));
                                                destData.Data[pd + 3] = CoreBitmapOperation.TrimByte(Math.Min(sourceData.Data[ps + 3], destData.Data[pd + 3]));
                                                break;
                                            case enuGdiBlendingBrushOperation.Difference:
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte((int)Math.Abs ( (sourceData.Data[ps]) - (destData.Data[pd] * opacity)));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte((int)Math.Abs ((-sourceData.Data[ps + 1]) - (destData.Data[pd + 1] * opacity)));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte((int)Math.Abs((-sourceData.Data[ps + 2]) - (destData.Data[pd + 2] * opacity)));
                                                destData.Data[pd + 3] = CoreBitmapOperation.TrimByte((int)((sourceData.Data[ps + 3]) - (destData.Data[pd + 3] * opacity)));
                                                break;
                                            case enuGdiBlendingBrushOperation.Multiply :
                                                destData.Data[pd] = CoreBitmapOperation.TrimByte((int)(sourceData.Data[ps] * destData.Data[pd] * opacity));
                                                destData.Data[pd + 1] = CoreBitmapOperation.TrimByte((int)(sourceData.Data[ps + 1] * destData.Data[pd + 1] * opacity));
                                                destData.Data[pd + 2] = CoreBitmapOperation.TrimByte((int)(sourceData.Data[ps+2]* destData.Data[pd+2] *opacity ));
                                                destData.Data[pd + 3] = CoreBitmapOperation.TrimByte((int)(sourceData.Data[ps + 3] * destData.Data[pd + 3] * opacity));
                                                break;
                                        }
                                        //sourceData.Data[ps] = CoreBitmapOperation.TrimByte(sourceData.Data[ps] + (int)(opacity * destData.Data[pd] * opacity));
                                        //sourceData.Data[ps +1] = CoreBitmapOperation.TrimByte(sourceData.Data[ps + 1] + (int)(opacity * destData.Data[pd + 1] * opacity));
                                        //sourceData.Data[ps+2] = CoreBitmapOperation.TrimByte(sourceData.Data[ps + 2] + (int)(opacity * destData.Data[pd + 2] * opacity));
                                        //sourceData.Data[ps+3] = CoreBitmapOperation.TrimByte(sourceData.Data[ps + 3] + (int)(opacity * destData.Data[pd + 3] * opacity));
                                    }
                                }
                            }
                        }
                        g.CompositingMode = System.Drawing.Drawing2D.CompositingMode.SourceOver;
                        g.SmoothingMode = global::System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                        g.CompositingQuality = this.MarkerElement.DestQuality;
                        g.InterpolationMode = this.MarkerElement.DestInterpolation;
                        p.Reset();
                        p.AddEllipse(v_r);
                        Region rg = new System.Drawing.Region(p);
                        g.CompositingMode = this.MarkerElement.CompositingMode;
                        g.Clip = (rg);
                        g.DrawImage(
                          destData.ToBitmap(),
                         v_r,
                         0, 0, offBitmap.Width, offBitmap.Height,
                          GraphicsUnit.Pixel,
                          attr
                          );
                        //v_r = new Rectanglei(0, 0, sourceData.Width, sourceData.Height);
                        //g.DrawImage(
                        // sourceData.ToBitmap(),
                        //v_r,
                        //0, 0, v_r.Width, v_r.Height,
                        // GraphicsUnit.Pixel,
                        // attr
                        // );
                    }
                }
                if (this.MarkerElement.Additive)
                {
                    this.sourceData = CoreBitmapData.FromBitmap(this.SelectedImageElement.Bitmap);
                }
                this.m_hasChanged = true;
            }
        }
    }
}

