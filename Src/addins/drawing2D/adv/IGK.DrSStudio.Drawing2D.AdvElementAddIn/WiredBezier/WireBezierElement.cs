

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WireBezierElement.cs
*/

using IGK.ICore.Actions;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using IGK.DrSStudio.WireAddIn.Actions;
using System; using IGK.ICore; 
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.MecanismActions;
namespace IGK.DrSStudio.WireAddIn
{
    [Core2DDrawingStandardElement ("WireBezierElement", typeof(Mecanism))]
    /// <summary>
    /// represent the wire element
    /// </summary>
    class WireBezierElement : Core2DDrawingDualBrushElement
    {
        private Vector2f  m_StartPoint;
        private Vector2f m_EndPoint;
        private List<WireBeizerPoint > m_Definitions;
      
        [CoreXMLAttribute()]
        [TypeConverter(typeof(WireBeizerPointTypeConverter))]
        public WireBeizerPoint[] Definitions
        {
            get { return m_Definitions.ToArray(); }
        }
        [CoreXMLAttribute()]
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
        protected override void SetAttributeValue(string name, string value)
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
            base.SetAttributeValue(name, value);
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All; }
        }
    
        public WireBezierElement()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.StrokeBrush.SetSolidColor(Colorf.Black);
            this.FillBrush.SetSolidColor(Colorf.White);
            this.m_Definitions = new List<WireBeizerPoint>();
            this.m_Definitions.Add(new WireBeizerPoint(Vector2f.Zero, Vector2f.Zero));
        }
      
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
          p.Reset ();
          if (this.m_Definitions == null)
              return;
            foreach (WireBeizerPoint item in this.m_Definitions)
	        {
                //p.SetMarkers ();
                //p.StartFigure();
                p.AddBezier (this.StartPoint, item.Definition1, item.Definition2 , this.EndPoint );
                //p.CloseFigure();
	        }
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
        //public override void Draw(Graphics g)
        //{
        //    CoreGraphicsPath vp = this.GetPath();
        //    if (vp == null)
        //        return;
        //    Brush v_br = this.m_FillBrush.GetBrush();
        //    Pen v_pen = this.m_StrokeBrush.GetPen();
        //    object v_state = g.Save ();
        //    this.SetGraphicsProperty(g);
        //    if (v_br != null) g.FillPath(v_br, vp);
        //    CoreGraphicsPathIterator v_iterator = new CoreGraphicsPathIterator(vp);
        //    using (CoreGraphicsPath v_outpath = new CoreGraphicsPath())
        //    {
        //        int v_index = 0;
        //        for (int i = 0; i < v_iterator.SubpathCount; i++)
        //        {
        //            v_outpath.Reset();
        //            v_index = v_iterator.NextMarker(v_outpath);
        //            //if (v_br != null) g.FillPath(v_br, v_outpath);
        //            if (v_pen != null) g.DrawPath(v_pen, v_outpath);
        //        }
        //    }
        //    g.Restore(v_state);
        //}
        public new class Mecanism : Core2DDrawingSurfaceMecanismBase<WireBezierElement> 
        {
           public override void Render(ICoreGraphics device)
            {
 	            WireBezierElement vl = this.Element;
                if (vl == null)
                    return;
                WireBeizerPoint[] d = vl.Definitions;
                var v_s = this.CurrentSurface;
                Vector2f px = Vector2f.Zero;
                Vector2f py = Vector2f.Zero;
                for (int i = 0; i < d.Length ; i++)
                {
                    px =
                        v_s.GetScreenLocation(d[i].Definition1);
                    py =
                        v_s.GetScreenLocation(d[i].Definition2);
                    device.DrawLine(Colorf.Pink,px.X, px.Y ,py.X, py.Y );
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
           
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.A, new AddWireSegmentAction(this));
                this.AddAction(enuKeys.R, new RemoveLastWireSegmentAction(this));
                this.AddAction(enuKeys.R | enuKeys.Shift, new ResetAllWireSegmentAction(this));
            }

            protected override void EndMove(CoreMouseEventArgs e)
            {
                base.EndMove(e);
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
                this.Invalidate();
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
                this.Invalidate();
                base.UpdateSnippetEdit(e);
            }
        }
    }
}

