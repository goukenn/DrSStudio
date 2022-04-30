

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CropDocumentSelectionElement.cs
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
file:CropDocumentSelectionElement.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D.SelectionTool
{
    using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.WinUI;
using IGK.ICore.MecanismActions;
    using IGK.ICore.GraphicModels;
    [Core2DDrawingSelectionAttribute("CropDocumentSelection",
    typeof(Mecanism))]
    sealed class CropDocumentSelectionElement : Core2DDrawingObjectBase
    {
        class Mecanism : Core2DDrawingSelectionMecanismBase
        {
            Rectanglef m_docRc;
            bool m_croping;
            public override void EndEdition()
            {
                base.EndEdition();
                if (this.CurrentDocument != null)
                {
                    this.m_docRc = this.CurrentDocument.Bounds;
                }
                this.GotoDefaultTool();
                this.Invalidate();
                
            }
            protected override void GenerateActions()
            {                
                base.GenerateActions();
                this.AddAction(enuKeys.Enter, new ValidateCropSelection(this));
            }
            class ValidateCropSelection : CoreMecanismActionBase
            {
                private Mecanism mecanism;
                public ValidateCropSelection(Mecanism mecanism)
                {
                    this.mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    if (!mecanism.m_docRc.IsEmpty)
                    {
                        mecanism.CurrentSurface.CurrentDocument.Translate(
                            -mecanism.m_docRc.X,
                            -mecanism.m_docRc.Y);
                        mecanism.CurrentSurface.CurrentDocument.SetSize(
                            (int)mecanism.m_docRc.Width,
                            (int)mecanism.m_docRc.Height);
                        mecanism.m_docRc = mecanism.CurrentSurface.CurrentDocument.Bounds;
                        mecanism.InitSnippetsLocation();
                        mecanism.Invalidate();
                        return true;
                    }
                    return false;
                }
            }
            public override bool Register(ICore2DDrawingSurface surface)
            {
                bool v = base.Register(surface);
                this.m_docRc = surface.CurrentDocument.Bounds;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.CurrentSurface.RefreshScene();
                return v && true;
            }
            public override void Render(ICoreGraphics device)
            {
                CoreGraphicsPath p = new CoreGraphicsPath();
                p.AddRectangle(this.CurrentSurface.Bounds);
                //CoreGraphicsRegion rg = new CoreGraphicsRegion();
                //rg.Exclude(CurrentSurface.GetScreenBound(m_docRc));
                p.AddRectangle(CurrentSurface.GetScreenBound(m_docRc));
                device.FillPath(Colorf.FromFloat(0.5f, 0.0f), p);
                p.Dispose();
                //device.FillRegion(
                //    Colorf.FromFloat (0.5f, 0.0f),
                //    rg);
                //rg.Dispose();
            }
            protected internal override void GenerateSnippets()
            {
                this.DisposeSnippet();
                for (int i = 0; i < 8; i++)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, i, i));    
                }
            }
            protected override void OnCurrentSurfaceSizeZoomChanged(EventArgs e)
            {
                base.OnCurrentSurfaceSizeZoomChanged(e);
                this.InitSnippetsLocation();
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                Vector2f [] cp = CoreMathOperation.GetResizePoints (this.CurrentSurface.GetScreenBound(m_docRc));
                for (int i = 0; i < cp.Length; i++)
                {
                    this.RegSnippets[i].Location = (cp[i]);
                }              
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet != null)
                        {
                            this.Snippet.Location = (e.Location);
                            this.UpdateSize(e);
                        }
                        else if (this.m_croping)
                            UpdateCroping(e);
                        break;
                }
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet != null)
                        {
                            this.Snippet.Location = (e.Location);
                            this.UpdateSize(e);
                        }
                        else if (!this.m_croping )
                            BeginCroping(e);
                        break;
                }
            }
            private void BeginCroping(CoreMouseEventArgs e)
            {
                this.m_croping = true;
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.DisableSnippet();
            }
            private void UpdateCroping(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                this.m_docRc = CoreMathOperation.GetBounds(this.EndPoint, this.StartPoint);
                this.CurrentSurface.RefreshScene();
            }
            private void EndCroping(CoreMouseEventArgs e)
            {
                this.m_croping = false;
                this.EnabledSnippet();
                this.InitSnippetsLocation(); 
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        if (this.Snippet != null)
                        {
                            this.Snippet.Location = (e.Location);
                            this.UpdateSize(e);
                            this.InitSnippetsLocation();
                        }
                        else
                            EndCroping(e);
                        break;
                }
            }
            void UpdateSize(CoreMouseEventArgs e)
            {
                Rectanglef v_rc = this.m_docRc;
                Rectanglef v_nRectangle = Rectanglef.Empty;
                switch (this.Snippet.Demand)
                {
                    case 0:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new Vector2f(v_rc.Right, v_rc.Bottom));
                        break;
                    case 1:
                        this.m_docRc = CoreMathOperation.GetBounds(new Vector2f(v_rc.Left ,(float)Math.Min ( e.FactorPoint .Y , v_rc.Bottom )),   new Vector2f(v_rc.Right , v_rc.Bottom));
                        break;
                    case 2:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new Vector2f(v_rc.Left, v_rc.Bottom));
                        break;
                    case 3:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location  , new Vector2f(Math.Max (e.FactorPoint.X, v_rc .X ),  v_rc.Bottom));
                        break;
                    case 4:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location, e.FactorPoint);
                        break;
                    case 5:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location, new Vector2f(v_rc.Right, Math.Max(e.FactorPoint.Y, v_rc.Y)));
                        break;
                    case 6:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new Vector2f(v_rc.Right, v_rc.Y));
                        break;
                    case 7:
                        this.m_docRc = CoreMathOperation.GetBounds(new Vector2f(Math.Min (e.FactorPoint.X , v_rc .Right ), v_rc.Y), new Vector2f(v_rc.Right, v_rc.Bottom ));
                        break;
                    default:
                        break;
                }
                this.CurrentSurface.RefreshScene();
            }
        }
    }
}

