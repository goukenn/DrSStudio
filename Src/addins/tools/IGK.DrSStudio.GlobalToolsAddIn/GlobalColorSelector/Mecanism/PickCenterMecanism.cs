

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PickCenterMecanism.cs
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
file:PickCenterMecanism.cs
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
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
        [Core2DDrawingStandardElement("PickCenter", typeof(Mecanism), IsVisible = false)]
        sealed class PickCenterMecanism : RectangleElement
        {
            new internal class Mecanism : RectangleElement.Mecanism
            {
                ICoreWorkingMecanism m_parent;
                private Vector2f  m_selectedCenter;
                public event EventHandler SelectionComplete;
                public event EventHandler SelectionChanged;
                public event EventHandler SelectionAbort;
                /// <summary>
                /// get the selected rectangle
                /// </summary>
                public Vector2f SelectedCenter
                {
                    get
                    {
                        return this.m_selectedCenter;
                    }
                }
                public Mecanism(ICoreWorkingMecanism parent)
                {
                    this.m_parent = parent;
                    this.m_parent.Freeze();
                    this.Register(this.m_parent.CurrentSurface as ICore2DDrawingSurface);
                    ICoreHelpWorkbench v = CoreSystem.GetWorkbench<ICoreHelpWorkbench>();
                    if (v != null)
                        v.OnHelpMessage("Pick Center");
                }
                public override bool Register(ICore2DDrawingSurface t)
                {
                    if (base.Register(t))
                    {
                        ICoreWorkingToolManagerSurface v_s = t as ICoreWorkingToolManagerSurface;
                        if (v_s != null)
                        {
                            v_s.CurrentToolChanged += new EventHandler(surface_CurrentToolChanged);
                        }
                        return true;
                    }
                    return false;
                }
                public override bool UnRegister()
                {
                    ICoreWorkingToolManagerSurface v_s = this.CurrentSurface as ICoreWorkingToolManagerSurface;
                    if (v_s != null)
                    {
                        v_s.CurrentToolChanged -= new EventHandler(surface_CurrentToolChanged);
                    }
                    return base.UnRegister();
                }
              
                void surface_CurrentToolChanged(object sender, EventArgs e)
                {
                    this.UnRegister();
                    this.Abort();
                }
                private void Abort()
                {
                    if (this.SelectionAbort != null)
                        this.SelectionAbort(this, EventArgs.Empty);
                }
                protected override void  OnMouseClick(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            {
                                this.State = ST_NONE;
                                this.OnSelectionComplete(EventArgs.Empty);
                                UnRegister();
                                var v = (CoreSystem.GetWorkbench<ICoreHelpWorkbench>());
                                if (v != null)
                                    v.OnHelpMessage (CoreConstant.MSG_DONE.R());
                                this.m_parent.UnFreeze();
                            }
                            break;
                        case enuMouseButtons.Right :
                            CancelCenterSelection();
                            break;
                    }
                }
                private void CancelCenterSelection()
                {
                    UnRegister();
                    ICoreHelpWorkbench v = CoreSystem.GetWorkbench<ICoreHelpWorkbench>();
                    if (v != null)
                        v.OnHelpMessage("Cancel");
                    this.m_parent.UnFreeze();
                }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                }
                protected override void OnMouseUp(CoreMouseEventArgs e)
                {
                }
                private void OnSelectionComplete(EventArgs e)
                {
                    if (this.SelectionComplete != null)
                        this.SelectionComplete(this, e);
                }
                private void OnSelectionChanged(EventArgs e)
                {
                    if (this.SelectionChanged != null)
                        this.SelectionChanged(this, e);
                }
                protected override void OnMouseMove(CoreMouseEventArgs e)
                {
                  this.m_selectedCenter = e.FactorPoint ;
                    OnSelectionChanged (EventArgs.Empty );
                }
               
                public  override void EndEdition()
                {
                }
            }
        }    
}

