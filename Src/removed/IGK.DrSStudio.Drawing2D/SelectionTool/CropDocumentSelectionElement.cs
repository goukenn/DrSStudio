

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CropDocumentSelectionElement.cs
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
using System.Windows.Forms;
using System.Drawing ;
namespace IGK.DrSStudio.Drawing2D.SelectionTool
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    [Core2DDrawingSelectionItem("CropDocumentSelection",
    typeof(Mecanism))]
    sealed class CropDocumentSelectionElement : Core2DDrawingObjectBase
    {
        class Mecanism : Core2DDrawingMecanismBase
        {
            RectangleF m_docRc;
            bool m_croping;
            public override void EndEdition()
            {
                base.EndEdition();
                this.CurrentSurface.Invalidate();
            }
            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                switch ((Keys)e.KeyChar)
                {
                    case Keys.Enter:
                        if (!this.m_docRc.IsEmpty)
                        {
                            this.CurrentSurface.CurrentDocument.Translate(
                                -this.m_docRc.X,
                                -this.m_docRc.Y);
                            this.CurrentSurface.CurrentDocument.SetSize(
                                (int)this.m_docRc.Width,
                                (int)this.m_docRc.Height);
                            this.m_docRc = this.CurrentSurface.CurrentDocument.Bounds;
                            this.InitSnippetsLocation();
                            this.CurrentSurface.Invalidate();
                            e.Handled = true;
                        }
                        break;
                    case Keys.Escape:
                        e.Handled = true;
                        this.EndEdition();
                        this.GoToDefaultTool();
                        break;
                }
                base.OnKeyPress(e);
            }
            protected internal override void RegisterSurface(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                this.m_docRc = surface.CurrentDocument.Bounds;
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.CurrentSurface.Invalidate();
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                Region rg = new Region();
                rg.Exclude(CurrentSurface.GetScreenBound(m_docRc));
                e.Graphics.FillRegion(
                    CoreBrushRegister.GetBrush(Color.FromArgb(128, Color.Black)),
                    rg);
                rg.Dispose();
            }
            protected override void GenerateSnippets()
            {
                this.DisposeSnippet();
                for (int i = 0; i < 8; i++)
                {
                    AddSnippet(CurrentSurface.CreateSnippet(this, i, i));    
                }
            }
            protected override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
                Vector2f [] cp = CoreMathOperation.GetResizePoints (this.CurrentSurface.GetScreenBound(m_docRc));
                for (int i = 0; i < cp.Length; i++)
                {
                    this.RegSnippets[i].Location = (cp[i]);
                }
                //this.RegSnippets[0].Location = (cp[0]);
                //this.RegSnippets[2].Location = (cp[1]);
                //this.RegSnippets[2].Location = (cp[1]);
                //this.RegSnippets[4].Location = (cp[2]);
                //this.RegSnippets[6].Location = (cp[3]);
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
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
                    case MouseButtons.Left:
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
                this.CurrentSurface.Invalidate();
            }
            private void EndCroping(CoreMouseEventArgs e)
            {
                this.m_croping = false;
                this.EnabledSnippet();
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
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
                RectangleF v_rc = this.m_docRc;
                RectangleF v_nRectangle = RectangleF.Empty;
                switch (this.Snippet.Demand)
                {
                    case 0:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new PointF(v_rc.Right, v_rc.Bottom));
                        break;
                    case 1:
                        this.m_docRc = CoreMathOperation.GetBounds(new Vector2f(v_rc.Left ,(float)Math.Min ( e.FactorPoint .Y , v_rc.Bottom )),   new PointF(v_rc.Right , v_rc.Bottom));
                        break;
                    case 2:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new PointF(v_rc.Left, v_rc.Bottom));
                        break;
                    case 3:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location  , new PointF(Math.Max (e.FactorPoint.X, v_rc .X ),  v_rc.Bottom));
                        break;
                    case 4:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location, e.FactorPoint);
                        break;
                    case 5:
                        this.m_docRc = CoreMathOperation.GetBounds(v_rc.Location, new Vector2f(v_rc.Right, Math.Max(e.FactorPoint.Y, v_rc.Y)));
                        break;
                    case 6:
                        this.m_docRc = CoreMathOperation.GetBounds(e.FactorPoint, new PointF(v_rc.Right, v_rc.Y));
                        break;
                    case 7:
                        this.m_docRc = CoreMathOperation.GetBounds(new Vector2f(Math.Min (e.FactorPoint.X , v_rc .Right ), v_rc.Y), new PointF(v_rc.Right, v_rc.Bottom ));
                        break;
                    default:
                        break;
                }
                this.CurrentSurface.Invalidate();
            }
        }
    }
}

