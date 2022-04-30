

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ArcElement.cs
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
file:ArcElement.cs
*/
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.Segments;
using IGK.ICore.Drawing2D.WorkingObjects.Standard;
using IGK.ICore.MecanismActions;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Arc",
     typeof(Mecanism),
     Keys = enuKeys.A)]
    public class ArcElement :
        PieElement,
        ICore2DClosableElement 
    {
        private enuArcJoinMode m_JoinMode;

        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue(enuArcJoinMode.Default)]
        /// <summary>
        /// get or set the Joint Mode
        /// </summary>
        public enuArcJoinMode JoinMode
        {
            get { return m_JoinMode; }
            set
            {
                if (m_JoinMode != value)
                {
                    m_JoinMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);                    
                }
            }
        }
     


        public override Rectanglef GetAlignmentBound()
        {
            return CoreMathOperation.GetBounds(this.Center, this.Radius);
        }
        private bool m_Closed;
        /// <summary>
        /// get or set closed element
        /// </summary>
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public ArcElement():base()
        {
            this.m_Closed = false;
            this.m_JoinMode = enuArcJoinMode.Default;
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.SweepAngle == 0)
                return;

            if (this.Radius != null)
            {
                Rectanglef v_rc = Rectanglef.Empty;
               // enuArcJoinMode v_mode = enuArcJoinMode.Round;

                switch (this.m_JoinMode)
                {
                    case enuArcJoinMode.Round:
                        PathSegment cpath = new PathSegment();
                        for (int i = 0; i < this.Radius.Length; i++)
                        {
                            v_rc = new Rectanglef(
                                   Center.X - this.Radius[i].X,
                                   Center.Y - this.Radius[i].Y,
                                   2 * this.Radius[i].X,
                                   2 * this.Radius[i].Y);
                            if (v_rc.IsEmpty)
                                continue;
                            if ((i % 2) == 1)
                            {

                                if (cpath.PointCount<=0)
                                    break;

                                var g = cpath.GetPathPoints();// path.Segments[path.Segments.Count - 1];
                                var v_tpts = cpath.GetPathPoints();// g.GetPathPoints();
                                var pts = v_tpts.Last();//[0]; // g.GetPathPoints().Last();
                                PathSegment c = new PathSegment();
                               // cpath.SetPoint(-1, 1);
                                c.AddArc(v_rc,
                                    this.StartAngle + this.SweepAngle,
                                    -this.SweepAngle, false);
                                var cp = c.GetPathPoints();
                                var ets = cp.First();//.Last();//[0];
                                c.SetPoint(0, 1);
                                //g.SetPoint(0, 1);
                                //g.CloseFigure();
                                var r = CoreMathOperation.GetDistance(ets, pts) / 2;
                                var center = CoreMathOperation.GetMiddlePoint(pts, ets);
                                var ah = CoreMathOperation.GetAngle(center, pts) * CoreMathOperation.ConvRdToDEGREE;
                                var wh = CoreMathOperation.GetAngle(center, ets) * CoreMathOperation.ConvRdToDEGREE;

                           

                                float aa = wh - ah;
                                if (ah > wh)
                                    aa *= -1;
                                //System.Diagnostics.Debug.WriteLine(aa);
                                PathSegment cc = new PathSegment();
                                cc.AddArc(center,
                                     new Vector2f(r,r), ah, aa, false);
                                cc.SetPoint(0, 1);

                                

                                cpath.Append (cc.GetPathPoints(), cc.GetPathTypes());
                                cpath.Append(c.GetPathPoints(), c.GetPathTypes());

                                ets = cp[cp.Length - 1];
                                pts = v_tpts[0];// vg.GetPathPoints()[0];
                                center = CoreMathOperation.GetMiddlePoint(pts, ets);

                                ah = CoreMathOperation.GetAngle(center, pts) * CoreMathOperation.ConvRdToDEGREE;
                                wh = CoreMathOperation.GetAngle(center, ets) * CoreMathOperation.ConvRdToDEGREE;
                                aa = wh - ah;
                                if (ah < wh)
                                    aa *= -1;

                                //System.Diagnostics.Debug.WriteLine(" x " + ah);
                                //System.Diagnostics.Debug.WriteLine(" y " + wh);
                                cc = new PathSegment();
                                cc.AddArc (center,
                                new Vector2f(r, r), ah,  aa, true);
                                cpath.Append(cc.GetPathPoints(), cc.GetPathTypes());

                                //path.CloseFigure();
                                //c.CloseFigure();
                                path.AddSegment(cpath);
                                cpath = new PathSegment();//.Reset();
                            }
                            else
                            {
                                cpath.AddArc(v_rc,
                                    this.StartAngle,
                                    this.SweepAngle, false);
                            }
                        }

                        if (!cpath.IsEmpty) {
                            path.AddSegment(cpath);
                        }


                        break;
                    case enuArcJoinMode.Square:
                        for (int i = 0; i < this.Radius.Length; i++)
                        {
                            v_rc = new Rectanglef(
                                   Center.X - this.Radius[i].X,
                                   Center.Y - this.Radius[i].Y,
                                   2 * this.Radius[i].X,
                                   2 * this.Radius[i].Y);
                            if (v_rc.IsEmpty)
                                continue;
                            if ((i % 2) == 1)
                            {
                                path.AddArc(v_rc,
                                    this.StartAngle + this.SweepAngle,
                                    -this.SweepAngle, false);
                                var g = path.Segments[path.Segments.Count - 1];
                                g.SetPoint(0, 1);
                                g.CloseFigure();
                            }
                            else
                            {
                                path.AddArc(v_rc,
                                    this.StartAngle,
                                    this.SweepAngle, false);
                            }
                        }
                        break;
                    case enuArcJoinMode.Default:                        
                    default:
               
                            for (int i = 0; i < this.Radius.Length; i++)
                            {
                                v_rc = new Rectanglef(
                                       Center.X - this.Radius[i].X,
                                       Center.Y - this.Radius[i].Y,
                                       2 * this.Radius[i].X,
                                       2 * this.Radius[i].Y);
                                if (v_rc.IsEmpty)
                                    continue;
                                path.AddArc(v_rc,
                                    this.StartAngle,
                                    this.SweepAngle, this.Closed);
                            }
                        break;
                }

                path.FillMode = this.FillMode;
            }

  
            //if (c != null)
            //{
            //    byte[] Data1 = null;
            //    Vector2f[] points1 = null;
            //    path.GetAllDefinition(out points1, out Data1);

            //    byte[] Data = null;
            //    Vector2f[] points = null;
            //    c.GetPathDefinition(out points, out  Data);
            //    c.Dispose();
            //}
        }
        
        
        public new class Mecanism : PieElement.Mecanism 
        {
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[enuKeys.T] = new ToggleJoinMode(this);

            }
        }

        class ToggleJoinMode : CoreMecanismActionBase
        {
            private Mecanism mecanism;

            public ToggleJoinMode(Mecanism mecanism)
            {
                this.mecanism = mecanism;
            }
            protected override bool PerformAction()
            {
                if (this.mecanism.Element is ArcElement s) {
                    switch (s.JoinMode)
                    {
                        case enuArcJoinMode.Default:
                            s.JoinMode = enuArcJoinMode.Square;
                            break;
                        case enuArcJoinMode.Square:
                            s.JoinMode = enuArcJoinMode.Round;
                            break;
                        case enuArcJoinMode.Round:
                            s.JoinMode = enuArcJoinMode.Default;
                            break;
                        default:
                            break;
                    }

                    return true;
                }
                return false;

                
            }
        }
    }
}

