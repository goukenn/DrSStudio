

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WidenPath.cs
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
file:WidenPath.cs
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
using System; 
using IGK.ICore;
using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace IGK.DrSStudio.Drawing2D.Selection
{

    using IGK.ICore;
    using IGK.ICore.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.MecanismActions;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Drawing2D;

    /// <summary>
    /// mecanism that works only on a single path element
    /// </summary>
    [Core2DDrawingSelection("WidenPath", typeof(Mecanism), IsVisible = true)]
    class WidenPath : Core2DDrawingDualBrushElement, ICore2DDrawingVisitable 
    {
        Mecanism m_targetMecanism;
        CoreGraphicsPath m_path;
        public override void Dispose()
        {
            if (m_path != null)
            {
                this.m_path.Dispose();
            }
            base.Dispose();
        }
        internal  WidenPath(Mecanism target)
        {
            this.m_targetMecanism = target;
            this.FillBrush.SetSolidColor(new Colorf(0.5f, 0.2f, 0.4f, 1.0f));
            this.StrokeBrush.SetSolidColor(new Colorf(0.5f, 0.2f, 0.4f, 0.1f));
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections c = base.GetParameters(parameters);
            c.AddGroup(CoreConstant.PARAM_DEFINITION).AddItem(GetType().GetProperty("Flatness")); 
            return c;            
        }
        public override enuBrushSupport BrushSupport
        {
            get
            {
                
                return enuBrushSupport.StrokeOnly;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Stroke)
                return this.StrokeBrush;
            return null;
        }
        internal new class Mecanism : PathElementMecanism
        {
            private WidenPath m_flattenItem;
            private CoreGraphicsPath m_bckgPath;
            private float m_Flatness;
            public float Flatness
            {
                get { return m_Flatness; }
                set
                {
                    if ((m_Flatness != value) && (value >= 0) && (value <= 1.0f))
                    {
                        m_Flatness = value;
                        this.BuildResult();
                    }
                }
            }
            public Mecanism()
            {
                this.m_Flatness = 1.0f;
            }
            private void BuildResult()
            {
                CoreGraphicsPath vp = m_bckgPath.Clone() as CoreGraphicsPath ;
                Matrix m = new Matrix();
                ICorePen p = this.m_flattenItem.StrokeBrush;
                vp.Widen (p, m, Flatness);
                m.Dispose();
                this.m_flattenItem.m_path.Dispose();
                this.m_flattenItem.m_path = vp;
                this.m_flattenItem.InitElement();
                this.Invalidate();
            }
            public override void Dispose()
            {
                this.DisposeBackUp();
                base.Dispose();
            }

            public override void Render(ICoreGraphics e)
            {
                if (this.m_flattenItem != null)
                {
                    object v_state = e.Save();
                    this.ApplyCurrentSurfaceTransform(e);
                    
                    this.m_flattenItem.Draw(e );
                    e.Restore(v_state);
                }
            }
            protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<PathElement> e)
            {
                
                base.OnElementChanged(e);
                if (this.Element != null)
                {
                    this.BuildBCK();
                 }
            }
            private void BuildBCK()
            {
                this.DisposeBackUp();
                this.m_flattenItem = new WidenPath(this);
                this.m_bckgPath = this.Element.GetPath().Clone () as CoreGraphicsPath ;
                this.m_flattenItem.m_path = m_bckgPath.Clone() as CoreGraphicsPath;
                this.CurrentSurface.ElementToConfigure = this.m_flattenItem;
                this.m_flattenItem.StrokeBrush.BrushDefinitionChanged += new EventHandler(StrokeBrush_BrushDefinitionChanged);
            }
            private void DisposeBackUp()
            {
                if (this.m_flattenItem != null)
                {
                    this.m_flattenItem.StrokeBrush.BrushDefinitionChanged -= new EventHandler(StrokeBrush_BrushDefinitionChanged);          
                    this.m_flattenItem.Dispose();
                    this.m_flattenItem = null;
                }
                if (this.m_bckgPath != null)
                {
                    this.m_bckgPath.Dispose();
                    this.m_bckgPath = null;
                }
            }
            void StrokeBrush_BrushDefinitionChanged(object sender, EventArgs e)
            {
                this.BuildResult();
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                {
                    if (this.Element == null)
                    {
                        this.SelectElement(e.FactorPoint);
                    }
                }
            }
            public override void EndEdition()
            {
                this.DisposeBackUp();
                base.EndEdition();
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
            }

            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[enuKeys.Enter] = new ApplyWidenAction();
                this.Actions[enuKeys.Escape] = new EscapeWidenAction();
            }

            class ApplyWidenAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism m = this.Mecanism as Mecanism;
                    if ((m.Element != null) && (m.m_flattenItem != null))
                    {
                        m.Element.SetDefinition(
                            m.m_flattenItem.m_path);
                        m.BuildBCK();
                        m.Invalidate();
                        return true;
                    }
                    return false;
                }
            }
            class EscapeWidenAction : CoreMecanismActionBase
            {
                protected override bool PerformAction()
                {
                    Mecanism m = this.Mecanism as Mecanism;
                    if ((m.Element != null) && (m.m_flattenItem != null))
                    {
                        m.CurrentLayer.Select(null);
                        m.DisposeBackUp();
                        m.CurrentSurface.ElementToConfigure = null;
                        m.Invalidate();
                        return true;
                    }
                    return false;
                }
            }
          
        }

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }




        public void Visit(ICore2DDrawingVisitor visitor)
        {
           // throw new NotImplementedException();
        }
    }
}

