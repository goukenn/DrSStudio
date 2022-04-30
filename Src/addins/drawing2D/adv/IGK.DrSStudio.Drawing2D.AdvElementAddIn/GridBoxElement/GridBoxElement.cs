

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GridBoxElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.Codec;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore;
    [Core2DDrawingStandardElement ("GridBox", typeof (Mecanism))]
    public class GridBoxElement : RectangleElement , ICore2DDrawingVisitable 
    {
        private CoreUnit m_Small;
        private CoreUnit m_Large;
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue("10px")]
        [CoreConfigurableProperty(Group=CoreConstant.PARAM_DEFINITION)]
        public CoreUnit Large
        {
            get { return m_Large; }
            set
            {
                if (m_Large != value)
                {
                    m_Large = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue("1px")]
        [CoreConfigurableProperty(Group = CoreConstant.PARAM_DEFINITION)]
        public CoreUnit Small
        {
            get { return m_Small; }
            set
            {
                if (m_Small != value)
                {
                    m_Small = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public GridBoxElement()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Large = "10px";
            this.m_Small = "1px";
        }
    /// <summary>
    /// override the brush support
    /// </summary>
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Fill |  enuBrushSupport.Stroke | enuBrushSupport.Solid;
            }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode== enuBrushMode.Fill )
                return this.FillBrush;
            return this.StrokeBrush;
        }
        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (visitor == null)
                return;
            float v_sm = this.Small.GetValue(enuUnitType.px);
            float v_lm = this.Large.GetValue(enuUnitType.px);
            object obj = visitor.Save();

            Matrix m = visitor.GetCurrentTransform();

            Vector2f c = new Vector2f(v_sm, v_lm);
            m.TransformVectors (new Vector2f[]{c});
           c =  c.TransformVector(m);
            //c = c.Transform(m);
            visitor.MultiplyTransform(GetMatrix(), enuMatrixOrder.Prepend);
            if (c.X > 5.0f)
            {
                DrawGrid(visitor, true, c.X);
            }
            if ((c.Y > 5.0f) && (c.Y>c.X))
            {
                DrawGrid(visitor, false, c.Y);
            }
            m.Dispose();
            var b = this.Bounds ;
            visitor.DrawRectangle(this.StrokeBrush.Colors[0],
                b.X,
                b.Y,
                b.Width,
                b.Height);

            visitor.Restore(obj);
        }

        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);

            return parameters;
        }
        private void DrawGrid(ICoreGraphics g, bool dashed, float size)
            {
                float v_pixelMinSize = size;
                if (v_pixelMinSize <= 5.0f)
                    return;
                Rectanglef v_rc = this.Bounds;
                float v_offsetx = v_rc.X ;
                enuDashStyle v_dash = dashed ? enuDashStyle.Dot : enuDashStyle.Solid;
            float v_offsety = v_rc.Y;
                Colorf v_cl = this.FillBrush.Colors[0];
                float h = 0.0f;
                //float ex = this.CurrentSurface.PosX + (this.CurrentSurface.CurrentDocument.Width * ZoomX);
                //float ey = this.CurrentSurface.PosY + (this.CurrentSurface.CurrentDocument.Height * ZoomY);
                object v_state = g.Save();
                g.SmoothingMode = enuSmoothingMode.None;
    
                //g.TranslateTransform(v_offsetx, v_offsety, MatrixOrder.Append);
                //draw conventional grid according to one grid per pixel
                float end = v_rc.Right;// v_rc.X + v_rc.Width;
                ////float h = (float)Math.Ceiling (v_offsetx % v_pixelMinSize);
                //h = (float)Math.Floor(v_offsetx % v_pixelMinSize);
                h = 0;
                for (float i = v_pixelMinSize + (v_rc.X) - h; i < end; i += v_pixelMinSize)
                {
                    if (i == 0) 
                        continue;
                    g.DrawLine(v_cl,
                        1,
                        v_dash,
                        i,
                        v_rc.Y,
                        i,
                        (v_rc.Y + v_rc.Height));
                }
                end = v_rc.Bottom;// v_rc.Y + v_rc.Height;
                //h = (float)Math.Floor(v_offsety % v_pixelMinSize);
                h = 0;
                for (float i = v_pixelMinSize + (v_rc.Y) - h; i < end; i += v_pixelMinSize)
                {
                    if (i == 0) continue;
                    g.DrawLine(v_cl,
                             1,
                        v_dash,
                        v_rc.X,
                        i,
                        (v_rc.X + (v_rc.Width)),
                        i);
                }
                
                g.ExcludeClip(v_rc);
                g.Restore(v_state);
            }
        public new class Mecanism : RectangleElement.Mecanism
        { 

        }
    }
}
