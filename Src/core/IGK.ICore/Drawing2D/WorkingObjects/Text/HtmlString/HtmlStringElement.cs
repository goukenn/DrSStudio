

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlStringElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI.Configuration;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingGroupElement("HtmlString", CoreConstant.GROUP_TEXT, typeof (Mecanism ))]
    public class HtmlStringElement : TextElementBase, ICore2DDrawingVisitable, ICoreTextElement
    {
        HtmlStringSegmentCollections m_Segments;

        public event EventHandler MeasureRequired;

        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (MeasureRequired != null)
                this.MeasureRequired(this, EventArgs.Empty);
            base.OnPropertyChanged(e);
        }
        /// <summary>
        /// get or set the segments
        /// </summary>
        public HtmlStringSegmentCollections Segments {
            get {
                return this.m_Segments;
            }
        }
        [CoreXMLElement()]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (base.Text != value)
                {
                    m_Segments.Load(value);
                    base.Text = value;
                }
            }
        }

        public class HtmlStringSegmentCollections : IEnumerable 
        {
            private HtmlStringElement m_owner;
            private List<HtmlStringSegment> m_segments;
            private Rectanglef m_Bounds;

            public Rectanglef Bounds { get { return this.m_Bounds; } }

            public HtmlStringSegmentCollections(HtmlStringElement owner)
            {
                this.m_owner = owner;
                this.m_segments = new List<HtmlStringSegment>();
            }
            public void Add(HtmlStringSegment segment)
            {
                this.m_segments.Add(segment);
            }
            public void Remove(HtmlStringSegment segment)
            {
                this.m_segments.Remove(segment);
            }
            public void Clear() {
                foreach (var item in this.m_segments)
                {
                    item.Release();
                }
                this.m_segments.Clear();
            }
            public int Count { get { return this.m_segments.Count; } }
            public void InitLayout()
            {
                Rectanglef rc = Rectanglef.Empty;
                Rectanglef b = this.m_owner.Bounds;
                float x = 0.0f;
                float y = 0.0f;
                float h = 0.0f;
                float w = 0.0f;
                foreach (HtmlStringSegment item in this.m_segments)
                {
                    item.MeasureSegment();
                    w += item.Width;
                    h = Math.Max(h,item.VirtualLineHeight);
                }
                this.m_Bounds = new Rectanglef(x, y, w, h); 
            }
            /// <summary>
            /// get the enumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return this.m_segments.GetEnumerator();
            }

            internal void Load(string value)
            {//load string value
                this.Clear();
                if (string.IsNullOrEmpty(value))
                    return;
                var g =  IGK.ICore.Xml.CoreXmlElement.CreateXmlNode("dummy");
                //remove line fied an replace symbol.
                value = value.Replace("\n", string.Empty).Replace("\r", string.Empty);
                g.LoadString(value);

                string v_c = CoreXmlUtility.GetStringValue(g.Content);
                if (!string.IsNullOrEmpty(v_c))
                {

                    HtmlStringSegment sg = HtmlStringSegment.CreateSegment(this.m_owner, string.Empty);
                    this.m_segments.Add(sg);
                    sg.Load(g);
                }
                else
                {
                    foreach (IGK.ICore.Xml.CoreXmlElementBase c in g.Childs)
                    {
                        //if (!string.IsNullOrEmpty(c.TagName))
                        //{
                        HtmlStringSegment sg = HtmlStringSegment.CreateSegment(this.m_owner, c.TagName);
                        this.m_segments.Add(sg);
                        sg.Load(c);
                        //}

                    }
                }
            }
        }
        public HtmlStringElement():base()
        {
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Segments = new HtmlStringSegmentCollections(this);
#if DEBUG
            this.Text = @"text";
#endif 
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            p.Reset();
            p.AddRectangle(this.Bounds);
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            return base.GetParameters(parameters);
        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {
            Rectanglef b = this.Bounds;
            float  x = 0.0f;
            float y = 0.0f; //virtual line height
            float W = b.Width ;
            x = b.X ;
            y = b.Y;
            //get All string measurement
            Rectanglef bc =  this.Segments.Bounds;
            object obj = visitor.Save();
            visitor.SetupGraphicsDevice(this);
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
            float v_defaultFontHeight = this.Font.GetLineHeight();
            HtmlStringElementRenderingEventArgs e = new HtmlStringElementRenderingEventArgs(this.Bounds,
                v_defaultFontHeight, x, y);
            visitor.SetClip(this.Bounds);
            foreach (HtmlStringSegment item in this.Segments)
            {        
                    item.Render(visitor,e);
            }
            visitor.Restore(obj);
        }

        new class Mecanism : RectangleElement.Mecanism
        { 
        }

    }
}
