

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreBrushRegister.cs
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
file:WinCoreBrushRegister.cs
*/
using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinCore
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;

    /// <summary>
    /// Represent a brush register
    /// </summary>
    public class WinCoreBrushRegister : ICoreBrushRegister
    {
        private Dictionary<Colorf, SolidBrush> m_Sbrush;
        private Dictionary<Colorf, Pen> m_SPens;
        private Dictionary<ICoreBitmap, TextureBrush> m_bitmapBrush;
        private Dictionary<Image, TextureBrush> m_STextureBrush;
        private Dictionary<ICore2DDrawingDocument, TextureBrush> m_DocumentTextureBrush;
        private Dictionary<CoreBrushRegisterManager.HatchBrushStruct, HatchBrush> m_rSystemHatchBrush;
        private Dictionary<CoreBrushRegisterManager.DualLinearBrushInfoStruct, LinearGradientBrush> m_rLinearBrush;
        private Dictionary<ICoreBrush, IDisposable> m_brushes;


        public WinCoreBrushRegister()
        {
            m_Sbrush = new Dictionary<Colorf, SolidBrush>();
            m_SPens = new Dictionary<Colorf, Pen>();
            m_rSystemHatchBrush = new Dictionary<CoreBrushRegisterManager.HatchBrushStruct, HatchBrush>();
            m_STextureBrush = new Dictionary<Image, TextureBrush>();
            m_rLinearBrush = new Dictionary<CoreBrushRegisterManager.DualLinearBrushInfoStruct, LinearGradientBrush>();
            m_brushes = new Dictionary<ICoreBrush, IDisposable >();
            m_bitmapBrush = new Dictionary<ICoreBitmap, TextureBrush>();
            m_DocumentTextureBrush = new Dictionary<ICore2DDrawingDocument, TextureBrush>();
        }
        private void FreeResources()
        {
            foreach (IDisposable item in m_Sbrush.Values)
            {
                item.Dispose();
            }
            m_Sbrush.Clear();
            foreach (IDisposable item in m_STextureBrush.Values)
            {
                item.Dispose();
            }
            m_STextureBrush.Clear();
            foreach (IDisposable item in m_rLinearBrush.Values)
            {
                item.Dispose();
            }
            m_rLinearBrush.Clear();
            foreach (IDisposable item in m_bitmapBrush.Values )
            {
                item.Dispose();
            }
            m_bitmapBrush.Clear();


            foreach (IDisposable item in m_DocumentTextureBrush.Values )
            {
                item.Dispose();
            }
            m_DocumentTextureBrush.Clear();
        }
        public void Dispose()
        {
            FreeResources();
        }
        public static Brush GetBrush(Color cl)
        {
            return GetBrush(cl.CoreConvertFrom<Colorf>());
        }

        public T GetBrush<T>(ICore2DDrawingDocument document) where T : class, IDisposable
        {
            return this.GetTextureBrush(document) as T;
        }
        public T GetBrush<T>(Colorf color) where T : class , IDisposable 
        {
            if (m_Sbrush.ContainsKey(color))
            {
                return m_Sbrush[color] as T;
            }
            else
            {
                SolidBrush v_br = new SolidBrush(color.CoreConvertTo<Color>());
                m_Sbrush.Add(color, v_br);
                return v_br as T;
            }
        }
        public T GetBrush<T>(ICoreBitmap bitmapBrush) where T : class , IDisposable
        {
            if (bitmapBrush == null)
                return default(T);
            if (m_bitmapBrush.ContainsKey(bitmapBrush))
            {
                return m_bitmapBrush[bitmapBrush] as T;
            }
            else
            {
                TextureBrush v_br = new TextureBrush(bitmapBrush.ToGdiBitmap());
                m_bitmapBrush.Add(bitmapBrush, v_br);
                return v_br as T;
            }
        }
        public T GetPen<T>(Colorf color) where T : class , IDisposable 
        {
            if (m_SPens.ContainsKey(color))
            {
                return m_SPens[color] as T;
            }
            else
            {
                Pen v_br = new Pen(color.CoreConvertTo<Color>(), 1.0f);
                m_SPens.Add(color, v_br);
                return v_br as T;
            }
        }
        public T GetBrush<T>(enuHatchStyle style, Colorf cl1, Colorf cl2) where T : class , IDisposable 
        {
            CoreBrushRegisterManager.HatchBrushStruct h = new CoreBrushRegisterManager.HatchBrushStruct(style, cl1, cl2);
            HatchBrush br = null;
            if (!m_rSystemHatchBrush.ContainsKey(h))
            {
                br = new HatchBrush((HatchStyle)(int)style, cl1.CoreConvertTo<Color>(),
                    cl2.CoreConvertTo <Color>());
                m_rSystemHatchBrush.Add(h, br);
                return br as T;
            }
            return m_rSystemHatchBrush[h] as T;
        }

        public static Brush GetBrush(enuHatchStyle style, Colorf cl1, Colorf cl2) {

            if (CoreApplicationManager.Application != null)
            {
                var e = CoreApplicationManager.Application.BrushRegister as WinCoreBrushRegister ;
                if (e != null)
                    return e.GetBrush<Brush>(style, cl1, cl2);
            }
            return null;
        }
        public static void SetStyle(ICoreLineStyle style, Pen p)
        {
            if (style.Style != enuDashStyle.Custom)
            {
                p.DashStyle = (DashStyle)(int)style.Style;
            }
            else
            {
                p.DashStyle = DashStyle.Custom;
                try
                {
                    p.DashPattern = style.GetUnit();
                    p.DashOffset = style.DashOffset;
                }
                catch
                {
                    CoreLog.WriteDebug("Can't set unit pixel format");
                    p.DashStyle = DashStyle.Solid;
                }
            }
        }
        public void Unregister(ICoreBrush coreBrush)
        {
            lock (this.m_brushes)
            {
                if (this.m_brushes.ContainsKey(coreBrush))
                {
                    IDisposable c = this.m_brushes[coreBrush];
                    if (c != null)
                    {
                        c.Dispose();
                    }
                    coreBrush.BrushDefinitionChanged -= _BrushDefinitionChanged;
                    this.m_brushes.Remove(coreBrush);
                }
            }
        }
        public void Register(ICoreBrush coreBrush)
        {
            if ((coreBrush == null) || this.m_brushes.ContainsKey(coreBrush))
                return;
            IDisposable s = GenerateBrush(coreBrush);
            coreBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
            this.m_brushes.Add(coreBrush, s);
        }
        private void _BrushDefinitionChanged(object sender, EventArgs e)
        {
            ICoreBrush br = sender as ICoreBrush;
            
            this.Reload(br);
        }
        private IDisposable GenerateBrush(ICoreBrush br)
        {
            return MethodInfo.GetCurrentMethod().Visit(this, br) as IDisposable ;
        }
        private IDisposable GenerateBrush(CorePen coreBrush)
        {
            Brush br = GenerateBrush(coreBrush as CoreBrush) as Brush;
            if (br != null)
            {
                Pen p = new Pen(br);
                SetPenProperty(coreBrush, p);
                br.Dispose();
                return p;
            }
            return null;
        }
        private static void SetPenProperty(CorePen cPen, Pen pen)
        {
            if (pen == null)
                return;

            pen.Width = cPen.Width;
            pen.DashCap =(DashCap ) cPen.DashCap.WinCoreGetValue();
            if (cPen.EndCap != null)
            {
                if (cPen.EndCap.LineCap == enuLineCap.Custom)
                {
                    pen.CustomEndCap = GetCustomLineCap(cPen.EndCap);
                }
                else
                    pen.EndCap =(LineCap )cPen.EndCap.LineCap.WinCoreGetValue();
            }
            if (cPen.StartCap != null)
            {
                if (pen.StartCap == LineCap.Custom)
                {
                    pen.CustomStartCap = GetCustomLineCap(cPen.StartCap);
                }
                else
                    pen.StartCap = (LineCap)cPen.StartCap.LineCap.WinCoreGetValue();
            }
            pen.MiterLimit = cPen.MiterLimit;
            pen.LineJoin =(LineJoin ) cPen.LineJoin.WinCoreGetValue();
            if (cPen.DashStyle != null) SetStyle ( cPen.DashStyle, pen);
            pen.Alignment = (PenAlignment)cPen.Alignment;

            Matrix m = null;
            if ((cPen.Owner != null) && ((m = cPen.Owner.GetMatrix()) != null))
            {
                System.Drawing.Drawing2D.Matrix v_m = m.ToGdiMatrix();
                try
                {
                    if (v_m != null)
                    {
                        pen.Transform = v_m;
                    }
                }
                catch { 

                }
            }
        }
        private static CustomLineCap GetCustomLineCap(ICoreLineCap coreLineCap)
        {
            if (coreLineCap.PathElement  ==null)
                return null;
            CoreGraphicsPath v_path = coreLineCap.PathElement.Clone () as CoreGraphicsPath ;
            Rectanglef rc = v_path.GetBounds();
            Vector2f  d = CoreMathOperation.GetCenter(rc);
            Matrix m = new Matrix ();
            m.Translate (-d.X , -d.Y , enuMatrixOrder.Append );
            v_path.Transform(m);
            m.Dispose();
            System.Drawing.Drawing2D.CustomLineCap v_lcap = null;
            GraphicsPath c = v_path.WinCoreToGdiGraphicsPath();
            v_lcap = new System.Drawing.Drawing2D.CustomLineCap(null, c, 
                (LineCap ) coreLineCap.CustomCap, coreLineCap.BaseInset);
            c.Dispose();
            v_lcap.WidthScale = coreLineCap.WidthScale;
            return v_lcap;
        }
        private IDisposable GenerateBrush(CoreBrush coreBrush)
        {
            if (coreBrush == null) return null;
            IDisposable m_Brush = null;
            //if (this.BrushType != enuBrushType.Texture)
            //    this.m_textureRes = null;
            switch (coreBrush.BrushType)
            {
                case enuBrushType.Solid:
                    m_Brush = new SolidBrush(coreBrush.Colors[0].CoreConvertTo<Color>());
                    break;
                case enuBrushType.Hatch:
                    m_Brush = new HatchBrush(
                       (HatchStyle )coreBrush.HatchStyle ,
                       coreBrush.Colors[0].CoreConvertTo<Color>(), coreBrush.Colors[1].CoreConvertTo<Color>());
                    break;
                case enuBrushType.LinearGradient:
                    m_Brush = GenerateLinearBrush(coreBrush);
                    break;
                case enuBrushType.PathGradient:
                    m_Brush = GeneratePathBrush(coreBrush);
                    break;
                case enuBrushType.Texture:
                    //if (this.m_textureRes != null)
                    //    this.m_Bitmap = this.m_textureRes.GetBitmap();
                    m_Brush = GenerateTextureBrush(coreBrush);
                    break;
                default:
                    break;
            }
            return m_Brush ;
        }
        /// <summary>
        /// generate a texture brush
        /// </summary>
        private static TextureBrush  GenerateTextureBrush(ICoreBrush br)
        {
            if ((br.Bitmap  == null) || (br.Bitmap.PixelFormat == enuPixelFormat.Undefined))
                return null;
            Rectanglei v_rc = new Rectanglei(0, 0, 1, 1);
            Matrix v_m = null;
            CoreGraphicsPath v_p = null;
            Matrix v_m1 = null;
            if (br.Owner != null)
            {
                v_m = br.Owner.GetMatrix();
                v_p = br.Owner.GetPath();
                v_rc = Rectanglef.Round(v_p.GetBounds());//Rectanglef.Round(br.Owner.GetBound());
                if (v_p != null)
                    v_p = v_p.Clone() as CoreGraphicsPath;
            }
            if (br.AutoSize)
            {
                if (v_m != null)//&& (br.PathBrushMode != enuPathBrushMode.Path ))
                {
                    v_m1 = v_m.Clone() as Matrix;
                    if (v_p != null)
                    {
                        if ((!v_m1.IsIdentity) && (v_m1.IsInvertible))
                        {
                            v_m1.Invert();
                            v_p.Transform(v_m1);
                            v_rc = Rectanglef.Round(v_p.GetBounds());
                        }
                        else
                        {
                            v_rc = Rectanglef.Round(v_p.GetBounds());
                        }
                    }
                }
            }
            else
            {
                v_rc = Rectanglef.Round(br.Bounds);
            }
            v_rc.Width = Math.Max(1, v_rc.Width);
            v_rc.Height = Math.Max(1, v_rc.Height);
            try
            {
                Bitmap bmp = br.Bitmap.ToGdiBitmap();
                TextureBrush txb = new TextureBrush(bmp);
                Matrix v_tm = new Matrix();
                float dx = v_rc.X;
                float dy = v_rc.Y;
                float zoomx = 1.0f;
                float zoomy = 1.0f;
                zoomx = v_rc.Width / (float)br.Bitmap.Width;
                zoomy = v_rc.Height / (float)br.Bitmap.Height;
                v_tm.Scale(zoomx, zoomy, enuMatrixOrder.Append);
                v_tm.Translate(dx, dy, enuMatrixOrder.Append);
                if (br.AutoSize )
                     v_tm.Multiply(v_m, enuMatrixOrder.Append  );
                txb.Transform = v_tm.CoreConvertTo<System.Drawing.Drawing2D.Matrix >();//.MultiplyTransform(v_tm, enuMatrixOrder.Prepend );
                txb.WrapMode =(WrapMode ) br.WrapMode.WinCoreGetValue();
                return txb;   
            }
            catch
            {
                CoreLog.WriteLine("Error TextureBrush ");
            }
            return null;
        }

        class WinCorePathBrushBuilder : ICoreBrushBuilder
        {
           

            public void Setup(ICoreBrush brush, params object[] param)
            {
                PathGradientBrush b = param[0] as PathGradientBrush;
                if (b != null)
                {
                    SetUpBrush(b, brush, (int)param[1]);
                }
            }
        }
        private static PathGradientBrush  GeneratePathBrush(ICoreBrush br)
        {
            var v_pathBrush = new WinCorePathBrushBuilder();
            if (br is ICorePathBrushGenerator)
            {
                return (br as ICorePathBrushGenerator).Generate(v_pathBrush) as PathGradientBrush;
            }

            GraphicsPath v_p = null;
            PathGradientBrush v_ln = null;
            Rectanglef v_rc = Rectanglef.Empty;
            if (br.Owner != null)
            {
                var vp = br.Owner.GetPath();
                v_p = vp.WinCoreToGdiGraphicsPath();
                if (vp == null)
                {
                    v_p = new GraphicsPath();
                    v_rc = new Rectanglef(0, 0, 1, 1);
                    v_p.AddRectangle(new RectangleF(v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height));
                    
                }
                else
                {
                    //get bound that englobe the graphics path. and protect the style
                    v_rc = (vp != null) ? vp.GetBounds() : new Rectanglef(0, 0, 1, 1);
                }
            }
            else { 
                v_rc =   br.Bounds;
                v_p = new GraphicsPath();
                v_p.AddRectangle(new RectangleF(v_rc.X, v_rc.Y , Math.Max(1.0f, v_rc.Width), Math.Max (1.0f, v_rc.Height)));
            }
            if (!br.AutoSize)    
            {
                v_rc = br.Bounds;
                //scale the path to fit bound
                if (!v_rc.IsEmpty && (v_p != null))
                {
                    System.Drawing.Drawing2D.Matrix v_LM = new System.Drawing.Drawing2D.Matrix();
                    RectangleF v_prc = v_p.GetBounds();
                    v_LM.Scale(v_rc.Width / v_prc.Width, v_rc.Height / v_prc.Height, MatrixOrder.Append);
                    v_LM.Translate(v_rc.X, v_rc.Y, MatrixOrder.Append);
                    v_p.Transform(v_LM);
                    v_LM.Dispose();
                }
            }
            if (v_rc.IsEmpty)
            {
                if (v_p != null)
                    v_p.Dispose();
                return null;
            }
          
            switch (br.PathBrushMode)
            {
                case enuPathBrushMode.Path:
                    //generate previous motif
                    if (v_p != null)
                        v_ln = new PathGradientBrush(v_p);                   
                    break;
                case enuPathBrushMode.Rectangle:
                    v_ln = new PathGradientBrush(CoreMathOperation.GetPoints(v_rc).CoreConvertTo<PointF[]>());
                    //v_ln.MultiplyTransform(v_m, MatrixOrder.Append);
                    break;
                case enuPathBrushMode.InnerEllipse:
                    {
                        GraphicsPath vp = new GraphicsPath();
                        vp.AddEllipse(
                            CoreMathOperation.GetInternalCircle(v_rc).CoreConvertTo<RectangleF>());
                        v_ln = new PathGradientBrush(vp);
                        vp.Dispose();
                    }
                    break;
                case enuPathBrushMode.OuterEllipse:
                    {
                        GraphicsPath vp = new GraphicsPath();
                        vp.AddEllipse(
                            CoreMathOperation.GetOuterEllipseRectangle(v_rc).CoreConvertTo<RectangleF>());
                        v_ln = new PathGradientBrush(vp);
                        vp.Dispose();
                    }
                    break;
                case enuPathBrushMode.CustomRectangle:
                    v_ln =
                    new PathGradientBrush(
                        CoreMathOperation.GetPoints(v_rc).CoreConvertTo<PointF[]>());
                    break;
                default:
                    break;
            }
            v_pathBrush.Setup(br, v_ln, v_p.PointCount);
                 
            return v_ln;
        }

        private static void SetUpBrush(PathGradientBrush v_ln, ICoreBrush br, int PointCount)
        {
            WrapMode wp = (WrapMode)(int)br.WrapMode;
           // int PointCount = v_p.PointCount;
            //v_p.Dispose();
            v_ln.WrapMode = wp;
            switch (br.LinearMode)
            {
                case enuLinearMode.Dual:
                    v_ln.CenterColor = br.Colors[0].CoreConvertTo<Color>();
                    v_ln.SurroundColors = new Color[] { br.Colors[1].CoreConvertTo<Color>() };
                    break;
                case enuLinearMode.DualBlend:
                    {
                        v_ln.CenterColor = br.Colors[0].CoreConvertTo<Color>();
                        Color[] tb = new Color[br.Colors.Length];
                        v_ln.SurroundColors = tb;
                        Blend bl = new Blend();
                        bl.Factors = br.Factors;
                        bl.Positions = br.Positions;
                        v_ln.Blend = bl;
                    }
                    break;
                case enuLinearMode.MultiColor:
                    {
                        v_ln.CenterColor = br.Colors[0].CoreConvertTo<Color>();
                        Color[] tb = br.Colors.WinCoreArraySub(1).CoreConvertTo<Color[]>();
                        if (br.OneColorPerVertex && (tb.Length <= PointCount))
                        {
                            try
                            {
                                v_ln.SurroundColors = tb;
                            }
                            catch (Exception Exception)
                            {
                                CoreLog.WriteDebug(Exception.Message);
                            }
                        }
                        else
                        {
                            ColorBlend clBlend = new ColorBlend();
                            float[] f = br.Positions.Clone() as float[];
                            Array.Reverse(tb);
                            for (int i = 0; i < f.Length; i++)
                            {//complement for reversing
                                f[i] = 1.0f - f[i];
                            }
                            Array.Reverse(f);
                            //f[0] = 0.0f;
                            //f[f.Length -1] = 1.0f;
                            clBlend.Colors = tb;
                            clBlend.Positions = f;
                            v_ln.InterpolationColors = clBlend;
                        }
                    }
                    break;
                default:
                    break;
            }
            switch (br.LinearOperator)
            {
                case enuLinearOperator.None:
                    break;
                case enuLinearOperator.SetSigmaBellShape:
                    v_ln.SetSigmaBellShape(br.Focus, br.Scale);
                    break;
                case enuLinearOperator.SetTriangularBellShape:
                    v_ln.SetBlendTriangularShape(br.Focus, br.Scale);
                    break;
                default:
                    break;
            }
            v_ln.FocusScales = br.PathFocusScale.CoreConvertTo<PointF>();
            if (!br.AutoCenter)
                v_ln.CenterPoint = br.PathCenter.CoreConvertTo<PointF>();
        }

        private static LinearGradientBrush  GenerateLinearBrush(ICoreBrush br)
        {
            LinearGradientBrush v_ln = null;
            Rectanglef v_rc = new Rectanglef(0, 0, 1, 1);
            Matrix v_m = null;
            CoreGraphicsPath v_p = null;
            if (br.Owner !=null)
            {

                if (br.AutoSize)
                {
                   CoreGraphicsPath cp=  br.Owner.GetPath();
                   v_rc = cp != null ? cp.GetBounds() : new Rectanglef(0, 0, 1, 1) ;
                }
                else
                {
                    v_rc = Rectanglef.Round(
             new Rectanglef(br.Bounds.X,
             br.Bounds.Y,
             Math.Max(1.0f, br.Bounds.Width),
             Math.Max(1.0f, br.Bounds.Height)));
                }
            }
            else
            {
                if (br.AutoSize)
                {
                    if (br.Owner != null)
                    {
                        v_m = br.Owner.GetMatrix();
                        v_p = br.Owner.GetPath();
                        if (v_p != null)
                        {
                            v_p = v_p.Clone() as CoreGraphicsPath;
                            v_rc = Rectanglei.Round((Rectanglef)v_p.GetBounds());
                        }
                        else
                        {
                            v_rc = Rectanglei.Round(v_p.GetBounds());
                        }
                    }
                }
                else
                {
                    v_rc = Rectanglef.Round(
                        new Rectanglef(br.Bounds.X,
                        br.Bounds.Y,
                        Math.Max(1.0f, br.Bounds.Width),
                        Math.Max(1.0f, br.Bounds.Height)));
                }
            }
            //setup line config
            if (br is ICorePen)
            {
                ICorePen v_pen = br as ICorePen;
                if (v_pen.Alignment == enuPenAlignment.Center)
                {
                    v_rc.Inflate(v_pen.Width, v_pen.Width);
                }
            }
            v_rc.Width = (int)Math.Max(1.0f, v_rc.Width);
            v_rc.Height = (int)Math.Max(1.0f, v_rc.Height);
            //form orignal bound
            v_ln = WinCoreBrushRegister.CreateBrush<LinearGradientBrush>(
v_rc,
br.Colors[0],
br.Colors[1],
br.Angle);
            switch (br.LinearMode)
            {
                case enuLinearMode.Dual:
                    break;
                case enuLinearMode.DualBlend:
                    Blend bl = new Blend();
                    bl.Factors = br.Factors;
                    bl.Positions = br.Positions;
                    v_ln.Blend = bl;
                    v_ln.LinearColors = new Color[]{
                        br.Colors[0].CoreConvertTo<Color>(),
                        br.Colors[br.Colors.Length -1].CoreConvertTo<Color>()};
                    break;
                case enuLinearMode.MultiColor:
                    v_ln.LinearColors = br.Colors.CoreConvertTo<Color[]>();
                    ColorBlend v_clBlend = new ColorBlend();
                    v_clBlend.Colors = br.Colors.CoreConvertTo<Color[]>();
                    v_clBlend.Positions = br.Positions;
                    v_ln.InterpolationColors = v_clBlend;
                    break;
                default:
                    break;
            }
            switch (br.LinearOperator)
            {
                case enuLinearOperator.None:
                    break;
                case enuLinearOperator.SetSigmaBellShape:
                    v_ln.SetSigmaBellShape(br.Focus, br.Scale);
                    break;
                case enuLinearOperator.SetTriangularBellShape:
                    v_ln.SetBlendTriangularShape(br.Focus, br.Scale);
                    break;
                default:
                    break;
            }
            v_ln.GammaCorrection = br.GammaCorrection;
            if (br.WrapMode != enuWrapMode.Clamp)
                v_ln.WrapMode = (WrapMode)br.WrapMode.WinCoreGetValue();
            if (v_p != null)
            {
                v_p.Dispose();
                v_p = null;
            }
            return v_ln;
        }
        public T GetPen<T>(ICorePen corePen) where T : class, IDisposable
        {
            if (corePen == null)
                return null;
            lock (this.m_brushes)
            {
               if (this.m_brushes.ContainsKey(corePen))
                {
                    try
                    {
                        T s = this.m_brushes[corePen] as T;
                        if ((s is Pen) && typeof(T) == typeof(Pen))
                            SetPenProperty(corePen as CorePen, s as Pen);
                        return s;
                    }
                    catch (KeyNotFoundException ex) {
                        CoreLog.WriteDebug("Key miss : " + ex.Message);
                    }
                }
            }
            return null;
        }
        public T GetBrush<T>(ICoreBrush brush) where T : class, IDisposable
        {
            if (brush == null)
                return (null);
            if (this.m_brushes.ContainsKey(brush))
            {
                return this.m_brushes[brush] as T;
            }
            return null;
        }
        public static T CreateBrush<T>(Rectanglef rc, Colorf v_cl1, Colorf v_cl2, float p) where T : class , IDisposable 
        {
            return new LinearGradientBrush(
                new RectangleF(rc.X, rc.Y, rc.Width, rc.Height),
                v_cl1.CoreConvertTo<Color>(),
                v_cl2.CoreConvertTo<Color>(),
                p) as T;
        }
        public static T CreateBrush<T>(float x , float y , float width, float height, Colorf v_cl1, Colorf v_cl2, float p) where T : class , IDisposable
        {
            //if ((width == 0) || (height == 0))
            //    return default(T);

            return new LinearGradientBrush(
                new RectangleF(x, y,width, height),
                v_cl1.CoreConvertTo<Color>(),
                v_cl2.CoreConvertTo<Color>(),
                p) as T;
        }
        public static T CreateBrush<T>(Rectangle rc, Colorf v_cl1, Colorf v_cl2, float p) where T : class , IDisposable
        {
            return new LinearGradientBrush(
                new RectangleF(rc.X, rc.Y, rc.Width, rc.Height),
                v_cl1.CoreConvertTo<Color>(),
                v_cl2.CoreConvertTo<Color>(),
                p) as T;
        }
        //public static T CreateBrush<T>(float x, float y , float width, float height, Colorf v_cl1, Colorf v_cl2, float p) where T : class , IDisposable
        //{
        //    return new LinearGradientBrush(
        //        new RectangleF(x,y,width,height ),
        //        v_cl1.CoreConvertTo<Color>(),
        //        v_cl2.CoreConvertTo<Color>(),
        //        p) as T;
        //}
        public static Brush GetBrush(Colorf cl)
        {
            if (CoreApplicationManager.Application  !=null)
                return CoreBrushRegisterManager.GetBrush<Brush>(cl);
            return null;
        }
        public static Pen GetPen(Colorf cl)
        {
            Pen p =  CoreBrushRegisterManager.GetPen<Pen>(cl);
            if (p == null)
            {
                return Pens.Black;
            }
            return p;
        }
        /// <summary>
        /// return get pen
        /// </summary>
        /// <param name="pen"></param>
        /// <returns></returns>
        public static Pen GetPen(ICorePen pen)
        {
            if (CoreApplicationManager.Application != null)
            {
                var e = CoreApplicationManager.Application.BrushRegister;
                if (e!=null)
                    return e.GetPen<Pen>(pen);
            }
            return null;
        }
        public static LinearGradientBrush CreateBrush(Rectanglef rectangle, Colorf colorf1, Colorf colorf2, float angle)
        {
            return CreateBrush<LinearGradientBrush>(
                rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height ,
                colorf1,
                colorf2,
                angle);
        }
        public static LinearGradientBrush CreateBrush(Rectangle rectangle, Colorf colorf1, Colorf colorf2, float angle)
        {
            return CreateBrush<LinearGradientBrush>(
                rectangle.X, rectangle.Y, rectangle.Width, rectangle.Width ,
                colorf1,
                colorf2,
                angle);
        }
        //public  static LinearGradientBrush CreateBrush(Rectangle rectangle, Colorf colorf1, Colorf colorf2, float p)
        //{
        //    return CreateBrush<LinearGradientBrush>(
        //        rectangle,
        //        colorf1,
        //        colorf2,
        //        p);
        //}
        public static Pen GetPen(ICoreGraphicsPath v_p)
        {           
            if (v_p.StrokeBrush != null)
            {
                return CoreApplicationManager.Application.BrushRegister.GetPen<Pen>(v_p.StrokeBrush);
            }
            return null;
        }
        public static Brush  GetBrush(ICoreGraphicsPath v_p)
        {
            if (v_p.FillBrush != null)
            {
                return CoreApplicationManager.Application.BrushRegister.GetBrush<Brush>(v_p.FillBrush);
            }
            return null;
        }
        /// <summary>
        /// get brush from document
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Brush GetBrush(ICore2DDrawingDocument document)
        {
            ICoreApplication iapp = CoreApplicationManager.Application;
            if (iapp !=null )
                return iapp.BrushRegister.GetBrush<Brush>(document);
            return null;
        }

        public static Brush GetBrush(ICoreBrush brush)
        {
            ICoreApplication iapp = CoreApplicationManager.Application;
            if (iapp != null)
                return iapp.BrushRegister.GetBrush<Brush>(brush);
            return null;
        }
        public void Reload(ICoreBrush coreBrush)
        {
            if (this.m_brushes.ContainsKey(coreBrush))
            {
                IDisposable c = GenerateBrush(coreBrush);          
                this.m_brushes[coreBrush] = c;
            }
        }
        /// <summary>
        /// Get Brush from document 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public Brush GetTextureBrush(ICore2DDrawingDocument document)
        {
            if (document == null)
                return null;
            if (this.m_DocumentTextureBrush.ContainsKey(document))
                return this.m_DocumentTextureBrush[document];
            using (Bitmap bmp = new Bitmap(document.Width, document.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    document.Draw(g);
                }
                TextureBrush txb = new TextureBrush(bmp);
                this.m_DocumentTextureBrush.Add(document, txb);
                return txb;
            }
        }




     
    }
}

