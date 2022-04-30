

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDualBrushBoundElement.cs
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
file:Core2DDrawingDualBrushBoundElement.cs
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
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// represent the base class for dual bound element. 
    /// </summary>
    public abstract class Core2DDrawingDualBrushBoundElement : 
        Core2DDrawingLayeredDualBrushElement
    {
        private Rectanglef m_bound;
        protected override void BuildBeforeResetTransform()
        {            
            Matrix m = this.GetMatrix();
            if (m.IsIdentity)
                return;
            this.m_bound = CoreMathOperation.GetBounds(CoreMathOperation.ApplyMatrix(this.m_bound, m));
        }
        public Core2DDrawingDualBrushBoundElement():base()
        {
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g = parameters["General"];
            if (g!=null)
            g.AddItem("Bound", "lb.Bound.caption", ProcBoundChanged);
            return parameters;
        }
        private void ProcBoundChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            if (e.Value == null) return;
            string[] t = e.Value.ToString().Split(';');
            if (t.Length == 4)
            {
                CoreUnit x = t[0];
                CoreUnit y = t[1];
                CoreUnit z = t[2];
                CoreUnit w = t[3];
                this.m_bound = new Rectanglef(x.GetValue(enuUnitType.px),
                    y.GetValue(enuUnitType.px),
                    z.GetValue(enuUnitType.px),
                    w.GetValue(enuUnitType.px));
                this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public new class Mecanism : Core2DDrawingLayeredDualBrushElement.Mecanism
        {
            public new Core2DDrawingDualBrushBoundElement Element
            {
                get { return base.Element as Core2DDrawingDualBrushBoundElement; }
                set { base.Element = value; }
            }
            public Mecanism()
            {
                this.DesignMode = true;
            }
            protected override void OnElementPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(e);
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                            case ST_CREATING:
                                this.CreateAndAddElement(e);
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                {
                                }
                                else
                                {
                                    if ((this.Element != null) && (this.Element.Contains(e.FactorPoint)))
                                    {
                                        this.BeginMove(e);
                                    }
                                    else
                                    {
                                        if (this.Snippet == null)
                                        {
                                            this.State = ST_NONE;
                                            this.CreateAndAddElement(e);
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
            private void CreateAndAddElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.Element = this.CreateNewElement() as Core2DDrawingDualBrushBoundElement;
                if (this.Element != null)
                {
                    this.StartPoint = e.FactorPoint;
                    this.EndPoint = e.FactorPoint;
                    this.CurrentSurface.CurrentDocument.CurrentLayer.Elements.Add(this.Element);
                    this.CurrentSurface.CurrentDocument.CurrentLayer.Select(this.Element);
                    this.Element.InitElement();                    
                    this.State = ST_CREATING;
                }
            }
            protected override void OnMouseMove(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                                break;
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                    UpdateSnippetElement(e);
                                break;
                            case ST_MOVING :
                                if (this.Element != null)
                                {
                                    this.UpdateMove(e);
                                }
                                else
                                    this.State = ST_NONE;
                                break;
                        }
                        break;
                }
            }
            protected override void OnMouseUp(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_NONE:
                                break;
                            case ST_CREATING:
                                UpdateCreateElement(e);
                                this.State = ST_EDITING;
                                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                break;
                            case ST_EDITING:
                                if (this.Snippet != null)
                                {
                                    UpdateSnippetElement(e);
                                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                    this.InitSnippetsLocation();
                                }
                                break;
                            case ST_MOVING:
                                if (this.Element != null)
                                {
                                    EndMove(e);
                                }
                                break;
                        }
                        break;
                }
            }
            protected override void UpdateCreateElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                this.Element.m_bound =
                    CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void UpdateSnippetElement(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                Rectanglef v_rc = this.Element.m_bound;
                this.Snippet.Location = e.Location;
                switch (this.Snippet.Index)
                {
                    case 3:
                        if (e.FactorPoint.X < v_rc.Right)
                        {
                            v_rc.Width = Math.Abs(v_rc.Right - e.FactorPoint.X);
                            v_rc.X = e.FactorPoint.X;
                        }
                        break;
                    case 0:
                        if (e.FactorPoint.Y < v_rc.Bottom)
                        {
                            v_rc.Height = Math.Abs(v_rc.Bottom - e.FactorPoint.Y);
                            v_rc.Y = e.FactorPoint.Y;
                        }
                        break;
                    case 2:
                        if (e.FactorPoint.Y > v_rc.Top)
                        {
                            v_rc.Height = Math.Abs(e.FactorPoint.Y - v_rc.Top);
                        }
                        break;
                    case 1:
                        if (e.FactorPoint.X > v_rc.Left)
                        {
                            v_rc.Width = Math.Abs(e.FactorPoint.X - v_rc.Left);
                            //v_rc.X = e.FactorPoint.X;
                        }
                        break;
                }
                this.Element.m_bound = v_rc;
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
            }
            protected override void InitSnippetsLocation()
            {
                Vector2f [] t = CoreMathOperation.GetResizePoints(this.Element.m_bound);
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
                this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
                this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
            }
        }
        public new class Mecanism<T> : Mecanism
            where T : Core2DDrawingDualBrushBoundElement
        {
            public new T Element {
                get {
                    return (T)base.Element;
                }
            }
        }
        #region ICore2DRectangleElement Members
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public Rectanglef Bound
        {
            get
            {
                return this.m_bound;
            }
            set
            {
                if (!this.m_bound.Equals(value))
                {
                    this.m_bound = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition );
                }
            }
        }
        #endregion
        protected override void GeneratePath()
        {
        }
        protected void SetBound(Rectanglef bound)
        {
            this.m_bound = bound;
        }
    }
}

