

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: MangaRoundBullet.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;

namespace IGK.DrSStudio.MangaStuffAddIn.WorkingElements
{
    using IGK.ICore.MecanismActions;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D.Segments;
    [MangaStuffElement ("MangaRoundBullet", typeof (Mecanism))]
    public class MangaRoundBullet : RoundRectangleElement
    {
            enuBulletDirection m_Direction;
            private float m_defPoint1;
            private Vector2f m_defPointer;
            private float m_defPoint2;

            [CoreXMLAttribute()]
            [CoreConfigurableProperty(Group = CoreConstant.PARAM_DEFINITION)]
            public enuBulletDirection Direction
            {
                get
                {
                    return m_Direction;
                }
                set
                {
                    if (value != m_Direction)
                    {
                        m_Direction = value;
                        InitDefinition(this.Bounds);
                        OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                    }

                }
            }

            [CoreXMLAttribute()]
            public float DefPoint1
            {
                get
                {
                    return m_defPoint1;
                }
                set
                {
                    if (m_defPoint1 != value)
                    {
                        m_defPoint1 = value;
                        OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                        
                    }
                }
            }
            [CoreXMLAttribute()]
            public Vector2f DefPointer
            {
                get
                {
                    return m_defPointer;
                }
                set
                {
                    if (m_defPointer != value)
                    {
                        m_defPointer = value;
                        OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                    }
                }
            }
            [CoreXMLAttribute()]
            public float DefPoint2
            {
                get
                {
                    return m_defPoint2;
                }
                set
                {
                    if (m_defPoint2 != value)
                    {
                        m_defPoint2 = value;
                        OnPropertyChanged (CoreWorkingObjectPropertyChangedEventArgs.Definition );
                    }
                }
            }


            protected override void BuildBeforeResetTransform()
            {
                
                Matrix m = GetMatrix();
                if (m.IsIdentity) return;

                base.BuildBeforeResetTransform ();

                Vector2f[] pts = new Vector2f[] { getDefPoint(DefPoint1), getDefPoint(DefPoint2), m_defPointer };

                m.TransformPoints(pts);

                switch (this.Direction)
                {
                    case enuBulletDirection.Left:
                    case enuBulletDirection.Right:
                        m_defPoint1 = pts[0].Y;
                        m_defPoint2 = pts[1].Y;
                        break;
                    case enuBulletDirection.Top:
                    case enuBulletDirection.Bottom:
                        m_defPoint1 = pts[0].X;
                        m_defPoint2 = pts[1].X;
                        break;
                }
                m_defPointer = pts[2];
            }


            private void InitDefinition(Rectanglef rectangle)
            {
                switch (this.Direction)
                {
                    case enuBulletDirection.Left:
                        this.m_defPoint1 = rectangle.Y + this.TopLeft.Y ;
                        this.m_defPoint2 = rectangle.Y + rectangle.Height - BottomLeft.Y;
                        this.m_defPointer = CoreMathOperation.GetInnerPoint(
                            rectangle.Location,
                            new Vector2f(rectangle.X, rectangle.Y + rectangle.Height),
                            4);
                        break;
                    case enuBulletDirection.Bottom:
                        this.m_defPoint1 = rectangle.X + TopLeft.X;
                        this.m_defPoint2 = rectangle.X + rectangle.Width - TopRight.X;
                        this.m_defPointer = CoreMathOperation.GetInnerPoint(
                            new Vector2f(rectangle.X, rectangle.Y + rectangle.Height),
                            new Vector2f(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                            4);

                        break;
                    case enuBulletDirection.Right:
                        this.m_defPoint1 = rectangle.Y + TopRight.Y;
                        this.m_defPoint2 = rectangle.Y + rectangle.Height - BottomRight.Y;

                        this.m_defPointer = CoreMathOperation.GetInnerPoint(
                            new Vector2f(rectangle.X + rectangle.Width, rectangle.Y),
                            new Vector2f(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height),
                            4);

                        break;
                    case enuBulletDirection.Top:
                        this.m_defPoint1 = rectangle.X + TopLeft.X;
                        this.m_defPoint2 = rectangle.X + rectangle.Width - TopRight.X;
                        this.m_defPointer = CoreMathOperation.GetInnerPoint(
                            rectangle.Location,
                            new Vector2f(rectangle.X + rectangle.Width, rectangle.Y),
                            4);
                        break;
                }
            }

            protected override void InitGraphicPath(CoreGraphicsPath path)
            {

                path.Reset();
               // this.InitDefinition(this.Bounds );
                float vtl_dx = Math.Max(this.TopLeft.X * 2, 0.1f);
                float vtl_dy = Math.Max(this.TopLeft.Y * 2, 0.1f);

                float vtr_dx = Math.Max(this.TopRight.X * 2, 0.1f);
                float vtr_dy = Math.Max(this.TopRight.Y * 2, 0.1f);

                float vbr_dx = Math.Max(this.BottomRight.X * 2, 0.1f);
                float vbr_dy = Math.Max(this.BottomRight.Y * 2, 0.1f);

                float vbl_dx = Math.Max(this.BottomLeft.X * 2, 0.1f);
                float vbl_dy = Math.Max(this.BottomLeft.Y * 2, 0.1f);

                Rectanglef v_rect = this.Bounds;


                PathSegment pSegment = new PathSegment();
                //pSegment.AddArc(new Rectanglef(
                //    v_rect.X - vtl_dx / 2.0f, v_rect.Y - vtl_dy / 2.0f,
                //    vtl_dx, vtl_dy), 90, 270.0f);
                //pSegment.AddArc(new Rectanglef(
                //    v_rect.X + v_rect.Width - vtr_dx / 2.0f,
                //        v_rect.Y - vtr_dy / 2.0f,
                //    vtr_dx, vtr_dy),
                //    180.0f, 270.0f);
                //pSegment.AddArc(new Rectanglef(
                //    v_rect.X + v_rect.Width - vbr_dx / 2.0f,
                //    v_rect.Y + v_rect.Height - vbr_dy / 2.0f,
                //    vbr_dx, vbr_dy), -90.0f, 270.0f);
                //pSegment.AddArc(
                //    new Rectanglef(
                //        v_rect.X - vbl_dx / 2.0f, v_rect.Y + v_rect.Height - vbl_dy / 2.0f,
                //        vbl_dx, vbl_dy), 0.0f, 270.0f);
                //pSegment.CloseFigure();
                //path.AddSegment(pSegment);

                pSegment.AddArc(new Rectanglef(v_rect.Location, new Size2f(vtl_dx, vtl_dy)), 180.0f, 90.0f);

                if (Direction == enuBulletDirection.Top)
                    pSegment.AddLines(new Vector2f[] {
                    new Vector2f(this.DefPoint1, v_rect.Y ),
                    this.DefPointer , 
                    new Vector2f(this.DefPoint2, v_rect.Y ) });
                pSegment.AddArc(new Rectanglef(new Vector2f(v_rect.X + v_rect.Width - vtr_dx, v_rect.Y),

                    new Size2f(vtr_dx, vtr_dy)), -90.0f, 90.0f);


                if (Direction == enuBulletDirection.Right)
                    pSegment.AddLines(new Vector2f[] {
                    new Vector2f(v_rect.X + v_rect.Width ,this.DefPoint1),
                    this.DefPointer , 
                    new Vector2f(v_rect.X + v_rect.Width , this.DefPoint2 ) });
                pSegment.AddArc(new Rectanglef(new Vector2f(v_rect.X + v_rect.Width - vbr_dx, v_rect.Y + v_rect.Height - vbr_dy),
                    new Size2f(vbr_dx, vbr_dy)), 0.0f, 90.0f);


                if (Direction == enuBulletDirection.Bottom)
                    pSegment.AddLines(new Vector2f[] {
                    new Vector2f(this.DefPoint2, v_rect.Y + v_rect.Height),                    
                    this.DefPointer , 
                    new Vector2f(this.DefPoint1, v_rect.Y + v_rect.Height)
                     });
                pSegment.AddArc(new Rectanglef(new Vector2f(v_rect.X, v_rect.Y + v_rect.Height - vbl_dy), new Size2f(vbl_dx, vbl_dy)), 90.0f, 90.0f);


                if (Direction == enuBulletDirection.Left)
                    pSegment.AddLines(new Vector2f[] {
                    new Vector2f(v_rect.X , this.DefPoint2 ),
                    this.DefPointer , 
                    new Vector2f(v_rect.X ,this.DefPoint1),
                     });
                pSegment.CloseFigure();
                path.AddSegment(pSegment);
                path.CloseFigure();

            }

            public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
            {
                parameters = base.GetParameters(parameters);               
                //parameters.AddGroup(CoreConstant.PARAM_DEFINITION);
                //g.AddItem(GetType().GetProperty("Direction"));
                return parameters;
            }
            new class Mecanism : RoundRectangleElement.Mecanism
            {
                const int DEF_SIZE_BOTTOM = 30;
                const int DEF_POINT1 = DEF_SIZE_BOTTOM + 1;
                const int DEF_POINT2 = DEF_POINT1 + 2;
                const int DEF_POINTER = DEF_POINT1 + 1;

                public new MangaRoundBullet Element
                {
                    get
                    {
                        return base.Element as MangaRoundBullet;
                    }
                    set
                    {
                        base.Element = value;
                    }
                }
                protected override void InitSnippetsLocation()
                {
                    base.InitSnippetsLocation();

                    this.RegSnippets[DEF_POINT1].Location = (CurrentSurface.GetScreenLocation(this.Element.getDefPoint(this.Element.DefPoint1)));
                    this.RegSnippets[DEF_POINT2].Location = (CurrentSurface.GetScreenLocation(this.Element.getDefPoint(this.Element.DefPoint2)));
                    this.RegSnippets[DEF_POINTER].Location = (CurrentSurface.GetScreenLocation(this.Element.m_defPointer));
                }
                protected override void UpdateDrawing(CoreMouseEventArgs e)
                {

                    this.EndPoint = e.FactorPoint;
                    this.Element.InitDefinition(
                        CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint));
                    base.UpdateDrawing(e);
                }
                protected override void GenerateSnippets()
                {
                    base.GenerateSnippets();
                    this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DEF_POINT1, DEF_POINT1));
                    this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DEF_POINT2, DEF_POINT2));
                    this.AddSnippet(this.CurrentSurface.CreateSnippet(this, DEF_POINTER, DEF_POINTER));
                }
                protected override void OnMouseMove(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:

                            Rectanglef rc = this.Element.Bounds;
                            if (this.Snippet != null)
                            {
                                switch (this.Snippet.Demand)
                                {
                                    case DEF_POINT1:
                                        switch (this.Element.Direction)
                                        {
                                            case enuBulletDirection.Left:
                                                if ((e.FactorPoint.Y >= (rc.Y + Element.TopLeft.Y)) &&
                                                    (e.FactorPoint.Y <= (Element.m_defPoint2)))
                                                {
                                                    this.Element.m_defPoint1 = e.FactorPoint.Y;
                                                }
                                                break;
                                            case enuBulletDirection.Right:
                                                if ((e.FactorPoint.Y >= (rc.Y + Element.TopRight.Y)) &&
                                                    (e.FactorPoint.Y <= (Element.m_defPoint2)))
                                                {
                                                    this.Element.m_defPoint1 = e.FactorPoint.Y;
                                                }
                                                break;
                                            case enuBulletDirection.Top:
                                                if ((e.FactorPoint.X >= (rc.X + Element.TopLeft.X)) &&
                                                    (e.FactorPoint.X <= (Element.m_defPoint2)))
                                                {
                                                    this.Element.m_defPoint1 = e.FactorPoint.X;
                                                }

                                                break;
                                            case enuBulletDirection.Bottom:
                                                if ((e.FactorPoint.X >= (rc.X + Element.BottomLeft.X)) &&
                                                    (e.FactorPoint.X <= (Element.m_defPoint2)))
                                                {
                                                    this.Element.m_defPoint1 = e.FactorPoint.X;
                                                }
                                                break;
                                        }
                                        //this.Element.m_defPoint1 = e.FactorPoint;
                                        this.Snippet.Location = (CurrentSurface.GetScreenLocation(this.Element.getDefPoint(this.Element.DefPoint1)));
                                        this.Element.InitElement();
                                        this.CurrentSurface.Invalidate();
                                        return;
                                    case DEF_POINT2:
                                        this.Element.Invalidate(false);
                                        switch (this.Element.Direction)
                                        {
                                            case enuBulletDirection.Left:
                                                if ((e.FactorPoint.Y >= Element.DefPoint1) &&
                                                    (e.FactorPoint.Y <= (rc.Y + rc.Height - Element.BottomLeft.Y)))
                                                {
                                                    this.Element.m_defPoint2 = e.FactorPoint.Y;
                                                }
                                                break;
                                            case enuBulletDirection.Right:
                                                if ((e.FactorPoint.Y >= Element.DefPoint1) &&
                                                    (e.FactorPoint.Y <= (rc.Y + rc.Height - Element.BottomRight.Y)))
                                                {
                                                    this.Element.m_defPoint2 = e.FactorPoint.Y;
                                                }
                                                break;
                                            case enuBulletDirection.Top:
                                                if ((e.FactorPoint.X >= Element.DefPoint1) &&
                                                    (e.FactorPoint.X <= (rc.X + rc.Width - Element.TopRight.X)))
                                                {
                                                    this.Element.m_defPoint2 = e.FactorPoint.X;
                                                }
                                                break;
                                            case enuBulletDirection.Bottom:
                                                if ((e.FactorPoint.X >= Element.DefPoint1) &&
                                                    (e.FactorPoint.X <= (rc.X + rc.Width - Element.TopRight.X)))
                                                {
                                                    this.Element.m_defPoint2 = e.FactorPoint.X;
                                                }
                                                break;
                                        }
                                        this.Snippet.Location = e.Location;//.SetLocation(CurrentSurface.GetScreenLocation(this.Element.getDefPoint(this.Element.DefPoint2)));
                                        this.Element.InitElement();
                                        this.Element.Invalidate(true);
                                        return;
                                    case DEF_POINTER:
                                        this.Element.Invalidate(false);
                                        this.Element.m_defPointer = e.FactorPoint;
                                        this.Snippet.Location = e.Location; //this.RegSnippets[Demand].SetLocation(e.Location);
                                        this.Element.InitElement();
                                        this.Element.Invalidate(true);
                                        return;
                                }
                                break;
                            }
                            break;
                    }
                    base.OnMouseMove(e);
                }

                protected override void GenerateActions()
                {
                    base.GenerateActions();
                    this.AddAction(enuKeys.T, new MangaToggleState(this));
                }

                class MangaToggleState : CoreMecanismActionBase
                {
                    private Mecanism mecanism;

                    public MangaToggleState(Mecanism mecanism)
                    {

                        this.mecanism = mecanism;
                    }
                    protected override bool PerformAction()
                    {
                        var e = this.mecanism.Element;
                        if (e == null) return false;
                        switch (e.Direction)
                        {
                            case enuBulletDirection.Left:
                                e.Direction = enuBulletDirection.Top;
                                break;
                            case enuBulletDirection.Top:
                                e.Direction = enuBulletDirection.Right;
                                break;
                            case enuBulletDirection.Right:
                                e.Direction = enuBulletDirection.Bottom;
                                break;
                            case enuBulletDirection.Bottom:

                                e.Direction = enuBulletDirection.Left;
                                break;

                        }
                        this.mecanism.Element.InitElement();
                        this.mecanism.Invalidate();
                        return true;

                    }
                }
            }



            internal Vector2f getDefPoint(float w)
            {
                Vector2f pts = new Vector2f();
                Rectanglef def = this.Bounds;
                switch (this.Direction)
                {
                    case enuBulletDirection.Left:
                        pts = new Vector2f(def.X, w);
                        break;
                    case enuBulletDirection.Right:
                        pts = new Vector2f(def.X + def.Width, w);
                        break;
                    case enuBulletDirection.Bottom:
                        pts = new Vector2f(w, def.Y + def.Height);
                        break;
                    case enuBulletDirection.Top:
                        pts = new Vector2f(w, def.Y);
                        break;
                }
                return pts;
            }
        }
    
}
