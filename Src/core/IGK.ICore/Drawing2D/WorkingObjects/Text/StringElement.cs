

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: StringElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D
{

    /// <summary>
    /// represent a string to render a single line string
    /// </summary>
    [Core2DDrawingGroupElement("String",
        CoreConstant.GROUP_TEXT,
     typeof(Mecanism),
     Keys = enuKeys.Shift | enuKeys.T)]
    public class StringElement :  TextElement , ICore2DDrawingVisitable 
    {
        private enuTextRenderingMode m_TextRenderingHint;
        private bool m_IsMnemonic;
        private enuTextTrimming m_Trimming;
        [IGK.ICore.Codec.CoreXMLAttribute()]
        [IGK.ICore.Codec.CoreXMLDefaultAttributeValue(enuTextTrimming.None)]
        [IGK.ICore.WinUI.Configuration.CoreConfigurableProperty(Group = "RenderingHint")]
        public enuTextTrimming Trimming
        {
            get { return m_Trimming; }
            set
            {
                if (m_Trimming != value)
                {
                    m_Trimming = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [IGK.ICore.Codec.CoreXMLAttribute()]
        [IGK.ICore.Codec.CoreXMLDefaultAttributeValue(false)]
        [IGK.ICore.WinUI.Configuration.CoreConfigurableProperty(Group = "RenderingHint")]
        public bool IsMnemonic
        {
            get { return m_IsMnemonic; }
            set
            {
                if (m_IsMnemonic != value)
                {
                    m_IsMnemonic = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        public StringElement()
        {
            this.m_TextRenderingHint = enuTextRenderingMode.GridFitAntiAliazed;
        }
        [IGK.ICore.Codec.CoreXMLAttribute ()]
        [IGK.ICore.Codec.CoreXMLDefaultAttributeValue (enuTextRenderingMode.GridFitAntiAliazed )]
        [IGK.ICore.WinUI.Configuration.CoreConfigurableProperty(Group="RenderingHint")]
        public enuTextRenderingMode TextRenderingHint
        {
            get { return m_TextRenderingHint; }
            set
            {
                if (m_TextRenderingHint != value)
                {
                    m_TextRenderingHint = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            p.AddRectangle (this.Bounds);
        }
        public override Rectanglef GetBound()
        {
            return this.GetPath().GetBounds();
        }
        public override bool Contains(Vector2f point)
        {
            return base.Contains(point);
        }
        public override bool IsOutiliseVisible(Vector2f point)
        {
            return base.IsOutiliseVisible(point);
        }

        public new class Mecanism : RectangleElement.Mecanism 
        {
          
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
        }

        public void VisitShadow(ICore2DDrawingVisitor visitor)
        {
            if (this.AllowShadow)
            {
                //render shadow property
                object info = visitor.Save();
                visitor.SetupGraphicsDevice(this);
                visitor.TextRenderingMode = this.TextRenderingHint;
                visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
                visitor.TranslateTransform(this.ShadowProperty.Offset.X, this.ShadowProperty.Offset.Y, enuMatrixOrder.Append);
                visitor.DrawString(this.Text, this.Font,
                        this.ShadowProperty.Brush, this.Bounds,
                        this.Trimming,
                        this.IsMnemonic);
                visitor.Restore(info);
            }

        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {
            object obj = visitor.Save();
            visitor.SetupGraphicsDevice(this);
            visitor.TextRenderingMode = this.TextRenderingHint;
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
            visitor.DrawString(this.Text, this.Font,
                this.FillBrush, this.Bounds,
                this.Trimming, 
                this.IsMnemonic);
            visitor.Restore(obj);
        }
        public override bool CanRenderShadow
        {
            get
            {
                return true;
            }
        }
        protected internal override void Scale(float ex, float ey, Vector2f endLocation, enuMatrixOrder order, bool temp)
        {
            base.Scale(ex, ey, endLocation, order, temp);
        }
    }
}
