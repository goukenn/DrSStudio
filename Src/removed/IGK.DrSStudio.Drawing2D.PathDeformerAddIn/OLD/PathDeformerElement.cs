

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathDeformerElement.cs
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
file:PathDeformerElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [Core2DDrawingSelection ("DeformWithQuadraticSelection", typeof (Mecanism ), ImageKey="DE_QuadricDeform")]
    class PathDeformerElement : Core2DDrawingObjectBase 
    {
        /// <summary>
        /// mecanism used to deform path element
        /// </summary>
        class Mecanism : Core2DDrawingMecanismBase
        {
            /*
             *how the mecanism work.
             *1: select a path element 
             *2: draw the curpoint attached the that element
            */
            private Vector2f m_P0;
            private Vector2f m_P1;
            private Vector2f m_P2;
            private Vector2f m_P3;
            private GraphicsPath m_tempPath;
            private GraphicsPath m_initPath;
            private PointF[] m_initPoints;
            private Byte[] m_initByte;
            //private Thread m_workingThread;
            public new PathElement  Element
            {
                get
                {
                    return base.Element as PathElement;
                }
                set
                {
                    base.Element = value as Core2DDrawingLayeredElement;
                }
            }
            public override void Dispose()
            {
                base.Dispose();
                this.DisposeTempPath();
            }
            private void DisposeTempPath()
            {
                if (this.m_tempPath != null)
                {
                    this.m_tempPath.Dispose();
                }
                this.m_tempPath = null;
            }
            public Vector2f P3
            {
                get { return m_P3; }
                set { m_P3 = value; }
            }
            public Vector2f P2
            {
                get { return m_P2; }
                set { m_P2 = value; }
            }
            public Vector2f P1
            {
                get { return m_P1; }
                set { m_P1 = value; }
            }
            public Vector2f P0
            {
                get { return m_P0; }
                set { m_P0 = value; }
            }
            void StartJob()
            {
                lock (this)
                {
                    this.SetTempPath();
                }
            }
            private void SetTempPath()
            {
                getPointDef(P0, P1, P2, P3);
                if (this.Element != null)
                {
                    this.Element.ClearTransform();
                    this.Element.SetPathDefinition(
                    this.m_tempPath.PathPoints,
                    this.m_tempPath.PathTypes);
                    this.CurrentSurface.Invalidate();
                }
            }
            //void BeginGetWorkingThread()
            //{
            //    //this.AbortGetPathPoint();
            //    //this.m_workingThread = new Thread(StartJob);
            //    //this.m_workingThread.IsBackground = true;
            //    //this.m_workingThread.Start();
            //}
            //void AbortGetPathPoint()
            //{
            //    //if (this.m_workingThread != null)
            //    //{
            //    //    this.m_workingThread.Abort();
            //    //    this.m_workingThread.Join();
            //    //}
            //}
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);
                if (this.State == ST_EDITING)
                {
                    PointF [] v_ptg = GetPoints();
                    for (int i = 0; i < v_ptg.Length; i++)
                    {
                        v_ptg[i] = this.CurrentSurface.GetScreenLocation(v_ptg[i]);
                    }
                    e.Graphics.DrawBeziers(Pens.Yellow, v_ptg);
                    e.Graphics.DrawLine(Pens.Pink, v_ptg[0], v_ptg[1]);
                    e.Graphics.DrawLine(Pens.Pink, v_ptg[2], v_ptg[3]);
                    GraphicsState s = e.Graphics.Save();
                    this.ApplyCurrentSurfaceTransform(e.Graphics);
                    if (this.m_tempPath != null)
                    {
                        e.Graphics.FillPath(
                            CoreBrushRegister.GetBrush(Colorf.FromFloat(0.5f, 0.6f)), this.m_tempPath);
                    }
                    e.Graphics.Restore(s);
                }
            }
            private PointF [] GetPoints()
            {
                return new PointF[] { 
                    P0,
                    P1,
                    P2,
                    P3
                };
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                            case ST_CREATING:
                                this.SelectOne(e.FactorPoint);
                                if (this.CurrentLayer.SelectedElements.Count == 1)
                                {
                                    PathElement v_l = this.CurrentLayer.SelectedElements[0] as PathElement;//.GetFirstElement(e.FactorPoint);
                                    if (v_l != null)
                                    {
                                        this.Element = v_l as PathElement;
                                        if (this.Element != null)
                                        {
                                            this.CurrentSurface.ElementToConfigure = this.Element;
                                            this.State = ST_EDITING;
                                            RectangleF rc = this.Element.GetBound();
                                            rc.Height = rc.Height / 2.0f;
                                            //get int point
                                            Vector2f[] tps = CoreMathOperation.GetPoints(rc);
                                            P0 = tps[3];
                                            P1 = new Vector2f(P0.X + rc.Width / 2.0f, P0.Y);
                                            P2 = P1;
                                            P3 = tps[2];
                                            this.m_initPath = this.Element.GetPath().Clone() as GraphicsPath;
                                            this.m_initPoints = this.m_initPath.PathPoints;
                                            this.m_initByte = this.m_initPath.PathTypes;
                                            this.GenerateSnippets();
                                            this.InitSnippetsLocation();
                                            this.EnabledSnippet();                                            
                                            this.CurrentSurface.Invalidate();
                                        }
                                    }
                                }
                                break;
                            case ST_EDITING:
                                if ((this.Snippet !=null) && (this.Snippet.Demand != DM_NONE))
                                {
                                    this.UpdateSnippetEdit(e);
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                if ((this.Snippet != null) && (this.Snippet.Demand != DM_NONE))                            
                                {
                                   // this.UpdateSnippetEdit(e);
                                    lock (this)
                                    {
                                        this.Snippet.Location = e.Location;
                                        this.GetType().GetProperty("P" + this.Snippet.Demand).SetValue(this, e.FactorPoint, null);
                                        this.getPointDef(P0, P1, P2, P3);
                                        this.CurrentSurface.Invalidate();
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                this.Snippet.Location = e.Location;
                this.GetType().GetProperty("P" + this.Snippet.Demand).SetValue(this, e.FactorPoint, null);
                this.getPointDef(P0, P1, P2, P3);
                SetTempPath();
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_EDITING:
                                if (this.Snippet !=null )
                                {
                                    this.UpdateSnippetEdit(e);
                                }
                                break;
                        }
                        break;
                    case MouseButtons.Right:
                        if (this.State == ST_EDITING)
                        {
                            //
                            EndEdition();
                            this.CurrentSurface.Invalidate();
                        }
                        break;
                }
            }
            void getPointDef(
           Vector2f t0, Vector2f t1, Vector2f t2, Vector2f t3)
            {
                //[[ Calculate coefficients A thru H from the control points ]]
                float A = t3.X - (3 * t2.X) + (3 * t1.X) - t0.X;
                float B = 3 * t2.X - (6 * t1.X) + 3 * t0.X;
                float C = 3 * t1.X - 3 * t0.X;
                float D = t0.X;
                float E = t3.Y - 3 * t2.Y + 3 * t1.Y - t0.Y;
                float F = 3 * t2.Y - 6 * t1.Y + 3 * t0.Y;
                float G = 3 * t1.Y - 3 * t0.Y;
                float H = t0.Y;
                this.DisposeTempPath();
                this.m_tempPath = null;// this.m_initPath.Clone() as GraphicsPath;
                // the baseline should start at 0,0, so the next line is not quite correct
                RectangleF textBounds = this.m_initPath.GetBounds();
                List<PointF > cpt = new List<PointF >();
                float v_offx = textBounds.X;
                float v_offy = textBounds.Y + textBounds.Height / 2.0f;
                for (int i = 0; i < this.m_initPoints .Length; i++)
                {
                    Vector2f pt = m_initPoints[i];
                    float textX = pt.X - v_offx;
                    float textY = pt.Y - v_offy;
                    // normalize the x coordinate into the parameterized value
                    // with a domain between 0 and 1.
                    float t = textX / textBounds.Width;
                    // calculate spline point for parameter t
                    float Sx = (float)(A * Math.Pow(t, 3) + B * Math.Pow(t, 2) + C * t + D);
                    float Sy = (float)(E * Math.Pow(t, 3) + F * Math.Pow(t, 2) + G * t + H);
                    // calculate the tangent vector for the point
                    float Tx = (float)(3 * A * Math.Pow(t, 2) + 2 * B * t + C);
                    float Ty = (float)(3 * E * Math.Pow(t, 2) + 2 * F * t + G);
                    // rotate 90 or 270 degrees to make it a perpendicular
                    float Px = -Ty;
                    float Py = Tx;
                    // normalize the perpendicular into a unit vector
                    float magnitude = (float)Math.Sqrt(Px * Px + Py * Py);
                    Px = Px / magnitude;
                    Py = Py / magnitude;
                    // assume that input text point y coord is the "height" or
                    // distance from the spline. Multiply the perpendicular vector
                    // with y. it becomes the new magnitude of the vector.
                    Px *= textY;
                    Py *= textY;
                    // translate the spline point using the resultant vector
                    float finalX = Px + Sx;
                    float finalY = Py + Sy;
                    // I wish it were this easy, actually need
                    // to create a new path.
                    cpt.Add(new Vector2f(finalX, finalY));
                    //if message require
                    //Application.DoEvents();
                }
                m_tempPath = new GraphicsPath(
                    cpt.ToArray(),
                    m_initByte );
                // draw the transformed text path
            }
            protected override void OnKeyPress(System.Windows.Forms.KeyPressEventArgs e)
            {
                base.OnKeyPress(e);
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                this.AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
                this.RegSnippets[1].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
                this.RegSnippets[2].Shape = IGK.DrSStudio.WinUI.enuSnippetShape.Circle;
            }
            protected override void InitSnippetsLocation()
            {
                //if (this.m_workingThread == Thread.CurrentThread)
                //{
                //    (this.CurrentSurface as Control).Invoke(new MethodInvoker(InitSnippetLocation));
                //    return;
                //}
                base.InitSnippetsLocation();
                if ((this.State == ST_EDITING) && (this.RegSnippets.Count >= 4))
                {
                    this.RegSnippets[0].Location=(CurrentSurface.GetScreenLocation(P0));
                    this.RegSnippets[1].Location=(CurrentSurface.GetScreenLocation(P1));
                    this.RegSnippets[2].Location=(CurrentSurface.GetScreenLocation(P2));
                    this.RegSnippets[3].Location=(CurrentSurface.GetScreenLocation(P3));
                }
            }
        }
    }
}

