

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WireElement.cs
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
file:WireElement.cs
*/
using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    [ConnectorCatElement ("Wire", typeof (Mecanism), ImageKey="Menu_joint")]
    /// <summary>
    /// represent a simple wire wire element
    /// </summary>
    public class WireElement : Core2DDrawingLayeredElement, ICore2DDrawingElement, ICore2DDrawingObject, 
        ICoreWorkingObject, ICoreIdentifier, ICloneable,
        IConnectorElement 
    {
        private ICore2DDrawingLayeredElement  m_StartElement;
        private ICore2DDrawingLayeredElement m_TargetElement;
        private ICorePen  m_StrokeBrush;
        public WireElement()
        {
            this.m_StrokeBrush = new CorePen(this);
            this.m_StrokeBrush.SetSolidColor(Colorf.Black);
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        /// <summary>
        /// get the stroke brush
        /// </summary>
        public ICorePen  StrokeBrush
        {
            get { return m_StrokeBrush; }
        }
public ICore2DDrawingLayeredElement TargetElement{
get{ return m_TargetElement;}
set{ 
if (m_TargetElement !=value)
{
m_TargetElement =value;
}
}
}
public ICore2DDrawingLayeredElement  StartElement{
get{ return m_StartElement;}
set{ 
if (m_StartElement !=value)
{
m_StartElement =value;
}
}
}
        public override void  Draw(System.Drawing.Graphics g)
{
    if ((this.TargetElement == null) || (this.StartElement == null))
        return;
    Rectanglef rc1 = this.StartElement.GetBound();
    Rectanglef rc2 = this.TargetElement.GetBound();
    Vector2f c1 = CoreMathOperation.GetCenter(rc1);
    Vector2f c2 = CoreMathOperation.GetCenter(rc2);
            System.Drawing.Pen v_pen = this.StrokeBrush .GetPen();
            if (v_pen != null)
            {
                System.Drawing.Drawing2D.GraphicsState s = g.Save();
                this.SetGraphicsProperty(g);
                g.Clip.Exclude(rc1);
                g.Clip.Exclude(rc2);
                g.DrawLine(v_pen, c1, c2);
                g.Restore(s);
            }
}
        public override enuBrushSupport BrushSupport
        {
            get {
                return enuBrushSupport.StrokeOnly;
            }
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public static WireElement CreateElement(ICore2DDrawingLayeredElement startElement,
            ICore2DDrawingLayeredElement targetElement)
        {
            WireElement l = new WireElement();
            l.m_StartElement = startElement;
            l.m_TargetElement = targetElement;
            return l;
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return this.StrokeBrush;    
        }
        public class Mecanism : Core2DDrawingMecanismBase
        {
            const int DEF_START = 1;
            const int DEF_TARGET = 2;
            const int DEF_NONE = 0;
            int m_def = DEF_NONE;
            ICore2DDrawingLayeredElement m_start;
            ICore2DDrawingLayeredElement m_target;
            ICore2DDrawingLayeredElement[] m_presents;
            protected override void GenerateSnippets()
            {
            }
            protected override void InitSnippetsLocation()
            {
            }
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                base.OnPaint(e);
                if (m_def != DEF_NONE)
                {
                    Rectanglef rcBound = Rectanglef.Empty;
                    e.Graphics.DrawLine(
                        System.Drawing.Pens.Aqua,
                        CurrentSurface.GetScreenLocation(this.StartPoint),
                        CurrentSurface.GetScreenLocation(this.EndPoint));
                    if (m_target != null)
                    {
                        rcBound = CurrentSurface.GetScreenBound (this.m_target.GetSelectionBound());
                        e.Graphics.DrawRectangle(
                            System.Drawing.Pens.Red ,
                            Rectanglef.Round(rcBound));
                    }
                    if (m_start != null)
                    {
                        rcBound = CurrentSurface.GetScreenBound (this.m_start.GetSelectionBound());
                        e.Graphics.DrawRectangle(
                            System.Drawing.Pens.Blue,
                            Rectanglef.Round(rcBound));
                    }
                }
            }
            void Init()
            {
                this.m_target = null;
                this.m_start = null;
                this.m_def = DEF_NONE;
                this.m_presents = null;
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case System.Windows.Forms.MouseButtons.Left :
                        if (m_def == DEF_NONE)
                        {
                            SelectOne(e.FactorPoint);
                            if (this.CurrentLayer .SelectedElements .Count == 1)
                            {
                                this.m_start = this.CurrentLayer.SelectedElements[0];
                                this.m_target = null;
                                m_def = DEF_START;
                                this.m_presents = this.CurrentLayer.Elements.ToArray();
                                this.StartPoint = e.FactorPoint;
                                this.EndPoint = e.FactorPoint;
                            }
                        }
                        break;
                }
            }
            ICore2DDrawingLayeredElement  GetTarget(Vector2f ipoint)
            {
                if ((m_presents == null)||(m_presents.Length == 0))
                    return null;
                for (int i = m_presents.Length - 1; i >=0; i--)
                {
                    if (( this.m_presents[i] != m_start ) && this.m_presents[i].Contains(ipoint))
                    { 
                        return m_presents[i];
                    }
                }
                return null;
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                { 
                    case System.Windows.Forms.MouseButtons.Left :
                        if (m_def == DEF_START)
                        {
                            this.m_target = this.GetTarget(e.FactorPoint);
                            this.EndPoint = e.FactorPoint;
                            this.CurrentSurface.Invalidate();
                        }
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        if (m_def == DEF_START)
                        {
                           Tools.WireSurfaceManager t = 
                               Tools.WireManagerTool.Instance.GetSurfaceManager(this.CurrentSurface );
                            if (t.Contains (m_start , m_target )== false )
                            {
                                IConnectorElement cl = CreateElement(
                                this.m_start,
                                this.m_target);
                                if (cl != null)
                                {
                                    this.CurrentLayer.Elements.Add(cl);
                                    t.Add(m_start, m_target, cl);
                                }
                            }
                            this.CurrentSurface.Invalidate();
                            this.Init();
                        }
                        break;
                }
            }
        }
    }
}

