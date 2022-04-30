

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PickColorMecanism.cs
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
file:PickColorMecanism.cs
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
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio
{
    using IGK.ICore.Drawing2D;
    using IGK.ICore.WinUI;
    using IGK.ICore.Drawing2D.WinUI;
        [Core2DDrawingStandardElement("PickColor", typeof(Mecanism), IsVisible = false)]
        sealed class PickColorMecanism : RectangleElement
        {
            private PickColorMecanism() { }
            new internal class Mecanism : RectangleElement.Mecanism
            {
                private ICoreWorkingMecanism m_parent;
                public event EventHandler SelectedColorChanged;
                public event EventHandler SelectionAbort;
                /// <summary>
                /// parent mecanism
                /// </summary>
                public ICoreWorkingMecanism Parent {
                    get {
                        return this.m_parent;
                    }
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
                        this.SendHelpMessage("tip.Pickcolor".R());
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
                public Mecanism(ICoreWorkingMecanism parent)
                {
                    this.m_parent = parent;
                    this.m_parent.Freeze(); // free the parent
                    this.Register(this.m_parent.CurrentSurface as ICore2DDrawingSurface );
                }
                private Colorf m_SelectedColor;
                public Colorf SelectedColor
                {
                    get { return m_SelectedColor; }
                    set
                    {
                        if (!m_SelectedColor.Equals (value))
                        {
                            m_SelectedColor = value;
                        }
                    }
                }
                void OnSelectedColorChanged(EventArgs e)
                {
                    if (this.SelectedColorChanged != null)
                        this.SelectedColorChanged(this, e);
                }
               
                protected override void OnMouseClick(CoreMouseEventArgs e)
                {
                    switch (e.Button)
                    {
                        case enuMouseButtons.Left :
                            //get color
                            Vector2f vc = this.CurrentSurface.GetFactorLocation(
                                new Vector2f(e.X, e.Y));
                            Bitmap bmp = new Bitmap(1, 1);
                            Graphics g = Graphics.FromImage(bmp);
                            g.TranslateTransform(-vc.X,- vc.Y, MatrixOrder.Append);
                            this.CurrentSurface.CurrentDocument.Draw(g);
                            g.Flush();
                            g.Dispose();
                            this.m_SelectedColor = bmp.GetPixel(0, 0).CoreConvertFrom<Colorf>();
                            bmp.Dispose();
                            

                            
                            this.m_parent.UnFreeze();
                            OnSelectedColorChanged(EventArgs.Empty);
                            this.Invalidate();
                            this.UnRegister();
                            this.Dispose();
                            break;
                    }
                }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                }
                protected override void OnMouseMove(CoreMouseEventArgs e)
                {                    
                }
                protected override void OnMouseUp(CoreMouseEventArgs e)
                {
                    this.OnMouseClick(e);
                }
            }
        }    
}

