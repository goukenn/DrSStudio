

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RtfTextZoneElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System; using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Text
{
    [Core2DDrawingGroupElement("TextRtf",CoreConstant.GROUP_TEXT ,  typeof(Mecanism))]
    class RtfTextZoneElement : RectangleElement, ICore2DDrawingVisitable, ICore2DDrawingRTFElement
    {
        private string m_RtfText;

        public RtfTextZoneElement()
        {
            this.m_RtfText = @"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang2060{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
{\*\generator Riched20 6.3.9600}\viewkind4\uc1 
\pard\sa200\sl276\slmult1\f0\fs22\lang12\par
}
";
        }

        [CoreXMLElement]
        [CoreXMLDefaultAttributeValue (@"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang2060{\fonttbl{\f0\fnil\fcharset0 Calibri;}}
{\*\generator Riched20 6.3.9600}\viewkind4\uc1 
\pard\sa200\sl276\slmult1\f0\fs22\lang12\par
}
")]
        /// <summary>
        /// get or set the rtf ext
        /// </summary>
        public string RtfText
        {
            get { return m_RtfText; }
            set
            {
                if (m_RtfText != value)
                {
                    m_RtfText = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition );
                }
            }
        }
        new class Mecanism : RectangleElement.Mecanism
        { 

        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            if (visitor != null)
                return true;
            return false;
        }

        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public override bool CanReSize
        {
            get
            {
                return base.CanReSize;
            }
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var p = base.GetParameters(parameters);
            var g = p.AddGroup("Text");
            ICoreRtfControl c = CoreControlFactory.CreateControl("IGKXRtfZone") as ICoreRtfControl;

            if (c != null)
            {
                c.Rtf = this.RtfText;
                c.TextChanged += (o, e) => { this.RtfText = c.Rtf; };
                g.AddItem("RtfText", "lb.RtfText", c);
            }
            return p;
        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {
            object obj = visitor.Save();

            visitor.SetupGraphicsDevice(this);
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend );
            visitor.InterpolationMode = enuInterpolationMode.Hight;
            Rectanglef r = this.Bounds;
            visitor.DrawRectangle(Colorf.Black, r.X, r.Y, r.Width, r.Height);
            visitor.Visit(this, typeof(ICore2DDrawingRTFElement));//.DrawRtf(this.RtfText, this.Bounds);
            visitor.Restore(obj);
        }
    }
}
