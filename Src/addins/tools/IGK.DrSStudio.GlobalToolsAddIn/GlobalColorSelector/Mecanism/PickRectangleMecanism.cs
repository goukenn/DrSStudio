

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PickRectangleMecanism.cs
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
file:PickRectangleMecanism.cs
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
using System; using IGK.ICore.WinCore;
using IGK.ICore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WinUI;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    [Core2DDrawingStandardElement("PickRectangle", typeof (Mecanism), IsVisible=false )]
    sealed class PickRectangleMecanism : RectangleElement 
    {
        new internal class Mecanism : RectangleElement.Mecanism
        {
            ICoreWorkingMecanism m_parent;
            private Rectanglef m_selectedRectangle;
            public event EventHandler SelectionComplete;
            public event EventHandler SelectionChanged;
            public event EventHandler SelectionAbort;
            /// <summary>
            /// get the selected rectangle
            /// </summary>
            public Rectanglef SelectedRectangle{
                get{
                    return this.m_selectedRectangle ;
                }
            }
            public Mecanism(ICoreWorkingMecanism parent)
            {
                this.m_parent = parent;
                this.m_parent.Freeze();
                this.Register(this.m_parent.CurrentSurface as ICore2DDrawingSurface );
                ICoreHelpWorkbench v = CoreSystem.GetWorkbench<ICoreHelpWorkbench>();
                if (v != null)
                    v.OnHelpMessage("tip.mecanism.pickrectangle".R());// Pick Rectangle");
            }
            
            public override bool Register(ICore2DDrawingSurface t)
            {
                if (base.Register(t))
                {

                    //inline matching variable technique
                    if (t is ICoreWorkingToolManagerSurface v_s)
                    {
                        v_s.CurrentToolChanged += new EventHandler(Surface_CurrentToolChanged);
                    }
                    return true;
                }
                return false;
            }
            public override bool UnRegister()
            {
                if (this.CurrentSurface is ICoreWorkingToolManagerSurface v_s)
                {
                    v_s.CurrentToolChanged -= new EventHandler(Surface_CurrentToolChanged);
                }
                return base.UnRegister();
            }

            protected override void OnFreeFreezed()
            {
                //base.OnFreeFreezed();
               
            }

            void Surface_CurrentToolChanged(object sender, EventArgs e)
            {
                this.UnRegister();
                this.Abort();
            }
            private void Abort()
            {
                this.SelectionAbort?.Invoke(this, EventArgs.Empty);
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                {
                    this.StartPoint = e.FactorPoint;
                    this.EndPoint = e.FactorPoint;
                    this.State = ST_EDITING;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)            
            {
                if (e.Button == enuMouseButtons.Left)
                {            
                    this.EndPoint = e.FactorPoint;
                    this.State = ST_NONE;
                    
                    this.m_selectedRectangle = CoreMathOperation .GetBounds (this.StartPoint,this.EndPoint );
                    this.OnSelectionComplete(EventArgs.Empty);
                    this.UnRegister();
                    this.m_parent.UnFreeze();

                   // this.CurrentSurface.ElementToConfigure = this.m_parent.Element; //.RefreshScene();
                    this.CurrentSurface.RefreshScene();

                }    
            }            
            private void OnSelectionComplete(EventArgs e)
            {
                this.SelectionComplete?.Invoke(this, e);
            }
            private void OnSelectionChanged(EventArgs e)
            {
                this.SelectionChanged?.Invoke(this, e);
            } 
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                if (e.Button == enuMouseButtons.Left)
                {
                    this.EndPoint = e.FactorPoint;
                    this.m_selectedRectangle = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                    OnSelectionChanged(EventArgs.Empty);
                    this.CurrentSurface.RefreshScene();
                }                
            }
            public override void Render(ICoreGraphics device)
            {
               
                if (State == ST_EDITING)
                {
                    Rectanglei rc =Rectanglef.Round ( CurrentSurface.GetScreenBound ( CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint )));

                    WinCoreControlPaint.DrawSelectionRectangle(device, rc.X, rc.Y , rc.Width , rc.Height );
                }
            }
            public  override void EndEdition()
            {
            }
        }
    }
}

