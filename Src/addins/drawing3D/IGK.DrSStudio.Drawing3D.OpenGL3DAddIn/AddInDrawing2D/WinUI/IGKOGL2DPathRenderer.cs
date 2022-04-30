

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKOGL2DPathRenderer.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using IGK.GLLib;
using IGK.OGLGame.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D.OpenGL
{
    public class IGKOGL2DPathRenderer
    {
        private byte[] m_types;
        private Vector2f[] m_points;
        private IGKOGL2DDrawingSegmentCollections m_segments;
        private List<Vector2f> m_startIndices;
        private Vector2f m_StartPoint;

        /// <summary>
        /// get the string point
        /// </summary>
        public Vector2f StartPoint
        {
            get { return m_StartPoint; }           
        }
        public IGKOGL2DPathRenderer(Vector2f[] points, byte[] types)
        {
            if ((points == null) || (types == null) || (points.Length == 0) || 
                (points.Length != types.Length))
            {
                throw new ArgumentNullException("points or types is null");
            }
            this.m_startIndices = new List<Vector2f>();
            this.m_segments = new IGKOGL2DDrawingSegmentCollections(this);
            // add start indice for 3D rendergin
            this.m_points = points;
            this.m_types = types;
            this.InitPathRendering();
            
        }

        private void InitPathRendering()
        {
            //CoreLog.WriteDebug(System.Reflection.MethodInfo.GetCurrentMethod().Name + " :: Init path rendereging ...");
            IGKOGL2DDrawingSegment v_csegment = null;
            int v_nextIndice = -1;
            //this.m_segments.Clear();
            enuGdiGraphicPathType t = enuGdiGraphicPathType.StartFigure;

            for (int i = 0; i < this.m_types.Length; i++)
            {

                t = (enuGdiGraphicPathType)this.m_types[i];
                if (v_csegment == null)
                {
                    this.m_startIndices.Add (this.m_points[i]);


                    this.startFigure(ref i, ref v_csegment);
                    if (v_csegment != null)
                    {
                        if (t == enuGdiGraphicPathType.StartFigure)
                        {
                            v_nextIndice++;
                        }
                        v_csegment.StartPointIndex = v_nextIndice;
                   
                    }
                }
                else
                {
                    switch (t)
                    {
                        case enuGdiGraphicPathType.ControlPoint:
                            if (v_csegment.DefPoint == 3)
                            {//current segment is line
                                v_csegment.append(this.m_points[i], false);
                            }
                            else
                            {
                                //create na new line segment
                                //i--;
                                //startFigure(ref i, ref v_csegment);
                                v_csegment = new IGKOGL2DDrawingCurveSegment(
                                    this,
                                      this.m_points[i - 1],
                                      this.m_points[i]);
                                v_csegment.StartPointIndex = v_nextIndice;
                                this.m_segments.Add(v_csegment);
                            }
                            break;
                        case enuGdiGraphicPathType.EndPoint:
                            //generally combine with a button type
                            break;
                        case enuGdiGraphicPathType.LinePoint:
                            if (v_csegment.DefPoint == 1)
                            {//current segment is line
                                v_csegment.append(this.m_points[i], false);
                            }
                            else
                            {
                                //create na new line segment
                                v_csegment = new IGKOGL2DDrawingLineSegment(
                                    this,
                                      this.m_points[i - 1],
                                      this.m_points[i]);
                                v_csegment.StartPointIndex = v_nextIndice;
                                this.m_segments.Add(v_csegment);
                            }
                            break;
                        case enuGdiGraphicPathType.Marker:
                            break;
                        case enuGdiGraphicPathType.Mask:
                            break;
                        case enuGdiGraphicPathType.StartFigure:
                            //starting figure 
                            //normaly must not reach here
                            this.startFigure(ref i, ref v_csegment);

                            break;
                        default:
                            if ((t & enuGdiGraphicPathType.EndPoint) == enuGdiGraphicPathType.EndPoint)
                            {
                                //deactivate end point
                                t &= ~enuGdiGraphicPathType.EndPoint;
                                switch (t)
                                {
                                    case enuGdiGraphicPathType.Marker | enuGdiGraphicPathType.LinePoint:

                                        if ((v_csegment != null) && (v_csegment.DefPoint == 1))
                                        {
                                            v_csegment.append(this.m_points[i], false);
                                            v_csegment.closeSegment();
                                            //this.m_startIndices.Add(this.m_points[i]);
                                            //v_nextIndice++;
                                            v_csegment = null;
                                        }
                                        break;
                                    case enuGdiGraphicPathType.Marker | enuGdiGraphicPathType.ControlPoint:
                                        if ((v_csegment != null) && (v_csegment.DefPoint == 3))
                                        {
                                            v_csegment.append(this.m_points[i], false);
                                            v_csegment.closeSegment();
                                            //this.m_startIndices.Add(this.m_points[i]);
                                            //v_nextIndice++;
                                            v_csegment = null;
                                        }
                                        break;
                                    case enuGdiGraphicPathType.ControlPoint:
                                        //start control point
                                        if (v_csegment.DefPoint == 3)
                                        {//current segment is line

                                            v_csegment.append(this.m_points[i], false);
                                            v_csegment.closeSegment();
                                            v_csegment = null;
                                            //startFigure(ref i, ref v_csegment);
                                        }
                                        else
                                        {
                                            v_csegment = null;
                                            //create new curve segment leave the last one open
                                            v_csegment = new IGKOGL2DDrawingCurveSegment(
                                                this,
                                                  this.m_points[i - 1],
                                                  this.m_points[i]);
                                            this.m_segments.Add(v_csegment);
                                        }


                                        break;
                                    case enuGdiGraphicPathType.LinePoint:
                                        //append last line
                                        if (v_csegment.DefPoint == 1)
                                        {//current segment is line
                                            v_csegment.append(this.m_points[i], false);
                                            v_csegment.closeSegment();
                                            startFigure(ref i, ref v_csegment);
                                        }
                                        else
                                        {
                                            //create a new line segment
                                            v_csegment = new IGKOGL2DDrawingLineSegment(
                                                this,
                                                  this.m_points[i - 1],
                                                  this.m_points[i]);
                                            this.m_segments.Add(v_csegment);
                                        }
                                        break;
                                    default:
                                        break;
                                }
                            }
                            else
                            {
                            }
                            break;
                    }
                }
            }
            this.m_StartPoint = this.m_points[0];
        }

        private void startFigure(ref int i, ref IGKOGL2DDrawingSegment v_csegment)
        {

            if (i + 1 < this.m_types.Length)
            {
                int k = i + 1;
                if (this.m_types[k] == (byte)enuGdiGraphicPathType.LinePoint)
                {
                    v_csegment = new IGKOGL2DDrawingLineSegment(
                        this,
                        this.m_points[i],
                        this.m_points[i+1]);
                    this.m_segments.Add(v_csegment);
                    i++;
                }
                else
                {
                    if (this.m_types[k] == (byte)enuGdiGraphicPathType.ControlPoint)
                    {
                        v_csegment = new IGKOGL2DDrawingCurveSegment(
                            this,
                            this.m_points[i],
                            this.m_points[i+1]);
                        this.m_segments.Add(v_csegment);
                        i++;
                    }
                    else if (this.m_types[k] == 0)
                    {
                        
                        v_csegment = null;
                        i = k;
                        startFigure (ref i , ref v_csegment );
                    }
                    else{
                        CoreLog.WriteDebug("Start Segment not detected...");
                    }
                }
            }
        }


       
        /// <summary>
        /// render on segment
        /// </summary>
        /// <param name="device"></param>
        public void Render(IGKOGL2DDrawingDeviceVisitor visitor)
        {
            foreach (IGKOGL2DDrawingSegment segment in this.m_segments)
            {
                segment.Render(visitor);
            }
        }
        internal void RenderPolygon(IGKOGL2DDrawingDeviceVisitor visitor)
        {
            visitor.Device.Begin(enuGraphicsPrimitives.Polygon);
            bool opened = true;
           // Vector2f lpts = Vector2f.Zero;
            
            foreach (IGKOGL2DDrawingSegment segment in this.m_segments)
            {
                if (!opened )
                    visitor.Device.Begin(enuGraphicsPrimitives.Polygon);                   
                Vector2f[]  ts = segment.GetVertices();

                for (int i = 0; i < ts.Length; i++)
                {
                    GL.glVertex2f(ts[i].X, ts[i].Y);
                }
                if (segment.Closed)
                {
                    //start new figure
                    visitor.Device.End();
                    opened = false;                    
                }
                //segment.Render(visitor);
            }
            if (opened)
            visitor.Device.End();
        }
        public abstract class IGKOGL2DDrawingSegment 
        {
            private bool m_closed;
            public abstract byte DefPoint { get; }
            public bool Closed { get { return this.m_closed; } }
            private IGKOGL2DPathRenderer m_renderer;
            public  IGKOGL2DPathRenderer Renderer { get { return this.m_renderer; } }
            public override string ToString()
            {
                return "Segment[PointType:"+DefPoint +" :CloseStart"+StartPointIndex +" : index:"+Index+"]";
            }
            private int Index {
                get {
                    return this.m_renderer.m_segments.IndexOf(this);
                }
            }
            private int m_StartPointIndex;

            public int StartPointIndex
            {
                get { return m_StartPointIndex; }
                set
                {
                    if (m_StartPointIndex != value)
                    {
                        m_StartPointIndex = value;
                    }
                }
            }
            public IGKOGL2DDrawingSegment(IGKOGL2DPathRenderer renderer)
            {
                this.m_closed = false;
                this.m_renderer = renderer;
            }
            public virtual void append(Vector2f point, bool compressed)
            { 
            }
            public abstract void Render(IGKOGL2DDrawingDeviceVisitor device);


            public void closeSegment() {
                this.m_closed = true;
            }
            public abstract  Vector2f[] GetVertices();
        
        }
        class IGKOGL2DDrawingLineSegment : IGKOGL2DDrawingSegment
        {
            List<Vector2f> m_points;
            public override byte DefPoint{get{return 1;}}


            public IGKOGL2DDrawingLineSegment(IGKOGL2DPathRenderer renderer, Vector2f start, Vector2f end):base(renderer)
            {
                this.m_points = new List<Vector2f>();
                this.m_points.Add(start);
                this.m_points.Add (end);
            }
            public override void append(Vector2f pts, bool compressed)
            {
                if (!(compressed) ||                 
                    ((m_points.Count<= 0 ) || (m_points [m_points.Count -1 ]!= pts)))
                        this.m_points.Add(pts);
            }

            public override void Render(IGKOGL2DDrawingDeviceVisitor device)
            {
                device.drawLines(m_points.ToArray (), false );
                if (Closed)
                {
                    device.drawLine(m_points[m_points.Count - 1],
                        this.Renderer.GetStartPoint(this.StartPointIndex)
                        );
                }
            }

            public override Vector2f[] GetVertices()
            {
                return m_points.ToArray();
            }
        }
        class IGKOGL2DDrawingCurveSegment : IGKOGL2DDrawingSegment
        {
            List<Vector2f> m_points;
            
            public override byte DefPoint{get{return (byte) enuGdiGraphicPathType.ControlPoint;}}


            public IGKOGL2DDrawingCurveSegment(IGKOGL2DPathRenderer renderer , Vector2f start, Vector2f end):base(renderer)
            {
                this.m_points = new List<Vector2f>();
                this.m_points.Add(start);
                this.m_points.Add (end);
            }
            public override void append(Vector2f pts, bool compressed)
            {
                if (!(compressed) ||                 
                    ((m_points.Count<= 0 ) || (m_points [m_points.Count -1 ]!= pts)))
                        this.m_points.Add(pts);
            }

            public override void Render(IGKOGL2DDrawingDeviceVisitor device)
            {
                device.drawCurve(m_points.ToArray(),false);
                if (Closed)
                {
                    device.drawLine(m_points[m_points.Count - 1],
                        this.Renderer.GetStartPoint(this.StartPointIndex)
                        );
                }
                
            }

            public override Vector2f[] GetVertices()
            {
                return this.m_points.ToArray();
            }
        }
        class IGKOGL2DDrawingSegmentCollections : IEnumerable 
        {
            List<IGKOGL2DDrawingSegment> m_segments;
            private IGKOGL2DPathRenderer m_owner;

            public IGKOGL2DDrawingSegmentCollections(IGKOGL2DPathRenderer owner)
            {
                this.m_owner = owner;
                this.m_segments = new List<IGKOGL2DDrawingSegment>();
            }


            public IEnumerator GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }
            public void Add(IGKOGL2DDrawingSegment segment) {
                this.m_segments.Add(segment);
            }
            public void Remove(IGKOGL2DDrawingSegment segment)
            {
                this.m_segments.Remove(segment);
            }
            public int Count { get { return this.m_segments.Count; } }


            internal int IndexOf(IGKOGL2DDrawingSegment segment)
            {
                return this.m_segments.IndexOf(segment);
            }
        }

        internal Vector2f GetStartPoint(int index)
        {
            if ((index >= 0) && (index < this.m_startIndices.Count))
            {
                return this.m_startIndices[index];
            }
            return Vector2f.Zero;
        }

       
    }
}
