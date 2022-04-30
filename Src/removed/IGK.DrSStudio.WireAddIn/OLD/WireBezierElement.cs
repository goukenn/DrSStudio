

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WireBezierElement.cs
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
file:WireBezierElement.cs
*/
using IGK.ICore;using IGK.DrSStudio.Actions;
using IGK.DrSStudio.Codec;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.WireAddIn.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WireAddIn
{
    [Core2DDrawingStandardElement ("WireBezier", typeof(Mecanism))]
    /// <summary>
    /// represent the wire element
    /// </summary>
    class WireBezierElement : Core2DDrawingLayeredElement
    {
        private ICorePen m_StrokeBrush;
        private Vector2f  m_StartPoint;
        private Vector2f m_EndPoint;
        private List<WireBeizerPoint > m_Definitions;
        private ICoreBrush  m_FillBrush;
        [CoreXMLAttribute ()]
        public ICoreBrush  FillBrush
        {
            get { return m_FillBrush; }
        }
        [CoreXMLAttribute()]
        [TypeConverter(typeof(WireBeizerPointTypeConverter))]
        public WireBeizerPoint[] Definitions
        {
            get { return m_Definitions.ToArray(); }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Vector2f EndPoint
        {
            get { return m_EndPoint; }
            set
            {
                if (m_EndPoint != value)
                {
                    m_EndPoint = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }
            set
            {
                if (m_StartPoint != value)
                {
                    m_StartPoint = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        protected override void ReadAttributeValue(string name, string value)
        {
            if (string.IsNullOrEmpty(name))
                return;
            switch (name.ToLower())
            { 
                case "definitions":
                    WireBeizerPointTypeConverter f = new WireBeizerPointTypeConverter();
                    object boj = f.ConvertFromString(value);
                    WireBeizerPoint[] t  = null;
                    if (boj != null)
                    {
                        if (boj.GetType().IsArray)
                            t = (WireBeizerPoint[])boj;
                        else
                            t = new WireBeizerPoint[] { (WireBeizerPoint) boj   };
                    }                    
                    if ((t !=null) && (t.Length > 0))
                    {
                        this.m_Definitions.Clear();
                        this.m_Definitions.AddRange(t);
                    }
                    return;
            }
            base.ReadAttributeValue(name, value);
        }
        protected override void ReadAttributes(Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);         
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public ICorePen StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke)
                return m_StrokeBrush;
            if (mode == enuBrushMode.Fill)
                return m_FillBrush;
            return null;
        }
        public WireBezierElement()
        {
            this.m_StrokeBrush = new CorePen(this);
            this.m_FillBrush = new CoreBrush(this);
            this.StrokeBrush.SetSolidColor(Colorf.Black);
            this.FillBrush.SetSolidColor(Colorf.White);
            this.m_Definitions = new List<WireBeizerPoint> ();
            this.m_Definitions.Add (new WireBeizerPoint (Vector2f.Zero , Vector2f.Zero));
            this.m_StrokeBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
            this.m_FillBrush.BrushDefinitionChanged += _BrushDefinitionChanged;
        }
        void _BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
        protected override void GeneratePath()
        {
            GraphicsPath p = new GraphicsPath();
            foreach (WireBeizerPoint item in this.m_Definitions)
	        {
                p.SetMarkers ();
                p.StartFigure();
                p.AddBezier (this.StartPoint, item.Definition1, item.Definition2 , this.EndPoint );
                //p.CloseFigure();
	        }
            this.SetPath(p);
        }
        public void AddDefinition(Vector2f def1, Vector2f def2)
        {
            this.m_Definitions .Add (new WireBeizerPoint (def1 , def2 ));
            this.OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
        }
        public void RemoveLastSegment()
        {
            if (this.m_Definitions.Count > 1)
            {
                this.m_Definitions.RemoveAt (this.m_Definitions.Count - 1);
                this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public  void ResetAllDefinitions()
        {
            WireBeizerPoint[] t = this.Definitions;
            Vector2f mid = CoreMathOperation.GetMiddlePoint (this.StartPoint, this.EndPoint );
            for (int i = 0; i < t.Length ; i++)
            {
                this.m_Definitions[i] = new WireBeizerPoint(mid, mid);
            }
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public void ResetDefinition()
        {
            this.m_Definitions.Clear ();
            Vector2f pt = CoreMathOperation.GetMiddlePoint (this.m_StartPoint , this.EndPoint );
            this.AddDefinition (pt, pt);
        }
        public override void Draw(Graphics g)
        {
            GraphicsPath vp = this.GetPath();
            if (vp == null)
                return;
            Brush v_br = this.m_FillBrush.GetBrush();
            Pen v_pen = this.m_StrokeBrush.GetPen();
            GraphicsState v_state = g.Save ();
            this.SetGraphicsProperty(g);
            if (v_br != null) g.FillPath(v_br, vp);
            GraphicsPathIterator v_iterator = new GraphicsPathIterator(vp);
            using (GraphicsPath v_outpath = new GraphicsPath())
            {
                int v_index = 0;
                for (int i = 0; i < v_iterator.SubpathCount; i++)
                {
                    v_outpath.Reset();
                    v_index = v_iterator.NextMarker(v_outpath);
                    //if (v_br != null) g.FillPath(v_br, v_outpath);
                    if (v_pen != null) g.DrawPath(v_pen, v_outpath);
                }
            }
            g.Restore(v_state);
        }
        public class Mecanism : Core2DDrawingMecanismBase
        {
            public new WireBezierElement Element {
                get {
                    return base.Element as WireBezierElement;
                }
                set {
                    base.Element = value;
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                WireBezierElement vl = this.Element;
                if (vl == null)
                    return;
                WireBeizerPoint[] d = vl.Definitions;
                var v_s = this.CurrentSurface;
                for (int i = 0; i < d.Length ; i++)
                {
                    e.Graphics.DrawLine(Pens.Pink,
                        v_s.GetScreenLocation(d[i].Definition1),
                        v_s.GetScreenLocation(d[i].Definition2));
                }
            }
            public abstract class WireSegmentActionBase : CoreMecanismActionBase
            {
                private Mecanism m_Mecanism;
                public new Mecanism Mecanism
                {
                    get { return m_Mecanism; }
                }
                public WireSegmentActionBase(Mecanism mecanism):base()
                {
                    this.m_Mecanism = mecanism;
                }
            }
            protected override Core2DDrawingLayeredElement CreateNewElement()
            {
                return base.CreateNewElement();
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(System.Windows.Forms.Keys.A, new AddWireSegmentAction(this));
                this.AddAction(System.Windows.Forms.Keys.R, new RemoveLastWireSegmentAction(this));
                this.AddAction(Keys.R | Keys.Shift , new ResetAllWireSegmentAction(this));
            }
            public override bool AllowActions
            {
                get
                {
                    return true;
                }
            }
            protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
            {
                WireBezierElement l = this.Element;
                if (l != null)
                {
                    WireBeizerPoint[] pts = l.Definitions;
                    if (this.RegSnippets.Count != (pts.Length * 2) + 2)
                    {
                        this.GenerateSnippets();
                        this.InitSnippetsLocation();
                    }
                }
                base.OnElementPropertyChanged(e);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                WireBezierElement l = this.Element;
                if (l == null) return;
                WireBeizerPoint[] pts = l.Definitions;
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 1, 0));
                for (int i = 0; i < pts.Length; i++)
                {
                    this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 2, (i*2)+ 1));
                    this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 2, (i*2)+ 2));
                }
                this.RegSnippets.Add(this.CurrentSurface.CreateSnippet(this, 1, (pts.Length *2) +1));
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                WireBezierElement l = this.Element;
                if (l == null) return;
                WireBeizerPoint[] pts = l.Definitions;
                RegSnippets[0].Location = CurrentSurface.GetScreenLocation(l.StartPoint);
                RegSnippets[(pts.Length * 2)+1].Location = CurrentSurface.GetScreenLocation(l.EndPoint);                
                for (int i = 0; i < pts.Length; i++)
                {
                    this.RegSnippets[(i*2) + 1].Location = CurrentSurface.GetScreenLocation(pts[i].Definition1);
                    this.RegSnippets[(i*2) + 2].Location = CurrentSurface.GetScreenLocation(pts[i].Definition2);
                }              
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                WireBezierElement l = this.Element;
                if (l == null) return;
                this.EndPoint = e.FactorPoint;
                l.m_StartPoint = this.StartPoint;
                l.m_EndPoint = e.FactorPoint;
                Vector2f mid = CoreMathOperation.GetMiddlePoint (this.StartPoint, this.EndPoint );
                l.m_Definitions[0] = new WireBeizerPoint(mid, mid);
                l.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                WireBezierElement l = this.Element;
                if (l == null) return;
                switch (Snippet.Demand)
                {
                    case 1:
                        if (Snippet.Index == 0)
                        {
                            l.m_StartPoint = e.FactorPoint;
                        }
                        else
                            l.m_EndPoint = e.FactorPoint;
                        break;
                    case 2:
                        int index = ((Snippet.Index-1) / 2);
                        int pindex = (Snippet.Index-1) % 2;
                        if ( (index >= 0) && (index < l.m_Definitions.Count))
                        {
                            if (pindex == 0)
                            {
                                l.m_Definitions[index] = new WireBeizerPoint(e.FactorPoint, l.m_Definitions[index].Definition2);
                            }
                            else
                            {
                                l.m_Definitions[index] = new WireBeizerPoint(l.m_Definitions[index].Definition1, e.FactorPoint);
                            }
                        }
                        else { 
                        }
                        break;
                    default:
                        break;
                }
                l.InitElement();
                this.Snippet.Location = e.Location;
                this.CurrentSurface.Invalidate();
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

