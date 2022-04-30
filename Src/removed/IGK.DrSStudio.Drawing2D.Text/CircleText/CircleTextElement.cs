

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CircleTextElement.cs
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
file:CircleTextElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace IGK.DrSStudio.Drawing2D.Text
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    [TextElementCategory("CircleText2", typeof(Mecanism), ImageKey = "DE_TextCircle")]
    sealed class CircleTextElement : TextElement 
    {
        private enuTextOrientation m_Orientation;
        private float m_ArcAngle;
        private enuTextDirection m_Direction;
        public enuTextDirection Direction
        {
            get { return m_Direction; }
            set
            {
                if (m_Direction != value)
                {
                    m_Direction = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(360.0f)]
        public float ArcAngle
        {
            get { return m_ArcAngle; }
            set
            {
                if (m_ArcAngle != value)
                {
                    m_ArcAngle = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (enuTextOrientation.Horizontal )]
        public enuTextOrientation Orientation
        {
            get { return m_Orientation; }
            set
            {
                if (m_Orientation != value)
                {
                    m_Orientation = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public CircleTextElement()
        {
        }
        public override bool Contains(Vector2f position)
        {
            return this.GetBound().Contains(position);
            //return base.Contains(position);
        }
        protected override void GeneratePath()
        {
            GraphicsPath v_path = new GraphicsPath();
            v_path.FillMode = this.FillMode;
            this.SetPath (v_path);
        }
        public override void Draw(System.Drawing.Graphics g)
        {
            string v_txt = this.Content;
            if (string.IsNullOrEmpty (v_txt))
                return ;
            GraphicsState v_state = g.Save();
            this.SetGraphicsProperty(g);
            g.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend );
            System.Drawing.Font v_ft =  Font.GetFont();
            StringFormat v_format = this.GetNewStringFormat();
            SizeF v_s = g.MeasureString(v_txt, v_ft, Vector2f.Zero, v_format );
            float v_w = v_s.Width;
            float v_h = v_s.Height;
            Brush v_br = this.FillBrush.GetBrush ();
            float v_x = this.Bound.X;
            float v_y = this.Bound.Y;
            float v_lineh = this.SingleLineBound.Height;
            float v_linew = g.MeasureString("_", v_ft).Width;
            switch (this.Orientation)
            {
                case enuTextOrientation.Horizontal:
                    foreach (string l in this.Lines)
                    {
                        g.DrawString(l, v_ft, v_br, new Rectanglef(v_x, v_y, v_w, v_h), v_format);
                        v_y += v_lineh;
                    }
                    break;
                case enuTextOrientation.Vertical:
                    foreach (string l in this.Lines)
                    {
                        for (int i = 0; i < l.Length; i++)
                        {
                            g.DrawString(l[i].ToString (), v_ft, v_br, new Rectanglef(v_x, v_y, v_w, v_h));
                            v_y += v_lineh;
                        }
                        v_x += v_linew;
                        v_y = this.Bound.Y;
                    }
                    break;
                case enuTextOrientation.Arc:
                    switch (this.Direction)
	                {
                        case enuTextDirection.ClockWize:
                            break;
                        case enuTextDirection.AntiClockWize:
                            break;
                        default:
                            break;
	                }
                    break;
                case enuTextOrientation.Circle:
                    break;
                default:
                    break;
            }
            g.Restore(v_state);
        }
        public override DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return base.GetConfigType();
        }
        public override DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var g  = parameters.AddGroup("Definition");
            g.AddItem(this.GetType().GetProperty("Orientation"));
            g.AddItem(this.GetType().GetProperty("Direction"));
            return parameters;
        }
        new class Mecanism : TextElement.Mecanism
        { 
        }
    }
}

